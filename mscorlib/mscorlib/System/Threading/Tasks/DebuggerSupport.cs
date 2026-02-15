using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	// Token: 0x020002A4 RID: 676
	internal static class DebuggerSupport
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00033991 File Offset: 0x00031B91
		public static bool LoggingOn
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00002C89 File Offset: 0x00000E89
		public static void TraceOperationCreation(CausalityTraceLevel traceLevel, Task task, string operationName, ulong relatedContext)
		{
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00002C89 File Offset: 0x00000E89
		public static void TraceOperationCompletion(CausalityTraceLevel traceLevel, Task task, AsyncStatus status)
		{
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00002C89 File Offset: 0x00000E89
		public static void TraceOperationRelation(CausalityTraceLevel traceLevel, Task task, CausalityRelation relation)
		{
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00002C89 File Offset: 0x00000E89
		public static void TraceSynchronousWorkStart(CausalityTraceLevel traceLevel, Task task, CausalitySynchronousWork work)
		{
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00002C89 File Offset: 0x00000E89
		public static void TraceSynchronousWorkCompletion(CausalityTraceLevel traceLevel, CausalitySynchronousWork work)
		{
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0005E03A File Offset: 0x0005C23A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddToActiveTasks(Task task)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				DebuggerSupport.AddToActiveTasksNonInlined(task);
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0005E04C File Offset: 0x0005C24C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void AddToActiveTasksNonInlined(Task task)
		{
			int id = task.Id;
			object obj = DebuggerSupport.s_activeTasksLock;
			lock (obj)
			{
				DebuggerSupport.s_activeTasks[id] = task;
			}
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0005E098 File Offset: 0x0005C298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveFromActiveTasks(Task task)
		{
			if (Task.s_asyncDebuggingEnabled)
			{
				DebuggerSupport.RemoveFromActiveTasksNonInlined(task);
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0005E0A8 File Offset: 0x0005C2A8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void RemoveFromActiveTasksNonInlined(Task task)
		{
			int id = task.Id;
			object obj = DebuggerSupport.s_activeTasksLock;
			lock (obj)
			{
				DebuggerSupport.s_activeTasks.Remove(id);
			}
		}

		// Token: 0x04000BAA RID: 2986
		private static readonly LowLevelDictionary<int, Task> s_activeTasks = new LowLevelDictionary<int, Task>();

		// Token: 0x04000BAB RID: 2987
		private static readonly object s_activeTasksLock = new object();
	}
}
