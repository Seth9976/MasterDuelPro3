using System;
using System.Diagnostics;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000038 RID: 56
	[DebuggerTypeProxy(typeof(BitField32DebugView))]
	[GenerateTestsForBurstCompatibility]
	public struct BitField32
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00004B00 File Offset: 0x00002D00
		public BitField32(uint initialValue = 0U)
		{
			this.Value = initialValue;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004B09 File Offset: 0x00002D09
		public void Clear()
		{
			this.Value = 0U;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004B12 File Offset: 0x00002D12
		public void SetBits(int pos, bool value)
		{
			this.Value = Bitwise.SetBits(this.Value, pos, 1U, value);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004B28 File Offset: 0x00002D28
		public void SetBits(int pos, bool value, int numBits)
		{
			uint mask = uint.MaxValue >> 32 - numBits;
			this.Value = Bitwise.SetBits(this.Value, pos, mask, value);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004B54 File Offset: 0x00002D54
		public uint GetBits(int pos, int numBits = 1)
		{
			uint mask = uint.MaxValue >> 32 - numBits;
			return Bitwise.ExtractBits(this.Value, pos, mask);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B78 File Offset: 0x00002D78
		public bool IsSet(int pos)
		{
			return this.GetBits(pos, 1) > 0U;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B85 File Offset: 0x00002D85
		public bool TestNone(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) == 0U;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004B92 File Offset: 0x00002D92
		public bool TestAny(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) > 0U;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public bool TestAll(int pos, int numBits = 1)
		{
			uint mask = uint.MaxValue >> 32 - numBits;
			return mask == Bitwise.ExtractBits(this.Value, pos, mask);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public int CountBits()
		{
			return math.countbits(this.Value);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public int CountLeadingZeros()
		{
			return math.lzcnt(this.Value);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004BE1 File Offset: 0x00002DE1
		public int CountTrailingZeros()
		{
			return math.tzcnt(this.Value);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004BEE File Offset: 0x00002DEE
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[Conditional("UNITY_DOTS_DEBUG")]
		private static void CheckArgs(int pos, int numBits)
		{
			if (pos > 31 || numBits == 0 || numBits > 32 || pos + numBits > 32)
			{
				throw new ArgumentException(string.Format("BitField32 invalid arguments: pos {0} (must be 0-31), numBits {1} (must be 1-32).", pos, numBits));
			}
		}

		// Token: 0x04000082 RID: 130
		public uint Value;
	}
}
