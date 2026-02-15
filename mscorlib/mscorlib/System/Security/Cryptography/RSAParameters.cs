using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the standard parameters for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
	// Token: 0x020003A8 RID: 936
	[ComVisible(true)]
	[Serializable]
	public struct RSAParameters
	{
		/// <summary>Represents the Exponent parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000F10 RID: 3856
		public byte[] Exponent;

		/// <summary>Represents the Modulus parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000F11 RID: 3857
		public byte[] Modulus;

		/// <summary>Represents the P parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000F12 RID: 3858
		[NonSerialized]
		public byte[] P;

		/// <summary>Represents the Q parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000F13 RID: 3859
		[NonSerialized]
		public byte[] Q;

		/// <summary>Represents the DP parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000F14 RID: 3860
		[NonSerialized]
		public byte[] DP;

		/// <summary>Represents the DQ parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000F15 RID: 3861
		[NonSerialized]
		public byte[] DQ;

		/// <summary>Represents the InverseQ parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000F16 RID: 3862
		[NonSerialized]
		public byte[] InverseQ;

		/// <summary>Represents the D parameter for the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		// Token: 0x04000F17 RID: 3863
		[NonSerialized]
		public byte[] D;
	}
}
