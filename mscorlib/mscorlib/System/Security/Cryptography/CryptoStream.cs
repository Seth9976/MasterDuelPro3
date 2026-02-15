using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Security.Cryptography
{
	/// <summary>Defines a stream that links data streams to cryptographic transformations.</summary>
	// Token: 0x02000374 RID: 884
	public class CryptoStream : Stream, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptoStream" /> class with a target data stream, the transformation to use, and the mode of the stream.</summary>
		/// <param name="stream">The stream on which to perform the cryptographic transformation. </param>
		/// <param name="transform">The cryptographic transformation that is to be performed on the stream. </param>
		/// <param name="mode">One of the <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not readable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is invalid.</exception>
		// Token: 0x06001ED9 RID: 7897 RVA: 0x0007A394 File Offset: 0x00078594
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
			: this(stream, transform, mode, false)
		{
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0007A3A0 File Offset: 0x000785A0
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen)
		{
			this._stream = stream;
			this._transformMode = mode;
			this._transform = transform;
			this._leaveOpen = leaveOpen;
			CryptoStreamMode transformMode = this._transformMode;
			if (transformMode != CryptoStreamMode.Read)
			{
				if (transformMode != CryptoStreamMode.Write)
				{
					throw new ArgumentException("Argument {0} should be larger than {1}.");
				}
				if (!this._stream.CanWrite)
				{
					throw new ArgumentException(SR.Format("Stream was not writable.", "stream"));
				}
				this._canWrite = true;
			}
			else
			{
				if (!this._stream.CanRead)
				{
					throw new ArgumentException(SR.Format("Stream was not readable.", "stream"));
				}
				this._canRead = true;
			}
			this.InitializeBuffer();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.CryptoStream" /> is readable.</summary>
		/// <returns>true if the current stream is readable; otherwise, false.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0007A447 File Offset: 0x00078647
		public override bool CanRead
		{
			get
			{
				return this._canRead;
			}
		}

		/// <summary>Gets a value indicating whether you can seek within the current <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00033991 File Offset: 0x00031B91
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.CryptoStream" /> is writable.</summary>
		/// <returns>true if the current stream is writable; otherwise, false.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0007A44F File Offset: 0x0007864F
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <returns>This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0007A457 File Offset: 0x00078657
		public override long Length
		{
			get
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
		}

		/// <summary>Gets or sets the position within the current stream.</summary>
		/// <returns>This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported. </exception>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0007A457 File Offset: 0x00078657
		// (set) Token: 0x06001EE0 RID: 7904 RVA: 0x0007A457 File Offset: 0x00078657
		public override long Position
		{
			get
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
			set
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
		}

		/// <summary>Gets a value indicating whether the final buffer block has been written to the underlying stream. </summary>
		/// <returns>true if the final block has been flushed; otherwise, false. </returns>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0007A463 File Offset: 0x00078663
		public bool HasFlushedFinalBlock
		{
			get
			{
				return this._finalBlockTransformed;
			}
		}

		/// <summary>Updates the underlying data source or repository with the current state of the buffer, then clears the buffer.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key is corrupt which can cause invalid padding to the stream. </exception>
		/// <exception cref="T:System.NotSupportedException">The current stream is not writable.-or- The final block has already been transformed. </exception>
		// Token: 0x06001EE2 RID: 7906 RVA: 0x0007A46C File Offset: 0x0007866C
		public void FlushFinalBlock()
		{
			if (this._finalBlockTransformed)
			{
				throw new NotSupportedException("FlushFinalBlock() method was called twice on a CryptoStream. It can only be called once.");
			}
			byte[] array = this._transform.TransformFinalBlock(this._inputBuffer, 0, this._inputBufferIndex);
			this._finalBlockTransformed = true;
			if (this._canWrite && this._outputBufferIndex > 0)
			{
				this._stream.Write(this._outputBuffer, 0, this._outputBufferIndex);
				this._outputBufferIndex = 0;
			}
			if (this._canWrite)
			{
				this._stream.Write(array, 0, array.Length);
			}
			CryptoStream cryptoStream = this._stream as CryptoStream;
			if (cryptoStream != null)
			{
				if (!cryptoStream.HasFlushedFinalBlock)
				{
					cryptoStream.FlushFinalBlock();
				}
			}
			else
			{
				this._stream.Flush();
			}
			if (this._inputBuffer != null)
			{
				Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
			}
			if (this._outputBuffer != null)
			{
				Array.Clear(this._outputBuffer, 0, this._outputBuffer.Length);
			}
		}

		/// <summary>Clears all buffers for the current stream and causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06001EE3 RID: 7907 RVA: 0x00002C89 File Offset: 0x00000E89
		public override void Flush()
		{
		}

		/// <summary>Clears all buffers for the current stream asynchronously, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06001EE4 RID: 7908 RVA: 0x0007A556 File Offset: 0x00078756
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.FlushAsync(cancellationToken);
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCanceled(cancellationToken);
		}

		/// <summary>Sets the position within the current stream.</summary>
		/// <returns>This method is not supported.</returns>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter. </param>
		/// <param name="origin">A <see cref="T:System.IO.SeekOrigin" /> object indicating the reference point used to obtain the new position. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06001EE5 RID: 7909 RVA: 0x0007A457 File Offset: 0x00078657
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Stream does not support seeking.");
		}

		/// <summary>Sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes. </param>
		/// <exception cref="T:System.NotSupportedException">This property exists only to support inheritance from <see cref="T:System.IO.Stream" />, and cannot be used.</exception>
		// Token: 0x06001EE6 RID: 7910 RVA: 0x0007A457 File Offset: 0x00078657
		public override void SetLength(long value)
		{
			throw new NotSupportedException("Stream does not support seeking.");
		}

		/// <summary>Reads a sequence of bytes from the current stream asynchronously, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the task object's <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached. </returns>
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
		// Token: 0x06001EE7 RID: 7911 RVA: 0x0007A58C File Offset: 0x0007878C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckReadArguments(buffer, offset, count);
			return this.ReadAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0007A5A2 File Offset: 0x000787A2
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0007A5BB File Offset: 0x000787BB
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0007A5C4 File Offset: 0x000787C4
		private async Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SemaphoreSlim semaphore = this.AsyncActiveSemaphore;
			await semaphore.WaitAsync().ForceAsync();
			int num;
			try
			{
				num = await this.ReadAsyncCore(buffer, offset, count, cancellationToken, true);
			}
			finally
			{
				semaphore.Release();
			}
			return num;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0007A628 File Offset: 0x00078828
		public override int ReadByte()
		{
			if (this._outputBufferIndex > 1)
			{
				int num = (int)this._outputBuffer[0];
				Buffer.BlockCopy(this._outputBuffer, 1, this._outputBuffer, 0, this._outputBufferIndex - 1);
				this._outputBufferIndex--;
				return num;
			}
			return base.ReadByte();
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0007A678 File Offset: 0x00078878
		public override void WriteByte(byte value)
		{
			if (this._inputBufferIndex + 1 < this._inputBlockSize)
			{
				byte[] inputBuffer = this._inputBuffer;
				int inputBufferIndex = this._inputBufferIndex;
				this._inputBufferIndex = inputBufferIndex + 1;
				inputBuffer[inputBufferIndex] = value;
				return;
			}
			base.WriteByte(value);
		}

		/// <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero if the end of the stream has been reached.</returns>
		/// <param name="buffer">An array of bytes. A maximum of <paramref name="count" /> bytes are read from the current stream and stored in <paramref name="buffer" />. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream. </param>
		/// <param name="count">The maximum number of bytes to be read from the current stream. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> associated with current <see cref="T:System.Security.Cryptography.CryptoStream" /> object does not match the underlying stream.  For example, this exception is thrown when using <see cref="F:System.Security.Cryptography.CryptoStreamMode.Read" /> with an underlying stream that is write only.  </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.-or- The <paramref name="count" /> parameter is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">Thesum of the <paramref name="count" /> and <paramref name="offset" /> parameters is longer than the length of the buffer. </exception>
		// Token: 0x06001EED RID: 7917 RVA: 0x0007A6B8 File Offset: 0x000788B8
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckReadArguments(buffer, offset, count);
			return this.ReadAsyncCore(buffer, offset, count, default(CancellationToken), false).GetAwaiter().GetResult();
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0007A6F0 File Offset: 0x000788F0
		private void CheckReadArguments(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
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
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0007A74C File Offset: 0x0007894C
		private async Task<int> ReadAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool useAsync)
		{
			int bytesToDeliver = count;
			int currentOutputIndex = offset;
			if (this._outputBufferIndex != 0)
			{
				if (this._outputBufferIndex > count)
				{
					Buffer.BlockCopy(this._outputBuffer, 0, buffer, offset, count);
					Buffer.BlockCopy(this._outputBuffer, count, this._outputBuffer, 0, this._outputBufferIndex - count);
					this._outputBufferIndex -= count;
					int num = this._outputBuffer.Length - this._outputBufferIndex;
					CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, this._outputBufferIndex, num));
					return count;
				}
				Buffer.BlockCopy(this._outputBuffer, 0, buffer, offset, this._outputBufferIndex);
				bytesToDeliver -= this._outputBufferIndex;
				currentOutputIndex += this._outputBufferIndex;
				int num2 = this._outputBuffer.Length - this._outputBufferIndex;
				CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, this._outputBufferIndex, num2));
				this._outputBufferIndex = 0;
			}
			int num3;
			if (this._finalBlockTransformed)
			{
				num3 = count - bytesToDeliver;
			}
			else
			{
				int num4 = bytesToDeliver / this._outputBlockSize;
				if (num4 > 1 && this._transform.CanTransformMultipleBlocks)
				{
					int numWholeBlocksInBytes = num4 * this._inputBlockSize;
					byte[] tempInputBuffer = ArrayPool<byte>.Shared.Rent(numWholeBlocksInBytes);
					byte[] tempOutputBuffer = null;
					try
					{
						int num5;
						if (useAsync)
						{
							num5 = await this._stream.ReadAsync(new Memory<byte>(tempInputBuffer, this._inputBufferIndex, numWholeBlocksInBytes - this._inputBufferIndex), cancellationToken);
						}
						else
						{
							num5 = this._stream.Read(tempInputBuffer, this._inputBufferIndex, numWholeBlocksInBytes - this._inputBufferIndex);
						}
						int num6 = num5;
						int num7 = this._inputBufferIndex + num6;
						if (num7 < this._inputBlockSize)
						{
							Buffer.BlockCopy(tempInputBuffer, this._inputBufferIndex, this._inputBuffer, this._inputBufferIndex, num6);
							this._inputBufferIndex = num7;
						}
						else
						{
							Buffer.BlockCopy(this._inputBuffer, 0, tempInputBuffer, 0, this._inputBufferIndex);
							CryptographicOperations.ZeroMemory(new Span<byte>(this._inputBuffer, 0, this._inputBufferIndex));
							num6 += this._inputBufferIndex;
							this._inputBufferIndex = 0;
							int num8 = num6 / this._inputBlockSize;
							int num9 = num8 * this._inputBlockSize;
							int num10 = num6 - num9;
							if (num10 != 0)
							{
								this._inputBufferIndex = num10;
								Buffer.BlockCopy(tempInputBuffer, num9, this._inputBuffer, 0, num10);
							}
							tempOutputBuffer = ArrayPool<byte>.Shared.Rent(num8 * this._outputBlockSize);
							int num11 = this._transform.TransformBlock(tempInputBuffer, 0, num9, tempOutputBuffer, 0);
							Buffer.BlockCopy(tempOutputBuffer, 0, buffer, currentOutputIndex, num11);
							CryptographicOperations.ZeroMemory(new Span<byte>(tempOutputBuffer, 0, num11));
							ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
							tempOutputBuffer = null;
							bytesToDeliver -= num11;
							currentOutputIndex += num11;
						}
					}
					finally
					{
						if (tempOutputBuffer != null)
						{
							CryptographicOperations.ZeroMemory(tempOutputBuffer);
							ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
							tempOutputBuffer = null;
						}
						CryptographicOperations.ZeroMemory(new Span<byte>(tempInputBuffer, 0, numWholeBlocksInBytes));
						ArrayPool<byte>.Shared.Return(tempInputBuffer, false);
						tempInputBuffer = null;
					}
					tempInputBuffer = null;
					tempOutputBuffer = null;
				}
				while (bytesToDeliver > 0)
				{
					while (this._inputBufferIndex < this._inputBlockSize)
					{
						int num5;
						if (useAsync)
						{
							num5 = await this._stream.ReadAsync(new Memory<byte>(this._inputBuffer, this._inputBufferIndex, this._inputBlockSize - this._inputBufferIndex), cancellationToken);
						}
						else
						{
							num5 = this._stream.Read(this._inputBuffer, this._inputBufferIndex, this._inputBlockSize - this._inputBufferIndex);
						}
						int num6 = num5;
						if (num6 != 0)
						{
							this._inputBufferIndex += num6;
						}
						else
						{
							byte[] array = this._transform.TransformFinalBlock(this._inputBuffer, 0, this._inputBufferIndex);
							this._outputBuffer = array;
							this._outputBufferIndex = array.Length;
							this._finalBlockTransformed = true;
							if (bytesToDeliver < this._outputBufferIndex)
							{
								Buffer.BlockCopy(this._outputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
								this._outputBufferIndex -= bytesToDeliver;
								Buffer.BlockCopy(this._outputBuffer, bytesToDeliver, this._outputBuffer, 0, this._outputBufferIndex);
								int num12 = this._outputBuffer.Length - this._outputBufferIndex;
								CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, this._outputBufferIndex, num12));
								return count;
							}
							Buffer.BlockCopy(this._outputBuffer, 0, buffer, currentOutputIndex, this._outputBufferIndex);
							bytesToDeliver -= this._outputBufferIndex;
							this._outputBufferIndex = 0;
							CryptographicOperations.ZeroMemory(this._outputBuffer);
							return count - bytesToDeliver;
						}
					}
					int num11 = this._transform.TransformBlock(this._inputBuffer, 0, this._inputBlockSize, this._outputBuffer, 0);
					this._inputBufferIndex = 0;
					if (bytesToDeliver < num11)
					{
						Buffer.BlockCopy(this._outputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
						this._outputBufferIndex = num11 - bytesToDeliver;
						Buffer.BlockCopy(this._outputBuffer, bytesToDeliver, this._outputBuffer, 0, this._outputBufferIndex);
						int num13 = this._outputBuffer.Length - this._outputBufferIndex;
						CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, this._outputBufferIndex, num13));
						return count;
					}
					Buffer.BlockCopy(this._outputBuffer, 0, buffer, currentOutputIndex, num11);
					CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, 0, num11));
					currentOutputIndex += num11;
					bytesToDeliver -= num11;
				}
				num3 = count;
			}
			return num3;
		}

		/// <summary>Writes a sequence of bytes to the current stream asynchronously, advances the current position within the stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin writing bytes to the stream.</param>
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
		// Token: 0x06001EF0 RID: 7920 RVA: 0x0007A7B9 File Offset: 0x000789B9
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckWriteArguments(buffer, offset, count);
			return this.WriteAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0007A7CF File Offset: 0x000789CF
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0007A7E8 File Offset: 0x000789E8
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0007A7F0 File Offset: 0x000789F0
		private async Task WriteAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SemaphoreSlim semaphore = this.AsyncActiveSemaphore;
			await semaphore.WaitAsync().ForceAsync();
			try
			{
				await this.WriteAsyncCore(buffer, offset, count, cancellationToken, true);
			}
			finally
			{
				semaphore.Release();
			}
		}

		/// <summary>Writes a sequence of bytes to the current <see cref="T:System.Security.Cryptography.CryptoStream" /> and advances the current position within the stream by the number of bytes written.</summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream. </param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream. </param>
		/// <param name="count">The number of bytes to be written to the current stream. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.Cryptography.CryptoStreamMode" /> associated with current <see cref="T:System.Security.Cryptography.CryptoStream" /> object does not match the underlying stream.  For example, this exception is thrown when using <see cref="F:System.Security.Cryptography.CryptoStreamMode.Write" />  with an underlying stream that is read only.  </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> parameter is less than zero.-or- The <paramref name="count" /> parameter is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">The sum of the <paramref name="count" /> and <paramref name="offset" /> parameters is longer than the length of the buffer. </exception>
		// Token: 0x06001EF4 RID: 7924 RVA: 0x0007A854 File Offset: 0x00078A54
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckWriteArguments(buffer, offset, count);
			this.WriteAsyncCore(buffer, offset, count, default(CancellationToken), false).GetAwaiter().GetResult();
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0007A88C File Offset: 0x00078A8C
		private void CheckWriteArguments(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
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
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0007A8E8 File Offset: 0x00078AE8
		private async Task WriteAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool useAsync)
		{
			int bytesToWrite = count;
			int currentInputIndex = offset;
			if (this._inputBufferIndex > 0)
			{
				if (count < this._inputBlockSize - this._inputBufferIndex)
				{
					Buffer.BlockCopy(buffer, offset, this._inputBuffer, this._inputBufferIndex, count);
					this._inputBufferIndex += count;
					return;
				}
				Buffer.BlockCopy(buffer, offset, this._inputBuffer, this._inputBufferIndex, this._inputBlockSize - this._inputBufferIndex);
				currentInputIndex += this._inputBlockSize - this._inputBufferIndex;
				bytesToWrite -= this._inputBlockSize - this._inputBufferIndex;
				this._inputBufferIndex = this._inputBlockSize;
			}
			if (this._outputBufferIndex > 0)
			{
				if (useAsync)
				{
					await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._outputBuffer, 0, this._outputBufferIndex), cancellationToken);
				}
				else
				{
					this._stream.Write(this._outputBuffer, 0, this._outputBufferIndex);
				}
				this._outputBufferIndex = 0;
			}
			if (this._inputBufferIndex == this._inputBlockSize)
			{
				int numOutputBytes = this._transform.TransformBlock(this._inputBuffer, 0, this._inputBlockSize, this._outputBuffer, 0);
				if (useAsync)
				{
					await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._outputBuffer, 0, numOutputBytes), cancellationToken);
				}
				else
				{
					this._stream.Write(this._outputBuffer, 0, numOutputBytes);
				}
				this._inputBufferIndex = 0;
			}
			while (bytesToWrite > 0)
			{
				if (bytesToWrite < this._inputBlockSize)
				{
					Buffer.BlockCopy(buffer, currentInputIndex, this._inputBuffer, 0, bytesToWrite);
					this._inputBufferIndex += bytesToWrite;
					break;
				}
				int num = bytesToWrite / this._inputBlockSize;
				if (this._transform.CanTransformMultipleBlocks && num > 1)
				{
					int numWholeBlocksInBytes = num * this._inputBlockSize;
					byte[] tempOutputBuffer = ArrayPool<byte>.Shared.Rent(num * this._outputBlockSize);
					int numOutputBytes = 0;
					try
					{
						numOutputBytes = this._transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, tempOutputBuffer, 0);
						if (useAsync)
						{
							await this._stream.WriteAsync(new ReadOnlyMemory<byte>(tempOutputBuffer, 0, numOutputBytes), cancellationToken);
						}
						else
						{
							this._stream.Write(tempOutputBuffer, 0, numOutputBytes);
						}
						currentInputIndex += numWholeBlocksInBytes;
						bytesToWrite -= numWholeBlocksInBytes;
					}
					finally
					{
						CryptographicOperations.ZeroMemory(new Span<byte>(tempOutputBuffer, 0, numOutputBytes));
						ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
						tempOutputBuffer = null;
					}
					tempOutputBuffer = null;
				}
				else
				{
					int numOutputBytes = this._transform.TransformBlock(buffer, currentInputIndex, this._inputBlockSize, this._outputBuffer, 0);
					if (useAsync)
					{
						await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._outputBuffer, 0, numOutputBytes), cancellationToken);
					}
					else
					{
						this._stream.Write(this._outputBuffer, 0, numOutputBytes);
					}
					currentInputIndex += this._inputBlockSize;
					bytesToWrite -= this._inputBlockSize;
				}
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.CryptoStream" />.</summary>
		// Token: 0x06001EF7 RID: 7927 RVA: 0x0007A955 File Offset: 0x00078B55
		public void Clear()
		{
			this.Close();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.CryptoStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001EF8 RID: 7928 RVA: 0x0007A960 File Offset: 0x00078B60
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (!this._finalBlockTransformed)
					{
						this.FlushFinalBlock();
					}
					if (!this._leaveOpen)
					{
						this._stream.Dispose();
					}
				}
			}
			finally
			{
				try
				{
					this._finalBlockTransformed = true;
					if (this._inputBuffer != null)
					{
						Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
					}
					if (this._outputBuffer != null)
					{
						Array.Clear(this._outputBuffer, 0, this._outputBuffer.Length);
					}
					this._inputBuffer = null;
					this._outputBuffer = null;
					this._canRead = false;
					this._canWrite = false;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0007AA18 File Offset: 0x00078C18
		private void InitializeBuffer()
		{
			if (this._transform != null)
			{
				this._inputBlockSize = this._transform.InputBlockSize;
				this._inputBuffer = new byte[this._inputBlockSize];
				this._outputBlockSize = this._transform.OutputBlockSize;
				this._outputBuffer = new byte[this._outputBlockSize];
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0007AA71 File Offset: 0x00078C71
		private SemaphoreSlim AsyncActiveSemaphore
		{
			get
			{
				return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._lazyAsyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
			}
		}

		// Token: 0x04000E50 RID: 3664
		private readonly Stream _stream;

		// Token: 0x04000E51 RID: 3665
		private readonly ICryptoTransform _transform;

		// Token: 0x04000E52 RID: 3666
		private readonly CryptoStreamMode _transformMode;

		// Token: 0x04000E53 RID: 3667
		private byte[] _inputBuffer;

		// Token: 0x04000E54 RID: 3668
		private int _inputBufferIndex;

		// Token: 0x04000E55 RID: 3669
		private int _inputBlockSize;

		// Token: 0x04000E56 RID: 3670
		private byte[] _outputBuffer;

		// Token: 0x04000E57 RID: 3671
		private int _outputBufferIndex;

		// Token: 0x04000E58 RID: 3672
		private int _outputBlockSize;

		// Token: 0x04000E59 RID: 3673
		private bool _canRead;

		// Token: 0x04000E5A RID: 3674
		private bool _canWrite;

		// Token: 0x04000E5B RID: 3675
		private bool _finalBlockTransformed;

		// Token: 0x04000E5C RID: 3676
		private SemaphoreSlim _lazyAsyncActiveSemaphore;

		// Token: 0x04000E5D RID: 3677
		private readonly bool _leaveOpen;
	}
}
