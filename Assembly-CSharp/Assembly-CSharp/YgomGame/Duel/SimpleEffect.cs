using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000F15 RID: 3861
	public class SimpleEffect : DuelEffectHandle
	{
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060071BE RID: 29118 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (set) Token: 0x060071BF RID: 29119 RVA: 0x0000216D File Offset: 0x0000036D
		public float delay
		{
			set
			{
			}
		}

		// Token: 0x060071C0 RID: 29120 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnInitialize()
		{
		}

		// Token: 0x060071C1 RID: 29121 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnInitializeImpl()
		{
		}

		// Token: 0x060071C2 RID: 29122 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTerminate()
		{
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnTerminateImpl()
		{
		}

		// Token: 0x060071C4 RID: 29124 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPlay()
		{
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnStop()
		{
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnUpdate()
		{
		}

		// Token: 0x0400AB94 RID: 43924
		private List<ParticleSystem> pss;

		// Token: 0x0400AB95 RID: 43925
		private List<TrailRenderer> trs;

		// Token: 0x0400AB96 RID: 43926
		private List<Animator> animators;

		// Token: 0x0400AB97 RID: 43927
		private Dictionary<string, float[]> trailW;

		// Token: 0x0400AB98 RID: 43928
		private Dictionary<ParticleSystem, float> defaultStartDelay;

		// Token: 0x0400AB99 RID: 43929
		private float duration;

		// Token: 0x0400AB9A RID: 43930
		private float time;

		// Token: 0x0400AB9B RID: 43931
		private bool looping;
	}
}
