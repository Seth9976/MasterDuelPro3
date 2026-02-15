using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a failure occurs in a managed thread after the underlying operating system thread has been started, but before the thread is ready to execute user code.</summary>
	// Token: 0x02000225 RID: 549
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x06001484 RID: 5252 RVA: 0x0005370B File Offset: 0x0005190B
		internal ThreadStartException()
			: base("Thread failed to start.")
		{
			base.HResult = -2146233051;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00053723 File Offset: 0x00051923
		internal ThreadStartException(Exception reason)
			: base("Thread failed to start.", reason)
		{
			base.HResult = -2146233051;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00018324 File Offset: 0x00016524
		private ThreadStartException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
