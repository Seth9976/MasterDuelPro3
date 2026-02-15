using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the user action that is required to activate items in a list view control and the feedback that is given as the user moves the mouse pointer over an item.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E5 RID: 229
	public enum ItemActivation
	{
		/// <summary>The user must single-click to activate items. The cursor changes to a hand pointer cursor, and the item text changes color as the user moves the mouse pointer over the item.</summary>
		// Token: 0x0400055D RID: 1373
		OneClick = 1,
		/// <summary>The user must double-click to activate items. No feedback is given as the user moves the mouse pointer over an item.</summary>
		// Token: 0x0400055E RID: 1374
		Standard = 0,
		/// <summary>The user must click an item twice to activate it. This is different from the standard double-click because the two clicks can have any duration between them. The item text changes color as the user moves the mouse pointer over the item.</summary>
		// Token: 0x0400055F RID: 1375
		TwoClick = 2
	}
}
