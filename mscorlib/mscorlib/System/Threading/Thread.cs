using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using Internal.Runtime.Augments;

namespace System.Threading
{
	/// <summary>Creates and controls a thread, sets its priority, and gets its status. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000262 RID: 610
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Thread : CriticalFinalizerObject, _Thread
	{
		// Token: 0x06001648 RID: 5704 RVA: 0x00058C7F File Offset: 0x00056E7F
		private static void AsyncLocalSetCurrentCulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.m_CurrentCulture = args.CurrentValue;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00058C8D File Offset: 0x00056E8D
		private static void AsyncLocalSetCurrentUICulture(AsyncLocalValueChangedArgs<CultureInfo> args)
		{
			Thread.m_CurrentUICulture = args.CurrentValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="start" /> parameter is null. </exception>
		// Token: 0x0600164A RID: 5706 RVA: 0x00058C9B File Offset: 0x00056E9B
		public Thread(ThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying the maximum stack size for the thread.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <param name="maxStackSize">The maximum stack size, in bytes, to be used by the thread, or 0 to use the default maximum stack size specified in the header for the executable.Important   For partially trusted code, <paramref name="maxStackSize" /> is ignored if it is greater than the default stack size. No exception is thrown. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStackSize" /> is less than zero.</exception>
		// Token: 0x0600164B RID: 5707 RVA: 0x00058CB9 File Offset: 0x00056EB9
		public Thread(ThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("Non-negative number required."));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying a delegate that allows an object to be passed to the thread when the thread is started.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		// Token: 0x0600164C RID: 5708 RVA: 0x00058C9B File Offset: 0x00056E9B
		public Thread(ParameterizedThreadStart start)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.SetStartHelper(start, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Thread" /> class, specifying a delegate that allows an object to be passed to the thread when the thread is started and specifying the maximum stack size for the thread.</summary>
		/// <param name="start">A <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate that represents the methods to be invoked when this thread begins executing.</param>
		/// <param name="maxStackSize">The maximum stack size, in bytes, to be used by the thread, or 0 to use the default maximum stack size specified in the header for the executable.Important   For partially trusted code, <paramref name="maxStackSize" /> is ignored if it is greater than the default stack size. No exception is thrown.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="start" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStackSize" /> is less than zero.</exception>
		// Token: 0x0600164D RID: 5709 RVA: 0x00058CB9 File Offset: 0x00056EB9
		public Thread(ParameterizedThreadStart start, int maxStackSize)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			if (0 > maxStackSize)
			{
				throw new ArgumentOutOfRangeException("maxStackSize", Environment.GetResourceString("Non-negative number required."));
			}
			this.SetStartHelper(start, maxStackSize);
		}

		/// <summary>Causes the operating system to change the state of the current instance to <see cref="F:System.Threading.ThreadState.Running" />.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available to start this thread. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600164E RID: 5710 RVA: 0x00058CF0 File Offset: 0x00056EF0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		/// <summary>Causes the operating system to change the state of the current instance to <see cref="F:System.Threading.ThreadState.Running" />, and optionally supplies an object containing data to be used by the method the thread executes.</summary>
		/// <param name="parameter">An object that contains data to be used by the method the thread executes.</param>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available to start this thread. </exception>
		/// <exception cref="T:System.InvalidOperationException">This thread was created using a <see cref="T:System.Threading.ThreadStart" /> delegate instead of a <see cref="T:System.Threading.ParameterizedThreadStart" /> delegate.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600164F RID: 5711 RVA: 0x00058D08 File Offset: 0x00056F08
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Start(object parameter)
		{
			if (this.m_Delegate is ThreadStart)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The thread was created with a ThreadStart delegate that does not accept a parameter."));
			}
			this.m_ThreadStartArg = parameter;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.Start(ref stackCrawlMark);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00058D44 File Offset: 0x00056F44
		private void Start(ref StackCrawlMark stackMark)
		{
			if (this.m_Delegate != null)
			{
				ThreadHelper threadHelper = (ThreadHelper)this.m_Delegate.Target;
				ExecutionContext executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx);
				threadHelper.SetExecutionContextHelper(executionContext);
			}
			object obj = null;
			this.StartInternal(obj, ref stackMark);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00058D81 File Offset: 0x00056F81
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext.Reader GetExecutionContextReader()
		{
			return new ExecutionContext.Reader(this.m_ExecutionContext);
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00058D8E File Offset: 0x00056F8E
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x00058D99 File Offset: 0x00056F99
		internal bool ExecutionContextBelongsToCurrentScope
		{
			get
			{
				return !this.m_ExecutionContextBelongsToOuterScope;
			}
			set
			{
				this.m_ExecutionContextBelongsToOuterScope = !value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Threading.ExecutionContext" /> object that contains information about the various contexts of the current thread. </summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object that consolidates context information for the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00058DA8 File Offset: 0x00056FA8
		public ExecutionContext ExecutionContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				ExecutionContext executionContext;
				if (this == Thread.CurrentThread)
				{
					executionContext = this.GetMutableExecutionContext();
				}
				else
				{
					executionContext = this.m_ExecutionContext;
				}
				return executionContext;
			}
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00058DD0 File Offset: 0x00056FD0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal ExecutionContext GetMutableExecutionContext()
		{
			if (this.m_ExecutionContext == null)
			{
				this.m_ExecutionContext = new ExecutionContext();
			}
			else if (!this.ExecutionContextBelongsToCurrentScope)
			{
				ExecutionContext executionContext = this.m_ExecutionContext.CreateMutableCopy();
				this.m_ExecutionContext = executionContext;
			}
			this.ExecutionContextBelongsToCurrentScope = true;
			return this.m_ExecutionContext;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00058E1A File Offset: 0x0005701A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value;
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00058E2A File Offset: 0x0005702A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetExecutionContext(ExecutionContext.Reader value, bool belongsToCurrentScope)
		{
			this.m_ExecutionContext = value.DangerousGetRawExecutionContext();
			this.ExecutionContextBelongsToCurrentScope = belongsToCurrentScope;
		}

		/// <summary>Applies a captured <see cref="T:System.Threading.CompressedStack" /> to the current thread. </summary>
		/// <param name="stack">The <see cref="T:System.Threading.CompressedStack" /> object to be applied to the current thread.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x06001658 RID: 5720 RVA: 0x00058E40 File Offset: 0x00057040
		[Obsolete("Thread.SetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public void SetCompressedStack(CompressedStack stack)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Use CompressedStack.(Capture/Run) or ExecutionContext.(Capture/Run) APIs instead."));
		}

		/// <summary>Returns a <see cref="T:System.Threading.CompressedStack" /> object that can be used to capture the stack for the current thread. </summary>
		/// <returns>None. </returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" />
		/// </PermissionSet>
		// Token: 0x06001659 RID: 5721 RVA: 0x00058E40 File Offset: 0x00057040
		[Obsolete("Thread.GetCompressedStack is no longer supported. Please use the System.Threading.CompressedStack class")]
		public CompressedStack GetCompressedStack()
		{
			throw new InvalidOperationException(Environment.GetResourceString("Use CompressedStack.(Capture/Run) or ExecutionContext.(Capture/Run) APIs instead."));
		}

		/// <summary>Cancels an <see cref="M:System.Threading.Thread.Abort(System.Object)" /> requested for the current thread.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">Abort was not invoked on the current thread. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required security permission for the current thread. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600165A RID: 5722 RVA: 0x00058E51 File Offset: 0x00057051
		public static void ResetAbort()
		{
			Thread currentThread = Thread.CurrentThread;
			if ((currentThread.ThreadState & ThreadState.AbortRequested) == ThreadState.Running)
			{
				throw new ThreadStateException(Environment.GetResourceString("Unable to reset abort because no abort was requested."));
			}
			currentThread.ResetAbortNative();
			currentThread.ClearAbortReason();
		}

		// Token: 0x0600165B RID: 5723
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResetAbortNative();

		/// <summary>Either suspends the thread, or if the thread is already suspended, has no effect.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started or is dead. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600165C RID: 5724 RVA: 0x00058E81 File Offset: 0x00057081
		[Obsolete("Thread.Suspend has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public void Suspend()
		{
			this.SuspendInternal();
		}

		// Token: 0x0600165D RID: 5725
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SuspendInternal();

		/// <summary>Resumes a thread that has been suspended.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started, is dead, or is not in the suspended state. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600165E RID: 5726 RVA: 0x00058E89 File Offset: 0x00057089
		[Obsolete("Thread.Resume has been deprecated.  Please use other classes in System.Threading, such as Monitor, Mutex, Event, and Semaphore, to synchronize Threads or protect resources.  http://go.microsoft.com/fwlink/?linkid=14202", false)]
		public void Resume()
		{
			this.ResumeInternal();
		}

		// Token: 0x0600165F RID: 5727
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResumeInternal();

		/// <summary>Interrupts a thread that is in the WaitSleepJoin thread state.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate <see cref="T:System.Security.Permissions.SecurityPermission" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x06001660 RID: 5728 RVA: 0x00058E91 File Offset: 0x00057091
		public void Interrupt()
		{
			this.InterruptInternal();
		}

		// Token: 0x06001661 RID: 5729
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InterruptInternal();

		/// <summary>Gets or sets a value indicating the scheduling priority of a thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ThreadPriority" /> values. The default value is Normal.</returns>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has reached a final state, such as <see cref="F:System.Threading.ThreadState.Aborted" />. </exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is not a valid ThreadPriority value. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x00058E99 File Offset: 0x00057099
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x00058EA1 File Offset: 0x000570A1
		public ThreadPriority Priority
		{
			get
			{
				return (ThreadPriority)this.GetPriorityNative();
			}
			set
			{
				this.SetPriorityNative((int)value);
			}
		}

		// Token: 0x06001664 RID: 5732
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetPriorityNative();

		// Token: 0x06001665 RID: 5733
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPriorityNative(int priority);

		// Token: 0x06001666 RID: 5734
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool JoinInternal(int millisecondsTimeout);

		/// <summary>Blocks the calling thread until a thread terminates, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <exception cref="T:System.Threading.ThreadStateException">The caller attempted to join a thread that is in the <see cref="F:System.Threading.ThreadState.Unstarted" /> state. </exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread is interrupted while waiting. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001667 RID: 5735 RVA: 0x00058EAA File Offset: 0x000570AA
		public void Join()
		{
			this.JoinInternal(-1);
		}

		/// <summary>Blocks the calling thread until a thread terminates or the specified time elapses, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <returns>true if the thread has terminated; false if the thread has not terminated after the amount of time specified by the <paramref name="millisecondsTimeout" /> parameter has elapsed.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for the thread to terminate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has not been started. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001668 RID: 5736 RVA: 0x00058EB4 File Offset: 0x000570B4
		public bool Join(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.JoinInternal(millisecondsTimeout);
		}

		/// <summary>Blocks the calling thread until a thread terminates or the specified time elapses, while continuing to perform standard COM and SendMessage pumping.</summary>
		/// <returns>true if the thread terminated; false if the thread has not terminated after the amount of time specified by the <paramref name="timeout" /> parameter has elapsed.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> set to the amount of time to wait for the thread to terminate. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds, or is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The caller attempted to join a thread that is in the <see cref="F:System.Threading.ThreadState.Unstarted" /> state. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001669 RID: 5737 RVA: 0x00058ED8 File Offset: 0x000570D8
		public bool Join(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.Join((int)num);
		}

		// Token: 0x0600166A RID: 5738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SleepInternal(int millisecondsTimeout);

		/// <summary>Suspends the current thread for a specified time.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds for which the thread is blocked. Specify zero (0) to indicate that this thread should be suspended to allow other waiting threads to execute. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to block the thread indefinitely. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out value is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600166B RID: 5739 RVA: 0x00058F19 File Offset: 0x00057119
		public static void Sleep(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			Thread.SleepInternal(millisecondsTimeout);
		}

		/// <summary>Blocks the current thread for a specified time.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> set to the amount of time for which the thread is blocked. Specify zero to indicate that this thread should be suspended to allow other waiting threads to execute. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to block the thread indefinitely. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> in milliseconds, or is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600166C RID: 5740 RVA: 0x00058F3C File Offset: 0x0005713C
		public static void Sleep(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			Thread.Sleep((int)num);
		}

		// Token: 0x0600166D RID: 5741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool YieldInternal();

		/// <summary>Causes the calling thread to yield execution to another thread that is ready to run on the current processor. The operating system selects the thread to yield to.</summary>
		/// <returns>true if the operating system switched execution to another thread; otherwise, false.</returns>
		// Token: 0x0600166E RID: 5742 RVA: 0x00058F7C File Offset: 0x0005717C
		public static bool Yield()
		{
			return Thread.YieldInternal();
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00058F84 File Offset: 0x00057184
		private void SetStartHelper(Delegate start, int maxStackSize)
		{
			maxStackSize = Thread.GetProcessDefaultStackSize(maxStackSize);
			ThreadHelper threadHelper = new ThreadHelper(start);
			if (start is ThreadStart)
			{
				this.SetStart(new ThreadStart(threadHelper.ThreadStart), maxStackSize);
				return;
			}
			this.SetStart(new ParameterizedThreadStart(threadHelper.ThreadStart), maxStackSize);
		}

		/// <summary>Allocates an unnamed data slot on all the threads. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>The allocated named data slot on all threads.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001670 RID: 5744 RVA: 0x00058FCF File Offset: 0x000571CF
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Thread.LocalDataStoreManager.AllocateDataSlot();
		}

		/// <summary>Allocates a named data slot on all threads. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>The allocated named data slot on all threads.</returns>
		/// <param name="name">The name of the data slot to be allocated. </param>
		/// <exception cref="T:System.ArgumentException">A named data slot with the specified name already exists.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001671 RID: 5745 RVA: 0x00058FDB File Offset: 0x000571DB
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.AllocateNamedDataSlot(name);
		}

		/// <summary>Looks up a named data slot. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>A <see cref="T:System.LocalDataStoreSlot" /> allocated for this thread.</returns>
		/// <param name="name">The name of the local data slot. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001672 RID: 5746 RVA: 0x00058FE8 File Offset: 0x000571E8
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Thread.LocalDataStoreManager.GetNamedDataSlot(name);
		}

		/// <summary>Eliminates the association between a name and a slot, for all threads in the process. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="name">The name of the data slot to be freed. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001673 RID: 5747 RVA: 0x00058FF5 File Offset: 0x000571F5
		public static void FreeNamedDataSlot(string name)
		{
			Thread.LocalDataStoreManager.FreeNamedDataSlot(name);
		}

		/// <summary>Retrieves the value from the specified slot on the current thread, within the current thread's current domain. For better performance, use fields that are marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <returns>The retrieved value.</returns>
		/// <param name="slot">The <see cref="T:System.LocalDataStoreSlot" /> from which to get the value. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001674 RID: 5748 RVA: 0x00059004 File Offset: 0x00057204
		public static object GetData(LocalDataStoreSlot slot)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				Thread.LocalDataStoreManager.ValidateSlot(slot);
				return null;
			}
			return localDataStoreHolder.Store.GetData(slot);
		}

		/// <summary>Sets the data in the specified slot on the currently running thread, for that thread's current domain. For better performance, use fields marked with the <see cref="T:System.ThreadStaticAttribute" /> attribute instead.</summary>
		/// <param name="slot">The <see cref="T:System.LocalDataStoreSlot" /> in which to set the value. </param>
		/// <param name="data">The value to be set. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001675 RID: 5749 RVA: 0x00059034 File Offset: 0x00057234
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			LocalDataStoreHolder localDataStoreHolder = Thread.s_LocalDataStore;
			if (localDataStoreHolder == null)
			{
				localDataStoreHolder = Thread.LocalDataStoreManager.CreateLocalDataStore();
				Thread.s_LocalDataStore = localDataStoreHolder;
			}
			localDataStoreHolder.Store.SetData(slot, data);
		}

		/// <summary>Gets or sets the current culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>An object that represents the current culture.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null. </exception>
		/// <exception cref="T:System.ArgumentException">The property is set to a culture name that cannot be used to locate a resource file. Resource filenames must include only letters, numbers, hyphens or underscores.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x00059068 File Offset: 0x00057268
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x00059088 File Offset: 0x00057288
		public CultureInfo CurrentUICulture
		{
			get
			{
				if (AppDomain.IsAppXModel())
				{
					return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentUICultureNoAppX();
				}
				return this.GetCurrentUICultureNoAppX();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				CultureInfo.VerifyCultureName(value, true);
				if (AppDomain.IsAppXModel())
				{
					CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value);
					return;
				}
				if (Thread.m_CurrentUICulture == null && Thread.m_CurrentCulture == null)
				{
					Thread.nativeInitCultureAccessors();
				}
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentUICulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentUICulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentUICulture)), null);
					}
					Thread.s_asyncLocalCurrentUICulture.Value = value;
					return;
				}
				Thread.m_CurrentUICulture = value;
			}
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0005910C File Offset: 0x0005730C
		internal CultureInfo GetCurrentUICultureNoAppX()
		{
			if (Thread.m_CurrentUICulture != null)
			{
				return Thread.m_CurrentUICulture;
			}
			CultureInfo defaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
			if (defaultThreadCurrentUICulture == null)
			{
				return CultureInfo.UserDefaultUICulture;
			}
			return defaultThreadCurrentUICulture;
		}

		/// <summary>Gets or sets the culture for the current thread.</summary>
		/// <returns>An object that represents the culture for the current thread.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00059136 File Offset: 0x00057336
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x00059158 File Offset: 0x00057358
		public CultureInfo CurrentCulture
		{
			get
			{
				if (AppDomain.IsAppXModel())
				{
					return CultureInfo.GetCultureInfoForUserPreferredLanguageInAppX() ?? this.GetCurrentCultureNoAppX();
				}
				return this.GetCurrentCultureNoAppX();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (AppDomain.IsAppXModel())
				{
					CultureInfo.SetCultureInfoForUserPreferredLanguageInAppX(value);
					return;
				}
				if (Thread.m_CurrentCulture == null && Thread.m_CurrentUICulture == null)
				{
					Thread.nativeInitCultureAccessors();
				}
				if (!AppContextSwitches.NoAsyncCurrentCulture)
				{
					if (Thread.s_asyncLocalCurrentCulture == null)
					{
						Interlocked.CompareExchange<AsyncLocal<CultureInfo>>(ref Thread.s_asyncLocalCurrentCulture, new AsyncLocal<CultureInfo>(new Action<AsyncLocalValueChangedArgs<CultureInfo>>(Thread.AsyncLocalSetCurrentCulture)), null);
					}
					Thread.s_asyncLocalCurrentCulture.Value = value;
					return;
				}
				Thread.m_CurrentCulture = value;
			}
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000591D4 File Offset: 0x000573D4
		private CultureInfo GetCurrentCultureNoAppX()
		{
			if (Thread.m_CurrentCulture != null)
			{
				return Thread.m_CurrentCulture;
			}
			CultureInfo defaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
			if (defaultThreadCurrentCulture == null)
			{
				return CultureInfo.UserDefaultCulture;
			}
			return defaultThreadCurrentCulture;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x000591FE File Offset: 0x000573FE
		private static void nativeInitCultureAccessors()
		{
			Thread.m_CurrentCulture = CultureInfo.ConstructCurrentCulture();
			Thread.m_CurrentUICulture = CultureInfo.ConstructCurrentUICulture();
		}

		/// <summary>Synchronizes memory access as follows: The processor executing the current thread cannot reorder instructions in such a way that memory accesses prior to the call to <see cref="M:System.Threading.Thread.MemoryBarrier" /> execute after memory accesses that follow the call to <see cref="M:System.Threading.Thread.MemoryBarrier" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600167D RID: 5757
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrier();

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00059214 File Offset: 0x00057414
		private static LocalDataStoreMgr LocalDataStoreManager
		{
			get
			{
				if (Thread.s_LocalDataStoreMgr == null)
				{
					Interlocked.CompareExchange<LocalDataStoreMgr>(ref Thread.s_LocalDataStoreMgr, new LocalDataStoreMgr(), null);
				}
				return Thread.s_LocalDataStoreMgr;
			}
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600167F RID: 5759 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _Thread.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06001680 RID: 5760 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _Thread.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06001681 RID: 5761 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _Thread.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06001682 RID: 5762 RVA: 0x00033EF4 File Offset: 0x000320F4
		void _Thread.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001683 RID: 5763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ConstructInternalThread();

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00059233 File Offset: 0x00057433
		private InternalThread Internal
		{
			get
			{
				if (this.internal_thread == null)
				{
					this.ConstructInternalThread();
				}
				return this.internal_thread;
			}
		}

		// Token: 0x06001685 RID: 5765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] ByteArrayToRootDomain(byte[] arr);

		// Token: 0x06001686 RID: 5766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] ByteArrayToCurrentDomain(byte[] arr);

		/// <summary>Gets the current context in which the thread is executing.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Contexts.Context" /> representing the current thread context.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00059249 File Offset: 0x00057449
		public static Context CurrentContext
		{
			get
			{
				return AppDomain.InternalGetContext();
			}
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00059250 File Offset: 0x00057450
		private static void DeserializePrincipal(Thread th)
		{
			MemoryStream memoryStream = new MemoryStream(Thread.ByteArrayToCurrentDomain(th.Internal._serialized_principal));
			int num = memoryStream.ReadByte();
			if (num == 0)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				th.principal = (IPrincipal)binaryFormatter.Deserialize(memoryStream);
				th.principal_version = th.Internal._serialized_principal_version;
				return;
			}
			if (num == 1)
			{
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				string text = binaryReader.ReadString();
				string text2 = binaryReader.ReadString();
				int num2 = binaryReader.ReadInt32();
				string[] array = null;
				if (num2 >= 0)
				{
					array = new string[num2];
					for (int i = 0; i < num2; i++)
					{
						array[i] = binaryReader.ReadString();
					}
				}
				th.principal = new GenericPrincipal(new GenericIdentity(text, text2), array);
				return;
			}
			if (num == 2 || num == 3)
			{
				string[] array2 = ((num == 2) ? null : new string[0]);
				th.principal = new GenericPrincipal(new GenericIdentity("", ""), array2);
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00059344 File Offset: 0x00057544
		private static void SerializePrincipal(Thread th, IPrincipal value)
		{
			MemoryStream memoryStream = new MemoryStream();
			bool flag = false;
			if (value.GetType() == typeof(GenericPrincipal))
			{
				GenericPrincipal genericPrincipal = (GenericPrincipal)value;
				if (genericPrincipal.Identity != null && genericPrincipal.Identity.GetType() == typeof(GenericIdentity))
				{
					GenericIdentity genericIdentity = (GenericIdentity)genericPrincipal.Identity;
					if (genericIdentity.Name == "" && genericIdentity.AuthenticationType == "")
					{
						if (genericPrincipal.Roles == null)
						{
							memoryStream.WriteByte(2);
							flag = true;
						}
						else if (genericPrincipal.Roles.Length == 0)
						{
							memoryStream.WriteByte(3);
							flag = true;
						}
					}
					else
					{
						memoryStream.WriteByte(1);
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(genericPrincipal.Identity.Name);
						binaryWriter.Write(genericPrincipal.Identity.AuthenticationType);
						string[] roles = genericPrincipal.Roles;
						if (roles == null)
						{
							binaryWriter.Write(-1);
						}
						else
						{
							binaryWriter.Write(roles.Length);
							foreach (string text in roles)
							{
								binaryWriter.Write(text);
							}
						}
						binaryWriter.Flush();
						flag = true;
					}
				}
			}
			if (!flag)
			{
				memoryStream.WriteByte(0);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				try
				{
					binaryFormatter.Serialize(memoryStream, value);
				}
				catch
				{
				}
			}
			th.Internal._serialized_principal = Thread.ByteArrayToRootDomain(memoryStream.ToArray());
		}

		/// <summary>Gets or sets the thread's current principal (for role-based security).</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> value representing the security context.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the permission required to set the principal. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x000594D0 File Offset: 0x000576D0
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x00059584 File Offset: 0x00057784
		public static IPrincipal CurrentPrincipal
		{
			get
			{
				Thread currentThread = Thread.CurrentThread;
				IPrincipal principal = currentThread.GetExecutionContextReader().LogicalCallContext.Principal;
				if (principal != null)
				{
					return principal;
				}
				if (currentThread.principal_version != currentThread.Internal._serialized_principal_version)
				{
					currentThread.principal = null;
				}
				if (currentThread.principal != null)
				{
					return currentThread.principal;
				}
				if (currentThread.Internal._serialized_principal != null)
				{
					try
					{
						Thread.DeserializePrincipal(currentThread);
						return currentThread.principal;
					}
					catch
					{
					}
				}
				currentThread.principal = Thread.GetDomain().DefaultPrincipal;
				currentThread.principal_version = currentThread.Internal._serialized_principal_version;
				return currentThread.principal;
			}
			set
			{
				Thread currentThread = Thread.CurrentThread;
				currentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
				if (value != Thread.GetDomain().DefaultPrincipal)
				{
					currentThread.Internal._serialized_principal_version++;
					try
					{
						Thread.SerializePrincipal(currentThread, value);
					}
					catch (Exception)
					{
						currentThread.Internal._serialized_principal = null;
					}
					currentThread.principal_version = currentThread.Internal._serialized_principal_version;
				}
				else
				{
					currentThread.Internal._serialized_principal = null;
				}
				currentThread.principal = value;
			}
		}

		/// <summary>Returns the current domain in which the current thread is running.</summary>
		/// <returns>An <see cref="T:System.AppDomain" /> representing the current application domain of the running thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600168C RID: 5772 RVA: 0x00059618 File Offset: 0x00057818
		public static AppDomain GetDomain()
		{
			return AppDomain.CurrentDomain;
		}

		// Token: 0x0600168D RID: 5773
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentThread_icall(ref Thread thread);

		// Token: 0x0600168E RID: 5774 RVA: 0x00059620 File Offset: 0x00057820
		private static Thread GetCurrentThread()
		{
			Thread thread = null;
			Thread.GetCurrentThread_icall(ref thread);
			return thread;
		}

		/// <summary>Gets the currently running thread.</summary>
		/// <returns>A <see cref="T:System.Threading.Thread" /> that is the representation of the currently running thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x00059638 File Offset: 0x00057838
		public static Thread CurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				Thread thread = Thread.current_thread;
				if (thread != null)
				{
					return thread;
				}
				return Thread.GetCurrentThread();
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00059655 File Offset: 0x00057855
		internal static int CurrentThreadId
		{
			get
			{
				return (int)Thread.CurrentThread.internal_thread.thread_id;
			}
		}

		/// <summary>Returns a unique application domain identifier.</summary>
		/// <returns>A 32-bit signed integer uniquely identifying the application domain.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001691 RID: 5777
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetDomainID();

		// Token: 0x06001692 RID: 5778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Thread_internal(MulticastDelegate start);

		// Token: 0x06001693 RID: 5779 RVA: 0x00059667 File Offset: 0x00057867
		private Thread(InternalThread it)
		{
			this.internal_thread = it;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00059678 File Offset: 0x00057878
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~Thread()
		{
		}

		/// <summary>Gets or sets the apartment state of this thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values. The initial value is Unknown.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to a state that is not a valid apartment state (a state other than single-threaded apartment (STA) or multithreaded apartment (MTA)). </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x000596A0 File Offset: 0x000578A0
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x000596B4 File Offset: 0x000578B4
		[Obsolete("Deprecated in favor of GetApartmentState, SetApartmentState and TrySetApartmentState.")]
		public ApartmentState ApartmentState
		{
			get
			{
				this.ValidateThreadState();
				return (ApartmentState)this.Internal.apartment_state;
			}
			set
			{
				this.ValidateThreadState();
				this.TrySetApartmentState(value);
			}
		}

		/// <summary>Gets a value indicating whether or not a thread belongs to the managed thread pool.</summary>
		/// <returns>true if this thread belongs to the managed thread pool; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x000596C5 File Offset: 0x000578C5
		public bool IsThreadPoolThread
		{
			get
			{
				return this.IsThreadPoolThreadInternal;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000596CD File Offset: 0x000578CD
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x000596DA File Offset: 0x000578DA
		internal bool IsThreadPoolThreadInternal
		{
			get
			{
				return this.Internal.threadpool_thread;
			}
			set
			{
				this.Internal.threadpool_thread = value;
			}
		}

		/// <summary>Gets a value indicating the execution status of the current thread.</summary>
		/// <returns>true if this thread has been started and has not terminated normally or aborted; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x000596E8 File Offset: 0x000578E8
		public bool IsAlive
		{
			get
			{
				ThreadState state = Thread.GetState(this.Internal);
				return (state & ThreadState.Aborted) == ThreadState.Running && (state & ThreadState.Stopped) == ThreadState.Running && (state & ThreadState.Unstarted) == ThreadState.Running;
			}
		}

		/// <summary>Gets or sets a value indicating whether or not a thread is a background thread.</summary>
		/// <returns>true if this thread is or is to become a background thread; otherwise, false.</returns>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread is dead. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00059718 File Offset: 0x00057918
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x00059725 File Offset: 0x00057925
		public bool IsBackground
		{
			get
			{
				return (this.ValidateThreadState() & ThreadState.Background) > ThreadState.Running;
			}
			set
			{
				this.ValidateThreadState();
				if (value)
				{
					Thread.SetState(this.Internal, ThreadState.Background);
					return;
				}
				Thread.ClrState(this.Internal, ThreadState.Background);
			}
		}

		// Token: 0x0600169D RID: 5789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetName_internal(InternalThread thread);

		// Token: 0x0600169E RID: 5790
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetName_icall(InternalThread thread, char* name, int nameLength);

		// Token: 0x0600169F RID: 5791 RVA: 0x0005974C File Offset: 0x0005794C
		private unsafe static void SetName_internal(InternalThread thread, string name)
		{
			fixed (string text = name)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				Thread.SetName_icall(thread, ptr, (name != null) ? name.Length : 0);
			}
		}

		/// <summary>Gets or sets the name of the thread.</summary>
		/// <returns>A string containing the name of the thread, or null if no name was set.</returns>
		/// <exception cref="T:System.InvalidOperationException">A set operation was requested, and the Name property has already been set. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0005977E File Offset: 0x0005797E
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x0005978B File Offset: 0x0005798B
		public string Name
		{
			get
			{
				return Thread.GetName_internal(this.Internal);
			}
			set
			{
				Thread.SetName_internal(this.Internal, value);
			}
		}

		/// <summary>Gets a value containing the states of the current thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ThreadState" /> values indicating the state of the current thread. The initial value is Unstarted.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00059799 File Offset: 0x00057999
		public ThreadState ThreadState
		{
			get
			{
				return Thread.GetState(this.Internal);
			}
		}

		// Token: 0x060016A3 RID: 5795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Abort_internal(InternalThread thread, object stateInfo);

		/// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the thread on which it is invoked, to begin the process of terminating the thread. Calling this method usually terminates the thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread that is being aborted is currently suspended.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x060016A4 RID: 5796 RVA: 0x000597A6 File Offset: 0x000579A6
		public void Abort()
		{
			Thread.Abort_internal(this.Internal, null);
		}

		/// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the thread on which it is invoked, to begin the process of terminating the thread while also providing exception information about the thread termination. Calling this method usually terminates the thread.</summary>
		/// <param name="stateInfo">An object that contains application-specific information, such as state, which can be used by the thread being aborted. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread that is being aborted is currently suspended.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x060016A5 RID: 5797 RVA: 0x000597B4 File Offset: 0x000579B4
		public void Abort(object stateInfo)
		{
			Thread.Abort_internal(this.Internal, stateInfo);
		}

		// Token: 0x060016A6 RID: 5798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetAbortExceptionState();

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000597C2 File Offset: 0x000579C2
		internal object AbortReason
		{
			get
			{
				return this.GetAbortExceptionState();
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00002C89 File Offset: 0x00000E89
		private void ClearAbortReason()
		{
		}

		// Token: 0x060016A9 RID: 5801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SpinWait_nop();

		/// <summary>Causes a thread to wait the number of times defined by the <paramref name="iterations" /> parameter.</summary>
		/// <param name="iterations">A 32-bit signed integer that defines how long a thread is to wait. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016AA RID: 5802 RVA: 0x000597CA File Offset: 0x000579CA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SpinWait(int iterations)
		{
			if (iterations < 0)
			{
				return;
			}
			while (iterations-- > 0)
			{
				Thread.SpinWait_nop();
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000597DF File Offset: 0x000579DF
		private void StartInternal(object principal, ref StackCrawlMark stackMark)
		{
			this.Internal._serialized_principal = Thread.CurrentThread.Internal._serialized_principal;
			if (!this.Thread_internal(this.m_Delegate))
			{
				throw new SystemException("Thread creation failed.");
			}
			this.m_ThreadStartArg = null;
		}

		// Token: 0x060016AC RID: 5804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetState(InternalThread thread, ThreadState set);

		// Token: 0x060016AD RID: 5805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClrState(InternalThread thread, ThreadState clr);

		// Token: 0x060016AE RID: 5806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ThreadState GetState(InternalThread thread);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016AF RID: 5807
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte VolatileRead(ref byte address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B0 RID: 5808
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double VolatileRead(ref double address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B1 RID: 5809
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short VolatileRead(ref short address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B2 RID: 5810
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int VolatileRead(ref int address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B3 RID: 5811
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long VolatileRead(ref long address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B4 RID: 5812
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr VolatileRead(ref IntPtr address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B5 RID: 5813
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object VolatileRead(ref object address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B6 RID: 5814
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte VolatileRead(ref sbyte address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B7 RID: 5815
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float VolatileRead(ref float address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B8 RID: 5816
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort VolatileRead(ref ushort address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B9 RID: 5817
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint VolatileRead(ref uint address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016BA RID: 5818
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong VolatileRead(ref ulong address);

		/// <summary>Reads the value of a field. The value is the latest written by any processor in a computer, regardless of the number of processors or the state of processor cache.</summary>
		/// <returns>The latest value written to the field by any processor.</returns>
		/// <param name="address">The field to be read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016BB RID: 5819
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr VolatileRead(ref UIntPtr address);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016BC RID: 5820
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref byte address, byte value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016BD RID: 5821
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref double address, double value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016BE RID: 5822
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref short address, short value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016BF RID: 5823
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref int address, int value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C0 RID: 5824
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref long address, long value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C1 RID: 5825
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref IntPtr address, IntPtr value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C2 RID: 5826
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref object address, object value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C3 RID: 5827
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref sbyte address, sbyte value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C4 RID: 5828
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref float address, float value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C5 RID: 5829
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref ushort address, ushort value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C6 RID: 5830
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref uint address, uint value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C7 RID: 5831
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref ulong address, ulong value);

		/// <summary>Writes a value to a field immediately, so that the value is visible to all processors in the computer.</summary>
		/// <param name="address">The field to which the value is to be written. </param>
		/// <param name="value">The value to be written. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016C8 RID: 5832
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void VolatileWrite(ref UIntPtr address, UIntPtr value);

		// Token: 0x060016C9 RID: 5833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SystemMaxStackStize();

		// Token: 0x060016CA RID: 5834 RVA: 0x0005981C File Offset: 0x00057A1C
		private static int GetProcessDefaultStackSize(int maxStackSize)
		{
			if (maxStackSize == 0)
			{
				return 0;
			}
			if (maxStackSize < 131072)
			{
				return 131072;
			}
			int pageSize = Environment.GetPageSize();
			if (maxStackSize % pageSize != 0)
			{
				maxStackSize = maxStackSize / (pageSize - 1) * pageSize;
			}
			return Math.Min(maxStackSize, Thread.SystemMaxStackStize());
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0005985B File Offset: 0x00057A5B
		private void SetStart(MulticastDelegate start, int maxStackSize)
		{
			this.m_Delegate = start;
			this.Internal.stack_size = maxStackSize;
		}

		/// <summary>Gets a unique identifier for the current managed thread.</summary>
		/// <returns>An integer that represents a unique identifier for this managed thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00059870 File Offset: 0x00057A70
		public int ManagedThreadId
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.Internal.managed_id;
			}
		}

		/// <summary>Notifies a host that execution is about to enter a region of code in which the effects of a thread abort or unhandled exception might jeopardize other tasks in the application domain.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016CD RID: 5837 RVA: 0x0005987D File Offset: 0x00057A7D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginCriticalRegion()
		{
			Thread.CurrentThread.Internal.critical_region_level++;
		}

		/// <summary>Notifies a host that execution is about to enter a region of code in which the effects of a thread abort or unhandled exception are limited to the current task.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016CE RID: 5838 RVA: 0x0005989A File Offset: 0x00057A9A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EndCriticalRegion()
		{
			Thread.CurrentThread.Internal.critical_region_level--;
		}

		/// <summary>Notifies a host that managed code is about to execute instructions that depend on the identity of the current physical operating system thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x060016CF RID: 5839 RVA: 0x00002C89 File Offset: 0x00000E89
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void BeginThreadAffinity()
		{
		}

		/// <summary>Notifies a host that managed code has finished executing instructions that depend on the identity of the current physical operating system thread.</summary>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x060016D0 RID: 5840 RVA: 0x00002C89 File Offset: 0x00000E89
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void EndThreadAffinity()
		{
		}

		/// <summary>Returns an <see cref="T:System.Threading.ApartmentState" /> value indicating the apartment state.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values indicating the apartment state of the managed thread. The default is <see cref="F:System.Threading.ApartmentState.Unknown" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016D1 RID: 5841 RVA: 0x000596A0 File Offset: 0x000578A0
		public ApartmentState GetApartmentState()
		{
			this.ValidateThreadState();
			return (ApartmentState)this.Internal.apartment_state;
		}

		/// <summary>Sets the apartment state of a thread before it is started.</summary>
		/// <param name="state">The new apartment state.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid apartment state.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <exception cref="T:System.InvalidOperationException">The apartment state has already been initialized.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016D2 RID: 5842 RVA: 0x000598B7 File Offset: 0x00057AB7
		public void SetApartmentState(ApartmentState state)
		{
			if (!this.TrySetApartmentState(state))
			{
				throw new InvalidOperationException("Failed to set the specified COM apartment state.");
			}
		}

		/// <summary>Sets the apartment state of a thread before it is started.</summary>
		/// <returns>true if the apartment state is set; otherwise, false.</returns>
		/// <param name="state">The new apartment state.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid apartment state.</exception>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread has already been started.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016D3 RID: 5843 RVA: 0x000598D0 File Offset: 0x00057AD0
		public bool TrySetApartmentState(ApartmentState state)
		{
			if ((this.ThreadState & ThreadState.Unstarted) == ThreadState.Running)
			{
				throw new ThreadStateException("Thread was in an invalid state for the operation being executed.");
			}
			if (this.Internal.apartment_state != 2 && (ApartmentState)this.Internal.apartment_state != state)
			{
				return false;
			}
			this.Internal.apartment_state = (byte)state;
			return true;
		}

		/// <summary>Returns a hash code for the current thread.</summary>
		/// <returns>An integer hash code value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016D4 RID: 5844 RVA: 0x0005991E File Offset: 0x00057B1E
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.ManagedThreadId;
		}

		// Token: 0x060016D5 RID: 5845
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetStackTraces(out Thread[] threads, out object[] stack_frames);

		// Token: 0x060016D6 RID: 5846 RVA: 0x00059928 File Offset: 0x00057B28
		internal static Dictionary<Thread, StackTrace> Mono_GetStackTraces()
		{
			Thread[] array;
			object[] array2;
			Thread.GetStackTraces(out array, out array2);
			Dictionary<Thread, StackTrace> dictionary = new Dictionary<Thread, StackTrace>();
			for (int i = 0; i < array.Length; i++)
			{
				dictionary[array[i]] = new StackTrace((StackFrame[])array2[i]);
			}
			return dictionary;
		}

		/// <summary>Turns off automatic cleanup of runtime callable wrappers (RCW) for the current thread. </summary>
		// Token: 0x060016D7 RID: 5847 RVA: 0x000145B3 File Offset: 0x000127B3
		public void DisableComObjectEagerCleanup()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00059969 File Offset: 0x00057B69
		private ThreadState ValidateThreadState()
		{
			ThreadState state = Thread.GetState(this.Internal);
			if ((state & ThreadState.Stopped) != ThreadState.Running)
			{
				throw new ThreadStateException("Thread is dead; state can not be accessed.");
			}
			return state;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00059987 File Offset: 0x00057B87
		public static int GetCurrentProcessorId()
		{
			return RuntimeThread.GetCurrentProcessorId();
		}

		// Token: 0x04000ADF RID: 2783
		private static LocalDataStoreMgr s_LocalDataStoreMgr;

		// Token: 0x04000AE0 RID: 2784
		[ThreadStatic]
		private static LocalDataStoreHolder s_LocalDataStore;

		// Token: 0x04000AE1 RID: 2785
		[ThreadStatic]
		internal static CultureInfo m_CurrentCulture;

		// Token: 0x04000AE2 RID: 2786
		[ThreadStatic]
		internal static CultureInfo m_CurrentUICulture;

		// Token: 0x04000AE3 RID: 2787
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentCulture;

		// Token: 0x04000AE4 RID: 2788
		private static AsyncLocal<CultureInfo> s_asyncLocalCurrentUICulture;

		// Token: 0x04000AE5 RID: 2789
		private InternalThread internal_thread;

		// Token: 0x04000AE6 RID: 2790
		private object m_ThreadStartArg;

		// Token: 0x04000AE7 RID: 2791
		private object pending_exception;

		// Token: 0x04000AE8 RID: 2792
		[ThreadStatic]
		private static Thread current_thread;

		// Token: 0x04000AE9 RID: 2793
		private MulticastDelegate m_Delegate;

		// Token: 0x04000AEA RID: 2794
		private ExecutionContext m_ExecutionContext;

		// Token: 0x04000AEB RID: 2795
		private bool m_ExecutionContextBelongsToOuterScope;

		// Token: 0x04000AEC RID: 2796
		private IPrincipal principal;

		// Token: 0x04000AED RID: 2797
		private int principal_version;
	}
}
