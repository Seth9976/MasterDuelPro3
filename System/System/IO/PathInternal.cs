using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x02000339 RID: 825
	internal static class PathInternal
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x00058EAF File Offset: 0x000570AF
		internal static StringComparison StringComparison
		{
			get
			{
				if (!PathInternal.s_isCaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00058EBB File Offset: 0x000570BB
		internal static bool IsCaseSensitive
		{
			get
			{
				return PathInternal.s_isCaseSensitive;
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00058EC4 File Offset: 0x000570C4
		private static bool GetIsCaseSensitive()
		{
			bool flag;
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "CASESENSITIVETEST" + Guid.NewGuid().ToString("N"));
				using (new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose))
				{
					flag = !File.Exists(text.ToLowerInvariant());
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00058F4C File Offset: 0x0005714C
		internal static bool IsValidDriveChar(char value)
		{
			return (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z');
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00058F6C File Offset: 0x0005716C
		private static bool EndsWithPeriodOrSpace(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			char c = path[path.Length - 1];
			return c == ' ' || c == '.';
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00058F9E File Offset: 0x0005719E
		internal static string EnsureExtendedPrefixIfNeeded(string path)
		{
			if (path != null && (path.Length >= 260 || PathInternal.EndsWithPeriodOrSpace(path)))
			{
				return PathInternal.EnsureExtendedPrefix(path);
			}
			return path;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00058FC0 File Offset: 0x000571C0
		internal static string EnsureExtendedPrefix(string path)
		{
			if (PathInternal.IsPartiallyQualified(path) || PathInternal.IsDevice(path))
			{
				return path;
			}
			if (path.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
			{
				return path.Insert(2, "?\\UNC\\");
			}
			return "\\\\?\\" + path;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00058FFC File Offset: 0x000571FC
		internal static bool IsDevice(string path)
		{
			return PathInternal.IsExtended(path) || (path.Length >= 4 && PathInternal.IsDirectorySeparator(path[0]) && PathInternal.IsDirectorySeparator(path[1]) && (path[2] == '.' || path[2] == '?') && PathInternal.IsDirectorySeparator(path[3]));
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0005905C File Offset: 0x0005725C
		internal static bool IsExtended(string path)
		{
			return path.Length >= 4 && path[0] == '\\' && (path[1] == '\\' || path[1] == '?') && path[2] == '?' && path[3] == '\\';
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000590AC File Offset: 0x000572AC
		internal static bool IsPartiallyQualified(string path)
		{
			if (path.Length < 2)
			{
				return true;
			}
			if (PathInternal.IsDirectorySeparator(path[0]))
			{
				return path[1] != '?' && !PathInternal.IsDirectorySeparator(path[1]);
			}
			return path.Length < 3 || path[1] != Path.VolumeSeparatorChar || !PathInternal.IsDirectorySeparator(path[2]) || !PathInternal.IsValidDriveChar(path[0]);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00059125 File Offset: 0x00057325
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsDirectorySeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x04000C0F RID: 3087
		private static readonly bool s_isCaseSensitive = PathInternal.GetIsCaseSensitive();
	}
}
