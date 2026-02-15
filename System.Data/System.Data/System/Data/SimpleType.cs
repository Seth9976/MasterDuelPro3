using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x020000A0 RID: 160
	[Serializable]
	internal sealed class SimpleType : ISerializable
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x000279AC File Offset: 0x00025BAC
		internal SimpleType(string baseType)
		{
			this._baseType = baseType;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00027A34 File Offset: 0x00025C34
		internal SimpleType(XmlSchemaSimpleType node)
		{
			this._name = node.Name;
			this._ns = ((node.QualifiedName != null) ? node.QualifiedName.Namespace : "");
			this.LoadTypeValues(node);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00027AED File Offset: 0x00025CED
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00027AF4 File Offset: 0x00025CF4
		internal void LoadTypeValues(XmlSchemaSimpleType node)
		{
			if (node.Content is XmlSchemaSimpleTypeList || node.Content is XmlSchemaSimpleTypeUnion)
			{
				throw ExceptionBuilder.SimpleTypeNotSupported();
			}
			if (node.Content is XmlSchemaSimpleTypeRestriction)
			{
				XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)node.Content;
				XmlSchemaSimpleType xmlSchemaSimpleType = node.BaseXmlSchemaType as XmlSchemaSimpleType;
				if (xmlSchemaSimpleType != null && xmlSchemaSimpleType.QualifiedName.Namespace != "http://www.w3.org/2001/XMLSchema")
				{
					this._baseSimpleType = new SimpleType(node.BaseXmlSchemaType as XmlSchemaSimpleType);
				}
				if (xmlSchemaSimpleTypeRestriction.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema")
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.Name;
				}
				else
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseTypeName.ToString();
				}
				if (this._baseSimpleType != null && this._baseSimpleType.Name != null && this._baseSimpleType.Name.Length > 0)
				{
					this._xmlBaseType = this._baseSimpleType.XmlBaseType;
				}
				else
				{
					this._xmlBaseType = xmlSchemaSimpleTypeRestriction.BaseTypeName;
				}
				if (this._baseType == null || this._baseType.Length == 0)
				{
					this._baseType = xmlSchemaSimpleTypeRestriction.BaseType.Name;
					this._xmlBaseType = null;
				}
				if (this._baseType == "NOTATION")
				{
					this._baseType = "string";
				}
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchemaSimpleTypeRestriction.Facets)
				{
					XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
					if (xmlSchemaFacet is XmlSchemaLengthFacet)
					{
						this._length = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMinLengthFacet)
					{
						this._minLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaMaxLengthFacet)
					{
						this._maxLength = Convert.ToInt32(xmlSchemaFacet.Value, null);
					}
					if (xmlSchemaFacet is XmlSchemaPatternFacet)
					{
						this._pattern = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaEnumerationFacet)
					{
						this._enumeration = ((!string.IsNullOrEmpty(this._enumeration)) ? (this._enumeration + " " + xmlSchemaFacet.Value) : xmlSchemaFacet.Value);
					}
					if (xmlSchemaFacet is XmlSchemaMinExclusiveFacet)
					{
						this._minExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMinInclusiveFacet)
					{
						this._minInclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxExclusiveFacet)
					{
						this._maxExclusive = xmlSchemaFacet.Value;
					}
					if (xmlSchemaFacet is XmlSchemaMaxInclusiveFacet)
					{
						this._maxInclusive = xmlSchemaFacet.Value;
					}
				}
			}
			string msdataAttribute = XSDSchema.GetMsdataAttribute(node, "targetNamespace");
			if (msdataAttribute != null)
			{
				this._ns = msdataAttribute;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00027DB4 File Offset: 0x00025FB4
		internal bool IsPlainString()
		{
			return XSDSchema.QualifiedName(this._baseType) == XSDSchema.QualifiedName("string") && string.IsNullOrEmpty(this._name) && this._length == -1 && this._minLength == -1 && this._maxLength == -1 && string.IsNullOrEmpty(this._pattern) && string.IsNullOrEmpty(this._maxExclusive) && string.IsNullOrEmpty(this._maxInclusive) && string.IsNullOrEmpty(this._minExclusive) && string.IsNullOrEmpty(this._minInclusive) && string.IsNullOrEmpty(this._enumeration);
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00027E53 File Offset: 0x00026053
		internal string BaseType
		{
			get
			{
				return this._baseType;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00027E5B File Offset: 0x0002605B
		internal XmlQualifiedName XmlBaseType
		{
			get
			{
				return this._xmlBaseType;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00027E63 File Offset: 0x00026063
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00027E6B File Offset: 0x0002606B
		internal string Namespace
		{
			get
			{
				return this._ns;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00027E73 File Offset: 0x00026073
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00027E7B File Offset: 0x0002607B
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x00027E83 File Offset: 0x00026083
		internal int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00027E8C File Offset: 0x0002608C
		internal SimpleType BaseSimpleType
		{
			get
			{
				return this._baseSimpleType;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00027E94 File Offset: 0x00026094
		public string SimpleTypeQualifiedName
		{
			get
			{
				if (this._ns.Length == 0)
				{
					return this._name;
				}
				return this._ns + ":" + this._name;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00027EC0 File Offset: 0x000260C0
		internal string QualifiedName(string name)
		{
			if (name.IndexOf(':') == -1)
			{
				return "xs:" + name;
			}
			return name;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00027EDC File Offset: 0x000260DC
		internal XmlNode ToNode(XmlDocument dc, Hashtable prefixes, bool inRemoting)
		{
			XmlElement xmlElement = dc.CreateElement("xs", "simpleType", "http://www.w3.org/2001/XMLSchema");
			if (this._name != null && this._name.Length != 0)
			{
				xmlElement.SetAttribute("name", this._name);
				if (inRemoting)
				{
					xmlElement.SetAttribute("targetNamespace", "urn:schemas-microsoft-com:xml-msdata", this.Namespace);
				}
			}
			XmlElement xmlElement2 = dc.CreateElement("xs", "restriction", "http://www.w3.org/2001/XMLSchema");
			if (!inRemoting)
			{
				if (this._baseSimpleType != null)
				{
					if (this._baseSimpleType.Namespace != null && this._baseSimpleType.Namespace.Length > 0)
					{
						string text = ((prefixes != null) ? ((string)prefixes[this._baseSimpleType.Namespace]) : null);
						if (text != null)
						{
							xmlElement2.SetAttribute("base", text + ":" + this._baseSimpleType.Name);
						}
						else
						{
							xmlElement2.SetAttribute("base", this._baseSimpleType.Name);
						}
					}
					else
					{
						xmlElement2.SetAttribute("base", this._baseSimpleType.Name);
					}
				}
				else
				{
					xmlElement2.SetAttribute("base", this.QualifiedName(this._baseType));
				}
			}
			else
			{
				xmlElement2.SetAttribute("base", (this._baseSimpleType != null) ? this._baseSimpleType.Name : this.QualifiedName(this._baseType));
			}
			if (this._length >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "length", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this._length.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			if (this._maxLength >= 0)
			{
				XmlElement xmlElement3 = dc.CreateElement("xs", "maxLength", "http://www.w3.org/2001/XMLSchema");
				xmlElement3.SetAttribute("value", this._maxLength.ToString(CultureInfo.InvariantCulture));
				xmlElement2.AppendChild(xmlElement3);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000280CB File Offset: 0x000262CB
		internal static SimpleType CreateEnumeratedType(string values)
		{
			return new SimpleType("string")
			{
				_enumeration = values
			};
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000280DE File Offset: 0x000262DE
		internal static SimpleType CreateByteArrayType(string encoding)
		{
			return new SimpleType("base64Binary");
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000280EA File Offset: 0x000262EA
		internal static SimpleType CreateLimitedStringType(int length)
		{
			return new SimpleType("string")
			{
				_maxLength = length
			};
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000280FD File Offset: 0x000262FD
		internal static SimpleType CreateSimpleType(StorageType typeCode, Type type)
		{
			if (typeCode == StorageType.Char && type == typeof(char))
			{
				return new SimpleType("string")
				{
					_length = 1
				};
			}
			return null;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00028128 File Offset: 0x00026328
		internal string HasConflictingDefinition(SimpleType otherSimpleType)
		{
			if (otherSimpleType == null)
			{
				return "otherSimpleType";
			}
			if (this.MaxLength != otherSimpleType.MaxLength)
			{
				return "MaxLength";
			}
			if (!string.Equals(this.BaseType, otherSimpleType.BaseType, StringComparison.Ordinal))
			{
				return "BaseType";
			}
			if (this.BaseSimpleType != null && otherSimpleType.BaseSimpleType != null && this.BaseSimpleType.HasConflictingDefinition(otherSimpleType.BaseSimpleType).Length != 0)
			{
				return "BaseSimpleType";
			}
			return string.Empty;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000281A0 File Offset: 0x000263A0
		internal bool CanHaveMaxLength()
		{
			SimpleType simpleType = this;
			while (simpleType.BaseSimpleType != null)
			{
				simpleType = simpleType.BaseSimpleType;
			}
			return string.Equals(simpleType.BaseType, "string", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000281D4 File Offset: 0x000263D4
		internal void ConvertToAnnonymousSimpleType()
		{
			this._name = null;
			this._ns = string.Empty;
			SimpleType simpleType = this;
			while (simpleType._baseSimpleType != null)
			{
				simpleType = simpleType._baseSimpleType;
			}
			this._baseType = simpleType._baseType;
			this._baseSimpleType = simpleType._baseSimpleType;
			this._xmlBaseType = simpleType._xmlBaseType;
		}

		// Token: 0x0400031A RID: 794
		private string _baseType;

		// Token: 0x0400031B RID: 795
		private SimpleType _baseSimpleType;

		// Token: 0x0400031C RID: 796
		private XmlQualifiedName _xmlBaseType;

		// Token: 0x0400031D RID: 797
		private string _name = string.Empty;

		// Token: 0x0400031E RID: 798
		private int _length = -1;

		// Token: 0x0400031F RID: 799
		private int _minLength = -1;

		// Token: 0x04000320 RID: 800
		private int _maxLength = -1;

		// Token: 0x04000321 RID: 801
		private string _pattern = string.Empty;

		// Token: 0x04000322 RID: 802
		private string _ns = string.Empty;

		// Token: 0x04000323 RID: 803
		private string _maxExclusive = string.Empty;

		// Token: 0x04000324 RID: 804
		private string _maxInclusive = string.Empty;

		// Token: 0x04000325 RID: 805
		private string _minExclusive = string.Empty;

		// Token: 0x04000326 RID: 806
		private string _minInclusive = string.Empty;

		// Token: 0x04000327 RID: 807
		internal string _enumeration = string.Empty;
	}
}
