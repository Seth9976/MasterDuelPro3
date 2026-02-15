using System;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML comment. </summary>
	// Token: 0x02000008 RID: 8
	public class XComment : XNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XComment" /> class with the specified string content. </summary>
		/// <param name="value">A string that contains the contents of the new <see cref="T:System.Xml.Linq.XComment" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x06000015 RID: 21 RVA: 0x000023E8 File Offset: 0x000005E8
		public XComment(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XComment" /> class from an existing comment node. </summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XComment" /> node to copy from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is null.</exception>
		// Token: 0x06000016 RID: 22 RVA: 0x00002405 File Offset: 0x00000605
		public XComment(XComment other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.value = other.value;
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XComment" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Comment" />.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002427 File Offset: 0x00000627
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Comment;
			}
		}

		/// <summary>Gets or sets the string value of this comment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string value of this comment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is null.</exception>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000242A File Offset: 0x0000062A
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002432 File Offset: 0x00000632
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.value = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Write this comment to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600001A RID: 26 RVA: 0x00002464 File Offset: 0x00000664
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteComment(this.value);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002480 File Offset: 0x00000680
		internal override XNode CloneNode()
		{
			return new XComment(this);
		}

		// Token: 0x04000007 RID: 7
		internal string value;
	}
}
