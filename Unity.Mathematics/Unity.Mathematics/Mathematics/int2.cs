using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000041 RID: 65
	[DebuggerTypeProxy(typeof(int2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int2 : IEquatable<int2>, IFormattable
	{
		// Token: 0x060018A3 RID: 6307 RVA: 0x00049149 File Offset: 0x00047349
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00049159 File Offset: 0x00047359
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(int2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00049173 File Offset: 0x00047373
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(int v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00049183 File Offset: 0x00047383
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(bool v)
		{
			this.x = (v ? 1 : 0);
			this.y = (v ? 1 : 0);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0004919F File Offset: 0x0004739F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(bool2 v)
		{
			this.x = (v.x ? 1 : 0);
			this.y = (v.y ? 1 : 0);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00049173 File Offset: 0x00047373
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(uint v)
		{
			this.x = (int)v;
			this.y = (int)v;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000491C5 File Offset: 0x000473C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(uint2 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000491DF File Offset: 0x000473DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(float v)
		{
			this.x = (int)v;
			this.y = (int)v;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000491F1 File Offset: 0x000473F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(float2 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000491DF File Offset: 0x000473DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(double v)
		{
			this.x = (int)v;
			this.y = (int)v;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0004920D File Offset: 0x0004740D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2(double2 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0000DCBF File Offset: 0x0000BEBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int2(int v)
		{
			return new int2(v);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0000DCC7 File Offset: 0x0000BEC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(bool v)
		{
			return new int2(v);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0000DCCF File Offset: 0x0000BECF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(bool2 v)
		{
			return new int2(v);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0000DCD7 File Offset: 0x0000BED7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(uint v)
		{
			return new int2(v);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0000DCDF File Offset: 0x0000BEDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(uint2 v)
		{
			return new int2(v);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0000DCE7 File Offset: 0x0000BEE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(float v)
		{
			return new int2(v);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0000DCEF File Offset: 0x0000BEEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(float2 v)
		{
			return new int2(v);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0000DCF7 File Offset: 0x0000BEF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(double v)
		{
			return new int2(v);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0000DCFF File Offset: 0x0000BEFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int2(double2 v)
		{
			return new int2(v);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00049229 File Offset: 0x00047429
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0004924A File Offset: 0x0004744A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int2 lhs, int rhs)
		{
			return new int2(lhs.x * rhs, lhs.y * rhs);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00049261 File Offset: 0x00047461
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int lhs, int2 rhs)
		{
			return new int2(lhs * rhs.x, lhs * rhs.y);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00049278 File Offset: 0x00047478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00049299 File Offset: 0x00047499
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int2 lhs, int rhs)
		{
			return new int2(lhs.x + rhs, lhs.y + rhs);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000492B0 File Offset: 0x000474B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int lhs, int2 rhs)
		{
			return new int2(lhs + rhs.x, lhs + rhs.y);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000492C7 File Offset: 0x000474C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000492E8 File Offset: 0x000474E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 lhs, int rhs)
		{
			return new int2(lhs.x - rhs, lhs.y - rhs);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000492FF File Offset: 0x000474FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int lhs, int2 rhs)
		{
			return new int2(lhs - rhs.x, lhs - rhs.y);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00049316 File Offset: 0x00047516
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00049337 File Offset: 0x00047537
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int2 lhs, int rhs)
		{
			return new int2(lhs.x / rhs, lhs.y / rhs);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0004934E File Offset: 0x0004754E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int lhs, int2 rhs)
		{
			return new int2(lhs / rhs.x, lhs / rhs.y);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00049365 File Offset: 0x00047565
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00049386 File Offset: 0x00047586
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int2 lhs, int rhs)
		{
			return new int2(lhs.x % rhs, lhs.y % rhs);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0004939D File Offset: 0x0004759D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int lhs, int2 rhs)
		{
			return new int2(lhs % rhs.x, lhs % rhs.y);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000493B4 File Offset: 0x000475B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ++(int2 val)
		{
			int num = val.x + 1;
			val.x = num;
			int num2 = num;
			num = val.y + 1;
			val.y = num;
			return new int2(num2, num);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000493E4 File Offset: 0x000475E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator --(int2 val)
		{
			int num = val.x - 1;
			val.x = num;
			int num2 = num;
			num = val.y - 1;
			val.y = num;
			return new int2(num2, num);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00049414 File Offset: 0x00047614
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00049437 File Offset: 0x00047637
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(int2 lhs, int rhs)
		{
			return new bool2(lhs.x < rhs, lhs.y < rhs);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00049450 File Offset: 0x00047650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(int lhs, int2 rhs)
		{
			return new bool2(lhs < rhs.x, lhs < rhs.y);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00049469 File Offset: 0x00047669
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00049492 File Offset: 0x00047692
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(int2 lhs, int rhs)
		{
			return new bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000494B1 File Offset: 0x000476B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(int lhs, int2 rhs)
		{
			return new bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000494D0 File Offset: 0x000476D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000494F3 File Offset: 0x000476F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(int2 lhs, int rhs)
		{
			return new bool2(lhs.x > rhs, lhs.y > rhs);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0004950C File Offset: 0x0004770C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(int lhs, int2 rhs)
		{
			return new bool2(lhs > rhs.x, lhs > rhs.y);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00049525 File Offset: 0x00047725
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0004954E File Offset: 0x0004774E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(int2 lhs, int rhs)
		{
			return new bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0004956D File Offset: 0x0004776D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(int lhs, int2 rhs)
		{
			return new bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0004958C File Offset: 0x0004778C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 val)
		{
			return new int2(-val.x, -val.y);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000495A1 File Offset: 0x000477A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int2 val)
		{
			return new int2(val.x, val.y);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000495B4 File Offset: 0x000477B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator <<(int2 x, int n)
		{
			return new int2(x.x << n, x.y << n);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000495D1 File Offset: 0x000477D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator >>(int2 x, int n)
		{
			return new int2(x.x >> n, x.y >> n);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000495EE File Offset: 0x000477EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00049611 File Offset: 0x00047811
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(int2 lhs, int rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0004962A File Offset: 0x0004782A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(int lhs, int2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00049643 File Offset: 0x00047843
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(int2 lhs, int2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0004966C File Offset: 0x0004786C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(int2 lhs, int rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0004968B File Offset: 0x0004788B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(int lhs, int2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000496AA File Offset: 0x000478AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ~(int2 val)
		{
			return new int2(~val.x, ~val.y);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000496BF File Offset: 0x000478BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator &(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x & rhs.x, lhs.y & rhs.y);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000496E0 File Offset: 0x000478E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator &(int2 lhs, int rhs)
		{
			return new int2(lhs.x & rhs, lhs.y & rhs);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000496F7 File Offset: 0x000478F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator &(int lhs, int2 rhs)
		{
			return new int2(lhs & rhs.x, lhs & rhs.y);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0004970E File Offset: 0x0004790E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator |(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x | rhs.x, lhs.y | rhs.y);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0004972F File Offset: 0x0004792F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator |(int2 lhs, int rhs)
		{
			return new int2(lhs.x | rhs, lhs.y | rhs);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00049746 File Offset: 0x00047946
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator |(int lhs, int2 rhs)
		{
			return new int2(lhs | rhs.x, lhs | rhs.y);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0004975D File Offset: 0x0004795D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ^(int2 lhs, int2 rhs)
		{
			return new int2(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0004977E File Offset: 0x0004797E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ^(int2 lhs, int rhs)
		{
			return new int2(lhs.x ^ rhs, lhs.y ^ rhs);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00049795 File Offset: 0x00047995
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ^(int lhs, int2 rhs)
		{
			return new int2(lhs ^ rhs.x, lhs ^ rhs.y);
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x000497AC File Offset: 0x000479AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x000497CB File Offset: 0x000479CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x000497EA File Offset: 0x000479EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x00049809 File Offset: 0x00047A09
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00049828 File Offset: 0x00047A28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x00049847 File Offset: 0x00047A47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00049866 File Offset: 0x00047A66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x00049885 File Offset: 0x00047A85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x000498A4 File Offset: 0x00047AA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000498C3 File Offset: 0x00047AC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x000498E2 File Offset: 0x00047AE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x00049901 File Offset: 0x00047B01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x00049920 File Offset: 0x00047B20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0004993F File Offset: 0x00047B3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x0004995E File Offset: 0x00047B5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0004997D File Offset: 0x00047B7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0004999C File Offset: 0x00047B9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x000499B5 File Offset: 0x00047BB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.y);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x000499CE File Offset: 0x00047BCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.x);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x000499E7 File Offset: 0x00047BE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x00049A00 File Offset: 0x00047C00
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00049A19 File Offset: 0x00047C19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x00049A32 File Offset: 0x00047C32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x00049A4B File Offset: 0x00047C4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x00049A64 File Offset: 0x00047C64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.x);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x000495A1 File Offset: 0x000477A1
		// (set) Token: 0x06001902 RID: 6402 RVA: 0x00049159 File Offset: 0x00047359
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00049A77 File Offset: 0x00047C77
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x00049A8A File Offset: 0x00047C8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x00049AA4 File Offset: 0x00047CA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.y);
			}
		}

		// Token: 0x170007CC RID: 1996
		public unsafe int this[int index]
		{
			get
			{
				fixed (int2* ptr = &this)
				{
					return ((int*)ptr)[index];
				}
			}
			set
			{
				fixed (int* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00049AF0 File Offset: 0x00047CF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00049B10 File Offset: 0x00047D10
		public override bool Equals(object o)
		{
			if (o is int2)
			{
				int2 converted = (int2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00049B35 File Offset: 0x00047D35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00049B42 File Offset: 0x00047D42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int2({0}, {1})", this.x, this.y);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00049B64 File Offset: 0x00047D64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int2({0}, {1})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x040000F9 RID: 249
		public int x;

		// Token: 0x040000FA RID: 250
		public int y;

		// Token: 0x040000FB RID: 251
		public static readonly int2 zero;

		// Token: 0x02000042 RID: 66
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600190D RID: 6413 RVA: 0x00049B8A File Offset: 0x00047D8A
			public DebuggerProxy(int2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x040000FC RID: 252
			public int x;

			// Token: 0x040000FD RID: 253
			public int y;
		}
	}
}
