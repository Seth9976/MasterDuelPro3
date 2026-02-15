using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its strong name. This class cannot be inherited.</summary>
	// Token: 0x0200034C RID: 844
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> class with the strong name public key blob, name, and version number that determine membership.</summary>
		/// <param name="blob">The strong name public key blob of the software publisher. </param>
		/// <param name="name">The simple name section of the strong name. </param>
		/// <param name="version">The version number of the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is null.-or-The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06001DBC RID: 7612 RVA: 0x0007541C File Offset: 0x0007361C
		public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			this.blob = blob;
			this.name = name;
			if (version != null)
			{
				this.assemblyVersion = (Version)version.Clone();
			}
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0007546C File Offset: 0x0007366C
		internal StrongNameMembershipCondition(SecurityElement e)
		{
			this.FromXml(e);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00075482 File Offset: 0x00073682
		internal StrongNameMembershipCondition()
		{
		}

		/// <summary>Gets or sets the simple name of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The simple name of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentException">The value is null.-or-The value is an empty string ("").</exception>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x00075491 File Offset: 0x00073691
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Version" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Version" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x00075499 File Offset: 0x00073699
		public Version Version
		{
			get
			{
				return this.assemblyVersion;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set the <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> to null. </exception>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x000754A1 File Offset: 0x000736A1
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.blob;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test. </param>
		// Token: 0x06001DC2 RID: 7618 RVA: 0x000754AC File Offset: 0x000736AC
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				StrongName strongName = obj as StrongName;
				if (strongName != null)
				{
					return strongName.PublicKey.Equals(this.blob) && (this.name == null || !(this.name != strongName.Name)) && (!(this.assemblyVersion != null) || this.assemblyVersion.Equals(strongName.Version));
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>A new, identical copy of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /></returns>
		// Token: 0x06001DC3 RID: 7619 RVA: 0x00075536 File Offset: 0x00073736
		public IMembershipCondition Copy()
		{
			return new StrongNameMembershipCondition(this.blob, this.name, this.assemblyVersion);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Policy.StrongName" /> from the specified object is equivalent to the <see cref="T:System.Security.Policy.StrongName" /> contained in the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>true if the <see cref="T:System.Security.Policy.StrongName" /> from the specified object is equivalent to the <see cref="T:System.Security.Policy.StrongName" /> contained in the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> property of the current object or the specified object is null. </exception>
		// Token: 0x06001DC4 RID: 7620 RVA: 0x00075550 File Offset: 0x00073750
		public override bool Equals(object o)
		{
			StrongNameMembershipCondition strongNameMembershipCondition = o as StrongNameMembershipCondition;
			if (strongNameMembershipCondition == null)
			{
				return false;
			}
			if (!strongNameMembershipCondition.PublicKey.Equals(this.PublicKey))
			{
				return false;
			}
			if (this.name != strongNameMembershipCondition.Name)
			{
				return false;
			}
			if (this.assemblyVersion != null)
			{
				return this.assemblyVersion.Equals(strongNameMembershipCondition.Version);
			}
			return strongNameMembershipCondition.Version == null;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>The hash code for the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> property is null. </exception>
		// Token: 0x06001DC5 RID: 7621 RVA: 0x000755BF File Offset: 0x000737BF
		public override int GetHashCode()
		{
			return this.blob.GetHashCode();
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		// Token: 0x06001DC6 RID: 7622 RVA: 0x000755CC File Offset: 0x000737CC
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06001DC7 RID: 7623 RVA: 0x000755D8 File Offset: 0x000737D8
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this.blob = StrongNamePublicKeyBlob.FromString(e.Attribute("PublicKeyBlob"));
			this.name = e.Attribute("Name");
			string text = e.Attribute("AssemblyVersion");
			if (text == null)
			{
				this.assemblyVersion = null;
				return;
			}
			this.assemblyVersion = new Version(text);
		}

		/// <summary>Creates and returns a string representation of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</returns>
		// Token: 0x06001DC8 RID: 7624 RVA: 0x00075648 File Offset: 0x00073848
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("StrongName - ");
			stringBuilder.Append(this.blob);
			if (this.name != null)
			{
				stringBuilder.AppendFormat(" name = {0}", this.name);
			}
			if (this.assemblyVersion != null)
			{
				stringBuilder.AppendFormat(" version = {0}", this.assemblyVersion);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06001DC9 RID: 7625 RVA: 0x000756AD File Offset: 0x000738AD
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, which is used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		// Token: 0x06001DCA RID: 7626 RVA: 0x000756B8 File Offset: 0x000738B8
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(StrongNameMembershipCondition), this.version);
			if (this.blob != null)
			{
				securityElement.AddAttribute("PublicKeyBlob", this.blob.ToString());
			}
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.assemblyVersion != null)
			{
				string text = this.assemblyVersion.ToString();
				if (text != "0.0")
				{
					securityElement.AddAttribute("AssemblyVersion", text);
				}
			}
			return securityElement;
		}

		// Token: 0x04000DB7 RID: 3511
		private readonly int version = 1;

		// Token: 0x04000DB8 RID: 3512
		private StrongNamePublicKeyBlob blob;

		// Token: 0x04000DB9 RID: 3513
		private string name;

		// Token: 0x04000DBA RID: 3514
		private Version assemblyVersion;
	}
}
