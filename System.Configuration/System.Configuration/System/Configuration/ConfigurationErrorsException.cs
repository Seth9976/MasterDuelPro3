using System;
using System.Configuration.Internal;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Configuration
{
	/// <summary>The current value is not one of the <see cref="P:System.Web.Configuration.PagesSection.EnableSessionState" /> values.</summary>
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class ConfigurationErrorsException : ConfigurationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		// Token: 0x060000A0 RID: 160 RVA: 0x000046D8 File Offset: 0x000028D8
		public ConfigurationErrorsException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x060000A1 RID: 161 RVA: 0x000046E0 File Offset: 0x000028E0
		public ConfigurationErrorsException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type is not a <see cref="T:System.Configuration.ConfigurationException" /> or a <see cref="T:System.Configuration.ConfigurationErrorsException" />.</exception>
		// Token: 0x060000A2 RID: 162 RVA: 0x000046E9 File Offset: 0x000028E9
		protected ConfigurationErrorsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.filename = info.GetString("ConfigurationErrors_Filename");
			this.line = info.GetInt32("ConfigurationErrors_Line");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x060000A3 RID: 163 RVA: 0x00004715 File Offset: 0x00002915
		public ConfigurationErrorsException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x060000A4 RID: 164 RVA: 0x0000471F File Offset: 0x0000291F
		public ConfigurationErrorsException(string message, XmlNode node)
			: this(message, null, ConfigurationErrorsException.GetFilename(node), ConfigurationErrorsException.GetLineNumber(node))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x060000A5 RID: 165 RVA: 0x00004735 File Offset: 0x00002935
		public ConfigurationErrorsException(string message, XmlReader reader)
			: this(message, null, ConfigurationErrorsException.GetFilename(reader), ConfigurationErrorsException.GetLineNumber(reader))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x060000A6 RID: 166 RVA: 0x0000474B File Offset: 0x0000294B
		public ConfigurationErrorsException(string message, string filename, int line)
			: this(message, null, filename, line)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Configuration.ConfigurationErrorsException" /> class.</summary>
		/// <param name="message">A message that describes why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="filename">The path to the configuration file that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		/// <param name="line">The line number within the configuration file at which this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</param>
		// Token: 0x060000A7 RID: 167 RVA: 0x00004757 File Offset: 0x00002957
		public ConfigurationErrorsException(string message, Exception inner, string filename, int line)
			: base(message, inner)
		{
			this.filename = filename;
			this.line = line;
		}

		/// <summary>Gets a description of why this configuration exception was thrown.</summary>
		/// <returns>A description of why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> was thrown.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004770 File Offset: 0x00002970
		public override string BareMessage
		{
			get
			{
				return base.BareMessage;
			}
		}

		/// <summary>Gets an extended description of why this configuration exception was thrown.</summary>
		/// <returns>An extended description of why this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception was thrown.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004778 File Offset: 0x00002978
		public override string Message
		{
			get
			{
				string text;
				if (!string.IsNullOrEmpty(this.filename))
				{
					if (this.line != 0)
					{
						text = string.Concat(new string[]
						{
							this.BareMessage,
							" (",
							this.filename,
							" line ",
							this.line.ToString(),
							")"
						});
					}
					else
					{
						text = this.BareMessage + " (" + this.filename + ")";
					}
				}
				else if (this.line != 0)
				{
					text = this.BareMessage + " (line " + this.line.ToString() + ")";
				}
				else
				{
					text = this.BareMessage;
				}
				return text;
			}
		}

		/// <summary>Gets the path to the configuration file that the internal <see cref="T:System.Xml.XmlReader" /> was reading when this configuration exception was thrown.</summary>
		/// <returns>The path of the configuration file the internal <see cref="T:System.Xml.XmlReader" /> object was accessing when the exception occurred.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x060000AA RID: 170 RVA: 0x00004831 File Offset: 0x00002A31
		public static string GetFilename(XmlReader reader)
		{
			if (reader is IConfigErrorInfo)
			{
				return ((IConfigErrorInfo)reader).Filename;
			}
			if (reader == null)
			{
				return null;
			}
			return reader.BaseURI;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlReader" /> object was processing when this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file that the <see cref="T:System.Xml.XmlReader" /> object was accessing when the exception occurred.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x060000AB RID: 171 RVA: 0x00004854 File Offset: 0x00002A54
		public static int GetLineNumber(XmlReader reader)
		{
			if (reader is IConfigErrorInfo)
			{
				return ((IConfigErrorInfo)reader).LineNumber;
			}
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo == null)
			{
				return 0;
			}
			return xmlLineInfo.LineNumber;
		}

		/// <summary>Gets the path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</summary>
		/// <returns>The path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown. </returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x060000AC RID: 172 RVA: 0x00004887 File Offset: 0x00002A87
		public static string GetFilename(XmlNode node)
		{
			if (!(node is IConfigErrorInfo))
			{
				return null;
			}
			return ((IConfigErrorInfo)node).Filename;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlNode" /> object represented when this configuration exception was thrown.</summary>
		/// <returns>The line number within the configuration file that contains the <see cref="T:System.Xml.XmlNode" /> object being parsed when this configuration exception was thrown.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> object that caused this <see cref="T:System.Configuration.ConfigurationErrorsException" /> exception to be thrown.</param>
		// Token: 0x060000AD RID: 173 RVA: 0x0000489E File Offset: 0x00002A9E
		public static int GetLineNumber(XmlNode node)
		{
			if (!(node is IConfigErrorInfo))
			{
				return 0;
			}
			return ((IConfigErrorInfo)node).LineNumber;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this configuration exception occurred.</summary>
		/// <param name="info">The object that holds the information to be serialized.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060000AE RID: 174 RVA: 0x000048B5 File Offset: 0x00002AB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ConfigurationErrors_Filename", this.filename);
			info.AddValue("ConfigurationErrors_Line", this.line);
		}

		// Token: 0x0400004E RID: 78
		private readonly string filename;

		// Token: 0x0400004F RID: 79
		private readonly int line;
	}
}
