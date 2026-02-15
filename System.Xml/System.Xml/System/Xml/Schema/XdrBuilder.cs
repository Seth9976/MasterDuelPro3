using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x020002A5 RID: 677
	internal sealed class XdrBuilder : SchemaBuilder
	{
		// Token: 0x06001F0C RID: 7948 RVA: 0x000B9BB4 File Offset: 0x000B7DB4
		internal XdrBuilder(XmlReader reader, XmlNamespaceManager curmgr, SchemaInfo sinfo, string targetNamspace, XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventhandler)
		{
			this._SchemaInfo = sinfo;
			this._TargetNamespace = targetNamspace;
			this._reader = reader;
			this._CurNsMgr = curmgr;
			this.validationEventHandler = eventhandler;
			this._StateHistory = new HWStack(10);
			this._ElementDef = new XdrBuilder.ElementContent();
			this._AttributeDef = new XdrBuilder.AttributeContent();
			this._GroupStack = new HWStack(10);
			this._GroupDef = new XdrBuilder.GroupContent();
			this._NameTable = nameTable;
			this._SchemaNames = schemaNames;
			this._CurState = XdrBuilder.S_SchemaEntries[0];
			this.positionInfo = PositionInfo.GetPositionInfo(this._reader);
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000B9C6C File Offset: 0x000B7E6C
		internal override bool ProcessElement(string prefix, string name, string ns)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, this._NameTable, this._SchemaNames));
			if (this.GetNextState(xmlQualifiedName))
			{
				this.Push();
				if (this._CurState._InitFunc != null)
				{
					this._CurState._InitFunc(this, xmlQualifiedName);
				}
				return true;
			}
			if (!this.IsSkipableElement(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' element is not supported in this context.", XmlQualifiedName.ToString(name, prefix));
			}
			return false;
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000B9CE0 File Offset: 0x000B7EE0
		internal override void ProcessAttribute(string prefix, string name, string ns, string value)
		{
			XmlQualifiedName xmlQualifiedName = new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, this._NameTable, this._SchemaNames));
			int i = 0;
			while (i < this._CurState._Attributes.Length)
			{
				XdrBuilder.XdrAttributeEntry xdrAttributeEntry = this._CurState._Attributes[i];
				if (this._SchemaNames.TokenToQName[(int)xdrAttributeEntry._Attribute].Equals(xmlQualifiedName))
				{
					XdrBuilder.XdrBuildFunction buildFunc = xdrAttributeEntry._BuildFunc;
					if (xdrAttributeEntry._Datatype.TokenizedType == XmlTokenizedType.QName)
					{
						string text;
						XmlQualifiedName xmlQualifiedName2 = XmlQualifiedName.Parse(value, this._CurNsMgr, out text);
						xmlQualifiedName2.Atomize(this._NameTable);
						if (text.Length != 0)
						{
							if (xdrAttributeEntry._Attribute != SchemaNames.Token.SchemaType)
							{
								throw new XmlException("This is an unexpected token. The expected token is '{0}'.", "NAME");
							}
						}
						else if (this.IsGlobal(xdrAttributeEntry._SchemaFlags))
						{
							xmlQualifiedName2 = new XmlQualifiedName(xmlQualifiedName2.Name, this._TargetNamespace);
						}
						else
						{
							xmlQualifiedName2 = new XmlQualifiedName(xmlQualifiedName2.Name);
						}
						buildFunc(this, xmlQualifiedName2, text);
						return;
					}
					buildFunc(this, xdrAttributeEntry._Datatype.ParseValue(value, this._NameTable, this._CurNsMgr), string.Empty);
					return;
				}
				else
				{
					i++;
				}
			}
			if (ns == this._SchemaNames.NsXmlNs && XdrBuilder.IsXdrSchema(value))
			{
				this.LoadSchema(value);
				return;
			}
			if (!this.IsSkipableAttribute(xmlQualifiedName))
			{
				this.SendValidationEvent("The '{0}' attribute is not supported in this context.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
			}
		}

		// Token: 0x17000750 RID: 1872
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x000B9E4A File Offset: 0x000B804A
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000B9E54 File Offset: 0x000B8054
		private bool LoadSchema(string uri)
		{
			if (this.xmlResolver == null)
			{
				return false;
			}
			uri = this._NameTable.Add(uri);
			if (this._SchemaInfo.TargetNamespaces.ContainsKey(uri))
			{
				return false;
			}
			SchemaInfo schemaInfo = null;
			Uri uri2 = this.xmlResolver.ResolveUri(null, this._reader.BaseURI);
			XmlReader xmlReader = null;
			try
			{
				Uri uri3 = this.xmlResolver.ResolveUri(uri2, uri.Substring("x-schema:".Length));
				Stream stream = (Stream)this.xmlResolver.GetEntity(uri3, null, null);
				xmlReader = new XmlTextReader(uri3.ToString(), stream, this._NameTable);
				schemaInfo = new SchemaInfo();
				Parser parser = new Parser(SchemaType.XDR, this._NameTable, this._SchemaNames, this.validationEventHandler);
				parser.XmlResolver = this.xmlResolver;
				parser.Parse(xmlReader, uri);
				schemaInfo = parser.XdrSchema;
			}
			catch (XmlException ex)
			{
				this.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[] { uri, ex.Message }, XmlSeverityType.Warning);
				schemaInfo = null;
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
			if (schemaInfo != null && schemaInfo.ErrorCount == 0)
			{
				this._SchemaInfo.Add(schemaInfo, this.validationEventHandler);
				return true;
			}
			return false;
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000B9F98 File Offset: 0x000B8198
		internal static bool IsXdrSchema(string uri)
		{
			return uri.Length >= "x-schema:".Length && string.Compare(uri, 0, "x-schema:", 0, "x-schema:".Length, StringComparison.Ordinal) == 0 && !uri.StartsWith("x-schema:#", StringComparison.Ordinal);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		internal override bool IsContentParsed()
		{
			return true;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0000AB38 File Offset: 0x00008D38
		internal override void ProcessMarkup(XmlNode[] markup)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000B9FD7 File Offset: 0x000B81D7
		internal override void ProcessCData(string value)
		{
			if (this._CurState._AllowText)
			{
				this._Text = value;
				return;
			}
			this.SendValidationEvent("The following text is not allowed in this context: '{0}'.", value);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000B9FFA File Offset: 0x000B81FA
		internal override void StartChildren()
		{
			if (this._CurState._BeginChildFunc != null)
			{
				this._CurState._BeginChildFunc(this);
			}
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000BA01A File Offset: 0x000B821A
		internal override void EndChildren()
		{
			if (this._CurState._EndChildFunc != null)
			{
				this._CurState._EndChildFunc(this);
			}
			this.Pop();
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000BA040 File Offset: 0x000B8240
		private void Push()
		{
			this._StateHistory.Push();
			this._StateHistory[this._StateHistory.Length - 1] = this._CurState;
			this._CurState = this._NextState;
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000BA078 File Offset: 0x000B8278
		private void Pop()
		{
			this._CurState = (XdrBuilder.XdrEntry)this._StateHistory.Pop();
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000BA090 File Offset: 0x000B8290
		private void PushGroupInfo()
		{
			this._GroupStack.Push();
			this._GroupStack[this._GroupStack.Length - 1] = XdrBuilder.GroupContent.Copy(this._GroupDef);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000BA0C1 File Offset: 0x000B82C1
		private void PopGroupInfo()
		{
			this._GroupDef = (XdrBuilder.GroupContent)this._GroupStack.Pop();
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000BA0D9 File Offset: 0x000B82D9
		private static void XDR_InitRoot(XdrBuilder builder, object obj)
		{
			builder._SchemaInfo.SchemaType = SchemaType.XDR;
			builder._ElementDef._ElementDecl = null;
			builder._ElementDef._AttDefList = null;
			builder._AttributeDef._AttDef = null;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000BA10B File Offset: 0x000B830B
		private static void XDR_BuildRoot_Name(XdrBuilder builder, object obj, string prefix)
		{
			builder._XdrName = (string)obj;
			builder._XdrPrefix = prefix;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0000A558 File Offset: 0x00008758
		private static void XDR_BuildRoot_ID(XdrBuilder builder, object obj, string prefix)
		{
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000BA120 File Offset: 0x000B8320
		private static void XDR_BeginRoot(XdrBuilder builder)
		{
			if (builder._TargetNamespace == null)
			{
				if (builder._XdrName != null)
				{
					builder._TargetNamespace = builder._NameTable.Add("x-schema:#" + builder._XdrName);
				}
				else
				{
					builder._TargetNamespace = string.Empty;
				}
			}
			builder._SchemaInfo.TargetNamespaces.Add(builder._TargetNamespace, true);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000BA184 File Offset: 0x000B8384
		private static void XDR_EndRoot(XdrBuilder builder)
		{
			while (builder._UndefinedAttributeTypes != null)
			{
				XmlQualifiedName xmlQualifiedName = builder._UndefinedAttributeTypes._TypeName;
				if (xmlQualifiedName.Namespace.Length == 0)
				{
					xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, builder._TargetNamespace);
				}
				SchemaAttDef schemaAttDef;
				if (builder._SchemaInfo.AttributeDecls.TryGetValue(xmlQualifiedName, out schemaAttDef))
				{
					builder._UndefinedAttributeTypes._Attdef = schemaAttDef.Clone();
					builder._UndefinedAttributeTypes._Attdef.Name = xmlQualifiedName;
					builder.XDR_CheckAttributeDefault(builder._UndefinedAttributeTypes, builder._UndefinedAttributeTypes._Attdef);
				}
				else
				{
					builder.SendValidationEvent("The '{0}' attribute is not declared.", xmlQualifiedName.Name);
				}
				builder._UndefinedAttributeTypes = builder._UndefinedAttributeTypes._Next;
			}
			foreach (object obj in builder._UndeclaredElements.Values)
			{
				SchemaElementDecl schemaElementDecl = (SchemaElementDecl)obj;
				builder.SendValidationEvent("The '{0}' element is not declared.", XmlQualifiedName.ToString(schemaElementDecl.Name.Name, schemaElementDecl.Prefix));
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000BA2AC File Offset: 0x000B84AC
		private static void XDR_InitElementType(XdrBuilder builder, object obj)
		{
			builder._ElementDef._ElementDecl = new SchemaElementDecl();
			builder._contentValidator = new ParticleContentValidator(XmlSchemaContentType.Mixed);
			builder._contentValidator.IsOpen = true;
			builder._ElementDef._ContentAttr = 0;
			builder._ElementDef._OrderAttr = 0;
			builder._ElementDef._MasterGroupRequired = false;
			builder._ElementDef._ExistTerminal = false;
			builder._ElementDef._AllowDataType = true;
			builder._ElementDef._HasDataType = false;
			builder._ElementDef._EnumerationRequired = false;
			builder._ElementDef._AttDefList = new Hashtable();
			builder._ElementDef._MaxLength = uint.MaxValue;
			builder._ElementDef._MinLength = uint.MaxValue;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000BA360 File Offset: 0x000B8560
		private static void XDR_BuildElementType_Name(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			if (builder._SchemaInfo.ElementDecls.ContainsKey(xmlQualifiedName))
			{
				builder.SendValidationEvent("The '{0}' element has already been declared.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
			}
			builder._ElementDef._ElementDecl.Name = xmlQualifiedName;
			builder._ElementDef._ElementDecl.Prefix = prefix;
			builder._SchemaInfo.ElementDecls.Add(xmlQualifiedName, builder._ElementDef._ElementDecl);
			if (builder._UndeclaredElements[xmlQualifiedName] != null)
			{
				builder._UndeclaredElements.Remove(xmlQualifiedName);
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000BA3F6 File Offset: 0x000B85F6
		private static void XDR_BuildElementType_Content(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._ContentAttr = builder.GetContent((XmlQualifiedName)obj);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000BA40F File Offset: 0x000B860F
		private static void XDR_BuildElementType_Model(XdrBuilder builder, object obj, string prefix)
		{
			builder._contentValidator.IsOpen = builder.GetModel((XmlQualifiedName)obj);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000BA428 File Offset: 0x000B8628
		private static void XDR_BuildElementType_Order(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._OrderAttr = (builder._GroupDef._Order = builder.GetOrder((XmlQualifiedName)obj));
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x000BA45C File Offset: 0x000B865C
		private static void XDR_BuildElementType_DtType(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._HasDataType = true;
			string text = ((string)obj).Trim();
			if (text.Length == 0)
			{
				builder.SendValidationEvent("The DataType value cannot be empty.");
				return;
			}
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXdrName(text);
			if (xmlSchemaDatatype == null)
			{
				builder.SendValidationEvent("Reference to an unknown data type, '{0}'.", text);
			}
			builder._ElementDef._ElementDecl.Datatype = xmlSchemaDatatype;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000BA4BC File Offset: 0x000B86BC
		private static void XDR_BuildElementType_DtValues(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._EnumerationRequired = true;
			builder._ElementDef._ElementDecl.Values = new List<string>((string[])obj);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x000BA4E5 File Offset: 0x000B86E5
		private static void XDR_BuildElementType_DtMaxLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMaxLength(ref builder._ElementDef._MaxLength, obj, builder);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000BA4F9 File Offset: 0x000B86F9
		private static void XDR_BuildElementType_DtMinLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMinLength(ref builder._ElementDef._MinLength, obj, builder);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000BA510 File Offset: 0x000B8710
		private static void XDR_BeginElementType(XdrBuilder builder)
		{
			string text = null;
			string text2 = null;
			if (builder._ElementDef._ElementDecl.Name.IsEmpty)
			{
				text = "The '{0}' attribute is either invalid or missing.";
				text2 = "name";
			}
			else
			{
				if (builder._ElementDef._HasDataType)
				{
					if (!builder._ElementDef._AllowDataType)
					{
						text = "Content must be \"textOnly\" when using DataType on an ElementType.";
						goto IL_01F4;
					}
					builder._ElementDef._ContentAttr = 2;
				}
				else if (builder._ElementDef._ContentAttr == 0)
				{
					switch (builder._ElementDef._OrderAttr)
					{
					case 0:
						builder._ElementDef._ContentAttr = 3;
						builder._ElementDef._OrderAttr = 1;
						break;
					case 1:
						builder._ElementDef._ContentAttr = 3;
						break;
					case 2:
						builder._ElementDef._ContentAttr = 4;
						break;
					case 3:
						builder._ElementDef._ContentAttr = 4;
						break;
					}
				}
				bool isOpen = builder._contentValidator.IsOpen;
				XdrBuilder.ElementContent elementDef = builder._ElementDef;
				switch (builder._ElementDef._ContentAttr)
				{
				case 1:
					builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.Empty;
					builder._contentValidator = null;
					break;
				case 2:
					builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.TextOnly;
					builder._GroupDef._Order = 1;
					builder._contentValidator = null;
					break;
				case 3:
					if (elementDef._OrderAttr != 0 && elementDef._OrderAttr != 1)
					{
						text = "The order must be many when content is mixed.";
						goto IL_01F4;
					}
					builder._GroupDef._Order = 1;
					elementDef._MasterGroupRequired = true;
					builder._contentValidator.IsOpen = isOpen;
					break;
				case 4:
					builder._contentValidator = new ParticleContentValidator(XmlSchemaContentType.ElementOnly);
					if (elementDef._OrderAttr == 0)
					{
						builder._GroupDef._Order = 2;
					}
					elementDef._MasterGroupRequired = true;
					builder._contentValidator.IsOpen = isOpen;
					break;
				}
				if (elementDef._ContentAttr == 3 || elementDef._ContentAttr == 4)
				{
					builder._contentValidator.Start();
					builder._contentValidator.OpenGroup();
				}
			}
			IL_01F4:
			if (text != null)
			{
				builder.SendValidationEvent(text, text2);
			}
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000BA71C File Offset: 0x000B891C
		private static void XDR_EndElementType(XdrBuilder builder)
		{
			SchemaElementDecl elementDecl = builder._ElementDef._ElementDecl;
			if (builder._UndefinedAttributeTypes != null && builder._ElementDef._AttDefList != null)
			{
				XdrBuilder.DeclBaseInfo declBaseInfo = builder._UndefinedAttributeTypes;
				XdrBuilder.DeclBaseInfo declBaseInfo2 = declBaseInfo;
				while (declBaseInfo != null)
				{
					SchemaAttDef schemaAttDef = null;
					if (declBaseInfo._ElementDecl == elementDecl)
					{
						XmlQualifiedName typeName = declBaseInfo._TypeName;
						schemaAttDef = (SchemaAttDef)builder._ElementDef._AttDefList[typeName];
						if (schemaAttDef != null)
						{
							declBaseInfo._Attdef = schemaAttDef.Clone();
							declBaseInfo._Attdef.Name = typeName;
							builder.XDR_CheckAttributeDefault(declBaseInfo, schemaAttDef);
							if (declBaseInfo == builder._UndefinedAttributeTypes)
							{
								declBaseInfo = (builder._UndefinedAttributeTypes = declBaseInfo._Next);
								declBaseInfo2 = declBaseInfo;
							}
							else
							{
								declBaseInfo2._Next = declBaseInfo._Next;
								declBaseInfo = declBaseInfo2._Next;
							}
						}
					}
					if (schemaAttDef == null)
					{
						if (declBaseInfo != builder._UndefinedAttributeTypes)
						{
							declBaseInfo2 = declBaseInfo2._Next;
						}
						declBaseInfo = declBaseInfo._Next;
					}
				}
			}
			if (builder._ElementDef._MasterGroupRequired)
			{
				builder._contentValidator.CloseGroup();
				if (!builder._ElementDef._ExistTerminal)
				{
					if (builder._contentValidator.IsOpen)
					{
						builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.Any;
						builder._contentValidator = null;
					}
					else if (builder._ElementDef._ContentAttr != 3)
					{
						builder.SendValidationEvent("There is a missing element.");
					}
				}
				else if (builder._GroupDef._Order == 1)
				{
					builder._contentValidator.AddStar();
				}
			}
			if (elementDecl.Datatype != null)
			{
				XmlTokenizedType tokenizedType = elementDecl.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._ElementDef._EnumerationRequired)
				{
					builder.SendValidationEvent("The dt:values attribute is missing.");
				}
				if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._ElementDef._EnumerationRequired)
				{
					builder.SendValidationEvent("Data type should be enumeration when the values attribute is present.");
				}
			}
			XdrBuilder.CompareMinMaxLength(builder._ElementDef._MinLength, builder._ElementDef._MaxLength, builder);
			elementDecl.MaxLength = (long)((ulong)builder._ElementDef._MaxLength);
			elementDecl.MinLength = (long)((ulong)builder._ElementDef._MinLength);
			if (builder._contentValidator != null)
			{
				builder._ElementDef._ElementDecl.ContentValidator = builder._contentValidator.Finish(true);
				builder._contentValidator = null;
			}
			builder._ElementDef._ElementDecl = null;
			builder._ElementDef._AttDefList = null;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000BA954 File Offset: 0x000B8B54
		private static void XDR_InitAttributeType(XdrBuilder builder, object obj)
		{
			XdrBuilder.AttributeContent attributeDef = builder._AttributeDef;
			attributeDef._AttDef = new SchemaAttDef(XmlQualifiedName.Empty, null);
			attributeDef._Required = false;
			attributeDef._Prefix = null;
			attributeDef._Default = null;
			attributeDef._MinVal = 0U;
			attributeDef._MaxVal = 1U;
			attributeDef._EnumerationRequired = false;
			attributeDef._HasDataType = false;
			attributeDef._Global = builder._StateHistory.Length == 2;
			attributeDef._MaxLength = uint.MaxValue;
			attributeDef._MinLength = uint.MaxValue;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000BA9CC File Offset: 0x000B8BCC
		private static void XDR_BuildAttributeType_Name(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			builder._AttributeDef._Name = xmlQualifiedName;
			builder._AttributeDef._Prefix = prefix;
			builder._AttributeDef._AttDef.Name = xmlQualifiedName;
			if (builder._ElementDef._ElementDecl != null)
			{
				if (builder._ElementDef._AttDefList[xmlQualifiedName] == null)
				{
					builder._ElementDef._AttDefList.Add(xmlQualifiedName, builder._AttributeDef._AttDef);
					return;
				}
				builder.SendValidationEvent("The '{0}' attribute has already been declared for this ElementType.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
				return;
			}
			else
			{
				xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, builder._TargetNamespace);
				builder._AttributeDef._AttDef.Name = xmlQualifiedName;
				if (!builder._SchemaInfo.AttributeDecls.ContainsKey(xmlQualifiedName))
				{
					builder._SchemaInfo.AttributeDecls.Add(xmlQualifiedName, builder._AttributeDef._AttDef);
					return;
				}
				builder.SendValidationEvent("The '{0}' attribute has already been declared for this ElementType.", XmlQualifiedName.ToString(xmlQualifiedName.Name, prefix));
				return;
			}
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000BAAC8 File Offset: 0x000B8CC8
		private static void XDR_BuildAttributeType_Required(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._Required = XdrBuilder.IsYes(obj, builder);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000BAADC File Offset: 0x000B8CDC
		private static void XDR_BuildAttributeType_Default(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._Default = obj;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x000BAAEC File Offset: 0x000B8CEC
		private static void XDR_BuildAttributeType_DtType(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			builder._AttributeDef._HasDataType = true;
			builder._AttributeDef._AttDef.Datatype = builder.CheckDatatype(xmlQualifiedName.Name);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x000BAB28 File Offset: 0x000B8D28
		private static void XDR_BuildAttributeType_DtValues(XdrBuilder builder, object obj, string prefix)
		{
			builder._AttributeDef._EnumerationRequired = true;
			builder._AttributeDef._AttDef.Values = new List<string>((string[])obj);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000BAB51 File Offset: 0x000B8D51
		private static void XDR_BuildAttributeType_DtMaxLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMaxLength(ref builder._AttributeDef._MaxLength, obj, builder);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000BAB65 File Offset: 0x000B8D65
		private static void XDR_BuildAttributeType_DtMinLength(XdrBuilder builder, object obj, string prefix)
		{
			XdrBuilder.ParseDtMinLength(ref builder._AttributeDef._MinLength, obj, builder);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000BAB79 File Offset: 0x000B8D79
		private static void XDR_BeginAttributeType(XdrBuilder builder)
		{
			if (builder._AttributeDef._Name.IsEmpty)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000BAB98 File Offset: 0x000B8D98
		private static void XDR_EndAttributeType(XdrBuilder builder)
		{
			string text = null;
			if (builder._AttributeDef._HasDataType && builder._AttributeDef._AttDef.Datatype != null)
			{
				XmlTokenizedType tokenizedType = builder._AttributeDef._AttDef.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._AttributeDef._EnumerationRequired)
				{
					text = "The dt:values attribute is missing.";
					goto IL_0164;
				}
				if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._AttributeDef._EnumerationRequired)
				{
					text = "Data type should be enumeration when the values attribute is present.";
					goto IL_0164;
				}
				if (builder._AttributeDef._Default != null && tokenizedType == XmlTokenizedType.ID)
				{
					text = "An attribute or element of type xs:ID or derived from xs:ID, should not have a value constraint.";
					goto IL_0164;
				}
			}
			else
			{
				builder._AttributeDef._AttDef.Datatype = XmlSchemaDatatype.FromXmlTokenizedType(XmlTokenizedType.CDATA);
			}
			XdrBuilder.CompareMinMaxLength(builder._AttributeDef._MinLength, builder._AttributeDef._MaxLength, builder);
			builder._AttributeDef._AttDef.MaxLength = (long)((ulong)builder._AttributeDef._MaxLength);
			builder._AttributeDef._AttDef.MinLength = (long)((ulong)builder._AttributeDef._MinLength);
			if (builder._AttributeDef._Default != null)
			{
				builder._AttributeDef._AttDef.DefaultValueRaw = (builder._AttributeDef._AttDef.DefaultValueExpanded = (string)builder._AttributeDef._Default);
				builder.CheckDefaultAttValue(builder._AttributeDef._AttDef);
			}
			builder.SetAttributePresence(builder._AttributeDef._AttDef, builder._AttributeDef._Required);
			IL_0164:
			if (text != null)
			{
				builder.SendValidationEvent(text);
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000BAD14 File Offset: 0x000B8F14
		private static void XDR_InitElement(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._HasDataType || builder._ElementDef._ContentAttr == 1 || builder._ElementDef._ContentAttr == 2)
			{
				builder.SendValidationEvent("Element is not allowed when the content is empty or textOnly.");
			}
			builder._ElementDef._AllowDataType = false;
			builder._ElementDef._HasType = false;
			builder._ElementDef._MinVal = 1U;
			builder._ElementDef._MaxVal = 1U;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000BAD88 File Offset: 0x000B8F88
		private static void XDR_BuildElement_Type(XdrBuilder builder, object obj, string prefix)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			if (!builder._SchemaInfo.ElementDecls.ContainsKey(xmlQualifiedName) && (SchemaElementDecl)builder._UndeclaredElements[xmlQualifiedName] == null)
			{
				SchemaElementDecl schemaElementDecl = new SchemaElementDecl(xmlQualifiedName, prefix);
				builder._UndeclaredElements.Add(xmlQualifiedName, schemaElementDecl);
			}
			builder._ElementDef._HasType = true;
			if (builder._ElementDef._ExistTerminal)
			{
				builder.AddOrder();
			}
			else
			{
				builder._ElementDef._ExistTerminal = true;
			}
			builder._contentValidator.AddName(xmlQualifiedName, null);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000BAE13 File Offset: 0x000B9013
		private static void XDR_BuildElement_MinOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._MinVal = XdrBuilder.ParseMinOccurs(obj, builder);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000BAE27 File Offset: 0x000B9027
		private static void XDR_BuildElement_MaxOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._ElementDef._MaxVal = XdrBuilder.ParseMaxOccurs(obj, builder);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x000BAE3B File Offset: 0x000B903B
		private static void XDR_EndElement(XdrBuilder builder)
		{
			if (builder._ElementDef._HasType)
			{
				XdrBuilder.HandleMinMax(builder._contentValidator, builder._ElementDef._MinVal, builder._ElementDef._MaxVal);
				return;
			}
			builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000BAE77 File Offset: 0x000B9077
		private static void XDR_InitAttribute(XdrBuilder builder, object obj)
		{
			if (builder._BaseDecl == null)
			{
				builder._BaseDecl = new XdrBuilder.DeclBaseInfo();
			}
			builder._BaseDecl._MinOccurs = 0U;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000BAE98 File Offset: 0x000B9098
		private static void XDR_BuildAttribute_Type(XdrBuilder builder, object obj, string prefix)
		{
			builder._BaseDecl._TypeName = (XmlQualifiedName)obj;
			builder._BaseDecl._Prefix = prefix;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000BAEB7 File Offset: 0x000B90B7
		private static void XDR_BuildAttribute_Required(XdrBuilder builder, object obj, string prefix)
		{
			if (XdrBuilder.IsYes(obj, builder))
			{
				builder._BaseDecl._MinOccurs = 1U;
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000BAECE File Offset: 0x000B90CE
		private static void XDR_BuildAttribute_Default(XdrBuilder builder, object obj, string prefix)
		{
			builder._BaseDecl._Default = obj;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000BAEDC File Offset: 0x000B90DC
		private static void XDR_BeginAttribute(XdrBuilder builder)
		{
			if (builder._BaseDecl._TypeName.IsEmpty)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
			SchemaAttDef schemaAttDef = null;
			XmlQualifiedName typeName = builder._BaseDecl._TypeName;
			string prefix = builder._BaseDecl._Prefix;
			if (builder._ElementDef._AttDefList != null)
			{
				schemaAttDef = (SchemaAttDef)builder._ElementDef._AttDefList[typeName];
			}
			if (schemaAttDef == null)
			{
				XmlQualifiedName xmlQualifiedName = typeName;
				if (prefix.Length == 0)
				{
					xmlQualifiedName = new XmlQualifiedName(typeName.Name, builder._TargetNamespace);
				}
				SchemaAttDef schemaAttDef2;
				if (builder._SchemaInfo.AttributeDecls.TryGetValue(xmlQualifiedName, out schemaAttDef2))
				{
					schemaAttDef = schemaAttDef2.Clone();
					schemaAttDef.Name = typeName;
				}
				else if (prefix.Length != 0)
				{
					builder.SendValidationEvent("The '{0}' attribute is not declared.", XmlQualifiedName.ToString(typeName.Name, prefix));
				}
			}
			if (schemaAttDef != null)
			{
				builder.XDR_CheckAttributeDefault(builder._BaseDecl, schemaAttDef);
			}
			else
			{
				schemaAttDef = new SchemaAttDef(typeName, prefix);
				builder._UndefinedAttributeTypes = new XdrBuilder.DeclBaseInfo
				{
					_Checking = true,
					_Attdef = schemaAttDef,
					_TypeName = builder._BaseDecl._TypeName,
					_ElementDecl = builder._ElementDef._ElementDecl,
					_MinOccurs = builder._BaseDecl._MinOccurs,
					_Default = builder._BaseDecl._Default,
					_Next = builder._UndefinedAttributeTypes
				};
			}
			builder._ElementDef._ElementDecl.AddAttDef(schemaAttDef);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000BB047 File Offset: 0x000B9247
		private static void XDR_EndAttribute(XdrBuilder builder)
		{
			builder._BaseDecl.Reset();
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000BB054 File Offset: 0x000B9254
		private static void XDR_InitGroup(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._ContentAttr == 1 || builder._ElementDef._ContentAttr == 2)
			{
				builder.SendValidationEvent("The group is not allowed when ElementType has empty or textOnly content.");
			}
			builder.PushGroupInfo();
			builder._GroupDef._MinVal = 1U;
			builder._GroupDef._MaxVal = 1U;
			builder._GroupDef._HasMaxAttr = false;
			builder._GroupDef._HasMinAttr = false;
			if (builder._ElementDef._ExistTerminal)
			{
				builder.AddOrder();
			}
			builder._ElementDef._ExistTerminal = false;
			builder._contentValidator.OpenGroup();
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000BB0E8 File Offset: 0x000B92E8
		private static void XDR_BuildGroup_Order(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._Order = builder.GetOrder((XmlQualifiedName)obj);
			if (builder._ElementDef._ContentAttr == 3 && builder._GroupDef._Order != 1)
			{
				builder.SendValidationEvent("The order must be many when content is mixed.");
			}
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000BB128 File Offset: 0x000B9328
		private static void XDR_BuildGroup_MinOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._MinVal = XdrBuilder.ParseMinOccurs(obj, builder);
			builder._GroupDef._HasMinAttr = true;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000BB148 File Offset: 0x000B9348
		private static void XDR_BuildGroup_MaxOccurs(XdrBuilder builder, object obj, string prefix)
		{
			builder._GroupDef._MaxVal = XdrBuilder.ParseMaxOccurs(obj, builder);
			builder._GroupDef._HasMaxAttr = true;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x000BB168 File Offset: 0x000B9368
		private static void XDR_EndGroup(XdrBuilder builder)
		{
			if (!builder._ElementDef._ExistTerminal)
			{
				builder.SendValidationEvent("There is a missing element.");
			}
			builder._contentValidator.CloseGroup();
			if (builder._GroupDef._Order == 1)
			{
				builder._contentValidator.AddStar();
			}
			if (1 == builder._GroupDef._Order && builder._GroupDef._HasMaxAttr && builder._GroupDef._MaxVal != 4294967295U)
			{
				builder.SendValidationEvent("When the order is many, the maxOccurs attribute must have a value of '*'.");
			}
			XdrBuilder.HandleMinMax(builder._contentValidator, builder._GroupDef._MinVal, builder._GroupDef._MaxVal);
			builder.PopGroupInfo();
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000BB20C File Offset: 0x000B940C
		private static void XDR_InitElementDtType(XdrBuilder builder, object obj)
		{
			if (builder._ElementDef._HasDataType)
			{
				builder.SendValidationEvent("Data type has already been declared.");
			}
			if (!builder._ElementDef._AllowDataType)
			{
				builder.SendValidationEvent("Content must be \"textOnly\" when using DataType on an ElementType.");
			}
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x000BB240 File Offset: 0x000B9440
		private static void XDR_EndElementDtType(XdrBuilder builder)
		{
			if (!builder._ElementDef._HasDataType)
			{
				builder.SendValidationEvent("The '{0}' attribute is either invalid or missing.");
			}
			builder._ElementDef._ElementDecl.ContentValidator = ContentValidator.TextOnly;
			builder._ElementDef._ContentAttr = 2;
			builder._ElementDef._MasterGroupRequired = false;
			builder._contentValidator = null;
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000BB299 File Offset: 0x000B9499
		private static void XDR_InitAttributeDtType(XdrBuilder builder, object obj)
		{
			if (builder._AttributeDef._HasDataType)
			{
				builder.SendValidationEvent("Data type has already been declared.");
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x000BB2B4 File Offset: 0x000B94B4
		private static void XDR_EndAttributeDtType(XdrBuilder builder)
		{
			string text = null;
			if (!builder._AttributeDef._HasDataType)
			{
				text = "The '{0}' attribute is either invalid or missing.";
			}
			else if (builder._AttributeDef._AttDef.Datatype != null)
			{
				XmlTokenizedType tokenizedType = builder._AttributeDef._AttDef.Datatype.TokenizedType;
				if (tokenizedType == XmlTokenizedType.ENUMERATION && !builder._AttributeDef._EnumerationRequired)
				{
					text = "The dt:values attribute is missing.";
				}
				else if (tokenizedType != XmlTokenizedType.ENUMERATION && builder._AttributeDef._EnumerationRequired)
				{
					text = "Data type should be enumeration when the values attribute is present.";
				}
			}
			if (text != null)
			{
				builder.SendValidationEvent(text);
			}
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000BB33C File Offset: 0x000B953C
		private bool GetNextState(XmlQualifiedName qname)
		{
			if (this._CurState._NextStates != null)
			{
				for (int i = 0; i < this._CurState._NextStates.Length; i++)
				{
					if (this._SchemaNames.TokenToQName[(int)XdrBuilder.S_SchemaEntries[this._CurState._NextStates[i]]._Name].Equals(qname))
					{
						this._NextState = XdrBuilder.S_SchemaEntries[this._CurState._NextStates[i]];
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000BB3B8 File Offset: 0x000B95B8
		private bool IsSkipableElement(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			return (@namespace != null && !Ref.Equal(@namespace, this._SchemaNames.NsXdr)) || (this._SchemaNames.TokenToQName[38].Equals(qname) || this._SchemaNames.TokenToQName[39].Equals(qname));
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000BB414 File Offset: 0x000B9614
		private bool IsSkipableAttribute(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			return (@namespace.Length != 0 && !Ref.Equal(@namespace, this._SchemaNames.NsXdr) && !Ref.Equal(@namespace, this._SchemaNames.NsDataType)) || (Ref.Equal(@namespace, this._SchemaNames.NsDataType) && this._CurState._Name == SchemaNames.Token.XdrDatatype && (this._SchemaNames.QnDtMax.Equals(qname) || this._SchemaNames.QnDtMin.Equals(qname) || this._SchemaNames.QnDtMaxExclusive.Equals(qname) || this._SchemaNames.QnDtMinExclusive.Equals(qname)));
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x000BB4CC File Offset: 0x000B96CC
		private int GetOrder(XmlQualifiedName qname)
		{
			int num = 0;
			if (this._SchemaNames.TokenToQName[15].Equals(qname))
			{
				num = 2;
			}
			else if (this._SchemaNames.TokenToQName[16].Equals(qname))
			{
				num = 3;
			}
			else if (this._SchemaNames.TokenToQName[17].Equals(qname))
			{
				num = 1;
			}
			else
			{
				this.SendValidationEvent("The order attribute must have a value of 'seq', 'one', or 'many', not '{0}'.", qname.Name);
			}
			return num;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x000BB53C File Offset: 0x000B973C
		private void AddOrder()
		{
			switch (this._GroupDef._Order)
			{
			case 1:
			case 3:
				this._contentValidator.AddChoice();
				return;
			case 2:
				this._contentValidator.AddSequence();
				return;
			}
			throw new XmlException("This is an unexpected token. The expected token is '{0}'.", "NAME");
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x000BB598 File Offset: 0x000B9798
		private static bool IsYes(object obj, XdrBuilder builder)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
			bool flag = false;
			if (xmlQualifiedName.Name == "yes")
			{
				flag = true;
			}
			else if (xmlQualifiedName.Name != "no")
			{
				builder.SendValidationEvent("The required attribute must have a value of yes or no.");
			}
			return flag;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x000BB5E4 File Offset: 0x000B97E4
		private static uint ParseMinOccurs(object obj, XdrBuilder builder)
		{
			uint num = 1U;
			if (!XdrBuilder.ParseInteger((string)obj, ref num) || (num != 0U && num != 1U))
			{
				builder.SendValidationEvent("The minOccurs attribute must have a value of 0 or 1.");
			}
			return num;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x000BB618 File Offset: 0x000B9818
		private static uint ParseMaxOccurs(object obj, XdrBuilder builder)
		{
			uint maxValue = uint.MaxValue;
			string text = (string)obj;
			if (!text.Equals("*") && (!XdrBuilder.ParseInteger(text, ref maxValue) || (maxValue != 4294967295U && maxValue != 1U)))
			{
				builder.SendValidationEvent("The maxOccurs attribute must have a value of 1 or *.");
			}
			return maxValue;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x000BB659 File Offset: 0x000B9859
		private static void HandleMinMax(ParticleContentValidator pContent, uint cMin, uint cMax)
		{
			if (pContent != null)
			{
				if (cMax == 4294967295U)
				{
					if (cMin == 0U)
					{
						pContent.AddStar();
						return;
					}
					pContent.AddPlus();
					return;
				}
				else if (cMin == 0U)
				{
					pContent.AddQMark();
				}
			}
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x000BB67C File Offset: 0x000B987C
		private static void ParseDtMaxLength(ref uint cVal, object obj, XdrBuilder builder)
		{
			if (4294967295U != cVal)
			{
				builder.SendValidationEvent("The value of maxLength has already been declared.");
			}
			if (!XdrBuilder.ParseInteger((string)obj, ref cVal) || cVal < 0U)
			{
				builder.SendValidationEvent("The value '{0}' is invalid for dt:maxLength.", obj.ToString());
			}
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x000BB6B2 File Offset: 0x000B98B2
		private static void ParseDtMinLength(ref uint cVal, object obj, XdrBuilder builder)
		{
			if (4294967295U != cVal)
			{
				builder.SendValidationEvent("The value of minLength has already been declared.");
			}
			if (!XdrBuilder.ParseInteger((string)obj, ref cVal) || cVal < 0U)
			{
				builder.SendValidationEvent("The value '{0}' is invalid for dt:minLength.", obj.ToString());
			}
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000BB6E8 File Offset: 0x000B98E8
		private static void CompareMinMaxLength(uint cMin, uint cMax, XdrBuilder builder)
		{
			if (cMin != 4294967295U && cMax != 4294967295U && cMin > cMax)
			{
				builder.SendValidationEvent("The maxLength value must be equal to or greater than the minLength value.");
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x000BB701 File Offset: 0x000B9901
		private static bool ParseInteger(string str, ref uint n)
		{
			return uint.TryParse(str, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out n);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000BB710 File Offset: 0x000B9910
		private void XDR_CheckAttributeDefault(XdrBuilder.DeclBaseInfo decl, SchemaAttDef pAttdef)
		{
			if ((decl._Default != null || pAttdef.DefaultValueTyped != null) && decl._Default != null)
			{
				pAttdef.DefaultValueRaw = (pAttdef.DefaultValueExpanded = (string)decl._Default);
				this.CheckDefaultAttValue(pAttdef);
			}
			this.SetAttributePresence(pAttdef, 1U == decl._MinOccurs);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x000BB768 File Offset: 0x000B9968
		private void SetAttributePresence(SchemaAttDef pAttdef, bool fRequired)
		{
			if (SchemaDeclBase.Use.Fixed != pAttdef.Presence)
			{
				if (fRequired || SchemaDeclBase.Use.Required == pAttdef.Presence)
				{
					if (pAttdef.DefaultValueTyped != null)
					{
						pAttdef.Presence = SchemaDeclBase.Use.Fixed;
						return;
					}
					pAttdef.Presence = SchemaDeclBase.Use.Required;
					return;
				}
				else
				{
					if (pAttdef.DefaultValueTyped != null)
					{
						pAttdef.Presence = SchemaDeclBase.Use.Default;
						return;
					}
					pAttdef.Presence = SchemaDeclBase.Use.Implied;
				}
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000BB7BC File Offset: 0x000B99BC
		private int GetContent(XmlQualifiedName qname)
		{
			int num = 0;
			if (this._SchemaNames.TokenToQName[11].Equals(qname))
			{
				num = 1;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[12].Equals(qname))
			{
				num = 4;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[10].Equals(qname))
			{
				num = 3;
				this._ElementDef._AllowDataType = false;
			}
			else if (this._SchemaNames.TokenToQName[13].Equals(qname))
			{
				num = 2;
			}
			else
			{
				this.SendValidationEvent("The content attribute must have a value of 'textOnly', 'eltOnly', 'mixed', or 'empty', not '{0}'.", qname.Name);
			}
			return num;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x000BB86C File Offset: 0x000B9A6C
		private bool GetModel(XmlQualifiedName qname)
		{
			bool flag = false;
			if (this._SchemaNames.TokenToQName[7].Equals(qname))
			{
				flag = true;
			}
			else if (this._SchemaNames.TokenToQName[8].Equals(qname))
			{
				flag = false;
			}
			else
			{
				this.SendValidationEvent("The model attribute must have a value of open or closed, not '{0}'.", qname.Name);
			}
			return flag;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000BB8C0 File Offset: 0x000B9AC0
		private XmlSchemaDatatype CheckDatatype(string str)
		{
			XmlSchemaDatatype xmlSchemaDatatype = XmlSchemaDatatype.FromXdrName(str);
			if (xmlSchemaDatatype == null)
			{
				this.SendValidationEvent("Reference to an unknown data type, '{0}'.", str);
			}
			else if (xmlSchemaDatatype.TokenizedType == XmlTokenizedType.ID && !this._AttributeDef._Global)
			{
				if (this._ElementDef._ElementDecl.IsIdDeclared)
				{
					this.SendValidationEvent("The attribute of type ID is already declared on the '{0}' element.", XmlQualifiedName.ToString(this._ElementDef._ElementDecl.Name.Name, this._ElementDef._ElementDecl.Prefix));
				}
				this._ElementDef._ElementDecl.IsIdDeclared = true;
			}
			return xmlSchemaDatatype;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x000BB954 File Offset: 0x000B9B54
		private void CheckDefaultAttValue(SchemaAttDef attDef)
		{
			XdrValidator.CheckDefaultValue(attDef.DefaultValueRaw.Trim(), attDef, this._SchemaInfo, this._CurNsMgr, this._NameTable, null, this.validationEventHandler, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x000BB9AC File Offset: 0x000B9BAC
		private bool IsGlobal(int flags)
		{
			return flags == 256;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000BB9B6 File Offset: 0x000B9BB6
		private void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000BB9E7 File Offset: 0x000B9BE7
		private void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000BB9F5 File Offset: 0x000B9BF5
		private void SendValidationEvent(string code, string msg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this._reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), XmlSeverityType.Error);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000BBA28 File Offset: 0x000B9C28
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			SchemaInfo schemaInfo = this._SchemaInfo;
			int errorCount = schemaInfo.ErrorCount;
			schemaInfo.ErrorCount = errorCount + 1;
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(this, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x04000E66 RID: 3686
		private static readonly int[] S_XDR_Root_Element = new int[] { 1 };

		// Token: 0x04000E67 RID: 3687
		private static readonly int[] S_XDR_Root_SubElements = new int[] { 2, 3 };

		// Token: 0x04000E68 RID: 3688
		private static readonly int[] S_XDR_ElementType_SubElements = new int[] { 4, 6, 3, 5, 7 };

		// Token: 0x04000E69 RID: 3689
		private static readonly int[] S_XDR_AttributeType_SubElements = new int[] { 8 };

		// Token: 0x04000E6A RID: 3690
		private static readonly int[] S_XDR_Group_SubElements = new int[] { 4, 6 };

		// Token: 0x04000E6B RID: 3691
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Root_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildRoot_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaId, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildRoot_ID))
		};

		// Token: 0x04000E6C RID: 3692
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_ElementType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.QName, 256, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaContent, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Content)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaModel, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Model)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaOrder, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_Order)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMinLength))
		};

		// Token: 0x04000E6D RID: 3693
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_AttributeType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaName, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Name)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaRequired, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Required)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDefault, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_Default)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMinLength))
		};

		// Token: 0x04000E6E RID: 3694
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Element_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaType, XmlTokenizedType.QName, 256, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_Type)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMinOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_MinOccurs)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElement_MaxOccurs))
		};

		// Token: 0x04000E6F RID: 3695
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Attribute_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Type)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaRequired, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Required)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDefault, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttribute_Default))
		};

		// Token: 0x04000E70 RID: 3696
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_Group_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaOrder, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_Order)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMinOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_MinOccurs)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaMaxOccurs, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildGroup_MaxOccurs))
		};

		// Token: 0x04000E71 RID: 3697
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_ElementDataType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildElementType_DtMinLength))
		};

		// Token: 0x04000E72 RID: 3698
		private static readonly XdrBuilder.XdrAttributeEntry[] S_XDR_AttributeDataType_Attributes = new XdrBuilder.XdrAttributeEntry[]
		{
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtType, XmlTokenizedType.QName, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtType)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtValues, XmlTokenizedType.NMTOKENS, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtValues)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMaxLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMaxLength)),
			new XdrBuilder.XdrAttributeEntry(SchemaNames.Token.SchemaDtMinLength, XmlTokenizedType.CDATA, new XdrBuilder.XdrBuildFunction(XdrBuilder.XDR_BuildAttributeType_DtMinLength))
		};

		// Token: 0x04000E73 RID: 3699
		private static readonly XdrBuilder.XdrEntry[] S_SchemaEntries = new XdrBuilder.XdrEntry[]
		{
			new XdrBuilder.XdrEntry(SchemaNames.Token.Empty, XdrBuilder.S_XDR_Root_Element, null, null, null, null, false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrRoot, XdrBuilder.S_XDR_Root_SubElements, XdrBuilder.S_XDR_Root_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitRoot), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginRoot), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndRoot), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrElementType, XdrBuilder.S_XDR_ElementType_SubElements, XdrBuilder.S_XDR_ElementType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElementType), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginElementType), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElementType), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrAttributeType, XdrBuilder.S_XDR_AttributeType_SubElements, XdrBuilder.S_XDR_AttributeType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttributeType), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginAttributeType), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttributeType), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrElement, null, XdrBuilder.S_XDR_Element_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElement), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElement), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrAttribute, null, XdrBuilder.S_XDR_Attribute_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttribute), new XdrBuilder.XdrBeginChildFunction(XdrBuilder.XDR_BeginAttribute), new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttribute), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrGroup, XdrBuilder.S_XDR_Group_SubElements, XdrBuilder.S_XDR_Group_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitGroup), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndGroup), false),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrDatatype, null, XdrBuilder.S_XDR_ElementDataType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitElementDtType), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndElementDtType), true),
			new XdrBuilder.XdrEntry(SchemaNames.Token.XdrDatatype, null, XdrBuilder.S_XDR_AttributeDataType_Attributes, new XdrBuilder.XdrInitFunction(XdrBuilder.XDR_InitAttributeDtType), null, new XdrBuilder.XdrEndChildFunction(XdrBuilder.XDR_EndAttributeDtType), true)
		};

		// Token: 0x04000E74 RID: 3700
		private SchemaInfo _SchemaInfo;

		// Token: 0x04000E75 RID: 3701
		private string _TargetNamespace;

		// Token: 0x04000E76 RID: 3702
		private XmlReader _reader;

		// Token: 0x04000E77 RID: 3703
		private PositionInfo positionInfo;

		// Token: 0x04000E78 RID: 3704
		private ParticleContentValidator _contentValidator;

		// Token: 0x04000E79 RID: 3705
		private XdrBuilder.XdrEntry _CurState;

		// Token: 0x04000E7A RID: 3706
		private XdrBuilder.XdrEntry _NextState;

		// Token: 0x04000E7B RID: 3707
		private HWStack _StateHistory;

		// Token: 0x04000E7C RID: 3708
		private HWStack _GroupStack;

		// Token: 0x04000E7D RID: 3709
		private string _XdrName;

		// Token: 0x04000E7E RID: 3710
		private string _XdrPrefix;

		// Token: 0x04000E7F RID: 3711
		private XdrBuilder.ElementContent _ElementDef;

		// Token: 0x04000E80 RID: 3712
		private XdrBuilder.GroupContent _GroupDef;

		// Token: 0x04000E81 RID: 3713
		private XdrBuilder.AttributeContent _AttributeDef;

		// Token: 0x04000E82 RID: 3714
		private XdrBuilder.DeclBaseInfo _UndefinedAttributeTypes;

		// Token: 0x04000E83 RID: 3715
		private XdrBuilder.DeclBaseInfo _BaseDecl;

		// Token: 0x04000E84 RID: 3716
		private XmlNameTable _NameTable;

		// Token: 0x04000E85 RID: 3717
		private SchemaNames _SchemaNames;

		// Token: 0x04000E86 RID: 3718
		private XmlNamespaceManager _CurNsMgr;

		// Token: 0x04000E87 RID: 3719
		private string _Text;

		// Token: 0x04000E88 RID: 3720
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04000E89 RID: 3721
		private Hashtable _UndeclaredElements = new Hashtable();

		// Token: 0x04000E8A RID: 3722
		private XmlResolver xmlResolver;

		// Token: 0x020002A6 RID: 678
		private sealed class DeclBaseInfo
		{
			// Token: 0x06001F62 RID: 8034 RVA: 0x000BBFF1 File Offset: 0x000BA1F1
			internal DeclBaseInfo()
			{
				this.Reset();
			}

			// Token: 0x06001F63 RID: 8035 RVA: 0x000BC000 File Offset: 0x000BA200
			internal void Reset()
			{
				this._Name = XmlQualifiedName.Empty;
				this._Prefix = null;
				this._TypeName = XmlQualifiedName.Empty;
				this._TypePrefix = null;
				this._Default = null;
				this._Revises = null;
				this._MaxOccurs = 1U;
				this._MinOccurs = 1U;
				this._Checking = false;
				this._ElementDecl = null;
				this._Next = null;
				this._Attdef = null;
			}

			// Token: 0x04000E8B RID: 3723
			internal XmlQualifiedName _Name;

			// Token: 0x04000E8C RID: 3724
			internal string _Prefix;

			// Token: 0x04000E8D RID: 3725
			internal XmlQualifiedName _TypeName;

			// Token: 0x04000E8E RID: 3726
			internal string _TypePrefix;

			// Token: 0x04000E8F RID: 3727
			internal object _Default;

			// Token: 0x04000E90 RID: 3728
			internal object _Revises;

			// Token: 0x04000E91 RID: 3729
			internal uint _MaxOccurs;

			// Token: 0x04000E92 RID: 3730
			internal uint _MinOccurs;

			// Token: 0x04000E93 RID: 3731
			internal bool _Checking;

			// Token: 0x04000E94 RID: 3732
			internal SchemaElementDecl _ElementDecl;

			// Token: 0x04000E95 RID: 3733
			internal SchemaAttDef _Attdef;

			// Token: 0x04000E96 RID: 3734
			internal XdrBuilder.DeclBaseInfo _Next;
		}

		// Token: 0x020002A7 RID: 679
		private sealed class GroupContent
		{
			// Token: 0x06001F64 RID: 8036 RVA: 0x000BC069 File Offset: 0x000BA269
			internal static void Copy(XdrBuilder.GroupContent from, XdrBuilder.GroupContent to)
			{
				to._MinVal = from._MinVal;
				to._MaxVal = from._MaxVal;
				to._Order = from._Order;
			}

			// Token: 0x06001F65 RID: 8037 RVA: 0x000BC090 File Offset: 0x000BA290
			internal static XdrBuilder.GroupContent Copy(XdrBuilder.GroupContent other)
			{
				XdrBuilder.GroupContent groupContent = new XdrBuilder.GroupContent();
				XdrBuilder.GroupContent.Copy(other, groupContent);
				return groupContent;
			}

			// Token: 0x04000E97 RID: 3735
			internal uint _MinVal;

			// Token: 0x04000E98 RID: 3736
			internal uint _MaxVal;

			// Token: 0x04000E99 RID: 3737
			internal bool _HasMaxAttr;

			// Token: 0x04000E9A RID: 3738
			internal bool _HasMinAttr;

			// Token: 0x04000E9B RID: 3739
			internal int _Order;
		}

		// Token: 0x020002A8 RID: 680
		private sealed class ElementContent
		{
			// Token: 0x04000E9C RID: 3740
			internal SchemaElementDecl _ElementDecl;

			// Token: 0x04000E9D RID: 3741
			internal int _ContentAttr;

			// Token: 0x04000E9E RID: 3742
			internal int _OrderAttr;

			// Token: 0x04000E9F RID: 3743
			internal bool _MasterGroupRequired;

			// Token: 0x04000EA0 RID: 3744
			internal bool _ExistTerminal;

			// Token: 0x04000EA1 RID: 3745
			internal bool _AllowDataType;

			// Token: 0x04000EA2 RID: 3746
			internal bool _HasDataType;

			// Token: 0x04000EA3 RID: 3747
			internal bool _HasType;

			// Token: 0x04000EA4 RID: 3748
			internal bool _EnumerationRequired;

			// Token: 0x04000EA5 RID: 3749
			internal uint _MinVal;

			// Token: 0x04000EA6 RID: 3750
			internal uint _MaxVal;

			// Token: 0x04000EA7 RID: 3751
			internal uint _MaxLength;

			// Token: 0x04000EA8 RID: 3752
			internal uint _MinLength;

			// Token: 0x04000EA9 RID: 3753
			internal Hashtable _AttDefList;
		}

		// Token: 0x020002A9 RID: 681
		private sealed class AttributeContent
		{
			// Token: 0x04000EAA RID: 3754
			internal SchemaAttDef _AttDef;

			// Token: 0x04000EAB RID: 3755
			internal XmlQualifiedName _Name;

			// Token: 0x04000EAC RID: 3756
			internal string _Prefix;

			// Token: 0x04000EAD RID: 3757
			internal bool _Required;

			// Token: 0x04000EAE RID: 3758
			internal uint _MinVal;

			// Token: 0x04000EAF RID: 3759
			internal uint _MaxVal;

			// Token: 0x04000EB0 RID: 3760
			internal uint _MaxLength;

			// Token: 0x04000EB1 RID: 3761
			internal uint _MinLength;

			// Token: 0x04000EB2 RID: 3762
			internal bool _EnumerationRequired;

			// Token: 0x04000EB3 RID: 3763
			internal bool _HasDataType;

			// Token: 0x04000EB4 RID: 3764
			internal bool _Global;

			// Token: 0x04000EB5 RID: 3765
			internal object _Default;
		}

		// Token: 0x020002AA RID: 682
		// (Invoke) Token: 0x06001F6A RID: 8042
		private delegate void XdrBuildFunction(XdrBuilder builder, object obj, string prefix);

		// Token: 0x020002AB RID: 683
		// (Invoke) Token: 0x06001F6C RID: 8044
		private delegate void XdrInitFunction(XdrBuilder builder, object obj);

		// Token: 0x020002AC RID: 684
		// (Invoke) Token: 0x06001F6E RID: 8046
		private delegate void XdrBeginChildFunction(XdrBuilder builder);

		// Token: 0x020002AD RID: 685
		// (Invoke) Token: 0x06001F70 RID: 8048
		private delegate void XdrEndChildFunction(XdrBuilder builder);

		// Token: 0x020002AE RID: 686
		private sealed class XdrAttributeEntry
		{
			// Token: 0x06001F71 RID: 8049 RVA: 0x000BC0AB File Offset: 0x000BA2AB
			internal XdrAttributeEntry(SchemaNames.Token a, XmlTokenizedType ttype, XdrBuilder.XdrBuildFunction build)
			{
				this._Attribute = a;
				this._Datatype = XmlSchemaDatatype.FromXmlTokenizedType(ttype);
				this._SchemaFlags = 0;
				this._BuildFunc = build;
			}

			// Token: 0x06001F72 RID: 8050 RVA: 0x000BC0D4 File Offset: 0x000BA2D4
			internal XdrAttributeEntry(SchemaNames.Token a, XmlTokenizedType ttype, int schemaFlags, XdrBuilder.XdrBuildFunction build)
			{
				this._Attribute = a;
				this._Datatype = XmlSchemaDatatype.FromXmlTokenizedType(ttype);
				this._SchemaFlags = schemaFlags;
				this._BuildFunc = build;
			}

			// Token: 0x04000EB6 RID: 3766
			internal SchemaNames.Token _Attribute;

			// Token: 0x04000EB7 RID: 3767
			internal int _SchemaFlags;

			// Token: 0x04000EB8 RID: 3768
			internal XmlSchemaDatatype _Datatype;

			// Token: 0x04000EB9 RID: 3769
			internal XdrBuilder.XdrBuildFunction _BuildFunc;
		}

		// Token: 0x020002AF RID: 687
		private sealed class XdrEntry
		{
			// Token: 0x06001F73 RID: 8051 RVA: 0x000BC0FE File Offset: 0x000BA2FE
			internal XdrEntry(SchemaNames.Token n, int[] states, XdrBuilder.XdrAttributeEntry[] attributes, XdrBuilder.XdrInitFunction init, XdrBuilder.XdrBeginChildFunction begin, XdrBuilder.XdrEndChildFunction end, bool fText)
			{
				this._Name = n;
				this._NextStates = states;
				this._Attributes = attributes;
				this._InitFunc = init;
				this._BeginChildFunc = begin;
				this._EndChildFunc = end;
				this._AllowText = fText;
			}

			// Token: 0x04000EBA RID: 3770
			internal SchemaNames.Token _Name;

			// Token: 0x04000EBB RID: 3771
			internal int[] _NextStates;

			// Token: 0x04000EBC RID: 3772
			internal XdrBuilder.XdrAttributeEntry[] _Attributes;

			// Token: 0x04000EBD RID: 3773
			internal XdrBuilder.XdrInitFunction _InitFunc;

			// Token: 0x04000EBE RID: 3774
			internal XdrBuilder.XdrBeginChildFunction _BeginChildFunc;

			// Token: 0x04000EBF RID: 3775
			internal XdrBuilder.XdrEndChildFunction _EndChildFunc;

			// Token: 0x04000EC0 RID: 3776
			internal bool _AllowText;
		}
	}
}
