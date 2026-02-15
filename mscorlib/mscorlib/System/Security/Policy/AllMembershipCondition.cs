using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents a membership condition that matches all code. This class cannot be inherited.</summary>
	// Token: 0x02000337 RID: 823
	[ComVisible(true)]
	[Serializable]
	public sealed class AllMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>Always true.</returns>
		/// <param name="evidence">The evidence set against which to make the test. </param>
		// Token: 0x06001D19 RID: 7449 RVA: 0x0000C091 File Offset: 0x0000A291
		public bool Check(Evidence evidence)
		{
			return true;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		// Token: 0x06001D1A RID: 7450 RVA: 0x000726DE File Offset: 0x000708DE
		public IMembershipCondition Copy()
		{
			return new AllMembershipCondition();
		}

		/// <summary>Determines whether the specified membership condition is an <see cref="T:System.Security.Policy.AllMembershipCondition" />.</summary>
		/// <returns>true if the specified membership condition is an <see cref="T:System.Security.Policy.AllMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to <see cref="T:System.Security.Policy.AllMembershipCondition" />. </param>
		// Token: 0x06001D1B RID: 7451 RVA: 0x000726E5 File Offset: 0x000708E5
		public override bool Equals(object o)
		{
			return o is AllMembershipCondition;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		// Token: 0x06001D1C RID: 7452 RVA: 0x000726F0 File Offset: 0x000708F0
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level context used to resolve named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06001D1D RID: 7453 RVA: 0x000726FA File Offset: 0x000708FA
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		// Token: 0x06001D1E RID: 7454 RVA: 0x00072714 File Offset: 0x00070914
		public override int GetHashCode()
		{
			return typeof(AllMembershipCondition).GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A representation of the membership condition.</returns>
		// Token: 0x06001D1F RID: 7455 RVA: 0x00072725 File Offset: 0x00070925
		public override string ToString()
		{
			return "All code";
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001D20 RID: 7456 RVA: 0x0007272C File Offset: 0x0007092C
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level context for resolving named permission set references. </param>
		// Token: 0x06001D21 RID: 7457 RVA: 0x00072735 File Offset: 0x00070935
		public SecurityElement ToXml(PolicyLevel level)
		{
			return MembershipConditionHelper.Element(typeof(AllMembershipCondition), this.version);
		}

		// Token: 0x04000D79 RID: 3449
		private readonly int version = 1;
	}
}
