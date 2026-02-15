using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Threading
{
	/// <summary>Represents a handle that has been registered when calling <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000284 RID: 644
	[ComVisible(true)]
	public sealed class RegisteredWaitHandle : MarshalByRefObject
	{
		// Token: 0x06001804 RID: 6148 RVA: 0x0005C91C File Offset: 0x0005AB1C
		internal RegisteredWaitHandle(WaitHandle waitObject, WaitOrTimerCallback callback, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			this._waitObject = waitObject;
			this._callback = callback;
			this._state = state;
			this._timeout = timeout;
			this._executeOnlyOnce = executeOnlyOnce;
			this._finalEvent = null;
			this._cancelEvent = new ManualResetEvent(false);
			this._callsInProcess = 0;
			this._unregistered = false;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0005C978 File Offset: 0x0005AB78
		internal void Wait(object state)
		{
			bool flag = false;
			try
			{
				this._waitObject.SafeWaitHandle.DangerousAddRef(ref flag);
				RegisteredWaitHandle registeredWaitHandle;
				try
				{
					WaitHandle[] array = new WaitHandle[] { this._waitObject, this._cancelEvent };
					do
					{
						int num = WaitHandle.WaitAny(array, this._timeout, false);
						if (!this._unregistered)
						{
							registeredWaitHandle = this;
							lock (registeredWaitHandle)
							{
								this._callsInProcess++;
							}
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoCallBack), num == 258);
						}
					}
					while (!this._unregistered && !this._executeOnlyOnce);
				}
				catch
				{
				}
				registeredWaitHandle = this;
				lock (registeredWaitHandle)
				{
					this._unregistered = true;
					if (this._callsInProcess == 0 && this._finalEvent != null)
					{
						NativeEventCalls.SetEvent(this._finalEvent.SafeWaitHandle);
						this._finalEvent = null;
					}
				}
			}
			catch (ObjectDisposedException)
			{
				if (flag)
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this._waitObject.SafeWaitHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0005CACC File Offset: 0x0005ACCC
		private void DoCallBack(object timedOut)
		{
			try
			{
				if (this._callback != null)
				{
					this._callback(this._state, (bool)timedOut);
				}
			}
			finally
			{
				lock (this)
				{
					this._callsInProcess--;
					if (this._unregistered && this._callsInProcess == 0 && this._finalEvent != null)
					{
						NativeEventCalls.SetEvent(this._finalEvent.SafeWaitHandle);
						this._finalEvent = null;
					}
				}
			}
		}

		/// <summary>Cancels a registered wait operation issued by the <see cref="M:System.Threading.ThreadPool.RegisterWaitForSingleObject(System.Threading.WaitHandle,System.Threading.WaitOrTimerCallback,System.Object,System.UInt32,System.Boolean)" /> method.</summary>
		/// <returns>true if the function succeeds; otherwise, false.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to be signaled. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001807 RID: 6151 RVA: 0x0005CB70 File Offset: 0x0005AD70
		[ComVisible(true)]
		public bool Unregister(WaitHandle waitObject)
		{
			bool flag2;
			lock (this)
			{
				if (this._unregistered)
				{
					flag2 = false;
				}
				else
				{
					this._finalEvent = waitObject;
					this._unregistered = true;
					this._cancelEvent.Set();
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000176B9 File Offset: 0x000158B9
		internal RegisteredWaitHandle()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B47 RID: 2887
		private WaitHandle _waitObject;

		// Token: 0x04000B48 RID: 2888
		private WaitOrTimerCallback _callback;

		// Token: 0x04000B49 RID: 2889
		private object _state;

		// Token: 0x04000B4A RID: 2890
		private WaitHandle _finalEvent;

		// Token: 0x04000B4B RID: 2891
		private ManualResetEvent _cancelEvent;

		// Token: 0x04000B4C RID: 2892
		private TimeSpan _timeout;

		// Token: 0x04000B4D RID: 2893
		private int _callsInProcess;

		// Token: 0x04000B4E RID: 2894
		private bool _executeOnlyOnce;

		// Token: 0x04000B4F RID: 2895
		private bool _unregistered;
	}
}
