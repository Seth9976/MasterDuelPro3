using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200003B RID: 59
	public static class UniTaskStatusExtensions
	{
		// Token: 0x06000126 RID: 294 RVA: 0x0000449E File Offset: 0x0000269E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCompleted(this UniTaskStatus status)
		{
			return status > UniTaskStatus.Pending;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000044A4 File Offset: 0x000026A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCompletedSuccessfully(this UniTaskStatus status)
		{
			return status == UniTaskStatus.Succeeded;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000044AA File Offset: 0x000026AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCanceled(this UniTaskStatus status)
		{
			return status == UniTaskStatus.Canceled;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000044B0 File Offset: 0x000026B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFaulted(this UniTaskStatus status)
		{
			return status == UniTaskStatus.Faulted;
		}
	}
}
