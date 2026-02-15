using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000013 RID: 19
	public static class GenericBitFlagsExtensions
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000032D7 File Offset: 0x000014D7
		public static bool HasAny(this GeneralBitFlags target, GeneralBitFlags flags)
		{
			return (target & flags) > (GeneralBitFlags)0;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000032DF File Offset: 0x000014DF
		public static bool HasAll(this GeneralBitFlags target, GeneralBitFlags flags)
		{
			return (target & flags) == flags;
		}
	}
}
