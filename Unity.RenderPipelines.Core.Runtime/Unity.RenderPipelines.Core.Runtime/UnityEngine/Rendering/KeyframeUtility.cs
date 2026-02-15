using System;
using System.Diagnostics.CodeAnalysis;
using Unity.Collections;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E5 RID: 485
	public class KeyframeUtility
	{
		// Token: 0x06000DC1 RID: 3521 RVA: 0x00032D82 File Offset: 0x00030F82
		public static void ResetAnimationCurve(AnimationCurve curve)
		{
			curve.ClearKeys();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00032D8C File Offset: 0x00030F8C
		private static Keyframe LerpSingleKeyframe(Keyframe lhs, Keyframe rhs, float t)
		{
			return new Keyframe
			{
				time = Mathf.Lerp(lhs.time, rhs.time, t),
				value = Mathf.Lerp(lhs.value, rhs.value, t),
				inTangent = Mathf.Lerp(lhs.inTangent, rhs.inTangent, t),
				outTangent = Mathf.Lerp(lhs.outTangent, rhs.outTangent, t),
				inWeight = Mathf.Lerp(lhs.inWeight, rhs.inWeight, t),
				outWeight = Mathf.Lerp(lhs.outWeight, rhs.outWeight, t),
				weightedMode = lhs.weightedMode
			};
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00032E54 File Offset: 0x00031054
		private static Keyframe GetKeyframeAndClampEdge([DisallowNull] NativeArray<Keyframe> keys, int index)
		{
			int lastKeyIndex = keys.Length - 1;
			if (index < 0 || index > lastKeyIndex)
			{
				Debug.LogWarning("Invalid index in GetKeyframeAndClampEdge. This is likely a bug.");
				return default(Keyframe);
			}
			Keyframe currKey = keys[index];
			if (index == 0)
			{
				currKey.inTangent = 0f;
			}
			if (index == lastKeyIndex)
			{
				currKey.outTangent = 0f;
			}
			return currKey;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00032EB0 File Offset: 0x000310B0
		private static Keyframe FetchKeyFromIndexClampEdge([DisallowNull] NativeArray<Keyframe> keys, int index, float segmentStartTime, float segmentEndTime)
		{
			float startTime = Mathf.Min(segmentStartTime, keys[0].time);
			float endTime = Mathf.Max(segmentEndTime, keys[keys.Length - 1].time);
			float startValue = keys[0].value;
			float endValue = keys[keys.Length - 1].value;
			Keyframe ret;
			if (index < 0)
			{
				ret = new Keyframe(startTime, startValue, 0f, 0f);
			}
			else if (index >= keys.Length)
			{
				Keyframe keyframe = keys[keys.Length - 1];
				ret = new Keyframe(endTime, endValue, 0f, 0f);
			}
			else
			{
				ret = KeyframeUtility.GetKeyframeAndClampEdge(keys, index);
			}
			return ret;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00032F78 File Offset: 0x00031178
		private static void EvalCurveSegmentAndDeriv(out float dstValue, out float dstDeriv, Keyframe lhsKey, Keyframe rhsKey, float desiredTime)
		{
			float num = Mathf.Clamp(desiredTime, lhsKey.time, rhsKey.time);
			float dx = Mathf.Max(rhsKey.time - lhsKey.time, 0.0001f);
			float dy = rhsKey.value - lhsKey.value;
			float length = 1f / dx;
			float lengthSqr = length * length;
			float outTangent = lhsKey.outTangent;
			float m2 = rhsKey.inTangent;
			float d = outTangent * dx;
			float d2 = m2 * dx;
			float c0 = (d + d2 - dy - dy) * lengthSqr * length;
			float c = (dy + dy + dy - d - d - d2) * lengthSqr;
			float c2 = outTangent;
			float c3 = lhsKey.value;
			float t = Mathf.Clamp(num - lhsKey.time, 0f, dx);
			dstValue = t * (t * (t * c0 + c) + c2) + c3;
			dstDeriv = t * (3f * t * c0 + 2f * c) + c2;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00033060 File Offset: 0x00031260
		private static Keyframe EvalKeyAtTime([DisallowNull] NativeArray<Keyframe> keys, int lhsIndex, int rhsIndex, float startTime, float endTime, float currTime)
		{
			Keyframe lhsKey = KeyframeUtility.FetchKeyFromIndexClampEdge(keys, lhsIndex, startTime, endTime);
			Keyframe rhsKey = KeyframeUtility.FetchKeyFromIndexClampEdge(keys, rhsIndex, startTime, endTime);
			float currValue;
			float currDeriv;
			KeyframeUtility.EvalCurveSegmentAndDeriv(out currValue, out currDeriv, lhsKey, rhsKey, currTime);
			return new Keyframe(currTime, currValue, currDeriv, currDeriv);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003309C File Offset: 0x0003129C
		public static void InterpAnimationCurve(ref AnimationCurve lhsAndResultCurve, [DisallowNull] AnimationCurve rhsCurve, float t)
		{
			if (t > 0f && rhsCurve.length != 0)
			{
				if (t >= 1f || lhsAndResultCurve.length == 0)
				{
					lhsAndResultCurve.CopyFrom(rhsCurve);
					return;
				}
				NativeArray<Keyframe> lhsCurveKeys = new NativeArray<Keyframe>(lhsAndResultCurve.length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				NativeArray<Keyframe> rhsCurveKeys = new NativeArray<Keyframe>(rhsCurve.length, Allocator.Temp, NativeArrayOptions.ClearMemory);
				for (int i = 0; i < lhsAndResultCurve.length; i++)
				{
					lhsCurveKeys[i] = lhsAndResultCurve[i];
				}
				for (int j = 0; j < rhsCurve.length; j++)
				{
					rhsCurveKeys[j] = rhsCurve[j];
				}
				float startTime = Mathf.Min(lhsCurveKeys[0].time, rhsCurveKeys[0].time);
				float endTime = Mathf.Max(lhsCurveKeys[lhsAndResultCurve.length - 1].time, rhsCurveKeys[rhsCurve.length - 1].time);
				int maxNumKeys = lhsAndResultCurve.length + rhsCurve.length;
				int currNumKeys = 0;
				NativeArray<Keyframe> dstKeys = new NativeArray<Keyframe>(maxNumKeys, Allocator.Temp, NativeArrayOptions.ClearMemory);
				int lhsKeyCurr = 0;
				int rhsKeyCurr = 0;
				while (lhsKeyCurr < lhsCurveKeys.Length || rhsKeyCurr < rhsCurveKeys.Length)
				{
					bool lhsValid = lhsKeyCurr < lhsCurveKeys.Length;
					bool rhsValid = rhsKeyCurr < rhsCurveKeys.Length;
					Keyframe lhsKey = default(Keyframe);
					Keyframe rhsKey = default(Keyframe);
					if (lhsValid && rhsValid)
					{
						lhsKey = KeyframeUtility.GetKeyframeAndClampEdge(lhsCurveKeys, lhsKeyCurr);
						rhsKey = KeyframeUtility.GetKeyframeAndClampEdge(rhsCurveKeys, rhsKeyCurr);
						if (lhsKey.time == rhsKey.time)
						{
							lhsKeyCurr++;
							rhsKeyCurr++;
						}
						else if (lhsKey.time < rhsKey.time)
						{
							rhsKey = KeyframeUtility.EvalKeyAtTime(rhsCurveKeys, rhsKeyCurr - 1, rhsKeyCurr, startTime, endTime, lhsKey.time);
							lhsKeyCurr++;
						}
						else
						{
							lhsKey = KeyframeUtility.EvalKeyAtTime(lhsCurveKeys, lhsKeyCurr - 1, lhsKeyCurr, startTime, endTime, rhsKey.time);
							rhsKeyCurr++;
						}
					}
					else if (lhsValid)
					{
						lhsKey = KeyframeUtility.GetKeyframeAndClampEdge(lhsCurveKeys, lhsKeyCurr);
						rhsKey = KeyframeUtility.EvalKeyAtTime(rhsCurveKeys, rhsKeyCurr - 1, rhsKeyCurr, startTime, endTime, lhsKey.time);
						lhsKeyCurr++;
					}
					else
					{
						rhsKey = KeyframeUtility.GetKeyframeAndClampEdge(rhsCurveKeys, rhsKeyCurr);
						lhsKey = KeyframeUtility.EvalKeyAtTime(lhsCurveKeys, lhsKeyCurr - 1, lhsKeyCurr, startTime, endTime, rhsKey.time);
						rhsKeyCurr++;
					}
					Keyframe dstKey = KeyframeUtility.LerpSingleKeyframe(lhsKey, rhsKey, t);
					dstKeys[currNumKeys] = dstKey;
					currNumKeys++;
				}
				KeyframeUtility.ResetAnimationCurve(lhsAndResultCurve);
				for (int k = 0; k < currNumKeys; k++)
				{
					lhsAndResultCurve.AddKey(dstKeys[k]);
				}
				dstKeys.Dispose();
			}
		}
	}
}
