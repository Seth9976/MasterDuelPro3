using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x0200006C RID: 108
	public static class TweenSettingsExtensions
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00007C32 File Offset: 0x00005E32
		public static T SetAutoKill<T>(this T t) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.autoKill = true;
			return t;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007C65 File Offset: 0x00005E65
		public static T SetAutoKill<T>(this T t, bool autoKillOnCompletion) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.autoKill = autoKillOnCompletion;
			return t;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007C98 File Offset: 0x00005E98
		public static T SetId<T>(this T t, object objectId) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.id = objectId;
			return t;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007CBE File Offset: 0x00005EBE
		public static T SetId<T>(this T t, string stringId) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.stringId = stringId;
			return t;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public static T SetId<T>(this T t, int intId) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.intId = intId;
			return t;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007D0C File Offset: 0x00005F0C
		public static T SetLink<T>(this T t, GameObject gameObject) where T : Tween
		{
			if (t == null || !t.active || t.isSequenced || gameObject == null)
			{
				return t;
			}
			TweenManager.AddTweenLink(t, new TweenLink(gameObject, LinkBehaviour.KillOnDestroy));
			return t;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007D5C File Offset: 0x00005F5C
		public static T SetLink<T>(this T t, GameObject gameObject, LinkBehaviour behaviour) where T : Tween
		{
			if (t == null || !t.active || t.isSequenced || gameObject == null)
			{
				return t;
			}
			TweenManager.AddTweenLink(t, new TweenLink(gameObject, behaviour));
			return t;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007DAC File Offset: 0x00005FAC
		public static T SetTarget<T>(this T t, object target) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			if (DOTween.debugStoreTargetId)
			{
				Component component = target as Component;
				t.debugTargetId = ((component != null) ? component.name : target.ToString());
			}
			t.target = target;
			return t;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007E10 File Offset: 0x00006010
		public static T SetLoops<T>(this T t, int loops) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			t.loops = loops;
			if (t.tweenType == TweenType.Tweener)
			{
				if (loops > -1)
				{
					t.fullDuration = t.duration * (float)loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			return t;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007E9C File Offset: 0x0000609C
		public static T SetLoops<T>(this T t, int loops, LoopType loopType) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			t.loops = loops;
			t.loopType = loopType;
			if (t.tweenType == TweenType.Tweener)
			{
				if (loops > -1)
				{
					t.fullDuration = t.duration * (float)loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			return t;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007F34 File Offset: 0x00006134
		public static T SetEase<T>(this T t, Ease ease) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (EaseManager.IsFlashEase(ease))
			{
				t.easeOvershootOrAmplitude = (float)((int)t.easeOvershootOrAmplitude);
			}
			t.customEase = null;
			return t;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007F94 File Offset: 0x00006194
		public static T SetEase<T>(this T t, Ease ease, float overshoot) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (EaseManager.IsFlashEase(ease))
			{
				overshoot = (float)((int)overshoot);
			}
			t.easeOvershootOrAmplitude = overshoot;
			t.customEase = null;
			return t;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007FEC File Offset: 0x000061EC
		public static T SetEase<T>(this T t, Ease ease, float amplitude, float period) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = ease;
			if (EaseManager.IsFlashEase(ease))
			{
				amplitude = (float)((int)amplitude);
			}
			t.easeOvershootOrAmplitude = amplitude;
			t.easePeriod = period;
			t.customEase = null;
			return t;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008050 File Offset: 0x00006250
		public static T SetEase<T>(this T t, AnimationCurve animCurve) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = Ease.INTERNAL_Custom;
			t.customEase = new EaseFunction(new EaseCurve(animCurve).Evaluate);
			return t;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000809E File Offset: 0x0000629E
		public static T SetEase<T>(this T t, EaseFunction customEase) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.easeType = Ease.INTERNAL_Custom;
			t.customEase = customEase;
			return t;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000080D1 File Offset: 0x000062D1
		public static T SetRecyclable<T>(this T t) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.isRecyclable = true;
			return t;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000080F7 File Offset: 0x000062F7
		public static T SetRecyclable<T>(this T t, bool recyclable) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.isRecyclable = recyclable;
			return t;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000811D File Offset: 0x0000631D
		public static T SetUpdate<T>(this T t, bool isIndependentUpdate) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, DOTween.defaultUpdateType, isIndependentUpdate);
			return t;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008148 File Offset: 0x00006348
		public static T SetUpdate<T>(this T t, UpdateType updateType) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, updateType, DOTween.defaultTimeScaleIndependent);
			return t;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008173 File Offset: 0x00006373
		public static T SetUpdate<T>(this T t, UpdateType updateType, bool isIndependentUpdate) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, updateType, isIndependentUpdate);
			return t;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000819A File Offset: 0x0000639A
		public static T SetInverted<T>(this T t) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isInverted = true;
			return t;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000081CD File Offset: 0x000063CD
		public static T SetInverted<T>(this T t, bool inverted) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isInverted = inverted;
			return t;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008200 File Offset: 0x00006400
		public static T OnStart<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onStart = action;
			return t;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008226 File Offset: 0x00006426
		public static T OnPlay<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onPlay = action;
			return t;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000824C File Offset: 0x0000644C
		public static T OnPause<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onPause = action;
			return t;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008272 File Offset: 0x00006472
		public static T OnRewind<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onRewind = action;
			return t;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008298 File Offset: 0x00006498
		public static T OnUpdate<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onUpdate = action;
			return t;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000082BE File Offset: 0x000064BE
		public static T OnStepComplete<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onStepComplete = action;
			return t;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000082E4 File Offset: 0x000064E4
		public static T OnComplete<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onComplete = action;
			return t;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000830A File Offset: 0x0000650A
		public static T OnKill<T>(this T t, TweenCallback action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onKill = action;
			return t;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008330 File Offset: 0x00006530
		public static T OnWaypointChange<T>(this T t, TweenCallback<int> action) where T : Tween
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.onWaypointChange = action;
			return t;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008358 File Offset: 0x00006558
		public static T SetAs<T>(this T t, Tween asTween) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.timeScale = asTween.timeScale;
			t.isBackwards = asTween.isBackwards;
			TweenManager.SetUpdateType(t, asTween.updateType, asTween.isIndependentUpdate);
			t.id = asTween.id;
			t.stringId = asTween.stringId;
			t.intId = asTween.intId;
			t.onStart = asTween.onStart;
			t.onPlay = asTween.onPlay;
			t.onRewind = asTween.onRewind;
			t.onUpdate = asTween.onUpdate;
			t.onStepComplete = asTween.onStepComplete;
			t.onComplete = asTween.onComplete;
			t.onKill = asTween.onKill;
			t.onWaypointChange = asTween.onWaypointChange;
			t.isRecyclable = asTween.isRecyclable;
			t.isSpeedBased = asTween.isSpeedBased;
			t.autoKill = asTween.autoKill;
			t.loops = asTween.loops;
			t.loopType = asTween.loopType;
			if (t.tweenType == TweenType.Tweener)
			{
				if (t.loops > -1)
				{
					t.fullDuration = t.duration * (float)t.loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			t.delay = asTween.delay;
			t.delayComplete = t.delay <= 0f;
			t.isRelative = asTween.isRelative;
			t.easeType = asTween.easeType;
			t.customEase = asTween.customEase;
			t.easeOvershootOrAmplitude = asTween.easeOvershootOrAmplitude;
			t.easePeriod = asTween.easePeriod;
			return t;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000085AC File Offset: 0x000067AC
		public static T SetAs<T>(this T t, TweenParams tweenParams) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			TweenManager.SetUpdateType(t, tweenParams.updateType, tweenParams.isIndependentUpdate);
			t.id = tweenParams.id;
			t.stringId = tweenParams.stringId;
			t.intId = tweenParams.intId;
			t.onStart = tweenParams.onStart;
			t.onPlay = tweenParams.onPlay;
			t.onRewind = tweenParams.onRewind;
			t.onUpdate = tweenParams.onUpdate;
			t.onStepComplete = tweenParams.onStepComplete;
			t.onComplete = tweenParams.onComplete;
			t.onKill = tweenParams.onKill;
			t.onWaypointChange = tweenParams.onWaypointChange;
			t.isRecyclable = tweenParams.isRecyclable;
			t.isSpeedBased = tweenParams.isSpeedBased;
			t.autoKill = tweenParams.autoKill;
			t.loops = tweenParams.loops;
			t.loopType = tweenParams.loopType;
			if (t.tweenType == TweenType.Tweener)
			{
				if (t.loops > -1)
				{
					t.fullDuration = t.duration * (float)t.loops;
				}
				else
				{
					t.fullDuration = float.PositiveInfinity;
				}
			}
			t.delay = tweenParams.delay;
			t.delayComplete = t.delay <= 0f;
			t.isRelative = tweenParams.isRelative;
			if (tweenParams.easeType == Ease.Unset)
			{
				if (t.tweenType == TweenType.Sequence)
				{
					t.easeType = Ease.Linear;
				}
				else
				{
					t.easeType = DOTween.defaultEaseType;
				}
			}
			else
			{
				t.easeType = tweenParams.easeType;
			}
			t.customEase = tweenParams.customEase;
			t.easeOvershootOrAmplitude = tweenParams.easeOvershootOrAmplitude;
			t.easePeriod = tweenParams.easePeriod;
			return t;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008811 File Offset: 0x00006A11
		public static Sequence Append(this Sequence s, Tween t)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, t, false))
			{
				return s;
			}
			Sequence.DoInsert(s, t, s.duration);
			return s;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000882E File Offset: 0x00006A2E
		public static Sequence Prepend(this Sequence s, Tween t)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, t, false))
			{
				return s;
			}
			Sequence.DoPrepend(s, t);
			return s;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008845 File Offset: 0x00006A45
		public static Sequence Join(this Sequence s, Tween t)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, t, false))
			{
				return s;
			}
			Sequence.DoInsert(s, t, s.lastTweenInsertTime);
			return s;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008862 File Offset: 0x00006A62
		public static Sequence Insert(this Sequence s, float atPosition, Tween t)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, t, false))
			{
				return s;
			}
			Sequence.DoInsert(s, t, atPosition);
			return s;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000887A File Offset: 0x00006A7A
		public static Sequence AppendInterval(this Sequence s, float interval)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, null, true))
			{
				return s;
			}
			Sequence.DoAppendInterval(s, interval);
			return s;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008891 File Offset: 0x00006A91
		public static Sequence PrependInterval(this Sequence s, float interval)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, null, true))
			{
				return s;
			}
			Sequence.DoPrependInterval(s, interval);
			return s;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000088A8 File Offset: 0x00006AA8
		public static Sequence AppendCallback(this Sequence s, TweenCallback callback)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, null, true))
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, s.duration);
			return s;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000088CA File Offset: 0x00006ACA
		public static Sequence PrependCallback(this Sequence s, TweenCallback callback)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, null, true))
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, 0f);
			return s;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000088EB File Offset: 0x00006AEB
		public static Sequence InsertCallback(this Sequence s, float atPosition, TweenCallback callback)
		{
			if (!TweenSettingsExtensions.ValidateAddToSequence(s, null, true))
			{
				return s;
			}
			if (callback == null)
			{
				return s;
			}
			Sequence.DoInsertCallback(s, callback, atPosition);
			return s;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008908 File Offset: 0x00006B08
		private static bool ValidateAddToSequence(Sequence s, Tween t, bool ignoreTween = false)
		{
			if (s == null)
			{
				Debugger.Sequence.LogAddToNullSequence();
				return false;
			}
			if (!s.active)
			{
				Debugger.Sequence.LogAddToInactiveSequence();
				return false;
			}
			if (s.creationLocked)
			{
				Debugger.Sequence.LogAddToLockedSequence();
				return false;
			}
			if (!ignoreTween)
			{
				if (t == null)
				{
					Debugger.Sequence.LogAddNullTween();
					return false;
				}
				if (!t.active)
				{
					Debugger.Sequence.LogAddInactiveTween(t);
					return false;
				}
				if (t.isSequenced)
				{
					Debugger.Sequence.LogAddAlreadySequencedTween(t);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000896B File Offset: 0x00006B6B
		public static T From<T>(this T t) where T : Tweener
		{
			return t.From(true, false);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008975 File Offset: 0x00006B75
		public static T From<T>(this T t, bool isRelative) where T : Tweener
		{
			return t.From(true, isRelative);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008980 File Offset: 0x00006B80
		public static T From<T>(this T t, bool setImmediately, bool isRelative) where T : Tweener
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			if (setImmediately)
			{
				t.SetFrom(isRelative && !t.isBlendable);
			}
			else
			{
				t.isRelative = isRelative;
			}
			return t;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000089FC File Offset: 0x00006BFC
		public static TweenerCore<T1, T2, TPlugOptions> From<T1, T2, TPlugOptions>(this TweenerCore<T1, T2, TPlugOptions> t, T2 fromValue, bool setImmediately = true, bool isRelative = false) where TPlugOptions : struct, IPlugOptions
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(fromValue, setImmediately, isRelative);
			return t;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008A30 File Offset: 0x00006C30
		public static TweenerCore<Color, Color, ColorOptions> From(this TweenerCore<Color, Color, ColorOptions> t, float fromAlphaValue, bool setImmediately = true, bool isRelative = false)
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(new Color(0f, 0f, 0f, fromAlphaValue), setImmediately, isRelative);
			return t;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008A80 File Offset: 0x00006C80
		public static TweenerCore<Vector3, Vector3, VectorOptions> From(this TweenerCore<Vector3, Vector3, VectorOptions> t, float fromValue, bool setImmediately = true, bool isRelative = false)
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(new Vector3(fromValue, fromValue, fromValue), setImmediately, isRelative);
			return t;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008AB8 File Offset: 0x00006CB8
		public static TweenerCore<Vector2, Vector2, CircleOptions> From(this TweenerCore<Vector2, Vector2, CircleOptions> t, float fromValueDegrees, bool setImmediately = true, bool isRelative = false)
		{
			if (t == null || !t.active || t.creationLocked || !t.isFromAllowed)
			{
				return t;
			}
			t.isFrom = true;
			t.SetFrom(new Vector2(fromValueDegrees, 0f), setImmediately, isRelative);
			return t;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public static T SetDelay<T>(this T t, float delay) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (t.tweenType == TweenType.Sequence)
			{
				(t as Sequence).PrependInterval(delay);
			}
			else
			{
				t.delay = delay;
				t.delayComplete = delay <= 0f;
			}
			return t;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008B6C File Offset: 0x00006D6C
		public static T SetDelay<T>(this T t, float delay, bool asPrependedIntervalIfSequence) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			if (t.tweenType != TweenType.Sequence || !asPrependedIntervalIfSequence)
			{
				t.delay = delay;
				t.delayComplete = delay <= 0f;
			}
			else
			{
				(t as Sequence).PrependInterval(delay);
			}
			return t;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public static T SetRelative<T>(this T t) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked || t.isFrom || t.isBlendable)
			{
				return t;
			}
			t.isRelative = true;
			return t;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008C40 File Offset: 0x00006E40
		public static T SetRelative<T>(this T t, bool isRelative) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked || t.isFrom || t.isBlendable)
			{
				return t;
			}
			t.isRelative = isRelative;
			return t;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008C98 File Offset: 0x00006E98
		public static T SetSpeedBased<T>(this T t) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isSpeedBased = true;
			return t;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00008CCB File Offset: 0x00006ECB
		public static T SetSpeedBased<T>(this T t, bool isSpeedBased) where T : Tween
		{
			if (t == null || !t.active || t.creationLocked)
			{
				return t;
			}
			t.isSpeedBased = isSpeedBased;
			return t;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00008CFE File Offset: 0x00006EFE
		public static Tweener SetOptions(this TweenerCore<float, float, FloatOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008D1A File Offset: 0x00006F1A
		public static Tweener SetOptions(this TweenerCore<Vector2, Vector2, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008D36 File Offset: 0x00006F36
		public static Tweener SetOptions(this TweenerCore<Vector2, Vector2, VectorOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008D5E File Offset: 0x00006F5E
		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008D7A File Offset: 0x00006F7A
		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3, VectorOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008DA2 File Offset: 0x00006FA2
		public static Tweener SetOptions(this TweenerCore<Vector4, Vector4, VectorOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008DBE File Offset: 0x00006FBE
		public static Tweener SetOptions(this TweenerCore<Vector4, Vector4, VectorOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008DE6 File Offset: 0x00006FE6
		public static Tweener SetOptions(this TweenerCore<Quaternion, Vector3, QuaternionOptions> t, bool useShortest360Route = true)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.rotateMode = (useShortest360Route ? RotateMode.Fast : RotateMode.FastBeyond360);
			return t;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008E08 File Offset: 0x00007008
		public static Tweener SetOptions(this TweenerCore<Color, Color, ColorOptions> t, bool alphaOnly)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.alphaOnly = alphaOnly;
			return t;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008E24 File Offset: 0x00007024
		public static Tweener SetOptions(this TweenerCore<Rect, Rect, RectOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008E40 File Offset: 0x00007040
		public static Tweener SetOptions(this TweenerCore<string, string, StringOptions> t, bool richTextEnabled, ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.richTextEnabled = richTextEnabled;
			t.plugOptions.scrambleMode = scrambleMode;
			if (!string.IsNullOrEmpty(scrambleChars))
			{
				if (scrambleChars.Length <= 1)
				{
					scrambleChars += scrambleChars;
				}
				t.plugOptions.scrambledChars = scrambleChars.ToCharArray();
				t.plugOptions.scrambledChars.ScrambleChars();
			}
			return t;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008EAE File Offset: 0x000070AE
		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, bool snapping)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008ECA File Offset: 0x000070CA
		public static Tweener SetOptions(this TweenerCore<Vector3, Vector3[], Vector3ArrayOptions> t, AxisConstraint axisConstraint, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.axisConstraint = axisConstraint;
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008EF2 File Offset: 0x000070F2
		public static Tweener SetOptions(this TweenerCore<Vector2, Vector2, CircleOptions> t, float endValueDegrees, bool relativeCenter = true, bool snapping = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.endValueDegrees = endValueDegrees;
			t.plugOptions.relativeCenter = relativeCenter;
			t.plugOptions.snapping = snapping;
			return t;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008F26 File Offset: 0x00007126
		public static TweenerCore<Vector3, Path, PathOptions> SetOptions(this TweenerCore<Vector3, Path, PathOptions> t, AxisConstraint lockPosition, AxisConstraint lockRotation = AxisConstraint.None)
		{
			return t.SetOptions(false, lockPosition, lockRotation);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008F31 File Offset: 0x00007131
		public static TweenerCore<Vector3, Path, PathOptions> SetOptions(this TweenerCore<Vector3, Path, PathOptions> t, bool closePath, AxisConstraint lockPosition = AxisConstraint.None, AxisConstraint lockRotation = AxisConstraint.None)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.isClosedPath = closePath;
			t.plugOptions.lockPositionAxis = lockPosition;
			t.plugOptions.lockRotationAxis = lockRotation;
			return t;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008F65 File Offset: 0x00007165
		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, Vector3 lookAtPosition, Vector3? forwardDirection = null, Vector3? up = null)
		{
			return t.SetLookAt(OrientType.LookAtPosition, lookAtPosition, null, -1f, forwardDirection, up, false);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008F78 File Offset: 0x00007178
		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, Vector3 lookAtPosition, bool stableZRotation)
		{
			return t.SetLookAt(OrientType.LookAtPosition, lookAtPosition, null, -1f, default(Vector3?), default(Vector3?), stableZRotation);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00008FA6 File Offset: 0x000071A6
		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, Transform lookAtTransform, Vector3? forwardDirection = null, Vector3? up = null)
		{
			return t.SetLookAt(OrientType.LookAtTransform, Vector3.zero, lookAtTransform, -1f, forwardDirection, up, false);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008FC0 File Offset: 0x000071C0
		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, Transform lookAtTransform, bool stableZRotation)
		{
			return t.SetLookAt(OrientType.LookAtTransform, Vector3.zero, lookAtTransform, -1f, default(Vector3?), default(Vector3?), stableZRotation);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00008FF2 File Offset: 0x000071F2
		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, float lookAhead, Vector3? forwardDirection = null, Vector3? up = null)
		{
			return t.SetLookAt(OrientType.ToPath, Vector3.zero, null, lookAhead, forwardDirection, up, false);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009008 File Offset: 0x00007208
		public static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, float lookAhead, bool stableZRotation)
		{
			return t.SetLookAt(OrientType.ToPath, Vector3.zero, null, lookAhead, default(Vector3?), default(Vector3?), stableZRotation);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009038 File Offset: 0x00007238
		private static TweenerCore<Vector3, Path, PathOptions> SetLookAt(this TweenerCore<Vector3, Path, PathOptions> t, OrientType orientType, Vector3 lookAtPosition, Transform lookAtTransform, float lookAhead, Vector3? forwardDirection = null, Vector3? up = null, bool stableZRotation = false)
		{
			if (t == null || !t.active)
			{
				return t;
			}
			t.plugOptions.orientType = orientType;
			switch (orientType)
			{
			case OrientType.ToPath:
				if (lookAhead < 0.0001f)
				{
					lookAhead = 0.0001f;
				}
				t.plugOptions.lookAhead = lookAhead;
				break;
			case OrientType.LookAtTransform:
				t.plugOptions.lookAtTransform = lookAtTransform;
				break;
			case OrientType.LookAtPosition:
				t.plugOptions.lookAtPosition = lookAtPosition;
				break;
			}
			t.plugOptions.lookAtPosition = lookAtPosition;
			t.plugOptions.stableZRotation = stableZRotation;
			t.SetPathForwardDirection(forwardDirection, up);
			return t;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000090D4 File Offset: 0x000072D4
		private static void SetPathForwardDirection(this TweenerCore<Vector3, Path, PathOptions> t, Vector3? forwardDirection = null, Vector3? up = null)
		{
			if (t == null || !t.active)
			{
				return;
			}
			bool flag;
			if (forwardDirection != null)
			{
				Vector3? vector = forwardDirection;
				Vector3 vector2 = Vector3.zero;
				if (vector == null || (vector != null && vector.GetValueOrDefault() != vector2))
				{
					flag = true;
					goto IL_0086;
				}
			}
			if (up != null)
			{
				Vector3? vector = up;
				Vector3 vector2 = Vector3.zero;
				flag = vector == null || (vector != null && vector.GetValueOrDefault() != vector2);
			}
			else
			{
				flag = false;
			}
			IL_0086:
			t.plugOptions.hasCustomForwardDirection = flag;
			if (t.plugOptions.hasCustomForwardDirection)
			{
				Vector3? vector = forwardDirection;
				Vector3 vector2 = Vector3.zero;
				if (vector != null && (vector == null || vector.GetValueOrDefault() == vector2))
				{
					forwardDirection = new Vector3?(Vector3.forward);
				}
				t.plugOptions.forward = Quaternion.LookRotation((forwardDirection == null) ? Vector3.forward : forwardDirection.Value, (up == null) ? Vector3.up : up.Value);
			}
		}
	}
}
