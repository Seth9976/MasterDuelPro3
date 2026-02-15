using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics.Geometry
{
	// Token: 0x02000065 RID: 101
	[DebuggerDisplay("{Normal}, {Distance}")]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct Plane
	{
		// Token: 0x060024C3 RID: 9411 RVA: 0x0006789C File Offset: 0x00065A9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(float coefficientA, float coefficientB, float coefficientC, float coefficientD)
		{
			this.NormalAndDistance = Plane.Normalize(new float4(coefficientA, coefficientB, coefficientC, coefficientD));
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000678B3 File Offset: 0x00065AB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(float3 normal, float distance)
		{
			this.NormalAndDistance = Plane.Normalize(new float4(normal, distance));
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000678C7 File Offset: 0x00065AC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(float3 normal, float3 pointInPlane)
		{
			this = new Plane(normal, -math.dot(normal, pointInPlane));
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000678D8 File Offset: 0x00065AD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(float3 vector1InPlane, float3 vector2InPlane, float3 pointInPlane)
		{
			this = new Plane(math.cross(vector1InPlane, vector2InPlane), pointInPlane);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000678E8 File Offset: 0x00065AE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane CreateFromUnitNormalAndDistance(float3 unitNormal, float distance)
		{
			return new Plane
			{
				NormalAndDistance = new float4(unitNormal, distance)
			};
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0006790C File Offset: 0x00065B0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane CreateFromUnitNormalAndPointInPlane(float3 unitNormal, float3 pointInPlane)
		{
			return new Plane
			{
				NormalAndDistance = new float4(unitNormal, -math.dot(unitNormal, pointInPlane))
			};
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x00067937 File Offset: 0x00065B37
		// (set) Token: 0x060024CA RID: 9418 RVA: 0x00067944 File Offset: 0x00065B44
		public float3 Normal
		{
			get
			{
				return this.NormalAndDistance.xyz;
			}
			set
			{
				this.NormalAndDistance.xyz = value;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x00067952 File Offset: 0x00065B52
		// (set) Token: 0x060024CC RID: 9420 RVA: 0x0006795F File Offset: 0x00065B5F
		public float Distance
		{
			get
			{
				return this.NormalAndDistance.w;
			}
			set
			{
				this.NormalAndDistance.w = value;
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x00067970 File Offset: 0x00065B70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Normalize(Plane plane)
		{
			return new Plane
			{
				NormalAndDistance = Plane.Normalize(plane.NormalAndDistance)
			};
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x00067998 File Offset: 0x00065B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 Normalize(float4 planeCoefficients)
		{
			float recipLength = math.rsqrt(math.lengthsq(planeCoefficients.xyz));
			return new Plane
			{
				NormalAndDistance = planeCoefficients * recipLength
			};
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000679D3 File Offset: 0x00065BD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float SignedDistanceToPoint(float3 point)
		{
			return math.dot(this.NormalAndDistance, new float4(point, 1f));
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000679EB File Offset: 0x00065BEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 Projection(float3 point)
		{
			return point - this.Normal * this.SignedDistanceToPoint(point);
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x00067A08 File Offset: 0x00065C08
		public Plane Flipped
		{
			get
			{
				return new Plane
				{
					NormalAndDistance = -this.NormalAndDistance
				};
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00067A30 File Offset: 0x00065C30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(Plane plane)
		{
			return plane.NormalAndDistance;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00067A38 File Offset: 0x00065C38
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckPlaneIsNormalized()
		{
			float ll = math.lengthsq(this.Normal.xyz);
			if (ll < 0.99800104f || ll > 1.002001f)
			{
				throw new ArgumentException("Plane must be normalized. Call Plane.Normalize() to normalize plane.");
			}
		}

		// Token: 0x0400017D RID: 381
		public float4 NormalAndDistance;
	}
}
