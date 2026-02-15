using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent a signature.</summary>
	// Token: 0x02000680 RID: 1664
	[ComVisible(true)]
	public readonly struct SignatureToken : IEquatable<SignatureToken>
	{
		// Token: 0x0600338F RID: 13199 RVA: 0x000C2257 File Offset: 0x000C0457
		internal SignatureToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of SignatureToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of SignatureToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this SignatureToken. </param>
		// Token: 0x06003390 RID: 13200 RVA: 0x000C2260 File Offset: 0x000C0460
		public override bool Equals(object obj)
		{
			bool flag = obj is SignatureToken;
			if (flag)
			{
				SignatureToken signatureToken = (SignatureToken)obj;
				flag = this.tokValue == signatureToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.SignatureToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to the current instance.</param>
		// Token: 0x06003391 RID: 13201 RVA: 0x000C2291 File Offset: 0x000C0491
		public bool Equals(SignatureToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this signature.</summary>
		/// <returns>Returns the hash code for this signature.</returns>
		// Token: 0x06003392 RID: 13202 RVA: 0x000C22A1 File Offset: 0x000C04A1
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x04001B00 RID: 6912
		internal readonly int tokValue;
	}
}
