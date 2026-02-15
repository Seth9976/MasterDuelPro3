using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains information about the properties of a digital signature.</summary>
	// Token: 0x020003B3 RID: 947
	[ComVisible(true)]
	public class SignatureDescription
	{
		/// <summary>Gets or sets the key algorithm for the signature description.</summary>
		/// <returns>The key algorithm for the signature description.</returns>
		// Token: 0x1700038B RID: 907
		// (set) Token: 0x06002068 RID: 8296 RVA: 0x000842F2 File Offset: 0x000824F2
		public string KeyAlgorithm
		{
			set
			{
				this._strKey = value;
			}
		}

		/// <summary>Gets or sets the digest algorithm for the signature description.</summary>
		/// <returns>The digest algorithm for the signature description.</returns>
		// Token: 0x1700038C RID: 908
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x000842FB File Offset: 0x000824FB
		public string DigestAlgorithm
		{
			set
			{
				this._strDigest = value;
			}
		}

		/// <summary>Gets or sets the formatter algorithm for the signature description.</summary>
		/// <returns>The formatter algorithm for the signature description.</returns>
		// Token: 0x1700038D RID: 909
		// (set) Token: 0x0600206A RID: 8298 RVA: 0x00084304 File Offset: 0x00082504
		public string FormatterAlgorithm
		{
			set
			{
				this._strFormatter = value;
			}
		}

		/// <summary>Gets or sets the deformatter algorithm for the signature description.</summary>
		/// <returns>The deformatter algorithm for the signature description.</returns>
		// Token: 0x1700038E RID: 910
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x0008430D File Offset: 0x0008250D
		public string DeformatterAlgorithm
		{
			set
			{
				this._strDeformatter = value;
			}
		}

		// Token: 0x04000F32 RID: 3890
		private string _strKey;

		// Token: 0x04000F33 RID: 3891
		private string _strDigest;

		// Token: 0x04000F34 RID: 3892
		private string _strFormatter;

		// Token: 0x04000F35 RID: 3893
		private string _strDeformatter;
	}
}
