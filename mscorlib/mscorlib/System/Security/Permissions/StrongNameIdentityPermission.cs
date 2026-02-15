using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for strong names. This class cannot be inherited.</summary>
	// Token: 0x0200036D RID: 877
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermission : CodeAccessPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001E8F RID: 7823 RVA: 0x00078B60 File Offset: 0x00076D60
		public StrongNameIdentityPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this._list = new ArrayList();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> class for the specified strong name identity.</summary>
		/// <param name="blob">The public key defining the strong name identity namespace. </param>
		/// <param name="name">The simple name part of the strong name identity. This corresponds to the name of the assembly. </param>
		/// <param name="version">The version number of the identity. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06001E90 RID: 7824 RVA: 0x00078B98 File Offset: 0x00076D98
		public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this._state = PermissionState.None;
			this._list = new ArrayList();
			this._list.Add(new StrongNameIdentityPermission.SNIP(blob, name, version));
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00078BFC File Offset: 0x00076DFC
		internal StrongNameIdentityPermission(StrongNameIdentityPermission snip)
		{
			this._state = snip._state;
			this._list = new ArrayList(snip._list.Count);
			foreach (object obj in snip._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				this._list.Add(new StrongNameIdentityPermission.SNIP(snip2.PublicKey, snip2.Name, snip2.AssemblyVersion));
			}
		}

		/// <summary>Gets or sets the simple name portion of the strong name identity.</summary>
		/// <returns>The simple name of the identity.</returns>
		/// <exception cref="T:System.ArgumentException">The value is an empty string ("").</exception>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x00078CA0 File Offset: 0x00076EA0
		public string Name
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).Name;
			}
		}

		/// <summary>Gets or sets the public key blob that defines the strong name identity namespace.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> that contains the public key of the identity, or null if there is no key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is set to null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00078CCC File Offset: 0x00076ECC
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).PublicKey;
			}
		}

		/// <summary>Gets or sets the version number of the identity.</summary>
		/// <returns>The version of the identity.</returns>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x00078CF8 File Offset: 0x00076EF8
		public Version Version
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).AssemblyVersion;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06001E95 RID: 7829 RVA: 0x00078D24 File Offset: 0x00076F24
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			return new StrongNameIdentityPermission(this);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid permission element.-or- The <paramref name="e" /> parameter's version number is not valid. </exception>
		// Token: 0x06001E96 RID: 7830 RVA: 0x00078D3C File Offset: 0x00076F3C
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._list.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				using (IEnumerator enumerator = e.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SecurityElement securityElement = (SecurityElement)obj;
						this._list.Add(this.FromSecurityElement(securityElement));
					}
					return;
				}
			}
			this._list.Add(this.FromSecurityElement(e));
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00078DF0 File Offset: 0x00076FF0
		private StrongNameIdentityPermission.SNIP FromSecurityElement(SecurityElement se)
		{
			string text = se.Attribute("Name");
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = StrongNamePublicKeyBlob.FromString(se.Attribute("PublicKeyBlob"));
			string text2 = se.Attribute("AssemblyVersion");
			Version version = ((text2 == null) ? null : new Version(text2));
			return new StrongNameIdentityPermission.SNIP(strongNamePublicKeyBlob, text, version);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission, or null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E98 RID: 7832 RVA: 0x00078E3C File Offset: 0x0007703C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				throw new ArgumentException(Locale.GetText("Wrong permission type."));
			}
			if (this.IsEmpty() || strongNameIdentityPermission.IsEmpty())
			{
				return null;
			}
			if (!this.Match(strongNameIdentityPermission.Name))
			{
				return null;
			}
			string text = ((this.Name.Length < strongNameIdentityPermission.Name.Length) ? this.Name : strongNameIdentityPermission.Name);
			if (!this.Version.Equals(strongNameIdentityPermission.Version))
			{
				return null;
			}
			if (!this.PublicKey.Equals(strongNameIdentityPermission.PublicKey))
			{
				return null;
			}
			return new StrongNameIdentityPermission(this.PublicKey, text, this.Version);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06001E99 RID: 7833 RVA: 0x00078EF0 File Offset: 0x000770F0
		public override bool IsSubsetOf(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsEmpty())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return strongNameIdentityPermission.IsUnrestricted();
			}
			if (strongNameIdentityPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				foreach (object obj2 in strongNameIdentityPermission._list)
				{
					StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj2;
					if (!snip.IsSubsetOf(snip2))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06001E9A RID: 7834 RVA: 0x00078FD4 File Offset: 0x000771D4
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._list.Count > 1)
			{
				using (IEnumerator enumerator = this._list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
						SecurityElement securityElement2 = new SecurityElement("StrongName");
						this.ToSecurityElement(securityElement2, snip);
						securityElement.AddChild(securityElement2);
					}
					return securityElement;
				}
			}
			if (this._list.Count == 1)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)this._list[0];
				if (!this.IsEmpty(snip2))
				{
					this.ToSecurityElement(securityElement, snip2);
				}
			}
			return securityElement;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00079090 File Offset: 0x00077290
		private void ToSecurityElement(SecurityElement se, StrongNameIdentityPermission.SNIP snip)
		{
			if (snip.PublicKey != null)
			{
				se.AddAttribute("PublicKeyBlob", snip.PublicKey.ToString());
			}
			if (snip.Name != null)
			{
				se.AddAttribute("Name", snip.Name);
			}
			if (snip.AssemblyVersion != null)
			{
				se.AddAttribute("AssemblyVersion", snip.AssemblyVersion.ToString());
			}
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The two permissions are not equal and one is a subset of the other.</exception>
		// Token: 0x06001E9C RID: 7836 RVA: 0x000790F8 File Offset: 0x000772F8
		public override IPermission Union(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null || strongNameIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return strongNameIdentityPermission.Copy();
			}
			StrongNameIdentityPermission strongNameIdentityPermission2 = (StrongNameIdentityPermission)this.Copy();
			foreach (object obj in strongNameIdentityPermission._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				if (!this.IsEmpty(snip) && !this.Contains(snip))
				{
					strongNameIdentityPermission2._list.Add(snip);
				}
			}
			return strongNameIdentityPermission2;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000791A8 File Offset: 0x000773A8
		private bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000791B4 File Offset: 0x000773B4
		private bool Contains(StrongNameIdentityPermission.SNIP snip)
		{
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				bool flag = (snip2.PublicKey == null && snip.PublicKey == null) || (snip2.PublicKey != null && snip2.PublicKey.Equals(snip.PublicKey));
				bool flag2 = snip2.IsNameSubsetOf(snip.Name);
				bool flag3 = (snip2.AssemblyVersion == null && snip.AssemblyVersion == null) || (snip2.AssemblyVersion != null && snip2.AssemblyVersion.Equals(snip.AssemblyVersion));
				if (flag && flag2 && flag3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000792A0 File Offset: 0x000774A0
		private bool IsEmpty(StrongNameIdentityPermission.SNIP snip)
		{
			return this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000792F0 File Offset: 0x000774F0
		private bool IsEmpty()
		{
			return !this.IsUnrestricted() && this._list.Count <= 1 && this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00079357 File Offset: 0x00077557
		private StrongNameIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(StrongNameIdentityPermission));
			}
			return strongNameIdentityPermission;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00079378 File Offset: 0x00077578
		private bool Match(string target)
		{
			if (this.Name == null || target == null)
			{
				return false;
			}
			int num = this.Name.LastIndexOf('*');
			int num2 = target.LastIndexOf('*');
			int num3;
			if (num == -1 && num2 == -1)
			{
				num3 = Math.Max(this.Name.Length, target.Length);
			}
			else if (num == -1)
			{
				num3 = num2;
			}
			else if (num2 == -1)
			{
				num3 = num;
			}
			else
			{
				num3 = Math.Min(num, num2);
			}
			return string.Compare(this.Name, 0, target, 0, num3, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x04000E3B RID: 3643
		private static Version defaultVersion = new Version(0, 0);

		// Token: 0x04000E3C RID: 3644
		private PermissionState _state;

		// Token: 0x04000E3D RID: 3645
		private ArrayList _list;

		// Token: 0x0200036E RID: 878
		private struct SNIP
		{
			// Token: 0x06001EA4 RID: 7844 RVA: 0x0007940E File Offset: 0x0007760E
			internal SNIP(StrongNamePublicKeyBlob pk, string name, Version version)
			{
				this.PublicKey = pk;
				this.Name = name;
				this.AssemblyVersion = version;
			}

			// Token: 0x06001EA5 RID: 7845 RVA: 0x00079425 File Offset: 0x00077625
			internal static StrongNameIdentityPermission.SNIP CreateDefault()
			{
				return new StrongNameIdentityPermission.SNIP(null, string.Empty, (Version)StrongNameIdentityPermission.defaultVersion.Clone());
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x00079444 File Offset: 0x00077644
			internal bool IsNameSubsetOf(string target)
			{
				if (this.Name == null)
				{
					return target == null;
				}
				if (target == null)
				{
					return true;
				}
				int num = this.Name.LastIndexOf('*');
				if (num == 0)
				{
					return true;
				}
				if (num == -1)
				{
					num = this.Name.Length;
				}
				return string.Compare(this.Name, 0, target, 0, num, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x06001EA7 RID: 7847 RVA: 0x000794A0 File Offset: 0x000776A0
			internal bool IsSubsetOf(StrongNameIdentityPermission.SNIP target)
			{
				return (this.PublicKey != null && this.PublicKey.Equals(target.PublicKey)) || (this.IsNameSubsetOf(target.Name) && (!(this.AssemblyVersion != null) || this.AssemblyVersion.Equals(target.AssemblyVersion)) && this.PublicKey == null && target.PublicKey == null);
			}

			// Token: 0x04000E3E RID: 3646
			public StrongNamePublicKeyBlob PublicKey;

			// Token: 0x04000E3F RID: 3647
			public string Name;

			// Token: 0x04000E40 RID: 3648
			public Version AssemblyVersion;
		}
	}
}
