using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200019B RID: 411
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct EnumData
	{
		// Token: 0x04000653 RID: 1619
		public Enum[] values;

		// Token: 0x04000654 RID: 1620
		public int[] flagValues;

		// Token: 0x04000655 RID: 1621
		public string[] displayNames;

		// Token: 0x04000656 RID: 1622
		public string[] names;

		// Token: 0x04000657 RID: 1623
		public string[] tooltip;

		// Token: 0x04000658 RID: 1624
		public bool flags;

		// Token: 0x04000659 RID: 1625
		public Type underlyingType;

		// Token: 0x0400065A RID: 1626
		public bool unsigned;

		// Token: 0x0400065B RID: 1627
		public bool serializable;
	}
}
