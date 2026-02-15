using System;

namespace System.Drawing.Text
{
	/// <summary>Represents the fonts installed on the system. This class cannot be inherited. </summary>
	// Token: 0x0200007D RID: 125
	public sealed class InstalledFontCollection : FontCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Text.InstalledFontCollection" /> class. </summary>
		// Token: 0x0600044D RID: 1101 RVA: 0x0000D880 File Offset: 0x0000BA80
		public InstalledFontCollection()
		{
			SafeNativeMethods.Gdip.CheckStatus(GDIPlus.GdipNewInstalledFontCollection(out this._nativeFontCollection));
		}
	}
}
