using System;

namespace System.Xml.Serialization
{
	/// <summary>Contains fields that can be used to pass event delegates to a thread-safe <see cref="Overload:System.Xml.Serialization.XmlSerializer.Deserialize" /> method of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	// Token: 0x020001DD RID: 477
	public struct XmlDeserializationEvents
	{
		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlNodeEventHandler" /> that points to the event handler.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x00094BA1 File Offset: 0x00092DA1
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x00094BA9 File Offset: 0x00092DA9
		public XmlNodeEventHandler OnUnknownNode
		{
			get
			{
				return this.onUnknownNode;
			}
			set
			{
				this.onUnknownNode = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributeEventHandler" /> that points to the event handler.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00094BB2 File Offset: 0x00092DB2
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x00094BBA File Offset: 0x00092DBA
		public XmlAttributeEventHandler OnUnknownAttribute
		{
			get
			{
				return this.onUnknownAttribute;
			}
			set
			{
				this.onUnknownAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlElementEventHandler" /> that points to the event handler.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00094BC3 File Offset: 0x00092DC3
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x00094BCB File Offset: 0x00092DCB
		public XmlElementEventHandler OnUnknownElement
		{
			get
			{
				return this.onUnknownElement;
			}
			set
			{
				this.onUnknownElement = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnreferencedObject" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.UnreferencedObjectEventHandler" /> that points to the event handler.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x00094BD4 File Offset: 0x00092DD4
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x00094BDC File Offset: 0x00092DDC
		public UnreferencedObjectEventHandler OnUnreferencedObject
		{
			get
			{
				return this.onUnreferencedObject;
			}
			set
			{
				this.onUnreferencedObject = value;
			}
		}

		// Token: 0x04000A8D RID: 2701
		private XmlNodeEventHandler onUnknownNode;

		// Token: 0x04000A8E RID: 2702
		private XmlAttributeEventHandler onUnknownAttribute;

		// Token: 0x04000A8F RID: 2703
		private XmlElementEventHandler onUnknownElement;

		// Token: 0x04000A90 RID: 2704
		private UnreferencedObjectEventHandler onUnreferencedObject;

		// Token: 0x04000A91 RID: 2705
		internal object sender;
	}
}
