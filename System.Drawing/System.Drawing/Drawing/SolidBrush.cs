using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Defines a brush of a single color. Brushes are used to fill graphics shapes, such as rectangles, ellipses, pies, polygons, and paths. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000017 RID: 23
	public sealed class SolidBrush : Brush
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.SolidBrush" /> object of the specified color.</summary>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that represents the color of this brush. </param>
		// Token: 0x06000055 RID: 85 RVA: 0x00004578 File Offset: 0x00002778
		public SolidBrush(Color color)
		{
			this._color = color;
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCreateSolidFill(this._color.ToArgb(), out zero));
			base.SetNativeBrushInternal(zero);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000045C1 File Offset: 0x000027C1
		internal SolidBrush(Color color, bool immutable)
			: this(color)
		{
			this._immutable = immutable;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000045D1 File Offset: 0x000027D1
		internal SolidBrush(IntPtr nativeBrush)
		{
			base.SetNativeBrushInternal(nativeBrush);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.SolidBrush" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.SolidBrush" /> object that this method creates.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000058 RID: 88 RVA: 0x000045EC File Offset: 0x000027EC
		public override object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipCloneBrush(new HandleRef(this, base.NativeBrush), out zero));
			return new SolidBrush(zero);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000461D File Offset: 0x0000281D
		protected override void Dispose(bool disposing)
		{
			if (!disposing)
			{
				this._immutable = false;
			}
			else if (this._immutable)
			{
				throw new ArgumentException(SR.Format("Changes cannot be made to {0} because permissions are not valid.", new object[] { "Brush" }));
			}
			base.Dispose(disposing);
		}

		// Token: 0x040000D7 RID: 215
		private Color _color = Color.Empty;

		// Token: 0x040000D8 RID: 216
		private bool _immutable;
	}
}
