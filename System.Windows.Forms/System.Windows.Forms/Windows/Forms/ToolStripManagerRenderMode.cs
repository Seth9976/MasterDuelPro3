using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Specifies the painting style applied to multiple <see cref="T:System.Windows.Forms.ToolStrip" /> objects contained in a form.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E2 RID: 482
	public enum ToolStripManagerRenderMode
	{
		/// <summary>Indicates the use of a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> other than <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> or <see cref="T:System.Windows.Forms.ToolStripSystemRenderer" />.</summary>
		// Token: 0x04000C79 RID: 3193
		[Browsable(false)]
		Custom,
		/// <summary>Indicates the use of a <see cref="T:System.Windows.Forms.ToolStripSystemRenderer" /> to paint.</summary>
		// Token: 0x04000C7A RID: 3194
		System,
		/// <summary>Indicates the use of a <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> to paint.</summary>
		// Token: 0x04000C7B RID: 3195
		Professional
	}
}
