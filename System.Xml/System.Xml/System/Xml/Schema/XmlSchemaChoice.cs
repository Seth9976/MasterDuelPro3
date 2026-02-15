using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the choice element (compositor) from the XML Schema as specified by the World Wide Web Consortium (W3C). The choice allows only one of its children to appear in an instance. </summary>
	// Token: 0x020002BE RID: 702
	public class XmlSchemaChoice : XmlSchemaGroupBase
	{
		/// <summary>Gets the collection of the elements contained with the compositor (choice): XmlSchemaElement, XmlSchemaGroupRef, XmlSchemaChoice, XmlSchemaSequence, or XmlSchemaAny.</summary>
		/// <returns>The collection of elements contained within XmlSchemaChoice.</returns>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x000BE78D File Offset: 0x000BC98D
		[XmlElement("sequence", typeof(XmlSchemaSequence))]
		[XmlElement("any", typeof(XmlSchemaAny))]
		[XmlElement("group", typeof(XmlSchemaGroupRef))]
		[XmlElement("element", typeof(XmlSchemaElement))]
		[XmlElement("choice", typeof(XmlSchemaChoice))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x000BE795 File Offset: 0x000BC995
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000BE79D File Offset: 0x000BC99D
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04000F16 RID: 3862
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
