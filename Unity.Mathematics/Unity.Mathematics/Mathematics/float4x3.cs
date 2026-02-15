using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000038 RID: 56
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float4x3 : IEquatable<float4x3>, IFormattable
	{
		// Token: 0x0600156F RID: 5487 RVA: 0x00041621 File Offset: 0x0003F821
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(float4 c0, float4 c1, float4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00041638 File Offset: 0x0003F838
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22, float m30, float m31, float m32)
		{
			this.c0 = new float4(m00, m10, m20, m30);
			this.c1 = new float4(m01, m11, m21, m31);
			this.c2 = new float4(m02, m12, m22, m32);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00041670 File Offset: 0x0003F870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00041698 File Offset: 0x0003F898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(bool v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v);
			this.c1 = math.select(new float4(0f), new float4(1f), v);
			this.c2 = math.select(new float4(0f), new float4(1f), v);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00041708 File Offset: 0x0003F908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(bool4x3 v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v.c0);
			this.c1 = math.select(new float4(0f), new float4(1f), v.c1);
			this.c2 = math.select(new float4(0f), new float4(1f), v.c2);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00041784 File Offset: 0x0003F984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000417AA File Offset: 0x0003F9AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(int4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000417DF File Offset: 0x0003F9DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00041805 File Offset: 0x0003FA05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(uint4x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004183A File Offset: 0x0003FA3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(double v)
		{
			this.c0 = (float4)v;
			this.c1 = (float4)v;
			this.c2 = (float4)v;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00041860 File Offset: 0x0003FA60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x3(double4x3 v)
		{
			this.c0 = (float4)v.c0;
			this.c1 = (float4)v.c1;
			this.c2 = (float4)v.c2;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0000CE9A File Offset: 0x0000B09A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(float v)
		{
			return new float4x3(v);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0000CEA2 File Offset: 0x0000B0A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x3(bool v)
		{
			return new float4x3(v);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0000CEAA File Offset: 0x0000B0AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x3(bool4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0000CEB2 File Offset: 0x0000B0B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(int v)
		{
			return new float4x3(v);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0000CEBA File Offset: 0x0000B0BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(int4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0000CEC2 File Offset: 0x0000B0C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(uint v)
		{
			return new float4x3(v);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0000CECA File Offset: 0x0000B0CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x3(uint4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0000CED2 File Offset: 0x0000B0D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x3(double v)
		{
			return new float4x3(v);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0000CEDA File Offset: 0x0000B0DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x3(double4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00041895 File Offset: 0x0003FA95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator *(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000418CF File Offset: 0x0003FACF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator *(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000418FA File Offset: 0x0003FAFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator *(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00041925 File Offset: 0x0003FB25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator +(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0004195F File Offset: 0x0003FB5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator +(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0004198A File Offset: 0x0003FB8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator +(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000419B5 File Offset: 0x0003FBB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator -(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000419EF File Offset: 0x0003FBEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator -(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00041A1A File Offset: 0x0003FC1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator -(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00041A45 File Offset: 0x0003FC45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator /(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00041A7F File Offset: 0x0003FC7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator /(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00041AAA File Offset: 0x0003FCAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator /(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00041AD5 File Offset: 0x0003FCD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator %(float4x3 lhs, float4x3 rhs)
		{
			return new float4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00041B0F File Offset: 0x0003FD0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator %(float4x3 lhs, float rhs)
		{
			return new float4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00041B3A File Offset: 0x0003FD3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator %(float lhs, float4x3 rhs)
		{
			return new float4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00041B68 File Offset: 0x0003FD68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator ++(float4x3 val)
		{
			float4 @float = float4.op_Increment(val.c0);
			val.c0 = @float;
			float4 float2 = @float;
			@float = float4.op_Increment(val.c1);
			val.c1 = @float;
			float4 float3 = @float;
			@float = float4.op_Increment(val.c2);
			val.c2 = @float;
			return new float4x3(float2, float3, @float);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00041BC8 File Offset: 0x0003FDC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator --(float4x3 val)
		{
			float4 @float = float4.op_Decrement(val.c0);
			val.c0 = @float;
			float4 float2 = @float;
			@float = float4.op_Decrement(val.c1);
			val.c1 = @float;
			float4 float3 = @float;
			@float = float4.op_Decrement(val.c2);
			val.c2 = @float;
			return new float4x3(float2, float3, @float);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00041C28 File Offset: 0x0003FE28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00041C62 File Offset: 0x0003FE62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00041C8D File Offset: 0x0003FE8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00041CF2 File Offset: 0x0003FEF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00041D1D File Offset: 0x0003FF1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00041D48 File Offset: 0x0003FF48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00041D82 File Offset: 0x0003FF82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00041DAD File Offset: 0x0003FFAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00041DD8 File Offset: 0x0003FFD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00041E12 File Offset: 0x00040012
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00041E3D File Offset: 0x0004003D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00041E68 File Offset: 0x00040068
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator -(float4x3 val)
		{
			return new float4x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00041E90 File Offset: 0x00040090
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 operator +(float4x3 val)
		{
			return new float4x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00041EB8 File Offset: 0x000400B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00041EF2 File Offset: 0x000400F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00041F1D File Offset: 0x0004011D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00041F48 File Offset: 0x00040148
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(float4x3 lhs, float4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00041F82 File Offset: 0x00040182
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(float4x3 lhs, float rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00041FAD File Offset: 0x000401AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(float lhs, float4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x170005C6 RID: 1478
		public unsafe ref float4 this[int index]
		{
			get
			{
				fixed (float4x3* ptr = &this)
				{
					return ref *(float4*)(ptr + (IntPtr)index * (IntPtr)sizeof(float4) / (IntPtr)sizeof(float4x3));
				}
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00041FF3 File Offset: 0x000401F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00042030 File Offset: 0x00040230
		public override bool Equals(object o)
		{
			if (o is float4x3)
			{
				float4x3 converted = (float4x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00042055 File Offset: 0x00040255
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00042064 File Offset: 0x00040264
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float4x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f,  {9}f, {10}f, {11}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c0.w,
				this.c1.w,
				this.c2.w
			});
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0004216C File Offset: 0x0004036C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f,  {9}f, {10}f, {11}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000D8 RID: 216
		public float4 c0;

		// Token: 0x040000D9 RID: 217
		public float4 c1;

		// Token: 0x040000DA RID: 218
		public float4 c2;

		// Token: 0x040000DB RID: 219
		public static readonly float4x3 zero;
	}
}
