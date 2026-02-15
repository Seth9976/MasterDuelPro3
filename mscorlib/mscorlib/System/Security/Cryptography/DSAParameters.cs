using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains the typical parameters for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
	// Token: 0x0200038C RID: 908
	[ComVisible(true)]
	[Serializable]
	public struct DSAParameters
	{
		/// <summary>Specifies the P parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000EBC RID: 3772
		public byte[] P;

		/// <summary>Specifies the Q parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000EBD RID: 3773
		public byte[] Q;

		/// <summary>Specifies the G parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000EBE RID: 3774
		public byte[] G;

		/// <summary>Specifies the Y parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000EBF RID: 3775
		public byte[] Y;

		/// <summary>Specifies the J parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000EC0 RID: 3776
		public byte[] J;

		/// <summary>Specifies the X parameter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000EC1 RID: 3777
		[NonSerialized]
		public byte[] X;

		/// <summary>Specifies the seed for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000EC2 RID: 3778
		public byte[] Seed;

		/// <summary>Specifies the counter for the <see cref="T:System.Security.Cryptography.DSA" /> algorithm.</summary>
		// Token: 0x04000EC3 RID: 3779
		public int Counter;
	}
}
