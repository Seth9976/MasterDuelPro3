using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a requested method or operation is not implemented.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000126 RID: 294
	[Serializable]
	public class NotImplementedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with default properties.</summary>
		// Token: 0x060009B4 RID: 2484 RVA: 0x00029879 File Offset: 0x00027A79
		public NotImplementedException()
			: base("The method or operation is not implemented.")
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060009B5 RID: 2485 RVA: 0x00029891 File Offset: 0x00027A91
		public NotImplementedException(string message)
			: base(message)
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060009B6 RID: 2486 RVA: 0x000298A5 File Offset: 0x00027AA5
		public NotImplementedException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147467263;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NotImplementedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x060009B7 RID: 2487 RVA: 0x00018324 File Offset: 0x00016524
		protected NotImplementedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
