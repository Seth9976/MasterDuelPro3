using System;
using System.Collections.Generic;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C7 RID: 711
	internal static class ComputedTransitionUtils
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x0005970C File Offset: 0x0005790C
		internal static void UpdateComputedTransitions(ref ComputedStyle computedStyle)
		{
			bool flag = computedStyle.computedTransitions == null;
			if (flag)
			{
				computedStyle.computedTransitions = ComputedTransitionUtils.GetOrComputeTransitionPropertyData(ref computedStyle);
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00059738 File Offset: 0x00057938
		internal static bool HasTransitionProperty(this ComputedStyle computedStyle, StylePropertyId id)
		{
			for (int i = computedStyle.computedTransitions.Length - 1; i >= 0; i--)
			{
				ComputedTransitionProperty t = computedStyle.computedTransitions[i];
				bool flag = t.id == id || StylePropertyUtil.IsMatchingShorthand(t.id, id);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00059798 File Offset: 0x00057998
		internal static bool GetTransitionProperty(this ComputedStyle computedStyle, StylePropertyId id, out ComputedTransitionProperty result)
		{
			for (int i = computedStyle.computedTransitions.Length - 1; i >= 0; i--)
			{
				ComputedTransitionProperty t = computedStyle.computedTransitions[i];
				bool flag = t.id == id || StylePropertyUtil.IsMatchingShorthand(t.id, id);
				if (flag)
				{
					result = t;
					return true;
				}
			}
			result = default(ComputedTransitionProperty);
			return false;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00059808 File Offset: 0x00057A08
		private static ComputedTransitionProperty[] GetOrComputeTransitionPropertyData(ref ComputedStyle computedStyle)
		{
			int hash = ComputedTransitionUtils.GetTransitionHashCode(ref computedStyle);
			ComputedTransitionProperty[] computedTransitions;
			bool flag = !StyleCache.TryGetValue(hash, out computedTransitions);
			if (flag)
			{
				ComputedTransitionUtils.ComputeTransitionPropertyData(ref computedStyle, ComputedTransitionUtils.s_ComputedTransitionsBuffer);
				computedTransitions = new ComputedTransitionProperty[ComputedTransitionUtils.s_ComputedTransitionsBuffer.Count];
				ComputedTransitionUtils.s_ComputedTransitionsBuffer.CopyTo(computedTransitions);
				ComputedTransitionUtils.s_ComputedTransitionsBuffer.Clear();
				StyleCache.SetValue(hash, computedTransitions);
			}
			return computedTransitions;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00059870 File Offset: 0x00057A70
		private static int GetTransitionHashCode(ref ComputedStyle cs)
		{
			int hashCode = 0;
			foreach (TimeValue x in cs.transitionDelay)
			{
				hashCode = (hashCode * 397) ^ x.GetHashCode();
			}
			foreach (TimeValue x2 in cs.transitionDuration)
			{
				hashCode = (hashCode * 397) ^ x2.GetHashCode();
			}
			foreach (StylePropertyName x3 in cs.transitionProperty)
			{
				hashCode = (hashCode * 397) ^ x3.GetHashCode();
			}
			foreach (EasingFunction x4 in cs.transitionTimingFunction)
			{
				hashCode = (hashCode * 397) ^ x4.GetHashCode();
			}
			return hashCode;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000599E0 File Offset: 0x00057BE0
		internal static bool SameTransitionProperty(ref ComputedStyle x, ref ComputedStyle y)
		{
			bool flag = x.computedTransitions == y.computedTransitions && x.computedTransitions != null;
			return flag || (ComputedTransitionUtils.SameTransitionProperty(x.transitionProperty, y.transitionProperty) && ComputedTransitionUtils.SameTransitionProperty(x.transitionDuration, y.transitionDuration) && ComputedTransitionUtils.SameTransitionProperty(x.transitionDelay, y.transitionDelay));
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00059A50 File Offset: 0x00057C50
		private static bool SameTransitionProperty(List<StylePropertyName> a, List<StylePropertyName> b)
		{
			bool flag = a == b;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = a == null || b == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = a.Count != b.Count;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						int i = a.Count;
						for (int j = 0; j < i; j++)
						{
							bool flag5 = a[j] != b[j];
							if (flag5)
							{
								return false;
							}
						}
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00059AD8 File Offset: 0x00057CD8
		private static bool SameTransitionProperty(List<TimeValue> a, List<TimeValue> b)
		{
			bool flag = a == b;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = a == null || b == null;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = a.Count != b.Count;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						int i = a.Count;
						for (int j = 0; j < i; j++)
						{
							bool flag5 = a[j] != b[j];
							if (flag5)
							{
								return false;
							}
						}
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00059B60 File Offset: 0x00057D60
		private static void ComputeTransitionPropertyData(ref ComputedStyle computedStyle, List<ComputedTransitionProperty> outData)
		{
			List<StylePropertyName> properties = computedStyle.transitionProperty;
			bool flag = properties == null || properties.Count == 0;
			if (!flag)
			{
				List<TimeValue> durations = computedStyle.transitionDuration;
				List<TimeValue> delays = computedStyle.transitionDelay;
				List<EasingFunction> timingFunctions = computedStyle.transitionTimingFunction;
				int nProperties = properties.Count;
				for (int i = 0; i < nProperties; i++)
				{
					StylePropertyId id = properties[i].id;
					bool flag2 = id == StylePropertyId.Unknown || !StylePropertyUtil.IsAnimatable(id);
					if (!flag2)
					{
						int durationMs = ComputedTransitionUtils.ConvertTransitionTime(ComputedTransitionUtils.GetWrappingTransitionData<TimeValue>(durations, i, new TimeValue(0f)));
						int delayMs = ComputedTransitionUtils.ConvertTransitionTime(ComputedTransitionUtils.GetWrappingTransitionData<TimeValue>(delays, i, new TimeValue(0f)));
						float combinedDuration = (float)(Mathf.Max(0, durationMs) + delayMs);
						bool flag3 = combinedDuration <= 0f;
						if (!flag3)
						{
							EasingFunction easingFunction = ComputedTransitionUtils.GetWrappingTransitionData<EasingFunction>(timingFunctions, i, EasingMode.Ease);
							outData.Add(new ComputedTransitionProperty
							{
								id = id,
								durationMs = durationMs,
								delayMs = delayMs,
								easingCurve = ComputedTransitionUtils.ConvertTransitionFunction(easingFunction.mode)
							});
						}
					}
				}
			}
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00059CA0 File Offset: 0x00057EA0
		private static T GetWrappingTransitionData<T>(List<T> list, int i, T defaultValue)
		{
			return (list.Count == 0) ? defaultValue : list[i % list.Count];
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00059CCC File Offset: 0x00057ECC
		private static int ConvertTransitionTime(TimeValue time)
		{
			return Mathf.RoundToInt((time.unit == TimeUnit.Millisecond) ? time.value : (time.value * 1000f));
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00059D04 File Offset: 0x00057F04
		private static Func<float, float> ConvertTransitionFunction(EasingMode mode)
		{
			Func<float, float> func;
			switch (mode)
			{
			default:
				func = (float t) => t * (1.8f + t * (-0.6f + t * -0.2f));
				break;
			case EasingMode.EaseIn:
				func = (float t) => Easing.InQuad(t);
				break;
			case EasingMode.EaseOut:
				func = (float t) => Easing.OutQuad(t);
				break;
			case EasingMode.EaseInOut:
				func = (float t) => Easing.InOutQuad(t);
				break;
			case EasingMode.Linear:
				func = (float t) => Easing.Linear(t);
				break;
			case EasingMode.EaseInSine:
				func = (float t) => Easing.InSine(t);
				break;
			case EasingMode.EaseOutSine:
				func = (float t) => Easing.OutSine(t);
				break;
			case EasingMode.EaseInOutSine:
				func = (float t) => Easing.InOutSine(t);
				break;
			case EasingMode.EaseInCubic:
				func = (float t) => Easing.InCubic(t);
				break;
			case EasingMode.EaseOutCubic:
				func = (float t) => Easing.OutCubic(t);
				break;
			case EasingMode.EaseInOutCubic:
				func = (float t) => Easing.InOutCubic(t);
				break;
			case EasingMode.EaseInCirc:
				func = (float t) => Easing.InCirc(t);
				break;
			case EasingMode.EaseOutCirc:
				func = (float t) => Easing.OutCirc(t);
				break;
			case EasingMode.EaseInOutCirc:
				func = (float t) => Easing.InOutCirc(t);
				break;
			case EasingMode.EaseInElastic:
				func = (float t) => Easing.InElastic(t);
				break;
			case EasingMode.EaseOutElastic:
				func = (float t) => Easing.OutElastic(t);
				break;
			case EasingMode.EaseInOutElastic:
				func = (float t) => Easing.InOutElastic(t);
				break;
			case EasingMode.EaseInBack:
				func = (float t) => Easing.InBack(t);
				break;
			case EasingMode.EaseOutBack:
				func = (float t) => Easing.OutBack(t);
				break;
			case EasingMode.EaseInOutBack:
				func = (float t) => Easing.InOutBack(t);
				break;
			case EasingMode.EaseInBounce:
				func = (float t) => Easing.InBounce(t);
				break;
			case EasingMode.EaseOutBounce:
				func = (float t) => Easing.OutBounce(t);
				break;
			case EasingMode.EaseInOutBounce:
				func = (float t) => Easing.InOutBounce(t);
				break;
			}
			return func;
		}

		// Token: 0x04000B11 RID: 2833
		private static List<ComputedTransitionProperty> s_ComputedTransitionsBuffer = new List<ComputedTransitionProperty>();
	}
}
