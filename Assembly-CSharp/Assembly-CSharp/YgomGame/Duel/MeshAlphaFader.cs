using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000ED2 RID: 3794
	public class MeshAlphaFader : MonoBehaviour
	{
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06006E87 RID: 28295 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isShowing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06006E88 RID: 28296 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isHiding
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06006E8A RID: 28298 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006E8B RID: 28299 RVA: 0x0000216D File Offset: 0x0000036D
		private void Finish()
		{
		}

		// Token: 0x06006E8C RID: 28300 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartFade(MeshAlphaFader.FadeType fadeType, float dulation, bool recursively, Action onFinished)
		{
		}

		// Token: 0x06006E8D RID: 28301 RVA: 0x0000216D File Offset: 0x0000036D
		public void Abort()
		{
		}

		// Token: 0x06006E8E RID: 28302 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMeshAlpha(float alpha)
		{
		}

		// Token: 0x06006E8F RID: 28303 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetAlphaFade()
		{
		}

		// Token: 0x0400A96B RID: 43371
		private MeshAlphaFader.FadeType fadeType;

		// Token: 0x0400A96C RID: 43372
		private float alphaTime;

		// Token: 0x0400A96D RID: 43373
		private Action onFinishedAlpha;

		// Token: 0x0400A96E RID: 43374
		private float dulation;

		// Token: 0x0400A96F RID: 43375
		private bool recursively;

		// Token: 0x02000ED3 RID: 3795
		public enum FadeType
		{
			// Token: 0x0400A971 RID: 43377
			FadeIn,
			// Token: 0x0400A972 RID: 43378
			FadeOut
		}
	}
}
