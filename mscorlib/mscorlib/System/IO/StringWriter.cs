using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing information to a string. The information is stored in an underlying <see cref="T:System.Text.StringBuilder" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020007D8 RID: 2008
	[ComVisible(true)]
	[Serializable]
	public class StringWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class.</summary>
		// Token: 0x0600406C RID: 16492 RVA: 0x000F818B File Offset: 0x000F638B
		public StringWriter()
			: this(new StringBuilder(), CultureInfo.CurrentCulture)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class with the specified format control.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		// Token: 0x0600406D RID: 16493 RVA: 0x000F819D File Offset: 0x000F639D
		public StringWriter(IFormatProvider formatProvider)
			: this(new StringBuilder(), formatProvider)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <param name="sb">The StringBuilder to write to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is null. </exception>
		// Token: 0x0600406E RID: 16494 RVA: 0x000F81AB File Offset: 0x000F63AB
		public StringWriter(StringBuilder sb)
			: this(sb, CultureInfo.CurrentCulture)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" /> and has the specified format provider.</summary>
		/// <param name="sb">The StringBuilder to write to. </param>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is null. </exception>
		// Token: 0x0600406F RID: 16495 RVA: 0x000F81B9 File Offset: 0x000F63B9
		public StringWriter(StringBuilder sb, IFormatProvider formatProvider)
			: base(formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb", Environment.GetResourceString("Buffer cannot be null."));
			}
			this._sb = sb;
			this._isOpen = true;
		}

		/// <summary>Closes the current <see cref="T:System.IO.StringWriter" /> and the underlying stream.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004070 RID: 16496 RVA: 0x000F81E8 File Offset: 0x000F63E8
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StringWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06004071 RID: 16497 RVA: 0x000F81F1 File Offset: 0x000F63F1
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			base.Dispose(disposing);
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The Encoding in which the output is written.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x000F8201 File Offset: 0x000F6401
		public override Encoding Encoding
		{
			get
			{
				if (StringWriter.m_encoding == null)
				{
					StringWriter.m_encoding = new UnicodeEncoding(false, false);
				}
				return StringWriter.m_encoding;
			}
		}

		/// <summary>Writes a character to the string.</summary>
		/// <param name="value">The character to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004073 RID: 16499 RVA: 0x000F8221 File Offset: 0x000F6421
		public override void Write(char value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(value);
		}

		/// <summary>Writes a subarray of characters to the string.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ArgumentException">(<paramref name="index" /> + <paramref name="count" />)&gt; <paramref name="buffer" />. Length. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004074 RID: 16500 RVA: 0x000F8240 File Offset: 0x000F6440
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(buffer, index, count);
		}

		/// <summary>Writes a string to the current string.</summary>
		/// <param name="value">The string to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004075 RID: 16501 RVA: 0x000F82CB File Offset: 0x000F64CB
		public override void Write(string value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			if (value != null)
			{
				this._sb.Append(value);
			}
		}

		/// <summary>Writes a character to the string asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the string.</param>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06004076 RID: 16502 RVA: 0x000F152D File Offset: 0x000EF72D
		[ComVisible(false)]
		public override Task WriteAsync(char value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a string to the current string asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, nothing is written to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06004077 RID: 16503 RVA: 0x000F153B File Offset: 0x000EF73B
		[ComVisible(false)]
		public override Task WriteAsync(string value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a subarray of characters to the string asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation. </exception>
		// Token: 0x06004078 RID: 16504 RVA: 0x000F1549 File Offset: 0x000EF749
		[ComVisible(false)]
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			return Task.CompletedTask;
		}

		/// <summary>Asynchronously clears all buffers for the current writer and causes any buffered data to be written to the underlying device. </summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		// Token: 0x06004079 RID: 16505 RVA: 0x0005D81E File Offset: 0x0005BA1E
		[ComVisible(false)]
		public override Task FlushAsync()
		{
			return Task.CompletedTask;
		}

		/// <summary>Returns a string containing the characters written to the current StringWriter so far.</summary>
		/// <returns>The string containing the characters written to the current StringWriter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600407A RID: 16506 RVA: 0x000F82EA File Offset: 0x000F64EA
		public override string ToString()
		{
			return this._sb.ToString();
		}

		// Token: 0x040020CE RID: 8398
		private static volatile UnicodeEncoding m_encoding;

		// Token: 0x040020CF RID: 8399
		private StringBuilder _sb;

		// Token: 0x040020D0 RID: 8400
		private bool _isOpen;
	}
}
