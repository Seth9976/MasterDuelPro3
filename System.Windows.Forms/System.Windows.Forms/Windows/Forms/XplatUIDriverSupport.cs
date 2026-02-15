using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x0200029C RID: 668
	internal static class XplatUIDriverSupport
	{
		// Token: 0x0600190F RID: 6415 RVA: 0x00078F04 File Offset: 0x00077104
		internal static void ExecutionCallback(AsyncMethodData data)
		{
			AsyncMethodResult result = data.Result;
			object obj;
			try
			{
				obj = data.Method.DynamicInvoke(data.Args);
			}
			catch (Exception ex)
			{
				if (result != null)
				{
					result.CompleteWithException(ex);
					return;
				}
				throw;
			}
			if (result != null)
			{
				result.Complete(obj);
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00078F58 File Offset: 0x00077158
		private static void ExecutionCallbackInContext(object state)
		{
			AsyncMethodData asyncMethodData = (AsyncMethodData)state;
			if (asyncMethodData.SyncContext == null)
			{
				XplatUIDriverSupport.ExecutionCallback(asyncMethodData);
				return;
			}
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			SynchronizationContext.SetSynchronizationContext(asyncMethodData.SyncContext);
			try
			{
				XplatUIDriverSupport.ExecutionCallback(asyncMethodData);
			}
			finally
			{
				SynchronizationContext.SetSynchronizationContext(synchronizationContext);
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00078FAC File Offset: 0x000771AC
		internal static void ExecuteClientMessage(GCHandle gchandle)
		{
			AsyncMethodData asyncMethodData = (AsyncMethodData)gchandle.Target;
			try
			{
				if (asyncMethodData.Context == null)
				{
					XplatUIDriverSupport.ExecutionCallback(asyncMethodData);
				}
				else
				{
					asyncMethodData.SyncContext = SynchronizationContext.Current;
					ExecutionContext.Run(asyncMethodData.Context, new ContextCallback(XplatUIDriverSupport.ExecutionCallbackInContext), asyncMethodData);
				}
			}
			finally
			{
				gchandle.Free();
			}
		}
	}
}
