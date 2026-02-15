using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200005E RID: 94
	public class SequentialSearchPrimeGeneratorBase : PrimeGeneratorBase
	{
		// Token: 0x0600023E RID: 574 RVA: 0x0000E8F7 File Offset: 0x0000CAF7
		protected virtual BigInteger GenerateSearchBase(int bits, object context)
		{
			BigInteger bigInteger = BigInteger.GenerateRandom(bits);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000E906 File Offset: 0x0000CB06
		public override BigInteger GenerateNewPrime(int bits)
		{
			return this.GenerateNewPrime(bits, null);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000E910 File Offset: 0x0000CB10
		public virtual BigInteger GenerateNewPrime(int bits, object context)
		{
			BigInteger bigInteger = this.GenerateSearchBase(bits, context);
			uint num = bigInteger % 3234846615U;
			int trialDivisionBounds = this.TrialDivisionBounds;
			uint[] smallPrimes = BigInteger.smallPrimes;
			for (;;)
			{
				if (num % 3U != 0U && num % 5U != 0U && num % 7U != 0U && num % 11U != 0U && num % 13U != 0U && num % 17U != 0U && num % 19U != 0U && num % 23U != 0U && num % 29U != 0U)
				{
					int num2 = 10;
					while (num2 < smallPrimes.Length && (ulong)smallPrimes[num2] <= (ulong)((long)trialDivisionBounds))
					{
						if (bigInteger % smallPrimes[num2] == 0U)
						{
							goto IL_009D;
						}
						num2++;
					}
					if (this.IsPrimeAcceptable(bigInteger, context) && this.PrimalityTest(bigInteger, this.Confidence))
					{
						break;
					}
				}
				IL_009D:
				num += 2U;
				if (num >= 3234846615U)
				{
					num -= 3234846615U;
				}
				bigInteger.Incr2();
			}
			return bigInteger;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009861 File Offset: 0x00007A61
		protected virtual bool IsPrimeAcceptable(BigInteger bi, object context)
		{
			return true;
		}
	}
}
