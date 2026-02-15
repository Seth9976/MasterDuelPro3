using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200003E RID: 62
	internal class SectionGroupInfo : ConfigInfo
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00006A38 File Offset: 0x00004C38
		public SectionGroupInfo()
		{
			this.Type = typeof(ConfigurationSectionGroup);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006A50 File Offset: 0x00004C50
		public void AddChild(ConfigInfo data)
		{
			this.modified = true;
			data.Parent = this;
			if (data is SectionInfo)
			{
				if (this.sections == null)
				{
					this.sections = new ConfigInfoCollection();
				}
				this.sections[data.Name] = data;
				return;
			}
			if (this.groups == null)
			{
				this.groups = new ConfigInfoCollection();
			}
			this.groups[data.Name] = data;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006ABE File Offset: 0x00004CBE
		public void Clear()
		{
			this.modified = true;
			if (this.sections != null)
			{
				this.sections.Clear();
			}
			if (this.groups != null)
			{
				this.groups.Clear();
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006AED File Offset: 0x00004CED
		public bool HasChild(string name)
		{
			return (this.sections != null && this.sections[name] != null) || (this.groups != null && this.groups[name] != null);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006B20 File Offset: 0x00004D20
		public void RemoveChild(string name)
		{
			this.modified = true;
			if (this.sections != null)
			{
				this.sections.Remove(name);
			}
			if (this.groups != null)
			{
				this.groups.Remove(name);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00006B51 File Offset: 0x00004D51
		public ConfigInfoCollection Sections
		{
			get
			{
				if (this.sections == null)
				{
					return SectionGroupInfo.emptyList;
				}
				return this.sections;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006B67 File Offset: 0x00004D67
		public ConfigInfoCollection Groups
		{
			get
			{
				if (this.groups == null)
				{
					return SectionGroupInfo.emptyList;
				}
				return this.groups;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006B80 File Offset: 0x00004D80
		public override void ReadConfig(Configuration cfg, string streamName, XmlReader reader)
		{
			base.StreamName = streamName;
			this.ConfigHost = cfg.ConfigHost;
			if (reader.LocalName != "configSections")
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.Name == "name")
					{
						this.Name = reader.Value;
					}
					else if (reader.Name == "type")
					{
						this.TypeName = reader.Value;
						this.Type = null;
					}
					else
					{
						base.ThrowException("Unrecognized attribute", reader);
					}
				}
				if (this.Name == null)
				{
					base.ThrowException("sectionGroup must have a 'name' attribute", reader);
				}
				if (this.Name == "location")
				{
					base.ThrowException("location is a reserved section name", reader);
				}
			}
			if (this.TypeName == null)
			{
				this.TypeName = "System.Configuration.ConfigurationSectionGroup";
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			reader.MoveToContent();
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
				}
				else
				{
					string localName = reader.LocalName;
					ConfigInfo configInfo = null;
					if (localName == "remove")
					{
						this.ReadRemoveSection(reader);
					}
					else if (localName == "clear")
					{
						if (reader.HasAttributes)
						{
							base.ThrowException("Unrecognized attribute.", reader);
						}
						this.Clear();
						reader.Skip();
					}
					else
					{
						if (localName == "section")
						{
							configInfo = new SectionInfo();
						}
						else if (localName == "sectionGroup")
						{
							configInfo = new SectionGroupInfo();
						}
						else
						{
							base.ThrowException("Unrecognized element: " + reader.Name, reader);
						}
						configInfo.ReadConfig(cfg, streamName, reader);
						ConfigInfo configInfo2 = this.Groups[configInfo.Name];
						if (configInfo2 == null)
						{
							configInfo2 = this.Sections[configInfo.Name];
						}
						if (configInfo2 != null)
						{
							if (configInfo2.GetType() != configInfo.GetType())
							{
								base.ThrowException("A section or section group named '" + configInfo.Name + "' already exists", reader);
							}
							configInfo2.Merge(configInfo);
							configInfo2.StreamName = streamName;
						}
						else
						{
							this.AddChild(configInfo);
						}
					}
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006DB0 File Offset: 0x00004FB0
		private void ReadRemoveSection(XmlReader reader)
		{
			if (!reader.MoveToNextAttribute() || reader.Name != "name")
			{
				base.ThrowException("Unrecognized attribute.", reader);
			}
			string value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				base.ThrowException("Empty name to remove", reader);
			}
			reader.MoveToElement();
			if (!this.HasChild(value))
			{
				base.ThrowException("No factory for " + value, reader);
			}
			this.RemoveChild(value);
			reader.Skip();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006E2D File Offset: 0x0000502D
		public void ReadRootData(XmlReader reader, Configuration config, bool overrideAllowed)
		{
			reader.MoveToContent();
			this.ReadContent(reader, config, overrideAllowed, true);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006E40 File Offset: 0x00005040
		public override void ReadData(Configuration config, XmlReader reader, bool overrideAllowed)
		{
			reader.MoveToContent();
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				this.ReadContent(reader, config, overrideAllowed, false);
				reader.MoveToContent();
				reader.ReadEndElement();
				return;
			}
			reader.Read();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006E78 File Offset: 0x00005078
		private void ReadContent(XmlReader reader, Configuration config, bool overrideAllowed, bool root)
		{
			while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.None)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
				}
				else if (reader.LocalName == "dllmap")
				{
					reader.Skip();
				}
				else if (reader.LocalName == "location")
				{
					if (!root)
					{
						base.ThrowException("<location> elements are only allowed in <configuration> elements.", reader);
					}
					string attribute = reader.GetAttribute("allowOverride");
					bool flag = attribute == null || attribute.Length == 0 || bool.Parse(attribute);
					string attribute2 = reader.GetAttribute("path");
					if (attribute2 != null && attribute2.Length > 0)
					{
						string text = reader.ReadOuterXml();
						string[] array = attribute2.Split(',', StringSplitOptions.None);
						for (int i = 0; i < array.Length; i++)
						{
							string text2 = array[i].Trim();
							if (config.Locations.Find(text2) != null)
							{
								base.ThrowException("Sections must only appear once per config file.", reader);
							}
							ConfigurationLocation configurationLocation = new ConfigurationLocation(text2, text, config, flag);
							config.Locations.Add(configurationLocation);
						}
					}
					else
					{
						this.ReadData(config, reader, flag);
					}
				}
				else
				{
					ConfigInfo configInfo = this.GetConfigInfo(reader, this);
					if (configInfo != null)
					{
						configInfo.ReadData(config, reader, overrideAllowed);
					}
					else
					{
						base.ThrowException("Unrecognized configuration section <" + reader.LocalName + ">", reader);
					}
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006FD8 File Offset: 0x000051D8
		private ConfigInfo GetConfigInfo(XmlReader reader, SectionGroupInfo current)
		{
			ConfigInfo configInfo = null;
			if (current.sections != null)
			{
				configInfo = current.sections[reader.LocalName];
			}
			if (configInfo != null)
			{
				return configInfo;
			}
			if (current.groups != null)
			{
				configInfo = current.groups[reader.LocalName];
			}
			if (configInfo != null)
			{
				return configInfo;
			}
			if (current.groups == null)
			{
				return null;
			}
			foreach (object obj in current.groups.AllKeys)
			{
				string text = (string)obj;
				configInfo = this.GetConfigInfo(reader, (SectionGroupInfo)current.groups[text]);
				if (configInfo != null)
				{
					return configInfo;
				}
			}
			return null;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000070A0 File Offset: 0x000052A0
		internal override void Merge(ConfigInfo newData)
		{
			SectionGroupInfo sectionGroupInfo = newData as SectionGroupInfo;
			if (sectionGroupInfo == null)
			{
				return;
			}
			if (sectionGroupInfo.sections != null && sectionGroupInfo.sections.Count > 0)
			{
				foreach (object obj in sectionGroupInfo.sections.AllKeys)
				{
					string text = (string)obj;
					if (this.sections[text] == null)
					{
						this.sections.Add(text, sectionGroupInfo.sections[text]);
					}
				}
			}
			if (sectionGroupInfo.groups != null && sectionGroupInfo.sections != null && sectionGroupInfo.sections.Count > 0)
			{
				foreach (object obj2 in sectionGroupInfo.groups.AllKeys)
				{
					string text2 = (string)obj2;
					if (this.groups[text2] == null)
					{
						this.groups.Add(text2, sectionGroupInfo.groups[text2]);
					}
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000071CC File Offset: 0x000053CC
		internal override void ResetModified(Configuration config)
		{
			this.modified = false;
			foreach (ConfigInfoCollection configInfoCollection in new object[] { this.Sections, this.Groups })
			{
				foreach (object obj in configInfoCollection)
				{
					string text = (string)obj;
					configInfoCollection[text].ResetModified(config);
				}
			}
		}

		// Token: 0x040000C6 RID: 198
		private bool modified;

		// Token: 0x040000C7 RID: 199
		private ConfigInfoCollection sections;

		// Token: 0x040000C8 RID: 200
		private ConfigInfoCollection groups;

		// Token: 0x040000C9 RID: 201
		private static ConfigInfoCollection emptyList = new ConfigInfoCollection();
	}
}
