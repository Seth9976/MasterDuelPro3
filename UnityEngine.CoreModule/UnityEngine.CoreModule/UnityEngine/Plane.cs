using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C6 RID: 198
	[UsedByNativeCode]
	public struct Plane : IFormattable
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public Vector3 normal
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		public float distance
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000ABFC File Offset: 0x00008DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = -Vector3.Dot(this.m_Normal, inPoint);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000AC1E File Offset: 0x00008E1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(Vector3 inNormal, float d)
		{
			this.m_Normal = Vector3.Normalize(inNormal);
			this.m_Distance = d;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000AC34 File Offset: 0x00008E34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			this.m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			this.m_Distance = -Vector3.Dot(this.m_Normal, a);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000AC68 File Offset: 0x00008E68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetDistanceToPoint(Vector3 point)
		{
			return Vector3.Dot(this.m_Normal, point) + this.m_Distance;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000AC90 File Offset: 0x00008E90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Raycast(Ray ray, out float enter)
		{
			float vdot = Vector3.Dot(ray.direction, this.m_Normal);
			float ndot = -Vector3.Dot(ray.origin, this.m_Normal) - this.m_Distance;
			bool flag = Mathf.Approximately(vdot, 0f);
			bool flag2;
			if (flag)
			{
				enter = 0f;
				flag2 = false;
			}
			else
			{
				enter = ndot / vdot;
				flag2 = enter > 0f;
			}
			return flag2;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000ACFC File Offset: 0x00008EFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000AD18 File Offset: 0x00008F18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("(normal:{0}, distance:{1})", new object[]
			{
				this.m_Normal.ToString(format, formatProvider),
				this.m_Distance.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000267 RID: 615
		internal const int size = 16;

		// Token: 0x04000268 RID: 616
		private Vector3 m_Normal;

		// Token: 0x04000269 RID: 617
		private float m_Distance;
	}
}
