using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200005D RID: 93
	public abstract class PrimeGeneratorBase
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000E8DF File Offset: 0x0000CADF
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000E8E2 File Offset: 0x0000CAE2
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x0600023C RID: 572
		public abstract BigInteger GenerateNewPrime(int bits);
	}
}
