using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Encapsulates the results of an asynchronous operation on a delegate.</summary>
	// Token: 0x02000479 RID: 1145
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncResult : IAsyncResult, IMessageSink, IThreadPoolWorkItem
	{
		// Token: 0x060024FB RID: 9467 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal AsyncResult()
		{
		}

		/// <summary>Gets the object provided as the last parameter of a BeginInvoke method call.</summary>
		/// <returns>The object provided as the last parameter of a BeginInvoke method call.</returns>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x00096FF4 File Offset: 0x000951F4
		public virtual object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that encapsulates Win32 synchronization handles, and allows the implementation of various synchronization schemes.</returns>
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x00096FFC File Offset: 0x000951FC
		public virtual WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle waitHandle;
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
					waitHandle = this.handle;
				}
				return waitHandle;
			}
		}

		/// <summary>Gets a value indicating whether the BeginInvoke call completed synchronously.</summary>
		/// <returns>true if the BeginInvoke call completed synchronously; otherwise, false.</returns>
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x00097054 File Offset: 0x00095254
		public virtual bool CompletedSynchronously
		{
			get
			{
				return this.sync_completed;
			}
		}

		/// <summary>Gets a value indicating whether the server has completed the call.</summary>
		/// <returns>true after the server has completed the call; otherwise, false.</returns>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x0009705C File Offset: 0x0009525C
		public virtual bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		/// <summary>Gets or sets a value indicating whether EndInvoke has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />.</summary>
		/// <returns>true if EndInvoke has been called on the current <see cref="T:System.Runtime.Remoting.Messaging.AsyncResult" />; otherwise, false.</returns>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x00097064 File Offset: 0x00095264
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x0009706C File Offset: 0x0009526C
		public bool EndInvokeCalled
		{
			get
			{
				return this.endinvoke_called;
			}
			set
			{
				this.endinvoke_called = value;
			}
		}

		/// <summary>Gets the delegate object on which the asynchronous call was invoked.</summary>
		/// <returns>The delegate object on which the asynchronous call was invoked.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x00097075 File Offset: 0x00095275
		public virtual object AsyncDelegate
		{
			get
			{
				return this.async_delegate;
			}
		}

		/// <summary>Gets the next message sink in the sink chain.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface that represents the next message sink in the sink chain.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x000082D2 File Offset: 0x000064D2
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface.</summary>
		/// <returns>No value is returned.</returns>
		/// <param name="msg">The request <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface. </param>
		/// <param name="replySink">The response <see cref="T:System.Runtime.Remoting.Messaging.IMessageSink" /> interface. </param>
		// Token: 0x06002504 RID: 9476 RVA: 0x000339FF File Offset: 0x00031BFF
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the response message for the asynchronous call.</summary>
		/// <returns>A remoting message that should represent a response to a method call on a remote object.</returns>
		// Token: 0x06002505 RID: 9477 RVA: 0x0009707D File Offset: 0x0009527D
		public virtual IMessage GetReplyMessage()
		{
			return this.reply_message;
		}

		/// <summary>Sets an <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call, which provides a way to control asynchronous messages after they have been dispatched.</summary>
		/// <param name="mc">The <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> for the current remote method call. </param>
		// Token: 0x06002506 RID: 9478 RVA: 0x00097085 File Offset: 0x00095285
		public virtual void SetMessageCtrl(IMessageCtrl mc)
		{
			this.message_ctrl = mc;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x0009708E File Offset: 0x0009528E
		internal void SetCompletedSynchronously(bool completed)
		{
			this.sync_completed = completed;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00097098 File Offset: 0x00095298
		internal IMessage EndInvoke()
		{
			lock (this)
			{
				if (this.completed)
				{
					return this.reply_message;
				}
			}
			this.AsyncWaitHandle.WaitOne();
			return this.reply_message;
		}

		/// <summary>Synchronously processes a response message returned by a method call on a remote object.</summary>
		/// <returns>Returns null.</returns>
		/// <param name="msg">A response message to a method call on a remote object.</param>
		// Token: 0x06002509 RID: 9481 RVA: 0x000970F4 File Offset: 0x000952F4
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			this.reply_message = msg;
			lock (this)
			{
				this.completed = true;
				if (this.handle != null)
				{
					((ManualResetEvent)this.AsyncWaitHandle).Set();
				}
			}
			if (this.async_callback != null)
			{
				((AsyncCallback)this.async_callback)(this);
			}
			return null;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x0009716C File Offset: 0x0009536C
		// (set) Token: 0x0600250B RID: 9483 RVA: 0x00097174 File Offset: 0x00095374
		internal MonoMethodMessage CallMessage
		{
			get
			{
				return this.call_message;
			}
			set
			{
				this.call_message = value;
			}
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0009717D File Offset: 0x0009537D
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.Invoke();
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00002C89 File Offset: 0x00000E89
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x0600250E RID: 9486
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object Invoke();

		// Token: 0x040011C6 RID: 4550
		private object async_state;

		// Token: 0x040011C7 RID: 4551
		private WaitHandle handle;

		// Token: 0x040011C8 RID: 4552
		private object async_delegate;

		// Token: 0x040011C9 RID: 4553
		private IntPtr data;

		// Token: 0x040011CA RID: 4554
		private object object_data;

		// Token: 0x040011CB RID: 4555
		private bool sync_completed;

		// Token: 0x040011CC RID: 4556
		private bool completed;

		// Token: 0x040011CD RID: 4557
		private bool endinvoke_called;

		// Token: 0x040011CE RID: 4558
		private object async_callback;

		// Token: 0x040011CF RID: 4559
		private ExecutionContext current;

		// Token: 0x040011D0 RID: 4560
		private ExecutionContext original;

		// Token: 0x040011D1 RID: 4561
		private long add_time;

		// Token: 0x040011D2 RID: 4562
		private MonoMethodMessage call_message;

		// Token: 0x040011D3 RID: 4563
		private IMessageCtrl message_ctrl;

		// Token: 0x040011D4 RID: 4564
		private IMessage reply_message;

		// Token: 0x040011D5 RID: 4565
		private WaitCallback orig_cb;
	}
}
