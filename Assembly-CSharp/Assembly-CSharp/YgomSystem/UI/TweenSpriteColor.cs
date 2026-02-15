using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200063F RID: 1599
	public class TweenSpriteColor : Tween
	{
		// Token: 0x06003210 RID: 12816 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000F2811 File Offset: 0x000F0A11
		private void OnEnable()
		{
			this.m_spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.TweenColor();
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000F2828 File Offset: 0x000F0A28
		private void TweenColor()
		{
			this.m_spriteRenderer.color = this.from;
			this.m_spriteRenderer.DOColor(this.to, this.duration).SetEase(Tween.GetDGTweenEase(this.easing)).OnComplete(delegate
			{
				if (this.style.ToString().Contains("Loop"))
				{
					this.m_spriteRenderer.DOColor(this.from, this.duration).SetEase(Tween.GetDGTweenEase(this.easing)).OnComplete(delegate
					{
						if (this.style.ToString().Contains("Loop"))
						{
							this.TweenColor();
						}
					});
				}
			});
		}

		// Token: 0x04002EB0 RID: 11952
		[ColorLabelString]
		[SerializeField]
		public string fromLabel;

		// Token: 0x04002EB1 RID: 11953
		[SerializeField]
		public Color from;

		// Token: 0x04002EB2 RID: 11954
		[ColorLabelString]
		[SerializeField]
		public string toLabel;

		// Token: 0x04002EB3 RID: 11955
		[SerializeField]
		public Color to;

		// Token: 0x04002EB4 RID: 11956
		public bool isOverride;

		// Token: 0x04002EB5 RID: 11957
		public bool isRecusive;

		// Token: 0x04002EB6 RID: 11958
		private List<KeyValuePair<SpriteRenderer, Color>> childGraps;

		// Token: 0x04002EB7 RID: 11959
		private SpriteRenderer m_spriteRenderer;
	}
}
