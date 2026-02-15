using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000C7 RID: 199
	public struct Ray : IFormattable
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0000AD7F File Offset: 0x00008F7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.m_Origin = origin;
			this.m_Direction = direction.normalized;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000AD98 File Offset: 0x00008F98
		public Vector3 origin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Origin;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public Vector3 direction
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Direction;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public Vector3 GetPoint(float distance)
		{
			return this.m_Origin + this.m_Direction * distance;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000AE10 File Offset: 0x00009010
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
			return UnityString.Format("Origin: {0}, Dir: {1}", new object[]
			{
				this.m_Origin.ToString(format, formatProvider),
				this.m_Direction.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400026A RID: 618
		private Vector3 m_Origin;

		// Token: 0x0400026B RID: 619
		private Vector3 m_Direction;
	}
}
