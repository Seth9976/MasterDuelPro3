using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown in <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> when the filter criteria is not valid for the type of filter you are using.</summary>
	// Token: 0x02000605 RID: 1541
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the default properties.</summary>
		// Token: 0x06002CE4 RID: 11492 RVA: 0x000B1D4C File Offset: 0x000AFF4C
		public InvalidFilterCriteriaException()
			: this("Specified filter criteria was invalid.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the given HRESULT and message string.</summary>
		/// <param name="message">The message text for the exception. </param>
		// Token: 0x06002CE5 RID: 11493 RVA: 0x000B1D59 File Offset: 0x000AFF59
		public InvalidFilterCriteriaException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002CE6 RID: 11494 RVA: 0x000B1D63 File Offset: 0x000AFF63
		public InvalidFilterCriteriaException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232831;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">A SerializationInfo object that contains the information required to serialize this instance. </param>
		/// <param name="context">A StreamingContext object that contains the source and destination of the serialized stream associated with this instance. </param>
		// Token: 0x06002CE7 RID: 11495 RVA: 0x000537CF File Offset: 0x000519CF
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
