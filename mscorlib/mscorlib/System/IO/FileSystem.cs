using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	// Token: 0x02000792 RID: 1938
	internal static class FileSystem
	{
		// Token: 0x06003D28 RID: 15656 RVA: 0x000EB858 File Offset: 0x000E9A58
		public static void CopyFile(string sourceFullPath, string destFullPath, bool overwrite)
		{
			int num = FileSystem.UnityCopyFile(sourceFullPath, destFullPath, !overwrite);
			if (num != 0)
			{
				string text = destFullPath;
				if (num != 80)
				{
					using (SafeFileHandle safeFileHandle = Interop.Kernel32.CreateFile(sourceFullPath, int.MinValue, FileShare.Read, FileMode.Open, 0))
					{
						if (safeFileHandle.IsInvalid)
						{
							text = sourceFullPath;
						}
					}
					if (num == 5 && FileSystem.DirectoryExists(destFullPath))
					{
						throw new UnauthorizedAccessException(SR.Format("The target file '{0}' is a directory, not a file.", destFullPath));
					}
				}
				throw Win32Marshal.GetExceptionForWin32Error(num, text);
			}
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x000EB8D8 File Offset: 0x000E9AD8
		public static void CreateDirectory(string fullPath)
		{
			if (FileSystem.DirectoryExists(fullPath))
			{
				return;
			}
			List<string> list = new List<string>();
			bool flag = false;
			int num = fullPath.Length;
			if (num >= 2 && PathInternal.EndsInDirectorySeparator(fullPath))
			{
				num--;
			}
			int rootLength = PathInternal.GetRootLength(fullPath);
			if (num > rootLength)
			{
				int num2 = num - 1;
				while (num2 >= rootLength && !flag)
				{
					string text = fullPath.Substring(0, num2 + 1);
					if (!FileSystem.DirectoryExists(text))
					{
						list.Add(text);
					}
					else
					{
						flag = true;
					}
					while (num2 > rootLength && !PathInternal.IsDirectorySeparator(fullPath[num2]))
					{
						num2--;
					}
					num2--;
				}
			}
			int count = list.Count;
			bool flag2 = true;
			int num3 = 0;
			string text2 = fullPath;
			while (list.Count > 0)
			{
				string text3 = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
				flag2 = FileSystem.UnityCreateDirectory(text3);
				if (!flag2 && num3 == 0)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 183)
					{
						num3 = lastWin32Error;
					}
					else if (FileSystem.FileExists(text3) || (!FileSystem.DirectoryExists(text3, out lastWin32Error) && lastWin32Error == 5))
					{
						num3 = lastWin32Error;
						text2 = text3;
					}
				}
			}
			if (count == 0 && !flag)
			{
				string text4 = Directory.InternalGetDirectoryRoot(fullPath);
				if (!FileSystem.DirectoryExists(text4))
				{
					throw Win32Marshal.GetExceptionForWin32Error(3, text4);
				}
				return;
			}
			else
			{
				if (!flag2 && num3 != 0)
				{
					throw Win32Marshal.GetExceptionForWin32Error(num3, text2);
				}
				return;
			}
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x000EBA2C File Offset: 0x000E9C2C
		public static void DeleteFile(string fullPath)
		{
			if (FileSystem.UnityDeleteFile(fullPath))
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 2)
			{
				return;
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, fullPath);
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x000EBA54 File Offset: 0x000E9C54
		public static bool DirectoryExists(string fullPath)
		{
			int num;
			return FileSystem.DirectoryExists(fullPath, out num);
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000EBA6C File Offset: 0x000E9C6C
		private static bool DirectoryExists(string path, out int lastError)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			lastError = FileSystem.FillAttributeInfo(path, ref win32_FILE_ATTRIBUTE_DATA, true);
			return lastError == 0 && win32_FILE_ATTRIBUTE_DATA.dwFileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.dwFileAttributes & 16) != 0;
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x000EBAA8 File Offset: 0x000E9CA8
		internal static int FillAttributeInfo(string path, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA data, bool returnErrorOnNotFound)
		{
			int num = 0;
			path = PathInternal.TrimEndingDirectorySeparator(path);
			using (DisableMediaInsertionPrompt.Create())
			{
				if (!FileSystem.UnityGetFileAttributesEx(path, ref data))
				{
					num = Marshal.GetLastWin32Error();
					if (num != 2 && num != 3 && num != 21 && num != 123 && num != 161 && num != 53 && num != 67 && num != 87 && num != 1231)
					{
						Interop.Kernel32.WIN32_FIND_DATA win32_FIND_DATA = default(Interop.Kernel32.WIN32_FIND_DATA);
						using (SafeFindHandle safeFindHandle = FileSystem.UnityFindFirstFile(path, ref win32_FIND_DATA))
						{
							if (safeFindHandle.IsInvalid)
							{
								num = Marshal.GetLastWin32Error();
							}
							else
							{
								num = 0;
								data.PopulateFrom(ref win32_FIND_DATA);
							}
						}
					}
				}
			}
			if (num != 0 && !returnErrorOnNotFound && (num - 2 <= 1 || num == 21))
			{
				data.dwFileAttributes = -1;
				return 0;
			}
			return num;
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000EBB84 File Offset: 0x000E9D84
		public static bool FileExists(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			return FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, true) == 0 && win32_FILE_ATTRIBUTE_DATA.dwFileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.dwFileAttributes & 16) == 0;
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x000EBBBC File Offset: 0x000E9DBC
		public static FileAttributes GetAttributes(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			int num = FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, true);
			if (num != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return (FileAttributes)win32_FILE_ATTRIBUTE_DATA.dwFileAttributes;
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x000EBBEC File Offset: 0x000E9DEC
		public static DateTimeOffset GetCreationTime(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			int num = FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, false);
			if (num != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return win32_FILE_ATTRIBUTE_DATA.ftCreationTime.ToDateTimeOffset();
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000EBC24 File Offset: 0x000E9E24
		public static DateTimeOffset GetLastAccessTime(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			int num = FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, false);
			if (num != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return win32_FILE_ATTRIBUTE_DATA.ftLastAccessTime.ToDateTimeOffset();
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000EBC5C File Offset: 0x000E9E5C
		public static DateTimeOffset GetLastWriteTime(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			int num = FileSystem.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, false);
			if (num != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return win32_FILE_ATTRIBUTE_DATA.ftLastWriteTime.ToDateTimeOffset();
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000EBC94 File Offset: 0x000E9E94
		public static void MoveDirectory(string sourceFullPath, string destFullPath)
		{
			if (FileSystem.UnityMoveFile(sourceFullPath, destFullPath))
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 2)
			{
				throw Win32Marshal.GetExceptionForWin32Error(3, sourceFullPath);
			}
			if (lastWin32Error == 5)
			{
				throw new IOException(SR.Format("Access to the path '{0}' is denied.", sourceFullPath), Win32Marshal.MakeHRFromErrorCode(lastWin32Error));
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000EBCE3 File Offset: 0x000E9EE3
		public static void MoveFile(string sourceFullPath, string destFullPath)
		{
			if (!FileSystem.UnityMoveFile(sourceFullPath, destFullPath))
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000EBCFC File Offset: 0x000E9EFC
		private static SafeFileHandle OpenHandle(string fullPath, bool asDirectory)
		{
			string text = fullPath.Substring(0, PathInternal.GetRootLength(fullPath));
			if (text == fullPath && text[1] == Path.VolumeSeparatorChar)
			{
				throw new ArgumentException("Path must not be a drive.", "path");
			}
			SafeFileHandle safeFileHandle = Interop.Kernel32.CreateFile(fullPath, 1073741824, FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open, asDirectory ? 33554432 : 0);
			if (safeFileHandle.IsInvalid)
			{
				int num = Marshal.GetLastWin32Error();
				if (!asDirectory && num == 3 && fullPath.Equals(Directory.GetDirectoryRoot(fullPath)))
				{
					num = 5;
				}
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
			return safeFileHandle;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000EBD88 File Offset: 0x000E9F88
		public static void RemoveDirectory(string fullPath, bool recursive)
		{
			if (!recursive)
			{
				FileSystem.RemoveDirectoryInternal(fullPath, true, false);
				return;
			}
			Interop.Kernel32.WIN32_FIND_DATA win32_FIND_DATA = default(Interop.Kernel32.WIN32_FIND_DATA);
			FileSystem.GetFindData(fullPath, ref win32_FIND_DATA);
			if (FileSystem.IsNameSurrogateReparsePoint(ref win32_FIND_DATA))
			{
				FileSystem.RemoveDirectoryInternal(fullPath, true, false);
				return;
			}
			fullPath = PathInternal.EnsureExtendedPrefix(fullPath);
			FileSystem.RemoveDirectoryRecursive(fullPath, ref win32_FIND_DATA, true);
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000EBDD4 File Offset: 0x000E9FD4
		private static void GetFindData(string fullPath, ref Interop.Kernel32.WIN32_FIND_DATA findData)
		{
			using (SafeFindHandle safeFindHandle = FileSystem.UnityFindFirstFile(PathInternal.TrimEndingDirectorySeparator(fullPath), ref findData))
			{
				if (safeFindHandle.IsInvalid)
				{
					int num = Marshal.GetLastWin32Error();
					if (num == 2)
					{
						num = 3;
					}
					throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
				}
			}
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000EBE28 File Offset: 0x000EA028
		private static bool IsNameSurrogateReparsePoint(ref Interop.Kernel32.WIN32_FIND_DATA data)
		{
			return (data.dwFileAttributes & 1024U) != 0U && (data.dwReserved0 & 536870912U) > 0U;
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x000EBE4C File Offset: 0x000EA04C
		private static void RemoveDirectoryRecursive(string fullPath, ref Interop.Kernel32.WIN32_FIND_DATA findData, bool topLevel)
		{
			Exception ex = null;
			using (SafeFindHandle safeFindHandle = FileSystem.UnityFindFirstFile(Path.Join(fullPath, "*"), ref findData))
			{
				if (safeFindHandle.IsInvalid)
				{
					throw Win32Marshal.GetExceptionForLastWin32Error(fullPath);
				}
				int num;
				do
				{
					if ((findData.dwFileAttributes & 16U) == 0U)
					{
						string stringFromFixedBuffer = findData.cFileName.GetStringFromFixedBuffer();
						if (!FileSystem.UnityDeleteFile(Path.Combine(fullPath, stringFromFixedBuffer)) && ex == null)
						{
							num = Marshal.GetLastWin32Error();
							if (num != 2)
							{
								ex = Win32Marshal.GetExceptionForWin32Error(num, stringFromFixedBuffer);
							}
						}
					}
					else if (!findData.cFileName.FixedBufferEqualsString(".") && !findData.cFileName.FixedBufferEqualsString(".."))
					{
						string stringFromFixedBuffer2 = findData.cFileName.GetStringFromFixedBuffer();
						if (!FileSystem.IsNameSurrogateReparsePoint(ref findData))
						{
							try
							{
								FileSystem.RemoveDirectoryRecursive(Path.Combine(fullPath, stringFromFixedBuffer2), ref findData, false);
								goto IL_013D;
							}
							catch (Exception ex2)
							{
								if (ex == null)
								{
									ex = ex2;
								}
								goto IL_013D;
							}
						}
						if (findData.dwReserved0 == 2684354563U && !Interop.Kernel32.DeleteVolumeMountPoint(Path.Join(fullPath, stringFromFixedBuffer2, "\\")) && ex == null)
						{
							num = Marshal.GetLastWin32Error();
							if (num != 0 && num != 3)
							{
								ex = Win32Marshal.GetExceptionForWin32Error(num, stringFromFixedBuffer2);
							}
						}
						if (!FileSystem.UnityRemoveDirectory(Path.Combine(fullPath, stringFromFixedBuffer2)) && ex == null)
						{
							num = Marshal.GetLastWin32Error();
							if (num != 3)
							{
								ex = Win32Marshal.GetExceptionForWin32Error(num, stringFromFixedBuffer2);
							}
						}
					}
					IL_013D:;
				}
				while (FileSystem.UnityFindNextFile(safeFindHandle, ref findData));
				if (ex != null)
				{
					throw ex;
				}
				num = Marshal.GetLastWin32Error();
				if (num != 0 && num != 18)
				{
					throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
				}
			}
			FileSystem.RemoveDirectoryInternal(fullPath, topLevel, true);
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x000EC008 File Offset: 0x000EA208
		private static void RemoveDirectoryInternal(string fullPath, bool topLevel, bool allowDirectoryNotEmpty = false)
		{
			if (!FileSystem.UnityRemoveDirectory(fullPath))
			{
				int num = Marshal.GetLastWin32Error();
				switch (num)
				{
				case 2:
					num = 3;
					break;
				case 3:
					break;
				case 4:
					goto IL_004B;
				case 5:
					throw new IOException(SR.Format("Access to the path '{0}' is denied.", fullPath));
				default:
					if (num != 145)
					{
						goto IL_004B;
					}
					if (allowDirectoryNotEmpty)
					{
						return;
					}
					goto IL_004B;
				}
				if (!topLevel)
				{
					return;
				}
				IL_004B:
				throw Win32Marshal.GetExceptionForWin32Error(num, fullPath);
			}
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000EC068 File Offset: 0x000EA268
		public static void SetAttributes(string fullPath, FileAttributes attributes)
		{
			if (FileSystem.UnitySetFileAttributes(fullPath, attributes))
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 87)
			{
				throw new ArgumentException("Invalid File or Directory attributes value.", "attributes");
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, fullPath);
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000EC094 File Offset: 0x000EA294
		public static void SetCreationTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			using (SafeFileHandle safeFileHandle = FileSystem.OpenHandle(fullPath, asDirectory))
			{
				if (!Interop.Kernel32.SetFileTime(safeFileHandle, time.ToFileTime(), -1L, -1L, -1L, 0U))
				{
					throw Win32Marshal.GetExceptionForLastWin32Error(fullPath);
				}
			}
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000EC0E4 File Offset: 0x000EA2E4
		public static void SetLastAccessTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			using (SafeFileHandle safeFileHandle = FileSystem.OpenHandle(fullPath, asDirectory))
			{
				if (!Interop.Kernel32.SetFileTime(safeFileHandle, -1L, time.ToFileTime(), -1L, -1L, 0U))
				{
					throw Win32Marshal.GetExceptionForLastWin32Error(fullPath);
				}
			}
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x000EC134 File Offset: 0x000EA334
		public static void SetLastWriteTime(string fullPath, DateTimeOffset time, bool asDirectory)
		{
			using (SafeFileHandle safeFileHandle = FileSystem.OpenHandle(fullPath, asDirectory))
			{
				if (!Interop.Kernel32.SetFileTime(safeFileHandle, -1L, -1L, time.ToFileTime(), -1L, 0U))
				{
					throw Win32Marshal.GetExceptionForLastWin32Error(fullPath);
				}
			}
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x000EC184 File Offset: 0x000EA384
		public static string[] GetLogicalDrives()
		{
			return DriveInfoInternal.GetLogicalDrives();
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x000EC18C File Offset: 0x000EA38C
		private static bool UnityCreateDirectory(string name)
		{
			Interop.Kernel32.SECURITY_ATTRIBUTES security_ATTRIBUTES = default(Interop.Kernel32.SECURITY_ATTRIBUTES);
			return Interop.Kernel32.CreateDirectory(name, ref security_ATTRIBUTES);
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x000EC1A9 File Offset: 0x000EA3A9
		private static bool UnityRemoveDirectory(string fullPath)
		{
			return Interop.Kernel32.RemoveDirectory(fullPath);
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000EC1B4 File Offset: 0x000EA3B4
		private static bool UnityGetFileAttributesEx(string path, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA data)
		{
			if ((path.StartsWith("\\?\\") || path.StartsWith("\\\\?\\")) && path.Contains("GLOBALROOT\\Device\\Harddisk") && path.Length - path.IndexOf("Partition") <= 11 && path[path.Length - 1] != '\\')
			{
				path += "\\";
			}
			return Interop.Kernel32.GetFileAttributesEx(path, Interop.Kernel32.GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, ref data);
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000EC225 File Offset: 0x000EA425
		private static bool UnitySetFileAttributes(string fullPath, FileAttributes attributes)
		{
			return Interop.Kernel32.SetFileAttributes(fullPath, (int)attributes);
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000EC22E File Offset: 0x000EA42E
		private static int UnityCopyFile(string sourceFullPath, string destFullPath, bool failIfExists)
		{
			return Interop.Kernel32.CopyFile(sourceFullPath, destFullPath, failIfExists);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x000EC238 File Offset: 0x000EA438
		private static bool UnityDeleteFile(string path)
		{
			return Interop.Kernel32.DeleteFile(path);
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000EC240 File Offset: 0x000EA440
		private static bool UnityMoveFile(string sourceFullPath, string destFullPath)
		{
			return Interop.Kernel32.MoveFile(sourceFullPath, destFullPath);
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x000EC249 File Offset: 0x000EA449
		private static SafeFindHandle UnityFindFirstFile(string path, ref Interop.Kernel32.WIN32_FIND_DATA findData)
		{
			return Interop.Kernel32.FindFirstFile(path, ref findData);
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x000EC254 File Offset: 0x000EA454
		private static bool UnityFindNextFile(SafeFindHandle handle, ref Interop.Kernel32.WIN32_FIND_DATA findData)
		{
			bool flag = false;
			bool flag2 = false;
			if (!flag)
			{
				flag2 = Interop.Kernel32.FindNextFile(handle, ref findData);
			}
			return flag2;
		}
	}
}
