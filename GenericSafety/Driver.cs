//tabs=4
// --------------------------------------------------------------------------------

// ASCOM SafetyMonitor driver for GenericSafety
//
// Description:	Safety monitor that read the first line of a file and parses it
//              determine the safety state.
//              The line must contain an exact match of two strings, one for the safe
//              and one for the unsafe state.
//              An unrecognised line is reported as unsafe.
//              A file read error throws an exception.

// Implements:	ASCOM SafetyMonitor interface version: <To be completed by driver developer>
// Author:		(CDR) Chris Rowland <chris.rowland@cherryfield.me.uk>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// 04-Jun-2019	CDR	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//


using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.GenericSafety
{
    //
    // Your driver's DeviceID is ASCOM.GenericSafety.SafetyMonitor
    //
    // The Guid attribute sets the CLSID for ASCOM.GenericSafety.SafetyMonitor
    // The ClassInterface/None addribute prevents an empty interface called
    // _GenericSafety from being created and used as the [default] interface
    //

    /// <summary>
    /// ASCOM SafetyMonitor Driver for GenericSafety.
    /// </summary>
    [Guid("58037a22-fd1e-47e3-83ff-41a12c7f116a")]
    [ClassInterface(ClassInterfaceType.None)]
    public class SafetyMonitor : ISafetyMonitor
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.GenericSafety.SafetyMonitor";

        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "Generic SafetyMonitor Driver.";

        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        private bool connectedState;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal static TraceLogger tl;

        /// <summary>
        /// name of file used to report the safety state
        /// </summary>
        internal static string DataFile;

        internal static string SafeString;
        internal static string UnsafeString;
        internal static bool usePowerStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericSafety"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public SafetyMonitor()
        {
            tl = new TraceLogger("", "GenericSafety");
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl.LogMessage("SafetyMonitor", "Starting initialisation");

            connectedState = false; // Initialise connected to false

            tl.LogMessage("SafetyMonitor", "Completed initialisation");
        }

        //
        // PUBLIC COM INTERFACE ISafetyMonitor IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (connectedState)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm())
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
        }

        public bool Connected
        {
            get
            {
                LogMessage("Connected", "Get {0}", connectedState);
                return connectedState;
            }
            set
            {
                LogMessage("Connected", "Set {0}", value);
                if (value == connectedState)
                    return;

                if (value)
                {
                    connectedState = true;
                }
                else
                {
                    connectedState = false;
                }
            }
        }

        public string Description
        {
            get
            {
                LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                // TODO customise this driver description
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "1");
                return Convert.ToInt16("1");
            }
        }

        public string Name
        {
            get
            {
                string name = "Generic Safety Monitor";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region ISafetyMonitor Implementation
        public bool IsSafe
        {
            get
            {
                if (!connectedState)
                {
                    LogMessage("IsSafe", "not connected so IsSafe -> false");
                    return false;
                }
                try
                {
                    if (usePowerStatus)
                    {
                        var ps = SystemInformation.PowerStatus;
                        LogMessage("IsSafe", "PowerLineStatus {0}", ps.PowerLineStatus);
                        if (ps.PowerLineStatus == PowerLineStatus.Offline)
                            return false;
                    }

                    if (string.IsNullOrWhiteSpace(DataFile))
                        return true;

                    var line = ReadDataFile();
                    // line must contain an exact match to one of the test strings
                    // check unsafe string first, in case the safe string is in the unsafe string
                    if (line.Contains(UnsafeString))
                    {
                        LogMessage("IsSafe", "Line {0}: IsSafe -> false", line);
                        return false;
                    }
                    if (line.Contains(SafeString))
                    {
                        LogMessage("IsSafe", "Line {0}: IsSafe -> true", line);
                        return true;
                    }
                    LogMessage("IsSafe", "Unrecognised line {0}, assume false", line);
                    return false;
                }
                catch (Exception ex)
                {
                    LogMessage("IsSafe", "Error {0}", ex);
                    throw;
                }
            }
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "SafetyMonitor";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "SafetyMonitor";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                DataFile = driverProfile.GetValue(driverID, "DataFile", string.Empty, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\safety.txt");
                SafeString = driverProfile.GetValue(driverID, "SafeString", string.Empty, "safe");
                UnsafeString = driverProfile.GetValue(driverID, "UnsafeString", string.Empty, "unsafe");
                usePowerStatus = Convert.ToBoolean(driverProfile.GetValue(driverID, "UsePowerStatus", string.Empty, bool.FalseString));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "SafetyMonitor";
                driverProfile.WriteValue(driverID, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(driverID, "DataFile", DataFile);
                driverProfile.WriteValue(driverID, "SafeString", SafeString);
                driverProfile.WriteValue(driverID, "UnsafeString", UnsafeString);
                driverProfile.WriteValue(driverID, "UsePowerStatus", usePowerStatus.ToString());
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }
        #endregion

        private static string ReadDataFile()
        {
            try
            {
                using (TextReader tr = new StreamReader(new FileStream(DataFile, FileMode.Open,
                      FileAccess.Read, FileShare.ReadWrite)))
                {
                    var line = tr.ReadLine();
                    return line;
                }
            }
            catch (Exception ex)
            {
                LogMessage("ReadDataFile", "error: " + ex.ToString());
                return string.Empty;
            }
        }
    }
}
