using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>An abstract class for <see cref="T:System.Xml.Schema.XmlSchemaAll" />, <see cref="T:System.Xml.Schema.XmlSchemaChoice" />, or <see cref="T:System.Xml.Schema.XmlSchemaSequence" />.</summary>
	// Token: 0x020002E2 RID: 738
	public abstract class XmlSchemaGroupBase : XmlSchemaParticle
	{
		/// <summary>This collection is used to add new elements to the compositor.</summary>
		/// <returns>An XmlSchemaObjectCollection.</returns>
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002151 RID: 8529
		[XmlIgnore]
		public abstract XmlSchemaObjectCollection Items { get; }

		// Token: 0x06002152 RID: 8530
		internal abstract void SetItems(XmlSchemaObjectCollection newItems);
	}
}
