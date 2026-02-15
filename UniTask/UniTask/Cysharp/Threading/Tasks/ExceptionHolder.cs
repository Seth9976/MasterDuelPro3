using System;
using System.Runtime.ExceptionServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200011E RID: 286
	internal class ExceptionHolder
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x0002061B File Offset: 0x0001E81B
		public ExceptionHolder(ExceptionDispatchInfo exception)
		{
			this.exception = exception;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0002062A File Offset: 0x0001E82A
		public ExceptionDispatchInfo GetException()
		{
			if (!this.calledGet)
			{
				this.calledGet = true;
				GC.SuppressFinalize(this);
			}
			return this.exception;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00020648 File Offset: 0x0001E848
		~ExceptionHolder()
		{
			if (!this.calledGet)
			{
				UniTaskScheduler.PublishUnobservedTaskException(this.exception.SourceException);
			}
		}

		// Token: 0x04000435 RID: 1077
		private ExceptionDispatchInfo exception;

		// Token: 0x04000436 RID: 1078
		private bool calledGet;
	}
}
