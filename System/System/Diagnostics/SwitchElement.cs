using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Diagnostics
{
	// Token: 0x0200015C RID: 348
	internal class SwitchElement : ConfigurationElement
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x0002C428 File Offset: 0x0002A628
		static SwitchElement()
		{
			SwitchElement._properties.Add(SwitchElement._propName);
			SwitchElement._properties.Add(SwitchElement._propValue);
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0002C497 File Offset: 0x0002A697
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

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0002C4B7 File Offset: 0x0002A6B7
		[ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[SwitchElement._propName];
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002C4C9 File Offset: 0x0002A6C9
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SwitchElement._properties;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0002C4D0 File Offset: 0x0002A6D0
		[ConfigurationProperty("value", IsRequired = true)]
		public string Value
		{
			get
			{
				return (string)base[SwitchElement._propValue];
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002C4E2 File Offset: 0x0002A6E2
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			this.Attributes.Add(name, value);
			return true;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002C4F4 File Offset: 0x0002A6F4
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

		// Token: 0x06000821 RID: 2081 RVA: 0x0002C545 File Offset: 0x0002A745
		protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			return base.SerializeElement(writer, serializeCollectionKey) || (this._attributes != null && this._attributes.Count > 0);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002C56C File Offset: 0x0002A76C
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			SwitchElement switchElement = sourceElement as SwitchElement;
			if (switchElement != null && switchElement._attributes != null)
			{
				this._attributes = switchElement._attributes;
			}
		}

		// Token: 0x04000635 RID: 1589
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04000636 RID: 1590
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000637 RID: 1591
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000638 RID: 1592
		private Hashtable _attributes;
	}
}
