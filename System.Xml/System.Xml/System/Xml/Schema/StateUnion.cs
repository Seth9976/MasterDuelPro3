using System;
using System.Runtime.InteropServices;

namespace System.Xml.Schema
{
	// Token: 0x020002A3 RID: 675
	[StructLayout(LayoutKind.Explicit)]
	internal struct StateUnion
	{
		// Token: 0x04000E50 RID: 3664
		[FieldOffset(0)]
		public int State;

		// Token: 0x04000E51 RID: 3665
		[FieldOffset(0)]
		public int AllElementsRequired;

		// Token: 0x04000E52 RID: 3666
		[FieldOffset(0)]
		public int CurPosIndex;

		// Token: 0x04000E53 RID: 3667
		[FieldOffset(0)]
		public int NumberOfRunningPos;
	}
}
