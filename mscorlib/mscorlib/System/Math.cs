using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Provides constants and static methods for trigonometric, logarithmic, and other common mathematical functions.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200011A RID: 282
	public static class Math
	{
		/// <summary>Returns the absolute value of a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.Int32.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than <see cref="F:System.Int32.MinValue" />, but less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000933 RID: 2355 RVA: 0x0002862C File Offset: 0x0002682C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Abs(int value)
		{
			if (value < 0)
			{
				value = -value;
				if (value < 0)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		/// <summary>Returns the absolute value of a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.Int64.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than <see cref="F:System.Int64.MinValue" />, but less than or equal to <see cref="F:System.Int64.MaxValue" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.Int64.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000934 RID: 2356 RVA: 0x00028640 File Offset: 0x00026840
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long Abs(long value)
		{
			if (value < 0L)
			{
				value = -value;
				if (value < 0L)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		/// <summary>Returns the absolute value of a <see cref="T:System.Decimal" /> number.</summary>
		/// <returns>A decimal number, x, such that 0 ≤ x ≤<see cref="F:System.Decimal.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Decimal.MinValue" />, but less than or equal to <see cref="F:System.Decimal.MaxValue" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000935 RID: 2357 RVA: 0x00028656 File Offset: 0x00026856
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Abs(decimal value)
		{
			return decimal.Abs(ref value);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002865F File Offset: 0x0002685F
		[StackTraceHidden]
		private static void ThrowAbsOverflow()
		{
			throw new OverflowException("Negating the minimum value of a twos complement number is invalid.");
		}

		/// <summary>Calculates the quotient of two 32-bit signed integers and also returns the remainder in an output parameter.</summary>
		/// <returns>The quotient of the specified numbers.</returns>
		/// <param name="a">The dividend. </param>
		/// <param name="b">The divisor. </param>
		/// <param name="result">The remainder. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="b" /> is zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000937 RID: 2359 RVA: 0x0002866C File Offset: 0x0002686C
		public static int DivRem(int a, int b, out int result)
		{
			int num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00028685 File Offset: 0x00026885
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Clamp(int value, int min, int max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<int>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002869F File Offset: 0x0002689F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float value, float min, float max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<float>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		/// <summary>Returns the logarithm of a specified number in a specified base.</summary>
		/// <returns>One of the values in the following table. (+Infinity denotes <see cref="F:System.Double.PositiveInfinity" />, -Infinity denotes <see cref="F:System.Double.NegativeInfinity" />, and NaN denotes <see cref="F:System.Double.NaN" />.)<paramref name="a" /><paramref name="newBase" />Return value<paramref name="a" />&gt; 0(0 &lt;<paramref name="newBase" />&lt; 1) -or-(<paramref name="newBase" />&gt; 1)lognewBase(a)<paramref name="a" />&lt; 0(any value)NaN(any value)<paramref name="newBase" />&lt; 0NaN<paramref name="a" /> != 1<paramref name="newBase" /> = 0NaN<paramref name="a" /> != 1<paramref name="newBase" /> = +InfinityNaN<paramref name="a" /> = NaN(any value)NaN(any value)<paramref name="newBase" /> = NaNNaN(any value)<paramref name="newBase" /> = 1NaN<paramref name="a" /> = 00 &lt;<paramref name="newBase" />&lt; 1 +Infinity<paramref name="a" /> = 0<paramref name="newBase" />&gt; 1-Infinity<paramref name="a" /> =  +Infinity0 &lt;<paramref name="newBase" />&lt; 1-Infinity<paramref name="a" /> =  +Infinity<paramref name="newBase" />&gt; 1+Infinity<paramref name="a" /> = 1<paramref name="newBase" /> = 00<paramref name="a" /> = 1<paramref name="newBase" /> = +Infinity0</returns>
		/// <param name="a">The number whose logarithm is to be found. </param>
		/// <param name="newBase">The base of the logarithm. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093A RID: 2362 RVA: 0x000286BC File Offset: 0x000268BC
		public static double Log(double a, double newBase)
		{
			if (double.IsNaN(a))
			{
				return a;
			}
			if (double.IsNaN(newBase))
			{
				return newBase;
			}
			if (newBase == 1.0)
			{
				return double.NaN;
			}
			if (a != 1.0 && (newBase == 0.0 || double.IsPositiveInfinity(newBase)))
			{
				return double.NaN;
			}
			return Math.Log(a) / Math.Log(newBase);
		}

		/// <summary>Returns the larger of two 8-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 8-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 8-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093B RID: 2363 RVA: 0x0002872A File Offset: 0x0002692A
		[NonVersionable]
		public static byte Max(byte val1, byte val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two decimal numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two decimal numbers to compare. </param>
		/// <param name="val2">The second of two decimal numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093C RID: 2364 RVA: 0x00028733 File Offset: 0x00026933
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static decimal Max(decimal val1, decimal val2)
		{
			return *decimal.Max(ref val1, ref val2);
		}

		/// <summary>Returns the larger of two double-precision floating-point numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		/// <param name="val1">The first of two double-precision floating-point numbers to compare. </param>
		/// <param name="val2">The second of two double-precision floating-point numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093D RID: 2365 RVA: 0x00028743 File Offset: 0x00026943
		public static double Max(double val1, double val2)
		{
			if (val1 > val2)
			{
				return val1;
			}
			if (double.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		/// <summary>Returns the larger of two 16-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 16-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 16-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093E RID: 2366 RVA: 0x0002872A File Offset: 0x0002692A
		[NonVersionable]
		public static short Max(short val1, short val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 32-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 32-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 32-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093F RID: 2367 RVA: 0x0002872A File Offset: 0x0002692A
		[NonVersionable]
		public static int Max(int val1, int val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 64-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 64-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 64-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000940 RID: 2368 RVA: 0x0002872A File Offset: 0x0002692A
		[NonVersionable]
		public static long Max(long val1, long val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 8-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 8-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 8-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000941 RID: 2369 RVA: 0x0002872A File Offset: 0x0002692A
		[CLSCompliant(false)]
		[NonVersionable]
		public static sbyte Max(sbyte val1, sbyte val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two single-precision floating-point numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger. If <paramref name="val1" />, or <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NaN" /> is returned.</returns>
		/// <param name="val1">The first of two single-precision floating-point numbers to compare. </param>
		/// <param name="val2">The second of two single-precision floating-point numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000942 RID: 2370 RVA: 0x00028756 File Offset: 0x00026956
		public static float Max(float val1, float val2)
		{
			if (val1 > val2)
			{
				return val1;
			}
			if (float.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		/// <summary>Returns the larger of two 16-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 16-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 16-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000943 RID: 2371 RVA: 0x0002872A File Offset: 0x0002692A
		[NonVersionable]
		[CLSCompliant(false)]
		public static ushort Max(ushort val1, ushort val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 32-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 32-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 32-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000944 RID: 2372 RVA: 0x00028769 File Offset: 0x00026969
		[NonVersionable]
		[CLSCompliant(false)]
		public static uint Max(uint val1, uint val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 64-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 64-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 64-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000945 RID: 2373 RVA: 0x00028769 File Offset: 0x00026969
		[NonVersionable]
		[CLSCompliant(false)]
		public static ulong Max(ulong val1, ulong val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 8-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 8-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 8-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000946 RID: 2374 RVA: 0x00028772 File Offset: 0x00026972
		[NonVersionable]
		public static byte Min(byte val1, byte val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two decimal numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two decimal numbers to compare. </param>
		/// <param name="val2">The second of two decimal numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000947 RID: 2375 RVA: 0x0002877B File Offset: 0x0002697B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static decimal Min(decimal val1, decimal val2)
		{
			return *decimal.Min(ref val1, ref val2);
		}

		/// <summary>Returns the smaller of two double-precision floating-point numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		/// <param name="val1">The first of two double-precision floating-point numbers to compare. </param>
		/// <param name="val2">The second of two double-precision floating-point numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000948 RID: 2376 RVA: 0x0002878B File Offset: 0x0002698B
		public static double Min(double val1, double val2)
		{
			if (val1 < val2)
			{
				return val1;
			}
			if (double.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		/// <summary>Returns the smaller of two 16-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 16-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 16-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000949 RID: 2377 RVA: 0x00028772 File Offset: 0x00026972
		[NonVersionable]
		public static short Min(short val1, short val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 32-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 32-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 32-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094A RID: 2378 RVA: 0x00028772 File Offset: 0x00026972
		[NonVersionable]
		public static int Min(int val1, int val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 64-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 64-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 64-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094B RID: 2379 RVA: 0x00028772 File Offset: 0x00026972
		[NonVersionable]
		public static long Min(long val1, long val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 8-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 8-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 8-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094C RID: 2380 RVA: 0x00028772 File Offset: 0x00026972
		[NonVersionable]
		[CLSCompliant(false)]
		public static sbyte Min(sbyte val1, sbyte val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two single-precision floating-point numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NaN" /> is returned.</returns>
		/// <param name="val1">The first of two single-precision floating-point numbers to compare. </param>
		/// <param name="val2">The second of two single-precision floating-point numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094D RID: 2381 RVA: 0x0002879E File Offset: 0x0002699E
		public static float Min(float val1, float val2)
		{
			if (val1 < val2)
			{
				return val1;
			}
			if (float.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		/// <summary>Returns the smaller of two 16-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 16-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 16-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094E RID: 2382 RVA: 0x00028772 File Offset: 0x00026972
		[NonVersionable]
		[CLSCompliant(false)]
		public static ushort Min(ushort val1, ushort val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 32-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 32-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 32-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094F RID: 2383 RVA: 0x000287B1 File Offset: 0x000269B1
		[CLSCompliant(false)]
		[NonVersionable]
		public static uint Min(uint val1, uint val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 64-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 64-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 64-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000950 RID: 2384 RVA: 0x000287B1 File Offset: 0x000269B1
		[NonVersionable]
		[CLSCompliant(false)]
		public static ulong Min(ulong val1, ulong val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Rounds a decimal value to the nearest integral value.</summary>
		/// <returns>The integer nearest parameter <paramref name="d" />. If the fractional component of <paramref name="d" /> is halfway between two integers, one of which is even and the other odd, the even number is returned. Note that this method returns a <see cref="T:System.Decimal" /> instead of an integral type.</returns>
		/// <param name="d">A decimal number to be rounded. </param>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000951 RID: 2385 RVA: 0x000287BA File Offset: 0x000269BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d)
		{
			return decimal.Round(d, 0);
		}

		/// <summary>Rounds a double-precision floating-point value to the nearest integral value.</summary>
		/// <returns>The integer nearest <paramref name="a" />. If the fractional component of <paramref name="a" /> is halfway between two integers, one of which is even and the other odd, then the even number is returned. Note that this method returns a <see cref="T:System.Double" /> instead of an integral type.</returns>
		/// <param name="a">A double-precision floating-point number to be rounded. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000952 RID: 2386
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Round(double a);

		/// <summary>Rounds a double-precision floating-point value to a specified number of fractional digits.</summary>
		/// <returns>The number nearest to <paramref name="value" /> that contains a number of fractional digits equal to <paramref name="digits" />.</returns>
		/// <param name="value">A double-precision floating-point number to be rounded. </param>
		/// <param name="digits">The number of fractional digits in the return value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="digits" /> is less than 0 or greater than 15. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000953 RID: 2387 RVA: 0x000287C3 File Offset: 0x000269C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Round(double value, int digits)
		{
			return Math.Round(value, digits, MidpointRounding.ToEven);
		}

		/// <summary>Rounds a double-precision floating-point value to the nearest integer. A parameter specifies how to round the value if it is midway between two numbers.</summary>
		/// <returns>The integer nearest <paramref name="value" />. If <paramref name="value" /> is halfway between two integers, one of which is even and the other odd, then <paramref name="mode" /> determines which of the two is returned.</returns>
		/// <param name="value">A double-precision floating-point number to be rounded. </param>
		/// <param name="mode">Specification for how to round <paramref name="value" /> if it is midway between two other numbers.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000954 RID: 2388 RVA: 0x000287CD File Offset: 0x000269CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Round(double value, MidpointRounding mode)
		{
			return Math.Round(value, 0, mode);
		}

		/// <summary>Rounds a double-precision floating-point value to a specified number of fractional digits. A parameter specifies how to round the value if it is midway between two numbers.</summary>
		/// <returns>The number nearest to <paramref name="value" /> that has a number of fractional digits equal to <paramref name="digits" />. If <paramref name="value" /> has fewer fractional digits than <paramref name="digits" />, <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">A double-precision floating-point number to be rounded. </param>
		/// <param name="digits">The number of fractional digits in the return value. </param>
		/// <param name="mode">Specification for how to round <paramref name="value" /> if it is midway between two other numbers.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="digits" /> is less than 0 or greater than 15. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000955 RID: 2389 RVA: 0x000287D8 File Offset: 0x000269D8
		public unsafe static double Round(double value, int digits, MidpointRounding mode)
		{
			if (digits < 0 || digits > 15)
			{
				throw new ArgumentOutOfRangeException("digits", "Rounding digits must be between 0 and 15, inclusive.");
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not valid for this usage of the type {1}.", mode, "MidpointRounding"), "mode");
			}
			if (Math.Abs(value) < Math.doubleRoundLimit)
			{
				double num = Math.roundPower10Double[digits];
				value *= num;
				if (mode == MidpointRounding.AwayFromZero)
				{
					double num2 = Math.ModF(value, &value);
					if (Math.Abs(num2) >= 0.5)
					{
						value += (double)Math.Sign(num2);
					}
				}
				else
				{
					value = Math.Round(value);
				}
				value /= num;
			}
			return value;
		}

		/// <summary>Returns a value indicating the sign of a decimal number.</summary>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.Return value Meaning -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed decimal number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000956 RID: 2390 RVA: 0x0002887A File Offset: 0x00026A7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(decimal value)
		{
			return decimal.Sign(ref value);
		}

		/// <summary>Returns a value indicating the sign of a double-precision floating-point number.</summary>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.Return value Meaning -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <exception cref="T:System.ArithmeticException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000957 RID: 2391 RVA: 0x00028883 File Offset: 0x00026A83
		public static int Sign(double value)
		{
			if (value < 0.0)
			{
				return -1;
			}
			if (value > 0.0)
			{
				return 1;
			}
			if (value == 0.0)
			{
				return 0;
			}
			throw new ArithmeticException("Function does not accept floating point Not-a-Number values.");
		}

		/// <summary>Returns a value indicating the sign of a 64-bit signed integer.</summary>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.Return value Meaning -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000958 RID: 2392 RVA: 0x000288B9 File Offset: 0x00026AB9
		public static int Sign(long value)
		{
			return (int)((value >> 63) | (long)((ulong)(-(ulong)value) >> 63));
		}

		/// <summary>Returns a value indicating the sign of a single-precision floating-point number.</summary>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.Return value Meaning -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <exception cref="T:System.ArithmeticException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Single.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000959 RID: 2393 RVA: 0x000288C6 File Offset: 0x00026AC6
		public static int Sign(float value)
		{
			if (value < 0f)
			{
				return -1;
			}
			if (value > 0f)
			{
				return 1;
			}
			if (value == 0f)
			{
				return 0;
			}
			throw new ArithmeticException("Function does not accept floating point Not-a-Number values.");
		}

		/// <summary>Calculates the integral part of a specified decimal number. </summary>
		/// <returns>The integral part of <paramref name="d" />; that is, the number that remains after any fractional digits have been discarded.</returns>
		/// <param name="d">A number to truncate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600095A RID: 2394 RVA: 0x000288F0 File Offset: 0x00026AF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Truncate(decimal d)
		{
			return decimal.Truncate(d);
		}

		/// <summary>Calculates the integral part of a specified double-precision floating-point number. </summary>
		/// <returns>The integral part of <paramref name="d" />; that is, the number that remains after any fractional digits have been discarded, or one of the values listed in the following table. <paramref name="d" />Return value<see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" /><see cref="F:System.Double.NegativeInfinity" /><see cref="F:System.Double.NegativeInfinity" /><see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		/// <param name="d">A number to truncate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600095B RID: 2395 RVA: 0x000288F8 File Offset: 0x00026AF8
		public unsafe static double Truncate(double d)
		{
			Math.ModF(d, &d);
			return d;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00028905 File Offset: 0x00026B05
		private static void ThrowMinMaxException<T>(T min, T max)
		{
			throw new ArgumentException(SR.Format("'{0}' cannot be greater than {1}.", min, max));
		}

		/// <summary>Returns the absolute value of a double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤<see cref="F:System.Double.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Double.MinValue" />, but less than or equal to <see cref="F:System.Double.MaxValue" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600095D RID: 2397
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Abs(double value);

		/// <summary>Returns the absolute value of a single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number, x, such that 0 ≤ x ≤<see cref="F:System.Single.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Single.MinValue" />, but less than or equal to <see cref="F:System.Single.MaxValue" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600095E RID: 2398
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Abs(float value);

		/// <summary>Returns the angle whose cosine is the specified number.</summary>
		/// <returns>An angle, θ, measured in radians, such that 0 ≤θ≤π-or- <see cref="F:System.Double.NaN" /> if <paramref name="d" /> &lt; -1 or <paramref name="d" /> &gt; 1 or <paramref name="d" /> equals <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="d">A number representing a cosine, where <paramref name="d" /> must be greater than or equal to -1, but less than or equal to 1. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600095F RID: 2399
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Acos(double d);

		/// <summary>Returns the angle whose sine is the specified number.</summary>
		/// <returns>An angle, θ, measured in radians, such that -π/2 ≤θ≤π/2 -or- <see cref="F:System.Double.NaN" /> if <paramref name="d" /> &lt; -1 or <paramref name="d" /> &gt; 1 or <paramref name="d" /> equals <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="d">A number representing a sine, where <paramref name="d" /> must be greater than or equal to -1, but less than or equal to 1. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000960 RID: 2400
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Asin(double d);

		/// <summary>Returns the angle whose tangent is the specified number.</summary>
		/// <returns>An angle, θ, measured in radians, such that -π/2 ≤θ≤π/2.-or- <see cref="F:System.Double.NaN" /> if <paramref name="d" /> equals <see cref="F:System.Double.NaN" />, -π/2 rounded to double precision (-1.5707963267949) if <paramref name="d" /> equals <see cref="F:System.Double.NegativeInfinity" />, or π/2 rounded to double precision (1.5707963267949) if <paramref name="d" /> equals <see cref="F:System.Double.PositiveInfinity" />.</returns>
		/// <param name="d">A number representing a tangent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000961 RID: 2401
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan(double d);

		/// <summary>Returns the angle whose tangent is the quotient of two specified numbers.</summary>
		/// <returns>An angle, θ, measured in radians, such that -π≤θ≤π, and tan(θ) = <paramref name="y" /> / <paramref name="x" />, where (<paramref name="x" />, <paramref name="y" />) is a point in the Cartesian plane. Observe the following: For (<paramref name="x" />, <paramref name="y" />) in quadrant 1, 0 &lt; θ &lt; π/2.For (<paramref name="x" />, <paramref name="y" />) in quadrant 2, π/2 &lt; θ≤π.For (<paramref name="x" />, <paramref name="y" />) in quadrant 3, -π &lt; θ &lt; -π/2.For (<paramref name="x" />, <paramref name="y" />) in quadrant 4, -π/2 &lt; θ &lt; 0.For points on the boundaries of the quadrants, the return value is the following:If y is 0 and x is not negative, θ = 0.If y is 0 and x is negative, θ = π.If y is positive and x is 0, θ = π/2.If y is negative and x is 0, θ = -π/2.If <paramref name="x" /> or <paramref name="y" /> is <see cref="F:System.Double.NaN" />, or if <paramref name="x" /> and <paramref name="y" /> are either <see cref="F:System.Double.PositiveInfinity" /> or <see cref="F:System.Double.NegativeInfinity" />, the method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="y">The y coordinate of a point. </param>
		/// <param name="x">The x coordinate of a point. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000962 RID: 2402
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan2(double y, double x);

		/// <summary>Returns the smallest integral value that is greater than or equal to the specified double-precision floating-point number.</summary>
		/// <returns>The smallest integral value that is greater than or equal to <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, that value is returned. Note that this method returns a <see cref="T:System.Double" /> instead of an integral type.</returns>
		/// <param name="a">A double-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000963 RID: 2403
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Ceiling(double a);

		/// <summary>Returns the cosine of the specified angle.</summary>
		/// <returns>The cosine of <paramref name="d" />. If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="d">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000964 RID: 2404
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cos(double d);

		/// <summary>Returns the hyperbolic cosine of the specified angle.</summary>
		/// <returns>The hyperbolic cosine of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" /> or <see cref="F:System.Double.PositiveInfinity" />, <see cref="F:System.Double.PositiveInfinity" /> is returned. If <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		/// <param name="value">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000965 RID: 2405
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cosh(double value);

		/// <summary>Returns e raised to the specified power.</summary>
		/// <returns>The number e raised to the power <paramref name="d" />. If <paramref name="d" /> equals <see cref="F:System.Double.NaN" /> or <see cref="F:System.Double.PositiveInfinity" />, that value is returned. If <paramref name="d" /> equals <see cref="F:System.Double.NegativeInfinity" />, 0 is returned.</returns>
		/// <param name="d">A number specifying a power. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000966 RID: 2406
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exp(double d);

		/// <summary>Returns the largest integer less than or equal to the specified double-precision floating-point number.</summary>
		/// <returns>The largest integer less than or equal to <paramref name="d" />. If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, that value is returned.</returns>
		/// <param name="d">A double-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000967 RID: 2407
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Floor(double d);

		/// <summary>Returns the natural (base e) logarithm of a specified number.</summary>
		/// <returns>One of the values in the following table. <paramref name="d" /> parameterReturn value Positive The natural logarithm of <paramref name="d" />; that is, ln <paramref name="d" />, or log e<paramref name="d" />Zero <see cref="F:System.Double.NegativeInfinity" />Negative <see cref="F:System.Double.NaN" />Equal to <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" />Equal to <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		/// <param name="d">The number whose logarithm is to be found. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000968 RID: 2408
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log(double d);

		/// <summary>Returns the base 10 logarithm of a specified number.</summary>
		/// <returns>One of the values in the following table. <paramref name="d" /> parameter Return value Positive The base 10 log of <paramref name="d" />; that is, log 10<paramref name="d" />. Zero <see cref="F:System.Double.NegativeInfinity" />Negative <see cref="F:System.Double.NaN" />Equal to <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" />Equal to <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		/// <param name="d">A number whose logarithm is to be found. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000969 RID: 2409
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log10(double d);

		/// <summary>Returns a specified number raised to the specified power.</summary>
		/// <returns>The number <paramref name="x" /> raised to the power <paramref name="y" />.</returns>
		/// <param name="x">A double-precision floating-point number to be raised to a power. </param>
		/// <param name="y">A double-precision floating-point number that specifies a power. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096A RID: 2410
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Pow(double x, double y);

		/// <summary>Returns the sine of the specified angle.</summary>
		/// <returns>The sine of <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="a">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096B RID: 2411
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sin(double a);

		/// <summary>Returns the hyperbolic sine of the specified angle.</summary>
		/// <returns>The hyperbolic sine of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NaN" />, this method returns a <see cref="T:System.Double" /> equal to <paramref name="value" />.</returns>
		/// <param name="value">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096C RID: 2412
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sinh(double value);

		/// <summary>Returns the square root of a specified number.</summary>
		/// <returns>One of the values in the following table. <paramref name="d" /> parameter Return value Zero or positive The positive square root of <paramref name="d" />. Negative <see cref="F:System.Double.NaN" />Equals <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" />Equals <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		/// <param name="d">The number whose square root is to be found. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096D RID: 2413
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sqrt(double d);

		/// <summary>Returns the tangent of the specified angle.</summary>
		/// <returns>The tangent of <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="a">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096E RID: 2414
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tan(double a);

		/// <summary>Returns the hyperbolic tangent of the specified angle.</summary>
		/// <returns>The hyperbolic tangent of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" />, this method returns -1. If value is equal to <see cref="F:System.Double.PositiveInfinity" />, this method returns 1. If <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="value">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096F RID: 2415
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tanh(double value);

		// Token: 0x06000970 RID: 2416
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern double ModF(double x, double* intptr);

		// Token: 0x04000446 RID: 1094
		private static double doubleRoundLimit = 10000000000000000.0;

		// Token: 0x04000447 RID: 1095
		private static double[] roundPower10Double = new double[]
		{
			1.0, 10.0, 100.0, 1000.0, 10000.0, 100000.0, 1000000.0, 10000000.0, 100000000.0, 1000000000.0,
			10000000000.0, 100000000000.0, 1000000000000.0, 10000000000000.0, 100000000000000.0, 1000000000000000.0
		};
	}
}
