using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Drawing
{
	// Token: 0x02000062 RID: 98
	internal class GDIPlus
	{
		// Token: 0x06000366 RID: 870
		[DllImport("gdiplus")]
		internal static extern Status GdiplusStartup(ref ulong token, ref GdiplusStartupInput input, ref GdiplusStartupOutput output);

		// Token: 0x06000367 RID: 871 RVA: 0x0000C898 File Offset: 0x0000AA98
		private static void ProcessExit(object sender, EventArgs e)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		static GDIPlus()
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 6 || platform == 128)
			{
				if (Environment.GetEnvironmentVariable("not_supported_MONO_MWF_USE_NEW_X11_BACKEND") != null || Environment.GetEnvironmentVariable("MONO_MWF_MAC_FORCE_X11") != null)
				{
					GDIPlus.UseX11Drawable = true;
				}
				else
				{
					IntPtr intPtr = Marshal.AllocHGlobal(8192);
					if (GDIPlus.uname(intPtr) != 0)
					{
						GDIPlus.UseX11Drawable = true;
					}
					else if (Marshal.PtrToStringAnsi(intPtr) == "Darwin")
					{
						GDIPlus.UseCarbonDrawable = true;
					}
					else
					{
						GDIPlus.UseX11Drawable = true;
					}
					Marshal.FreeHGlobal(intPtr);
				}
			}
			GdiplusStartupInput gdiplusStartupInput = GdiplusStartupInput.MakeGdiplusStartupInput();
			GdiplusStartupOutput gdiplusStartupOutput = GdiplusStartupOutput.MakeGdiplusStartupOutput();
			try
			{
				GDIPlus.GdiplusStartup(ref GDIPlus.GdiPlusToken, ref gdiplusStartupInput, ref gdiplusStartupOutput);
			}
			catch (TypeInitializationException)
			{
				Console.Error.WriteLine("* ERROR: Can not initialize GDI+ library{0}{0}Please check http://www.mono-project.com/Problem:GDIPlusInit for details", Environment.NewLine);
			}
			AppDomain.CurrentDomain.ProcessExit += GDIPlus.ProcessExit;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		public static bool RunningOnWindows()
		{
			return !GDIPlus.UseX11Drawable && !GDIPlus.UseCarbonDrawable && !GDIPlus.UseCocoaDrawable;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C9CA File Offset: 0x0000ABCA
		public static bool RunningOnUnix()
		{
			return GDIPlus.UseX11Drawable || GDIPlus.UseCarbonDrawable || GDIPlus.UseCocoaDrawable;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		internal static void CheckStatus(Status status)
		{
			switch (status)
			{
			case Status.Ok:
				return;
			case Status.GenericError:
				throw new Exception(Locale.GetText("Generic Error [GDI+ status: {0}]", new object[] { status }));
			case Status.InvalidParameter:
				throw new ArgumentException(Locale.GetText("A null reference or invalid value was found [GDI+ status: {0}]", new object[] { status }));
			case Status.OutOfMemory:
				throw new OutOfMemoryException(Locale.GetText("Not enough memory to complete operation [GDI+ status: {0}]", new object[] { status }));
			case Status.ObjectBusy:
				throw new MemberAccessException(Locale.GetText("Object is busy and cannot state allow this operation [GDI+ status: {0}]", new object[] { status }));
			case Status.InsufficientBuffer:
				throw new InternalBufferOverflowException(Locale.GetText("Insufficient buffer provided to complete operation [GDI+ status: {0}]", new object[] { status }));
			case Status.NotImplemented:
				throw new NotImplementedException(Locale.GetText("The requested feature is not implemented [GDI+ status: {0}]", new object[] { status }));
			case Status.Win32Error:
				throw new InvalidOperationException(Locale.GetText("The operation is invalid [GDI+ status: {0}]", new object[] { status }));
			case Status.WrongState:
				throw new InvalidOperationException(Locale.GetText("Object is not in a state that can allow this operation [GDI+ status: {0}]", new object[] { status }));
			case Status.FileNotFound:
				throw new FileNotFoundException(Locale.GetText("Requested file was not found [GDI+ status: {0}]", new object[] { status }));
			case Status.ValueOverflow:
				throw new OverflowException(Locale.GetText("Argument is out of range [GDI+ status: {0}]", new object[] { status }));
			case Status.AccessDenied:
				throw new UnauthorizedAccessException(Locale.GetText("Access to resource was denied [GDI+ status: {0}]", new object[] { status }));
			case Status.UnknownImageFormat:
				throw new NotSupportedException(Locale.GetText("Either the image format is unknown or you don't have the required libraries to decode this format [GDI+ status: {0}]", new object[] { status }));
			case Status.FontFamilyNotFound:
				throw new ArgumentException(Locale.GetText("The requested FontFamily could not be found [GDI+ status: {0}]", new object[] { status }));
			case Status.PropertyNotSupported:
				throw new NotSupportedException(Locale.GetText("Property not supported [GDI+ status: {0}]", new object[] { status }));
			}
			throw new Exception(Locale.GetText("Unknown Error [GDI+ status: {0}]", new object[] { status }));
		}

		// Token: 0x0600036C RID: 876
		[DllImport("gdiplus")]
		internal static extern int GdipCloneBrush(HandleRef brush, out IntPtr clonedBrush);

		// Token: 0x0600036D RID: 877
		[DllImport("gdiplus")]
		internal static extern int GdipDeleteBrush(HandleRef brush);

		// Token: 0x0600036E RID: 878
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegion(out IntPtr region);

		// Token: 0x0600036F RID: 879
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteRegion(IntPtr region);

		// Token: 0x06000370 RID: 880
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegionRectI(ref Rectangle rect, out IntPtr region);

		// Token: 0x06000371 RID: 881
		[DllImport("gdiplus")]
		internal static extern Status GdipCombineRegionRectI(IntPtr region, ref Rectangle rect, CombineMode combineMode);

		// Token: 0x06000372 RID: 882
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRegionBounds(IntPtr region, IntPtr graphics, ref RectangleF rect);

		// Token: 0x06000373 RID: 883
		[DllImport("gdiplus")]
		internal static extern Status GdipSetEmpty(IntPtr region);

		// Token: 0x06000374 RID: 884
		[DllImport("gdiplus")]
		internal static extern Status GdipIsInfiniteRegion(IntPtr region, IntPtr graphics, out bool result);

		// Token: 0x06000375 RID: 885
		[DllImport("gdiplus")]
		internal static extern Status GdipCombineRegionRegion(IntPtr region, IntPtr region2, CombineMode combineMode);

		// Token: 0x06000376 RID: 886
		[DllImport("gdiplus")]
		internal static extern Status GdipGetRegionHRgn(IntPtr region, IntPtr graphics, ref IntPtr hRgn);

		// Token: 0x06000377 RID: 887
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateRegionHrgn(IntPtr hRgn, out IntPtr region);

		// Token: 0x06000378 RID: 888
		[DllImport("gdiplus")]
		internal static extern int GdipCreateSolidFill(int color, out IntPtr brush);

		// Token: 0x06000379 RID: 889
		[DllImport("gdiplus")]
		internal static extern int GdipCreateHatchBrush(int hatchstyle, int foreColor, int backColor, out IntPtr brush);

		// Token: 0x0600037A RID: 890
		[DllImport("gdiplus")]
		internal static extern int GdipCreateTexture(HandleRef image, int wrapMode, out IntPtr texture);

		// Token: 0x0600037B RID: 891
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateLineBrushI(ref Point point1, ref Point point2, int color1, int color2, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x0600037C RID: 892
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateLineBrushFromRectI(ref Rectangle rect, int color1, int color2, LinearGradientMode linearGradientMode, WrapMode wrapMode, out IntPtr brush);

		// Token: 0x0600037D RID: 893
		[DllImport("gdiplus")]
		internal static extern Status GdipGetLineRect(IntPtr brush, out RectangleF rect);

		// Token: 0x0600037E RID: 894
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFromHDC(IntPtr hDC, out IntPtr graphics);

		// Token: 0x0600037F RID: 895
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteGraphics(IntPtr graphics);

		// Token: 0x06000380 RID: 896
		[DllImport("gdiplus")]
		internal static extern Status GdipRestoreGraphics(IntPtr graphics, uint graphicsState);

		// Token: 0x06000381 RID: 897
		[DllImport("gdiplus")]
		internal static extern Status GdipSaveGraphics(IntPtr graphics, out uint state);

		// Token: 0x06000382 RID: 898
		[DllImport("gdiplus")]
		internal static extern Status GdipRotateWorldTransform(IntPtr graphics, float angle, MatrixOrder order);

		// Token: 0x06000383 RID: 899
		[DllImport("gdiplus")]
		internal static extern Status GdipTranslateWorldTransform(IntPtr graphics, float dx, float dy, MatrixOrder order);

		// Token: 0x06000384 RID: 900
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawLine(IntPtr graphics, IntPtr pen, float x1, float y1, float x2, float y2);

		// Token: 0x06000385 RID: 901
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawLineI(IntPtr graphics, IntPtr pen, int x1, int y1, int x2, int y2);

		// Token: 0x06000386 RID: 902
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawLinesI(IntPtr graphics, IntPtr pen, Point[] points, int count);

		// Token: 0x06000387 RID: 903
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawPolygonI(IntPtr graphics, IntPtr pen, Point[] points, int count);

		// Token: 0x06000388 RID: 904
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawRectangleI(IntPtr graphics, IntPtr pen, int x, int y, int width, int height);

		// Token: 0x06000389 RID: 905
		[DllImport("gdiplus")]
		internal static extern Status GdipFillEllipseI(IntPtr graphics, IntPtr pen, int x, int y, int width, int height);

		// Token: 0x0600038A RID: 906
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPolygonI(IntPtr graphics, IntPtr brush, Point[] points, int count, FillMode fillMode);

		// Token: 0x0600038B RID: 907
		[DllImport("gdiplus")]
		internal static extern Status GdipFillPolygon2(IntPtr graphics, IntPtr brush, PointF[] points, int count);

		// Token: 0x0600038C RID: 908
		[DllImport("gdiplus")]
		internal static extern Status GdipFillRectangle(IntPtr graphics, IntPtr brush, float x1, float y1, float x2, float y2);

		// Token: 0x0600038D RID: 909
		[DllImport("gdiplus")]
		internal static extern Status GdipFillRectangleI(IntPtr graphics, IntPtr brush, int x1, int y1, int x2, int y2);

		// Token: 0x0600038E RID: 910
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal static extern Status GdipDrawString(IntPtr graphics, string text, int len, IntPtr font, ref RectangleF rc, IntPtr format, IntPtr brush);

		// Token: 0x0600038F RID: 911
		[DllImport("gdiplus")]
		internal static extern Status GdipGetDC(IntPtr graphics, out IntPtr hdc);

		// Token: 0x06000390 RID: 912
		[DllImport("gdiplus")]
		internal static extern Status GdipReleaseDC(IntPtr graphics, IntPtr hdc);

		// Token: 0x06000391 RID: 913
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImageRectI(IntPtr graphics, IntPtr image, int x, int y, int width, int height);

		// Token: 0x06000392 RID: 914
		[DllImport("gdiplus")]
		internal static extern Status GdipResetWorldTransform(IntPtr graphics);

		// Token: 0x06000393 RID: 915
		[DllImport("gdiplus")]
		internal static extern Status GdipGetWorldTransform(IntPtr graphics, IntPtr matrix);

		// Token: 0x06000394 RID: 916
		[DllImport("gdiplus")]
		internal static extern Status GdipGraphicsClear(IntPtr graphics, int argb);

		// Token: 0x06000395 RID: 917
		[DllImport("gdiplus")]
		internal static extern Status GdipSetClipRectI(IntPtr graphics, int x, int y, int width, int height, CombineMode combineMode);

		// Token: 0x06000396 RID: 918
		[DllImport("gdiplus")]
		internal static extern Status GdipSetClipRegion(IntPtr graphics, IntPtr region, CombineMode combineMode);

		// Token: 0x06000397 RID: 919
		[DllImport("gdiplus")]
		internal static extern Status GdipResetClip(IntPtr graphics);

		// Token: 0x06000398 RID: 920
		[DllImport("gdiplus")]
		internal static extern Status GdipGetClip(IntPtr graphics, IntPtr region);

		// Token: 0x06000399 RID: 921
		[DllImport("gdiplus")]
		internal static extern Status GdipGetDpiX(IntPtr graphics, out float dpi);

		// Token: 0x0600039A RID: 922
		[DllImport("gdiplus")]
		internal static extern Status GdipGetDpiY(IntPtr graphics, out float dpi);

		// Token: 0x0600039B RID: 923
		[DllImport("gdiplus")]
		internal static extern Status GdipGetVisibleClipBounds(IntPtr graphics, out RectangleF rect);

		// Token: 0x0600039C RID: 924
		[DllImport("gdiplus")]
		internal static extern Status GdipFlush(IntPtr graphics, FlushIntention intention);

		// Token: 0x0600039D RID: 925
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePen1(int argb, float width, GraphicsUnit unit, out IntPtr pen);

		// Token: 0x0600039E RID: 926
		[DllImport("gdiplus")]
		internal static extern Status GdipCreatePen2(IntPtr brush, float width, GraphicsUnit unit, out IntPtr pen);

		// Token: 0x0600039F RID: 927
		[DllImport("gdiplus")]
		internal static extern Status GdipClonePen(IntPtr pen, out IntPtr clonepen);

		// Token: 0x060003A0 RID: 928
		[DllImport("gdiplus")]
		internal static extern Status GdipDeletePen(IntPtr pen);

		// Token: 0x060003A1 RID: 929
		[DllImport("gdiplus")]
		internal static extern Status GdipSetPenDashStyle(IntPtr pen, DashStyle dashStyle);

		// Token: 0x060003A2 RID: 930
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPenWidth(IntPtr pen, out float width);

		// Token: 0x060003A3 RID: 931
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFromHWND(IntPtr hwnd, out IntPtr graphics);

		// Token: 0x060003A4 RID: 932
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal unsafe static extern Status GdipMeasureString(IntPtr graphics, string str, int length, IntPtr font, ref RectangleF layoutRect, IntPtr stringFormat, out RectangleF boundingBox, int* codepointsFitted, int* linesFilled);

		// Token: 0x060003A5 RID: 933
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromScan0(int width, int height, int stride, PixelFormat format, IntPtr scan0, out IntPtr bmp);

		// Token: 0x060003A6 RID: 934
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromGraphics(int width, int height, IntPtr target, out IntPtr bitmap);

		// Token: 0x060003A7 RID: 935
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapLockBits(IntPtr bmp, ref Rectangle rc, ImageLockMode flags, PixelFormat format, [In] [Out] BitmapData bmpData);

		// Token: 0x060003A8 RID: 936
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapUnlockBits(IntPtr bmp, [In] [Out] BitmapData bmpData);

		// Token: 0x060003A9 RID: 937
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapGetPixel(IntPtr bmp, int x, int y, out int argb);

		// Token: 0x060003AA RID: 938
		[DllImport("gdiplus")]
		internal static extern Status GdipBitmapSetPixel(IntPtr bmp, int x, int y, int argb);

		// Token: 0x060003AB RID: 939
		[DllImport("gdiplus", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern Status GdipLoadImageFromStream([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream stream, out IntPtr image);

		// Token: 0x060003AC RID: 940
		[DllImport("gdiplus", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern Status GdipSaveImageToStream(HandleRef image, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.ComIStreamMarshaler)] IStream stream, [In] ref Guid clsidEncoder, HandleRef encoderParams);

		// Token: 0x060003AD RID: 941
		[DllImport("gdiplus")]
		internal static extern Status GdipCloneImage(IntPtr image, out IntPtr imageclone);

		// Token: 0x060003AE RID: 942
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromHBITMAP(IntPtr hBitMap, IntPtr gdiPalette, out IntPtr image);

		// Token: 0x060003AF RID: 943
		[DllImport("gdiplus")]
		internal static extern Status GdipDisposeImage(IntPtr image);

		// Token: 0x060003B0 RID: 944
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageType(IntPtr image, out ImageType type);

		// Token: 0x060003B1 RID: 945
		[DllImport("gdiplus")]
		internal static extern Status GdipImageGetFrameDimensionsCount(IntPtr image, out uint count);

		// Token: 0x060003B2 RID: 946
		[DllImport("gdiplus")]
		internal static extern Status GdipImageGetFrameDimensionsList(IntPtr image, [Out] Guid[] dimensionIDs, uint count);

		// Token: 0x060003B3 RID: 947
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageHeight(IntPtr image, out uint height);

		// Token: 0x060003B4 RID: 948
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImagePaletteSize(IntPtr image, out int size);

		// Token: 0x060003B5 RID: 949
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImagePalette(IntPtr image, IntPtr palette, int size);

		// Token: 0x060003B6 RID: 950
		[DllImport("gdiplus")]
		internal static extern Status GdipSetImagePalette(IntPtr image, IntPtr palette);

		// Token: 0x060003B7 RID: 951
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImagePixelFormat(IntPtr image, out PixelFormat format);

		// Token: 0x060003B8 RID: 952
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageRawFormat(IntPtr image, out Guid format);

		// Token: 0x060003B9 RID: 953
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageWidth(IntPtr image, out uint width);

		// Token: 0x060003BA RID: 954
		[DllImport("gdiplus")]
		internal static extern Status GdipImageGetFrameCount(IntPtr image, ref Guid guidDimension, out uint count);

		// Token: 0x060003BB RID: 955
		[DllImport("gdiplus")]
		internal static extern Status GdipImageSelectActiveFrame(IntPtr image, ref Guid guidDimension, int frameIndex);

		// Token: 0x060003BC RID: 956
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPropertyItemSize(IntPtr image, int propertyID, out int propertySize);

		// Token: 0x060003BD RID: 957
		[DllImport("gdiplus")]
		internal static extern Status GdipGetPropertyItem(IntPtr image, int propertyID, int propertySize, IntPtr buffer);

		// Token: 0x060003BE RID: 958
		[DllImport("gdiplus")]
		internal static extern Status GdipImageRotateFlip(IntPtr image, RotateFlipType rotateFlipType);

		// Token: 0x060003BF RID: 959
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImageI(IntPtr graphics, IntPtr image, int x, int y);

		// Token: 0x060003C0 RID: 960
		[DllImport("gdiplus")]
		internal static extern Status GdipGetImageGraphicsContext(IntPtr image, out IntPtr graphics);

		// Token: 0x060003C1 RID: 961
		[DllImport("gdiplus")]
		internal static extern Status GdipDrawImageRectRectI(IntPtr graphics, IntPtr image, int dstx, int dsty, int dstwidth, int dstheight, int srcx, int srcy, int srcwidth, int srcheight, GraphicsUnit srcUnit, IntPtr imageattr, Graphics.DrawImageAbort callback, IntPtr callbackData);

		// Token: 0x060003C2 RID: 962
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateStringFormat(StringFormatFlags formatAttributes, int language, out IntPtr native);

		// Token: 0x060003C3 RID: 963
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateHBITMAPFromBitmap(IntPtr bmp, out IntPtr HandleBmp, int clrbackground);

		// Token: 0x060003C4 RID: 964
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipCreateBitmapFromFile([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr bitmap);

		// Token: 0x060003C5 RID: 965
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipCreateBitmapFromFileICM([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr bitmap);

		// Token: 0x060003C6 RID: 966
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateBitmapFromHICON(IntPtr hicon, out IntPtr bitmap);

		// Token: 0x060003C7 RID: 967
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateMatrix(out IntPtr matrix);

		// Token: 0x060003C8 RID: 968
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteMatrix(IntPtr matrix);

		// Token: 0x060003C9 RID: 969
		[DllImport("gdiplus")]
		internal static extern Status GdipGetMatrixElements(IntPtr matrix, IntPtr matrixOut);

		// Token: 0x060003CA RID: 970
		[DllImport("gdiplus")]
		internal static extern Status GdipIsMatrixEqual(IntPtr matrix, IntPtr matrix2, out bool result);

		// Token: 0x060003CB RID: 971
		[DllImport("gdiplus")]
		internal static extern int GdipCreateImageAttributes(out IntPtr imageattr);

		// Token: 0x060003CC RID: 972
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesColorKeys(HandleRef imageattr, ColorAdjustType type, bool enableFlag, int colorLow, int colorHigh);

		// Token: 0x060003CD RID: 973
		[DllImport("gdiplus")]
		internal static extern int GdipDisposeImageAttributes(HandleRef imageattr);

		// Token: 0x060003CE RID: 974
		[DllImport("gdiplus")]
		internal static extern int GdipSetImageAttributesColorMatrix(HandleRef imageattr, ColorAdjustType type, bool enableFlag, ColorMatrix colorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag flags);

		// Token: 0x060003CF RID: 975
		[DllImport("gdiplus")]
		internal static extern int GdipCloneImageAttributes(HandleRef imageattr, out IntPtr cloneImageattr);

		// Token: 0x060003D0 RID: 976
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFont(IntPtr fontFamily, float emSize, FontStyle style, GraphicsUnit unit, out IntPtr font);

		// Token: 0x060003D1 RID: 977
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteFont(IntPtr font);

		// Token: 0x060003D2 RID: 978
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipGetLogFont(IntPtr font, IntPtr graphics, [MarshalAs(UnmanagedType.AsAny)] [Out] object logfontA);

		// Token: 0x060003D3 RID: 979
		[DllImport("gdi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
		internal static extern IntPtr CreateFontIndirect(ref LOGFONT logfont);

		// Token: 0x060003D4 RID: 980
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		internal static extern IntPtr GetDC(IntPtr hwnd);

		// Token: 0x060003D5 RID: 981
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		// Token: 0x060003D6 RID: 982
		[DllImport("user32.dll", SetLastError = true)]
		internal static extern bool GetIconInfo(IntPtr hIcon, out IconInfo iconinfo);

		// Token: 0x060003D7 RID: 983
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		internal static extern IntPtr CreateIconIndirect([In] ref IconInfo piconinfo);

		// Token: 0x060003D8 RID: 984
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		internal static extern bool DestroyIcon(IntPtr hIcon);

		// Token: 0x060003D9 RID: 985
		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetSysColor")]
		public static extern uint Win32GetSysColor(GetSysColorIndex index);

		// Token: 0x060003DA RID: 986
		[DllImport("libX11")]
		internal static extern IntPtr XOpenDisplay(IntPtr display);

		// Token: 0x060003DB RID: 987
		[DllImport("libX11")]
		internal static extern IntPtr XRootWindow(IntPtr display, int screen);

		// Token: 0x060003DC RID: 988
		[DllImport("libX11")]
		internal static extern int XDefaultScreen(IntPtr display);

		// Token: 0x060003DD RID: 989
		[DllImport("gdiplus")]
		internal static extern int GdipGetFontCollectionFamilyCount(HandleRef collection, out int found);

		// Token: 0x060003DE RID: 990
		[DllImport("gdiplus")]
		internal static extern int GdipGetFontCollectionFamilyList(HandleRef collection, int getCount, IntPtr[] dest, out int retCount);

		// Token: 0x060003DF RID: 991
		[DllImport("gdiplus")]
		internal static extern int GdipNewInstalledFontCollection(out IntPtr collection);

		// Token: 0x060003E0 RID: 992
		[DllImport("gdiplus")]
		internal static extern Status GdipNewPrivateFontCollection(out IntPtr collection);

		// Token: 0x060003E1 RID: 993
		[DllImport("gdiplus")]
		internal static extern Status GdipDeletePrivateFontCollection(ref IntPtr collection);

		// Token: 0x060003E2 RID: 994
		[DllImport("gdiplus", CharSet = CharSet.Auto)]
		internal static extern Status GdipCreateFontFamilyFromName([MarshalAs(UnmanagedType.LPWStr)] string fName, IntPtr collection, out IntPtr fontFamily);

		// Token: 0x060003E3 RID: 995
		[DllImport("gdiplus", CharSet = CharSet.Unicode)]
		internal static extern Status GdipGetFamilyName(IntPtr family, IntPtr name, int language);

		// Token: 0x060003E4 RID: 996
		[DllImport("gdiplus")]
		internal static extern Status GdipGetGenericFontFamilySansSerif(out IntPtr fontFamily);

		// Token: 0x060003E5 RID: 997
		[DllImport("gdiplus")]
		internal static extern Status GdipGetGenericFontFamilySerif(out IntPtr fontFamily);

		// Token: 0x060003E6 RID: 998
		[DllImport("gdiplus")]
		internal static extern Status GdipGetGenericFontFamilyMonospace(out IntPtr fontFamily);

		// Token: 0x060003E7 RID: 999
		[DllImport("gdiplus")]
		internal static extern Status GdipGetCellAscent(IntPtr fontFamily, int style, out short ascent);

		// Token: 0x060003E8 RID: 1000
		[DllImport("gdiplus")]
		internal static extern Status GdipGetCellDescent(IntPtr fontFamily, int style, out short descent);

		// Token: 0x060003E9 RID: 1001
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteFontFamily(IntPtr fontFamily);

		// Token: 0x060003EA RID: 1002
		[DllImport("gdiplus")]
		internal static extern Status GdipGetFontHeightGivenDPI(IntPtr font, float dpi, out float height);

		// Token: 0x060003EB RID: 1003
		[DllImport("gdiplus")]
		internal static extern int GdipCloneFontFamily(HandleRef fontFamily, out IntPtr clone);

		// Token: 0x060003EC RID: 1004
		[DllImport("gdiplus")]
		internal static extern Status GdipStringFormatGetGenericTypographic(out IntPtr format);

		// Token: 0x060003ED RID: 1005
		[DllImport("gdiplus")]
		internal static extern Status GdipDeleteStringFormat(IntPtr format);

		// Token: 0x060003EE RID: 1006
		[DllImport("gdiplus")]
		internal static extern Status GdipCloneStringFormat(IntPtr srcformat, out IntPtr format);

		// Token: 0x060003EF RID: 1007
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatFlags(IntPtr format, StringFormatFlags flags);

		// Token: 0x060003F0 RID: 1008
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatFlags(IntPtr format, out StringFormatFlags flags);

		// Token: 0x060003F1 RID: 1009
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatAlign(IntPtr format, StringAlignment align);

		// Token: 0x060003F2 RID: 1010
		[DllImport("gdiplus")]
		internal static extern Status GdipGetStringFormatAlign(IntPtr format, out StringAlignment align);

		// Token: 0x060003F3 RID: 1011
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatLineAlign(IntPtr format, StringAlignment align);

		// Token: 0x060003F4 RID: 1012
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatTrimming(IntPtr format, StringTrimming trimming);

		// Token: 0x060003F5 RID: 1013
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatHotkeyPrefix(IntPtr format, HotkeyPrefix hotkeyPrefix);

		// Token: 0x060003F6 RID: 1014
		[DllImport("gdiplus")]
		internal static extern Status GdipSetStringFormatTabStops(IntPtr format, float firstTabOffset, int count, float[] tabStops);

		// Token: 0x060003F7 RID: 1015
		[DllImport("gdiplus")]
		internal static extern int GdipGetImageEncodersSize(out int encoderNums, out int arraySize);

		// Token: 0x060003F8 RID: 1016
		[DllImport("gdiplus")]
		internal static extern int GdipGetImageEncoders(int encoderNums, int arraySize, IntPtr encoders);

		// Token: 0x060003F9 RID: 1017
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFromContext_macosx(IntPtr cgref, int width, int height, out IntPtr graphics);

		// Token: 0x060003FA RID: 1018
		[DllImport("gdiplus")]
		internal static extern Status GdipSetVisibleClip_linux(IntPtr graphics, ref Rectangle rect);

		// Token: 0x060003FB RID: 1019
		[DllImport("gdiplus")]
		internal static extern Status GdipCreateFromXDrawable_linux(IntPtr drawable, IntPtr display, out IntPtr graphics);

		// Token: 0x060003FC RID: 1020
		[DllImport("gdiplus")]
		internal static extern Status GdipLoadImageFromDelegate_linux(GDIPlus.StreamGetHeaderDelegate getHeader, GDIPlus.StreamGetBytesDelegate getBytes, GDIPlus.StreamPutBytesDelegate putBytes, GDIPlus.StreamSeekDelegate doSeek, GDIPlus.StreamCloseDelegate close, GDIPlus.StreamSizeDelegate size, out IntPtr image);

		// Token: 0x060003FD RID: 1021
		[DllImport("gdiplus")]
		internal static extern Status GdipSaveImageToDelegate_linux(IntPtr image, GDIPlus.StreamGetBytesDelegate getBytes, GDIPlus.StreamPutBytesDelegate putBytes, GDIPlus.StreamSeekDelegate doSeek, GDIPlus.StreamCloseDelegate close, GDIPlus.StreamSizeDelegate size, ref Guid encoderClsID, IntPtr encoderParameters);

		// Token: 0x060003FE RID: 1022
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x040001DD RID: 477
		public static IntPtr Display = IntPtr.Zero;

		// Token: 0x040001DE RID: 478
		public static bool UseX11Drawable = false;

		// Token: 0x040001DF RID: 479
		public static bool UseCarbonDrawable = false;

		// Token: 0x040001E0 RID: 480
		public static bool UseCocoaDrawable = false;

		// Token: 0x040001E1 RID: 481
		internal static ulong GdiPlusToken = 0UL;

		// Token: 0x02000063 RID: 99
		// (Invoke) Token: 0x06000400 RID: 1024
		public delegate int StreamGetHeaderDelegate(IntPtr buf, int bufsz);

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x06000402 RID: 1026
		public delegate int StreamGetBytesDelegate(IntPtr buf, int bufsz, bool peek);

		// Token: 0x02000065 RID: 101
		// (Invoke) Token: 0x06000404 RID: 1028
		public delegate long StreamSeekDelegate(int offset, int whence);

		// Token: 0x02000066 RID: 102
		// (Invoke) Token: 0x06000406 RID: 1030
		public delegate int StreamPutBytesDelegate(IntPtr buf, int bufsz);

		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x06000408 RID: 1032
		public delegate void StreamCloseDelegate();

		// Token: 0x02000068 RID: 104
		// (Invoke) Token: 0x0600040A RID: 1034
		public delegate long StreamSizeDelegate();

		// Token: 0x02000069 RID: 105
		internal sealed class GdiPlusStreamHelper
		{
			// Token: 0x0600040B RID: 1035 RVA: 0x0000CC24 File Offset: 0x0000AE24
			public GdiPlusStreamHelper(Stream s, bool seekToOrigin)
			{
				this.managedBuf = new byte[4096];
				this.stream = s;
				if (this.stream != null && this.stream.CanSeek && seekToOrigin)
				{
					this.stream.Seek(0L, SeekOrigin.Begin);
				}
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000CC78 File Offset: 0x0000AE78
			public int StreamGetHeaderImpl(IntPtr buf, int bufsz)
			{
				this.start_buf = new byte[bufsz];
				int num;
				try
				{
					num = this.stream.Read(this.start_buf, 0, bufsz);
				}
				catch (IOException)
				{
					return -1;
				}
				if (num > 0 && buf != IntPtr.Zero)
				{
					Marshal.Copy(this.start_buf, 0, (IntPtr)buf.ToInt64(), num);
				}
				this.start_buf_pos = 0;
				this.start_buf_len = num;
				return num;
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
			public GDIPlus.StreamGetHeaderDelegate GetHeaderDelegate
			{
				get
				{
					if (this.stream != null && this.stream.CanRead)
					{
						if (this.sghd == null)
						{
							this.sghd = new GDIPlus.StreamGetHeaderDelegate(this.StreamGetHeaderImpl);
						}
						return this.sghd;
					}
					return null;
				}
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0000CD34 File Offset: 0x0000AF34
			public int StreamGetBytesImpl(IntPtr buf, int bufsz, bool peek)
			{
				if (buf == IntPtr.Zero && peek)
				{
					return -1;
				}
				if (bufsz > this.managedBuf.Length)
				{
					this.managedBuf = new byte[bufsz];
				}
				int num = 0;
				long num2 = 0L;
				if (bufsz > 0)
				{
					if (this.stream.CanSeek)
					{
						num2 = this.stream.Position;
					}
					if (this.start_buf_len > 0)
					{
						if (this.start_buf_len > bufsz)
						{
							Array.Copy(this.start_buf, this.start_buf_pos, this.managedBuf, 0, bufsz);
							this.start_buf_pos += bufsz;
							this.start_buf_len -= bufsz;
							num = bufsz;
							bufsz = 0;
						}
						else
						{
							Array.Copy(this.start_buf, this.start_buf_pos, this.managedBuf, 0, this.start_buf_len);
							bufsz -= this.start_buf_len;
							num = this.start_buf_len;
							this.start_buf_len = 0;
						}
					}
					if (bufsz > 0)
					{
						try
						{
							num += this.stream.Read(this.managedBuf, num, bufsz);
						}
						catch (IOException)
						{
							return -1;
						}
					}
					if (num > 0 && buf != IntPtr.Zero)
					{
						Marshal.Copy(this.managedBuf, 0, (IntPtr)buf.ToInt64(), num);
					}
					bool flag = !this.stream.CanSeek && bufsz == 10 && peek;
					if (peek)
					{
						if (!this.stream.CanSeek)
						{
							throw new NotSupportedException();
						}
						this.stream.Seek(num2, SeekOrigin.Begin);
					}
				}
				return num;
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
			public GDIPlus.StreamGetBytesDelegate GetBytesDelegate
			{
				get
				{
					if (this.stream != null && this.stream.CanRead)
					{
						if (this.sgbd == null)
						{
							this.sgbd = new GDIPlus.StreamGetBytesDelegate(this.StreamGetBytesImpl);
						}
						return this.sgbd;
					}
					return null;
				}
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x0000CEEC File Offset: 0x0000B0EC
			public long StreamSeekImpl(int offset, int whence)
			{
				if (whence < 0 || whence > 2)
				{
					return -1L;
				}
				this.start_buf_pos += this.start_buf_len;
				this.start_buf_len = 0;
				SeekOrigin seekOrigin;
				switch (whence)
				{
				case 0:
					seekOrigin = SeekOrigin.Begin;
					break;
				case 1:
					seekOrigin = SeekOrigin.Current;
					break;
				case 2:
					seekOrigin = SeekOrigin.End;
					break;
				default:
					return -1L;
				}
				return this.stream.Seek((long)offset, seekOrigin);
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000CF4F File Offset: 0x0000B14F
			public GDIPlus.StreamSeekDelegate SeekDelegate
			{
				get
				{
					if (this.stream != null && this.stream.CanSeek)
					{
						if (this.skd == null)
						{
							this.skd = new GDIPlus.StreamSeekDelegate(this.StreamSeekImpl);
						}
						return this.skd;
					}
					return null;
				}
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x0000CF88 File Offset: 0x0000B188
			public int StreamPutBytesImpl(IntPtr buf, int bufsz)
			{
				if (bufsz > this.managedBuf.Length)
				{
					this.managedBuf = new byte[bufsz];
				}
				Marshal.Copy(buf, this.managedBuf, 0, bufsz);
				this.stream.Write(this.managedBuf, 0, bufsz);
				return bufsz;
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000CFC3 File Offset: 0x0000B1C3
			public GDIPlus.StreamPutBytesDelegate PutBytesDelegate
			{
				get
				{
					if (this.stream != null && this.stream.CanWrite)
					{
						if (this.spbd == null)
						{
							this.spbd = new GDIPlus.StreamPutBytesDelegate(this.StreamPutBytesImpl);
						}
						return this.spbd;
					}
					return null;
				}
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000CFFC File Offset: 0x0000B1FC
			public void StreamCloseImpl()
			{
				this.stream.Dispose();
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000D009 File Offset: 0x0000B209
			public GDIPlus.StreamCloseDelegate CloseDelegate
			{
				get
				{
					if (this.stream != null)
					{
						if (this.scd == null)
						{
							this.scd = new GDIPlus.StreamCloseDelegate(this.StreamCloseImpl);
						}
						return this.scd;
					}
					return null;
				}
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x0000D038 File Offset: 0x0000B238
			public long StreamSizeImpl()
			{
				long num;
				try
				{
					num = this.stream.Length;
				}
				catch
				{
					num = -1L;
				}
				return num;
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000D06C File Offset: 0x0000B26C
			public GDIPlus.StreamSizeDelegate SizeDelegate
			{
				get
				{
					if (this.stream != null)
					{
						if (this.ssd == null)
						{
							this.ssd = new GDIPlus.StreamSizeDelegate(this.StreamSizeImpl);
						}
						return this.ssd;
					}
					return null;
				}
			}

			// Token: 0x040001E2 RID: 482
			public Stream stream;

			// Token: 0x040001E3 RID: 483
			private GDIPlus.StreamGetHeaderDelegate sghd;

			// Token: 0x040001E4 RID: 484
			private GDIPlus.StreamGetBytesDelegate sgbd;

			// Token: 0x040001E5 RID: 485
			private GDIPlus.StreamSeekDelegate skd;

			// Token: 0x040001E6 RID: 486
			private GDIPlus.StreamPutBytesDelegate spbd;

			// Token: 0x040001E7 RID: 487
			private GDIPlus.StreamCloseDelegate scd;

			// Token: 0x040001E8 RID: 488
			private GDIPlus.StreamSizeDelegate ssd;

			// Token: 0x040001E9 RID: 489
			private byte[] start_buf;

			// Token: 0x040001EA RID: 490
			private int start_buf_pos;

			// Token: 0x040001EB RID: 491
			private int start_buf_len;

			// Token: 0x040001EC RID: 492
			private byte[] managedBuf;
		}
	}
}
