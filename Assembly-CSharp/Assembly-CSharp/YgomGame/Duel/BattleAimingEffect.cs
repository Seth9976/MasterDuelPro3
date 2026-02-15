using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C93 RID: 3219
	public class BattleAimingEffect : DuelEffectHandle
	{
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06005C3D RID: 23613 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPositions(Vector3 srcPos, Vector3 dstPos)
		{
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetColor(Color col)
		{
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetVisible(bool visible)
		{
		}

		// Token: 0x06005C41 RID: 23617 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnInitialize()
		{
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTerminate()
		{
		}

		// Token: 0x06005C43 RID: 23619 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnUpdate()
		{
		}

		// Token: 0x06005C44 RID: 23620 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnStop()
		{
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MakeAimingArrowMesh(BattleAimingEffect.Shading shading, Mesh mesh, Vector3 from, Vector3 to, Vector3 aim, float lengthRate, float width, float wingWidthRate, Color color, Vector3 camPos, bool isTopHorizontally)
		{
		}

		// Token: 0x04009769 RID: 38761
		private const int TETRAGON_COUNT = 16;

		// Token: 0x0400976A RID: 38762
		private const int NOSE_VERTEX_COUNT = 3;

		// Token: 0x0400976B RID: 38763
		private const int ARROW_POLYGON_COUNT = 3;

		// Token: 0x0400976C RID: 38764
		private const int Z_POINT_COUNT = 17;

		// Token: 0x0400976D RID: 38765
		private const int VERTEX_COUNT = 37;

		// Token: 0x0400976E RID: 38766
		private float STREACH_SPEED;

		// Token: 0x0400976F RID: 38767
		private float FADE_SPEED;

		// Token: 0x04009770 RID: 38768
		private Vector3 from;

		// Token: 0x04009771 RID: 38769
		private Vector3 to;

		// Token: 0x04009772 RID: 38770
		private Vector3 aim;

		// Token: 0x04009773 RID: 38771
		private float width;

		// Token: 0x04009774 RID: 38772
		private float alpha;

		// Token: 0x04009775 RID: 38773
		private Color color;

		// Token: 0x04009776 RID: 38774
		private MeshRenderer mr;

		// Token: 0x04009777 RID: 38775
		private float lengthRate;

		// Token: 0x04009778 RID: 38776
		private bool reqStop;

		// Token: 0x04009779 RID: 38777
		private bool isEnd;

		// Token: 0x0400977A RID: 38778
		private bool isSetPosition;

		// Token: 0x02000C94 RID: 3220
		public enum Shading
		{
			// Token: 0x0400977C RID: 38780
			SURFACE,
			// Token: 0x0400977D RID: 38781
			LINE
		}
	}
}
