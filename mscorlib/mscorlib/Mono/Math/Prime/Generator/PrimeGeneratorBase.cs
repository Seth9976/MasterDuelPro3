using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200007B RID: 123
	internal abstract class PrimeGeneratorBase
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000F253 File Offset: 0x0000D453
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000F256 File Offset: 0x0000D456
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000F264 File Offset: 0x0000D464
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x06000254 RID: 596
		public abstract BigInteger GenerateNewPrime(int bits);
	}
}
