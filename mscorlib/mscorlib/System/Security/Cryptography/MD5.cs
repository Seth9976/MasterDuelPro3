using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm inherit. </summary>
	// Token: 0x0200039E RID: 926
	[ComVisible(true)]
	public abstract class MD5 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.MD5" />.</summary>
		// Token: 0x06001FC3 RID: 8131 RVA: 0x0007D844 File Offset: 0x0007BA44
		protected MD5()
		{
			this.HashSizeValue = 128;
		}

		/// <summary>Creates an instance of the default implementation of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</summary>
		/// <returns>A new instance of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001FC4 RID: 8132 RVA: 0x0007D857 File Offset: 0x0007BA57
		public static MD5 Create()
		{
			return MD5.Create("System.Security.Cryptography.MD5");
		}

		/// <summary>Creates an instance of the specified implementation of the <see cref="T:System.Security.Cryptography.MD5" /> hash algorithm.</summary>
		/// <returns>A new instance of the specified implementation of <see cref="T:System.Security.Cryptography.MD5" />.</returns>
		/// <param name="algName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.MD5" /> to use. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="algName" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06001FC5 RID: 8133 RVA: 0x0007D863 File Offset: 0x0007BA63
		public new static MD5 Create(string algName)
		{
			return (MD5)CryptoConfig.CreateFromName(algName);
		}
	}
}
