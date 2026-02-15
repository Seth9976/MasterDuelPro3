using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of action used to raise the <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000180 RID: 384
	[ComVisible(true)]
	public enum ScrollEventType
	{
		/// <summary>The scroll box was moved a small distance. The user clicked the left(horizontal) or top(vertical) scroll arrow, or pressed the UP ARROW key.</summary>
		// Token: 0x04000957 RID: 2391
		SmallDecrement,
		/// <summary>The scroll box was moved a small distance. The user clicked the right(horizontal) or bottom(vertical) scroll arrow, or pressed the DOWN ARROW key.</summary>
		// Token: 0x04000958 RID: 2392
		SmallIncrement,
		/// <summary>The scroll box moved a large distance. The user clicked the scroll bar to the left(horizontal) or above(vertical) the scroll box, or pressed the PAGE UP key.</summary>
		// Token: 0x04000959 RID: 2393
		LargeDecrement,
		/// <summary>The scroll box moved a large distance. The user clicked the scroll bar to the right(horizontal) or below(vertical) the scroll box, or pressed the PAGE DOWN key.</summary>
		// Token: 0x0400095A RID: 2394
		LargeIncrement,
		/// <summary>The scroll box was moved.</summary>
		// Token: 0x0400095B RID: 2395
		ThumbPosition,
		/// <summary>The scroll box is currently being moved.</summary>
		// Token: 0x0400095C RID: 2396
		ThumbTrack,
		/// <summary>The scroll box was moved to the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> position.</summary>
		// Token: 0x0400095D RID: 2397
		First,
		/// <summary>The scroll box was moved to the <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> position.</summary>
		// Token: 0x0400095E RID: 2398
		Last,
		/// <summary>The scroll box has stopped moving.</summary>
		// Token: 0x0400095F RID: 2399
		EndScroll
	}
}
