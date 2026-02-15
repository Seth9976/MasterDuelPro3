using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.UI;

namespace MDPro3.UI
{
	// Token: 0x02001373 RID: 4979
	public class RectLoopMoveY : MonoBehaviour
	{
		// Token: 0x06009029 RID: 36905 RVA: 0x0013ACEC File Offset: 0x00138EEC
		private void Awake()
		{
			this._rt = base.GetComponent<RectTransform>();
			this._img = base.GetComponent<Image>();
		}

		// Token: 0x0600902A RID: 36906 RVA: 0x0013AD08 File Offset: 0x00138F08
		private void Start()
		{
			this._centerY = this._rt.anchoredPosition.y;
			foreach (TweenAlpha alpha in base.GetComponents<TweenAlpha>())
			{
				if (alpha.label == "Show" && this._img != null)
				{
					this._img.color = new Color(1f, 1f, 1f, alpha.to);
				}
			}
			foreach (TweenPosition position in base.GetComponents<TweenPosition>())
			{
				if (position.label == "Loop")
				{
					this.range = position.from.y - position.to.y;
					this.time = position.duration;
					break;
				}
			}
			if (this.time > 0f && !Mathf.Approximately(this.range, 0f))
			{
				this.StartSmoothLoop();
			}
		}

		// Token: 0x0600902B RID: 36907 RVA: 0x0013AE0C File Offset: 0x0013900C
		private void StartSmoothLoop()
		{
			this.KillTweens();
			float yMin = this._centerY - this.range;
			float yMax = this._centerY + this.range;
			float curY = this._rt.anchoredPosition.y;
			float firstTarget = ((Mathf.Abs(curY - yMax) <= Mathf.Abs(curY - yMin)) ? yMax : yMin);
			float otherTarget = ((firstTarget == yMax) ? yMin : yMax);
			float fullDist = Mathf.Abs(yMax - yMin);
			float firstDist = Mathf.Abs(curY - firstTarget);
			float firstTime = ((fullDist > 0.0001f) ? (this.time * (firstDist / fullDist)) : 0f);
			this._startupTween = this._rt.DOAnchorPosY(firstTarget, firstTime, false).SetEase(this.ease).SetUpdate(this.updateType, this.ignoreTimeScale);
			this._startupTween.OnComplete(delegate
			{
				this._loopTween = this._rt.DOAnchorPosY(otherTarget, this.time, false).SetEase(this.ease).SetLoops(-1, LoopType.Yoyo)
					.SetUpdate(this.updateType, this.ignoreTimeScale);
			});
		}

		// Token: 0x0600902C RID: 36908 RVA: 0x0013AF00 File Offset: 0x00139100
		private void OnDisable()
		{
			if (this._startupTween != null && this._startupTween.IsActive())
			{
				this._startupTween.Pause<DG.Tweening.Tween>();
			}
			if (this._loopTween != null && this._loopTween.IsActive())
			{
				this._loopTween.Pause<DG.Tweening.Tween>();
			}
		}

		// Token: 0x0600902D RID: 36909 RVA: 0x0013AF50 File Offset: 0x00139150
		private void OnEnable()
		{
			if (this._startupTween != null && this._startupTween.IsActive())
			{
				this._startupTween.Play<DG.Tweening.Tween>();
			}
			if (this._loopTween != null && this._loopTween.IsActive())
			{
				this._loopTween.Play<DG.Tweening.Tween>();
			}
		}

		// Token: 0x0600902E RID: 36910 RVA: 0x0013AF9F File Offset: 0x0013919F
		private void OnDestroy()
		{
			this.KillTweens();
		}

		// Token: 0x0600902F RID: 36911 RVA: 0x0013AFA8 File Offset: 0x001391A8
		private void KillTweens()
		{
			if (this._startupTween != null && this._startupTween.IsActive())
			{
				this._startupTween.Kill(false);
			}
			if (this._loopTween != null && this._loopTween.IsActive())
			{
				this._loopTween.Kill(false);
			}
			this._startupTween = null;
			this._loopTween = null;
		}

		// Token: 0x0400CEC6 RID: 52934
		public float range;

		// Token: 0x0400CEC7 RID: 52935
		public float time;

		// Token: 0x0400CEC8 RID: 52936
		[Header("Smoothing")]
		public Ease ease = Ease.InOutSine;

		// Token: 0x0400CEC9 RID: 52937
		[Tooltip("Normal = Update, Late = LateUpdate. Late is often smoother for UI.")]
		public UpdateType updateType = UpdateType.Late;

		// Token: 0x0400CECA RID: 52938
		[Tooltip("If true, animation ignores Time.timeScale (keeps moving in pause/slowmo).")]
		public bool ignoreTimeScale = true;

		// Token: 0x0400CECB RID: 52939
		private RectTransform _rt;

		// Token: 0x0400CECC RID: 52940
		private Image _img;

		// Token: 0x0400CECD RID: 52941
		private float _centerY;

		// Token: 0x0400CECE RID: 52942
		private DG.Tweening.Tween _startupTween;

		// Token: 0x0400CECF RID: 52943
		private DG.Tweening.Tween _loopTween;
	}
}
