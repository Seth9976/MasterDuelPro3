using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200001B RID: 27
	internal class MyIntersectNodeSort : IComparer<IntersectNode>
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000028CC File Offset: 0x00000ACC
		public int Compare(IntersectNode node1, IntersectNode node2)
		{
			long i = node2.Pt.Y - node1.Pt.Y;
			if (i > 0L)
			{
				return 1;
			}
			if (i < 0L)
			{
				return -1;
			}
			return 0;
		}
	}
}
