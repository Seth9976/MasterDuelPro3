using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Xml;

namespace System.Windows.Forms
{
	// Token: 0x020000A0 RID: 160
	internal class MWFConfig
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x0001B2C0 File Offset: 0x000194C0
		public static object GetValue(string class_name, string value_name)
		{
			object obj = MWFConfig.lock_object;
			object value;
			lock (obj)
			{
				value = MWFConfig.Instance.GetValue(class_name, value_name);
			}
			return value;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001B308 File Offset: 0x00019508
		public static void SetValue(string class_name, string value_name, object value)
		{
			object obj = MWFConfig.lock_object;
			lock (obj)
			{
				MWFConfig.Instance.SetValue(class_name, value_name, value);
			}
		}

		// Token: 0x04000423 RID: 1059
		private static MWFConfig.MWFConfigInstance Instance = new MWFConfig.MWFConfigInstance();

		// Token: 0x04000424 RID: 1060
		private static object lock_object = new object();

		// Token: 0x020000A1 RID: 161
		internal class MWFConfigInstance
		{
			// Token: 0x0600064C RID: 1612 RVA: 0x0001B368 File Offset: 0x00019568
			static MWFConfigInstance()
			{
				string text = "mwf_config";
				string text2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				if (XplatUI.RunningOnUnix)
				{
					text2 = Path.Combine(text2, ".mono");
					try
					{
						Directory.CreateDirectory(text2);
					}
					catch
					{
					}
				}
				MWFConfig.MWFConfigInstance.default_file_name = Path.Combine(text2, text);
				MWFConfig.MWFConfigInstance.full_file_name = MWFConfig.MWFConfigInstance.default_file_name;
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x0001B3C8 File Offset: 0x000195C8
			public MWFConfigInstance()
			{
				this.Open(MWFConfig.MWFConfigInstance.default_file_name);
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x0001B3F4 File Offset: 0x000195F4
			~MWFConfigInstance()
			{
				this.Flush();
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x0001B420 File Offset: 0x00019620
			public object GetValue(string class_name, string value_name)
			{
				MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[class_name] as MWFConfig.MWFConfigInstance.ClassEntry;
				if (classEntry != null)
				{
					return classEntry.GetValue(value_name);
				}
				return null;
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x0001B44C File Offset: 0x0001964C
			public void SetValue(string class_name, string value_name, object value)
			{
				MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[class_name] as MWFConfig.MWFConfigInstance.ClassEntry;
				if (classEntry == null)
				{
					classEntry = new MWFConfig.MWFConfigInstance.ClassEntry();
					classEntry.ClassName = class_name;
					this.classes_hashtable[class_name] = classEntry;
				}
				classEntry.SetValue(value_name, value);
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x0001B490 File Offset: 0x00019690
			private void Open(string filename)
			{
				try
				{
					XmlTextReader xmlTextReader = new XmlTextReader(filename);
					this.ReadConfig(xmlTextReader);
					xmlTextReader.Close();
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x0001B4C8 File Offset: 0x000196C8
			public void Flush()
			{
				try
				{
					XmlTextWriter xmlTextWriter = new XmlTextWriter(MWFConfig.MWFConfigInstance.full_file_name, null);
					xmlTextWriter.Formatting = Formatting.Indented;
					this.WriteConfig(xmlTextWriter);
					xmlTextWriter.Close();
					if (!XplatUI.RunningOnUnix)
					{
						File.SetAttributes(MWFConfig.MWFConfigInstance.full_file_name, FileAttributes.Hidden);
					}
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x0001B51C File Offset: 0x0001971C
			private void ReadConfig(XmlTextReader xtr)
			{
				if (!this.CheckForMWFConfig(xtr))
				{
					return;
				}
				while (xtr.Read())
				{
					XmlNodeType nodeType = xtr.NodeType;
					if (nodeType == XmlNodeType.Element)
					{
						MWFConfig.MWFConfigInstance.ClassEntry classEntry = this.classes_hashtable[xtr.Name] as MWFConfig.MWFConfigInstance.ClassEntry;
						if (classEntry == null)
						{
							classEntry = new MWFConfig.MWFConfigInstance.ClassEntry();
							classEntry.ClassName = xtr.Name;
							this.classes_hashtable[xtr.Name] = classEntry;
						}
						classEntry.ReadXml(xtr);
					}
				}
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x0001B58B File Offset: 0x0001978B
			private bool CheckForMWFConfig(XmlTextReader xtr)
			{
				return xtr.Read() && xtr.NodeType == XmlNodeType.Element && xtr.Name == this.configName;
			}

			// Token: 0x06000655 RID: 1621 RVA: 0x0001B5B4 File Offset: 0x000197B4
			private void WriteConfig(XmlTextWriter xtw)
			{
				if (this.classes_hashtable.Count == 0)
				{
					return;
				}
				xtw.WriteStartElement(this.configName);
				foreach (object obj in this.classes_hashtable)
				{
					(((DictionaryEntry)obj).Value as MWFConfig.MWFConfigInstance.ClassEntry).WriteXml(xtw);
				}
				xtw.WriteEndElement();
			}

			// Token: 0x04000425 RID: 1061
			private Hashtable classes_hashtable = new Hashtable();

			// Token: 0x04000426 RID: 1062
			private static string full_file_name;

			// Token: 0x04000427 RID: 1063
			private static string default_file_name;

			// Token: 0x04000428 RID: 1064
			private readonly string configName = "MWFConfig";

			// Token: 0x020000A2 RID: 162
			internal class ClassEntry
			{
				// Token: 0x17000184 RID: 388
				// (set) Token: 0x06000656 RID: 1622 RVA: 0x0001B638 File Offset: 0x00019838
				public string ClassName
				{
					set
					{
						this.className = value;
					}
				}

				// Token: 0x06000657 RID: 1623 RVA: 0x0001B644 File Offset: 0x00019844
				public void SetValue(string value_name, object value)
				{
					MWFConfig.MWFConfigInstance.ClassValue classValue = this.classvalues_hashtable[value_name] as MWFConfig.MWFConfigInstance.ClassValue;
					if (classValue == null)
					{
						classValue = new MWFConfig.MWFConfigInstance.ClassValue();
						classValue.Name = value_name;
						this.classvalues_hashtable[value_name] = classValue;
					}
					classValue.SetValue(value);
				}

				// Token: 0x06000658 RID: 1624 RVA: 0x0001B688 File Offset: 0x00019888
				public object GetValue(string value_name)
				{
					MWFConfig.MWFConfigInstance.ClassValue classValue = this.classvalues_hashtable[value_name] as MWFConfig.MWFConfigInstance.ClassValue;
					if (classValue == null)
					{
						return null;
					}
					return classValue.GetValue();
				}

				// Token: 0x06000659 RID: 1625 RVA: 0x0001B6B4 File Offset: 0x000198B4
				public void ReadXml(XmlTextReader xtr)
				{
					while (xtr.Read())
					{
						XmlNodeType nodeType = xtr.NodeType;
						if (nodeType != XmlNodeType.Element)
						{
							if (nodeType == XmlNodeType.EndElement)
							{
								return;
							}
						}
						else
						{
							string attribute = xtr.GetAttribute("name");
							MWFConfig.MWFConfigInstance.ClassValue classValue = this.classvalues_hashtable[attribute] as MWFConfig.MWFConfigInstance.ClassValue;
							if (classValue == null)
							{
								classValue = new MWFConfig.MWFConfigInstance.ClassValue();
								classValue.Name = attribute;
								this.classvalues_hashtable[attribute] = classValue;
							}
							classValue.ReadXml(xtr);
						}
					}
				}

				// Token: 0x0600065A RID: 1626 RVA: 0x0001B724 File Offset: 0x00019924
				public void WriteXml(XmlTextWriter xtw)
				{
					if (this.classvalues_hashtable.Count == 0)
					{
						return;
					}
					xtw.WriteStartElement(this.className);
					foreach (object obj in this.classvalues_hashtable)
					{
						(((DictionaryEntry)obj).Value as MWFConfig.MWFConfigInstance.ClassValue).WriteXml(xtw);
					}
					xtw.WriteEndElement();
				}

				// Token: 0x04000429 RID: 1065
				private Hashtable classvalues_hashtable = new Hashtable();

				// Token: 0x0400042A RID: 1066
				private string className;
			}

			// Token: 0x020000A3 RID: 163
			internal class ClassValue
			{
				// Token: 0x17000185 RID: 389
				// (set) Token: 0x0600065C RID: 1628 RVA: 0x0001B7BB File Offset: 0x000199BB
				public string Name
				{
					set
					{
						this.name = value;
					}
				}

				// Token: 0x0600065D RID: 1629 RVA: 0x0001B7C4 File Offset: 0x000199C4
				public void SetValue(object value)
				{
					this.value = value;
				}

				// Token: 0x0600065E RID: 1630 RVA: 0x0001B7CD File Offset: 0x000199CD
				public object GetValue()
				{
					return this.value;
				}

				// Token: 0x0600065F RID: 1631 RVA: 0x0001B7D8 File Offset: 0x000199D8
				public void ReadXml(XmlTextReader xtr)
				{
					string attribute = xtr.GetAttribute("type");
					if (attribute == "byte_array" || attribute.IndexOf("-array") == -1)
					{
						string text = xtr.ReadString();
						if (attribute == "string")
						{
							this.value = text;
							return;
						}
						if (attribute == "int")
						{
							this.value = int.Parse(text);
							return;
						}
						if (attribute == "byte")
						{
							this.value = byte.Parse(text);
							return;
						}
						if (attribute == "color")
						{
							int num = int.Parse(text);
							this.value = Color.FromArgb(num);
							return;
						}
						if (attribute == "byte-array")
						{
							byte[] array = Convert.FromBase64String(text);
							this.value = array;
							return;
						}
					}
					else
					{
						this.ReadXmlArrayValues(xtr, attribute);
					}
				}

				// Token: 0x06000660 RID: 1632 RVA: 0x0001B8B4 File Offset: 0x00019AB4
				private void ReadXmlArrayValues(XmlTextReader xtr, string type)
				{
					ArrayList arrayList = new ArrayList();
					while (xtr.Read())
					{
						XmlNodeType nodeType = xtr.NodeType;
						if (nodeType != XmlNodeType.Element)
						{
							if (nodeType == XmlNodeType.EndElement)
							{
								if (xtr.Name == "value")
								{
									if (type == "int-array")
									{
										this.value = arrayList.ToArray(typeof(int));
										return;
									}
									if (type == "string-array")
									{
										this.value = arrayList.ToArray(typeof(string));
									}
									return;
								}
							}
						}
						else
						{
							string text = xtr.ReadString();
							if (type == "int-array")
							{
								int num = int.Parse(text);
								arrayList.Add(num);
							}
							else if (type == "string-array")
							{
								string text2 = text;
								arrayList.Add(text2);
							}
						}
					}
				}

				// Token: 0x06000661 RID: 1633 RVA: 0x0001B98C File Offset: 0x00019B8C
				public void WriteXml(XmlTextWriter xtw)
				{
					xtw.WriteStartElement("value");
					xtw.WriteAttributeString("name", this.name);
					if (this.value is Array)
					{
						this.WriteArrayContent(xtw);
					}
					else
					{
						this.WriteSingleContent(xtw);
					}
					xtw.WriteEndElement();
				}

				// Token: 0x06000662 RID: 1634 RVA: 0x0001B9D8 File Offset: 0x00019BD8
				private void WriteSingleContent(XmlTextWriter xtw)
				{
					string text = string.Empty;
					if (this.value is string)
					{
						text = "string";
					}
					else if (this.value is int)
					{
						text = "int";
					}
					else if (this.value is byte)
					{
						text = "byte";
					}
					else if (this.value is Color)
					{
						text = "color";
					}
					xtw.WriteAttributeString("type", text);
					if (this.value is Color)
					{
						xtw.WriteString(((Color)this.value).ToArgb().ToString());
						return;
					}
					xtw.WriteString(this.value.ToString());
				}

				// Token: 0x06000663 RID: 1635 RVA: 0x0001BA8C File Offset: 0x00019C8C
				private void WriteArrayContent(XmlTextWriter xtw)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					if (this.value is string[])
					{
						text = "string-array";
						text2 = "string";
					}
					else if (this.value is int[])
					{
						text = "int-array";
						text2 = "int";
					}
					else if (this.value is byte[])
					{
						text = "byte-array";
						text2 = "byte";
					}
					xtw.WriteAttributeString("type", text);
					if (text != "byte-array")
					{
						using (IEnumerator enumerator = (this.value as Array).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								xtw.WriteStartElement(text2);
								xtw.WriteString(obj.ToString());
								xtw.WriteEndElement();
							}
							return;
						}
					}
					byte[] array = this.value as byte[];
					xtw.WriteString(Convert.ToBase64String(array, 0, array.Length));
				}

				// Token: 0x0400042B RID: 1067
				private object value;

				// Token: 0x0400042C RID: 1068
				private string name;
			}
		}
	}
}
