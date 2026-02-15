using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception that is thrown by the marshaler when it encounters a <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> it does not support.</summary>
	// Token: 0x02000513 RID: 1299
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		/// <summary>Initializes a new instance of the MarshalDirectiveException class with default properties.</summary>
		// Token: 0x060028DD RID: 10461 RVA: 0x000A7A07 File Offset: 0x000A5C07
		public MarshalDirectiveException()
			: base("Marshaling directives are invalid.")
		{
			base.HResult = -2146233035;
		}

		/// <summary>Initializes a new instance of the MarshalDirectiveException class with a specified error message.</summary>
		/// <param name="message">The error message that specifies the reason for the exception. </param>
		// Token: 0x060028DE RID: 10462 RVA: 0x000A7A1F File Offset: 0x000A5C1F
		public MarshalDirectiveException(string message)
			: base(message)
		{
			base.HResult = -2146233035;
		}

		/// <summary>Initializes a new instance of the MarshalDirectiveException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x060028DF RID: 10463 RVA: 0x00018324 File Offset: 0x00016524
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
