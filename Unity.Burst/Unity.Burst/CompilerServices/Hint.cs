using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000052 RID: 82
	public static class Hint
	{
		// Token: 0x06000E35 RID: 3637 RVA: 0x00006072 File Offset: 0x00004272
		public static bool Likely(bool condition)
		{
			return condition;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00006072 File Offset: 0x00004272
		public static bool Unlikely(bool condition)
		{
			return condition;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000024D5 File Offset: 0x000006D5
		public static void Assume(bool condition)
		{
		}
	}
}
