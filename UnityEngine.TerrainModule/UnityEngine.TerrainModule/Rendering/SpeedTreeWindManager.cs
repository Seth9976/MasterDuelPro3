using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200000F RID: 15
	[StaticAccessor("GetSpeedTreeWindManager()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/Terrain/Public/SpeedTreeWindManager.h")]
	internal static class SpeedTreeWindManager
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public unsafe static void UpdateWindAndWriteBufferWindParams(ReadOnlySpan<int> renderersID, SpeedTreeWindParamsBufferIterator windParams, bool history)
		{
			ReadOnlySpan<int> readOnlySpan = renderersID;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				SpeedTreeWindManager.UpdateWindAndWriteBufferWindParams_Injected(ref managedSpanWrapper, ref windParams, history);
			}
		}

		// Token: 0x0600002D RID: 45
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateWindAndWriteBufferWindParams_Injected(ref ManagedSpanWrapper renderersID, [In] ref SpeedTreeWindParamsBufferIterator windParams, bool history);
	}
}
