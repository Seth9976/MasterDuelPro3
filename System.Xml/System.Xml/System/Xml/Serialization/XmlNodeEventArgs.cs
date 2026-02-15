using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event.</summary>
	// Token: 0x020001EE RID: 494
	public class XmlNodeEventArgs : EventArgs
	{
		// Token: 0x06001968 RID: 6504 RVA: 0x00096D82 File Offset: 0x00094F82
		internal XmlNodeEventArgs(XmlNode xmlNode, int lineNumber, int linePosition, object o)
		{
			this.o = o;
			this.xmlNode = xmlNode;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the object being deserialized.</summary>
		/// <returns>The <see cref="T:System.Object" /> being deserialized.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x00096DA7 File Offset: 0x00094FA7
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets the type of the XML node being deserialized.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNodeType" /> that represents the XML node being deserialized.</returns>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x00096DAF File Offset: 0x00094FAF
		public XmlNodeType NodeType
		{
			get
			{
				return this.xmlNode.NodeType;
			}
		}

		/// <summary>Gets the name of the XML node being deserialized.</summary>
		/// <returns>The name of the node being deserialized.</returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x00096DBC File Offset: 0x00094FBC
		public string Name
		{
			get
			{
				return this.xmlNode.Name;
			}
		}

		/// <summary>Gets the XML local name of the XML node being deserialized.</summary>
		/// <returns>The XML local name of the node being deserialized.</returns>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x00096DC9 File Offset: 0x00094FC9
		public string LocalName
		{
			get
			{
				return this.xmlNode.LocalName;
			}
		}

		/// <summary>Gets the namespace URI that is associated with the XML node being deserialized.</summary>
		/// <returns>The namespace URI that is associated with the XML node being deserialized.</returns>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x00096DD6 File Offset: 0x00094FD6
		public string NamespaceURI
		{
			get
			{
				return this.xmlNode.NamespaceURI;
			}
		}

		/// <summary>Gets the text of the XML node being deserialized.</summary>
		/// <returns>The text of the XML node being deserialized.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00096DE3 File Offset: 0x00094FE3
		public string Text
		{
			get
			{
				return this.xmlNode.Value;
			}
		}

		/// <summary>Gets the line number of the unknown XML node.</summary>
		/// <returns>The line number of the unknown XML node.</returns>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00096DF0 File Offset: 0x00094FF0
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the position in the line of the unknown XML node.</summary>
		/// <returns>The position number of the unknown XML node.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00096DF8 File Offset: 0x00094FF8
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal XmlNodeEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AB7 RID: 2743
		private object o;

		// Token: 0x04000AB8 RID: 2744
		private XmlNode xmlNode;

		// Token: 0x04000AB9 RID: 2745
		private int lineNumber;

		// Token: 0x04000ABA RID: 2746
		private int linePosition;
	}
}
