using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace System.Drawing
{
	/// <summary>Encapsulates a GDI+ drawing surface. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000043 RID: 67
	public sealed class Graphics : MarshalByRefObject, IDisposable, IDeviceContext
	{
		// Token: 0x06000269 RID: 617 RVA: 0x000084D6 File Offset: 0x000066D6
		internal Graphics(IntPtr nativeGraphics)
		{
			this.nativeObject = nativeGraphics;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000084F0 File Offset: 0x000066F0
		internal Graphics(IntPtr nativeGraphics, Image image)
			: this(nativeGraphics)
		{
			Metafile metafile = image as Metafile;
			if (metafile != null)
			{
				this._metafileHolder = metafile.AddMetafileHolder();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000851C File Offset: 0x0000671C
		~Graphics()
		{
			this.Dispose();
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00008548 File Offset: 0x00006748
		internal static float systemDpiX
		{
			get
			{
				if (Graphics.defDpiX == 0f)
				{
					Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
					Graphics.defDpiX = graphics.DpiX;
					Graphics.defDpiY = graphics.DpiY;
				}
				return Graphics.defDpiX;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000857C File Offset: 0x0000677C
		internal static float systemDpiY
		{
			get
			{
				if (Graphics.defDpiY == 0f)
				{
					Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
					Graphics.defDpiX = graphics.DpiX;
					Graphics.defDpiY = graphics.DpiY;
				}
				return Graphics.defDpiY;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000085B0 File Offset: 0x000067B0
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeObject;
			}
		}

		/// <summary>Clears the entire drawing surface and fills it with the specified background color.</summary>
		/// <param name="color">
		///   <see cref="T:System.Drawing.Color" /> structure that represents the background color of the drawing surface. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600026F RID: 623 RVA: 0x000085B8 File Offset: 0x000067B8
		public void Clear(Color color)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipGraphicsClear(this.nativeObject, color.ToArgb()));
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000270 RID: 624 RVA: 0x000085D4 File Offset: 0x000067D4
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.deviceContextHdc != IntPtr.Zero)
				{
					this.ReleaseHdc();
				}
				if (GDIPlus.UseCarbonDrawable || GDIPlus.UseCocoaDrawable)
				{
					this.Flush();
					if (this.maccontext != null)
					{
						this.maccontext.Release();
					}
				}
				Status status = GDIPlus.GdipDeleteGraphics(this.nativeObject);
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
				if (this._metafileHolder != null)
				{
					Metafile.MetafileHolder metafileHolder = this._metafileHolder;
					this._metafileHolder = null;
					metafileHolder.GraphicsDisposed();
				}
				this.disposed = true;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> within the area specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="icon">
		///   <see cref="T:System.Drawing.Icon" /> to draw. </param>
		/// <param name="targetRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image on the display surface. The image contained in the <paramref name="icon" /> parameter is scaled to the dimensions of this rectangular area. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000271 RID: 625 RVA: 0x0000866A File Offset: 0x0000686A
		public void DrawIcon(Icon icon, Rectangle targetRect)
		{
			if (icon == null)
			{
				throw new ArgumentNullException("icon");
			}
			this.DrawImage(icon.GetInternalBitmap(), targetRect);
		}

		/// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> at the specified coordinates.</summary>
		/// <param name="icon">
		///   <see cref="T:System.Drawing.Icon" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000272 RID: 626 RVA: 0x00008687 File Offset: 0x00006887
		public void DrawIcon(Icon icon, int x, int y)
		{
			if (icon == null)
			{
				throw new ArgumentNullException("icon");
			}
			this.DrawImage(icon.GetInternalBitmap(), x, y);
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the location of the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000273 RID: 627 RVA: 0x000086A5 File Offset: 0x000068A5
		public void DrawImage(Image image, Point point)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.DrawImage(image, point.X, point.Y);
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000274 RID: 628 RVA: 0x000086CA File Offset: 0x000068CA
		public void DrawImage(Image image, Rectangle rect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws the specified image, using its original physical size, at the location specified by a coordinate pair.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000275 RID: 629 RVA: 0x000086FD File Offset: 0x000068FD
		public void DrawImage(Image image, int x, int y)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageI(this.nativeObject, image.NativeObject, x, y));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000276 RID: 630 RVA: 0x00008728 File Offset: 0x00006928
		public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="width">Width of the drawn image. </param>
		/// <param name="height">Height of the drawn image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000277 RID: 631 RVA: 0x0000879E File Offset: 0x0000699E
		public void DrawImage(Image image, int x, int y, int width, int height)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectI(this.nativeObject, image.nativeObject, x, y, width, height));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle. </param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw. </param>
		/// <param name="srcWidth">Width of the portion of the source image to draw. </param>
		/// <param name="srcHeight">Height of the portion of the source image to draw. </param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle. </param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000278 RID: 632 RVA: 0x000087CC File Offset: 0x000069CC
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws a specified image using its original physical size at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image. </param>
		/// <param name="width">Not used. </param>
		/// <param name="height">Not used. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000279 RID: 633 RVA: 0x0000883C File Offset: 0x00006A3C
		public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (width <= 0 || height <= 0)
			{
				return;
			}
			using (Image image2 = new Bitmap(width, height))
			{
				using (Graphics graphics = Graphics.FromImage(image2))
				{
					graphics.DrawImage(image, 0, 0, image.Width, image.Height);
					this.DrawImage(image2, x, y, width, height);
				}
			}
		}

		/// <summary>Draws the specified image without scaling and clips it, if necessary, to fit in the specified rectangle.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600027A RID: 634 RVA: 0x000088C8 File Offset: 0x00006AC8
		public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			int num = ((image.Width > rect.Width) ? rect.Width : image.Width);
			int num2 = ((image.Height > rect.Height) ? rect.Height : image.Height);
			this.DrawImageUnscaled(image, rect.X, rect.Y, num, num2);
		}

		/// <summary>Draws a line connecting two <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line. </param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the first point to connect. </param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the second point to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600027B RID: 635 RVA: 0x00008938 File Offset: 0x00006B38
		public void DrawLine(Pen pen, Point pt1, Point pt2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLineI(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line. </param>
		/// <param name="x1">The x-coordinate of the first point. </param>
		/// <param name="y1">The y-coordinate of the first point. </param>
		/// <param name="x2">The x-coordinate of the second point. </param>
		/// <param name="y2">The y-coordinate of the second point. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600027C RID: 636 RVA: 0x00008985 File Offset: 0x00006B85
		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLineI(this.nativeObject, pen.NativePen, x1, y1, x2, y2));
		}

		/// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line. </param>
		/// <param name="x1">The x-coordinate of the first point. </param>
		/// <param name="y1">The y-coordinate of the first point. </param>
		/// <param name="x2">The x-coordinate of the second point. </param>
		/// <param name="y2">The y-coordinate of the second point. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600027D RID: 637 RVA: 0x000089B4 File Offset: 0x00006BB4
		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (!float.IsNaN(x1) && !float.IsNaN(y1) && !float.IsNaN(x2) && !float.IsNaN(y2))
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDrawLine(this.nativeObject, pen.NativePen, x1, y1, x2, y2));
			}
		}

		/// <summary>Draws a series of line segments that connect an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line segments. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the points to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600027E RID: 638 RVA: 0x00008A0D File Offset: 0x00006C0D
		public void DrawLines(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLinesI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a polygon defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the polygon. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600027F RID: 639 RVA: 0x00008A45 File Offset: 0x00006C45
		public void DrawPolygon(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPolygonI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle. </param>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to draw. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000280 RID: 640 RVA: 0x00008A7D File Offset: 0x00006C7D
		public void DrawRectangle(Pen pen, Rectangle rect)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
		}

		/// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw. </param>
		/// <param name="width">Width of the rectangle to draw. </param>
		/// <param name="height">Height of the rectangle to draw. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000281 RID: 641 RVA: 0x00008AB0 File Offset: 0x00006CB0
		public void DrawRectangle(Pen pen, int x, int y, int width, int height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectangleI(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="layoutRectangle">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000282 RID: 642 RVA: 0x00008ADC File Offset: 0x00006CDC
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
		{
			this.DrawString(s, font, brush, layoutRectangle, null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000283 RID: 643 RVA: 0x00008AEA File Offset: 0x00006CEA
		public void DrawString(string s, Font font, Brush brush, PointF point)
		{
			this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000284 RID: 644 RVA: 0x00008B13 File Offset: 0x00006D13
		public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
		{
			this.DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), format);
		}

		/// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text. </param>
		/// <param name="layoutRectangle">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="s" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000285 RID: 645 RVA: 0x00008B34 File Offset: 0x00006D34
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
		{
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (s == null || s.Length == 0)
			{
				return;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawString(this.nativeObject, s, s.Length, font.NativeObject, ref layoutRectangle, (format != null) ? format.NativeObject : IntPtr.Zero, brush.NativeBrush));
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000286 RID: 646 RVA: 0x00008BA0 File Offset: 0x00006DA0
		public void FillEllipse(Brush brush, Rectangle rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse. </param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse. </param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000287 RID: 647 RVA: 0x00008BD3 File Offset: 0x00006DD3
		public void FillEllipse(Brush brush, int x, int y, int width, int height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillEllipseI(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000288 RID: 648 RVA: 0x00008BFF File Offset: 0x00006DFF
		public void FillPolygon(Brush brush, PointF[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygon2(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.Point" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon to fill. </param>
		/// <param name="fillMode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines the style of the fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.-or-<paramref name="points" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000289 RID: 649 RVA: 0x00008C37 File Offset: 0x00006E37
		public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygonI(this.nativeObject, brush.NativeBrush, points, points.Length, fillMode));
		}

		/// <summary>Fills the interior of a rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600028A RID: 650 RVA: 0x00008C70 File Offset: 0x00006E70
		public void FillRectangle(Brush brush, Rectangle rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill. </param>
		/// <param name="width">Width of the rectangle to fill. </param>
		/// <param name="height">Height of the rectangle to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600028B RID: 651 RVA: 0x00008CA3 File Offset: 0x00006EA3
		public void FillRectangle(Brush brush, int x, int y, int width, int height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectangleI(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill. </param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill. </param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill. </param>
		/// <param name="width">Width of the rectangle to fill. </param>
		/// <param name="height">Height of the rectangle to fill. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600028C RID: 652 RVA: 0x00008CCF File Offset: 0x00006ECF
		public void FillRectangle(Brush brush, float x, float y, float width, float height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectangle(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Forces execution of all pending graphics operations and returns immediately without waiting for the operations to finish.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600028D RID: 653 RVA: 0x00008CFB File Offset: 0x00006EFB
		public void Flush()
		{
			this.Flush(FlushIntention.Flush);
		}

		/// <summary>Forces execution of all pending graphics operations with the method waiting or not waiting, as specified, to return before the operations finish.</summary>
		/// <param name="intention">Member of the <see cref="T:System.Drawing.Drawing2D.FlushIntention" /> enumeration that specifies whether the method returns immediately or waits for any existing operations to finish. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600028E RID: 654 RVA: 0x00008D04 File Offset: 0x00006F04
		public void Flush(FlushIntention intention)
		{
			if (this.nativeObject == IntPtr.Zero)
			{
				return;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFlush(this.nativeObject, intention));
			if (this.maccontext != null)
			{
				this.maccontext.Synchronize();
			}
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a device context.</summary>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified device context.</returns>
		/// <param name="hdc">Handle to a device context. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600028F RID: 655 RVA: 0x00008D40 File Offset: 0x00006F40
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHdc(IntPtr hdc)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFromHDC(hdc, out intPtr));
			return new Graphics(intPtr);
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> for the specified device context.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> for the specified device context.</returns>
		/// <param name="hdc">Handle to a device context. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000290 RID: 656 RVA: 0x00008D60 File Offset: 0x00006F60
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHdcInternal(IntPtr hdc)
		{
			GDIPlus.Display = hdc;
			return null;
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a window.</summary>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified window handle.</returns>
		/// <param name="hwnd">Handle to a window. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000291 RID: 657 RVA: 0x00008D6C File Offset: 0x00006F6C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHwnd(IntPtr hwnd)
		{
			if (GDIPlus.UseCocoaDrawable)
			{
				if (hwnd == IntPtr.Zero)
				{
					throw new NotSupportedException("Opening display graphics is not supported");
				}
				CocoaContext cgcontextForNSView = MacSupport.GetCGContextForNSView(hwnd);
				IntPtr intPtr;
				GDIPlus.GdipCreateFromContext_macosx(cgcontextForNSView.ctx, cgcontextForNSView.width, cgcontextForNSView.height, out intPtr);
				return new Graphics(intPtr)
				{
					maccontext = cgcontextForNSView
				};
			}
			else
			{
				IntPtr intPtr;
				if (GDIPlus.UseCarbonDrawable)
				{
					CarbonContext cgcontextForView = MacSupport.GetCGContextForView(hwnd);
					GDIPlus.GdipCreateFromContext_macosx(cgcontextForView.ctx, cgcontextForView.width, cgcontextForView.height, out intPtr);
					return new Graphics(intPtr)
					{
						maccontext = cgcontextForView
					};
				}
				if (GDIPlus.UseX11Drawable)
				{
					if (GDIPlus.Display == IntPtr.Zero)
					{
						GDIPlus.Display = GDIPlus.XOpenDisplay(IntPtr.Zero);
						if (GDIPlus.Display == IntPtr.Zero)
						{
							throw new NotSupportedException("Could not open display (X-Server required. Check your DISPLAY environment variable)");
						}
					}
					if (hwnd == IntPtr.Zero)
					{
						hwnd = GDIPlus.XRootWindow(GDIPlus.Display, GDIPlus.XDefaultScreen(GDIPlus.Display));
					}
					return Graphics.FromXDrawable(hwnd, GDIPlus.Display);
				}
				GDIPlus.CheckStatus(GDIPlus.GdipCreateFromHWND(hwnd, out intPtr));
				return new Graphics(intPtr);
			}
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified <see cref="T:System.Drawing.Image" />.</returns>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Graphics" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <exception cref="T:System.Exception">
		///   <paramref name="image" /> has an indexed pixel format or its format is undefined.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000292 RID: 658 RVA: 0x00008E88 File Offset: 0x00007088
		public static Graphics FromImage(Image image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if ((image.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
			{
				throw new Exception(Locale.GetText("Cannot create Graphics from an indexed bitmap."));
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageGraphicsContext(image.nativeObject, out intPtr));
			Graphics graphics = new Graphics(intPtr, image);
			if (GDIPlus.RunningOnUnix())
			{
				Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
				GDIPlus.GdipSetVisibleClip_linux(graphics.NativeObject, ref rectangle);
			}
			return graphics;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00008F08 File Offset: 0x00007108
		internal static Graphics FromXDrawable(IntPtr drawable, IntPtr display)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFromXDrawable_linux(drawable, display, out intPtr));
			return new Graphics(intPtr);
		}

		/// <summary>Gets the handle to the device context associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>Handle to the device context associated with this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000294 RID: 660 RVA: 0x00008F29 File Offset: 0x00007129
		public IntPtr GetHdc()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipGetDC(this.nativeObject, out this.deviceContextHdc));
			return this.deviceContextHdc;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008F48 File Offset: 0x00007148
		private SizeF GdipMeasureString(IntPtr graphics, string text, Font font, ref RectangleF layoutRect, IntPtr stringFormat)
		{
			if (text == null || text.Length == 0)
			{
				return SizeF.Empty;
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			RectangleF rectangleF = default(RectangleF);
			GDIPlus.CheckStatus(GDIPlus.GdipMeasureString(this.nativeObject, text, text.Length, font.NativeObject, ref layoutRect, stringFormat, out rectangleF, null, null));
			return new SizeF(rectangleF.Width, rectangleF.Height);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000296 RID: 662 RVA: 0x00008FB6 File Offset: 0x000071B6
		public SizeF MeasureString(string text, Font font)
		{
			return this.MeasureString(text, font, SizeF.Empty);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> within the specified layout area.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> defines the text format of the string. </param>
		/// <param name="layoutArea">
		///   <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000297 RID: 663 RVA: 0x00008FC8 File Offset: 0x000071C8
		public SizeF MeasureString(string text, Font font, SizeF layoutArea)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, layoutArea.Width, layoutArea.Height);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, IntPtr.Zero);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> defines the text format of the string. </param>
		/// <param name="layoutArea">
		///   <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text. </param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000298 RID: 664 RVA: 0x0000900C File Offset: 0x0000720C
		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, layoutArea.Width, layoutArea.Height);
			IntPtr intPtr = ((stringFormat == null) ? IntPtr.Zero : stringFormat.NativeObject);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, intPtr);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <param name="text">String to measure. </param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string. </param>
		/// <param name="width">Maximum width of the string. </param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000299 RID: 665 RVA: 0x0000905C File Offset: 0x0000725C
		public SizeF MeasureString(string text, Font font, int width, StringFormat format)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, (float)width, 2.1474836E+09f);
			IntPtr intPtr = ((format == null) ? IntPtr.Zero : format.NativeObject);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, intPtr);
		}

		/// <summary>Releases a device context handle obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="hdc">Handle to a device context obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600029A RID: 666 RVA: 0x000090A5 File Offset: 0x000072A5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void ReleaseHdc(IntPtr hdc)
		{
			this.ReleaseHdcInternal(hdc);
		}

		/// <summary>Releases a device context handle obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600029B RID: 667 RVA: 0x000090AE File Offset: 0x000072AE
		public void ReleaseHdc()
		{
			this.ReleaseHdcInternal(this.deviceContextHdc);
		}

		/// <summary>Releases a handle to a device context.</summary>
		/// <param name="hdc">Handle to a device context. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600029C RID: 668 RVA: 0x000090BC File Offset: 0x000072BC
		[MonoLimitation("Can only be used when hdc was provided by Graphics.GetHdc() method")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ReleaseHdcInternal(IntPtr hdc)
		{
			Status status = Status.InvalidParameter;
			if (hdc == this.deviceContextHdc)
			{
				status = GDIPlus.GdipReleaseDC(this.nativeObject, this.deviceContextHdc);
				this.deviceContextHdc = IntPtr.Zero;
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Resets the clip region of this <see cref="T:System.Drawing.Graphics" /> to an infinite region.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600029D RID: 669 RVA: 0x000090FC File Offset: 0x000072FC
		public void ResetClip()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetClip(this.nativeObject));
		}

		/// <summary>Resets the world transformation matrix of this <see cref="T:System.Drawing.Graphics" /> to the identity matrix.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600029E RID: 670 RVA: 0x0000910E File Offset: 0x0000730E
		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetWorldTransform(this.nativeObject));
		}

		/// <summary>Restores the state of this <see cref="T:System.Drawing.Graphics" /> to the state represented by a <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.</summary>
		/// <param name="gstate">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that represents the state to which to restore this <see cref="T:System.Drawing.Graphics" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600029F RID: 671 RVA: 0x00009120 File Offset: 0x00007320
		public void Restore(GraphicsState gstate)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRestoreGraphics(this.nativeObject, (uint)gstate.nativeState));
		}

		/// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="angle">Angle of rotation in degrees. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002A0 RID: 672 RVA: 0x00009138 File Offset: 0x00007338
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
		/// <param name="angle">Angle of rotation in degrees. </param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the rotation is appended or prepended to the matrix transformation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002A1 RID: 673 RVA: 0x00009142 File Offset: 0x00007342
		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateWorldTransform(this.nativeObject, angle, order));
		}

		/// <summary>Saves the current state of this <see cref="T:System.Drawing.Graphics" /> and identifies the saved state with a <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that represents the saved state of this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002A2 RID: 674 RVA: 0x00009158 File Offset: 0x00007358
		public GraphicsState Save()
		{
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipSaveGraphics(this.nativeObject, out num));
			return new GraphicsState((int)num);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the new clip region. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002A3 RID: 675 RVA: 0x0000917D File Offset: 0x0000737D
		public void SetClip(Rectangle rect)
		{
			this.SetClip(rect, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure to combine. </param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002A4 RID: 676 RVA: 0x00009187 File Offset: 0x00007387
		public void SetClip(Rectangle rect, CombineMode combineMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> to combine. </param>
		/// <param name="combineMode">Member from the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002A5 RID: 677 RVA: 0x000091B6 File Offset: 0x000073B6
		public void SetClip(Region region, CombineMode combineMode)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRegion(this.nativeObject, region.NativeObject, combineMode));
		}

		/// <summary>Changes the origin of the coordinate system by prepending the specified translation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="dx">The x-coordinate of the translation. </param>
		/// <param name="dy">The y-coordinate of the translation. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002A6 RID: 678 RVA: 0x000091DD File Offset: 0x000073DD
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Changes the origin of the coordinate system by applying the specified translation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
		/// <param name="dx">The x-coordinate of the translation. </param>
		/// <param name="dy">The y-coordinate of the translation. </param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the translation is prepended or appended to the transformation matrix. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002A7 RID: 679 RVA: 0x000091E8 File Offset: 0x000073E8
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateWorldTransform(this.nativeObject, dx, dy, order));
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Region" /> that limits the drawing region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Region" /> that limits the portion of this <see cref="T:System.Drawing.Graphics" /> that is currently available for drawing.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00009200 File Offset: 0x00007400
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000922A File Offset: 0x0000742A
		public Region Clip
		{
			get
			{
				Region region = new Region();
				GDIPlus.CheckStatus(GDIPlus.GdipGetClip(this.nativeObject, region.NativeObject));
				return region;
			}
			set
			{
				this.SetClip(value, CombineMode.Replace);
			}
		}

		/// <summary>Gets the horizontal resolution of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>The value, in dots per inch, for the horizontal resolution supported by this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00009234 File Offset: 0x00007434
		public float DpiX
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetDpiX(this.nativeObject, out num));
				return num;
			}
		}

		/// <summary>Gets the vertical resolution of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>The value, in dots per inch, for the vertical resolution supported by this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00009254 File Offset: 0x00007454
		public float DpiY
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetDpiY(this.nativeObject, out num));
				return num;
			}
		}

		/// <summary>Gets or sets a copy of the geometric world transformation for this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the geometric world transformation for this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00009274 File Offset: 0x00007474
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetWorldTransform(this.nativeObject, matrix.nativeMatrix));
				return matrix;
			}
		}

		/// <summary>Gets the bounding rectangle of the visible clipping region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents a bounding rectangle for the visible clipping region of this <see cref="T:System.Drawing.Graphics" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Drawing.Printing.PrintingPermission, System.Drawing, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000092A0 File Offset: 0x000074A0
		public RectangleF VisibleClipBounds
		{
			get
			{
				RectangleF rectangleF;
				GDIPlus.CheckStatus(GDIPlus.GdipGetVisibleClipBounds(this.nativeObject, out rectangleF));
				return rectangleF;
			}
		}

		// Token: 0x04000149 RID: 329
		internal IntPtr nativeObject = IntPtr.Zero;

		// Token: 0x0400014A RID: 330
		internal IMacContext maccontext;

		// Token: 0x0400014B RID: 331
		private bool disposed;

		// Token: 0x0400014C RID: 332
		private static float defDpiX;

		// Token: 0x0400014D RID: 333
		private static float defDpiY;

		// Token: 0x0400014E RID: 334
		private IntPtr deviceContextHdc;

		// Token: 0x0400014F RID: 335
		private Metafile.MetafileHolder _metafileHolder;

		/// <summary>Provides a callback method for deciding when the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should prematurely cancel execution and stop drawing an image.</summary>
		/// <returns>This method returns true if it decides that the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should prematurely stop execution. Otherwise it returns false to indicate that the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should continue execution.</returns>
		/// <param name="callbackdata">Internal pointer that specifies data for the callback method. This parameter is not passed by all <see cref="Overload:System.Drawing.Graphics.DrawImage" /> overloads. You can test for its absence by checking for the value <see cref="F:System.IntPtr.Zero" />. </param>
		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x060002AF RID: 687
		public delegate bool DrawImageAbort(IntPtr callbackdata);
	}
}
