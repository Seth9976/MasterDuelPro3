using System;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x0200006B RID: 107
	public class TweenParams
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00007990 File Offset: 0x00005B90
		public TweenParams()
		{
			this.Clear();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000079AC File Offset: 0x00005BAC
		public TweenParams Clear()
		{
			this.id = (this.target = null);
			this.stringId = null;
			this.intId = -999;
			this.updateType = DOTween.defaultUpdateType;
			this.isIndependentUpdate = DOTween.defaultTimeScaleIndependent;
			this.onStart = (this.onPlay = (this.onRewind = (this.onUpdate = (this.onStepComplete = (this.onComplete = (this.onKill = null))))));
			this.onWaypointChange = null;
			this.isRecyclable = DOTween.defaultRecyclable;
			this.isSpeedBased = false;
			this.autoKill = DOTween.defaultAutoKill;
			this.loops = 1;
			this.loopType = DOTween.defaultLoopType;
			this.delay = 0f;
			this.isRelative = false;
			this.easeType = Ease.Unset;
			this.customEase = null;
			this.easeOvershootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude;
			this.easePeriod = DOTween.defaultEasePeriod;
			return this;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007A9B File Offset: 0x00005C9B
		public TweenParams SetAutoKill(bool autoKillOnCompletion = true)
		{
			this.autoKill = autoKillOnCompletion;
			return this;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007AA5 File Offset: 0x00005CA5
		public TweenParams SetId(object objectId)
		{
			this.id = objectId;
			return this;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007AAF File Offset: 0x00005CAF
		public TweenParams SetId(string stringId)
		{
			this.stringId = stringId;
			return this;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007AB9 File Offset: 0x00005CB9
		public TweenParams SetId(int intId)
		{
			this.intId = intId;
			return this;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007AC3 File Offset: 0x00005CC3
		public TweenParams SetTarget(object target)
		{
			this.target = target;
			return this;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007ACD File Offset: 0x00005CCD
		public TweenParams SetLoops(int loops, LoopType? loopType = null)
		{
			if (loops < -1)
			{
				loops = -1;
			}
			else if (loops == 0)
			{
				loops = 1;
			}
			this.loops = loops;
			if (loopType != null)
			{
				this.loopType = loopType.Value;
			}
			return this;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007AFC File Offset: 0x00005CFC
		public TweenParams SetEase(Ease ease, float? overshootOrAmplitude = null, float? period = null)
		{
			this.easeType = ease;
			this.easeOvershootOrAmplitude = ((overshootOrAmplitude != null) ? overshootOrAmplitude.Value : DOTween.defaultEaseOvershootOrAmplitude);
			this.easePeriod = ((period != null) ? period.Value : DOTween.defaultEasePeriod);
			this.customEase = null;
			return this;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007B54 File Offset: 0x00005D54
		public TweenParams SetEase(AnimationCurve animCurve)
		{
			this.easeType = Ease.INTERNAL_Custom;
			this.customEase = new EaseFunction(new EaseCurve(animCurve).Evaluate);
			return this;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007B76 File Offset: 0x00005D76
		public TweenParams SetEase(EaseFunction customEase)
		{
			this.easeType = Ease.INTERNAL_Custom;
			this.customEase = customEase;
			return this;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007B88 File Offset: 0x00005D88
		public TweenParams SetRecyclable(bool recyclable = true)
		{
			this.isRecyclable = recyclable;
			return this;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007B92 File Offset: 0x00005D92
		public TweenParams SetUpdate(bool isIndependentUpdate)
		{
			this.updateType = DOTween.defaultUpdateType;
			this.isIndependentUpdate = isIndependentUpdate;
			return this;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007BA7 File Offset: 0x00005DA7
		public TweenParams SetUpdate(UpdateType updateType, bool isIndependentUpdate = false)
		{
			this.updateType = updateType;
			this.isIndependentUpdate = isIndependentUpdate;
			return this;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public TweenParams OnStart(TweenCallback action)
		{
			this.onStart = action;
			return this;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007BC2 File Offset: 0x00005DC2
		public TweenParams OnPlay(TweenCallback action)
		{
			this.onPlay = action;
			return this;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007BCC File Offset: 0x00005DCC
		public TweenParams OnRewind(TweenCallback action)
		{
			this.onRewind = action;
			return this;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007BD6 File Offset: 0x00005DD6
		public TweenParams OnUpdate(TweenCallback action)
		{
			this.onUpdate = action;
			return this;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public TweenParams OnStepComplete(TweenCallback action)
		{
			this.onStepComplete = action;
			return this;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007BEA File Offset: 0x00005DEA
		public TweenParams OnComplete(TweenCallback action)
		{
			this.onComplete = action;
			return this;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public TweenParams OnKill(TweenCallback action)
		{
			this.onKill = action;
			return this;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007BFE File Offset: 0x00005DFE
		public TweenParams OnWaypointChange(TweenCallback<int> action)
		{
			this.onWaypointChange = action;
			return this;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007C08 File Offset: 0x00005E08
		public TweenParams SetDelay(float delay)
		{
			this.delay = delay;
			return this;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007C12 File Offset: 0x00005E12
		public TweenParams SetRelative(bool isRelative = true)
		{
			this.isRelative = isRelative;
			return this;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007C1C File Offset: 0x00005E1C
		public TweenParams SetSpeedBased(bool isSpeedBased = true)
		{
			this.isSpeedBased = isSpeedBased;
			return this;
		}

		// Token: 0x040000FB RID: 251
		public static readonly TweenParams Params = new TweenParams();

		// Token: 0x040000FC RID: 252
		internal object id;

		// Token: 0x040000FD RID: 253
		internal string stringId;

		// Token: 0x040000FE RID: 254
		internal int intId = -999;

		// Token: 0x040000FF RID: 255
		internal object target;

		// Token: 0x04000100 RID: 256
		internal UpdateType updateType;

		// Token: 0x04000101 RID: 257
		internal bool isIndependentUpdate;

		// Token: 0x04000102 RID: 258
		internal TweenCallback onStart;

		// Token: 0x04000103 RID: 259
		internal TweenCallback onPlay;

		// Token: 0x04000104 RID: 260
		internal TweenCallback onRewind;

		// Token: 0x04000105 RID: 261
		internal TweenCallback onUpdate;

		// Token: 0x04000106 RID: 262
		internal TweenCallback onStepComplete;

		// Token: 0x04000107 RID: 263
		internal TweenCallback onComplete;

		// Token: 0x04000108 RID: 264
		internal TweenCallback onKill;

		// Token: 0x04000109 RID: 265
		internal TweenCallback<int> onWaypointChange;

		// Token: 0x0400010A RID: 266
		internal bool isRecyclable;

		// Token: 0x0400010B RID: 267
		internal bool isSpeedBased;

		// Token: 0x0400010C RID: 268
		internal bool autoKill;

		// Token: 0x0400010D RID: 269
		internal int loops;

		// Token: 0x0400010E RID: 270
		internal LoopType loopType;

		// Token: 0x0400010F RID: 271
		internal float delay;

		// Token: 0x04000110 RID: 272
		internal bool isRelative;

		// Token: 0x04000111 RID: 273
		internal Ease easeType;

		// Token: 0x04000112 RID: 274
		internal EaseFunction customEase;

		// Token: 0x04000113 RID: 275
		internal float easeOvershootOrAmplitude;

		// Token: 0x04000114 RID: 276
		internal float easePeriod;
	}
}
