using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Defines the configuration file mapping for the machine configuration file. </summary>
	// Token: 0x02000014 RID: 20
	public class ConfigurationFileMap : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationFileMap" /> class. </summary>
		// Token: 0x060000AF RID: 175 RVA: 0x000048E1 File Offset: 0x00002AE1
		public ConfigurationFileMap()
		{
			this.machineConfigFilename = RuntimeEnvironment.SystemConfigurationFile;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationFileMap" /> class based on the supplied parameter.</summary>
		/// <param name="machineConfigFilename">The name of the machine configuration file.</param>
		// Token: 0x060000B0 RID: 176 RVA: 0x000048F4 File Offset: 0x00002AF4
		public ConfigurationFileMap(string machineConfigFilename)
		{
			this.machineConfigFilename = machineConfigFilename;
		}

		/// <summary>Gets or sets the name of the machine configuration file name.</summary>
		/// <returns>The machine configuration file name.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004903 File Offset: 0x00002B03
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000490B File Offset: 0x00002B0B
		public string MachineConfigFilename
		{
			get
			{
				return this.machineConfigFilename;
			}
			set
			{
				this.machineConfigFilename = value;
			}
		}

		/// <summary>Creates a copy of the existing <see cref="T:System.Configuration.ConfigurationFileMap" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationFileMap" /> object.</returns>
		// Token: 0x060000B3 RID: 179 RVA: 0x00004914 File Offset: 0x00002B14
		public virtual object Clone()
		{
			return new ConfigurationFileMap(this.machineConfigFilename);
		}

		// Token: 0x04000050 RID: 80
		private string machineConfigFilename;
	}
}
