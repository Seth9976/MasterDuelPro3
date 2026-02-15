using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown by methods invoked through reflection. This class cannot be inherited.</summary>
	// Token: 0x02000624 RID: 1572
	[Serializable]
	public sealed class TargetInvocationException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetInvocationException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002E2A RID: 11818 RVA: 0x000B2E04 File Offset: 0x000B1004
		public TargetInvocationException(Exception inner)
			: base("Exception has been thrown by the target of an invocation.", inner)
		{
			base.HResult = -2146232828;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetInvocationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002E2B RID: 11819 RVA: 0x000B2E1D File Offset: 0x000B101D
		public TargetInvocationException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232828;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000537CF File Offset: 0x000519CF
		internal TargetInvocationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
