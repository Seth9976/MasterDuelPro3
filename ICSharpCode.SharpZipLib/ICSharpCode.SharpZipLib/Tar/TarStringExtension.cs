using System;
using System.IO;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200008A RID: 138
	internal static class TarStringExtension
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x000175DE File Offset: 0x000157DE
		public static string ToTarArchivePath(this string s)
		{
			return PathUtils.DropPathRoot(s).Replace(Path.DirectorySeparatorChar, '/');
		}
	}
}
