using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000235 RID: 565
	internal abstract class DatatypeImplementation : XmlSchemaDatatype
	{
		// Token: 0x06001B15 RID: 6933 RVA: 0x0009C6B4 File Offset: 0x0009A8B4
		static DatatypeImplementation()
		{
			DatatypeImplementation[] array = new DatatypeImplementation[13];
			array[0] = DatatypeImplementation.c_string;
			array[1] = DatatypeImplementation.c_ID;
			array[2] = DatatypeImplementation.c_IDREF;
			array[3] = DatatypeImplementation.c_IDREFS;
			array[4] = DatatypeImplementation.c_ENTITY;
			array[5] = DatatypeImplementation.c_ENTITIES;
			array[6] = DatatypeImplementation.c_NMTOKEN;
			array[7] = DatatypeImplementation.c_NMTOKENS;
			array[8] = DatatypeImplementation.c_NOTATION;
			array[9] = DatatypeImplementation.c_ENUMERATION;
			array[10] = DatatypeImplementation.c_QNameXdr;
			array[11] = DatatypeImplementation.c_NCName;
			DatatypeImplementation.c_tokenizedTypes = array;
			DatatypeImplementation[] array2 = new DatatypeImplementation[13];
			array2[0] = DatatypeImplementation.c_string;
			array2[1] = DatatypeImplementation.c_ID;
			array2[2] = DatatypeImplementation.c_IDREF;
			array2[3] = DatatypeImplementation.c_IDREFS;
			array2[4] = DatatypeImplementation.c_ENTITY;
			array2[5] = DatatypeImplementation.c_ENTITIES;
			array2[6] = DatatypeImplementation.c_NMTOKEN;
			array2[7] = DatatypeImplementation.c_NMTOKENS;
			array2[8] = DatatypeImplementation.c_NOTATION;
			array2[9] = DatatypeImplementation.c_ENUMERATION;
			array2[10] = DatatypeImplementation.c_QName;
			array2[11] = DatatypeImplementation.c_NCName;
			DatatypeImplementation.c_tokenizedTypesXsd = array2;
			DatatypeImplementation.c_XdrTypes = new DatatypeImplementation.SchemaDatatypeMap[]
			{
				new DatatypeImplementation.SchemaDatatypeMap("bin.base64", DatatypeImplementation.c_base64Binary),
				new DatatypeImplementation.SchemaDatatypeMap("bin.hex", DatatypeImplementation.c_hexBinary),
				new DatatypeImplementation.SchemaDatatypeMap("boolean", DatatypeImplementation.c_boolean),
				new DatatypeImplementation.SchemaDatatypeMap("char", DatatypeImplementation.c_char),
				new DatatypeImplementation.SchemaDatatypeMap("date", DatatypeImplementation.c_date),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime", DatatypeImplementation.c_dateTimeNoTz),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime.tz", DatatypeImplementation.c_dateTimeTz),
				new DatatypeImplementation.SchemaDatatypeMap("decimal", DatatypeImplementation.c_decimal),
				new DatatypeImplementation.SchemaDatatypeMap("entities", DatatypeImplementation.c_ENTITIES),
				new DatatypeImplementation.SchemaDatatypeMap("entity", DatatypeImplementation.c_ENTITY),
				new DatatypeImplementation.SchemaDatatypeMap("enumeration", DatatypeImplementation.c_ENUMERATION),
				new DatatypeImplementation.SchemaDatatypeMap("fixed.14.4", DatatypeImplementation.c_fixed),
				new DatatypeImplementation.SchemaDatatypeMap("float", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("float.ieee.754.32", DatatypeImplementation.c_floatXdr),
				new DatatypeImplementation.SchemaDatatypeMap("float.ieee.754.64", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("i1", DatatypeImplementation.c_byte),
				new DatatypeImplementation.SchemaDatatypeMap("i2", DatatypeImplementation.c_short),
				new DatatypeImplementation.SchemaDatatypeMap("i4", DatatypeImplementation.c_int),
				new DatatypeImplementation.SchemaDatatypeMap("i8", DatatypeImplementation.c_long),
				new DatatypeImplementation.SchemaDatatypeMap("id", DatatypeImplementation.c_ID),
				new DatatypeImplementation.SchemaDatatypeMap("idref", DatatypeImplementation.c_IDREF),
				new DatatypeImplementation.SchemaDatatypeMap("idrefs", DatatypeImplementation.c_IDREFS),
				new DatatypeImplementation.SchemaDatatypeMap("int", DatatypeImplementation.c_int),
				new DatatypeImplementation.SchemaDatatypeMap("nmtoken", DatatypeImplementation.c_NMTOKEN),
				new DatatypeImplementation.SchemaDatatypeMap("nmtokens", DatatypeImplementation.c_NMTOKENS),
				new DatatypeImplementation.SchemaDatatypeMap("notation", DatatypeImplementation.c_NOTATION),
				new DatatypeImplementation.SchemaDatatypeMap("number", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("r4", DatatypeImplementation.c_floatXdr),
				new DatatypeImplementation.SchemaDatatypeMap("r8", DatatypeImplementation.c_doubleXdr),
				new DatatypeImplementation.SchemaDatatypeMap("string", DatatypeImplementation.c_string),
				new DatatypeImplementation.SchemaDatatypeMap("time", DatatypeImplementation.c_timeNoTz),
				new DatatypeImplementation.SchemaDatatypeMap("time.tz", DatatypeImplementation.c_timeTz),
				new DatatypeImplementation.SchemaDatatypeMap("ui1", DatatypeImplementation.c_unsignedByte),
				new DatatypeImplementation.SchemaDatatypeMap("ui2", DatatypeImplementation.c_unsignedShort),
				new DatatypeImplementation.SchemaDatatypeMap("ui4", DatatypeImplementation.c_unsignedInt),
				new DatatypeImplementation.SchemaDatatypeMap("ui8", DatatypeImplementation.c_unsignedLong),
				new DatatypeImplementation.SchemaDatatypeMap("uri", DatatypeImplementation.c_anyURI),
				new DatatypeImplementation.SchemaDatatypeMap("uuid", DatatypeImplementation.c_uuid)
			};
			DatatypeImplementation.c_XsdTypes = new DatatypeImplementation.SchemaDatatypeMap[]
			{
				new DatatypeImplementation.SchemaDatatypeMap("ENTITIES", DatatypeImplementation.c_ENTITIES, 11),
				new DatatypeImplementation.SchemaDatatypeMap("ENTITY", DatatypeImplementation.c_ENTITY, 11),
				new DatatypeImplementation.SchemaDatatypeMap("ID", DatatypeImplementation.c_ID, 5),
				new DatatypeImplementation.SchemaDatatypeMap("IDREF", DatatypeImplementation.c_IDREF, 5),
				new DatatypeImplementation.SchemaDatatypeMap("IDREFS", DatatypeImplementation.c_IDREFS, 11),
				new DatatypeImplementation.SchemaDatatypeMap("NCName", DatatypeImplementation.c_NCName, 9),
				new DatatypeImplementation.SchemaDatatypeMap("NMTOKEN", DatatypeImplementation.c_NMTOKEN, 40),
				new DatatypeImplementation.SchemaDatatypeMap("NMTOKENS", DatatypeImplementation.c_NMTOKENS, 11),
				new DatatypeImplementation.SchemaDatatypeMap("NOTATION", DatatypeImplementation.c_NOTATION, 11),
				new DatatypeImplementation.SchemaDatatypeMap("Name", DatatypeImplementation.c_Name, 40),
				new DatatypeImplementation.SchemaDatatypeMap("QName", DatatypeImplementation.c_QName, 11),
				new DatatypeImplementation.SchemaDatatypeMap("anySimpleType", DatatypeImplementation.c_anySimpleType, -1),
				new DatatypeImplementation.SchemaDatatypeMap("anyURI", DatatypeImplementation.c_anyURI, 11),
				new DatatypeImplementation.SchemaDatatypeMap("base64Binary", DatatypeImplementation.c_base64Binary, 11),
				new DatatypeImplementation.SchemaDatatypeMap("boolean", DatatypeImplementation.c_boolean, 11),
				new DatatypeImplementation.SchemaDatatypeMap("byte", DatatypeImplementation.c_byte, 37),
				new DatatypeImplementation.SchemaDatatypeMap("date", DatatypeImplementation.c_date, 11),
				new DatatypeImplementation.SchemaDatatypeMap("dateTime", DatatypeImplementation.c_dateTime, 11),
				new DatatypeImplementation.SchemaDatatypeMap("decimal", DatatypeImplementation.c_decimal, 11),
				new DatatypeImplementation.SchemaDatatypeMap("double", DatatypeImplementation.c_double, 11),
				new DatatypeImplementation.SchemaDatatypeMap("duration", DatatypeImplementation.c_duration, 11),
				new DatatypeImplementation.SchemaDatatypeMap("float", DatatypeImplementation.c_float, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gDay", DatatypeImplementation.c_day, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gMonth", DatatypeImplementation.c_month, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gMonthDay", DatatypeImplementation.c_monthDay, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gYear", DatatypeImplementation.c_year, 11),
				new DatatypeImplementation.SchemaDatatypeMap("gYearMonth", DatatypeImplementation.c_yearMonth, 11),
				new DatatypeImplementation.SchemaDatatypeMap("hexBinary", DatatypeImplementation.c_hexBinary, 11),
				new DatatypeImplementation.SchemaDatatypeMap("int", DatatypeImplementation.c_int, 31),
				new DatatypeImplementation.SchemaDatatypeMap("integer", DatatypeImplementation.c_integer, 18),
				new DatatypeImplementation.SchemaDatatypeMap("language", DatatypeImplementation.c_language, 40),
				new DatatypeImplementation.SchemaDatatypeMap("long", DatatypeImplementation.c_long, 29),
				new DatatypeImplementation.SchemaDatatypeMap("negativeInteger", DatatypeImplementation.c_negativeInteger, 34),
				new DatatypeImplementation.SchemaDatatypeMap("nonNegativeInteger", DatatypeImplementation.c_nonNegativeInteger, 29),
				new DatatypeImplementation.SchemaDatatypeMap("nonPositiveInteger", DatatypeImplementation.c_nonPositiveInteger, 29),
				new DatatypeImplementation.SchemaDatatypeMap("normalizedString", DatatypeImplementation.c_normalizedString, 38),
				new DatatypeImplementation.SchemaDatatypeMap("positiveInteger", DatatypeImplementation.c_positiveInteger, 33),
				new DatatypeImplementation.SchemaDatatypeMap("short", DatatypeImplementation.c_short, 28),
				new DatatypeImplementation.SchemaDatatypeMap("string", DatatypeImplementation.c_string, 11),
				new DatatypeImplementation.SchemaDatatypeMap("time", DatatypeImplementation.c_time, 11),
				new DatatypeImplementation.SchemaDatatypeMap("token", DatatypeImplementation.c_token, 35),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedByte", DatatypeImplementation.c_unsignedByte, 44),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedInt", DatatypeImplementation.c_unsignedInt, 43),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedLong", DatatypeImplementation.c_unsignedLong, 33),
				new DatatypeImplementation.SchemaDatatypeMap("unsignedShort", DatatypeImplementation.c_unsignedShort, 42)
			};
			DatatypeImplementation.CreateBuiltinTypes();
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x0009D152 File Offset: 0x0009B352
		internal static XmlSchemaSimpleType AnySimpleType
		{
			get
			{
				return DatatypeImplementation.anySimpleType;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0009D159 File Offset: 0x0009B359
		internal static XmlSchemaSimpleType UntypedAtomicType
		{
			get
			{
				return DatatypeImplementation.untypedAtomicType;
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0009D160 File Offset: 0x0009B360
		internal new static DatatypeImplementation FromXmlTokenizedType(XmlTokenizedType token)
		{
			return DatatypeImplementation.c_tokenizedTypes[(int)token];
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0009D169 File Offset: 0x0009B369
		internal new static DatatypeImplementation FromXmlTokenizedTypeXsd(XmlTokenizedType token)
		{
			return DatatypeImplementation.c_tokenizedTypesXsd[(int)token];
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0009D174 File Offset: 0x0009B374
		internal new static DatatypeImplementation FromXdrName(string name)
		{
			int num = Array.BinarySearch(DatatypeImplementation.c_XdrTypes, name, null);
			if (num >= 0)
			{
				return (DatatypeImplementation)DatatypeImplementation.c_XdrTypes[num];
			}
			return null;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0009D1A0 File Offset: 0x0009B3A0
		private static DatatypeImplementation FromTypeName(string name)
		{
			int num = Array.BinarySearch(DatatypeImplementation.c_XsdTypes, name, null);
			if (num >= 0)
			{
				return (DatatypeImplementation)DatatypeImplementation.c_XsdTypes[num];
			}
			return null;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0009D1CC File Offset: 0x0009B3CC
		internal static XmlSchemaSimpleType StartBuiltinType(XmlQualifiedName qname, XmlSchemaDatatype dataType)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.SetQualifiedName(qname);
			xmlSchemaSimpleType.SetDatatype(dataType);
			xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(dataType);
			xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
			return xmlSchemaSimpleType;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0009D208 File Offset: 0x0009B408
		internal static void FinishBuiltinType(XmlSchemaSimpleType derivedType, XmlSchemaSimpleType baseType)
		{
			derivedType.SetBaseSchemaType(baseType);
			derivedType.SetDerivedBy(XmlSchemaDerivationMethod.Restriction);
			if (derivedType.Datatype.Variety == XmlSchemaDatatypeVariety.Atomic)
			{
				derivedType.Content = new XmlSchemaSimpleTypeRestriction
				{
					BaseTypeName = baseType.QualifiedName
				};
			}
			if (derivedType.Datatype.Variety == XmlSchemaDatatypeVariety.List)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
				derivedType.SetDerivedBy(XmlSchemaDerivationMethod.List);
				XmlTypeCode typeCode = derivedType.Datatype.TypeCode;
				if (typeCode != XmlTypeCode.NmToken)
				{
					if (typeCode != XmlTypeCode.Idref)
					{
						if (typeCode == XmlTypeCode.Entity)
						{
							xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[39]);
						}
					}
					else
					{
						xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[38]);
					}
				}
				else
				{
					xmlSchemaSimpleTypeList.ItemType = (xmlSchemaSimpleTypeList.BaseItemType = DatatypeImplementation.enumToTypeCode[34]);
				}
				derivedType.Content = xmlSchemaSimpleTypeList;
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0009D2D4 File Offset: 0x0009B4D4
		internal static void CreateBuiltinTypes()
		{
			DatatypeImplementation.SchemaDatatypeMap schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[11];
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema");
			DatatypeImplementation datatypeImplementation = DatatypeImplementation.FromTypeName(xmlQualifiedName.Name);
			DatatypeImplementation.anySimpleType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, datatypeImplementation);
			datatypeImplementation.parentSchemaType = DatatypeImplementation.anySimpleType;
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.anySimpleType);
			for (int i = 0; i < DatatypeImplementation.c_XsdTypes.Length; i++)
			{
				if (i != 11)
				{
					schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[i];
					xmlQualifiedName = new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema");
					datatypeImplementation = DatatypeImplementation.FromTypeName(xmlQualifiedName.Name);
					XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, datatypeImplementation);
					datatypeImplementation.parentSchemaType = xmlSchemaSimpleType;
					DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, xmlSchemaSimpleType);
					if (datatypeImplementation.variety == XmlSchemaDatatypeVariety.Atomic)
					{
						DatatypeImplementation.enumToTypeCode[(int)datatypeImplementation.TypeCode] = xmlSchemaSimpleType;
					}
				}
			}
			for (int j = 0; j < DatatypeImplementation.c_XsdTypes.Length; j++)
			{
				if (j != 11)
				{
					schemaDatatypeMap = DatatypeImplementation.c_XsdTypes[j];
					XmlSchemaSimpleType xmlSchemaSimpleType2 = (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[new XmlQualifiedName(schemaDatatypeMap.Name, "http://www.w3.org/2001/XMLSchema")];
					if (schemaDatatypeMap.ParentIndex == 11)
					{
						DatatypeImplementation.FinishBuiltinType(xmlSchemaSimpleType2, DatatypeImplementation.anySimpleType);
					}
					else
					{
						XmlSchemaSimpleType xmlSchemaSimpleType3 = (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[new XmlQualifiedName(DatatypeImplementation.c_XsdTypes[schemaDatatypeMap.ParentIndex].Name, "http://www.w3.org/2001/XMLSchema")];
						DatatypeImplementation.FinishBuiltinType(xmlSchemaSimpleType2, xmlSchemaSimpleType3);
					}
				}
			}
			xmlQualifiedName = new XmlQualifiedName("anyAtomicType", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.anyAtomicType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_anyAtomicType);
			DatatypeImplementation.c_anyAtomicType.parentSchemaType = DatatypeImplementation.anyAtomicType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.anyAtomicType, DatatypeImplementation.anySimpleType);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.anyAtomicType);
			DatatypeImplementation.enumToTypeCode[10] = DatatypeImplementation.anyAtomicType;
			xmlQualifiedName = new XmlQualifiedName("untypedAtomic", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.untypedAtomicType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_untypedAtomicType);
			DatatypeImplementation.c_untypedAtomicType.parentSchemaType = DatatypeImplementation.untypedAtomicType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.untypedAtomicType, DatatypeImplementation.anyAtomicType);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.untypedAtomicType);
			DatatypeImplementation.enumToTypeCode[11] = DatatypeImplementation.untypedAtomicType;
			xmlQualifiedName = new XmlQualifiedName("yearMonthDuration", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.yearMonthDurationType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_yearMonthDuration);
			DatatypeImplementation.c_yearMonthDuration.parentSchemaType = DatatypeImplementation.yearMonthDurationType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.yearMonthDurationType, DatatypeImplementation.enumToTypeCode[17]);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.yearMonthDurationType);
			DatatypeImplementation.enumToTypeCode[53] = DatatypeImplementation.yearMonthDurationType;
			xmlQualifiedName = new XmlQualifiedName("dayTimeDuration", "http://www.w3.org/2003/11/xpath-datatypes");
			DatatypeImplementation.dayTimeDurationType = DatatypeImplementation.StartBuiltinType(xmlQualifiedName, DatatypeImplementation.c_dayTimeDuration);
			DatatypeImplementation.c_dayTimeDuration.parentSchemaType = DatatypeImplementation.dayTimeDurationType;
			DatatypeImplementation.FinishBuiltinType(DatatypeImplementation.dayTimeDurationType, DatatypeImplementation.enumToTypeCode[17]);
			DatatypeImplementation.builtinTypes.Add(xmlQualifiedName, DatatypeImplementation.dayTimeDurationType);
			DatatypeImplementation.enumToTypeCode[54] = DatatypeImplementation.dayTimeDurationType;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0009D5AB File Offset: 0x0009B7AB
		internal static XmlSchemaSimpleType GetSimpleTypeFromTypeCode(XmlTypeCode typeCode)
		{
			return DatatypeImplementation.enumToTypeCode[(int)typeCode];
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0009D5B4 File Offset: 0x0009B7B4
		internal static XmlSchemaSimpleType GetSimpleTypeFromXsdType(XmlQualifiedName qname)
		{
			return (XmlSchemaSimpleType)DatatypeImplementation.builtinTypes[qname];
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0009D5C8 File Offset: 0x0009B7C8
		internal static XmlSchemaSimpleType GetNormalizedStringTypeV1Compat()
		{
			if (DatatypeImplementation.normalizedStringTypeV1Compat == null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.NormalizedString).Clone() as XmlSchemaSimpleType;
				xmlSchemaSimpleType.SetDatatype(DatatypeImplementation.c_normalizedStringV1Compat);
				xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(DatatypeImplementation.c_normalizedStringV1Compat);
				xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
				DatatypeImplementation.normalizedStringTypeV1Compat = xmlSchemaSimpleType;
			}
			return DatatypeImplementation.normalizedStringTypeV1Compat;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0009D628 File Offset: 0x0009B828
		internal static XmlSchemaSimpleType GetTokenTypeV1Compat()
		{
			if (DatatypeImplementation.tokenTypeV1Compat == null)
			{
				XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.Token).Clone() as XmlSchemaSimpleType;
				xmlSchemaSimpleType.SetDatatype(DatatypeImplementation.c_tokenV1Compat);
				xmlSchemaSimpleType.ElementDecl = new SchemaElementDecl(DatatypeImplementation.c_tokenV1Compat);
				xmlSchemaSimpleType.ElementDecl.SchemaType = xmlSchemaSimpleType;
				DatatypeImplementation.tokenTypeV1Compat = xmlSchemaSimpleType;
			}
			return DatatypeImplementation.tokenTypeV1Compat;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0009D686 File Offset: 0x0009B886
		internal static XmlSchemaSimpleType[] GetBuiltInTypes()
		{
			return DatatypeImplementation.enumToTypeCode;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0009D690 File Offset: 0x0009B890
		internal static XmlTypeCode GetPrimitiveTypeCode(XmlTypeCode typeCode)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = DatatypeImplementation.enumToTypeCode[(int)typeCode];
			while (xmlSchemaSimpleType.BaseXmlSchemaType != DatatypeImplementation.AnySimpleType)
			{
				xmlSchemaSimpleType = xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType;
			}
			return xmlSchemaSimpleType.TypeCode;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0009D6C6 File Offset: 0x0009B8C6
		internal override XmlSchemaDatatype DeriveByRestriction(XmlSchemaObjectCollection facets, XmlNameTable nameTable, XmlSchemaType schemaType)
		{
			DatatypeImplementation datatypeImplementation = (DatatypeImplementation)base.MemberwiseClone();
			datatypeImplementation.restriction = this.FacetsChecker.ConstructRestriction(this, facets, nameTable);
			datatypeImplementation.baseType = this;
			datatypeImplementation.parentSchemaType = schemaType;
			datatypeImplementation.valueConverter = null;
			return datatypeImplementation;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0009D6FC File Offset: 0x0009B8FC
		internal override XmlSchemaDatatype DeriveByList(XmlSchemaType schemaType)
		{
			return this.DeriveByList(0, schemaType);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0009D708 File Offset: 0x0009B908
		internal XmlSchemaDatatype DeriveByList(int minSize, XmlSchemaType schemaType)
		{
			if (this.variety == XmlSchemaDatatypeVariety.List)
			{
				throw new XmlSchemaException("A list data type must be derived from an atomic or union data type.", string.Empty);
			}
			if (this.variety == XmlSchemaDatatypeVariety.Union && !((Datatype_union)this).HasAtomicMembers())
			{
				throw new XmlSchemaException("A list data type must be derived from an atomic or union data type.", string.Empty);
			}
			return new Datatype_List(this, minSize)
			{
				variety = XmlSchemaDatatypeVariety.List,
				restriction = null,
				baseType = DatatypeImplementation.c_anySimpleType,
				parentSchemaType = schemaType
			};
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0009D77B File Offset: 0x0009B97B
		internal new static DatatypeImplementation DeriveByUnion(XmlSchemaSimpleType[] types, XmlSchemaType schemaType)
		{
			return new Datatype_union(types)
			{
				baseType = DatatypeImplementation.c_anySimpleType,
				variety = XmlSchemaDatatypeVariety.Union,
				parentSchemaType = schemaType
			};
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0009D79C File Offset: 0x0009B99C
		public override bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			if (datatype == null)
			{
				return false;
			}
			for (DatatypeImplementation datatypeImplementation = this; datatypeImplementation != null; datatypeImplementation = datatypeImplementation.baseType)
			{
				if (datatypeImplementation == datatype)
				{
					return true;
				}
			}
			if (((DatatypeImplementation)datatype).baseType == null)
			{
				Type type = base.GetType();
				Type type2 = datatype.GetType();
				return type2 == type || type.IsSubclassOf(type2);
			}
			if (datatype.Variety == XmlSchemaDatatypeVariety.Union && !datatype.HasLexicalFacets && !datatype.HasValueFacets && this.variety != XmlSchemaDatatypeVariety.Union)
			{
				return ((Datatype_union)datatype).IsUnionBaseOf(this);
			}
			return (this.variety == XmlSchemaDatatypeVariety.Union || this.variety == XmlSchemaDatatypeVariety.List) && this.restriction == null && datatype == DatatypeImplementation.anySimpleType.Datatype;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0009D848 File Offset: 0x0009BA48
		internal override bool IsEqual(object o1, object o2)
		{
			return this.Compare(o1, o2) == 0;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0009D858 File Offset: 0x0009BA58
		internal override bool IsComparable(XmlSchemaDatatype dtype)
		{
			XmlTypeCode typeCode = this.TypeCode;
			XmlTypeCode typeCode2 = dtype.TypeCode;
			return typeCode == typeCode2 || DatatypeImplementation.GetPrimitiveTypeCode(typeCode) == DatatypeImplementation.GetPrimitiveTypeCode(typeCode2) || (this.IsDerivedFrom(dtype) || dtype.IsDerivedFrom(this));
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00014C6C File Offset: 0x00012E6C
		internal virtual XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return null;
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0009D89E File Offset: 0x0009BA9E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x0009D8A5 File Offset: 0x0009BAA5
		internal override XmlValueConverter ValueConverter
		{
			get
			{
				if (this.valueConverter == null)
				{
					this.valueConverter = this.CreateValueConverter(this.parentSchemaType);
				}
				return this.valueConverter;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0000A2FC File Offset: 0x000084FC
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00015460 File Offset: 0x00013660
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0009D8C7 File Offset: 0x0009BAC7
		public override XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return this.variety;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0009D8CF File Offset: 0x0009BACF
		internal override RestrictionFacets Restriction
		{
			get
			{
				return this.restriction;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0009D8D8 File Offset: 0x0009BAD8
		internal override bool HasLexicalFacets
		{
			get
			{
				RestrictionFlags restrictionFlags = ((this.restriction != null) ? this.restriction.Flags : ((RestrictionFlags)0));
				return restrictionFlags != (RestrictionFlags)0 && (restrictionFlags & (RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) != (RestrictionFlags)0;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0009D90C File Offset: 0x0009BB0C
		internal override bool HasValueFacets
		{
			get
			{
				RestrictionFlags restrictionFlags = ((this.restriction != null) ? this.restriction.Flags : ((RestrictionFlags)0));
				return restrictionFlags != (RestrictionFlags)0 && (restrictionFlags & (RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Enumeration | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive | RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) != (RestrictionFlags)0;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0009D93F File Offset: 0x0009BB3F
		protected DatatypeImplementation Base
		{
			get
			{
				return this.baseType;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001B38 RID: 6968
		internal abstract Type ListValueType { get; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001B39 RID: 6969
		internal abstract RestrictionFlags ValidRestrictionFlags { get; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0009D948 File Offset: 0x0009BB48
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object obj;
			Exception ex = this.TryParseValue(s, nameTable, nsmgr, out obj);
			if (ex != null)
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its schema type '{1}' - {2}", new string[]
				{
					s,
					this.GetTypeName(),
					ex.Message
				}, ex, null, 0, 0, null);
			}
			if (this.Variety == XmlSchemaDatatypeVariety.Union)
			{
				return (obj as XsdSimpleValue).TypedValue;
			}
			return obj;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0009D9A8 File Offset: 0x0009BBA8
		internal override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, bool createAtomicValue)
		{
			if (!createAtomicValue)
			{
				return this.ParseValue(s, nameTable, nsmgr);
			}
			object obj;
			Exception ex = this.TryParseValue(s, nameTable, nsmgr, out obj);
			if (ex != null)
			{
				throw new XmlSchemaException("The value '{0}' is invalid according to its schema type '{1}' - {2}", new string[]
				{
					s,
					this.GetTypeName(),
					ex.Message
				}, ex, null, 0, 0, null);
			}
			return obj;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0009DA00 File Offset: 0x0009BC00
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue)
		{
			Exception ex = null;
			typedValue = null;
			if (value == null)
			{
				return new ArgumentNullException("value");
			}
			string text = value as string;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, namespaceResolver, out typedValue);
			}
			try
			{
				object obj = value;
				if (value.GetType() != this.ValueType)
				{
					obj = this.ValueConverter.ChangeType(value, this.ValueType, namespaceResolver);
				}
				if (this.HasLexicalFacets)
				{
					string text2 = (string)this.ValueConverter.ChangeType(value, typeof(string), namespaceResolver);
					ex = this.FacetsChecker.CheckLexicalFacets(ref text2, this);
					if (ex != null)
					{
						return ex;
					}
				}
				if (this.HasValueFacets)
				{
					ex = this.FacetsChecker.CheckValueFacets(obj, this);
					if (ex != null)
					{
						return ex;
					}
				}
				typedValue = obj;
				return null;
			}
			catch (FormatException ex)
			{
			}
			catch (InvalidCastException ex)
			{
			}
			catch (OverflowException ex)
			{
			}
			catch (ArgumentException ex)
			{
			}
			return ex;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0009DB04 File Offset: 0x0009BD04
		internal string GetTypeName()
		{
			XmlSchemaType xmlSchemaType = this.parentSchemaType;
			string text;
			if (xmlSchemaType == null || xmlSchemaType.QualifiedName.IsEmpty)
			{
				text = base.TypeCodeString;
			}
			else
			{
				text = xmlSchemaType.QualifiedName.ToString();
			}
			return text;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0009DB40 File Offset: 0x0009BD40
		protected int Compare(byte[] value1, byte[] value2)
		{
			int num = value1.Length;
			if (num != value2.Length)
			{
				return -1;
			}
			for (int i = 0; i < num; i++)
			{
				if (value1[i] != value2[i])
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x04000BBB RID: 3003
		private XmlSchemaDatatypeVariety variety;

		// Token: 0x04000BBC RID: 3004
		private RestrictionFacets restriction;

		// Token: 0x04000BBD RID: 3005
		private DatatypeImplementation baseType;

		// Token: 0x04000BBE RID: 3006
		private XmlValueConverter valueConverter;

		// Token: 0x04000BBF RID: 3007
		private XmlSchemaType parentSchemaType;

		// Token: 0x04000BC0 RID: 3008
		private static Hashtable builtinTypes = new Hashtable();

		// Token: 0x04000BC1 RID: 3009
		private static XmlSchemaSimpleType[] enumToTypeCode = new XmlSchemaSimpleType[55];

		// Token: 0x04000BC2 RID: 3010
		private static XmlSchemaSimpleType anySimpleType;

		// Token: 0x04000BC3 RID: 3011
		private static XmlSchemaSimpleType anyAtomicType;

		// Token: 0x04000BC4 RID: 3012
		private static XmlSchemaSimpleType untypedAtomicType;

		// Token: 0x04000BC5 RID: 3013
		private static XmlSchemaSimpleType yearMonthDurationType;

		// Token: 0x04000BC6 RID: 3014
		private static XmlSchemaSimpleType dayTimeDurationType;

		// Token: 0x04000BC7 RID: 3015
		private static volatile XmlSchemaSimpleType normalizedStringTypeV1Compat;

		// Token: 0x04000BC8 RID: 3016
		private static volatile XmlSchemaSimpleType tokenTypeV1Compat;

		// Token: 0x04000BC9 RID: 3017
		internal static XmlQualifiedName QnAnySimpleType = new XmlQualifiedName("anySimpleType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x04000BCA RID: 3018
		internal static XmlQualifiedName QnAnyType = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");

		// Token: 0x04000BCB RID: 3019
		internal static FacetsChecker stringFacetsChecker = new StringFacetsChecker();

		// Token: 0x04000BCC RID: 3020
		internal static FacetsChecker miscFacetsChecker = new MiscFacetsChecker();

		// Token: 0x04000BCD RID: 3021
		internal static FacetsChecker numeric2FacetsChecker = new Numeric2FacetsChecker();

		// Token: 0x04000BCE RID: 3022
		internal static FacetsChecker binaryFacetsChecker = new BinaryFacetsChecker();

		// Token: 0x04000BCF RID: 3023
		internal static FacetsChecker dateTimeFacetsChecker = new DateTimeFacetsChecker();

		// Token: 0x04000BD0 RID: 3024
		internal static FacetsChecker durationFacetsChecker = new DurationFacetsChecker();

		// Token: 0x04000BD1 RID: 3025
		internal static FacetsChecker listFacetsChecker = new ListFacetsChecker();

		// Token: 0x04000BD2 RID: 3026
		internal static FacetsChecker qnameFacetsChecker = new QNameFacetsChecker();

		// Token: 0x04000BD3 RID: 3027
		internal static FacetsChecker unionFacetsChecker = new UnionFacetsChecker();

		// Token: 0x04000BD4 RID: 3028
		private static readonly DatatypeImplementation c_anySimpleType = new Datatype_anySimpleType();

		// Token: 0x04000BD5 RID: 3029
		private static readonly DatatypeImplementation c_anyURI = new Datatype_anyURI();

		// Token: 0x04000BD6 RID: 3030
		private static readonly DatatypeImplementation c_base64Binary = new Datatype_base64Binary();

		// Token: 0x04000BD7 RID: 3031
		private static readonly DatatypeImplementation c_boolean = new Datatype_boolean();

		// Token: 0x04000BD8 RID: 3032
		private static readonly DatatypeImplementation c_byte = new Datatype_byte();

		// Token: 0x04000BD9 RID: 3033
		private static readonly DatatypeImplementation c_char = new Datatype_char();

		// Token: 0x04000BDA RID: 3034
		private static readonly DatatypeImplementation c_date = new Datatype_date();

		// Token: 0x04000BDB RID: 3035
		private static readonly DatatypeImplementation c_dateTime = new Datatype_dateTime();

		// Token: 0x04000BDC RID: 3036
		private static readonly DatatypeImplementation c_dateTimeNoTz = new Datatype_dateTimeNoTimeZone();

		// Token: 0x04000BDD RID: 3037
		private static readonly DatatypeImplementation c_dateTimeTz = new Datatype_dateTimeTimeZone();

		// Token: 0x04000BDE RID: 3038
		private static readonly DatatypeImplementation c_day = new Datatype_day();

		// Token: 0x04000BDF RID: 3039
		private static readonly DatatypeImplementation c_decimal = new Datatype_decimal();

		// Token: 0x04000BE0 RID: 3040
		private static readonly DatatypeImplementation c_double = new Datatype_double();

		// Token: 0x04000BE1 RID: 3041
		private static readonly DatatypeImplementation c_doubleXdr = new Datatype_doubleXdr();

		// Token: 0x04000BE2 RID: 3042
		private static readonly DatatypeImplementation c_duration = new Datatype_duration();

		// Token: 0x04000BE3 RID: 3043
		private static readonly DatatypeImplementation c_ENTITY = new Datatype_ENTITY();

		// Token: 0x04000BE4 RID: 3044
		private static readonly DatatypeImplementation c_ENTITIES = (DatatypeImplementation)DatatypeImplementation.c_ENTITY.DeriveByList(1, null);

		// Token: 0x04000BE5 RID: 3045
		private static readonly DatatypeImplementation c_ENUMERATION = new Datatype_ENUMERATION();

		// Token: 0x04000BE6 RID: 3046
		private static readonly DatatypeImplementation c_fixed = new Datatype_fixed();

		// Token: 0x04000BE7 RID: 3047
		private static readonly DatatypeImplementation c_float = new Datatype_float();

		// Token: 0x04000BE8 RID: 3048
		private static readonly DatatypeImplementation c_floatXdr = new Datatype_floatXdr();

		// Token: 0x04000BE9 RID: 3049
		private static readonly DatatypeImplementation c_hexBinary = new Datatype_hexBinary();

		// Token: 0x04000BEA RID: 3050
		private static readonly DatatypeImplementation c_ID = new Datatype_ID();

		// Token: 0x04000BEB RID: 3051
		private static readonly DatatypeImplementation c_IDREF = new Datatype_IDREF();

		// Token: 0x04000BEC RID: 3052
		private static readonly DatatypeImplementation c_IDREFS = (DatatypeImplementation)DatatypeImplementation.c_IDREF.DeriveByList(1, null);

		// Token: 0x04000BED RID: 3053
		private static readonly DatatypeImplementation c_int = new Datatype_int();

		// Token: 0x04000BEE RID: 3054
		private static readonly DatatypeImplementation c_integer = new Datatype_integer();

		// Token: 0x04000BEF RID: 3055
		private static readonly DatatypeImplementation c_language = new Datatype_language();

		// Token: 0x04000BF0 RID: 3056
		private static readonly DatatypeImplementation c_long = new Datatype_long();

		// Token: 0x04000BF1 RID: 3057
		private static readonly DatatypeImplementation c_month = new Datatype_month();

		// Token: 0x04000BF2 RID: 3058
		private static readonly DatatypeImplementation c_monthDay = new Datatype_monthDay();

		// Token: 0x04000BF3 RID: 3059
		private static readonly DatatypeImplementation c_Name = new Datatype_Name();

		// Token: 0x04000BF4 RID: 3060
		private static readonly DatatypeImplementation c_NCName = new Datatype_NCName();

		// Token: 0x04000BF5 RID: 3061
		private static readonly DatatypeImplementation c_negativeInteger = new Datatype_negativeInteger();

		// Token: 0x04000BF6 RID: 3062
		private static readonly DatatypeImplementation c_NMTOKEN = new Datatype_NMTOKEN();

		// Token: 0x04000BF7 RID: 3063
		private static readonly DatatypeImplementation c_NMTOKENS = (DatatypeImplementation)DatatypeImplementation.c_NMTOKEN.DeriveByList(1, null);

		// Token: 0x04000BF8 RID: 3064
		private static readonly DatatypeImplementation c_nonNegativeInteger = new Datatype_nonNegativeInteger();

		// Token: 0x04000BF9 RID: 3065
		private static readonly DatatypeImplementation c_nonPositiveInteger = new Datatype_nonPositiveInteger();

		// Token: 0x04000BFA RID: 3066
		private static readonly DatatypeImplementation c_normalizedString = new Datatype_normalizedString();

		// Token: 0x04000BFB RID: 3067
		private static readonly DatatypeImplementation c_NOTATION = new Datatype_NOTATION();

		// Token: 0x04000BFC RID: 3068
		private static readonly DatatypeImplementation c_positiveInteger = new Datatype_positiveInteger();

		// Token: 0x04000BFD RID: 3069
		private static readonly DatatypeImplementation c_QName = new Datatype_QName();

		// Token: 0x04000BFE RID: 3070
		private static readonly DatatypeImplementation c_QNameXdr = new Datatype_QNameXdr();

		// Token: 0x04000BFF RID: 3071
		private static readonly DatatypeImplementation c_short = new Datatype_short();

		// Token: 0x04000C00 RID: 3072
		private static readonly DatatypeImplementation c_string = new Datatype_string();

		// Token: 0x04000C01 RID: 3073
		private static readonly DatatypeImplementation c_time = new Datatype_time();

		// Token: 0x04000C02 RID: 3074
		private static readonly DatatypeImplementation c_timeNoTz = new Datatype_timeNoTimeZone();

		// Token: 0x04000C03 RID: 3075
		private static readonly DatatypeImplementation c_timeTz = new Datatype_timeTimeZone();

		// Token: 0x04000C04 RID: 3076
		private static readonly DatatypeImplementation c_token = new Datatype_token();

		// Token: 0x04000C05 RID: 3077
		private static readonly DatatypeImplementation c_unsignedByte = new Datatype_unsignedByte();

		// Token: 0x04000C06 RID: 3078
		private static readonly DatatypeImplementation c_unsignedInt = new Datatype_unsignedInt();

		// Token: 0x04000C07 RID: 3079
		private static readonly DatatypeImplementation c_unsignedLong = new Datatype_unsignedLong();

		// Token: 0x04000C08 RID: 3080
		private static readonly DatatypeImplementation c_unsignedShort = new Datatype_unsignedShort();

		// Token: 0x04000C09 RID: 3081
		private static readonly DatatypeImplementation c_uuid = new Datatype_uuid();

		// Token: 0x04000C0A RID: 3082
		private static readonly DatatypeImplementation c_year = new Datatype_year();

		// Token: 0x04000C0B RID: 3083
		private static readonly DatatypeImplementation c_yearMonth = new Datatype_yearMonth();

		// Token: 0x04000C0C RID: 3084
		internal static readonly DatatypeImplementation c_normalizedStringV1Compat = new Datatype_normalizedStringV1Compat();

		// Token: 0x04000C0D RID: 3085
		internal static readonly DatatypeImplementation c_tokenV1Compat = new Datatype_tokenV1Compat();

		// Token: 0x04000C0E RID: 3086
		private static readonly DatatypeImplementation c_anyAtomicType = new Datatype_anyAtomicType();

		// Token: 0x04000C0F RID: 3087
		private static readonly DatatypeImplementation c_dayTimeDuration = new Datatype_dayTimeDuration();

		// Token: 0x04000C10 RID: 3088
		private static readonly DatatypeImplementation c_untypedAtomicType = new Datatype_untypedAtomicType();

		// Token: 0x04000C11 RID: 3089
		private static readonly DatatypeImplementation c_yearMonthDuration = new Datatype_yearMonthDuration();

		// Token: 0x04000C12 RID: 3090
		private static readonly DatatypeImplementation[] c_tokenizedTypes;

		// Token: 0x04000C13 RID: 3091
		private static readonly DatatypeImplementation[] c_tokenizedTypesXsd;

		// Token: 0x04000C14 RID: 3092
		private static readonly DatatypeImplementation.SchemaDatatypeMap[] c_XdrTypes;

		// Token: 0x04000C15 RID: 3093
		private static readonly DatatypeImplementation.SchemaDatatypeMap[] c_XsdTypes;

		// Token: 0x02000236 RID: 566
		private class SchemaDatatypeMap : IComparable
		{
			// Token: 0x06001B41 RID: 6977 RVA: 0x0009DB78 File Offset: 0x0009BD78
			internal SchemaDatatypeMap(string name, DatatypeImplementation type)
			{
				this.name = name;
				this.type = type;
			}

			// Token: 0x06001B42 RID: 6978 RVA: 0x0009DB8E File Offset: 0x0009BD8E
			internal SchemaDatatypeMap(string name, DatatypeImplementation type, int parentIndex)
			{
				this.name = name;
				this.type = type;
				this.parentIndex = parentIndex;
			}

			// Token: 0x06001B43 RID: 6979 RVA: 0x0009DBAB File Offset: 0x0009BDAB
			public static explicit operator DatatypeImplementation(DatatypeImplementation.SchemaDatatypeMap sdm)
			{
				return sdm.type;
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0009DBB3 File Offset: 0x0009BDB3
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0009DBBB File Offset: 0x0009BDBB
			public int ParentIndex
			{
				get
				{
					return this.parentIndex;
				}
			}

			// Token: 0x06001B46 RID: 6982 RVA: 0x0009DBC3 File Offset: 0x0009BDC3
			public int CompareTo(object obj)
			{
				return string.Compare(this.name, (string)obj, StringComparison.Ordinal);
			}

			// Token: 0x04000C16 RID: 3094
			private string name;

			// Token: 0x04000C17 RID: 3095
			private DatatypeImplementation type;

			// Token: 0x04000C18 RID: 3096
			private int parentIndex;
		}
	}
}
