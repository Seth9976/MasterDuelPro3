using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000018 RID: 24
	public static class ZipEntryExtensions
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003E48 File Offset: 0x00002048
		public static bool HasFlag(this ZipEntry entry, GeneralBitFlags flag)
		{
			return (entry.Flags & (int)flag) != 0;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003E55 File Offset: 0x00002055
		public static void SetFlag(this ZipEntry entry, GeneralBitFlags flag, bool enabled = true)
		{
			entry.Flags = (enabled ? (entry.Flags | (int)flag) : (entry.Flags & (int)(~(int)flag)));
		}
	}
}
