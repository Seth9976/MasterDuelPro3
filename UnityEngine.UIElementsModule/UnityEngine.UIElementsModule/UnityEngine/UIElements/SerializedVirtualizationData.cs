using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000083 RID: 131
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	[Serializable]
	internal class SerializedVirtualizationData
	{
		// Token: 0x040002CE RID: 718
		public Vector2 scrollOffset;

		// Token: 0x040002CF RID: 719
		public int firstVisibleIndex;

		// Token: 0x040002D0 RID: 720
		public float contentPadding;

		// Token: 0x040002D1 RID: 721
		public float contentHeight;

		// Token: 0x040002D2 RID: 722
		public int anchoredItemIndex;

		// Token: 0x040002D3 RID: 723
		public float anchorOffset;
	}
}
