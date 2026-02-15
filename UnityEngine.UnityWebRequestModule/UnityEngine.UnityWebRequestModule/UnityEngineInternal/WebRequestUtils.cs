using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Scripting;

namespace UnityEngineInternal
{
	// Token: 0x02000002 RID: 2
	internal static class WebRequestUtils
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[RequiredByNativeCode]
		internal static string RedirectTo(string baseUri, string redirectUri)
		{
			bool flag = redirectUri[0] == '/';
			Uri redirectURI;
			if (flag)
			{
				redirectURI = new Uri(redirectUri, UriKind.Relative);
			}
			else
			{
				redirectURI = new Uri(redirectUri, UriKind.RelativeOrAbsolute);
			}
			bool isAbsoluteUri = redirectURI.IsAbsoluteUri;
			string text;
			if (isAbsoluteUri)
			{
				text = redirectURI.AbsoluteUri;
			}
			else
			{
				Uri baseURI = new Uri(baseUri, UriKind.Absolute);
				Uri finalUri = new Uri(baseURI, redirectURI);
				text = finalUri.AbsoluteUri;
			}
			return text;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B4 File Offset: 0x000002B4
		internal static string MakeInitialUrl(string targetUrl, string localUrl)
		{
			bool flag = string.IsNullOrEmpty(targetUrl);
			string text;
			if (flag)
			{
				text = "";
			}
			else
			{
				bool prependProtocol = false;
				Uri localUri = new Uri(localUrl);
				Uri targetUri = null;
				bool flag2 = targetUrl[0] == '/';
				if (flag2)
				{
					targetUri = new Uri(localUri, targetUrl);
					prependProtocol = true;
				}
				bool flag3 = targetUri == null && WebRequestUtils.domainRegex.IsMatch(targetUrl);
				if (flag3)
				{
					targetUrl = localUri.Scheme + "://" + targetUrl;
					prependProtocol = true;
				}
				FormatException ex = null;
				try
				{
					bool flag4 = targetUri == null && targetUrl[0] != '.';
					if (flag4)
					{
						targetUri = new Uri(targetUrl);
					}
				}
				catch (FormatException e)
				{
					ex = e;
				}
				bool flag5 = targetUri == null;
				if (flag5)
				{
					try
					{
						targetUri = new Uri(localUri, targetUrl);
						prependProtocol = true;
					}
					catch (FormatException)
					{
						throw ex;
					}
				}
				text = WebRequestUtils.MakeUriString(targetUri, targetUrl, prependProtocol);
			}
			return text;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021B8 File Offset: 0x000003B8
		internal static string MakeUriString(Uri targetUri, string targetUrl, bool prependProtocol)
		{
			bool isFile = targetUri.IsFile;
			string text;
			if (isFile)
			{
				bool flag = !targetUri.IsLoopback;
				if (flag)
				{
					text = targetUri.OriginalString;
				}
				else
				{
					string path = targetUri.AbsolutePath;
					bool flag2 = path.Contains("%");
					if (flag2)
					{
						bool flag3 = path.Contains('+');
						if (flag3)
						{
							string original = targetUri.OriginalString;
							bool flag4 = !original.StartsWith("file:");
							if (flag4)
							{
								return "file:///" + original.Replace('\\', '/');
							}
						}
						path = WebRequestUtils.URLDecode(path);
					}
					bool flag5 = path.Length > 0 && path[0] != '/';
					if (flag5)
					{
						path = "/" + path;
					}
					text = "file://" + path;
				}
			}
			else
			{
				string scheme = targetUri.Scheme;
				bool flag6 = !prependProtocol && targetUrl.Length >= scheme.Length + 2 && targetUrl[scheme.Length + 1] != '/';
				if (flag6)
				{
					StringBuilder sb = new StringBuilder(scheme, targetUrl.Length);
					sb.Append(':');
					bool flag7 = scheme == "jar";
					if (flag7)
					{
						string path2 = targetUri.AbsolutePath;
						bool flag8 = path2.Contains("%");
						if (flag8)
						{
							path2 = WebRequestUtils.URLDecode(path2);
						}
						bool flag9 = path2.StartsWith("file:/") && path2.Length > 6 && path2[6] != '/';
						if (flag9)
						{
							sb.Append("file://");
							sb.Append(path2.Substring(5));
						}
						else
						{
							sb.Append(path2);
						}
						text = sb.ToString();
					}
					else
					{
						sb.Append(targetUri.PathAndQuery);
						sb.Append(targetUri.Fragment);
						text = sb.ToString();
					}
				}
				else
				{
					bool flag10 = targetUrl.Contains("%");
					if (flag10)
					{
						text = targetUri.OriginalString;
					}
					else
					{
						text = targetUri.AbsoluteUri;
					}
				}
			}
			return text;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023D8 File Offset: 0x000005D8
		private static string URLDecode(string encoded)
		{
			byte[] urlBytes = Encoding.UTF8.GetBytes(encoded);
			byte[] decodedBytes = WWWTranscoder.URLDecode(urlBytes);
			return Encoding.UTF8.GetString(decodedBytes);
		}

		// Token: 0x04000001 RID: 1
		private static Regex domainRegex = new Regex("^\\s*\\w+(?:\\.\\w+)+(\\/.*)?$");
	}
}
