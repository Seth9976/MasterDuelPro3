using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000253 RID: 595
	internal class EmptyDisposable : IDisposable
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x000020BB File Offset: 0x000002BB
		private EmptyDisposable()
		{
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000030EE File Offset: 0x000012EE
		public void Dispose()
		{
		}

		// Token: 0x040006B5 RID: 1717
		public static EmptyDisposable Instance = new EmptyDisposable();
	}
}
