using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of scroll arrow to draw on a scroll bar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017D RID: 381
	public enum ScrollButton
	{
		/// <summary>A minimum-scroll arrow.</summary>
		// Token: 0x0400094C RID: 2380
		Min,
		/// <summary>An up-scroll arrow.</summary>
		// Token: 0x0400094D RID: 2381
		Up = 0,
		/// <summary>A down-scroll arrow.</summary>
		// Token: 0x0400094E RID: 2382
		Down,
		/// <summary>A left-scroll arrow.</summary>
		// Token: 0x0400094F RID: 2383
		Left,
		/// <summary>A right-scroll arrow.</summary>
		// Token: 0x04000950 RID: 2384
		Right,
		/// <summary>A maximum-scroll arrow.</summary>
		// Token: 0x04000951 RID: 2385
		Max = 3
	}
}
