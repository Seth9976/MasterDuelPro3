using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Unity.Profiling
{
	// Token: 0x0200000A RID: 10
	public readonly struct ProfilerMarker<[IsUnmanaged] TP1, [IsUnmanaged] TP2> where TP1 : struct, ValueType where TP2 : struct, ValueType
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000020C7 File Offset: 0x000002C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name, string param1Name, string param2Name)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000020C7 File Offset: 0x000002C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(ProfilerCategory category, string name, string param1Name, string param2Name)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000020C7 File Offset: 0x000002C7
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Begin(TP1 p1, TP2 p2)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000020C7 File Offset: 0x000002C7
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void End()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000020FC File Offset: 0x000002FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker<TP1, TP2>.AutoScope Auto(TP1 p1, TP2 p2)
		{
			return default(ProfilerMarker<TP1, TP2>.AutoScope);
		}

		// Token: 0x0200000B RID: 11
		public readonly struct AutoScope : IDisposable
		{
			// Token: 0x0600001B RID: 27 RVA: 0x000020C7 File Offset: 0x000002C7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal AutoScope(ProfilerMarker<TP1, TP2> marker, TP1 p1, TP2 p2)
			{
			}

			// Token: 0x0600001C RID: 28 RVA: 0x000020C7 File Offset: 0x000002C7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
			}
		}
	}
}
