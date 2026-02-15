using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event.</summary>
	// Token: 0x020001EC RID: 492
	public class XmlElementEventArgs : EventArgs
	{
		// Token: 0x0600195D RID: 6493 RVA: 0x00096D1F File Offset: 0x00094F1F
		internal XmlElementEventArgs(XmlElement elem, int lineNumber, int linePosition, object o, string qnames)
		{
			this.elem = elem;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the object the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is deserializing.</summary>
		/// <returns>The object that is being deserialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00096D4C File Offset: 0x00094F4C
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets the object that represents the unknown XML element.</summary>
		/// <returns>The object that represents the unknown XML element.</returns>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00096D54 File Offset: 0x00094F54
		public XmlElement Element
		{
			get
			{
				return this.elem;
			}
		}

		/// <summary>Gets the line number where the unknown element was encountered if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />.</summary>
		/// <returns>The line number where the unknown element was encountered if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />; otherwise, -1.</returns>
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x00096D5C File Offset: 0x00094F5C
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the place in the line where the unknown element occurs if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />.</summary>
		/// <returns>The number in the line where the unknown element occurs if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />; otherwise, -1.</returns>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00096D64 File Offset: 0x00094F64
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets a comma-delimited list of XML element names expected to be in an XML document instance.</summary>
		/// <returns>A comma-delimited list of XML element names. Each name is in the following format: <paramref name="namespace" />:<paramref name="name" />.</returns>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x00096D6C File Offset: 0x00094F6C
		public string ExpectedElements
		{
			get
			{
				if (this.qnames != null)
				{
					return this.qnames;
				}
				return string.Empty;
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal XmlElementEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AB2 RID: 2738
		private object o;

		// Token: 0x04000AB3 RID: 2739
		private XmlElement elem;

		// Token: 0x04000AB4 RID: 2740
		private string qnames;

		// Token: 0x04000AB5 RID: 2741
		private int lineNumber;

		// Token: 0x04000AB6 RID: 2742
		private int linePosition;
	}
}
