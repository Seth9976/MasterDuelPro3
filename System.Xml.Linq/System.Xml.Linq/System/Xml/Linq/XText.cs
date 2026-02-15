using System;
using System.Text;

namespace System.Xml.Linq
{
	/// <summary>Represents a text node. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000024 RID: 36
	public class XText : XNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XText" /> class. </summary>
		/// <param name="value">The <see cref="T:System.String" /> that contains the value of the <see cref="T:System.Xml.Linq.XText" /> node.</param>
		// Token: 0x060000E1 RID: 225 RVA: 0x0000536F File Offset: 0x0000356F
		public XText(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.text = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XText" /> class from another <see cref="T:System.Xml.Linq.XText" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XText" /> node to copy from.</param>
		// Token: 0x060000E2 RID: 226 RVA: 0x0000538C File Offset: 0x0000358C
		public XText(XText other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.text = other.text;
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XText" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Text" />.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000053AE File Offset: 0x000035AE
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		/// <summary>Gets or sets the value of this node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of this node.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000053B1 File Offset: 0x000035B1
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x000053B9 File Offset: 0x000035B9
		public string Value
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.text = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Writes this node to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000E6 RID: 230 RVA: 0x000053EB File Offset: 0x000035EB
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (this.parent is XDocument)
			{
				writer.WriteWhitespace(this.text);
				return;
			}
			writer.WriteString(this.text);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005421 File Offset: 0x00003621
		internal override void AppendText(StringBuilder sb)
		{
			sb.Append(this.text);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005430 File Offset: 0x00003630
		internal override XNode CloneNode()
		{
			return new XText(this);
		}

		// Token: 0x0400005D RID: 93
		internal string text;
	}
}
