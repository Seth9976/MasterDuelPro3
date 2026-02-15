using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	/// <summary>The exception that is thrown when an unexpected operation occurs during a cryptographic operation.</summary>
	// Token: 0x02000386 RID: 902
	[ComVisible(true)]
	[Serializable]
	public class CryptographicUnexpectedOperationException : CryptographicException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with default properties.</summary>
		// Token: 0x06001F49 RID: 8009 RVA: 0x0007C269 File Offset: 0x0007A469
		public CryptographicUnexpectedOperationException()
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06001F4A RID: 8010 RVA: 0x0007C27C File Offset: 0x0007A47C
		public CryptographicUnexpectedOperationException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233295);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001F4B RID: 8011 RVA: 0x0007C290 File Offset: 0x0007A490
		protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
