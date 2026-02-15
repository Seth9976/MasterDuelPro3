using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies what to render (image or text) for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D2 RID: 466
	public enum ToolStripItemDisplayStyle
	{
		/// <summary>Specifies that neither image nor text is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04000C46 RID: 3142
		None,
		/// <summary>Specifies that only text is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04000C47 RID: 3143
		Text,
		/// <summary>Specifies that only an image is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04000C48 RID: 3144
		Image,
		/// <summary>Specifies that both an image and text are to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x04000C49 RID: 3145
		ImageAndText
	}
}
