using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000EFF RID: 3839
	public class SelectingCursorEffect : DuelEffectHandle
	{
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06007137 RID: 28983 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007138 RID: 28984 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPosition(Vector3 srcPos, Vector3 dstPos)
		{
		}

		// Token: 0x06007139 RID: 28985 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetColor(Color col)
		{
		}

		// Token: 0x0600713A RID: 28986 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetVisible(bool visible)
		{
		}

		// Token: 0x0600713B RID: 28987 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnInitialize()
		{
		}

		// Token: 0x0600713C RID: 28988 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTerminate()
		{
		}

		// Token: 0x0600713D RID: 28989 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnUpdate()
		{
		}

		// Token: 0x0600713E RID: 28990 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnStop()
		{
		}

		// Token: 0x0400AADD RID: 43741
		private Vector3 from;

		// Token: 0x0400AADE RID: 43742
		private Vector3 to;

		// Token: 0x0400AADF RID: 43743
		private Color color;

		// Token: 0x0400AAE0 RID: 43744
		private MeshRenderer mr;

		// Token: 0x0400AAE1 RID: 43745
		private float rate;

		// Token: 0x0400AAE2 RID: 43746
		private float timer;

		// Token: 0x0400AAE3 RID: 43747
		private bool isEnd;

		// Token: 0x0400AAE4 RID: 43748
		private bool isSetPosition;
	}
}
