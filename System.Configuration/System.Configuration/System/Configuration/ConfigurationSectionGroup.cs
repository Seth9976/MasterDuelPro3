using System;

namespace System.Configuration
{
	/// <summary>Represents a group of related sections within a configuration file.</summary>
	// Token: 0x02000022 RID: 34
	public class ConfigurationSectionGroup
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005B18 File Offset: 0x00003D18
		private Configuration Config
		{
			get
			{
				if (this.config == null)
				{
					throw new InvalidOperationException("ConfigurationSectionGroup cannot be edited until it is added to a Configuration instance as its descendant");
				}
				return this.config;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005B34 File Offset: 0x00003D34
		internal void Initialize(Configuration config, SectionGroupInfo group)
		{
			if (this.initialized)
			{
				string text = "INTERNAL ERROR: this configuration section is being initialized twice: ";
				Type type = base.GetType();
				throw new SystemException(text + ((type != null) ? type.ToString() : null));
			}
			this.initialized = true;
			this.config = config;
			this.group = group;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects that are children of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects that are children of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005B80 File Offset: 0x00003D80
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new ConfigurationSectionGroupCollection(this.Config, this.group);
				}
				return this.groups;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object that contains all of <see cref="T:System.Configuration.ConfigurationSection" /> objects within this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSection" /> objects within this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005BA7 File Offset: 0x00003DA7
		public ConfigurationSectionCollection Sections
		{
			get
			{
				if (this.sections == null)
				{
					this.sections = new ConfigurationSectionCollection(this.Config, this.group);
				}
				return this.sections;
			}
		}

		// Token: 0x04000089 RID: 137
		private ConfigurationSectionCollection sections;

		// Token: 0x0400008A RID: 138
		private ConfigurationSectionGroupCollection groups;

		// Token: 0x0400008B RID: 139
		private Configuration config;

		// Token: 0x0400008C RID: 140
		private SectionGroupInfo group;

		// Token: 0x0400008D RID: 141
		private bool initialized;
	}
}
