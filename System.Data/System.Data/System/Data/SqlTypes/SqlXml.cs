using System;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents XML data stored in or retrieved from a server.</summary>
	// Token: 0x020000D4 RID: 212
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlXml : INullable, IXmlSerializable
	{
		/// <summary>Creates a new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance.</summary>
		// Token: 0x06000B17 RID: 2839 RVA: 0x0003E2AC File Offset: 0x0003C4AC
		public SqlXml()
		{
			this.SetNull();
		}

		/// <summary>Gets the value of the XML content of this <see cref="T:System.Data.SqlTypes.SqlXml" /> as a <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlReader" />-derived instance that contains the XML content. The actual type may vary (for example, the return value might be <see cref="T:System.Xml.XmlTextReader" />) depending on how the information is represented internally, on the server.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">Attempt was made to access this property on a null instance of <see cref="T:System.Data.SqlTypes.SqlXml" />.</exception>
		// Token: 0x06000B18 RID: 2840 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		public XmlReader CreateReader()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			SqlXmlStreamWrapper sqlXmlStreamWrapper = new SqlXmlStreamWrapper(this._stream);
			if ((!this._firstCreateReader || sqlXmlStreamWrapper.CanSeek) && sqlXmlStreamWrapper.Position != 0L)
			{
				sqlXmlStreamWrapper.Seek(0L, SeekOrigin.Begin);
			}
			if (this._createSqlReaderMethodInfo == null)
			{
				this._createSqlReaderMethodInfo = SqlXml.CreateSqlReaderMethodInfo;
			}
			XmlReader xmlReader = SqlXml.CreateSqlXmlReader(sqlXmlStreamWrapper, false, false);
			this._firstCreateReader = false;
			return xmlReader;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0003E330 File Offset: 0x0003C530
		internal static XmlReader CreateSqlXmlReader(Stream stream, bool closeInput = false, bool throwTargetInvocationExceptions = false)
		{
			XmlReaderSettings xmlReaderSettings = (closeInput ? SqlXml.s_defaultXmlReaderSettingsCloseInput : SqlXml.s_defaultXmlReaderSettings);
			XmlReader xmlReader;
			try
			{
				xmlReader = SqlXml.s_sqlReaderDelegate(stream, xmlReaderSettings, null);
			}
			catch (Exception ex)
			{
				if (!throwTargetInvocationExceptions || !ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw new TargetInvocationException(ex);
			}
			return xmlReader;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0003E384 File Offset: 0x0003C584
		private static Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader> CreateSqlReaderDelegate()
		{
			return (Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader>)SqlXml.CreateSqlReaderMethodInfo.CreateDelegate(typeof(Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader>));
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0003E39F File Offset: 0x0003C59F
		private static MethodInfo CreateSqlReaderMethodInfo
		{
			get
			{
				if (SqlXml.s_createSqlReaderMethodInfo == null)
				{
					SqlXml.s_createSqlReaderMethodInfo = typeof(XmlReader).GetMethod("CreateSqlReader", BindingFlags.Static | BindingFlags.NonPublic);
				}
				return SqlXml.s_createSqlReaderMethodInfo;
			}
		}

		/// <summary>Indicates whether this instance represents a null <see cref="T:System.Data.SqlTypes.SqlXml" /> value.</summary>
		/// <returns>true if Value is null. Otherwise, false.</returns>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0003E3CE File Offset: 0x0003C5CE
		public bool IsNull
		{
			get
			{
				return !this._fNotNull;
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0003E3D9 File Offset: 0x0003C5D9
		private void SetNull()
		{
			this._fNotNull = false;
			this._stream = null;
			this._firstCreateReader = true;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.</returns>
		// Token: 0x06000B1E RID: 2846 RVA: 0x00011F10 File Offset: 0x00010110
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />.</summary>
		/// <param name="r">An XmlReader.</param>
		// Token: 0x06000B1F RID: 2847 RVA: 0x0003E3F0 File Offset: 0x0003C5F0
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadInnerXml();
				this.SetNull();
				return;
			}
			this._fNotNull = true;
			this._firstCreateReader = true;
			this._stream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(this._stream);
			streamWriter.Write(r.ReadInnerXml());
			streamWriter.Flush();
			if (this._stream.CanSeek)
			{
				this._stream.Seek(0L, SeekOrigin.Begin);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />.</summary>
		/// <param name="writer">An XmlWriter</param>
		// Token: 0x06000B20 RID: 2848 RVA: 0x0003E478 File Offset: 0x0003C678
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			}
			else
			{
				XmlReader xmlReader = this.CreateReader();
				if (xmlReader.ReadState == ReadState.Initial)
				{
					xmlReader.Read();
				}
				while (!xmlReader.EOF)
				{
					writer.WriteNode(xmlReader, true);
				}
			}
			writer.Flush();
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />. </returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000B21 RID: 2849 RVA: 0x0003E4D7 File Offset: 0x0003C6D7
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000484 RID: 1156
		private static readonly Func<Stream, XmlReaderSettings, XmlParserContext, XmlReader> s_sqlReaderDelegate = SqlXml.CreateSqlReaderDelegate();

		// Token: 0x04000485 RID: 1157
		private static readonly XmlReaderSettings s_defaultXmlReaderSettings = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};

		// Token: 0x04000486 RID: 1158
		private static readonly XmlReaderSettings s_defaultXmlReaderSettingsCloseInput = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment,
			CloseInput = true
		};

		// Token: 0x04000487 RID: 1159
		private static MethodInfo s_createSqlReaderMethodInfo;

		// Token: 0x04000488 RID: 1160
		private MethodInfo _createSqlReaderMethodInfo;

		// Token: 0x04000489 RID: 1161
		private bool _fNotNull;

		// Token: 0x0400048A RID: 1162
		private Stream _stream;

		// Token: 0x0400048B RID: 1163
		private bool _firstCreateReader;
	}
}
