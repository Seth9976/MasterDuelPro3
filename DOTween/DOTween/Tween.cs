using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	// Token: 0x0200006E RID: 110
	public abstract class Tween : ABSSequentiable
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000091F2 File Offset: 0x000073F2
		// (set) Token: 0x0600028E RID: 654 RVA: 0x000091FA File Offset: 0x000073FA
		public bool isRelative { get; internal set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009203 File Offset: 0x00007403
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000920B File Offset: 0x0000740B
		public bool active { get; internal set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009214 File Offset: 0x00007414
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000921D File Offset: 0x0000741D
		public float fullPosition
		{
			get
			{
				return this.Elapsed(true);
			}
			set
			{
				this.Goto(value, this.isPlaying);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000922C File Offset: 0x0000742C
		public bool hasLoops
		{
			get
			{
				return this.loops == -1 || this.loops > 1;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00009242 File Offset: 0x00007442
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000924A File Offset: 0x0000744A
		public bool playedOnce { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00009253 File Offset: 0x00007453
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000925B File Offset: 0x0000745B
		public float position { get; internal set; }

		// Token: 0x06000298 RID: 664 RVA: 0x00009264 File Offset: 0x00007464
		internal virtual void Reset()
		{
			this.timeScale = 1f;
			this.isBackwards = false;
			this.id = null;
			this.stringId = null;
			this.intId = -999;
			this.isIndependentUpdate = false;
			this.onStart = (this.onPlay = (this.onRewind = (this.onUpdate = (this.onComplete = (this.onStepComplete = (this.onKill = null))))));
			this.onWaypointChange = null;
			this.debugTargetId = null;
			this.target = null;
			this.isFrom = false;
			this.isBlendable = false;
			this.isSpeedBased = false;
			this.duration = 0f;
			this.loops = 1;
			this.delay = 0f;
			this.isRelative = false;
			this.customEase = null;
			this.isSequenced = false;
			this.sequenceParent = null;
			this.specialStartupMode = SpecialStartupMode.None;
			this.creationLocked = (this.startupDone = (this.playedOnce = false));
			this.position = (this.fullDuration = (float)(this.completedLoops = 0));
			this.isPlaying = (this.isComplete = false);
			this.elapsedDelay = 0f;
			this.delayComplete = true;
			this.miscInt = -1;
		}

		// Token: 0x06000299 RID: 665
		internal abstract bool Validate();

		// Token: 0x0600029A RID: 666 RVA: 0x000093A6 File Offset: 0x000075A6
		internal virtual float UpdateDelay(float elapsed)
		{
			return 0f;
		}

		// Token: 0x0600029B RID: 667
		internal abstract bool Startup();

		// Token: 0x0600029C RID: 668
		internal abstract bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice);

		// Token: 0x0600029D RID: 669 RVA: 0x000093B0 File Offset: 0x000075B0
		internal static bool DoGoto(Tween t, float toPosition, int toCompletedLoops, UpdateMode updateMode)
		{
			if (!t.startupDone && !t.Startup())
			{
				return true;
			}
			if (!t.playedOnce && updateMode == UpdateMode.Update)
			{
				t.playedOnce = true;
				if (t.onStart != null)
				{
					Tween.OnTweenCallback(t.onStart, t);
					if (!t.active)
					{
						return true;
					}
				}
				if (t.onPlay != null)
				{
					Tween.OnTweenCallback(t.onPlay, t);
					if (!t.active)
					{
						return true;
					}
				}
			}
			float position = t.position;
			int num = t.completedLoops;
			t.completedLoops = toCompletedLoops;
			bool flag = t.position <= 0f && num <= 0;
			bool flag2 = t.isComplete;
			if (t.loops != -1)
			{
				t.isComplete = t.completedLoops == t.loops;
			}
			int num2 = 0;
			if (updateMode == UpdateMode.Update)
			{
				if (t.isBackwards)
				{
					num2 = ((t.completedLoops < num) ? (num - t.completedLoops) : ((toPosition <= 0f && !flag) ? 1 : 0));
					if (flag2)
					{
						num2--;
					}
				}
				else
				{
					num2 = ((t.completedLoops > num) ? (t.completedLoops - num) : 0);
				}
			}
			else if (t.tweenType == TweenType.Sequence)
			{
				num2 = num - toCompletedLoops;
				if (num2 < 0)
				{
					num2 = -num2;
				}
			}
			t.position = toPosition;
			if (t.position > t.duration)
			{
				t.position = t.duration;
			}
			else if (t.position <= 0f)
			{
				if (t.completedLoops > 0 || t.isComplete)
				{
					t.position = t.duration;
				}
				else
				{
					t.position = 0f;
				}
			}
			bool flag3 = t.isPlaying;
			if (t.isPlaying)
			{
				if (!t.isBackwards)
				{
					t.isPlaying = !t.isComplete;
				}
				else
				{
					t.isPlaying = t.completedLoops != 0 || t.position > 0f;
				}
			}
			bool flag4 = t.hasLoops && t.loopType == LoopType.Yoyo && ((t.position < t.duration) ? (t.completedLoops % 2 != 0) : (t.completedLoops % 2 == 0));
			UpdateNotice updateNotice = ((!flag && ((t.loopType == LoopType.Restart && t.completedLoops != num && (t.loops == -1 || t.completedLoops < t.loops)) || (t.position <= 0f && t.completedLoops <= 0))) ? UpdateNotice.RewindStep : UpdateNotice.None);
			if (t.ApplyTween(position, num, num2, flag4, updateMode, updateNotice))
			{
				return true;
			}
			if (t.onUpdate != null && updateMode != UpdateMode.IgnoreOnUpdate)
			{
				Tween.OnTweenCallback(t.onUpdate, t);
			}
			if (t.position <= 0f && t.completedLoops <= 0 && !flag && t.onRewind != null)
			{
				Tween.OnTweenCallback(t.onRewind, t);
			}
			if (num2 > 0 && updateMode == UpdateMode.Update && t.onStepComplete != null)
			{
				for (int i = 0; i < num2; i++)
				{
					Tween.OnTweenCallback(t.onStepComplete, t);
					if (!t.active)
					{
						break;
					}
				}
			}
			if (t.isComplete && !flag2 && updateMode != UpdateMode.IgnoreOnComplete && t.onComplete != null)
			{
				Tween.OnTweenCallback(t.onComplete, t);
			}
			if (!t.isPlaying && flag3 && (!t.isComplete || !t.autoKill) && t.onPause != null)
			{
				Tween.OnTweenCallback(t.onPause, t);
			}
			return t.autoKill && t.isComplete;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009708 File Offset: 0x00007908
		internal static bool OnTweenCallback(TweenCallback callback, Tween t)
		{
			if (DOTween.useSafeMode)
			{
				try
				{
					callback();
					return true;
				}
				catch (Exception ex)
				{
					if (Debugger.ShouldLogSafeModeCapturedError())
					{
						Debugger.LogSafeModeCapturedError(string.Format("An error inside a tween callback was taken care of ({0}) ► {1}\n\n{2}\n\n", ex.TargetSite, ex.Message, ex.StackTrace), t);
					}
					DOTween.safeModeReport.Add(SafeModeReport.SafeModeReportType.Callback);
					return false;
				}
			}
			callback();
			return true;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009778 File Offset: 0x00007978
		internal static bool OnTweenCallback<T>(TweenCallback<T> callback, Tween t, T param)
		{
			if (DOTween.useSafeMode)
			{
				try
				{
					callback(param);
					return true;
				}
				catch (Exception ex)
				{
					if (Debugger.ShouldLogSafeModeCapturedError())
					{
						Debugger.LogSafeModeCapturedError(string.Format("An error inside a tween callback was taken care of ({0}) ► {1}", ex.TargetSite, ex.Message), t);
					}
					DOTween.safeModeReport.Add(SafeModeReport.SafeModeReportType.Callback);
					return false;
				}
			}
			callback(param);
			return true;
		}

		// Token: 0x04000119 RID: 281
		public float timeScale;

		// Token: 0x0400011A RID: 282
		public bool isBackwards;

		// Token: 0x0400011B RID: 283
		internal bool isInverted;

		// Token: 0x0400011C RID: 284
		public object id;

		// Token: 0x0400011D RID: 285
		public string stringId;

		// Token: 0x0400011E RID: 286
		public int intId = -999;

		// Token: 0x0400011F RID: 287
		public object target;

		// Token: 0x04000120 RID: 288
		internal UpdateType updateType;

		// Token: 0x04000121 RID: 289
		internal bool isIndependentUpdate;

		// Token: 0x04000122 RID: 290
		public TweenCallback onPlay;

		// Token: 0x04000123 RID: 291
		public TweenCallback onPause;

		// Token: 0x04000124 RID: 292
		public TweenCallback onRewind;

		// Token: 0x04000125 RID: 293
		public TweenCallback onUpdate;

		// Token: 0x04000126 RID: 294
		public TweenCallback onStepComplete;

		// Token: 0x04000127 RID: 295
		public TweenCallback onComplete;

		// Token: 0x04000128 RID: 296
		public TweenCallback onKill;

		// Token: 0x04000129 RID: 297
		public TweenCallback<int> onWaypointChange;

		// Token: 0x0400012A RID: 298
		internal bool isFrom;

		// Token: 0x0400012B RID: 299
		internal bool isBlendable;

		// Token: 0x0400012C RID: 300
		internal bool isRecyclable;

		// Token: 0x0400012D RID: 301
		internal bool isSpeedBased;

		// Token: 0x0400012E RID: 302
		internal bool autoKill;

		// Token: 0x0400012F RID: 303
		internal float duration;

		// Token: 0x04000130 RID: 304
		internal int loops;

		// Token: 0x04000131 RID: 305
		internal LoopType loopType;

		// Token: 0x04000132 RID: 306
		internal float delay;

		// Token: 0x04000134 RID: 308
		internal Ease easeType;

		// Token: 0x04000135 RID: 309
		internal EaseFunction customEase;

		// Token: 0x04000136 RID: 310
		public float easeOvershootOrAmplitude;

		// Token: 0x04000137 RID: 311
		public float easePeriod;

		// Token: 0x04000138 RID: 312
		public string debugTargetId;

		// Token: 0x04000139 RID: 313
		internal Type typeofT1;

		// Token: 0x0400013A RID: 314
		internal Type typeofT2;

		// Token: 0x0400013B RID: 315
		internal Type typeofTPlugOptions;

		// Token: 0x0400013D RID: 317
		internal bool isSequenced;

		// Token: 0x0400013E RID: 318
		internal Sequence sequenceParent;

		// Token: 0x0400013F RID: 319
		internal int activeId = -1;

		// Token: 0x04000140 RID: 320
		internal SpecialStartupMode specialStartupMode;

		// Token: 0x04000141 RID: 321
		internal bool creationLocked;

		// Token: 0x04000142 RID: 322
		internal bool startupDone;

		// Token: 0x04000145 RID: 325
		internal float fullDuration;

		// Token: 0x04000146 RID: 326
		internal int completedLoops;

		// Token: 0x04000147 RID: 327
		internal bool isPlaying;

		// Token: 0x04000148 RID: 328
		internal bool isComplete;

		// Token: 0x04000149 RID: 329
		internal float elapsedDelay;

		// Token: 0x0400014A RID: 330
		internal bool delayComplete = true;

		// Token: 0x0400014B RID: 331
		internal int miscInt = -1;
	}
}
