using System;
using System.Collections.Concurrent;
using System.Text;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000C1 RID: 193
	internal class StringBuilderPool
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001B44E File Offset: 0x0001964E
		public static StringBuilderPool Instance { get; } = new StringBuilderPool();

		// Token: 0x060005CF RID: 1487 RVA: 0x0001B458 File Offset: 0x00019658
		public StringBuilder Rent()
		{
			StringBuilder stringBuilder;
			if (!this.pool.TryDequeue(out stringBuilder))
			{
				return new StringBuilder();
			}
			return stringBuilder;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001B47B File Offset: 0x0001967B
		public void Return(StringBuilder builder)
		{
			builder.Clear();
			this.pool.Enqueue(builder);
		}

		// Token: 0x0400045C RID: 1116
		private readonly ConcurrentQueue<StringBuilder> pool = new ConcurrentQueue<StringBuilder>();
	}
}
