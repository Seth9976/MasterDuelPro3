using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000DF RID: 223
	[StaticAccessor("GetUncheckedRealGfxDevice().GetFrameTimingManager()", StaticAccessorType.Dot)]
	public static class FrameTimingManager
	{
		// Token: 0x060005D1 RID: 1489
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CaptureFrameTimings();

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		public unsafe static uint GetLatestTimings(uint numFrames, FrameTiming[] timings)
		{
			Span<FrameTiming> span = new Span<FrameTiming>(timings);
			uint latestTimings_Injected;
			fixed (FrameTiming* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				latestTimings_Injected = FrameTimingManager.GetLatestTimings_Injected(numFrames, ref managedSpanWrapper);
			}
			return latestTimings_Injected;
		}

		// Token: 0x060005D3 RID: 1491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetLatestTimings_Injected(uint numFrames, ref ManagedSpanWrapper timings);
	}
}
