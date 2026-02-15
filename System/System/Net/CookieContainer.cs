using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides a container for a collection of <see cref="T:System.Net.CookieCollection" /> objects.</summary>
	// Token: 0x020003E6 RID: 998
	[Serializable]
	public class CookieContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieContainer" /> class.</summary>
		// Token: 0x060018C9 RID: 6345 RVA: 0x00069830 File Offset: 0x00067A30
		public CookieContainer()
		{
			string domainName = IPGlobalProperties.InternalGetIPGlobalProperties().DomainName;
			if (domainName != null && domainName.Length > 1)
			{
				this.m_fqdnMyDomain = "." + domainName;
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000698A0 File Offset: 0x00067AA0
		private void AddRemoveDomain(string key, PathList value)
		{
			object syncRoot = this.m_domainTable.SyncRoot;
			lock (syncRoot)
			{
				if (value == null)
				{
					this.m_domainTable.Remove(key);
				}
				else
				{
					this.m_domainTable[key] = value;
				}
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00069900 File Offset: 0x00067B00
		internal void Add(Cookie cookie, bool throwOnError)
		{
			if (cookie.Value.Length <= this.m_maxCookieSize)
			{
				try
				{
					object obj = this.m_domainTable.SyncRoot;
					PathList pathList;
					lock (obj)
					{
						pathList = (PathList)this.m_domainTable[cookie.DomainKey];
						if (pathList == null)
						{
							pathList = new PathList();
							this.AddRemoveDomain(cookie.DomainKey, pathList);
						}
					}
					int cookiesCount = pathList.GetCookiesCount();
					obj = pathList.SyncRoot;
					CookieCollection cookieCollection;
					lock (obj)
					{
						cookieCollection = (CookieCollection)pathList[cookie.Path];
						if (cookieCollection == null)
						{
							cookieCollection = new CookieCollection();
							pathList[cookie.Path] = cookieCollection;
						}
					}
					if (cookie.Expired)
					{
						CookieCollection cookieCollection2 = cookieCollection;
						lock (cookieCollection2)
						{
							int num = cookieCollection.IndexOf(cookie);
							if (num != -1)
							{
								cookieCollection.RemoveAt(num);
								this.m_count--;
							}
							goto IL_0194;
						}
					}
					if (cookiesCount < this.m_maxCookiesPerDomain || this.AgeCookies(cookie.DomainKey))
					{
						if (this.m_count < this.m_maxCookies || this.AgeCookies(null))
						{
							CookieCollection cookieCollection2 = cookieCollection;
							lock (cookieCollection2)
							{
								this.m_count += cookieCollection.InternalAdd(cookie, true);
							}
						}
					}
					IL_0194:;
				}
				catch (Exception ex)
				{
					if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
					{
						throw;
					}
					if (throwOnError)
					{
						throw new CookieException(SR.GetString("An error occurred when adding a cookie to the container."), ex);
					}
				}
				return;
			}
			if (throwOnError)
			{
				throw new CookieException(SR.GetString("The value size of the cookie is '{0}'. This exceeds the configured maximum size, which is '{1}'.", new object[]
				{
					cookie.ToString(),
					this.m_maxCookieSize
				}));
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00069B58 File Offset: 0x00067D58
		private bool AgeCookies(string domain)
		{
			if (this.m_maxCookies == 0 || this.m_maxCookiesPerDomain == 0)
			{
				this.m_domainTable = new Hashtable();
				this.m_count = 0;
				return false;
			}
			int num = 0;
			DateTime dateTime = DateTime.MaxValue;
			CookieCollection cookieCollection = null;
			int num2 = 0;
			int num3 = 0;
			float num4 = 1f;
			if (this.m_count > this.m_maxCookies)
			{
				num4 = (float)this.m_maxCookies / (float)this.m_count;
			}
			object syncRoot = this.m_domainTable.SyncRoot;
			CookieCollection cookieCollection5;
			lock (syncRoot)
			{
				foreach (object obj in this.m_domainTable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					PathList pathList;
					if (domain == null)
					{
						string text = (string)dictionaryEntry.Key;
						pathList = (PathList)dictionaryEntry.Value;
					}
					else
					{
						pathList = (PathList)this.m_domainTable[domain];
					}
					num2 = 0;
					object obj2 = pathList.SyncRoot;
					lock (obj2)
					{
						foreach (object obj3 in pathList.Values)
						{
							CookieCollection cookieCollection2 = (CookieCollection)obj3;
							num3 = this.ExpireCollection(cookieCollection2);
							num += num3;
							this.m_count -= num3;
							num2 += cookieCollection2.Count;
							DateTime dateTime2;
							if (cookieCollection2.Count > 0 && (dateTime2 = cookieCollection2.TimeStamp(CookieCollection.Stamp.Check)) < dateTime)
							{
								cookieCollection = cookieCollection2;
								dateTime = dateTime2;
							}
						}
					}
					int num5 = Math.Min((int)((float)num2 * num4), Math.Min(this.m_maxCookiesPerDomain, this.m_maxCookies) - 1);
					if (num2 > num5)
					{
						obj2 = pathList.SyncRoot;
						Array array;
						Array array2;
						lock (obj2)
						{
							array = Array.CreateInstance(typeof(CookieCollection), pathList.Count);
							array2 = Array.CreateInstance(typeof(DateTime), pathList.Count);
							foreach (object obj4 in pathList.Values)
							{
								CookieCollection cookieCollection3 = (CookieCollection)obj4;
								array2.SetValue(cookieCollection3.TimeStamp(CookieCollection.Stamp.Check), num3);
								array.SetValue(cookieCollection3, num3);
								num3++;
							}
						}
						Array.Sort(array2, array);
						num3 = 0;
						for (int i = 0; i < array.Length; i++)
						{
							CookieCollection cookieCollection4 = (CookieCollection)array.GetValue(i);
							cookieCollection5 = cookieCollection4;
							lock (cookieCollection5)
							{
								while (num2 > num5 && cookieCollection4.Count > 0)
								{
									cookieCollection4.RemoveAt(0);
									num2--;
									this.m_count--;
									num++;
								}
							}
							if (num2 <= num5)
							{
								break;
							}
						}
						if (num2 > num5 && domain != null)
						{
							return false;
						}
					}
				}
			}
			if (domain != null)
			{
				return true;
			}
			if (num != 0)
			{
				return true;
			}
			if (dateTime == DateTime.MaxValue)
			{
				return false;
			}
			cookieCollection5 = cookieCollection;
			lock (cookieCollection5)
			{
				while (this.m_count >= this.m_maxCookies && cookieCollection.Count > 0)
				{
					cookieCollection.RemoveAt(0);
					this.m_count--;
				}
			}
			return true;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00069FAC File Offset: 0x000681AC
		private int ExpireCollection(CookieCollection cc)
		{
			int num;
			lock (cc)
			{
				int count = cc.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					if (cc[i].Expired)
					{
						cc.RemoveAt(i);
					}
				}
				num = count - cc.Count;
			}
			return num;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0006A018 File Offset: 0x00068218
		internal bool IsLocalDomain(string host)
		{
			int num = host.IndexOf('.');
			if (num == -1)
			{
				return true;
			}
			if (host == "127.0.0.1" || host == "::1" || host == "0:0:0:0:0:0:0:1")
			{
				return true;
			}
			if (string.Compare(this.m_fqdnMyDomain, 0, host, num, this.m_fqdnMyDomain.Length, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			string[] array = host.Split('.', StringSplitOptions.None);
			if (array != null && array.Length == 4 && array[0] == "127")
			{
				int i = 1;
				while (i < 4)
				{
					switch (array[i].Length)
					{
					case 1:
						break;
					case 2:
						goto IL_00BB;
					case 3:
						if (array[i][2] >= '0' && array[i][2] <= '9')
						{
							goto IL_00BB;
						}
						goto IL_00F7;
					default:
						goto IL_00F7;
					}
					IL_00D5:
					if (array[i][0] >= '0' && array[i][0] <= '9')
					{
						i++;
						continue;
					}
					break;
					IL_00BB:
					if (array[i][1] >= '0' && array[i][1] <= '9')
					{
						goto IL_00D5;
					}
					break;
				}
				IL_00F7:
				if (i == 4)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0006A124 File Offset: 0x00068324
		internal CookieCollection CookieCutter(Uri uri, string headerName, string setCookieHeader, bool isThrow)
		{
			CookieCollection cookieCollection = new CookieCollection();
			CookieVariant cookieVariant = CookieVariant.Unknown;
			if (headerName == null)
			{
				cookieVariant = CookieVariant.Rfc2109;
			}
			else
			{
				for (int i = 0; i < CookieContainer.HeaderInfo.Length; i++)
				{
					if (string.Compare(headerName, CookieContainer.HeaderInfo[i].Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						cookieVariant = CookieContainer.HeaderInfo[i].Variant;
					}
				}
			}
			bool flag = this.IsLocalDomain(uri.Host);
			try
			{
				CookieParser cookieParser = new CookieParser(setCookieHeader);
				for (;;)
				{
					Cookie cookie = cookieParser.Get();
					if (cookie == null)
					{
						goto IL_00B0;
					}
					if (ValidationHelper.IsBlankString(cookie.Name))
					{
						if (isThrow)
						{
							break;
						}
					}
					else if (cookie.VerifySetDefaults(cookieVariant, uri, flag, this.m_fqdnMyDomain, true, isThrow))
					{
						cookieCollection.InternalAdd(cookie, true);
					}
				}
				throw new CookieException(SR.GetString("Cookie format error."));
				IL_00B0:;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (isThrow)
				{
					throw new CookieException(SR.GetString("An error occurred when parsing the Cookie header for Uri '{0}'.", new object[] { uri.AbsoluteUri }), ex);
				}
			}
			foreach (object obj in cookieCollection)
			{
				Cookie cookie2 = (Cookie)obj;
				this.Add(cookie2, isThrow);
			}
			return cookieCollection;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0006A288 File Offset: 0x00068488
		internal CookieCollection InternalGetCookies(Uri uri)
		{
			bool flag = uri.Scheme == Uri.UriSchemeHttps;
			int port = uri.Port;
			CookieCollection cookieCollection = new CookieCollection();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string host = uri.Host;
			list.Add(host);
			list.Add("." + host);
			int num = host.IndexOf('.');
			if (num == -1)
			{
				if (this.m_fqdnMyDomain != null && this.m_fqdnMyDomain.Length != 0)
				{
					list.Add(host + this.m_fqdnMyDomain);
					list.Add(this.m_fqdnMyDomain);
				}
			}
			else
			{
				list.Add(host.Substring(num));
				if (host.Length > 2)
				{
					int num2 = host.LastIndexOf('.', host.Length - 2);
					if (num2 > 0)
					{
						num2 = host.LastIndexOf('.', num2 - 1);
					}
					if (num2 != -1)
					{
						while (num < num2 && (num = host.IndexOf('.', num + 1)) != -1)
						{
							list2.Add(host.Substring(num));
						}
					}
				}
			}
			this.BuildCookieCollectionFromDomainMatches(uri, flag, port, cookieCollection, list, false);
			this.BuildCookieCollectionFromDomainMatches(uri, flag, port, cookieCollection, list2, true);
			return cookieCollection;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0006A3BC File Offset: 0x000685BC
		private void BuildCookieCollectionFromDomainMatches(Uri uri, bool isSecure, int port, CookieCollection cookies, List<string> domainAttribute, bool matchOnlyPlainCookie)
		{
			for (int i = 0; i < domainAttribute.Count; i++)
			{
				bool flag = false;
				bool flag2 = false;
				object obj = this.m_domainTable.SyncRoot;
				PathList pathList;
				lock (obj)
				{
					pathList = (PathList)this.m_domainTable[domainAttribute[i]];
				}
				if (pathList != null)
				{
					obj = pathList.SyncRoot;
					lock (obj)
					{
						foreach (object obj2 in pathList)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
							string text = (string)dictionaryEntry.Key;
							if (uri.AbsolutePath.StartsWith(CookieParser.CheckQuoted(text)))
							{
								flag = true;
								CookieCollection cookieCollection = (CookieCollection)dictionaryEntry.Value;
								cookieCollection.TimeStamp(CookieCollection.Stamp.Set);
								this.MergeUpdateCollections(cookies, cookieCollection, port, isSecure, matchOnlyPlainCookie);
								if (text == "/")
								{
									flag2 = true;
								}
							}
							else if (flag)
							{
								break;
							}
						}
					}
					if (!flag2)
					{
						CookieCollection cookieCollection2 = (CookieCollection)pathList["/"];
						if (cookieCollection2 != null)
						{
							cookieCollection2.TimeStamp(CookieCollection.Stamp.Set);
							this.MergeUpdateCollections(cookies, cookieCollection2, port, isSecure, matchOnlyPlainCookie);
						}
					}
					if (pathList.Count == 0)
					{
						this.AddRemoveDomain(domainAttribute[i], null);
					}
				}
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0006A554 File Offset: 0x00068754
		private void MergeUpdateCollections(CookieCollection destination, CookieCollection source, int port, bool isSecure, bool isPlainOnly)
		{
			lock (source)
			{
				for (int i = 0; i < source.Count; i++)
				{
					bool flag2 = false;
					Cookie cookie = source[i];
					if (cookie.Expired)
					{
						source.RemoveAt(i);
						this.m_count--;
						i--;
					}
					else
					{
						if (!isPlainOnly || cookie.Variant == CookieVariant.Plain)
						{
							if (cookie.PortList != null)
							{
								int[] portList = cookie.PortList;
								for (int j = 0; j < portList.Length; j++)
								{
									if (portList[j] == port)
									{
										flag2 = true;
										break;
									}
								}
							}
							else
							{
								flag2 = true;
							}
						}
						if (cookie.Secure && !isSecure)
						{
							flag2 = false;
						}
						if (flag2)
						{
							destination.InternalAdd(cookie, false);
						}
					}
				}
			}
		}

		/// <summary>Gets the HTTP cookie header that contains the HTTP cookies that represent the <see cref="T:System.Net.Cookie" /> instances that are associated with a specific URI.</summary>
		/// <returns>An HTTP cookie header, with strings representing <see cref="T:System.Net.Cookie" /> instances delimited by semicolons.</returns>
		/// <param name="uri">The URI of the <see cref="T:System.Net.Cookie" /> instances desired. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060018D3 RID: 6355 RVA: 0x0006A630 File Offset: 0x00068830
		public string GetCookieHeader(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			string text;
			return this.GetCookieHeader(uri, out text);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0006A65C File Offset: 0x0006885C
		internal string GetCookieHeader(Uri uri, out string optCookie2)
		{
			CookieCollection cookieCollection = this.InternalGetCookies(uri);
			string text = string.Empty;
			string text2 = string.Empty;
			foreach (object obj in cookieCollection)
			{
				Cookie cookie = (Cookie)obj;
				text = text + text2 + cookie.ToString();
				text2 = "; ";
			}
			optCookie2 = (cookieCollection.IsOtherVersionSeen ? ("$Version=" + 1.ToString(NumberFormatInfo.InvariantInfo)) : string.Empty);
			return text;
		}

		/// <summary>Represents the default maximum number of <see cref="T:System.Net.Cookie" /> instances that the <see cref="T:System.Net.CookieContainer" /> can hold. This field is constant.</summary>
		// Token: 0x04000FD2 RID: 4050
		public const int DefaultCookieLimit = 300;

		/// <summary>Represents the default maximum number of <see cref="T:System.Net.Cookie" /> instances that the <see cref="T:System.Net.CookieContainer" /> can reference per domain. This field is constant.</summary>
		// Token: 0x04000FD3 RID: 4051
		public const int DefaultPerDomainCookieLimit = 20;

		/// <summary>Represents the default maximum size, in bytes, of the <see cref="T:System.Net.Cookie" /> instances that the <see cref="T:System.Net.CookieContainer" /> can hold. This field is constant.</summary>
		// Token: 0x04000FD4 RID: 4052
		public const int DefaultCookieLengthLimit = 4096;

		// Token: 0x04000FD5 RID: 4053
		private static readonly HeaderVariantInfo[] HeaderInfo = new HeaderVariantInfo[]
		{
			new HeaderVariantInfo("Set-Cookie", CookieVariant.Rfc2109),
			new HeaderVariantInfo("Set-Cookie2", CookieVariant.Rfc2965)
		};

		// Token: 0x04000FD6 RID: 4054
		private Hashtable m_domainTable = new Hashtable();

		// Token: 0x04000FD7 RID: 4055
		private int m_maxCookieSize = 4096;

		// Token: 0x04000FD8 RID: 4056
		private int m_maxCookies = 300;

		// Token: 0x04000FD9 RID: 4057
		private int m_maxCookiesPerDomain = 20;

		// Token: 0x04000FDA RID: 4058
		private int m_count;

		// Token: 0x04000FDB RID: 4059
		private string m_fqdnMyDomain = string.Empty;
	}
}
