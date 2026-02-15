using System;

namespace System.Xml.Linq
{
	/// <summary>Represents a text node that contains CDATA. </summary>
	// Token: 0x02000007 RID: 7
	public class XCData : XText
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XCData" /> class. </summary>
		/// <param name="value">A string that contains the value of the <see cref="T:System.Xml.Linq.XCData" /> node.</param>
		// Token: 0x06000010 RID: 16 RVA: 0x000023AF File Offset: 0x000005AF
		public XCData(string value)
			: base(value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XCData" /> class. </summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XCData" /> node to copy from.</param>
		// Token: 0x06000011 RID: 17 RVA: 0x000023B8 File Offset: 0x000005B8
		public XCData(XCData other)
			: base(other)
		{
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XCData" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.CDATA" />.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023C1 File Offset: 0x000005C1
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		/// <summary>Writes this CDATA object to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000013 RID: 19 RVA: 0x000023C4 File Offset: 0x000005C4
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteCData(this.text);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023E0 File Offset: 0x000005E0
		internal override XNode CloneNode()
		{
			return new XCData(this);
		}
	}
}
