using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	/// <summary>Provides interaction with Windows event logs. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000184 RID: 388
	[DefaultEvent("EntryWritten")]
	[MonitoringDescription("Represents an event log")]
	[InstallerType(typeof(EventLogInstaller))]
	public class EventLog : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Does not associate the instance with any log.</summary>
		// Token: 0x0600092B RID: 2347 RVA: 0x00030F1F File Offset: 0x0002F11F
		public EventLog()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the local computer.</summary>
		/// <param name="logName">The name of the log on the local computer. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid. </exception>
		// Token: 0x0600092C RID: 2348 RVA: 0x00030F2C File Offset: 0x0002F12C
		public EventLog(string logName)
			: this(logName, ".")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer.</summary>
		/// <param name="logName">The name of the log on the specified computer. </param>
		/// <param name="machineName">The computer on which the log exists. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.-or- The computer name is invalid. </exception>
		// Token: 0x0600092D RID: 2349 RVA: 0x00030F3A File Offset: 0x0002F13A
		public EventLog(string logName, string machineName)
			: this(logName, machineName, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLog" /> class. Associates the instance with a log on the specified computer and creates or assigns the specified source to the <see cref="T:System.Diagnostics.EventLog" />.</summary>
		/// <param name="logName">The name of the log on the specified computer </param>
		/// <param name="machineName">The computer on which the log exists. </param>
		/// <param name="source">The source of event log entries. </param>
		/// <exception cref="T:System.ArgumentNullException">The log name is null. </exception>
		/// <exception cref="T:System.ArgumentException">The log name is invalid.-or- The computer name is invalid. </exception>
		// Token: 0x0600092E RID: 2350 RVA: 0x00030F4C File Offset: 0x0002F14C
		public EventLog(string logName, string machineName, string source)
		{
			if (logName == null)
			{
				throw new ArgumentNullException("logName");
			}
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", machineName));
			}
			this.source = source;
			this.machineName = machineName;
			this.logName = logName;
			this.Impl = EventLog.CreateEventLogImpl(this);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Diagnostics.EventLog" /> receives <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event notifications.</summary>
		/// <returns>true if the <see cref="T:System.Diagnostics.EventLog" /> receives notification when an entry is written to the log; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The event log is on a remote computer.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00030FB4 File Offset: 0x0002F1B4
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00030FBC File Offset: 0x0002F1BC
		[Browsable(false)]
		[DefaultValue(false)]
		[MonitoringDescription("If enabled raises event when a log is written.")]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.doRaiseEvents;
			}
			set
			{
				if (value == this.doRaiseEvents)
				{
					return;
				}
				if (value)
				{
					this.Impl.EnableNotification();
				}
				else
				{
					this.Impl.DisableNotification();
				}
				this.doRaiseEvents = value;
			}
		}

		/// <summary>Gets or sets the name of the log to read from or write to.</summary>
		/// <returns>The name of the log. This can be Application, System, Security, or a custom log name. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00030FEA File Offset: 0x0002F1EA
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0003100F File Offset: 0x0002F20F
		[ReadOnly(true)]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.LogConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("Name of the log that is read and written.")]
		[RecommendedAsConfigurable(true)]
		public string Log
		{
			get
			{
				if (this.source != null && this.source.Length > 0)
				{
					return this.GetLogName();
				}
				return this.logName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (string.Compare(this.logName, value, true) != 0)
				{
					this.logName = value;
					this.Reset();
				}
			}
		}

		/// <summary>Gets or sets the name of the computer on which to read or write events.</summary>
		/// <returns>The name of the server on which the event log resides. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0003103B File Offset: 0x0002F23B
		[DefaultValue(".")]
		[RecommendedAsConfigurable(true)]
		[ReadOnly(true)]
		[MonitoringDescription("Name of the machine that this log get written to.")]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets or sets the source name to register and use when writing to the event log.</summary>
		/// <returns>The name registered with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The source name results in a registry key path longer than 254 characters.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00031043 File Offset: 0x0002F243
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0003104C File Offset: 0x0002F24C
		[MonitoringDescription("The application name that writes the log.")]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ReadOnly(true)]
		[RecommendedAsConfigurable(true)]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.source == null || (this.source.Length == 0 && (this.logName == null || this.logName.Length == 0)))
				{
					this.source = value;
					return;
				}
				if (string.Compare(this.source, value, true) != 0)
				{
					this.source = value;
					this.Reset();
				}
			}
		}

		/// <summary>Closes the event log and releases read and write handles.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The event log's read handle or write handle was not released successfully. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000936 RID: 2358 RVA: 0x000310B1 File Offset: 0x0002F2B1
		public void Close()
		{
			this.Impl.Close();
			this.EnableRaisingEvents = false;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000310C8 File Offset: 0x0002F2C8
		internal void Reset()
		{
			bool enableRaisingEvents = this.EnableRaisingEvents;
			this.Close();
			this.EnableRaisingEvents = enableRaisingEvents;
		}

		/// <summary>Establishes the specified source name as a valid event source for writing entries to a log on the specified computer. This method can also be used to create a new custom log on the specified computer.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="logName">The name of the log the source's entries are written to. Possible values include Application, System, or a custom event log. If you do not specify a value, <paramref name="logName" /> defaults to Application. </param>
		/// <param name="machineName">The name of the computer to register this event source with, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> is not a valid computer name.- or - <paramref name="source" /> is an empty string ("") or null.- or - <paramref name="logName" /> is not a valid event log name. Event log names must consist of printable characters, and cannot include the characters '*', '?', or '\'.- or - <paramref name="logName" /> is not valid for user log creation. The event log names AppEvent, SysEvent, and SecEvent are reserved for system use.- or - The log name matches an existing event source name.- or - The source name results in a registry key path longer than 254 characters.- or - The first 8 characters of <paramref name="logName" /> match the first 8 characters of an existing event log name on the specified computer.- or - The source cannot be registered because it already exists on the specified computer.- or - The source name matches an existing event source name. </exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened on the specified computer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000938 RID: 2360 RVA: 0x000310E9 File Offset: 0x0002F2E9
		[Obsolete("use CreateEventSource(EventSourceCreationData) instead")]
		public static void CreateEventSource(string source, string logName, string machineName)
		{
			EventLog.CreateEventSource(new EventSourceCreationData(source, logName, machineName));
		}

		/// <summary>Establishes a valid event source for writing localized event messages, using the specified configuration properties for the event source and the corresponding event log.</summary>
		/// <param name="sourceData">The configuration properties for the event source and its target event log. </param>
		/// <exception cref="T:System.ArgumentException">The computer name specified in <paramref name="sourceData" /> is not valid.- or - The source name specified in <paramref name="sourceData" /> is null.- or - The log name specified in <paramref name="sourceData" /> is not valid. Event log names must consist of printable characters and cannot include the characters '*', '?', or '\'.- or - The log name specified in <paramref name="sourceData" /> is not valid for user log creation. The Event log names AppEvent, SysEvent, and SecEvent are reserved for system use.- or - The log name matches an existing event source name.- or - The source name specified in <paramref name="sourceData" /> results in a registry key path longer than 254 characters.- or - The first 8 characters of the log name specified in <paramref name="sourceData" /> are not unique.- or - The source name specified in <paramref name="sourceData" /> is already registered.- or - The source name specified in <paramref name="sourceData" /> matches an existing event log name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceData" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000939 RID: 2361 RVA: 0x000310F8 File Offset: 0x0002F2F8
		[MonoNotSupported("remote machine is not supported")]
		public static void CreateEventSource(EventSourceCreationData sourceData)
		{
			if (sourceData.Source == null || sourceData.Source.Length == 0)
			{
				throw new ArgumentException("Source property value has not been specified.");
			}
			if (sourceData.LogName == null || sourceData.LogName.Length == 0)
			{
				throw new ArgumentException("Log property value has not been specified.");
			}
			if (EventLog.SourceExists(sourceData.Source, sourceData.MachineName))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Source '{0}' already exists on '{1}'.", sourceData.Source, sourceData.MachineName));
			}
			EventLog.CreateEventLogImpl(sourceData.LogName, sourceData.MachineName, sourceData.Source).CreateEventSource(sourceData);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.EventLog" />, and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600093A RID: 2362 RVA: 0x00031196 File Offset: 0x0002F396
		protected override void Dispose(bool disposing)
		{
			if (this.Impl != null)
			{
				this.Impl.Dispose(disposing);
			}
		}

		/// <summary>Determines whether the log exists on the specified computer.</summary>
		/// <returns>true if the log exists on the specified computer; otherwise, false.</returns>
		/// <param name="logName">The log for which to search. Possible values include: Application, Security, System, other application-specific logs (such as those associated with Active Directory), or any custom log on the computer. </param>
		/// <param name="machineName">The name of the computer on which to search for the log, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="machineName" /> parameter is an invalid format. Make sure you have used proper syntax for the computer on which you are searching.-or- The <paramref name="logName" /> is null or the value is empty. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600093B RID: 2363 RVA: 0x000311AC File Offset: 0x0002F3AC
		[MonoNotSupported("remote machine is not supported")]
		public static bool Exists(string logName, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException("Invalid format for argument machineName.");
			}
			return logName != null && logName.Length != 0 && EventLog.CreateEventLogImpl(logName, machineName, string.Empty).Exists(logName, machineName);
		}

		/// <summary>Gets the name of the log to which the specified source is registered.</summary>
		/// <returns>The name of the log associated with the specified source in the registry.</returns>
		/// <param name="source">The name of the event source. </param>
		/// <param name="machineName">The name of the computer on which to look, or "." for the local computer. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600093C RID: 2364 RVA: 0x000311E9 File Offset: 0x0002F3E9
		[MonoNotSupported("remote machine is not supported")]
		public static string LogNameFromSourceName(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'MachineName'.", machineName));
			}
			return EventLog.CreateEventLogImpl(string.Empty, machineName, source).LogNameFromSourceName(source, machineName);
		}

		/// <summary>Determines whether an event source is registered on a specified computer.</summary>
		/// <returns>true if the event source is registered on the given computer; otherwise, false.</returns>
		/// <param name="source">The name of the event source. </param>
		/// <param name="machineName">The name the computer on which to look, or "." for the local computer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="machineName" /> is an invalid computer name. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <paramref name="source" /> was not found, but some or all of the event logs could not be searched.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600093D RID: 2365 RVA: 0x00031224 File Offset: 0x0002F424
		[MonoNotSupported("remote machine is not supported")]
		public static bool SourceExists(string source, string machineName)
		{
			if (machineName == null || machineName.Trim().Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for parameter 'machineName'.", machineName));
			}
			return EventLog.CreateEventLogImpl(string.Empty, machineName, source).SourceExists(source, machineName);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, and appends binary data to the message.</summary>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Diagnostics.EventLog.Source" /> property of the <see cref="T:System.Diagnostics.EventLog" /> has not been set.-or- The method attempted to register a new event source, but the computer name in <see cref="P:System.Diagnostics.EventLog.MachineName" /> is not valid.- or -The source is already registered for a different event log.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600093E RID: 2366 RVA: 0x0003125F File Offset: 0x0002F45F
		public void WriteEntry(string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			this.WriteEntry(new string[] { message }, type, (long)eventID, category, rawData);
		}

		/// <summary>Writes an error, warning, information, success audit, or failure audit entry with the given message text to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600093F RID: 2367 RVA: 0x00031278 File Offset: 0x0002F478
		public static void WriteEntry(string source, string message, EventLogEntryType type)
		{
			EventLog.WriteEntry(source, message, type, 0);
		}

		/// <summary>Writes an entry with the given message text and application-defined event identifier to the event log, using the specified registered event source.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000940 RID: 2368 RVA: 0x00031283 File Offset: 0x0002F483
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID)
		{
			EventLog.WriteEntry(source, message, type, eventID, 0);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log, using the specified registered event source. The <paramref name="category" /> can be used by the Event Viewer to filter events in the log.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000941 RID: 2369 RVA: 0x0003128F File Offset: 0x0002F48F
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category)
		{
			EventLog.WriteEntry(source, message, type, eventID, category, null);
		}

		/// <summary>Writes an entry with the given message text, application-defined event identifier, and application-defined category to the event log (using the specified registered event source) and appends binary data to the message.</summary>
		/// <param name="source">The source by which the application is registered on the specified computer. </param>
		/// <param name="message">The string to write to the event log. </param>
		/// <param name="type">One of the <see cref="T:System.Diagnostics.EventLogEntryType" /> values. </param>
		/// <param name="eventID">The application-specific identifier for the event. </param>
		/// <param name="category">The application-specific subcategory associated with the message. </param>
		/// <param name="rawData">An array of bytes that holds the binary data associated with the entry. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> value is an empty string ("").- or -The <paramref name="source" /> value is null.- or -<paramref name="eventID" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.- or -The message string is longer than 31,839 bytes (32,766 bytes on Windows operating systems before Windows Vista).- or -The source name results in a registry key path longer than 254 characters. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="type" /> is not a valid <see cref="T:System.Diagnostics.EventLogEntryType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The registry key for the event log could not be opened.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system reported an error when writing the event entry to the event log. A Windows error code is not available.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000942 RID: 2370 RVA: 0x000312A0 File Offset: 0x0002F4A0
		public static void WriteEntry(string source, string message, EventLogEntryType type, int eventID, short category, byte[] rawData)
		{
			using (EventLog eventLog = new EventLog())
			{
				eventLog.Source = source;
				eventLog.WriteEntry(message, type, eventID, category, rawData);
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000312E4 File Offset: 0x0002F4E4
		internal void OnEntryWritten(EventLogEntry newEntry)
		{
			if (this.doRaiseEvents && this.EntryWritten != null)
			{
				this.EntryWritten(this, new EntryWrittenEventArgs(newEntry));
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00031308 File Offset: 0x0002F508
		internal string GetLogName()
		{
			if (this.logName != null && this.logName.Length > 0)
			{
				return this.logName;
			}
			this.logName = EventLog.LogNameFromSourceName(this.source, this.machineName);
			return this.logName;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00031344 File Offset: 0x0002F544
		private static EventLogImpl CreateEventLogImpl(string logName, string machineName, string source)
		{
			return EventLog.CreateEventLogImpl(new EventLog(logName, machineName, source));
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00031354 File Offset: 0x0002F554
		private static EventLogImpl CreateEventLogImpl(EventLog eventLog)
		{
			string eventLogImplType = EventLog.EventLogImplType;
			if (eventLogImplType == "local")
			{
				return new LocalFileEventLog(eventLog);
			}
			if (eventLogImplType == "win32")
			{
				return new Win32EventLog(eventLog);
			}
			if (!(eventLogImplType == "null"))
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Eventlog implementation '{0}' is not supported.", EventLog.EventLogImplType));
			}
			return new NullEventLog(eventLog);
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x000313BE File Offset: 0x0002F5BE
		private static bool Win32EventLogEnabled
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x000313D0 File Offset: 0x0002F5D0
		private static string EventLogImplType
		{
			get
			{
				string text = Environment.GetEnvironmentVariable("MONO_EVENTLOG_TYPE");
				if (text == null)
				{
					if (EventLog.Win32EventLogEnabled)
					{
						return "win32";
					}
					text = "null";
				}
				else if (EventLog.Win32EventLogEnabled && string.Compare(text, "win32", true) == 0)
				{
					text = "win32";
				}
				else if (string.Compare(text, "null", true) == 0)
				{
					text = "null";
				}
				else
				{
					if (string.Compare(text, 0, "local", 0, "local".Length, true) != 0)
					{
						throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Eventlog implementation '{0}' is not supported.", text));
					}
					text = "local";
				}
				return text;
			}
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0003146C File Offset: 0x0002F66C
		private void WriteEntry(string[] replacementStrings, EventLogEntryType type, long instanceID, short category, byte[] rawData)
		{
			if (this.Source.Length == 0)
			{
				throw new ArgumentException("Source property was not setbefore writing to the event log.");
			}
			if (!Enum.IsDefined(typeof(EventLogEntryType), type))
			{
				throw new InvalidEnumArgumentException("type", (int)type, typeof(EventLogEntryType));
			}
			this.ValidateEventID(instanceID);
			if (!EventLog.SourceExists(this.Source, this.MachineName))
			{
				if (this.Log == null || this.Log.Length == 0)
				{
					this.Log = "Application";
				}
				EventLog.CreateEventSource(this.Source, this.Log, this.MachineName);
			}
			else if (this.logName != null && this.logName.Length != 0)
			{
				string text = EventLog.LogNameFromSourceName(this.Source, this.MachineName);
				if (string.Compare(this.logName, text, true, CultureInfo.InvariantCulture) != 0)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered in log '{1}' (it is registered in log '{2}'). The Source and Log properties must be matched, or you may set Log to the empty string, and it will automatically be matched to the Source property.", this.Source, this.logName, text));
				}
			}
			if (rawData == null)
			{
				rawData = new byte[0];
			}
			this.Impl.WriteEntry(replacementStrings, type, (uint)instanceID, category, rawData);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00031590 File Offset: 0x0002F790
		private void ValidateEventID(long instanceID)
		{
			int eventID = EventLog.GetEventID(instanceID);
			if (eventID < 0 || eventID > 65535)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid eventID value '{0}'. It must be in the range between '{1}' and '{2}'.", instanceID, 0, ushort.MaxValue));
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000315DC File Offset: 0x0002F7DC
		internal static int GetEventID(long instanceID)
		{
			int num = (int)(((instanceID < 0L) ? (-instanceID) : instanceID) & 1073741823L);
			if (instanceID >= 0L)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x040006F9 RID: 1785
		private string source;

		// Token: 0x040006FA RID: 1786
		private string logName;

		// Token: 0x040006FB RID: 1787
		private string machineName;

		// Token: 0x040006FC RID: 1788
		private bool doRaiseEvents;

		// Token: 0x040006FD RID: 1789
		private EventLogImpl Impl;

		// Token: 0x040006FE RID: 1790
		[CompilerGenerated]
		private EntryWrittenEventHandler EntryWritten;
	}
}
