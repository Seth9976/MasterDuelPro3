using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000015 RID: 21
	public readonly struct AsyncUnit : IEquatable<AsyncUnit>
	{
		// Token: 0x06000081 RID: 129 RVA: 0x000030E1 File Offset: 0x000012E1
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000030E4 File Offset: 0x000012E4
		public bool Equals(AsyncUnit other)
		{
			return true;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000030E7 File Offset: 0x000012E7
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x0400004B RID: 75
		public static readonly AsyncUnit Default;
	}
}
