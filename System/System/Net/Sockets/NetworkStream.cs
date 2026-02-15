using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Provides the underlying stream of data for network access.</summary>
	// Token: 0x020004A4 RID: 1188
	public class NetworkStream : Stream
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- The <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x06001CD4 RID: 7380 RVA: 0x0007D43D File Offset: 0x0007B63D
		public NetworkStream(Socket socket)
			: this(socket, FileAccess.ReadWrite, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <param name="ownsSocket">Set to true to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- the value of the <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- the <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x06001CD5 RID: 7381 RVA: 0x0007D448 File Offset: 0x0007B648
		public NetworkStream(Socket socket, bool ownsSocket)
			: this(socket, FileAccess.ReadWrite, ownsSocket)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Sockets.NetworkStream" /> class for the specified <see cref="T:System.Net.Sockets.Socket" /> with the specified access rights and the specified <see cref="T:System.Net.Sockets.Socket" /> ownership.</summary>
		/// <param name="socket">The <see cref="T:System.Net.Sockets.Socket" /> that the <see cref="T:System.Net.Sockets.NetworkStream" /> will use to send and receive data. </param>
		/// <param name="access">A bitwise combination of the <see cref="T:System.IO.FileAccess" /> values that specifies the type of access given to the <see cref="T:System.Net.Sockets.NetworkStream" /> over the provided <see cref="T:System.Net.Sockets.Socket" />. </param>
		/// <param name="ownsSocket">Set to true to indicate that the <see cref="T:System.Net.Sockets.NetworkStream" /> will take ownership of the <see cref="T:System.Net.Sockets.Socket" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="socket" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The <paramref name="socket" /> parameter is not connected.-or- The <see cref="P:System.Net.Sockets.Socket.SocketType" /> property of the <paramref name="socket" /> parameter is not <see cref="F:System.Net.Sockets.SocketType.Stream" />.-or- The <paramref name="socket" /> parameter is in a nonblocking state. </exception>
		// Token: 0x06001CD6 RID: 7382 RVA: 0x0007D454 File Offset: 0x0007B654
		public NetworkStream(Socket socket, FileAccess access, bool ownsSocket)
		{
			if (socket == null)
			{
				throw new ArgumentNullException("socket");
			}
			if (!socket.Blocking)
			{
				throw new IOException("The operation is not allowed on a non-blocking Socket.");
			}
			if (!socket.Connected)
			{
				throw new IOException("The operation is not allowed on non-connected sockets.");
			}
			if (socket.SocketType != SocketType.Stream)
			{
				throw new IOException("The operation is not allowed on non-stream oriented sockets.");
			}
			this._streamSocket = socket;
			this._ownsSocket = ownsSocket;
			switch (access)
			{
			case FileAccess.Read:
				this._readable = true;
				return;
			case FileAccess.Write:
				this._writeable = true;
				return;
			}
			this._readable = true;
			this._writeable = true;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports reading.</summary>
		/// <returns>true if data can be read from the stream; otherwise, false. The default value is true.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0007D506 File Offset: 0x0007B706
		public override bool CanRead
		{
			get
			{
				return this._readable;
			}
		}

		/// <summary>Gets a value that indicates whether the stream supports seeking. This property is not currently supported.This property always returns false.</summary>
		/// <returns>false in all cases to indicate that <see cref="T:System.Net.Sockets.NetworkStream" /> cannot seek a specific location in the stream.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x000028AE File Offset: 0x00000AAE
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Net.Sockets.NetworkStream" /> supports writing.</summary>
		/// <returns>true if data can be written to the <see cref="T:System.Net.Sockets.NetworkStream" />; otherwise, false. The default value is true.</returns>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x0007D50E File Offset: 0x0007B70E
		public override bool CanWrite
		{
			get
			{
				return this._writeable;
			}
		}

		/// <summary>Indicates whether timeout properties are usable for <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the amount of time that a read operation blocks waiting for data. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a read operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the read operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0007D518 File Offset: 0x0007B718
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0007D546 File Offset: 0x0007B746
		public override int ReadTimeout
		{
			get
			{
				int num = (int)this._streamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.");
				}
				this.SetSocketTimeoutOption(SocketShutdown.Receive, value, false);
			}
		}

		/// <summary>Gets or sets the amount of time that a write operation blocks waiting for data. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the amount of time, in milliseconds, that will elapse before a write operation fails. The default value, <see cref="F:System.Threading.Timeout.Infinite" />, specifies that the write operation does not time out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than or equal to zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0007D56C File Offset: 0x0007B76C
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0007D59A File Offset: 0x0007B79A
		public override int WriteTimeout
		{
			get
			{
				int num = (int)this._streamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.");
				}
				this.SetSocketTimeoutOption(SocketShutdown.Send, value, false);
			}
		}

		/// <summary>Gets the length of the data available on the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The length of the data available on the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00077B22 File Offset: 0x00075D22
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		/// <summary>Gets or sets the current position in the stream. This property is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The current position in the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00077B22 File Offset: 0x00075D22
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x00077B22 File Offset: 0x00075D22
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
			set
			{
				throw new NotSupportedException("This stream does not support seek operations.");
			}
		}

		/// <summary>Sets the current position of the stream to the given value. This method is not currently supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The position in the stream.</returns>
		/// <param name="offset">This parameter is not used. </param>
		/// <param name="origin">This parameter is not used. </param>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		// Token: 0x06001CE2 RID: 7394 RVA: 0x00077B22 File Offset: 0x00075D22
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		/// <summary>Reads data from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data to. </param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.-or- There is a failure reading from the network. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CE3 RID: 7395 RVA: 0x0007D5C0 File Offset: 0x0007B7C0
		public override int Read(byte[] buffer, int offset, int size)
		{
			bool canRead = this.CanRead;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			int num;
			try
			{
				num = this._streamSocket.Receive(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return num;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0007D68C File Offset: 0x0007B88C
		public override int Read(Span<byte> destination)
		{
			if (base.GetType() != typeof(NetworkStream))
			{
				return base.Read(destination);
			}
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			SocketError socketError;
			int num = this._streamSocket.Receive(destination, SocketFlags.None, out socketError);
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException((int)socketError);
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return num;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0007D718 File Offset: 0x0007B918
		public unsafe override int ReadByte()
		{
			byte b;
			if (this.Read(new Span<byte>((void*)(&b), 1)) != 0)
			{
				return (int)b;
			}
			return -1;
		}

		/// <summary>Writes data to the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> from which to start writing data. </param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.IO.IOException">There was a failure while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed.-or- There was a failure reading from the network. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CE6 RID: 7398 RVA: 0x0007D73C File Offset: 0x0007B93C
		public override void Write(byte[] buffer, int offset, int size)
		{
			bool canWrite = this.CanWrite;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			try
			{
				this._streamSocket.Send(buffer, offset, size, SocketFlags.None);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0007D808 File Offset: 0x0007BA08
		public override void Write(ReadOnlySpan<byte> source)
		{
			if (base.GetType() != typeof(NetworkStream))
			{
				base.Write(source);
				return;
			}
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!this.CanWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			SocketError socketError;
			this._streamSocket.Send(source, SocketFlags.None, out socketError);
			if (socketError != SocketError.Success)
			{
				SocketException ex = new SocketException((int)socketError);
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0007D893 File Offset: 0x0007BA93
		public unsafe override void WriteByte(byte value)
		{
			this.Write(new ReadOnlySpan<byte>((void*)(&value), 1));
		}

		/// <summary>Closes the <see cref="T:System.Net.Sockets.NetworkStream" /> after waiting the specified time to allow data to be sent.</summary>
		/// <param name="timeout">A 32-bit signed integer that specifies the number of milliseconds to wait to send any remaining data before closing.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1.</exception>
		// Token: 0x06001CE9 RID: 7401 RVA: 0x0007D8A4 File Offset: 0x0007BAA4
		public void Close(int timeout)
		{
			if (timeout < -1)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			this._closeTimeout = timeout;
			base.Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.NetworkStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001CEA RID: 7402 RVA: 0x0007D8C4 File Offset: 0x0007BAC4
		protected override void Dispose(bool disposing)
		{
			int cleanedUp = (this._cleanedUp ? 1 : 0);
			this._cleanedUp = true;
			if (cleanedUp == 0 && disposing)
			{
				this._readable = false;
				this._writeable = false;
				if (this._ownsSocket)
				{
					this._streamSocket.InternalShutdown(SocketShutdown.Both);
					this._streamSocket.Close(this._closeTimeout);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0007D924 File Offset: 0x0007BB24
		~NetworkStream()
		{
			this.Dispose(false);
		}

		/// <summary>Begins an asynchronous read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that is the location in memory to store data read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin storing the data. </param>
		/// <param name="size">The number of bytes to read from the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes. </param>
		/// <param name="state">An object that contains any additional user-defined data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of the <paramref name="buffer" /> paramater.-or- The <paramref name="size" /> is less than 0.-or- The <paramref name="size" /> is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter.</exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- There was a failure while reading from the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CEC RID: 7404 RVA: 0x0007D954 File Offset: 0x0007BB54
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canRead = this.CanRead;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this._streamSocket.BeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return asyncResult;
		}

		/// <summary>Handles the end of an asynchronous read.</summary>
		/// <returns>The number of bytes read from the <see cref="T:System.Net.Sockets.NetworkStream" />.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that represents an asynchronous call. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CED RID: 7405 RVA: 0x0007DA24 File Offset: 0x0007BC24
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			int num;
			try
			{
				num = this._streamSocket.EndReceive(asyncResult);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return num;
		}

		/// <summary>Begins an asynchronous write to a stream.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous call.</returns>
		/// <param name="buffer">An array of type <see cref="T:System.Byte" /> that contains the data to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="offset">The location in <paramref name="buffer" /> to begin sending the data. </param>
		/// <param name="size">The number of bytes to write to the <see cref="T:System.Net.Sockets.NetworkStream" />. </param>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate that is executed when <see cref="M:System.Net.Sockets.NetworkStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> completes. </param>
		/// <param name="state">An object that contains any additional user-defined data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than 0.-or- The <paramref name="offset" /> parameter is greater than the length of <paramref name="buffer" />.-or- The <paramref name="size" /> parameter is less than 0.-or- The <paramref name="size" /> parameter is greater than the length of <paramref name="buffer" /> minus the value of the <paramref name="offset" /> parameter. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- There was a failure while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CEE RID: 7406 RVA: 0x0007DAB4 File Offset: 0x0007BCB4
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			bool canWrite = this.CanWrite;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this._streamSocket.BeginSend(buffer, offset, size, SocketFlags.None, callback, state);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
			return asyncResult;
		}

		/// <summary>Handles the end of an asynchronous write.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents the asynchronous call. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is closed.-or- An error occurred while writing to the network. -or-An error occurred when accessing the socket. See the Remarks section for more information.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.NetworkStream" /> is closed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CEF RID: 7407 RVA: 0x0007DB84 File Offset: 0x0007BD84
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			try
			{
				this._streamSocket.EndSend(asyncResult);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0007DC10 File Offset: 0x0007BE10
		public override Task<int> ReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			bool canRead = this.CanRead;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Task<int> task;
			try
			{
				task = this._streamSocket.ReceiveAsync(new Memory<byte>(buffer, offset, size), SocketFlags.None, true, cancellationToken).AsTask();
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return task;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0007DCEC File Offset: 0x0007BEEC
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			bool canRead = this.CanRead;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canRead)
			{
				throw new InvalidOperationException("The stream does not support reading.");
			}
			ValueTask<int> valueTask;
			try
			{
				valueTask = this._streamSocket.ReceiveAsync(buffer, SocketFlags.None, true, cancellationToken);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to read data from the transport connection: {0}.", ex.Message), ex);
			}
			return valueTask;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0007DD84 File Offset: 0x0007BF84
		public override Task WriteAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			bool canWrite = this.CanWrite;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if ((ulong)offset > (ulong)((long)buffer.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if ((ulong)size > (ulong)((long)(buffer.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Task task;
			try
			{
				task = this._streamSocket.SendAsyncForNetworkStream(new ReadOnlyMemory<byte>(buffer, offset, size), SocketFlags.None, cancellationToken).AsTask();
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
			return task;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0007DE60 File Offset: 0x0007C060
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
		{
			bool canWrite = this.CanWrite;
			if (this._cleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (!canWrite)
			{
				throw new InvalidOperationException("The stream does not support writing.");
			}
			ValueTask valueTask;
			try
			{
				valueTask = this._streamSocket.SendAsyncForNetworkStream(buffer, SocketFlags.None, cancellationToken);
			}
			catch (Exception ex) when (!(ex is OutOfMemoryException))
			{
				throw new IOException(SR.Format("Unable to write data to the transport connection: {0}.", ex.Message), ex);
			}
			return valueTask;
		}

		/// <summary>Flushes data from the stream. This method is reserved for future use.</summary>
		// Token: 0x06001CF4 RID: 7412 RVA: 0x00002FA0 File Offset: 0x000011A0
		public override void Flush()
		{
		}

		/// <summary>Flushes data from the stream as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x06001CF5 RID: 7413 RVA: 0x0007DEF4 File Offset: 0x0007C0F4
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		/// <summary>Sets the length of the stream. This method always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">This parameter is not used. </param>
		/// <exception cref="T:System.NotSupportedException">Any use of this property. </exception>
		// Token: 0x06001CF6 RID: 7414 RVA: 0x00077B22 File Offset: 0x00075D22
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This stream does not support seek operations.");
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0007DEFC File Offset: 0x0007C0FC
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout, bool silent)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, mode, timeout, silent, "SetSocketTimeoutOption");
			}
			if (timeout < 0)
			{
				timeout = 0;
			}
			if ((mode == SocketShutdown.Send || mode == SocketShutdown.Both) && timeout != this._currentWriteTimeout)
			{
				this._streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout, silent);
				this._currentWriteTimeout = timeout;
			}
			if ((mode == SocketShutdown.Receive || mode == SocketShutdown.Both) && timeout != this._currentReadTimeout)
			{
				this._streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout, silent);
				this._currentReadTimeout = timeout;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x0007DF94 File Offset: 0x0007C194
		internal Socket InternalSocket
		{
			get
			{
				Socket streamSocket = this._streamSocket;
				if (this._cleanedUp || streamSocket == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				return streamSocket;
			}
		}

		// Token: 0x040013E3 RID: 5091
		private readonly Socket _streamSocket;

		// Token: 0x040013E4 RID: 5092
		private readonly bool _ownsSocket;

		// Token: 0x040013E5 RID: 5093
		private bool _readable;

		// Token: 0x040013E6 RID: 5094
		private bool _writeable;

		// Token: 0x040013E7 RID: 5095
		private int _closeTimeout = -1;

		// Token: 0x040013E8 RID: 5096
		private volatile bool _cleanedUp;

		// Token: 0x040013E9 RID: 5097
		private int _currentReadTimeout = -1;

		// Token: 0x040013EA RID: 5098
		private int _currentWriteTimeout = -1;
	}
}
