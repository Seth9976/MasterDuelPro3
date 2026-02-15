using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000024 RID: 36
	[IgnoredByDeepProfiler]
	[UsedByNativeCode]
	public struct ProfilerMarker
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public IntPtr Handle
		{
			get
			{
				return this.m_Ptr;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002BE0 File Offset: 0x00000DE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.Default, 0);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002BF2 File Offset: 0x00000DF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(ProfilerCategory category, string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, category, MarkerFlags.Default, 0);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002C0C File Offset: 0x00000E0C
		[Pure]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker.AutoScope Auto()
		{
			return new ProfilerMarker.AutoScope(this.m_Ptr);
		}

		// Token: 0x04000048 RID: 72
		[NativeDisableUnsafePtrRestriction]
		[NonSerialized]
		internal readonly IntPtr m_Ptr;

		// Token: 0x02000025 RID: 37
		[IgnoredByDeepProfiler]
		[UsedByNativeCode]
		public struct AutoScope : IDisposable
		{
			// Token: 0x06000074 RID: 116 RVA: 0x00002C2C File Offset: 0x00000E2C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal AutoScope(IntPtr markerPtr)
			{
				this.m_Ptr = markerPtr;
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.BeginSample(markerPtr);
				}
			}

			// Token: 0x06000075 RID: 117 RVA: 0x00002C5C File Offset: 0x00000E5C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.EndSample(this.m_Ptr);
				}
			}

			// Token: 0x04000049 RID: 73
			[NativeDisableUnsafePtrRestriction]
			internal readonly IntPtr m_Ptr;
		}
	}
}
