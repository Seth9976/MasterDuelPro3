using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides a wrapper type definition for configuration sections that are not handled by the <see cref="N:System.Configuration" /> types.</summary>
	// Token: 0x0200002C RID: 44
	public sealed class IgnoreSection : ConfigurationSection
	{
		// Token: 0x0600013E RID: 318 RVA: 0x0000329C File Offset: 0x0000149C
		protected internal override bool IsModified()
		{
			return false;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005EFF File Offset: 0x000040FF
		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			this.xml = xmlReader.ReadOuterXml();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005CFA File Offset: 0x00003EFA
		[MonoTODO]
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			base.Reset(parentSection);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005D03 File Offset: 0x00003F03
		[MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005F0D File Offset: 0x0000410D
		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return this.xml;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005F15 File Offset: 0x00004115
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IgnoreSection.properties;
			}
		}

		// Token: 0x0400009F RID: 159
		private string xml;

		// Token: 0x040000A0 RID: 160
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
