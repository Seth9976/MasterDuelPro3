using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020001A4 RID: 420
	public interface IAsyncOnAudioFilterReadHandler
	{
		// Token: 0x06000AB9 RID: 2745
		[return: TupleElementNames(new string[] { "data", "channels" })]
		UniTask<ValueTuple<float[], int>> OnAudioFilterReadAsync();
	}
}
