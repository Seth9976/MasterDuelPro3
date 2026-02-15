using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;

namespace DG.Tweening
{
	// Token: 0x0200001D RID: 29
	public sealed class Sequence : Tween
	{
		// Token: 0x060000BC RID: 188 RVA: 0x000040FB File Offset: 0x000022FB
		internal Sequence()
		{
			this.tweenType = TweenType.Sequence;
			this.Reset();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004128 File Offset: 0x00002328
		internal static Sequence DoPrepend(Sequence inSequence, Tween t)
		{
			if (t.loops == -1)
			{
				t.loops = int.MaxValue;
				Debugger.LogWarning("Infinite loops aren't allowed inside a Sequence (only on the Sequence itself) and will be changed to int.MaxValue", t);
			}
			float num = t.delay + t.duration * (float)t.loops;
			inSequence.duration += num;
			int count = inSequence._sequencedObjs.Count;
			for (int i = 0; i < count; i++)
			{
				ABSSequentiable abssequentiable = inSequence._sequencedObjs[i];
				abssequentiable.sequencedPosition += num;
				abssequentiable.sequencedEndPosition += num;
			}
			return Sequence.DoInsert(inSequence, t, 0f);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000041C4 File Offset: 0x000023C4
		internal static Sequence DoInsert(Sequence inSequence, Tween t, float atPosition)
		{
			TweenManager.AddActiveTweenToSequence(t);
			atPosition += t.delay;
			inSequence.lastTweenInsertTime = atPosition;
			t.isSequenced = (t.creationLocked = true);
			t.sequenceParent = inSequence;
			if (t.loops == -1)
			{
				t.loops = int.MaxValue;
				Debugger.LogWarning("Infinite loops aren't allowed inside a Sequence (only on the Sequence itself) and will be changed to int.MaxValue", t);
			}
			float num = t.duration * (float)t.loops;
			t.autoKill = false;
			t.delay = (t.elapsedDelay = 0f);
			t.delayComplete = true;
			if (t.isSpeedBased)
			{
				t.isSpeedBased = false;
				Debugger.LogWarning("SpeedBased tweens are not allowed inside a Sequence: interpreting speed as duration", t);
			}
			t.sequencedPosition = atPosition;
			t.sequencedEndPosition = atPosition + num;
			if (t.sequencedEndPosition > inSequence.duration)
			{
				inSequence.duration = t.sequencedEndPosition;
			}
			inSequence._sequencedObjs.Add(t);
			inSequence.sequencedTweens.Add(t);
			return inSequence;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000042AC File Offset: 0x000024AC
		internal static Sequence DoAppendInterval(Sequence inSequence, float interval)
		{
			inSequence.lastTweenInsertTime = inSequence.duration;
			inSequence.duration += interval;
			return inSequence;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000042CC File Offset: 0x000024CC
		internal static Sequence DoPrependInterval(Sequence inSequence, float interval)
		{
			inSequence.lastTweenInsertTime = 0f;
			inSequence.duration += interval;
			int count = inSequence._sequencedObjs.Count;
			for (int i = 0; i < count; i++)
			{
				ABSSequentiable abssequentiable = inSequence._sequencedObjs[i];
				abssequentiable.sequencedPosition += interval;
				abssequentiable.sequencedEndPosition += interval;
			}
			return inSequence;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004334 File Offset: 0x00002534
		internal static Sequence DoInsertCallback(Sequence inSequence, TweenCallback callback, float atPosition)
		{
			inSequence.lastTweenInsertTime = atPosition;
			SequenceCallback sequenceCallback = new SequenceCallback(atPosition, callback);
			ABSSequentiable abssequentiable = sequenceCallback;
			sequenceCallback.sequencedEndPosition = atPosition;
			abssequentiable.sequencedPosition = atPosition;
			inSequence._sequencedObjs.Add(sequenceCallback);
			if (inSequence.duration < atPosition)
			{
				inSequence.duration = atPosition;
			}
			return inSequence;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004380 File Offset: 0x00002580
		internal override float UpdateDelay(float elapsed)
		{
			float delay = this.delay;
			if (elapsed > delay)
			{
				this.elapsedDelay = delay;
				this.delayComplete = true;
				return elapsed - delay;
			}
			this.elapsedDelay = elapsed;
			return 0f;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000043B6 File Offset: 0x000025B6
		internal override void Reset()
		{
			base.Reset();
			this.sequencedTweens.Clear();
			this._sequencedObjs.Clear();
			this.lastTweenInsertTime = 0f;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000043E0 File Offset: 0x000025E0
		internal override bool Validate()
		{
			int count = this.sequencedTweens.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.sequencedTweens[i].Validate())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000441B File Offset: 0x0000261B
		internal override bool Startup()
		{
			return Sequence.DoStartup(this);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004423 File Offset: 0x00002623
		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice)
		{
			return Sequence.DoApplyTween(this, prevPosition, prevCompletedLoops, newCompletedSteps, useInversePosition, updateMode);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004434 File Offset: 0x00002634
		internal static void Setup(Sequence s)
		{
			s.autoKill = DOTween.defaultAutoKill;
			s.isRecyclable = DOTween.defaultRecyclable;
			s.isPlaying = DOTween.defaultAutoPlay == AutoPlay.All || DOTween.defaultAutoPlay == AutoPlay.AutoPlaySequences;
			s.loopType = DOTween.defaultLoopType;
			s.easeType = Ease.Linear;
			s.easeOvershootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude;
			s.easePeriod = DOTween.defaultEasePeriod;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004498 File Offset: 0x00002698
		internal static bool DoStartup(Sequence s)
		{
			int count = s._sequencedObjs.Count;
			if (s.sequencedTweens.Count == 0 && count == 0 && !Sequence.IsAnyCallbackSet(s))
			{
				return false;
			}
			s.startupDone = true;
			s.fullDuration = ((s.loops > -1) ? (s.duration * (float)s.loops) : float.PositiveInfinity);
			Sequence.StableSortSequencedObjs(s._sequencedObjs);
			if (s.isRelative)
			{
				int count2 = s.sequencedTweens.Count;
				for (int i = 0; i < count2; i++)
				{
					Tween tween = s.sequencedTweens[i];
					if (!s.isBlendable)
					{
						s.sequencedTweens[i].isRelative = true;
					}
				}
			}
			if (s.isInverted)
			{
				for (int j = 0; j < count; j++)
				{
					ABSSequentiable abssequentiable = s._sequencedObjs[j];
					if (abssequentiable.tweenType == TweenType.Tweener)
					{
						Tween tween2 = (Tween)abssequentiable;
						TweenManager.Goto(tween2, tween2.duration * (float)tween2.loops, false, UpdateMode.IgnoreOnComplete);
						tween2.isInverted = true;
					}
				}
			}
			return true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000045A0 File Offset: 0x000027A0
		internal static bool DoApplyTween(Sequence s, float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode)
		{
			float num = prevPosition;
			float num2 = s.position;
			if (s.isInverted)
			{
				useInversePosition = !useInversePosition;
			}
			if (s.easeType != Ease.Linear)
			{
				num = s.duration * EaseManager.Evaluate(s.easeType, s.customEase, num, s.duration, s.easeOvershootOrAmplitude, s.easePeriod);
				num2 = s.duration * EaseManager.Evaluate(s.easeType, s.customEase, num2, s.duration, s.easeOvershootOrAmplitude, s.easePeriod);
			}
			float num3 = 0f;
			bool flag = (s.loops == -1 || s.loops > 1) && s.loopType == LoopType.Yoyo && ((num < s.duration) ? (prevCompletedLoops % 2 != 0) : (prevCompletedLoops % 2 == 0));
			if (s.isBackwards)
			{
				flag = !flag;
			}
			if (s.isInverted)
			{
				flag = !flag;
			}
			float num5;
			if (newCompletedSteps > 0)
			{
				int completedLoops = s.completedLoops;
				float position = s.position;
				int num4 = newCompletedSteps;
				int i = 0;
				num5 = num;
				if (updateMode == UpdateMode.Update)
				{
					while (i < num4)
					{
						if (i > 0)
						{
							num5 = num3;
						}
						else if (flag && !s.isBackwards)
						{
							num5 = s.duration - num5;
						}
						num3 = (flag ? 0f : s.duration);
						if (Sequence.ApplyInternalCycle(s, num5, num3, updateMode, useInversePosition, flag, true))
						{
							return true;
						}
						i++;
						if (s.hasLoops && s.loopType == LoopType.Yoyo)
						{
							flag = !flag;
						}
					}
					if (completedLoops != s.completedLoops || Math.Abs(position - s.position) > 1E-45f)
					{
						return !s.active;
					}
				}
				else
				{
					if (s.hasLoops && s.loopType == LoopType.Yoyo && newCompletedSteps % 2 != 0)
					{
						flag = !flag;
						num = s.duration - num;
					}
					newCompletedSteps = 0;
				}
			}
			if (newCompletedSteps == 1 && s.isComplete)
			{
				return false;
			}
			if (newCompletedSteps > 0 && !s.isComplete)
			{
				num5 = (useInversePosition ? s.duration : 0f);
				if (s.loopType == LoopType.Restart && num3 > 0f)
				{
					Sequence.ApplyInternalCycle(s, s.duration, 0f, UpdateMode.Goto, false, false, false);
				}
			}
			else
			{
				num5 = (useInversePosition ? (s.duration - num) : num);
			}
			return Sequence.ApplyInternalCycle(s, num5, useInversePosition ? (s.duration - num2) : num2, updateMode, useInversePosition, flag, false);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000047E4 File Offset: 0x000029E4
		private static bool ApplyInternalCycle(Sequence s, float fromPos, float toPos, UpdateMode updateMode, bool useInverse, bool prevPosIsInverse, bool multiCycleStep = false)
		{
			bool isPlaying = s.isPlaying;
			if (toPos < fromPos)
			{
				int num = s._sequencedObjs.Count - 1;
				for (int i = num; i > -1; i--)
				{
					if (!s.active)
					{
						return true;
					}
					if (!s.isPlaying && isPlaying)
					{
						return false;
					}
					ABSSequentiable abssequentiable = s._sequencedObjs[i];
					if (abssequentiable.sequencedEndPosition >= toPos && abssequentiable.sequencedPosition <= fromPos)
					{
						if (abssequentiable.tweenType == TweenType.Callback)
						{
							if (updateMode == UpdateMode.Update && prevPosIsInverse)
							{
								Tween.OnTweenCallback(abssequentiable.onStart, s);
							}
						}
						else
						{
							float num2 = toPos - abssequentiable.sequencedPosition;
							if (num2 < 0f)
							{
								num2 = 0f;
							}
							Tween tween = (Tween)abssequentiable;
							if (tween.startupDone)
							{
								tween.isBackwards = true;
								if (s.isInverted)
								{
									num2 = tween.fullDuration - num2;
								}
								if (TweenManager.Goto(tween, num2, false, updateMode))
								{
									if (DOTween.nestedTweenFailureBehaviour == NestedTweenFailureBehaviour.KillWholeSequence)
									{
										return true;
									}
									if (s.sequencedTweens.Count == 1 && s._sequencedObjs.Count == 1 && !Sequence.IsAnyCallbackSet(s))
									{
										return true;
									}
									TweenManager.Despawn(tween, false);
									s._sequencedObjs.RemoveAt(i);
									s.sequencedTweens.Remove(tween);
									i--;
									num--;
								}
								else if (multiCycleStep && tween.tweenType == TweenType.Sequence)
								{
									if (s.position <= 0f && s.completedLoops == 0)
									{
										tween.position = 0f;
									}
									else
									{
										bool flag = s.completedLoops == 0 || (s.isBackwards && (s.completedLoops < s.loops || s.loops == -1));
										if (tween.isBackwards)
										{
											flag = !flag;
										}
										if (useInverse)
										{
											flag = !flag;
										}
										if (s.isBackwards && !useInverse && !prevPosIsInverse)
										{
											flag = !flag;
										}
										tween.position = (flag ? 0f : tween.duration);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				int num3 = s._sequencedObjs.Count;
				for (int j = 0; j < num3; j++)
				{
					if (!s.active)
					{
						return true;
					}
					if (!s.isPlaying && isPlaying)
					{
						return false;
					}
					ABSSequentiable abssequentiable2 = s._sequencedObjs[j];
					if (abssequentiable2.sequencedPosition <= toPos && (abssequentiable2.sequencedPosition <= 0f || abssequentiable2.sequencedEndPosition > fromPos) && (abssequentiable2.sequencedPosition > 0f || abssequentiable2.sequencedEndPosition >= fromPos))
					{
						if (abssequentiable2.tweenType == TweenType.Callback)
						{
							if (updateMode == UpdateMode.Update && ((!s.isBackwards && !useInverse && !prevPosIsInverse) || (s.isBackwards && useInverse && !prevPosIsInverse)))
							{
								Tween.OnTweenCallback(abssequentiable2.onStart, s);
							}
						}
						else
						{
							float num4 = toPos - abssequentiable2.sequencedPosition;
							if (num4 < 0f)
							{
								num4 = 0f;
							}
							Tween tween2 = (Tween)abssequentiable2;
							if (toPos >= abssequentiable2.sequencedEndPosition)
							{
								if (!tween2.startupDone)
								{
									TweenManager.ForceInit(tween2, true);
								}
								if (num4 < tween2.fullDuration)
								{
									num4 = tween2.fullDuration;
								}
							}
							tween2.isBackwards = false;
							if (s.isInverted)
							{
								num4 = tween2.fullDuration - num4;
							}
							if (TweenManager.Goto(tween2, num4, false, updateMode))
							{
								if (DOTween.nestedTweenFailureBehaviour == NestedTweenFailureBehaviour.KillWholeSequence)
								{
									return true;
								}
								if (s.sequencedTweens.Count == 1 && s._sequencedObjs.Count == 1 && !Sequence.IsAnyCallbackSet(s))
								{
									return true;
								}
								TweenManager.Despawn(tween2, false);
								s._sequencedObjs.RemoveAt(j);
								s.sequencedTweens.Remove(tween2);
								j--;
								num3--;
							}
							else if (multiCycleStep && tween2.tweenType == TweenType.Sequence)
							{
								if (s.position <= 0f && s.completedLoops == 0)
								{
									tween2.position = 0f;
								}
								else
								{
									bool flag2 = s.completedLoops == 0 || (!s.isBackwards && (s.completedLoops < s.loops || s.loops == -1));
									if (tween2.isBackwards)
									{
										flag2 = !flag2;
									}
									if (useInverse)
									{
										flag2 = !flag2;
									}
									if (s.isBackwards && !useInverse && !prevPosIsInverse)
									{
										flag2 = !flag2;
									}
									tween2.position = (flag2 ? 0f : tween2.duration);
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004C68 File Offset: 0x00002E68
		private static void StableSortSequencedObjs(List<ABSSequentiable> list)
		{
			int count = list.Count;
			for (int i = 1; i < count; i++)
			{
				int num = i;
				ABSSequentiable abssequentiable = list[i];
				while (num > 0 && list[num - 1].sequencedPosition > abssequentiable.sequencedPosition)
				{
					list[num] = list[num - 1];
					num--;
				}
				list[num] = abssequentiable;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004CCC File Offset: 0x00002ECC
		private static bool IsAnyCallbackSet(Sequence s)
		{
			return s.onComplete != null || s.onKill != null || s.onPause != null || s.onPlay != null || s.onRewind != null || s.onStart != null || s.onStepComplete != null || s.onUpdate != null;
		}

		// Token: 0x04000089 RID: 137
		internal readonly List<Tween> sequencedTweens = new List<Tween>();

		// Token: 0x0400008A RID: 138
		private readonly List<ABSSequentiable> _sequencedObjs = new List<ABSSequentiable>();

		// Token: 0x0400008B RID: 139
		internal float lastTweenInsertTime;
	}
}
