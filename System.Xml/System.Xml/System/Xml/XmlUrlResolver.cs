using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml
{
	/// <summary>Resolves external XML resources named by a Uniform Resource Identifier (URI).</summary>
	// Token: 0x02000132 RID: 306
	public class XmlUrlResolver : XmlResolver
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0004CA84 File Offset: 0x0004AC84
		private static XmlDownloadManager DownloadManager
		{
			get
			{
				if (XmlUrlResolver.s_DownloadManager == null)
				{
					object obj = new XmlDownloadManager();
					Interlocked.CompareExchange<object>(ref XmlUrlResolver.s_DownloadManager, obj, null);
				}
				return (XmlDownloadManager)XmlUrlResolver.s_DownloadManager;
			}
		}

		/// <summary>Maps a URI to an object containing the actual resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object or null if a type other than stream is specified.</returns>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current implementation does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink: role and used as an implementation specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="T:System.IO.Stream" /> objects.</param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="ofObjectToReturn" /> is neither null nor a Stream type.</exception>
		/// <exception cref="T:System.UriFormatException">The specified URI is not an absolute URI.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="absoluteUri" /> is null.</exception>
		/// <exception cref="T:System.Exception">There is a runtime error (for example, an interrupted server connection).</exception>
		// Token: 0x06000F52 RID: 3922 RVA: 0x0004CAC0 File Offset: 0x0004ACC0
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
			{
				return XmlUrlResolver.DownloadManager.GetStream(absoluteUri, this._credentials, this._proxy, this._cachePolicy);
			}
			throw new XmlException("Object type is not supported.", string.Empty);
		}

		/// <summary>Resolves the absolute URI from the base and relative URIs.</summary>
		/// <returns>A <see cref="T:System.Uri" /> representing the absolute URI, or null if the relative URI cannot be resolved.</returns>
		/// <param name="baseUri">The base URI used to resolve the relative URI.</param>
		/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseUri" /> is null or <paramref name="relativeUri" /> is null.</exception>
		// Token: 0x06000F53 RID: 3923 RVA: 0x0004CB27 File Offset: 0x0004AD27
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			return base.ResolveUri(baseUri, relativeUri);
		}

		/// <summary>Asynchronously maps a URI to an object containing the actual resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object or null if a type other than stream is specified.</returns>
		/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
		/// <param name="role">The current implementation does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink: role and used as an implementation specific argument in other scenarios.</param>
		/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="T:System.IO.Stream" /> objects.</param>
		// Token: 0x06000F54 RID: 3924 RVA: 0x0004CB34 File Offset: 0x0004AD34
		public override async Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
			{
				return await XmlUrlResolver.DownloadManager.GetStreamAsync(absoluteUri, this._credentials, this._proxy, this._cachePolicy).ConfigureAwait(false);
			}
			throw new XmlException("Object type is not supported.", string.Empty);
		}

		// Token: 0x0400077C RID: 1916
		private static object s_DownloadManager;

		// Token: 0x0400077D RID: 1917
		private ICredentials _credentials;

		// Token: 0x0400077E RID: 1918
		private IWebProxy _proxy;

		// Token: 0x0400077F RID: 1919
		private RequestCachePolicy _cachePolicy;
	}
}
