using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x02000018 RID: 24
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Modules/HierarchyCore/HierarchyCommandListBindings.h")]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyCommandList.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HierarchyCommandList : IDisposable
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00003139 File Offset: 0x00001339
		private HierarchyCommandList(IntPtr nativePtr)
		{
			this.m_Ptr = nativePtr;
			this.m_IsOwner = false;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003154 File Offset: 0x00001354
		~HierarchyCommandList()
		{
			this.Dispose(false);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003188 File Offset: 0x00001388
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000319C File Offset: 0x0000139C
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool isOwner = this.m_IsOwner;
				if (isOwner)
				{
					HierarchyCommandList.Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031E4 File Offset: 0x000013E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HierarchyCommandList FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((HierarchyCommandList)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x06000083 RID: 131
		[FreeFunction("HierarchyCommandListBindings::Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr nativePtr);

		// Token: 0x06000084 RID: 132 RVA: 0x00003214 File Offset: 0x00001414
		[RequiredByNativeCode]
		private static IntPtr CreateCommandList(IntPtr nativePtr)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new HierarchyCommandList(nativePtr)));
		}

		// Token: 0x04000033 RID: 51
		private IntPtr m_Ptr;

		// Token: 0x04000034 RID: 52
		private readonly bool m_IsOwner;

		// Token: 0x02000019 RID: 25
		internal static class BindingsMarshaller
		{
			// Token: 0x06000085 RID: 133 RVA: 0x00003226 File Offset: 0x00001426
			public static IntPtr ConvertToNative(HierarchyCommandList cmdList)
			{
				return cmdList.m_Ptr;
			}
		}
	}
}
