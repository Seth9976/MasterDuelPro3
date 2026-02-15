using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a mutable reference type that wraps either a <see cref="P:System.Data.SqlTypes.SqlBytes.Buffer" /> or a <see cref="P:System.Data.SqlTypes.SqlBytes.Stream" />.</summary>
	// Token: 0x020000C2 RID: 194
	[DefaultMember("Item")]
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlBytes : INullable, IXmlSerializable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class.</summary>
		// Token: 0x06000988 RID: 2440 RVA: 0x00038453 File Offset: 0x00036653
		public SqlBytes()
		{
			this.SetNull();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes. </param>
		// Token: 0x06000989 RID: 2441 RVA: 0x00038464 File Offset: 0x00036664
		public SqlBytes(byte[] buffer)
		{
			this._rgbBuf = buffer;
			this._stream = null;
			if (this._rgbBuf == null)
			{
				this._state = SqlBytesCharsState.Null;
				this._lCurLen = -1L;
			}
			else
			{
				this._state = SqlBytesCharsState.Buffer;
				this._lCurLen = (long)this._rgbBuf.Length;
			}
			this._rgbWorkBuf = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</param>
		// Token: 0x0600098A RID: 2442 RVA: 0x000384BB File Offset: 0x000366BB
		public SqlBytes(SqlBinary value)
			: this(value.IsNull ? null : value.Value)
		{
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlBytes" /> is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBytes" /> is null, false otherwise.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000384D6 File Offset: 0x000366D6
		public bool IsNull
		{
			get
			{
				return this._state == SqlBytesCharsState.Null;
			}
		}

		/// <summary>Returns a reference to the internal buffer. </summary>
		/// <returns>Returns a reference to the internal buffer. For <see cref="T:System.Data.SqlTypes.SqlBytes" /> instances created on top of unmanaged pointers, it returns a managed copy of the internal buffer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000384E1 File Offset: 0x000366E1
		public byte[] Buffer
		{
			get
			{
				if (this.FStream())
				{
					this.CopyStreamToBuffer();
				}
				return this._rgbBuf;
			}
		}

		/// <summary>Gets the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value representing the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance. Returns -1 if no buffer is available to the instance or if the value is null. Returns a <see cref="P:System.IO.Stream.Length" /> for a stream-wrapped instance.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000384F8 File Offset: 0x000366F8
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

		/// <summary>Returns a managed copy of the value held by this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> as an array of bytes.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00038530 File Offset: 0x00036730
		public byte[] Value
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state != SqlBytesCharsState.Null)
				{
					byte[] array;
					if (state != SqlBytesCharsState.Stream)
					{
						array = new byte[this._lCurLen];
						Array.Copy(this._rgbBuf, 0, array, 0, (int)this._lCurLen);
					}
					else
					{
						if (this._stream.Length > 2147483647L)
						{
							throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
						}
						array = new byte[this._stream.Length];
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

		/// <summary>Sets this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance to null.</summary>
		// Token: 0x0600098F RID: 2447 RVA: 0x000385DE File Offset: 0x000367DE
		public void SetNull()
		{
			this._lCurLen = -1L;
			this._stream = null;
			this._state = SqlBytesCharsState.Null;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x000385F8 File Offset: 0x000367F8
		private void CopyStreamToBuffer()
		{
			long length = this._stream.Length;
			if (length >= 2147483647L)
			{
				throw new SqlTypeException("Cannot write from an offset that is larger than current length. It would leave uninitialized data in the buffer.");
			}
			if (this._rgbBuf == null || (long)this._rgbBuf.Length < length)
			{
				this._rgbBuf = new byte[length];
			}
			if (this._stream.Position != 0L)
			{
				this._stream.Seek(0L, SeekOrigin.Begin);
			}
			this._stream.Read(this._rgbBuf, 0, (int)length);
			this._stream = null;
			this._lCurLen = length;
			this._state = SqlBytesCharsState.Buffer;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0003868C File Offset: 0x0003688C
		internal bool FStream()
		{
			return this._state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00038697 File Offset: 0x00036897
		private void SetBuffer(byte[] buffer)
		{
			this._rgbBuf = buffer;
			this._lCurLen = ((this._rgbBuf == null) ? (-1L) : ((long)this._rgbBuf.Length));
			this._stream = null;
			this._state = ((this._rgbBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06000993 RID: 2451 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="r">XmlReader</param>
		// Token: 0x06000994 RID: 2452 RVA: 0x000386D4 File Offset: 0x000368D4
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			byte[] array = null;
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadElementString();
				this.SetNull();
			}
			else
			{
				string text = r.ReadElementString();
				if (text == null)
				{
					array = Array.Empty<byte>();
				}
				else
				{
					text = text.Trim();
					if (text.Length == 0)
					{
						array = Array.Empty<byte>();
					}
					else
					{
						array = Convert.FromBase64String(text);
					}
				}
			}
			this.SetBuffer(array);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000995 RID: 2453 RVA: 0x00038748 File Offset: 0x00036948
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			byte[] buffer = this.Buffer;
			writer.WriteString(Convert.ToBase64String(buffer, 0, (int)this.Length));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string that indicates the XSD of the specified XmlSchemaSet.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000996 RID: 2454 RVA: 0x00037C43 File Offset: 0x00035E43
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Gets serialization information with all the data needed to reinstantiate this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06000997 RID: 2455 RVA: 0x00027AED File Offset: 0x00025CED
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Returns a null instance of this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>Returns an instance in such a way that <see cref="P:System.Data.SqlTypes.SqlBytes.IsNull" /> returns true.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00038793 File Offset: 0x00036993
		public static SqlBytes Null
		{
			get
			{
				return new SqlBytes(null);
			}
		}

		// Token: 0x040003CE RID: 974
		internal byte[] _rgbBuf;

		// Token: 0x040003CF RID: 975
		private long _lCurLen;

		// Token: 0x040003D0 RID: 976
		internal Stream _stream;

		// Token: 0x040003D1 RID: 977
		private SqlBytesCharsState _state;

		// Token: 0x040003D2 RID: 978
		private byte[] _rgbWorkBuf;
	}
}
