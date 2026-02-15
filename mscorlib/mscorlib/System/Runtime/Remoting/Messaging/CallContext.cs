using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Provides a set of properties that are carried with the execution code path. This class cannot be inherited.</summary>
	// Token: 0x02000470 RID: 1136
	[ComVisible(true)]
	[Serializable]
	public sealed class CallContext
	{
		// Token: 0x060024CD RID: 9421 RVA: 0x00003CE1 File Offset: 0x00001EE1
		private CallContext()
		{
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000082D2 File Offset: 0x000064D2
		internal static object SetCurrentCallContext(LogicalCallContext ctx)
		{
			return null;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000968E4 File Offset: 0x00094AE4
		internal static LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			LogicalCallContext logicalCallContext = mutableExecutionContext.LogicalCallContext;
			mutableExecutionContext.LogicalCallContext = callCtx;
			return logicalCallContext;
		}

		/// <summary>Retrieves an object with the specified name from the logical call context.</summary>
		/// <returns>The object in the logical call context associated with the specified name.</returns>
		/// <param name="name">The name of the item in the logical call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x060024D0 RID: 9424 RVA: 0x0009690C File Offset: 0x00094B0C
		public static object LogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.GetData(name);
		}

		/// <summary>Stores a given object in the logical call context and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the logical call context. </param>
		/// <param name="data">The object to store in the logical call context. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x060024D1 RID: 9425 RVA: 0x00096934 File Offset: 0x00094B34
		public static void LogicalSetData(string name, object data)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.LogicalCallContext.SetData(name, data);
		}
	}
}
