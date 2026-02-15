using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200002F RID: 47
	public static class ExceptionExtensions
	{
		// Token: 0x06000109 RID: 265 RVA: 0x0000440C File Offset: 0x0000260C
		public static bool IsOperationCanceledException(this Exception exception)
		{
			return exception is OperationCanceledException;
		}
	}
}
