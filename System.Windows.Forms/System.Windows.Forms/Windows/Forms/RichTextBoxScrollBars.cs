using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of scroll bars to display in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000175 RID: 373
	public enum RichTextBoxScrollBars
	{
		/// <summary>No scroll bars are displayed.</summary>
		// Token: 0x04000909 RID: 2313
		None,
		/// <summary>Display a horizontal scroll bar only when text is longer than the width of the control.</summary>
		// Token: 0x0400090A RID: 2314
		Horizontal,
		/// <summary>Display a vertical scroll bar only when text is longer than the height of the control.</summary>
		// Token: 0x0400090B RID: 2315
		Vertical,
		/// <summary>Display both a horizontal and a vertical scroll bar when needed.</summary>
		// Token: 0x0400090C RID: 2316
		Both,
		/// <summary>Always display a horizontal scroll bar.</summary>
		// Token: 0x0400090D RID: 2317
		ForcedHorizontal = 17,
		/// <summary>Always display a vertical scroll bar.</summary>
		// Token: 0x0400090E RID: 2318
		ForcedVertical,
		/// <summary>Always display both a horizontal and a vertical scroll bar.</summary>
		// Token: 0x0400090F RID: 2319
		ForcedBoth
	}
}
