using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	/// <summary>Provides methods and properties used to compress and decompress streams.</summary>
	// Token: 0x02000372 RID: 882
	public class GZipStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression mode.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> enumeration value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x060015C8 RID: 5576 RVA: 0x0005D355 File Offset: 0x0005B555
		public GZipStream(Stream stream, CompressionMode mode)
			: this(stream, mode, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.GZipStream" /> class by using the specified stream and compression mode, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to compress or decompress.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to compress or decompress the stream.</param>
		/// <param name="leaveOpen">true to leave the stream open after disposing the <see cref="T:System.IO.Compression.GZipStream" /> object; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid <see cref="T:System.IO.Compression.CompressionMode" /> value.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Compress" />  and <see cref="P:System.IO.Stream.CanWrite" /> is false.-or-<see cref="T:System.IO.Compression.CompressionMode" /> is <see cref="F:System.IO.Compression.CompressionMode.Decompress" />  and <see cref="P:System.IO.Stream.CanRead" /> is false.</exception>
		// Token: 0x060015C9 RID: 5577 RVA: 0x0005D360 File Offset: 0x0005B560
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
		{
			this._deflateStream = new DeflateStream(stream, mode, leaveOpen, 31);
		}

		/// <summary>Gets a value indicating whether the stream supports reading while decompressing a file.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Decompress, and the underlying stream supports reading and is not closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0005D378 File Offset: 0x0005B578
		public override bool CanRead
		{
			get
			{
				DeflateStream deflateStream = this._deflateStream;
				return deflateStream != null && deflateStream.CanRead;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports writing.</summary>
		/// <returns>true if the <see cref="T:System.IO.Compression.CompressionMode" /> value is Compress, and the underlying stream supports writing and is not closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0005D38B File Offset: 0x0005B58B
		public override bool CanWrite
		{
			get
			{
				DeflateStream deflateStream = this._deflateStream;
				return deflateStream != null && deflateStream.CanWrite;
			}
		}

		/// <summary>Gets a value indicating whether the stream supports seeking.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x0005D39E File Offset: 0x0005B59E
		public override bool CanSeek
		{
			get
			{
				DeflateStream deflateStream = this._deflateStream;
				return deflateStream != null && deflateStream.CanSeek;
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0005D3B1 File Offset: 0x0005B5B1
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x0005D3B1 File Offset: 0x0005B5B1
		// (set) Token: 0x060015CF RID: 5583 RVA: 0x0005D3B1 File Offset: 0x0005B5B1
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		/// <summary>The current implementation of this method has no functionality.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060015D0 RID: 5584 RVA: 0x0005D3BD File Offset: 0x0005B5BD
		public override void Flush()
		{
			this.CheckDeflateStream();
			this._deflateStream.Flush();
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>A long value.</returns>
		/// <param name="offset">The location in the stream.</param>
		/// <param name="origin">One of the <see cref="T:System.IO.SeekOrigin" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x060015D1 RID: 5585 RVA: 0x0005D3B1 File Offset: 0x0005B5B1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		/// <summary>This property is not supported and always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <param name="value">The length of the stream.</param>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this stream.</exception>
		// Token: 0x060015D2 RID: 5586 RVA: 0x0005D3B1 File Offset: 0x0005B5B1
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0005D3D0 File Offset: 0x0005B5D0
		public override int ReadByte()
		{
			this.CheckDeflateStream();
			return this._deflateStream.ReadByte();
		}

		/// <summary>Begins an asynchronous read operation. (Consider using the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>An object that represents the asynchronous read operation, which could still be pending.</returns>
		/// <param name="array">The byte array to read the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="asyncCallback">An optional asynchronous callback, to be called when the read operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <exception cref="T:System.IO.IOException">The method tried to  read asynchronously past the end of the stream, or a disk error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.IO.Compression.GZipStream" /> implementation does not support the read operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">A read operation cannot be performed because the stream is closed.</exception>
		// Token: 0x060015D4 RID: 5588 RVA: 0x0005D3E3 File Offset: 0x0005B5E3
		public override IAsyncResult BeginRead(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(array, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		/// <summary>Waits for the pending asynchronous read to complete. (Consider using the the <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>The number of bytes read from the stream, between 0 (zero) and the number of bytes you requested. <see cref="T:System.IO.Compression.GZipStream" /> returns 0 only at the end of the stream; otherwise, it blocks until at least one byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Compression.DeflateStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">The end operation cannot be performed because the stream is closed.</exception>
		// Token: 0x060015D5 RID: 5589 RVA: 0x0005D3FC File Offset: 0x0005B5FC
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		/// <summary>Reads a number of decompressed bytes into the specified byte array.</summary>
		/// <returns>The number of bytes that were decompressed into the byte array. If the end of the stream has been reached, zero or the number of bytes read is returned.</returns>
		/// <param name="array">The array used to store decompressed bytes.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of decompressed bytes to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.IO.Compression.CompressionMode" /> value was Compress when the object was created.- or -The underlying stream does not support reading.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is less than zero.-or-<paramref name="array" /> length minus the index starting point is less than <paramref name="count" />.</exception>
		/// <exception cref="T:System.IO.InvalidDataException">The data is in an invalid format.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x060015D6 RID: 5590 RVA: 0x0005D404 File Offset: 0x0005B604
		public override int Read(byte[] array, int offset, int count)
		{
			this.CheckDeflateStream();
			return this._deflateStream.Read(array, offset, count);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0005D41A File Offset: 0x0005B61A
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() != typeof(GZipStream))
			{
				return base.Read(buffer);
			}
			this.CheckDeflateStream();
			return this._deflateStream.ReadCore(buffer);
		}

		/// <summary>Begins an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <returns>An  object that represents the asynchronous write operation, which could still be pending.</returns>
		/// <param name="array">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="asyncCallback">An optional asynchronous callback to be called when the write operation is complete.</param>
		/// <param name="asyncState">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying stream is null. -or-The underlying stream is closed.</exception>
		// Token: 0x060015D8 RID: 5592 RVA: 0x0005D44D File Offset: 0x0005B64D
		public override IAsyncResult BeginWrite(byte[] array, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.WriteAsync(array, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		/// <summary>Handles the end of an asynchronous write operation. (Consider using the <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> method instead; see the Remarks section.)</summary>
		/// <param name="asyncResult">The object that represents the asynchronous call.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying stream is null. -or-The underlying stream is closed.</exception>
		// Token: 0x060015D9 RID: 5593 RVA: 0x0005D466 File Offset: 0x0005B666
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		/// <summary>Writes compressed bytes to the underlying stream from the specified byte array.</summary>
		/// <param name="array">The buffer that contains the data to compress.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> from which the bytes will be read.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The write operation cannot be performed because the stream is closed.</exception>
		// Token: 0x060015DA RID: 5594 RVA: 0x0005D46E File Offset: 0x0005B66E
		public override void Write(byte[] array, int offset, int count)
		{
			this.CheckDeflateStream();
			this._deflateStream.Write(array, offset, count);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0005D484 File Offset: 0x0005B684
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() != typeof(GZipStream))
			{
				base.Write(buffer);
				return;
			}
			this.CheckDeflateStream();
			this._deflateStream.WriteCore(buffer);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0005D4B7 File Offset: 0x0005B6B7
		public override void CopyTo(Stream destination, int bufferSize)
		{
			this.CheckDeflateStream();
			this._deflateStream.CopyTo(destination, bufferSize);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Compression.GZipStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060015DD RID: 5597 RVA: 0x0005D4CC File Offset: 0x0005B6CC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._deflateStream != null)
				{
					this._deflateStream.Dispose();
				}
				this._deflateStream = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0005D510 File Offset: 0x0005B710
		public override Task<int> ReadAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckDeflateStream();
			return this._deflateStream.ReadAsync(array, offset, count, cancellationToken);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0005D528 File Offset: 0x0005B728
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(GZipStream))
			{
				return base.ReadAsync(buffer, cancellationToken);
			}
			this.CheckDeflateStream();
			return this._deflateStream.ReadAsyncMemory(buffer, cancellationToken);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0005D55D File Offset: 0x0005B75D
		public override Task WriteAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckDeflateStream();
			return this._deflateStream.WriteAsync(array, offset, count, cancellationToken);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0005D575 File Offset: 0x0005B775
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(GZipStream))
			{
				return base.WriteAsync(buffer, cancellationToken);
			}
			this.CheckDeflateStream();
			return this._deflateStream.WriteAsyncMemory(buffer, cancellationToken);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0005D5AA File Offset: 0x0005B7AA
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.CheckDeflateStream();
			return this._deflateStream.FlushAsync(cancellationToken);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0005D5BE File Offset: 0x0005B7BE
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			this.CheckDeflateStream();
			return this._deflateStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0005D5D4 File Offset: 0x0005B7D4
		private void CheckDeflateStream()
		{
			if (this._deflateStream == null)
			{
				GZipStream.ThrowStreamClosedException();
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x0005D5E3 File Offset: 0x0005B7E3
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowStreamClosedException()
		{
			throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
		}

		// Token: 0x04000D33 RID: 3379
		private DeflateStream _deflateStream;
	}
}
