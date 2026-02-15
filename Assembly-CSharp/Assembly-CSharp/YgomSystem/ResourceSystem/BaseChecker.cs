using System;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006D8 RID: 1752
	public abstract class BaseChecker : IResTypeChecker
	{
		// Token: 0x060036AB RID: 13995
		public abstract ResTypeData GetResType(string path);

		// Token: 0x060036AC RID: 13996
		public abstract ResTypeData GetResTypeSimpleCheck(string path);

		// Token: 0x060036AD RID: 13997 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Initialize()
		{
		}

		// Token: 0x060036AE RID: 13998 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void Destroy()
		{
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void ClearCache()
		{
		}
	}
}
