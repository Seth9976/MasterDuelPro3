using System;
using System.Runtime.CompilerServices;
using System.Security.Util;
using System.Threading;

namespace System
{
	// Token: 0x020001AE RID: 430
	internal sealed class SharedStatics
	{
		// Token: 0x0600103E RID: 4158 RVA: 0x00003CE1 File Offset: 0x00001EE1
		private SharedStatics()
		{
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00044D68 File Offset: 0x00042F68
		public static Tokenizer.StringMaker GetSharedStringMaker()
		{
			Tokenizer.StringMaker stringMaker = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				if (SharedStatics._sharedStatics._maker != null)
				{
					stringMaker = SharedStatics._sharedStatics._maker;
					SharedStatics._sharedStatics._maker = null;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
			if (stringMaker == null)
			{
				stringMaker = new Tokenizer.StringMaker();
			}
			return stringMaker;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00044DD8 File Offset: 0x00042FD8
		public static void ReleaseSharedStringMaker(ref Tokenizer.StringMaker maker)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Monitor.Enter(SharedStatics._sharedStatics, ref flag);
				SharedStatics._sharedStatics._maker = maker;
				maker = null;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(SharedStatics._sharedStatics);
				}
			}
		}

		// Token: 0x04000642 RID: 1602
		private static readonly SharedStatics _sharedStatics = new SharedStatics();

		// Token: 0x04000643 RID: 1603
		private Tokenizer.StringMaker _maker;
	}
}
