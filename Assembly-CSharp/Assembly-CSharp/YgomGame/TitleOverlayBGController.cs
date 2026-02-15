using System;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame
{
	// Token: 0x020007E3 RID: 2019
	public class TitleOverlayBGController : MonoBehaviour
	{
		// Token: 0x06003EB3 RID: 16051 RVA: 0x0000216D File Offset: 0x0000036D
		private static void log(string msg)
		{
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0000216D File Offset: 0x0000036D
		private void updateParticlesAlpha()
		{
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x0000216D File Offset: 0x0000036D
		private void onFinishFadeOut()
		{
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartFadeIn()
		{
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartFadeOut()
		{
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0000216D File Offset: 0x0000036D
		public void Release()
		{
		}

		// Token: 0x040037A5 RID: 14245
		private const string FadeInLabel = "In";

		// Token: 0x040037A6 RID: 14246
		private const string FadeOutLabel = "Out";

		// Token: 0x040037A7 RID: 14247
		private GameObject m_bgRoot;

		// Token: 0x040037A8 RID: 14248
		private TweenSpriteColor m_fadeInTween;

		// Token: 0x040037A9 RID: 14249
		private TweenSpriteColor m_fadeOutTween;

		// Token: 0x040037AA RID: 14250
		private SpriteRenderer m_spriteRenderer;

		// Token: 0x040037AB RID: 14251
		private ParticleSystem[] m_particleSystems;

		// Token: 0x040037AC RID: 14252
		private ParticleSystem.Particle[] m_particleWorkBuf;
	}
}
