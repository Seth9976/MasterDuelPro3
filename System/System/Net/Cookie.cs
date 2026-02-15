using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a set of properties and methods that are used to manage cookies. This class cannot be inherited.</summary>
	// Token: 0x020003DC RID: 988
	[Serializable]
	public sealed class Cookie
	{
		/// <summary>Gets or sets a comment that the server can add to a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>An optional comment to document intended usage for this <see cref="T:System.Net.Cookie" />.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x00067E3E File Offset: 0x0006603E
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x00067E46 File Offset: 0x00066046
		public string Comment
		{
			get
			{
				return this.m_comment;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_comment = value;
			}
		}

		/// <summary>Gets or sets a URI comment that the server can provide with a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>An optional comment that represents the intended usage of the URI reference for this <see cref="T:System.Net.Cookie" />. The value must conform to URI format.</returns>
		// Token: 0x1700053A RID: 1338
		// (set) Token: 0x06001876 RID: 6262 RVA: 0x00067E59 File Offset: 0x00066059
		public Uri CommentUri
		{
			set
			{
				this.m_commentUri = value;
			}
		}

		/// <summary>Determines whether a page script or other active content can access this cookie.</summary>
		/// <returns>Boolean value that determines whether a page script or other active content can access this cookie.</returns>
		// Token: 0x1700053B RID: 1339
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x00067E62 File Offset: 0x00066062
		public bool HttpOnly
		{
			set
			{
				this.m_httpOnly = value;
			}
		}

		/// <summary>Gets or sets the discard flag set by the server.</summary>
		/// <returns>true if the client is to discard the <see cref="T:System.Net.Cookie" /> at the end of the current session; otherwise, false. The default is false.</returns>
		// Token: 0x1700053C RID: 1340
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x00067E6B File Offset: 0x0006606B
		public bool Discard
		{
			set
			{
				this.m_discard = value;
			}
		}

		/// <summary>Gets or sets the URI for which the <see cref="T:System.Net.Cookie" /> is valid.</summary>
		/// <returns>The URI for which the <see cref="T:System.Net.Cookie" /> is valid.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00067E74 File Offset: 0x00066074
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x00067E7C File Offset: 0x0006607C
		public string Domain
		{
			get
			{
				return this.m_domain;
			}
			set
			{
				this.m_domain = ((value == null) ? string.Empty : value);
				this.m_domain_implicit = false;
				this.m_domainKey = string.Empty;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x00067EA4 File Offset: 0x000660A4
		private string _Domain
		{
			get
			{
				if (!this.Plain && !this.m_domain_implicit && this.m_domain.Length != 0)
				{
					return "$Domain=" + (this.IsQuotedDomain ? "\"" : string.Empty) + this.m_domain + (this.IsQuotedDomain ? "\"" : string.Empty);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the current state of the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>true if the <see cref="T:System.Net.Cookie" /> has expired; otherwise, false. The default is false.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00067F0C File Offset: 0x0006610C
		public bool Expired
		{
			get
			{
				return this.m_expires != DateTime.MinValue && this.m_expires.ToLocalTime() <= DateTime.Now;
			}
		}

		/// <summary>Gets or sets the expiration date and time for the <see cref="T:System.Net.Cookie" /> as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The expiration date and time for the <see cref="T:System.Net.Cookie" /> as a <see cref="T:System.DateTime" /> instance.</returns>
		// Token: 0x17000540 RID: 1344
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x00067F37 File Offset: 0x00066137
		public DateTime Expires
		{
			set
			{
				this.m_expires = value;
			}
		}

		/// <summary>Gets or sets the name for the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The name for the <see cref="T:System.Net.Cookie" />.</returns>
		/// <exception cref="T:System.Net.CookieException">The value specified for a set operation is null or the empty string- or -The value specified for a set operation contained an illegal character. The following characters must not be used inside the <see cref="P:System.Net.Cookie.Name" /> property: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character.</exception>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x00067F40 File Offset: 0x00066140
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x00067F48 File Offset: 0x00066148
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				if (ValidationHelper.IsBlankString(value) || !this.InternalSetName(value))
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Name",
						(value == null) ? "<null>" : value
					}));
				}
			}
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x00067F87 File Offset: 0x00066187
		internal bool InternalSetName(string value)
		{
			if (ValidationHelper.IsBlankString(value) || value[0] == '$' || value.IndexOfAny(Cookie.Reserved2Name) != -1)
			{
				this.m_name = string.Empty;
				return false;
			}
			this.m_name = value;
			return true;
		}

		/// <summary>Gets or sets the URIs to which the <see cref="T:System.Net.Cookie" /> applies.</summary>
		/// <returns>The URIs to which the <see cref="T:System.Net.Cookie" /> applies.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00067FBF File Offset: 0x000661BF
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x00067FC7 File Offset: 0x000661C7
		public string Path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				this.m_path = ((value == null) ? string.Empty : value);
				this.m_path_implicit = false;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00067FE1 File Offset: 0x000661E1
		private string _Path
		{
			get
			{
				if (!this.Plain && !this.m_path_implicit && this.m_path.Length != 0)
				{
					return "$Path=" + this.m_path;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x00068016 File Offset: 0x00066216
		internal bool Plain
		{
			get
			{
				return this.Variant == CookieVariant.Plain;
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00068021 File Offset: 0x00066221
		private static bool IsDomainEqualToHost(string domain, string host)
		{
			return host.Length + 1 == domain.Length && string.Compare(host, 0, domain, 1, host.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00068048 File Offset: 0x00066248
		internal bool VerifySetDefaults(CookieVariant variant, Uri uri, bool isLocalDomain, string localDomain, bool set_default, bool isThrow)
		{
			string host = uri.Host;
			int port = uri.Port;
			string absolutePath = uri.AbsolutePath;
			bool flag = true;
			if (set_default)
			{
				if (this.Version == 0)
				{
					variant = CookieVariant.Plain;
				}
				else if (this.Version == 1 && variant == CookieVariant.Unknown)
				{
					variant = CookieVariant.Rfc2109;
				}
				this.m_cookieVariant = variant;
			}
			if (this.m_name == null || this.m_name.Length == 0 || this.m_name[0] == '$' || this.m_name.IndexOfAny(Cookie.Reserved2Name) != -1)
			{
				if (isThrow)
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Name",
						(this.m_name == null) ? "<null>" : this.m_name
					}));
				}
				return false;
			}
			else if (this.m_value == null || ((this.m_value.Length <= 2 || this.m_value[0] != '"' || this.m_value[this.m_value.Length - 1] != '"') && this.m_value.IndexOfAny(Cookie.Reserved2Value) != -1))
			{
				if (isThrow)
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Value",
						(this.m_value == null) ? "<null>" : this.m_value
					}));
				}
				return false;
			}
			else if (this.Comment != null && (this.Comment.Length <= 2 || this.Comment[0] != '"' || this.Comment[this.Comment.Length - 1] != '"') && this.Comment.IndexOfAny(Cookie.Reserved2Value) != -1)
			{
				if (isThrow)
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Comment", this.Comment }));
				}
				return false;
			}
			else
			{
				if (this.Path == null || (this.Path.Length > 2 && this.Path[0] == '"' && this.Path[this.Path.Length - 1] == '"') || this.Path.IndexOfAny(Cookie.Reserved2Value) == -1)
				{
					if (set_default && this.m_domain_implicit)
					{
						this.m_domain = host;
					}
					else
					{
						if (!this.m_domain_implicit)
						{
							string text = this.m_domain;
							if (!Cookie.DomainCharsTest(text))
							{
								if (isThrow)
								{
									throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
									{
										"Domain",
										(text == null) ? "<null>" : text
									}));
								}
								return false;
							}
							else
							{
								if (text[0] != '.')
								{
									if (variant != CookieVariant.Rfc2965 && variant != CookieVariant.Plain)
									{
										if (isThrow)
										{
											throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Domain", this.m_domain }));
										}
										return false;
									}
									else
									{
										text = "." + text;
									}
								}
								int num = host.IndexOf('.');
								if (isLocalDomain && string.Compare(localDomain, text, StringComparison.OrdinalIgnoreCase) == 0)
								{
									flag = true;
								}
								else if (text.IndexOf('.', 1, text.Length - 2) == -1)
								{
									if (!Cookie.IsDomainEqualToHost(text, host))
									{
										flag = false;
									}
								}
								else if (variant == CookieVariant.Plain)
								{
									if (!Cookie.IsDomainEqualToHost(text, host) && (host.Length <= text.Length || string.Compare(host, host.Length - text.Length, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) != 0))
									{
										flag = false;
									}
								}
								else if ((num == -1 || text.Length != host.Length - num || string.Compare(host, num, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) != 0) && !Cookie.IsDomainEqualToHost(text, host))
								{
									flag = false;
								}
								if (flag)
								{
									this.m_domainKey = text.ToLower(CultureInfo.InvariantCulture);
								}
							}
						}
						else if (string.Compare(host, this.m_domain, StringComparison.OrdinalIgnoreCase) != 0)
						{
							flag = false;
						}
						if (!flag)
						{
							if (isThrow)
							{
								throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Domain", this.m_domain }));
							}
							return false;
						}
					}
					if (set_default && this.m_path_implicit)
					{
						switch (this.m_cookieVariant)
						{
						case CookieVariant.Plain:
							this.m_path = absolutePath;
							goto IL_04B8;
						case CookieVariant.Rfc2109:
							this.m_path = absolutePath.Substring(0, absolutePath.LastIndexOf('/'));
							goto IL_04B8;
						}
						this.m_path = absolutePath.Substring(0, absolutePath.LastIndexOf('/') + 1);
					}
					else if (!absolutePath.StartsWith(CookieParser.CheckQuoted(this.m_path)))
					{
						if (isThrow)
						{
							throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Path", this.m_path }));
						}
						return false;
					}
					IL_04B8:
					if (set_default && !this.m_port_implicit && this.m_port.Length == 0)
					{
						this.m_port_list = new int[] { port };
					}
					if (!this.m_port_implicit)
					{
						flag = false;
						int[] port_list = this.m_port_list;
						for (int i = 0; i < port_list.Length; i++)
						{
							if (port_list[i] == port)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (isThrow)
							{
								throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", this.m_port }));
							}
							return false;
						}
					}
					return true;
				}
				if (isThrow)
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Path", this.Path }));
				}
				return false;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00068598 File Offset: 0x00066798
		private static bool DomainCharsTest(string name)
		{
			if (name == null || name.Length == 0)
			{
				return false;
			}
			foreach (char c in name)
			{
				if ((c < '0' || c > '9') && c != '.' && c != '-' && (c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && c != '_')
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets or sets a list of TCP ports that the <see cref="T:System.Net.Cookie" /> applies to.</summary>
		/// <returns>The list of TCP ports that the <see cref="T:System.Net.Cookie" /> applies to.</returns>
		/// <exception cref="T:System.Net.CookieException">The value specified for a set operation could not be parsed or is not enclosed in double quotes. </exception>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x000685FB File Offset: 0x000667FB
		// (set) Token: 0x06001889 RID: 6281 RVA: 0x00068604 File Offset: 0x00066804
		public string Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				this.m_port_implicit = false;
				if (value == null || value.Length == 0)
				{
					this.m_port = string.Empty;
					return;
				}
				if (value[0] != '"' || value[value.Length - 1] != '"')
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
				}
				string[] array = value.Split(Cookie.PortSplitDelimiters);
				List<int> list = new List<int>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != string.Empty)
					{
						int num;
						if (!int.TryParse(array[i], out num))
						{
							throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
						}
						if (num < 0 || num > 65535)
						{
							throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
						}
						list.Add(num);
					}
				}
				this.m_port_list = list.ToArray();
				this.m_port = value;
				this.m_version = 1;
				this.m_cookieVariant = CookieVariant.Rfc2965;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00068721 File Offset: 0x00066921
		internal int[] PortList
		{
			get
			{
				return this.m_port_list;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00068729 File Offset: 0x00066929
		private string _Port
		{
			get
			{
				if (!this.m_port_implicit)
				{
					return "$Port" + ((this.m_port.Length == 0) ? string.Empty : ("=" + this.m_port));
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the security level of a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>true if the client is only to return the cookie in subsequent requests if those requests use Secure Hypertext Transfer Protocol (HTTPS); otherwise, false. The default is false.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x00068767 File Offset: 0x00066967
		// (set) Token: 0x0600188D RID: 6285 RVA: 0x0006876F File Offset: 0x0006696F
		public bool Secure
		{
			get
			{
				return this.m_secure;
			}
			set
			{
				this.m_secure = value;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Net.Cookie.Value" /> for the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The <see cref="P:System.Net.Cookie.Value" /> for the <see cref="T:System.Net.Cookie" />.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00068778 File Offset: 0x00066978
		// (set) Token: 0x0600188F RID: 6287 RVA: 0x00068780 File Offset: 0x00066980
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00068793 File Offset: 0x00066993
		internal CookieVariant Variant
		{
			get
			{
				return this.m_cookieVariant;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0006879B File Offset: 0x0006699B
		internal string DomainKey
		{
			get
			{
				if (!this.m_domain_implicit)
				{
					return this.m_domainKey;
				}
				return this.Domain;
			}
		}

		/// <summary>Gets or sets the version of HTTP state maintenance to which the cookie conforms.</summary>
		/// <returns>The version of HTTP state maintenance to which the cookie conforms.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a version is not allowed. </exception>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x000687B2 File Offset: 0x000669B2
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x000687BA File Offset: 0x000669BA
		public int Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_version = value;
				if (value > 0 && this.m_cookieVariant < CookieVariant.Rfc2109)
				{
					this.m_cookieVariant = CookieVariant.Rfc2109;
				}
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x000687E8 File Offset: 0x000669E8
		private string _Version
		{
			get
			{
				if (this.Version != 0)
				{
					return "$Version=" + (this.IsQuotedVersion ? "\"" : string.Empty) + this.m_version.ToString(NumberFormatInfo.InvariantInfo) + (this.IsQuotedVersion ? "\"" : string.Empty);
				}
				return string.Empty;
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00068845 File Offset: 0x00066A45
		internal static IComparer GetComparer()
		{
			return Cookie.staticComparer;
		}

		/// <summary>Overrides the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>Returns true if the <see cref="T:System.Net.Cookie" /> is equal to <paramref name="comparand" />. Two <see cref="T:System.Net.Cookie" /> instances are equal if their <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, <see cref="P:System.Net.Cookie.Path" />, <see cref="P:System.Net.Cookie.Domain" />, and <see cref="P:System.Net.Cookie.Version" /> properties are equal. <see cref="P:System.Net.Cookie.Name" /> and <see cref="P:System.Net.Cookie.Domain" /> string comparisons are case-insensitive.</returns>
		/// <param name="comparand">A reference to a <see cref="T:System.Net.Cookie" />. </param>
		// Token: 0x06001896 RID: 6294 RVA: 0x0006884C File Offset: 0x00066A4C
		public override bool Equals(object comparand)
		{
			if (!(comparand is Cookie))
			{
				return false;
			}
			Cookie cookie = (Cookie)comparand;
			return string.Compare(this.Name, cookie.Name, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Value, cookie.Value, StringComparison.Ordinal) == 0 && string.Compare(this.Path, cookie.Path, StringComparison.Ordinal) == 0 && string.Compare(this.Domain, cookie.Domain, StringComparison.OrdinalIgnoreCase) == 0 && this.Version == cookie.Version;
		}

		/// <summary>Overrides the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The 32-bit signed integer hash code for this instance.</returns>
		// Token: 0x06001897 RID: 6295 RVA: 0x000688CC File Offset: 0x00066ACC
		public override int GetHashCode()
		{
			return string.Concat(new string[]
			{
				this.Name,
				"=",
				this.Value,
				";",
				this.Path,
				"; ",
				this.Domain,
				"; ",
				this.Version.ToString()
			}).GetHashCode();
		}

		/// <summary>Overrides the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>Returns a string representation of this <see cref="T:System.Net.Cookie" /> object that is suitable for including in a HTTP Cookie: request header.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001898 RID: 6296 RVA: 0x00068940 File Offset: 0x00066B40
		public override string ToString()
		{
			string domain = this._Domain;
			string path = this._Path;
			string port = this._Port;
			string version = this._Version;
			string text = string.Concat(new string[]
			{
				(version.Length == 0) ? string.Empty : (version + "; "),
				this.Name,
				"=",
				this.Value,
				(path.Length == 0) ? string.Empty : ("; " + path),
				(domain.Length == 0) ? string.Empty : ("; " + domain),
				(port.Length == 0) ? string.Empty : ("; " + port)
			});
			if (text == "=")
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x04000F85 RID: 3973
		internal static readonly char[] PortSplitDelimiters = new char[] { ' ', ',', '"' };

		// Token: 0x04000F86 RID: 3974
		internal static readonly char[] Reserved2Name = new char[] { ' ', '\t', '\r', '\n', '=', ';', ',' };

		// Token: 0x04000F87 RID: 3975
		internal static readonly char[] Reserved2Value = new char[] { ';', ',' };

		// Token: 0x04000F88 RID: 3976
		private static Comparer staticComparer = new Comparer();

		// Token: 0x04000F89 RID: 3977
		private string m_comment = string.Empty;

		// Token: 0x04000F8A RID: 3978
		private Uri m_commentUri;

		// Token: 0x04000F8B RID: 3979
		private CookieVariant m_cookieVariant = CookieVariant.Plain;

		// Token: 0x04000F8C RID: 3980
		private bool m_discard;

		// Token: 0x04000F8D RID: 3981
		private string m_domain = string.Empty;

		// Token: 0x04000F8E RID: 3982
		private bool m_domain_implicit = true;

		// Token: 0x04000F8F RID: 3983
		private DateTime m_expires = DateTime.MinValue;

		// Token: 0x04000F90 RID: 3984
		private string m_name = string.Empty;

		// Token: 0x04000F91 RID: 3985
		private string m_path = string.Empty;

		// Token: 0x04000F92 RID: 3986
		private bool m_path_implicit = true;

		// Token: 0x04000F93 RID: 3987
		private string m_port = string.Empty;

		// Token: 0x04000F94 RID: 3988
		private bool m_port_implicit = true;

		// Token: 0x04000F95 RID: 3989
		private int[] m_port_list;

		// Token: 0x04000F96 RID: 3990
		private bool m_secure;

		// Token: 0x04000F97 RID: 3991
		[OptionalField]
		private bool m_httpOnly;

		// Token: 0x04000F98 RID: 3992
		private DateTime m_timeStamp = DateTime.Now;

		// Token: 0x04000F99 RID: 3993
		private string m_value = string.Empty;

		// Token: 0x04000F9A RID: 3994
		private int m_version;

		// Token: 0x04000F9B RID: 3995
		private string m_domainKey = string.Empty;

		// Token: 0x04000F9C RID: 3996
		internal bool IsQuotedVersion;

		// Token: 0x04000F9D RID: 3997
		internal bool IsQuotedDomain;
	}
}
