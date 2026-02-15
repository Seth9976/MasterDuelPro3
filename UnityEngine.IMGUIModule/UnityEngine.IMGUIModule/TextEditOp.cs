using System;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	internal enum TextEditOp
	{
		// Token: 0x040000FA RID: 250
		MoveLeft,
		// Token: 0x040000FB RID: 251
		MoveRight,
		// Token: 0x040000FC RID: 252
		MoveUp,
		// Token: 0x040000FD RID: 253
		MoveDown,
		// Token: 0x040000FE RID: 254
		MoveLineStart,
		// Token: 0x040000FF RID: 255
		MoveLineEnd,
		// Token: 0x04000100 RID: 256
		MoveTextStart,
		// Token: 0x04000101 RID: 257
		MoveTextEnd,
		// Token: 0x04000102 RID: 258
		MovePageUp,
		// Token: 0x04000103 RID: 259
		MovePageDown,
		// Token: 0x04000104 RID: 260
		MoveGraphicalLineStart,
		// Token: 0x04000105 RID: 261
		MoveGraphicalLineEnd,
		// Token: 0x04000106 RID: 262
		MoveWordLeft,
		// Token: 0x04000107 RID: 263
		MoveWordRight,
		// Token: 0x04000108 RID: 264
		MoveParagraphForward,
		// Token: 0x04000109 RID: 265
		MoveParagraphBackward,
		// Token: 0x0400010A RID: 266
		MoveToStartOfNextWord,
		// Token: 0x0400010B RID: 267
		MoveToEndOfPreviousWord,
		// Token: 0x0400010C RID: 268
		Delete,
		// Token: 0x0400010D RID: 269
		Backspace,
		// Token: 0x0400010E RID: 270
		DeleteWordBack,
		// Token: 0x0400010F RID: 271
		DeleteWordForward,
		// Token: 0x04000110 RID: 272
		DeleteLineBack,
		// Token: 0x04000111 RID: 273
		Cut,
		// Token: 0x04000112 RID: 274
		Paste,
		// Token: 0x04000113 RID: 275
		ScrollStart,
		// Token: 0x04000114 RID: 276
		ScrollEnd,
		// Token: 0x04000115 RID: 277
		ScrollPageUp,
		// Token: 0x04000116 RID: 278
		ScrollPageDown
	}
}
