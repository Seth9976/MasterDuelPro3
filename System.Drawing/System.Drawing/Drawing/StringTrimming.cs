using System;

namespace System.Drawing
{
	/// <summary>Specifies how to trim characters from a string that does not completely fit into a layout shape.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001A RID: 26
	public enum StringTrimming
	{
		/// <summary>Specifies no trimming.</summary>
		// Token: 0x040000E8 RID: 232
		None,
		/// <summary>Specifies that the text is trimmed to the nearest character.</summary>
		// Token: 0x040000E9 RID: 233
		Character,
		/// <summary>Specifies that text is trimmed to the nearest word.</summary>
		// Token: 0x040000EA RID: 234
		Word,
		/// <summary>Specifies that the text is trimmed to the nearest character, and an ellipsis is inserted at the end of a trimmed line.</summary>
		// Token: 0x040000EB RID: 235
		EllipsisCharacter,
		/// <summary>Specifies that text is trimmed to the nearest word, and an ellipsis is inserted at the end of a trimmed line.</summary>
		// Token: 0x040000EC RID: 236
		EllipsisWord,
		/// <summary>The center is removed from trimmed lines and replaced by an ellipsis. The algorithm keeps as much of the last slash-delimited segment of the line as possible.</summary>
		// Token: 0x040000ED RID: 237
		EllipsisPath
	}
}
