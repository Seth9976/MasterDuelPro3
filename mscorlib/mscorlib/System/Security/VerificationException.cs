using System;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when the security policy requires code to be type safe and the verification process is unable to verify that the code is type safe.</summary>
	// Token: 0x0200030E RID: 782
	[Serializable]
	public class VerificationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with default properties.</summary>
		// Token: 0x06001C49 RID: 7241 RVA: 0x0006EB24 File Offset: 0x0006CD24
		public VerificationException()
			: base("Operation could destabilize the runtime.")
		{
			base.HResult = -2146233075;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001C4A RID: 7242 RVA: 0x00018324 File Offset: 0x00016524
		protected VerificationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
