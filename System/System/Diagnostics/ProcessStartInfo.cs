using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Specifies a set of values that are used when you start a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000177 RID: 375
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProcessStartInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class without specifying a file name with which to start the process.</summary>
		// Token: 0x060008DD RID: 2269 RVA: 0x0002FAC4 File Offset: 0x0002DCC4
		public ProcessStartInfo()
		{
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002FAD3 File Offset: 0x0002DCD3
		internal ProcessStartInfo(Process parent)
		{
			this.weakParentProcess = new WeakReference(parent);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0002FAEE File Offset: 0x0002DCEE
		public Collection<string> ArgumentList
		{
			get
			{
				if (this._argumentList == null)
				{
					this._argumentList = new Collection<string>();
				}
				return this._argumentList;
			}
		}

		/// <summary>Gets or sets the set of command-line arguments to use when starting the application.</summary>
		/// <returns>File type–specific arguments that the system can associate with the application specified in the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property. The default is an empty string (""). On Windows Vista and earlier versions of the Windows operating system, the length of the arguments added to the length of the full path to the process must be less than 2080. On Windows 7 and later versions, the length must be less than 32699.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0002FB09 File Offset: 0x0002DD09
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0002FB1F File Offset: 0x0002DD1F
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		[SettingsBindable(true)]
		[DefaultValue("")]
		[MonitoringDescription("Command line arguments that will be passed to the application specified by the FileName property.")]
		public string Arguments
		{
			get
			{
				if (this.arguments == null)
				{
					return string.Empty;
				}
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to start the process in a new window.</summary>
		/// <returns>true if the process should be started without creating a new window to contain it; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000183 RID: 387
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0002FB28 File Offset: 0x0002DD28
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[MonitoringDescription("Whether to start the process without creating a new window to contain it.")]
		public bool CreateNoWindow
		{
			set
			{
				this.createNoWindow = value;
			}
		}

		/// <summary>Gets search paths for files, directories for temporary files, application-specific options, and other similar information.</summary>
		/// <returns>A string dictionary that provides environment variables that apply to this process and child processes. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002FB34 File Offset: 0x0002DD34
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[MonitoringDescription("Set of environment variables that apply to this process and child processes.")]
		[NotifyParentProperty(true)]
		[Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.environmentVariables == null)
				{
					this.environmentVariables = new CaseSensitiveStringDictionary();
					if (this.weakParentProcess == null || !this.weakParentProcess.IsAlive || ((Component)this.weakParentProcess.Target).Site == null || !((Component)this.weakParentProcess.Target).Site.DesignMode)
					{
						foreach (object obj in Environment.GetEnvironmentVariables())
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							this.environmentVariables.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
						}
					}
				}
				return this.environmentVariables;
			}
		}

		/// <summary>Gets or sets a value indicating whether the input for an application is read from the <see cref="P:System.Diagnostics.Process.StandardInput" /> stream.</summary>
		/// <returns>true if input should be read from <see cref="P:System.Diagnostics.Process.StandardInput" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0002FC0C File Offset: 0x0002DE0C
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[MonitoringDescription("Whether the process command input is read from the Process instance's StandardInput member.")]
		public bool RedirectStandardInput
		{
			get
			{
				return this.redirectStandardInput;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <returns>true if output should be written to <see cref="P:System.Diagnostics.Process.StandardOutput" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002FC14 File Offset: 0x0002DE14
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0002FC1C File Offset: 0x0002DE1C
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether the process output is written to the Process instance's StandardOutput member.")]
		[DefaultValue(false)]
		public bool RedirectStandardOutput
		{
			get
			{
				return this.redirectStandardOutput;
			}
			set
			{
				this.redirectStandardOutput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <returns>true if error output should be written to <see cref="P:System.Diagnostics.Process.StandardError" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002FC25 File Offset: 0x0002DE25
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0002FC2D File Offset: 0x0002DE2D
		[MonitoringDescription("Whether the process's error output is written to the Process instance's StandardError member.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool RedirectStandardError
		{
			get
			{
				return this.redirectStandardError;
			}
			set
			{
				this.redirectStandardError = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for error output.</summary>
		/// <returns>An object that represents the preferred encoding for error output. The default is null.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002FC36 File Offset: 0x0002DE36
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0002FC3E File Offset: 0x0002DE3E
		public Encoding StandardErrorEncoding
		{
			get
			{
				return this.standardErrorEncoding;
			}
			set
			{
				this.standardErrorEncoding = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for standard output.</summary>
		/// <returns>An object that represents the preferred encoding for standard output. The default is null.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0002FC47 File Offset: 0x0002DE47
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0002FC4F File Offset: 0x0002DE4F
		public Encoding StandardOutputEncoding
		{
			get
			{
				return this.standardOutputEncoding;
			}
			set
			{
				this.standardOutputEncoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the operating system shell to start the process.</summary>
		/// <returns>true if the shell should be used when starting the process; false if the process should be created directly from the executable file. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0002FC58 File Offset: 0x0002DE58
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0002FC60 File Offset: 0x0002DE60
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether to use the operating system shell to start the process.")]
		[DefaultValue(true)]
		public bool UseShellExecute
		{
			get
			{
				return this.useShellExecute;
			}
			set
			{
				this.useShellExecute = value;
			}
		}

		/// <summary>Gets or sets the user name to be used when starting the process.</summary>
		/// <returns>The user name to use when starting the process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0002FC69 File Offset: 0x0002DE69
		[NotifyParentProperty(true)]
		public string UserName
		{
			get
			{
				if (this.userName == null)
				{
					return string.Empty;
				}
				return this.userName;
			}
		}

		/// <summary>Gets or sets a secure string that contains the user password to use when starting the process.</summary>
		/// <returns>The user password to use when starting the process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0002FC7F File Offset: 0x0002DE7F
		public SecureString Password
		{
			get
			{
				return this.password;
			}
		}

		/// <summary>Gets or sets a value that identifies the domain to use when starting the process. </summary>
		/// <returns>The Active Directory domain to use when starting the process. The domain property is primarily of interest to users within enterprise environments that use Active Directory.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0002FC87 File Offset: 0x0002DE87
		[NotifyParentProperty(true)]
		public string Domain
		{
			get
			{
				if (this.domain == null)
				{
					return string.Empty;
				}
				return this.domain;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the Windows user profile is to be loaded from the registry. </summary>
		/// <returns>true if the Windows user profile should be loaded; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0002FC9D File Offset: 0x0002DE9D
		[NotifyParentProperty(true)]
		public bool LoadUserProfile
		{
			get
			{
				return this.loadUserProfile;
			}
		}

		/// <summary>Gets or sets the application or document to start.</summary>
		/// <returns>The name of the application to start, or the name of a document of a file type that is associated with an application and that has a default open action available to it. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0002FCA5 File Offset: 0x0002DEA5
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0002FCBB File Offset: 0x0002DEBB
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The name of the application, document or URL to start.")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		public string FileName
		{
			get
			{
				if (this.fileName == null)
				{
					return string.Empty;
				}
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		/// <summary>When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is false, gets or sets the working directory for the process to be started. When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is true, gets or sets the directory that contains the process to be started.</summary>
		/// <returns>When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is true, the fully qualified name of the directory that contains the process to be started. When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is false, the working directory for the process to be started. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002FCC4 File Offset: 0x0002DEC4
		[MonitoringDescription("The initial working directory for the process.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string WorkingDirectory
		{
			get
			{
				if (this.directory == null)
				{
					return string.Empty;
				}
				return this.directory;
			}
		}

		/// <summary>Gets or sets the window state to use when the process is started.</summary>
		/// <returns>One of the enumeration values that indicates whether the process is started in a window that is maximized, minimized, normal (neither maximized nor minimized), or not visible. The default is Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The window style is not one of the <see cref="T:System.Diagnostics.ProcessWindowStyle" /> enumeration members. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000191 RID: 401
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0002FCDA File Offset: 0x0002DEDA
		[DefaultValue(ProcessWindowStyle.Normal)]
		[MonitoringDescription("How the main window should be created when the process starts.")]
		[NotifyParentProperty(true)]
		public ProcessWindowStyle WindowStyle
		{
			set
			{
				if (!Enum.IsDefined(typeof(ProcessWindowStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessWindowStyle));
				}
				this.windowStyle = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0002FD10 File Offset: 0x0002DF10
		internal bool HaveEnvVars
		{
			get
			{
				return this.environmentVariables != null;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0002FD1B File Offset: 0x0002DF1B
		public Encoding StandardInputEncoding { get; }

		// Token: 0x040006D1 RID: 1745
		private string fileName;

		// Token: 0x040006D2 RID: 1746
		private string arguments;

		// Token: 0x040006D3 RID: 1747
		private string directory;

		// Token: 0x040006D4 RID: 1748
		private string verb;

		// Token: 0x040006D5 RID: 1749
		private ProcessWindowStyle windowStyle;

		// Token: 0x040006D6 RID: 1750
		private bool errorDialog;

		// Token: 0x040006D7 RID: 1751
		private IntPtr errorDialogParentHandle;

		// Token: 0x040006D8 RID: 1752
		private bool useShellExecute = true;

		// Token: 0x040006D9 RID: 1753
		private string userName;

		// Token: 0x040006DA RID: 1754
		private string domain;

		// Token: 0x040006DB RID: 1755
		private SecureString password;

		// Token: 0x040006DC RID: 1756
		private string passwordInClearText;

		// Token: 0x040006DD RID: 1757
		private bool loadUserProfile;

		// Token: 0x040006DE RID: 1758
		private bool redirectStandardInput;

		// Token: 0x040006DF RID: 1759
		private bool redirectStandardOutput;

		// Token: 0x040006E0 RID: 1760
		private bool redirectStandardError;

		// Token: 0x040006E1 RID: 1761
		private Encoding standardOutputEncoding;

		// Token: 0x040006E2 RID: 1762
		private Encoding standardErrorEncoding;

		// Token: 0x040006E3 RID: 1763
		private bool createNoWindow;

		// Token: 0x040006E4 RID: 1764
		private WeakReference weakParentProcess;

		// Token: 0x040006E5 RID: 1765
		internal StringDictionary environmentVariables;

		// Token: 0x040006E6 RID: 1766
		private static readonly string[] empty = new string[0];

		// Token: 0x040006E7 RID: 1767
		private Collection<string> _argumentList;

		// Token: 0x040006E8 RID: 1768
		private IDictionary<string, string> environment;
	}
}
