using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when the <see cref="Overload:System.Threading.Semaphore.Release" /> method is called on a semaphore whose count is already at the maximum. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000220 RID: 544
	[TypeForwardedFrom("System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class SemaphoreFullException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreFullException" /> class with default values.</summary>
		// Token: 0x06001474 RID: 5236 RVA: 0x000536AA File Offset: 0x000518AA
		public SemaphoreFullException()
			: base("Adding the specified count to the semaphore would cause it to exceed its maximum count.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreFullException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001475 RID: 5237 RVA: 0x000536B7 File Offset: 0x000518B7
		public SemaphoreFullException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreFullException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06001476 RID: 5238 RVA: 0x000536C0 File Offset: 0x000518C0
		public SemaphoreFullException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreFullException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001477 RID: 5239 RVA: 0x00018324 File Offset: 0x00016524
		protected SemaphoreFullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
