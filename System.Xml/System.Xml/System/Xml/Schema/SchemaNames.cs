using System;

namespace System.Xml.Schema
{
	// Token: 0x0200029B RID: 667
	internal sealed class SchemaNames
	{
		// Token: 0x06001EAB RID: 7851 RVA: 0x000B3450 File Offset: 0x000B1650
		public SchemaNames(XmlNameTable nameTable)
		{
			this.nameTable = nameTable;
			this.NsDataType = nameTable.Add("urn:schemas-microsoft-com:datatypes");
			this.NsDataTypeAlias = nameTable.Add("uuid:C2F41010-65B3-11D1-A29F-00AA00C14882");
			this.NsDataTypeOld = nameTable.Add("urn:uuid:C2F41010-65B3-11D1-A29F-00AA00C14882/");
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.NsXmlNs = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.NsXdr = nameTable.Add("urn:schemas-microsoft-com:xml-data");
			this.NsXdrAlias = nameTable.Add("uuid:BDC6E3F0-6DA3-11D1-A2A3-00AA00C14882");
			this.NsXs = nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.NsXsi = nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
			this.XsiType = nameTable.Add("type");
			this.XsiNil = nameTable.Add("nil");
			this.XsiSchemaLocation = nameTable.Add("schemaLocation");
			this.XsiNoNamespaceSchemaLocation = nameTable.Add("noNamespaceSchemaLocation");
			this.XsdSchema = nameTable.Add("schema");
			this.XdrSchema = nameTable.Add("Schema");
			this.QnPCData = new XmlQualifiedName(nameTable.Add("#PCDATA"));
			this.QnXml = new XmlQualifiedName(nameTable.Add("xml"));
			this.QnXmlNs = new XmlQualifiedName(nameTable.Add("xmlns"), this.NsXmlNs);
			this.QnDtDt = new XmlQualifiedName(nameTable.Add("dt"), this.NsDataType);
			this.QnXmlLang = new XmlQualifiedName(nameTable.Add("lang"), this.NsXml);
			this.QnName = new XmlQualifiedName(nameTable.Add("name"));
			this.QnType = new XmlQualifiedName(nameTable.Add("type"));
			this.QnMaxOccurs = new XmlQualifiedName(nameTable.Add("maxOccurs"));
			this.QnMinOccurs = new XmlQualifiedName(nameTable.Add("minOccurs"));
			this.QnInfinite = new XmlQualifiedName(nameTable.Add("*"));
			this.QnModel = new XmlQualifiedName(nameTable.Add("model"));
			this.QnOpen = new XmlQualifiedName(nameTable.Add("open"));
			this.QnClosed = new XmlQualifiedName(nameTable.Add("closed"));
			this.QnContent = new XmlQualifiedName(nameTable.Add("content"));
			this.QnMixed = new XmlQualifiedName(nameTable.Add("mixed"));
			this.QnEmpty = new XmlQualifiedName(nameTable.Add("empty"));
			this.QnEltOnly = new XmlQualifiedName(nameTable.Add("eltOnly"));
			this.QnTextOnly = new XmlQualifiedName(nameTable.Add("textOnly"));
			this.QnOrder = new XmlQualifiedName(nameTable.Add("order"));
			this.QnSeq = new XmlQualifiedName(nameTable.Add("seq"));
			this.QnOne = new XmlQualifiedName(nameTable.Add("one"));
			this.QnMany = new XmlQualifiedName(nameTable.Add("many"));
			this.QnRequired = new XmlQualifiedName(nameTable.Add("required"));
			this.QnYes = new XmlQualifiedName(nameTable.Add("yes"));
			this.QnNo = new XmlQualifiedName(nameTable.Add("no"));
			this.QnString = new XmlQualifiedName(nameTable.Add("string"));
			this.QnID = new XmlQualifiedName(nameTable.Add("id"));
			this.QnIDRef = new XmlQualifiedName(nameTable.Add("idref"));
			this.QnIDRefs = new XmlQualifiedName(nameTable.Add("idrefs"));
			this.QnEntity = new XmlQualifiedName(nameTable.Add("entity"));
			this.QnEntities = new XmlQualifiedName(nameTable.Add("entities"));
			this.QnNmToken = new XmlQualifiedName(nameTable.Add("nmtoken"));
			this.QnNmTokens = new XmlQualifiedName(nameTable.Add("nmtokens"));
			this.QnEnumeration = new XmlQualifiedName(nameTable.Add("enumeration"));
			this.QnDefault = new XmlQualifiedName(nameTable.Add("default"));
			this.QnTargetNamespace = new XmlQualifiedName(nameTable.Add("targetNamespace"));
			this.QnVersion = new XmlQualifiedName(nameTable.Add("version"));
			this.QnFinalDefault = new XmlQualifiedName(nameTable.Add("finalDefault"));
			this.QnBlockDefault = new XmlQualifiedName(nameTable.Add("blockDefault"));
			this.QnFixed = new XmlQualifiedName(nameTable.Add("fixed"));
			this.QnAbstract = new XmlQualifiedName(nameTable.Add("abstract"));
			this.QnBlock = new XmlQualifiedName(nameTable.Add("block"));
			this.QnSubstitutionGroup = new XmlQualifiedName(nameTable.Add("substitutionGroup"));
			this.QnFinal = new XmlQualifiedName(nameTable.Add("final"));
			this.QnNillable = new XmlQualifiedName(nameTable.Add("nillable"));
			this.QnRef = new XmlQualifiedName(nameTable.Add("ref"));
			this.QnBase = new XmlQualifiedName(nameTable.Add("base"));
			this.QnDerivedBy = new XmlQualifiedName(nameTable.Add("derivedBy"));
			this.QnNamespace = new XmlQualifiedName(nameTable.Add("namespace"));
			this.QnProcessContents = new XmlQualifiedName(nameTable.Add("processContents"));
			this.QnRefer = new XmlQualifiedName(nameTable.Add("refer"));
			this.QnPublic = new XmlQualifiedName(nameTable.Add("public"));
			this.QnSystem = new XmlQualifiedName(nameTable.Add("system"));
			this.QnSchemaLocation = new XmlQualifiedName(nameTable.Add("schemaLocation"));
			this.QnValue = new XmlQualifiedName(nameTable.Add("value"));
			this.QnUse = new XmlQualifiedName(nameTable.Add("use"));
			this.QnForm = new XmlQualifiedName(nameTable.Add("form"));
			this.QnAttributeFormDefault = new XmlQualifiedName(nameTable.Add("attributeFormDefault"));
			this.QnElementFormDefault = new XmlQualifiedName(nameTable.Add("elementFormDefault"));
			this.QnSource = new XmlQualifiedName(nameTable.Add("source"));
			this.QnMemberTypes = new XmlQualifiedName(nameTable.Add("memberTypes"));
			this.QnItemType = new XmlQualifiedName(nameTable.Add("itemType"));
			this.QnXPath = new XmlQualifiedName(nameTable.Add("xpath"));
			this.QnXdrSchema = new XmlQualifiedName(this.XdrSchema, this.NsXdr);
			this.QnXdrElementType = new XmlQualifiedName(nameTable.Add("ElementType"), this.NsXdr);
			this.QnXdrElement = new XmlQualifiedName(nameTable.Add("element"), this.NsXdr);
			this.QnXdrGroup = new XmlQualifiedName(nameTable.Add("group"), this.NsXdr);
			this.QnXdrAttributeType = new XmlQualifiedName(nameTable.Add("AttributeType"), this.NsXdr);
			this.QnXdrAttribute = new XmlQualifiedName(nameTable.Add("attribute"), this.NsXdr);
			this.QnXdrDataType = new XmlQualifiedName(nameTable.Add("datatype"), this.NsXdr);
			this.QnXdrDescription = new XmlQualifiedName(nameTable.Add("description"), this.NsXdr);
			this.QnXdrExtends = new XmlQualifiedName(nameTable.Add("extends"), this.NsXdr);
			this.QnXdrAliasSchema = new XmlQualifiedName(nameTable.Add("Schema"), this.NsDataTypeAlias);
			this.QnDtType = new XmlQualifiedName(nameTable.Add("type"), this.NsDataType);
			this.QnDtValues = new XmlQualifiedName(nameTable.Add("values"), this.NsDataType);
			this.QnDtMaxLength = new XmlQualifiedName(nameTable.Add("maxLength"), this.NsDataType);
			this.QnDtMinLength = new XmlQualifiedName(nameTable.Add("minLength"), this.NsDataType);
			this.QnDtMax = new XmlQualifiedName(nameTable.Add("max"), this.NsDataType);
			this.QnDtMin = new XmlQualifiedName(nameTable.Add("min"), this.NsDataType);
			this.QnDtMinExclusive = new XmlQualifiedName(nameTable.Add("minExclusive"), this.NsDataType);
			this.QnDtMaxExclusive = new XmlQualifiedName(nameTable.Add("maxExclusive"), this.NsDataType);
			this.QnXsdSchema = new XmlQualifiedName(this.XsdSchema, this.NsXs);
			this.QnXsdAnnotation = new XmlQualifiedName(nameTable.Add("annotation"), this.NsXs);
			this.QnXsdInclude = new XmlQualifiedName(nameTable.Add("include"), this.NsXs);
			this.QnXsdImport = new XmlQualifiedName(nameTable.Add("import"), this.NsXs);
			this.QnXsdElement = new XmlQualifiedName(nameTable.Add("element"), this.NsXs);
			this.QnXsdAttribute = new XmlQualifiedName(nameTable.Add("attribute"), this.NsXs);
			this.QnXsdAttributeGroup = new XmlQualifiedName(nameTable.Add("attributeGroup"), this.NsXs);
			this.QnXsdAnyAttribute = new XmlQualifiedName(nameTable.Add("anyAttribute"), this.NsXs);
			this.QnXsdGroup = new XmlQualifiedName(nameTable.Add("group"), this.NsXs);
			this.QnXsdAll = new XmlQualifiedName(nameTable.Add("all"), this.NsXs);
			this.QnXsdChoice = new XmlQualifiedName(nameTable.Add("choice"), this.NsXs);
			this.QnXsdSequence = new XmlQualifiedName(nameTable.Add("sequence"), this.NsXs);
			this.QnXsdAny = new XmlQualifiedName(nameTable.Add("any"), this.NsXs);
			this.QnXsdNotation = new XmlQualifiedName(nameTable.Add("notation"), this.NsXs);
			this.QnXsdSimpleType = new XmlQualifiedName(nameTable.Add("simpleType"), this.NsXs);
			this.QnXsdComplexType = new XmlQualifiedName(nameTable.Add("complexType"), this.NsXs);
			this.QnXsdUnique = new XmlQualifiedName(nameTable.Add("unique"), this.NsXs);
			this.QnXsdKey = new XmlQualifiedName(nameTable.Add("key"), this.NsXs);
			this.QnXsdKeyRef = new XmlQualifiedName(nameTable.Add("keyref"), this.NsXs);
			this.QnXsdSelector = new XmlQualifiedName(nameTable.Add("selector"), this.NsXs);
			this.QnXsdField = new XmlQualifiedName(nameTable.Add("field"), this.NsXs);
			this.QnXsdMinExclusive = new XmlQualifiedName(nameTable.Add("minExclusive"), this.NsXs);
			this.QnXsdMinInclusive = new XmlQualifiedName(nameTable.Add("minInclusive"), this.NsXs);
			this.QnXsdMaxInclusive = new XmlQualifiedName(nameTable.Add("maxInclusive"), this.NsXs);
			this.QnXsdMaxExclusive = new XmlQualifiedName(nameTable.Add("maxExclusive"), this.NsXs);
			this.QnXsdTotalDigits = new XmlQualifiedName(nameTable.Add("totalDigits"), this.NsXs);
			this.QnXsdFractionDigits = new XmlQualifiedName(nameTable.Add("fractionDigits"), this.NsXs);
			this.QnXsdLength = new XmlQualifiedName(nameTable.Add("length"), this.NsXs);
			this.QnXsdMinLength = new XmlQualifiedName(nameTable.Add("minLength"), this.NsXs);
			this.QnXsdMaxLength = new XmlQualifiedName(nameTable.Add("maxLength"), this.NsXs);
			this.QnXsdEnumeration = new XmlQualifiedName(nameTable.Add("enumeration"), this.NsXs);
			this.QnXsdPattern = new XmlQualifiedName(nameTable.Add("pattern"), this.NsXs);
			this.QnXsdDocumentation = new XmlQualifiedName(nameTable.Add("documentation"), this.NsXs);
			this.QnXsdAppinfo = new XmlQualifiedName(nameTable.Add("appinfo"), this.NsXs);
			this.QnXsdComplexContent = new XmlQualifiedName(nameTable.Add("complexContent"), this.NsXs);
			this.QnXsdSimpleContent = new XmlQualifiedName(nameTable.Add("simpleContent"), this.NsXs);
			this.QnXsdRestriction = new XmlQualifiedName(nameTable.Add("restriction"), this.NsXs);
			this.QnXsdExtension = new XmlQualifiedName(nameTable.Add("extension"), this.NsXs);
			this.QnXsdUnion = new XmlQualifiedName(nameTable.Add("union"), this.NsXs);
			this.QnXsdList = new XmlQualifiedName(nameTable.Add("list"), this.NsXs);
			this.QnXsdWhiteSpace = new XmlQualifiedName(nameTable.Add("whiteSpace"), this.NsXs);
			this.QnXsdRedefine = new XmlQualifiedName(nameTable.Add("redefine"), this.NsXs);
			this.QnXsdAnyType = new XmlQualifiedName(nameTable.Add("anyType"), this.NsXs);
			this.CreateTokenToQNameTable();
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000B419C File Offset: 0x000B239C
		public void CreateTokenToQNameTable()
		{
			this.TokenToQName[1] = this.QnName;
			this.TokenToQName[2] = this.QnType;
			this.TokenToQName[3] = this.QnMaxOccurs;
			this.TokenToQName[4] = this.QnMinOccurs;
			this.TokenToQName[5] = this.QnInfinite;
			this.TokenToQName[6] = this.QnModel;
			this.TokenToQName[7] = this.QnOpen;
			this.TokenToQName[8] = this.QnClosed;
			this.TokenToQName[9] = this.QnContent;
			this.TokenToQName[10] = this.QnMixed;
			this.TokenToQName[11] = this.QnEmpty;
			this.TokenToQName[12] = this.QnEltOnly;
			this.TokenToQName[13] = this.QnTextOnly;
			this.TokenToQName[14] = this.QnOrder;
			this.TokenToQName[15] = this.QnSeq;
			this.TokenToQName[16] = this.QnOne;
			this.TokenToQName[17] = this.QnMany;
			this.TokenToQName[18] = this.QnRequired;
			this.TokenToQName[19] = this.QnYes;
			this.TokenToQName[20] = this.QnNo;
			this.TokenToQName[21] = this.QnString;
			this.TokenToQName[22] = this.QnID;
			this.TokenToQName[23] = this.QnIDRef;
			this.TokenToQName[24] = this.QnIDRefs;
			this.TokenToQName[25] = this.QnEntity;
			this.TokenToQName[26] = this.QnEntities;
			this.TokenToQName[27] = this.QnNmToken;
			this.TokenToQName[28] = this.QnNmTokens;
			this.TokenToQName[29] = this.QnEnumeration;
			this.TokenToQName[30] = this.QnDefault;
			this.TokenToQName[31] = this.QnXdrSchema;
			this.TokenToQName[32] = this.QnXdrElementType;
			this.TokenToQName[33] = this.QnXdrElement;
			this.TokenToQName[34] = this.QnXdrGroup;
			this.TokenToQName[35] = this.QnXdrAttributeType;
			this.TokenToQName[36] = this.QnXdrAttribute;
			this.TokenToQName[37] = this.QnXdrDataType;
			this.TokenToQName[38] = this.QnXdrDescription;
			this.TokenToQName[39] = this.QnXdrExtends;
			this.TokenToQName[40] = this.QnXdrAliasSchema;
			this.TokenToQName[41] = this.QnDtType;
			this.TokenToQName[42] = this.QnDtValues;
			this.TokenToQName[43] = this.QnDtMaxLength;
			this.TokenToQName[44] = this.QnDtMinLength;
			this.TokenToQName[45] = this.QnDtMax;
			this.TokenToQName[46] = this.QnDtMin;
			this.TokenToQName[47] = this.QnDtMinExclusive;
			this.TokenToQName[48] = this.QnDtMaxExclusive;
			this.TokenToQName[49] = this.QnTargetNamespace;
			this.TokenToQName[50] = this.QnVersion;
			this.TokenToQName[51] = this.QnFinalDefault;
			this.TokenToQName[52] = this.QnBlockDefault;
			this.TokenToQName[53] = this.QnFixed;
			this.TokenToQName[54] = this.QnAbstract;
			this.TokenToQName[55] = this.QnBlock;
			this.TokenToQName[56] = this.QnSubstitutionGroup;
			this.TokenToQName[57] = this.QnFinal;
			this.TokenToQName[58] = this.QnNillable;
			this.TokenToQName[59] = this.QnRef;
			this.TokenToQName[60] = this.QnBase;
			this.TokenToQName[61] = this.QnDerivedBy;
			this.TokenToQName[62] = this.QnNamespace;
			this.TokenToQName[63] = this.QnProcessContents;
			this.TokenToQName[64] = this.QnRefer;
			this.TokenToQName[65] = this.QnPublic;
			this.TokenToQName[66] = this.QnSystem;
			this.TokenToQName[67] = this.QnSchemaLocation;
			this.TokenToQName[68] = this.QnValue;
			this.TokenToQName[119] = this.QnItemType;
			this.TokenToQName[120] = this.QnMemberTypes;
			this.TokenToQName[121] = this.QnXPath;
			this.TokenToQName[74] = this.QnXsdSchema;
			this.TokenToQName[75] = this.QnXsdAnnotation;
			this.TokenToQName[76] = this.QnXsdInclude;
			this.TokenToQName[77] = this.QnXsdImport;
			this.TokenToQName[78] = this.QnXsdElement;
			this.TokenToQName[79] = this.QnXsdAttribute;
			this.TokenToQName[80] = this.QnXsdAttributeGroup;
			this.TokenToQName[81] = this.QnXsdAnyAttribute;
			this.TokenToQName[82] = this.QnXsdGroup;
			this.TokenToQName[83] = this.QnXsdAll;
			this.TokenToQName[84] = this.QnXsdChoice;
			this.TokenToQName[85] = this.QnXsdSequence;
			this.TokenToQName[86] = this.QnXsdAny;
			this.TokenToQName[87] = this.QnXsdNotation;
			this.TokenToQName[88] = this.QnXsdSimpleType;
			this.TokenToQName[89] = this.QnXsdComplexType;
			this.TokenToQName[90] = this.QnXsdUnique;
			this.TokenToQName[91] = this.QnXsdKey;
			this.TokenToQName[92] = this.QnXsdKeyRef;
			this.TokenToQName[93] = this.QnXsdSelector;
			this.TokenToQName[94] = this.QnXsdField;
			this.TokenToQName[95] = this.QnXsdMinExclusive;
			this.TokenToQName[96] = this.QnXsdMinInclusive;
			this.TokenToQName[97] = this.QnXsdMaxExclusive;
			this.TokenToQName[98] = this.QnXsdMaxInclusive;
			this.TokenToQName[99] = this.QnXsdTotalDigits;
			this.TokenToQName[100] = this.QnXsdFractionDigits;
			this.TokenToQName[101] = this.QnXsdLength;
			this.TokenToQName[102] = this.QnXsdMinLength;
			this.TokenToQName[103] = this.QnXsdMaxLength;
			this.TokenToQName[104] = this.QnXsdEnumeration;
			this.TokenToQName[105] = this.QnXsdPattern;
			this.TokenToQName[117] = this.QnXsdWhiteSpace;
			this.TokenToQName[106] = this.QnXsdDocumentation;
			this.TokenToQName[107] = this.QnXsdAppinfo;
			this.TokenToQName[108] = this.QnXsdComplexContent;
			this.TokenToQName[110] = this.QnXsdRestriction;
			this.TokenToQName[113] = this.QnXsdRestriction;
			this.TokenToQName[115] = this.QnXsdRestriction;
			this.TokenToQName[109] = this.QnXsdExtension;
			this.TokenToQName[112] = this.QnXsdExtension;
			this.TokenToQName[111] = this.QnXsdSimpleContent;
			this.TokenToQName[116] = this.QnXsdUnion;
			this.TokenToQName[114] = this.QnXsdList;
			this.TokenToQName[118] = this.QnXsdRedefine;
			this.TokenToQName[69] = this.QnSource;
			this.TokenToQName[72] = this.QnUse;
			this.TokenToQName[73] = this.QnForm;
			this.TokenToQName[71] = this.QnElementFormDefault;
			this.TokenToQName[70] = this.QnAttributeFormDefault;
			this.TokenToQName[122] = this.QnXmlLang;
			this.TokenToQName[0] = XmlQualifiedName.Empty;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000B48D4 File Offset: 0x000B2AD4
		public SchemaType SchemaTypeFromRoot(string localName, string ns)
		{
			if (this.IsXSDRoot(localName, ns))
			{
				return SchemaType.XSD;
			}
			if (this.IsXDRRoot(localName, XmlSchemaDatatype.XdrCanonizeUri(ns, this.nameTable, this)))
			{
				return SchemaType.XDR;
			}
			return SchemaType.None;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000B48FB File Offset: 0x000B2AFB
		public bool IsXSDRoot(string localName, string ns)
		{
			return localName == this.XsdSchema && ns == this.NsXs;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000B4919 File Offset: 0x000B2B19
		public bool IsXDRRoot(string localName, string ns)
		{
			return localName == this.XdrSchema && ns == this.NsXdr;
		}

		// Token: 0x04000D2F RID: 3375
		private XmlNameTable nameTable;

		// Token: 0x04000D30 RID: 3376
		public string NsDataType;

		// Token: 0x04000D31 RID: 3377
		public string NsDataTypeAlias;

		// Token: 0x04000D32 RID: 3378
		public string NsDataTypeOld;

		// Token: 0x04000D33 RID: 3379
		public string NsXml;

		// Token: 0x04000D34 RID: 3380
		public string NsXmlNs;

		// Token: 0x04000D35 RID: 3381
		public string NsXdr;

		// Token: 0x04000D36 RID: 3382
		public string NsXdrAlias;

		// Token: 0x04000D37 RID: 3383
		public string NsXs;

		// Token: 0x04000D38 RID: 3384
		public string NsXsi;

		// Token: 0x04000D39 RID: 3385
		public string XsiType;

		// Token: 0x04000D3A RID: 3386
		public string XsiNil;

		// Token: 0x04000D3B RID: 3387
		public string XsiSchemaLocation;

		// Token: 0x04000D3C RID: 3388
		public string XsiNoNamespaceSchemaLocation;

		// Token: 0x04000D3D RID: 3389
		public string XsdSchema;

		// Token: 0x04000D3E RID: 3390
		public string XdrSchema;

		// Token: 0x04000D3F RID: 3391
		public XmlQualifiedName QnPCData;

		// Token: 0x04000D40 RID: 3392
		public XmlQualifiedName QnXml;

		// Token: 0x04000D41 RID: 3393
		public XmlQualifiedName QnXmlNs;

		// Token: 0x04000D42 RID: 3394
		public XmlQualifiedName QnDtDt;

		// Token: 0x04000D43 RID: 3395
		public XmlQualifiedName QnXmlLang;

		// Token: 0x04000D44 RID: 3396
		public XmlQualifiedName QnName;

		// Token: 0x04000D45 RID: 3397
		public XmlQualifiedName QnType;

		// Token: 0x04000D46 RID: 3398
		public XmlQualifiedName QnMaxOccurs;

		// Token: 0x04000D47 RID: 3399
		public XmlQualifiedName QnMinOccurs;

		// Token: 0x04000D48 RID: 3400
		public XmlQualifiedName QnInfinite;

		// Token: 0x04000D49 RID: 3401
		public XmlQualifiedName QnModel;

		// Token: 0x04000D4A RID: 3402
		public XmlQualifiedName QnOpen;

		// Token: 0x04000D4B RID: 3403
		public XmlQualifiedName QnClosed;

		// Token: 0x04000D4C RID: 3404
		public XmlQualifiedName QnContent;

		// Token: 0x04000D4D RID: 3405
		public XmlQualifiedName QnMixed;

		// Token: 0x04000D4E RID: 3406
		public XmlQualifiedName QnEmpty;

		// Token: 0x04000D4F RID: 3407
		public XmlQualifiedName QnEltOnly;

		// Token: 0x04000D50 RID: 3408
		public XmlQualifiedName QnTextOnly;

		// Token: 0x04000D51 RID: 3409
		public XmlQualifiedName QnOrder;

		// Token: 0x04000D52 RID: 3410
		public XmlQualifiedName QnSeq;

		// Token: 0x04000D53 RID: 3411
		public XmlQualifiedName QnOne;

		// Token: 0x04000D54 RID: 3412
		public XmlQualifiedName QnMany;

		// Token: 0x04000D55 RID: 3413
		public XmlQualifiedName QnRequired;

		// Token: 0x04000D56 RID: 3414
		public XmlQualifiedName QnYes;

		// Token: 0x04000D57 RID: 3415
		public XmlQualifiedName QnNo;

		// Token: 0x04000D58 RID: 3416
		public XmlQualifiedName QnString;

		// Token: 0x04000D59 RID: 3417
		public XmlQualifiedName QnID;

		// Token: 0x04000D5A RID: 3418
		public XmlQualifiedName QnIDRef;

		// Token: 0x04000D5B RID: 3419
		public XmlQualifiedName QnIDRefs;

		// Token: 0x04000D5C RID: 3420
		public XmlQualifiedName QnEntity;

		// Token: 0x04000D5D RID: 3421
		public XmlQualifiedName QnEntities;

		// Token: 0x04000D5E RID: 3422
		public XmlQualifiedName QnNmToken;

		// Token: 0x04000D5F RID: 3423
		public XmlQualifiedName QnNmTokens;

		// Token: 0x04000D60 RID: 3424
		public XmlQualifiedName QnEnumeration;

		// Token: 0x04000D61 RID: 3425
		public XmlQualifiedName QnDefault;

		// Token: 0x04000D62 RID: 3426
		public XmlQualifiedName QnXdrSchema;

		// Token: 0x04000D63 RID: 3427
		public XmlQualifiedName QnXdrElementType;

		// Token: 0x04000D64 RID: 3428
		public XmlQualifiedName QnXdrElement;

		// Token: 0x04000D65 RID: 3429
		public XmlQualifiedName QnXdrGroup;

		// Token: 0x04000D66 RID: 3430
		public XmlQualifiedName QnXdrAttributeType;

		// Token: 0x04000D67 RID: 3431
		public XmlQualifiedName QnXdrAttribute;

		// Token: 0x04000D68 RID: 3432
		public XmlQualifiedName QnXdrDataType;

		// Token: 0x04000D69 RID: 3433
		public XmlQualifiedName QnXdrDescription;

		// Token: 0x04000D6A RID: 3434
		public XmlQualifiedName QnXdrExtends;

		// Token: 0x04000D6B RID: 3435
		public XmlQualifiedName QnXdrAliasSchema;

		// Token: 0x04000D6C RID: 3436
		public XmlQualifiedName QnDtType;

		// Token: 0x04000D6D RID: 3437
		public XmlQualifiedName QnDtValues;

		// Token: 0x04000D6E RID: 3438
		public XmlQualifiedName QnDtMaxLength;

		// Token: 0x04000D6F RID: 3439
		public XmlQualifiedName QnDtMinLength;

		// Token: 0x04000D70 RID: 3440
		public XmlQualifiedName QnDtMax;

		// Token: 0x04000D71 RID: 3441
		public XmlQualifiedName QnDtMin;

		// Token: 0x04000D72 RID: 3442
		public XmlQualifiedName QnDtMinExclusive;

		// Token: 0x04000D73 RID: 3443
		public XmlQualifiedName QnDtMaxExclusive;

		// Token: 0x04000D74 RID: 3444
		public XmlQualifiedName QnTargetNamespace;

		// Token: 0x04000D75 RID: 3445
		public XmlQualifiedName QnVersion;

		// Token: 0x04000D76 RID: 3446
		public XmlQualifiedName QnFinalDefault;

		// Token: 0x04000D77 RID: 3447
		public XmlQualifiedName QnBlockDefault;

		// Token: 0x04000D78 RID: 3448
		public XmlQualifiedName QnFixed;

		// Token: 0x04000D79 RID: 3449
		public XmlQualifiedName QnAbstract;

		// Token: 0x04000D7A RID: 3450
		public XmlQualifiedName QnBlock;

		// Token: 0x04000D7B RID: 3451
		public XmlQualifiedName QnSubstitutionGroup;

		// Token: 0x04000D7C RID: 3452
		public XmlQualifiedName QnFinal;

		// Token: 0x04000D7D RID: 3453
		public XmlQualifiedName QnNillable;

		// Token: 0x04000D7E RID: 3454
		public XmlQualifiedName QnRef;

		// Token: 0x04000D7F RID: 3455
		public XmlQualifiedName QnBase;

		// Token: 0x04000D80 RID: 3456
		public XmlQualifiedName QnDerivedBy;

		// Token: 0x04000D81 RID: 3457
		public XmlQualifiedName QnNamespace;

		// Token: 0x04000D82 RID: 3458
		public XmlQualifiedName QnProcessContents;

		// Token: 0x04000D83 RID: 3459
		public XmlQualifiedName QnRefer;

		// Token: 0x04000D84 RID: 3460
		public XmlQualifiedName QnPublic;

		// Token: 0x04000D85 RID: 3461
		public XmlQualifiedName QnSystem;

		// Token: 0x04000D86 RID: 3462
		public XmlQualifiedName QnSchemaLocation;

		// Token: 0x04000D87 RID: 3463
		public XmlQualifiedName QnValue;

		// Token: 0x04000D88 RID: 3464
		public XmlQualifiedName QnUse;

		// Token: 0x04000D89 RID: 3465
		public XmlQualifiedName QnForm;

		// Token: 0x04000D8A RID: 3466
		public XmlQualifiedName QnElementFormDefault;

		// Token: 0x04000D8B RID: 3467
		public XmlQualifiedName QnAttributeFormDefault;

		// Token: 0x04000D8C RID: 3468
		public XmlQualifiedName QnItemType;

		// Token: 0x04000D8D RID: 3469
		public XmlQualifiedName QnMemberTypes;

		// Token: 0x04000D8E RID: 3470
		public XmlQualifiedName QnXPath;

		// Token: 0x04000D8F RID: 3471
		public XmlQualifiedName QnXsdSchema;

		// Token: 0x04000D90 RID: 3472
		public XmlQualifiedName QnXsdAnnotation;

		// Token: 0x04000D91 RID: 3473
		public XmlQualifiedName QnXsdInclude;

		// Token: 0x04000D92 RID: 3474
		public XmlQualifiedName QnXsdImport;

		// Token: 0x04000D93 RID: 3475
		public XmlQualifiedName QnXsdElement;

		// Token: 0x04000D94 RID: 3476
		public XmlQualifiedName QnXsdAttribute;

		// Token: 0x04000D95 RID: 3477
		public XmlQualifiedName QnXsdAttributeGroup;

		// Token: 0x04000D96 RID: 3478
		public XmlQualifiedName QnXsdAnyAttribute;

		// Token: 0x04000D97 RID: 3479
		public XmlQualifiedName QnXsdGroup;

		// Token: 0x04000D98 RID: 3480
		public XmlQualifiedName QnXsdAll;

		// Token: 0x04000D99 RID: 3481
		public XmlQualifiedName QnXsdChoice;

		// Token: 0x04000D9A RID: 3482
		public XmlQualifiedName QnXsdSequence;

		// Token: 0x04000D9B RID: 3483
		public XmlQualifiedName QnXsdAny;

		// Token: 0x04000D9C RID: 3484
		public XmlQualifiedName QnXsdNotation;

		// Token: 0x04000D9D RID: 3485
		public XmlQualifiedName QnXsdSimpleType;

		// Token: 0x04000D9E RID: 3486
		public XmlQualifiedName QnXsdComplexType;

		// Token: 0x04000D9F RID: 3487
		public XmlQualifiedName QnXsdUnique;

		// Token: 0x04000DA0 RID: 3488
		public XmlQualifiedName QnXsdKey;

		// Token: 0x04000DA1 RID: 3489
		public XmlQualifiedName QnXsdKeyRef;

		// Token: 0x04000DA2 RID: 3490
		public XmlQualifiedName QnXsdSelector;

		// Token: 0x04000DA3 RID: 3491
		public XmlQualifiedName QnXsdField;

		// Token: 0x04000DA4 RID: 3492
		public XmlQualifiedName QnXsdMinExclusive;

		// Token: 0x04000DA5 RID: 3493
		public XmlQualifiedName QnXsdMinInclusive;

		// Token: 0x04000DA6 RID: 3494
		public XmlQualifiedName QnXsdMaxInclusive;

		// Token: 0x04000DA7 RID: 3495
		public XmlQualifiedName QnXsdMaxExclusive;

		// Token: 0x04000DA8 RID: 3496
		public XmlQualifiedName QnXsdTotalDigits;

		// Token: 0x04000DA9 RID: 3497
		public XmlQualifiedName QnXsdFractionDigits;

		// Token: 0x04000DAA RID: 3498
		public XmlQualifiedName QnXsdLength;

		// Token: 0x04000DAB RID: 3499
		public XmlQualifiedName QnXsdMinLength;

		// Token: 0x04000DAC RID: 3500
		public XmlQualifiedName QnXsdMaxLength;

		// Token: 0x04000DAD RID: 3501
		public XmlQualifiedName QnXsdEnumeration;

		// Token: 0x04000DAE RID: 3502
		public XmlQualifiedName QnXsdPattern;

		// Token: 0x04000DAF RID: 3503
		public XmlQualifiedName QnXsdDocumentation;

		// Token: 0x04000DB0 RID: 3504
		public XmlQualifiedName QnXsdAppinfo;

		// Token: 0x04000DB1 RID: 3505
		public XmlQualifiedName QnSource;

		// Token: 0x04000DB2 RID: 3506
		public XmlQualifiedName QnXsdComplexContent;

		// Token: 0x04000DB3 RID: 3507
		public XmlQualifiedName QnXsdSimpleContent;

		// Token: 0x04000DB4 RID: 3508
		public XmlQualifiedName QnXsdRestriction;

		// Token: 0x04000DB5 RID: 3509
		public XmlQualifiedName QnXsdExtension;

		// Token: 0x04000DB6 RID: 3510
		public XmlQualifiedName QnXsdUnion;

		// Token: 0x04000DB7 RID: 3511
		public XmlQualifiedName QnXsdList;

		// Token: 0x04000DB8 RID: 3512
		public XmlQualifiedName QnXsdWhiteSpace;

		// Token: 0x04000DB9 RID: 3513
		public XmlQualifiedName QnXsdRedefine;

		// Token: 0x04000DBA RID: 3514
		public XmlQualifiedName QnXsdAnyType;

		// Token: 0x04000DBB RID: 3515
		internal XmlQualifiedName[] TokenToQName = new XmlQualifiedName[123];

		// Token: 0x0200029C RID: 668
		public enum Token
		{
			// Token: 0x04000DBD RID: 3517
			Empty,
			// Token: 0x04000DBE RID: 3518
			SchemaName,
			// Token: 0x04000DBF RID: 3519
			SchemaType,
			// Token: 0x04000DC0 RID: 3520
			SchemaMaxOccurs,
			// Token: 0x04000DC1 RID: 3521
			SchemaMinOccurs,
			// Token: 0x04000DC2 RID: 3522
			SchemaInfinite,
			// Token: 0x04000DC3 RID: 3523
			SchemaModel,
			// Token: 0x04000DC4 RID: 3524
			SchemaOpen,
			// Token: 0x04000DC5 RID: 3525
			SchemaClosed,
			// Token: 0x04000DC6 RID: 3526
			SchemaContent,
			// Token: 0x04000DC7 RID: 3527
			SchemaMixed,
			// Token: 0x04000DC8 RID: 3528
			SchemaEmpty,
			// Token: 0x04000DC9 RID: 3529
			SchemaElementOnly,
			// Token: 0x04000DCA RID: 3530
			SchemaTextOnly,
			// Token: 0x04000DCB RID: 3531
			SchemaOrder,
			// Token: 0x04000DCC RID: 3532
			SchemaSeq,
			// Token: 0x04000DCD RID: 3533
			SchemaOne,
			// Token: 0x04000DCE RID: 3534
			SchemaMany,
			// Token: 0x04000DCF RID: 3535
			SchemaRequired,
			// Token: 0x04000DD0 RID: 3536
			SchemaYes,
			// Token: 0x04000DD1 RID: 3537
			SchemaNo,
			// Token: 0x04000DD2 RID: 3538
			SchemaString,
			// Token: 0x04000DD3 RID: 3539
			SchemaId,
			// Token: 0x04000DD4 RID: 3540
			SchemaIdref,
			// Token: 0x04000DD5 RID: 3541
			SchemaIdrefs,
			// Token: 0x04000DD6 RID: 3542
			SchemaEntity,
			// Token: 0x04000DD7 RID: 3543
			SchemaEntities,
			// Token: 0x04000DD8 RID: 3544
			SchemaNmtoken,
			// Token: 0x04000DD9 RID: 3545
			SchemaNmtokens,
			// Token: 0x04000DDA RID: 3546
			SchemaEnumeration,
			// Token: 0x04000DDB RID: 3547
			SchemaDefault,
			// Token: 0x04000DDC RID: 3548
			XdrRoot,
			// Token: 0x04000DDD RID: 3549
			XdrElementType,
			// Token: 0x04000DDE RID: 3550
			XdrElement,
			// Token: 0x04000DDF RID: 3551
			XdrGroup,
			// Token: 0x04000DE0 RID: 3552
			XdrAttributeType,
			// Token: 0x04000DE1 RID: 3553
			XdrAttribute,
			// Token: 0x04000DE2 RID: 3554
			XdrDatatype,
			// Token: 0x04000DE3 RID: 3555
			XdrDescription,
			// Token: 0x04000DE4 RID: 3556
			XdrExtends,
			// Token: 0x04000DE5 RID: 3557
			SchemaXdrRootAlias,
			// Token: 0x04000DE6 RID: 3558
			SchemaDtType,
			// Token: 0x04000DE7 RID: 3559
			SchemaDtValues,
			// Token: 0x04000DE8 RID: 3560
			SchemaDtMaxLength,
			// Token: 0x04000DE9 RID: 3561
			SchemaDtMinLength,
			// Token: 0x04000DEA RID: 3562
			SchemaDtMax,
			// Token: 0x04000DEB RID: 3563
			SchemaDtMin,
			// Token: 0x04000DEC RID: 3564
			SchemaDtMinExclusive,
			// Token: 0x04000DED RID: 3565
			SchemaDtMaxExclusive,
			// Token: 0x04000DEE RID: 3566
			SchemaTargetNamespace,
			// Token: 0x04000DEF RID: 3567
			SchemaVersion,
			// Token: 0x04000DF0 RID: 3568
			SchemaFinalDefault,
			// Token: 0x04000DF1 RID: 3569
			SchemaBlockDefault,
			// Token: 0x04000DF2 RID: 3570
			SchemaFixed,
			// Token: 0x04000DF3 RID: 3571
			SchemaAbstract,
			// Token: 0x04000DF4 RID: 3572
			SchemaBlock,
			// Token: 0x04000DF5 RID: 3573
			SchemaSubstitutionGroup,
			// Token: 0x04000DF6 RID: 3574
			SchemaFinal,
			// Token: 0x04000DF7 RID: 3575
			SchemaNillable,
			// Token: 0x04000DF8 RID: 3576
			SchemaRef,
			// Token: 0x04000DF9 RID: 3577
			SchemaBase,
			// Token: 0x04000DFA RID: 3578
			SchemaDerivedBy,
			// Token: 0x04000DFB RID: 3579
			SchemaNamespace,
			// Token: 0x04000DFC RID: 3580
			SchemaProcessContents,
			// Token: 0x04000DFD RID: 3581
			SchemaRefer,
			// Token: 0x04000DFE RID: 3582
			SchemaPublic,
			// Token: 0x04000DFF RID: 3583
			SchemaSystem,
			// Token: 0x04000E00 RID: 3584
			SchemaSchemaLocation,
			// Token: 0x04000E01 RID: 3585
			SchemaValue,
			// Token: 0x04000E02 RID: 3586
			SchemaSource,
			// Token: 0x04000E03 RID: 3587
			SchemaAttributeFormDefault,
			// Token: 0x04000E04 RID: 3588
			SchemaElementFormDefault,
			// Token: 0x04000E05 RID: 3589
			SchemaUse,
			// Token: 0x04000E06 RID: 3590
			SchemaForm,
			// Token: 0x04000E07 RID: 3591
			XsdSchema,
			// Token: 0x04000E08 RID: 3592
			XsdAnnotation,
			// Token: 0x04000E09 RID: 3593
			XsdInclude,
			// Token: 0x04000E0A RID: 3594
			XsdImport,
			// Token: 0x04000E0B RID: 3595
			XsdElement,
			// Token: 0x04000E0C RID: 3596
			XsdAttribute,
			// Token: 0x04000E0D RID: 3597
			xsdAttributeGroup,
			// Token: 0x04000E0E RID: 3598
			XsdAnyAttribute,
			// Token: 0x04000E0F RID: 3599
			XsdGroup,
			// Token: 0x04000E10 RID: 3600
			XsdAll,
			// Token: 0x04000E11 RID: 3601
			XsdChoice,
			// Token: 0x04000E12 RID: 3602
			XsdSequence,
			// Token: 0x04000E13 RID: 3603
			XsdAny,
			// Token: 0x04000E14 RID: 3604
			XsdNotation,
			// Token: 0x04000E15 RID: 3605
			XsdSimpleType,
			// Token: 0x04000E16 RID: 3606
			XsdComplexType,
			// Token: 0x04000E17 RID: 3607
			XsdUnique,
			// Token: 0x04000E18 RID: 3608
			XsdKey,
			// Token: 0x04000E19 RID: 3609
			XsdKeyref,
			// Token: 0x04000E1A RID: 3610
			XsdSelector,
			// Token: 0x04000E1B RID: 3611
			XsdField,
			// Token: 0x04000E1C RID: 3612
			XsdMinExclusive,
			// Token: 0x04000E1D RID: 3613
			XsdMinInclusive,
			// Token: 0x04000E1E RID: 3614
			XsdMaxExclusive,
			// Token: 0x04000E1F RID: 3615
			XsdMaxInclusive,
			// Token: 0x04000E20 RID: 3616
			XsdTotalDigits,
			// Token: 0x04000E21 RID: 3617
			XsdFractionDigits,
			// Token: 0x04000E22 RID: 3618
			XsdLength,
			// Token: 0x04000E23 RID: 3619
			XsdMinLength,
			// Token: 0x04000E24 RID: 3620
			XsdMaxLength,
			// Token: 0x04000E25 RID: 3621
			XsdEnumeration,
			// Token: 0x04000E26 RID: 3622
			XsdPattern,
			// Token: 0x04000E27 RID: 3623
			XsdDocumentation,
			// Token: 0x04000E28 RID: 3624
			XsdAppInfo,
			// Token: 0x04000E29 RID: 3625
			XsdComplexContent,
			// Token: 0x04000E2A RID: 3626
			XsdComplexContentExtension,
			// Token: 0x04000E2B RID: 3627
			XsdComplexContentRestriction,
			// Token: 0x04000E2C RID: 3628
			XsdSimpleContent,
			// Token: 0x04000E2D RID: 3629
			XsdSimpleContentExtension,
			// Token: 0x04000E2E RID: 3630
			XsdSimpleContentRestriction,
			// Token: 0x04000E2F RID: 3631
			XsdSimpleTypeList,
			// Token: 0x04000E30 RID: 3632
			XsdSimpleTypeRestriction,
			// Token: 0x04000E31 RID: 3633
			XsdSimpleTypeUnion,
			// Token: 0x04000E32 RID: 3634
			XsdWhitespace,
			// Token: 0x04000E33 RID: 3635
			XsdRedefine,
			// Token: 0x04000E34 RID: 3636
			SchemaItemType,
			// Token: 0x04000E35 RID: 3637
			SchemaMemberTypes,
			// Token: 0x04000E36 RID: 3638
			SchemaXPath,
			// Token: 0x04000E37 RID: 3639
			XmlLang
		}
	}
}
