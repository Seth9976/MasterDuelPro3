using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	// Token: 0x02000007 RID: 7
	internal sealed class DeflateManagedStream : Stream
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002195 File Offset: 0x00000395
		internal DeflateManagedStream(Stream stream, ZipArchiveEntry.CompressionMethodValues method)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			this.InitializeInflater(stream, false, null, method);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021D0 File Offset: 0x000003D0
		internal void InitializeInflater(Stream stream, bool leaveOpen, IFileFormatReader reader = null, ZipArchiveEntry.CompressionMethodValues method = ZipArchiveEntry.CompressionMethodValues.Deflate)
		{
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			this._inflater = new InflaterManaged(reader, method == ZipArchiveEntry.CompressionMethodValues.Deflate64);
			this._stream = stream;
			this._mode = CompressionMode.Decompress;
			this._leaveOpen = leaveOpen;
			this._buffer = new byte[8192];
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002230 File Offset: 0x00000430
		public override bool CanRead
		{
			get
			{
				return this._stream != null && this._mode == CompressionMode.Decompress && this._stream.CanRead;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002251 File Offset: 0x00000451
		public override bool CanWrite
		{
			get
			{
				return this._stream != null && this._mode == CompressionMode.Compress && this._stream.CanWrite;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002273 File Offset: 0x00000473
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002276 File Offset: 0x00000476
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002276 File Offset: 0x00000476
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002276 File Offset: 0x00000476
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

		// Token: 0x06000017 RID: 23 RVA: 0x00002282 File Offset: 0x00000482
		public override void Flush()
		{
			this.EnsureNotDisposed();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000228A File Offset: 0x0000048A
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.EnsureNotDisposed();
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002276 File Offset: 0x00000476
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002276 File Offset: 0x00000476
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022A8 File Offset: 0x000004A8
		public override int Read(byte[] array, int offset, int count)
		{
			this.EnsureDecompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			int num = offset;
			int num2 = count;
			for (;;)
			{
				int num3 = this._inflater.Inflate(array, num, num2);
				num += num3;
				num2 -= num3;
				if (num2 == 0 || this._inflater.Finished())
				{
					goto IL_008A;
				}
				int num4 = this._stream.Read(this._buffer, 0, this._buffer.Length);
				if (num4 <= 0)
				{
					goto IL_008A;
				}
				if (num4 > this._buffer.Length)
				{
					break;
				}
				this._inflater.SetInput(this._buffer, 0, num4);
			}
			throw new InvalidDataException("Found invalid data while decoding.");
			IL_008A:
			return count - num2;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002344 File Offset: 0x00000544
		private void ValidateParameters(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset plus count is larger than the length of target array.");
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002390 File Offset: 0x00000590
		private void EnsureNotDisposed()
		{
			if (this._stream == null)
			{
				DeflateManagedStream.ThrowStreamClosedException();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000239F File Offset: 0x0000059F
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowStreamClosedException()
		{
			throw new ObjectDisposedException(null, "Can not access a closed Stream.");
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023AC File Offset: 0x000005AC
		private void EnsureDecompressionMode()
		{
			if (this._mode != CompressionMode.Decompress)
			{
				DeflateManagedStream.ThrowCannotReadFromDeflateManagedStreamException();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023BB File Offset: 0x000005BB
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCannotReadFromDeflateManagedStreamException()
		{
			throw new InvalidOperationException("Reading from the compression stream is not supported.");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023C7 File Offset: 0x000005C7
		private void EnsureCompressionMode()
		{
			if (this._mode != CompressionMode.Compress)
			{
				DeflateManagedStream.ThrowCannotWriteToDeflateManagedStreamException();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023D7 File Offset: 0x000005D7
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCannotWriteToDeflateManagedStreamException()
		{
			throw new InvalidOperationException("Writing to the compression stream is not supported.");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023E3 File Offset: 0x000005E3
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023FC File Offset: 0x000005FC
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002404 File Offset: 0x00000604
		public override Task<int> ReadAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.EnsureDecompressionMode();
			if (this._asyncOperations != 0)
			{
				throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			Interlocked.Increment(ref this._asyncOperations);
			Task<int> task = null;
			Task<int> task2;
			try
			{
				int num = this._inflater.Inflate(array, offset, count);
				if (num != 0)
				{
					task2 = Task.FromResult<int>(num);
				}
				else if (this._inflater.Finished())
				{
					task2 = Task.FromResult<int>(0);
				}
				else
				{
					task = this._stream.ReadAsync(this._buffer, 0, this._buffer.Length, cancellationToken);
					if (task == null)
					{
						throw new InvalidOperationException("Stream does not support reading.");
					}
					task2 = this.ReadAsyncCore(task, array, offset, count, cancellationToken);
				}
			}
			finally
			{
				if (task == null)
				{
					Interlocked.Decrement(ref this._asyncOperations);
				}
			}
			return task2;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024E4 File Offset: 0x000006E4
		private async Task<int> ReadAsyncCore(Task<int> readTask, byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			int num2;
			try
			{
				int num;
				for (;;)
				{
					num = await readTask.ConfigureAwait(false);
					this.EnsureNotDisposed();
					if (num <= 0)
					{
						break;
					}
					if (num > this._buffer.Length)
					{
						goto Block_2;
					}
					cancellationToken.ThrowIfCancellationRequested();
					this._inflater.SetInput(this._buffer, 0, num);
					num = this._inflater.Inflate(array, offset, count);
					if (num != 0 || this._inflater.Finished())
					{
						goto IL_012C;
					}
					readTask = this._stream.ReadAsync(this._buffer, 0, this._buffer.Length, cancellationToken);
					if (readTask == null)
					{
						goto Block_5;
					}
				}
				return 0;
				Block_2:
				throw new InvalidDataException("Found invalid data while decoding.");
				Block_5:
				throw new InvalidOperationException("Stream does not support reading.");
				IL_012C:
				num2 = num;
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncOperations);
			}
			return num2;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002551 File Offset: 0x00000751
		public override void Write(byte[] array, int offset, int count)
		{
			this.EnsureCompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			this.DoMaintenance(array, offset, count);
			this.WriteDeflaterOutput();
			this._deflater.SetInput(array, offset, count);
			this.WriteDeflaterOutput();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000258C File Offset: 0x0000078C
		private void WriteDeflaterOutput()
		{
			while (!this._deflater.NeedsInput())
			{
				int deflateOutput = this._deflater.GetDeflateOutput(this._buffer);
				if (deflateOutput > 0)
				{
					this._stream.Write(this._buffer, 0, deflateOutput);
				}
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025D4 File Offset: 0x000007D4
		private void DoMaintenance(byte[] array, int offset, int count)
		{
			if (count <= 0)
			{
				return;
			}
			this._wroteBytes = true;
			if (this._formatWriter == null)
			{
				return;
			}
			if (!this._wroteHeader)
			{
				byte[] header = this._formatWriter.GetHeader();
				this._stream.Write(header, 0, header.Length);
				this._wroteHeader = true;
			}
			this._formatWriter.UpdateWithBytesRead(array, offset, count);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002630 File Offset: 0x00000830
		private void PurgeBuffers(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this._stream == null)
			{
				return;
			}
			this.Flush();
			if (this._mode != CompressionMode.Compress)
			{
				return;
			}
			if (this._wroteBytes)
			{
				this.WriteDeflaterOutput();
				bool flag;
				do
				{
					int num;
					flag = this._deflater.Finish(this._buffer, out num);
					if (num > 0)
					{
						this._stream.Write(this._buffer, 0, num);
					}
				}
				while (!flag);
			}
			else
			{
				bool flag2;
				do
				{
					int num2;
					flag2 = this._deflater.Finish(this._buffer, out num2);
				}
				while (!flag2);
			}
			if (this._formatWriter != null && this._wroteHeader)
			{
				byte[] footer = this._formatWriter.GetFooter();
				this._stream.Write(footer, 0, footer.Length);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026E0 File Offset: 0x000008E0
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.PurgeBuffers(disposing);
			}
			finally
			{
				try
				{
					if (disposing && !this._leaveOpen && this._stream != null)
					{
						this._stream.Dispose();
					}
				}
				finally
				{
					this._stream = null;
					try
					{
						DeflaterManaged deflater = this._deflater;
						if (deflater != null)
						{
							deflater.Dispose();
						}
						InflaterManaged inflater = this._inflater;
						if (inflater != null)
						{
							inflater.Dispose();
						}
					}
					finally
					{
						this._deflater = null;
						this._inflater = null;
						base.Dispose(disposing);
					}
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002784 File Offset: 0x00000984
		public override Task WriteAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.EnsureCompressionMode();
			if (this._asyncOperations != 0)
			{
				throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			return this.WriteAsyncCore(array, offset, count, cancellationToken);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027D8 File Offset: 0x000009D8
		private async Task WriteAsyncCore(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			Interlocked.Increment(ref this._asyncOperations);
			try
			{
				await base.WriteAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncOperations);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000283C File Offset: 0x00000A3C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002855 File Offset: 0x00000A55
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x0400000A RID: 10
		private Stream _stream;

		// Token: 0x0400000B RID: 11
		private CompressionMode _mode;

		// Token: 0x0400000C RID: 12
		private bool _leaveOpen;

		// Token: 0x0400000D RID: 13
		private InflaterManaged _inflater;

		// Token: 0x0400000E RID: 14
		private DeflaterManaged _deflater;

		// Token: 0x0400000F RID: 15
		private byte[] _buffer;

		// Token: 0x04000010 RID: 16
		private int _asyncOperations;

		// Token: 0x04000011 RID: 17
		private IFileFormatWriter _formatWriter;

		// Token: 0x04000012 RID: 18
		private bool _wroteHeader;

		// Token: 0x04000013 RID: 19
		private bool _wroteBytes;
	}
}
