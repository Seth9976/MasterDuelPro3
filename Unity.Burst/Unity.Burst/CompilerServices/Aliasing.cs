using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x0200004F RID: 79
	public static class Aliasing
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x000024D5 File Offset: 0x000006D5
		public unsafe static void ExpectAliased(void* a, void* b)
		{
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x000024D5 File Offset: 0x000006D5
		public static void ExpectAliased<A, B>(in A a, in B b) where A : struct where B : struct
		{
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000024D5 File Offset: 0x000006D5
		public unsafe static void ExpectAliased<B>(void* a, in B b) where B : struct
		{
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000024D5 File Offset: 0x000006D5
		public unsafe static void ExpectAliased<A>(in A a, void* b) where A : struct
		{
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000024D5 File Offset: 0x000006D5
		public unsafe static void ExpectNotAliased(void* a, void* b)
		{
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000024D5 File Offset: 0x000006D5
		public static void ExpectNotAliased<A, B>(in A a, in B b) where A : struct where B : struct
		{
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000024D5 File Offset: 0x000006D5
		public unsafe static void ExpectNotAliased<B>(void* a, in B b) where B : struct
		{
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000024D5 File Offset: 0x000006D5
		public unsafe static void ExpectNotAliased<A>(in A a, void* b) where A : struct
		{
		}
	}
}
