using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000ECD RID: 3789
	public class ManualAimingEffect : DuelEffectHandle
	{
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06006E73 RID: 28275 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06006E74 RID: 28276 RVA: 0x000F6110 File Offset: 0x000F4310
		// (set) Token: 0x06006E75 RID: 28277 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 from
		{
			[CompilerGenerated]
			get
			{
				return default(Vector3);
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06006E76 RID: 28278 RVA: 0x000F6128 File Offset: 0x000F4328
		// (set) Token: 0x06006E77 RID: 28279 RVA: 0x0000216D File Offset: 0x0000036D
		public Vector3 to
		{
			[CompilerGenerated]
			get
			{
				return default(Vector3);
			}
			[CompilerGenerated]
			protected set
			{
			}
		}

		// Token: 0x06006E78 RID: 28280 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnInitialize()
		{
		}

		// Token: 0x06006E79 RID: 28281 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTerminate()
		{
		}

		// Token: 0x06006E7A RID: 28282 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPlay()
		{
		}

		// Token: 0x06006E7B RID: 28283 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnStop()
		{
		}

		// Token: 0x06006E7C RID: 28284 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnUpdate()
		{
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPosition(Vector3 from, Vector3 to)
		{
		}

		// Token: 0x06006E7E RID: 28286 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetColor(Color col)
		{
		}

		// Token: 0x06006E7F RID: 28287 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetShading(BattleAimingEffect.Shading shading)
		{
		}

		// Token: 0x06006E80 RID: 28288 RVA: 0x0000216D File Offset: 0x0000036D
		private void Show()
		{
		}

		// Token: 0x06006E81 RID: 28289 RVA: 0x0000216D File Offset: 0x0000036D
		private void Hide()
		{
		}

		// Token: 0x0400A95B RID: 43355
		private ManualAimingEffect.Step step;

		// Token: 0x0400A95C RID: 43356
		private Vector3 aim;

		// Token: 0x0400A95D RID: 43357
		private bool playing;

		// Token: 0x0400A95E RID: 43358
		private float lengthRate;

		// Token: 0x0400A95F RID: 43359
		private float alpha;

		// Token: 0x0400A960 RID: 43360
		private Color color;

		// Token: 0x0400A961 RID: 43361
		private BattleAimingEffect.Shading shading;

		// Token: 0x02000ECE RID: 3790
		private enum Step
		{
			// Token: 0x0400A963 RID: 43363
			MAIN,
			// Token: 0x0400A964 RID: 43364
			FADEOUT,
			// Token: 0x0400A965 RID: 43365
			END
		}
	}
}
