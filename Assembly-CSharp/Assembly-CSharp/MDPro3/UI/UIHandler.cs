using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013F0 RID: 5104
	public class UIHandler : MonoBehaviour
	{
		// Token: 0x06009405 RID: 37893 RVA: 0x001510A8 File Offset: 0x0014F2A8
		public virtual void Initialize()
		{
			this.showing = false;
			this.inTransition = false;
			if (this.cg != null)
			{
				this.cg.alpha = 0f;
				this.cg.blocksRaycasts = false;
			}
			if (this.shadow != null)
			{
				this.shadow.color = Color.clear;
			}
		}

		// Token: 0x06009406 RID: 37894 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void PerframeFunction()
		{
		}

		// Token: 0x06009407 RID: 37895 RVA: 0x0015110C File Offset: 0x0014F30C
		public virtual void Show()
		{
			this.showing = true;
			this.inTransition = true;
			if (this.cg != null)
			{
				this.cg.alpha = 1f;
				this.cg.blocksRaycasts = true;
			}
			if (this.shadow != null)
			{
				this.shadow.DOFade(this.shadowColor, this.transitionTime);
			}
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, this.transitionTime).OnComplete(delegate
			{
				this.inTransition = false;
			});
		}

		// Token: 0x06009408 RID: 37896 RVA: 0x001511C0 File Offset: 0x0014F3C0
		public virtual void Hide()
		{
			this.inTransition = true;
			if (this.shadow != null)
			{
				this.shadow.DOFade(0f, this.transitionTime);
				DOTween.To(delegate(float v)
				{
				}, 0f, 0f, this.transitionTime).OnComplete(delegate
				{
					if (this.cg != null)
					{
						this.cg.alpha = 0f;
						this.cg.blocksRaycasts = false;
					}
					this.showing = false;
					this.inTransition = false;
				});
			}
		}

		// Token: 0x0400D260 RID: 53856
		[Header("UI Handler")]
		public CanvasGroup cg;

		// Token: 0x0400D261 RID: 53857
		public Image shadow;

		// Token: 0x0400D262 RID: 53858
		public RectTransform window;

		// Token: 0x0400D263 RID: 53859
		public bool showing;

		// Token: 0x0400D264 RID: 53860
		public bool inTransition;

		// Token: 0x0400D265 RID: 53861
		public float transitionTime = 0.3f;

		// Token: 0x0400D266 RID: 53862
		public float shadowColor = 0.75f;
	}
}
