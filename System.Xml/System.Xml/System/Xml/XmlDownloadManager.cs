using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000116 RID: 278
	internal class XmlDownloadManager
	{
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0004AACF File Offset: 0x00048CCF
		internal Stream GetStream(Uri uri, ICredentials credentials, IWebProxy proxy, RequestCachePolicy cachePolicy)
		{
			if (uri.Scheme == "file")
			{
				return new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read, 1);
			}
			return this.GetNonFileStream(uri, credentials, proxy, cachePolicy);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0004AB00 File Offset: 0x00048D00
		private Stream GetNonFileStream(Uri uri, ICredentials credentials, IWebProxy proxy, RequestCachePolicy cachePolicy)
		{
			WebRequest webRequest = WebRequest.Create(uri);
			if (credentials != null)
			{
				webRequest.Credentials = credentials;
			}
			if (proxy != null)
			{
				webRequest.Proxy = proxy;
			}
			if (cachePolicy != null)
			{
				webRequest.CachePolicy = cachePolicy;
			}
			WebResponse response = webRequest.GetResponse();
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest != null)
			{
				lock (this)
				{
					if (this.connections == null)
					{
						this.connections = new Hashtable();
					}
					OpenedHost openedHost = (OpenedHost)this.connections[httpWebRequest.Address.Host];
					if (openedHost == null)
					{
						openedHost = new OpenedHost();
					}
					if (openedHost.nonCachedConnectionsCount < httpWebRequest.ServicePoint.ConnectionLimit - 1)
					{
						if (openedHost.nonCachedConnectionsCount == 0)
						{
							this.connections.Add(httpWebRequest.Address.Host, openedHost);
						}
						openedHost.nonCachedConnectionsCount++;
						return new XmlRegisteredNonCachedStream(response.GetResponseStream(), this, httpWebRequest.Address.Host);
					}
					return new XmlCachedStream(response.ResponseUri, response.GetResponseStream());
				}
			}
			return response.GetResponseStream();
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0004AC2C File Offset: 0x00048E2C
		internal void Remove(string host)
		{
			lock (this)
			{
				OpenedHost openedHost = (OpenedHost)this.connections[host];
				if (openedHost != null)
				{
					OpenedHost openedHost2 = openedHost;
					int num = openedHost2.nonCachedConnectionsCount - 1;
					openedHost2.nonCachedConnectionsCount = num;
					if (num == 0)
					{
						this.connections.Remove(host);
					}
				}
			}
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0004AC98 File Offset: 0x00048E98
		internal Task<Stream> GetStreamAsync(Uri uri, ICredentials credentials, IWebProxy proxy, RequestCachePolicy cachePolicy)
		{
			if (uri.Scheme == "file")
			{
				return Task.Run<Stream>(() => new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read, 1, true));
			}
			return this.GetNonFileStreamAsync(uri, credentials, proxy, cachePolicy);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0004ACEC File Offset: 0x00048EEC
		private async Task<Stream> GetNonFileStreamAsync(Uri uri, ICredentials credentials, IWebProxy proxy, RequestCachePolicy cachePolicy)
		{
			WebRequest req = WebRequest.Create(uri);
			if (credentials != null)
			{
				req.Credentials = credentials;
			}
			if (proxy != null)
			{
				req.Proxy = proxy;
			}
			if (cachePolicy != null)
			{
				req.CachePolicy = cachePolicy;
			}
			WebResponse webResponse = await Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(req.BeginGetResponse), new Func<IAsyncResult, WebResponse>(req.EndGetResponse), null).ConfigureAwait(false);
			HttpWebRequest httpWebRequest = req as HttpWebRequest;
			if (httpWebRequest != null)
			{
				lock (this)
				{
					if (this.connections == null)
					{
						this.connections = new Hashtable();
					}
					OpenedHost openedHost = (OpenedHost)this.connections[httpWebRequest.Address.Host];
					if (openedHost == null)
					{
						openedHost = new OpenedHost();
					}
					if (openedHost.nonCachedConnectionsCount < httpWebRequest.ServicePoint.ConnectionLimit - 1)
					{
						if (openedHost.nonCachedConnectionsCount == 0)
						{
							this.connections.Add(httpWebRequest.Address.Host, openedHost);
						}
						openedHost.nonCachedConnectionsCount++;
						return new XmlRegisteredNonCachedStream(webResponse.GetResponseStream(), this, httpWebRequest.Address.Host);
					}
					return new XmlCachedStream(webResponse.ResponseUri, webResponse.GetResponseStream());
				}
			}
			return webResponse.GetResponseStream();
		}

		// Token: 0x04000734 RID: 1844
		private Hashtable connections;
	}
}
