using System;
using System.Collections;
using System.IO;
using System.Net.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security.Private;
using Mono.Security.Authenticode;
using Mono.Security.Interface;

namespace System.Net
{
	/// <summary>Provides a simple, programmatically controlled HTTP protocol listener. This class cannot be inherited.</summary>
	// Token: 0x0200040D RID: 1037
	public sealed class HttpListener : IDisposable
	{
		// Token: 0x060019BE RID: 6590 RVA: 0x0006EAD8 File Offset: 0x0006CCD8
		internal X509Certificate LoadCertificateAndKey(IPAddress addr, int port)
		{
			object internalLock = this._internalLock;
			X509Certificate x509Certificate;
			lock (internalLock)
			{
				if (this.certificate != null)
				{
					x509Certificate = this.certificate;
				}
				else
				{
					try
					{
						string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
						text = Path.Combine(text, "httplistener");
						string text2 = Path.Combine(text, string.Format("{0}.cer", port));
						if (!File.Exists(text2))
						{
							x509Certificate = null;
						}
						else
						{
							string text3 = Path.Combine(text, string.Format("{0}.pvk", port));
							if (!File.Exists(text3))
							{
								x509Certificate = null;
							}
							else
							{
								X509Certificate2 x509Certificate2 = new X509Certificate2(text2);
								RSA rsa = PrivateKey.CreateFromFile(text3).RSA;
								this.certificate = new X509Certificate2((X509Certificate2Impl)x509Certificate2.Impl.CopyWithPrivateKey(rsa));
								x509Certificate = this.certificate;
							}
						}
					}
					catch
					{
						this.certificate = null;
						x509Certificate = null;
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0006EBE4 File Offset: 0x0006CDE4
		internal SslStream CreateSslStream(Stream innerStream, bool ownsStream, RemoteCertificateValidationCallback callback)
		{
			object internalLock = this._internalLock;
			SslStream sslStream;
			lock (internalLock)
			{
				if (this.tlsProvider == null)
				{
					this.tlsProvider = MonoTlsProviderFactory.GetProvider();
				}
				MonoTlsSettings monoTlsSettings = (this.tlsSettings ?? MonoTlsSettings.DefaultSettings).Clone();
				monoTlsSettings.RemoteCertificateValidationCallback = CallbackHelpers.PublicToMono(callback);
				sslStream = new SslStream(innerStream, ownsStream, this.tlsProvider, monoTlsSettings);
			}
			return sslStream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListener" /> class.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">This class cannot be used on the current operating system. Windows Server 2003 or Windows XP SP2 is required to use instances of this class.</exception>
		// Token: 0x060019C0 RID: 6592 RVA: 0x0006EC64 File Offset: 0x0006CE64
		public HttpListener()
		{
			this._internalLock = new object();
			this.prefixes = new HttpListenerPrefixCollection(this);
			this.registry = new Hashtable();
			this.connections = Hashtable.Synchronized(new Hashtable());
			this.ctx_queue = new ArrayList();
			this.wait_queue = new ArrayList();
			this.auth_schemes = AuthenticationSchemes.Anonymous;
			this.defaultServiceNames = new ServiceNameStore();
			this.extendedProtectionPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);
		}

		/// <summary>Gets or sets the scheme used to authenticate clients.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Net.AuthenticationSchemes" /> enumeration values that indicates how clients are to be authenticated. The default value is <see cref="F:System.Net.AuthenticationSchemes.Anonymous" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0006ECE1 File Offset: 0x0006CEE1
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x0006ECE9 File Offset: 0x0006CEE9
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this.auth_schemes;
			}
			set
			{
				this.CheckDisposed();
				this.auth_schemes = value;
			}
		}

		/// <summary>Gets or sets the delegate called to determine the protocol used to authenticate clients.</summary>
		/// <returns>An <see cref="T:System.Net.AuthenticationSchemeSelector" /> delegate that invokes the method used to select an authentication protocol. The default value is null.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0006ECF8 File Offset: 0x0006CEF8
		public AuthenticationSchemeSelector AuthenticationSchemeSelectorDelegate
		{
			get
			{
				return this.auth_selector;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether your application receives exceptions that occur when an <see cref="T:System.Net.HttpListener" /> sends the response to the client.</summary>
		/// <returns>true if this <see cref="T:System.Net.HttpListener" /> should not return exceptions that occur when sending the response to the client; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0006ED00 File Offset: 0x0006CF00
		public bool IgnoreWriteExceptions
		{
			get
			{
				return this.ignore_write_exceptions;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> has been started.</summary>
		/// <returns>true if the <see cref="T:System.Net.HttpListener" /> was started; otherwise, false.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0006ED08 File Offset: 0x0006CF08
		public bool IsListening
		{
			get
			{
				return this.listening;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) prefixes handled by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerPrefixCollection" /> that contains the URI prefixes that this <see cref="T:System.Net.HttpListener" /> object is configured to handle. </returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0006ED10 File Offset: 0x0006CF10
		public HttpListenerPrefixCollection Prefixes
		{
			get
			{
				this.CheckDisposed();
				return this.prefixes;
			}
		}

		/// <summary>Gets or sets the realm, or resource partition, associated with this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the name of the realm associated with the <see cref="T:System.Net.HttpListener" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0006ED1E File Offset: 0x0006CF1E
		public string Realm
		{
			get
			{
				return this.realm;
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0006ED26 File Offset: 0x0006CF26
		private void Close(bool force)
		{
			this.CheckDisposed();
			EndPointManager.RemoveListener(this);
			this.Cleanup(force);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0006ED3C File Offset: 0x0006CF3C
		private void Cleanup(bool close_existing)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				if (close_existing)
				{
					ICollection keys = this.registry.Keys;
					HttpListenerContext[] array = new HttpListenerContext[keys.Count];
					keys.CopyTo(array, 0);
					this.registry.Clear();
					for (int i = array.Length - 1; i >= 0; i--)
					{
						array[i].Connection.Close(true);
					}
				}
				object syncRoot = this.connections.SyncRoot;
				lock (syncRoot)
				{
					ICollection keys2 = this.connections.Keys;
					HttpConnection[] array2 = new HttpConnection[keys2.Count];
					keys2.CopyTo(array2, 0);
					this.connections.Clear();
					for (int j = array2.Length - 1; j >= 0; j--)
					{
						array2[j].Close(true);
					}
				}
				ArrayList arrayList = this.ctx_queue;
				lock (arrayList)
				{
					HttpListenerContext[] array3 = (HttpListenerContext[])this.ctx_queue.ToArray(typeof(HttpListenerContext));
					this.ctx_queue.Clear();
					for (int k = array3.Length - 1; k >= 0; k--)
					{
						array3[k].Connection.Close(true);
					}
				}
				arrayList = this.wait_queue;
				lock (arrayList)
				{
					Exception ex = new ObjectDisposedException("listener");
					foreach (object obj in this.wait_queue)
					{
						((ListenerAsyncResult)obj).Complete(ex);
					}
					this.wait_queue.Clear();
				}
			}
		}

		/// <summary>Begins asynchronously retrieving an incoming request.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when a client request is available.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060019CA RID: 6602 RVA: 0x0006EF88 File Offset: 0x0006D188
		public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Please, call Start before using this method.");
			}
			ListenerAsyncResult listenerAsyncResult = new ListenerAsyncResult(callback, state);
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				ArrayList arrayList2 = this.ctx_queue;
				lock (arrayList2)
				{
					HttpListenerContext contextFromQueue = this.GetContextFromQueue();
					if (contextFromQueue != null)
					{
						listenerAsyncResult.Complete(contextFromQueue, true);
						return listenerAsyncResult;
					}
				}
				this.wait_queue.Add(listenerAsyncResult);
			}
			return listenerAsyncResult;
		}

		/// <summary>Completes an asynchronous operation to retrieve an incoming client request.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents the client request.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that was obtained when the asynchronous operation was started.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling the <see cref="M:System.Net.HttpListener.BeginGetContext(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpListener.EndGetContext(System.IAsyncResult)" /> method was already called for the specified <paramref name="asyncResult" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060019CB RID: 6603 RVA: 0x0006F03C File Offset: 0x0006D23C
		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			this.CheckDisposed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ListenerAsyncResult listenerAsyncResult = asyncResult as ListenerAsyncResult;
			if (listenerAsyncResult == null)
			{
				throw new ArgumentException("Wrong IAsyncResult.", "asyncResult");
			}
			if (listenerAsyncResult.EndCalled)
			{
				throw new ArgumentException("Cannot reuse this IAsyncResult");
			}
			listenerAsyncResult.EndCalled = true;
			if (!listenerAsyncResult.IsCompleted)
			{
				listenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				int num = this.wait_queue.IndexOf(listenerAsyncResult);
				if (num >= 0)
				{
					this.wait_queue.RemoveAt(num);
				}
			}
			HttpListenerContext context = listenerAsyncResult.GetContext();
			context.ParseAuthentication(this.SelectAuthenticationScheme(context));
			return context;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0006F108 File Offset: 0x0006D308
		internal AuthenticationSchemes SelectAuthenticationScheme(HttpListenerContext context)
		{
			if (this.AuthenticationSchemeSelectorDelegate != null)
			{
				return this.AuthenticationSchemeSelectorDelegate(context.Request);
			}
			return this.auth_schemes;
		}

		/// <summary>Waits for an incoming request and returns when one is received.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents a client request.</returns>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.-or-The <see cref="T:System.Net.HttpListener" /> does not have any Uniform Resource Identifier (URI) prefixes to respond to. See Remarks.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060019CD RID: 6605 RVA: 0x0006F12C File Offset: 0x0006D32C
		public HttpListenerContext GetContext()
		{
			if (this.prefixes.Count == 0)
			{
				throw new InvalidOperationException("Please, call AddPrefix before using this method.");
			}
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)this.BeginGetContext(null, null);
			listenerAsyncResult.InGet = true;
			return this.EndGetContext(listenerAsyncResult);
		}

		/// <summary>Allows this instance to receive incoming requests.</summary>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060019CE RID: 6606 RVA: 0x0006F16D File Offset: 0x0006D36D
		public void Start()
		{
			this.CheckDisposed();
			if (this.listening)
			{
				return;
			}
			EndPointManager.AddListener(this);
			this.listening = true;
		}

		/// <summary>Releases the resources held by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		// Token: 0x060019CF RID: 6607 RVA: 0x0006F18B File Offset: 0x0006D38B
		void IDisposable.Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(true);
			this.disposed = true;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0006F1A4 File Offset: 0x0006D3A4
		internal void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0006F1BF File Offset: 0x0006D3BF
		private HttpListenerContext GetContextFromQueue()
		{
			if (this.ctx_queue.Count == 0)
			{
				return null;
			}
			HttpListenerContext httpListenerContext = (HttpListenerContext)this.ctx_queue[0];
			this.ctx_queue.RemoveAt(0);
			return httpListenerContext;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0006F1F0 File Offset: 0x0006D3F0
		internal void RegisterContext(HttpListenerContext context)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				this.registry[context] = context;
			}
			ListenerAsyncResult listenerAsyncResult = null;
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				if (this.wait_queue.Count == 0)
				{
					ArrayList arrayList2 = this.ctx_queue;
					lock (arrayList2)
					{
						this.ctx_queue.Add(context);
						goto IL_00A3;
					}
				}
				listenerAsyncResult = (ListenerAsyncResult)this.wait_queue[0];
				this.wait_queue.RemoveAt(0);
			}
			IL_00A3:
			if (listenerAsyncResult != null)
			{
				listenerAsyncResult.Complete(context);
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0006F2D4 File Offset: 0x0006D4D4
		internal void UnregisterContext(HttpListenerContext context)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				this.registry.Remove(context);
			}
			ArrayList arrayList = this.ctx_queue;
			lock (arrayList)
			{
				int num = this.ctx_queue.IndexOf(context);
				if (num >= 0)
				{
					this.ctx_queue.RemoveAt(num);
				}
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0006F360 File Offset: 0x0006D560
		internal void AddConnection(HttpConnection cnc)
		{
			this.connections[cnc] = cnc;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0006F36F File Offset: 0x0006D56F
		internal void RemoveConnection(HttpConnection cnc)
		{
			this.connections.Remove(cnc);
		}

		// Token: 0x04001070 RID: 4208
		private MonoTlsProvider tlsProvider;

		// Token: 0x04001071 RID: 4209
		private MonoTlsSettings tlsSettings;

		// Token: 0x04001072 RID: 4210
		private X509Certificate certificate;

		// Token: 0x04001073 RID: 4211
		private AuthenticationSchemes auth_schemes;

		// Token: 0x04001074 RID: 4212
		private HttpListenerPrefixCollection prefixes;

		// Token: 0x04001075 RID: 4213
		private AuthenticationSchemeSelector auth_selector;

		// Token: 0x04001076 RID: 4214
		private string realm;

		// Token: 0x04001077 RID: 4215
		private bool ignore_write_exceptions;

		// Token: 0x04001078 RID: 4216
		private bool listening;

		// Token: 0x04001079 RID: 4217
		private bool disposed;

		// Token: 0x0400107A RID: 4218
		private readonly object _internalLock;

		// Token: 0x0400107B RID: 4219
		private Hashtable registry;

		// Token: 0x0400107C RID: 4220
		private ArrayList ctx_queue;

		// Token: 0x0400107D RID: 4221
		private ArrayList wait_queue;

		// Token: 0x0400107E RID: 4222
		private Hashtable connections;

		// Token: 0x0400107F RID: 4223
		private ServiceNameStore defaultServiceNames;

		// Token: 0x04001080 RID: 4224
		private ExtendedProtectionPolicy extendedProtectionPolicy;
	}
}
