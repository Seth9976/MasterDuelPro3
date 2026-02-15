using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000052 RID: 82
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct RigidTransform
	{
		// Token: 0x06001EC8 RID: 7880 RVA: 0x00058969 File Offset: 0x00056B69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RigidTransform(quaternion rotation, float3 translation)
		{
			this.rot = rotation;
			this.pos = translation;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00058979 File Offset: 0x00056B79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RigidTransform(float3x3 rotation, float3 translation)
		{
			this.rot = new quaternion(rotation);
			this.pos = translation;
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0005898E File Offset: 0x00056B8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RigidTransform(float4x4 transform)
		{
			this.rot = new quaternion(transform);
			this.pos = transform.c3.xyz;
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000589AE File Offset: 0x00056BAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform AxisAngle(float3 axis, float angle)
		{
			return new RigidTransform(quaternion.AxisAngle(axis, angle), float3.zero);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000589C1 File Offset: 0x00056BC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerXYZ(float3 xyz)
		{
			return new RigidTransform(quaternion.EulerXYZ(xyz), float3.zero);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000589D3 File Offset: 0x00056BD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerXZY(float3 xyz)
		{
			return new RigidTransform(quaternion.EulerXZY(xyz), float3.zero);
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x000589E5 File Offset: 0x00056BE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerYXZ(float3 xyz)
		{
			return new RigidTransform(quaternion.EulerYXZ(xyz), float3.zero);
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x000589F7 File Offset: 0x00056BF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerYZX(float3 xyz)
		{
			return new RigidTransform(quaternion.EulerYZX(xyz), float3.zero);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00058A09 File Offset: 0x00056C09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerZXY(float3 xyz)
		{
			return new RigidTransform(quaternion.EulerZXY(xyz), float3.zero);
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00058A1B File Offset: 0x00056C1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerZYX(float3 xyz)
		{
			return new RigidTransform(quaternion.EulerZYX(xyz), float3.zero);
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00058A2D File Offset: 0x00056C2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerXYZ(float x, float y, float z)
		{
			return RigidTransform.EulerXYZ(math.float3(x, y, z));
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00058A3C File Offset: 0x00056C3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerXZY(float x, float y, float z)
		{
			return RigidTransform.EulerXZY(math.float3(x, y, z));
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00058A4B File Offset: 0x00056C4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerYXZ(float x, float y, float z)
		{
			return RigidTransform.EulerYXZ(math.float3(x, y, z));
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00058A5A File Offset: 0x00056C5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerYZX(float x, float y, float z)
		{
			return RigidTransform.EulerYZX(math.float3(x, y, z));
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00058A69 File Offset: 0x00056C69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerZXY(float x, float y, float z)
		{
			return RigidTransform.EulerZXY(math.float3(x, y, z));
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00058A78 File Offset: 0x00056C78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform EulerZYX(float x, float y, float z)
		{
			return RigidTransform.EulerZYX(math.float3(x, y, z));
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00058A88 File Offset: 0x00056C88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform Euler(float3 xyz, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			switch (order)
			{
			case math.RotationOrder.XYZ:
				return RigidTransform.EulerXYZ(xyz);
			case math.RotationOrder.XZY:
				return RigidTransform.EulerXZY(xyz);
			case math.RotationOrder.YXZ:
				return RigidTransform.EulerYXZ(xyz);
			case math.RotationOrder.YZX:
				return RigidTransform.EulerYZX(xyz);
			case math.RotationOrder.ZXY:
				return RigidTransform.EulerZXY(xyz);
			case math.RotationOrder.ZYX:
				return RigidTransform.EulerZYX(xyz);
			default:
				return RigidTransform.identity;
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00058AE4 File Offset: 0x00056CE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform Euler(float x, float y, float z, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			return RigidTransform.Euler(math.float3(x, y, z), order);
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00058AF4 File Offset: 0x00056CF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RotateX(float angle)
		{
			return new RigidTransform(quaternion.RotateX(angle), float3.zero);
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00058B06 File Offset: 0x00056D06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RotateY(float angle)
		{
			return new RigidTransform(quaternion.RotateY(angle), float3.zero);
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x00058B18 File Offset: 0x00056D18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RotateZ(float angle)
		{
			return new RigidTransform(quaternion.RotateZ(angle), float3.zero);
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00058B2A File Offset: 0x00056D2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform Translate(float3 vector)
		{
			return new RigidTransform(quaternion.identity, vector);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00058B37 File Offset: 0x00056D37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(RigidTransform x)
		{
			return this.rot.Equals(x.rot) && this.pos.Equals(x.pos);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00058B60 File Offset: 0x00056D60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object x)
		{
			if (x is RigidTransform)
			{
				RigidTransform converted = (RigidTransform)x;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00058B85 File Offset: 0x00056D85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00058B94 File Offset: 0x00056D94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("RigidTransform(({0}f, {1}f, {2}f, {3}f),  ({4}f, {5}f, {6}f))", new object[]
			{
				this.rot.value.x,
				this.rot.value.y,
				this.rot.value.z,
				this.rot.value.w,
				this.pos.x,
				this.pos.y,
				this.pos.z
			});
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00058C4C File Offset: 0x00056E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4x4(({0}f, {1}f, {2}f, {3}f),  ({4}f, {5}f, {6}f))", new object[]
			{
				this.rot.value.x.ToString(format, formatProvider),
				this.rot.value.y.ToString(format, formatProvider),
				this.rot.value.z.ToString(format, formatProvider),
				this.rot.value.w.ToString(format, formatProvider),
				this.pos.x.ToString(format, formatProvider),
				this.pos.y.ToString(format, formatProvider),
				this.pos.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000138 RID: 312
		public quaternion rot;

		// Token: 0x04000139 RID: 313
		public float3 pos;

		// Token: 0x0400013A RID: 314
		public static readonly RigidTransform identity = new RigidTransform(new quaternion(0f, 0f, 0f, 1f), new float3(0f, 0f, 0f));
	}
}
