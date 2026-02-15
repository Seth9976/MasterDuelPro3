using System;

namespace UnityEngine
{
	// Token: 0x020001E2 RID: 482
	public enum TouchScreenKeyboardType
	{
		// Token: 0x040006DC RID: 1756
		Default,
		// Token: 0x040006DD RID: 1757
		ASCIICapable,
		// Token: 0x040006DE RID: 1758
		NumbersAndPunctuation,
		// Token: 0x040006DF RID: 1759
		URL,
		// Token: 0x040006E0 RID: 1760
		NumberPad,
		// Token: 0x040006E1 RID: 1761
		PhonePad,
		// Token: 0x040006E2 RID: 1762
		NamePhonePad,
		// Token: 0x040006E3 RID: 1763
		EmailAddress,
		// Token: 0x040006E4 RID: 1764
		[Obsolete("Wii U is no longer supported as of Unity 2018.1.")]
		NintendoNetworkAccount,
		// Token: 0x040006E5 RID: 1765
		Social,
		// Token: 0x040006E6 RID: 1766
		Search,
		// Token: 0x040006E7 RID: 1767
		DecimalPad,
		// Token: 0x040006E8 RID: 1768
		OneTimeCode
	}
}
