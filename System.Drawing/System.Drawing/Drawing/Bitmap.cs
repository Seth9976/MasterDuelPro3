using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>Encapsulates a GDI+ bitmap, which consists of the pixel data for a graphics image and its attributes. A <see cref="T:System.Drawing.Bitmap" /> is an object used to work with images defined by pixel data.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000025 RID: 37
	[ComVisible(true)]
	[Editor("System.Drawing.Design.BitmapEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public sealed class Bitmap : Image
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00005FEC File Offset: 0x000041EC
		private Bitmap()
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005FF4 File Offset: 0x000041F4
		internal Bitmap(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006003 File Offset: 0x00004203
		internal Bitmap(IntPtr ptr, Stream stream)
		{
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = stream;
			}
			this.nativeObject = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x060001A4 RID: 420 RVA: 0x00006020 File Offset: 0x00004220
		public Bitmap(int width, int height)
			: this(width, height, PixelFormat.Format32bppArgb)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size and with the resolution of the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object that specifies the resolution for the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		// Token: 0x060001A5 RID: 421 RVA: 0x00006030 File Offset: 0x00004230
		public Bitmap(int width, int height, Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromGraphics(width, height, g.nativeObject, out intPtr));
			this.nativeObject = intPtr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size and format.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with <paramref name="Format" />.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
		// Token: 0x060001A6 RID: 422 RVA: 0x0000606C File Offset: 0x0000426C
		public Bitmap(int width, int height, PixelFormat format)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromScan0(width, height, 0, format, IntPtr.Zero, out intPtr));
			this.nativeObject = intPtr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />. </param>
		// Token: 0x060001A7 RID: 423 RVA: 0x0000609B File Offset: 0x0000429B
		public Bitmap(Image original)
			: this(original, original.Width, original.Height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream used to load the image. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not contain image data or is null.-or-<paramref name="stream" /> contains a PNG image file with a single dimension greater than 65,535 pixels.</exception>
		// Token: 0x060001A8 RID: 424 RVA: 0x000060B0 File Offset: 0x000042B0
		public Bitmap(Stream stream)
			: this(stream, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified file.</summary>
		/// <param name="filename">The bitmap file name and path. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file is not found.</exception>
		// Token: 0x060001A9 RID: 425 RVA: 0x000060BA File Offset: 0x000042BA
		public Bitmap(string filename)
			: this(filename, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image, scaled to the specified size.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="newSize">The <see cref="T:System.Drawing.Size" /> structure that represent the size of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x060001AA RID: 426 RVA: 0x000060C4 File Offset: 0x000042C4
		public Bitmap(Image original, Size newSize)
			: this(original, newSize.Width, newSize.Height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream used to load the image. </param>
		/// <param name="useIcm">true to use color correction for this <see cref="T:System.Drawing.Bitmap" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not contain image data or is null.-or-<paramref name="stream" /> contains a PNG image file with a single dimension greater than 65,535 pixels.</exception>
		// Token: 0x060001AB RID: 427 RVA: 0x000060DB File Offset: 0x000042DB
		public Bitmap(Stream stream, bool useIcm)
		{
			this.nativeObject = Image.InitFromStream(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified file.</summary>
		/// <param name="filename">The name of the bitmap file. </param>
		/// <param name="useIcm">true to use color correction for this <see cref="T:System.Drawing.Bitmap" />; otherwise, false. </param>
		// Token: 0x060001AC RID: 428 RVA: 0x000060F0 File Offset: 0x000042F0
		public Bitmap(string filename, bool useIcm)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			IntPtr intPtr;
			Status status;
			if (useIcm)
			{
				status = GDIPlus.GdipCreateBitmapFromFileICM(filename, out intPtr);
			}
			else
			{
				status = GDIPlus.GdipCreateBitmapFromFile(filename, out intPtr);
			}
			GDIPlus.CheckStatus(status);
			this.nativeObject = intPtr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image, scaled to the specified size.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x060001AD RID: 429 RVA: 0x00006135 File Offset: 0x00004335
		public Bitmap(Image original, int width, int height)
			: this(width, height, PixelFormat.Format32bppArgb)
		{
			Graphics graphics = Graphics.FromImage(this);
			graphics.DrawImage(original, 0, 0, width, height);
			graphics.Dispose();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size, pixel format, and pixel data.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="stride">Integer that specifies the byte offset between the beginning of one scan line and the next. This is usually (but not necessarily) the number of bytes in the pixel format (for example, 2 for 16 bits per pixel) multiplied by the width of the bitmap. The value passed to this parameter must be a multiple of four.. </param>
		/// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with <paramref name="Format" />.</param>
		/// <param name="scan0">Pointer to an array of bytes that contains the pixel data.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
		// Token: 0x060001AE RID: 430 RVA: 0x0000615C File Offset: 0x0000435C
		public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromScan0(width, height, stride, format, scan0, out intPtr));
			this.nativeObject = intPtr;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006189 File Offset: 0x00004389
		private Bitmap(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the color of the specified pixel in this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of the specified pixel.</returns>
		/// <param name="x">The x-coordinate of the pixel to retrieve. </param>
		/// <param name="y">The y-coordinate of the pixel to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="x" /> is less than 0, or greater than or equal to <see cref="P:System.Drawing.Image.Width" />. -or-<paramref name="y" /> is less than 0, or greater than or equal to <see cref="P:System.Drawing.Image.Height" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001B0 RID: 432 RVA: 0x00006194 File Offset: 0x00004394
		public Color GetPixel(int x, int y)
		{
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapGetPixel(this.nativeObject, x, y, out num));
			return Color.FromArgb(num);
		}

		/// <summary>Sets the color of the specified pixel in this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="x">The x-coordinate of the pixel to set. </param>
		/// <param name="y">The y-coordinate of the pixel to set. </param>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that represents the color to assign to the specified pixel. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001B1 RID: 433 RVA: 0x000061BB File Offset: 0x000043BB
		public void SetPixel(int x, int y, Color color)
		{
			Status status = GDIPlus.GdipBitmapSetPixel(this.nativeObject, x, y, color.ToArgb());
			if (status == Status.InvalidParameter && (base.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
			{
				throw new InvalidOperationException(Locale.GetText("SetPixel cannot be called on indexed bitmaps."));
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a Windows handle to an icon.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		/// <param name="hicon">A handle to an icon. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001B2 RID: 434 RVA: 0x000061F8 File Offset: 0x000043F8
		public static Bitmap FromHicon(IntPtr hicon)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromHICON(hicon, out intPtr));
			return new Bitmap(intPtr);
		}

		/// <summary>Creates a GDI bitmap object from this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A handle to the GDI bitmap object that this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060001B3 RID: 435 RVA: 0x00006218 File Offset: 0x00004418
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IntPtr GetHbitmap()
		{
			return this.GetHbitmap(Color.Gray);
		}

		/// <summary>Creates a GDI bitmap object from this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A handle to the GDI bitmap object that this method creates.</returns>
		/// <param name="background">A <see cref="T:System.Drawing.Color" /> structure that specifies the background color. This parameter is ignored if the bitmap is totally opaque. </param>
		/// <exception cref="T:System.ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060001B4 RID: 436 RVA: 0x00006228 File Offset: 0x00004428
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IntPtr GetHbitmap(Color background)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateHBITMAPFromBitmap(this.nativeObject, out intPtr, background.ToArgb()));
			return intPtr;
		}

		/// <summary>Locks a <see cref="T:System.Drawing.Bitmap" /> into system memory.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about this lock operation.</returns>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <see cref="T:System.Drawing.Bitmap" /> to lock. </param>
		/// <param name="flags">An <see cref="T:System.Drawing.Imaging.ImageLockMode" /> enumeration that specifies the access level (read/write) for the <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="format">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration that specifies the data format of this <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> is not a specific bits-per-pixel value.-or-The incorrect <see cref="T:System.Drawing.Imaging.PixelFormat" /> is passed in for a bitmap.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001B5 RID: 437 RVA: 0x00006250 File Offset: 0x00004450
		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
		{
			BitmapData bitmapData = new BitmapData();
			return this.LockBits(rect, flags, format, bitmapData);
		}

		/// <summary>Locks a <see cref="T:System.Drawing.Bitmap" /> into system memory </summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about the lock operation.</returns>
		/// <param name="rect">A rectangle structure that specifies the portion of the <see cref="T:System.Drawing.Bitmap" /> to lock.</param>
		/// <param name="flags">One of the <see cref="T:System.Drawing.Imaging.ImageLockMode" /> values that specifies the access level (read/write) for the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="format">One of the <see cref="T:System.Drawing.Imaging.PixelFormat" /> values that specifies the data format of the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="bitmapData">A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about the lock operation.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is not a specific bits-per-pixel value.-or-The incorrect <see cref="T:System.Drawing.Imaging.PixelFormat" /> is passed in for a bitmap.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001B6 RID: 438 RVA: 0x0000626D File Offset: 0x0000446D
		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format, BitmapData bitmapData)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapLockBits(this.nativeObject, ref rect, flags, format, bitmapData));
			return bitmapData;
		}

		/// <summary>Makes the specified color transparent for this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="transparentColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color to make transparent. </param>
		/// <exception cref="T:System.InvalidOperationException">The image format of the <see cref="T:System.Drawing.Bitmap" /> is an icon format.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001B7 RID: 439 RVA: 0x00006288 File Offset: 0x00004488
		public void MakeTransparent(Color transparentColor)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorKey(transparentColor, transparentColor);
			graphics.DrawImage(this, rectangle, 0, 0, base.Width, base.Height, GraphicsUnit.Pixel, imageAttributes);
			IntPtr nativeObject = this.nativeObject;
			this.nativeObject = bitmap.nativeObject;
			bitmap.nativeObject = nativeObject;
			graphics.Dispose();
			bitmap.Dispose();
			imageAttributes.Dispose();
		}

		/// <summary>Unlocks this <see cref="T:System.Drawing.Bitmap" /> from system memory.</summary>
		/// <param name="bitmapdata">A <see cref="T:System.Drawing.Imaging.BitmapData" /> that specifies information about the lock operation. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060001B8 RID: 440 RVA: 0x00006318 File Offset: 0x00004518
		public void UnlockBits(BitmapData bitmapdata)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapUnlockBits(this.nativeObject, bitmapdata));
		}
	}
}
