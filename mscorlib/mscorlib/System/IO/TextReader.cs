using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Represents a reader that can read a sequential series of characters.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020007AC RID: 1964
	[Serializable]
	public abstract class TextReader : MarshalByRefObject, IDisposable
	{
		/// <summary>Closes the <see cref="T:System.IO.TextReader" /> and releases any system resources associated with the TextReader.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E1B RID: 15899 RVA: 0x000F0A9E File Offset: 0x000EEC9E
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.TextReader" /> object.</summary>
		// Token: 0x06003E1C RID: 15900 RVA: 0x000F0A9E File Offset: 0x000EEC9E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextReader" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003E1D RID: 15901 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Reads the next character without changing the state of the reader or the character source. Returns the next available character without actually reading it from the reader.</summary>
		/// <returns>An integer representing the next character to be read, or -1 if no more characters are available or the reader does not support seeking.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E1E RID: 15902 RVA: 0x000CE6F2 File Offset: 0x000CC8F2
		public virtual int Peek()
		{
			return -1;
		}

		/// <summary>Reads the next character from the text reader and advances the character position by one character.</summary>
		/// <returns>The next character from the text reader, or -1 if no more characters are available. The default implementation returns -1.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E1F RID: 15903 RVA: 0x000CE6F2 File Offset: 0x000CC8F2
		public virtual int Read()
		{
			return -1;
		}

		/// <summary>Reads a specified maximum number of characters from the current reader and writes the data to a buffer, beginning at the specified index.</summary>
		/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether the data is available within the reader. This method returns 0 (zero) if it is called when no more characters are left to read.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source. </param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing. </param>
		/// <param name="count">The maximum number of characters to read. If the end of the reader is reached before the specified number of characters is read into the buffer, the method returns. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E20 RID: 15904 RVA: 0x000F0AB0 File Offset: 0x000EECB0
		public virtual int Read(char[] buffer, int index, int count)
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
			int i;
			for (i = 0; i < count; i++)
			{
				int num = this.Read();
				if (num == -1)
				{
					break;
				}
				buffer[index + i] = (char)num;
			}
			return i;
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000F0B2C File Offset: 0x000EED2C
		public virtual int Read(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int num2;
			try
			{
				int num = this.Read(array, 0, buffer.Length);
				if ((ulong)num > (ulong)((long)buffer.Length))
				{
					throw new IOException("The read operation returned an invalid length.");
				}
				new Span<char>(array, 0, num).CopyTo(buffer);
				num2 = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return num2;
		}

		/// <summary>Reads all characters from the current position to the end of the text reader and returns them as one string.</summary>
		/// <returns>A string that contains all characters from the current position to the end of the text reader.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E22 RID: 15906 RVA: 0x000F0BA8 File Offset: 0x000EEDA8
		public virtual string ReadToEnd()
		{
			char[] array = new char[4096];
			StringBuilder stringBuilder = new StringBuilder(4096);
			int num;
			while ((num = this.Read(array, 0, array.Length)) != 0)
			{
				stringBuilder.Append(array, 0, num);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Reads a line of characters from the text reader and returns the data as a string.</summary>
		/// <returns>The next line from the reader, or null if all characters have been read.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E23 RID: 15907 RVA: 0x000F0BEC File Offset: 0x000EEDEC
		public virtual string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				num = this.Read();
				if (num == -1)
				{
					goto IL_0043;
				}
				if (num == 13 || num == 10)
				{
					break;
				}
				stringBuilder.Append((char)num);
			}
			if (num == 13 && this.Peek() == 10)
			{
				this.Read();
			}
			return stringBuilder.ToString();
			IL_0043:
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		/// <summary>Reads a specified maximum number of characters from the current text reader asynchronously and writes the data to a buffer, beginning at the specified index. </summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the text has been reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the text is reached before the specified number of characters is read into the buffer, the current method returns.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06003E24 RID: 15908 RVA: 0x000F0C50 File Offset: 0x000EEE50
		public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return this.ReadAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x000F0CC8 File Offset: 0x000EEEC8
		internal virtual ValueTask<int> ReadAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			Tuple<TextReader, Memory<char>> tuple = new Tuple<TextReader, Memory<char>>(this, buffer);
			return new ValueTask<int>(Task<int>.Factory.StartNew(delegate(object state)
			{
				Tuple<TextReader, Memory<char>> tuple2 = (Tuple<TextReader, Memory<char>>)state;
				return tuple2.Item1.Read(tuple2.Item2.Span);
			}, tuple, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default));
		}

		/// <summary>Creates a thread-safe wrapper around the specified TextReader.</summary>
		/// <returns>A thread-safe <see cref="T:System.IO.TextReader" />.</returns>
		/// <param name="reader">The TextReader to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reader" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003E26 RID: 15910 RVA: 0x000F0D13 File Offset: 0x000EEF13
		public static TextReader Synchronized(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (!(reader is TextReader.SyncTextReader))
			{
				return new TextReader.SyncTextReader(reader);
			}
			return reader;
		}

		/// <summary>Provides a TextReader with no data to read from.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400200C RID: 8204
		public static readonly TextReader Null = new TextReader.NullTextReader();

		// Token: 0x020007AD RID: 1965
		[Serializable]
		private sealed class NullTextReader : TextReader
		{
			// Token: 0x06003E29 RID: 15913 RVA: 0x00033991 File Offset: 0x00031B91
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06003E2A RID: 15914 RVA: 0x000082D2 File Offset: 0x000064D2
			public override string ReadLine()
			{
				return null;
			}
		}

		// Token: 0x020007AE RID: 1966
		[Serializable]
		internal sealed class SyncTextReader : TextReader
		{
			// Token: 0x06003E2B RID: 15915 RVA: 0x000F0D47 File Offset: 0x000EEF47
			internal SyncTextReader(TextReader t)
			{
				this._in = t;
			}

			// Token: 0x06003E2C RID: 15916 RVA: 0x000F0D56 File Offset: 0x000EEF56
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._in.Close();
			}

			// Token: 0x06003E2D RID: 15917 RVA: 0x000F0D63 File Offset: 0x000EEF63
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._in).Dispose();
				}
			}

			// Token: 0x06003E2E RID: 15918 RVA: 0x000F0D73 File Offset: 0x000EEF73
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Peek()
			{
				return this._in.Peek();
			}

			// Token: 0x06003E2F RID: 15919 RVA: 0x000F0D80 File Offset: 0x000EEF80
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read()
			{
				return this._in.Read();
			}

			// Token: 0x06003E30 RID: 15920 RVA: 0x000F0D8D File Offset: 0x000EEF8D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read(char[] buffer, int index, int count)
			{
				return this._in.Read(buffer, index, count);
			}

			// Token: 0x06003E31 RID: 15921 RVA: 0x000F0D9D File Offset: 0x000EEF9D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadLine()
			{
				return this._in.ReadLine();
			}

			// Token: 0x06003E32 RID: 15922 RVA: 0x000F0DAA File Offset: 0x000EEFAA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadToEnd()
			{
				return this._in.ReadToEnd();
			}

			// Token: 0x06003E33 RID: 15923 RVA: 0x000F0DB8 File Offset: 0x000EEFB8
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<int> ReadAsync(char[] buffer, int index, int count)
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer", "Buffer cannot be null.");
				}
				if (index < 0 || count < 0)
				{
					throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
				}
				if (buffer.Length - index < count)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				return Task.FromResult<int>(this.Read(buffer, index, count));
			}

			// Token: 0x0400200D RID: 8205
			internal readonly TextReader _in;
		}
	}
}
