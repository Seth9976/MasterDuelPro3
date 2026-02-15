using System;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001C6 RID: 454
	public interface IAsyncOnJointBreakHandler
	{
		// Token: 0x06000B30 RID: 2864
		UniTask<float> OnJointBreakAsync();
	}
}
