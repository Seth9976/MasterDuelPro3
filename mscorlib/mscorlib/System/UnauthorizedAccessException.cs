using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the operating system denies access because of an I/O error or a specific type of security error.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015F RID: 351
	[Serializable]
	public class UnauthorizedAccessException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class.</summary>
		// Token: 0x06000CB0 RID: 3248 RVA: 0x0003478E File Offset: 0x0003298E
		public UnauthorizedAccessException()
			: base("Attempted to perform an unauthorized operation.")
		{
			base.HResult = -2147024891;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000CB1 RID: 3249 RVA: 0x000347A6 File Offset: 0x000329A6
		public UnauthorizedAccessException(string message)
			: base(message)
		{
			base.HResult = -2147024891;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UnauthorizedAccessException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000CB2 RID: 3250 RVA: 0x00018324 File Offset: 0x00016524
		protected UnauthorizedAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
