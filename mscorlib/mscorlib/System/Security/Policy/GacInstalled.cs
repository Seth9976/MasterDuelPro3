using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Confirms that a code assembly originates in the global assembly cache (GAC) as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000342 RID: 834
	[ComVisible(true)]
	[Serializable]
	public sealed class GacInstalled : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Creates a new identity permission that corresponds to the current object.</summary>
		/// <returns>A new identity permission that corresponds to the current object.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission. </param>
		// Token: 0x06001D73 RID: 7539 RVA: 0x00073BC7 File Offset: 0x00071DC7
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new GacIdentityPermission();
		}

		/// <summary>Indicates whether the current object is equivalent to the specified object.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Security.Policy.GacInstalled" /> object; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current object. </param>
		// Token: 0x06001D74 RID: 7540 RVA: 0x00073BCE File Offset: 0x00071DCE
		public override bool Equals(object o)
		{
			return o != null && o is GacInstalled;
		}

		/// <summary>Returns a hash code for the current object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x06001D75 RID: 7541 RVA: 0x00033991 File Offset: 0x00031B91
		public override int GetHashCode()
		{
			return 0;
		}

		/// <summary>Returns a string representation of the current  object.</summary>
		/// <returns>A string representation of the current object.</returns>
		// Token: 0x06001D76 RID: 7542 RVA: 0x00073BDE File Offset: 0x00071DDE
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement.ToString();
		}
	}
}
