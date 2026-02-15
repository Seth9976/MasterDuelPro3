using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Better.StreamingAssets
{
	// Token: 0x02000007 RID: 7
	public static class PathUtil
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000242F File Offset: 0x0000062F
		public static bool IsDirectorySeparator(char c)
		{
			return c == '/' || c == '\\';
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002440 File Offset: 0x00000640
		public static string FixTrailingDirectorySeparators(string path)
		{
			if (path.Length >= 2)
			{
				char lastChar = path[path.Length - 1];
				char prevChar = path[path.Length - 2];
				if (PathUtil.IsDirectorySeparator(lastChar) && PathUtil.IsDirectorySeparator(prevChar))
				{
					return path.TrimEnd(new char[] { '\\', '/' }) + lastChar.ToString();
				}
			}
			return path;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A8 File Offset: 0x000006A8
		public static string CombineSlash(string a, string b)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a");
			}
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			if (string.IsNullOrEmpty(b))
			{
				return a;
			}
			if (string.IsNullOrEmpty(a))
			{
				return b;
			}
			if (b[0] == '/')
			{
				return b;
			}
			if (PathUtil.IsDirectorySeparator(a[a.Length - 1]))
			{
				return a + b;
			}
			return a + "/" + b;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000251C File Offset: 0x0000071C
		public static string NormalizeRelativePath(string relative, bool forceTrailingSlash = false)
		{
			if (string.IsNullOrEmpty(relative))
			{
				throw new ArgumentException("Empty or null", "relative");
			}
			StringBuilder output = new StringBuilder(relative.Length);
			PathUtil.NormalizeState state = PathUtil.NormalizeState.PrevSlash;
			output.Append('/');
			int startIndex = 0;
			int lastIndexPlus = relative.Length;
			if (relative[0] == '"' && relative.Length > 2 && relative[relative.Length - 1] == '"')
			{
				startIndex++;
				lastIndexPlus--;
			}
			for (int i = startIndex; i <= lastIndexPlus; i++)
			{
				if (i == lastIndexPlus || relative[i] == '/' || relative[i] == '\\')
				{
					if (state != PathUtil.NormalizeState.PrevSlash && state != PathUtil.NormalizeState.PrevDot)
					{
						if (state == PathUtil.NormalizeState.PrevDoubleDot)
						{
							if (output.Length == 1)
							{
								throw new IOException("Invalid path: double dot error (before " + i.ToString() + ")");
							}
							int j = output.Length - 2;
							while (j >= 0 && output[j] != '/')
							{
								j--;
							}
							output.Remove(j + 1, output.Length - j - 1);
						}
						else if (i < lastIndexPlus || forceTrailingSlash)
						{
							output.Append('/');
						}
					}
					state = PathUtil.NormalizeState.PrevSlash;
				}
				else if (relative[i] == '.')
				{
					if (state == PathUtil.NormalizeState.PrevSlash)
					{
						state = PathUtil.NormalizeState.PrevDot;
					}
					else if (state == PathUtil.NormalizeState.PrevDot)
					{
						state = PathUtil.NormalizeState.PrevDoubleDot;
					}
					else if (state == PathUtil.NormalizeState.PrevDoubleDot)
					{
						state = PathUtil.NormalizeState.NothingSpecial;
						output.Append("...");
					}
					else
					{
						output.Append('.');
					}
				}
				else
				{
					if (state == PathUtil.NormalizeState.PrevDot)
					{
						output.Append('.');
					}
					else if (state == PathUtil.NormalizeState.PrevDoubleDot)
					{
						output.Append("..");
					}
					if (!PathUtil.IsValidCharacter(relative[i]))
					{
						throw new IOException("Invalid characters");
					}
					output.Append(relative[i]);
					state = PathUtil.NormalizeState.NothingSpecial;
				}
			}
			return output.ToString();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026D3 File Offset: 0x000008D3
		public static bool IsValidCharacter(char c)
		{
			return c != '"' && c != '<' && c != '>' && c != '|' && c >= ' ' && c != ':' && c != '*' && c != '?';
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002700 File Offset: 0x00000900
		public static Regex WildcardToRegex(string pattern)
		{
			return new Regex("^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$", RegexOptions.IgnoreCase);
		}

		// Token: 0x02000008 RID: 8
		private enum NormalizeState
		{
			// Token: 0x0400000D RID: 13
			PrevSlash,
			// Token: 0x0400000E RID: 14
			PrevDot,
			// Token: 0x0400000F RID: 15
			PrevDoubleDot,
			// Token: 0x04000010 RID: 16
			NothingSpecial
		}
	}
}
