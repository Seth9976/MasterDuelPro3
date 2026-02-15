using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position of the text and image relative to each other on a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A2 RID: 418
	public enum TextImageRelation
	{
		/// <summary>Specifies that the image and text share the same space on a control.</summary>
		// Token: 0x04000B0A RID: 2826
		Overlay,
		/// <summary>Specifies that the image is displayed vertically above the text of a control.</summary>
		// Token: 0x04000B0B RID: 2827
		ImageAboveText,
		/// <summary>Specifies that the text is displayed vertically above the image of a control.</summary>
		// Token: 0x04000B0C RID: 2828
		TextAboveImage,
		/// <summary>Specifies that the image is displayed horizontally before the text of a control.</summary>
		// Token: 0x04000B0D RID: 2829
		ImageBeforeText = 4,
		/// <summary>Specifies that the text is displayed horizontally before the image of a control.</summary>
		// Token: 0x04000B0E RID: 2830
		TextBeforeImage = 8
	}
}
