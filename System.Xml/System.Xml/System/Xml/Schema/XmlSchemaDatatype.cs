using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.Schema
{
	/// <summary>The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> class is an abstract class for mapping XML Schema definition language (XSD) types to Common Language Runtime (CLR) types.</summary>
	// Token: 0x020002CB RID: 715
	public abstract class XmlSchemaDatatype
	{
		/// <summary>When overridden in a derived class, gets the Common Language Runtime (CLR) type of the item.</summary>
		/// <returns>The Common Language Runtime (CLR) type of the item.</returns>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060020AF RID: 8367
		public abstract Type ValueType { get; }

		/// <summary>When overridden in a derived class, gets the type for the string as specified in the World Wide Web Consortium (W3C) XML 1.0 specification.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlTokenizedType" /> value for the string.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060020B0 RID: 8368
		public abstract XmlTokenizedType TokenizedType { get; }

		/// <summary>When overridden in a derived class, validates the string specified against a built-in or user-defined simple type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast safely to the type returned by the <see cref="P:System.Xml.Schema.XmlSchemaDatatype.ValueType" /> property.</returns>
		/// <param name="s">The string to validate against the simple type.</param>
		/// <param name="nameTable">The <see cref="T:System.Xml.XmlNameTable" /> to use for atomization while parsing the string if this <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> object represents the xs:NCName type. </param>
		/// <param name="nsmgr">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object to use while parsing the string if this <see cref="T:System.Xml.Schema.XmlSchemaDatatype" /> object represents the xs:QName type.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">The input value is not a valid instance of this W3C XML Schema type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value to parse cannot be null.</exception>
		// Token: 0x060020B1 RID: 8369
		public abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr);

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaDatatypeVariety" /> value for the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaDatatypeVariety" /> value for the simple type.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return XmlSchemaDatatypeVariety.Atomic;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlTypeCode" /> value for the simple type.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlTypeCode" /> value for the simple type.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		/// <summary>The <see cref="M:System.Xml.Schema.XmlSchemaDatatype.IsDerivedFrom(System.Xml.Schema.XmlSchemaDatatype)" /> method always returns false.</summary>
		/// <returns>Always returns false.</returns>
		/// <param name="datatype">The <see cref="T:System.Xml.Schema.XmlSchemaDatatype" />.</param>
		// Token: 0x060020B4 RID: 8372 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public virtual bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			return false;
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060020B5 RID: 8373
		internal abstract bool HasLexicalFacets { get; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060020B6 RID: 8374
		internal abstract bool HasValueFacets { get; }

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060020B7 RID: 8375
		internal abstract XmlValueConverter ValueConverter { get; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060020B8 RID: 8376
		internal abstract RestrictionFacets Restriction { get; }

		// Token: 0x060020B9 RID: 8377
		internal abstract int Compare(object value1, object value2);

		// Token: 0x060020BA RID: 8378
		internal abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, bool createAtomicValue);

		// Token: 0x060020BB RID: 8379
		internal abstract Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue);

		// Token: 0x060020BC RID: 8380
		internal abstract Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue);

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060020BD RID: 8381
		internal abstract FacetsChecker FacetsChecker { get; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060020BE RID: 8382
		internal abstract XmlSchemaWhiteSpace BuiltInWhitespaceFacet { get; }

		// Token: 0x060020BF RID: 8383
		internal abstract XmlSchemaDatatype DeriveByRestriction(XmlSchemaObjectCollection facets, XmlNameTable nameTable, XmlSchemaType schemaType);

		// Token: 0x060020C0 RID: 8384
		internal abstract XmlSchemaDatatype DeriveByList(XmlSchemaType schemaType);

		// Token: 0x060020C1 RID: 8385
		internal abstract void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller);

		// Token: 0x060020C2 RID: 8386
		internal abstract bool IsEqual(object o1, object o2);

		// Token: 0x060020C3 RID: 8387
		internal abstract bool IsComparable(XmlSchemaDatatype dtype);

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x000BF5E4 File Offset: 0x000BD7E4
		internal string TypeCodeString
		{
			get
			{
				string text = string.Empty;
				XmlTypeCode typeCode = this.TypeCode;
				switch (this.Variety)
				{
				case XmlSchemaDatatypeVariety.Atomic:
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						text = "anySimpleType";
					}
					else
					{
						text = this.TypeCodeToString(typeCode);
					}
					break;
				case XmlSchemaDatatypeVariety.List:
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						text = "List of Union";
					}
					else
					{
						text = "List of " + this.TypeCodeToString(typeCode);
					}
					break;
				case XmlSchemaDatatypeVariety.Union:
					text = "Union";
					break;
				}
				return text;
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000BF658 File Offset: 0x000BD858
		internal string TypeCodeToString(XmlTypeCode typeCode)
		{
			switch (typeCode)
			{
			case XmlTypeCode.None:
				return "None";
			case XmlTypeCode.Item:
				return "AnyType";
			case XmlTypeCode.AnyAtomicType:
				return "AnyAtomicType";
			case XmlTypeCode.String:
				return "String";
			case XmlTypeCode.Boolean:
				return "Boolean";
			case XmlTypeCode.Decimal:
				return "Decimal";
			case XmlTypeCode.Float:
				return "Float";
			case XmlTypeCode.Double:
				return "Double";
			case XmlTypeCode.Duration:
				return "Duration";
			case XmlTypeCode.DateTime:
				return "DateTime";
			case XmlTypeCode.Time:
				return "Time";
			case XmlTypeCode.Date:
				return "Date";
			case XmlTypeCode.GYearMonth:
				return "GYearMonth";
			case XmlTypeCode.GYear:
				return "GYear";
			case XmlTypeCode.GMonthDay:
				return "GMonthDay";
			case XmlTypeCode.GDay:
				return "GDay";
			case XmlTypeCode.GMonth:
				return "GMonth";
			case XmlTypeCode.HexBinary:
				return "HexBinary";
			case XmlTypeCode.Base64Binary:
				return "Base64Binary";
			case XmlTypeCode.AnyUri:
				return "AnyUri";
			case XmlTypeCode.QName:
				return "QName";
			case XmlTypeCode.Notation:
				return "Notation";
			case XmlTypeCode.NormalizedString:
				return "NormalizedString";
			case XmlTypeCode.Token:
				return "Token";
			case XmlTypeCode.Language:
				return "Language";
			case XmlTypeCode.NmToken:
				return "NmToken";
			case XmlTypeCode.Name:
				return "Name";
			case XmlTypeCode.NCName:
				return "NCName";
			case XmlTypeCode.Id:
				return "Id";
			case XmlTypeCode.Idref:
				return "Idref";
			case XmlTypeCode.Entity:
				return "Entity";
			case XmlTypeCode.Integer:
				return "Integer";
			case XmlTypeCode.NonPositiveInteger:
				return "NonPositiveInteger";
			case XmlTypeCode.NegativeInteger:
				return "NegativeInteger";
			case XmlTypeCode.Long:
				return "Long";
			case XmlTypeCode.Int:
				return "Int";
			case XmlTypeCode.Short:
				return "Short";
			case XmlTypeCode.Byte:
				return "Byte";
			case XmlTypeCode.NonNegativeInteger:
				return "NonNegativeInteger";
			case XmlTypeCode.UnsignedLong:
				return "UnsignedLong";
			case XmlTypeCode.UnsignedInt:
				return "UnsignedInt";
			case XmlTypeCode.UnsignedShort:
				return "UnsignedShort";
			case XmlTypeCode.UnsignedByte:
				return "UnsignedByte";
			case XmlTypeCode.PositiveInteger:
				return "PositiveInteger";
			}
			return typeCode.ToString();
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000BF85C File Offset: 0x000BDA5C
		internal static string ConcatenatedToString(object value)
		{
			Type type = value.GetType();
			string text = string.Empty;
			if (type == typeof(IEnumerable) && type != typeof(string))
			{
				StringBuilder stringBuilder = new StringBuilder();
				IEnumerator enumerator = (value as IEnumerable).GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("{");
					object obj = enumerator.Current;
					if (obj is IFormattable)
					{
						stringBuilder.Append(((IFormattable)obj).ToString("", CultureInfo.InvariantCulture));
					}
					else
					{
						stringBuilder.Append(obj.ToString());
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(" , ");
						obj = enumerator.Current;
						if (obj is IFormattable)
						{
							stringBuilder.Append(((IFormattable)obj).ToString("", CultureInfo.InvariantCulture));
						}
						else
						{
							stringBuilder.Append(obj.ToString());
						}
					}
					stringBuilder.Append("}");
					text = stringBuilder.ToString();
				}
			}
			else if (value is IFormattable)
			{
				text = ((IFormattable)value).ToString("", CultureInfo.InvariantCulture);
			}
			else
			{
				text = value.ToString();
			}
			return text;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000BF998 File Offset: 0x000BDB98
		internal static XmlSchemaDatatype FromXmlTokenizedType(XmlTokenizedType token)
		{
			return DatatypeImplementation.FromXmlTokenizedType(token);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000BF9A0 File Offset: 0x000BDBA0
		internal static XmlSchemaDatatype FromXmlTokenizedTypeXsd(XmlTokenizedType token)
		{
			return DatatypeImplementation.FromXmlTokenizedTypeXsd(token);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000BF9A8 File Offset: 0x000BDBA8
		internal static XmlSchemaDatatype FromXdrName(string name)
		{
			return DatatypeImplementation.FromXdrName(name);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000BF9B0 File Offset: 0x000BDBB0
		internal static XmlSchemaDatatype DeriveByUnion(XmlSchemaSimpleType[] types, XmlSchemaType schemaType)
		{
			return DatatypeImplementation.DeriveByUnion(types, schemaType);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000BF9BC File Offset: 0x000BDBBC
		internal static string XdrCanonizeUri(string uri, XmlNameTable nameTable, SchemaNames schemaNames)
		{
			int num = 5;
			bool flag = false;
			if (uri.Length > 5 && uri.StartsWith("uuid:", StringComparison.Ordinal))
			{
				flag = true;
			}
			else if (uri.Length > 9 && uri.StartsWith("urn:uuid:", StringComparison.Ordinal))
			{
				flag = true;
				num = 9;
			}
			string text;
			if (flag)
			{
				text = nameTable.Add(uri.Substring(0, num) + uri.Substring(num, uri.Length - num).ToUpper(CultureInfo.InvariantCulture));
			}
			else
			{
				text = uri;
			}
			if (Ref.Equal(schemaNames.NsDataTypeAlias, text) || Ref.Equal(schemaNames.NsDataTypeOld, text))
			{
				text = schemaNames.NsDataType;
			}
			else if (Ref.Equal(schemaNames.NsXdrAlias, text))
			{
				text = schemaNames.NsXdr;
			}
			return text;
		}
	}
}
