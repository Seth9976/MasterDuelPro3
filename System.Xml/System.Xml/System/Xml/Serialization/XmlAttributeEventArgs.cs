using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event.</summary>
	// Token: 0x020001EA RID: 490
	public class XmlAttributeEventArgs : EventArgs
	{
		// Token: 0x06001952 RID: 6482 RVA: 0x00096CBC File Offset: 0x00094EBC
		internal XmlAttributeEventArgs(XmlAttribute attr, int lineNumber, int linePosition, object o, string qnames)
		{
			this.attr = attr;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the object being deserialized.</summary>
		/// <returns>The object being deserialized.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00096CE9 File Offset: 0x00094EE9
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets an object that represents the unknown XML attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlAttribute" /> that represents the unknown XML attribute.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00096CF1 File Offset: 0x00094EF1
		public XmlAttribute Attr
		{
			get
			{
				return this.attr;
			}
		}

		/// <summary>Gets the line number of the unknown XML attribute.</summary>
		/// <returns>The line number of the unknown XML attribute.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00096CF9 File Offset: 0x00094EF9
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the position in the line of the unknown XML attribute.</summary>
		/// <returns>The position number of the unknown XML attribute.</returns>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00096D01 File Offset: 0x00094F01
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets a comma-delimited list of XML attribute names expected to be in an XML document instance.</summary>
		/// <returns>A comma-delimited list of XML attribute names. Each name is in the following format: <paramref name="namespace" />:<paramref name="name" />.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00096D09 File Offset: 0x00094F09
		public string ExpectedAttributes
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

		// Token: 0x06001958 RID: 6488 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal XmlAttributeEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000AAD RID: 2733
		private object o;

		// Token: 0x04000AAE RID: 2734
		private XmlAttribute attr;

		// Token: 0x04000AAF RID: 2735
		private string qnames;

		// Token: 0x04000AB0 RID: 2736
		private int lineNumber;

		// Token: 0x04000AB1 RID: 2737
		private int linePosition;
	}
}
