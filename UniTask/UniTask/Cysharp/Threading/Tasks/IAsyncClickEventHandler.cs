using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000173 RID: 371
	public interface IAsyncClickEventHandler : IDisposable
	{
		// Token: 0x06000901 RID: 2305
		UniTask OnClickAsync();
	}
}
