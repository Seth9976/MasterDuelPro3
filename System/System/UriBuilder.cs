using System;

namespace System
{
	/// <summary>Provides a custom constructor for uniform resource identifiers (URIs) and modifies URIs for the <see cref="T:System.Uri" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000ED RID: 237
	public class UriBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class.</summary>
		// Token: 0x06000463 RID: 1123 RVA: 0x00011648 File Offset: 0x0000F848
		public UriBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="uri">An instance of the <see cref="T:System.Uri" /> class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		// Token: 0x06000464 RID: 1124 RVA: 0x000116C4 File Offset: 0x0000F8C4
		public UriBuilder(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.Init(uri);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00011754 File Offset: 0x0000F954
		private void Init(Uri uri)
		{
			this._fragment = uri.Fragment;
			this._query = uri.Query;
			this._host = uri.Host;
			this._path = uri.AbsolutePath;
			this._port = uri.Port;
			this._scheme = uri.Scheme;
			this._schemeDelimiter = (uri.HasAuthority ? Uri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (!string.IsNullOrEmpty(userInfo))
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this._password = userInfo.Substring(num + 1);
					this._username = userInfo.Substring(0, num);
				}
				else
				{
					this._username = userInfo;
				}
			}
			this.SetFieldsFromUri(uri);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme and host.</summary>
		/// <param name="schemeName">An Internet access protocol. </param>
		/// <param name="hostName">A DNS-style domain name or IP address. </param>
		// Token: 0x06000466 RID: 1126 RVA: 0x0001180C File Offset: 0x0000FA0C
		public UriBuilder(string schemeName, string hostName)
		{
			this.Scheme = schemeName;
			this.Host = hostName;
		}

		/// <summary>Gets or sets the Domain Name System (DNS) host name or IP address of a server.</summary>
		/// <returns>The DNS host name or IP address of the server.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BD RID: 189
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00011894 File Offset: 0x0000FA94
		public string Host
		{
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this._host = value;
				if (this._host.IndexOf(':') >= 0 && this._host[0] != '[')
				{
					this._host = "[" + this._host + "]";
				}
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the path to the resource referenced by the URI.</summary>
		/// <returns>The path to the resource referenced by the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BE RID: 190
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000118F4 File Offset: 0x0000FAF4
		public string Path
		{
			set
			{
				if (value == null || value.Length == 0)
				{
					value = "/";
				}
				this._path = Uri.InternalEscapeString(value.Replace('\\', '/'));
				this._changed = true;
			}
		}

		/// <summary>Gets or sets any query information included in the URI.</summary>
		/// <returns>The query information included in the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00011924 File Offset: 0x0000FB24
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0001192C File Offset: 0x0000FB2C
		public string Query
		{
			get
			{
				return this._query;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0 && value[0] != '?')
				{
					value = "?" + value;
				}
				this._query = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the scheme name of the URI.</summary>
		/// <returns>The scheme of the URI.</returns>
		/// <exception cref="T:System.ArgumentException">The scheme cannot be set to an invalid scheme name. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C0 RID: 192
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00011968 File Offset: 0x0000FB68
		public string Scheme
		{
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				int num = value.IndexOf(':');
				if (num != -1)
				{
					value = value.Substring(0, num);
				}
				if (value.Length != 0)
				{
					if (!Uri.CheckSchemeName(value))
					{
						throw new ArgumentException("Invalid URI: The URI scheme is not valid.", "value");
					}
					value = value.ToLowerInvariant();
				}
				this._scheme = value;
				this._changed = true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> instance constructed by the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI constructed by the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The URI constructed by the <see cref="T:System.UriBuilder" /> properties is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000119CC File Offset: 0x0000FBCC
		public Uri Uri
		{
			get
			{
				if (this._changed)
				{
					this._uri = new Uri(this.ToString());
					this.SetFieldsFromUri(this._uri);
					this._changed = false;
				}
				return this._uri;
			}
		}

		/// <summary>Compares an existing <see cref="T:System.Uri" /> instance with the contents of the <see cref="T:System.UriBuilder" /> for equality.</summary>
		/// <returns>true if <paramref name="rparam" /> represents the same <see cref="T:System.Uri" /> as the <see cref="T:System.Uri" /> constructed by this <see cref="T:System.UriBuilder" /> instance; otherwise, false.</returns>
		/// <param name="rparam">The object to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600046D RID: 1133 RVA: 0x00011A00 File Offset: 0x0000FC00
		public override bool Equals(object rparam)
		{
			return rparam != null && this.Uri.Equals(rparam.ToString());
		}

		/// <summary>Returns the hash code for the URI.</summary>
		/// <returns>The hash code generated for the URI.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600046E RID: 1134 RVA: 0x00011A18 File Offset: 0x0000FC18
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00011A28 File Offset: 0x0000FC28
		private void SetFieldsFromUri(Uri uri)
		{
			this._fragment = uri.Fragment;
			this._query = uri.Query;
			this._host = uri.Host;
			this._path = uri.AbsolutePath;
			this._port = uri.Port;
			this._scheme = uri.Scheme;
			this._schemeDelimiter = (uri.HasAuthority ? Uri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (userInfo.Length > 0)
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this._password = userInfo.Substring(num + 1);
					this._username = userInfo.Substring(0, num);
					return;
				}
				this._username = userInfo;
			}
		}

		/// <summary>Returns the display string for the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>The string that contains the unescaped display string of the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The <see cref="T:System.UriBuilder" /> instance has a bad password. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000470 RID: 1136 RVA: 0x00011ADC File Offset: 0x0000FCDC
		public override string ToString()
		{
			if (this._username.Length == 0 && this._password.Length > 0)
			{
				throw new UriFormatException("Invalid URI: The username:password construct is badly formed.");
			}
			if (this._scheme.Length != 0)
			{
				UriParser syntax = UriParser.GetSyntax(this._scheme);
				if (syntax != null)
				{
					this._schemeDelimiter = ((syntax.InFact(UriSyntaxFlags.MustHaveAuthority) || (this._host.Length != 0 && syntax.NotAny(UriSyntaxFlags.MailToLikeUri) && syntax.InFact(UriSyntaxFlags.OptionalAuthority))) ? Uri.SchemeDelimiter : ":");
				}
				else
				{
					this._schemeDelimiter = ((this._host.Length != 0) ? Uri.SchemeDelimiter : ":");
				}
			}
			string text = ((this._scheme.Length != 0) ? (this._scheme + this._schemeDelimiter) : string.Empty);
			return string.Concat(new string[]
			{
				text,
				this._username,
				(this._password.Length > 0) ? (":" + this._password) : string.Empty,
				(this._username.Length > 0) ? "@" : string.Empty,
				this._host,
				(this._port != -1 && this._host.Length > 0) ? (":" + this._port.ToString()) : string.Empty,
				(this._host.Length > 0 && this._path.Length != 0 && this._path[0] != '/') ? "/" : string.Empty,
				this._path,
				this._query,
				this._fragment
			});
		}

		// Token: 0x04000383 RID: 899
		private bool _changed = true;

		// Token: 0x04000384 RID: 900
		private string _fragment = string.Empty;

		// Token: 0x04000385 RID: 901
		private string _host = "localhost";

		// Token: 0x04000386 RID: 902
		private string _password = string.Empty;

		// Token: 0x04000387 RID: 903
		private string _path = "/";

		// Token: 0x04000388 RID: 904
		private int _port = -1;

		// Token: 0x04000389 RID: 905
		private string _query = string.Empty;

		// Token: 0x0400038A RID: 906
		private string _scheme = "http";

		// Token: 0x0400038B RID: 907
		private string _schemeDelimiter = Uri.SchemeDelimiter;

		// Token: 0x0400038C RID: 908
		private Uri _uri;

		// Token: 0x0400038D RID: 909
		private string _username = string.Empty;
	}
}
