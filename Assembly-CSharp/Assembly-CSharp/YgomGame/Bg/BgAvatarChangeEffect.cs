using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Bg
{
	// Token: 0x0200112A RID: 4394
	public class BgAvatarChangeEffect : MonoBehaviour
	{
		// Token: 0x060082E9 RID: 33513 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060082EA RID: 33514 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060082EB RID: 33515 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayEffect(bool toMain, Action callback = null)
		{
		}

		// Token: 0x060082EC RID: 33516 RVA: 0x0000216D File Offset: 0x0000036D
		public void TraceMainCameraSetting(Camera target)
		{
		}

		// Token: 0x060082ED RID: 33517 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSePan(float pan)
		{
		}

		// Token: 0x0400BDFA RID: 48634
		private Action delayCallback;

		// Token: 0x0400BDFB RID: 48635
		private bool delayCheck;

		// Token: 0x0400BDFC RID: 48636
		private float playtime;

		// Token: 0x0400BDFD RID: 48637
		private float sePan;

		// Token: 0x0400BDFE RID: 48638
		private ElementObjectManager manager;

		// Token: 0x0400BDFF RID: 48639
		private ParticleSystem toMainObj;

		// Token: 0x0400BE00 RID: 48640
		private ParticleSystem toSubObj;

		// Token: 0x0400BE01 RID: 48641
		public float delay;

		// Token: 0x0400BE02 RID: 48642
		public string toMainLabel;

		// Token: 0x0400BE03 RID: 48643
		public string toSubLabel;

		// Token: 0x0400BE04 RID: 48644
		public string toMainSELabel;

		// Token: 0x0400BE05 RID: 48645
		public string toSubSELabel;
	}
}
