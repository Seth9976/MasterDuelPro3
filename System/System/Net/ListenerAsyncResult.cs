using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200041F RID: 1055
	internal class ListenerAsyncResult : IAsyncResult
	{
		// Token: 0x06001A8A RID: 6794 RVA: 0x000733D0 File Offset: 0x000715D0
		public ListenerAsyncResult(AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000733F4 File Offset: 0x000715F4
		internal void Complete(Exception exc)
		{
			if (this.forward != null)
			{
				this.forward.Complete(exc);
				return;
			}
			this.exception = exc;
			if (this.InGet && exc is ObjectDisposedException)
			{
				this.exception = new HttpListenerException(500, "Listener closed");
			}
			object obj = this.locker;
			lock (obj)
			{
				this.completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
				if (this.cb != null)
				{
					ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
				}
			}
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000734A0 File Offset: 0x000716A0
		private static void InvokeCallback(object o)
		{
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)o;
			if (listenerAsyncResult.forward != null)
			{
				ListenerAsyncResult.InvokeCallback(listenerAsyncResult.forward);
				return;
			}
			try
			{
				listenerAsyncResult.cb(listenerAsyncResult);
			}
			catch
			{
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000734EC File Offset: 0x000716EC
		internal void Complete(HttpListenerContext context)
		{
			this.Complete(context, false);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000734F8 File Offset: 0x000716F8
		internal void Complete(HttpListenerContext context, bool synch)
		{
			if (this.forward != null)
			{
				this.forward.Complete(context, synch);
				return;
			}
			this.synch = synch;
			this.context = context;
			object obj = this.locker;
			lock (obj)
			{
				AuthenticationSchemes authenticationSchemes = context.Listener.SelectAuthenticationScheme(context);
				if ((authenticationSchemes == AuthenticationSchemes.Basic || context.Listener.AuthenticationSchemes == AuthenticationSchemes.Negotiate) && context.Request.Headers["Authorization"] == null)
				{
					context.Response.StatusCode = 401;
					context.Response.Headers["WWW-Authenticate"] = authenticationSchemes.ToString() + " realm=\"" + context.Listener.Realm + "\"";
					context.Response.OutputStream.Close();
					IAsyncResult asyncResult = context.Listener.BeginGetContext(this.cb, this.state);
					this.forward = (ListenerAsyncResult)asyncResult;
					object obj2 = this.forward.locker;
					lock (obj2)
					{
						if (this.handle != null)
						{
							this.forward.handle = this.handle;
						}
					}
					ListenerAsyncResult listenerAsyncResult = this.forward;
					int num = 0;
					while (listenerAsyncResult.forward != null)
					{
						if (num > 20)
						{
							this.Complete(new HttpListenerException(400, "Too many authentication errors"));
						}
						listenerAsyncResult = listenerAsyncResult.forward;
						num++;
					}
				}
				else
				{
					this.completed = true;
					this.synch = false;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.cb != null)
					{
						ThreadPool.UnsafeQueueUserWorkItem(ListenerAsyncResult.InvokeCB, this);
					}
				}
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000736F0 File Offset: 0x000718F0
		internal HttpListenerContext GetContext()
		{
			if (this.forward != null)
			{
				return this.forward.GetContext();
			}
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.context;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0007371B File Offset: 0x0007191B
		public object AsyncState
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncState;
				}
				return this.state;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00073738 File Offset: 0x00071938
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncWaitHandle;
				}
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x000737A8 File Offset: 0x000719A8
		public bool CompletedSynchronously
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.CompletedSynchronously;
				}
				return this.synch;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x000737C4 File Offset: 0x000719C4
		public bool IsCompleted
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.IsCompleted;
				}
				object obj = this.locker;
				bool flag2;
				lock (obj)
				{
					flag2 = this.completed;
				}
				return flag2;
			}
		}

		// Token: 0x04001133 RID: 4403
		private ManualResetEvent handle;

		// Token: 0x04001134 RID: 4404
		private bool synch;

		// Token: 0x04001135 RID: 4405
		private bool completed;

		// Token: 0x04001136 RID: 4406
		private AsyncCallback cb;

		// Token: 0x04001137 RID: 4407
		private object state;

		// Token: 0x04001138 RID: 4408
		private Exception exception;

		// Token: 0x04001139 RID: 4409
		private HttpListenerContext context;

		// Token: 0x0400113A RID: 4410
		private object locker = new object();

		// Token: 0x0400113B RID: 4411
		private ListenerAsyncResult forward;

		// Token: 0x0400113C RID: 4412
		internal bool EndCalled;

		// Token: 0x0400113D RID: 4413
		internal bool InGet;

		// Token: 0x0400113E RID: 4414
		private static WaitCallback InvokeCB = new WaitCallback(ListenerAsyncResult.InvokeCallback);
	}
}
