using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001C RID: 28
	public static class Channel
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x000034D8 File Offset: 0x000016D8
		public static Channel<T> CreateSingleConsumerUnbounded<T>()
		{
			return new SingleConsumerUnboundedChannel<T>();
		}
	}
}
