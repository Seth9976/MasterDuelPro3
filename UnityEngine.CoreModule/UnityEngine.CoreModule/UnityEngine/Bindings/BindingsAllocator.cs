using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200023E RID: 574
	[NativeHeader("Runtime/Scripting/Marshalling/BindingsAllocator.h")]
	[StaticAccessor("Marshalling::BindingsAllocator", StaticAccessorType.DoubleColon)]
	[VisibleToOtherModules]
	internal static class BindingsAllocator
	{
		// Token: 0x0600149D RID: 5277
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Free(void* ptr);

		// Token: 0x0600149E RID: 5278
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void FreeNativeOwnedMemory(void* ptr);

		// Token: 0x0600149F RID: 5279 RVA: 0x0002B8B8 File Offset: 0x00029AB8
		public unsafe static void* GetNativeOwnedDataPointer(void* ptr)
		{
			return ((BindingsAllocator.NativeOwnedMemory*)ptr)->data;
		}

		// Token: 0x0200023F RID: 575
		private struct NativeOwnedMemory
		{
			// Token: 0x0400079B RID: 1947
			public unsafe void* data;
		}
	}
}
