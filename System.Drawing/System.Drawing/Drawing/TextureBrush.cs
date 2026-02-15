using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.TextureBrush" /> class is a <see cref="T:System.Drawing.Brush" /> object that uses an image to fill the interior of a shape. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001D RID: 29
	public sealed class TextureBrush : Brush
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.TextureBrush" /> object that uses the specified image and wrap mode.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> object with which this <see cref="T:System.Drawing.TextureBrush" /> object fills interiors. </param>
		/// <param name="wrapMode">A <see cref="T:System.Drawing.Drawing2D.WrapMode" /> enumeration that specifies how this <see cref="T:System.Drawing.TextureBrush" /> object is tiled. </param>
		// Token: 0x06000087 RID: 135 RVA: 0x000048AC File Offset: 0x00002AAC
		public TextureBrush(Image image, WrapMode wrapMode)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (wrapMode < WrapMode.Tile || wrapMode > WrapMode.Clamp)
			{
				throw new InvalidEnumArgumentException("wrapMode", (int)wrapMode, typeof(WrapMode));
			}
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateTexture(new HandleRef(image, image.nativeImage), (int)wrapMode, out zero));
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004911 File Offset: 0x00002B11
		internal TextureBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.TextureBrush" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.TextureBrush" /> object this method creates, cast as an <see cref="T:System.Object" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000089 RID: 137 RVA: 0x00004920 File Offset: 0x00002B20
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero));
			return new TextureBrush(zero);
		}
	}
}
