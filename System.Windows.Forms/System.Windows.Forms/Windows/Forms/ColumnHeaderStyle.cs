using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the styles of the column headers in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003A RID: 58
	public enum ColumnHeaderStyle
	{
		/// <summary>The column header is not displayed in report view.</summary>
		// Token: 0x0400013E RID: 318
		None,
		/// <summary>The column headers do not respond to the click of a mouse.</summary>
		// Token: 0x0400013F RID: 319
		Nonclickable,
		/// <summary>The column headers function like buttons and can carry out an action, such as sorting, when clicked.</summary>
		// Token: 0x04000140 RID: 320
		Clickable
	}
}
