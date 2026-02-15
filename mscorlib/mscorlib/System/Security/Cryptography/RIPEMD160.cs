using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of the MD160 hash algorithm inherit.</summary>
	// Token: 0x020003A6 RID: 934
	[ComVisible(true)]
	public abstract class RIPEMD160 : HashAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RIPEMD160" /> class.</summary>
		// Token: 0x06001FF6 RID: 8182 RVA: 0x0007F916 File Offset: 0x0007DB16
		protected RIPEMD160()
		{
			this.HashSizeValue = 160;
		}
	}
}
