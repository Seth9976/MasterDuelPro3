using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000011 RID: 17
	public static class GeneralBitFlagsExtensions
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000032CF File Offset: 0x000014CF
		public static bool Includes(this GeneralBitFlags flagData, GeneralBitFlags flag)
		{
			return (flag & flagData) > (GeneralBitFlags)0;
		}
	}
}
