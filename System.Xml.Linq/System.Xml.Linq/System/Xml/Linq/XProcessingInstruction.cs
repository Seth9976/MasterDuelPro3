using System;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML processing instruction. </summary>
	// Token: 0x02000022 RID: 34
	public class XProcessingInstruction : XNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XProcessingInstruction" /> class. </summary>
		/// <param name="target">A <see cref="T:System.String" /> containing the target application for this <see cref="T:System.Xml.Linq.XProcessingInstruction" />.</param>
		/// <param name="data">The string data for this <see cref="T:System.Xml.Linq.XProcessingInstruction" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="target" /> or <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> does not follow the constraints of an XML name.</exception>
		// Token: 0x060000D8 RID: 216 RVA: 0x00005280 File Offset: 0x00003480
		public XProcessingInstruction(string target, string data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			XProcessingInstruction.ValidateName(target);
			this.target = target;
			this.data = data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XProcessingInstruction" /> class. </summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XProcessingInstruction" /> node to copy from.</param>
		// Token: 0x060000D9 RID: 217 RVA: 0x000052AA File Offset: 0x000034AA
		public XProcessingInstruction(XProcessingInstruction other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.target = other.target;
			this.data = other.data;
		}

		/// <summary>Gets or sets the string value of this processing instruction.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string value of this processing instruction.</returns>
		/// <exception cref="T:System.ArgumentNullException">The string <paramref name="value" /> is null.</exception>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000052D8 File Offset: 0x000034D8
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000052E0 File Offset: 0x000034E0
		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.data = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XProcessingInstruction" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.ProcessingInstruction" />.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005312 File Offset: 0x00003512
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		/// <summary>Gets or sets a string containing the target application for this processing instruction.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the target application for this processing instruction.</returns>
		/// <exception cref="T:System.ArgumentNullException">The string <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> does not follow the constraints of an XML name.</exception>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005315 File Offset: 0x00003515
		public string Target
		{
			get
			{
				return this.target;
			}
		}

		/// <summary>Writes this processing instruction to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write this processing instruction to.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000DE RID: 222 RVA: 0x0000531D File Offset: 0x0000351D
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteProcessingInstruction(this.target, this.data);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000533F File Offset: 0x0000353F
		internal override XNode CloneNode()
		{
			return new XProcessingInstruction(this);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005347 File Offset: 0x00003547
		private static void ValidateName(string name)
		{
			XmlConvert.VerifyNCName(name);
			if (string.Equals(name, "xml", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(global::SR.Format("'{0}' is an invalid name for a processing instruction.", name));
			}
		}

		// Token: 0x04000059 RID: 89
		internal string target;

		// Token: 0x0400005A RID: 90
		internal string data;
	}
}
