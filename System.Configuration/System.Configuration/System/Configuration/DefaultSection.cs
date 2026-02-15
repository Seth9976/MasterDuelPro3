using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a basic configuration-section handler that exposes the configuration section's XML for both read and write access.</summary>
	// Token: 0x02000028 RID: 40
	public sealed class DefaultSection : ConfigurationSection
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00005CD5 File Offset: 0x00003ED5
		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			if (base.RawXml == null)
			{
				base.RawXml = xmlReader.ReadOuterXml();
				return;
			}
			xmlReader.Skip();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005CF2 File Offset: 0x00003EF2
		[MonoTODO]
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005CFA File Offset: 0x00003EFA
		[MonoTODO]
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			base.Reset(parentSection);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005D03 File Offset: 0x00003F03
		[MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005D0B File Offset: 0x00003F0B
		[MonoTODO]
		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return base.SerializeSection(parentSection, name, saveMode);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005D16 File Offset: 0x00003F16
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultSection.properties;
			}
		}

		// Token: 0x04000098 RID: 152
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
