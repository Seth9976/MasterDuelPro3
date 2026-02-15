using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Policy
{
	/// <summary>Provides the Authenticode X.509v3 digital signature of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000336 RID: 822
	[Serializable]
	public sealed class Publisher : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Publisher" /> class with the Authenticode X.509v3 certificate containing the publisher's public key.</summary>
		/// <param name="cert">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> that contains the software publisher's public key. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is null. </exception>
		// Token: 0x06001D13 RID: 7443 RVA: 0x000726B6 File Offset: 0x000708B6
		public Publisher(X509Certificate cert)
		{
		}

		/// <summary>Creates an identity permission that corresponds to the current instance of the <see cref="T:System.Security.Policy.Publisher" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Publisher" />.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission. </param>
		// Token: 0x06001D14 RID: 7444 RVA: 0x000082D2 File Offset: 0x000064D2
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return null;
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Publisher" /> to the specified object for equivalence.</summary>
		/// <returns>true if the two instances of the <see cref="T:System.Security.Policy.Publisher" /> class are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Publisher" /> to test for equivalence with the current object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="o" /> parameter is not a <see cref="T:System.Security.Policy.Publisher" /> object. </exception>
		// Token: 0x06001D15 RID: 7445 RVA: 0x000726BE File Offset: 0x000708BE
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		/// <summary>Gets the hash code of the current <see cref="P:System.Security.Policy.Publisher.Certificate" />.</summary>
		/// <returns>The hash code of the current <see cref="P:System.Security.Policy.Publisher.Certificate" />.</returns>
		// Token: 0x06001D16 RID: 7446 RVA: 0x0006EF14 File Offset: 0x0006D114
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Publisher" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Publisher" />.</returns>
		// Token: 0x06001D17 RID: 7447 RVA: 0x000726C7 File Offset: 0x000708C7
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
