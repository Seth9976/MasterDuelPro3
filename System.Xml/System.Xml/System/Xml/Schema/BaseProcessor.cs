using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020D RID: 525
	internal class BaseProcessor
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x00098B7F File Offset: 0x00096D7F
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler)
			: this(nameTable, schemaNames, eventHandler, new XmlSchemaCompilationSettings())
		{
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00098B8F File Offset: 0x00096D8F
		public BaseProcessor(XmlNameTable nameTable, SchemaNames schemaNames, ValidationEventHandler eventHandler, XmlSchemaCompilationSettings compilationSettings)
		{
			this.nameTable = nameTable;
			this.schemaNames = schemaNames;
			this.eventHandler = eventHandler;
			this.compilationSettings = compilationSettings;
			this.NsXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00098BC5 File Offset: 0x00096DC5
		protected XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00098BCD File Offset: 0x00096DCD
		protected SchemaNames SchemaNames
		{
			get
			{
				if (this.schemaNames == null)
				{
					this.schemaNames = new SchemaNames(this.nameTable);
				}
				return this.schemaNames;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00098BEE File Offset: 0x00096DEE
		protected ValidationEventHandler EventHandler
		{
			get
			{
				return this.eventHandler;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00098BF6 File Offset: 0x00096DF6
		protected XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.compilationSettings;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00098BFE File Offset: 0x00096DFE
		protected bool HasErrors
		{
			get
			{
				return this.errorCount != 0;
			}
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x00098C0C File Offset: 0x00096E0C
		protected void AddToTable(XmlSchemaObjectTable table, XmlQualifiedName qname, XmlSchemaObject item)
		{
			if (qname.Name.Length == 0)
			{
				return;
			}
			XmlSchemaObject xmlSchemaObject = table[qname];
			if (xmlSchemaObject == null)
			{
				table.Add(qname, item);
				return;
			}
			if (xmlSchemaObject == item)
			{
				return;
			}
			string text = "The global element '{0}' has already been declared.";
			if (item is XmlSchemaAttributeGroup)
			{
				if (Ref.Equal(this.nameTable.Add(qname.Namespace), this.NsXml))
				{
					XmlSchemaObject xmlSchemaObject2 = Preprocessor.GetBuildInSchema().AttributeGroups[qname];
					if (xmlSchemaObject == xmlSchemaObject2)
					{
						table.Insert(qname, item);
						return;
					}
					if (item == xmlSchemaObject2)
					{
						return;
					}
				}
				else if (this.IsValidAttributeGroupRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				text = "The attributeGroup '{0}' has already been declared.";
			}
			else if (item is XmlSchemaAttribute)
			{
				if (Ref.Equal(this.nameTable.Add(qname.Namespace), this.NsXml))
				{
					XmlSchemaObject xmlSchemaObject3 = Preprocessor.GetBuildInSchema().Attributes[qname];
					if (xmlSchemaObject == xmlSchemaObject3)
					{
						table.Insert(qname, item);
						return;
					}
					if (item == xmlSchemaObject3)
					{
						return;
					}
				}
				text = "The global attribute '{0}' has already been declared.";
			}
			else if (item is XmlSchemaSimpleType)
			{
				if (this.IsValidTypeRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				text = "The simpleType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaComplexType)
			{
				if (this.IsValidTypeRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				text = "The complexType '{0}' has already been declared.";
			}
			else if (item is XmlSchemaGroup)
			{
				if (this.IsValidGroupRedefine(xmlSchemaObject, item, table))
				{
					return;
				}
				text = "The group '{0}' has already been declared.";
			}
			else if (item is XmlSchemaNotation)
			{
				text = "The notation '{0}' has already been declared.";
			}
			else if (item is XmlSchemaIdentityConstraint)
			{
				text = "The identity constraint '{0}' has already been declared.";
			}
			this.SendValidationEvent(text, qname.ToString(), item);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00098D7C File Offset: 0x00096F7C
		private bool IsValidAttributeGroupRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup = item as XmlSchemaAttributeGroup;
			XmlSchemaAttributeGroup xmlSchemaAttributeGroup2 = existingObject as XmlSchemaAttributeGroup;
			if (xmlSchemaAttributeGroup2 == xmlSchemaAttributeGroup.Redefined)
			{
				if (xmlSchemaAttributeGroup2.AttributeUses.Count == 0)
				{
					table.Insert(xmlSchemaAttributeGroup.QualifiedName, xmlSchemaAttributeGroup);
					return true;
				}
			}
			else if (xmlSchemaAttributeGroup2.Redefined == xmlSchemaAttributeGroup)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00098DC8 File Offset: 0x00096FC8
		private bool IsValidGroupRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaGroup xmlSchemaGroup = item as XmlSchemaGroup;
			XmlSchemaGroup xmlSchemaGroup2 = existingObject as XmlSchemaGroup;
			if (xmlSchemaGroup2 == xmlSchemaGroup.Redefined)
			{
				if (xmlSchemaGroup2.CanonicalParticle == null)
				{
					table.Insert(xmlSchemaGroup.QualifiedName, xmlSchemaGroup);
					return true;
				}
			}
			else if (xmlSchemaGroup2.Redefined == xmlSchemaGroup)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00098E10 File Offset: 0x00097010
		private bool IsValidTypeRedefine(XmlSchemaObject existingObject, XmlSchemaObject item, XmlSchemaObjectTable table)
		{
			XmlSchemaType xmlSchemaType = item as XmlSchemaType;
			XmlSchemaType xmlSchemaType2 = existingObject as XmlSchemaType;
			if (xmlSchemaType2 == xmlSchemaType.Redefined)
			{
				if (xmlSchemaType2.ElementDecl == null)
				{
					table.Insert(xmlSchemaType.QualifiedName, xmlSchemaType);
					return true;
				}
			}
			else if (xmlSchemaType2.Redefined == xmlSchemaType)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00098E57 File Offset: 0x00097057
		protected void SendValidationEvent(string code, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), XmlSeverityType.Error);
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00098E67 File Offset: 0x00097067
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), XmlSeverityType.Error);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00098E78 File Offset: 0x00097078
		protected void SendValidationEvent(string code, string msg1, string msg2, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[] { msg1, msg2 }, source), XmlSeverityType.Error);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00098E97 File Offset: 0x00097097
		protected void SendValidationEvent(string code, string[] args, Exception innerException, XmlSchemaObject source)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, innerException, source.SourceUri, source.LineNumber, source.LinePosition, source), XmlSeverityType.Error);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00098EBF File Offset: 0x000970BF
		protected void SendValidationEvent(string code, string msg1, string msg2, string sourceUri, int lineNumber, int linePosition)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[] { msg1, msg2 }, sourceUri, lineNumber, linePosition), XmlSeverityType.Error);
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00098EE2 File Offset: 0x000970E2
		protected void SendValidationEvent(string code, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, source), severity);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00098EF2 File Offset: 0x000970F2
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00098EFC File Offset: 0x000970FC
		protected void SendValidationEvent(string code, string msg, XmlSchemaObject source, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, source), severity);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00098F0E File Offset: 0x0009710E
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (severity == XmlSeverityType.Error)
			{
				this.errorCount++;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(null, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00098F42 File Offset: 0x00097142
		protected void SendValidationEventNoThrow(XmlSchemaException e, XmlSeverityType severity)
		{
			if (severity == XmlSeverityType.Error)
			{
				this.errorCount++;
			}
			if (this.eventHandler != null)
			{
				this.eventHandler(null, new ValidationEventArgs(e, severity));
			}
		}

		// Token: 0x04000B1B RID: 2843
		private XmlNameTable nameTable;

		// Token: 0x04000B1C RID: 2844
		private SchemaNames schemaNames;

		// Token: 0x04000B1D RID: 2845
		private ValidationEventHandler eventHandler;

		// Token: 0x04000B1E RID: 2846
		private XmlSchemaCompilationSettings compilationSettings;

		// Token: 0x04000B1F RID: 2847
		private int errorCount;

		// Token: 0x04000B20 RID: 2848
		private string NsXml;
	}
}
