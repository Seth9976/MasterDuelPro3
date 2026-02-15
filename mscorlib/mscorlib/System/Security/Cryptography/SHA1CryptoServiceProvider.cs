using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the input data using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited. </summary>
	// Token: 0x020003C7 RID: 967
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> class.</summary>
		// Token: 0x060020F5 RID: 8437 RVA: 0x00089994 File Offset: 0x00087B94
		public SHA1CryptoServiceProvider()
		{
			this.sha = new SHA1Internal();
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000899A8 File Offset: 0x00087BA8
		~SHA1CryptoServiceProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000899D8 File Offset: 0x00087BD8
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000899E1 File Offset: 0x00087BE1
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this.State = 1;
			this.sha.HashCore(rgb, ibStart, cbSize);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000899F8 File Offset: 0x00087BF8
		protected override byte[] HashFinal()
		{
			this.State = 0;
			return this.sha.HashFinal();
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020FA RID: 8442 RVA: 0x00089A0C File Offset: 0x00087C0C
		public override void Initialize()
		{
			this.sha.Initialize();
		}

		// Token: 0x04000F71 RID: 3953
		private SHA1Internal sha;
	}
}
