using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
	/// <summary>Provides a base class for installed and private font collections. </summary>
	// Token: 0x0200007A RID: 122
	public abstract class FontCollection : IDisposable
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x0000D7AD File Offset: 0x0000B9AD
		internal FontCollection()
		{
			this._nativeFontCollection = IntPtr.Zero;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Text.FontCollection" />.</summary>
		// Token: 0x06000449 RID: 1097 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Text.FontCollection" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600044A RID: 1098 RVA: 0x000081F2 File Offset: 0x000063F2
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets the array of <see cref="T:System.Drawing.FontFamily" /> objects associated with this <see cref="T:System.Drawing.Text.FontCollection" />. </summary>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		public FontFamily[] Families
		{
			get
			{
				int num = 0;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyCount(new HandleRef(this, this._nativeFontCollection), out num));
				IntPtr[] array = new IntPtr[num];
				int num2 = 0;
				SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipGetFontCollectionFamilyList(new HandleRef(this, this._nativeFontCollection), num, array, out num2));
				FontFamily[] array2 = new FontFamily[num2];
				for (int i = 0; i < num2; i++)
				{
					IntPtr intPtr;
					GDIPlus.GdipCloneFontFamily(new HandleRef(null, array[i]), out intPtr);
					array2[i] = new FontFamily(intPtr);
				}
				return array2;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000D850 File Offset: 0x0000BA50
		~FontCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x04000225 RID: 549
		internal IntPtr _nativeFontCollection;
	}
}
