using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for that is the base class for all particle types (e.g. <see cref="T:System.Xml.Schema.XmlSchemaAny" />).</summary>
	// Token: 0x020002F6 RID: 758
	public abstract class XmlSchemaParticle : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the number as a string value. The minimum number of times the particle can occur.</summary>
		/// <returns>The number as a string value. String.Empty indicates that MinOccurs is equal to the default value. The default is a null reference.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x000C0D66 File Offset: 0x000BEF66
		// (set) Token: 0x060021DD RID: 8669 RVA: 0x000C0D80 File Offset: 0x000BEF80
		[XmlAttribute("minOccurs")]
		public string MinOccursString
		{
			get
			{
				if ((this.flags & XmlSchemaParticle.Occurs.Min) != XmlSchemaParticle.Occurs.None)
				{
					return XmlConvert.ToString(this.minOccurs);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.minOccurs = 1m;
					this.flags &= ~XmlSchemaParticle.Occurs.Min;
					return;
				}
				this.minOccurs = XmlConvert.ToInteger(value);
				if (this.minOccurs < 0m)
				{
					throw new XmlSchemaException("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", string.Empty);
				}
				this.flags |= XmlSchemaParticle.Occurs.Min;
			}
		}

		/// <summary>Gets or sets the number as a string value. Maximum number of times the particle can occur.</summary>
		/// <returns>The number as a string value. String.Empty indicates that MaxOccurs is equal to the default value. The default is a null reference.</returns>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x000C0DE7 File Offset: 0x000BEFE7
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x000C0E20 File Offset: 0x000BF020
		[XmlAttribute("maxOccurs")]
		public string MaxOccursString
		{
			get
			{
				if ((this.flags & XmlSchemaParticle.Occurs.Max) == XmlSchemaParticle.Occurs.None)
				{
					return null;
				}
				if (!(this.maxOccurs == 79228162514264337593543950335m))
				{
					return XmlConvert.ToString(this.maxOccurs);
				}
				return "unbounded";
			}
			set
			{
				if (value == null)
				{
					this.maxOccurs = 1m;
					this.flags &= ~XmlSchemaParticle.Occurs.Max;
					return;
				}
				if (value == "unbounded")
				{
					this.maxOccurs = decimal.MaxValue;
				}
				else
				{
					this.maxOccurs = XmlConvert.ToInteger(value);
					if (this.maxOccurs < 0m)
					{
						throw new XmlSchemaException("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", string.Empty);
					}
					if (this.maxOccurs == 0m && (this.flags & XmlSchemaParticle.Occurs.Min) == XmlSchemaParticle.Occurs.None)
					{
						this.minOccurs = 0m;
					}
				}
				this.flags |= XmlSchemaParticle.Occurs.Max;
			}
		}

		/// <summary>Gets or sets the minimum number of times the particle can occur.</summary>
		/// <returns>The minimum number of times the particle can occur. The default is 1.</returns>
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000C0ECE File Offset: 0x000BF0CE
		// (set) Token: 0x060021E1 RID: 8673 RVA: 0x000C0ED8 File Offset: 0x000BF0D8
		[XmlIgnore]
		public decimal MinOccurs
		{
			get
			{
				return this.minOccurs;
			}
			set
			{
				if (value < 0m || value != decimal.Truncate(value))
				{
					throw new XmlSchemaException("The value for the 'minOccurs' attribute must be xsd:nonNegativeInteger.", string.Empty);
				}
				this.minOccurs = value;
				this.flags |= XmlSchemaParticle.Occurs.Min;
			}
		}

		/// <summary>Gets or sets the maximum number of times the particle can occur.</summary>
		/// <returns>The maximum number of times the particle can occur. The default is 1.</returns>
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000C0F25 File Offset: 0x000BF125
		// (set) Token: 0x060021E3 RID: 8675 RVA: 0x000C0F30 File Offset: 0x000BF130
		[XmlIgnore]
		public decimal MaxOccurs
		{
			get
			{
				return this.maxOccurs;
			}
			set
			{
				if (value < 0m || value != decimal.Truncate(value))
				{
					throw new XmlSchemaException("The value for the 'maxOccurs' attribute must be xsd:nonNegativeInteger or 'unbounded'.", string.Empty);
				}
				this.maxOccurs = value;
				if (this.maxOccurs == 0m && (this.flags & XmlSchemaParticle.Occurs.Min) == XmlSchemaParticle.Occurs.None)
				{
					this.minOccurs = 0m;
				}
				this.flags |= XmlSchemaParticle.Occurs.Max;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000C0FA5 File Offset: 0x000BF1A5
		internal virtual bool IsEmpty
		{
			get
			{
				return this.maxOccurs == 0m;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000C0FB7 File Offset: 0x000BF1B7
		internal bool IsMultipleOccurrence
		{
			get
			{
				return this.maxOccurs > 1m;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x00015451 File Offset: 0x00013651
		internal virtual string NameString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000C0FCC File Offset: 0x000BF1CC
		internal XmlQualifiedName GetQualifiedName()
		{
			XmlSchemaElement xmlSchemaElement = this as XmlSchemaElement;
			if (xmlSchemaElement != null)
			{
				return xmlSchemaElement.QualifiedName;
			}
			XmlSchemaAny xmlSchemaAny = this as XmlSchemaAny;
			if (xmlSchemaAny != null)
			{
				string text = xmlSchemaAny.Namespace;
				if (text != null)
				{
					text = text.Trim();
				}
				else
				{
					text = string.Empty;
				}
				return new XmlQualifiedName("*", (text.Length == 0) ? "##any" : text);
			}
			return XmlQualifiedName.Empty;
		}

		// Token: 0x04000FC2 RID: 4034
		private decimal minOccurs = 1m;

		// Token: 0x04000FC3 RID: 4035
		private decimal maxOccurs = 1m;

		// Token: 0x04000FC4 RID: 4036
		private XmlSchemaParticle.Occurs flags;

		// Token: 0x04000FC5 RID: 4037
		internal static readonly XmlSchemaParticle Empty = new XmlSchemaParticle.EmptyParticle();

		// Token: 0x020002F7 RID: 759
		[Flags]
		private enum Occurs
		{
			// Token: 0x04000FC7 RID: 4039
			None = 0,
			// Token: 0x04000FC8 RID: 4040
			Min = 1,
			// Token: 0x04000FC9 RID: 4041
			Max = 2
		}

		// Token: 0x020002F8 RID: 760
		private class EmptyParticle : XmlSchemaParticle
		{
			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x060021EA RID: 8682 RVA: 0x0000EFDF File Offset: 0x0000D1DF
			internal override bool IsEmpty
			{
				get
				{
					return true;
				}
			}
		}
	}
}
