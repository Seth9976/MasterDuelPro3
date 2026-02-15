using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200006F RID: 111
	[RequireComponent(typeof(CanvasRenderer))]
	public class TMP_SelectionCaret : MaskableGraphic
	{
		// Token: 0x06000352 RID: 850 RVA: 0x00012394 File Offset: 0x00010594
		public override void Cull(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.cull = false;
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
				return;
			}
			base.Cull(clipRect, validRect);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected override void UpdateGeometry()
		{
		}
	}
}
