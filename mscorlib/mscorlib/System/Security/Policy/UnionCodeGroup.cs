using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents a code group whose policy statement is the union of the current code group's policy statement and the policy statement of all its matching child code groups. This class cannot be inherited.</summary>
	// Token: 0x0200034D RID: 845
	[ComVisible(true)]
	[Serializable]
	public sealed class UnionCodeGroup : CodeGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.UnionCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy. </param>
		/// <param name="policy">The policy statement for the code group in the form of a permission set and attributes to grant code that matches the membership condition. </param>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.-or- The type of the <paramref name="policy" /> parameter is not valid. </exception>
		// Token: 0x06001DCB RID: 7627 RVA: 0x00073AA2 File Offset: 0x00071CA2
		public UnionCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
			: base(membershipCondition, policy)
		{
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00073883 File Offset: 0x00071A83
		internal UnionCodeGroup(SecurityElement e, PolicyLevel level)
			: base(e, level)
		{
		}

		/// <summary>Makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001DCD RID: 7629 RVA: 0x00075746 File Offset: 0x00073946
		public override CodeGroup Copy()
		{
			return this.Copy(true);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00075750 File Offset: 0x00073950
		internal CodeGroup Copy(bool childs)
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup(base.MembershipCondition, base.PolicyStatement);
			unionCodeGroup.Name = base.Name;
			unionCodeGroup.Description = base.Description;
			if (childs)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					unionCodeGroup.AddChild(codeGroup.Copy());
				}
			}
			return unionCodeGroup;
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <returns>A policy statement consisting of the permissions granted by the code group with optional attributes, or null if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">More than one code group (including the parent code group and any child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001DCF RID: 7631 RVA: 0x000757DC File Offset: 0x000739DC
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			PermissionSet permissionSet = base.PolicyStatement.PermissionSet.Copy();
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					PolicyStatement policyStatement = ((CodeGroup)obj).Resolve(evidence);
					if (policyStatement != null)
					{
						permissionSet = permissionSet.Union(policyStatement.PermissionSet);
					}
				}
			}
			PolicyStatement policyStatement2 = base.PolicyStatement.Copy();
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}
	}
}
