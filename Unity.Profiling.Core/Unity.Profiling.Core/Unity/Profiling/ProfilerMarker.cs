using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Unity.Profiling
{
	// Token: 0x02000008 RID: 8
	public readonly struct ProfilerMarker<[IsUnmanaged] TP1> where TP1 : struct, ValueType
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000020C7 File Offset: 0x000002C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name, string param1Name)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020C7 File Offset: 0x000002C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(ProfilerCategory category, string name, string param1Name)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000020C7 File Offset: 0x000002C7
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Begin(TP1 p1)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020C7 File Offset: 0x000002C7
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void End()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000020E4 File Offset: 0x000002E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker<TP1>.AutoScope Auto(TP1 p1)
		{
			return default(ProfilerMarker<TP1>.AutoScope);
		}

		// Token: 0x02000009 RID: 9
		public readonly struct AutoScope : IDisposable
		{
			// Token: 0x06000014 RID: 20 RVA: 0x000020C7 File Offset: 0x000002C7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal AutoScope(ProfilerMarker<TP1> marker, TP1 p1)
			{
			}

			// Token: 0x06000015 RID: 21 RVA: 0x000020C7 File Offset: 0x000002C7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
			}
		}
	}
}
