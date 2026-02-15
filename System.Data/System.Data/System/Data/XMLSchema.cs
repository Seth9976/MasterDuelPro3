using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000A9 RID: 169
	internal class XMLSchema
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x0002A35F File Offset: 0x0002855F
		internal static TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002A368 File Offset: 0x00028568
		internal static void SetProperties(object instance, XmlAttributeCollection attrs)
		{
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
				{
					string localName = attrs[i].LocalName;
					string value = attrs[i].Value;
					if (!(localName == "DefaultValue") && !(localName == "RemotingFormat") && (!(localName == "Expression") || !(instance is DataColumn)))
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(instance)[localName];
						if (propertyDescriptor != null)
						{
							Type propertyType = propertyDescriptor.PropertyType;
							TypeConverter converter = XMLSchema.GetConverter(propertyType);
							object obj;
							if (converter.CanConvertFrom(typeof(string)))
							{
								obj = converter.ConvertFromInvariantString(value);
							}
							else if (propertyType == typeof(Type))
							{
								obj = DataStorage.GetType(value);
							}
							else
							{
								if (!(propertyType == typeof(CultureInfo)))
								{
									throw ExceptionBuilder.CannotConvert(value, propertyType.FullName);
								}
								obj = new CultureInfo(value);
							}
							propertyDescriptor.SetValue(instance, obj);
						}
					}
				}
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002A48D File Offset: 0x0002868D
		internal static bool FEqualIdentity(XmlNode node, string name, string ns)
		{
			return node != null && node.LocalName == name && node.NamespaceURI == ns;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0002A4B4 File Offset: 0x000286B4
		internal static bool GetBooleanAttribute(XmlElement element, string attrName, string attrNS, bool defVal)
		{
			string attribute = element.GetAttribute(attrName, attrNS);
			if (attribute == null || attribute.Length == 0)
			{
				return defVal;
			}
			if (attribute == "true" || attribute == "1")
			{
				return true;
			}
			if (attribute == "false" || attribute == "0")
			{
				return false;
			}
			throw ExceptionBuilder.InvalidAttributeValue(attrName, attribute);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002A518 File Offset: 0x00028718
		internal static string GenUniqueColumnName(string proposedName, DataTable table)
		{
			if (table.Columns.IndexOf(proposedName) >= 0)
			{
				for (int i = 0; i <= table.Columns.Count; i++)
				{
					string text = proposedName + "_" + i.ToString(CultureInfo.InvariantCulture);
					if (table.Columns.IndexOf(text) < 0)
					{
						return text;
					}
				}
			}
			return proposedName;
		}
	}
}
