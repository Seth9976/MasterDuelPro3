using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x02000613 RID: 1555
	public abstract class Tween : MonoBehaviour
	{
		// Token: 0x06003183 RID: 12675 RVA: 0x000F2310 File Offset: 0x000F0510
		public static Ease GetDGTweenEase(Tween.Easing ease)
		{
			switch (ease)
			{
			case Tween.Easing.Linear:
				return Ease.Linear;
			case Tween.Easing.CubicIn:
				return Ease.InCubic;
			case Tween.Easing.CubicOut:
				return Ease.OutCubic;
			case Tween.Easing.CubicInOut:
				return Ease.InOutCubic;
			case Tween.Easing.BackIn:
				return Ease.InBack;
			case Tween.Easing.BackOut:
				return Ease.OutBack;
			case Tween.Easing.BackInOut:
				return Ease.InOutBack;
			case Tween.Easing.BounceIn:
				return Ease.InBounce;
			case Tween.Easing.BounceOut:
				return Ease.OutBounce;
			case Tween.Easing.BounceInOut:
				return Ease.InOutBounce;
			case Tween.Easing.QuartIn:
				return Ease.InQuart;
			case Tween.Easing.QuartOut:
				return Ease.OutQuart;
			case Tween.Easing.QuartInOut:
				return Ease.InOutQuart;
			}
			return Ease.Linear;
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000F2384 File Offset: 0x000F0584
		private static float BounceOut(float k)
		{
			if (k < 0.36363637f)
			{
				return 7.5625f * k * k;
			}
			if (k < 0.72727275f)
			{
				return 7.5625f * (k -= 0.54545456f) * k + 0.75f;
			}
			if ((double)k < 0.9090909090909091)
			{
				return 7.5625f * (k -= 0.8181818f) * k + 0.9375f;
			}
			return 7.5625f * (k -= 0.95454544f) * k + 0.984375f;
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000F2402 File Offset: 0x000F0602
		private static float BounceIn(float k)
		{
			return 1f - Tween.BounceOut(1f - k);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000F2418 File Offset: 0x000F0618
		public static float EasingValue(float k, Tween.Easing e)
		{
			switch (e)
			{
			case Tween.Easing.Linear:
				return k;
			case Tween.Easing.CubicIn:
				return k * k * k;
			case Tween.Easing.CubicOut:
				return 1f - Mathf.Pow(1f - k, 3f);
			case Tween.Easing.CubicInOut:
				if ((double)k >= 0.5)
				{
					return 1f - Mathf.Pow(-2f * k + 2f, 3f) / 2f;
				}
				return 4f * k * k * k;
			case Tween.Easing.BackIn:
				return k * k * (2.70158f * k - 1.70158f);
			case Tween.Easing.BackOut:
				return 1f + (k -= 1f) * k * (2.70158f * k + 1.70158f);
			case Tween.Easing.BackInOut:
			{
				float s = 2.5949094f;
				if ((k *= 2f) >= 1f)
				{
					return 0.5f * ((k -= 2f) * k * ((s + 1f) * k + s) + 2f);
				}
				return 0.5f * (k * k * ((s + 1f) * k - s));
			}
			case Tween.Easing.BounceIn:
				return Tween.BounceIn(k);
			case Tween.Easing.BounceOut:
				return Tween.BounceOut(k);
			case Tween.Easing.BounceInOut:
				if (k >= 0.5f)
				{
					return Tween.BounceOut(k * 2f - 1f) * 0.5f + 0.5f;
				}
				return Tween.BounceIn(k * 2f) * 0.5f;
			case Tween.Easing.QuartIn:
				return k * k * k * k;
			case Tween.Easing.QuartOut:
				return 1f - Mathf.Pow(1f - k, 4f);
			case Tween.Easing.QuartInOut:
				if ((double)k >= 0.5)
				{
					return 1f - Mathf.Pow(-2f * k + 2f, 4f) / 2f;
				}
				return 8f * k * k * k * k;
			}
			return k;
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000F25F2 File Offset: 0x000F07F2
		private float GetEasing(float k)
		{
			if (this.easing != Tween.Easing.Customize)
			{
				return Tween.EasingValue(k, this.easing);
			}
			return this.curve.Evaluate(k);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void CaptureAwake()
		{
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void CaptureFrom()
		{
		}

		// Token: 0x0600318A RID: 12682
		protected abstract void OnSetValue(float par);

		// Token: 0x0600318B RID: 12683 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x0000216D File Offset: 0x0000036D
		private void ExecSetup()
		{
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000F2618 File Offset: 0x000F0818
		private void ExecPlay(float time, bool forceUpdate = false)
		{
			this.crntTime = Mathf.Clamp(time, 0f, this.duration);
			float t = ((this.duration > 0f) ? (this.crntTime / this.duration) : 1f);
			Tween.Style style = this.style;
			if (style != Tween.Style.Loop)
			{
				if (style == Tween.Style.PingPong)
				{
					t = Mathf.PingPong(t, 1f);
				}
			}
			else
			{
				t %= 1f;
			}
			this.OnSetValue(this.GetEasing(t));
			if (this.crntTime >= this.duration)
			{
				if (this.style == Tween.Style.Once)
				{
					this.isExecFinished = true;
					UnityEvent unityEvent = this.onFinished;
					if (unityEvent != null)
					{
						unityEvent.Invoke();
					}
					if (this.callOnFinishedDestroy)
					{
						this.DestroySelf();
						return;
					}
				}
				else if (this.style == Tween.Style.Loop)
				{
					this.ResetWithTimeDelta();
				}
			}
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000F26DE File Offset: 0x000F08DE
		public void Play()
		{
			if (this.setupWait > 0f)
			{
				this.setupWaitCount = this.setupWait;
			}
			this.isExecFinished = false;
			base.enabled = true;
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x0000216D File Offset: 0x0000036D
		public void Pause()
		{
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop()
		{
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x0000216D File Offset: 0x0000036D
		public void End()
		{
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reset()
		{
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000F2707 File Offset: 0x000F0907
		public void ResetWithTimeDelta()
		{
			this.crntTime = 0f;
			this.timeDelta = 0f;
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x0000216D File Offset: 0x0000036D
		public void GotoAndPlay(float time)
		{
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x0000216D File Offset: 0x0000036D
		public void GotoAndPause(float time)
		{
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x0000216D File Offset: 0x0000036D
		public void DestroySelf()
		{
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayLabel(string _label)
		{
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlaying(string _label = "", bool isActive = false)
		{
			return false;
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFinished()
		{
			return false;
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetPlayLabel(GameObject target, string _label = "", bool includeChildren = false, bool wakeup = false)
		{
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool TargetIsPlaying(GameObject target, string _label = "", bool includeChildren = false, bool isActive = false)
		{
			return false;
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetGotoAndPlayLabel(GameObject target, float time, string _label = "", bool includeChildren = false, bool wakeup = false)
		{
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetGotoAndPauseLabel(GameObject target, float time, string _label = "", bool includeChildren = false, bool wakeup = false)
		{
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetPauseLabel(GameObject target, string _label = "", bool includeChildren = false)
		{
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetStopLabel(GameObject target, string _label = "", bool includeChildren = false, string exlabel = "")
		{
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetEndLabel(GameObject target, string _label = "", bool includeChildren = false)
		{
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetForwardLabel(GameObject target, float sec, string _label = "", bool includeChildren = false)
		{
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetCaptureFrom(GameObject target, string _label = "", bool includeChildren = false, bool force = false)
		{
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x0000216D File Offset: 0x0000036D
		public static void TargetCaptureFrom(Tween tween, bool force)
		{
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AllPlayLabel(string label)
		{
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AllStopLabel(string label)
		{
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AllPauseLabel(string label)
		{
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AllEndLabel(string label)
		{
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000F2720 File Offset: 0x000F0920
		public static List<Tween> GetTweenTarget(GameObject target, string _label = "", bool includeChildren = false)
		{
			List<Tween> result = new List<Tween>();
			foreach (Tween t in includeChildren ? target.GetComponentsInChildren<Tween>() : target.GetComponents<Tween>())
			{
				if (string.IsNullOrEmpty(_label) || t.label == _label)
				{
					result.Add(t);
				}
			}
			return result;
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<Tween> GetTweenAll(string label)
		{
			return null;
		}

		// Token: 0x04002DED RID: 11757
		private static readonly float FRAMERATE_LIMIT;

		// Token: 0x04002DEE RID: 11758
		public string label;

		// Token: 0x04002DEF RID: 11759
		[SerializeField]
		public Tween.Easing easing;

		// Token: 0x04002DF0 RID: 11760
		[SerializeField]
		public Tween.Style style;

		// Token: 0x04002DF1 RID: 11761
		[SecField]
		[SerializeField]
		public float duration;

		// Token: 0x04002DF2 RID: 11762
		[SerializeField]
		[SecField]
		public float setupWait;

		// Token: 0x04002DF3 RID: 11763
		[SecField]
		[SerializeField]
		public float startDelay;

		// Token: 0x04002DF4 RID: 11764
		[SerializeField]
		public bool ignoreTimeScale;

		// Token: 0x04002DF5 RID: 11765
		[SerializeField]
		public UnityEvent onFinished;

		// Token: 0x04002DF6 RID: 11766
		[SerializeField]
		public bool callOnFinishedDestroy;

		// Token: 0x04002DF7 RID: 11767
		[HideInInspector]
		public AnimationCurve curve;

		// Token: 0x04002DF8 RID: 11768
		protected float timeDelta;

		// Token: 0x04002DF9 RID: 11769
		protected float crntTime;

		// Token: 0x04002DFA RID: 11770
		private bool isCaptured;

		// Token: 0x04002DFB RID: 11771
		private float setupWaitCount;

		// Token: 0x04002DFC RID: 11772
		private bool isExecFinished;

		// Token: 0x02000614 RID: 1556
		public enum Easing
		{
			// Token: 0x04002DFE RID: 11774
			Linear,
			// Token: 0x04002DFF RID: 11775
			CubicIn,
			// Token: 0x04002E00 RID: 11776
			CubicOut,
			// Token: 0x04002E01 RID: 11777
			CubicInOut,
			// Token: 0x04002E02 RID: 11778
			BackIn,
			// Token: 0x04002E03 RID: 11779
			BackOut,
			// Token: 0x04002E04 RID: 11780
			BackInOut,
			// Token: 0x04002E05 RID: 11781
			BounceIn,
			// Token: 0x04002E06 RID: 11782
			BounceOut,
			// Token: 0x04002E07 RID: 11783
			BounceInOut,
			// Token: 0x04002E08 RID: 11784
			Customize,
			// Token: 0x04002E09 RID: 11785
			QuartIn,
			// Token: 0x04002E0A RID: 11786
			QuartOut,
			// Token: 0x04002E0B RID: 11787
			QuartInOut
		}

		// Token: 0x02000615 RID: 1557
		public enum Style
		{
			// Token: 0x04002E0D RID: 11789
			Once,
			// Token: 0x04002E0E RID: 11790
			Loop,
			// Token: 0x04002E0F RID: 11791
			PingPong,
			// Token: 0x04002E10 RID: 11792
			PingPongLoop,
			// Token: 0x04002E11 RID: 11793
			SyncLoop,
			// Token: 0x04002E12 RID: 11794
			SyncPingPongLoop
		}
	}
}
