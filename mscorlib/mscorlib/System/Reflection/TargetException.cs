using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Represents the exception that is thrown when an attempt is made to invoke an invalid target.</summary>
	// Token: 0x02000623 RID: 1571
	[Serializable]
	public class TargetException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with an empty message and the root cause of the exception.</summary>
		// Token: 0x06002E26 RID: 11814 RVA: 0x000B2DDC File Offset: 0x000B0FDC
		public TargetException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with the given message and the root cause exception.</summary>
		/// <param name="message">A String describing the reason why the exception occurred. </param>
		// Token: 0x06002E27 RID: 11815 RVA: 0x000B2DE5 File Offset: 0x000B0FE5
		public TargetException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002E28 RID: 11816 RVA: 0x000B2DEF File Offset: 0x000B0FEF
		public TargetException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232829;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the object. </param>
		/// <param name="context">The source of and destination for the object. </param>
		// Token: 0x06002E29 RID: 11817 RVA: 0x000537CF File Offset: 0x000519CF
		protected TargetException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
