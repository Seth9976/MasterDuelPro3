using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Contains information about how bitmap and metafile colors are manipulated during rendering. </summary>
	// Token: 0x02000087 RID: 135
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ImageAttributes : ICloneable, IDisposable
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		internal void SetNativeImageAttributes(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.nativeImageAttributes = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class.</summary>
		// Token: 0x06000461 RID: 1121 RVA: 0x0000DD18 File Offset: 0x0000BF18
		public ImageAttributes()
		{
			IntPtr zero = IntPtr.Zero;
			int num = GDIPlus.GdipCreateImageAttributes(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeImageAttributes(zero);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000DD4A File Offset: 0x0000BF4A
		internal ImageAttributes(IntPtr newNativeImageAttributes)
		{
			this.SetNativeImageAttributes(newNativeImageAttributes);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000463 RID: 1123 RVA: 0x0000DD59 File Offset: 0x0000BF59
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000DD68 File Offset: 0x0000BF68
		private void Dispose(bool disposing)
		{
			if (this.nativeImageAttributes != IntPtr.Zero)
			{
				try
				{
					GDIPlus.GdipDisposeImageAttributes(new HandleRef(this, this.nativeImageAttributes));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeImageAttributes = IntPtr.Zero;
				}
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		~ImageAttributes()
		{
			this.Dispose(false);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object this class creates, cast as an object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000466 RID: 1126 RVA: 0x0000DE00 File Offset: 0x0000C000
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = GDIPlus.GdipCloneImageAttributes(new HandleRef(this, this.nativeImageAttributes), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new ImageAttributes(zero);
		}

		/// <summary>Sets the color-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000467 RID: 1127 RVA: 0x0000DE37 File Offset: 0x0000C037
		public void SetColorMatrix(ColorMatrix newColorMatrix)
		{
			this.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix for a specified category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix. </param>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is set. </param>
		// Token: 0x06000468 RID: 1128 RVA: 0x0000DE44 File Offset: 0x0000C044
		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, true, newColorMatrix, null, mode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color key for the default category.</summary>
		/// <param name="colorLow">The low color-key value. </param>
		/// <param name="colorHigh">The high color-key value. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000469 RID: 1129 RVA: 0x0000DE72 File Offset: 0x0000C072
		public void SetColorKey(Color colorLow, Color colorHigh)
		{
			this.SetColorKey(colorLow, colorHigh, ColorAdjustType.Default);
		}

		/// <summary>Sets the color key (transparency range) for a specified category.</summary>
		/// <param name="colorLow">The low color-key value. </param>
		/// <param name="colorHigh">The high color-key value. </param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is set. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600046A RID: 1130 RVA: 0x0000DE80 File Offset: 0x0000C080
		public void SetColorKey(Color colorLow, Color colorHigh, ColorAdjustType type)
		{
			int num = colorLow.ToArgb();
			int num2 = colorHigh.ToArgb();
			int num3 = GDIPlus.GdipSetImageAttributesColorKeys(new HandleRef(this, this.nativeImageAttributes), type, true, num, num2);
			if (num3 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num3);
			}
		}

		// Token: 0x04000267 RID: 615
		internal IntPtr nativeImageAttributes;
	}
}
