using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x0200006F RID: 111
	public abstract class Tweener : Tween
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000980C File Offset: 0x00007A0C
		internal Tweener()
		{
		}

		// Token: 0x060002A2 RID: 674
		public abstract Tweener ChangeStartValue(object newStartValue, float newDuration = -1f);

		// Token: 0x060002A3 RID: 675
		public abstract Tweener ChangeEndValue(object newEndValue, float newDuration = -1f, bool snapStartValue = false);

		// Token: 0x060002A4 RID: 676
		public abstract Tweener ChangeEndValue(object newEndValue, bool snapStartValue);

		// Token: 0x060002A5 RID: 677
		public abstract Tweener ChangeValues(object newStartValue, object newEndValue, float newDuration = -1f);

		// Token: 0x060002A6 RID: 678
		internal abstract Tweener SetFrom(bool relative);

		// Token: 0x060002A7 RID: 679 RVA: 0x0000981C File Offset: 0x00007A1C
		internal static bool Setup<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t, DOGetter<T1> getter, DOSetter<T1> setter, T2 endValue, float duration, ABSTweenPlugin<T1, T2, TPlugOptions> plugin = null) where TPlugOptions : struct, IPlugOptions
		{
			if (plugin != null)
			{
				t.tweenPlugin = plugin;
			}
			else
			{
				if (t.tweenPlugin == null)
				{
					t.tweenPlugin = PluginsManager.GetDefaultPlugin<T1, T2, TPlugOptions>();
				}
				if (t.tweenPlugin == null)
				{
					Debugger.LogError("No suitable plugin found for this type", null);
					return false;
				}
			}
			t.getter = getter;
			t.setter = setter;
			t.endValue = endValue;
			t.duration = duration;
			t.autoKill = DOTween.defaultAutoKill;
			t.isRecyclable = DOTween.defaultRecyclable;
			t.easeType = DOTween.defaultEaseType;
			t.easeOvershootOrAmplitude = DOTween.defaultEaseOvershootOrAmplitude;
			t.easePeriod = DOTween.defaultEasePeriod;
			t.loopType = DOTween.defaultLoopType;
			t.isPlaying = DOTween.defaultAutoPlay == AutoPlay.All || DOTween.defaultAutoPlay == AutoPlay.AutoPlayTweeners;
			return true;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000098D8 File Offset: 0x00007AD8
		internal static float DoUpdateDelay<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t, float elapsed) where TPlugOptions : struct, IPlugOptions
		{
			float delay = t.delay;
			if (elapsed > delay)
			{
				t.elapsedDelay = delay;
				t.delayComplete = true;
				return elapsed - delay;
			}
			t.elapsedDelay = elapsed;
			return 0f;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009910 File Offset: 0x00007B10
		internal static bool DoStartup<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, IPlugOptions
		{
			t.startupDone = true;
			if (t.specialStartupMode != SpecialStartupMode.None && !Tweener.DOStartupSpecials<T1, T2, TPlugOptions>(t))
			{
				return false;
			}
			if (!t.hasManuallySetStartValue)
			{
				if (DOTween.useSafeMode)
				{
					try
					{
						if (t.isFrom)
						{
							t.SetFrom(t.isRelative && !t.isBlendable);
							t.isRelative = false;
						}
						else
						{
							t.startValue = t.tweenPlugin.ConvertToStartValue(t, t.getter());
						}
						goto IL_00F8;
					}
					catch (Exception ex)
					{
						if (Debugger.ShouldLogSafeModeCapturedError())
						{
							Debugger.LogSafeModeCapturedError(string.Format("Tween startup failed (NULL target/property - {0}): the tween will now be killed ► {1}", ex.TargetSite, ex.Message), t);
						}
						DOTween.safeModeReport.Add(SafeModeReport.SafeModeReportType.StartupFailure);
						return false;
					}
				}
				if (t.isFrom)
				{
					t.SetFrom(t.isRelative && !t.isBlendable);
					t.isRelative = false;
				}
				else
				{
					t.startValue = t.tweenPlugin.ConvertToStartValue(t, t.getter());
				}
			}
			IL_00F8:
			if (t.isRelative)
			{
				t.tweenPlugin.SetRelativeEndValue(t);
			}
			t.tweenPlugin.SetChangeValue(t);
			Tweener.DOStartupDurationBased<T1, T2, TPlugOptions>(t);
			if (t.duration <= 0f)
			{
				t.easeType = Ease.INTERNAL_Zero;
			}
			return true;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009A64 File Offset: 0x00007C64
		internal static TweenerCore<T1, T2, TPlugOptions> DoChangeStartValue<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t, T2 newStartValue, float newDuration) where TPlugOptions : struct, IPlugOptions
		{
			t.hasManuallySetStartValue = true;
			t.startValue = newStartValue;
			if (t.startupDone)
			{
				if (t.specialStartupMode != SpecialStartupMode.None && !Tweener.DOStartupSpecials<T1, T2, TPlugOptions>(t))
				{
					return null;
				}
				t.tweenPlugin.SetChangeValue(t);
			}
			if (newDuration > 0f)
			{
				t.duration = newDuration;
				if (t.startupDone)
				{
					Tweener.DOStartupDurationBased<T1, T2, TPlugOptions>(t);
				}
			}
			Tween.DoGoto(t, 0f, 0, UpdateMode.IgnoreOnUpdate);
			return t;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009AD4 File Offset: 0x00007CD4
		internal static TweenerCore<T1, T2, TPlugOptions> DoChangeEndValue<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t, T2 newEndValue, float newDuration, bool snapStartValue) where TPlugOptions : struct, IPlugOptions
		{
			t.endValue = newEndValue;
			t.isRelative = false;
			if (t.startupDone)
			{
				if (t.specialStartupMode != SpecialStartupMode.None && !Tweener.DOStartupSpecials<T1, T2, TPlugOptions>(t))
				{
					return null;
				}
				if (snapStartValue)
				{
					if (DOTween.useSafeMode)
					{
						try
						{
							t.startValue = t.tweenPlugin.ConvertToStartValue(t, t.getter());
							goto IL_00B4;
						}
						catch (Exception ex)
						{
							if (Debugger.ShouldLogSafeModeCapturedError())
							{
								Debugger.LogSafeModeCapturedError(string.Format("Target or field is missing/null ({0}) ► {1}\n\n{2}\n\n", ex.TargetSite, ex.Message, ex.StackTrace), t);
							}
							TweenManager.Despawn(t, true);
							DOTween.safeModeReport.Add(SafeModeReport.SafeModeReportType.TargetOrFieldMissing);
							return null;
						}
					}
					t.startValue = t.tweenPlugin.ConvertToStartValue(t, t.getter());
				}
				IL_00B4:
				t.tweenPlugin.SetChangeValue(t);
			}
			if (newDuration > 0f)
			{
				t.duration = newDuration;
				if (t.startupDone)
				{
					Tweener.DOStartupDurationBased<T1, T2, TPlugOptions>(t);
				}
			}
			Tween.DoGoto(t, 0f, 0, UpdateMode.IgnoreOnUpdate);
			return t;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00009BE0 File Offset: 0x00007DE0
		internal static TweenerCore<T1, T2, TPlugOptions> DoChangeValues<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t, T2 newStartValue, T2 newEndValue, float newDuration) where TPlugOptions : struct, IPlugOptions
		{
			t.hasManuallySetStartValue = true;
			t.isRelative = (t.isFrom = false);
			t.startValue = newStartValue;
			t.endValue = newEndValue;
			if (t.startupDone)
			{
				if (t.specialStartupMode != SpecialStartupMode.None && !Tweener.DOStartupSpecials<T1, T2, TPlugOptions>(t))
				{
					return null;
				}
				t.tweenPlugin.SetChangeValue(t);
			}
			if (newDuration > 0f)
			{
				t.duration = newDuration;
				if (t.startupDone)
				{
					Tweener.DOStartupDurationBased<T1, T2, TPlugOptions>(t);
				}
			}
			Tween.DoGoto(t, 0f, 0, UpdateMode.IgnoreOnUpdate);
			return t;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009C64 File Offset: 0x00007E64
		private static bool DOStartupSpecials<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, IPlugOptions
		{
			bool flag;
			try
			{
				switch (t.specialStartupMode)
				{
				case SpecialStartupMode.SetLookAt:
					if (!SpecialPluginsUtils.SetLookAt(t as TweenerCore<Quaternion, Vector3, QuaternionOptions>))
					{
						return false;
					}
					break;
				case SpecialStartupMode.SetShake:
					if (!SpecialPluginsUtils.SetShake(t as TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>))
					{
						return false;
					}
					break;
				case SpecialStartupMode.SetPunch:
					if (!SpecialPluginsUtils.SetPunch(t as TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>))
					{
						return false;
					}
					break;
				case SpecialStartupMode.SetCameraShakePosition:
					if (!SpecialPluginsUtils.SetCameraShakePosition(t as TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>))
					{
						return false;
					}
					break;
				}
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00009CF0 File Offset: 0x00007EF0
		private static void DOStartupDurationBased<T1, T2, TPlugOptions>(TweenerCore<T1, T2, TPlugOptions> t) where TPlugOptions : struct, IPlugOptions
		{
			if (t.isSpeedBased)
			{
				t.duration = t.tweenPlugin.GetSpeedBasedDuration(t.plugOptions, t.duration, t.changeValue);
			}
			t.fullDuration = ((t.loops > -1) ? (t.duration * (float)t.loops) : float.PositiveInfinity);
		}

		// Token: 0x0400014C RID: 332
		internal bool hasManuallySetStartValue;

		// Token: 0x0400014D RID: 333
		internal bool isFromAllowed = true;
	}
}
