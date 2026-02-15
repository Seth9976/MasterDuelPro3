using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Provides a generic view of a sequence of bytes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020007CA RID: 1994
	[Serializable]
	public abstract class Stream : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x06003FA8 RID: 16296 RVA: 0x000F5962 File Offset: 0x000F3B62
		internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream supports reading; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003FA9 RID: 16297
		public abstract bool CanRead { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream supports seeking; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003FAA RID: 16298
		public abstract bool CanSeek { get; }

		/// <summary>Gets a value that determines whether the current stream can time out.</summary>
		/// <returns>A value that determines whether the current stream can time out.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003FAB RID: 16299 RVA: 0x00033991 File Offset: 0x00031B91
		public virtual bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06003FAC RID: 16300
		public abstract bool CanWrite { get; }

		/// <summary>When overridden in a derived class, gets the length in bytes of the stream.</summary>
		/// <returns>A long value representing the length of the stream in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003FAD RID: 16301
		public abstract long Length { get; }

		/// <summary>When overridden in a derived class, gets or sets the position within the current stream.</summary>
		/// <returns>The current position within the stream.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06003FAE RID: 16302
		// (set) Token: 0x06003FAF RID: 16303
		public abstract long Position { get; set; }

		/// <summary>Gets or sets a value, in miliseconds, that determines how long the stream will attempt to read before timing out. </summary>
		/// <returns>A value, in miliseconds, that determines how long the stream will attempt to read before timing out.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Stream.ReadTimeout" /> method always throws an <see cref="T:System.InvalidOperationException" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x000F598E File Offset: 0x000F3B8E
		// (set) Token: 0x06003FB1 RID: 16305 RVA: 0x000F598E File Offset: 0x000F3B8E
		public virtual int ReadTimeout
		{
			get
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
			set
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
		}

		/// <summary>Gets or sets a value, in miliseconds, that determines how long the stream will attempt to write before timing out. </summary>
		/// <returns>A value, in miliseconds, that determines how long the stream will attempt to write before timing out.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Stream.WriteTimeout" /> method always throws an <see cref="T:System.InvalidOperationException" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x000F598E File Offset: 0x000F3B8E
		// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x000F598E File Offset: 0x000F3B8E
		public virtual int WriteTimeout
		{
			get
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
			set
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
		}

		/// <summary>Asynchronously reads the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token.</summary>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero. The default size is 4096.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is negative or zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x06003FB4 RID: 16308 RVA: 0x000F599A File Offset: 0x000F3B9A
		public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x000F59B0 File Offset: 0x000F3BB0
		private async Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				for (;;)
				{
					int num = await this.ReadAsync(new Memory<byte>(buffer), cancellationToken).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					await destination.WriteAsync(new ReadOnlyMemory<byte>(buffer, 0, num), cancellationToken).ConfigureAwait(false);
				}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(buffer, false);
			}
		}

		/// <summary>Reads the bytes from the current stream and writes them to another stream.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading.-or-<paramref name="destination" /> does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or <paramref name="destination" /> were closed before the <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> method was called.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06003FB6 RID: 16310 RVA: 0x000F5A0C File Offset: 0x000F3C0C
		public void CopyTo(Stream destination)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			this.CopyTo(destination, copyBufferSize);
		}

		/// <summary>Reads the bytes from the current stream and writes them to another stream, using a specified buffer size.</summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size of the buffer. This value must be greater than zero. The default size is 4096.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading.-or-<paramref name="destination" /> does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or <paramref name="destination" /> were closed before the <see cref="M:System.IO.Stream.CopyTo(System.IO.Stream)" /> method was called.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x06003FB7 RID: 16311 RVA: 0x000F5A28 File Offset: 0x000F3C28
		public virtual void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			byte[] array = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				int num;
				while ((num = this.Read(array, 0, array.Length)) != 0)
				{
					destination.Write(array, 0, num);
				}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x000F5A84 File Offset: 0x000F3C84
		private int GetCopyBufferSize()
		{
			int num = 81920;
			if (this.CanSeek)
			{
				long length = this.Length;
				long position = this.Position;
				if (length <= position)
				{
					num = 1;
				}
				else
				{
					long num2 = length - position;
					if (num2 > 0L)
					{
						num = (int)Math.Min((long)num, num2);
					}
				}
			}
			return num;
		}

		/// <summary>Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. Instead of calling this method, ensure that the stream is properly disposed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FB9 RID: 16313 RVA: 0x000F5AC9 File Offset: 0x000F3CC9
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.Stream" />.</summary>
		// Token: 0x06003FBA RID: 16314 RVA: 0x0007A955 File Offset: 0x00078B55
		public void Dispose()
		{
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Stream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06003FBB RID: 16315 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FBC RID: 16316
		public abstract void Flush();

		/// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06003FBD RID: 16317 RVA: 0x000F5AD8 File Offset: 0x000F3CD8
		public virtual Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((Stream)state).Flush();
			}, this, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Begins an asynchronous read operation. (Consider using <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the asynchronous read, which could still be pending.</returns>
		/// <param name="buffer">The buffer to read the data into. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data read from the stream. </param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <param name="callback">An optional asynchronous callback, to be called when the read is complete. </param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests. </param>
		/// <exception cref="T:System.IO.IOException">Attempted an asynchronous read past the end of the stream, or a disk error occurs. </exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The current Stream implementation does not support the read operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FBE RID: 16318 RVA: 0x000F5B0B File Offset: 0x000F3D0B
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginReadInternal(buffer, offset, count, callback, state, false, true);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x000F5B1C File Offset: 0x000F3D1C
		internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously, bool apm)
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(true, apm, delegate
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int num;
				try
				{
					num = readWriteTask2._stream.Read(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				}
				finally
				{
					if (!readWriteTask2._apm)
					{
						readWriteTask2._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask2.ClearBeginState();
				}
				return num;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		/// <summary>Waits for the pending asynchronous read to complete. (Consider using <see cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)</summary>
		/// <returns>The number of bytes read from the stream, between zero (0) and the number of bytes you requested. Streams return zero (0) only at the end of the stream, otherwise, they should block until at least one byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A handle to the pending read operation is not available.-or-The pending operation does not support reading.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Stream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FC0 RID: 16320 RVA: 0x000F5B98 File Offset: 0x000F3D98
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			if (!activeReadWriteTask._isRead)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			int result;
			try
			{
				result = activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this.FinishTrackingAsyncOperation();
			}
			return result;
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation. </exception>
		// Token: 0x06003FC1 RID: 16321 RVA: 0x000F5C14 File Offset: 0x000F3E14
		public Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			return this.ReadAsync(buffer, offset, count, CancellationToken.None);
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation. </exception>
		// Token: 0x06003FC2 RID: 16322 RVA: 0x000F5C24 File Offset: 0x000F3E24
		public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndReadAsync(buffer, offset, count);
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x000F5C40 File Offset: 0x000F3E40
		public virtual ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return new ValueTask<int>(this.ReadAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken));
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			return Stream.<ReadAsync>g__FinishReadAsync|44_0(this.ReadAsync(array, 0, buffer.Length, cancellationToken), array, buffer);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000F5CA8 File Offset: 0x000F3EA8
		private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
		{
			if (!this.HasOverriddenBeginEndRead())
			{
				return (Task<int>)this.BeginReadInternal(buffer, offset, count, null, null, true, false);
			}
			return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state), (Stream stream, IAsyncResult asyncResult) => stream.EndRead(asyncResult));
		}

		/// <summary>Begins an asynchronous write operation. (Consider using <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)</summary>
		/// <returns>An IAsyncResult that represents the asynchronous write, which could still be pending.</returns>
		/// <param name="buffer">The buffer to write data from. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> from which to begin writing. </param>
		/// <param name="count">The maximum number of bytes to write. </param>
		/// <param name="callback">An optional asynchronous callback, to be called when the write is complete. </param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests. </param>
		/// <exception cref="T:System.IO.IOException">Attempted an asynchronous write past the end of the stream, or a disk error occurs. </exception>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments is invalid. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The current Stream implementation does not support the write operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FC5 RID: 16325 RVA: 0x000F5D35 File Offset: 0x000F3F35
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginWriteInternal(buffer, offset, count, callback, state, false, true);
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x000F5D48 File Offset: 0x000F3F48
		internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously, bool apm)
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(false, apm, delegate
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int num;
				try
				{
					readWriteTask2._stream.Write(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
					num = 0;
				}
				finally
				{
					if (!readWriteTask2._apm)
					{
						readWriteTask2._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask2.ClearBeginState();
				}
				return num;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x000F5DC4 File Offset: 0x000F3FC4
		private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
		{
			if (asyncWaiter.IsCompleted)
			{
				this.RunReadWriteTask(readWriteTask);
				return;
			}
			asyncWaiter.ContinueWith(delegate(Task t, object state)
			{
				Stream.ReadWriteTask readWriteTask2 = (Stream.ReadWriteTask)state;
				readWriteTask2._stream.RunReadWriteTask(readWriteTask2);
			}, readWriteTask, default(CancellationToken), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x000F5E1B File Offset: 0x000F401B
		private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
		{
			this._activeReadWriteTask = readWriteTask;
			readWriteTask.m_taskScheduler = TaskScheduler.Default;
			readWriteTask.ScheduleAndStart(false);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x000F5E36 File Offset: 0x000F4036
		private void FinishTrackingAsyncOperation()
		{
			this._activeReadWriteTask = null;
			this._asyncActiveSemaphore.Release();
		}

		/// <summary>Ends an asynchronous write operation. (Consider using <see cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)" /> instead; see the Remarks section.)</summary>
		/// <param name="asyncResult">A reference to the outstanding asynchronous I/O request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A handle to the pending write operation is not available.-or-The pending operation does not support writing.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FCA RID: 16330 RVA: 0x000F5E4C File Offset: 0x000F404C
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask._isRead)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			try
			{
				activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this.FinishTrackingAsyncOperation();
			}
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation. </exception>
		// Token: 0x06003FCB RID: 16331 RVA: 0x000F5EC8 File Offset: 0x000F40C8
		public Task WriteAsync(byte[] buffer, int offset, int count)
		{
			return this.WriteAsync(buffer, offset, count, CancellationToken.None);
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation. </exception>
		// Token: 0x06003FCC RID: 16332 RVA: 0x000F5ED8 File Offset: 0x000F40D8
		public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndWriteAsync(buffer, offset, count);
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x000F5EF4 File Offset: 0x000F40F4
		public virtual ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return new ValueTask(this.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken));
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			buffer.Span.CopyTo(array);
			return new ValueTask(this.FinishWriteAsync(this.WriteAsync(array, 0, buffer.Length, cancellationToken), array));
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x000F5F70 File Offset: 0x000F4170
		private async Task FinishWriteAsync(Task writeTask, byte[] localBuffer)
		{
			try
			{
				await writeTask.ConfigureAwait(false);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(localBuffer, false);
			}
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000F5FBC File Offset: 0x000F41BC
		private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
		{
			if (!this.HasOverriddenBeginEndWrite())
			{
				return (Task)this.BeginWriteInternal(buffer, offset, count, null, null, true, false);
			}
			return TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state), delegate(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			});
		}

		/// <summary>When overridden in a derived class, sets the position within the current stream.</summary>
		/// <returns>The new position within the current stream.</returns>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter. </param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FD0 RID: 16336
		public abstract long Seek(long offset, SeekOrigin origin);

		/// <summary>When overridden in a derived class, sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FD1 RID: 16337
		public abstract void SetLength(long value);

		/// <summary>When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream. </param>
		/// <param name="count">The maximum number of bytes to be read from the current stream. </param>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FD2 RID: 16338
		public abstract int Read(byte[] buffer, int offset, int count);

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000F604C File Offset: 0x000F424C
		public virtual int Read(Span<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			int num2;
			try
			{
				int num = this.Read(array, 0, buffer.Length);
				if ((ulong)num > (ulong)((long)buffer.Length))
				{
					throw new IOException("Stream was too long.");
				}
				new Span<byte>(array, 0, num).CopyTo(buffer);
				num2 = num;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return num2;
		}

		/// <summary>Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.</summary>
		/// <returns>The unsigned byte cast to an Int32, or -1 if at the end of the stream.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FD4 RID: 16340 RVA: 0x000F60C8 File Offset: 0x000F42C8
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		/// <summary>When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream. </param>
		/// <param name="count">The number of bytes to be written to the current stream. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FD5 RID: 16341
		public abstract void Write(byte[] buffer, int offset, int count);

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000F60EC File Offset: 0x000F42EC
		public virtual void Write(ReadOnlySpan<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(array);
				this.Write(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		/// <summary>Writes a byte to the current position in the stream and advances the position within the stream by one byte.</summary>
		/// <param name="value">The byte to write to the stream. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing, or the stream is already closed. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003FD7 RID: 16343 RVA: 0x000F6148 File Offset: 0x000F4348
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[] { value }, 0, 1);
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000F616C File Offset: 0x000F436C
		internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(this.Read(buffer, offset, count), state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, false);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000F61B4 File Offset: 0x000F43B4
		internal static int BlockingEndRead(IAsyncResult asyncResult)
		{
			return Stream.SynchronousAsyncResult.EndRead(asyncResult);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000F61BC File Offset: 0x000F43BC
		internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				this.Write(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, true);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000F6204 File Offset: 0x000F4404
		internal static void BlockingEndWrite(IAsyncResult asyncResult)
		{
			Stream.SynchronousAsyncResult.EndWrite(asyncResult);
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x0000C091 File Offset: 0x0000A291
		private bool HasOverriddenBeginEndRead()
		{
			return true;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x0000C091 File Offset: 0x0000A291
		private bool HasOverriddenBeginEndWrite()
		{
			return true;
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000F6218 File Offset: 0x000F4418
		[CompilerGenerated]
		internal static async ValueTask<int> <ReadAsync>g__FinishReadAsync|44_0(Task<int> readTask, byte[] localBuffer, Memory<byte> localDestination)
		{
			int num2;
			try
			{
				int num = await readTask.ConfigureAwait(false);
				new Span<byte>(localBuffer, 0, num).CopyTo(localDestination.Span);
				num2 = num;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(localBuffer, false);
			}
			return num2;
		}

		/// <summary>A Stream with no backing store.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04002083 RID: 8323
		public static readonly Stream Null = new Stream.NullStream();

		// Token: 0x04002084 RID: 8324
		[NonSerialized]
		private Stream.ReadWriteTask _activeReadWriteTask;

		// Token: 0x04002085 RID: 8325
		[NonSerialized]
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x020007CB RID: 1995
		private struct ReadWriteParameters
		{
			// Token: 0x04002086 RID: 8326
			internal byte[] Buffer;

			// Token: 0x04002087 RID: 8327
			internal int Offset;

			// Token: 0x04002088 RID: 8328
			internal int Count;
		}

		// Token: 0x020007CC RID: 1996
		private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
		{
			// Token: 0x06003FE1 RID: 16353 RVA: 0x000F626B File Offset: 0x000F446B
			internal void ClearBeginState()
			{
				this._stream = null;
				this._buffer = null;
			}

			// Token: 0x06003FE2 RID: 16354 RVA: 0x000F627C File Offset: 0x000F447C
			public ReadWriteTask(bool isRead, bool apm, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback)
				: base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
			{
				this._isRead = isRead;
				this._apm = apm;
				this._stream = stream;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				if (callback != null)
				{
					this._callback = callback;
					this._context = ExecutionContext.Capture();
					base.AddCompletionAction(this);
				}
			}

			// Token: 0x06003FE3 RID: 16355 RVA: 0x000F62E4 File Offset: 0x000F44E4
			private static void InvokeAsyncCallback(object completedTask)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)completedTask;
				AsyncCallback callback = readWriteTask._callback;
				readWriteTask._callback = null;
				callback(readWriteTask);
			}

			// Token: 0x06003FE4 RID: 16356 RVA: 0x000F630C File Offset: 0x000F450C
			void ITaskCompletionAction.Invoke(Task completingTask)
			{
				ExecutionContext context = this._context;
				if (context == null)
				{
					AsyncCallback callback = this._callback;
					this._callback = null;
					callback(completingTask);
					return;
				}
				this._context = null;
				ContextCallback contextCallback = Stream.ReadWriteTask.s_invokeAsyncCallback;
				if (contextCallback == null)
				{
					contextCallback = (Stream.ReadWriteTask.s_invokeAsyncCallback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback));
				}
				ExecutionContext.RunInternal(context, contextCallback, this);
			}

			// Token: 0x17000A43 RID: 2627
			// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x0000C091 File Offset: 0x0000A291
			bool ITaskCompletionAction.InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04002089 RID: 8329
			internal readonly bool _isRead;

			// Token: 0x0400208A RID: 8330
			internal readonly bool _apm;

			// Token: 0x0400208B RID: 8331
			internal Stream _stream;

			// Token: 0x0400208C RID: 8332
			internal byte[] _buffer;

			// Token: 0x0400208D RID: 8333
			internal readonly int _offset;

			// Token: 0x0400208E RID: 8334
			internal readonly int _count;

			// Token: 0x0400208F RID: 8335
			private AsyncCallback _callback;

			// Token: 0x04002090 RID: 8336
			private ExecutionContext _context;

			// Token: 0x04002091 RID: 8337
			private static ContextCallback s_invokeAsyncCallback;
		}

		// Token: 0x020007CD RID: 1997
		private sealed class NullStream : Stream
		{
			// Token: 0x06003FE6 RID: 16358 RVA: 0x000F6362 File Offset: 0x000F4562
			internal NullStream()
			{
			}

			// Token: 0x17000A44 RID: 2628
			// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x0000C091 File Offset: 0x0000A291
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000A45 RID: 2629
			// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x0000C091 File Offset: 0x0000A291
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000A46 RID: 2630
			// (get) Token: 0x06003FE9 RID: 16361 RVA: 0x0000C091 File Offset: 0x0000A291
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000A47 RID: 2631
			// (get) Token: 0x06003FEA RID: 16362 RVA: 0x0004737A File Offset: 0x0004557A
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000A48 RID: 2632
			// (get) Token: 0x06003FEB RID: 16363 RVA: 0x0004737A File Offset: 0x0004557A
			// (set) Token: 0x06003FEC RID: 16364 RVA: 0x00002C89 File Offset: 0x00000E89
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x06003FED RID: 16365 RVA: 0x000F636A File Offset: 0x000F456A
			public override void CopyTo(Stream destination, int bufferSize)
			{
				StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			}

			// Token: 0x06003FEE RID: 16366 RVA: 0x000F6374 File Offset: 0x000F4574
			public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
			{
				StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x06003FEF RID: 16367 RVA: 0x00002C89 File Offset: 0x00000E89
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06003FF0 RID: 16368 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void Flush()
			{
			}

			// Token: 0x06003FF1 RID: 16369 RVA: 0x000F6393 File Offset: 0x000F4593
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x06003FF2 RID: 16370 RVA: 0x000F63AA File Offset: 0x000F45AA
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanRead)
				{
					throw Error.GetReadNotSupported();
				}
				return base.BlockingBeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x06003FF3 RID: 16371 RVA: 0x000F63C7 File Offset: 0x000F45C7
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				return Stream.BlockingEndRead(asyncResult);
			}

			// Token: 0x06003FF4 RID: 16372 RVA: 0x000F63DD File Offset: 0x000F45DD
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanWrite)
				{
					throw Error.GetWriteNotSupported();
				}
				return base.BlockingBeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06003FF5 RID: 16373 RVA: 0x000F63FA File Offset: 0x000F45FA
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream.BlockingEndWrite(asyncResult);
			}

			// Token: 0x06003FF6 RID: 16374 RVA: 0x00033991 File Offset: 0x00031B91
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06003FF7 RID: 16375 RVA: 0x00033991 File Offset: 0x00031B91
			public override int Read(Span<byte> buffer)
			{
				return 0;
			}

			// Token: 0x06003FF8 RID: 16376 RVA: 0x000F6410 File Offset: 0x000F4610
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				return Stream.NullStream.s_zeroTask;
			}

			// Token: 0x06003FF9 RID: 16377 RVA: 0x000F6417 File Offset: 0x000F4617
			public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				return new ValueTask<int>(0);
			}

			// Token: 0x06003FFA RID: 16378 RVA: 0x000CE6F2 File Offset: 0x000CC8F2
			public override int ReadByte()
			{
				return -1;
			}

			// Token: 0x06003FFB RID: 16379 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x06003FFC RID: 16380 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void Write(ReadOnlySpan<byte> buffer)
			{
			}

			// Token: 0x06003FFD RID: 16381 RVA: 0x000F641F File Offset: 0x000F461F
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x06003FFE RID: 16382 RVA: 0x000F6438 File Offset: 0x000F4638
			public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return default(ValueTask);
				}
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}

			// Token: 0x06003FFF RID: 16383 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x06004000 RID: 16384 RVA: 0x0004737A File Offset: 0x0004557A
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06004001 RID: 16385 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void SetLength(long length)
			{
			}

			// Token: 0x04002092 RID: 8338
			private static readonly Task<int> s_zeroTask = Task.FromResult<int>(0);
		}

		// Token: 0x020007CE RID: 1998
		private sealed class SynchronousAsyncResult : IAsyncResult
		{
			// Token: 0x06004003 RID: 16387 RVA: 0x000F6470 File Offset: 0x000F4670
			internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
			{
				this._bytesRead = bytesRead;
				this._stateObject = asyncStateObject;
			}

			// Token: 0x06004004 RID: 16388 RVA: 0x000F6486 File Offset: 0x000F4686
			internal SynchronousAsyncResult(object asyncStateObject)
			{
				this._stateObject = asyncStateObject;
				this._isWrite = true;
			}

			// Token: 0x06004005 RID: 16389 RVA: 0x000F649C File Offset: 0x000F469C
			internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
			{
				this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
				this._stateObject = asyncStateObject;
				this._isWrite = isWrite;
			}

			// Token: 0x17000A49 RID: 2633
			// (get) Token: 0x06004006 RID: 16390 RVA: 0x0000C091 File Offset: 0x0000A291
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000A4A RID: 2634
			// (get) Token: 0x06004007 RID: 16391 RVA: 0x000F64BE File Offset: 0x000F46BE
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, () => new ManualResetEvent(true));
				}
			}

			// Token: 0x17000A4B RID: 2635
			// (get) Token: 0x06004008 RID: 16392 RVA: 0x000F64EA File Offset: 0x000F46EA
			public object AsyncState
			{
				get
				{
					return this._stateObject;
				}
			}

			// Token: 0x17000A4C RID: 2636
			// (get) Token: 0x06004009 RID: 16393 RVA: 0x0000C091 File Offset: 0x0000A291
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600400A RID: 16394 RVA: 0x000F64F2 File Offset: 0x000F46F2
			internal void ThrowIfError()
			{
				if (this._exceptionInfo != null)
				{
					this._exceptionInfo.Throw();
				}
			}

			// Token: 0x0600400B RID: 16395 RVA: 0x000F6508 File Offset: 0x000F4708
			internal static int EndRead(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
				{
					throw new ArgumentException("IAsyncResult object did not come from the corresponding async method on this type.");
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					throw new ArgumentException("EndRead can only be called once for each asynchronous operation.");
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
				return synchronousAsyncResult._bytesRead;
			}

			// Token: 0x0600400C RID: 16396 RVA: 0x000F6558 File Offset: 0x000F4758
			internal static void EndWrite(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
				{
					throw new ArgumentException("IAsyncResult object did not come from the corresponding async method on this type.");
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					throw new ArgumentException("EndWrite can only be called once for each asynchronous operation.");
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
			}

			// Token: 0x04002093 RID: 8339
			private readonly object _stateObject;

			// Token: 0x04002094 RID: 8340
			private readonly bool _isWrite;

			// Token: 0x04002095 RID: 8341
			private ManualResetEvent _waitHandle;

			// Token: 0x04002096 RID: 8342
			private ExceptionDispatchInfo _exceptionInfo;

			// Token: 0x04002097 RID: 8343
			private bool _endXxxCalled;

			// Token: 0x04002098 RID: 8344
			private int _bytesRead;
		}
	}
}
