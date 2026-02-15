using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is insufficient execution stack available to allow most methods to execute.</summary>
	// Token: 0x0200010D RID: 269
	[Serializable]
	public sealed class InsufficientExecutionStackException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientExecutionStackException" /> class. </summary>
		// Token: 0x060008A5 RID: 2213 RVA: 0x0002780E File Offset: 0x00025A0E
		public InsufficientExecutionStackException()
			: base("Insufficient stack to continue executing the program safely. This can happen from having too many functions on the call stack or function on the stack using too much stack space.")
		{
			base.HResult = -2146232968;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00018324 File Offset: 0x00016524
		internal InsufficientExecutionStackException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
