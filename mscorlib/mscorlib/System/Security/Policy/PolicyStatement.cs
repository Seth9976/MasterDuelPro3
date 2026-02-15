using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Represents the statement of a <see cref="T:System.Security.Policy.CodeGroup" /> describing the permissions and other information that apply to code with a particular set of evidence. This class cannot be inherited.</summary>
	// Token: 0x02000349 RID: 841
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityEncodable, ISecurityPolicyEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance. </param>
		// Token: 0x06001D9D RID: 7581 RVA: 0x00074E68 File Offset: 0x00073068
		public PolicyStatement(PermissionSet permSet)
			: this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" /> and attributes.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance. </param>
		/// <param name="attributes">A bitwise combination of the <see cref="T:System.Security.Policy.PolicyStatementAttribute" /> values. </param>
		// Token: 0x06001D9E RID: 7582 RVA: 0x00074E72 File Offset: 0x00073072
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet != null)
			{
				this.perms = permSet.Copy();
				this.perms.SetReadOnly(true);
			}
			this.attrs = attributes;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.PermissionSet" /> of the policy statement.</summary>
		/// <returns>The <see cref="T:System.Security.PermissionSet" /> of the policy statement.</returns>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x00074E9C File Offset: 0x0007309C
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x00074EC4 File Offset: 0x000730C4
		public PermissionSet PermissionSet
		{
			get
			{
				if (this.perms == null)
				{
					this.perms = new PermissionSet(PermissionState.None);
					this.perms.SetReadOnly(true);
				}
				return this.perms;
			}
			set
			{
				this.perms = value;
			}
		}

		/// <summary>Gets or sets the attributes of the policy statement.</summary>
		/// <returns>The attributes of the policy statement.</returns>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x00074ECD File Offset: 0x000730CD
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Creates an equivalent copy of the current policy statement.</summary>
		/// <returns>A new copy of the <see cref="T:System.Security.Policy.PolicyStatement" /> with <see cref="P:System.Security.Policy.PolicyStatement.PermissionSet" /> and <see cref="P:System.Security.Policy.PolicyStatement.Attributes" /> identical to those of the current <see cref="T:System.Security.Policy.PolicyStatement" />.</returns>
		// Token: 0x06001DA2 RID: 7586 RVA: 0x00074ED5 File Offset: 0x000730D5
		public PolicyStatement Copy()
		{
			return new PolicyStatement(this.perms, this.attrs);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001DA3 RID: 7587 RVA: 0x00074EE8 File Offset: 0x000730E8
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x00074EF4 File Offset: 0x000730F4
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PolicyStatement")
			{
				throw new ArgumentException(Locale.GetText("Invalid tag."));
			}
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.attrs = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), text);
			}
			SecurityElement securityElement = et.SearchForChildByTag("PermissionSet");
			this.PermissionSet.FromXml(securityElement);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001DA5 RID: 7589 RVA: 0x00074F73 File Offset: 0x00073173
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001DA6 RID: 7590 RVA: 0x00074F7C File Offset: 0x0007317C
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.attrs != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", this.attrs.ToString());
			}
			securityElement.AddChild(this.PermissionSet.ToXml());
			return securityElement;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Policy.PolicyStatement" /> object is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Policy.PolicyStatement" /> is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Policy.PolicyStatement" /> object to compare with the current <see cref="T:System.Security.Policy.PolicyStatement" />. </param>
		// Token: 0x06001DA7 RID: 7591 RVA: 0x00074FDC File Offset: 0x000731DC
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.PermissionSet.Equals(obj) && this.attrs == policyStatement.attrs;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Policy.PolicyStatement" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Policy.PolicyStatement" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x00075018 File Offset: 0x00073218
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.PermissionSet.GetHashCode() ^ (int)this.attrs;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00072A88 File Offset: 0x00070C88
		internal static PolicyStatement Empty()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04000DB1 RID: 3505
		private PermissionSet perms;

		// Token: 0x04000DB2 RID: 3506
		private PolicyStatementAttribute attrs;
	}
}
