using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000002 RID: 2
internal static class Interop
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	internal unsafe static void GetRandomBytes(byte* buffer, int length)
	{
		Interop.BCrypt.NTSTATUS ntstatus = Interop.BCrypt.BCryptGenRandom(IntPtr.Zero, buffer, length, 2);
		if (ntstatus == Interop.BCrypt.NTSTATUS.STATUS_SUCCESS)
		{
			return;
		}
		if (ntstatus == (Interop.BCrypt.NTSTATUS)3221225495U)
		{
			throw new OutOfMemoryException();
		}
		throw new InvalidOperationException();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002082 File Offset: 0x00000282
	internal static IntPtr MemAlloc(UIntPtr sizeInBytes)
	{
		IntPtr intPtr = Interop.mincore.HeapAlloc(Interop.mincore.GetProcessHeap(), 0U, sizeInBytes);
		if (intPtr == IntPtr.Zero)
		{
			throw new OutOfMemoryException();
		}
		return intPtr;
	}

	// Token: 0x02000003 RID: 3
	internal static class Kernel32
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
		internal static int CopyFile(string src, string dst, bool failIfExists)
		{
			int num = (failIfExists ? 1 : 0);
			int num2 = 0;
			if (!Interop.Kernel32.CopyFileEx(src, dst, IntPtr.Zero, IntPtr.Zero, ref num2, num))
			{
				return Marshal.GetLastWin32Error();
			}
			return 0;
		}

		// Token: 0x06000004 RID: 4
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "DeleteVolumeMountPointW", SetLastError = true)]
		internal static extern bool DeleteVolumeMountPointPrivate(string mountPoint);

		// Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000002D8
		internal static bool DeleteVolumeMountPoint(string mountPoint)
		{
			mountPoint = PathInternal.EnsureExtendedPrefixIfNeeded(mountPoint);
			return Interop.Kernel32.DeleteVolumeMountPointPrivate(mountPoint);
		}

		// Token: 0x06000006 RID: 6
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x06000007 RID: 7
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

		// Token: 0x06000008 RID: 8
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern bool GetFileMUIPath(uint flags, string filePath, [Out] StringBuilder language, ref int languageLength, [Out] StringBuilder fileMuiPath, ref int fileMuiPathLength, ref long enumerator);

		// Token: 0x06000009 RID: 9
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetDynamicTimeZoneInformation(out Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION pTimeZoneInformation);

		// Token: 0x0600000A RID: 10
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetTimeZoneInformation(out Interop.Kernel32.TIME_ZONE_INFORMATION lpTimeZoneInformation);

		// Token: 0x0600000B RID: 11
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600000C RID: 12
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CopyFileExW", SetLastError = true)]
		private static extern bool CopyFileExPrivate(string src, string dst, IntPtr progressRoutine, IntPtr progressData, ref int cancel, int flags);

		// Token: 0x0600000D RID: 13 RVA: 0x000020E8 File Offset: 0x000002E8
		internal static bool CopyFileEx(string src, string dst, IntPtr progressRoutine, IntPtr progressData, ref int cancel, int flags)
		{
			src = PathInternal.EnsureExtendedPrefixIfNeeded(src);
			dst = PathInternal.EnsureExtendedPrefixIfNeeded(dst);
			return Interop.Kernel32.CopyFileExPrivate(src, dst, progressRoutine, progressData, ref cancel, flags);
		}

		// Token: 0x0600000E RID: 14
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateDirectoryW", SetLastError = true)]
		private static extern bool CreateDirectoryPrivate(string path, ref Interop.Kernel32.SECURITY_ATTRIBUTES lpSecurityAttributes);

		// Token: 0x0600000F RID: 15 RVA: 0x00002107 File Offset: 0x00000307
		internal static bool CreateDirectory(string path, ref Interop.Kernel32.SECURITY_ATTRIBUTES lpSecurityAttributes)
		{
			path = PathInternal.EnsureExtendedPrefix(path);
			return Interop.Kernel32.CreateDirectoryPrivate(path, ref lpSecurityAttributes);
		}

		// Token: 0x06000010 RID: 16
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateFileW", ExactSpelling = true, SetLastError = true)]
		private unsafe static extern IntPtr CreateFilePrivate(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, Interop.Kernel32.SECURITY_ATTRIBUTES* securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000011 RID: 17 RVA: 0x00002118 File Offset: 0x00000318
		internal static SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			IntPtr intPtr = Interop.Kernel32.CreateFile_IntPtr(lpFileName, dwDesiredAccess, dwShareMode, dwCreationDisposition, dwFlagsAndAttributes);
			SafeFileHandle safeFileHandle;
			try
			{
				safeFileHandle = new SafeFileHandle(intPtr, true);
			}
			catch
			{
				Interop.Kernel32.CloseHandle(intPtr);
				throw;
			}
			return safeFileHandle;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002158 File Offset: 0x00000358
		internal static IntPtr CreateFile_IntPtr(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			lpFileName = PathInternal.EnsureExtendedPrefixIfNeeded(lpFileName);
			return Interop.Kernel32.CreateFilePrivate(lpFileName, dwDesiredAccess, dwShareMode, null, dwCreationDisposition, dwFlagsAndAttributes, IntPtr.Zero);
		}

		// Token: 0x06000013 RID: 19
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "DeleteFileW", SetLastError = true)]
		private static extern bool DeleteFilePrivate(string path);

		// Token: 0x06000014 RID: 20 RVA: 0x00002174 File Offset: 0x00000374
		internal static bool DeleteFile(string path)
		{
			path = PathInternal.EnsureExtendedPrefixIfNeeded(path);
			return Interop.Kernel32.DeleteFilePrivate(path);
		}

		// Token: 0x06000015 RID: 21
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindFirstFileExW", SetLastError = true)]
		private static extern SafeFindHandle FindFirstFileExPrivate(string lpFileName, Interop.Kernel32.FINDEX_INFO_LEVELS fInfoLevelId, ref Interop.Kernel32.WIN32_FIND_DATA lpFindFileData, Interop.Kernel32.FINDEX_SEARCH_OPS fSearchOp, IntPtr lpSearchFilter, int dwAdditionalFlags);

		// Token: 0x06000016 RID: 22 RVA: 0x00002184 File Offset: 0x00000384
		internal static SafeFindHandle FindFirstFile(string fileName, ref Interop.Kernel32.WIN32_FIND_DATA data)
		{
			fileName = PathInternal.EnsureExtendedPrefixIfNeeded(fileName);
			return Interop.Kernel32.FindFirstFileExPrivate(fileName, Interop.Kernel32.FINDEX_INFO_LEVELS.FindExInfoBasic, ref data, Interop.Kernel32.FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero, 0);
		}

		// Token: 0x06000017 RID: 23
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FindNextFileW", SetLastError = true)]
		internal static extern bool FindNextFile(SafeFindHandle hndFindFile, ref Interop.Kernel32.WIN32_FIND_DATA lpFindFileData);

		// Token: 0x06000018 RID: 24
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Unicode, EntryPoint = "FormatMessageW", SetLastError = true)]
		private unsafe static extern int FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, char* lpBuffer, int nSize, IntPtr[] arguments);

		// Token: 0x06000019 RID: 25 RVA: 0x0000219D File Offset: 0x0000039D
		internal static string GetMessage(int errorCode)
		{
			return Interop.Kernel32.GetMessage(IntPtr.Zero, errorCode);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021AC File Offset: 0x000003AC
		internal unsafe static string GetMessage(IntPtr moduleHandle, int errorCode)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			string text;
			while (!Interop.Kernel32.TryGetErrorMessage(moduleHandle, errorCode, span, out text))
			{
				span = new char[span.Length * 4];
				if (span.Length >= 66560)
				{
					return string.Format("Unknown error (0x{0:x})", errorCode);
				}
			}
			return text;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000220C File Offset: 0x0000040C
		private unsafe static bool TryGetErrorMessage(IntPtr moduleHandle, int errorCode, Span<char> buffer, out string errorMsg)
		{
			int num = 12800;
			if (moduleHandle != IntPtr.Zero)
			{
				num |= 2048;
			}
			int num2;
			fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
			{
				char* ptr = reference;
				num2 = Interop.Kernel32.FormatMessage(num, moduleHandle, (uint)errorCode, 0, ptr, buffer.Length, null);
			}
			if (num2 != 0)
			{
				int i;
				for (i = num2; i > 0; i--)
				{
					char c = *buffer[i - 1];
					if (c > ' ' && c != '.')
					{
						break;
					}
				}
				errorMsg = buffer.Slice(0, i).ToString();
			}
			else
			{
				if (Marshal.GetLastWin32Error() == 122)
				{
					errorMsg = "";
					return false;
				}
				errorMsg = string.Format("Unknown error (0x{0:x})", errorCode);
			}
			return true;
		}

		// Token: 0x0600001C RID: 28
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetFileAttributesExW", SetLastError = true)]
		private static extern bool GetFileAttributesExPrivate(string name, Interop.Kernel32.GET_FILEEX_INFO_LEVELS fileInfoLevel, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

		// Token: 0x0600001D RID: 29 RVA: 0x000022C3 File Offset: 0x000004C3
		internal static bool GetFileAttributesEx(string name, Interop.Kernel32.GET_FILEEX_INFO_LEVELS fileInfoLevel, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA lpFileInformation)
		{
			name = PathInternal.EnsureExtendedPrefixIfNeeded(name);
			return Interop.Kernel32.GetFileAttributesExPrivate(name, fileInfoLevel, ref lpFileInformation);
		}

		// Token: 0x0600001E RID: 30
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetLogicalDrives();

		// Token: 0x0600001F RID: 31
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "MoveFileExW", SetLastError = true)]
		private static extern bool MoveFileExPrivate(string src, string dst, uint flags);

		// Token: 0x06000020 RID: 32 RVA: 0x000022D5 File Offset: 0x000004D5
		internal static bool MoveFile(string src, string dst)
		{
			src = PathInternal.EnsureExtendedPrefixIfNeeded(src);
			dst = PathInternal.EnsureExtendedPrefixIfNeeded(dst);
			return Interop.Kernel32.MoveFileExPrivate(src, dst, 2U);
		}

		// Token: 0x06000021 RID: 33
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RemoveDirectoryW", SetLastError = true)]
		private static extern bool RemoveDirectoryPrivate(string path);

		// Token: 0x06000022 RID: 34 RVA: 0x000022EF File Offset: 0x000004EF
		internal static bool RemoveDirectory(string path)
		{
			path = PathInternal.EnsureExtendedPrefixIfNeeded(path);
			return Interop.Kernel32.RemoveDirectoryPrivate(path);
		}

		// Token: 0x06000023 RID: 35
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "SetFileAttributesW", SetLastError = true)]
		private static extern bool SetFileAttributesPrivate(string name, int attr);

		// Token: 0x06000024 RID: 36 RVA: 0x000022FF File Offset: 0x000004FF
		internal static bool SetFileAttributes(string name, int attr)
		{
			name = PathInternal.EnsureExtendedPrefixIfNeeded(name);
			return Interop.Kernel32.SetFileAttributesPrivate(name, attr);
		}

		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetFileInformationByHandle(SafeFileHandle hFile, Interop.Kernel32.FILE_INFO_BY_HANDLE_CLASS FileInformationClass, ref Interop.Kernel32.FILE_BASIC_INFO lpFileInformation, uint dwBufferSize);

		// Token: 0x06000026 RID: 38 RVA: 0x00002310 File Offset: 0x00000510
		internal unsafe static bool SetFileTime(SafeFileHandle hFile, long creationTime = -1L, long lastAccessTime = -1L, long lastWriteTime = -1L, long changeTime = -1L, uint fileAttributes = 0U)
		{
			Interop.Kernel32.FILE_BASIC_INFO file_BASIC_INFO = new Interop.Kernel32.FILE_BASIC_INFO
			{
				CreationTime = creationTime,
				LastAccessTime = lastAccessTime,
				LastWriteTime = lastWriteTime,
				ChangeTime = changeTime,
				FileAttributes = fileAttributes
			};
			return Interop.Kernel32.SetFileInformationByHandle(hFile, Interop.Kernel32.FILE_INFO_BY_HANDLE_CLASS.FileBasicInfo, ref file_BASIC_INFO, (uint)sizeof(Interop.Kernel32.FILE_BASIC_INFO));
		}

		// Token: 0x06000027 RID: 39
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetThreadErrorMode(uint dwNewMode, out uint lpOldMode);

		// Token: 0x02000004 RID: 4
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WIN32_FIND_DATA
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000028 RID: 40 RVA: 0x00002360 File Offset: 0x00000560
			internal unsafe ReadOnlySpan<char> cFileName
			{
				get
				{
					fixed (char* ptr = &this._cFileName.FixedElementField)
					{
						return new ReadOnlySpan<char>((void*)ptr, 260);
					}
				}
			}

			// Token: 0x04000001 RID: 1
			internal uint dwFileAttributes;

			// Token: 0x04000002 RID: 2
			internal Interop.Kernel32.FILE_TIME ftCreationTime;

			// Token: 0x04000003 RID: 3
			internal Interop.Kernel32.FILE_TIME ftLastAccessTime;

			// Token: 0x04000004 RID: 4
			internal Interop.Kernel32.FILE_TIME ftLastWriteTime;

			// Token: 0x04000005 RID: 5
			internal uint nFileSizeHigh;

			// Token: 0x04000006 RID: 6
			internal uint nFileSizeLow;

			// Token: 0x04000007 RID: 7
			internal uint dwReserved0;

			// Token: 0x04000008 RID: 8
			internal uint dwReserved1;

			// Token: 0x04000009 RID: 9
			[FixedBuffer(typeof(char), 260)]
			private Interop.Kernel32.WIN32_FIND_DATA.<_cFileName>e__FixedBuffer _cFileName;

			// Token: 0x0400000A RID: 10
			[FixedBuffer(typeof(char), 14)]
			private Interop.Kernel32.WIN32_FIND_DATA.<_cAlternateFileName>e__FixedBuffer _cAlternateFileName;

			// Token: 0x02000005 RID: 5
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 520)]
			public struct <_cFileName>e__FixedBuffer
			{
				// Token: 0x0400000B RID: 11
				public char FixedElementField;
			}

			// Token: 0x02000006 RID: 6
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 28)]
			public struct <_cAlternateFileName>e__FixedBuffer
			{
				// Token: 0x0400000C RID: 12
				public char FixedElementField;
			}
		}

		// Token: 0x02000007 RID: 7
		internal struct REG_TZI_FORMAT
		{
			// Token: 0x06000029 RID: 41 RVA: 0x00002385 File Offset: 0x00000585
			internal REG_TZI_FORMAT(in Interop.Kernel32.TIME_ZONE_INFORMATION tzi)
			{
				this.Bias = tzi.Bias;
				this.StandardDate = tzi.StandardDate;
				this.StandardBias = tzi.StandardBias;
				this.DaylightDate = tzi.DaylightDate;
				this.DaylightBias = tzi.DaylightBias;
			}

			// Token: 0x0400000D RID: 13
			internal int Bias;

			// Token: 0x0400000E RID: 14
			internal int StandardBias;

			// Token: 0x0400000F RID: 15
			internal int DaylightBias;

			// Token: 0x04000010 RID: 16
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x04000011 RID: 17
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;
		}

		// Token: 0x02000008 RID: 8
		internal struct SYSTEMTIME
		{
			// Token: 0x0600002A RID: 42 RVA: 0x000023C4 File Offset: 0x000005C4
			internal bool Equals(in Interop.Kernel32.SYSTEMTIME other)
			{
				return this.Year == other.Year && this.Month == other.Month && this.DayOfWeek == other.DayOfWeek && this.Day == other.Day && this.Hour == other.Hour && this.Minute == other.Minute && this.Second == other.Second && this.Milliseconds == other.Milliseconds;
			}

			// Token: 0x04000012 RID: 18
			internal ushort Year;

			// Token: 0x04000013 RID: 19
			internal ushort Month;

			// Token: 0x04000014 RID: 20
			internal ushort DayOfWeek;

			// Token: 0x04000015 RID: 21
			internal ushort Day;

			// Token: 0x04000016 RID: 22
			internal ushort Hour;

			// Token: 0x04000017 RID: 23
			internal ushort Minute;

			// Token: 0x04000018 RID: 24
			internal ushort Second;

			// Token: 0x04000019 RID: 25
			internal ushort Milliseconds;
		}

		// Token: 0x02000009 RID: 9
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TIME_DYNAMIC_ZONE_INFORMATION
		{
			// Token: 0x0600002B RID: 43 RVA: 0x00002444 File Offset: 0x00000644
			internal unsafe string GetTimeZoneKeyName()
			{
				fixed (char* ptr = &this.TimeZoneKeyName.FixedElementField)
				{
					return new string(ptr);
				}
			}

			// Token: 0x0400001A RID: 26
			internal int Bias;

			// Token: 0x0400001B RID: 27
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<StandardName>e__FixedBuffer StandardName;

			// Token: 0x0400001C RID: 28
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x0400001D RID: 29
			internal int StandardBias;

			// Token: 0x0400001E RID: 30
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<DaylightName>e__FixedBuffer DaylightName;

			// Token: 0x0400001F RID: 31
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;

			// Token: 0x04000020 RID: 32
			internal int DaylightBias;

			// Token: 0x04000021 RID: 33
			[FixedBuffer(typeof(char), 128)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<TimeZoneKeyName>e__FixedBuffer TimeZoneKeyName;

			// Token: 0x04000022 RID: 34
			internal byte DynamicDaylightTimeDisabled;

			// Token: 0x0200000A RID: 10
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <StandardName>e__FixedBuffer
			{
				// Token: 0x04000023 RID: 35
				public char FixedElementField;
			}

			// Token: 0x0200000B RID: 11
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <DaylightName>e__FixedBuffer
			{
				// Token: 0x04000024 RID: 36
				public char FixedElementField;
			}

			// Token: 0x0200000C RID: 12
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 256)]
			public struct <TimeZoneKeyName>e__FixedBuffer
			{
				// Token: 0x04000025 RID: 37
				public char FixedElementField;
			}
		}

		// Token: 0x0200000D RID: 13
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TIME_ZONE_INFORMATION
		{
			// Token: 0x0600002C RID: 44 RVA: 0x00002464 File Offset: 0x00000664
			internal unsafe TIME_ZONE_INFORMATION(in Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION dtzi)
			{
				fixed (Interop.Kernel32.TIME_ZONE_INFORMATION* ptr = &this)
				{
					ref Interop.Kernel32.TIME_ZONE_INFORMATION ptr2 = ref *ptr;
					fixed (Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION* ptr3 = &dtzi)
					{
						Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION* ptr4 = ptr3;
						ptr2 = *(Interop.Kernel32.TIME_ZONE_INFORMATION*)ptr4;
					}
				}
			}

			// Token: 0x0600002D RID: 45 RVA: 0x0000248C File Offset: 0x0000068C
			internal unsafe string GetStandardName()
			{
				fixed (char* ptr = &this.StandardName.FixedElementField)
				{
					return new string(ptr);
				}
			}

			// Token: 0x0600002E RID: 46 RVA: 0x000024AC File Offset: 0x000006AC
			internal unsafe string GetDaylightName()
			{
				fixed (char* ptr = &this.DaylightName.FixedElementField)
				{
					return new string(ptr);
				}
			}

			// Token: 0x04000026 RID: 38
			internal int Bias;

			// Token: 0x04000027 RID: 39
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_ZONE_INFORMATION.<StandardName>e__FixedBuffer StandardName;

			// Token: 0x04000028 RID: 40
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x04000029 RID: 41
			internal int StandardBias;

			// Token: 0x0400002A RID: 42
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_ZONE_INFORMATION.<DaylightName>e__FixedBuffer DaylightName;

			// Token: 0x0400002B RID: 43
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;

			// Token: 0x0400002C RID: 44
			internal int DaylightBias;

			// Token: 0x0200000E RID: 14
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <StandardName>e__FixedBuffer
			{
				// Token: 0x0400002D RID: 45
				public char FixedElementField;
			}

			// Token: 0x0200000F RID: 15
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <DaylightName>e__FixedBuffer
			{
				// Token: 0x0400002E RID: 46
				public char FixedElementField;
			}
		}

		// Token: 0x02000010 RID: 16
		internal enum FILE_INFO_BY_HANDLE_CLASS : uint
		{
			// Token: 0x04000030 RID: 48
			FileBasicInfo,
			// Token: 0x04000031 RID: 49
			FileStandardInfo,
			// Token: 0x04000032 RID: 50
			FileNameInfo,
			// Token: 0x04000033 RID: 51
			FileRenameInfo,
			// Token: 0x04000034 RID: 52
			FileDispositionInfo,
			// Token: 0x04000035 RID: 53
			FileAllocationInfo,
			// Token: 0x04000036 RID: 54
			FileEndOfFileInfo,
			// Token: 0x04000037 RID: 55
			FileStreamInfo,
			// Token: 0x04000038 RID: 56
			FileCompressionInfo,
			// Token: 0x04000039 RID: 57
			FileAttributeTagInfo,
			// Token: 0x0400003A RID: 58
			FileIdBothDirectoryInfo,
			// Token: 0x0400003B RID: 59
			FileIdBothDirectoryRestartInfo,
			// Token: 0x0400003C RID: 60
			FileIoPriorityHintInfo,
			// Token: 0x0400003D RID: 61
			FileRemoteProtocolInfo,
			// Token: 0x0400003E RID: 62
			FileFullDirectoryInfo,
			// Token: 0x0400003F RID: 63
			FileFullDirectoryRestartInfo
		}

		// Token: 0x02000011 RID: 17
		internal struct FILE_TIME
		{
			// Token: 0x0600002F RID: 47 RVA: 0x000024CC File Offset: 0x000006CC
			internal long ToTicks()
			{
				return (long)(((ulong)this.dwHighDateTime << 32) + (ulong)this.dwLowDateTime);
			}

			// Token: 0x06000030 RID: 48 RVA: 0x000024E0 File Offset: 0x000006E0
			internal DateTimeOffset ToDateTimeOffset()
			{
				return DateTimeOffset.FromFileTime(this.ToTicks());
			}

			// Token: 0x04000040 RID: 64
			internal uint dwLowDateTime;

			// Token: 0x04000041 RID: 65
			internal uint dwHighDateTime;
		}

		// Token: 0x02000012 RID: 18
		internal enum FINDEX_INFO_LEVELS : uint
		{
			// Token: 0x04000043 RID: 67
			FindExInfoStandard,
			// Token: 0x04000044 RID: 68
			FindExInfoBasic,
			// Token: 0x04000045 RID: 69
			FindExInfoMaxInfoLevel
		}

		// Token: 0x02000013 RID: 19
		internal enum FINDEX_SEARCH_OPS : uint
		{
			// Token: 0x04000047 RID: 71
			FindExSearchNameMatch,
			// Token: 0x04000048 RID: 72
			FindExSearchLimitToDirectories,
			// Token: 0x04000049 RID: 73
			FindExSearchLimitToDevices,
			// Token: 0x0400004A RID: 74
			FindExSearchMaxSearchOp
		}

		// Token: 0x02000014 RID: 20
		internal enum GET_FILEEX_INFO_LEVELS : uint
		{
			// Token: 0x0400004C RID: 76
			GetFileExInfoStandard,
			// Token: 0x0400004D RID: 77
			GetFileExMaxInfoLevel
		}

		// Token: 0x02000015 RID: 21
		internal struct SECURITY_ATTRIBUTES
		{
			// Token: 0x0400004E RID: 78
			internal uint nLength;

			// Token: 0x0400004F RID: 79
			internal IntPtr lpSecurityDescriptor;

			// Token: 0x04000050 RID: 80
			internal Interop.BOOL bInheritHandle;
		}

		// Token: 0x02000016 RID: 22
		internal struct FILE_BASIC_INFO
		{
			// Token: 0x04000051 RID: 81
			internal long CreationTime;

			// Token: 0x04000052 RID: 82
			internal long LastAccessTime;

			// Token: 0x04000053 RID: 83
			internal long LastWriteTime;

			// Token: 0x04000054 RID: 84
			internal long ChangeTime;

			// Token: 0x04000055 RID: 85
			internal uint FileAttributes;
		}

		// Token: 0x02000017 RID: 23
		internal struct WIN32_FILE_ATTRIBUTE_DATA
		{
			// Token: 0x06000031 RID: 49 RVA: 0x000024F0 File Offset: 0x000006F0
			internal void PopulateFrom(ref Interop.Kernel32.WIN32_FIND_DATA findData)
			{
				this.dwFileAttributes = (int)findData.dwFileAttributes;
				this.ftCreationTime = findData.ftCreationTime;
				this.ftLastAccessTime = findData.ftLastAccessTime;
				this.ftLastWriteTime = findData.ftLastWriteTime;
				this.nFileSizeHigh = findData.nFileSizeHigh;
				this.nFileSizeLow = findData.nFileSizeLow;
			}

			// Token: 0x04000056 RID: 86
			internal int dwFileAttributes;

			// Token: 0x04000057 RID: 87
			internal Interop.Kernel32.FILE_TIME ftCreationTime;

			// Token: 0x04000058 RID: 88
			internal Interop.Kernel32.FILE_TIME ftLastAccessTime;

			// Token: 0x04000059 RID: 89
			internal Interop.Kernel32.FILE_TIME ftLastWriteTime;

			// Token: 0x0400005A RID: 90
			internal uint nFileSizeHigh;

			// Token: 0x0400005B RID: 91
			internal uint nFileSizeLow;
		}
	}

	// Token: 0x02000018 RID: 24
	internal class BCrypt
	{
		// Token: 0x06000032 RID: 50
		[DllImport("BCrypt.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern Interop.BCrypt.NTSTATUS BCryptGenRandom(IntPtr hAlgorithm, byte* pbBuffer, int cbBuffer, int dwFlags);

		// Token: 0x02000019 RID: 25
		internal enum NTSTATUS : uint
		{
			// Token: 0x0400005D RID: 93
			STATUS_SUCCESS,
			// Token: 0x0400005E RID: 94
			STATUS_NOT_FOUND = 3221226021U,
			// Token: 0x0400005F RID: 95
			STATUS_INVALID_PARAMETER = 3221225485U,
			// Token: 0x04000060 RID: 96
			STATUS_NO_MEMORY = 3221225495U
		}
	}

	// Token: 0x0200001A RID: 26
	internal class User32
	{
		// Token: 0x06000033 RID: 51
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadStringW", SetLastError = true)]
		internal static extern int LoadString(SafeLibraryHandle handle, int id, [Out] StringBuilder buffer, int bufferLength);
	}

	// Token: 0x0200001B RID: 27
	internal enum BOOL
	{
		// Token: 0x04000062 RID: 98
		FALSE,
		// Token: 0x04000063 RID: 99
		TRUE
	}

	// Token: 0x0200001C RID: 28
	internal enum BOOLEAN : byte
	{
		// Token: 0x04000065 RID: 101
		FALSE,
		// Token: 0x04000066 RID: 102
		TRUE
	}

	// Token: 0x0200001D RID: 29
	internal struct LongFileTime
	{
		// Token: 0x04000067 RID: 103
		internal long TicksSince1601;
	}

	// Token: 0x0200001E RID: 30
	internal struct UNICODE_STRING
	{
		// Token: 0x04000068 RID: 104
		internal ushort Length;

		// Token: 0x04000069 RID: 105
		internal ushort MaximumLength;

		// Token: 0x0400006A RID: 106
		internal IntPtr Buffer;
	}

	// Token: 0x0200001F RID: 31
	internal class NtDll
	{
		// Token: 0x06000034 RID: 52
		[DllImport("ntdll.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		private unsafe static extern int NtCreateFile(out IntPtr FileHandle, Interop.NtDll.DesiredAccess DesiredAccess, ref Interop.NtDll.OBJECT_ATTRIBUTES ObjectAttributes, out Interop.NtDll.IO_STATUS_BLOCK IoStatusBlock, long* AllocationSize, FileAttributes FileAttributes, FileShare ShareAccess, Interop.NtDll.CreateDisposition CreateDisposition, Interop.NtDll.CreateOptions CreateOptions, void* EaBuffer, uint EaLength);

		// Token: 0x06000035 RID: 53 RVA: 0x00002548 File Offset: 0x00000748
		[return: TupleElementNames(new string[] { "status", "handle" })]
		internal unsafe static ValueTuple<int, IntPtr> CreateFile(ReadOnlySpan<char> path, IntPtr rootDirectory, Interop.NtDll.CreateDisposition createDisposition, Interop.NtDll.DesiredAccess desiredAccess = Interop.NtDll.DesiredAccess.SYNCHRONIZE | Interop.NtDll.DesiredAccess.FILE_GENERIC_READ, FileShare shareAccess = FileShare.Read | FileShare.Write | FileShare.Delete, FileAttributes fileAttributes = (FileAttributes)0, Interop.NtDll.CreateOptions createOptions = Interop.NtDll.CreateOptions.FILE_SYNCHRONOUS_IO_NONALERT, Interop.NtDll.ObjectAttributes objectAttributes = Interop.NtDll.ObjectAttributes.OBJ_CASE_INSENSITIVE)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(path))
			{
				char* ptr = reference;
				Interop.UNICODE_STRING unicode_STRING = checked(new Interop.UNICODE_STRING
				{
					Length = (ushort)(path.Length * 2),
					MaximumLength = (ushort)(path.Length * 2),
					Buffer = (IntPtr)((void*)ptr)
				});
				Interop.NtDll.OBJECT_ATTRIBUTES object_ATTRIBUTES = new Interop.NtDll.OBJECT_ATTRIBUTES(&unicode_STRING, objectAttributes, rootDirectory);
				IntPtr intPtr;
				Interop.NtDll.IO_STATUS_BLOCK io_STATUS_BLOCK;
				return new ValueTuple<int, IntPtr>(Interop.NtDll.NtCreateFile(out intPtr, desiredAccess, ref object_ATTRIBUTES, out io_STATUS_BLOCK, null, fileAttributes, shareAccess, createDisposition, createOptions, null, 0U), intPtr);
			}
		}

		// Token: 0x06000036 RID: 54
		[DllImport("ntdll.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public unsafe static extern int NtQueryDirectoryFile(IntPtr FileHandle, IntPtr Event, IntPtr ApcRoutine, IntPtr ApcContext, out Interop.NtDll.IO_STATUS_BLOCK IoStatusBlock, IntPtr FileInformation, uint Length, Interop.NtDll.FILE_INFORMATION_CLASS FileInformationClass, Interop.BOOLEAN ReturnSingleEntry, Interop.UNICODE_STRING* FileName, Interop.BOOLEAN RestartScan);

		// Token: 0x06000037 RID: 55
		[DllImport("ntdll.dll", ExactSpelling = true)]
		public static extern uint RtlNtStatusToDosError(int Status);

		// Token: 0x02000020 RID: 32
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct FILE_FULL_DIR_INFORMATION
		{
			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000038 RID: 56 RVA: 0x000025C8 File Offset: 0x000007C8
			public unsafe ReadOnlySpan<char> FileName
			{
				get
				{
					fixed (char* ptr = &this._fileName)
					{
						return new ReadOnlySpan<char>((void*)ptr, (int)(this.FileNameLength / 2U));
					}
				}
			}

			// Token: 0x06000039 RID: 57 RVA: 0x000025EC File Offset: 0x000007EC
			public unsafe static Interop.NtDll.FILE_FULL_DIR_INFORMATION* GetNextInfo(Interop.NtDll.FILE_FULL_DIR_INFORMATION* info)
			{
				if (info == null)
				{
					return null;
				}
				uint nextEntryOffset = info->NextEntryOffset;
				if (nextEntryOffset == 0U)
				{
					return null;
				}
				return info + nextEntryOffset / (uint)sizeof(Interop.NtDll.FILE_FULL_DIR_INFORMATION);
			}

			// Token: 0x0400006B RID: 107
			public uint NextEntryOffset;

			// Token: 0x0400006C RID: 108
			public uint FileIndex;

			// Token: 0x0400006D RID: 109
			public Interop.LongFileTime CreationTime;

			// Token: 0x0400006E RID: 110
			public Interop.LongFileTime LastAccessTime;

			// Token: 0x0400006F RID: 111
			public Interop.LongFileTime LastWriteTime;

			// Token: 0x04000070 RID: 112
			public Interop.LongFileTime ChangeTime;

			// Token: 0x04000071 RID: 113
			public long EndOfFile;

			// Token: 0x04000072 RID: 114
			public long AllocationSize;

			// Token: 0x04000073 RID: 115
			public FileAttributes FileAttributes;

			// Token: 0x04000074 RID: 116
			public uint FileNameLength;

			// Token: 0x04000075 RID: 117
			public uint EaSize;

			// Token: 0x04000076 RID: 118
			private char _fileName;
		}

		// Token: 0x02000021 RID: 33
		public enum FILE_INFORMATION_CLASS : uint
		{
			// Token: 0x04000078 RID: 120
			FileDirectoryInformation = 1U,
			// Token: 0x04000079 RID: 121
			FileFullDirectoryInformation,
			// Token: 0x0400007A RID: 122
			FileBothDirectoryInformation,
			// Token: 0x0400007B RID: 123
			FileBasicInformation,
			// Token: 0x0400007C RID: 124
			FileStandardInformation,
			// Token: 0x0400007D RID: 125
			FileInternalInformation,
			// Token: 0x0400007E RID: 126
			FileEaInformation,
			// Token: 0x0400007F RID: 127
			FileAccessInformation,
			// Token: 0x04000080 RID: 128
			FileNameInformation,
			// Token: 0x04000081 RID: 129
			FileRenameInformation,
			// Token: 0x04000082 RID: 130
			FileLinkInformation,
			// Token: 0x04000083 RID: 131
			FileNamesInformation,
			// Token: 0x04000084 RID: 132
			FileDispositionInformation,
			// Token: 0x04000085 RID: 133
			FilePositionInformation,
			// Token: 0x04000086 RID: 134
			FileFullEaInformation,
			// Token: 0x04000087 RID: 135
			FileModeInformation,
			// Token: 0x04000088 RID: 136
			FileAlignmentInformation,
			// Token: 0x04000089 RID: 137
			FileAllInformation,
			// Token: 0x0400008A RID: 138
			FileAllocationInformation,
			// Token: 0x0400008B RID: 139
			FileEndOfFileInformation,
			// Token: 0x0400008C RID: 140
			FileAlternateNameInformation,
			// Token: 0x0400008D RID: 141
			FileStreamInformation,
			// Token: 0x0400008E RID: 142
			FilePipeInformation,
			// Token: 0x0400008F RID: 143
			FilePipeLocalInformation,
			// Token: 0x04000090 RID: 144
			FilePipeRemoteInformation,
			// Token: 0x04000091 RID: 145
			FileMailslotQueryInformation,
			// Token: 0x04000092 RID: 146
			FileMailslotSetInformation,
			// Token: 0x04000093 RID: 147
			FileCompressionInformation,
			// Token: 0x04000094 RID: 148
			FileObjectIdInformation,
			// Token: 0x04000095 RID: 149
			FileCompletionInformation,
			// Token: 0x04000096 RID: 150
			FileMoveClusterInformation,
			// Token: 0x04000097 RID: 151
			FileQuotaInformation,
			// Token: 0x04000098 RID: 152
			FileReparsePointInformation,
			// Token: 0x04000099 RID: 153
			FileNetworkOpenInformation,
			// Token: 0x0400009A RID: 154
			FileAttributeTagInformation,
			// Token: 0x0400009B RID: 155
			FileTrackingInformation,
			// Token: 0x0400009C RID: 156
			FileIdBothDirectoryInformation,
			// Token: 0x0400009D RID: 157
			FileIdFullDirectoryInformation,
			// Token: 0x0400009E RID: 158
			FileValidDataLengthInformation,
			// Token: 0x0400009F RID: 159
			FileShortNameInformation,
			// Token: 0x040000A0 RID: 160
			FileIoCompletionNotificationInformation,
			// Token: 0x040000A1 RID: 161
			FileIoStatusBlockRangeInformation,
			// Token: 0x040000A2 RID: 162
			FileIoPriorityHintInformation,
			// Token: 0x040000A3 RID: 163
			FileSfioReserveInformation,
			// Token: 0x040000A4 RID: 164
			FileSfioVolumeInformation,
			// Token: 0x040000A5 RID: 165
			FileHardLinkInformation,
			// Token: 0x040000A6 RID: 166
			FileProcessIdsUsingFileInformation,
			// Token: 0x040000A7 RID: 167
			FileNormalizedNameInformation,
			// Token: 0x040000A8 RID: 168
			FileNetworkPhysicalNameInformation,
			// Token: 0x040000A9 RID: 169
			FileIdGlobalTxDirectoryInformation,
			// Token: 0x040000AA RID: 170
			FileIsRemoteDeviceInformation,
			// Token: 0x040000AB RID: 171
			FileUnusedInformation,
			// Token: 0x040000AC RID: 172
			FileNumaNodeInformation,
			// Token: 0x040000AD RID: 173
			FileStandardLinkInformation,
			// Token: 0x040000AE RID: 174
			FileRemoteProtocolInformation,
			// Token: 0x040000AF RID: 175
			FileRenameInformationBypassAccessCheck,
			// Token: 0x040000B0 RID: 176
			FileLinkInformationBypassAccessCheck,
			// Token: 0x040000B1 RID: 177
			FileVolumeNameInformation,
			// Token: 0x040000B2 RID: 178
			FileIdInformation,
			// Token: 0x040000B3 RID: 179
			FileIdExtdDirectoryInformation,
			// Token: 0x040000B4 RID: 180
			FileReplaceCompletionInformation,
			// Token: 0x040000B5 RID: 181
			FileHardLinkFullIdInformation,
			// Token: 0x040000B6 RID: 182
			FileIdExtdBothDirectoryInformation,
			// Token: 0x040000B7 RID: 183
			FileDispositionInformationEx,
			// Token: 0x040000B8 RID: 184
			FileRenameInformationEx,
			// Token: 0x040000B9 RID: 185
			FileRenameInformationExBypassAccessCheck,
			// Token: 0x040000BA RID: 186
			FileDesiredStorageClassInformation,
			// Token: 0x040000BB RID: 187
			FileStatInformation
		}

		// Token: 0x02000022 RID: 34
		public struct IO_STATUS_BLOCK
		{
			// Token: 0x040000BC RID: 188
			public Interop.NtDll.IO_STATUS_BLOCK.IO_STATUS Status;

			// Token: 0x040000BD RID: 189
			public IntPtr Information;

			// Token: 0x02000023 RID: 35
			[StructLayout(LayoutKind.Explicit)]
			public struct IO_STATUS
			{
				// Token: 0x040000BE RID: 190
				[FieldOffset(0)]
				public uint Status;

				// Token: 0x040000BF RID: 191
				[FieldOffset(0)]
				public IntPtr Pointer;
			}
		}

		// Token: 0x02000024 RID: 36
		public struct OBJECT_ATTRIBUTES
		{
			// Token: 0x0600003A RID: 58 RVA: 0x00002612 File Offset: 0x00000812
			public unsafe OBJECT_ATTRIBUTES(Interop.UNICODE_STRING* objectName, Interop.NtDll.ObjectAttributes attributes, IntPtr rootDirectory)
			{
				this.Length = (uint)sizeof(Interop.NtDll.OBJECT_ATTRIBUTES);
				this.RootDirectory = rootDirectory;
				this.ObjectName = objectName;
				this.Attributes = attributes;
				this.SecurityDescriptor = null;
				this.SecurityQualityOfService = null;
			}

			// Token: 0x040000C0 RID: 192
			public uint Length;

			// Token: 0x040000C1 RID: 193
			public IntPtr RootDirectory;

			// Token: 0x040000C2 RID: 194
			public unsafe Interop.UNICODE_STRING* ObjectName;

			// Token: 0x040000C3 RID: 195
			public Interop.NtDll.ObjectAttributes Attributes;

			// Token: 0x040000C4 RID: 196
			public unsafe void* SecurityDescriptor;

			// Token: 0x040000C5 RID: 197
			public unsafe void* SecurityQualityOfService;
		}

		// Token: 0x02000025 RID: 37
		[Flags]
		public enum ObjectAttributes : uint
		{
			// Token: 0x040000C7 RID: 199
			OBJ_INHERIT = 2U,
			// Token: 0x040000C8 RID: 200
			OBJ_PERMANENT = 16U,
			// Token: 0x040000C9 RID: 201
			OBJ_EXCLUSIVE = 32U,
			// Token: 0x040000CA RID: 202
			OBJ_CASE_INSENSITIVE = 64U,
			// Token: 0x040000CB RID: 203
			OBJ_OPENIF = 128U,
			// Token: 0x040000CC RID: 204
			OBJ_OPENLINK = 256U
		}

		// Token: 0x02000026 RID: 38
		public enum CreateDisposition : uint
		{
			// Token: 0x040000CE RID: 206
			FILE_SUPERSEDE,
			// Token: 0x040000CF RID: 207
			FILE_OPEN,
			// Token: 0x040000D0 RID: 208
			FILE_CREATE,
			// Token: 0x040000D1 RID: 209
			FILE_OPEN_IF,
			// Token: 0x040000D2 RID: 210
			FILE_OVERWRITE,
			// Token: 0x040000D3 RID: 211
			FILE_OVERWRITE_IF
		}

		// Token: 0x02000027 RID: 39
		public enum CreateOptions : uint
		{
			// Token: 0x040000D5 RID: 213
			FILE_DIRECTORY_FILE = 1U,
			// Token: 0x040000D6 RID: 214
			FILE_WRITE_THROUGH,
			// Token: 0x040000D7 RID: 215
			FILE_SEQUENTIAL_ONLY = 4U,
			// Token: 0x040000D8 RID: 216
			FILE_NO_INTERMEDIATE_BUFFERING = 8U,
			// Token: 0x040000D9 RID: 217
			FILE_SYNCHRONOUS_IO_ALERT = 16U,
			// Token: 0x040000DA RID: 218
			FILE_SYNCHRONOUS_IO_NONALERT = 32U,
			// Token: 0x040000DB RID: 219
			FILE_NON_DIRECTORY_FILE = 64U,
			// Token: 0x040000DC RID: 220
			FILE_CREATE_TREE_CONNECTION = 128U,
			// Token: 0x040000DD RID: 221
			FILE_COMPLETE_IF_OPLOCKED = 256U,
			// Token: 0x040000DE RID: 222
			FILE_NO_EA_KNOWLEDGE = 512U,
			// Token: 0x040000DF RID: 223
			FILE_RANDOM_ACCESS = 2048U,
			// Token: 0x040000E0 RID: 224
			FILE_DELETE_ON_CLOSE = 4096U,
			// Token: 0x040000E1 RID: 225
			FILE_OPEN_BY_FILE_ID = 8192U,
			// Token: 0x040000E2 RID: 226
			FILE_OPEN_FOR_BACKUP_INTENT = 16384U,
			// Token: 0x040000E3 RID: 227
			FILE_NO_COMPRESSION = 32768U,
			// Token: 0x040000E4 RID: 228
			FILE_OPEN_REQUIRING_OPLOCK = 65536U,
			// Token: 0x040000E5 RID: 229
			FILE_DISALLOW_EXCLUSIVE = 131072U,
			// Token: 0x040000E6 RID: 230
			FILE_SESSION_AWARE = 262144U,
			// Token: 0x040000E7 RID: 231
			FILE_RESERVE_OPFILTER = 1048576U,
			// Token: 0x040000E8 RID: 232
			FILE_OPEN_REPARSE_POINT = 2097152U,
			// Token: 0x040000E9 RID: 233
			FILE_OPEN_NO_RECALL = 4194304U
		}

		// Token: 0x02000028 RID: 40
		[Flags]
		public enum DesiredAccess : uint
		{
			// Token: 0x040000EB RID: 235
			FILE_READ_DATA = 1U,
			// Token: 0x040000EC RID: 236
			FILE_LIST_DIRECTORY = 1U,
			// Token: 0x040000ED RID: 237
			FILE_WRITE_DATA = 2U,
			// Token: 0x040000EE RID: 238
			FILE_ADD_FILE = 2U,
			// Token: 0x040000EF RID: 239
			FILE_APPEND_DATA = 4U,
			// Token: 0x040000F0 RID: 240
			FILE_ADD_SUBDIRECTORY = 4U,
			// Token: 0x040000F1 RID: 241
			FILE_CREATE_PIPE_INSTANCE = 4U,
			// Token: 0x040000F2 RID: 242
			FILE_READ_EA = 8U,
			// Token: 0x040000F3 RID: 243
			FILE_WRITE_EA = 16U,
			// Token: 0x040000F4 RID: 244
			FILE_EXECUTE = 32U,
			// Token: 0x040000F5 RID: 245
			FILE_TRAVERSE = 32U,
			// Token: 0x040000F6 RID: 246
			FILE_DELETE_CHILD = 64U,
			// Token: 0x040000F7 RID: 247
			FILE_READ_ATTRIBUTES = 128U,
			// Token: 0x040000F8 RID: 248
			FILE_WRITE_ATTRIBUTES = 256U,
			// Token: 0x040000F9 RID: 249
			FILE_ALL_ACCESS = 983551U,
			// Token: 0x040000FA RID: 250
			DELETE = 65536U,
			// Token: 0x040000FB RID: 251
			READ_CONTROL = 131072U,
			// Token: 0x040000FC RID: 252
			WRITE_DAC = 262144U,
			// Token: 0x040000FD RID: 253
			WRITE_OWNER = 524288U,
			// Token: 0x040000FE RID: 254
			SYNCHRONIZE = 1048576U,
			// Token: 0x040000FF RID: 255
			STANDARD_RIGHTS_READ = 131072U,
			// Token: 0x04000100 RID: 256
			STANDARD_RIGHTS_WRITE = 131072U,
			// Token: 0x04000101 RID: 257
			STANDARD_RIGHTS_EXECUTE = 131072U,
			// Token: 0x04000102 RID: 258
			FILE_GENERIC_READ = 2147483648U,
			// Token: 0x04000103 RID: 259
			FILE_GENERIC_WRITE = 1073741824U,
			// Token: 0x04000104 RID: 260
			FILE_GENERIC_EXECUTE = 536870912U
		}
	}

	// Token: 0x02000029 RID: 41
	internal class Advapi32
	{
		// Token: 0x0600003B RID: 59
		[DllImport("advapi32.dll")]
		internal static extern int RegCloseKey(IntPtr hKey);

		// Token: 0x0600003C RID: 60
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegCreateKeyExW")]
		internal static extern int RegCreateKeyEx(SafeRegistryHandle hKey, string lpSubKey, int Reserved, string lpClass, int dwOptions, int samDesired, ref Interop.Kernel32.SECURITY_ATTRIBUTES secAttrs, out SafeRegistryHandle hkResult, out int lpdwDisposition);

		// Token: 0x0600003D RID: 61
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegDeleteValueW")]
		internal static extern int RegDeleteValue(SafeRegistryHandle hKey, string lpValueName);

		// Token: 0x0600003E RID: 62
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegEnumKeyExW")]
		internal static extern int RegEnumKeyEx(SafeRegistryHandle hKey, int dwIndex, char[] lpName, ref int lpcbName, int[] lpReserved, [Out] StringBuilder lpClass, int[] lpcbClass, long[] lpftLastWriteTime);

		// Token: 0x0600003F RID: 63
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegEnumValueW")]
		internal static extern int RegEnumValue(SafeRegistryHandle hKey, int dwIndex, char[] lpValueName, ref int lpcbValueName, IntPtr lpReserved_MustBeZero, int[] lpType, byte[] lpData, int[] lpcbData);

		// Token: 0x06000040 RID: 64
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyExW")]
		internal static extern int RegOpenKeyEx(SafeRegistryHandle hKey, string lpSubKey, int ulOptions, int samDesired, out SafeRegistryHandle hkResult);

		// Token: 0x06000041 RID: 65
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryInfoKeyW")]
		internal static extern int RegQueryInfoKey(SafeRegistryHandle hKey, [Out] StringBuilder lpClass, int[] lpcbClass, IntPtr lpReserved_MustBeZero, ref int lpcSubKeys, int[] lpcbMaxSubKeyLen, int[] lpcbMaxClassLen, ref int lpcValues, int[] lpcbMaxValueNameLen, int[] lpcbMaxValueLen, int[] lpcbSecurityDescriptor, int[] lpftLastWriteTime);

		// Token: 0x06000042 RID: 66
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] byte[] lpData, ref int lpcbData);

		// Token: 0x06000043 RID: 67
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, ref int lpData, ref int lpcbData);

		// Token: 0x06000044 RID: 68
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, ref long lpData, ref int lpcbData);

		// Token: 0x06000045 RID: 69
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW")]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] char[] lpData, ref int lpcbData);

		// Token: 0x06000046 RID: 70
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, byte[] lpData, int cbData);

		// Token: 0x06000047 RID: 71
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, char[] lpData, int cbData);

		// Token: 0x06000048 RID: 72
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, ref int lpData, int cbData);

		// Token: 0x06000049 RID: 73
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, ref long lpData, int cbData);

		// Token: 0x0600004A RID: 74
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW")]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, RegistryValueKind dwType, string lpData, int cbData);
	}

	// Token: 0x0200002A RID: 42
	internal static class mincore
	{
		// Token: 0x0600004B RID: 75
		[DllImport("api-ms-win-core-heap-l1-1-0.dll")]
		internal static extern IntPtr GetProcessHeap();

		// Token: 0x0600004C RID: 76
		[DllImport("api-ms-win-core-heap-l1-1-0.dll")]
		internal static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

		// Token: 0x0600004D RID: 77
		[DllImport("api-ms-win-core-threadpool-l1-2-0.dll", SetLastError = true)]
		internal static extern SafeThreadPoolIOHandle CreateThreadpoolIo(SafeHandle fl, IntPtr pfnio, IntPtr context, IntPtr pcbe);

		// Token: 0x0600004E RID: 78
		[DllImport("api-ms-win-core-threadpool-l1-2-0.dll")]
		internal static extern void CloseThreadpoolIo(IntPtr pio);

		// Token: 0x0600004F RID: 79
		[DllImport("api-ms-win-core-threadpool-l1-2-0.dll")]
		internal static extern void StartThreadpoolIo(SafeThreadPoolIOHandle pio);

		// Token: 0x06000050 RID: 80
		[DllImport("api-ms-win-core-threadpool-l1-2-0.dll")]
		internal static extern void CancelThreadpoolIo(SafeThreadPoolIOHandle pio);
	}

	// Token: 0x0200002B RID: 43
	// (Invoke) Token: 0x06000052 RID: 82
	internal delegate void NativeIoCompletionCallback(IntPtr instance, IntPtr context, IntPtr overlapped, uint ioResult, UIntPtr numberOfBytesTransferred, IntPtr io);
}
