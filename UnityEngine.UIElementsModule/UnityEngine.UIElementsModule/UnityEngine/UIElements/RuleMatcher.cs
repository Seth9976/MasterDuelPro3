using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000464 RID: 1124
	internal struct RuleMatcher
	{
		// Token: 0x06002136 RID: 8502 RVA: 0x0007A2E8 File Offset: 0x000784E8
		public override string ToString()
		{
			return this.complexSelector.ToString();
		}

		// Token: 0x04000EAE RID: 3758
		public StyleSheet sheet;

		// Token: 0x04000EAF RID: 3759
		public StyleComplexSelector complexSelector;
	}
}
