using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Collections.Concurrent
{
	// Token: 0x0200072B RID: 1835
	[DebuggerDisplay("Head = {Head}, Tail = {Tail}")]
	[StructLayout(LayoutKind.Explicit, Size = 384)]
	internal struct PaddedHeadAndTail
	{
		// Token: 0x04001ECE RID: 7886
		[FieldOffset(128)]
		public int Head;

		// Token: 0x04001ECF RID: 7887
		[FieldOffset(256)]
		public int Tail;
	}
}
