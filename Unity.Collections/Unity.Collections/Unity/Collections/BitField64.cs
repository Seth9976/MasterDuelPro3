using System;
using System.Diagnostics;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x0200003A RID: 58
	[DebuggerTypeProxy(typeof(BitField64DebugView))]
	[GenerateTestsForBurstCompatibility]
	public struct BitField64
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00004C62 File Offset: 0x00002E62
		public BitField64(ulong initialValue = 0UL)
		{
			this.Value = initialValue;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004C6B File Offset: 0x00002E6B
		public void Clear()
		{
			this.Value = 0UL;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004C75 File Offset: 0x00002E75
		public void SetBits(int pos, bool value)
		{
			this.Value = Bitwise.SetBits(this.Value, pos, 1UL, value);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004C8C File Offset: 0x00002E8C
		public void SetBits(int pos, bool value, int numBits = 1)
		{
			ulong mask = ulong.MaxValue >> 64 - numBits;
			this.Value = Bitwise.SetBits(this.Value, pos, mask, value);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public ulong GetBits(int pos, int numBits = 1)
		{
			ulong mask = ulong.MaxValue >> 64 - numBits;
			return Bitwise.ExtractBits(this.Value, pos, mask);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004CDD File Offset: 0x00002EDD
		public bool IsSet(int pos)
		{
			return this.GetBits(pos, 1) > 0UL;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004CEB File Offset: 0x00002EEB
		public bool TestNone(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) == 0UL;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004CF9 File Offset: 0x00002EF9
		public bool TestAny(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) > 0UL;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004D08 File Offset: 0x00002F08
		public bool TestAll(int pos, int numBits = 1)
		{
			ulong mask = ulong.MaxValue >> 64 - numBits;
			return mask == Bitwise.ExtractBits(this.Value, pos, mask);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004D30 File Offset: 0x00002F30
		public int CountBits()
		{
			return math.countbits(this.Value);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004D3D File Offset: 0x00002F3D
		public int CountLeadingZeros()
		{
			return math.lzcnt(this.Value);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004D4A File Offset: 0x00002F4A
		public int CountTrailingZeros()
		{
			return math.tzcnt(this.Value);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004D57 File Offset: 0x00002F57
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckArgs(int pos, int numBits)
		{
			if (pos > 63 || numBits == 0 || numBits > 64 || pos + numBits > 64)
			{
				throw new ArgumentException(string.Format("BitField32 invalid arguments: pos {0} (must be 0-63), numBits {1} (must be 1-64).", pos, numBits));
			}
		}

		// Token: 0x04000084 RID: 132
		public ulong Value;
	}
}
