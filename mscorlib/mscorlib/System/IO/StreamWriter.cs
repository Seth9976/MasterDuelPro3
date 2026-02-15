using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing characters to a stream in a particular encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020007A7 RID: 1959
	[Serializable]
	public class StreamWriter : TextWriter
	{
		// Token: 0x06003DEB RID: 15851 RVA: 0x000EF3BE File Offset: 0x000ED5BE
		private void CheckAsyncTaskInProgress()
		{
			if (!this._asyncWriteTask.IsCompleted)
			{
				StreamWriter.ThrowAsyncIOInProgress();
			}
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000EDCDB File Offset: 0x000EBEDB
		private static void ThrowAsyncIOInProgress()
		{
			throw new InvalidOperationException("The stream is currently in use by a previous operation on the stream.");
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x000EF3D2 File Offset: 0x000ED5D2
		private static Encoding UTF8NoBOM
		{
			get
			{
				return EncodingHelper.UTF8Unmarked;
			}
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000EF3D9 File Offset: 0x000ED5D9
		internal StreamWriter()
			: base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using UTF-8 encoding and the default buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06003DEF RID: 15855 RVA: 0x000EF3ED File Offset: 0x000ED5ED
		public StreamWriter(Stream stream)
			: this(stream, StreamWriter.UTF8NoBOM, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and the default buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		// Token: 0x06003DF0 RID: 15856 RVA: 0x000EF401 File Offset: 0x000ED601
		public StreamWriter(Stream stream, Encoding encoding)
			: this(stream, encoding, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and buffer size.</summary>
		/// <param name="stream">The stream to write to. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="bufferSize">The buffer size, in bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		// Token: 0x06003DF1 RID: 15857 RVA: 0x000EF411 File Offset: 0x000ED611
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
			: this(stream, encoding, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified stream by using the specified encoding and buffer size, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="bufferSize">The buffer size, in bytes.</param>
		/// <param name="leaveOpen">true to leave the stream open after the <see cref="T:System.IO.StreamWriter" /> object is disposed; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is not writable. </exception>
		// Token: 0x06003DF2 RID: 15858 RVA: 0x000EF420 File Offset: 0x000ED620
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)
			: base(null)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream was not writable.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			this.Init(stream, encoding, bufferSize, leaveOpen);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the default encoding and buffer size.</summary>
		/// <param name="path">The complete file path to write to. <paramref name="path" /> can be a file name. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003DF3 RID: 15859 RVA: 0x000EF48C File Offset: 0x000ED68C
		public StreamWriter(string path)
			: this(path, false, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the default encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">true to append data to the file; false to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty. -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003DF4 RID: 15860 RVA: 0x000EF4A0 File Offset: 0x000ED6A0
		public StreamWriter(string path, bool append)
			: this(path, append, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file by using the specified encoding and default buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">true to append data to the file; false to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file.</param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is empty. -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000EF4B4 File Offset: 0x000ED6B4
		public StreamWriter(string path, bool append, Encoding encoding)
			: this(path, append, encoding, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamWriter" /> class for the specified file on the specified path, using the specified encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.</summary>
		/// <param name="path">The complete file path to write to. </param>
		/// <param name="append">true to append data to the file; false to overwrite the file. If the specified file does not exist, this parameter has no effect, and the constructor creates a new file.</param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="bufferSize">The buffer size, in bytes. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). -or-<paramref name="path" /> contains the name of a system device (com1, com2, and so on).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must not exceed 248 characters, and file names must not exceed 260 characters. </exception>
		// Token: 0x06003DF6 RID: 15862 RVA: 0x000EF4C4 File Offset: 0x000ED6C4
		public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			Stream stream = new FileStream(path, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan);
			this.Init(stream, encoding, bufferSize, false);
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000EF54C File Offset: 0x000ED74C
		private void Init(Stream streamArg, Encoding encodingArg, int bufferSize, bool shouldLeaveOpen)
		{
			this._stream = streamArg;
			this._encoding = encodingArg;
			this._encoder = this._encoding.GetEncoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this._charBuffer = new char[bufferSize];
			this._byteBuffer = new byte[this._encoding.GetMaxByteCount(bufferSize)];
			this._charLen = bufferSize;
			if (this._stream.CanSeek && this._stream.Position > 0L)
			{
				this._haveWrittenPreamble = true;
			}
			this._closable = !shouldLeaveOpen;
		}

		/// <summary>Closes the current StreamWriter object and the underlying stream.</summary>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003DF8 RID: 15864 RVA: 0x000EF5DF File Offset: 0x000ED7DF
		public override void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StreamWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair.</exception>
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000EF5F0 File Offset: 0x000ED7F0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._stream != null && disposing)
				{
					this.CheckAsyncTaskInProgress();
					this.Flush(true, true);
				}
			}
			finally
			{
				if (!this.LeaveOpen && this._stream != null)
				{
					try
					{
						if (disposing)
						{
							this._stream.Close();
						}
					}
					finally
					{
						this._stream = null;
						this._byteBuffer = null;
						this._charBuffer = null;
						this._encoding = null;
						this._encoder = null;
						this._charLen = 0;
						base.Dispose(disposing);
					}
				}
			}
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current writer is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">The current encoding does not support displaying half of a Unicode surrogate pair. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003DFA RID: 15866 RVA: 0x000EF688 File Offset: 0x000ED888
		public override void Flush()
		{
			this.CheckAsyncTaskInProgress();
			this.Flush(true, true);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000EF698 File Offset: 0x000ED898
		private void Flush(bool flushStream, bool flushEncoder)
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			if (this._charPos == 0 && !flushStream && !flushEncoder)
			{
				return;
			}
			if (!this._haveWrittenPreamble)
			{
				this._haveWrittenPreamble = true;
				ReadOnlySpan<byte> preamble = this._encoding.Preamble;
				if (preamble.Length > 0)
				{
					this._stream.Write(preamble);
				}
			}
			int bytes = this._encoder.GetBytes(this._charBuffer, 0, this._charPos, this._byteBuffer, 0, flushEncoder);
			this._charPos = 0;
			if (bytes > 0)
			{
				this._stream.Write(this._byteBuffer, 0, bytes);
			}
			if (flushStream)
			{
				this._stream.Flush();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.IO.StreamWriter" /> will flush its buffer to the underlying stream after every call to <see cref="M:System.IO.StreamWriter.Write(System.Char)" />.</summary>
		/// <returns>true to force <see cref="T:System.IO.StreamWriter" /> to flush its buffer; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009FD RID: 2557
		// (set) Token: 0x06003DFC RID: 15868 RVA: 0x000EF746 File Offset: 0x000ED946
		public virtual bool AutoFlush
		{
			set
			{
				this.CheckAsyncTaskInProgress();
				this._autoFlush = value;
				if (value)
				{
					this.Flush(true, false);
				}
			}
		}

		/// <summary>Gets the underlying stream that interfaces with a backing store.</summary>
		/// <returns>The stream this StreamWriter is writing to.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000EF760 File Offset: 0x000ED960
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x000EF768 File Offset: 0x000ED968
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The <see cref="T:System.Text.Encoding" /> specified in the constructor for the current instance, or <see cref="T:System.Text.UTF8Encoding" /> if an encoding was not specified.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000EF773 File Offset: 0x000ED973
		public override Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		/// <summary>Writes a character to the stream.</summary>
		/// <param name="value">The character to write to the stream. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E00 RID: 15872 RVA: 0x000EF77C File Offset: 0x000ED97C
		public override void Write(char value)
		{
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen)
			{
				this.Flush(false, false);
			}
			this._charBuffer[this._charPos] = value;
			this._charPos++;
			if (this._autoFlush)
			{
				this.Flush(true, false);
			}
		}

		/// <summary>Writes a character array to the stream.</summary>
		/// <param name="buffer">A character array containing the data to write. If <paramref name="buffer" /> is null, nothing is written. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E01 RID: 15873 RVA: 0x000EF7D1 File Offset: 0x000ED9D1
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(char[] buffer)
		{
			this.WriteSpan(buffer, false);
		}

		/// <summary>Writes a subarray of characters to the stream.</summary>
		/// <param name="buffer">A character array that contains the data to write. </param>
		/// <param name="index">The character position in the buffer at which to start reading data. </param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E02 RID: 15874 RVA: 0x000EF7E0 File Offset: 0x000ED9E0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(char[] buffer, int index, int count)
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
			this.WriteSpan(buffer.AsSpan(index, count), false);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000EF850 File Offset: 0x000EDA50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteSpan(ReadOnlySpan<char> buffer, bool appendNewLine)
		{
			this.CheckAsyncTaskInProgress();
			if (buffer.Length <= 4 && buffer.Length <= this._charLen - this._charPos)
			{
				for (int i = 0; i < buffer.Length; i++)
				{
					char[] charBuffer = this._charBuffer;
					int charPos = this._charPos;
					this._charPos = charPos + 1;
					charBuffer[charPos] = *buffer[i];
				}
			}
			else
			{
				char[] charBuffer2 = this._charBuffer;
				if (charBuffer2 == null)
				{
					throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
				}
				fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
				{
					char* ptr = reference;
					fixed (char* ptr2 = &charBuffer2[0])
					{
						char* ptr3 = ptr2;
						char* ptr4 = ptr;
						int j = buffer.Length;
						int num = this._charPos;
						while (j > 0)
						{
							if (num == charBuffer2.Length)
							{
								this.Flush(false, false);
								num = 0;
							}
							int num2 = Math.Min(charBuffer2.Length - num, j);
							int num3 = num2 * 2;
							Buffer.MemoryCopy((void*)ptr4, (void*)(ptr3 + num), (long)num3, (long)num3);
							this._charPos += num2;
							num += num2;
							ptr4 += num2;
							j -= num2;
						}
					}
				}
			}
			if (appendNewLine)
			{
				char[] coreNewLine = this.CoreNewLine;
				for (int k = 0; k < coreNewLine.Length; k++)
				{
					if (this._charPos == this._charLen)
					{
						this.Flush(false, false);
					}
					this._charBuffer[this._charPos] = coreNewLine[k];
					this._charPos++;
				}
			}
			if (this._autoFlush)
			{
				this.Flush(true, false);
			}
		}

		/// <summary>Writes a string to the stream.</summary>
		/// <param name="value">The string to write to the stream. If <paramref name="value" /> is null, nothing is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and current writer is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.StreamWriter.AutoFlush" /> is true or the <see cref="T:System.IO.StreamWriter" /> buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the <see cref="T:System.IO.StreamWriter" /> is at the end the stream. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E04 RID: 15876 RVA: 0x000EF9D0 File Offset: 0x000EDBD0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(string value)
		{
			this.WriteSpan(value, false);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000EF9DF File Offset: 0x000EDBDF
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void WriteLine(string value)
		{
			this.CheckAsyncTaskInProgress();
			this.WriteSpan(value, true);
		}

		/// <summary>Writes a character to the stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x06003E06 RID: 15878 RVA: 0x000EF9F4 File Offset: 0x000EDBF4
		public override Task WriteAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000EFA6C File Offset: 0x000EDC6C
		private static async Task WriteAsyncInternal(StreamWriter _this, char value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			if (charPos == charLen)
			{
				await _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
				charPos = 0;
			}
			charBuffer[charPos] = value;
			charPos++;
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		/// <summary>Writes a string to the stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The string to write to the stream. If <paramref name="value" /> is null, nothing is written.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation.</exception>
		// Token: 0x06003E08 RID: 15880 RVA: 0x000EFAEC File Offset: 0x000EDCEC
		public override Task WriteAsync(string value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (value == null)
			{
				return Task.CompletedTask;
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000EFB6C File Offset: 0x000EDD6C
		private static async Task WriteAsyncInternal(StreamWriter _this, string value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			int count = value.Length;
			int index = 0;
			while (count > 0)
			{
				if (charPos == charLen)
				{
					await _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
					charPos = 0;
				}
				int num = charLen - charPos;
				if (num > count)
				{
					num = count;
				}
				value.CopyTo(index, charBuffer, charPos, num);
				charPos += num;
				index += num;
				count -= num;
			}
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		/// <summary>Writes a subarray of characters to the stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">A character array that contains the data to write.</param>
		/// <param name="index">The character position in the buffer at which to begin reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream writer is currently in use by a previous write operation. </exception>
		// Token: 0x06003E0A RID: 15882 RVA: 0x000EFBEC File Offset: 0x000EDDEC
		public override Task WriteAsync(char[] buffer, int index, int count)
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
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, new ReadOnlyMemory<char>(buffer, index, count), this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000EFCC4 File Offset: 0x000EDEC4
		private static async Task WriteAsyncInternal(StreamWriter _this, ReadOnlyMemory<char> source, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine, CancellationToken cancellationToken)
		{
			int num;
			for (int copied = 0; copied < source.Length; copied += num)
			{
				if (charPos == charLen)
				{
					await _this.FlushAsyncInternal(false, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false);
					charPos = 0;
				}
				num = Math.Min(charLen - charPos, source.Length - copied);
				source.Span.Slice(copied, num).CopyTo(new Span<char>(charBuffer, charPos, num));
				charPos += num;
			}
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		/// <summary>Clears all buffers for this stream asynchronously and causes any buffered data to be written to the underlying device.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x06003E0C RID: 15884 RVA: 0x000EFD4C File Offset: 0x000EDF4C
		public override Task FlushAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.FlushAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = this.FlushAsyncInternal(true, true, this._charBuffer, this._charPos, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x17000A01 RID: 2561
		// (set) Token: 0x06003E0D RID: 15885 RVA: 0x000EFDB7 File Offset: 0x000EDFB7
		private int CharPos_Prop
		{
			set
			{
				this._charPos = value;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (set) Token: 0x06003E0E RID: 15886 RVA: 0x000EFDC0 File Offset: 0x000EDFC0
		private bool HaveWrittenPreamble_Prop
		{
			set
			{
				this._haveWrittenPreamble = value;
			}
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000EFDCC File Offset: 0x000EDFCC
		private Task FlushAsyncInternal(bool flushStream, bool flushEncoder, char[] sCharBuffer, int sCharPos, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (sCharPos == 0 && !flushStream && !flushEncoder)
			{
				return Task.CompletedTask;
			}
			Task task = StreamWriter.FlushAsyncInternal(this, flushStream, flushEncoder, sCharBuffer, sCharPos, this._haveWrittenPreamble, this._encoding, this._encoder, this._byteBuffer, this._stream, cancellationToken);
			this._charPos = 0;
			return task;
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000EFE2C File Offset: 0x000EE02C
		private static async Task FlushAsyncInternal(StreamWriter _this, bool flushStream, bool flushEncoder, char[] charBuffer, int charPos, bool haveWrittenPreamble, Encoding encoding, Encoder encoder, byte[] byteBuffer, Stream stream, CancellationToken cancellationToken)
		{
			if (!haveWrittenPreamble)
			{
				_this.HaveWrittenPreamble_Prop = true;
				byte[] preamble = encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					await stream.WriteAsync(new ReadOnlyMemory<byte>(preamble), cancellationToken).ConfigureAwait(false);
				}
			}
			int bytes = encoder.GetBytes(charBuffer, 0, charPos, byteBuffer, 0, flushEncoder);
			if (bytes > 0)
			{
				await stream.WriteAsync(new ReadOnlyMemory<byte>(byteBuffer, 0, bytes), cancellationToken).ConfigureAwait(false);
			}
			if (flushStream)
			{
				await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		/// <summary>Provides a StreamWriter with no backing store that can be written to, but not read from.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001FC9 RID: 8137
		public new static readonly StreamWriter Null = new StreamWriter(Stream.Null, StreamWriter.UTF8NoBOM, 128, true);

		// Token: 0x04001FCA RID: 8138
		private Stream _stream;

		// Token: 0x04001FCB RID: 8139
		private Encoding _encoding;

		// Token: 0x04001FCC RID: 8140
		private Encoder _encoder;

		// Token: 0x04001FCD RID: 8141
		private byte[] _byteBuffer;

		// Token: 0x04001FCE RID: 8142
		private char[] _charBuffer;

		// Token: 0x04001FCF RID: 8143
		private int _charPos;

		// Token: 0x04001FD0 RID: 8144
		private int _charLen;

		// Token: 0x04001FD1 RID: 8145
		private bool _autoFlush;

		// Token: 0x04001FD2 RID: 8146
		private bool _haveWrittenPreamble;

		// Token: 0x04001FD3 RID: 8147
		private bool _closable;

		// Token: 0x04001FD4 RID: 8148
		private Task _asyncWriteTask = Task.CompletedTask;
	}
}
