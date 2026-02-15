using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when recursive entry into a lock is not compatible with the recursion policy for the lock.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021D RID: 541
	[Serializable]
	public class LockRecursionException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.LockRecursionException" /> class with a system-supplied message that describes the error.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600146B RID: 5227 RVA: 0x00028040 File Offset: 0x00026240
		public LockRecursionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.LockRecursionException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor must make sure that this string has been localized for the current system culture. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600146C RID: 5228 RVA: 0x00028048 File Offset: 0x00026248
		public LockRecursionException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.LockRecursionException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor must make sure that this string has been localized for the current system culture. </param>
		/// <param name="innerException">The exception that caused the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600146D RID: 5229 RVA: 0x00053696 File Offset: 0x00051896
		public LockRecursionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.LockRecursionException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600146E RID: 5230 RVA: 0x0001876A File Offset: 0x0001696A
		protected LockRecursionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
