using System;
using System.Security.Cryptography;

namespace Meisui.Random
{
	// Token: 0x0200045C RID: 1116
	public class MersenneTwister
	{
		// Token: 0x060024CA RID: 9418 RVA: 0x000F1168 File Offset: 0x000EF368
		~MersenneTwister()
		{
			this.mt = null;
			this.mag01 = null;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000F119C File Offset: 0x000EF39C
		public uint genrand_Int32()
		{
			uint y;
			if (this.mti >= 624)
			{
				if (this.mti == 625)
				{
					this.init_genrand(5489U);
				}
				short kk;
				for (kk = 0; kk < 227; kk += 1)
				{
					y = ((this.mt[(int)kk] & 2147483648U) | (this.mt[(int)(kk + 1)] & 2147483647U)) >> 1;
					this.mt[(int)kk] = this.mt[(int)(kk + 397)] ^ this.mag01[(int)(this.mt[(int)(kk + 1)] & 1U)] ^ y;
				}
				while (kk < 623)
				{
					y = ((this.mt[(int)kk] & 2147483648U) | (this.mt[(int)(kk + 1)] & 2147483647U)) >> 1;
					this.mt[(int)kk] = this.mt[(int)(kk + -227)] ^ this.mag01[(int)(this.mt[(int)(kk + 1)] & 1U)] ^ y;
					kk += 1;
				}
				y = ((this.mt[623] & 2147483648U) | (this.mt[0] & 2147483647U)) >> 1;
				this.mt[623] = this.mt[396] ^ this.mag01[(int)(this.mt[0] & 1U)] ^ y;
				this.mti = 0;
			}
			uint[] array = this.mt;
			ushort num = this.mti;
			this.mti = num + 1;
			y = array[(int)num];
			y ^= y >> 11;
			y ^= (y << 7) & 2636928640U;
			y ^= (y << 15) & 4022730752U;
			return y ^ (y >> 18);
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000F1326 File Offset: 0x000EF526
		public uint genrand_Int31()
		{
			return this.genrand_Int32() >> 1;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000F1330 File Offset: 0x000EF530
		public MersenneTwister(uint s)
		{
			this.MT();
			this.init_genrand(s);
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000F1348 File Offset: 0x000EF548
		public MersenneTwister()
		{
			this.MT();
			uint[] seed_key = new uint[6];
			byte[] rnseed = new byte[8];
			seed_key[0] = (uint)DateTime.Now.Millisecond;
			seed_key[1] = (uint)DateTime.Now.Second;
			seed_key[2] = (uint)DateTime.Now.DayOfYear;
			seed_key[3] = (uint)DateTime.Now.Year;
			new RNGCryptoServiceProvider().GetNonZeroBytes(rnseed);
			seed_key[4] = (uint)(((int)rnseed[0] << 24) | ((int)rnseed[1] << 16) | ((int)rnseed[2] << 8) | (int)rnseed[3]);
			seed_key[5] = (uint)(((int)rnseed[4] << 24) | ((int)rnseed[5] << 16) | ((int)rnseed[6] << 8) | (int)rnseed[7]);
			this.init_by_array(seed_key);
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000F13F9 File Offset: 0x000EF5F9
		public MersenneTwister(uint[] init_key)
		{
			this.MT();
			this.init_by_array(init_key);
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000F140E File Offset: 0x000EF60E
		private void MT()
		{
			this.mt = new uint[624];
			this.mag01 = new uint[] { 0U, 2567483615U };
			this.mti = 625;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000F1440 File Offset: 0x000EF640
		private void init_genrand(uint s)
		{
			this.mt[0] = s;
			this.mti = 1;
			while (this.mti < 624)
			{
				this.mt[(int)this.mti] = 1812433253U * (this.mt[(int)(this.mti - 1)] ^ (this.mt[(int)(this.mti - 1)] >> 30)) + (uint)this.mti;
				this.mti += 1;
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000F14B8 File Offset: 0x000EF6B8
		private void init_by_array(uint[] init_key)
		{
			int key_length = init_key.Length;
			this.init_genrand(19650218U);
			uint i = 1U;
			uint j = 0U;
			for (int k = ((624 > key_length) ? 624 : key_length); k > 0; k--)
			{
				this.mt[(int)i] = (this.mt[(int)i] ^ ((this.mt[(int)(i - 1U)] ^ (this.mt[(int)(i - 1U)] >> 30)) * 1664525U)) + init_key[(int)j] + j;
				i += 1U;
				j += 1U;
				if ((ulong)i >= 624UL)
				{
					this.mt[0] = this.mt[623];
					i = 1U;
				}
				if ((ulong)j >= (ulong)((long)key_length))
				{
					j = 0U;
				}
			}
			for (int k = 623; k > 0; k--)
			{
				this.mt[(int)i] = (this.mt[(int)i] ^ ((this.mt[(int)(i - 1U)] ^ (this.mt[(int)(i - 1U)] >> 30)) * 1566083941U)) - i;
				i += 1U;
				if ((ulong)i >= 624UL)
				{
					this.mt[0] = this.mt[623];
					i = 1U;
				}
			}
			this.mt[0] = 2147483648U;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000F15CA File Offset: 0x000EF7CA
		public double genrand_real1()
		{
			return this.genrand_Int32() * 2.3283064370807974E-10;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000F15DE File Offset: 0x000EF7DE
		public double genrand_real2()
		{
			return this.genrand_Int32() * 2.3283064365386963E-10;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000F15F2 File Offset: 0x000EF7F2
		public double genrand_real3()
		{
			return (this.genrand_Int32() + 0.5) * 2.3283064365386963E-10;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000F1610 File Offset: 0x000EF810
		public double genrand_res53()
		{
			uint num = this.genrand_Int32() >> 5;
			uint b = this.genrand_Int32() >> 6;
			return (num * 67108864.0 + b) * 1.1102230246251565E-16;
		}

		// Token: 0x0400269A RID: 9882
		private const short N = 624;

		// Token: 0x0400269B RID: 9883
		private const short M = 397;

		// Token: 0x0400269C RID: 9884
		private const uint MATRIX_A = 2567483615U;

		// Token: 0x0400269D RID: 9885
		private const uint UPPER_MASK = 2147483648U;

		// Token: 0x0400269E RID: 9886
		private const uint LOWER_MASK = 2147483647U;

		// Token: 0x0400269F RID: 9887
		private uint[] mt;

		// Token: 0x040026A0 RID: 9888
		private ushort mti;

		// Token: 0x040026A1 RID: 9889
		private uint[] mag01;
	}
}
