using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a method requires the caller to own the lock on a given Monitor, and the method is invoked by a caller that does not own that lock.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000222 RID: 546
	[Serializable]
	public class SynchronizationLockException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with default properties.</summary>
		// Token: 0x0600147C RID: 5244 RVA: 0x000536CA File Offset: 0x000518CA
		public SynchronizationLockException()
			: base("Object synchronization method was called from an unsynchronized block of code.")
		{
			base.HResult = -2146233064;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x0600147D RID: 5245 RVA: 0x000536E2 File Offset: 0x000518E2
		public SynchronizationLockException(string message)
			: base(message)
		{
			base.HResult = -2146233064;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600147E RID: 5246 RVA: 0x000536F6 File Offset: 0x000518F6
		public SynchronizationLockException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233064;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x0600147F RID: 5247 RVA: 0x00018324 File Offset: 0x00016524
		protected SynchronizationLockException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
