using System;

namespace System.Configuration
{
	/// <summary>Defines the configuration file mapping for an .exe application. This class cannot be inherited.</summary>
	// Token: 0x0200002B RID: 43
	public sealed class ExeConfigurationFileMap : ConfigurationFileMap
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ExeConfigurationFileMap" /> class.</summary>
		// Token: 0x06000134 RID: 308 RVA: 0x00005E60 File Offset: 0x00004060
		public ExeConfigurationFileMap()
		{
			this.exeConfigFilename = "";
			this.localUserConfigFilename = "";
			this.roamingUserConfigFilename = "";
		}

		/// <summary>Gets or sets the name of the configuration file.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005E89 File Offset: 0x00004089
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00005E91 File Offset: 0x00004091
		public string ExeConfigFilename
		{
			get
			{
				return this.exeConfigFilename;
			}
			set
			{
				this.exeConfigFilename = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for the local user.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005E9A File Offset: 0x0000409A
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005EA2 File Offset: 0x000040A2
		public string LocalUserConfigFilename
		{
			get
			{
				return this.localUserConfigFilename;
			}
			set
			{
				this.localUserConfigFilename = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for the roaming user.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005EAB File Offset: 0x000040AB
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005EB3 File Offset: 0x000040B3
		public string RoamingUserConfigFilename
		{
			get
			{
				return this.roamingUserConfigFilename;
			}
			set
			{
				this.roamingUserConfigFilename = value;
			}
		}

		/// <summary>Creates a copy of the existing <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object.</returns>
		// Token: 0x0600013B RID: 315 RVA: 0x00005EBC File Offset: 0x000040BC
		public override object Clone()
		{
			return new ExeConfigurationFileMap
			{
				exeConfigFilename = this.exeConfigFilename,
				localUserConfigFilename = this.localUserConfigFilename,
				roamingUserConfigFilename = this.roamingUserConfigFilename,
				MachineConfigFilename = base.MachineConfigFilename
			};
		}

		// Token: 0x0400009C RID: 156
		private string exeConfigFilename;

		// Token: 0x0400009D RID: 157
		private string localUserConfigFilename;

		// Token: 0x0400009E RID: 158
		private string roamingUserConfigFilename;
	}
}
