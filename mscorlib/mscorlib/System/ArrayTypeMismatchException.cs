using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to store an element of the wrong type within an array. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C2 RID: 194
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class.</summary>
		// Token: 0x060004D0 RID: 1232 RVA: 0x00018CCB File Offset: 0x00016ECB
		public ArrayTypeMismatchException()
			: base("Attempted to access an element as a type incompatible with the array.")
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060004D1 RID: 1233 RVA: 0x00018324 File Offset: 0x00016524
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
