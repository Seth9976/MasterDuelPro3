using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when an I/O error occurs.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200079C RID: 1948
	[Serializable]
	public class IOException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to the empty string (""), its HRESULT set to COR_E_IO, and its inner exception set to a null reference.</summary>
		// Token: 0x06003D66 RID: 15718 RVA: 0x000EC6AA File Offset: 0x000EA8AA
		public IOException()
			: base("I/O error occurred.")
		{
			base.HResult = -2146232800;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to <paramref name="message" />, its HRESULT set to COR_E_IO, and its inner exception set to null.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06003D67 RID: 15719 RVA: 0x000EC6C2 File Offset: 0x000EA8C2
		public IOException(string message)
			: base(message)
		{
			base.HResult = -2146232800;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with its message string set to <paramref name="message" /> and its HRESULT user-defined.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		/// <param name="hresult">An integer identifying the error that has occurred. </param>
		// Token: 0x06003D68 RID: 15720 RVA: 0x000A7949 File Offset: 0x000A5B49
		public IOException(string message, int hresult)
			: base(message)
		{
			base.HResult = hresult;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003D69 RID: 15721 RVA: 0x000EC6D6 File Offset: 0x000EA8D6
		public IOException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232800;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IOException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the object. </param>
		/// <param name="context">The source and destination for the object. </param>
		// Token: 0x06003D6A RID: 15722 RVA: 0x00018324 File Offset: 0x00016524
		protected IOException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
