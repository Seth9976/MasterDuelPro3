using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000100 RID: 256
	internal static class CompletedTasks
	{
		// Token: 0x040003F8 RID: 1016
		public static readonly UniTask<AsyncUnit> AsyncUnit = UniTask.FromResult<AsyncUnit>(Cysharp.Threading.Tasks.AsyncUnit.Default);

		// Token: 0x040003F9 RID: 1017
		public static readonly UniTask<bool> True = UniTask.FromResult<bool>(true);

		// Token: 0x040003FA RID: 1018
		public static readonly UniTask<bool> False = UniTask.FromResult<bool>(false);

		// Token: 0x040003FB RID: 1019
		public static readonly UniTask<int> Zero = UniTask.FromResult<int>(0);

		// Token: 0x040003FC RID: 1020
		public static readonly UniTask<int> MinusOne = UniTask.FromResult<int>(-1);

		// Token: 0x040003FD RID: 1021
		public static readonly UniTask<int> One = UniTask.FromResult<int>(1);
	}
}
