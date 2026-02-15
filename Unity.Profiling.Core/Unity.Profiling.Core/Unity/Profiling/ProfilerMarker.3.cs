using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Unity.Profiling
{
	// Token: 0x0200000C RID: 12
	public readonly struct ProfilerMarker<[IsUnmanaged] TP1, [IsUnmanaged] TP2, [IsUnmanaged] TP3> where TP1 : struct, ValueType where TP2 : struct, ValueType where TP3 : struct, ValueType
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000020C7 File Offset: 0x000002C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name, string param1Name, string param2Name, string param3Name)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000020C7 File Offset: 0x000002C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(ProfilerCategory category, string name, string param1Name, string param2Name, string param3Name)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000020C7 File Offset: 0x000002C7
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Begin(TP1 p1, TP2 p2, TP3 p3)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000020C7 File Offset: 0x000002C7
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void End()
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002114 File Offset: 0x00000314
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker<TP1, TP2, TP3>.AutoScope Auto(TP1 p1, TP2 p2, TP3 p3)
		{
			return default(ProfilerMarker<TP1, TP2, TP3>.AutoScope);
		}

		// Token: 0x0200000D RID: 13
		public readonly struct AutoScope : IDisposable
		{
			// Token: 0x06000022 RID: 34 RVA: 0x000020C7 File Offset: 0x000002C7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal AutoScope(ProfilerMarker<TP1, TP2, TP3> marker, TP1 p1, TP2 p2, TP3 p3)
			{
			}

			// Token: 0x06000023 RID: 35 RVA: 0x000020C7 File Offset: 0x000002C7
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
			}
		}
	}
}
