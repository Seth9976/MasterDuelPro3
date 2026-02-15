using System;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when there is a syntax error in XML parsing. This class cannot be inherited.</summary>
	// Token: 0x02000316 RID: 790
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with default properties.</summary>
		// Token: 0x06001C52 RID: 7250 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		public XmlSyntaxException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected. </param>
		// Token: 0x06001C53 RID: 7251 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		public XmlSyntaxException(int lineNumber)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected. </param>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06001C54 RID: 7252 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		public XmlSyntaxException(int lineNumber, string message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06001C55 RID: 7253 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		public XmlSyntaxException(string message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001C56 RID: 7254 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		public XmlSyntaxException(string message, Exception inner)
		{
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00018324 File Offset: 0x00016524
		private XmlSyntaxException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
