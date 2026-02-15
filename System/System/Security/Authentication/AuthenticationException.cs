using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	/// <summary>The exception that is thrown when authentication fails for an authentication stream.</summary>
	// Token: 0x0200019E RID: 414
	[Serializable]
	public class AuthenticationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with no message.</summary>
		// Token: 0x060009EB RID: 2539 RVA: 0x00033AFB File Offset: 0x00031CFB
		public AuthenticationException()
			: base(global::Locale.GetText("Authentication exception."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		// Token: 0x060009EC RID: 2540 RVA: 0x0001C4FC File Offset: 0x0001A6FC
		public AuthenticationException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception.</param>
		// Token: 0x060009ED RID: 2541 RVA: 0x0001C531 File Offset: 0x0001A731
		public AuthenticationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to deserialize the new <see cref="T:System.Security.Authentication.AuthenticationException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance. </param>
		// Token: 0x060009EE RID: 2542 RVA: 0x00033B0D File Offset: 0x00031D0D
		protected AuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
