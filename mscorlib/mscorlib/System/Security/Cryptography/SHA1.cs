using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA1" /> hash for the input data.</summary>
	// Token: 0x020003AB RID: 939
	[ComVisible(true)]
	public abstract class SHA1 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.SHA1" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The policy on this object is not compliant with the FIPS algorithm.</exception>
		// Token: 0x0600201F RID: 8223 RVA: 0x0007F916 File Offset: 0x0007DB16
		protected SHA1()
		{
			this.HashSizeValue = 160;
		}

		/// <summary>Creates an instance of the default implementation of <see cref="T:System.Security.Cryptography.SHA1" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA1" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002020 RID: 8224 RVA: 0x000824BA File Offset: 0x000806BA
		public static SHA1 Create()
		{
			return SHA1.Create("System.Security.Cryptography.SHA1");
		}

		/// <summary>Creates an instance of the specified implementation of <see cref="T:System.Security.Cryptography.SHA1" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Security.Cryptography.SHA1" /> using the specified implementation.</returns>
		/// <param name="hashName">The name of the specific implementation of <see cref="T:System.Security.Cryptography.SHA1" /> to be used. </param>
		// Token: 0x06002021 RID: 8225 RVA: 0x000824C6 File Offset: 0x000806C6
		public new static SHA1 Create(string hashName)
		{
			return (SHA1)CryptoConfig.CreateFromName(hashName);
		}
	}
}
