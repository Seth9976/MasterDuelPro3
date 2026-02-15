using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when the type of the incoming SAFEARRAY does not match the type specified in the managed signature.</summary>
	// Token: 0x02000522 RID: 1314
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class with default values.</summary>
		// Token: 0x0600290B RID: 10507 RVA: 0x000A7E8F File Offset: 0x000A608F
		public SafeArrayTypeMismatchException()
			: base("Specified array was not of the expected type.")
		{
			base.HResult = -2146233037;
		}

		/// <summary>Initializes a new instance of the SafeArrayTypeMismatchException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x0600290C RID: 10508 RVA: 0x00018324 File Offset: 0x00016524
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
