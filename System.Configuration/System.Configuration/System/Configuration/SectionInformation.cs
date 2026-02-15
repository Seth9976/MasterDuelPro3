using System;

namespace System.Configuration
{
	/// <summary>Contains metadata about an individual section within the configuration hierarchy. This class cannot be inherited.</summary>
	// Token: 0x02000041 RID: 65
	public sealed class SectionInformation
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x0000777C File Offset: 0x0000597C
		[MonoTODO("default value for require_permission")]
		internal SectionInformation()
		{
			this.allow_definition = ConfigurationAllowDefinition.Everywhere;
			this.allow_location = true;
			this.allow_override = true;
			this.inherit_on_child_apps = true;
			this.restart_on_external_changes = true;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000077D4 File Offset: 0x000059D4
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000077DC File Offset: 0x000059DC
		internal string ConfigFilePath { get; set; }

		/// <summary>Gets or sets a value that indicates where in the configuration file hierarchy the associated configuration section can be defined. </summary>
		/// <returns>A value that indicates where in the configuration file hierarchy the associated <see cref="T:System.Configuration.ConfigurationSection" /> object can be declared.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000083 RID: 131
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x000077E5 File Offset: 0x000059E5
		public ConfigurationAllowDefinition AllowDefinition
		{
			set
			{
				this.allow_definition = value;
			}
		}

		/// <summary>Gets or sets a value that indicates where in the configuration file hierarchy the associated configuration section can be declared.</summary>
		/// <returns>A value that indicates where in the configuration file hierarchy the associated <see cref="T:System.Configuration.ConfigurationSection" /> object can be declared for .exe files.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000084 RID: 132
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000077EE File Offset: 0x000059EE
		public ConfigurationAllowExeDefinition AllowExeDefinition
		{
			set
			{
				this.allow_exe_definition = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the configuration section allows the location attribute.</summary>
		/// <returns>true if the location attribute is allowed; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000085 RID: 133
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000077F7 File Offset: 0x000059F7
		public bool AllowLocation
		{
			set
			{
				this.allow_location = value;
			}
		}

		/// <summary>Gets or sets the name of the include file in which the associated configuration section is defined, if such a file exists.</summary>
		/// <returns>The name of the include file in which the associated <see cref="T:System.Configuration.ConfigurationSection" /> is defined, if such a file exists; otherwise, an empty string ("").</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00007800 File Offset: 0x00005A00
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00007808 File Offset: 0x00005A08
		public string ConfigSource
		{
			get
			{
				return this.config_source;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.config_source = value;
			}
		}

		/// <summary>Gets the name of the associated configuration section.</summary>
		/// <returns>The complete name of the configuration section.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000781B File Offset: 0x00005A1B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section requires access permissions.</summary>
		/// <returns>true if the requirePermission attribute is set to true; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000088 RID: 136
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00007823 File Offset: 0x00005A23
		[MonoTODO]
		public bool RequirePermission
		{
			set
			{
				this.require_permission = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether a change in an external configuration include file requires an application restart.</summary>
		/// <returns>true if a change in an external configuration include file requires an application restart; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000089 RID: 137
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000782C File Offset: 0x00005A2C
		[MonoTODO]
		public bool RestartOnExternalChanges
		{
			set
			{
				this.restart_on_external_changes = value;
			}
		}

		/// <summary>Gets the configuration section that contains the configuration section associated with this object.</summary>
		/// <returns>The configuration section that contains the <see cref="T:System.Configuration.ConfigurationSection" /> that is associated with this <see cref="T:System.Configuration.SectionInformation" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is invoked from a parent section.</exception>
		// Token: 0x060001BE RID: 446 RVA: 0x00007835 File Offset: 0x00005A35
		public ConfigurationSection GetParentSection()
		{
			return this.parent;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000783D File Offset: 0x00005A3D
		internal void SetParentSection(ConfigurationSection parent)
		{
			this.parent = parent;
		}

		/// <summary>Marks a configuration section for protection. </summary>
		/// <param name="protectionProvider">The name of the protection provider to use.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Configuration.SectionInformation.AllowLocation" /> property is set to false.- or -The target section is already a protected data section.</exception>
		// Token: 0x060001C0 RID: 448 RVA: 0x00007846 File Offset: 0x00005A46
		public void ProtectSection(string protectionProvider)
		{
			this.protection_provider = ProtectedConfiguration.GetProvider(protectionProvider, true);
		}

		/// <summary>Sets the object to an XML representation of the associated configuration section within the configuration file.</summary>
		/// <param name="rawXml">The XML to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rawXml" /> is null.</exception>
		// Token: 0x060001C1 RID: 449 RVA: 0x00007855 File Offset: 0x00005A55
		public void SetRawXml(string rawXml)
		{
			this.raw_xml = rawXml;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000785E File Offset: 0x00005A5E
		[MonoTODO]
		internal void SetName(string name)
		{
			this.name = name;
		}

		// Token: 0x040000CF RID: 207
		private ConfigurationSection parent;

		// Token: 0x040000D0 RID: 208
		private ConfigurationAllowDefinition allow_definition = ConfigurationAllowDefinition.Everywhere;

		// Token: 0x040000D1 RID: 209
		private ConfigurationAllowExeDefinition allow_exe_definition = ConfigurationAllowExeDefinition.MachineToApplication;

		// Token: 0x040000D2 RID: 210
		private bool allow_location;

		// Token: 0x040000D3 RID: 211
		private bool allow_override;

		// Token: 0x040000D4 RID: 212
		private bool inherit_on_child_apps;

		// Token: 0x040000D5 RID: 213
		private bool restart_on_external_changes;

		// Token: 0x040000D6 RID: 214
		private bool require_permission;

		// Token: 0x040000D7 RID: 215
		private string config_source = string.Empty;

		// Token: 0x040000D8 RID: 216
		private string name;

		// Token: 0x040000D9 RID: 217
		private string raw_xml;

		// Token: 0x040000DA RID: 218
		private ProtectedConfigurationProvider protection_provider;
	}
}
