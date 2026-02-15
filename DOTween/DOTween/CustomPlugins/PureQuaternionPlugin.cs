using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.CustomPlugins
{
	// Token: 0x0200009E RID: 158
	public class PureQuaternionPlugin : ABSTweenPlugin<Quaternion, Quaternion, NoOptions>
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00010552 File Offset: 0x0000E752
		public static PureQuaternionPlugin Plug()
		{
			if (PureQuaternionPlugin._plug == null)
			{
				PureQuaternionPlugin._plug = new PureQuaternionPlugin();
			}
			return PureQuaternionPlugin._plug;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<Quaternion, Quaternion, NoOptions> t)
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001056C File Offset: 0x0000E76C
		public override void SetFrom(TweenerCore<Quaternion, Quaternion, NoOptions> t, bool isRelative)
		{
			Quaternion endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = (isRelative ? (t.endValue * endValue) : endValue);
			t.setter(t.startValue);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000105BC File Offset: 0x0000E7BC
		public override void SetFrom(TweenerCore<Quaternion, Quaternion, NoOptions> t, Quaternion fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Quaternion quaternion = t.getter();
				t.endValue = quaternion * t.endValue;
				fromValue = quaternion * fromValue;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00009F4F File Offset: 0x0000814F
		public override Quaternion ConvertToStartValue(TweenerCore<Quaternion, Quaternion, NoOptions> t, Quaternion value)
		{
			return value;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001060A File Offset: 0x0000E80A
		public override void SetRelativeEndValue(TweenerCore<Quaternion, Quaternion, NoOptions> t)
		{
			t.endValue *= t.startValue;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00010623 File Offset: 0x0000E823
		public override void SetChangeValue(TweenerCore<Quaternion, Quaternion, NoOptions> t)
		{
			t.changeValue = t.endValue;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00010634 File Offset: 0x0000E834
		public override float GetSpeedBasedDuration(NoOptions options, float unitsXSecond, Quaternion changeValue)
		{
			return changeValue.eulerAngles.magnitude / unitsXSecond;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00010654 File Offset: 0x0000E854
		public override void EvaluateAndApply(NoOptions options, Tween t, bool isRelative, DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, float elapsed, Quaternion startValue, Quaternion changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			setter(Quaternion.Slerp(startValue, changeValue, num));
		}

		// Token: 0x040001C3 RID: 451
		private static PureQuaternionPlugin _plug;
	}
}
