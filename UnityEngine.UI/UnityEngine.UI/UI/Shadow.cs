using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000088 RID: 136
	[AddComponentMenu("UI/Effects/Shadow", 80)]
	public class Shadow : BaseMeshEffect
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00017198 File Offset: 0x00015398
		protected Shadow()
		{
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x000171E6 File Offset: 0x000153E6
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x000171EE File Offset: 0x000153EE
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00017210 File Offset: 0x00015410
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00017218 File Offset: 0x00015418
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000172B8 File Offset: 0x000154B8
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x000172C0 File Offset: 0x000154C0
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000172E4 File Offset: 0x000154E4
		protected void ApplyShadowZeroAlloc(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int neededCapacity = verts.Count + end - start;
			if (verts.Capacity < neededCapacity)
			{
				verts.Capacity = neededCapacity;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex vt = verts[i];
				verts.Add(vt);
				Vector3 v = vt.position;
				v.x += x;
				v.y += y;
				vt.position = v;
				Color32 newColor = color;
				if (this.m_UseGraphicAlpha)
				{
					newColor.a = newColor.a * verts[i].color.a / byte.MaxValue;
				}
				vt.color = newColor;
				verts[i] = vt;
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00017398 File Offset: 0x00015598
		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			this.ApplyShadowZeroAlloc(verts, color, start, end, x, y);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000173AC File Offset: 0x000155AC
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> output = CollectionPool<List<UIVertex>, UIVertex>.Get();
			vh.GetUIVertexStream(output);
			this.ApplyShadow(output, this.effectColor, 0, output.Count, this.effectDistance.x, this.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(output);
			CollectionPool<List<UIVertex>, UIVertex>.Release(output);
		}

		// Token: 0x0400026F RID: 623
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000270 RID: 624
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x04000271 RID: 625
		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		// Token: 0x04000272 RID: 626
		private const float kMaxEffectDistance = 600f;
	}
}
