using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the Web site from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200034A RID: 842
	[ComVisible(true)]
	[Serializable]
	public sealed class Site : EvidenceBase, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Site" /> class with the website from which a code assembly originates.</summary>
		/// <param name="name">The website of origin for the associated code assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06001DAA RID: 7594 RVA: 0x0007502C File Offset: 0x0007322C
		public Site(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!Site.IsValid(name))
			{
				throw new ArgumentException(Locale.GetText("name is not valid"));
			}
			this.origin_site = name;
		}

		/// <summary>Creates a new <see cref="T:System.Security.Policy.Site" /> object from the specified URL.</summary>
		/// <returns>A new site object.</returns>
		/// <param name="url">The URL from which to create the new <see cref="T:System.Security.Policy.Site" /> object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter is not a valid URL. -or-The <paramref name="url" /> parameter is a file name.</exception>
		// Token: 0x06001DAB RID: 7595 RVA: 0x00075064 File Offset: 0x00073264
		public static Site CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new FormatException(Locale.GetText("Empty URL."));
			}
			string text = Site.UrlToSite(url);
			if (text == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid URL '{0}'."), url), "url");
			}
			return new Site(text);
		}

		/// <summary>Creates an identity permission that corresponds to the current <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A site identity permission for the current <see cref="T:System.Security.Policy.Site" /> object.</returns>
		/// <param name="evidence">The evidence from which to construct the identity permission. </param>
		// Token: 0x06001DAC RID: 7596 RVA: 0x000750C0 File Offset: 0x000732C0
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new SiteIdentityPermission(this.origin_site);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Site" /> to the specified object for equivalence.</summary>
		/// <returns>true if the two instances of the <see cref="T:System.Security.Policy.Site" /> class are equal; otherwise, false.</returns>
		/// <param name="o">The object to test for equivalence with the current object. </param>
		// Token: 0x06001DAD RID: 7597 RVA: 0x000750D0 File Offset: 0x000732D0
		public override bool Equals(object o)
		{
			Site site = o as Site;
			return site != null && string.Compare(site.Name, this.origin_site, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Returns the hash code of the current website name.</summary>
		/// <returns>The hash code of the current website name.</returns>
		// Token: 0x06001DAE RID: 7598 RVA: 0x00075103 File Offset: 0x00073303
		public override int GetHashCode()
		{
			return this.origin_site.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A representation of the current site.</returns>
		// Token: 0x06001DAF RID: 7599 RVA: 0x00075110 File Offset: 0x00073310
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Name", this.origin_site));
			return securityElement.ToString();
		}

		/// <summary>Gets the website from which the code assembly originates.</summary>
		/// <returns>The name of the website from which the code assembly originates.</returns>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00075147 File Offset: 0x00073347
		public string Name
		{
			get
			{
				return this.origin_site;
			}
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00075150 File Offset: 0x00073350
		internal static bool IsValid(string name)
		{
			if (name == string.Empty)
			{
				return false;
			}
			if (name.Length == 1 && name == ".")
			{
				return false;
			}
			string[] array = name.Split('.', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (i != 0 || !(text == "*"))
				{
					string text2 = text;
					for (int j = 0; j < text2.Length; j++)
					{
						int num = Convert.ToInt32(text2[j]);
						if (num != 33 && num != 45 && (num < 35 || num > 41) && (num < 48 || num > 57) && (num < 64 || num > 90) && (num < 94 || num > 95) && (num < 97 || num > 123) && (num < 125 || num > 126))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00075244 File Offset: 0x00073444
		internal static string UrlToSite(string url)
		{
			if (url == null)
			{
				return null;
			}
			Uri uri = new Uri(url);
			if (uri.Scheme == Uri.UriSchemeFile)
			{
				return null;
			}
			string host = uri.Host;
			if (!Site.IsValid(host))
			{
				return null;
			}
			return host;
		}

		// Token: 0x04000DB3 RID: 3507
		internal string origin_site;
	}
}
