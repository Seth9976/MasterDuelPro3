using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002D5 RID: 725
	[FriendAccessAllowed]
	internal static class AsyncCausalityTracer
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x00033991 File Offset: 0x00031B91
		[FriendAccessAllowed]
		internal static bool LoggingOn
		{
			[FriendAccessAllowed]
			get
			{
				return false;
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00002C89 File Offset: 0x00000E89
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCreation(CausalityTraceLevel traceLevel, int taskId, string operationName, ulong relatedContext)
		{
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00002C89 File Offset: 0x00000E89
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceOperationCompletion(CausalityTraceLevel traceLevel, int taskId, AsyncCausalityStatus status)
		{
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00002C89 File Offset: 0x00000E89
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, int taskId, CausalitySynchronousWork work)
		{
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00002C89 File Offset: 0x00000E89
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
		}
	}
}
