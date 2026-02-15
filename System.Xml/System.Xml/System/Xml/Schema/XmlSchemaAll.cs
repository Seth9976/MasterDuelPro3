using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the World Wide Web Consortium (W3C) all element (compositor).</summary>
	// Token: 0x020002B5 RID: 693
	public class XmlSchemaAll : XmlSchemaGroupBase
	{
		/// <summary>Gets the collection of XmlSchemaElement elements contained within the all compositor.</summary>
		/// <returns>The collection of elements contained in XmlSchemaAll.</returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x000BE108 File Offset: 0x000BC308
		[XmlElement("element", typeof(XmlSchemaElement))]
		public override XmlSchemaObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x000BE110 File Offset: 0x000BC310
		internal override bool IsEmpty
		{
			get
			{
				return base.IsEmpty || this.items.Count == 0;
			}
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000BE12A File Offset: 0x000BC32A
		internal override void SetItems(XmlSchemaObjectCollection newItems)
		{
			this.items = newItems;
		}

		// Token: 0x04000EF3 RID: 3827
		private XmlSchemaObjectCollection items = new XmlSchemaObjectCollection();
	}
}
