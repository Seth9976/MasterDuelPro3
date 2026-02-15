using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the URL from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200034E RID: 846
	[ComVisible(true)]
	[Serializable]
	public sealed class Url : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Url" /> class with the URL from which a code assembly originates.</summary>
		/// <param name="name">The URL of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06001DD0 RID: 7632 RVA: 0x00075894 File Offset: 0x00073A94
		public Url(string name)
			: this(name, false)
		{
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0007589E File Offset: 0x00073A9E
		internal Url(string name, bool validated)
		{
			this.origin_url = (validated ? name : this.Prepare(name));
		}

		/// <summary>Creates an identity permission corresponding to the current instance of the <see cref="T:System.Security.Policy.Url" /> evidence class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Url" /> evidence.</returns>
		/// <param name="evidence">The evidence set from which to construct the identity permission. </param>
		// Token: 0x06001DD2 RID: 7634 RVA: 0x000758B9 File Offset: 0x00073AB9
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new UrlIdentityPermission(this.origin_url);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Url" /> evidence object to the specified object for equivalence.</summary>
		/// <returns>true if the two <see cref="T:System.Security.Policy.Url" /> objects are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Url" /> evidence object to test for equivalence with the current object. </param>
		// Token: 0x06001DD3 RID: 7635 RVA: 0x000758C8 File Offset: 0x00073AC8
		public override bool Equals(object o)
		{
			Url url = o as Url;
			if (url == null)
			{
				return false;
			}
			string text = url.Value;
			string text2 = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			if (text2.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text2 = "file://" + text2;
			}
			return string.Compare(text, text2, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Gets the hash code of the current URL.</summary>
		/// <returns>The hash code of the current URL.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001DD4 RID: 7636 RVA: 0x00075934 File Offset: 0x00073B34
		public override int GetHashCode()
		{
			string text = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			return text.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Url" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Url" />.</returns>
		// Token: 0x06001DD5 RID: 7637 RVA: 0x00075968 File Offset: 0x00073B68
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Url", this.origin_url));
			return securityElement.ToString();
		}

		/// <summary>Gets the URL from which the code assembly originates.</summary>
		/// <returns>The URL from which the code assembly originates.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0007599F File Offset: 0x00073B9F
		public string Value
		{
			get
			{
				return this.origin_url;
			}
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000759A8 File Offset: 0x00073BA8
		private string Prepare(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("Url");
			}
			if (url == string.Empty)
			{
				throw new FormatException(Locale.GetText("Invalid (empty) Url"));
			}
			if (url.IndexOf(Uri.SchemeDelimiter) > 0)
			{
				if (url.StartsWith("file://"))
				{
					url = "file://" + url.Substring(7);
				}
				url = new Uri(url, false, false).ToString();
			}
			int num = url.Length - 1;
			if (url[num] == '/')
			{
				url = url.Substring(0, num);
			}
			return url;
		}

		// Token: 0x04000DBB RID: 3515
		private string origin_url;
	}
}
