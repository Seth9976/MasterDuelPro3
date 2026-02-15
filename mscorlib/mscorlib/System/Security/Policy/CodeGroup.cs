using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Represents the abstract base class from which all implementations of code groups must derive.</summary>
	// Token: 0x0200033A RID: 826
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Policy.CodeGroup" />.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy. </param>
		/// <param name="policy">The policy statement for the code group in the form of a permission set and attributes to grant code that matches the membership condition. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.-or- The type of the <paramref name="policy" /> parameter is not valid. </exception>
		// Token: 0x06001D29 RID: 7465 RVA: 0x00072AB9 File Offset: 0x00070CB9
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy != null)
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00072AF5 File Offset: 0x00070CF5
		internal CodeGroup(SecurityElement e, PolicyLevel level)
		{
			this.FromXml(e, level);
		}

		/// <summary>When overridden in a derived class, makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06001D2B RID: 7467
		public abstract CodeGroup Copy();

		/// <summary>When overridden in a derived class, resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <returns>A policy statement that consists of the permissions granted by the code group with optional attributes, or null if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		// Token: 0x06001D2C RID: 7468
		public abstract PolicyStatement Resolve(Evidence evidence);

		/// <summary>Gets or sets the policy statement associated with the code group.</summary>
		/// <returns>The policy statement for the code group.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x00072B10 File Offset: 0x00070D10
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x00072B18 File Offset: 0x00070D18
		public PolicyStatement PolicyStatement
		{
			get
			{
				return this.m_policy;
			}
			set
			{
				this.m_policy = value;
			}
		}

		/// <summary>Gets or sets the description of the code group.</summary>
		/// <returns>The description of the code group.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x00072B21 File Offset: 0x00070D21
		// (set) Token: 0x06001D30 RID: 7472 RVA: 0x00072B29 File Offset: 0x00070D29
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		/// <summary>Gets or sets the code group's membership condition.</summary>
		/// <returns>The membership condition that determines to which evidence the code group is applicable.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this parameter to null. </exception>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x00072B32 File Offset: 0x00070D32
		public IMembershipCondition MembershipCondition
		{
			get
			{
				return this.m_membershipCondition;
			}
		}

		/// <summary>Gets or sets the name of the code group.</summary>
		/// <returns>The name of the code group.</returns>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x00072B3A File Offset: 0x00070D3A
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x00072B42 File Offset: 0x00070D42
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets an ordered list of the child code groups of a code group.</summary>
		/// <returns>A list of child code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this property to null. </exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property with a list of children that are not <see cref="T:System.Security.Policy.CodeGroup" /> objects.</exception>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00072B4B File Offset: 0x00070D4B
		public IList Children
		{
			get
			{
				return this.m_children;
			}
		}

		/// <summary>Adds a child code group to the current code group.</summary>
		/// <param name="group">The code group to be added as a child. This new child code group is added to the end of the list. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="group" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not a valid code group. </exception>
		// Token: 0x06001D35 RID: 7477 RVA: 0x00072B53 File Offset: 0x00070D53
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.m_children.Add(group.Copy());
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="o">The code group to compare with the current code group. </param>
		// Token: 0x06001D36 RID: 7478 RVA: 0x00072B78 File Offset: 0x00070D78
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			return codeGroup != null && this.Equals(codeGroup, false);
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group, checking the child code groups as well, if specified.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="cg">The code group to compare with the current code group. </param>
		/// <param name="compareChildren">true to compare child code groups, as well; otherwise, false. </param>
		// Token: 0x06001D37 RID: 7479 RVA: 0x00072B9C File Offset: 0x00070D9C
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (cg.Name != this.Name)
			{
				return false;
			}
			if (cg.Description != this.Description)
			{
				return false;
			}
			if (!cg.MembershipCondition.Equals(this.m_membershipCondition))
			{
				return false;
			}
			if (compareChildren)
			{
				int count = cg.Children.Count;
				if (this.Children.Count != count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					if (!((CodeGroup)this.Children[i]).Equals((CodeGroup)cg.Children[i], false))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06001D38 RID: 7480 RVA: 0x00072C40 File Offset: 0x00070E40
		public override int GetHashCode()
		{
			int num = this.m_membershipCondition.GetHashCode();
			if (this.m_policy != null)
			{
				num += this.m_policy.GetHashCode();
			}
			return num;
		}

		/// <summary>Reconstructs a security object with a given state and policy level from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		// Token: 0x06001D39 RID: 7481 RVA: 0x00072C70 File Offset: 0x00070E70
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			string text = e.Attribute("PermissionSetName");
			PermissionSet permissionSet;
			if (text != null && level != null)
			{
				permissionSet = level.GetNamedPermissionSet(text);
			}
			else
			{
				SecurityElement securityElement = e.SearchForChildByTag("PermissionSet");
				if (securityElement != null)
				{
					permissionSet = (PermissionSet)Activator.CreateInstance(Type.GetType(securityElement.Attribute("class")), true);
					permissionSet.FromXml(securityElement);
				}
				else
				{
					permissionSet = new PermissionSet(new PermissionSet(PermissionState.None));
				}
			}
			this.m_policy = new PolicyStatement(permissionSet);
			this.m_children.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				foreach (object obj in e.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2.Tag == "CodeGroup")
					{
						this.AddChild(CodeGroup.CreateFromXml(securityElement2, level));
					}
				}
			}
			this.m_membershipCondition = null;
			SecurityElement securityElement3 = e.SearchForChildByTag("IMembershipCondition");
			if (securityElement3 != null)
			{
				string text2 = securityElement3.Attribute("class");
				Type type = Type.GetType(text2);
				if (type == null)
				{
					type = Type.GetType("System.Security.Policy." + text2);
				}
				this.m_membershipCondition = (IMembershipCondition)Activator.CreateInstance(type, true);
				this.m_membershipCondition.FromXml(securityElement3, level);
			}
			this.m_name = e.Attribute("Name");
			this.m_description = e.Attribute("Description");
			this.ParseXml(e, level);
		}

		/// <summary>When overridden in a derived class, reconstructs properties and internal state specific to a derived code group from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		// Token: 0x06001D3A RID: 7482 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001D3B RID: 7483 RVA: 0x00072E18 File Offset: 0x00071018
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object, its current state, and the policy level within which the code exists.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level within which the code group exists. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001D3C RID: 7484 RVA: 0x00072E24 File Offset: 0x00071024
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.Name != null)
			{
				securityElement.AddAttribute("Name", this.Name);
			}
			if (this.Description != null)
			{
				securityElement.AddAttribute("Description", this.Description);
			}
			if (this.MembershipCondition != null)
			{
				securityElement.AddChild(this.MembershipCondition.ToXml());
			}
			if (this.PolicyStatement != null && this.PolicyStatement.PermissionSet != null)
			{
				securityElement.AddChild(this.PolicyStatement.PermissionSet.ToXml());
			}
			foreach (object obj in this.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				securityElement.AddChild(codeGroup.ToXml());
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		/// <summary>When overridden in a derived class, serializes properties and internal state specific to a derived code group and adds the serialization to the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="element">The XML encoding to which to add the serialization. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		// Token: 0x06001D3D RID: 7485 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00072F34 File Offset: 0x00071134
		internal static CodeGroup CreateFromXml(SecurityElement se, PolicyLevel level)
		{
			string text = se.Attribute("class");
			string text2 = text;
			int num = text2.IndexOf(",");
			if (num > 0)
			{
				text2 = text2.Substring(0, num);
			}
			num = text2.LastIndexOf(".");
			if (num > 0)
			{
				text2 = text2.Substring(num + 1);
			}
			if (text2 == "FileCodeGroup")
			{
				return new FileCodeGroup(se, level);
			}
			if (text2 == "FirstMatchCodeGroup")
			{
				return new FirstMatchCodeGroup(se, level);
			}
			if (text2 == "NetCodeGroup")
			{
				return new NetCodeGroup(se, level);
			}
			if (!(text2 == "UnionCodeGroup"))
			{
				CodeGroup codeGroup = (CodeGroup)Activator.CreateInstance(Type.GetType(text), true);
				codeGroup.FromXml(se, level);
				return codeGroup;
			}
			return new UnionCodeGroup(se, level);
		}

		// Token: 0x04000D84 RID: 3460
		private PolicyStatement m_policy;

		// Token: 0x04000D85 RID: 3461
		private IMembershipCondition m_membershipCondition;

		// Token: 0x04000D86 RID: 3462
		private string m_description;

		// Token: 0x04000D87 RID: 3463
		private string m_name;

		// Token: 0x04000D88 RID: 3464
		private ArrayList m_children = new ArrayList();
	}
}
