using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Unity.Hierarchy
{
	// Token: 0x02000008 RID: 8
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal struct HierarchyNodeChildrenAlloc
	{
		// Token: 0x04000011 RID: 17
		[FieldOffset(0)]
		public unsafe HierarchyNode* Ptr;

		// Token: 0x04000012 RID: 18
		[FieldOffset(8)]
		public int Size;

		// Token: 0x04000013 RID: 19
		[FieldOffset(12)]
		public int Capacity;

		// Token: 0x04000014 RID: 20
		[FieldOffset(16)]
		public int RemovedCount;

		// Token: 0x04000015 RID: 21
		[FixedBuffer(typeof(int), 3)]
		[FieldOffset(20)]
		public HierarchyNodeChildrenAlloc.<Reserved>e__FixedBuffer Reserved;

		// Token: 0x02000009 RID: 9
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 12)]
		public struct <Reserved>e__FixedBuffer
		{
			// Token: 0x04000016 RID: 22
			public int FixedElementField;
		}
	}
}
