using System;
using System.Collections.Generic;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000016 RID: 22
	public class CancellationTokenEqualityComparer : IEqualityComparer<CancellationToken>
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000030F0 File Offset: 0x000012F0
		public bool Equals(CancellationToken x, CancellationToken y)
		{
			return x.Equals(y);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000030FA File Offset: 0x000012FA
		public int GetHashCode(CancellationToken obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x0400004C RID: 76
		public static readonly IEqualityComparer<CancellationToken> Default = new CancellationTokenEqualityComparer();
	}
}
