using System;

namespace System.Drawing.Imaging
{
	/// <summary>Encapsulates a metadata property to be included in an image file. Not inheritable.</summary>
	// Token: 0x0200008D RID: 141
	public sealed class PropertyItem
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00003F24 File Offset: 0x00002124
		internal PropertyItem()
		{
		}

		/// <summary>Gets or sets the ID of the property.</summary>
		/// <returns>The integer that represents the ID of the property.</returns>
		// Token: 0x1700013F RID: 319
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		public int Id
		{
			set
			{
				this._id = value;
			}
		}

		/// <summary>Gets or sets the length (in bytes) of the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
		/// <returns>An integer that represents the length (in bytes) of the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> byte array.</returns>
		// Token: 0x17000140 RID: 320
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000E1C5 File Offset: 0x0000C3C5
		public int Len
		{
			set
			{
				this._len = value;
			}
		}

		/// <summary>Gets or sets an integer that defines the type of data contained in the <see cref="P:System.Drawing.Imaging.PropertyItem.Value" /> property.</summary>
		/// <returns>An integer that defines the type of data contained in <see cref="P:System.Drawing.Imaging.PropertyItem.Value" />.</returns>
		// Token: 0x17000141 RID: 321
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0000E1CE File Offset: 0x0000C3CE
		public short Type
		{
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the value of the property item.</summary>
		/// <returns>A byte array that represents the value of the property item.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000E1D7 File Offset: 0x0000C3D7
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0000E1DF File Offset: 0x0000C3DF
		public byte[] Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x040002A7 RID: 679
		private int _id;

		// Token: 0x040002A8 RID: 680
		private int _len;

		// Token: 0x040002A9 RID: 681
		private short _type;

		// Token: 0x040002AA RID: 682
		private byte[] _value;
	}
}
