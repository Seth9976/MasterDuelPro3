using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020002B0 RID: 688
	internal sealed class XdrValidator : BaseValidator
	{
		// Token: 0x06001F74 RID: 8052 RVA: 0x000BC13B File Offset: 0x000BA33B
		internal XdrValidator(BaseValidator validator)
			: base(validator)
		{
			this.Init();
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000BC155 File Offset: 0x000BA355
		internal XdrValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
			: base(reader, schemaCollection, eventHandling)
		{
			this.Init();
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000BC174 File Offset: 0x000BA374
		private void Init()
		{
			this.nsManager = this.reader.NamespaceManager;
			if (this.nsManager == null)
			{
				this.nsManager = new XmlNamespaceManager(base.NameTable);
				this.isProcessContents = true;
			}
			this.validationStack = new HWStack(10);
			this.textValue = new StringBuilder();
			this.name = XmlQualifiedName.Empty;
			this.attPresence = new Hashtable();
			this.Push(XmlQualifiedName.Empty);
			this.schemaInfo = new SchemaInfo();
			this.checkDatatype = false;
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000BC200 File Offset: 0x000BA400
		public override void Validate()
		{
			if (this.IsInlineSchemaStarted)
			{
				this.ProcessInlineSchema();
				return;
			}
			XmlNodeType nodeType = this.reader.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				if (nodeType - XmlNodeType.Text > 1)
				{
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
						base.ValidateWhitespace();
						return;
					case XmlNodeType.SignificantWhitespace:
						break;
					case XmlNodeType.EndElement:
						goto IL_005E;
					default:
						return;
					}
				}
				base.ValidateText();
				return;
			}
			this.ValidateElement();
			if (!this.reader.IsEmptyElement)
			{
				return;
			}
			IL_005E:
			this.ValidateEndElement();
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000BC274 File Offset: 0x000BA474
		private void ValidateElement()
		{
			this.elementName.Init(this.reader.LocalName, XmlSchemaDatatype.XdrCanonizeUri(this.reader.NamespaceURI, base.NameTable, base.SchemaNames));
			this.ValidateChildElement();
			if (base.SchemaNames.IsXDRRoot(this.elementName.Name, this.elementName.Namespace) && this.reader.Depth > 0)
			{
				this.inlineSchemaParser = new Parser(SchemaType.XDR, base.NameTable, base.SchemaNames, base.EventHandler);
				this.inlineSchemaParser.StartParsing(this.reader, null);
				this.inlineSchemaParser.ParseReaderNode();
				return;
			}
			this.ProcessElement();
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000BC330 File Offset: 0x000BA530
		private void ValidateChildElement()
		{
			if (this.context.NeedValidateChildren)
			{
				int num = 0;
				this.context.ElementDecl.ContentValidator.ValidateElement(this.elementName, this.context, out num);
				if (num < 0)
				{
					XmlSchemaValidator.ElementValidationError(this.elementName, this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
				}
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000BC3B3 File Offset: 0x000BA5B3
		private bool IsInlineSchemaStarted
		{
			get
			{
				return this.inlineSchemaParser != null;
			}
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000BC3C0 File Offset: 0x000BA5C0
		private void ProcessInlineSchema()
		{
			if (!this.inlineSchemaParser.ParseReaderNode())
			{
				this.inlineSchemaParser.FinishParsing();
				SchemaInfo xdrSchema = this.inlineSchemaParser.XdrSchema;
				if (xdrSchema != null && xdrSchema.ErrorCount == 0)
				{
					foreach (string text in xdrSchema.TargetNamespaces.Keys)
					{
						if (!this.schemaInfo.HasSchema(text))
						{
							this.schemaInfo.Add(xdrSchema, base.EventHandler);
							base.SchemaCollection.Add(text, xdrSchema, null, false);
							break;
						}
					}
				}
				this.inlineSchemaParser = null;
			}
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000BC480 File Offset: 0x000BA680
		private void ProcessElement()
		{
			this.Push(this.elementName);
			if (this.isProcessContents)
			{
				this.nsManager.PopScope();
			}
			this.context.ElementDecl = this.ThoroughGetElementDecl();
			if (this.context.ElementDecl != null)
			{
				this.ValidateStartElement();
				this.ValidateEndStartElement();
				this.context.NeedValidateChildren = true;
				this.context.ElementDecl.ContentValidator.InitValidation(this.context);
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000BC500 File Offset: 0x000BA700
		private void ValidateEndElement()
		{
			if (this.isProcessContents)
			{
				this.nsManager.PopScope();
			}
			if (this.context.ElementDecl != null)
			{
				if (this.context.NeedValidateChildren && !this.context.ElementDecl.ContentValidator.CompleteValidation(this.context))
				{
					XmlSchemaValidator.CompleteValidationError(this.context, base.EventHandler, this.reader, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition, null);
				}
				if (this.checkDatatype)
				{
					string text = ((!this.hasSibling) ? this.textString : this.textValue.ToString());
					this.CheckValue(text, null);
					this.checkDatatype = false;
					this.textValue.Length = 0;
					this.textString = string.Empty;
				}
			}
			this.Pop();
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000BC5E4 File Offset: 0x000BA7E4
		private SchemaElementDecl ThoroughGetElementDecl()
		{
			if (this.reader.Depth == 0)
			{
				this.LoadSchema(string.Empty);
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, base.SchemaNames.NsXmlNs))
					{
						this.LoadSchema(this.reader.Value);
						if (this.isProcessContents)
						{
							this.nsManager.AddNamespace((this.reader.Prefix.Length == 0) ? string.Empty : this.reader.LocalName, this.reader.Value);
						}
					}
					if (Ref.Equal(namespaceURI, base.SchemaNames.QnDtDt.Namespace) && Ref.Equal(localName, base.SchemaNames.QnDtDt.Name))
					{
						this.reader.SchemaTypeObject = XmlSchemaDatatype.FromXdrName(this.reader.Value);
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
			SchemaElementDecl elementDecl = this.schemaInfo.GetElementDecl(this.elementName);
			if (elementDecl == null && this.schemaInfo.TargetNamespaces.ContainsKey(this.context.Namespace))
			{
				base.SendValidationEvent("The '{0}' element is not declared.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			}
			return elementDecl;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000BC754 File Offset: 0x000BA954
		private void ValidateStartElement()
		{
			if (this.context.ElementDecl != null)
			{
				if (this.context.ElementDecl.SchemaType != null)
				{
					this.reader.SchemaTypeObject = this.context.ElementDecl.SchemaType;
				}
				else
				{
					this.reader.SchemaTypeObject = this.context.ElementDecl.Datatype;
				}
				if (this.reader.IsEmptyElement && !this.context.IsNill && this.context.ElementDecl.DefaultValueTyped != null)
				{
					this.reader.TypedValueObject = this.context.ElementDecl.DefaultValueTyped;
					this.context.IsNill = true;
				}
				if (this.context.ElementDecl.HasRequiredAttribute)
				{
					this.attPresence.Clear();
				}
			}
			if (this.reader.MoveToFirstAttribute())
			{
				do
				{
					if (this.reader.NamespaceURI != base.SchemaNames.NsXmlNs)
					{
						try
						{
							this.reader.SchemaTypeObject = null;
							SchemaAttDef attributeXdr = this.schemaInfo.GetAttributeXdr(this.context.ElementDecl, this.QualifiedName(this.reader.LocalName, this.reader.NamespaceURI));
							if (attributeXdr != null)
							{
								if (this.context.ElementDecl != null && this.context.ElementDecl.HasRequiredAttribute)
								{
									this.attPresence.Add(attributeXdr.Name, attributeXdr);
								}
								this.reader.SchemaTypeObject = ((attributeXdr.SchemaType != null) ? attributeXdr.SchemaType : attributeXdr.Datatype);
								if (attributeXdr.Datatype != null)
								{
									string value = this.reader.Value;
									this.CheckValue(value, attributeXdr);
								}
							}
						}
						catch (XmlSchemaException ex)
						{
							ex.SetSource(this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
							base.SendValidationEvent(ex);
						}
					}
				}
				while (this.reader.MoveToNextAttribute());
				this.reader.MoveToElement();
			}
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000BC968 File Offset: 0x000BAB68
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasDefaultAttribute)
			{
				for (int i = 0; i < this.context.ElementDecl.DefaultAttDefs.Count; i++)
				{
					this.reader.AddDefaultAttribute((SchemaAttDef)this.context.ElementDecl.DefaultAttDefs[i]);
				}
			}
			if (this.context.ElementDecl.HasRequiredAttribute)
			{
				try
				{
					this.context.ElementDecl.CheckAttributes(this.attPresence, this.reader.StandAlone);
				}
				catch (XmlSchemaException ex)
				{
					ex.SetSource(this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					base.SendValidationEvent(ex);
				}
			}
			if (this.context.ElementDecl.Datatype != null)
			{
				this.checkDatatype = true;
				this.hasSibling = false;
				this.textString = string.Empty;
				this.textValue.Length = 0;
			}
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000BCA7C File Offset: 0x000BAC7C
		private void LoadSchemaFromLocation(string uri)
		{
			if (!XdrBuilder.IsXdrSchema(uri))
			{
				return;
			}
			string text = uri.Substring("x-schema:".Length);
			XmlReader xmlReader = null;
			SchemaInfo schemaInfo = null;
			try
			{
				Uri uri2 = base.XmlResolver.ResolveUri(base.BaseUri, text);
				Stream stream = (Stream)base.XmlResolver.GetEntity(uri2, null, null);
				xmlReader = new XmlTextReader(uri2.ToString(), stream, base.NameTable);
				((XmlTextReader)xmlReader).XmlResolver = base.XmlResolver;
				Parser parser = new Parser(SchemaType.XDR, base.NameTable, base.SchemaNames, base.EventHandler);
				parser.XmlResolver = base.XmlResolver;
				parser.Parse(xmlReader, uri);
				while (xmlReader.Read())
				{
				}
				schemaInfo = parser.XdrSchema;
			}
			catch (XmlSchemaException ex)
			{
				base.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[] { uri, ex.Message }, XmlSeverityType.Error);
			}
			catch (Exception ex2)
			{
				base.SendValidationEvent("Cannot load the schema for the namespace '{0}' - {1}", new string[] { uri, ex2.Message }, XmlSeverityType.Warning);
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
				this.schemaInfo.Add(schemaInfo, base.EventHandler);
				base.SchemaCollection.Add(uri, schemaInfo, null, false);
			}
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x000BCBE0 File Offset: 0x000BADE0
		private void LoadSchema(string uri)
		{
			if (this.schemaInfo.TargetNamespaces.ContainsKey(uri))
			{
				return;
			}
			if (base.XmlResolver == null)
			{
				return;
			}
			SchemaInfo schemaInfo = null;
			if (base.SchemaCollection != null)
			{
				schemaInfo = base.SchemaCollection.GetSchemaInfo(uri);
			}
			if (schemaInfo == null)
			{
				this.LoadSchemaFromLocation(uri);
				return;
			}
			if (schemaInfo.SchemaType != SchemaType.XDR)
			{
				throw new XmlException("Unsupported combination of validation types.", string.Empty, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
			}
			this.schemaInfo.Add(schemaInfo, base.EventHandler);
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x000BCC6D File Offset: 0x000BAE6D
		private bool HasSchema
		{
			get
			{
				return this.schemaInfo.SchemaType > SchemaType.None;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x000A010D File Offset: 0x0009E30D
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000BCC80 File Offset: 0x000BAE80
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.FindId(name) != null)
				{
					base.SendValidationEvent("'{0}' is already used as an ID.", name);
					return;
				}
				this.AddID(name, this.context.LocalName);
				return;
			case XmlTokenizedType.IDREF:
				if (this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				BaseValidator.ProcessEntity(this.schemaInfo, name, this, base.EventHandler, this.reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000BCD35 File Offset: 0x000BAF35
		public override void CompleteValidation()
		{
			if (this.HasSchema)
			{
				this.CheckForwardRefs();
				return;
			}
			base.SendValidationEvent(new XmlSchemaException("No validation occurred.", string.Empty), XmlSeverityType.Warning);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000BCD5C File Offset: 0x000BAF5C
		private void CheckValue(string value, SchemaAttDef attdef)
		{
			try
			{
				this.reader.TypedValueObject = null;
				bool flag = attdef != null;
				XmlSchemaDatatype xmlSchemaDatatype = (flag ? attdef.Datatype : this.context.ElementDecl.Datatype);
				if (xmlSchemaDatatype != null)
				{
					if (xmlSchemaDatatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						value = value.Trim();
					}
					if (value.Length != 0)
					{
						object obj = xmlSchemaDatatype.ParseValue(value, base.NameTable, this.nsManager);
						this.reader.TypedValueObject = obj;
						XmlTokenizedType tokenizedType = xmlSchemaDatatype.TokenizedType;
						if (tokenizedType == XmlTokenizedType.ENTITY || tokenizedType == XmlTokenizedType.ID || tokenizedType == XmlTokenizedType.IDREF)
						{
							if (xmlSchemaDatatype.Variety == XmlSchemaDatatypeVariety.List)
							{
								string[] array = (string[])obj;
								for (int i = 0; i < array.Length; i++)
								{
									this.ProcessTokenizedType(xmlSchemaDatatype.TokenizedType, array[i]);
								}
							}
							else
							{
								this.ProcessTokenizedType(xmlSchemaDatatype.TokenizedType, (string)obj);
							}
						}
						SchemaDeclBase schemaDeclBase = (flag ? attdef : this.context.ElementDecl);
						if (schemaDeclBase.MaxLength != (long)((ulong)(-1)) && (long)value.Length > schemaDeclBase.MaxLength)
						{
							base.SendValidationEvent("The actual length is greater than the MaxLength value.", value);
						}
						if (schemaDeclBase.MinLength != (long)((ulong)(-1)) && (long)value.Length < schemaDeclBase.MinLength)
						{
							base.SendValidationEvent("The actual length is less than the MinLength value.", value);
						}
						if (schemaDeclBase.Values != null && !schemaDeclBase.CheckEnumeration(obj))
						{
							if (xmlSchemaDatatype.TokenizedType == XmlTokenizedType.NOTATION)
							{
								base.SendValidationEvent("'{0}' is not in the notation list.", obj.ToString());
							}
							else
							{
								base.SendValidationEvent("'{0}' is not in the enumeration list.", obj.ToString());
							}
						}
						if (!schemaDeclBase.CheckValue(obj))
						{
							if (flag)
							{
								base.SendValidationEvent("The value of the '{0}' attribute does not equal its fixed value.", attdef.Name.ToString());
							}
							else
							{
								base.SendValidationEvent("The value of the '{0}' element does not equal its fixed value.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
							}
						}
					}
				}
			}
			catch (XmlSchemaException)
			{
				if (attdef != null)
				{
					base.SendValidationEvent("The '{0}' attribute has an invalid value according to its data type.", attdef.Name.ToString());
				}
				else
				{
					base.SendValidationEvent("The '{0}' element has an invalid value according to its data type.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000BCF88 File Offset: 0x000BB188
		public static void CheckDefaultValue(string value, SchemaAttDef attdef, SchemaInfo sinfo, XmlNamespaceManager nsManager, XmlNameTable NameTable, object sender, ValidationEventHandler eventhandler, string baseUri, int lineNo, int linePos)
		{
			try
			{
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					if (datatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						value = value.Trim();
					}
					if (value.Length != 0)
					{
						object obj = datatype.ParseValue(value, NameTable, nsManager);
						XmlTokenizedType tokenizedType = datatype.TokenizedType;
						if (tokenizedType == XmlTokenizedType.ENTITY)
						{
							if (datatype.Variety == XmlSchemaDatatypeVariety.List)
							{
								string[] array = (string[])obj;
								for (int i = 0; i < array.Length; i++)
								{
									BaseValidator.ProcessEntity(sinfo, array[i], sender, eventhandler, baseUri, lineNo, linePos);
								}
							}
							else
							{
								BaseValidator.ProcessEntity(sinfo, (string)obj, sender, eventhandler, baseUri, lineNo, linePos);
							}
						}
						else if (tokenizedType == XmlTokenizedType.ENUMERATION && !attdef.CheckEnumeration(obj))
						{
							XmlSchemaException ex = new XmlSchemaException("'{0}' is not in the enumeration list.", obj.ToString(), baseUri, lineNo, linePos);
							if (eventhandler == null)
							{
								throw ex;
							}
							eventhandler(sender, new ValidationEventArgs(ex));
						}
						attdef.DefaultValueTyped = obj;
					}
				}
			}
			catch
			{
				XmlSchemaException ex2 = new XmlSchemaException("The default value of '{0}' attribute is invalid according to its datatype.", attdef.Name.ToString(), baseUri, lineNo, linePos);
				if (eventhandler == null)
				{
					throw ex2;
				}
				eventhandler(sender, new ValidationEventArgs(ex2));
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000BD0B8 File Offset: 0x000BB2B8
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000BD0DA File Offset: 0x000BB2DA
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000BD0F4 File Offset: 0x000BB2F4
		private void Push(XmlQualifiedName elementName)
		{
			this.context = (ValidationState)this.validationStack.Push();
			if (this.context == null)
			{
				this.context = new ValidationState();
				this.validationStack.AddToTop(this.context);
			}
			this.context.LocalName = elementName.Name;
			this.context.Namespace = elementName.Namespace;
			this.context.HasMatched = false;
			this.context.IsNill = false;
			this.context.NeedValidateChildren = false;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000BD181 File Offset: 0x000BB381
		private void Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				this.context = (ValidationState)this.validationStack.Peek();
			}
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000BD1B4 File Offset: 0x000BB3B4
		private void CheckForwardRefs()
		{
			IdRefNode next;
			for (IdRefNode idRefNode = this.idRefListHead; idRefNode != null; idRefNode = next)
			{
				if (this.FindId(idRefNode.Id) == null)
				{
					base.SendValidationEvent(new XmlSchemaException("Reference to undeclared ID is '{0}'.", idRefNode.Id, this.reader.BaseURI, idRefNode.LineNo, idRefNode.LinePos));
				}
				next = idRefNode.Next;
				idRefNode.Next = null;
			}
			this.idRefListHead = null;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000BD21D File Offset: 0x000BB41D
		private XmlQualifiedName QualifiedName(string name, string ns)
		{
			return new XmlQualifiedName(name, XmlSchemaDatatype.XdrCanonizeUri(ns, base.NameTable, base.SchemaNames));
		}

		// Token: 0x04000EC1 RID: 3777
		private HWStack validationStack;

		// Token: 0x04000EC2 RID: 3778
		private Hashtable attPresence;

		// Token: 0x04000EC3 RID: 3779
		private XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000EC4 RID: 3780
		private XmlNamespaceManager nsManager;

		// Token: 0x04000EC5 RID: 3781
		private bool isProcessContents;

		// Token: 0x04000EC6 RID: 3782
		private Hashtable IDs;

		// Token: 0x04000EC7 RID: 3783
		private IdRefNode idRefListHead;

		// Token: 0x04000EC8 RID: 3784
		private Parser inlineSchemaParser;
	}
}
