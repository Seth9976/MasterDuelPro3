using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Text
{
	/// <summary>Represents a character encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000306 RID: 774
	[ComVisible(true)]
	[Serializable]
	public abstract class Encoding : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class.</summary>
		// Token: 0x06001BBD RID: 7101 RVA: 0x0006CB16 File Offset: 0x0006AD16
		protected Encoding()
			: this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class that corresponds to the specified code page.</summary>
		/// <param name="codePage">The code page identifier of the preferred encoding.-or- 0, to use the default encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codePage" /> is less than zero. </exception>
		// Token: 0x06001BBE RID: 7102 RVA: 0x0006CB1F File Offset: 0x0006AD1F
		protected Encoding(int codePage)
		{
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.SetDefaultFallbacks();
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0006CB4A File Offset: 0x0006AD4A
		internal virtual void SetDefaultFallbacks()
		{
			this.encoderFallback = new InternalEncoderBestFitFallback(this);
			this.decoderFallback = new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0006CB64 File Offset: 0x0006AD64
		internal void OnDeserializing()
		{
			this.encoderFallback = null;
			this.decoderFallback = null;
			this.m_isReadOnly = true;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0006CB7B File Offset: 0x0006AD7B
		internal void OnDeserialized()
		{
			if (this.encoderFallback == null || this.decoderFallback == null)
			{
				this.m_deserializedFromEverett = true;
				this.SetDefaultFallbacks();
			}
			this.dataItem = null;
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0006CBA1 File Offset: 0x0006ADA1
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.OnDeserializing();
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0006CBA9 File Offset: 0x0006ADA9
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0006CBB1 File Offset: 0x0006ADB1
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.dataItem = null;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0006CBBC File Offset: 0x0006ADBC
		internal void DeserializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			this.dataItem = null;
			try
			{
				this.m_isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.m_deserializedFromEverett = true;
				this.m_isReadOnly = true;
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0006CC88 File Offset: 0x0006AE88
		internal void SerializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_isReadOnly", this.m_isReadOnly);
			info.AddValue("encoderFallback", this.EncoderFallback);
			info.AddValue("decoderFallback", this.DecoderFallback);
			info.AddValue("m_codePage", this.m_codePage);
			info.AddValue("dataItem", null);
			info.AddValue("Encoding+m_codePage", this.m_codePage);
			info.AddValue("Encoding+dataItem", null);
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0006CD10 File Offset: 0x0006AF10
		private static object InternalSyncObject
		{
			get
			{
				if (Encoding.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref Encoding.s_InternalSyncObject, obj, null);
				}
				return Encoding.s_InternalSyncObject;
			}
		}

		/// <summary>Returns the encoding associated with the specified code page identifier.</summary>
		/// <returns>The encoding that is associated with the specified code page.</returns>
		/// <param name="codepage">The code page identifier of the preferred encoding. Possible values are listed in the Code Page column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.-or- 0 (zero), to use the default encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codepage" /> is less than zero or greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BC8 RID: 7112 RVA: 0x0006CD3C File Offset: 0x0006AF3C
		public static Encoding GetEncoding(int codepage)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage);
			if (encoding != null)
			{
				return encoding;
			}
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException("codepage", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 0, 65535 }));
			}
			if (Encoding.encodings != null)
			{
				Encoding.encodings.TryGetValue(codepage, out encoding);
			}
			if (encoding == null)
			{
				object internalSyncObject = Encoding.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (Encoding.encodings == null)
					{
						Encoding.encodings = new Dictionary<int, Encoding>();
					}
					if (Encoding.encodings.TryGetValue(codepage, out encoding))
					{
						return encoding;
					}
					if (codepage <= 1201)
					{
						if (codepage <= 3)
						{
							if (codepage == 0)
							{
								encoding = Encoding.Default;
								goto IL_0233;
							}
							if (codepage - 1 > 2)
							{
								goto IL_01B0;
							}
						}
						else if (codepage != 42)
						{
							if (codepage == 1200)
							{
								encoding = Encoding.Unicode;
								goto IL_0233;
							}
							if (codepage != 1201)
							{
								goto IL_01B0;
							}
							encoding = Encoding.BigEndianUnicode;
							goto IL_0233;
						}
						throw new ArgumentException(Environment.GetResourceString("{0} is not a supported code page.", new object[] { codepage }), "codepage");
					}
					if (codepage <= 20127)
					{
						if (codepage == 12000)
						{
							encoding = Encoding.UTF32;
							goto IL_0233;
						}
						if (codepage == 12001)
						{
							encoding = new UTF32Encoding(true, true);
							goto IL_0233;
						}
						if (codepage == 20127)
						{
							encoding = Encoding.ASCII;
							goto IL_0233;
						}
					}
					else
					{
						if (codepage == 28591)
						{
							encoding = Encoding.Latin1;
							goto IL_0233;
						}
						if (codepage == 65000)
						{
							encoding = Encoding.UTF7;
							goto IL_0233;
						}
						if (codepage == 65001)
						{
							encoding = Encoding.UTF8;
							goto IL_0233;
						}
					}
					IL_01B0:
					if (EncodingTable.GetCodePageDataItem(codepage) == null)
					{
						throw new NotSupportedException(Environment.GetResourceString("No data is available for encoding {0}. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.", new object[] { codepage }));
					}
					if (codepage != 12000)
					{
						if (codepage != 12001)
						{
							encoding = (Encoding)EncodingHelper.InvokeI18N("GetEncoding", new object[] { codepage });
							if (encoding == null)
							{
								throw new NotSupportedException(string.Format("Encoding {0} data could not be found. Make sure you have correct international codeset assembly installed and enabled.", codepage));
							}
						}
						else
						{
							encoding = new UTF32Encoding(true, true);
						}
					}
					else
					{
						encoding = Encoding.UTF32;
					}
					IL_0233:
					Encoding.encodings.Add(codepage, encoding);
				}
				return encoding;
			}
			return encoding;
		}

		/// <summary>Returns the encoding associated with the specified code page identifier. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <returns>The encoding that is associated with the specified code page.</returns>
		/// <param name="codepage">The code page identifier of the preferred encoding. Possible values are listed in the Code Page column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.-or- 0 (zero), to use the default encoding. </param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with the current encoding. </param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codepage" /> is less than zero or greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BC9 RID: 7113 RVA: 0x0006CFB8 File Offset: 0x0006B1B8
		public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage, encoderFallback, decoderFallback);
			if (encoding != null)
			{
				return encoding;
			}
			encoding = Encoding.GetEncoding(codepage);
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = encoderFallback;
			encoding2.DecoderFallback = decoderFallback;
			return encoding2;
		}

		/// <summary>Returns the encoding associated with the specified code page name.</summary>
		/// <returns>The encoding  associated with the specified code page.</returns>
		/// <param name="name">The code page name of the preferred encoding. Any value returned by the <see cref="P:System.Text.Encoding.WebName" /> property is valid. Possible values are listed in the Name column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid code page name.-or- The code page indicated by <paramref name="name" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BCA RID: 7114 RVA: 0x0006CFF4 File Offset: 0x0006B1F4
		public static Encoding GetEncoding(string name)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name));
		}

		/// <summary>When overridden in a derived class, returns a sequence of bytes that specifies the encoding used.</summary>
		/// <returns>A byte array containing a sequence of bytes that specifies the encoding used.-or- A byte array of length zero, if a preamble is not required.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BCB RID: 7115 RVA: 0x0006D018 File Offset: 0x0006B218
		public virtual byte[] GetPreamble()
		{
			return EmptyArray<byte>.Value;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0006D01F File Offset: 0x0006B21F
		public virtual ReadOnlySpan<byte> Preamble
		{
			get
			{
				return this.GetPreamble();
			}
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0006D02C File Offset: 0x0006B22C
		private void GetDataItem()
		{
			if (this.dataItem == null)
			{
				this.dataItem = EncodingTable.GetCodePageDataItem(this.m_codePage);
				if (this.dataItem == null)
				{
					throw new NotSupportedException(Environment.GetResourceString("No data is available for encoding {0}. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.", new object[] { this.m_codePage }));
				}
			}
		}

		/// <summary>When overridden in a derived class, gets the human-readable description of the current encoding.</summary>
		/// <returns>The human-readable description of the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0006D07E File Offset: 0x0006B27E
		public virtual string EncodingName
		{
			get
			{
				return Environment.GetResourceStringEncodingName(this.m_codePage);
			}
		}

		/// <summary>When overridden in a derived class, gets the name registered with the Internet Assigned Numbers Authority (IANA) for the current encoding.</summary>
		/// <returns>The IANA name for the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x0006D08B File Offset: 0x0006B28B
		public virtual string WebName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.WebName;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.EncoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>The encoder fallback object for the current <see cref="T:System.Text.Encoding" /> object. </returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A value cannot be assigned in a set operation because the current <see cref="T:System.Text.Encoding" /> object is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x0006D0A6 File Offset: 0x0006B2A6
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x0006D0AE File Offset: 0x0006B2AE
		[ComVisible(false)]
		public EncoderFallback EncoderFallback
		{
			get
			{
				return this.encoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoderFallback = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.DecoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>The decoder fallback object for the current <see cref="T:System.Text.Encoding" /> object. </returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A value cannot be assigned in a set operation because the current <see cref="T:System.Text.Encoding" /> object is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x0006D0DD File Offset: 0x0006B2DD
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x0006D0E5 File Offset: 0x0006B2E5
		[ComVisible(false)]
		public DecoderFallback DecoderFallback
		{
			get
			{
				return this.decoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.decoderFallback = value;
			}
		}

		/// <summary>When overridden in a derived class, creates a shallow copy of the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>A copy of the current <see cref="T:System.Text.Encoding" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BD4 RID: 7124 RVA: 0x0006D114 File Offset: 0x0006B314
		[ComVisible(false)]
		public virtual object Clone()
		{
			Encoding encoding = (Encoding)base.MemberwiseClone();
			encoding.m_isReadOnly = false;
			return encoding;
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> is read-only; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0006D128 File Offset: 0x0006B328
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Gets an encoding for the ASCII (7-bit) character set.</summary>
		/// <returns>An  encoding for the ASCII (7-bit) character set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0006D130 File Offset: 0x0006B330
		public static Encoding ASCII
		{
			get
			{
				if (Encoding.asciiEncoding == null)
				{
					Encoding.asciiEncoding = new ASCIIEncoding();
				}
				return Encoding.asciiEncoding;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x0006D14E File Offset: 0x0006B34E
		private static Encoding Latin1
		{
			get
			{
				if (Encoding.latin1Encoding == null)
				{
					Encoding.latin1Encoding = new Latin1Encoding();
				}
				return Encoding.latin1Encoding;
			}
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding the characters in the specified string.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="s">The string containing the set of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BD8 RID: 7128 RVA: 0x0006D16C File Offset: 0x0006B36C
		public virtual int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char[] array = s.ToCharArray();
			return this.GetByteCount(array, 0, array.Length);
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters from the specified character array.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="index">The index of the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BD9 RID: 7129
		public abstract int GetByteCount(char[] chars, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters starting at the specified character pointer.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BDA RID: 7130 RVA: 0x0006D19C File Offset: 0x0006B39C
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe virtual int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0006D202 File Offset: 0x0006B402
		internal unsafe virtual int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			return this.GetByteCount(chars, count);
		}

		/// <summary>When overridden in a derived class, encodes all the characters in the specified character array into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="chars">The character array containing the characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BDC RID: 7132 RVA: 0x0006D20C File Offset: 0x0006B40C
		public virtual byte[] GetBytes(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetBytes(chars, 0, chars.Length);
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="index">The index of the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BDD RID: 7133 RVA: 0x0006D234 File Offset: 0x0006B434
		public virtual byte[] GetBytes(char[] chars, int index, int count)
		{
			byte[] array = new byte[this.GetByteCount(chars, index, count)];
			this.GetBytes(chars, index, count, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array into the specified byte array.</summary>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="charIndex">The index of the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes. </param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BDE RID: 7134
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

		/// <summary>When overridden in a derived class, encodes all the characters in the specified string into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="s">The string containing the characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BDF RID: 7135 RVA: 0x0006D260 File Offset: 0x0006B460
		public virtual byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			byte[] array = new byte[this.GetByteCount(s)];
			this.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified string into the specified byte array.</summary>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <param name="s">The string containing the set of characters to encode. </param>
		/// <param name="charIndex">The index of the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes. </param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BE0 RID: 7136 RVA: 0x0006D2A4 File Offset: 0x0006B4A4
		public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return this.GetBytes(s.ToCharArray(), charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0006D2C6 File Offset: 0x0006B4C6
		internal unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			return this.GetBytes(chars, charCount, bytes, byteCount);
		}

		/// <summary>When overridden in a derived class, encodes a set of characters starting at the specified character pointer into a sequence of bytes that are stored starting at the specified byte pointer.</summary>
		/// <returns>The actual number of bytes written at the location indicated by the <paramref name="bytes" /> parameter.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">A pointer to the location at which to start writing the resulting sequence of bytes. </param>
		/// <param name="byteCount">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="byteCount" /> is less than the resulting number of bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BE2 RID: 7138 RVA: 0x0006D2D4 File Offset: 0x0006B4D4
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BE3 RID: 7139
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes starting at the specified byte pointer.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">A pointer to the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BE4 RID: 7140 RVA: 0x0006D384 File Offset: 0x0006B584
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0006D3E7 File Offset: 0x0006B5E7
		internal unsafe virtual int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
		{
			return this.GetCharCount(bytes, count);
		}

		/// <summary>When overridden in a derived class, decodes all the bytes in the specified byte array into a set of characters.</summary>
		/// <returns>A character array containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BE6 RID: 7142 RVA: 0x0006D3F1 File Offset: 0x0006B5F1
		public virtual char[] GetChars(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetChars(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into a set of characters.</summary>
		/// <returns>A character array containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BE7 RID: 7143 RVA: 0x0006D418 File Offset: 0x0006B618
		public virtual char[] GetChars(byte[] bytes, int index, int count)
		{
			char[] array = new char[this.GetCharCount(bytes, index, count)];
			this.GetChars(bytes, index, count, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into the specified character array.</summary>
		/// <returns>The actual number of characters written into <paramref name="chars" />.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="byteIndex">The index of the first byte to decode. </param>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <param name="chars">The character array to contain the resulting set of characters. </param>
		/// <param name="charIndex">The index at which to start writing the resulting set of characters. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null.-or- <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteIndex" /> or <paramref name="byteCount" /> or <paramref name="charIndex" /> is less than zero.-or- <paramref name="byteindex" /> and <paramref name="byteCount" /> do not denote a valid range in <paramref name="bytes" />.-or- <paramref name="charIndex" /> is not a valid index in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BE8 RID: 7144
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		/// <summary>When overridden in a derived class, decodes a sequence of bytes starting at the specified byte pointer into a set of characters that are stored starting at the specified character pointer.</summary>
		/// <returns>The actual number of characters written at the location indicated by the <paramref name="chars" /> parameter.</returns>
		/// <param name="bytes">A pointer to the first byte to decode. </param>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <param name="chars">A pointer to the location at which to start writing the resulting set of characters. </param>
		/// <param name="charCount">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null.-or- <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> or <paramref name="charCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="charCount" /> is less than the resulting number of characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BE9 RID: 7145 RVA: 0x0006D444 File Offset: 0x0006B644
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("Non-negative number required."));
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0006D4F4 File Offset: 0x0006B6F4
		internal unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
		{
			return this.GetChars(bytes, byteCount, chars, charCount);
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0006D501 File Offset: 0x0006B701
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe string GetString(byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			return string.CreateStringFromEncoding(bytes, byteCount, this);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0006D540 File Offset: 0x0006B740
		public unsafe string GetString(ReadOnlySpan<byte> bytes)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* ptr = nonNullPinnableReference;
				return this.GetString(ptr, bytes.Length);
			}
		}

		/// <summary>When overridden in a derived class, gets the code page identifier of the current <see cref="T:System.Text.Encoding" />.</summary>
		/// <returns>The code page identifier of the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0006D565 File Offset: 0x0006B765
		public virtual int CodePage
		{
			get
			{
				return this.m_codePage;
			}
		}

		/// <summary>When overridden in a derived class, obtains a decoder that converts an encoded sequence of bytes into a sequence of characters.</summary>
		/// <returns>A <see cref="T:System.Text.Decoder" /> that converts an encoded sequence of bytes into a sequence of characters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BEE RID: 7150 RVA: 0x0006D56D File Offset: 0x0006B76D
		public virtual Decoder GetDecoder()
		{
			return new Encoding.DefaultDecoder(this);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0006D575 File Offset: 0x0006B775
		private static Encoding CreateDefaultEncoding()
		{
			Encoding encoding = EncodingHelper.GetDefaultEncoding();
			encoding.m_isReadOnly = true;
			return encoding;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0006D583 File Offset: 0x0006B783
		internal void setReadOnly(bool value = true)
		{
			this.m_isReadOnly = value;
		}

		/// <summary>Gets an encoding for the operating system's current ANSI code page.</summary>
		/// <returns>An encoding for the operating system's current ANSI code page.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0006D58C File Offset: 0x0006B78C
		public static Encoding Default
		{
			get
			{
				if (Encoding.defaultEncoding == null)
				{
					Encoding.defaultEncoding = Encoding.CreateDefaultEncoding();
				}
				return Encoding.defaultEncoding;
			}
		}

		/// <summary>When overridden in a derived class, obtains an encoder that converts a sequence of Unicode characters into an encoded sequence of bytes.</summary>
		/// <returns>A <see cref="T:System.Text.Encoder" /> that converts a sequence of Unicode characters into an encoded sequence of bytes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BF2 RID: 7154 RVA: 0x0006D5AA File Offset: 0x0006B7AA
		public virtual Encoder GetEncoder()
		{
			return new Encoding.DefaultEncoder(this);
		}

		/// <summary>When overridden in a derived class, calculates the maximum number of bytes produced by encoding the specified number of characters.</summary>
		/// <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BF3 RID: 7155
		public abstract int GetMaxByteCount(int charCount);

		/// <summary>When overridden in a derived class, calculates the maximum number of characters produced by decoding the specified number of bytes.</summary>
		/// <returns>The maximum number of characters produced by decoding the specified number of bytes.</returns>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BF4 RID: 7156
		public abstract int GetMaxCharCount(int byteCount);

		/// <summary>When overridden in a derived class, decodes all the bytes in the specified byte array into a string.</summary>
		/// <returns>A string that contains the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentException">The byte array contains invalid Unicode code points.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BF5 RID: 7157 RVA: 0x0006D5B2 File Offset: 0x0006B7B2
		public virtual string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into a string.</summary>
		/// <returns>A string that contains the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentException">The byte array contains invalid Unicode code points.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BF6 RID: 7158 RVA: 0x0006D5D7 File Offset: 0x0006B7D7
		public virtual string GetString(byte[] bytes, int index, int count)
		{
			return new string(this.GetChars(bytes, index, count));
		}

		/// <summary>Gets an encoding for the UTF-16 format using the little endian byte order.</summary>
		/// <returns>An encoding for the UTF-16 format using the little endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0006D5E7 File Offset: 0x0006B7E7
		public static Encoding Unicode
		{
			get
			{
				if (Encoding.unicodeEncoding == null)
				{
					Encoding.unicodeEncoding = new UnicodeEncoding(false, true);
				}
				return Encoding.unicodeEncoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-16 format that uses the big endian byte order.</summary>
		/// <returns>An encoding object for the UTF-16 format that uses the big endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0006D607 File Offset: 0x0006B807
		public static Encoding BigEndianUnicode
		{
			get
			{
				if (Encoding.bigEndianUnicode == null)
				{
					Encoding.bigEndianUnicode = new UnicodeEncoding(true, true);
				}
				return Encoding.bigEndianUnicode;
			}
		}

		/// <summary>Gets an encoding for the UTF-7 format.</summary>
		/// <returns>An encoding for the UTF-7 format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x0006D627 File Offset: 0x0006B827
		public static Encoding UTF7
		{
			get
			{
				if (Encoding.utf7Encoding == null)
				{
					Encoding.utf7Encoding = new UTF7Encoding();
				}
				return Encoding.utf7Encoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-8 format.</summary>
		/// <returns>An encoding for the UTF-8 format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0006D645 File Offset: 0x0006B845
		public static Encoding UTF8
		{
			get
			{
				if (Encoding.utf8Encoding == null)
				{
					Encoding.utf8Encoding = new UTF8Encoding(true);
				}
				return Encoding.utf8Encoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-32 format using the little endian byte order.</summary>
		/// <returns>An  encoding object for the UTF-32 format using the little endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0006D664 File Offset: 0x0006B864
		public static Encoding UTF32
		{
			get
			{
				if (Encoding.utf32Encoding == null)
				{
					Encoding.utf32Encoding = new UTF32Encoding(false, true);
				}
				return Encoding.utf32Encoding;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current instance.</summary>
		/// <returns>true if <paramref name="value" /> is an instance of <see cref="T:System.Text.Encoding" /> and is equal to the current instance; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BFC RID: 7164 RVA: 0x0006D684 File Offset: 0x0006B884
		public override bool Equals(object value)
		{
			Encoding encoding = value as Encoding;
			return encoding != null && (this.m_codePage == encoding.m_codePage && this.EncoderFallback.Equals(encoding.EncoderFallback)) && this.DecoderFallback.Equals(encoding.DecoderFallback);
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>The hash code for the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BFD RID: 7165 RVA: 0x0006D6D1 File Offset: 0x0006B8D1
		public override int GetHashCode()
		{
			return this.m_codePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0006D6F1 File Offset: 0x0006B8F1
		internal virtual char[] GetBestFitUnicodeToBytesData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0006D6F1 File Offset: 0x0006B8F1
		internal virtual char[] GetBestFitBytesToUnicodeData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x0006D6F8 File Offset: 0x0006B8F8
		internal void ThrowBytesOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("The output byte buffer is too small to contain the encoded data, encoding '{0}' fallback '{1}'.", new object[]
			{
				this.EncodingName,
				this.EncoderFallback.GetType()
			}), "bytes");
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0006D72B File Offset: 0x0006B92B
		internal void ThrowBytesOverflow(EncoderNLS encoder, bool nothingEncoded)
		{
			if (encoder == null || encoder._throwOnOverflow || nothingEncoded)
			{
				if (encoder != null && encoder.InternalHasFallbackBuffer)
				{
					encoder.FallbackBuffer.InternalReset();
				}
				this.ThrowBytesOverflow();
			}
			encoder.ClearMustFlush();
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0006D75F File Offset: 0x0006B95F
		internal void ThrowCharsOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("The output char buffer is too small to contain the decoded characters, encoding '{0}' fallback '{1}'.", new object[]
			{
				this.EncodingName,
				this.DecoderFallback.GetType()
			}), "chars");
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0006D792 File Offset: 0x0006B992
		internal void ThrowCharsOverflow(DecoderNLS decoder, bool nothingDecoded)
		{
			if (decoder == null || decoder._throwOnOverflow || nothingDecoded)
			{
				if (decoder != null && decoder.InternalHasFallbackBuffer)
				{
					decoder.FallbackBuffer.InternalReset();
				}
				this.ThrowCharsOverflow();
			}
			decoder.ClearMustFlush();
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0006D7C8 File Offset: 0x0006B9C8
		public unsafe virtual int GetByteCount(ReadOnlySpan<char> chars)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* ptr = nonNullPinnableReference;
				return this.GetByteCount(ptr, chars.Length);
			}
		}

		// Token: 0x04000CC7 RID: 3271
		private static volatile Encoding defaultEncoding;

		// Token: 0x04000CC8 RID: 3272
		private static volatile Encoding unicodeEncoding;

		// Token: 0x04000CC9 RID: 3273
		private static volatile Encoding bigEndianUnicode;

		// Token: 0x04000CCA RID: 3274
		private static volatile Encoding utf7Encoding;

		// Token: 0x04000CCB RID: 3275
		private static volatile Encoding utf8Encoding;

		// Token: 0x04000CCC RID: 3276
		private static volatile Encoding utf32Encoding;

		// Token: 0x04000CCD RID: 3277
		private static volatile Encoding asciiEncoding;

		// Token: 0x04000CCE RID: 3278
		private static volatile Encoding latin1Encoding;

		// Token: 0x04000CCF RID: 3279
		private static volatile Dictionary<int, Encoding> encodings;

		// Token: 0x04000CD0 RID: 3280
		internal int m_codePage;

		// Token: 0x04000CD1 RID: 3281
		internal CodePageDataItem dataItem;

		// Token: 0x04000CD2 RID: 3282
		[NonSerialized]
		internal bool m_deserializedFromEverett;

		// Token: 0x04000CD3 RID: 3283
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly = true;

		// Token: 0x04000CD4 RID: 3284
		[OptionalField(VersionAdded = 2)]
		internal EncoderFallback encoderFallback;

		// Token: 0x04000CD5 RID: 3285
		[OptionalField(VersionAdded = 2)]
		internal DecoderFallback decoderFallback;

		// Token: 0x04000CD6 RID: 3286
		private static object s_InternalSyncObject;

		// Token: 0x02000307 RID: 775
		[Serializable]
		internal class DefaultEncoder : Encoder, ISerializable, IObjectReference
		{
			// Token: 0x06001C05 RID: 7173 RVA: 0x0006D7ED File Offset: 0x0006B9ED
			public DefaultEncoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x06001C06 RID: 7174 RVA: 0x0006D804 File Offset: 0x0006BA04
			internal DefaultEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this._fallback = (EncoderFallback)info.GetValue("_fallback", typeof(EncoderFallback));
					this.charLeftOver = (char)info.GetValue("charLeftOver", typeof(char));
				}
				catch (SerializationException)
				{
				}
			}

			// Token: 0x06001C07 RID: 7175 RVA: 0x0006D89C File Offset: 0x0006BA9C
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Encoder encoder = this.m_encoding.GetEncoder();
				if (this._fallback != null)
				{
					encoder._fallback = this._fallback;
				}
				if (this.charLeftOver != '\0')
				{
					EncoderNLS encoderNLS = encoder as EncoderNLS;
					if (encoderNLS != null)
					{
						encoderNLS._charLeftOver = this.charLeftOver;
					}
				}
				return encoder;
			}

			// Token: 0x06001C08 RID: 7176 RVA: 0x0006D8F2 File Offset: 0x0006BAF2
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x06001C09 RID: 7177 RVA: 0x0006D913 File Offset: 0x0006BB13
			public override int GetByteCount(char[] chars, int index, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, index, count);
			}

			// Token: 0x06001C0A RID: 7178 RVA: 0x0006D923 File Offset: 0x0006BB23
			public unsafe override int GetByteCount(char* chars, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, count);
			}

			// Token: 0x06001C0B RID: 7179 RVA: 0x0006D932 File Offset: 0x0006BB32
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
			}

			// Token: 0x06001C0C RID: 7180 RVA: 0x0006D946 File Offset: 0x0006BB46
			public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount);
			}

			// Token: 0x04000CD7 RID: 3287
			private Encoding m_encoding;

			// Token: 0x04000CD8 RID: 3288
			[NonSerialized]
			private bool m_hasInitializedEncoding;

			// Token: 0x04000CD9 RID: 3289
			[NonSerialized]
			internal char charLeftOver;
		}

		// Token: 0x02000308 RID: 776
		[Serializable]
		internal class DefaultDecoder : Decoder, ISerializable, IObjectReference
		{
			// Token: 0x06001C0D RID: 7181 RVA: 0x0006D958 File Offset: 0x0006BB58
			public DefaultDecoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x06001C0E RID: 7182 RVA: 0x0006D970 File Offset: 0x0006BB70
			internal DefaultDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this._fallback = (DecoderFallback)info.GetValue("_fallback", typeof(DecoderFallback));
				}
				catch (SerializationException)
				{
					this._fallback = null;
				}
			}

			// Token: 0x06001C0F RID: 7183 RVA: 0x0006D9F0 File Offset: 0x0006BBF0
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Decoder decoder = this.m_encoding.GetDecoder();
				if (this._fallback != null)
				{
					decoder._fallback = this._fallback;
				}
				return decoder;
			}

			// Token: 0x06001C10 RID: 7184 RVA: 0x0006DA28 File Offset: 0x0006BC28
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x06001C11 RID: 7185 RVA: 0x00063551 File Offset: 0x00061751
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return this.GetCharCount(bytes, index, count, false);
			}

			// Token: 0x06001C12 RID: 7186 RVA: 0x0006DA49 File Offset: 0x0006BC49
			public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, index, count);
			}

			// Token: 0x06001C13 RID: 7187 RVA: 0x0006DA59 File Offset: 0x0006BC59
			public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, count);
			}

			// Token: 0x06001C14 RID: 7188 RVA: 0x0006362A File Offset: 0x0006182A
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
			}

			// Token: 0x06001C15 RID: 7189 RVA: 0x0006DA68 File Offset: 0x0006BC68
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			// Token: 0x06001C16 RID: 7190 RVA: 0x0006DA7C File Offset: 0x0006BC7C
			public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteCount, chars, charCount);
			}

			// Token: 0x04000CDA RID: 3290
			private Encoding m_encoding;

			// Token: 0x04000CDB RID: 3291
			[NonSerialized]
			private bool m_hasInitializedEncoding;
		}

		// Token: 0x02000309 RID: 777
		internal class EncodingCharBuffer
		{
			// Token: 0x06001C17 RID: 7191 RVA: 0x0006DA90 File Offset: 0x0006BC90
			internal unsafe EncodingCharBuffer(Encoding enc, DecoderNLS decoder, char* charStart, int charCount, byte* byteStart, int byteCount)
			{
				this.enc = enc;
				this.decoder = decoder;
				this.chars = charStart;
				this.charStart = charStart;
				this.charEnd = charStart + charCount;
				this.byteStart = byteStart;
				this.bytes = byteStart;
				this.byteEnd = byteStart + byteCount;
				if (this.decoder == null)
				{
					this.fallbackBuffer = enc.DecoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.decoder.FallbackBuffer;
				}
				this.fallbackBuffer.InternalInitialize(this.bytes, this.charEnd);
			}

			// Token: 0x06001C18 RID: 7192 RVA: 0x0006DB2C File Offset: 0x0006BD2C
			internal unsafe bool AddChar(char ch, int numBytes)
			{
				if (this.chars != null)
				{
					if (this.chars >= this.charEnd)
					{
						this.bytes -= numBytes;
						this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
						return false;
					}
					char* ptr = this.chars;
					this.chars = ptr + 1;
					*ptr = ch;
				}
				this.charCountResult++;
				return true;
			}

			// Token: 0x06001C19 RID: 7193 RVA: 0x0006DBA5 File Offset: 0x0006BDA5
			internal bool AddChar(char ch)
			{
				return this.AddChar(ch, 1);
			}

			// Token: 0x06001C1A RID: 7194 RVA: 0x0006DBAF File Offset: 0x0006BDAF
			internal void AdjustBytes(int count)
			{
				this.bytes += count;
			}

			// Token: 0x17000308 RID: 776
			// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0006DBBF File Offset: 0x0006BDBF
			internal bool MoreData
			{
				get
				{
					return this.bytes < this.byteEnd;
				}
			}

			// Token: 0x06001C1C RID: 7196 RVA: 0x0006DBD0 File Offset: 0x0006BDD0
			internal unsafe byte GetNextByte()
			{
				if (this.bytes >= this.byteEnd)
				{
					return 0;
				}
				byte* ptr = this.bytes;
				this.bytes = ptr + 1;
				return *ptr;
			}

			// Token: 0x17000309 RID: 777
			// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0006DBFF File Offset: 0x0006BDFF
			internal int BytesUsed
			{
				get
				{
					return (int)((long)(this.bytes - this.byteStart));
				}
			}

			// Token: 0x06001C1E RID: 7198 RVA: 0x0006DC14 File Offset: 0x0006BE14
			internal bool Fallback(byte fallbackByte)
			{
				byte[] array = new byte[] { fallbackByte };
				return this.Fallback(array);
			}

			// Token: 0x06001C1F RID: 7199 RVA: 0x0006DC34 File Offset: 0x0006BE34
			internal unsafe bool Fallback(byte[] byteBuffer)
			{
				if (this.chars != null)
				{
					char* ptr = this.chars;
					if (!this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes, ref this.chars))
					{
						this.bytes -= byteBuffer.Length;
						this.fallbackBuffer.InternalReset();
						this.enc.ThrowCharsOverflow(this.decoder, this.chars == this.charStart);
						return false;
					}
					this.charCountResult += (int)((long)(this.chars - ptr));
				}
				else
				{
					this.charCountResult += this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes);
				}
				return true;
			}

			// Token: 0x1700030A RID: 778
			// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0006DCE3 File Offset: 0x0006BEE3
			internal int Count
			{
				get
				{
					return this.charCountResult;
				}
			}

			// Token: 0x04000CDC RID: 3292
			private unsafe char* chars;

			// Token: 0x04000CDD RID: 3293
			private unsafe char* charStart;

			// Token: 0x04000CDE RID: 3294
			private unsafe char* charEnd;

			// Token: 0x04000CDF RID: 3295
			private int charCountResult;

			// Token: 0x04000CE0 RID: 3296
			private Encoding enc;

			// Token: 0x04000CE1 RID: 3297
			private DecoderNLS decoder;

			// Token: 0x04000CE2 RID: 3298
			private unsafe byte* byteStart;

			// Token: 0x04000CE3 RID: 3299
			private unsafe byte* byteEnd;

			// Token: 0x04000CE4 RID: 3300
			private unsafe byte* bytes;

			// Token: 0x04000CE5 RID: 3301
			private DecoderFallbackBuffer fallbackBuffer;
		}

		// Token: 0x0200030A RID: 778
		internal class EncodingByteBuffer
		{
			// Token: 0x06001C21 RID: 7201 RVA: 0x0006DCEC File Offset: 0x0006BEEC
			internal unsafe EncodingByteBuffer(Encoding inEncoding, EncoderNLS inEncoder, byte* inByteStart, int inByteCount, char* inCharStart, int inCharCount)
			{
				this.enc = inEncoding;
				this.encoder = inEncoder;
				this.charStart = inCharStart;
				this.chars = inCharStart;
				this.charEnd = inCharStart + inCharCount;
				this.bytes = inByteStart;
				this.byteStart = inByteStart;
				this.byteEnd = inByteStart + inByteCount;
				if (this.encoder == null)
				{
					this.fallbackBuffer = this.enc.EncoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.encoder.FallbackBuffer;
					if (this.encoder._throwOnOverflow && this.encoder.InternalHasFallbackBuffer && this.fallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.", new object[]
						{
							this.encoder.Encoding.EncodingName,
							this.encoder.Fallback.GetType()
						}));
					}
				}
				this.fallbackBuffer.InternalInitialize(this.chars, this.charEnd, this.encoder, this.bytes != null);
			}

			// Token: 0x06001C22 RID: 7202 RVA: 0x0006DE04 File Offset: 0x0006C004
			internal unsafe bool AddByte(byte b, int moreBytesExpected)
			{
				if (this.bytes != null)
				{
					if (this.bytes >= this.byteEnd - moreBytesExpected)
					{
						this.MovePrevious(true);
						return false;
					}
					byte* ptr = this.bytes;
					this.bytes = ptr + 1;
					*ptr = b;
				}
				this.byteCountResult++;
				return true;
			}

			// Token: 0x06001C23 RID: 7203 RVA: 0x0006DE56 File Offset: 0x0006C056
			internal bool AddByte(byte b1)
			{
				return this.AddByte(b1, 0);
			}

			// Token: 0x06001C24 RID: 7204 RVA: 0x0006DE60 File Offset: 0x0006C060
			internal bool AddByte(byte b1, byte b2)
			{
				return this.AddByte(b1, b2, 0);
			}

			// Token: 0x06001C25 RID: 7205 RVA: 0x0006DE6B File Offset: 0x0006C06B
			internal bool AddByte(byte b1, byte b2, int moreBytesExpected)
			{
				return this.AddByte(b1, 1 + moreBytesExpected) && this.AddByte(b2, moreBytesExpected);
			}

			// Token: 0x06001C26 RID: 7206 RVA: 0x0006DE84 File Offset: 0x0006C084
			internal void MovePrevious(bool bThrow)
			{
				if (this.fallbackBuffer.bFallingBack)
				{
					this.fallbackBuffer.MovePrevious();
				}
				else if (this.chars != this.charStart)
				{
					this.chars--;
				}
				if (bThrow)
				{
					this.enc.ThrowBytesOverflow(this.encoder, this.bytes == this.byteStart);
				}
			}

			// Token: 0x1700030B RID: 779
			// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0006DEEA File Offset: 0x0006C0EA
			internal bool MoreData
			{
				get
				{
					return this.fallbackBuffer.Remaining > 0 || this.chars < this.charEnd;
				}
			}

			// Token: 0x06001C28 RID: 7208 RVA: 0x0006DF0C File Offset: 0x0006C10C
			internal unsafe char GetNextChar()
			{
				char c = this.fallbackBuffer.InternalGetNextChar();
				if (c == '\0' && this.chars < this.charEnd)
				{
					char* ptr = this.chars;
					this.chars = ptr + 1;
					c = *ptr;
				}
				return c;
			}

			// Token: 0x1700030C RID: 780
			// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0006DF4A File Offset: 0x0006C14A
			internal int CharsUsed
			{
				get
				{
					return (int)((long)(this.chars - this.charStart));
				}
			}

			// Token: 0x1700030D RID: 781
			// (get) Token: 0x06001C2A RID: 7210 RVA: 0x0006DF5D File Offset: 0x0006C15D
			internal int Count
			{
				get
				{
					return this.byteCountResult;
				}
			}

			// Token: 0x04000CE6 RID: 3302
			private unsafe byte* bytes;

			// Token: 0x04000CE7 RID: 3303
			private unsafe byte* byteStart;

			// Token: 0x04000CE8 RID: 3304
			private unsafe byte* byteEnd;

			// Token: 0x04000CE9 RID: 3305
			private unsafe char* chars;

			// Token: 0x04000CEA RID: 3306
			private unsafe char* charStart;

			// Token: 0x04000CEB RID: 3307
			private unsafe char* charEnd;

			// Token: 0x04000CEC RID: 3308
			private int byteCountResult;

			// Token: 0x04000CED RID: 3309
			private Encoding enc;

			// Token: 0x04000CEE RID: 3310
			private EncoderNLS encoder;

			// Token: 0x04000CEF RID: 3311
			internal EncoderFallbackBuffer fallbackBuffer;
		}
	}
}
