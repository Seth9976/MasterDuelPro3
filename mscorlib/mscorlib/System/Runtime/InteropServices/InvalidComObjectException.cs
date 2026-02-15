using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when an invalid COM object is used.</summary>
	// Token: 0x02000520 RID: 1312
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		/// <summary>Initializes an instance of the InvalidComObjectException with default properties.</summary>
		// Token: 0x06002906 RID: 10502 RVA: 0x000A7E4B File Offset: 0x000A604B
		public InvalidComObjectException()
			: base("Attempt has been made to use a COM object that does not have a backing class factory.")
		{
			base.HResult = -2146233049;
		}

		/// <summary>Initializes an instance of the InvalidComObjectException with a message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x06002907 RID: 10503 RVA: 0x000A7E63 File Offset: 0x000A6063
		public InvalidComObjectException(string message)
			: base(message)
		{
			base.HResult = -2146233049;
		}

		/// <summary>Initializes a new instance of the COMException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002908 RID: 10504 RVA: 0x00018324 File Offset: 0x00016524
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
