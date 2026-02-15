using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception thrown when the internal buffer overflows.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200035B RID: 859
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class.</summary>
		// Token: 0x06001549 RID: 5449 RVA: 0x0005B3C5 File Offset: 0x000595C5
		public InternalBufferOverflowException()
			: base("Internal buffer overflow occurred.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the error message to be displayed specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		// Token: 0x0600154A RID: 5450 RVA: 0x0001C4FC File Offset: 0x0001A6FC
		public InternalBufferOverflowException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The information required to serialize the T:System.IO.InternalBufferOverflowException object.</param>
		/// <param name="context">The source and destination of the serialized stream associated with the T:System.IO.InternalBufferOverflowException object.</param>
		// Token: 0x0600154B RID: 5451 RVA: 0x00033B0D File Offset: 0x00031D0D
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
