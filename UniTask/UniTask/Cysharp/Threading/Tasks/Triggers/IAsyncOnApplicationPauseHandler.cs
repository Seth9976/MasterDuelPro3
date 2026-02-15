using System;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001A0 RID: 416
	public interface IAsyncOnApplicationPauseHandler
	{
		// Token: 0x06000AAB RID: 2731
		UniTask<bool> OnApplicationPauseAsync();
	}
}
