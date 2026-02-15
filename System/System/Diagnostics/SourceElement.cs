using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x02000157 RID: 343
	internal class SourceElement : ConfigurationElement
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x0002BC9C File Offset: 0x00029E9C
		static SourceElement()
		{
			SourceElement._properties.Add(SourceElement._propName);
			SourceElement._properties.Add(SourceElement._propSwitchName);
			SourceElement._properties.Add(SourceElement._propSwitchValue);
			SourceElement._properties.Add(SourceElement._propSwitchType);
			SourceElement._properties.Add(SourceElement._propListeners);
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002BD8D File Offset: 0x00029F8D
		public Hashtable Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this._attributes;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0002BDAD File Offset: 0x00029FAD
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[SourceElement._propListeners];
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0002BDBF File Offset: 0x00029FBF
		[ConfigurationProperty("name", IsRequired = true, DefaultValue = "")]
		public string Name
		{
			get
			{
				return (string)base[SourceElement._propName];
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0002BDD1 File Offset: 0x00029FD1
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SourceElement._properties;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0002BDD8 File Offset: 0x00029FD8
		[ConfigurationProperty("switchName")]
		public string SwitchName
		{
			get
			{
				return (string)base[SourceElement._propSwitchName];
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0002BDEA File Offset: 0x00029FEA
		[ConfigurationProperty("switchValue")]
		public string SwitchValue
		{
			get
			{
				return (string)base[SourceElement._propSwitchValue];
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0002BDFC File Offset: 0x00029FFC
		[ConfigurationProperty("switchType")]
		public string SwitchType
		{
			get
			{
				return (string)base[SourceElement._propSwitchType];
			}
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0002BE10 File Offset: 0x0002A010
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			if (!string.IsNullOrEmpty(this.SwitchName) && !string.IsNullOrEmpty(this.SwitchValue))
			{
				throw new ConfigurationErrorsException(SR.GetString("'switchValue' and 'switchName' cannot both be specified on source '{0}'.", new object[] { this.Name }));
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0002BE5E File Offset: 0x0002A05E
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002BE70 File Offset: 0x0002A070
		protected override void PreSerialize(XmlWriter writer)
		{
			if (this._attributes != null)
			{
				IDictionaryEnumerator enumerator = this._attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Value;
					string text2 = (string)enumerator.Key;
					if (text != null && writer != null)
					{
						writer.WriteAttributeString(text2, text);
					}
				}
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002BEC1 File Offset: 0x0002A0C1
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002BEE8 File Offset: 0x0002A0E8
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SourceElement sourceElement2 = sourceElement as SourceElement;
			if (sourceElement2 != null && sourceElement2._attributes != null)
			{
				this._attributes = sourceElement2._attributes;
			}
		}

		// Token: 0x04000619 RID: 1561
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x0400061A RID: 1562
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400061B RID: 1563
		private static readonly ConfigurationProperty _propSwitchName = new ConfigurationProperty("switchName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400061C RID: 1564
		private static readonly ConfigurationProperty _propSwitchValue = new ConfigurationProperty("switchValue", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400061D RID: 1565
		private static readonly ConfigurationProperty _propSwitchType = new ConfigurationProperty("switchType", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400061E RID: 1566
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x0400061F RID: 1567
		private Hashtable _attributes;
	}
}
