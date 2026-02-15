using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the time allotted for a process or operation has expired.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000152 RID: 338
	[Serializable]
	public class TimeoutException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class.</summary>
		// Token: 0x06000B71 RID: 2929 RVA: 0x000329AF File Offset: 0x00030BAF
		public TimeoutException()
			: base("The operation has timed out.")
		{
			base.HResult = -2146233083;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000B72 RID: 2930 RVA: 0x000329C7 File Offset: 0x00030BC7
		public TimeoutException(string message)
			: base(message)
		{
			base.HResult = -2146233083;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. The <paramref name="context" /> parameter is reserved for future use, and can be specified as null.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null, or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
		// Token: 0x06000B73 RID: 2931 RVA: 0x00018324 File Offset: 0x00016524
		protected TimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
