using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005BC RID: 1468
	[DebuggerDisplay("id = {id}, keyword = {keyword}, number = {number}, boolean = {boolean}, color = {color}, object = {resource}")]
	[StructLayout(LayoutKind.Explicit)]
	internal struct StyleValue
	{
		// Token: 0x04001508 RID: 5384
		[FieldOffset(0)]
		public StylePropertyId id;

		// Token: 0x04001509 RID: 5385
		[FieldOffset(4)]
		public StyleKeyword keyword;

		// Token: 0x0400150A RID: 5386
		[FieldOffset(8)]
		public float number;

		// Token: 0x0400150B RID: 5387
		[FieldOffset(8)]
		public Length length;

		// Token: 0x0400150C RID: 5388
		[FieldOffset(8)]
		public Color color;

		// Token: 0x0400150D RID: 5389
		[FieldOffset(8)]
		public GCHandle resource;

		// Token: 0x0400150E RID: 5390
		[FieldOffset(8)]
		public BackgroundPosition position;

		// Token: 0x0400150F RID: 5391
		[FieldOffset(8)]
		public BackgroundRepeat repeat;
	}
}
