using System;
using UnityEngine;
using UnityEngine.UI;

namespace Spine.Unity
{
	// Token: 0x02000024 RID: 36
	[RequireComponent(typeof(CanvasRenderer))]
	public class SkeletonSubmeshGraphic : MaskableGraphic
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00003F81 File Offset: 0x00002181
		public override void SetMaterialDirty()
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003F81 File Offset: 0x00002181
		public override void SetVerticesDirty()
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005EF0 File Offset: 0x000040F0
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005EF8 File Offset: 0x000040F8
		protected override void OnDisable()
		{
			base.OnDisable();
			base.canvasRenderer.cull = true;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005F0C File Offset: 0x0000410C
		protected override void OnEnable()
		{
			base.OnEnable();
			base.canvasRenderer.cull = false;
		}
	}
}
