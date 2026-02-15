using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies a value that determines the Input Method Editor (IME) status of an object when the object is selected.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D9 RID: 217
	[ComVisible(true)]
	public enum ImeMode
	{
		/// <summary>None (Default).</summary>
		// Token: 0x0400051D RID: 1309
		NoControl,
		/// <summary>The IME is on. This value indicates that the IME is on and characters specific to Chinese or Japanese can be entered. This setting is valid for Japanese, Simplified Chinese, and Traditional Chinese IME only.</summary>
		// Token: 0x0400051E RID: 1310
		On,
		/// <summary>The IME is off. This mode indicates that the IME is off, meaning that the object behaves the same as English entry mode. This setting is valid for Japanese, Simplified Chinese, and Traditional Chinese IME only.</summary>
		// Token: 0x0400051F RID: 1311
		Off,
		/// <summary>The IME is disabled. With this setting, the users cannot turn the IME on from the keyboard, and the IME floating window is hidden.</summary>
		// Token: 0x04000520 RID: 1312
		Disable,
		/// <summary>Hiragana DBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04000521 RID: 1313
		Hiragana,
		/// <summary>Katakana DBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04000522 RID: 1314
		Katakana,
		/// <summary>Katakana SBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04000523 RID: 1315
		KatakanaHalf,
		/// <summary>Alphanumeric double-byte characters. This setting is valid for Korean and Japanese IME only.</summary>
		// Token: 0x04000524 RID: 1316
		AlphaFull,
		/// <summary>Alphanumeric single-byte characters(SBC). This setting is valid for Korean and Japanese IME only.</summary>
		// Token: 0x04000525 RID: 1317
		Alpha,
		/// <summary>Hangul DBC. This setting is valid for the Korean IME only.</summary>
		// Token: 0x04000526 RID: 1318
		HangulFull,
		/// <summary>Hangul SBC. This setting is valid for the Korean IME only.</summary>
		// Token: 0x04000527 RID: 1319
		Hangul,
		/// <summary>Inherits the IME mode of the parent control.</summary>
		// Token: 0x04000528 RID: 1320
		Inherit = -1,
		/// <summary>IME closed. This setting is valid for Chinese IME only.</summary>
		// Token: 0x04000529 RID: 1321
		Close = 11,
		/// <summary>IME on HalfShape. This setting is valid for Chinese IME only.</summary>
		// Token: 0x0400052A RID: 1322
		OnHalf
	}
}
