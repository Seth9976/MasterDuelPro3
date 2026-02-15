using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000002 RID: 2
internal static class Interop
{
	// Token: 0x02000003 RID: 3
	internal static class Crypt32
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static global::Interop.Crypt32.CRYPT_OID_INFO FindOidInfo(global::Interop.Crypt32.CryptOidInfoKeyType keyType, string key, OidGroup group, bool fallBackToAllGroups)
		{
			IntPtr intPtr = IntPtr.Zero;
			global::Interop.Crypt32.CRYPT_OID_INFO crypt_OID_INFO;
			try
			{
				if (keyType == global::Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_OID_KEY)
				{
					intPtr = Marshal.StringToCoTaskMemAnsi(key);
				}
				else
				{
					if (keyType != global::Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_NAME_KEY)
					{
						throw new NotSupportedException();
					}
					intPtr = Marshal.StringToCoTaskMemUni(key);
				}
				if (!global::Interop.Crypt32.OidGroupWillNotUseActiveDirectory(group))
				{
					OidGroup oidGroup = group | (OidGroup)(-2147483648);
					IntPtr intPtr2 = global::Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, oidGroup);
					if (intPtr2 != IntPtr.Zero)
					{
						return Marshal.PtrToStructure<global::Interop.Crypt32.CRYPT_OID_INFO>(intPtr2);
					}
				}
				IntPtr intPtr3 = global::Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, group);
				if (intPtr3 != IntPtr.Zero)
				{
					crypt_OID_INFO = Marshal.PtrToStructure<global::Interop.Crypt32.CRYPT_OID_INFO>(intPtr3);
				}
				else
				{
					if (fallBackToAllGroups && group != OidGroup.All)
					{
						IntPtr intPtr4 = global::Interop.Crypt32.CryptFindOIDInfo(keyType, intPtr, OidGroup.All);
						if (intPtr4 != IntPtr.Zero)
						{
							return Marshal.PtrToStructure<global::Interop.Crypt32.CRYPT_OID_INFO>(intPtr4);
						}
					}
					crypt_OID_INFO = new global::Interop.Crypt32.CRYPT_OID_INFO
					{
						AlgId = -1
					};
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return crypt_OID_INFO;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
		private static bool OidGroupWillNotUseActiveDirectory(OidGroup group)
		{
			return group == OidGroup.HashAlgorithm || group == OidGroup.EncryptionAlgorithm || group == OidGroup.PublicKeyAlgorithm || group == OidGroup.SignatureAlgorithm || group == OidGroup.Attribute || group == OidGroup.ExtensionOrAttribute || group == OidGroup.KeyDerivationFunction;
		}

		// Token: 0x06000003 RID: 3
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr CryptFindOIDInfo(global::Interop.Crypt32.CryptOidInfoKeyType dwKeyType, IntPtr pvKey, OidGroup group);

		// Token: 0x02000004 RID: 4
		internal struct CRYPT_OID_INFO
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000004 RID: 4 RVA: 0x00002159 File Offset: 0x00000359
			public string OID
			{
				get
				{
					return Marshal.PtrToStringAnsi(this.pszOID);
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000005 RID: 5 RVA: 0x00002166 File Offset: 0x00000366
			public string Name
			{
				get
				{
					return Marshal.PtrToStringUni(this.pwszName);
				}
			}

			// Token: 0x04000001 RID: 1
			public int cbSize;

			// Token: 0x04000002 RID: 2
			public IntPtr pszOID;

			// Token: 0x04000003 RID: 3
			public IntPtr pwszName;

			// Token: 0x04000004 RID: 4
			public OidGroup dwGroupId;

			// Token: 0x04000005 RID: 5
			public int AlgId;

			// Token: 0x04000006 RID: 6
			public int cbData;

			// Token: 0x04000007 RID: 7
			public IntPtr pbData;
		}

		// Token: 0x02000005 RID: 5
		internal enum CryptOidInfoKeyType
		{
			// Token: 0x04000009 RID: 9
			CRYPT_OID_INFO_OID_KEY = 1,
			// Token: 0x0400000A RID: 10
			CRYPT_OID_INFO_NAME_KEY,
			// Token: 0x0400000B RID: 11
			CRYPT_OID_INFO_ALGID_KEY,
			// Token: 0x0400000C RID: 12
			CRYPT_OID_INFO_SIGN_KEY,
			// Token: 0x0400000D RID: 13
			CRYPT_OID_INFO_CNG_ALGID_KEY,
			// Token: 0x0400000E RID: 14
			CRYPT_OID_INFO_CNG_SIGN_KEY
		}
	}

	// Token: 0x02000006 RID: 6
	internal enum BOOL
	{
		// Token: 0x04000010 RID: 16
		FALSE,
		// Token: 0x04000011 RID: 17
		TRUE
	}

	// Token: 0x02000007 RID: 7
	internal class Kernel32
	{
		// Token: 0x06000006 RID: 6
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000007 RID: 7
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateFileW", ExactSpelling = true, SetLastError = true)]
		private unsafe static extern IntPtr CreateFilePrivate(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, global::Interop.Kernel32.SECURITY_ATTRIBUTES* securityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000008 RID: 8 RVA: 0x00002174 File Offset: 0x00000374
		internal static SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			IntPtr intPtr = global::Interop.Kernel32.CreateFile_IntPtr(lpFileName, dwDesiredAccess, dwShareMode, dwCreationDisposition, dwFlagsAndAttributes);
			SafeFileHandle safeFileHandle;
			try
			{
				safeFileHandle = new SafeFileHandle(intPtr, true);
			}
			catch
			{
				global::Interop.Kernel32.CloseHandle(intPtr);
				throw;
			}
			return safeFileHandle;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B4 File Offset: 0x000003B4
		internal static IntPtr CreateFile_IntPtr(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, FileMode dwCreationDisposition, int dwFlagsAndAttributes)
		{
			lpFileName = global::System.IO.PathInternal.EnsureExtendedPrefixIfNeeded(lpFileName);
			return global::Interop.Kernel32.CreateFilePrivate(lpFileName, dwDesiredAccess, dwShareMode, null, dwCreationDisposition, dwFlagsAndAttributes, IntPtr.Zero);
		}

		// Token: 0x0600000A RID: 10
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal unsafe static extern bool ReadDirectoryChangesW(SafeFileHandle hDirectory, byte[] lpBuffer, uint nBufferLength, [MarshalAs(UnmanagedType.Bool)] bool bWatchSubtree, int dwNotifyFilter, out int lpBytesReturned, NativeOverlapped* lpOverlapped, IntPtr lpCompletionRoutine);

		// Token: 0x02000008 RID: 8
		internal struct SECURITY_ATTRIBUTES
		{
			// Token: 0x04000012 RID: 18
			internal uint nLength;

			// Token: 0x04000013 RID: 19
			internal IntPtr lpSecurityDescriptor;

			// Token: 0x04000014 RID: 20
			internal global::Interop.BOOL bInheritHandle;
		}
	}
}
