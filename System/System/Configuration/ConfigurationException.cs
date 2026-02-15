using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Configuration
{
	/// <summary>The exception that is thrown when a configuration system error has occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011D RID: 285
	[Serializable]
	public class ConfigurationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		// Token: 0x06000579 RID: 1401 RVA: 0x0001C4E5 File Offset: 0x0001A6E5
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException()
			: this(null)
		{
			this.filename = null;
			this.line = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		// Token: 0x0600057A RID: 1402 RVA: 0x0001C4FC File Offset: 0x0001A6FC
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		// Token: 0x0600057B RID: 1403 RVA: 0x0001C505 File Offset: 0x0001A705
		protected ConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.filename = info.GetString("filename");
			this.line = info.GetInt32("line");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		// Token: 0x0600057C RID: 1404 RVA: 0x0001C531 File Offset: 0x0001A731
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		// Token: 0x0600057D RID: 1405 RVA: 0x0001C53B File Offset: 0x0001A73B
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, XmlNode node)
			: base(message)
		{
			this.filename = ConfigurationException.GetXmlNodeFilename(node);
			this.line = ConfigurationException.GetXmlNodeLineNumber(node);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationException" /> class. </summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</param>
		/// <param name="inner">The inner exception that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown, if any.</param>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> to be thrown.</param>
		// Token: 0x0600057E RID: 1406 RVA: 0x0001C55C File Offset: 0x0001A75C
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, XmlNode node)
			: base(message, inner)
		{
			this.filename = ConfigurationException.GetXmlNodeFilename(node);
			this.line = ConfigurationException.GetXmlNodeLineNumber(node);
		}

		/// <summary>Gets a description of why this configuration exception was thrown.</summary>
		/// <returns>A description of why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0001C57E File Offset: 0x0001A77E
		public virtual string BareMessage
		{
			get
			{
				return base.Message;
			}
		}

		/// <summary>Gets an extended description of why this configuration exception was thrown.</summary>
		/// <returns>An extended description of why this <see cref="T:System.Configuration.ConfigurationException" /> exception was thrown.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001C588 File Offset: 0x0001A788
		public override string Message
		{
			get
			{
				string text;
				if (this.filename != null && this.filename.Length != 0)
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

		/// <summary>Gets the path to the configuration file from which the internal <see cref="T:System.Xml.XmlNode" /> object was loaded when this configuration exception was thrown.</summary>
		/// <returns>A string representing the node file name.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000581 RID: 1409 RVA: 0x0001C649 File Offset: 0x0001A849
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public static string GetXmlNodeFilename(XmlNode node)
		{
			if (!(node is IConfigXmlNode))
			{
				return string.Empty;
			}
			return ((IConfigXmlNode)node).Filename;
		}

		/// <summary>Gets the line number within the configuration file that the internal <see cref="T:System.Xml.XmlNode" /> object represented when this configuration exception was thrown.</summary>
		/// <returns>An int representing the node line number.</returns>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that caused this <see cref="T:System.Configuration.ConfigurationException" /> exception to be thrown.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000582 RID: 1410 RVA: 0x0001C664 File Offset: 0x0001A864
		[Obsolete("This class is obsolete.  Use System.Configuration.ConfigurationErrorsException")]
		public static int GetXmlNodeLineNumber(XmlNode node)
		{
			if (!(node is IConfigXmlNode))
			{
				return 0;
			}
			return ((IConfigXmlNode)node).LineNumber;
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and line number at which this configuration exception occurred.</summary>
		/// <param name="info">The object that holds the information to be serialized.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000583 RID: 1411 RVA: 0x0001C67B File Offset: 0x0001A87B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("line", this.line);
		}

		// Token: 0x040004B3 RID: 1203
		private readonly string filename;

		// Token: 0x040004B4 RID: 1204
		private readonly int line;
	}
}
