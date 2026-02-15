using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200062B RID: 1579
	public class TweenPositionTo : Tween
	{
		// Token: 0x060031DE RID: 12766 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000F278D File Offset: 0x000F098D
		private void Start()
		{
			this.startPosition = base.transform.localPosition;
			this.PlayDIY();
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000F27A8 File Offset: 0x000F09A8
		private void PlayDIY()
		{
			base.transform.localPosition = this.startPosition;
			base.transform.DOLocalMove(this.to, this.duration, false).SetEase(Tween.GetDGTweenEase(this.easing)).OnComplete(delegate
			{
				if (this.style == Tween.Style.Loop)
				{
					this.PlayDIY();
				}
			});
		}

		// Token: 0x04002E62 RID: 11874
		private RectTransform rtrans;

		// Token: 0x04002E63 RID: 11875
		private Vector3 from;

		// Token: 0x04002E64 RID: 11876
		[SerializeField]
		public Vector3 to;

		// Token: 0x04002E65 RID: 11877
		private Vector3 startPosition;
	}
}
