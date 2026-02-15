using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>
	///   <see cref="T:System.Data.SqlTypes.SqlChars" /> is a mutable reference type that wraps a <see cref="T:System.Char" /> array or a <see cref="T:System.Data.SqlTypes.SqlString" /> instance.</summary>
	// Token: 0x020000C3 RID: 195
	[DefaultMember("Item")]
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlChars : INullable, IXmlSerializable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class.</summary>
		// Token: 0x06000999 RID: 2457 RVA: 0x0003879B File Offset: 0x0003699B
		public SqlChars()
		{
			this.SetNull();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class based on the specified character array.</summary>
		/// <param name="buffer">A <see cref="T:System.Char" /> array.</param>
		// Token: 0x0600099A RID: 2458 RVA: 0x000387AC File Offset: 0x000369AC
		public SqlChars(char[] buffer)
		{
			this._rgchBuf = buffer;
			this._stream = null;
			if (this._rgchBuf == null)
			{
				this._state = SqlBytesCharsState.Null;
				this._lCurLen = -1L;
			}
			else
			{
				this._state = SqlBytesCharsState.Buffer;
				this._lCurLen = (long)this._rgchBuf.Length;
			}
			this._rgchWorkBuf = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class based on the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlString" />.</param>
		// Token: 0x0600099B RID: 2459 RVA: 0x00038803 File Offset: 0x00036A03
		public SqlChars(SqlString value)
			: this(value.IsNull ? null : value.Value.ToCharArray())
		{
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlChars" /> is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlChars" /> is null. Otherwise, false. </returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00038823 File Offset: 0x00036A23
		public bool IsNull
		{
			get
			{
				return this._state == SqlBytesCharsState.Null;
			}
		}

		/// <summary>Returns a reference to the internal buffer. </summary>
		/// <returns>Returns a reference to the internal buffer. For <see cref="T:System.Data.SqlTypes.SqlChars" /> instances created on top of unmanaged pointers, it returns a managed copy of the internal buffer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0003882E File Offset: 0x00036A2E
		public char[] Buffer
		{
			get
			{
				if (this.FStream())
				{
					this.CopyStreamToBuffer();
				}
				return this._rgchBuf;
			}
		}

		/// <summary>Gets the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value that indicates the length in characters of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.Returns -1 if no buffer is available to the instance, or if the value is null. Returns a <see cref="P:System.IO.Stream.Length" /> for a stream-wrapped instance.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00038844 File Offset: 0x00036A44
		public long Length
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state == SqlBytesCharsState.Null)
				{
					throw new SqlNullValueException();
				}
				if (state != SqlBytesCharsState.Stream)
				{
					return this._lCurLen;
				}
				return this._stream.Length;
			}
		}

		/// <summary>Returns a managed copy of the value held by this <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlChars" /> as an array of characters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0003887C File Offset: 0x00036A7C
		public char[] Value
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state != SqlBytesCharsState.Null)
				{
					char[] array;
					if (state != SqlBytesCharsState.Stream)
					{
						array = new char[this._lCurLen];
						Array.Copy(this._rgchBuf, 0, array, 0, (int)this._lCurLen);
					}
					else
					{
						if (this._stream.Length > 2147483647L)
						{
							throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
						}
						array = new char[this._stream.Length];
						if (this._stream.Position != 0L)
						{
							this._stream.Seek(0L, SeekOrigin.Begin);
						}
						this._stream.Read(array, 0, checked((int)this._stream.Length));
					}
					return array;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Sets this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to null.</summary>
		// Token: 0x060009A0 RID: 2464 RVA: 0x0003892A File Offset: 0x00036B2A
		public void SetNull()
		{
			this._lCurLen = -1L;
			this._stream = null;
			this._state = SqlBytesCharsState.Null;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00038942 File Offset: 0x00036B42
		internal bool FStream()
		{
			return this._state == SqlBytesCharsState.Stream;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00038950 File Offset: 0x00036B50
		private void CopyStreamToBuffer()
		{
			long length = this._stream.Length;
			if (length >= 2147483647L)
			{
				throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
			}
			if (this._rgchBuf == null || (long)this._rgchBuf.Length < length)
			{
				this._rgchBuf = new char[length];
			}
			if (this._stream.Position != 0L)
			{
				this._stream.Seek(0L, SeekOrigin.Begin);
			}
			this._stream.Read(this._rgchBuf, 0, (int)length);
			this._stream = null;
			this._lCurLen = length;
			this._state = SqlBytesCharsState.Buffer;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000389E4 File Offset: 0x00036BE4
		private void SetBuffer(char[] buffer)
		{
			this._rgchBuf = buffer;
			this._lCurLen = ((this._rgchBuf == null) ? (-1L) : ((long)this._rgchBuf.Length));
			this._stream = null;
			this._state = ((this._rgchBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x060009A4 RID: 2468 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="r">XmlReader</param>
		// Token: 0x060009A5 RID: 2469 RVA: 0x00038A24 File Offset: 0x00036C24
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadElementString();
				this.SetNull();
				return;
			}
			char[] array = r.ReadElementString().ToCharArray();
			this.SetBuffer(array);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x060009A6 RID: 2470 RVA: 0x00038A70 File Offset: 0x00036C70
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			char[] buffer = this.Buffer;
			writer.WriteString(new string(buffer, 0, (int)this.Length));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x060009A7 RID: 2471 RVA: 0x00038ABB File Offset: 0x00036CBB
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Gets serialization information with all the data needed to reinstantiate this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x060009A8 RID: 2472 RVA: 0x00027AED File Offset: 0x00025CED
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Returns a null instance of this <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>Returns an instance in such a way that <see cref="P:System.Data.SqlTypes.SqlChars.IsNull" /> returns true. For more information, see Handling Null Values (ADO.NET).</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00038ACC File Offset: 0x00036CCC
		public static SqlChars Null
		{
			get
			{
				return new SqlChars(null);
			}
		}

		// Token: 0x040003D3 RID: 979
		internal char[] _rgchBuf;

		// Token: 0x040003D4 RID: 980
		private long _lCurLen;

		// Token: 0x040003D5 RID: 981
		internal SqlStreamChars _stream;

		// Token: 0x040003D6 RID: 982
		private SqlBytesCharsState _state;

		// Token: 0x040003D7 RID: 983
		private char[] _rgchWorkBuf;
	}
}
