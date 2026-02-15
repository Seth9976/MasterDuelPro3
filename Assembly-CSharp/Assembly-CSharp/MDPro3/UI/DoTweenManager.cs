using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x020013DD RID: 5085
	public class DoTweenManager : MonoBehaviour
	{
		// Token: 0x0600934A RID: 37706 RVA: 0x0014C7D8 File Offset: 0x0014A9D8
		private void Awake()
		{
			foreach (DoTweenInitializer initializer in base.transform.GetComponentsInChildren<DoTweenInitializer>())
			{
				this.initializers.Add(initializer);
			}
		}

		// Token: 0x0600934B RID: 37707 RVA: 0x0014C810 File Offset: 0x0014AA10
		private void OnEnable()
		{
			if (this.autoPlay)
			{
				this.Show();
				return;
			}
			foreach (DoTweenInitializer ini in this.initializers)
			{
				ini.GetComponent<CanvasGroup>().alpha = ini.hideAlpha;
				ini.GetComponent<RectTransform>().localScale = ini.hideScale;
			}
		}

		// Token: 0x0600934C RID: 37708 RVA: 0x0014C890 File Offset: 0x0014AA90
		private void OnDisable()
		{
			foreach (DoTweenInitializer doTweenInitializer in this.initializers)
			{
				DOTweenAnimation[] components = doTweenInitializer.GetComponents<DOTweenAnimation>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].DOKill();
				}
			}
		}

		// Token: 0x0600934D RID: 37709 RVA: 0x0014C8F8 File Offset: 0x0014AAF8
		public void Show()
		{
			foreach (DoTweenInitializer ini in this.initializers)
			{
				foreach (DOTweenAnimation tweener in ini.GetComponents<DOTweenAnimation>())
				{
					if (tweener.id == this.showID)
					{
						if (tweener.animationType == DOTweenAnimation.AnimationType.Fade)
						{
							ini.GetComponent<CanvasGroup>().alpha = ini.hideAlpha;
						}
						else if (tweener.animationType == DOTweenAnimation.AnimationType.Scale)
						{
							ini.GetComponent<RectTransform>().localScale = ini.hideScale;
						}
						tweener.RecreateTweenAndPlay();
					}
				}
			}
		}

		// Token: 0x0600934E RID: 37710 RVA: 0x0014C9B4 File Offset: 0x0014ABB4
		public void Hide()
		{
			foreach (DoTweenInitializer ini in this.initializers)
			{
				foreach (DOTweenAnimation tweener in ini.GetComponents<DOTweenAnimation>())
				{
					if (tweener.id == this.hideID)
					{
						if (tweener.animationType == DOTweenAnimation.AnimationType.Fade)
						{
							ini.GetComponent<CanvasGroup>().alpha = ini.showAlpha;
						}
						else if (tweener.animationType == DOTweenAnimation.AnimationType.Scale)
						{
							ini.GetComponent<RectTransform>().localScale = ini.showScale;
						}
						tweener.RecreateTweenAndPlay();
					}
				}
			}
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, this.shutDownDelay).OnComplete(delegate
			{
				this.OnDisable();
			});
		}

		// Token: 0x0400D19F RID: 53663
		[SerializeField]
		private string showID = "Show";

		// Token: 0x0400D1A0 RID: 53664
		[SerializeField]
		private string hideID = "Hide";

		// Token: 0x0400D1A1 RID: 53665
		[SerializeField]
		private bool autoPlay = true;

		// Token: 0x0400D1A2 RID: 53666
		[SerializeField]
		private float shutDownDelay = 0.4f;

		// Token: 0x0400D1A3 RID: 53667
		private List<DoTweenInitializer> initializers = new List<DoTweenInitializer>();
	}
}
