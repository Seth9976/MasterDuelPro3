using System;

namespace DG.Tweening.Plugins.Options
{
	// Token: 0x0200008A RID: 138
	public struct UintOptions : IPlugOptions
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000EC19 File Offset: 0x0000CE19
		public void Reset()
		{
			this.isNegativeChangeValue = false;
		}

		// Token: 0x04000184 RID: 388
		public bool isNegativeChangeValue;
	}
}
