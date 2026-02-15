using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when the number of parameters for an invocation does not match the number expected. This class cannot be inherited.</summary>
	// Token: 0x02000625 RID: 1573
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with an empty message string and the root cause of the exception.</summary>
		// Token: 0x06002E2D RID: 11821 RVA: 0x000B2E32 File Offset: 0x000B1032
		public TargetParameterCountException()
			: base("Number of parameters specified does not match the expected number.")
		{
			base.HResult = -2147352562;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with its message string set to the given message and the root cause exception.</summary>
		/// <param name="message">A String describing the reason this exception was thrown. </param>
		// Token: 0x06002E2E RID: 11822 RVA: 0x000B2E4A File Offset: 0x000B104A
		public TargetParameterCountException(string message)
			: base(message)
		{
			base.HResult = -2147352562;
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000537CF File Offset: 0x000519CF
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
