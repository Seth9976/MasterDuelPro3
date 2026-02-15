using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Contains values of properties that a component might need only occasionally.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015B RID: 347
	[Serializable]
	public class OwnerDrawPropertyBag : MarshalByRefObject, ISerializable
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x0003AF24 File Offset: 0x00039124
		internal OwnerDrawPropertyBag()
		{
			this.fore_color = (this.back_color = Color.Empty);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0003AF4B File Offset: 0x0003914B
		private OwnerDrawPropertyBag(Color fore_color, Color back_color, Font font)
		{
			this.fore_color = fore_color;
			this.back_color = back_color;
			this.font = font;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> class. </summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> value.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> value.</param>
		// Token: 0x06000D6C RID: 3436 RVA: 0x0003AF68 File Offset: 0x00039168
		protected OwnerDrawPropertyBag(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (!(name == "Font"))
				{
					if (!(name == "ForeColor"))
					{
						if (name == "BackColor")
						{
							this.back_color = (Color)serializationEntry.Value;
						}
					}
					else
					{
						this.fore_color = (Color)serializationEntry.Value;
					}
				}
				else
				{
					this.font = (Font)serializationEntry.Value;
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the component.</summary>
		/// <returns>The foreground color of the component. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0003AFFE File Offset: 0x000391FE
		public Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
		}

		/// <summary>Gets or sets the background color for the component.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the component. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0003B006 File Offset: 0x00039206
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the component.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the component. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0003B00E File Offset: 0x0003920E
		public Font Font
		{
			get
			{
				return this.font;
			}
		}

		/// <summary>Populates the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x06000D70 RID: 3440 RVA: 0x0003B016 File Offset: 0x00039216
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("BackColor", this.BackColor);
			si.AddValue("ForeColor", this.ForeColor);
			si.AddValue("Font", this.Font);
		}

		/// <summary>Copies an <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" />.</summary>
		/// <returns>A new copy of the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> control.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> to be copied.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D71 RID: 3441 RVA: 0x0003B055 File Offset: 0x00039255
		public static OwnerDrawPropertyBag Copy(OwnerDrawPropertyBag value)
		{
			return new OwnerDrawPropertyBag(value.ForeColor, value.BackColor, value.Font);
		}

		// Token: 0x04000876 RID: 2166
		private Color fore_color;

		// Token: 0x04000877 RID: 2167
		private Color back_color;

		// Token: 0x04000878 RID: 2168
		private Font font;
	}
}
