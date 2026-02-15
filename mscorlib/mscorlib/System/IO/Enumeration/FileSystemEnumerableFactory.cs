using System;
using System.Collections.Generic;

namespace System.IO.Enumeration
{
	// Token: 0x020007ED RID: 2029
	internal static class FileSystemEnumerableFactory
	{
		// Token: 0x06004148 RID: 16712 RVA: 0x000FBBD8 File Offset: 0x000F9DD8
		internal static void NormalizeInputs(ref string directory, ref string expression, EnumerationOptions options)
		{
			if (Path.IsPathRooted(expression))
			{
				throw new ArgumentException("Second path fragment must not be a drive or UNC name.", "expression");
			}
			ReadOnlySpan<char> directoryName = Path.GetDirectoryName(expression.AsSpan());
			if (directoryName.Length != 0)
			{
				directory = Path.Join(directory, directoryName);
				expression = expression.Substring(directoryName.Length + 1);
			}
			MatchType matchType = options.MatchType;
			if (matchType == MatchType.Simple)
			{
				return;
			}
			if (matchType != MatchType.Win32)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if (string.IsNullOrEmpty(expression) || expression == "." || expression == "*.*")
			{
				expression = "*";
				return;
			}
			if (Path.DirectorySeparatorChar != '\\' && expression.IndexOfAny(FileSystemEnumerableFactory.s_unixEscapeChars) != -1)
			{
				expression = expression.Replace("\\", "\\\\");
				expression = expression.Replace("\"", "\\\"");
				expression = expression.Replace(">", "\\>");
				expression = expression.Replace("<", "\\<");
			}
			expression = FileSystemName.TranslateWin32Expression(expression);
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x000FBCF0 File Offset: 0x000F9EF0
		private static bool MatchesPattern(string expression, ReadOnlySpan<char> name, EnumerationOptions options)
		{
			bool flag = (options.MatchCasing == MatchCasing.PlatformDefault && !PathInternal.IsCaseSensitive) || options.MatchCasing == MatchCasing.CaseInsensitive;
			MatchType matchType = options.MatchType;
			if (matchType == MatchType.Simple)
			{
				return FileSystemName.MatchesSimpleExpression(expression, name, flag);
			}
			if (matchType != MatchType.Win32)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			return FileSystemName.MatchesWin32Expression(expression, name, flag);
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x000FBD50 File Offset: 0x000F9F50
		internal static IEnumerable<string> UserFiles(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000FBDB0 File Offset: 0x000F9FB0
		internal static IEnumerable<string> UserDirectories(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x000FBE10 File Offset: 0x000FA010
		internal static IEnumerable<string> UserEntries(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x000FBE70 File Offset: 0x000FA070
		internal static IEnumerable<FileInfo> FileInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<FileInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return (FileInfo)entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000FBED0 File Offset: 0x000FA0D0
		internal static IEnumerable<DirectoryInfo> DirectoryInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<DirectoryInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return (DirectoryInfo)entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x000FBF30 File Offset: 0x000FA130
		internal static IEnumerable<FileSystemInfo> FileSystemInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<FileSystemInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x04002130 RID: 8496
		private static readonly char[] s_unixEscapeChars = new char[] { '\\', '"', '<', '>' };
	}
}
