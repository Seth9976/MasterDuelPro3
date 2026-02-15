using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000086 RID: 134
	[AddComponentMenu("UI/Effects/Outline", 81)]
	public class Outline : Shadow
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x00016FF8 File Offset: 0x000151F8
		protected Outline()
		{
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00017000 File Offset: 0x00015200
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> verts = CollectionPool<List<UIVertex>, UIVertex>.Get();
			vh.GetUIVertexStream(verts);
			int neededCpacity = verts.Count * 5;
			if (verts.Capacity < neededCpacity)
			{
				verts.Capacity = neededCpacity;
			}
			int start = 0;
			int count = verts.Count;
			base.ApplyShadowZeroAlloc(verts, base.effectColor, start, verts.Count, base.effectDistance.x, base.effectDistance.y);
			start = count;
			int count2 = verts.Count;
			base.ApplyShadowZeroAlloc(verts, base.effectColor, start, verts.Count, base.effectDistance.x, -base.effectDistance.y);
			start = count2;
			int count3 = verts.Count;
			base.ApplyShadowZeroAlloc(verts, base.effectColor, start, verts.Count, -base.effectDistance.x, base.effectDistance.y);
			start = count3;
			int count4 = verts.Count;
			base.ApplyShadowZeroAlloc(verts, base.effectColor, start, verts.Count, -base.effectDistance.x, -base.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(verts);
			CollectionPool<List<UIVertex>, UIVertex>.Release(verts);
		}
	}
}
