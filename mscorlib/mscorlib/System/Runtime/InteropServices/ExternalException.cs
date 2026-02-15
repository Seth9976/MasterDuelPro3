using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The base exception type for all COM interop exceptions and structured exception handling (SEH) exceptions.</summary>
	// Token: 0x02000510 RID: 1296
	[Serializable]
	public class ExternalException : SystemException
	{
		/// <summary>Initializes a new instance of the ExternalException class with default properties.</summary>
		// Token: 0x060028D5 RID: 10453 RVA: 0x000A7908 File Offset: 0x000A5B08
		public ExternalException()
			: base("External component has thrown an exception.")
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the ExternalException class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		// Token: 0x060028D6 RID: 10454 RVA: 0x000A7920 File Offset: 0x000A5B20
		public ExternalException(string message)
			: base(message)
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ExternalException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060028D7 RID: 10455 RVA: 0x000A7934 File Offset: 0x000A5B34
		public ExternalException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147467259;
		}

		/// <summary>Initializes a new instance of the ExternalException class with a specified error message and the HRESULT of the error.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		/// <param name="errorCode">The HRESULT of the error. </param>
		// Token: 0x060028D8 RID: 10456 RVA: 0x000A7949 File Offset: 0x000A5B49
		public ExternalException(string message, int errorCode)
			: base(message)
		{
			base.HResult = errorCode;
		}

		/// <summary>Initializes a new instance of the ExternalException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x060028D9 RID: 10457 RVA: 0x00018324 File Offset: 0x00016524
		protected ExternalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Returns a string that contains the HRESULT of the error.</summary>
		/// <returns>A string that represents the HRESULT. </returns>
		// Token: 0x060028DA RID: 10458 RVA: 0x000A795C File Offset: 0x000A5B5C
		public override string ToString()
		{
			string message = this.Message;
			string text = base.GetType().ToString() + " (0x" + base.HResult.ToString("X8", CultureInfo.InvariantCulture) + ")";
			if (!string.IsNullOrEmpty(message))
			{
				text = text + ": " + message;
			}
			Exception innerException = base.InnerException;
			if (innerException != null)
			{
				text = text + " ---> " + innerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			return text;
		}
	}
}
