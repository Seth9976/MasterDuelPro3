using System;

namespace System.Drawing.Text
{
	/// <summary>Provides a collection of font families built from font files that are provided by the client application.</summary>
	// Token: 0x0200007E RID: 126
	public sealed class PrivateFontCollection : FontCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Text.PrivateFontCollection" /> class. </summary>
		// Token: 0x0600044E RID: 1102 RVA: 0x0000D898 File Offset: 0x0000BA98
		public PrivateFontCollection()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipNewPrivateFontCollection(out this._nativeFontCollection));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		protected override void Dispose(bool disposing)
		{
			if (this._nativeFontCollection != IntPtr.Zero)
			{
				GDIPlus.GdipDeletePrivateFontCollection(ref this._nativeFontCollection);
				this._nativeFontCollection = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}
	}
}
