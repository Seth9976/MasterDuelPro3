using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using Mono.Security;

namespace System.IO
{
	/// <summary>Writes primitive types in binary to a stream and supports writing strings in a specific encoding.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020007D6 RID: 2006
	[ComVisible(true)]
	[Serializable]
	public class BinaryWriter : IDisposable, IAsyncDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class that writes to a stream.</summary>
		// Token: 0x06004046 RID: 16454 RVA: 0x000F7863 File Offset: 0x000F5A63
		protected BinaryWriter()
		{
			this.OutStream = Stream.Null;
			this._buffer = new byte[16];
			this._encoding = new UTF8Encoding(false, true);
			this._encoder = this._encoding.GetEncoder();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class based on the specified stream and using UTF-8 encoding.</summary>
		/// <param name="output">The output stream. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not support writing or is already closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> is null. </exception>
		// Token: 0x06004047 RID: 16455 RVA: 0x000F78A1 File Offset: 0x000F5AA1
		public BinaryWriter(Stream output)
			: this(output, new UTF8Encoding(false, true), false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class based on the specified stream and character encoding.</summary>
		/// <param name="output">The output stream. </param>
		/// <param name="encoding">The character encoding to use. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not support writing or is already closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06004048 RID: 16456 RVA: 0x000F78B2 File Offset: 0x000F5AB2
		public BinaryWriter(Stream output, Encoding encoding)
			: this(output, encoding, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.BinaryWriter" /> class based on the specified stream and character encoding, and optionally leaves the stream open.</summary>
		/// <param name="output">The output stream.</param>
		/// <param name="encoding">The character encoding to use.</param>
		/// <param name="leaveOpen">true to leave the stream open after the <see cref="T:System.IO.BinaryWriter" /> object is disposed; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not support writing or is already closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="output" /> or <paramref name="encoding" /> is null. </exception>
		// Token: 0x06004049 RID: 16457 RVA: 0x000F78C0 File Offset: 0x000F5AC0
		public BinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (!output.CanWrite)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not writable."));
			}
			this.OutStream = output;
			this._buffer = new byte[16];
			this._encoding = encoding;
			this._encoder = this._encoding.GetEncoder();
			this._leaveOpen = leaveOpen;
		}

		/// <summary>Closes the current <see cref="T:System.IO.BinaryWriter" /> and the underlying stream.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600404A RID: 16458 RVA: 0x000F793A File Offset: 0x000F5B3A
		public virtual void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.BinaryWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600404B RID: 16459 RVA: 0x000F7943 File Offset: 0x000F5B43
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._leaveOpen)
				{
					this.OutStream.Flush();
					return;
				}
				this.OutStream.Close();
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.IO.BinaryWriter" /> class.</summary>
		// Token: 0x0600404C RID: 16460 RVA: 0x000F793A File Offset: 0x000F5B3A
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Gets the underlying stream of the <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <returns>The underlying stream associated with the BinaryWriter.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x000F7967 File Offset: 0x000F5B67
		public virtual Stream BaseStream
		{
			get
			{
				this.Flush();
				return this.OutStream;
			}
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600404E RID: 16462 RVA: 0x000F7975 File Offset: 0x000F5B75
		public virtual void Flush()
		{
			this.OutStream.Flush();
		}

		/// <summary>Sets the position within the current stream.</summary>
		/// <returns>The position with the current stream.</returns>
		/// <param name="offset">A byte offset relative to <paramref name="origin" />. </param>
		/// <param name="origin">A field of <see cref="T:System.IO.SeekOrigin" /> indicating the reference point from which the new position is to be obtained. </param>
		/// <exception cref="T:System.IO.IOException">The file pointer was moved to an invalid location. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.IO.SeekOrigin" /> value is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600404F RID: 16463 RVA: 0x000F7982 File Offset: 0x000F5B82
		public virtual long Seek(int offset, SeekOrigin origin)
		{
			return this.OutStream.Seek((long)offset, origin);
		}

		/// <summary>Writes a one-byte Boolean value to the current stream, with 0 representing false and 1 representing true.</summary>
		/// <param name="value">The Boolean value to write (0 or 1). </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004050 RID: 16464 RVA: 0x000F7992 File Offset: 0x000F5B92
		public virtual void Write(bool value)
		{
			this._buffer[0] = (value ? 1 : 0);
			this.OutStream.Write(this._buffer, 0, 1);
		}

		/// <summary>Writes an unsigned byte to the current stream and advances the stream position by one byte.</summary>
		/// <param name="value">The unsigned byte to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004051 RID: 16465 RVA: 0x000F79B7 File Offset: 0x000F5BB7
		public virtual void Write(byte value)
		{
			this.OutStream.WriteByte(value);
		}

		/// <summary>Writes a signed byte to the current stream and advances the stream position by one byte.</summary>
		/// <param name="value">The signed byte to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004052 RID: 16466 RVA: 0x000F79C5 File Offset: 0x000F5BC5
		[CLSCompliant(false)]
		public virtual void Write(sbyte value)
		{
			this.OutStream.WriteByte((byte)value);
		}

		/// <summary>Writes a byte array to the underlying stream.</summary>
		/// <param name="buffer">A byte array containing the data to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004053 RID: 16467 RVA: 0x000F79D4 File Offset: 0x000F5BD4
		public virtual void Write(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.OutStream.Write(buffer, 0, buffer.Length);
		}

		/// <summary>Writes a region of a byte array to the current stream.</summary>
		/// <param name="buffer">A byte array containing the data to write. </param>
		/// <param name="index">The starting point in <paramref name="buffer" /> at which to begin writing. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004054 RID: 16468 RVA: 0x000F79F4 File Offset: 0x000F5BF4
		public virtual void Write(byte[] buffer, int index, int count)
		{
			this.OutStream.Write(buffer, index, count);
		}

		/// <summary>Writes a Unicode character to the current stream and advances the current position of the stream in accordance with the Encoding used and the specific characters being written to the stream.</summary>
		/// <param name="ch">The non-surrogate, Unicode character to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ch" /> is a single surrogate character.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004055 RID: 16469 RVA: 0x000F7A04 File Offset: 0x000F5C04
		public unsafe virtual void Write(char ch)
		{
			if (char.IsSurrogate(ch))
			{
				throw new ArgumentException(Environment.GetResourceString("Unicode surrogate characters must be written out as pairs together in the same call, not individually. Consider passing in a character array instead."));
			}
			byte[] array;
			byte* ptr;
			if ((array = this._buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int bytes = this._encoder.GetBytes(&ch, 1, ptr, this._buffer.Length, true);
			array = null;
			this.OutStream.Write(this._buffer, 0, bytes);
		}

		/// <summary>Writes a character array to the current stream and advances the current position of the stream in accordance with the Encoding used and the specific characters being written to the stream.</summary>
		/// <param name="chars">A character array containing the data to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004056 RID: 16470 RVA: 0x000F7A78 File Offset: 0x000F5C78
		public virtual void Write(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			byte[] bytes = this._encoding.GetBytes(chars, 0, chars.Length);
			this.OutStream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Writes an eight-byte floating-point value to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte floating-point value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004057 RID: 16471 RVA: 0x000F7AB4 File Offset: 0x000F5CB4
		public virtual void Write(double value)
		{
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 8);
		}

		/// <summary>Writes a decimal value to the current stream and advances the stream position by sixteen bytes.</summary>
		/// <param name="value">The decimal value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004058 RID: 16472 RVA: 0x000F7AC9 File Offset: 0x000F5CC9
		public virtual void Write(decimal value)
		{
			decimal.GetBytes(in value, this._buffer);
			this.OutStream.Write(this._buffer, 0, 16);
		}

		/// <summary>Writes a two-byte signed integer to the current stream and advances the stream position by two bytes.</summary>
		/// <param name="value">The two-byte signed integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004059 RID: 16473 RVA: 0x000F7AEC File Offset: 0x000F5CEC
		public virtual void Write(short value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this._buffer, 0, 2);
		}

		/// <summary>Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes.</summary>
		/// <param name="value">The two-byte unsigned integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600405A RID: 16474 RVA: 0x000F7AEC File Offset: 0x000F5CEC
		[CLSCompliant(false)]
		public virtual void Write(ushort value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this.OutStream.Write(this._buffer, 0, 2);
		}

		/// <summary>Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte signed integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600405B RID: 16475 RVA: 0x000F7B18 File Offset: 0x000F5D18
		public virtual void Write(int value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		/// <summary>Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte unsigned integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600405C RID: 16476 RVA: 0x000F7B68 File Offset: 0x000F5D68
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this.OutStream.Write(this._buffer, 0, 4);
		}

		/// <summary>Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte signed integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600405D RID: 16477 RVA: 0x000F7BB8 File Offset: 0x000F5DB8
		public virtual void Write(long value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this._buffer[4] = (byte)(value >> 32);
			this._buffer[5] = (byte)(value >> 40);
			this._buffer[6] = (byte)(value >> 48);
			this._buffer[7] = (byte)(value >> 56);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		/// <summary>Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.</summary>
		/// <param name="value">The eight-byte unsigned integer to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600405E RID: 16478 RVA: 0x000F7C3C File Offset: 0x000F5E3C
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this._buffer[0] = (byte)value;
			this._buffer[1] = (byte)(value >> 8);
			this._buffer[2] = (byte)(value >> 16);
			this._buffer[3] = (byte)(value >> 24);
			this._buffer[4] = (byte)(value >> 32);
			this._buffer[5] = (byte)(value >> 40);
			this._buffer[6] = (byte)(value >> 48);
			this._buffer[7] = (byte)(value >> 56);
			this.OutStream.Write(this._buffer, 0, 8);
		}

		/// <summary>Writes a four-byte floating-point value to the current stream and advances the stream position by four bytes.</summary>
		/// <param name="value">The four-byte floating-point value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600405F RID: 16479 RVA: 0x000F7CC0 File Offset: 0x000F5EC0
		public virtual void Write(float value)
		{
			this.OutStream.Write(BitConverterLE.GetBytes(value), 0, 4);
		}

		/// <summary>Writes a length-prefixed string to this stream in the current encoding of the <see cref="T:System.IO.BinaryWriter" />, and advances the current position of the stream in accordance with the encoding used and the specific characters being written to the stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004060 RID: 16480 RVA: 0x000F7CD8 File Offset: 0x000F5ED8
		public unsafe virtual void Write(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int byteCount = this._encoding.GetByteCount(value);
			this.Write7BitEncodedInt(byteCount);
			if (this._largeByteBuffer == null)
			{
				this._largeByteBuffer = new byte[256];
				this._maxChars = this._largeByteBuffer.Length / this._encoding.GetMaxByteCount(1);
			}
			if (byteCount <= this._largeByteBuffer.Length)
			{
				this._encoding.GetBytes(value, 0, value.Length, this._largeByteBuffer, 0);
				this.OutStream.Write(this._largeByteBuffer, 0, byteCount);
				return;
			}
			int num = 0;
			int num2;
			for (int i = value.Length; i > 0; i -= num2)
			{
				num2 = ((i > this._maxChars) ? this._maxChars : i);
				if (num < 0 || num2 < 0 || checked(num + num2) > value.Length)
				{
					throw new ArgumentOutOfRangeException("charCount");
				}
				int bytes;
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					byte[] array;
					byte* ptr2;
					if ((array = this._largeByteBuffer) == null || array.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array[0];
					}
					bytes = this._encoder.GetBytes(checked(ptr + num), num2, ptr2, this._largeByteBuffer.Length, num2 == i);
					array = null;
				}
				this.OutStream.Write(this._largeByteBuffer, 0, bytes);
				num += num2;
			}
		}

		/// <summary>Writes a 32-bit integer in a compressed format.</summary>
		/// <param name="value">The 32-bit integer to be written. </param>
		/// <exception cref="T:System.IO.EndOfStreamException">The end of the stream is reached. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed. </exception>
		// Token: 0x06004061 RID: 16481 RVA: 0x000F7E38 File Offset: 0x000F6038
		protected void Write7BitEncodedInt(int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				this.Write((byte)(num | 128U));
			}
			this.Write((byte)num);
		}

		/// <summary>Specifies a <see cref="T:System.IO.BinaryWriter" /> with no backing store.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040020C3 RID: 8387
		public static readonly BinaryWriter Null = new BinaryWriter();

		/// <summary>Holds the underlying stream.</summary>
		// Token: 0x040020C4 RID: 8388
		protected Stream OutStream;

		// Token: 0x040020C5 RID: 8389
		private byte[] _buffer;

		// Token: 0x040020C6 RID: 8390
		private Encoding _encoding;

		// Token: 0x040020C7 RID: 8391
		private Encoder _encoder;

		// Token: 0x040020C8 RID: 8392
		[OptionalField]
		private bool _leaveOpen;

		// Token: 0x040020C9 RID: 8393
		private byte[] _largeByteBuffer;

		// Token: 0x040020CA RID: 8394
		private int _maxChars;
	}
}
