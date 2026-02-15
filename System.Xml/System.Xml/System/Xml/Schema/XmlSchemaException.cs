using System;
using System.Resources;
using System.Runtime.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Returns detailed information about the schema exception.</summary>
	// Token: 0x020002CF RID: 719
	[Serializable]
	public class XmlSchemaException : SystemException
	{
		/// <summary>Constructs a new XmlSchemaException object with the given SerializationInfo and StreamingContext information that contains all the properties of the XmlSchemaException.</summary>
		/// <param name="info">SerializationInfo.</param>
		/// <param name="context">StreamingContext.</param>
		// Token: 0x06002104 RID: 8452 RVA: 0x000BFDBC File Offset: 0x000BDFBC
		protected XmlSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			this.sourceUri = (string)info.GetValue("sourceUri", typeof(string));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "version")
				{
					text = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XmlSchemaException.CreateMessage(this.res, this.args);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the XmlSchemaException properties into the SerializationInfo class for the given StreamingContext.</summary>
		/// <param name="info">The SerializationInfo. </param>
		/// <param name="context">The StreamingContext information. </param>
		// Token: 0x06002105 RID: 8453 RVA: 0x000BFED0 File Offset: 0x000BE0D0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class.</summary>
		// Token: 0x06002106 RID: 8454 RVA: 0x000A6863 File Offset: 0x000A4A63
		public XmlSchemaException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		// Token: 0x06002107 RID: 8455 RVA: 0x000BFF4A File Offset: 0x000BE14A
		public XmlSchemaException(string message)
			: this(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message and original <see cref="T:System.Exception" /> object that caused this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original T:System.Exception object that caused this exception.</param>
		// Token: 0x06002108 RID: 8456 RVA: 0x000BFF56 File Offset: 0x000BE156
		public XmlSchemaException(string message, Exception innerException)
			: this(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message specified, and the original <see cref="T:System.Exception" /> object, line number, and line position of the XML that cause this exception specified.</summary>
		/// <param name="message">A string description of the error condition.</param>
		/// <param name="innerException">The original T:System.Exception object that caused this exception.</param>
		/// <param name="lineNumber">The line number of the XML that caused this exception.</param>
		/// <param name="linePosition">The line position of the XML that caused this exception.</param>
		// Token: 0x06002109 RID: 8457 RVA: 0x000BFF62 File Offset: 0x000BE162
		public XmlSchemaException(string message, Exception innerException, int lineNumber, int linePosition)
			: this((message == null) ? "A schema error occurred." : "{0}", new string[] { message }, innerException, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000BFF89 File Offset: 0x000BE189
		internal XmlSchemaException(string res, string[] args)
			: this(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000A686C File Offset: 0x000A4A6C
		internal XmlSchemaException(string res, string arg)
			: this(res, new string[] { arg }, null, null, 0, 0, null)
		{
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000BFF98 File Offset: 0x000BE198
		internal XmlSchemaException(string res, string arg, string sourceUri, int lineNumber, int linePosition)
			: this(res, new string[] { arg }, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000BFFB2 File Offset: 0x000BE1B2
		internal XmlSchemaException(string res, string sourceUri, int lineNumber, int linePosition)
			: this(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000BFFC2 File Offset: 0x000BE1C2
		internal XmlSchemaException(string res, string[] args, string sourceUri, int lineNumber, int linePosition)
			: this(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000BFFD3 File Offset: 0x000BE1D3
		internal XmlSchemaException(string res, XmlSchemaObject source)
			: this(res, null, source)
		{
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000BFFDE File Offset: 0x000BE1DE
		internal XmlSchemaException(string res, string arg, XmlSchemaObject source)
			: this(res, new string[] { arg }, source)
		{
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000BFFF2 File Offset: 0x000BE1F2
		internal XmlSchemaException(string res, string[] args, XmlSchemaObject source)
			: this(res, args, null, source.SourceUri, source.LineNumber, source.LinePosition, source)
		{
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000C0010 File Offset: 0x000BE210
		internal XmlSchemaException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject source)
			: base(XmlSchemaException.CreateMessage(res, args), innerException)
		{
			base.HResult = -2146231999;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
			this.sourceSchemaObject = source;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000C0064 File Offset: 0x000BE264
		internal static string CreateMessage(string res, string[] args)
		{
			string text;
			try
			{
				text = Res.GetString(res, args);
			}
			catch (MissingManifestResourceException)
			{
				text = "UNKNOWN(" + res + ")";
			}
			return text;
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x000C00A4 File Offset: 0x000BE2A4
		internal string GetRes
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000C00AC File Offset: 0x000BE2AC
		internal string[] Args
		{
			get
			{
				return this.args;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) location of the schema that caused the exception.</summary>
		/// <returns>The URI location of the schema that caused the exception.</returns>
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000C00B4 File Offset: 0x000BE2B4
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Gets the line number indicating where the error occurred.</summary>
		/// <returns>The line number indicating where the error occurred.</returns>
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000C00BC File Offset: 0x000BE2BC
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred.</summary>
		/// <returns>The line position indicating where the error occurred.</returns>
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x000C00C4 File Offset: 0x000BE2C4
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>The XmlSchemaObject that produced the XmlSchemaException.</summary>
		/// <returns>A valid object instance represents a structural validation error in the XML Schema Object Model (SOM).</returns>
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000C00CC File Offset: 0x000BE2CC
		public XmlSchemaObject SourceSchemaObject
		{
			get
			{
				return this.sourceSchemaObject;
			}
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000C00D4 File Offset: 0x000BE2D4
		internal void SetSource(string sourceUri, int lineNumber, int linePosition)
		{
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000C00EB File Offset: 0x000BE2EB
		internal void SetSchemaObject(XmlSchemaObject source)
		{
			this.sourceSchemaObject = source;
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x000C00F4 File Offset: 0x000BE2F4
		internal void SetSource(XmlSchemaObject source)
		{
			this.sourceSchemaObject = source;
			this.sourceUri = source.SourceUri;
			this.lineNumber = source.LineNumber;
			this.linePosition = source.LinePosition;
		}

		/// <summary>Gets the description of the error condition of this exception.</summary>
		/// <returns>The description of the error condition of this exception.</returns>
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x000C0121 File Offset: 0x000BE321
		public override string Message
		{
			get
			{
				if (this.message != null)
				{
					return this.message;
				}
				return base.Message;
			}
		}

		// Token: 0x04000F69 RID: 3945
		private string res;

		// Token: 0x04000F6A RID: 3946
		private string[] args;

		// Token: 0x04000F6B RID: 3947
		private string sourceUri;

		// Token: 0x04000F6C RID: 3948
		private int lineNumber;

		// Token: 0x04000F6D RID: 3949
		private int linePosition;

		// Token: 0x04000F6E RID: 3950
		[NonSerialized]
		private XmlSchemaObject sourceSchemaObject;

		// Token: 0x04000F6F RID: 3951
		private string message;
	}
}
