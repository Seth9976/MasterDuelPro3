using System;
using System.Collections.Generic;

namespace System
{
	/// <summary>Parses a new URI scheme. This is an abstract class.</summary>
	// Token: 0x02000100 RID: 256
	public abstract class UriParser
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001AE2D File Offset: 0x0001902D
		internal string SchemeName
		{
			get
			{
				return this.m_Scheme;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001AE35 File Offset: 0x00019035
		internal int DefaultPort
		{
			get
			{
				return this.m_Port;
			}
		}

		/// <summary>Invoked by a <see cref="T:System.Uri" /> constructor to get a <see cref="T:System.UriParser" /> instance</summary>
		/// <returns>A <see cref="T:System.UriParser" /> for the constructed <see cref="T:System.Uri" />.</returns>
		// Token: 0x06000519 RID: 1305 RVA: 0x0001AE3D File Offset: 0x0001903D
		protected virtual UriParser OnNewUri()
		{
			return this;
		}

		/// <summary>Initialize the state of the parser and validate the URI.</summary>
		/// <param name="uri">The T:System.Uri to validate.</param>
		/// <param name="parsingError">Validation errors, if any.</param>
		// Token: 0x0600051A RID: 1306 RVA: 0x0001AE40 File Offset: 0x00019040
		protected virtual void InitializeAndValidate(Uri uri, out UriFormatException parsingError)
		{
			parsingError = uri.ParseMinimal();
		}

		/// <summary>Called by <see cref="T:System.Uri" /> constructors and <see cref="Overload:System.Uri.TryCreate" /> to resolve a relative URI.</summary>
		/// <returns>The string of the resolved relative <see cref="T:System.Uri" />.</returns>
		/// <param name="baseUri">A base URI.</param>
		/// <param name="relativeUri">A relative URI.</param>
		/// <param name="parsingError">Errors during the resolve process, if any.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="baseUri" /> parameter is not an absolute <see cref="T:System.Uri" />- or -<paramref name="baseUri" /> parameter requires user-driven parsing.</exception>
		// Token: 0x0600051B RID: 1307 RVA: 0x0001AE4C File Offset: 0x0001904C
		protected virtual string Resolve(Uri baseUri, Uri relativeUri, out UriFormatException parsingError)
		{
			if (baseUri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.", new object[] { base.GetType().FullName }));
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("This operation is not supported for a relative URI."));
			}
			string text = null;
			bool flag = false;
			Uri uri = Uri.ResolveHelper(baseUri, relativeUri, ref text, ref flag, out parsingError);
			if (parsingError != null)
			{
				return null;
			}
			if (uri != null)
			{
				return uri.OriginalString;
			}
			return text;
		}

		/// <summary>Determines whether <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />.</summary>
		/// <returns>true if <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />; otherwise, false.</returns>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="relativeUri">The URI to test.</param>
		// Token: 0x0600051C RID: 1308 RVA: 0x0001AEC5 File Offset: 0x000190C5
		protected virtual bool IsBaseOf(Uri baseUri, Uri relativeUri)
		{
			return baseUri.IsBaseOfHelper(relativeUri);
		}

		/// <summary>Gets the components from a URI.</summary>
		/// <returns>A string that contains the components.</returns>
		/// <param name="uri">The URI to parse.</param>
		/// <param name="components">The <see cref="T:System.UriComponents" /> to retrieve from <paramref name="uri" />.</param>
		/// <param name="format">One of the <see cref="T:System.UriFormat" /> values that controls how special characters are escaped.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="uriFormat" /> is invalid.- or -<paramref name="uriComponents" /> is not a combination of valid <see cref="T:System.UriComponents" /> values. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="uri" /> requires user-driven parsing- or -<paramref name="uri" /> is not an absolute URI. Relative URIs cannot be used with this method.</exception>
		// Token: 0x0600051D RID: 1309 RVA: 0x0001AED0 File Offset: 0x000190D0
		protected virtual string GetComponents(Uri uri, UriComponents components, UriFormat format)
		{
			if ((components & UriComponents.SerializationInfoString) != (UriComponents)0 && components != UriComponents.SerializationInfoString)
			{
				throw new ArgumentOutOfRangeException("components", components, SR.GetString("UriComponents.SerializationInfoString must not be combined with other UriComponents."));
			}
			if ((format & (UriFormat)(-4)) != (UriFormat)0)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			if (uri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.", new object[] { base.GetType().FullName }));
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("This operation is not supported for a relative URI."));
			}
			return uri.GetComponentsHelper(components, format);
		}

		/// <summary>Indicates whether a URI is well-formed.</summary>
		/// <returns>true if <paramref name="uri" /> is well-formed; otherwise, false.</returns>
		/// <param name="uri">The URI to check.</param>
		// Token: 0x0600051E RID: 1310 RVA: 0x0001AF66 File Offset: 0x00019166
		protected virtual bool IsWellFormedOriginalString(Uri uri)
		{
			return uri.InternalIsWellFormedOriginalString();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0001AF6E File Offset: 0x0001916E
		internal static bool ShouldUseLegacyV2Quirks
		{
			get
			{
				return UriParser.s_QuirksVersion <= UriParser.UriQuirksVersion.V2;
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001AF7C File Offset: 0x0001917C
		static UriParser()
		{
			UriParser.m_Table[UriParser.HttpUri.SchemeName] = UriParser.HttpUri;
			UriParser.HttpsUri = new UriParser.BuiltInUriParser("https", 443, UriParser.HttpUri.m_Flags);
			UriParser.m_Table[UriParser.HttpsUri.SchemeName] = UriParser.HttpsUri;
			UriParser.WsUri = new UriParser.BuiltInUriParser("ws", 80, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.WsUri.SchemeName] = UriParser.WsUri;
			UriParser.WssUri = new UriParser.BuiltInUriParser("wss", 443, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.WssUri.SchemeName] = UriParser.WssUri;
			UriParser.FtpUri = new UriParser.BuiltInUriParser("ftp", 21, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.FtpUri.SchemeName] = UriParser.FtpUri;
			UriParser.FileUri = new UriParser.BuiltInUriParser("file", -1, UriParser.FileSyntaxFlags);
			UriParser.m_Table[UriParser.FileUri.SchemeName] = UriParser.FileUri;
			UriParser.GopherUri = new UriParser.BuiltInUriParser("gopher", 70, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.GopherUri.SchemeName] = UriParser.GopherUri;
			UriParser.NntpUri = new UriParser.BuiltInUriParser("nntp", 119, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NntpUri.SchemeName] = UriParser.NntpUri;
			UriParser.NewsUri = new UriParser.BuiltInUriParser("news", -1, UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NewsUri.SchemeName] = UriParser.NewsUri;
			UriParser.MailToUri = new UriParser.BuiltInUriParser("mailto", 25, UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.MailToUri.SchemeName] = UriParser.MailToUri;
			UriParser.UuidUri = new UriParser.BuiltInUriParser("uuid", -1, UriParser.NewsUri.m_Flags);
			UriParser.m_Table[UriParser.UuidUri.SchemeName] = UriParser.UuidUri;
			UriParser.TelnetUri = new UriParser.BuiltInUriParser("telnet", 23, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.TelnetUri.SchemeName] = UriParser.TelnetUri;
			UriParser.LdapUri = new UriParser.BuiltInUriParser("ldap", 389, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.LdapUri.SchemeName] = UriParser.LdapUri;
			UriParser.NetTcpUri = new UriParser.BuiltInUriParser("net.tcp", 808, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NetTcpUri.SchemeName] = UriParser.NetTcpUri;
			UriParser.NetPipeUri = new UriParser.BuiltInUriParser("net.pipe", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NetPipeUri.SchemeName] = UriParser.NetPipeUri;
			UriParser.VsMacrosUri = new UriParser.BuiltInUriParser("vsmacros", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.VsMacrosUri.SchemeName] = UriParser.VsMacrosUri;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001B31B File Offset: 0x0001951B
		internal UriSyntaxFlags Flags
		{
			get
			{
				return this.m_Flags;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001B323 File Offset: 0x00019523
		internal bool NotAny(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001B32D File Offset: 0x0001952D
		internal bool InFact(UriSyntaxFlags flags)
		{
			return !this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001B33A File Offset: 0x0001953A
		internal bool IsAllSet(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, flags);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001B344 File Offset: 0x00019544
		private bool IsFullMatch(UriSyntaxFlags flags, UriSyntaxFlags expected)
		{
			UriSyntaxFlags uriSyntaxFlags;
			if ((flags & UriSyntaxFlags.UnEscapeDotsAndSlashes) == UriSyntaxFlags.None || !this.m_UpdatableFlagsUsed)
			{
				uriSyntaxFlags = this.m_Flags;
			}
			else
			{
				uriSyntaxFlags = (this.m_Flags & ~UriSyntaxFlags.UnEscapeDotsAndSlashes) | this.m_UpdatableFlags;
			}
			return (uriSyntaxFlags & flags) == expected;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001B389 File Offset: 0x00019589
		internal UriParser(UriSyntaxFlags flags)
		{
			this.m_Flags = flags;
			this.m_Scheme = string.Empty;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001B3A4 File Offset: 0x000195A4
		internal static UriParser FindOrFetchAsUnknownV1Syntax(string lwrCaseScheme)
		{
			UriParser uriParser = null;
			UriParser.m_Table.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser != null)
			{
				return uriParser;
			}
			UriParser.m_TempTable.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser != null)
			{
				return uriParser;
			}
			Dictionary<string, UriParser> table = UriParser.m_Table;
			UriParser uriParser2;
			lock (table)
			{
				if (UriParser.m_TempTable.Count >= 512)
				{
					UriParser.m_TempTable = new Dictionary<string, UriParser>(25);
				}
				uriParser = new UriParser.BuiltInUriParser(lwrCaseScheme, -1, UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
				UriParser.m_TempTable[lwrCaseScheme] = uriParser;
				uriParser2 = uriParser;
			}
			return uriParser2;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001B440 File Offset: 0x00019640
		internal static UriParser GetSyntax(string lwrCaseScheme)
		{
			UriParser uriParser = null;
			UriParser.m_Table.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser == null)
			{
				UriParser.m_TempTable.TryGetValue(lwrCaseScheme, out uriParser);
			}
			return uriParser;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001B46F File Offset: 0x0001966F
		internal bool IsSimple
		{
			get
			{
				return this.InFact(UriSyntaxFlags.SimpleUserSyntax);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001B47C File Offset: 0x0001967C
		internal UriParser InternalOnNewUri()
		{
			UriParser uriParser = this.OnNewUri();
			if (this != uriParser)
			{
				uriParser.m_Scheme = this.m_Scheme;
				uriParser.m_Port = this.m_Port;
				uriParser.m_Flags = this.m_Flags;
			}
			return uriParser;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001B4B9 File Offset: 0x000196B9
		internal void InternalValidate(Uri thisUri, out UriFormatException parsingError)
		{
			this.InitializeAndValidate(thisUri, out parsingError);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001B4C3 File Offset: 0x000196C3
		internal string InternalResolve(Uri thisBaseUri, Uri uriLink, out UriFormatException parsingError)
		{
			return this.Resolve(thisBaseUri, uriLink, out parsingError);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001B4CE File Offset: 0x000196CE
		internal bool InternalIsBaseOf(Uri thisBaseUri, Uri uriLink)
		{
			return this.IsBaseOf(thisBaseUri, uriLink);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001B4D8 File Offset: 0x000196D8
		internal string InternalGetComponents(Uri thisUri, UriComponents uriComponents, UriFormat uriFormat)
		{
			return this.GetComponents(thisUri, uriComponents, uriFormat);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001B4E3 File Offset: 0x000196E3
		internal bool InternalIsWellFormedOriginalString(Uri thisUri)
		{
			return this.IsWellFormedOriginalString(thisUri);
		}

		// Token: 0x0400043F RID: 1087
		private static readonly Dictionary<string, UriParser> m_Table = new Dictionary<string, UriParser>(25);

		// Token: 0x04000440 RID: 1088
		private static Dictionary<string, UriParser> m_TempTable = new Dictionary<string, UriParser>(25);

		// Token: 0x04000441 RID: 1089
		private UriSyntaxFlags m_Flags;

		// Token: 0x04000442 RID: 1090
		private volatile UriSyntaxFlags m_UpdatableFlags;

		// Token: 0x04000443 RID: 1091
		private volatile bool m_UpdatableFlagsUsed;

		// Token: 0x04000444 RID: 1092
		private int m_Port;

		// Token: 0x04000445 RID: 1093
		private string m_Scheme;

		// Token: 0x04000446 RID: 1094
		internal static UriParser HttpUri = new UriParser.BuiltInUriParser("http", 80, UriParser.HttpSyntaxFlags);

		// Token: 0x04000447 RID: 1095
		internal static UriParser HttpsUri;

		// Token: 0x04000448 RID: 1096
		internal static UriParser WsUri;

		// Token: 0x04000449 RID: 1097
		internal static UriParser WssUri;

		// Token: 0x0400044A RID: 1098
		internal static UriParser FtpUri;

		// Token: 0x0400044B RID: 1099
		internal static UriParser FileUri;

		// Token: 0x0400044C RID: 1100
		internal static UriParser GopherUri;

		// Token: 0x0400044D RID: 1101
		internal static UriParser NntpUri;

		// Token: 0x0400044E RID: 1102
		internal static UriParser NewsUri;

		// Token: 0x0400044F RID: 1103
		internal static UriParser MailToUri;

		// Token: 0x04000450 RID: 1104
		internal static UriParser UuidUri;

		// Token: 0x04000451 RID: 1105
		internal static UriParser TelnetUri;

		// Token: 0x04000452 RID: 1106
		internal static UriParser LdapUri;

		// Token: 0x04000453 RID: 1107
		internal static UriParser NetTcpUri;

		// Token: 0x04000454 RID: 1108
		internal static UriParser NetPipeUri;

		// Token: 0x04000455 RID: 1109
		internal static UriParser VsMacrosUri;

		// Token: 0x04000456 RID: 1110
		private static readonly UriParser.UriQuirksVersion s_QuirksVersion = UriParser.UriQuirksVersion.V3;

		// Token: 0x04000457 RID: 1111
		private static readonly UriSyntaxFlags HttpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | (UriParser.ShouldUseLegacyV2Quirks ? UriSyntaxFlags.UnEscapeDotsAndSlashes : UriSyntaxFlags.None) | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000458 RID: 1112
		private static readonly UriSyntaxFlags FileSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | (UriParser.ShouldUseLegacyV2Quirks ? UriSyntaxFlags.None : UriSyntaxFlags.MayHaveQuery) | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x02000101 RID: 257
		private enum UriQuirksVersion
		{
			// Token: 0x0400045A RID: 1114
			V2 = 2,
			// Token: 0x0400045B RID: 1115
			V3
		}

		// Token: 0x02000102 RID: 258
		private class BuiltInUriParser : UriParser
		{
			// Token: 0x06000530 RID: 1328 RVA: 0x0001B4EC File Offset: 0x000196EC
			internal BuiltInUriParser(string lwrCaseScheme, int defaultPort, UriSyntaxFlags syntaxFlags)
				: base(syntaxFlags | UriSyntaxFlags.SimpleUserSyntax | UriSyntaxFlags.BuiltInSyntax)
			{
				this.m_Scheme = lwrCaseScheme;
				this.m_Port = defaultPort;
			}
		}
	}
}
