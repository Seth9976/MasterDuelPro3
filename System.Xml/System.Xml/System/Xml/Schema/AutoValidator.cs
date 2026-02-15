using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020C RID: 524
	internal class AutoValidator : BaseValidator
	{
		// Token: 0x06001A10 RID: 6672 RVA: 0x00098941 File Offset: 0x00096B41
		public AutoValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
			: base(reader, schemaCollection, eventHandling)
		{
			this.schemaInfo = new SchemaInfo();
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00098958 File Offset: 0x00096B58
		public override void Validate()
		{
			switch (this.DetectValidationType())
			{
			case ValidationType.Auto:
			case ValidationType.DTD:
				break;
			case ValidationType.XDR:
				this.reader.Validator = new XdrValidator(this);
				this.reader.Validator.Validate();
				return;
			case ValidationType.Schema:
				this.reader.Validator = new XsdValidator(this);
				this.reader.Validator.Validate();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0000A558 File Offset: 0x00008758
		public override void CompleteValidation()
		{
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override object FindId(string name)
		{
			return null;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000989C8 File Offset: 0x00096BC8
		private ValidationType DetectValidationType()
		{
			if (this.reader.Schemas != null && this.reader.Schemas.Count > 0)
			{
				XmlSchemaCollectionEnumerator enumerator = this.reader.Schemas.GetEnumerator();
				while (enumerator.MoveNext())
				{
					SchemaInfo schemaInfo = enumerator.CurrentNode.SchemaInfo;
					if (schemaInfo.SchemaType == SchemaType.XSD)
					{
						return ValidationType.Schema;
					}
					if (schemaInfo.SchemaType == SchemaType.XDR)
					{
						return ValidationType.XDR;
					}
				}
			}
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				SchemaType schemaType = base.SchemaNames.SchemaTypeFromRoot(this.reader.LocalName, this.reader.NamespaceURI);
				if (schemaType == SchemaType.XSD)
				{
					return ValidationType.Schema;
				}
				if (schemaType == SchemaType.XDR)
				{
					return ValidationType.XDR;
				}
				int attributeCount = this.reader.AttributeCount;
				for (int i = 0; i < attributeCount; i++)
				{
					this.reader.MoveToAttribute(i);
					string namespaceURI = this.reader.NamespaceURI;
					string localName = this.reader.LocalName;
					if (Ref.Equal(namespaceURI, base.SchemaNames.NsXmlNs))
					{
						if (XdrBuilder.IsXdrSchema(this.reader.Value))
						{
							this.reader.MoveToElement();
							return ValidationType.XDR;
						}
					}
					else
					{
						if (Ref.Equal(namespaceURI, base.SchemaNames.NsXsi))
						{
							this.reader.MoveToElement();
							return ValidationType.Schema;
						}
						if (Ref.Equal(namespaceURI, base.SchemaNames.QnDtDt.Namespace) && Ref.Equal(localName, base.SchemaNames.QnDtDt.Name))
						{
							this.reader.SchemaTypeObject = XmlSchemaDatatype.FromXdrName(this.reader.Value);
							this.reader.MoveToElement();
							return ValidationType.XDR;
						}
					}
				}
				if (attributeCount > 0)
				{
					this.reader.MoveToElement();
				}
			}
			return ValidationType.Auto;
		}
	}
}
