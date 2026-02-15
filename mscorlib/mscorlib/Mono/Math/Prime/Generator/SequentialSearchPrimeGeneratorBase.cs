using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200007C RID: 124
	internal class SequentialSearchPrimeGeneratorBase : PrimeGeneratorBase
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000F26B File Offset: 0x0000D46B
		protected virtual BigInteger GenerateSearchBase(int bits, object context)
		{
			BigInteger bigInteger = BigInteger.GenerateRandom(bits);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000F27A File Offset: 0x0000D47A
		public override BigInteger GenerateNewPrime(int bits)
		{
			return this.GenerateNewPrime(bits, null);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000F284 File Offset: 0x0000D484
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

		// Token: 0x06000259 RID: 601 RVA: 0x0000C091 File Offset: 0x0000A291
		protected virtual bool IsPrimeAcceptable(BigInteger bi, object context)
		{
			return true;
		}
	}
}
