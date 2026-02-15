using System;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200019E RID: 414
	public interface IAsyncOnApplicationFocusHandler
	{
		// Token: 0x06000AA4 RID: 2724
		UniTask<bool> OnApplicationFocusAsync();
	}
}
