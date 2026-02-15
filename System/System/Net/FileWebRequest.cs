using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides a file system implementation of the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x020003EA RID: 1002
	[Serializable]
	public class FileWebRequest : WebRequest, ISerializable
	{
		// Token: 0x060018E7 RID: 6375 RVA: 0x0006A8F0 File Offset: 0x00068AF0
		internal FileWebRequest(Uri uri)
		{
			if (uri.Scheme != Uri.UriSchemeFile)
			{
				throw new ArgumentOutOfRangeException("uri");
			}
			this.m_uri = uri;
			this.m_fileAccess = FileAccess.Read;
			this.m_headers = new WebHeaderCollection(WebHeaderCollectionType.FileWebRequest);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.FileWebRequest" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the new <see cref="T:System.Net.FileWebRequest" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.FileWebRequest" /> object. </param>
		// Token: 0x060018E8 RID: 6376 RVA: 0x0006A94C File Offset: 0x00068B4C
		[Obsolete("Serialization is obsoleted for this type. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected FileWebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			this.m_headers = (WebHeaderCollection)serializationInfo.GetValue("headers", typeof(WebHeaderCollection));
			this.m_proxy = (IWebProxy)serializationInfo.GetValue("proxy", typeof(IWebProxy));
			this.m_uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.m_connectionGroupName = serializationInfo.GetString("connectionGroupName");
			this.m_method = serializationInfo.GetString("method");
			this.m_contentLength = serializationInfo.GetInt64("contentLength");
			this.m_timeout = serializationInfo.GetInt32("timeout");
			this.m_fileAccess = (FileAccess)serializationInfo.GetInt32("fileAccess");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the required data to serialize the <see cref="T:System.Net.FileWebRequest" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data for the <see cref="T:System.Net.FileWebRequest" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.FileWebRequest" />. </param>
		// Token: 0x060018E9 RID: 6377 RVA: 0x000660DA File Offset: 0x000642DA
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" />  that specifies the destination for this serialization. </param>
		// Token: 0x060018EA RID: 6378 RVA: 0x0006AA2C File Offset: 0x00068C2C
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("headers", this.m_headers, typeof(WebHeaderCollection));
			serializationInfo.AddValue("proxy", this.m_proxy, typeof(IWebProxy));
			serializationInfo.AddValue("uri", this.m_uri, typeof(Uri));
			serializationInfo.AddValue("connectionGroupName", this.m_connectionGroupName);
			serializationInfo.AddValue("method", this.m_method);
			serializationInfo.AddValue("contentLength", this.m_contentLength);
			serializationInfo.AddValue("timeout", this.m_timeout);
			serializationInfo.AddValue("fileAccess", this.m_fileAccess);
			serializationInfo.AddValue("preauthenticate", false);
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0006AAF8 File Offset: 0x00068CF8
		internal bool Aborted
		{
			get
			{
				return this.m_Aborted != 0;
			}
		}

		/// <summary>Gets or sets the content length of the data being sent.</summary>
		/// <returns>The number of bytes of request data being sent.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.FileWebRequest.ContentLength" /> is less than 0. </exception>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0006AB03 File Offset: 0x00068D03
		public override long ContentLength
		{
			get
			{
				return this.m_contentLength;
			}
		}

		/// <summary>Gets or sets the credentials that are associated with this request. This property is reserved for future use.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that contains the authentication credentials that are associated with this request. The default is null.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0006AB0B File Offset: 0x00068D0B
		// (set) Token: 0x060018EE RID: 6382 RVA: 0x0006AB13 File Offset: 0x00068D13
		public override ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		/// <summary>Gets a collection of the name/value pairs that are associated with the request. This property is reserved for future use.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains header name/value pairs associated with this request.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0006AB1C File Offset: 0x00068D1C
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.m_headers;
			}
		}

		/// <summary>Gets or sets the protocol method used for the request. This property is reserved for future use.</summary>
		/// <returns>The protocol method to use in this request.</returns>
		/// <exception cref="T:System.ArgumentException">The method is invalid.- or -The method is not supported.- or -Multiple methods were specified.</exception>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0006AB24 File Offset: 0x00068D24
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x0006AB2C File Offset: 0x00068D2C
		public override string Method
		{
			get
			{
				return this.m_method;
			}
			set
			{
				if (ValidationHelper.IsBlankString(value))
				{
					throw new ArgumentException(SR.GetString("Cannot set null or blank methods on request."), "value");
				}
				this.m_method = value;
			}
		}

		/// <summary>Gets or sets the network proxy to use for this request. This property is reserved for future use.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that indicates the network proxy to use for this request.</returns>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0006AB52 File Offset: 0x00068D52
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0006AB5A File Offset: 0x00068D5A
		public override IWebProxy Proxy
		{
			get
			{
				return this.m_proxy;
			}
			set
			{
				this.m_proxy = value;
			}
		}

		/// <summary>Gets or sets the length of time until the request times out.</summary>
		/// <returns>The time, in milliseconds, until the request times out, or the value <see cref="F:System.Threading.Timeout.Infinite" /> to indicate that the request does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0006AB63 File Offset: 0x00068D63
		public override int Timeout
		{
			get
			{
				return this.m_timeout;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the request.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0006AB6B File Offset: 0x00068D6B
		public override Uri RequestUri
		{
			get
			{
				return this.m_uri;
			}
		}

		/// <summary>Begins an asynchronous request for a file system resource.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate. </param>
		/// <param name="state">An object that contains state information for this request. </param>
		/// <exception cref="T:System.InvalidOperationException">The stream is already in use by a previous call to <see cref="M:System.Net.FileWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.Net.WebException">The <see cref="T:System.Net.FileWebRequest" /> was aborted. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060018F6 RID: 6390 RVA: 0x0006AB74 File Offset: 0x00068D74
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			try
			{
				if (this.Aborted)
				{
					throw ExceptionHelper.RequestAbortedException;
				}
				lock (this)
				{
					if (this.m_readPending)
					{
						throw new InvalidOperationException(SR.GetString("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress."));
					}
					this.m_readPending = true;
				}
				this.m_WriteAResult = new LazyAsyncResult(this, state, callback);
				ThreadPool.QueueUserWorkItem(FileWebRequest.s_GetResponseCallback, this.m_WriteAResult);
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return this.m_WriteAResult;
		}

		/// <summary>Ends an asynchronous request for a file system resource.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the file system resource.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the pending request for a response. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		// Token: 0x060018F7 RID: 6391 RVA: 0x0006AC20 File Offset: 0x00068E20
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			WebResponse webResponse;
			try
			{
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (asyncResult == null || lazyAsyncResult == null)
				{
					throw (asyncResult == null) ? new ArgumentNullException("asyncResult") : new ArgumentException(SR.GetString("The AsyncResult is not valid."), "asyncResult");
				}
				object obj = lazyAsyncResult.InternalWaitForCompletion();
				if (obj is Exception)
				{
					throw (Exception)obj;
				}
				webResponse = (WebResponse)obj;
				this.m_readPending = false;
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return webResponse;
		}

		/// <summary>Returns a response to a file system request.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> that contains the response from the file system resource.</returns>
		/// <exception cref="T:System.Net.WebException">The request timed out. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060018F8 RID: 6392 RVA: 0x0006ACAC File Offset: 0x00068EAC
		public override WebResponse GetResponse()
		{
			this.m_syncHint = true;
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this.BeginGetResponse(null, null);
				if (this.Timeout != -1 && !asyncResult.IsCompleted && (!asyncResult.AsyncWaitHandle.WaitOne(this.Timeout, false) || !asyncResult.IsCompleted))
				{
					if (this.m_response != null)
					{
						this.m_response.Close();
					}
					throw new WebException(NetRes.GetWebStatusString(WebExceptionStatus.Timeout), WebExceptionStatus.Timeout);
				}
			}
			catch (Exception)
			{
				bool on = Logging.On;
				throw;
			}
			finally
			{
			}
			return this.EndGetResponse(asyncResult);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0006AD48 File Offset: 0x00068F48
		private static void GetRequestStreamCallback(object state)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)state;
			FileWebRequest fileWebRequest = (FileWebRequest)lazyAsyncResult.AsyncObject;
			try
			{
				if (fileWebRequest.m_stream == null)
				{
					fileWebRequest.m_stream = new FileWebStream(fileWebRequest, fileWebRequest.m_uri.LocalPath, FileMode.Create, FileAccess.Write, FileShare.Read);
					fileWebRequest.m_fileAccess = FileAccess.Write;
					fileWebRequest.m_writing = true;
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = new WebException(ex.Message, ex);
				lazyAsyncResult.InvokeCallback(ex2);
				return;
			}
			lazyAsyncResult.InvokeCallback(fileWebRequest.m_stream);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0006ADD0 File Offset: 0x00068FD0
		private static void GetResponseCallback(object state)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)state;
			FileWebRequest fileWebRequest = (FileWebRequest)lazyAsyncResult.AsyncObject;
			if (fileWebRequest.m_writePending || fileWebRequest.m_writing)
			{
				FileWebRequest fileWebRequest2 = fileWebRequest;
				lock (fileWebRequest2)
				{
					if (fileWebRequest.m_writePending || fileWebRequest.m_writing)
					{
						fileWebRequest.m_readerEvent = new ManualResetEvent(false);
					}
				}
			}
			if (fileWebRequest.m_readerEvent != null)
			{
				fileWebRequest.m_readerEvent.WaitOne();
			}
			try
			{
				if (fileWebRequest.m_response == null)
				{
					fileWebRequest.m_response = new FileWebResponse(fileWebRequest, fileWebRequest.m_uri, fileWebRequest.m_fileAccess, !fileWebRequest.m_syncHint);
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = new WebException(ex.Message, ex);
				lazyAsyncResult.InvokeCallback(ex2);
				return;
			}
			lazyAsyncResult.InvokeCallback(fileWebRequest.m_response);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0006AEB8 File Offset: 0x000690B8
		internal void UnblockReader()
		{
			lock (this)
			{
				if (this.m_readerEvent != null)
				{
					this.m_readerEvent.Set();
				}
			}
			this.m_writing = false;
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Default credentials are not supported for file Uniform Resource Identifiers (URIs).</exception>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0006374D File Offset: 0x0006194D
		public override bool UseDefaultCredentials
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		/// <summary>Cancels a request to an Internet resource.</summary>
		// Token: 0x060018FD RID: 6397 RVA: 0x0006AF08 File Offset: 0x00069108
		public override void Abort()
		{
			bool on = Logging.On;
			try
			{
				if (Interlocked.Increment(ref this.m_Aborted) == 1)
				{
					LazyAsyncResult readAResult = this.m_ReadAResult;
					LazyAsyncResult writeAResult = this.m_WriteAResult;
					WebException ex = new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
					Stream stream = this.m_stream;
					if (readAResult != null && !readAResult.IsCompleted)
					{
						readAResult.InvokeCallback(ex);
					}
					if (writeAResult != null && !writeAResult.IsCompleted)
					{
						writeAResult.InvokeCallback(ex);
					}
					if (stream != null)
					{
						if (stream is ICloseEx)
						{
							((ICloseEx)stream).CloseEx(CloseExState.Abort);
						}
						else
						{
							stream.Close();
						}
					}
					if (this.m_response != null)
					{
						((ICloseEx)this.m_response).CloseEx(CloseExState.Abort);
					}
				}
			}
			catch (Exception)
			{
				bool on2 = Logging.On;
				throw;
			}
			finally
			{
			}
		}

		// Token: 0x04000FDE RID: 4062
		private static WaitCallback s_GetRequestStreamCallback = new WaitCallback(FileWebRequest.GetRequestStreamCallback);

		// Token: 0x04000FDF RID: 4063
		private static WaitCallback s_GetResponseCallback = new WaitCallback(FileWebRequest.GetResponseCallback);

		// Token: 0x04000FE0 RID: 4064
		private string m_connectionGroupName;

		// Token: 0x04000FE1 RID: 4065
		private long m_contentLength;

		// Token: 0x04000FE2 RID: 4066
		private ICredentials m_credentials;

		// Token: 0x04000FE3 RID: 4067
		private FileAccess m_fileAccess;

		// Token: 0x04000FE4 RID: 4068
		private WebHeaderCollection m_headers;

		// Token: 0x04000FE5 RID: 4069
		private string m_method = "GET";

		// Token: 0x04000FE6 RID: 4070
		private IWebProxy m_proxy;

		// Token: 0x04000FE7 RID: 4071
		private ManualResetEvent m_readerEvent;

		// Token: 0x04000FE8 RID: 4072
		private bool m_readPending;

		// Token: 0x04000FE9 RID: 4073
		private WebResponse m_response;

		// Token: 0x04000FEA RID: 4074
		private Stream m_stream;

		// Token: 0x04000FEB RID: 4075
		private bool m_syncHint;

		// Token: 0x04000FEC RID: 4076
		private int m_timeout = 100000;

		// Token: 0x04000FED RID: 4077
		private Uri m_uri;

		// Token: 0x04000FEE RID: 4078
		private bool m_writePending;

		// Token: 0x04000FEF RID: 4079
		private bool m_writing;

		// Token: 0x04000FF0 RID: 4080
		private LazyAsyncResult m_WriteAResult;

		// Token: 0x04000FF1 RID: 4081
		private LazyAsyncResult m_ReadAResult;

		// Token: 0x04000FF2 RID: 4082
		private int m_Aborted;
	}
}
