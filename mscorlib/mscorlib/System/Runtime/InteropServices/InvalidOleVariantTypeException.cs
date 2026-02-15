using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown by the marshaler when it encounters an argument of a variant type that can not be marshaled to managed code.</summary>
	// Token: 0x02000521 RID: 1313
	[Serializable]
	public class InvalidOleVariantTypeException : SystemException
	{
		/// <summary>Initializes a new instance of the InvalidOleVariantTypeException class with default values.</summary>
		// Token: 0x06002909 RID: 10505 RVA: 0x000A7E77 File Offset: 0x000A6077
		public InvalidOleVariantTypeException()
			: base("Specified OLE variant was invalid.")
		{
			base.HResult = -2146233039;
		}

		/// <summary>Initializes a new instance of the InvalidOleVariantTypeException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x0600290A RID: 10506 RVA: 0x00018324 File Offset: 0x00016524
		protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
