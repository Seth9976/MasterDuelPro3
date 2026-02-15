using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Grants Web permission to the site from which the assembly was downloaded. This class cannot be inherited.</summary>
	// Token: 0x02000345 RID: 837
	[ComVisible(true)]
	[Serializable]
	public sealed class NetCodeGroup : CodeGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.NetCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies code access security policy. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid. </exception>
		// Token: 0x06001D80 RID: 7552 RVA: 0x00073E64 File Offset: 0x00072064
		public NetCodeGroup(IMembershipCondition membershipCondition)
			: base(membershipCondition, null)
		{
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x00073E79 File Offset: 0x00072079
		internal NetCodeGroup(SecurityElement e, PolicyLevel level)
			: base(e, level)
		{
		}

		/// <summary>Makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06001D82 RID: 7554 RVA: 0x00073E90 File Offset: 0x00072090
		public override CodeGroup Copy()
		{
			NetCodeGroup netCodeGroup = new NetCodeGroup(base.MembershipCondition);
			netCodeGroup.Name = base.Name;
			netCodeGroup.Description = base.Description;
			netCodeGroup.PolicyStatement = base.PolicyStatement;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				netCodeGroup.AddChild(codeGroup.Copy());
			}
			return netCodeGroup;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00073F20 File Offset: 0x00072120
		private bool Equals(CodeConnectAccess[] rules1, CodeConnectAccess[] rules2)
		{
			for (int i = 0; i < rules1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < rules2.Length; j++)
				{
					if (rules1[i].Equals(rules2[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.NetCodeGroup" /> object to compare with the current code group.</param>
		// Token: 0x06001D84 RID: 7556 RVA: 0x00073F64 File Offset: 0x00072164
		public override bool Equals(object o)
		{
			if (!base.Equals(o))
			{
				return false;
			}
			NetCodeGroup netCodeGroup = o as NetCodeGroup;
			if (netCodeGroup == null)
			{
				return false;
			}
			foreach (object obj in this._rules)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				CodeConnectAccess[] array = (CodeConnectAccess[])netCodeGroup._rules[dictionaryEntry.Key];
				bool flag;
				if (array != null)
				{
					flag = this.Equals((CodeConnectAccess[])dictionaryEntry.Value, array);
				}
				else
				{
					flag = dictionaryEntry.Value == null;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06001D85 RID: 7557 RVA: 0x00074020 File Offset: 0x00072220
		public override int GetHashCode()
		{
			if (this._hashcode == 0)
			{
				this._hashcode = base.GetHashCode();
				foreach (object obj in this._rules)
				{
					CodeConnectAccess[] array = (CodeConnectAccess[])((DictionaryEntry)obj).Value;
					if (array != null)
					{
						foreach (CodeConnectAccess codeConnectAccess in array)
						{
							this._hashcode ^= codeConnectAccess.GetHashCode();
						}
					}
				}
			}
			return this._hashcode;
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyStatement" /> that consists of the permissions granted by the code group with optional attributes, or null if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">More than one code group (including the parent code group and any child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001D86 RID: 7558 RVA: 0x000740D0 File Offset: 0x000722D0
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
			PermissionSet permissionSet = null;
			if (base.PolicyStatement == null)
			{
				permissionSet = new PermissionSet(PermissionState.None);
			}
			else
			{
				permissionSet = base.PolicyStatement.PermissionSet.Copy();
			}
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

		// Token: 0x06001D87 RID: 7559 RVA: 0x0007419C File Offset: 0x0007239C
		[MonoTODO("(2.0) Add new stuff (CodeConnectAccess) into XML")]
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			base.CreateXml(element, level);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000741A6 File Offset: 0x000723A6
		[MonoTODO("(2.0) Parse new stuff (CodeConnectAccess) from XML")]
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			base.ParseXml(e, level);
		}

		/// <summary>Contains a value used to specify connection access for code with an unknown or unrecognized origin scheme.</summary>
		// Token: 0x04000DA2 RID: 3490
		public static readonly string AbsentOriginScheme = string.Empty;

		/// <summary>Contains a value used to specify any other unspecified origin scheme.</summary>
		// Token: 0x04000DA3 RID: 3491
		public static readonly string AnyOtherOriginScheme = "*";

		// Token: 0x04000DA4 RID: 3492
		private Hashtable _rules = new Hashtable();

		// Token: 0x04000DA5 RID: 3493
		private int _hashcode;
	}
}
