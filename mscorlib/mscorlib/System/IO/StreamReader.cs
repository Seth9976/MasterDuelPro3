using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextReader" /> that reads characters from a byte stream in a particular encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020007A3 RID: 1955
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x06003DB6 RID: 15798 RVA: 0x000EDCC7 File Offset: 0x000EBEC7
		private void CheckAsyncTaskInProgress()
		{
			if (!this._asyncReadTask.IsCompleted)
			{
				StreamReader.ThrowAsyncIOInProgress();
			}
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000EDCDB File Offset: 0x000EBEDB
		private static void ThrowAsyncIOInProgress()
		{
			throw new InvalidOperationException("The stream is currently in use by a previous operation on the stream.");
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000EDCE7 File Offset: 0x000EBEE7
		internal StreamReader()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000EDCFA File Offset: 0x000EBEFA
		public StreamReader(Stream stream)
			: this(stream, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06003DBA RID: 15802 RVA: 0x000EDD04 File Offset: 0x000EBF04
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
			: this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06003DBB RID: 15803 RVA: 0x000EDD19 File Offset: 0x000EBF19
		public StreamReader(Stream stream, Encoding encoding)
			: this(stream, encoding, true, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06003DBC RID: 15804 RVA: 0x000EDD2A File Offset: 0x000EBF2A
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, 1024, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="stream">The stream to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <param name="bufferSize">The minimum buffer size. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not support reading. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		// Token: 0x06003DBD RID: 15805 RVA: 0x000EDD3B File Offset: 0x000EBF3B
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified stream based on the specified character encoding, byte order mark detection option, and buffer size, and optionally leaves the stream open.</summary>
		/// <param name="stream">The stream to read.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="detectEncodingFromByteOrderMarks">true to look for byte order marks at the beginning of the file; otherwise, false.</param>
		/// <param name="bufferSize">The minimum buffer size.</param>
		/// <param name="leaveOpen">true to leave the stream open after the <see cref="T:System.IO.StreamReader" /> object is disposed; otherwise, false.</param>
		// Token: 0x06003DBE RID: 15806 RVA: 0x000EDD4C File Offset: 0x000EBF4C
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream was not readable.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06003DBF RID: 15807 RVA: 0x000EDDBA File Offset: 0x000EBFBA
		public StreamReader(string path)
			: this(path, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06003DC0 RID: 15808 RVA: 0x000EDDC4 File Offset: 0x000EBFC4
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
			: this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06003DC1 RID: 15809 RVA: 0x000EDDD8 File Offset: 0x000EBFD8
		public StreamReader(string path, Encoding encoding)
			: this(path, encoding, true, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding and byte order mark detection option.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		// Token: 0x06003DC2 RID: 15810 RVA: 0x000EDDE8 File Offset: 0x000EBFE8
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(path, encoding, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StreamReader" /> class for the specified file name, with the specified character encoding, byte order mark detection option, and buffer size.</summary>
		/// <param name="path">The complete file path to be read. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file. </param>
		/// <param name="bufferSize">The minimum buffer size, in number of 16-bit characters. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> or <paramref name="encoding" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> includes an incorrect or invalid syntax for file name, directory name, or volume label. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="buffersize" /> is less than or equal to zero. </exception>
		// Token: 0x06003DC3 RID: 15811 RVA: 0x000EDDF8 File Offset: 0x000EBFF8
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
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
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false);
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x000EDE7C File Offset: 0x000EC07C
		private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			this._stream = stream;
			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this._byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this._charBuffer = new char[this._maxCharsPerBuffer];
			this._byteLen = 0;
			this._bytePos = 0;
			this._detectEncoding = detectEncodingFromByteOrderMarks;
			this._checkPreamble = encoding.Preamble.Length > 0;
			this._isBlocked = false;
			this._closable = !leaveOpen;
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000EDF1D File Offset: 0x000EC11D
		internal void Init(Stream stream)
		{
			this._stream = stream;
			this._closable = true;
		}

		/// <summary>Closes the <see cref="T:System.IO.StreamReader" /> object and the underlying stream, and releases any system resources associated with the reader.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003DC6 RID: 15814 RVA: 0x000EDF2D File Offset: 0x000EC12D
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Closes the underlying stream, releases the unmanaged resources used by the <see cref="T:System.IO.StreamReader" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003DC7 RID: 15815 RVA: 0x000EDF38 File Offset: 0x000EC138
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.LeaveOpen && disposing && this._stream != null)
				{
					this._stream.Close();
				}
			}
			finally
			{
				if (!this.LeaveOpen && this._stream != null)
				{
					this._stream = null;
					this._encoding = null;
					this._decoder = null;
					this._byteBuffer = null;
					this._charBuffer = null;
					this._charPos = 0;
					this._charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		/// <summary>Gets the current character encoding that the current <see cref="T:System.IO.StreamReader" /> object is using.</summary>
		/// <returns>The current character encoding used by the current reader. The value can be different after the first call to any <see cref="Overload:System.IO.StreamReader.Read" /> method of <see cref="T:System.IO.StreamReader" />, since encoding autodetection is not done until the first call to a <see cref="Overload:System.IO.StreamReader.Read" /> method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000EDFC0 File Offset: 0x000EC1C0
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this._encoding;
			}
		}

		/// <summary>Returns the underlying stream.</summary>
		/// <returns>The underlying stream.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x000EDFC8 File Offset: 0x000EC1C8
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000EDFD0 File Offset: 0x000EC1D0
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		/// <summary>Gets a value that indicates whether the current stream position is at the end of the stream.</summary>
		/// <returns>true if the current stream position is at the end of the stream; otherwise false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying stream has been disposed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x000EDFDB File Offset: 0x000EC1DB
		public bool EndOfStream
		{
			get
			{
				if (this._stream == null)
				{
					throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
				}
				this.CheckAsyncTaskInProgress();
				return this._charPos >= this._charLen && this.ReadBuffer() == 0;
			}
		}

		/// <summary>Returns the next available character but does not consume it.</summary>
		/// <returns>An integer representing the next character to be read, or -1 if there are no characters to be read or if the stream does not support seeking.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003DCC RID: 15820 RVA: 0x000EE010 File Offset: 0x000EC210
		public override int Peek()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && (this._isBlocked || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this._charBuffer[this._charPos];
		}

		/// <summary>Reads the next character from the input stream and advances the character position by one character.</summary>
		/// <returns>The next character from the input stream represented as an <see cref="T:System.Int32" /> object, or -1 if no more characters are available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003DCD RID: 15821 RVA: 0x000EE064 File Offset: 0x000EC264
		public override int Read()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && this.ReadBuffer() == 0)
			{
				return -1;
			}
			int num = (int)this._charBuffer[this._charPos];
			this._charPos++;
			return num;
		}

		/// <summary>Reads a specified maximum of characters from the current stream into a buffer, beginning at the specified index.</summary>
		/// <returns>The number of characters that have been read, or 0 if at the end of the stream and no data was read. The number will be less than or equal to the <paramref name="count" /> parameter, depending on whether the data is available within the stream.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index + count - 1" />) replaced by the characters read from the current source. </param>
		/// <param name="index">The index of <paramref name="buffer" /> at which to begin writing. </param>
		/// <param name="count">The maximum number of characters to read. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003DCE RID: 15822 RVA: 0x000EE0C0 File Offset: 0x000EC2C0
		public override int Read(char[] buffer, int index, int count)
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
			return this.ReadSpan(new Span<char>(buffer, index, count));
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000EE124 File Offset: 0x000EC324
		public override int Read(Span<char> buffer)
		{
			if (!(base.GetType() == typeof(StreamReader)))
			{
				return base.Read(buffer);
			}
			return this.ReadSpan(buffer);
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000EE14C File Offset: 0x000EC34C
		private int ReadSpan(Span<char> buffer)
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			int num = 0;
			bool flag = false;
			int i = buffer.Length;
			while (i > 0)
			{
				int num2 = this._charLen - this._charPos;
				if (num2 == 0)
				{
					num2 = this.ReadBuffer(buffer.Slice(num), out flag);
				}
				if (num2 == 0)
				{
					break;
				}
				if (num2 > i)
				{
					num2 = i;
				}
				if (!flag)
				{
					new Span<char>(this._charBuffer, this._charPos, num2).CopyTo(buffer.Slice(num));
					this._charPos += num2;
				}
				num += num2;
				i -= num2;
				if (this._isBlocked)
				{
					break;
				}
			}
			return num;
		}

		/// <summary>Reads all characters from the current position to the end of the stream.</summary>
		/// <returns>The rest of the stream as a string, from the current position to the end. If the current position is at the end of the stream, returns an empty string ("").</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003DD1 RID: 15825 RVA: 0x000EE1F8 File Offset: 0x000EC3F8
		public override string ReadToEnd()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			StringBuilder stringBuilder = new StringBuilder(this._charLen - this._charPos);
			do
			{
				stringBuilder.Append(this._charBuffer, this._charPos, this._charLen - this._charPos);
				this._charPos = this._charLen;
				this.ReadBuffer();
			}
			while (this._charLen > 0);
			return stringBuilder.ToString();
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000EE274 File Offset: 0x000EC474
		private void CompressBuffer(int n)
		{
			Buffer.BlockCopy(this._byteBuffer, n, this._byteBuffer, 0, this._byteLen - n);
			this._byteLen -= n;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000EE2A0 File Offset: 0x000EC4A0
		private void DetectEncoding()
		{
			if (this._byteLen < 2)
			{
				return;
			}
			this._detectEncoding = false;
			bool flag = false;
			if (this._byteBuffer[0] == 254 && this._byteBuffer[1] == 255)
			{
				this._encoding = Encoding.BigEndianUnicode;
				this.CompressBuffer(2);
				flag = true;
			}
			else if (this._byteBuffer[0] == 255 && this._byteBuffer[1] == 254)
			{
				if (this._byteLen < 4 || this._byteBuffer[2] != 0 || this._byteBuffer[3] != 0)
				{
					this._encoding = Encoding.Unicode;
					this.CompressBuffer(2);
					flag = true;
				}
				else
				{
					this._encoding = Encoding.UTF32;
					this.CompressBuffer(4);
					flag = true;
				}
			}
			else if (this._byteLen >= 3 && this._byteBuffer[0] == 239 && this._byteBuffer[1] == 187 && this._byteBuffer[2] == 191)
			{
				this._encoding = Encoding.UTF8;
				this.CompressBuffer(3);
				flag = true;
			}
			else if (this._byteLen >= 4 && this._byteBuffer[0] == 0 && this._byteBuffer[1] == 0 && this._byteBuffer[2] == 254 && this._byteBuffer[3] == 255)
			{
				this._encoding = new UTF32Encoding(true, true);
				this.CompressBuffer(4);
				flag = true;
			}
			else if (this._byteLen == 2)
			{
				this._detectEncoding = true;
			}
			if (flag)
			{
				this._decoder = this._encoding.GetDecoder();
				int maxCharCount = this._encoding.GetMaxCharCount(this._byteBuffer.Length);
				if (maxCharCount > this._maxCharsPerBuffer)
				{
					this._charBuffer = new char[maxCharCount];
				}
				this._maxCharsPerBuffer = maxCharCount;
			}
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000EE458 File Offset: 0x000EC658
		private unsafe bool IsPreamble()
		{
			if (!this._checkPreamble)
			{
				return this._checkPreamble;
			}
			ReadOnlySpan<byte> preamble = this._encoding.Preamble;
			int num = ((this._byteLen >= preamble.Length) ? (preamble.Length - this._bytePos) : (this._byteLen - this._bytePos));
			int i = 0;
			while (i < num)
			{
				if (this._byteBuffer[this._bytePos] != *preamble[this._bytePos])
				{
					this._bytePos = 0;
					this._checkPreamble = false;
					break;
				}
				i++;
				this._bytePos++;
			}
			if (this._checkPreamble && this._bytePos == preamble.Length)
			{
				this.CompressBuffer(preamble.Length);
				this._bytePos = 0;
				this._checkPreamble = false;
				this._detectEncoding = false;
			}
			return this._checkPreamble;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000EE534 File Offset: 0x000EC734
		internal virtual int ReadBuffer()
		{
			this._charLen = 0;
			this._charPos = 0;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num = this._stream.Read(this._byteBuffer, this._bytePos, this._byteBuffer.Length - this._bytePos);
					if (num == 0)
					{
						break;
					}
					this._byteLen += num;
				}
				else
				{
					this._byteLen = this._stream.Read(this._byteBuffer, 0, this._byteBuffer.Length);
					if (this._byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = this._byteLen < this._byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this._charLen += this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				}
				if (this._charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this._byteLen > 0)
			{
				this._charLen += this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				this._bytePos = (this._byteLen = 0);
			}
			return this._charLen;
			Block_5:
			return this._charLen;
			Block_9:
			return this._charLen;
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000EE69C File Offset: 0x000EC89C
		private int ReadBuffer(Span<char> userBuffer, out bool readToUserBuffer)
		{
			this._charLen = 0;
			this._charPos = 0;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			int num = 0;
			readToUserBuffer = userBuffer.Length >= this._maxCharsPerBuffer;
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num2 = this._stream.Read(this._byteBuffer, this._bytePos, this._byteBuffer.Length - this._bytePos);
					if (num2 == 0)
					{
						break;
					}
					this._byteLen += num2;
				}
				else
				{
					this._byteLen = this._stream.Read(this._byteBuffer, 0, this._byteBuffer.Length);
					if (this._byteLen == 0)
					{
						goto IL_01CD;
					}
				}
				this._isBlocked = this._byteLen < this._byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
						readToUserBuffer = userBuffer.Length >= this._maxCharsPerBuffer;
					}
					this._charPos = 0;
					if (readToUserBuffer)
					{
						num += this._decoder.GetChars(new ReadOnlySpan<byte>(this._byteBuffer, 0, this._byteLen), userBuffer.Slice(num), false);
						this._charLen = 0;
					}
					else
					{
						num = this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, num);
						this._charLen += num;
					}
				}
				if (num != 0)
				{
					goto IL_01CD;
				}
			}
			if (this._byteLen > 0)
			{
				if (readToUserBuffer)
				{
					num = this._decoder.GetChars(new ReadOnlySpan<byte>(this._byteBuffer, 0, this._byteLen), userBuffer.Slice(num), false);
					this._charLen = 0;
				}
				else
				{
					num = this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, num);
					this._charLen += num;
				}
			}
			return num;
			IL_01CD:
			this._isBlocked &= num < userBuffer.Length;
			return num;
		}

		/// <summary>Reads a line of characters from the current stream and returns the data as a string.</summary>
		/// <returns>The next line from the input stream, or null if the end of the input stream is reached.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003DD7 RID: 15831 RVA: 0x000EE890 File Offset: 0x000ECA90
		public override string ReadLine()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && this.ReadBuffer() == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num;
			char c;
			for (;;)
			{
				num = this._charPos;
				do
				{
					c = this._charBuffer[num];
					if (c == '\r' || c == '\n')
					{
						goto IL_0051;
					}
					num++;
				}
				while (num < this._charLen);
				num = this._charLen - this._charPos;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num + 80);
				}
				stringBuilder.Append(this._charBuffer, this._charPos, num);
				if (this.ReadBuffer() <= 0)
				{
					goto Block_11;
				}
			}
			IL_0051:
			string text;
			if (stringBuilder != null)
			{
				stringBuilder.Append(this._charBuffer, this._charPos, num - this._charPos);
				text = stringBuilder.ToString();
			}
			else
			{
				text = new string(this._charBuffer, this._charPos, num - this._charPos);
			}
			this._charPos = num + 1;
			if (c == '\r' && (this._charPos < this._charLen || this.ReadBuffer() > 0) && this._charBuffer[this._charPos] == '\n')
			{
				this._charPos++;
			}
			return text;
			Block_11:
			return stringBuilder.ToString();
		}

		/// <summary>Reads a specified maximum number of characters from the current stream asynchronously and writes the data to a buffer, beginning at the specified index. </summary>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the stream is reached before the specified number of characters is written into the buffer, the current method returns.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation. </exception>
		// Token: 0x06003DD8 RID: 15832 RVA: 0x000EE9C8 File Offset: 0x000ECBC8
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
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = this.ReadAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000EEA84 File Offset: 0x000ECC84
		internal override async ValueTask<int> ReadAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			bool flag = this._charPos == this._charLen;
			if (flag)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				flag = configuredTaskAwaiter.GetResult() == 0;
			}
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int charsRead = 0;
				bool readToUserBuffer = false;
				byte[] tmpByteBuffer = this._byteBuffer;
				Stream tmpStream = this._stream;
				int count = buffer.Length;
				while (count > 0)
				{
					int i = this._charLen - this._charPos;
					if (i == 0)
					{
						this._charLen = 0;
						this._charPos = 0;
						if (!this._checkPreamble)
						{
							this._byteLen = 0;
						}
						readToUserBuffer = count >= this._maxCharsPerBuffer;
						do
						{
							if (this._checkPreamble)
							{
								int bytePos = this._bytePos;
								int num2 = await tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer, bytePos, tmpByteBuffer.Length - bytePos), cancellationToken).ConfigureAwait(false);
								if (num2 == 0)
								{
									goto Block_7;
								}
								this._byteLen += num2;
							}
							else
							{
								this._byteLen = await tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer), cancellationToken).ConfigureAwait(false);
								if (this._byteLen == 0)
								{
									goto Block_10;
								}
							}
							this._isBlocked = this._byteLen < tmpByteBuffer.Length;
							if (!this.IsPreamble())
							{
								if (this._detectEncoding && this._byteLen >= 2)
								{
									this.DetectEncoding();
									readToUserBuffer = count >= this._maxCharsPerBuffer;
								}
								this._charPos = 0;
								if (readToUserBuffer)
								{
									i += this._decoder.GetChars(new ReadOnlySpan<byte>(tmpByteBuffer, 0, this._byteLen), buffer.Span.Slice(charsRead), false);
									this._charLen = 0;
								}
								else
								{
									i = this._decoder.GetChars(tmpByteBuffer, 0, this._byteLen, this._charBuffer, 0);
									this._charLen += i;
								}
							}
						}
						while (i == 0);
						IL_0423:
						if (i != 0)
						{
							goto IL_042E;
						}
						break;
						Block_10:
						this._isBlocked = true;
						goto IL_0423;
						Block_7:
						if (this._byteLen > 0)
						{
							if (readToUserBuffer)
							{
								i = this._decoder.GetChars(new ReadOnlySpan<byte>(tmpByteBuffer, 0, this._byteLen), buffer.Span.Slice(charsRead), false);
								this._charLen = 0;
							}
							else
							{
								i = this._decoder.GetChars(tmpByteBuffer, 0, this._byteLen, this._charBuffer, 0);
								this._charLen += i;
							}
						}
						this._isBlocked = true;
						goto IL_0423;
					}
					IL_042E:
					if (i > count)
					{
						i = count;
					}
					if (!readToUserBuffer)
					{
						new Span<char>(this._charBuffer, this._charPos, i).CopyTo(buffer.Span.Slice(charsRead));
						this._charPos += i;
					}
					charsRead += i;
					count -= i;
					if (this._isBlocked)
					{
						break;
					}
				}
				num = charsRead;
			}
			return num;
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000EEAD8 File Offset: 0x000ECCD8
		private async Task<int> ReadBufferAsync()
		{
			this._charLen = 0;
			this._charPos = 0;
			byte[] tmpByteBuffer = this._byteBuffer;
			Stream tmpStream = this._stream;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int bytePos = this._bytePos;
					int num = await tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer, bytePos, tmpByteBuffer.Length - bytePos), default(CancellationToken)).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					this._byteLen += num;
				}
				else
				{
					this._byteLen = await tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer), default(CancellationToken)).ConfigureAwait(false);
					if (this._byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = this._byteLen < tmpByteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this._charLen += this._decoder.GetChars(tmpByteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				}
				if (this._charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this._byteLen > 0)
			{
				this._charLen += this._decoder.GetChars(tmpByteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				this._bytePos = 0;
				this._byteLen = 0;
			}
			return this._charLen;
			Block_5:
			return this._charLen;
			Block_9:
			return this._charLen;
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000EEB1B File Offset: 0x000ECD1B
		internal bool DataAvailable()
		{
			return this._charPos < this._charLen;
		}

		/// <summary>A <see cref="T:System.IO.StreamReader" /> object around an empty stream.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001FA6 RID: 8102
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x04001FA7 RID: 8103
		private Stream _stream;

		// Token: 0x04001FA8 RID: 8104
		private Encoding _encoding;

		// Token: 0x04001FA9 RID: 8105
		private Decoder _decoder;

		// Token: 0x04001FAA RID: 8106
		private byte[] _byteBuffer;

		// Token: 0x04001FAB RID: 8107
		private char[] _charBuffer;

		// Token: 0x04001FAC RID: 8108
		private int _charPos;

		// Token: 0x04001FAD RID: 8109
		private int _charLen;

		// Token: 0x04001FAE RID: 8110
		private int _byteLen;

		// Token: 0x04001FAF RID: 8111
		private int _bytePos;

		// Token: 0x04001FB0 RID: 8112
		private int _maxCharsPerBuffer;

		// Token: 0x04001FB1 RID: 8113
		private bool _detectEncoding;

		// Token: 0x04001FB2 RID: 8114
		private bool _checkPreamble;

		// Token: 0x04001FB3 RID: 8115
		private bool _isBlocked;

		// Token: 0x04001FB4 RID: 8116
		private bool _closable;

		// Token: 0x04001FB5 RID: 8117
		private Task _asyncReadTask = Task.CompletedTask;

		// Token: 0x020007A4 RID: 1956
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06003DDD RID: 15837 RVA: 0x000EEB37 File Offset: 0x000ECD37
			internal NullStreamReader()
			{
				base.Init(Stream.Null);
			}

			// Token: 0x170009FA RID: 2554
			// (get) Token: 0x06003DDE RID: 15838 RVA: 0x000EEB4A File Offset: 0x000ECD4A
			public override Stream BaseStream
			{
				get
				{
					return Stream.Null;
				}
			}

			// Token: 0x170009FB RID: 2555
			// (get) Token: 0x06003DDF RID: 15839 RVA: 0x000EEB51 File Offset: 0x000ECD51
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06003DE0 RID: 15840 RVA: 0x00002C89 File Offset: 0x00000E89
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06003DE1 RID: 15841 RVA: 0x000CE6F2 File Offset: 0x000CC8F2
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06003DE2 RID: 15842 RVA: 0x000CE6F2 File Offset: 0x000CC8F2
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06003DE3 RID: 15843 RVA: 0x00033991 File Offset: 0x00031B91
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06003DE4 RID: 15844 RVA: 0x000082D2 File Offset: 0x000064D2
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06003DE5 RID: 15845 RVA: 0x0001C227 File Offset: 0x0001A427
			public override string ReadToEnd()
			{
				return string.Empty;
			}

			// Token: 0x06003DE6 RID: 15846 RVA: 0x00033991 File Offset: 0x00031B91
			internal override int ReadBuffer()
			{
				return 0;
			}
		}
	}
}
