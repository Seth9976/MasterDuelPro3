using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the sequence element (compositor) from the XML Schema as specified by the World Wide Web Consortium (W3C). The sequence requires the elements in the group to appear in the specified sequence within the containing element.</summary>
	// Token: 0x020002FA RID: 762
	public class XmlSchemaSequence : XmlSchemaGroupBase
	{
		/// <summary>The elements contained within the compositor. Collection of <see cref="T:System.Xml.Schema.XmlSchemaElement" />, <see cref="T:System.Xml.Schema.XmlSchemaGroupRef" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, <see cref="T:System.Xml.Schema.XmlSchemaSequence" />, or <see cref="T:System.Xml.Schema.XmlSchemaAny" />.</summary>
		/// <returns>The elements contained within the compositor.</returns>
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x000C10C1 File Offset: 0x000BF2C1
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000C10C9 File Offset: 0x000BF2C9
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000C10E3 File Offset: 0x000BF2E3
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04000FCE RID: 4046
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
