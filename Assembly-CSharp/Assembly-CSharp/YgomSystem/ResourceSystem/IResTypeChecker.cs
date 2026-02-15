using System;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006DF RID: 1759
	public interface IResTypeChecker
	{
		// Token: 0x060036E3 RID: 14051
		void Initialize();

		// Token: 0x060036E4 RID: 14052
		void Destroy();

		// Token: 0x060036E5 RID: 14053
		void ClearCache();

		// Token: 0x060036E6 RID: 14054
		ResTypeData GetResType(string path);

		// Token: 0x060036E7 RID: 14055
		ResTypeData GetResTypeSimpleCheck(string path);
	}
}
