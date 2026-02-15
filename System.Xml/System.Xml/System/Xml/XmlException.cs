using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;

namespace System.Xml
{
	/// <summary>Returns detailed information about the last exception.</summary>
	// Token: 0x02000128 RID: 296
	[Serializable]
	public class XmlException : SystemException
	{
		/// <summary>Initializes a new instance of the XmlException class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The SerializationInfo object containing all the properties of an XmlException. </param>
		/// <param name="context">The StreamingContext object containing the context information. </param>
		// Token: 0x06000F03 RID: 3843 RVA: 0x0004BB2C File Offset: 0x00049D2C
		protected XmlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
			this.sourceUri = string.Empty;
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (!(name == "sourceUri"))
				{
					if (name == "version")
					{
						text = (string)serializationEntry.Value;
					}
				}
				else
				{
					this.sourceUri = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XmlException.CreateMessage(this.res, this.args, this.lineNumber, this.linePosition);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the XmlException properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The SerializationInfo object. </param>
		/// <param name="context">The StreamingContext object. </param>
		// Token: 0x06000F04 RID: 3844 RVA: 0x0004BC5C File Offset: 0x00049E5C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the XmlException class.</summary>
		// Token: 0x06000F05 RID: 3845 RVA: 0x0004BCD6 File Offset: 0x00049ED6
		public XmlException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the XmlException class with a specified error message.</summary>
		/// <param name="message">The error description. </param>
		// Token: 0x06000F06 RID: 3846 RVA: 0x0004BCDF File Offset: 0x00049EDF
		public XmlException(string message)
			: this(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the XmlException class.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that threw the XmlException, if any. This value can be null. </param>
		// Token: 0x06000F07 RID: 3847 RVA: 0x0004BCEB File Offset: 0x00049EEB
		public XmlException(string message, Exception innerException)
			: this(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the XmlException class with the specified message, inner exception, line number, and line position.</summary>
		/// <param name="message">The error description. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. This value can be null. </param>
		/// <param name="lineNumber">The line number indicating where the error occurred. </param>
		/// <param name="linePosition">The line position indicating where the error occurred. </param>
		// Token: 0x06000F08 RID: 3848 RVA: 0x0004BCF7 File Offset: 0x00049EF7
		public XmlException(string message, Exception innerException, int lineNumber, int linePosition)
			: this(message, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0004BD08 File Offset: 0x00049F08
		internal XmlException(string message, Exception innerException, int lineNumber, int linePosition, string sourceUri)
			: base(XmlException.FormatUserMessage(message, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = ((message == null) ? "An XML error has occurred." : "{0}");
			this.args = new string[] { message };
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0004BD6C File Offset: 0x00049F6C
		internal XmlException(string res, string[] args)
			: this(res, args, null, 0, 0, null)
		{
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0004BD7A File Offset: 0x00049F7A
		internal XmlException(string res, string arg)
			: this(res, new string[] { arg }, null, 0, 0, null)
		{
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0004BD91 File Offset: 0x00049F91
		internal XmlException(string res, string arg, string sourceUri)
			: this(res, new string[] { arg }, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0004BDA8 File Offset: 0x00049FA8
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo)
			: this(res, new string[] { arg }, lineInfo, null)
		{
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0004BDBD File Offset: 0x00049FBD
		internal XmlException(string res, string arg, Exception innerException, IXmlLineInfo lineInfo)
			: this(res, new string[] { arg }, innerException, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, null)
		{
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0004BDEE File Offset: 0x00049FEE
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo)
			: this(res, args, lineInfo, null)
		{
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0004BDFA File Offset: 0x00049FFA
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo, string sourceUri)
			: this(res, args, null, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, sourceUri)
		{
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0004BE1F File Offset: 0x0004A01F
		internal XmlException(string res, string arg, int lineNumber, int linePosition)
			: this(res, new string[] { arg }, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0004BE37 File Offset: 0x0004A037
		internal XmlException(string res, string arg, int lineNumber, int linePosition, string sourceUri)
			: this(res, new string[] { arg }, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0004BE50 File Offset: 0x0004A050
		internal XmlException(string res, string[] args, int lineNumber, int linePosition)
			: this(res, args, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0004BE5F File Offset: 0x0004A05F
		internal XmlException(string res, string[] args, int lineNumber, int linePosition, string sourceUri)
			: this(res, args, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0004BE6F File Offset: 0x0004A06F
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition)
			: this(res, args, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0004BE80 File Offset: 0x0004A080
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition, string sourceUri)
			: base(XmlException.CreateMessage(res, args, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0004BED0 File Offset: 0x0004A0D0
		private static string FormatUserMessage(string message, int lineNumber, int linePosition)
		{
			if (message == null)
			{
				return XmlException.CreateMessage("An XML error has occurred.", null, lineNumber, linePosition);
			}
			if (lineNumber == 0 && linePosition == 0)
			{
				return message;
			}
			return XmlException.CreateMessage("{0}", new string[] { message }, lineNumber, linePosition);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0004BF04 File Offset: 0x0004A104
		private static string CreateMessage(string res, string[] args, int lineNumber, int linePosition)
		{
			string text5;
			try
			{
				string text;
				if (lineNumber == 0)
				{
					text = Res.GetString(res, args);
				}
				else
				{
					string text2 = lineNumber.ToString(CultureInfo.InvariantCulture);
					string text3 = linePosition.ToString(CultureInfo.InvariantCulture);
					text = Res.GetString(res, args);
					string text4 = "{0} Line {1}, position {2}.";
					object[] array = new string[] { text, text2, text3 };
					text = Res.GetString(text4, array);
				}
				text5 = text;
			}
			catch (MissingManifestResourceException)
			{
				text5 = "UNKNOWN(" + res + ")";
			}
			return text5;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0004BF90 File Offset: 0x0004A190
		internal static string[] BuildCharExceptionArgs(string data, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data[invCharIndex], (invCharIndex + 1 < data.Length) ? data[invCharIndex + 1] : '\0');
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0004BFB5 File Offset: 0x0004A1B5
		internal static string[] BuildCharExceptionArgs(char[] data, int length, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data[invCharIndex], (invCharIndex + 1 < length) ? data[invCharIndex + 1] : '\0');
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0004BFD0 File Offset: 0x0004A1D0
		internal static string[] BuildCharExceptionArgs(char invChar, char nextChar)
		{
			string[] array = new string[2];
			if (XmlCharType.IsHighSurrogate((int)invChar) && nextChar != '\0')
			{
				int num = XmlCharType.CombineSurrogateChar((int)nextChar, (int)invChar);
				array[0] = new string(new char[] { invChar, nextChar });
				array[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", num);
			}
			else
			{
				if (invChar == '\0')
				{
					array[0] = ".";
				}
				else
				{
					array[0] = invChar.ToString(CultureInfo.InvariantCulture);
				}
				array[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", (int)invChar);
			}
			return array;
		}

		/// <summary>Gets the line number indicating where the error occurred.</summary>
		/// <returns>The line number indicating where the error occurred.</returns>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0004C05C File Offset: 0x0004A25C
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred.</summary>
		/// <returns>The line position indicating where the error occurred.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0004C064 File Offset: 0x0004A264
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets a message describing the current exception.</summary>
		/// <returns>The error message that explains the reason for the exception.</returns>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0004C06C File Offset: 0x0004A26C
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

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0004C083 File Offset: 0x0004A283
		internal string ResString
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x04000749 RID: 1865
		private string res;

		// Token: 0x0400074A RID: 1866
		private string[] args;

		// Token: 0x0400074B RID: 1867
		private int lineNumber;

		// Token: 0x0400074C RID: 1868
		private int linePosition;

		// Token: 0x0400074D RID: 1869
		[OptionalField]
		private string sourceUri;

		// Token: 0x0400074E RID: 1870
		private string message;
	}
}
