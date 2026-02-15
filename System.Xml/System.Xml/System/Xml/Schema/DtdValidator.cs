using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000275 RID: 629
	internal sealed class DtdValidator : BaseValidator
	{
		// Token: 0x06001CA5 RID: 7333 RVA: 0x0009F9E5 File Offset: 0x0009DBE5
		internal DtdValidator(XmlValidatingReaderImpl reader, IValidationEventHandling eventHandling, bool processIdentityConstraints)
			: base(reader, null, eventHandling)
		{
			this.processIdentityConstraints = processIdentityConstraints;
			this.Init();
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0009FA08 File Offset: 0x0009DC08
		private void Init()
		{
			this.validationStack = new HWStack(10);
			this.textValue = new StringBuilder();
			this.name = XmlQualifiedName.Empty;
			this.attPresence = new Hashtable();
			this.schemaInfo = new SchemaInfo();
			this.checkDatatype = false;
			this.Push(this.name);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0009FA64 File Offset: 0x0009DC64
		public override void Validate()
		{
			if (this.schemaInfo.SchemaType == SchemaType.DTD)
			{
				switch (this.reader.NodeType)
				{
				case XmlNodeType.Element:
					this.ValidateElement();
					if (!this.reader.IsEmptyElement)
					{
						return;
					}
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
					return;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					base.ValidateText();
					return;
				case XmlNodeType.EntityReference:
					if (!this.GenEntity(new XmlQualifiedName(this.reader.LocalName, this.reader.Prefix)))
					{
						base.ValidateText();
						return;
					}
					return;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
					this.ValidatePIComment();
					return;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (this.MeetsStandAloneConstraint())
					{
						base.ValidateWhitespace();
						return;
					}
					return;
				case XmlNodeType.EndElement:
					break;
				default:
					return;
				}
				this.ValidateEndElement();
				return;
			}
			if (this.reader.Depth == 0 && this.reader.NodeType == XmlNodeType.Element)
			{
				base.SendValidationEvent("No DTD found.", this.name.ToString(), XmlSeverityType.Warning);
			}
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0009FB6C File Offset: 0x0009DD6C
		private bool MeetsStandAloneConstraint()
		{
			if (this.reader.StandAlone && this.context.ElementDecl != null && this.context.ElementDecl.IsDeclaredInExternal && this.context.ElementDecl.ContentValidator.ContentType == XmlSchemaContentType.ElementOnly)
			{
				base.SendValidationEvent("The standalone document declaration must have a value of 'no'.");
				return false;
			}
			return true;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0009FBCB File Offset: 0x0009DDCB
		private void ValidatePIComment()
		{
			if (this.context.NeedValidateChildren && this.context.ElementDecl.ContentValidator == ContentValidator.Empty)
			{
				base.SendValidationEvent("The element cannot contain comment or processing instruction. Content model is empty.");
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x0009FBFC File Offset: 0x0009DDFC
		private void ValidateElement()
		{
			this.elementName.Init(this.reader.LocalName, this.reader.Prefix);
			if (this.reader.Depth == 0 && !this.schemaInfo.DocTypeName.IsEmpty && !this.schemaInfo.DocTypeName.Equals(this.elementName))
			{
				base.SendValidationEvent("Root element name must match the DocType name.");
			}
			else
			{
				this.ValidateChildElement();
			}
			this.ProcessElement();
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0009FC7C File Offset: 0x0009DE7C
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

		// Token: 0x06001CAC RID: 7340 RVA: 0x0009FD00 File Offset: 0x0009DF00
		private void ValidateStartElement()
		{
			if (this.context.ElementDecl != null)
			{
				base.Reader.SchemaTypeObject = this.context.ElementDecl.SchemaType;
				if (base.Reader.IsEmptyElement && this.context.ElementDecl.DefaultValueTyped != null)
				{
					base.Reader.TypedValueObject = this.context.ElementDecl.DefaultValueTyped;
					this.context.IsNill = true;
				}
				if (this.context.ElementDecl.HasRequiredAttribute)
				{
					this.attPresence.Clear();
				}
			}
			if (base.Reader.MoveToFirstAttribute())
			{
				do
				{
					try
					{
						this.reader.SchemaTypeObject = null;
						SchemaAttDef attDef = this.context.ElementDecl.GetAttDef(new XmlQualifiedName(this.reader.LocalName, this.reader.Prefix));
						if (attDef != null)
						{
							if (this.context.ElementDecl != null && this.context.ElementDecl.HasRequiredAttribute)
							{
								this.attPresence.Add(attDef.Name, attDef);
							}
							base.Reader.SchemaTypeObject = attDef.SchemaType;
							if (attDef.Datatype != null && !this.reader.IsDefault)
							{
								this.CheckValue(base.Reader.Value, attDef);
							}
						}
						else
						{
							base.SendValidationEvent("The '{0}' attribute is not declared.", this.reader.Name);
						}
					}
					catch (XmlSchemaException ex)
					{
						ex.SetSource(base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
						base.SendValidationEvent(ex);
					}
				}
				while (base.Reader.MoveToNextAttribute());
				base.Reader.MoveToElement();
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0009FEC4 File Offset: 0x0009E0C4
		private void ValidateEndStartElement()
		{
			if (this.context.ElementDecl.HasRequiredAttribute)
			{
				try
				{
					this.context.ElementDecl.CheckAttributes(this.attPresence, base.Reader.StandAlone);
				}
				catch (XmlSchemaException ex)
				{
					ex.SetSource(base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
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

		// Token: 0x06001CAE RID: 7342 RVA: 0x0009FF80 File Offset: 0x0009E180
		private void ProcessElement()
		{
			SchemaElementDecl elementDecl = this.schemaInfo.GetElementDecl(this.elementName);
			this.Push(this.elementName);
			if (elementDecl != null)
			{
				this.context.ElementDecl = elementDecl;
				this.ValidateStartElement();
				this.ValidateEndStartElement();
				this.context.NeedValidateChildren = true;
				elementDecl.ContentValidator.InitValidation(this.context);
				return;
			}
			base.SendValidationEvent("The '{0}' element is not declared.", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
			this.context.ElementDecl = null;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000A0016 File Offset: 0x0009E216
		public override void CompleteValidation()
		{
			if (this.schemaInfo.SchemaType == SchemaType.DTD)
			{
				do
				{
					this.ValidateEndElement();
				}
				while (this.Pop());
				this.CheckForwardRefs();
			}
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x000A003C File Offset: 0x0009E23C
		private void ValidateEndElement()
		{
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

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x000A010D File Offset: 0x0009E30D
		public override bool PreserveWhitespace
		{
			get
			{
				return this.context.ElementDecl != null && this.context.ElementDecl.ContentValidator.PreserveWhitespace;
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000A0134 File Offset: 0x0009E334
		private void ProcessTokenizedType(XmlTokenizedType ttype, string name)
		{
			switch (ttype)
			{
			case XmlTokenizedType.ID:
				if (this.processIdentityConstraints)
				{
					if (this.FindId(name) != null)
					{
						base.SendValidationEvent("'{0}' is already used as an ID.", name);
						return;
					}
					this.AddID(name, this.context.LocalName);
					return;
				}
				break;
			case XmlTokenizedType.IDREF:
				if (this.processIdentityConstraints && this.FindId(name) == null)
				{
					this.idRefListHead = new IdRefNode(this.idRefListHead, name, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
					return;
				}
				break;
			case XmlTokenizedType.IDREFS:
				break;
			case XmlTokenizedType.ENTITY:
				BaseValidator.ProcessEntity(this.schemaInfo, name, this, base.EventHandler, base.Reader.BaseURI, base.PositionInfo.LineNumber, base.PositionInfo.LinePosition);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000A01FC File Offset: 0x0009E3FC
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
					object obj = xmlSchemaDatatype.ParseValue(value, base.NameTable, DtdValidator.namespaceManager);
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

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000A03CC File Offset: 0x0009E5CC
		internal void AddID(string name, object node)
		{
			if (this.IDs == null)
			{
				this.IDs = new Hashtable();
			}
			this.IDs.Add(name, node);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000A03EE File Offset: 0x0009E5EE
		public override object FindId(string name)
		{
			if (this.IDs != null)
			{
				return this.IDs[name];
			}
			return null;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000A0408 File Offset: 0x0009E608
		private bool GenEntity(XmlQualifiedName qname)
		{
			string text = qname.Name;
			if (text[0] == '#')
			{
				return false;
			}
			if (SchemaEntity.IsPredefinedEntity(text))
			{
				return false;
			}
			SchemaEntity entity = this.GetEntity(qname, false);
			if (entity == null)
			{
				throw new XmlException("Reference to undeclared entity '{0}'.", text);
			}
			if (!entity.NData.IsEmpty)
			{
				throw new XmlException("Reference to unparsed entity '{0}'.", text);
			}
			if (this.reader.StandAlone && entity.DeclaredInExternal)
			{
				base.SendValidationEvent("The standalone document declaration must have a value of 'no'.");
			}
			return true;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000A0488 File Offset: 0x0009E688
		private SchemaEntity GetEntity(XmlQualifiedName qname, bool fParameterEntity)
		{
			SchemaEntity schemaEntity;
			if (fParameterEntity)
			{
				if (this.schemaInfo.ParameterEntities.TryGetValue(qname, out schemaEntity))
				{
					return schemaEntity;
				}
			}
			else if (this.schemaInfo.GeneralEntities.TryGetValue(qname, out schemaEntity))
			{
				return schemaEntity;
			}
			return null;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000A04C8 File Offset: 0x0009E6C8
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

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000A0534 File Offset: 0x0009E734
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

		// Token: 0x06001CBA RID: 7354 RVA: 0x000A05C1 File Offset: 0x0009E7C1
		private bool Pop()
		{
			if (this.validationStack.Length > 1)
			{
				this.validationStack.Pop();
				this.context = (ValidationState)this.validationStack.Peek();
				return true;
			}
			return false;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000A05F8 File Offset: 0x0009E7F8
		public static void SetDefaultTypedValue(SchemaAttDef attdef, IDtdParserAdapter readerAdapter)
		{
			try
			{
				string text = attdef.DefaultValueExpanded;
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					if (datatype.TokenizedType != XmlTokenizedType.CDATA)
					{
						text = text.Trim();
					}
					attdef.DefaultValueTyped = datatype.ParseValue(text, readerAdapter.NameTable, readerAdapter.NamespaceResolver);
				}
			}
			catch (Exception)
			{
				IValidationEventHandling validationEventHandling = ((IDtdParserAdapterWithValidation)readerAdapter).ValidationEventHandling;
				if (validationEventHandling != null)
				{
					XmlSchemaException ex = new XmlSchemaException("The default value of '{0}' attribute is invalid according to its datatype.", attdef.Name.ToString());
					validationEventHandling.SendEvent(ex, XmlSeverityType.Error);
				}
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000A0684 File Offset: 0x0009E884
		public static void CheckDefaultValue(SchemaAttDef attdef, SchemaInfo sinfo, IValidationEventHandling eventHandling, string baseUriStr)
		{
			try
			{
				if (baseUriStr == null)
				{
					baseUriStr = string.Empty;
				}
				XmlSchemaDatatype datatype = attdef.Datatype;
				if (datatype != null)
				{
					object defaultValueTyped = attdef.DefaultValueTyped;
					XmlTokenizedType tokenizedType = datatype.TokenizedType;
					if (tokenizedType == XmlTokenizedType.ENTITY)
					{
						if (datatype.Variety == XmlSchemaDatatypeVariety.List)
						{
							string[] array = (string[])defaultValueTyped;
							for (int i = 0; i < array.Length; i++)
							{
								BaseValidator.ProcessEntity(sinfo, array[i], eventHandling, baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
							}
						}
						else
						{
							BaseValidator.ProcessEntity(sinfo, (string)defaultValueTyped, eventHandling, baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
						}
					}
					else if (tokenizedType == XmlTokenizedType.ENUMERATION && !attdef.CheckEnumeration(defaultValueTyped) && eventHandling != null)
					{
						XmlSchemaException ex = new XmlSchemaException("'{0}' is not in the enumeration list.", defaultValueTyped.ToString(), baseUriStr, attdef.ValueLineNumber, attdef.ValueLinePosition);
						eventHandling.SendEvent(ex, XmlSeverityType.Error);
					}
				}
			}
			catch (Exception)
			{
				if (eventHandling != null)
				{
					XmlSchemaException ex2 = new XmlSchemaException("The default value of '{0}' attribute is invalid according to its datatype.", attdef.Name.ToString());
					eventHandling.SendEvent(ex2, XmlSeverityType.Error);
				}
			}
		}

		// Token: 0x04000C5A RID: 3162
		private static DtdValidator.NamespaceManager namespaceManager = new DtdValidator.NamespaceManager();

		// Token: 0x04000C5B RID: 3163
		private HWStack validationStack;

		// Token: 0x04000C5C RID: 3164
		private Hashtable attPresence;

		// Token: 0x04000C5D RID: 3165
		private XmlQualifiedName name = XmlQualifiedName.Empty;

		// Token: 0x04000C5E RID: 3166
		private Hashtable IDs;

		// Token: 0x04000C5F RID: 3167
		private IdRefNode idRefListHead;

		// Token: 0x04000C60 RID: 3168
		private bool processIdentityConstraints;

		// Token: 0x02000276 RID: 630
		private class NamespaceManager : XmlNamespaceManager
		{
			// Token: 0x06001CBE RID: 7358 RVA: 0x000A0794 File Offset: 0x0009E994
			public override string LookupNamespace(string prefix)
			{
				return prefix;
			}
		}
	}
}
