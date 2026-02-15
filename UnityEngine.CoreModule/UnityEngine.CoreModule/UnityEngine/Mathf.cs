using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200014E RID: 334
	[NativeHeader("Runtime/Math/PerlinNoise.h")]
	[NativeHeader("Runtime/Math/ColorSpaceConversion.h")]
	[NativeHeader("Runtime/Math/FloatConversion.h")]
	[Il2CppEagerStaticClassConstruction]
	public struct Mathf
	{
		// Token: 0x06000E34 RID: 3636
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GammaToLinearSpace(float value);

		// Token: 0x06000E35 RID: 3637
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float LinearToGammaSpace(float value);

		// Token: 0x06000E36 RID: 3638 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		[FreeFunction(IsThreadSafe = true)]
		public static Color CorrelatedColorTemperatureToRGB(float kelvin)
		{
			Color color;
			Mathf.CorrelatedColorTemperatureToRGB_Injected(kelvin, out color);
			return color;
		}

		// Token: 0x06000E37 RID: 3639
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort FloatToHalf(float val);

		// Token: 0x06000E38 RID: 3640
		[FreeFunction("PerlinNoise::NoiseNormalized", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float PerlinNoise(float x, float y);

		// Token: 0x06000E39 RID: 3641 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0001D5EC File Offset: 0x0001B7EC
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0001D608 File Offset: 0x0001B808
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0001D624 File Offset: 0x0001B824
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0001D640 File Offset: 0x0001B840
		public static float Atan(float f)
		{
			return (float)Math.Atan((double)f);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0001D65C File Offset: 0x0001B85C
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0001D678 File Offset: 0x0001B878
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0001D694 File Offset: 0x0001B894
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		public static float Min(float a, float b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
		public static int Min(int a, int b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		public static float Max(float a, float b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0001D718 File Offset: 0x0001B918
		public static int Max(int a, int b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0001D734 File Offset: 0x0001B934
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0001D750 File Offset: 0x0001B950
		public static float Exp(float power)
		{
			return (float)Math.Exp((double)power);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0001D76C File Offset: 0x0001B96C
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0001D788 File Offset: 0x0001B988
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0001D7A4 File Offset: 0x0001B9A4
		public static float Log10(float f)
		{
			return (float)Math.Log10((double)f);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0001D7F8 File Offset: 0x0001B9F8
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0001D814 File Offset: 0x0001BA14
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0001D830 File Offset: 0x0001BA30
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0001D84C File Offset: 0x0001BA4C
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0001D868 File Offset: 0x0001BA68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sign(float f)
		{
			return (f >= 0f) ? 1f : (-1f);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0001D890 File Offset: 0x0001BA90
		public static float Clamp(float value, float min, float max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0001D8BC File Offset: 0x0001BABC
		public static int Clamp(int value, int min, int max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0001D8E8 File Offset: 0x0001BAE8
		public static float Clamp01(float value)
		{
			bool flag = value < 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = value > 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					num = value;
				}
			}
			return num;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0001D924 File Offset: 0x0001BB24
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Mathf.Clamp01(t);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0001D944 File Offset: 0x0001BB44
		public static float LerpUnclamped(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0001D960 File Offset: 0x0001BB60
		public static float LerpAngle(float a, float b, float t)
		{
			float delta = Mathf.Repeat(b - a, 360f);
			bool flag = delta > 180f;
			if (flag)
			{
				delta -= 360f;
			}
			return a + delta * Mathf.Clamp01(t);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0001D9A0 File Offset: 0x0001BBA0
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float omega = 2f / smoothTime;
			float x = omega * deltaTime;
			float exp = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);
			float change = current - target;
			float originalTo = target;
			float maxChange = maxSpeed * smoothTime;
			change = Mathf.Clamp(change, -maxChange, maxChange);
			target = current - change;
			float temp = (currentVelocity + omega * change) * deltaTime;
			currentVelocity = (currentVelocity - omega * temp) * exp;
			float output = target + (change + temp) * exp;
			bool flag = originalTo - current > 0f == output > originalTo;
			if (flag)
			{
				output = originalTo;
				currentVelocity = (output - originalTo) / deltaTime;
			}
			return output;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0001DAA0 File Offset: 0x0001BCA0
		public static float Repeat(float t, float length)
		{
			return Mathf.Clamp(t - Mathf.Floor(t / length) * length, 0f, length);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0001DACC File Offset: 0x0001BCCC
		public static float PingPong(float t, float length)
		{
			t = Mathf.Repeat(t, length * 2f);
			return length - Mathf.Abs(t - length);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		public static float InverseLerp(float a, float b, float value)
		{
			bool flag = a != b;
			float num;
			if (flag)
			{
				num = Mathf.Clamp01((value - a) / (b - a));
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static float ClampToFloat(double value)
		{
			bool flag = double.IsPositiveInfinity(value);
			float num;
			if (flag)
			{
				num = float.PositiveInfinity;
			}
			else
			{
				bool flag2 = double.IsNegativeInfinity(value);
				if (flag2)
				{
					num = float.NegativeInfinity;
				}
				else
				{
					bool flag3 = value < -3.4028234663852886E+38;
					if (flag3)
					{
						num = float.MinValue;
					}
					else
					{
						bool flag4 = value > 3.4028234663852886E+38;
						if (flag4)
						{
							num = float.MaxValue;
						}
						else
						{
							num = (float)value;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0001DB98 File Offset: 0x0001BD98
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
		internal static int ClampToInt(long value)
		{
			bool flag = value < -2147483648L;
			int num;
			if (flag)
			{
				num = int.MinValue;
			}
			else
			{
				bool flag2 = value > 2147483647L;
				if (flag2)
				{
					num = int.MaxValue;
				}
				else
				{
					num = (int)value;
				}
			}
			return num;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0001DBD8 File Offset: 0x0001BDD8
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static uint ClampToUInt(long value)
		{
			bool flag = value < 0L;
			uint num;
			if (flag)
			{
				num = 0U;
			}
			else
			{
				bool flag2 = value > (long)((ulong)(-1));
				if (flag2)
				{
					num = uint.MaxValue;
				}
				else
				{
					num = (uint)value;
				}
			}
			return num;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0001DC08 File Offset: 0x0001BE08
		internal static int GetNumberOfDecimalsForMinimumDifference(double minDifference)
		{
			return (int)Math.Max(0.0, -Math.Floor(Math.Log10(Math.Abs(minDifference))));
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static double RoundBasedOnMinimumDifference(double valueToRound, double minDifference)
		{
			bool flag = minDifference == 0.0;
			double num;
			if (flag)
			{
				num = Mathf.DiscardLeastSignificantDecimal(valueToRound);
			}
			else
			{
				num = Math.Round(valueToRound, Mathf.GetNumberOfDecimalsForMinimumDifference(minDifference), MidpointRounding.AwayFromZero);
			}
			return num;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0001DC74 File Offset: 0x0001BE74
		internal static double DiscardLeastSignificantDecimal(double v)
		{
			int decimals = Math.Max(0, (int)(5.0 - Math.Log10(Math.Abs(v))));
			double num;
			try
			{
				num = Math.Round(v, decimals);
			}
			catch (ArgumentOutOfRangeException)
			{
				num = 0.0;
			}
			return num;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0001DCCC File Offset: 0x0001BECC
		public static int NextPowerOfTwo(int value)
		{
			value--;
			value |= value >> 16;
			value |= value >> 8;
			value |= value >> 4;
			value |= value >> 2;
			value |= value >> 1;
			return value + 1;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0001DD0C File Offset: 0x0001BF0C
		public static bool IsPowerOfTwo(int value)
		{
			return (value & (value - 1)) == 0;
		}

		// Token: 0x06000E66 RID: 3686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CorrelatedColorTemperatureToRGB_Injected(float kelvin, out Color ret);

		// Token: 0x040005AD RID: 1453
		public static readonly float Epsilon = (MathfInternal.IsFlushToZeroEnabled ? MathfInternal.FloatMinNormal : MathfInternal.FloatMinDenormal);
	}
}
