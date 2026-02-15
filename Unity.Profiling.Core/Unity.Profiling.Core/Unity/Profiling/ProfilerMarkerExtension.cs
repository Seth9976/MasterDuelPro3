using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling.LowLevel.Unsafe;

namespace Unity.Profiling
{
	// Token: 0x0200000E RID: 14
	public static class ProfilerMarkerExtension
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000212C File Offset: 0x0000032C
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Begin(this ProfilerMarker marker, int metadata)
		{
			ProfilerMarkerData data = new ProfilerMarkerData
			{
				Type = 2,
				Size = (uint)UnsafeUtility.SizeOf<int>(),
				Ptr = (void*)(&metadata)
			};
			ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, (void*)(&data));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002174 File Offset: 0x00000374
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Begin(this ProfilerMarker marker, uint metadata)
		{
			ProfilerMarkerData data = new ProfilerMarkerData
			{
				Type = 3,
				Size = (uint)UnsafeUtility.SizeOf<uint>(),
				Ptr = (void*)(&metadata)
			};
			ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, (void*)(&data));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000021BC File Offset: 0x000003BC
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Begin(this ProfilerMarker marker, long metadata)
		{
			ProfilerMarkerData data = new ProfilerMarkerData
			{
				Type = 4,
				Size = (uint)UnsafeUtility.SizeOf<long>(),
				Ptr = (void*)(&metadata)
			};
			ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, (void*)(&data));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002204 File Offset: 0x00000404
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Begin(this ProfilerMarker marker, ulong metadata)
		{
			ProfilerMarkerData data = new ProfilerMarkerData
			{
				Type = 5,
				Size = (uint)UnsafeUtility.SizeOf<ulong>(),
				Ptr = (void*)(&metadata)
			};
			ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, (void*)(&data));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000224C File Offset: 0x0000044C
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Begin(this ProfilerMarker marker, float metadata)
		{
			ProfilerMarkerData data = new ProfilerMarkerData
			{
				Type = 6,
				Size = (uint)UnsafeUtility.SizeOf<float>(),
				Ptr = (void*)(&metadata)
			};
			ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, (void*)(&data));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002294 File Offset: 0x00000494
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Begin(this ProfilerMarker marker, double metadata)
		{
			ProfilerMarkerData data = new ProfilerMarkerData
			{
				Type = 7,
				Size = (uint)UnsafeUtility.SizeOf<double>(),
				Ptr = (void*)(&metadata)
			};
			ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, (void*)(&data));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000022DC File Offset: 0x000004DC
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void Begin(this ProfilerMarker marker, string metadata)
		{
			ProfilerMarkerData data = new ProfilerMarkerData
			{
				Type = 9
			};
			fixed (string text = metadata)
			{
				char* c = text;
				if (c != null)
				{
					c += RuntimeHelpers.OffsetToStringData / 2;
				}
				data.Size = (uint)((metadata.Length + 1) * 2);
				data.Ptr = (void*)c;
				ProfilerUnsafeUtility.BeginSampleWithMetadata(marker.Handle, 1, (void*)(&data));
			}
		}
	}
}
