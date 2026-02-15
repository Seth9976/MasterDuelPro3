using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an internal error in the execution engine of the common language runtime. This class cannot be inherited.  </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E0 RID: 224
	[Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class.</summary>
		// Token: 0x06000786 RID: 1926 RVA: 0x0001E722 File Offset: 0x0001C922
		public ExecutionEngineException()
			: base("Internal error in the runtime.")
		{
			base.HResult = -2146233082;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000787 RID: 1927 RVA: 0x0001E73A File Offset: 0x0001C93A
		public ExecutionEngineException(string message)
			: base(message)
		{
			base.HResult = -2146233082;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00018324 File Offset: 0x00016524
		internal ExecutionEngineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
