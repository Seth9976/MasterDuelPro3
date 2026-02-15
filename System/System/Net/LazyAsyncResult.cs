using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020003CE RID: 974
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00066F78 File Offset: 0x00065178
		private static LazyAsyncResult.ThreadContext CurrentThreadContext
		{
			get
			{
				LazyAsyncResult.ThreadContext threadContext = LazyAsyncResult.t_ThreadContext;
				if (threadContext == null)
				{
					threadContext = new LazyAsyncResult.ThreadContext();
					LazyAsyncResult.t_ThreadContext = threadContext;
				}
				return threadContext;
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00066F9B File Offset: 0x0006519B
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = DBNull.Value;
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x00066FC3 File Offset: 0x000651C3
		internal object AsyncObject
		{
			get
			{
				return this.m_AsyncObject;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00066FCB File Offset: 0x000651CB
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00066FD3 File Offset: 0x000651D3
		protected AsyncCallback AsyncCallback
		{
			get
			{
				return this.m_AsyncCallback;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00066FDC File Offset: 0x000651DC
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				this.m_UserEvent = true;
				if (this.m_IntCompleted == 0)
				{
					Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				while (manualResetEvent == null)
				{
					this.LazilyCreateEvent(out manualResetEvent);
				}
				return manualResetEvent;
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00067028 File Offset: 0x00065228
		private bool LazilyCreateEvent(out ManualResetEvent waitHandle)
		{
			waitHandle = new ManualResetEvent(false);
			bool flag;
			try
			{
				if (Interlocked.CompareExchange(ref this.m_Event, waitHandle, null) == null)
				{
					if (this.InternalPeekCompleted)
					{
						waitHandle.Set();
					}
					flag = true;
				}
				else
				{
					waitHandle.Close();
					waitHandle = (ManualResetEvent)this.m_Event;
					flag = false;
				}
			}
			catch
			{
				this.m_Event = null;
				if (waitHandle != null)
				{
					waitHandle.Close();
				}
				throw;
			}
			return flag;
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x000670A0 File Offset: 0x000652A0
		public bool CompletedSynchronously
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return num > 0;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x000670D0 File Offset: 0x000652D0
		public bool IsCompleted
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return (num & int.MaxValue) != 0;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x00067103 File Offset: 0x00065303
		internal bool InternalPeekCompleted
		{
			get
			{
				return (this.m_IntCompleted & int.MaxValue) != 0;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00067114 File Offset: 0x00065314
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x0006711C File Offset: 0x0006531C
		internal bool EndCalled
		{
			get
			{
				return this.m_EndCalled;
			}
			set
			{
				this.m_EndCalled = value;
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00067128 File Offset: 0x00065328
		protected void ProtectedInvokeCallback(object result, IntPtr userToken)
		{
			if (result == DBNull.Value)
			{
				throw new ArgumentNullException("result");
			}
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				if (this.m_Result == DBNull.Value)
				{
					this.m_Result = result;
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent != null)
				{
					try
					{
						manualResetEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Complete(userToken);
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000671B0 File Offset: 0x000653B0
		internal void InvokeCallback(object result)
		{
			this.ProtectedInvokeCallback(result, IntPtr.Zero);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000671BE File Offset: 0x000653BE
		internal void InvokeCallback()
		{
			this.ProtectedInvokeCallback(null, IntPtr.Zero);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000671CC File Offset: 0x000653CC
		protected virtual void Complete(IntPtr userToken)
		{
			bool flag = false;
			LazyAsyncResult.ThreadContext currentThreadContext = LazyAsyncResult.CurrentThreadContext;
			try
			{
				currentThreadContext.m_NestedIOCount++;
				if (this.m_AsyncCallback != null)
				{
					if (currentThreadContext.m_NestedIOCount >= 50)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerThreadComplete));
						flag = true;
					}
					else
					{
						this.m_AsyncCallback(this);
					}
				}
			}
			finally
			{
				currentThreadContext.m_NestedIOCount--;
				if (!flag)
				{
					this.Cleanup();
				}
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00067250 File Offset: 0x00065450
		private void WorkerThreadComplete(object state)
		{
			try
			{
				this.m_AsyncCallback(this);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected virtual void Cleanup()
		{
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00067284 File Offset: 0x00065484
		internal object InternalWaitForCompletion()
		{
			return this.WaitForCompletion(true);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00067290 File Offset: 0x00065490
		private object WaitForCompletion(bool snap)
		{
			ManualResetEvent manualResetEvent = null;
			bool flag = false;
			if (!(snap ? this.IsCompleted : this.InternalPeekCompleted))
			{
				manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent == null)
				{
					flag = this.LazilyCreateEvent(out manualResetEvent);
				}
			}
			if (manualResetEvent == null)
			{
				goto IL_0073;
			}
			try
			{
				manualResetEvent.WaitOne(-1, false);
				goto IL_0073;
			}
			catch (ObjectDisposedException)
			{
				goto IL_0073;
			}
			finally
			{
				if (flag && !this.m_UserEvent)
				{
					ManualResetEvent manualResetEvent2 = (ManualResetEvent)this.m_Event;
					this.m_Event = null;
					if (!this.m_UserEvent)
					{
						manualResetEvent2.Close();
					}
				}
			}
			IL_006D:
			Thread.SpinWait(1);
			IL_0073:
			if (this.m_Result != DBNull.Value)
			{
				return this.m_Result;
			}
			goto IL_006D;
		}

		// Token: 0x04000F5A RID: 3930
		[ThreadStatic]
		private static LazyAsyncResult.ThreadContext t_ThreadContext;

		// Token: 0x04000F5B RID: 3931
		private object m_AsyncObject;

		// Token: 0x04000F5C RID: 3932
		private object m_AsyncState;

		// Token: 0x04000F5D RID: 3933
		private AsyncCallback m_AsyncCallback;

		// Token: 0x04000F5E RID: 3934
		private object m_Result;

		// Token: 0x04000F5F RID: 3935
		private int m_IntCompleted;

		// Token: 0x04000F60 RID: 3936
		private bool m_EndCalled;

		// Token: 0x04000F61 RID: 3937
		private bool m_UserEvent;

		// Token: 0x04000F62 RID: 3938
		private object m_Event;

		// Token: 0x020003CF RID: 975
		private class ThreadContext
		{
			// Token: 0x04000F63 RID: 3939
			internal int m_NestedIOCount;
		}
	}
}
