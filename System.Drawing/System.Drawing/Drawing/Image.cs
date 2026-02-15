using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>An abstract base class that provides functionality for the <see cref="T:System.Drawing.Bitmap" /> and <see cref="T:System.Drawing.Imaging.Metafile" /> descended classes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200004E RID: 78
	[ImmutableObject(true)]
	[TypeConverter(typeof(ImageConverter))]
	[Editor("System.Drawing.Design.ImageEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	[Serializable]
	public abstract class Image : MarshalByRefObject, IDisposable, ICloneable, ISerializable
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000A8FC File Offset: 0x00008AFC
		internal Image()
		{
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A910 File Offset: 0x00008B10
		internal Image(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				if (string.Compare(serializationEntry.Name, "Data", true) == 0)
				{
					byte[] array = (byte[])serializationEntry.Value;
					if (array != null)
					{
						MemoryStream memoryStream = new MemoryStream(array);
						this.nativeObject = Image.InitFromStream(memoryStream);
						if (GDIPlus.RunningOnWindows())
						{
							this.stream = memoryStream;
						}
					}
				}
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060002D7 RID: 727 RVA: 0x0000A98C File Offset: 0x00008B8C
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (this.RawFormat.Equals(ImageFormat.Icon))
				{
					this.Save(memoryStream, ImageFormat.Png);
				}
				else
				{
					this.Save(memoryStream, this.RawFormat);
				}
				si.AddValue("Data", memoryStream.ToArray());
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a handle to a GDI bitmap.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> this method creates.</returns>
		/// <param name="hbitmap">The GDI bitmap handle from which to create the <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002D8 RID: 728 RVA: 0x0000A9FC File Offset: 0x00008BFC
		public static Bitmap FromHbitmap(IntPtr hbitmap)
		{
			return Image.FromHbitmap(hbitmap, IntPtr.Zero);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a handle to a GDI bitmap and a handle to a GDI palette.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> this method creates.</returns>
		/// <param name="hbitmap">The GDI bitmap handle from which to create the <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="hpalette">A handle to a GDI palette used to define the bitmap colors if the bitmap specified in the <paramref name="hBitmap" /> parameter is not a device-independent bitmap (DIB). </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002D9 RID: 729 RVA: 0x0000AA0C File Offset: 0x00008C0C
		public static Bitmap FromHbitmap(IntPtr hbitmap, IntPtr hpalette)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromHBITMAP(hbitmap, hpalette, out intPtr));
			return new Bitmap(intPtr);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not have a valid image format-or-<paramref name="stream" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002DA RID: 730 RVA: 0x0000AA2D File Offset: 0x00008C2D
		public static Image FromStream(Stream stream)
		{
			return Image.LoadFromStream(stream, false);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000AA38 File Offset: 0x00008C38
		internal static Image LoadFromStream(Stream stream, bool keepAlive)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Image image = Image.CreateFromHandle(Image.InitFromStream(stream));
			if (keepAlive && GDIPlus.RunningOnWindows())
			{
				image.stream = stream;
			}
			return image;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000AA74 File Offset: 0x00008C74
		internal static Image CreateFromHandle(IntPtr handle)
		{
			ImageType imageType;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageType(handle, out imageType));
			if (imageType == ImageType.Bitmap)
			{
				return new Bitmap(handle);
			}
			if (imageType != ImageType.Metafile)
			{
				throw new NotSupportedException(Locale.GetText("Unknown image type."));
			}
			return new Metafile(handle);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		internal static IntPtr InitFromStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException("stream");
			}
			if (!stream.CanSeek)
			{
				byte[] array = new byte[256];
				int num = 0;
				int num2;
				do
				{
					if (array.Length < num + 256)
					{
						byte[] array2 = new byte[array.Length * 2];
						Array.Copy(array, array2, array.Length);
						array = array2;
					}
					num2 = stream.Read(array, num, 256);
					num += num2;
				}
				while (num2 != 0);
				stream = new MemoryStream(array, 0, num);
			}
			IntPtr intPtr;
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, true);
				status = GDIPlus.GdipLoadImageFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, out intPtr);
			}
			else
			{
				status = GDIPlus.GdipLoadImageFromStream(new ComIStreamWrapper(stream), out intPtr);
			}
			if (status != Status.Ok)
			{
				return IntPtr.Zero;
			}
			return intPtr;
		}

		/// <summary>Returns the number of frames of the specified dimension.</summary>
		/// <returns>The number of frames in the specified dimension.</returns>
		/// <param name="dimension">A <see cref="T:System.Drawing.Imaging.FrameDimension" /> that specifies the identity of the dimension type. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002DE RID: 734 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public int GetFrameCount(FrameDimension dimension)
		{
			Guid guid = dimension.Guid;
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameCount(this.nativeObject, ref guid, out num));
			return (int)num;
		}

		/// <summary>Gets the specified property item from this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.PropertyItem" /> this method gets.</returns>
		/// <param name="propid">The ID of the property item to get. </param>
		/// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DF RID: 735 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public PropertyItem GetPropertyItem(int propid)
		{
			PropertyItem propertyItem = new PropertyItem();
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyItemSize(this.nativeObject, propid, out num));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyItem(this.nativeObject, propid, num, intPtr));
				GdipPropertyItem.MarshalTo((GdipPropertyItem)Marshal.PtrToStructure(intPtr, typeof(GdipPropertyItem)), propertyItem);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return propertyItem;
		}

		/// <summary>Returns a thumbnail for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the thumbnail.</returns>
		/// <param name="thumbWidth">The width, in pixels, of the requested thumbnail image. </param>
		/// <param name="thumbHeight">The height, in pixels, of the requested thumbnail image. </param>
		/// <param name="callback">A <see cref="T:System.Drawing.Image.GetThumbnailImageAbort" /> delegate. Note   You must create a delegate and pass a reference to the delegate as the <paramref name="callback" /> parameter, but the delegate is not used.</param>
		/// <param name="callbackData">Must be <see cref="F:System.IntPtr.Zero" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002E0 RID: 736 RVA: 0x0000AC30 File Offset: 0x00008E30
		public Image GetThumbnailImage(int thumbWidth, int thumbHeight, Image.GetThumbnailImageAbort callback, IntPtr callbackData)
		{
			if (thumbWidth <= 0 || thumbHeight <= 0)
			{
				throw new OutOfMemoryException("Invalid thumbnail size");
			}
			Image image = new Bitmap(thumbWidth, thumbHeight);
			using (Graphics graphics = Graphics.FromImage(image))
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(graphics.nativeObject, this.nativeObject, 0, 0, thumbWidth, thumbHeight, 0, 0, this.Width, this.Height, GraphicsUnit.Pixel, IntPtr.Zero, null, IntPtr.Zero));
			}
			return image;
		}

		/// <summary>Rotates, flips, or rotates and flips the <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="rotateFlipType">A <see cref="T:System.Drawing.RotateFlipType" /> member that specifies the type of rotation and flip to apply to the image. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002E1 RID: 737 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		public void RotateFlip(RotateFlipType rotateFlipType)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipImageRotateFlip(this.nativeObject, rotateFlipType));
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		internal ImageCodecInfo findEncoderForFormat(ImageFormat format)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			ImageCodecInfo imageCodecInfo = null;
			if (format.Guid.Equals(ImageFormat.MemoryBmp.Guid))
			{
				format = ImageFormat.Png;
			}
			for (int i = 0; i < imageEncoders.Length; i++)
			{
				if (imageEncoders[i].FormatID.Equals(format.Guid))
				{
					imageCodecInfo = imageEncoders[i];
					break;
				}
			}
			return imageCodecInfo;
		}

		/// <summary>Saves this image to the specified stream in the specified format.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved. </param>
		/// <param name="format">An <see cref="T:System.Drawing.Imaging.ImageFormat" /> that specifies the format of the saved image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E3 RID: 739 RVA: 0x0000AD28 File Offset: 0x00008F28
		public void Save(Stream stream, ImageFormat format)
		{
			ImageCodecInfo imageCodecInfo = this.findEncoderForFormat(format);
			if (imageCodecInfo == null)
			{
				throw new ArgumentException("No codec available for format:" + format.Guid.ToString());
			}
			this.Save(stream, imageCodecInfo, null);
		}

		/// <summary>Saves this image to the specified stream, with the specified encoder and image encoder parameters.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved. </param>
		/// <param name="encoder">The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> for this <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that specifies parameters used by the image encoder. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E4 RID: 740 RVA: 0x0000AD70 File Offset: 0x00008F70
		public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams)
		{
			Guid clsid = encoder.Clsid;
			IntPtr intPtr;
			if (encoderParams == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = encoderParams.ConvertToMemory();
			}
			Status status;
			try
			{
				if (GDIPlus.RunningOnUnix())
				{
					GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
					status = GDIPlus.GdipSaveImageToDelegate_linux(this.nativeObject, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, ref clsid, intPtr);
				}
				else
				{
					status = GDIPlus.GdipSaveImageToStream(new HandleRef(this, this.nativeObject), new ComIStreamWrapper(stream), ref clsid, new HandleRef(encoderParams, intPtr));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Selects the frame specified by the dimension and index.</summary>
		/// <returns>Always returns 0.</returns>
		/// <param name="dimension">A <see cref="T:System.Drawing.Imaging.FrameDimension" /> that specifies the identity of the dimension type. </param>
		/// <param name="frameIndex">The index of the active frame. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002E5 RID: 741 RVA: 0x0000AE24 File Offset: 0x00009024
		public int SelectActiveFrame(FrameDimension dimension, int frameIndex)
		{
			Guid guid = dimension.Guid;
			GDIPlus.CheckStatus(GDIPlus.GdipImageSelectActiveFrame(this.nativeObject, ref guid, frameIndex));
			return frameIndex;
		}

		/// <summary>Gets an array of GUIDs that represent the dimensions of frames within this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An array of GUIDs that specify the dimensions of frames within this <see cref="T:System.Drawing.Image" /> from most significant to least significant.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000AE4C File Offset: 0x0000904C
		[Browsable(false)]
		public Guid[] FrameDimensionsList
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameDimensionsCount(this.nativeObject, out num));
				Guid[] array = new Guid[num];
				GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameDimensionsList(this.nativeObject, array, num));
				return array;
			}
		}

		/// <summary>Gets the height, in pixels, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The height, in pixels, of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000AE88 File Offset: 0x00009088
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(false)]
		public int Height
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageHeight(this.nativeObject, out num));
				return (int)num;
			}
		}

		/// <summary>Gets or sets the color palette used for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.ColorPalette" /> that represents the color palette used for this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000AEA8 File Offset: 0x000090A8
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000AEB0 File Offset: 0x000090B0
		[Browsable(false)]
		public ColorPalette Palette
		{
			get
			{
				return this.retrieveGDIPalette();
			}
			set
			{
				this.storeGDIPalette(value);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000AEBC File Offset: 0x000090BC
		internal ColorPalette retrieveGDIPalette()
		{
			ColorPalette colorPalette = new ColorPalette();
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImagePaletteSize(this.nativeObject, out num));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			ColorPalette colorPalette2;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetImagePalette(this.nativeObject, intPtr, num));
				colorPalette.ConvertFromMemory(intPtr);
				colorPalette2 = colorPalette;
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return colorPalette2;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000AF20 File Offset: 0x00009120
		internal void storeGDIPalette(ColorPalette palette)
		{
			if (palette == null)
			{
				throw new ArgumentNullException("palette");
			}
			IntPtr intPtr = palette.ConvertToMemory();
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetImagePalette(this.nativeObject, intPtr));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Gets the pixel format for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.PixelFormat" /> that represents the pixel format for this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000AF7C File Offset: 0x0000917C
		public PixelFormat PixelFormat
		{
			get
			{
				PixelFormat pixelFormat;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImagePixelFormat(this.nativeObject, out pixelFormat));
				return pixelFormat;
			}
		}

		/// <summary>Gets the file format of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.ImageFormat" /> that represents the file format of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000AF9C File Offset: 0x0000919C
		public ImageFormat RawFormat
		{
			get
			{
				Guid guid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageRawFormat(this.nativeObject, out guid));
				return new ImageFormat(guid);
			}
		}

		/// <summary>Gets the width and height, in pixels, of this image.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that represents the width and height, in pixels, of this image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000AFC1 File Offset: 0x000091C1
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
		}

		/// <summary>Gets the width, in pixels, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The width, in pixels, of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000AFD4 File Offset: 0x000091D4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		public int Width
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageWidth(this.nativeObject, out num));
				return (int)num;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000AFF4 File Offset: 0x000091F4
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeObject;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000AFF4 File Offset: 0x000091F4
		internal IntPtr nativeImage
		{
			get
			{
				return this.nativeObject;
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Image" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002F2 RID: 754 RVA: 0x0000AFFC File Offset: 0x000091FC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B00C File Offset: 0x0000920C
		~Image()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Image" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060002F4 RID: 756 RVA: 0x0000B03C File Offset: 0x0000923C
		protected virtual void Dispose(bool disposing)
		{
			if (GDIPlus.GdiPlusToken != 0UL && this.nativeObject != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDisposeImage(this.nativeObject);
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates, cast as an object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002F5 RID: 757 RVA: 0x0000B098 File Offset: 0x00009298
		public object Clone()
		{
			if (GDIPlus.RunningOnWindows() && this.stream != null)
			{
				return this.CloneFromStream();
			}
			IntPtr zero = IntPtr.Zero;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneImage(this.NativeObject, out zero));
			if (this is Bitmap)
			{
				return new Bitmap(zero);
			}
			return new Metafile(zero);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B0E8 File Offset: 0x000092E8
		private object CloneFromStream()
		{
			MemoryStream memoryStream = new MemoryStream(new byte[this.stream.Length]);
			int num = ((this.stream.Length < 4096L) ? ((int)this.stream.Length) : 4096);
			byte[] array = new byte[num];
			this.stream.Position = 0L;
			do
			{
				num = this.stream.Read(array, 0, num);
				memoryStream.Write(array, 0, num);
			}
			while (num == 4096);
			IntPtr intPtr = IntPtr.Zero;
			intPtr = Image.InitFromStream(memoryStream);
			if (this is Bitmap)
			{
				return new Bitmap(intPtr, memoryStream);
			}
			return new Metafile(intPtr, memoryStream);
		}

		// Token: 0x0400017D RID: 381
		internal IntPtr nativeObject = IntPtr.Zero;

		// Token: 0x0400017E RID: 382
		internal Stream stream;

		/// <summary>Provides a callback method for determining when the <see cref="M:System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" /> method should prematurely cancel execution.</summary>
		/// <returns>This method returns true if it decides that the <see cref="M:System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" /> method should prematurely stop execution; otherwise, it returns false.</returns>
		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x060002F8 RID: 760
		public delegate bool GetThumbnailImageAbort();
	}
}
