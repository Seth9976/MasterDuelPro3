using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000B6 RID: 182
	internal struct TessLink
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0001CDA0 File Offset: 0x0001AFA0
		internal static TessLink CreateLink(int count, Allocator allocator)
		{
			TessLink link = default(TessLink);
			link.roots = new NativeArray<int>(count, allocator, NativeArrayOptions.ClearMemory);
			link.ranks = new NativeArray<int>(count, allocator, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < count; i++)
			{
				link.roots[i] = i;
				link.ranks[i] = 0;
			}
			return link;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001CDFC File Offset: 0x0001AFFC
		internal static void DestroyLink(TessLink link)
		{
			link.ranks.Dispose();
			link.roots.Dispose();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001CE18 File Offset: 0x0001B018
		internal int Find(int x)
		{
			int x2 = x;
			while (this.roots[x] != x)
			{
				x = this.roots[x];
			}
			while (this.roots[x2] != x)
			{
				int num = this.roots[x2];
				this.roots[x2] = x;
				x2 = num;
			}
			return x;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001CE74 File Offset: 0x0001B074
		internal void Link(int x, int y)
		{
			int xr = this.Find(x);
			int yr = this.Find(y);
			if (xr == yr)
			{
				return;
			}
			int xd = this.ranks[xr];
			int yd = this.ranks[yr];
			if (xd < yd)
			{
				this.roots[xr] = yr;
				return;
			}
			if (yd < xd)
			{
				this.roots[yr] = xr;
				return;
			}
			this.roots[yr] = xr;
			int num = xr;
			int num2 = this.ranks[num] + 1;
			this.ranks[num] = num2;
		}

		// Token: 0x0400032A RID: 810
		internal NativeArray<int> roots;

		// Token: 0x0400032B RID: 811
		internal NativeArray<int> ranks;
	}
}
