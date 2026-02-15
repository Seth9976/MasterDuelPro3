using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Provides client connections for TCP network services.</summary>
	// Token: 0x020004BF RID: 1215
	public class TcpClient : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpClient" /> class.</summary>
		// Token: 0x06001DD7 RID: 7639 RVA: 0x00081C6E File Offset: 0x0007FE6E
		public TcpClient()
			: this(AddressFamily.InterNetwork)
		{
			bool on = Logging.On;
			bool on2 = Logging.On;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpClient" /> class with the specified family.</summary>
		/// <param name="family">The <see cref="P:System.Net.IPAddress.AddressFamily" /> of the IP protocol. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="family" /> parameter is not equal to AddressFamily.InterNetwork -or- The <paramref name="family" /> parameter is not equal to AddressFamily.InterNetworkV6 </exception>
		// Token: 0x06001DD8 RID: 7640 RVA: 0x00081C84 File Offset: 0x0007FE84
		public TcpClient(AddressFamily family)
		{
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			bool on = Logging.On;
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(SR.GetString("'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses.", new object[] { "TCP" }), "family");
			}
			this.m_Family = family;
			this.initialize();
			bool on2 = Logging.On;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpClient" /> class and connects to the specified port on the specified host.</summary>
		/// <param name="hostname">The DNS name of the remote host to which you intend to connect. </param>
		/// <param name="port">The port number of the remote host to which you intend to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostname" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		// Token: 0x06001DD9 RID: 7641 RVA: 0x00081CE4 File Offset: 0x0007FEE4
		public TcpClient(string hostname, int port)
		{
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			bool on = Logging.On;
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			try
			{
				this.Connect(hostname, port);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (this.m_ClientSocket != null)
				{
					this.m_ClientSocket.Close();
				}
				throw ex;
			}
			bool on2 = Logging.On;
		}

		/// <summary>Gets or sets the underlying <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying network <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x00081D78 File Offset: 0x0007FF78
		// (set) Token: 0x06001DDB RID: 7643 RVA: 0x00081D80 File Offset: 0x0007FF80
		public Socket Client
		{
			get
			{
				return this.m_ClientSocket;
			}
			set
			{
				this.m_ClientSocket = value;
			}
		}

		/// <summary>Gets a value indicating whether the underlying <see cref="T:System.Net.Sockets.Socket" /> for a <see cref="T:System.Net.Sockets.TcpClient" /> is connected to a remote host.</summary>
		/// <returns>true if the <see cref="P:System.Net.Sockets.TcpClient.Client" /> socket was connected to a remote resource as of the most recent operation; otherwise, false.</returns>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00081D89 File Offset: 0x0007FF89
		public bool Connected
		{
			get
			{
				return this.m_ClientSocket.Connected;
			}
		}

		/// <summary>Connects the client to the specified port on the specified host.</summary>
		/// <param name="hostname">The DNS name of the remote host to which you intend to connect. </param>
		/// <param name="port">The port number of the remote host to which you intend to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostname" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.TcpClient" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001DDD RID: 7645 RVA: 0x00081D98 File Offset: 0x0007FF98
		public void Connect(string hostname, int port)
		{
			bool on = Logging.On;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (this.m_Active)
			{
				throw new SocketException(SocketError.IsConnected);
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			Exception ex = null;
			Socket socket = null;
			Socket socket2 = null;
			try
			{
				if (this.m_ClientSocket == null)
				{
					if (Socket.OSSupportsIPv4)
					{
						socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					}
					if (Socket.OSSupportsIPv6)
					{
						socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
					}
				}
				foreach (IPAddress ipaddress in hostAddresses)
				{
					try
					{
						if (this.m_ClientSocket == null)
						{
							if (ipaddress.AddressFamily == AddressFamily.InterNetwork && socket2 != null)
							{
								socket2.Connect(ipaddress, port);
								this.m_ClientSocket = socket2;
								if (socket != null)
								{
									socket.Close();
								}
							}
							else if (socket != null)
							{
								socket.Connect(ipaddress, port);
								this.m_ClientSocket = socket;
								if (socket2 != null)
								{
									socket2.Close();
								}
							}
							this.m_Family = ipaddress.AddressFamily;
							this.m_Active = true;
							break;
						}
						if (ipaddress.AddressFamily == this.m_Family)
						{
							this.Connect(new IPEndPoint(ipaddress, port));
							this.m_Active = true;
							break;
						}
					}
					catch (Exception ex2)
					{
						if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
						{
							throw;
						}
						ex = ex2;
					}
				}
			}
			catch (Exception ex3)
			{
				if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
				{
					throw;
				}
				ex = ex3;
			}
			finally
			{
				if (!this.m_Active)
				{
					if (socket != null)
					{
						socket.Close();
					}
					if (socket2 != null)
					{
						socket2.Close();
					}
					if (ex != null)
					{
						throw ex;
					}
					throw new SocketException(SocketError.NotConnected);
				}
			}
			bool on2 = Logging.On;
		}

		/// <summary>Connects the client to a remote TCP host using the specified remote network endpoint.</summary>
		/// <param name="remoteEP">The <see cref="T:System.Net.IPEndPoint" /> to which you intend to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="remoteEp" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.TcpClient" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001DDE RID: 7646 RVA: 0x00081FA4 File Offset: 0x000801A4
		public void Connect(IPEndPoint remoteEP)
		{
			bool on = Logging.On;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (remoteEP == null)
			{
				throw new ArgumentNullException("remoteEP");
			}
			this.Client.Connect(remoteEP);
			this.m_Active = true;
			bool on2 = Logging.On;
		}

		/// <summary>Begins an asynchronous request for a remote host connection. The remote host is specified by a host name (<see cref="T:System.String" />) and a port number (<see cref="T:System.Int32" />).</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous connection.</returns>
		/// <param name="host">The name of the remote host.</param>
		/// <param name="port">The port number of the remote host.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the connect operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is null. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have permission for the requested operation. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port number is not valid.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001DDF RID: 7647 RVA: 0x00081FF7 File Offset: 0x000801F7
		public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
		{
			bool on = Logging.On;
			IAsyncResult asyncResult = this.Client.BeginConnect(host, port, requestCallback, state);
			bool on2 = Logging.On;
			return asyncResult;
		}

		/// <summary>Ends a pending asynchronous connection attempt.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object returned by a call to <see cref="Overload:System.Net.Sockets.TcpClient.BeginConnect" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter was not returned by a call to a <see cref="Overload:System.Net.Sockets.TcpClient.BeginConnect" /> method. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpClient.EndConnect(System.IAsyncResult)" /> method was previously called for the asynchronous connection. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the <see cref="T:System.Net.Sockets.Socket" />. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001DE0 RID: 7648 RVA: 0x00082015 File Offset: 0x00080215
		public void EndConnect(IAsyncResult asyncResult)
		{
			bool on = Logging.On;
			this.Client.EndConnect(asyncResult);
			this.m_Active = true;
			bool on2 = Logging.On;
		}

		/// <summary>Connects the client to the specified TCP port on the specified host as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />The task object representing the asynchronous operation.</returns>
		/// <param name="host">The DNS name of the remote host to which you intend to connect. </param>
		/// <param name="port">The port number of the remote host to which you intend to connect. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hostname" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />. </exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket. See the Remarks section for more information. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.TcpClient" /> is closed. </exception>
		// Token: 0x06001DE1 RID: 7649 RVA: 0x00082036 File Offset: 0x00080236
		public Task ConnectAsync(string host, int port)
		{
			return Task.Factory.FromAsync<string, int>(new Func<string, int, AsyncCallback, object, IAsyncResult>(this.BeginConnect), new Action<IAsyncResult>(this.EndConnect), host, port, null);
		}

		/// <summary>Returns the <see cref="T:System.Net.Sockets.NetworkStream" /> used to send and receive data.</summary>
		/// <returns>The underlying <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.TcpClient" /> is not connected to a remote host. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.TcpClient" /> has been closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001DE2 RID: 7650 RVA: 0x00082060 File Offset: 0x00080260
		public NetworkStream GetStream()
		{
			bool on = Logging.On;
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.Client.Connected)
			{
				throw new InvalidOperationException(SR.GetString("The operation is not allowed on non-connected sockets."));
			}
			if (this.m_DataStream == null)
			{
				this.m_DataStream = new NetworkStream(this.Client, true);
			}
			bool on2 = Logging.On;
			return this.m_DataStream;
		}

		/// <summary>Disposes this <see cref="T:System.Net.Sockets.TcpClient" /> instance and requests that the underlying TCP connection be closed.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001DE3 RID: 7651 RVA: 0x000820CF File Offset: 0x000802CF
		public void Close()
		{
			bool on = Logging.On;
			((IDisposable)this).Dispose();
			bool on2 = Logging.On;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.TcpClient" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">Set to true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x000820E4 File Offset: 0x000802E4
		protected virtual void Dispose(bool disposing)
		{
			bool on = Logging.On;
			if (this.m_CleanedUp)
			{
				bool on2 = Logging.On;
				return;
			}
			if (disposing)
			{
				IDisposable dataStream = this.m_DataStream;
				if (dataStream != null)
				{
					dataStream.Dispose();
				}
				else
				{
					Socket client = this.Client;
					if (client != null)
					{
						try
						{
							client.InternalShutdown(SocketShutdown.Both);
						}
						finally
						{
							client.Close();
							this.Client = null;
						}
					}
				}
				GC.SuppressFinalize(this);
			}
			this.m_CleanedUp = true;
			bool on3 = Logging.On;
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00082160 File Offset: 0x00080360
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0008216C File Offset: 0x0008036C
		~TcpClient()
		{
			this.Dispose(false);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0008219C File Offset: 0x0008039C
		private void initialize()
		{
			this.Client = new Socket(this.m_Family, SocketType.Stream, ProtocolType.Tcp);
			this.m_Active = false;
		}

		// Token: 0x0400152B RID: 5419
		private Socket m_ClientSocket;

		// Token: 0x0400152C RID: 5420
		private bool m_Active;

		// Token: 0x0400152D RID: 5421
		private NetworkStream m_DataStream;

		// Token: 0x0400152E RID: 5422
		private AddressFamily m_Family;

		// Token: 0x0400152F RID: 5423
		private bool m_CleanedUp;
	}
}
