using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Creates a stream whose backing store is memory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200079D RID: 1949
	[Serializable]
	public class MemoryStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class with an expandable capacity initialized to zero.</summary>
		// Token: 0x06003D6B RID: 15723 RVA: 0x000EC6EB File Offset: 0x000EA8EB
		public MemoryStream()
			: this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class with an expandable capacity initialized as specified.</summary>
		/// <param name="capacity">The initial size of the internal array in bytes. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is negative. </exception>
		// Token: 0x06003D6C RID: 15724 RVA: 0x000EC6F4 File Offset: 0x000EA8F4
		public MemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity must be positive.");
			}
			this._buffer = ((capacity != 0) ? new byte[capacity] : Array.Empty<byte>());
			this._capacity = capacity;
			this._expandable = true;
			this._writable = true;
			this._exposable = true;
			this._origin = 0;
			this._isOpen = true;
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create the current stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		// Token: 0x06003D6D RID: 15725 RVA: 0x000EC75B File Offset: 0x000EA95B
		public MemoryStream(byte[] buffer)
			: this(buffer, true)
		{
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified byte array with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		// Token: 0x06003D6E RID: 15726 RVA: 0x000EC768 File Offset: 0x000EA968
		public MemoryStream(byte[] buffer, bool writable)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			this._buffer = buffer;
			this._length = (this._capacity = buffer.Length);
			this._writable = writable;
			this._exposable = false;
			this._origin = 0;
			this._isOpen = true;
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region (index) of a byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index into <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x06003D6F RID: 15727 RVA: 0x000EC7C3 File Offset: 0x000EA9C3
		public MemoryStream(byte[] buffer, int index, int count)
			: this(buffer, index, count, true, false)
		{
		}

		/// <summary>Initializes a new non-resizable instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region of a byte array, with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index in <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> are negative. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
		// Token: 0x06003D70 RID: 15728 RVA: 0x000EC7D0 File Offset: 0x000EA9D0
		public MemoryStream(byte[] buffer, int index, int count, bool writable)
			: this(buffer, index, count, writable, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.MemoryStream" /> class based on the specified region of a byte array, with the <see cref="P:System.IO.MemoryStream.CanWrite" /> property set as specified, and the ability to call <see cref="M:System.IO.MemoryStream.GetBuffer" /> set as specified.</summary>
		/// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
		/// <param name="index">The index into <paramref name="buffer" /> at which the stream begins. </param>
		/// <param name="count">The length of the stream in bytes. </param>
		/// <param name="writable">The setting of the <see cref="P:System.IO.MemoryStream.CanWrite" /> property, which determines whether the stream supports writing. </param>
		/// <param name="publiclyVisible">true to enable <see cref="M:System.IO.MemoryStream.GetBuffer" />, which returns the unsigned byte array from which the stream was created; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		// Token: 0x06003D71 RID: 15729 RVA: 0x000EC7E0 File Offset: 0x000EA9E0
		public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this._buffer = buffer;
			this._position = index;
			this._origin = index;
			this._length = (this._capacity = index + count);
			this._writable = writable;
			this._exposable = publiclyVisible;
			this._expandable = false;
			this._isOpen = true;
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream is open.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06003D72 RID: 15730 RVA: 0x000EC888 File Offset: 0x000EAA88
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream is open.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x000EC888 File Offset: 0x000EAA88
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x000EC890 File Offset: 0x000EAA90
		public override bool CanWrite
		{
			get
			{
				return this._writable;
			}
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000EC898 File Offset: 0x000EAA98
		private void EnsureNotClosed()
		{
			if (!this._isOpen)
			{
				throw Error.GetStreamIsClosed();
			}
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000EC8A8 File Offset: 0x000EAAA8
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.MemoryStream" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06003D77 RID: 15735 RVA: 0x000EC8B8 File Offset: 0x000EAAB8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._isOpen = false;
					this._writable = false;
					this._expandable = false;
					this._lastReadTask = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x000EC900 File Offset: 0x000EAB00
		private bool EnsureCapacity(int value)
		{
			if (value < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (value > this._capacity)
			{
				int num = value;
				if (num < 256)
				{
					num = 256;
				}
				if (num < this._capacity * 2)
				{
					num = this._capacity * 2;
				}
				if (this._capacity * 2 > 2147483591)
				{
					num = ((value > 2147483591) ? value : 2147483591);
				}
				this.Capacity = num;
				return true;
			}
			return false;
		}

		/// <summary>Overrides the <see cref="M:System.IO.Stream.Flush" /> method so that no action is performed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D79 RID: 15737 RVA: 0x00002C89 File Offset: 0x00000E89
		public override void Flush()
		{
		}

		/// <summary>Asynchronously clears all buffers for this stream, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06003D7A RID: 15738 RVA: 0x000EC974 File Offset: 0x000EAB74
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Flush();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		/// <summary>Returns the array of unsigned bytes from which this stream was created.</summary>
		/// <returns>The byte array from which this stream was created, or the underlying array if a byte array was not provided to the <see cref="T:System.IO.MemoryStream" /> constructor during construction of the current instance.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">The MemoryStream instance was not created with a publicly visible buffer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D7B RID: 15739 RVA: 0x000EC9BC File Offset: 0x000EABBC
		public virtual byte[] GetBuffer()
		{
			if (!this._exposable)
			{
				throw new UnauthorizedAccessException("MemoryStream's internal buffer cannot be accessed.");
			}
			return this._buffer;
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000EC9D7 File Offset: 0x000EABD7
		internal byte[] InternalGetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x000EC9DF File Offset: 0x000EABDF
		internal void InternalGetOriginAndLength(out int origin, out int length)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			origin = this._origin;
			length = this._length;
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000EC9FE File Offset: 0x000EABFE
		internal int InternalGetPosition()
		{
			return this._position;
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x000ECA08 File Offset: 0x000EAC08
		internal int InternalReadInt32()
		{
			this.EnsureNotClosed();
			int num = (this._position += 4);
			if (num > this._length)
			{
				this._position = this._length;
				throw Error.GetEndOfFile();
			}
			return (int)this._buffer[num - 4] | ((int)this._buffer[num - 3] << 8) | ((int)this._buffer[num - 2] << 16) | ((int)this._buffer[num - 1] << 24);
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000ECA7C File Offset: 0x000EAC7C
		internal int InternalEmulateRead(int count)
		{
			this.EnsureNotClosed();
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num < 0)
			{
				num = 0;
			}
			this._position += num;
			return num;
		}

		/// <summary>Gets or sets the number of bytes allocated for this stream.</summary>
		/// <returns>The length of the usable portion of the buffer for the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A capacity is set that is negative or less than the current length of the stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">set is invoked on a stream whose capacity cannot be modified. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000ECAB8 File Offset: 0x000EACB8
		// (set) Token: 0x06003D82 RID: 15746 RVA: 0x000ECAD0 File Offset: 0x000EACD0
		public virtual int Capacity
		{
			get
			{
				this.EnsureNotClosed();
				return this._capacity - this._origin;
			}
			set
			{
				if ((long)value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				this.EnsureNotClosed();
				if (!this._expandable && value != this.Capacity)
				{
					throw new NotSupportedException("Memory stream is not expandable.");
				}
				if (this._expandable && value != this._capacity)
				{
					if (value > 0)
					{
						byte[] array = new byte[value];
						if (this._length > 0)
						{
							Buffer.BlockCopy(this._buffer, 0, array, 0, this._length);
						}
						this._buffer = array;
					}
					else
					{
						this._buffer = null;
					}
					this._capacity = value;
				}
			}
		}

		/// <summary>Gets the length of the stream in bytes.</summary>
		/// <returns>The length of the stream in bytes.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000ECB69 File Offset: 0x000EAD69
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				return (long)(this._length - this._origin);
			}
		}

		/// <summary>Gets or sets the current position within the stream.</summary>
		/// <returns>The current position within the stream.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The position is set to a negative value or a value greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x000ECB7F File Offset: 0x000EAD7F
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x000ECB98 File Offset: 0x000EAD98
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				return (long)(this._position - this._origin);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				this.EnsureNotClosed();
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
				}
				this._position = this._origin + (int)value;
			}
		}

		/// <summary>Reads a block of bytes from the current stream and writes the data to a buffer.</summary>
		/// <returns>The total number of bytes written into the buffer. This can be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached before any bytes are read.</returns>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the characters read from the current stream. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing data from the current stream.</param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> subtracted from the buffer length is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D86 RID: 15750 RVA: 0x000ECBE8 File Offset: 0x000EADE8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.EnsureNotClosed();
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num <= 0)
			{
				return 0;
			}
			if (num <= 8)
			{
				int num2 = num;
				while (--num2 >= 0)
				{
					buffer[offset + num2] = this._buffer[this._position + num2];
				}
			}
			else
			{
				Buffer.BlockCopy(this._buffer, this._position, buffer, offset, num);
			}
			this._position += num;
			return num;
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000ECCAC File Offset: 0x000EAEAC
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.Read(buffer);
			}
			this.EnsureNotClosed();
			int num = Math.Min(this._length - this._position, buffer.Length);
			if (num <= 0)
			{
				return 0;
			}
			new Span<byte>(this._buffer, this._position, num).CopyTo(buffer);
			this._position += num;
			return num;
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
		// Token: 0x06003D88 RID: 15752 RVA: 0x000ECD28 File Offset: 0x000EAF28
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			Task<int> task;
			try
			{
				int num = this.Read(buffer, offset, count);
				Task<int> lastReadTask = this._lastReadTask;
				Task<int> task2;
				if (lastReadTask == null || lastReadTask.Result != num)
				{
					task = (this._lastReadTask = Task.FromResult<int>(num));
					task2 = task;
				}
				else
				{
					task2 = lastReadTask;
				}
				task = task2;
			}
			catch (OperationCanceledException ex)
			{
				task = Task.FromCancellation<int>(ex);
			}
			catch (Exception ex2)
			{
				task = Task.FromException<int>(ex2);
			}
			return task;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x000ECDF4 File Offset: 0x000EAFF4
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			ValueTask<int> valueTask;
			try
			{
				ArraySegment<byte> arraySegment;
				valueTask = new ValueTask<int>(MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment) ? this.Read(arraySegment.Array, arraySegment.Offset, arraySegment.Count) : this.Read(buffer.Span));
			}
			catch (OperationCanceledException ex)
			{
				valueTask = new ValueTask<int>(Task.FromCancellation<int>(ex));
			}
			catch (Exception ex2)
			{
				valueTask = new ValueTask<int>(Task.FromException<int>(ex2));
			}
			return valueTask;
		}

		/// <summary>Reads a byte from the current stream.</summary>
		/// <returns>The byte cast to a <see cref="T:System.Int32" />, or -1 if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D8A RID: 15754 RVA: 0x000ECE90 File Offset: 0x000EB090
		public override int ReadByte()
		{
			this.EnsureNotClosed();
			if (this._position >= this._length)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			return buffer[position];
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000ECECC File Offset: 0x000EB0CC
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (base.GetType() != typeof(MemoryStream))
			{
				base.CopyTo(destination, bufferSize);
				return;
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - position);
			if (num > 0)
			{
				destination.Write(this._buffer, position, num);
			}
		}

		/// <summary>Asynchronously reads all the bytes from the current stream and writes them to another stream, using a specified buffer size and cancellation token.</summary>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is negative or zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Either the current stream or the destination stream is disposed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading, or the destination stream does not support writing.</exception>
		// Token: 0x06003D8C RID: 15756 RVA: 0x000ECF2C File Offset: 0x000EB12C
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.CopyToAsync(destination, bufferSize, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - this._position);
			if (num == 0)
			{
				return Task.CompletedTask;
			}
			MemoryStream memoryStream = destination as MemoryStream;
			if (memoryStream == null)
			{
				return destination.WriteAsync(this._buffer, position, num, cancellationToken);
			}
			Task task;
			try
			{
				memoryStream.Write(this._buffer, position, num);
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		/// <summary>Sets the position within the current stream to the specified value.</summary>
		/// <returns>The new position within the stream, calculated by combining the initial reference point and the offset.</returns>
		/// <param name="offset">The new position within the stream. This is relative to the <paramref name="loc" /> parameter, and can be positive or negative. </param>
		/// <param name="loc">A value of type <see cref="T:System.IO.SeekOrigin" />, which acts as the seek reference point. </param>
		/// <exception cref="T:System.IO.IOException">Seeking is attempted before the beginning of the stream. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentException">There is an invalid <see cref="T:System.IO.SeekOrigin" />. -or-<paramref name="offset" /> caused an arithmetic overflow.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D8D RID: 15757 RVA: 0x000ECFE0 File Offset: 0x000EB1E0
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.EnsureNotClosed();
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			switch (loc)
			{
			case SeekOrigin.Begin:
			{
				int num = this._origin + (int)offset;
				if (offset < 0L || num < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num;
				break;
			}
			case SeekOrigin.Current:
			{
				int num2 = this._position + (int)offset;
				if ((long)this._position + offset < (long)this._origin || num2 < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num2;
				break;
			}
			case SeekOrigin.End:
			{
				int num3 = this._length + (int)offset;
				if ((long)this._length + offset < (long)this._origin || num3 < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num3;
				break;
			}
			default:
				throw new ArgumentException("Invalid seek origin.");
			}
			return (long)this._position;
		}

		/// <summary>Sets the length of the current stream to the specified value.</summary>
		/// <param name="value">The value at which to set the length. </param>
		/// <exception cref="T:System.NotSupportedException">The current stream is not resizable and <paramref name="value" /> is larger than the current capacity.-or- The current stream does not support writing. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is negative or is greater than the maximum length of the <see cref="T:System.IO.MemoryStream" />, where the maximum length is(<see cref="F:System.Int32.MaxValue" /> - origin), and origin is the index into the underlying buffer at which the stream starts. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D8E RID: 15758 RVA: 0x000ED0D4 File Offset: 0x000EB2D4
		public override void SetLength(long value)
		{
			if (value < 0L || value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			this.EnsureWriteable();
			if (value > (long)(2147483647 - this._origin))
			{
				throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			int num = this._origin + (int)value;
			if (!this.EnsureCapacity(num) && num > this._length)
			{
				Array.Clear(this._buffer, this._length, num - this._length);
			}
			this._length = num;
			if (this._position > num)
			{
				this._position = num;
			}
		}

		/// <summary>Writes the stream contents to a byte array, regardless of the <see cref="P:System.IO.MemoryStream.Position" /> property.</summary>
		/// <returns>A new byte array.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D8F RID: 15759 RVA: 0x000ED174 File Offset: 0x000EB374
		public virtual byte[] ToArray()
		{
			int num = this._length - this._origin;
			if (num == 0)
			{
				return Array.Empty<byte>();
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._buffer, this._origin, array, 0, num);
			return array;
		}

		/// <summary>Writes a block of bytes to the current stream using data read from a buffer.</summary>
		/// <param name="buffer">The buffer to write data from. </param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. For additional information see <see cref="P:System.IO.Stream.CanWrite" />.-or- The current position is closer than <paramref name="count" /> bytes to the end of the stream, and the capacity cannot be modified. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> subtracted from the buffer length is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> are negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream instance is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D90 RID: 15760 RVA: 0x000ED1B4 File Offset: 0x000EB3B4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			int num = this._position + count;
			if (num < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			if (count <= 8 && buffer != this._buffer)
			{
				int num2 = count;
				while (--num2 >= 0)
				{
					this._buffer[this._position + num2] = buffer[offset + num2];
				}
			}
			else
			{
				Buffer.BlockCopy(buffer, offset, this._buffer, this._position, count);
			}
			this._position = num;
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000ED2CC File Offset: 0x000EB4CC
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() != typeof(MemoryStream))
			{
				base.Write(buffer);
				return;
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			int num = this._position + buffer.Length;
			if (num < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			buffer.CopyTo(new Span<byte>(this._buffer, this._position, buffer.Length));
			this._position = num;
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
		// Token: 0x06003D92 RID: 15762 RVA: 0x000ED398 File Offset: 0x000EB598
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Write(buffer, offset, count);
				task = Task.CompletedTask;
			}
			catch (OperationCanceledException ex)
			{
				task = Task.FromCancellation<VoidTaskResult>(ex);
			}
			catch (Exception ex2)
			{
				task = Task.FromException(ex2);
			}
			return task;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000ED444 File Offset: 0x000EB644
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}
			ValueTask valueTask;
			try
			{
				ArraySegment<byte> arraySegment;
				if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
				{
					this.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				}
				else
				{
					this.Write(buffer.Span);
				}
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (OperationCanceledException ex)
			{
				valueTask = new ValueTask(Task.FromCancellation<VoidTaskResult>(ex));
			}
			catch (Exception ex2)
			{
				valueTask = new ValueTask(Task.FromException(ex2));
			}
			return valueTask;
		}

		/// <summary>Writes a byte to the current stream at the current position.</summary>
		/// <param name="value">The byte to write. </param>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. For additional information see <see cref="P:System.IO.Stream.CanWrite" />.-or- The current position is at the end of the stream, and the capacity cannot be modified. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003D94 RID: 15764 RVA: 0x000ED4E0 File Offset: 0x000EB6E0
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			if (this._position >= this._length)
			{
				int num = this._position + 1;
				bool flag = this._position > this._length;
				if (num >= this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, this._position - this._length);
				}
				this._length = num;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x04001F95 RID: 8085
		private byte[] _buffer;

		// Token: 0x04001F96 RID: 8086
		private int _origin;

		// Token: 0x04001F97 RID: 8087
		private int _position;

		// Token: 0x04001F98 RID: 8088
		private int _length;

		// Token: 0x04001F99 RID: 8089
		private int _capacity;

		// Token: 0x04001F9A RID: 8090
		private bool _expandable;

		// Token: 0x04001F9B RID: 8091
		private bool _writable;

		// Token: 0x04001F9C RID: 8092
		private bool _exposable;

		// Token: 0x04001F9D RID: 8093
		private bool _isOpen;

		// Token: 0x04001F9E RID: 8094
		[NonSerialized]
		private Task<int> _lastReadTask;
	}
}
