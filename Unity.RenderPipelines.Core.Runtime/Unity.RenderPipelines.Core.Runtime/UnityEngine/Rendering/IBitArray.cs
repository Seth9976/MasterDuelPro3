using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001A6 RID: 422
	public interface IBitArray
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000BEA RID: 3050
		uint capacity { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000BEB RID: 3051
		bool allFalse { get; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000BEC RID: 3052
		bool allTrue { get; }

		// Token: 0x1700017B RID: 379
		bool this[uint index] { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000BEF RID: 3055
		string humanizedData { get; }

		// Token: 0x06000BF0 RID: 3056
		IBitArray BitAnd(IBitArray other);

		// Token: 0x06000BF1 RID: 3057
		IBitArray BitOr(IBitArray other);

		// Token: 0x06000BF2 RID: 3058
		IBitArray BitNot();
	}
}
