using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005B4 RID: 1460
	internal struct MatchResultInfo
	{
		// Token: 0x060027C2 RID: 10178 RVA: 0x000A325E File Offset: 0x000A145E
		public MatchResultInfo(bool success, PseudoStates triggerPseudoMask, PseudoStates dependencyPseudoMask)
		{
			this.success = success;
			this.triggerPseudoMask = triggerPseudoMask;
			this.dependencyPseudoMask = dependencyPseudoMask;
		}

		// Token: 0x040014FD RID: 5373
		public readonly bool success;

		// Token: 0x040014FE RID: 5374
		public readonly PseudoStates triggerPseudoMask;

		// Token: 0x040014FF RID: 5375
		public readonly PseudoStates dependencyPseudoMask;
	}
}
