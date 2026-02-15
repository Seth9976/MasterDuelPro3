using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x02000284 RID: 644
	internal static class NativeListExtensions
	{
		// Token: 0x06001166 RID: 4454 RVA: 0x0003F112 File Offset: 0x0003D312
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static ReadOnlySpan<T> MakeReadOnlySpan<[IsUnmanaged] T>(this NativeList<T> list, int first, int numElements) where T : struct, ValueType
		{
			return new ReadOnlySpan<T>((void*)(list.GetUnsafeReadOnlyPtr<T>() + (IntPtr)first * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)), numElements);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003F130 File Offset: 0x0003D330
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int LastIndex<[IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return list.Length - 1;
		}
	}
}
