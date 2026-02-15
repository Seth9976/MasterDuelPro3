using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to access a class member fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011C RID: 284
	[Serializable]
	public class MemberAccessException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class.</summary>
		// Token: 0x06000975 RID: 2421 RVA: 0x00028973 File Offset: 0x00026B73
		public MemberAccessException()
			: base("Cannot access member.")
		{
			base.HResult = -2146233062;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000976 RID: 2422 RVA: 0x0002898B File Offset: 0x00026B8B
		public MemberAccessException(string message)
			: base(message)
		{
			base.HResult = -2146233062;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000977 RID: 2423 RVA: 0x00018324 File Offset: 0x00016524
		protected MemberAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
