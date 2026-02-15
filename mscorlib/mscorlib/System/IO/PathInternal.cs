using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x0200079E RID: 1950
	internal static class PathInternal
	{
		// Token: 0x06003D95 RID: 15765 RVA: 0x000ED574 File Offset: 0x000EB774
		internal static bool IsValidDriveChar(char value)
		{
			return (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z');
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000ED594 File Offset: 0x000EB794
		internal static bool EndsWithPeriodOrSpace(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			char c = path[path.Length - 1];
			return c == ' ' || c == '.';
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x000ED5C6 File Offset: 0x000EB7C6
		internal static string EnsureExtendedPrefixIfNeeded(string path)
		{
			if (path != null && (path.Length >= 260 || PathInternal.EndsWithPeriodOrSpace(path)))
			{
				return PathInternal.EnsureExtendedPrefix(path);
			}
			return path;
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x000ED5E8 File Offset: 0x000EB7E8
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

		// Token: 0x06003D99 RID: 15769 RVA: 0x000ED628 File Offset: 0x000EB828
		internal unsafe static bool IsDevice(ReadOnlySpan<char> path)
		{
			return PathInternal.IsExtended(path) || (path.Length >= 4 && PathInternal.IsDirectorySeparator((char)(*path[0])) && PathInternal.IsDirectorySeparator((char)(*path[1])) && (*path[2] == 46 || *path[2] == 63) && PathInternal.IsDirectorySeparator((char)(*path[3])));
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x000ED694 File Offset: 0x000EB894
		internal unsafe static bool IsDeviceUNC(ReadOnlySpan<char> path)
		{
			return path.Length >= 8 && PathInternal.IsDevice(path) && PathInternal.IsDirectorySeparator((char)(*path[7])) && *path[4] == 85 && *path[5] == 78 && *path[6] == 67;
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x000ED6EC File Offset: 0x000EB8EC
		internal unsafe static bool IsExtended(ReadOnlySpan<char> path)
		{
			return path.Length >= 4 && *path[0] == 92 && (*path[1] == 92 || *path[1] == 63) && *path[2] == 63 && *path[3] == 92;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x000ED748 File Offset: 0x000EB948
		internal unsafe static int GetRootLength(ReadOnlySpan<char> path)
		{
			int length = path.Length;
			int i = 0;
			bool flag = PathInternal.IsDevice(path);
			bool flag2 = flag && PathInternal.IsDeviceUNC(path);
			if ((!flag || flag2) && length > 0 && PathInternal.IsDirectorySeparator((char)(*path[0])))
			{
				if (flag2 || (length > 1 && PathInternal.IsDirectorySeparator((char)(*path[1]))))
				{
					i = (flag2 ? 8 : 2);
					int num = 2;
					while (i < length)
					{
						if (PathInternal.IsDirectorySeparator((char)(*path[i])) && --num <= 0)
						{
							break;
						}
						i++;
					}
				}
				else
				{
					i = 1;
				}
			}
			else if (flag)
			{
				i = 4;
				while (i < length && !PathInternal.IsDirectorySeparator((char)(*path[i])))
				{
					i++;
				}
				if (i < length && i > 4 && PathInternal.IsDirectorySeparator((char)(*path[i])))
				{
					i++;
				}
			}
			else if (length >= 2 && *path[1] == 58 && PathInternal.IsValidDriveChar((char)(*path[0])))
			{
				i = 2;
				if (length > 2 && PathInternal.IsDirectorySeparator((char)(*path[2])))
				{
					i++;
				}
			}
			return i;
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x000ED857 File Offset: 0x000EBA57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsDirectorySeparator(char c)
		{
			return c == '\\' || c == '/';
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x000ED868 File Offset: 0x000EBA68
		internal unsafe static bool IsEffectivelyEmpty(ReadOnlySpan<char> path)
		{
			if (path.IsEmpty)
			{
				return true;
			}
			ReadOnlySpan<char> readOnlySpan = path;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				if (*readOnlySpan[i] != 32)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x000ED8A4 File Offset: 0x000EBAA4
		internal unsafe static bool EndsInDirectorySeparator(ReadOnlySpan<char> path)
		{
			return path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[path.Length - 1]));
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x000ED8C8 File Offset: 0x000EBAC8
		internal unsafe static bool StartsWithDirectorySeparator(ReadOnlySpan<char> path)
		{
			return path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[0]));
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000ED8E4 File Offset: 0x000EBAE4
		internal static string EnsureTrailingSeparator(string path)
		{
			if (!PathInternal.EndsInDirectorySeparator(path))
			{
				return path + "\\";
			}
			return path;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x000ED900 File Offset: 0x000EBB00
		internal static string TrimEndingDirectorySeparator(string path)
		{
			if (!PathInternal.EndsInDirectorySeparator(path) || PathInternal.IsRoot(path))
			{
				return path;
			}
			return path.Substring(0, path.Length - 1);
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x000ED92D File Offset: 0x000EBB2D
		internal static ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path)
		{
			if (!PathInternal.EndsInDirectorySeparator(path) || PathInternal.IsRoot(path))
			{
				return path;
			}
			return path.Slice(0, path.Length - 1);
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x000ED952 File Offset: 0x000EBB52
		internal static bool IsRoot(ReadOnlySpan<char> path)
		{
			return path.Length == PathInternal.GetRootLength(path);
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x000ED964 File Offset: 0x000EBB64
		internal static int GetCommonPathLength(string first, string second, bool ignoreCase)
		{
			int num = PathInternal.EqualStartingCharacterCount(first, second, ignoreCase);
			if (num == 0)
			{
				return num;
			}
			if (num == first.Length && (num == second.Length || PathInternal.IsDirectorySeparator(second[num])))
			{
				return num;
			}
			if (num == second.Length && PathInternal.IsDirectorySeparator(first[num]))
			{
				return num;
			}
			while (num > 0 && !PathInternal.IsDirectorySeparator(first[num - 1]))
			{
				num--;
			}
			return num;
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x000ED9D4 File Offset: 0x000EBBD4
		internal unsafe static int EqualStartingCharacterCount(string first, string second, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second))
			{
				return 0;
			}
			int num = 0;
			fixed (string text = first)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = second)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					char* ptr5 = ptr3 + first.Length;
					char* ptr6 = ptr4 + second.Length;
					while (ptr3 != ptr5 && ptr4 != ptr6 && (*ptr3 == *ptr4 || (ignoreCase && char.ToUpperInvariant(*ptr3) == char.ToUpperInvariant(*ptr4))))
					{
						num++;
						ptr3++;
						ptr4++;
					}
				}
			}
			return num;
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x000EDA7C File Offset: 0x000EBC7C
		internal static bool AreRootsEqual(string first, string second, StringComparison comparisonType)
		{
			int rootLength = PathInternal.GetRootLength(first);
			int rootLength2 = PathInternal.GetRootLength(second);
			return rootLength == rootLength2 && string.Compare(first, 0, second, 0, rootLength, comparisonType) == 0;
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x000EDAB5 File Offset: 0x000EBCB5
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

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x000EDAC1 File Offset: 0x000EBCC1
		internal static bool IsCaseSensitive
		{
			get
			{
				return PathInternal.s_isCaseSensitive;
			}
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000EDAC8 File Offset: 0x000EBCC8
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

		// Token: 0x06003DAB RID: 15787 RVA: 0x00033991 File Offset: 0x00031B91
		public static bool IsPartiallyQualified(string path)
		{
			return false;
		}

		// Token: 0x04001F9F RID: 8095
		private static readonly bool s_isCaseSensitive = PathInternal.GetIsCaseSensitive();
	}
}
