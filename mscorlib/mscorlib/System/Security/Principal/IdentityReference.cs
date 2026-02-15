using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents an identity and is the base class for the <see cref="T:System.Security.Principal.NTAccount" /> and <see cref="T:System.Security.Principal.SecurityIdentifier" /> classes. This class does not provide a public constructor, and therefore cannot be inherited.</summary>
	// Token: 0x020003D6 RID: 982
	[ComVisible(false)]
	public abstract class IdentityReference
	{
		// Token: 0x06002159 RID: 8537 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal IdentityReference()
		{
		}

		/// <summary>Gets the string value of the identity represented by the <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <returns>The string value of the identity represented by the <see cref="T:System.Security.Principal.IdentityReference" /> object.</returns>
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600215A RID: 8538
		public abstract string Value { get; }

		/// <summary>Returns a value that indicates whether the specified object equals this instance of the <see cref="T:System.Security.Principal.IdentityReference" /> class.</summary>
		/// <returns>true if <paramref name="o" /> is an object with the same underlying type and value as this <see cref="T:System.Security.Principal.IdentityReference" /> instance; otherwise, false.</returns>
		/// <param name="o">An object to compare with this <see cref="T:System.Security.Principal.IdentityReference" /> instance, or a null reference.</param>
		// Token: 0x0600215B RID: 8539
		public abstract override bool Equals(object o);

		/// <summary>Serves as a hash function for <see cref="T:System.Security.Principal.IdentityReference" />. <see cref="M:System.Security.Principal.IdentityReference.GetHashCode" /> is suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>The hash code for this <see cref="T:System.Security.Principal.IdentityReference" /> object.</returns>
		// Token: 0x0600215C RID: 8540
		public abstract override int GetHashCode();

		/// <summary>Returns the string representation of the identity represented by the <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <returns>The identity in string format.</returns>
		// Token: 0x0600215D RID: 8541
		public abstract override string ToString();

		/// <summary>Translates the account name represented by the <see cref="T:System.Security.Principal.IdentityReference" /> object into another <see cref="T:System.Security.Principal.IdentityReference" />-derived type.</summary>
		/// <returns>The converted identity.</returns>
		/// <param name="targetType">The target type for the conversion from <see cref="T:System.Security.Principal.IdentityReference" />. </param>
		// Token: 0x0600215E RID: 8542
		public abstract IdentityReference Translate(Type targetType);
	}
}
