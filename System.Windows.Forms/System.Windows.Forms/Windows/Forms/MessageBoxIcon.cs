using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants defining which information to display.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000140 RID: 320
	public enum MessageBoxIcon
	{
		/// <summary>The message box contain no symbols.</summary>
		// Token: 0x04000819 RID: 2073
		None,
		/// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
		// Token: 0x0400081A RID: 2074
		Error = 16,
		/// <summary>The message box contains a symbol consisting of a white X in a circle with a red background.</summary>
		// Token: 0x0400081B RID: 2075
		Hand = 16,
		/// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
		// Token: 0x0400081C RID: 2076
		Stop = 16,
		/// <summary>The message box contains a symbol consisting of a question mark in a circle. The question-mark message icon is no longer recommended because it does not clearly represent a specific type of message and because the phrasing of a message as a question could apply to any message type. In addition, users can confuse the message symbol question mark with Help information. Therefore, do not use this question mark message symbol in your message boxes. The system continues to support its inclusion only for backward compatibility.</summary>
		// Token: 0x0400081D RID: 2077
		Question = 32,
		/// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
		// Token: 0x0400081E RID: 2078
		Exclamation = 48,
		/// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
		// Token: 0x0400081F RID: 2079
		Warning = 48,
		/// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
		// Token: 0x04000820 RID: 2080
		Asterisk = 64,
		/// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
		// Token: 0x04000821 RID: 2081
		Information = 64
	}
}
