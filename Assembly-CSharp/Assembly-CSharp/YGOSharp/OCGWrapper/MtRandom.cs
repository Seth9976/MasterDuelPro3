using System;

namespace YGOSharp.OCGWrapper
{
	// Token: 0x020001CB RID: 459
	internal class MtRandom
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x00026074 File Offset: 0x00024274
		internal MtRandom()
		{
			this.Init(19650218U);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002609E File Offset: 0x0002429E
		internal MtRandom(uint seed)
		{
			this.Init(seed);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000260C4 File Offset: 0x000242C4
		internal void Init(uint seed = 19650218U)
		{
			this._state[0] = seed & uint.MaxValue;
			for (int i = 1; i < 624; i++)
			{
				this._state[i] = (uint)((ulong)(1812433253U * (this._state[i - 1] ^ (this._state[i - 1] >> 30))) + (ulong)((long)i));
				this._state[i] &= uint.MaxValue;
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00026128 File Offset: 0x00024328
		internal uint Rand()
		{
			uint num = this._left - 1U;
			this._left = num;
			if (num == 0U)
			{
				this.NextState();
			}
			uint[] state = this._state;
			num = this._current;
			this._current = num + 1U;
			object obj = state[(int)num];
			object obj2 = obj ^ (obj >> 11);
			object obj3 = obj2 ^ ((obj2 << 7) & -1658038656);
			object obj4 = obj3 ^ ((obj3 << 15) & -272236544);
			return obj4 ^ (obj4 >> 18);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00026185 File Offset: 0x00024385
		internal void Reset(uint rs)
		{
			this.Init(rs);
			this.NextState();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00026194 File Offset: 0x00024394
		private void NextState()
		{
			int i = 0;
			int j = 228;
			while (--j != 0)
			{
				this._state[i] = this._state[i + 397] ^ MtRandom.Twist(this._state[i], this._state[i + 1]);
				i++;
			}
			int k = 397;
			while (--k != 0)
			{
				this._state[i] = this._state[i + 397 - 624] ^ MtRandom.Twist(this._state[i], this._state[i + 1]);
				i++;
			}
			this._state[i] = this._state[i + 397 - 624] ^ MtRandom.Twist(this._state[i], this._state[0]);
			this._left = 624U;
			this._current = 0U;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0002626F File Offset: 0x0002446F
		private static uint Twist(uint u, uint v)
		{
			return (MtRandom.MixBits(u, v) >> 1) ^ (((v & 1U) != 0U) ? 2567483615U : 0U);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00026288 File Offset: 0x00024488
		private static uint MixBits(uint u, uint v)
		{
			return (u & 2147483648U) | (v & 2147483647U);
		}

		// Token: 0x04000BC7 RID: 3015
		private const int N = 624;

		// Token: 0x04000BC8 RID: 3016
		private const int M = 397;

		// Token: 0x04000BC9 RID: 3017
		private uint _current;

		// Token: 0x04000BCA RID: 3018
		private uint _left = 1U;

		// Token: 0x04000BCB RID: 3019
		private uint[] _state = new uint[624];
	}
}
