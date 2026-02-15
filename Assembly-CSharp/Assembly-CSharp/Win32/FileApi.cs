using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Win32
{
	// Token: 0x0200116C RID: 4460
	public static class FileApi
	{
		// Token: 0x0600849E RID: 33950
		[PreserveSig]
		internal static extern SafeFileHandle CreateFile(string lpFileName, FileApi.DesiredAccess dwDesiredAccess, FileApi.ShareMode dwShareMode, IntPtr lpSecurityAttributess, FileApi.CreationDisposition dwCreationDisposition, FileApi.FlagsAndAttributes dwFlagsAndAttributes, SafeFileHandle hTemplateFile);

		// Token: 0x0600849F RID: 33951
		[PreserveSig]
		internal static extern bool WriteFile(SafeFileHandle handle, byte[] bytes, int numBytesToWrite, out int numBytesWrite, IntPtr overlapped_MustBeZero);

		// Token: 0x060084A0 RID: 33952
		[PreserveSig]
		internal static extern bool ReadFile(SafeFileHandle handle, byte[] buffer, int numBytesToRead, out int numBytesRead, IntPtr overlapped_MustBeZero);

		// Token: 0x060084A1 RID: 33953
		[PreserveSig]
		internal static extern bool GetFileSizeEx(SafeFileHandle hFile, ref FileApi.LARGE_INTEGER lpFileSize);

		// Token: 0x060084A2 RID: 33954
		[PreserveSig]
		internal static extern bool SetFilePointerEx(SafeFileHandle hFile, FileApi.LARGE_INTEGER liDistanceToMove, ref FileApi.LARGE_INTEGER lpNewFilePointer, FileApi.SeekOrigin dwMoveMethod);

		// Token: 0x060084A3 RID: 33955
		[PreserveSig]
		internal static extern bool SetEndOfFile(SafeFileHandle hFile);

		// Token: 0x060084A4 RID: 33956
		[PreserveSig]
		internal static extern bool FlushFileBuffers(SafeFileHandle hFile);

		// Token: 0x0200116D RID: 4461
		public enum DesiredAccess : uint
		{
			// Token: 0x0400C009 RID: 49161
			GENERIC_ALL = 268435456U,
			// Token: 0x0400C00A RID: 49162
			GENERIC_EXECUTE = 536870912U,
			// Token: 0x0400C00B RID: 49163
			GENERIC_WRITE = 1073741824U,
			// Token: 0x0400C00C RID: 49164
			GENERIC_READ = 2147483648U
		}

		// Token: 0x0200116E RID: 4462
		public enum ShareMode : uint
		{
			// Token: 0x0400C00E RID: 49166
			FILE_SHARE_NONE,
			// Token: 0x0400C00F RID: 49167
			FILE_SHARE_READ,
			// Token: 0x0400C010 RID: 49168
			FILE_SHARE_WRITE,
			// Token: 0x0400C011 RID: 49169
			FILE_SHARE_DELETE = 4U
		}

		// Token: 0x0200116F RID: 4463
		public enum CreationDisposition : uint
		{
			// Token: 0x0400C013 RID: 49171
			CREATE_NEW = 1U,
			// Token: 0x0400C014 RID: 49172
			CREATE_ALWAYS,
			// Token: 0x0400C015 RID: 49173
			OPEN_EXISTING,
			// Token: 0x0400C016 RID: 49174
			OPEN_ALWAYS,
			// Token: 0x0400C017 RID: 49175
			TRUNCATE_EXISTING
		}

		// Token: 0x02001170 RID: 4464
		public enum FlagsAndAttributes : uint
		{
			// Token: 0x0400C019 RID: 49177
			FILE_ATTRIBUTE_READONLY = 1U,
			// Token: 0x0400C01A RID: 49178
			FILE_ATTRIBUTE_HIDDEN,
			// Token: 0x0400C01B RID: 49179
			FILE_ATTRIBUTE_SYSTEM = 4U,
			// Token: 0x0400C01C RID: 49180
			FILE_ATTRIBUTE_DIRECTORY = 16U,
			// Token: 0x0400C01D RID: 49181
			FILE_ATTRIBUTE_ARCHIVE = 32U,
			// Token: 0x0400C01E RID: 49182
			FILE_ATTRIBUTE_DEVICE = 64U,
			// Token: 0x0400C01F RID: 49183
			FILE_ATTRIBUTE_NORMAL = 128U,
			// Token: 0x0400C020 RID: 49184
			FILE_ATTRIBUTE_TEMPORARY = 256U,
			// Token: 0x0400C021 RID: 49185
			FILE_ATTRIBUTE_SPARSE_FILE = 512U,
			// Token: 0x0400C022 RID: 49186
			FILE_ATTRIBUTE_REPARSE_POINT = 1024U,
			// Token: 0x0400C023 RID: 49187
			FILE_ATTRIBUTE_COMPRESSED = 2048U,
			// Token: 0x0400C024 RID: 49188
			FILE_ATTRIBUTE_OFFLINE = 4096U,
			// Token: 0x0400C025 RID: 49189
			FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 8192U,
			// Token: 0x0400C026 RID: 49190
			FILE_ATTRIBUTE_ENCRYPTED = 16384U,
			// Token: 0x0400C027 RID: 49191
			FILE_ATTRIBUTE_VIRTUAL = 65536U,
			// Token: 0x0400C028 RID: 49192
			FILE_ATTRIBUTE_VALID_FLAGS = 32695U,
			// Token: 0x0400C029 RID: 49193
			FILE_ATTRIBUTE_VALID_SET_FLAGS = 12711U,
			// Token: 0x0400C02A RID: 49194
			FILE_FLAG_OVERLAPPED = 1073741824U
		}

		// Token: 0x02001171 RID: 4465
		public enum SeekOrigin : uint
		{
			// Token: 0x0400C02C RID: 49196
			FILE_BEGIN,
			// Token: 0x0400C02D RID: 49197
			FILE_CURRENT,
			// Token: 0x0400C02E RID: 49198
			FILE_END
		}

		// Token: 0x02001172 RID: 4466
		internal struct LARGE_INTEGER
		{
		}
	}
}
