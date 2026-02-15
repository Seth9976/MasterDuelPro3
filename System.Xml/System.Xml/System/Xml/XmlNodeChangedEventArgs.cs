using System;

namespace System.Xml
{
	/// <summary>Provides data for the <see cref="E:System.Xml.XmlDocument.NodeChanged" />, <see cref="E:System.Xml.XmlDocument.NodeChanging" />, <see cref="E:System.Xml.XmlDocument.NodeInserted" />, <see cref="E:System.Xml.XmlDocument.NodeInserting" />, <see cref="E:System.Xml.XmlDocument.NodeRemoved" /> and <see cref="E:System.Xml.XmlDocument.NodeRemoving" /> events.</summary>
	// Token: 0x020000F2 RID: 242
	public class XmlNodeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNodeChangedEventArgs" /> class.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="oldParent">The old parent <see cref="T:System.Xml.XmlNode" /> of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="newParent">The new parent <see cref="T:System.Xml.XmlNode" /> of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="oldValue">The old value of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="newValue">The new value of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="action">The <see cref="T:System.Xml.XmlNodeChangedAction" />.</param>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x00040C26 File Offset: 0x0003EE26
		public XmlNodeChangedEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this.node = node;
			this.oldParent = oldParent;
			this.newParent = newParent;
			this.action = action;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		/// <summary>Gets a value indicating what type of node change event is occurring.</summary>
		/// <returns>An XmlNodeChangedAction value describing the node change event.XmlNodeChangedAction Value Description Insert A node has been or will be inserted. Remove A node has been or will be removed. Change A node has been or will be changed. NoteThe Action value does not differentiate between when the event occurred (before or after). You can create separate event handlers to handle both instances.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00040C5B File Offset: 0x0003EE5B
		public XmlNodeChangedAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x0400065D RID: 1629
		private XmlNodeChangedAction action;

		// Token: 0x0400065E RID: 1630
		private XmlNode node;

		// Token: 0x0400065F RID: 1631
		private XmlNode oldParent;

		// Token: 0x04000660 RID: 1632
		private XmlNode newParent;

		// Token: 0x04000661 RID: 1633
		private string oldValue;

		// Token: 0x04000662 RID: 1634
		private string newValue;
	}
}
