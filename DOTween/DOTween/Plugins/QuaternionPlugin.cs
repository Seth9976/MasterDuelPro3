using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins
{
	// Token: 0x0200007C RID: 124
	public class QuaternionPlugin : ABSTweenPlugin<Quaternion, Vector3, QuaternionOptions>
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00009DF5 File Offset: 0x00007FF5
		public override void Reset(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public override void SetFrom(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, bool isRelative)
		{
			Vector3 endValue = t.endValue;
			t.endValue = t.getter().eulerAngles;
			if (t.plugOptions.rotateMode == RotateMode.Fast && !t.isRelative)
			{
				t.startValue = this.GetEulerValForCalculations(t, endValue, t.endValue);
			}
			else if (t.plugOptions.rotateMode == RotateMode.FastBeyond360)
			{
				t.startValue = this.GetEulerValForCalculations(t, t.endValue + endValue, t.endValue);
			}
			else
			{
				Quaternion quaternion = t.getter();
				if (t.plugOptions.rotateMode == RotateMode.WorldAxisAdd)
				{
					t.startValue = (quaternion * Quaternion.Inverse(quaternion) * Quaternion.Euler(endValue) * quaternion).eulerAngles;
				}
				else
				{
					t.startValue = (quaternion * Quaternion.Euler(endValue)).eulerAngles;
				}
				t.endValue = -endValue;
			}
			t.setter(Quaternion.Euler(t.startValue));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000BD7C File Offset: 0x00009F7C
		public override void SetFrom(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, Vector3 fromValue, bool setImmediately, bool isRelative)
		{
			if (isRelative)
			{
				Vector3 eulerAngles = t.getter().eulerAngles;
				t.endValue += eulerAngles;
				fromValue += eulerAngles;
			}
			t.startValue = this.GetEulerValForCalculations(t, fromValue, t.endValue);
			if (setImmediately)
			{
				t.setter(Quaternion.Euler(fromValue));
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public override Vector3 ConvertToStartValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, Quaternion value)
		{
			return value.eulerAngles;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000BDED File Offset: 0x00009FED
		public override void SetRelativeEndValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			t.endValue += t.startValue;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000BE08 File Offset: 0x0000A008
		public override void SetChangeValue(TweenerCore<Quaternion, Vector3, QuaternionOptions> t)
		{
			Vector3 vector = (t.isFrom ? t.endValue : this.GetEulerValForCalculations(t, t.endValue, t.startValue));
			Vector3 startValue = t.startValue;
			if (t.plugOptions.rotateMode == RotateMode.Fast && !t.isRelative)
			{
				if (vector.x > 360f || vector.x < 360f)
				{
					vector.x %= 360f;
				}
				if (vector.y > 360f || vector.y < 360f)
				{
					vector.y %= 360f;
				}
				if (vector.z > 360f || vector.z < 360f)
				{
					vector.z %= 360f;
				}
				Vector3 vector2 = vector - startValue;
				float num = ((vector2.x > 0f) ? vector2.x : (-vector2.x));
				if (num > 180f)
				{
					vector2.x = ((vector2.x > 0f) ? (-(360f - num)) : (360f - num));
				}
				num = ((vector2.y > 0f) ? vector2.y : (-vector2.y));
				if (num > 180f)
				{
					vector2.y = ((vector2.y > 0f) ? (-(360f - num)) : (360f - num));
				}
				num = ((vector2.z > 0f) ? vector2.z : (-vector2.z));
				if (num > 180f)
				{
					vector2.z = ((vector2.z > 0f) ? (-(360f - num)) : (360f - num));
				}
				t.changeValue = vector2;
				return;
			}
			if (t.plugOptions.rotateMode == RotateMode.FastBeyond360 || t.isRelative)
			{
				t.changeValue = vector - startValue;
				return;
			}
			t.changeValue = vector;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000BFFB File Offset: 0x0000A1FB
		public override float GetSpeedBasedDuration(QuaternionOptions options, float unitsXSecond, Vector3 changeValue)
		{
			return changeValue.magnitude / unitsXSecond;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C008 File Offset: 0x0000A208
		public override void EvaluateAndApply(QuaternionOptions options, Tween t, bool isRelative, DOGetter<Quaternion> getter, DOSetter<Quaternion> setter, float elapsed, Vector3 startValue, Vector3 changeValue, float duration, bool usingInversePosition, int newCompletedSteps, UpdateNotice updateNotice)
		{
			if (options.dynamicLookAt)
			{
				TweenerCore<Quaternion, Vector3, QuaternionOptions> tweenerCore = (TweenerCore<Quaternion, Vector3, QuaternionOptions>)t;
				tweenerCore.endValue = options.dynamicLookAtWorldPosition;
				SpecialPluginsUtils.SetLookAt(tweenerCore);
				this.SetChangeValue(tweenerCore);
				changeValue = tweenerCore.changeValue;
			}
			Vector3 vector = startValue;
			if (t.loopType == LoopType.Incremental)
			{
				vector += changeValue * (float)(t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
			}
			if (t.isSequenced && t.sequenceParent.loopType == LoopType.Incremental)
			{
				vector += changeValue * (float)((t.loopType == LoopType.Incremental) ? t.loops : 1) * (float)(t.sequenceParent.isComplete ? (t.sequenceParent.completedLoops - 1) : t.sequenceParent.completedLoops);
			}
			float num = EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod);
			RotateMode rotateMode = options.rotateMode;
			if (rotateMode - RotateMode.WorldAxisAdd > 1)
			{
				vector.x += changeValue.x * num;
				vector.y += changeValue.y * num;
				vector.z += changeValue.z * num;
				setter(Quaternion.Euler(vector));
				return;
			}
			Quaternion quaternion = Quaternion.Euler(startValue);
			vector.x = changeValue.x * num;
			vector.y = changeValue.y * num;
			vector.z = changeValue.z * num;
			if (options.rotateMode == RotateMode.WorldAxisAdd)
			{
				setter(quaternion * Quaternion.Inverse(quaternion) * Quaternion.Euler(vector) * quaternion);
				return;
			}
			setter(quaternion * Quaternion.Euler(vector));
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		private Vector3 GetEulerValForCalculations(TweenerCore<Quaternion, Vector3, QuaternionOptions> t, Vector3 val, Vector3 counterVal)
		{
			if (t.isRelative)
			{
				return val;
			}
			Vector3 vector = this.FlipEulerAngles(val);
			bool flag = Mathf.Approximately(counterVal.x, val.x);
			bool flag2 = Mathf.Approximately(counterVal.x, vector.x);
			bool flag3 = Mathf.Approximately(counterVal.y, val.y);
			bool flag4 = Mathf.Approximately(counterVal.y, vector.y);
			bool flag5 = Mathf.Approximately(counterVal.z, val.z);
			bool flag6 = Mathf.Approximately(counterVal.z, vector.z);
			bool flag7 = (flag && (flag3 || flag5)) || (flag3 && flag5);
			bool flag8 = (!flag7 && flag2 && (flag4 || flag6)) || (flag4 && flag6);
			if (!flag7 && !flag8)
			{
				return val;
			}
			int num;
			if (flag7)
			{
				num = (flag ? (flag3 ? 2 : 1) : 0);
			}
			else
			{
				num = (flag2 ? (flag4 ? 2 : 1) : 0);
			}
			bool flag9 = false;
			switch (num)
			{
			case 0:
				flag9 = !Mathf.Approximately(counterVal.y, val.y) || !Mathf.Approximately(counterVal.z, val.z);
				break;
			case 1:
				flag9 = !Mathf.Approximately(counterVal.x, val.x) || !Mathf.Approximately(counterVal.z, val.z);
				break;
			case 2:
				flag9 = !Mathf.Approximately(counterVal.x, val.x) || !Mathf.Approximately(counterVal.y, val.y);
				break;
			}
			if (!flag9)
			{
				return val;
			}
			return vector;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000C36A File Offset: 0x0000A56A
		private Vector3 FlipEulerAngles(Vector3 euler)
		{
			return new Vector3(180f - euler.x, euler.y + 180f, euler.z + 180f);
		}
	}
}
