using System;
using System.Resources;
using System.Runtime.Serialization;

namespace System.Xml.XPath
{
	/// <summary>Provides the exception thrown when an error occurs while processing an XPath expression. </summary>
	// Token: 0x02000138 RID: 312
	[Serializable]
	public class XPathException : SystemException
	{
		/// <summary>Uses the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects to initialize a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains all the properties of an <see cref="T:System.Xml.XPath.XPathException" />. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object. </param>
		// Token: 0x06000F62 RID: 3938 RVA: 0x0004CDAC File Offset: 0x0004AFAC
		protected XPathException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
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
				this.message = XPathException.CreateMessage(this.res, this.args);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the <see cref="T:System.Xml.XPath.XPathException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the specified <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06000F63 RID: 3939 RVA: 0x0004CE5D File Offset: 0x0004B05D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class.</summary>
		// Token: 0x06000F64 RID: 3940 RVA: 0x0004CE99 File Offset: 0x0004B099
		public XPathException()
			: this(string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XPath.XPathException" /> class using the specified exception message and <see cref="T:System.Exception" /> object.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that threw the <see cref="T:System.Xml.XPath.XPathException" />, if any. This value can be null. </param>
		// Token: 0x06000F65 RID: 3941 RVA: 0x0004CEA7 File Offset: 0x0004B0A7
		public XPathException(string message, Exception innerException)
			: this("{0}", new string[] { message }, innerException)
		{
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0004CEBF File Offset: 0x0004B0BF
		internal static XPathException Create(string res)
		{
			return new XPathException(res, null);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0004CEC8 File Offset: 0x0004B0C8
		internal static XPathException Create(string res, string arg)
		{
			return new XPathException(res, new string[] { arg });
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0004CEDA File Offset: 0x0004B0DA
		internal static XPathException Create(string res, string arg, string arg2)
		{
			return new XPathException(res, new string[] { arg, arg2 });
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0004CEF0 File Offset: 0x0004B0F0
		internal static XPathException Create(string res, string arg, Exception innerException)
		{
			return new XPathException(res, new string[] { arg }, innerException);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0004CF03 File Offset: 0x0004B103
		private XPathException(string res, string[] args)
			: this(res, args, null)
		{
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0004CF0E File Offset: 0x0004B10E
		private XPathException(string res, string[] args, Exception inner)
			: base(XPathException.CreateMessage(res, args), inner)
		{
			base.HResult = -2146231997;
			this.res = res;
			this.args = args;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0004CF38 File Offset: 0x0004B138
		private static string CreateMessage(string res, string[] args)
		{
			string text2;
			try
			{
				string text = Res.GetString(res, args);
				if (text == null)
				{
					text = "UNKNOWN(" + res + ")";
				}
				text2 = text;
			}
			catch (MissingManifestResourceException)
			{
				text2 = "UNKNOWN(" + res + ")";
			}
			return text2;
		}

		/// <summary>Gets the description of the error condition for this exception.</summary>
		/// <returns>The string description of the error condition for this exception.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0004CF8C File Offset: 0x0004B18C
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

		// Token: 0x04000790 RID: 1936
		private string res;

		// Token: 0x04000791 RID: 1937
		private string[] args;

		// Token: 0x04000792 RID: 1938
		private string message;
	}
}
