using System;
using System.ComponentModel;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the file format of the image. Not inheritable.</summary>
	// Token: 0x0200008F RID: 143
	[TypeConverter(typeof(ImageFormatConverter))]
	public sealed class ImageFormat
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ImageFormat" /> class by using the specified <see cref="T:System.Guid" /> structure.</summary>
		/// <param name="guid">The <see cref="T:System.Guid" /> structure that specifies a particular image format. </param>
		// Token: 0x06000489 RID: 1161 RVA: 0x0000E208 File Offset: 0x0000C408
		public ImageFormat(Guid guid)
		{
			this.guid = guid;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000E217 File Offset: 0x0000C417
		private ImageFormat(string name, string guid)
		{
			this.name = name;
			this.guid = new Guid(guid);
		}

		/// <summary>Returns a value that indicates whether the specified object is an <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that is equivalent to this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</summary>
		/// <returns>true if <paramref name="o" /> is an <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that is equivalent to this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object; otherwise, false.</returns>
		/// <param name="o">The object to test. </param>
		// Token: 0x0600048B RID: 1163 RVA: 0x0000E234 File Offset: 0x0000C434
		public override bool Equals(object o)
		{
			ImageFormat imageFormat = o as ImageFormat;
			return imageFormat != null && imageFormat.Guid.Equals(this.guid);
		}

		/// <summary>Returns a hash code value that represents this object.</summary>
		/// <returns>A hash code that represents this object.</returns>
		// Token: 0x0600048C RID: 1164 RVA: 0x0000E261 File Offset: 0x0000C461
		public override int GetHashCode()
		{
			return this.guid.GetHashCode();
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</returns>
		// Token: 0x0600048D RID: 1165 RVA: 0x0000E274 File Offset: 0x0000C474
		public override string ToString()
		{
			if (this.name != null)
			{
				return this.name;
			}
			return "[ImageFormat: " + this.guid.ToString() + "]";
		}

		/// <summary>Gets a <see cref="T:System.Guid" /> structure that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</returns>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000E2A5 File Offset: 0x0000C4A5
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		/// <summary>Gets the bitmap (BMP) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the bitmap image format.</returns>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		public static ImageFormat Bmp
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat bmpImageFormat;
				lock (obj)
				{
					if (ImageFormat.BmpImageFormat == null)
					{
						ImageFormat.BmpImageFormat = new ImageFormat("Bmp", "b96b3cab-0728-11d3-9d7b-0000f81ef32e");
					}
					bmpImageFormat = ImageFormat.BmpImageFormat;
				}
				return bmpImageFormat;
			}
		}

		/// <summary>Gets the enhanced metafile (EMF) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the enhanced metafile image format.</returns>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000E30C File Offset: 0x0000C50C
		public static ImageFormat Emf
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat emfImageFormat;
				lock (obj)
				{
					if (ImageFormat.EmfImageFormat == null)
					{
						ImageFormat.EmfImageFormat = new ImageFormat("Emf", "b96b3cac-0728-11d3-9d7b-0000f81ef32e");
					}
					emfImageFormat = ImageFormat.EmfImageFormat;
				}
				return emfImageFormat;
			}
		}

		/// <summary>Gets the Exchangeable Image File (Exif) format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Exif format.</returns>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000E368 File Offset: 0x0000C568
		public static ImageFormat Exif
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat exifImageFormat;
				lock (obj)
				{
					if (ImageFormat.ExifImageFormat == null)
					{
						ImageFormat.ExifImageFormat = new ImageFormat("Exif", "b96b3cb2-0728-11d3-9d7b-0000f81ef32e");
					}
					exifImageFormat = ImageFormat.ExifImageFormat;
				}
				return exifImageFormat;
			}
		}

		/// <summary>Gets the Graphics Interchange Format (GIF) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the GIF image format.</returns>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public static ImageFormat Gif
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat gifImageFormat;
				lock (obj)
				{
					if (ImageFormat.GifImageFormat == null)
					{
						ImageFormat.GifImageFormat = new ImageFormat("Gif", "b96b3cb0-0728-11d3-9d7b-0000f81ef32e");
					}
					gifImageFormat = ImageFormat.GifImageFormat;
				}
				return gifImageFormat;
			}
		}

		/// <summary>Gets the Windows icon image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Windows icon image format.</returns>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000E420 File Offset: 0x0000C620
		public static ImageFormat Icon
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat iconImageFormat;
				lock (obj)
				{
					if (ImageFormat.IconImageFormat == null)
					{
						ImageFormat.IconImageFormat = new ImageFormat("Icon", "b96b3cb5-0728-11d3-9d7b-0000f81ef32e");
					}
					iconImageFormat = ImageFormat.IconImageFormat;
				}
				return iconImageFormat;
			}
		}

		/// <summary>Gets the Joint Photographic Experts Group (JPEG) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the JPEG image format.</returns>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000E47C File Offset: 0x0000C67C
		public static ImageFormat Jpeg
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat jpegImageFormat;
				lock (obj)
				{
					if (ImageFormat.JpegImageFormat == null)
					{
						ImageFormat.JpegImageFormat = new ImageFormat("Jpeg", "b96b3cae-0728-11d3-9d7b-0000f81ef32e");
					}
					jpegImageFormat = ImageFormat.JpegImageFormat;
				}
				return jpegImageFormat;
			}
		}

		/// <summary>Gets the format of a bitmap in memory.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the format of a bitmap in memory.</returns>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		public static ImageFormat MemoryBmp
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat memoryBmpImageFormat;
				lock (obj)
				{
					if (ImageFormat.MemoryBmpImageFormat == null)
					{
						ImageFormat.MemoryBmpImageFormat = new ImageFormat("MemoryBMP", "b96b3caa-0728-11d3-9d7b-0000f81ef32e");
					}
					memoryBmpImageFormat = ImageFormat.MemoryBmpImageFormat;
				}
				return memoryBmpImageFormat;
			}
		}

		/// <summary>Gets the W3C Portable Network Graphics (PNG) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the PNG image format.</returns>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000E534 File Offset: 0x0000C734
		public static ImageFormat Png
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat pngImageFormat;
				lock (obj)
				{
					if (ImageFormat.PngImageFormat == null)
					{
						ImageFormat.PngImageFormat = new ImageFormat("Png", "b96b3caf-0728-11d3-9d7b-0000f81ef32e");
					}
					pngImageFormat = ImageFormat.PngImageFormat;
				}
				return pngImageFormat;
			}
		}

		/// <summary>Gets the Tagged Image File Format (TIFF) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the TIFF image format.</returns>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000E590 File Offset: 0x0000C790
		public static ImageFormat Tiff
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat tiffImageFormat;
				lock (obj)
				{
					if (ImageFormat.TiffImageFormat == null)
					{
						ImageFormat.TiffImageFormat = new ImageFormat("Tiff", "b96b3cb1-0728-11d3-9d7b-0000f81ef32e");
					}
					tiffImageFormat = ImageFormat.TiffImageFormat;
				}
				return tiffImageFormat;
			}
		}

		/// <summary>Gets the Windows metafile (WMF) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Windows metafile image format.</returns>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		public static ImageFormat Wmf
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat wmfImageFormat;
				lock (obj)
				{
					if (ImageFormat.WmfImageFormat == null)
					{
						ImageFormat.WmfImageFormat = new ImageFormat("Wmf", "b96b3cad-0728-11d3-9d7b-0000f81ef32e");
					}
					wmfImageFormat = ImageFormat.WmfImageFormat;
				}
				return wmfImageFormat;
			}
		}

		// Token: 0x040002BC RID: 700
		private Guid guid;

		// Token: 0x040002BD RID: 701
		private string name;

		// Token: 0x040002BE RID: 702
		private static object locker = new object();

		// Token: 0x040002BF RID: 703
		private static ImageFormat BmpImageFormat;

		// Token: 0x040002C0 RID: 704
		private static ImageFormat EmfImageFormat;

		// Token: 0x040002C1 RID: 705
		private static ImageFormat ExifImageFormat;

		// Token: 0x040002C2 RID: 706
		private static ImageFormat GifImageFormat;

		// Token: 0x040002C3 RID: 707
		private static ImageFormat TiffImageFormat;

		// Token: 0x040002C4 RID: 708
		private static ImageFormat PngImageFormat;

		// Token: 0x040002C5 RID: 709
		private static ImageFormat MemoryBmpImageFormat;

		// Token: 0x040002C6 RID: 710
		private static ImageFormat IconImageFormat;

		// Token: 0x040002C7 RID: 711
		private static ImageFormat JpegImageFormat;

		// Token: 0x040002C8 RID: 712
		private static ImageFormat WmfImageFormat;
	}
}
