using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x0200001B RID: 27
	public static class TweenExtensions
	{
		// Token: 0x06000094 RID: 148 RVA: 0x0000344B File Offset: 0x0000164B
		public static void Complete(this Tween t)
		{
			t.Complete(false);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003454 File Offset: 0x00001654
		public static void Complete(this Tween t, bool withCallbacks)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Complete(t, true, withCallbacks ? UpdateMode.Update : UpdateMode.Goto);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000034B0 File Offset: 0x000016B0
		public static void Flip(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Flip(t);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003504 File Offset: 0x00001704
		public static void ForceInit(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.ForceInit(t, false);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003558 File Offset: 0x00001758
		public static void Goto(this Tween t, float to, bool andPlay = false)
		{
			TweenExtensions.DoGoto(t, to, andPlay, false);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003563 File Offset: 0x00001763
		public static void GotoWithCallbacks(this Tween t, float to, bool andPlay = false)
		{
			TweenExtensions.DoGoto(t, to, andPlay, true);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003570 File Offset: 0x00001770
		private static void DoGoto(Tween t, float to, bool andPlay, bool withCallbacks)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			if (to < 0f)
			{
				to = 0f;
			}
			if (!t.startupDone)
			{
				TweenManager.ForceInit(t, false);
			}
			TweenManager.Goto(t, to, andPlay, withCallbacks ? UpdateMode.Update : UpdateMode.Goto);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000035EC File Offset: 0x000017EC
		public static void Kill(this Tween t, bool complete = false)
		{
			if (!DOTween.initialized)
			{
				return;
			}
			if (t == null || !t.active)
			{
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			if (complete)
			{
				TweenManager.Complete(t, true, UpdateMode.Goto);
				if (t.autoKill && t.loops >= 0)
				{
					return;
				}
			}
			if (TweenManager.isUpdateLoop)
			{
				t.active = false;
				return;
			}
			TweenManager.Despawn(t, true);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003658 File Offset: 0x00001858
		public static void ManualUpdate(this Tween t, float deltaTime, float unscaledDeltaTime)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Update(t, deltaTime, unscaledDeltaTime, true);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000036B0 File Offset: 0x000018B0
		public static T Pause<T>(this T t) where T : Tween
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return t;
			}
			TweenManager.Pause(t);
			return t;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000372C File Offset: 0x0000192C
		public static T Play<T>(this T t) where T : Tween
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return t;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return t;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return t;
			}
			TweenManager.Play(t);
			return t;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000037A8 File Offset: 0x000019A8
		public static void PlayBackwards(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.PlayBackwards(t);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000037FC File Offset: 0x000019FC
		public static void PlayForward(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.PlayForward(t);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003850 File Offset: 0x00001A50
		public static void Restart(this Tween t, bool includeDelay = true, float changeDelayTo = -1f)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Restart(t, includeDelay, changeDelayTo);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000038A8 File Offset: 0x00001AA8
		public static void Rewind(this Tween t, bool includeDelay = true)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.Rewind(t, includeDelay);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003900 File Offset: 0x00001B00
		public static void SmoothRewind(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.SmoothRewind(t);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003954 File Offset: 0x00001B54
		public static void TogglePause(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenManager.TogglePause(t);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000039A8 File Offset: 0x00001BA8
		public static void GotoWaypoint(this Tween t, int waypointIndex, bool andPlay = false)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = t as TweenerCore<Vector3, Path, PathOptions>;
			if (tweenerCore == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return;
			}
			if (!t.startupDone)
			{
				TweenManager.ForceInit(t, false);
			}
			if (waypointIndex < 0)
			{
				waypointIndex = 0;
			}
			else if (waypointIndex > tweenerCore.changeValue.wps.Length - 1)
			{
				waypointIndex = tweenerCore.changeValue.wps.Length - 1;
			}
			float num = 0f;
			for (int i = 0; i < waypointIndex + 1; i++)
			{
				num += tweenerCore.changeValue.wpLengths[i];
			}
			float num2 = num / tweenerCore.changeValue.length;
			if (t.hasLoops && t.loopType == LoopType.Yoyo && ((t.position < t.duration) ? (t.completedLoops % 2 != 0) : (t.completedLoops % 2 == 0)))
			{
				num2 = 1f - num2;
			}
			float num3 = (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops) * t.duration + num2 * t.duration;
			TweenManager.Goto(t, num3, andPlay, UpdateMode.Goto);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public static YieldInstruction WaitForCompletion(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForCompletion(t));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003B27 File Offset: 0x00001D27
		public static YieldInstruction WaitForRewind(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForRewind(t));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003B56 File Offset: 0x00001D56
		public static YieldInstruction WaitForKill(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForKill(t));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003B85 File Offset: 0x00001D85
		public static YieldInstruction WaitForElapsedLoops(this Tween t, int elapsedLoops)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForElapsedLoops(t, elapsedLoops));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003BB5 File Offset: 0x00001DB5
		public static YieldInstruction WaitForPosition(this Tween t, float position)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForPosition(t, position));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003BE5 File Offset: 0x00001DE5
		public static Coroutine WaitForStart(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			return DOTween.instance.StartCoroutine(DOTween.instance.WaitForStart(t));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003C14 File Offset: 0x00001E14
		public static int CompletedLoops(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0;
			}
			return t.completedLoops;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C34 File Offset: 0x00001E34
		public static float Delay(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			return t.delay;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003C58 File Offset: 0x00001E58
		public static float ElapsedDelay(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			return t.elapsedDelay;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C7C File Offset: 0x00001E7C
		public static float Duration(this Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (!includeLoops)
			{
				return t.duration;
			}
			if (t.loops != -1)
			{
				return t.duration * (float)t.loops;
			}
			return float.PositiveInfinity;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003CCC File Offset: 0x00001ECC
		public static float Elapsed(this Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (includeLoops)
			{
				return (float)((t.position >= t.duration) ? (t.completedLoops - 1) : t.completedLoops) * t.duration + t.position;
			}
			return t.position;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003D2C File Offset: 0x00001F2C
		public static float ElapsedPercentage(this Tween t, bool includeLoops = true)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			if (!includeLoops)
			{
				return t.position / t.duration;
			}
			if (t.fullDuration <= 0f)
			{
				return 0f;
			}
			return ((float)((t.position >= t.duration) ? (t.completedLoops - 1) : t.completedLoops) * t.duration + t.position) / t.fullDuration;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003DB0 File Offset: 0x00001FB0
		public static float ElapsedDirectionalPercentage(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0f;
			}
			float num = t.position / t.duration;
			if (t.completedLoops <= 0 || !t.hasLoops || t.loopType != LoopType.Yoyo || ((t.isComplete || t.completedLoops % 2 == 0) && (!t.isComplete || t.completedLoops % 2 != 0)))
			{
				return num;
			}
			return 1f - num;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003E3A File Offset: 0x0000203A
		public static bool IsActive(this Tween t)
		{
			return t != null && t.active;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E47 File Offset: 0x00002047
		public static bool IsBackwards(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isBackwards;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E67 File Offset: 0x00002067
		public static bool IsComplete(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isComplete;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003E87 File Offset: 0x00002087
		public static bool IsInitialized(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.startupDone;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003EA7 File Offset: 0x000020A7
		public static bool IsPlaying(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return false;
			}
			return t.isPlaying;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003EC7 File Offset: 0x000020C7
		public static int Loops(this Tween t)
		{
			if (!t.active)
			{
				if (Debugger.logPriority > 0)
				{
					Debugger.LogInvalidTween(t);
				}
				return 0;
			}
			return t.loops;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003EE8 File Offset: 0x000020E8
		public static Vector3 PathGetPoint(this Tween t, float pathPercentage)
		{
			if (pathPercentage > 1f)
			{
				pathPercentage = 1f;
			}
			else if (pathPercentage < 0f)
			{
				pathPercentage = 0f;
			}
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return Vector3.zero;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return Vector3.zero;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return Vector3.zero;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = t as TweenerCore<Vector3, Path, PathOptions>;
			if (tweenerCore == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return Vector3.zero;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogWarning("The path is not finalized yet", t);
				}
				return Vector3.zero;
			}
			return tweenerCore.endValue.GetPoint(pathPercentage, true);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003FB8 File Offset: 0x000021B8
		public static Vector3[] PathGetDrawPoints(this Tween t, int subdivisionsXSegment = 10)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return null;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return null;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return null;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = t as TweenerCore<Vector3, Path, PathOptions>;
			if (tweenerCore == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return null;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogWarning("The path is not finalized yet", t);
				}
				return null;
			}
			return Path.GetDrawPoints(tweenerCore.endValue, subdivisionsXSegment);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004050 File Offset: 0x00002250
		public static float PathLength(this Tween t)
		{
			if (t == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(t);
				}
				return -1f;
			}
			if (!t.active)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogInvalidTween(t);
				}
				return -1f;
			}
			if (t.isSequenced)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNestedTween(t);
				}
				return -1f;
			}
			TweenerCore<Vector3, Path, PathOptions> tweenerCore = t as TweenerCore<Vector3, Path, PathOptions>;
			if (tweenerCore == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNonPathTween(t);
				}
				return -1f;
			}
			if (!tweenerCore.endValue.isFinalized)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogWarning("The path is not finalized yet", t);
				}
				return -1f;
			}
			return tweenerCore.endValue.length;
		}
	}
}
