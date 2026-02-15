using System;

namespace System.Threading
{
	// Token: 0x0200023A RID: 570
	internal struct CancellationCallbackCoreWorkArguments
	{
		// Token: 0x06001523 RID: 5411 RVA: 0x000551BC File Offset: 0x000533BC
		public CancellationCallbackCoreWorkArguments(SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment, int currArrayIndex)
		{
			this._currArrayFragment = currArrayFragment;
			this._currArrayIndex = currArrayIndex;
		}

		// Token: 0x04000A58 RID: 2648
		internal SparselyPopulatedArrayFragment<CancellationCallbackInfo> _currArrayFragment;

		// Token: 0x04000A59 RID: 2649
		internal int _currArrayIndex;
	}
}
