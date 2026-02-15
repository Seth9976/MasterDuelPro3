using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Provides the strong name of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200034B RID: 843
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongName : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.StrongName" /> class with the strong name public key blob, name, and version.</summary>
		/// <param name="blob">The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the software publisher. </param>
		/// <param name="name">The simple name section of the strong name. </param>
		/// <param name="version">The <see cref="T:System.Version" /> of the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is null.-or- The <paramref name="name" /> parameter is null.-or- The <paramref name="version" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string (""). </exception>
		// Token: 0x06001DB3 RID: 7603 RVA: 0x00075284 File Offset: 0x00073484
		public StrongName(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "name");
			}
			this.publickey = blob;
			this.name = name;
			this.version = version;
		}

		/// <summary>Gets the simple name of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The simple name part of the <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x000752F9 File Offset: 0x000734F9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00075301 File Offset: 0x00073501
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.publickey;
			}
		}

		/// <summary>Gets the <see cref="T:System.Version" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The <see cref="T:System.Version" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x00075309 File Offset: 0x00073509
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A new, identical copy of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06001DB7 RID: 7607 RVA: 0x00075311 File Offset: 0x00073511
		public object Copy()
		{
			return new StrongName(this.publickey, this.name, this.version);
		}

		/// <summary>Creates a <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> that corresponds to the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.StrongName" />.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />. </param>
		// Token: 0x06001DB8 RID: 7608 RVA: 0x0007532A File Offset: 0x0007352A
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new StrongNameIdentityPermission(this.publickey, this.name, this.version);
		}

		/// <summary>Determines whether the specified strong name is equal to the current strong name.</summary>
		/// <returns>true if the specified strong name is equal to the current strong name; otherwise, false.</returns>
		/// <param name="o">The strong name to compare against the current strong name. </param>
		// Token: 0x06001DB9 RID: 7609 RVA: 0x00075344 File Offset: 0x00073544
		public override bool Equals(object o)
		{
			StrongName strongName = o as StrongName;
			return strongName != null && !(this.name != strongName.Name) && this.Version.Equals(strongName.Version) && this.PublicKey.Equals(strongName.PublicKey);
		}

		/// <summary>Gets the hash code of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The hash code of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06001DBA RID: 7610 RVA: 0x00075398 File Offset: 0x00073598
		public override int GetHashCode()
		{
			return this.publickey.GetHashCode();
		}

		/// <summary>Creates a string representation of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06001DBB RID: 7611 RVA: 0x000753A8 File Offset: 0x000735A8
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(typeof(StrongName).Name);
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Key", this.publickey.ToString());
			securityElement.AddAttribute("Name", this.name);
			securityElement.AddAttribute("Version", this.version.ToString());
			return securityElement.ToString();
		}

		// Token: 0x04000DB4 RID: 3508
		private StrongNamePublicKeyBlob publickey;

		// Token: 0x04000DB5 RID: 3509
		private string name;

		// Token: 0x04000DB6 RID: 3510
		private Version version;
	}
}
