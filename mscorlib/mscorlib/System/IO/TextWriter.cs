using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Represents a writer that can write a sequential series of characters. This class is abstract.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020007B0 RID: 1968
	[Serializable]
	public abstract class TextWriter : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter" /> class.</summary>
		// Token: 0x06003E37 RID: 15927 RVA: 0x000F0E55 File Offset: 0x000EF055
		protected TextWriter()
		{
			this._internalFormatProvider = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter" /> class with the specified format provider.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting. </param>
		// Token: 0x06003E38 RID: 15928 RVA: 0x000F0E7A File Offset: 0x000EF07A
		protected TextWriter(IFormatProvider formatProvider)
		{
			this._internalFormatProvider = formatProvider;
		}

		/// <summary>Gets an object that controls formatting.</summary>
		/// <returns>An <see cref="T:System.IFormatProvider" /> object for a specific culture, or the formatting of the current culture if no other culture is specified.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06003E39 RID: 15929 RVA: 0x000F0E9F File Offset: 0x000EF09F
		public virtual IFormatProvider FormatProvider
		{
			get
			{
				if (this._internalFormatProvider == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._internalFormatProvider;
			}
		}

		/// <summary>Closes the current writer and releases any system resources associated with the writer.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E3A RID: 15930 RVA: 0x000EF5DF File Offset: 0x000ED7DF
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003E3B RID: 15931 RVA: 0x00002C89 File Offset: 0x00000E89
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.IO.TextWriter" /> object.</summary>
		// Token: 0x06003E3C RID: 15932 RVA: 0x000EF5DF File Offset: 0x000ED7DF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E3D RID: 15933 RVA: 0x00002C89 File Offset: 0x00000E89
		public virtual void Flush()
		{
		}

		/// <summary>When overridden in a derived class, returns the character encoding in which the output is written.</summary>
		/// <returns>The character encoding in which the output is written.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06003E3E RID: 15934
		public abstract Encoding Encoding { get; }

		/// <summary>Gets or sets the line terminator string used by the current TextWriter.</summary>
		/// <returns>The line terminator string for the current TextWriter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x000F0EB5 File Offset: 0x000EF0B5
		public virtual string NewLine
		{
			get
			{
				return this.CoreNewLineStr;
			}
		}

		/// <summary>Writes a character to the text string or stream.</summary>
		/// <param name="value">The character to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E40 RID: 15936 RVA: 0x00002C89 File Offset: 0x00000E89
		public virtual void Write(char value)
		{
		}

		/// <summary>Writes a character array to the text string or stream.</summary>
		/// <param name="buffer">The character array to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E41 RID: 15937 RVA: 0x000F0EBD File Offset: 0x000EF0BD
		public virtual void Write(char[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);
			}
		}

		/// <summary>Writes a subarray of characters to the text string or stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">The character position in the buffer at which to start retrieving data. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E42 RID: 15938 RVA: 0x000F0ED0 File Offset: 0x000EF0D0
		public virtual void Write(char[] buffer, int index, int count)
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
			for (int i = 0; i < count; i++)
			{
				this.Write(buffer[index + i]);
			}
		}

		/// <summary>Writes the text representation of a Boolean value to the text string or stream.</summary>
		/// <param name="value">The Boolean value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E43 RID: 15939 RVA: 0x000F0F42 File Offset: 0x000EF142
		public virtual void Write(bool value)
		{
			this.Write(value ? "True" : "False");
		}

		/// <summary>Writes the text representation of a 4-byte signed integer to the text string or stream.</summary>
		/// <param name="value">The 4-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E44 RID: 15940 RVA: 0x000F0F59 File Offset: 0x000EF159
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a 4-byte unsigned integer to the text string or stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E45 RID: 15941 RVA: 0x000F0F6E File Offset: 0x000EF16E
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte signed integer to the text string or stream.</summary>
		/// <param name="value">The 8-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E46 RID: 15942 RVA: 0x000F0F83 File Offset: 0x000EF183
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte unsigned integer to the text string or stream.</summary>
		/// <param name="value">The 8-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E47 RID: 15943 RVA: 0x000F0F98 File Offset: 0x000EF198
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a 4-byte floating-point value to the text string or stream.</summary>
		/// <param name="value">The 4-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E48 RID: 15944 RVA: 0x000F0FAD File Offset: 0x000EF1AD
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of an 8-byte floating-point value to the text string or stream.</summary>
		/// <param name="value">The 8-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E49 RID: 15945 RVA: 0x000F0FC2 File Offset: 0x000EF1C2
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes the text representation of a decimal value to the text string or stream.</summary>
		/// <param name="value">The decimal value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E4A RID: 15946 RVA: 0x000F0FD7 File Offset: 0x000EF1D7
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		/// <summary>Writes a string to the text string or stream.</summary>
		/// <param name="value">The string to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E4B RID: 15947 RVA: 0x000F0FEC File Offset: 0x000EF1EC
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is one). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E4C RID: 15948 RVA: 0x000F0FFD File Offset: 0x000EF1FD
		public virtual void Write(string format, object arg0)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0));
		}

		/// <summary>Writes a formatted string to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to format and write. </param>
		/// <param name="arg1">The second object to format and write. </param>
		/// <param name="arg2">The third object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is three). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E4D RID: 15949 RVA: 0x000F1012 File Offset: 0x000EF212
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		/// <summary>Writes a line terminator to the text string or stream.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E4E RID: 15950 RVA: 0x000F102A File Offset: 0x000EF22A
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		/// <summary>Writes a character followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The character to write to the text stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E4F RID: 15951 RVA: 0x000F1038 File Offset: 0x000EF238
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes an array of characters followed by a line terminator to the text string or stream.</summary>
		/// <param name="buffer">The character array from which data is read. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E50 RID: 15952 RVA: 0x000F1047 File Offset: 0x000EF247
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		/// <summary>Writes a subarray of characters followed by a line terminator to the text string or stream.</summary>
		/// <param name="buffer">The character array from which data is read. </param>
		/// <param name="index">The character position in <paramref name="buffer" /> at which to start reading data. </param>
		/// <param name="count">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E51 RID: 15953 RVA: 0x000F1056 File Offset: 0x000EF256
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a Boolean value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The Boolean value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E52 RID: 15954 RVA: 0x000F1067 File Offset: 0x000EF267
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte signed integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E53 RID: 15955 RVA: 0x000F1076 File Offset: 0x000EF276
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E54 RID: 15956 RVA: 0x000F1085 File Offset: 0x000EF285
		[CLSCompliant(false)]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an 8-byte signed integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte signed integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E55 RID: 15957 RVA: 0x000F1094 File Offset: 0x000EF294
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte unsigned integer to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E56 RID: 15958 RVA: 0x000F10A3 File Offset: 0x000EF2A3
		[CLSCompliant(false)]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 4-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E57 RID: 15959 RVA: 0x000F10B2 File Offset: 0x000EF2B2
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The 8-byte floating-point value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E58 RID: 15960 RVA: 0x000F10C1 File Offset: 0x000EF2C1
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes the text representation of a decimal value followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The decimal value to write. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E59 RID: 15961 RVA: 0x000F10D0 File Offset: 0x000EF2D0
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		/// <summary>Writes a string followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, only the line terminator is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E5A RID: 15962 RVA: 0x000F10DF File Offset: 0x000EF2DF
		public virtual void WriteLine(string value)
		{
			if (value != null)
			{
				this.Write(value);
			}
			this.Write(this.CoreNewLineStr);
		}

		/// <summary>Writes the text representation of an object by calling the ToString method on that object, followed by a line terminator to the text string or stream.</summary>
		/// <param name="value">The object to write. If <paramref name="value" /> is null, only the line terminator is written. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E5B RID: 15963 RVA: 0x000F10F8 File Offset: 0x000EF2F8
		public virtual void WriteLine(object value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				this.WriteLine(formattable.ToString(null, this.FormatProvider));
				return;
			}
			this.WriteLine(value.ToString());
		}

		/// <summary>Writes a formatted string and a new line to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is one). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E5C RID: 15964 RVA: 0x000F1139 File Offset: 0x000EF339
		public virtual void WriteLine(string format, object arg0)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0));
		}

		/// <summary>Writes a formatted string and a new line to the text string or stream, using the same semantics as the <see cref="M:System.String.Format(System.String,System.Object,System.Object)" /> method.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to format and write. </param>
		/// <param name="arg1">The second object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is two). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E5D RID: 15965 RVA: 0x000F114E File Offset: 0x000EF34E
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to format and write. </param>
		/// <param name="arg1">The second object to format and write. </param>
		/// <param name="arg2">The third object to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the number of objects to be formatted (which, for this method overload, is three). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E5E RID: 15966 RVA: 0x000F1164 File Offset: 0x000EF364
		public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		/// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)" />.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg">An object array that contains zero or more objects to format and write. </param>
		/// <exception cref="T:System.ArgumentNullException">A string or object is passed in as null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter" /> is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid composite format string.-or- The index of a format item is less than 0 (zero), or greater than or equal to the length of the <paramref name="arg" /> array. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003E5F RID: 15967 RVA: 0x000F117C File Offset: 0x000EF37C
		public virtual void WriteLine(string format, params object[] arg)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg));
		}

		/// <summary>Writes a character to the text string or stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="value">The character to write to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06003E60 RID: 15968 RVA: 0x000F1194 File Offset: 0x000EF394
		public virtual Task WriteAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char> tuple2 = (Tuple<TextWriter, char>)state;
				tuple2.Item1.Write(tuple2.Item2);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a string to the text string or stream asynchronously.</summary>
		/// <returns>A task that represents the asynchronous write operation. </returns>
		/// <param name="value">The string to write. If <paramref name="value" /> is null, nothing is written to the text stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06003E61 RID: 15969 RVA: 0x000F11E0 File Offset: 0x000EF3E0
		public virtual Task WriteAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, string> tuple2 = (Tuple<TextWriter, string>)state;
				tuple2.Item1.Write(tuple2.Item2);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Writes a subarray of characters to the text string or stream asynchronously. </summary>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">The character position in the buffer at which to start retrieving data. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The text writer is currently in use by a previous write operation. </exception>
		// Token: 0x06003E62 RID: 15970 RVA: 0x000F122C File Offset: 0x000EF42C
		public virtual Task WriteAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple2 = (Tuple<TextWriter, char[], int, int>)state;
				tuple2.Item1.Write(tuple2.Item2, tuple2.Item3, tuple2.Item4);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Asynchronously clears all buffers for the current writer and causes any buffered data to be written to the underlying device. </summary>
		/// <returns>A task that represents the asynchronous flush operation. </returns>
		/// <exception cref="T:System.ObjectDisposedException">The text writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The writer is currently in use by a previous write operation. </exception>
		// Token: 0x06003E63 RID: 15971 RVA: 0x000F1278 File Offset: 0x000EF478
		public virtual Task FlushAsync()
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((TextWriter)state).Flush();
			}, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Creates a thread-safe wrapper around the specified TextWriter.</summary>
		/// <returns>A thread-safe wrapper.</returns>
		/// <param name="writer">The TextWriter to synchronize. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003E64 RID: 15972 RVA: 0x000F12AF File Offset: 0x000EF4AF
		public static TextWriter Synchronized(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (!(writer is TextWriter.SyncTextWriter))
			{
				return new TextWriter.SyncTextWriter(writer);
			}
			return writer;
		}

		/// <summary>Provides a TextWriter with no backing store that can be written to, but not read from.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04002010 RID: 8208
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x04002011 RID: 8209
		private static readonly char[] s_coreNewLine = Environment.NewLine.ToCharArray();

		/// <summary>Stores the newline characters used for this TextWriter.</summary>
		// Token: 0x04002012 RID: 8210
		protected char[] CoreNewLine = TextWriter.s_coreNewLine;

		// Token: 0x04002013 RID: 8211
		private string CoreNewLineStr = Environment.NewLine;

		// Token: 0x04002014 RID: 8212
		private IFormatProvider _internalFormatProvider;

		// Token: 0x020007B1 RID: 1969
		[Serializable]
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x06003E66 RID: 15974 RVA: 0x000F12EA File Offset: 0x000EF4EA
			internal NullTextWriter()
				: base(CultureInfo.InvariantCulture)
			{
			}

			// Token: 0x17000A06 RID: 2566
			// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000EEB51 File Offset: 0x000ECD51
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06003E68 RID: 15976 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void Write(char[] buffer, int index, int count)
			{
			}

			// Token: 0x06003E69 RID: 15977 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void Write(string value)
			{
			}

			// Token: 0x06003E6A RID: 15978 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void WriteLine()
			{
			}

			// Token: 0x06003E6B RID: 15979 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void WriteLine(string value)
			{
			}

			// Token: 0x06003E6C RID: 15980 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void WriteLine(object value)
			{
			}

			// Token: 0x06003E6D RID: 15981 RVA: 0x00002C89 File Offset: 0x00000E89
			public override void Write(char value)
			{
			}
		}

		// Token: 0x020007B2 RID: 1970
		[Serializable]
		internal sealed class SyncTextWriter : TextWriter, IDisposable
		{
			// Token: 0x06003E6E RID: 15982 RVA: 0x000F12F7 File Offset: 0x000EF4F7
			internal SyncTextWriter(TextWriter t)
				: base(t.FormatProvider)
			{
				this._out = t;
			}

			// Token: 0x17000A07 RID: 2567
			// (get) Token: 0x06003E6F RID: 15983 RVA: 0x000F130C File Offset: 0x000EF50C
			public override Encoding Encoding
			{
				get
				{
					return this._out.Encoding;
				}
			}

			// Token: 0x17000A08 RID: 2568
			// (get) Token: 0x06003E70 RID: 15984 RVA: 0x000F1319 File Offset: 0x000EF519
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this._out.FormatProvider;
				}
			}

			// Token: 0x17000A09 RID: 2569
			// (get) Token: 0x06003E71 RID: 15985 RVA: 0x000F1326 File Offset: 0x000EF526
			public override string NewLine
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._out.NewLine;
				}
			}

			// Token: 0x06003E72 RID: 15986 RVA: 0x000F1333 File Offset: 0x000EF533
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._out.Close();
			}

			// Token: 0x06003E73 RID: 15987 RVA: 0x000F1340 File Offset: 0x000EF540
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._out).Dispose();
				}
			}

			// Token: 0x06003E74 RID: 15988 RVA: 0x000F1350 File Offset: 0x000EF550
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Flush()
			{
				this._out.Flush();
			}

			// Token: 0x06003E75 RID: 15989 RVA: 0x000F135D File Offset: 0x000EF55D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E76 RID: 15990 RVA: 0x000F136B File Offset: 0x000EF56B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x06003E77 RID: 15991 RVA: 0x000F1379 File Offset: 0x000EF579
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer, int index, int count)
			{
				this._out.Write(buffer, index, count);
			}

			// Token: 0x06003E78 RID: 15992 RVA: 0x000F1389 File Offset: 0x000EF589
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(bool value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E79 RID: 15993 RVA: 0x000F1397 File Offset: 0x000EF597
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(int value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E7A RID: 15994 RVA: 0x000F13A5 File Offset: 0x000EF5A5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(uint value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E7B RID: 15995 RVA: 0x000F13B3 File Offset: 0x000EF5B3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(long value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E7C RID: 15996 RVA: 0x000F13C1 File Offset: 0x000EF5C1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ulong value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E7D RID: 15997 RVA: 0x000F13CF File Offset: 0x000EF5CF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(float value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E7E RID: 15998 RVA: 0x000F13DD File Offset: 0x000EF5DD
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(double value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E7F RID: 15999 RVA: 0x000F13EB File Offset: 0x000EF5EB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(decimal value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E80 RID: 16000 RVA: 0x000F13F9 File Offset: 0x000EF5F9
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string value)
			{
				this._out.Write(value);
			}

			// Token: 0x06003E81 RID: 16001 RVA: 0x000F1407 File Offset: 0x000EF607
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0)
			{
				this._out.Write(format, arg0);
			}

			// Token: 0x06003E82 RID: 16002 RVA: 0x000F1416 File Offset: 0x000EF616
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this._out.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x06003E83 RID: 16003 RVA: 0x000F1428 File Offset: 0x000EF628
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine()
			{
				this._out.WriteLine();
			}

			// Token: 0x06003E84 RID: 16004 RVA: 0x000F1435 File Offset: 0x000EF635
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E85 RID: 16005 RVA: 0x000F1443 File Offset: 0x000EF643
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(decimal value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E86 RID: 16006 RVA: 0x000F1451 File Offset: 0x000EF651
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x06003E87 RID: 16007 RVA: 0x000F145F File Offset: 0x000EF65F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this._out.WriteLine(buffer, index, count);
			}

			// Token: 0x06003E88 RID: 16008 RVA: 0x000F146F File Offset: 0x000EF66F
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(bool value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E89 RID: 16009 RVA: 0x000F147D File Offset: 0x000EF67D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(int value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E8A RID: 16010 RVA: 0x000F148B File Offset: 0x000EF68B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(uint value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E8B RID: 16011 RVA: 0x000F1499 File Offset: 0x000EF699
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(long value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E8C RID: 16012 RVA: 0x000F14A7 File Offset: 0x000EF6A7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ulong value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E8D RID: 16013 RVA: 0x000F14B5 File Offset: 0x000EF6B5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(float value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E8E RID: 16014 RVA: 0x000F14C3 File Offset: 0x000EF6C3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(double value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E8F RID: 16015 RVA: 0x000F14D1 File Offset: 0x000EF6D1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E90 RID: 16016 RVA: 0x000F14DF File Offset: 0x000EF6DF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(object value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x06003E91 RID: 16017 RVA: 0x000F14ED File Offset: 0x000EF6ED
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0)
			{
				this._out.WriteLine(format, arg0);
			}

			// Token: 0x06003E92 RID: 16018 RVA: 0x000F14FC File Offset: 0x000EF6FC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this._out.WriteLine(format, arg0, arg1);
			}

			// Token: 0x06003E93 RID: 16019 RVA: 0x000F150C File Offset: 0x000EF70C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this._out.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x06003E94 RID: 16020 RVA: 0x000F151E File Offset: 0x000EF71E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, params object[] arg)
			{
				this._out.WriteLine(format, arg);
			}

			// Token: 0x06003E95 RID: 16021 RVA: 0x000F152D File Offset: 0x000EF72D
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06003E96 RID: 16022 RVA: 0x000F153B File Offset: 0x000EF73B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(string value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06003E97 RID: 16023 RVA: 0x000F1549 File Offset: 0x000EF749
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				this.Write(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06003E98 RID: 16024 RVA: 0x000F1559 File Offset: 0x000EF759
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task FlushAsync()
			{
				this.Flush();
				return Task.CompletedTask;
			}

			// Token: 0x04002015 RID: 8213
			private readonly TextWriter _out;
		}
	}
}
