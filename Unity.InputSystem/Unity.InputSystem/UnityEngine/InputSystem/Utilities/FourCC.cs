using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200023C RID: 572
	public struct FourCC : IEquatable<FourCC>
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x0005EE59 File Offset: 0x0005D059
		public FourCC(int code)
		{
			this.m_Code = code;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0005EE62 File Offset: 0x0005D062
		public FourCC(char a, char b = ' ', char c = ' ', char d = ' ')
		{
			this.m_Code = (int)(((int)a << 24) | ((int)b << 16) | ((int)c << 8) | d);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0005EE7C File Offset: 0x0005D07C
		public FourCC(string str)
		{
			this = default(FourCC);
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (length < 1 || length > 4)
			{
				throw new ArgumentException("FourCC string must be one to four characters long!", "str");
			}
			char a = str[0];
			char b = ((length > 1) ? str[1] : ' ');
			char c = ((length > 2) ? str[2] : ' ');
			char d = ((length > 3) ? str[3] : ' ');
			this.m_Code = (int)(((int)a << 24) | ((int)b << 16) | ((int)c << 8) | d);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0005EF0C File Offset: 0x0005D10C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0005EF14 File Offset: 0x0005D114
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator FourCC(int i)
		{
			return new FourCC(i);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0005EF1C File Offset: 0x0005D11C
		public override string ToString()
		{
			return string.Format("{0}{1}{2}{3}", new object[]
			{
				(char)(this.m_Code >> 24),
				(char)((this.m_Code & 16711680) >> 16),
				(char)((this.m_Code & 65280) >> 8),
				(char)(this.m_Code & 255)
			});
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0005EF8F File Offset: 0x0005D18F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(FourCC other)
		{
			return this.m_Code == other.m_Code;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0005EFA0 File Offset: 0x0005D1A0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is FourCC)
			{
				FourCC cc = (FourCC)obj;
				return this.Equals(cc);
			}
			return false;
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0005EF0C File Offset: 0x0005D10C
		public override int GetHashCode()
		{
			return this.m_Code;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0005EF8F File Offset: 0x0005D18F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(FourCC left, FourCC right)
		{
			return left.m_Code == right.m_Code;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0005EFCA File Offset: 0x0005D1CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(FourCC left, FourCC right)
		{
			return left.m_Code != right.m_Code;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0005EFDD File Offset: 0x0005D1DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FourCC FromInt32(int i)
		{
			return i;
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0005EF0C File Offset: 0x0005D10C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToInt32(FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		// Token: 0x04000C46 RID: 3142
		private int m_Code;
	}
}
