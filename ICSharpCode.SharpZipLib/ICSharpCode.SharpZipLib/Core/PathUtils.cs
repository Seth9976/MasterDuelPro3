using System;
using System.IO;
using System.Linq;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000BC RID: 188
	public static class PathUtils
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		public static string DropPathRoot(string path)
		{
			char[] invalidChars = Path.GetInvalidPathChars();
			bool cleanRootSep = path.Length >= 3 && path[1] == ':' && path[2] == ':';
			int num = Path.GetPathRoot(new string(path.Take(258).Select(delegate(char c, int i)
			{
				if (!invalidChars.Contains(c) && !((i == 2) & cleanRootSep))
				{
					return c;
				}
				return '_';
			}).ToArray<char>())).Length;
			while (path.Length > num && (path[num] == '/' || path[num] == '\\'))
			{
				num++;
			}
			return path.Substring(num);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001AE54 File Offset: 0x00019054
		public static string GetTempFileName(string original = null)
		{
			string tempPath = Path.GetTempPath();
			string text;
			do
			{
				text = ((original == null) ? Path.Combine(tempPath, Path.GetRandomFileName()) : (original + "." + Path.GetRandomFileName()));
			}
			while (File.Exists(text));
			return text;
		}
	}
}
