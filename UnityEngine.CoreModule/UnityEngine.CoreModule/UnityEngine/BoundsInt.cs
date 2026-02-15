using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000C4 RID: 196
	[UsedByNativeCode]
	public struct BoundsInt : IEquatable<BoundsInt>, IFormattable
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000A968 File Offset: 0x00008B68
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x0000A980 File Offset: 0x00008B80
		public Vector3Int position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Position;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000A98C File Offset: 0x00008B8C
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		public Vector3Int size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Size;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Size = value;
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000A9AE File Offset: 0x00008BAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BoundsInt(Vector3Int position, Vector3Int size)
		{
			this.m_Position = position;
			this.m_Size = size;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000A9DC File Offset: 0x00008BDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("Position: {0}, Size: {1}", new object[]
			{
				this.m_Position.ToString(format, formatProvider),
				this.m_Size.ToString(format, formatProvider)
			});
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000AA34 File Offset: 0x00008C34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is BoundsInt);
			return !flag && this.Equals((BoundsInt)other);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000AA68 File Offset: 0x00008C68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(BoundsInt other)
		{
			return this.m_Position.Equals(other.m_Position) && this.m_Size.Equals(other.m_Size);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public override int GetHashCode()
		{
			return this.m_Position.GetHashCode() ^ (this.m_Size.GetHashCode() << 2);
		}

		// Token: 0x04000265 RID: 613
		private Vector3Int m_Position;

		// Token: 0x04000266 RID: 614
		private Vector3Int m_Size;
	}
}
