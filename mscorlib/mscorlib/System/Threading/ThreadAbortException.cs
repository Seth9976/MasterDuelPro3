using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a call is made to the <see cref="M:System.Threading.Thread.Abort(System.Object)" /> method. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000264 RID: 612
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x060016DA RID: 5850 RVA: 0x0005998E File Offset: 0x00057B8E
		private ThreadAbortException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
		{
			base.SetErrorCode(-2146233040);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00018324 File Offset: 0x00016524
		internal ThreadAbortException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets an object that contains application-specific information related to the thread abort.</summary>
		/// <returns>An object containing application-specific information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000599A7 File Offset: 0x00057BA7
		public object ExceptionState
		{
			get
			{
				return Thread.CurrentThread.AbortReason;
			}
		}
	}
}
