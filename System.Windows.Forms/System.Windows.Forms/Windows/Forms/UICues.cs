using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the state of the user interface.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000209 RID: 521
	[Flags]
	public enum UICues
	{
		/// <summary>No change was made.</summary>
		// Token: 0x04000D5F RID: 3423
		None = 0,
		/// <summary>Focus rectangles are displayed after the change.</summary>
		// Token: 0x04000D60 RID: 3424
		ShowFocus = 1,
		/// <summary>Keyboard cues are underlined after the change.</summary>
		// Token: 0x04000D61 RID: 3425
		ShowKeyboard = 2,
		/// <summary>Focus rectangles are displayed and keyboard cues are underlined after the change.</summary>
		// Token: 0x04000D62 RID: 3426
		Shown = 3,
		/// <summary>The state of the focus cues has changed.</summary>
		// Token: 0x04000D63 RID: 3427
		ChangeFocus = 4,
		/// <summary>The state of the keyboard cues has changed.</summary>
		// Token: 0x04000D64 RID: 3428
		ChangeKeyboard = 8,
		/// <summary>The state of the focus cues and keyboard cues has changed.</summary>
		// Token: 0x04000D65 RID: 3429
		Changed = 12
	}
}
