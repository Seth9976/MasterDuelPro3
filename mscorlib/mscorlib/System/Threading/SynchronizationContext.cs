using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	/// <summary>Provides the basic functionality for propagating a synchronization context in various synchronization models. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200025E RID: 606
	public class SynchronizationContext
	{
		/// <summary>Sets notification that wait notification is required and prepares the callback method so it can be called more reliably when a wait occurs.</summary>
		// Token: 0x0600162B RID: 5675 RVA: 0x000589C8 File Offset: 0x00056BC8
		protected void SetWaitNotificationRequired()
		{
			Type type = base.GetType();
			if (SynchronizationContext.s_cachedPreparedType1 != type && SynchronizationContext.s_cachedPreparedType2 != type && SynchronizationContext.s_cachedPreparedType3 != type && SynchronizationContext.s_cachedPreparedType4 != type && SynchronizationContext.s_cachedPreparedType5 != type)
			{
				RuntimeHelpers.PrepareDelegate(new SynchronizationContext.WaitDelegate(this.Wait));
				if (SynchronizationContext.s_cachedPreparedType1 == null)
				{
					SynchronizationContext.s_cachedPreparedType1 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType2 == null)
				{
					SynchronizationContext.s_cachedPreparedType2 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType3 == null)
				{
					SynchronizationContext.s_cachedPreparedType3 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType4 == null)
				{
					SynchronizationContext.s_cachedPreparedType4 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType5 == null)
				{
					SynchronizationContext.s_cachedPreparedType5 = type;
				}
			}
			this._props |= SynchronizationContextProperties.RequireWaitNotification;
		}

		/// <summary>Determines if wait notification is required.</summary>
		/// <returns>true if wait notification is required; otherwise, false. </returns>
		// Token: 0x0600162C RID: 5676 RVA: 0x00058AB0 File Offset: 0x00056CB0
		public bool IsWaitNotificationRequired()
		{
			return (this._props & SynchronizationContextProperties.RequireWaitNotification) > SynchronizationContextProperties.None;
		}

		/// <summary>When overridden in a derived class, dispatches a synchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate. </param>
		/// <exception cref="T:System.NotSupportedException">The method was called in a Windows Store app. The implementation of <see cref="T:System.Threading.SynchronizationContext" /> for Windows Store apps does not support the <see cref="M:System.Threading.SynchronizationContext.Send(System.Threading.SendOrPostCallback,System.Object)" /> method. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600162D RID: 5677 RVA: 0x00058ABD File Offset: 0x00056CBD
		public virtual void Send(SendOrPostCallback d, object state)
		{
			d(state);
		}

		/// <summary>When overridden in a derived class, dispatches an asynchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600162E RID: 5678 RVA: 0x00058AC6 File Offset: 0x00056CC6
		public virtual void Post(SendOrPostCallback d, object state)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(d.Invoke), state);
		}

		/// <summary>When overridden in a derived class, responds to the notification that an operation has started.</summary>
		// Token: 0x0600162F RID: 5679 RVA: 0x00002C89 File Offset: 0x00000E89
		public virtual void OperationStarted()
		{
		}

		/// <summary>When overridden in a derived class, responds to the notification that an operation has completed.</summary>
		// Token: 0x06001630 RID: 5680 RVA: 0x00002C89 File Offset: 0x00000E89
		public virtual void OperationCompleted()
		{
		}

		/// <summary>Waits for any or all the elements in the specified array to receive a signal.</summary>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <param name="waitHandles">An array of type <see cref="T:System.IntPtr" /> that contains the native operating system handles.</param>
		/// <param name="waitAll">true to wait for all handles; false to wait for any handle. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="waitHandles" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001631 RID: 5681 RVA: 0x00058ADB File Offset: 0x00056CDB
		[CLSCompliant(false)]
		[PrePrepareMethod]
		public virtual int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException("waitHandles");
			}
			return SynchronizationContext.WaitHelper(waitHandles, waitAll, millisecondsTimeout);
		}

		/// <summary>Helper function that waits for any or all the elements in the specified array to receive a signal.</summary>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <param name="waitHandles">An array of type <see cref="T:System.IntPtr" /> that contains the native operating system handles.</param>
		/// <param name="waitAll">true to wait for all handles;  false to wait for any handle. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		// Token: 0x06001632 RID: 5682 RVA: 0x00058AF4 File Offset: 0x00056CF4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[CLSCompliant(false)]
		[PrePrepareMethod]
		protected unsafe static int WaitHelper(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			IntPtr* ptr;
			if (waitHandles == null || waitHandles.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &waitHandles[0];
			}
			return WaitHandle.Wait_internal(ptr, waitHandles.Length, waitAll, millisecondsTimeout);
		}

		/// <summary>Sets the current synchronization context.</summary>
		/// <param name="syncContext">The <see cref="T:System.Threading.SynchronizationContext" /> object to be set.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001633 RID: 5683 RVA: 0x00058B24 File Offset: 0x00056D24
		public static void SetSynchronizationContext(SynchronizationContext syncContext)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.SynchronizationContext = syncContext;
			mutableExecutionContext.SynchronizationContextNoFlow = syncContext;
		}

		/// <summary>Gets the synchronization context for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.SynchronizationContext" /> object representing the current synchronization context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00058B40 File Offset: 0x00056D40
		public static SynchronizationContext Current
		{
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContext ?? SynchronizationContext.GetThreadLocalContext();
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00058B68 File Offset: 0x00056D68
		internal static SynchronizationContext CurrentNoFlow
		{
			[FriendAccessAllowed]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContextNoFlow ?? SynchronizationContext.GetThreadLocalContext();
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000082D2 File Offset: 0x000064D2
		private static SynchronizationContext GetThreadLocalContext()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, creates a copy of the synchronization context.  </summary>
		/// <returns>A new <see cref="T:System.Threading.SynchronizationContext" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001637 RID: 5687 RVA: 0x00058B90 File Offset: 0x00056D90
		public virtual SynchronizationContext CreateCopy()
		{
			return new SynchronizationContext();
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00058B97 File Offset: 0x00056D97
		private static int InvokeWaitMethodHelper(SynchronizationContext syncContext, IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			return syncContext.Wait(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00058BA2 File Offset: 0x00056DA2
		internal static SynchronizationContext CurrentExplicit
		{
			get
			{
				return SynchronizationContext.Current;
			}
		}

		// Token: 0x04000AD5 RID: 2773
		private SynchronizationContextProperties _props;

		// Token: 0x04000AD6 RID: 2774
		private static Type s_cachedPreparedType1;

		// Token: 0x04000AD7 RID: 2775
		private static Type s_cachedPreparedType2;

		// Token: 0x04000AD8 RID: 2776
		private static Type s_cachedPreparedType3;

		// Token: 0x04000AD9 RID: 2777
		private static Type s_cachedPreparedType4;

		// Token: 0x04000ADA RID: 2778
		private static Type s_cachedPreparedType5;

		// Token: 0x0200025F RID: 607
		// (Invoke) Token: 0x0600163B RID: 5691
		private delegate int WaitDelegate(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);
	}
}
