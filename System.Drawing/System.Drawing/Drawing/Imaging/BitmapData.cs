using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the attributes of a bitmap image. The <see cref="T:System.Drawing.Imaging.BitmapData" /> class is used by the <see cref="Overload:System.Drawing.Bitmap.LockBits" /> and <see cref="M:System.Drawing.Bitmap.UnlockBits(System.Drawing.Imaging.BitmapData)" /> methods of the <see cref="T:System.Drawing.Bitmap" /> class. Not inheritable. </summary>
	// Token: 0x0200008E RID: 142
	[StructLayout(LayoutKind.Sequential)]
	public sealed class BitmapData
	{
		/// <summary>Gets or sets the pixel height of the <see cref="T:System.Drawing.Bitmap" /> object. Also sometimes referred to as the number of scan lines.</summary>
		/// <returns>The pixel height of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		/// <summary>Gets or sets the pixel width of the <see cref="T:System.Drawing.Bitmap" /> object. This can also be thought of as the number of pixels in one scan line.</summary>
		/// <returns>The pixel width of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		/// <summary>Gets or sets the address of the first pixel data in the bitmap. This can also be thought of as the first scan line in the bitmap.</summary>
		/// <returns>The address of the first pixel data in the bitmap.</returns>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public IntPtr Scan0
		{
			get
			{
				return this.scan0;
			}
		}

		/// <summary>Gets or sets the stride width (also called scan width) of the <see cref="T:System.Drawing.Bitmap" /> object.</summary>
		/// <returns>The stride width, in bytes, of the <see cref="T:System.Drawing.Bitmap" /> object.</returns>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000E200 File Offset: 0x0000C400
		public int Stride
		{
			get
			{
				return this.stride;
			}
		}

		// Token: 0x040002AB RID: 683
		private int width;

		// Token: 0x040002AC RID: 684
		private int height;

		// Token: 0x040002AD RID: 685
		private int stride;

		// Token: 0x040002AE RID: 686
		private PixelFormat pixel_format;

		// Token: 0x040002AF RID: 687
		private IntPtr scan0;

		// Token: 0x040002B0 RID: 688
		private int reserved;

		// Token: 0x040002B1 RID: 689
		private IntPtr palette;

		// Token: 0x040002B2 RID: 690
		private int property_count;

		// Token: 0x040002B3 RID: 691
		private IntPtr property;

		// Token: 0x040002B4 RID: 692
		private float dpi_horz;

		// Token: 0x040002B5 RID: 693
		private float dpi_vert;

		// Token: 0x040002B6 RID: 694
		private int image_flags;

		// Token: 0x040002B7 RID: 695
		private int left;

		// Token: 0x040002B8 RID: 696
		private int top;

		// Token: 0x040002B9 RID: 697
		private int x;

		// Token: 0x040002BA RID: 698
		private int y;

		// Token: 0x040002BB RID: 699
		private int transparent;
	}
}
