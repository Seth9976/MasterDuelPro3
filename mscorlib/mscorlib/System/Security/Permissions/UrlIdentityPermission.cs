using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for the URL from which the code originates. This class cannot be inherited.</summary>
	// Token: 0x02000371 RID: 881
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlIdentityPermission : CodeAccessPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" />  values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06001EBB RID: 7867 RVA: 0x000799FB File Offset: 0x00077BFB
		public UrlIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
			this.url = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> class to represent the URL identity described by <paramref name="site" />.</summary>
		/// <param name="site">A URL or wildcard expression. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="site" /> parameter is null. </exception>
		/// <exception cref="T:System.FormatException">The length of the <paramref name="site" /> parameter is zero. </exception>
		/// <exception cref="T:System.ArgumentException">The URL, directory, or site portion of the <paramref name="site" /> parameter is not valid. </exception>
		// Token: 0x06001EBC RID: 7868 RVA: 0x00079A16 File Offset: 0x00077C16
		public UrlIdentityPermission(string site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			this.url = site;
		}

		/// <summary>Gets or sets a URL representing the identity of Internet code.</summary>
		/// <returns>A URL representing the identity of Internet code.</returns>
		/// <exception cref="T:System.NotSupportedException">The URL cannot be retrieved because it has an ambiguous identity.</exception>
		// Token: 0x17000359 RID: 857
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x00079A33 File Offset: 0x00077C33
		public string Url
		{
			set
			{
				this.url = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06001EBE RID: 7870 RVA: 0x00079A46 File Offset: 0x00077C46
		public override IPermission Copy()
		{
			if (this.url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.url);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid.  </exception>
		// Token: 0x06001EBF RID: 7871 RVA: 0x00079A64 File Offset: 0x00077C64
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Url");
			if (text == null)
			{
				this.url = string.Empty;
				return;
			}
			this.Url = text;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The Url property is not a valid URL.</exception>
		// Token: 0x06001EC0 RID: 7872 RVA: 0x00079AA4 File Offset: 0x00077CA4
		public override IPermission Intersect(IPermission target)
		{
			UrlIdentityPermission urlIdentityPermission = this.Cast(target);
			if (urlIdentityPermission == null || this.IsEmpty())
			{
				return null;
			}
			if (!this.Match(urlIdentityPermission.url))
			{
				return null;
			}
			if (this.url.Length > urlIdentityPermission.url.Length)
			{
				return this.Copy();
			}
			return urlIdentityPermission.Copy();
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The Url property is not a valid URL.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001EC1 RID: 7873 RVA: 0x00079AFC File Offset: 0x00077CFC
		public override bool IsSubsetOf(IPermission target)
		{
			UrlIdentityPermission urlIdentityPermission = this.Cast(target);
			if (urlIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsEmpty())
			{
				return true;
			}
			if (urlIdentityPermission.url == null)
			{
				return false;
			}
			int num = urlIdentityPermission.url.LastIndexOf('*');
			if (num == -1)
			{
				num = urlIdentityPermission.url.Length;
			}
			return string.Compare(this.url, 0, urlIdentityPermission.url, 0, num, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information. </returns>
		// Token: 0x06001EC2 RID: 7874 RVA: 0x00079B6C File Offset: 0x00077D6C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (!this.IsEmpty())
			{
				securityElement.AddAttribute("Url", this.url);
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The <see cref="P:System.Security.Permissions.UrlIdentityPermission.Url" /> property is not a valid URL.-or-The two permissions are not equal and one is not a subset of the other.</exception>
		// Token: 0x06001EC3 RID: 7875 RVA: 0x00079B9C File Offset: 0x00077D9C
		public override IPermission Union(IPermission target)
		{
			UrlIdentityPermission urlIdentityPermission = this.Cast(target);
			if (urlIdentityPermission == null)
			{
				return this.Copy();
			}
			if (this.IsEmpty() && urlIdentityPermission.IsEmpty())
			{
				return null;
			}
			if (urlIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return urlIdentityPermission.Copy();
			}
			if (!this.Match(urlIdentityPermission.url))
			{
				throw new ArgumentException(Locale.GetText("Cannot union two different urls."), "target");
			}
			if (this.url.Length < urlIdentityPermission.url.Length)
			{
				return this.Copy();
			}
			return urlIdentityPermission.Copy();
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00079C33 File Offset: 0x00077E33
		private bool IsEmpty()
		{
			return this.url == null || this.url.Length == 0;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00079C4D File Offset: 0x00077E4D
		private UrlIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
			if (urlIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(UrlIdentityPermission));
			}
			return urlIdentityPermission;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00079C70 File Offset: 0x00077E70
		private bool Match(string target)
		{
			if (this.url == null || target == null)
			{
				return false;
			}
			int num = this.url.LastIndexOf('*');
			int num2 = target.LastIndexOf('*');
			int num3;
			if (num == -1 && num2 == -1)
			{
				num3 = Math.Max(this.url.Length, target.Length);
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
			return string.Compare(this.url, 0, target, 0, num3, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x04000E44 RID: 3652
		private string url;
	}
}
