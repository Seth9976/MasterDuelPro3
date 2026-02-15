using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Internal.Runtime.Augments;
using Internal.Threading.Tasks.Tracing;

namespace System.Threading.Tasks
{
	/// <summary>Represents an asynchronous operation.</summary>
	// Token: 0x020002AE RID: 686
	[DebuggerTypeProxy(typeof(SystemThreadingTasks_TaskDebugView))]
	[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}")]
	public class Task : IThreadPoolWorkItem, IAsyncResult, IDisposable
	{
		// Token: 0x060018D4 RID: 6356 RVA: 0x0005EC68 File Offset: 0x0005CE68
		internal Task(bool canceled, TaskCreationOptions creationOptions, CancellationToken ct)
		{
			if (canceled)
			{
				this.m_stateFlags = (int)((TaskCreationOptions)5242880 | creationOptions);
				Task.ContingentProperties contingentProperties = (this.m_contingentProperties = new Task.ContingentProperties());
				contingentProperties.m_cancellationToken = ct;
				contingentProperties.m_internalCancellationRequested = 1;
				return;
			}
			this.m_stateFlags = (int)((TaskCreationOptions)16777216 | creationOptions);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0005ECBE File Offset: 0x0005CEBE
		internal Task()
		{
			this.m_stateFlags = 33555456;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0005ECD4 File Offset: 0x0005CED4
		internal Task(object state, TaskCreationOptions creationOptions, bool promiseStyle)
		{
			if ((creationOptions & ~(TaskCreationOptions.AttachedToParent | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
			{
				this.m_parent = Task.InternalCurrent;
			}
			this.TaskConstructorCore(null, state, default(CancellationToken), creationOptions, InternalTaskOptions.PromiseTask, null);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that the new  task will observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x060018D7 RID: 6359 RVA: 0x0005ED20 File Offset: 0x0005CF20
		public Task(Action action, CancellationToken cancellationToken)
			: this(action, null, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0005ED2F File Offset: 0x0005CF2F
		internal Task(Delegate action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
			{
				this.m_parent = parent;
			}
			this.TaskConstructorCore(action, state, cancellationToken, creationOptions, internalOptions, scheduler);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0005ED64 File Offset: 0x0005CF64
		internal void TaskConstructorCore(Delegate action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			this.m_action = action;
			this.m_stateObject = state;
			this.m_taskScheduler = scheduler;
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
			int num = (int)(creationOptions | (TaskCreationOptions)internalOptions);
			if (this.m_action == null || (internalOptions & InternalTaskOptions.ContinuationTask) != InternalTaskOptions.None)
			{
				num |= 33554432;
			}
			this.m_stateFlags = num;
			if (this.m_parent != null && (creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
			{
				this.m_parent.AddNewChild();
			}
			if (cancellationToken.CanBeCanceled)
			{
				this.AssignCancellationToken(cancellationToken, null, null);
			}
			this.CapturedContext = ExecutionContext.Capture();
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0005EE08 File Offset: 0x0005D008
		private void AssignCancellationToken(CancellationToken cancellationToken, Task antecedent, TaskContinuation continuation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(false);
			contingentProperties.m_cancellationToken = cancellationToken;
			try
			{
				if ((this.Options & (TaskCreationOptions)13312) == TaskCreationOptions.None)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						this.InternalCancel(false);
					}
					else
					{
						CancellationTokenRegistration cancellationTokenRegistration;
						if (antecedent == null)
						{
							cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, this);
						}
						else
						{
							cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, new Tuple<Task, Task, TaskContinuation>(this, antecedent, continuation));
						}
						contingentProperties.m_cancellationRegistration = cancellationTokenRegistration;
					}
				}
			}
			catch
			{
				if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.Options & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
				{
					this.m_parent.DisregardChild();
				}
				throw;
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0005EEBC File Offset: 0x0005D0BC
		private static void TaskCancelCallback(object o)
		{
			Task task = o as Task;
			if (task == null)
			{
				Tuple<Task, Task, TaskContinuation> tuple = o as Tuple<Task, Task, TaskContinuation>;
				if (tuple != null)
				{
					task = tuple.Item1;
					Task item = tuple.Item2;
					TaskContinuation item2 = tuple.Item3;
					item.RemoveContinuation(item2);
				}
			}
			task.InternalCancel(false);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0005EF01 File Offset: 0x0005D101
		internal bool TrySetCanceled(CancellationToken tokenToRecord)
		{
			return this.TrySetCanceled(tokenToRecord, null);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005EF0C File Offset: 0x0005D10C
		internal bool TrySetCanceled(CancellationToken tokenToRecord, object cancellationException)
		{
			bool flag = false;
			if (this.AtomicStateUpdate(67108864, 90177536))
			{
				this.RecordInternalCancellationRequest(tokenToRecord, cancellationException);
				this.CancellationCleanupLogic();
				flag = true;
			}
			return flag;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005EF40 File Offset: 0x0005D140
		internal bool TrySetException(object exceptionObject)
		{
			bool flag = false;
			this.EnsureContingentPropertiesInitialized(true);
			if (this.AtomicStateUpdate(67108864, 90177536))
			{
				this.AddException(exceptionObject);
				this.Finish(false);
				flag = true;
			}
			return flag;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0005EF7A File Offset: 0x0005D17A
		internal TaskCreationOptions Options
		{
			get
			{
				return Task.OptionsMethod(this.m_stateFlags);
			}
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005EF89 File Offset: 0x0005D189
		internal static TaskCreationOptions OptionsMethod(int flags)
		{
			return (TaskCreationOptions)(flags & 65535);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005EF94 File Offset: 0x0005D194
		internal bool AtomicStateUpdate(int newBits, int illegalBits)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int stateFlags = this.m_stateFlags;
				if ((stateFlags & illegalBits) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_stateFlags, stateFlags | newBits, stateFlags) == stateFlags)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0005EFD8 File Offset: 0x0005D1D8
		internal bool AtomicStateUpdate(int newBits, int illegalBits, ref int oldFlags)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldFlags = this.m_stateFlags;
				if ((oldFlags & illegalBits) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_stateFlags, oldFlags | newBits, oldFlags) == oldFlags)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0005F020 File Offset: 0x0005D220
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			if (enabled)
			{
				this.AtomicStateUpdate(268435456, 90177536);
				return;
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int stateFlags = this.m_stateFlags;
				int num = stateFlags & -268435457;
				if (Interlocked.CompareExchange(ref this.m_stateFlags, num, stateFlags) == stateFlags)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0005F074 File Offset: 0x0005D274
		internal bool NotifyDebuggerOfWaitCompletionIfNecessary()
		{
			if (this.IsWaitNotificationEnabled && this.ShouldNotifyDebuggerOfWaitCompletion)
			{
				this.NotifyDebuggerOfWaitCompletion();
				return true;
			}
			return false;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0005F08F File Offset: 0x0005D28F
		internal bool IsWaitNotificationEnabledOrNotRanToCompletion
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.m_stateFlags & 285212672) != 16777216;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0005F0A9 File Offset: 0x0005D2A9
		internal virtual bool ShouldNotifyDebuggerOfWaitCompletion
		{
			get
			{
				return this.IsWaitNotificationEnabled;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0005F0B1 File Offset: 0x0005D2B1
		internal bool IsWaitNotificationEnabled
		{
			get
			{
				return (this.m_stateFlags & 268435456) != 0;
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0005F0C4 File Offset: 0x0005D2C4
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void NotifyDebuggerOfWaitCompletion()
		{
			this.SetNotificationForWaitCompletion(false);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0005F0CD File Offset: 0x0005D2CD
		internal bool MarkStarted()
		{
			return this.AtomicStateUpdate(65536, 4259840);
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x0005F0E0 File Offset: 0x0005D2E0
		internal void AddNewChild()
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			if (contingentProperties.m_completionCountdown == 1)
			{
				contingentProperties.m_completionCountdown++;
				return;
			}
			Interlocked.Increment(ref contingentProperties.m_completionCountdown);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x0005F11F File Offset: 0x0005D31F
		internal void DisregardChild()
		{
			Interlocked.Decrement(ref this.EnsureContingentPropertiesInitialized(true).m_completionCountdown);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x0005F133 File Offset: 0x0005D333
		internal static Task InternalStartNew(Task creatingTask, Delegate action, object state, CancellationToken cancellationToken, TaskScheduler scheduler, TaskCreationOptions options, InternalTaskOptions internalOptions)
		{
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task task = new Task(action, state, creatingTask, cancellationToken, options, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.ScheduleAndStart(false);
			return task;
		}

		/// <summary>Gets a unique ID for this <see cref="T:System.Threading.Tasks.Task" /> instance.</summary>
		/// <returns>An integer that was assigned by the system to this task instance.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0005F160 File Offset: 0x0005D360
		public int Id
		{
			get
			{
				if (this.m_taskId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref Task.s_taskIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskId, num, 0);
				}
				return this.m_taskId;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0005F19D File Offset: 0x0005D39D
		internal static Task InternalCurrent
		{
			get
			{
				return Task.t_currentTask;
			}
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0005F1A4 File Offset: 0x0005D3A4
		internal static Task InternalCurrentIfAttached(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None)
			{
				return null;
			}
			return Task.InternalCurrent;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0005F1B4 File Offset: 0x0005D3B4
		internal static StackGuard CurrentStackGuard
		{
			get
			{
				StackGuard stackGuard = Task.t_stackGuard;
				if (stackGuard == null)
				{
					stackGuard = (Task.t_stackGuard = new StackGuard());
				}
				return stackGuard;
			}
		}

		/// <summary>Gets the <see cref="T:System.AggregateException" /> that caused the <see cref="T:System.Threading.Tasks.Task" /> to end prematurely. If the <see cref="T:System.Threading.Tasks.Task" /> completed successfully or has not yet thrown any exceptions, this will return null.</summary>
		/// <returns>The <see cref="T:System.AggregateException" /> that caused the <see cref="T:System.Threading.Tasks.Task" /> to end prematurely.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0005F1D8 File Offset: 0x0005D3D8
		public AggregateException Exception
		{
			get
			{
				AggregateException ex = null;
				if (this.IsFaulted)
				{
					ex = this.GetExceptions(false);
				}
				return ex;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskStatus" /> of this task.</summary>
		/// <returns>The current <see cref="T:System.Threading.Tasks.TaskStatus" /> of this task instance.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0005F1F8 File Offset: 0x0005D3F8
		public TaskStatus Status
		{
			get
			{
				int stateFlags = this.m_stateFlags;
				TaskStatus taskStatus;
				if ((stateFlags & 2097152) != 0)
				{
					taskStatus = TaskStatus.Faulted;
				}
				else if ((stateFlags & 4194304) != 0)
				{
					taskStatus = TaskStatus.Canceled;
				}
				else if ((stateFlags & 16777216) != 0)
				{
					taskStatus = TaskStatus.RanToCompletion;
				}
				else if ((stateFlags & 8388608) != 0)
				{
					taskStatus = TaskStatus.WaitingForChildrenToComplete;
				}
				else if ((stateFlags & 131072) != 0)
				{
					taskStatus = TaskStatus.Running;
				}
				else if ((stateFlags & 65536) != 0)
				{
					taskStatus = TaskStatus.WaitingToRun;
				}
				else if ((stateFlags & 33554432) != 0)
				{
					taskStatus = TaskStatus.WaitingForActivation;
				}
				else
				{
					taskStatus = TaskStatus.Created;
				}
				return taskStatus;
			}
		}

		/// <summary>Gets whether this <see cref="T:System.Threading.Tasks.Task" /> instance has completed execution due to being canceled.</summary>
		/// <returns>true if the task has completed due to being canceled; otherwise false.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0005F26C File Offset: 0x0005D46C
		public bool IsCanceled
		{
			get
			{
				return (this.m_stateFlags & 6291456) == 4194304;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0005F284 File Offset: 0x0005D484
		internal bool IsCancellationRequested
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return contingentProperties != null && (contingentProperties.m_internalCancellationRequested == 1 || contingentProperties.m_cancellationToken.IsCancellationRequested);
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0005F2B8 File Offset: 0x0005D4B8
		internal Task.ContingentProperties EnsureContingentPropertiesInitialized(bool needsProtection)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null)
			{
				return this.EnsureContingentPropertiesInitializedCore(needsProtection);
			}
			return contingentProperties;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0005F2DC File Offset: 0x0005D4DC
		private Task.ContingentProperties EnsureContingentPropertiesInitializedCore(bool needsProtection)
		{
			if (needsProtection)
			{
				return LazyInitializer.EnsureInitialized<Task.ContingentProperties>(ref this.m_contingentProperties, Task.s_createContingentProperties);
			}
			return this.m_contingentProperties = new Task.ContingentProperties();
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0005F310 File Offset: 0x0005D510
		internal CancellationToken CancellationToken
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					return contingentProperties.m_cancellationToken;
				}
				return default(CancellationToken);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0005F339 File Offset: 0x0005D539
		internal bool IsCancellationAcknowledged
		{
			get
			{
				return (this.m_stateFlags & 1048576) != 0;
			}
		}

		/// <summary>Gets whether this <see cref="T:System.Threading.Tasks.Task" /> has completed.</summary>
		/// <returns>true if the task has completed; otherwise false.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0005F34C File Offset: 0x0005D54C
		public bool IsCompleted
		{
			get
			{
				return Task.IsCompletedMethod(this.m_stateFlags);
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0005F35B File Offset: 0x0005D55B
		private static bool IsCompletedMethod(int flags)
		{
			return (flags & 23068672) != 0;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0005F367 File Offset: 0x0005D567
		public bool IsCompletedSuccessfully
		{
			get
			{
				return (this.m_stateFlags & 23068672) == 16777216;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to create this task.</summary>
		/// <returns>The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to create this task.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x0005F37E File Offset: 0x0005D57E
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.Options & (TaskCreationOptions)(-65281);
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that can be used to wait for the task to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that can be used to wait for the task to complete.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0005F38C File Offset: 0x0005D58C
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				if ((this.m_stateFlags & 262144) != 0)
				{
					throw new ObjectDisposedException(null, "The task has been disposed.");
				}
				return this.CompletedEvent.WaitHandle;
			}
		}

		/// <summary>Gets the state object supplied when the <see cref="T:System.Threading.Tasks.Task" /> was created, or null if none was supplied.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the state data that was passed in to the task when it was created.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0005F3B8 File Offset: 0x0005D5B8
		public object AsyncState
		{
			get
			{
				return this.m_stateObject;
			}
		}

		/// <summary>Gets an indication of whether the operation completed synchronously.</summary>
		/// <returns>true if the operation completed synchronously; otherwise, false.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x00033991 File Offset: 0x00031B91
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005F3C0 File Offset: 0x0005D5C0
		internal TaskScheduler ExecutingTaskScheduler
		{
			get
			{
				return this.m_taskScheduler;
			}
		}

		/// <summary>Provides access to factory methods for creating <see cref="T:System.Threading.Tasks.Task" /> and <see cref="T:System.Threading.Tasks.Task`1" /> instances.</summary>
		/// <returns>The default <see cref="T:System.Threading.Tasks.TaskFactory" /> for the current task.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0005F3C8 File Offset: 0x0005D5C8
		public static TaskFactory Factory { get; } = new TaskFactory();

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0005F3CF File Offset: 0x0005D5CF
		public static Task CompletedTask { get; } = new Task(false, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0005F3D8 File Offset: 0x0005D5D8
		internal ManualResetEventSlim CompletedEvent
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
				if (contingentProperties.m_completionEvent == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(isCompleted);
					if (Interlocked.CompareExchange<ManualResetEventSlim>(ref contingentProperties.m_completionEvent, manualResetEventSlim, null) != null)
					{
						manualResetEventSlim.Dispose();
					}
					else if (!isCompleted && this.IsCompleted)
					{
						manualResetEventSlim.Set();
					}
				}
				return contingentProperties.m_completionEvent;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0005F438 File Offset: 0x0005D638
		internal bool ExceptionRecorded
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return contingentProperties != null && contingentProperties.m_exceptionsHolder != null && contingentProperties.m_exceptionsHolder.ContainsFaultList;
			}
		}

		/// <summary>Gets whether the <see cref="T:System.Threading.Tasks.Task" /> completed due to an unhandled exception.</summary>
		/// <returns>true if the task has thrown an unhandled exception; otherwise false.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0005F46A File Offset: 0x0005D66A
		public bool IsFaulted
		{
			get
			{
				return (this.m_stateFlags & 2097152) != 0;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0005F480 File Offset: 0x0005D680
		// (set) Token: 0x06001907 RID: 6407 RVA: 0x0005F4AD File Offset: 0x0005D6AD
		internal ExecutionContext CapturedContext
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null && contingentProperties.m_capturedContext != null)
				{
					return contingentProperties.m_capturedContext;
				}
				return ExecutionContext.Default;
			}
			set
			{
				if (value != ExecutionContext.Default)
				{
					this.EnsureContingentPropertiesInitialized(false).m_capturedContext = value;
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.Tasks.Task" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The exception that is thrown if the <see cref="T:System.Threading.Tasks.Task" /> is not in one of the final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x06001908 RID: 6408 RVA: 0x0005F4C4 File Offset: 0x0005D6C4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Disposes the <see cref="T:System.Threading.Tasks.Task" />, releasing all of its unmanaged resources.</summary>
		/// <param name="disposing">A Boolean value that indicates whether this method is being called due to a call to <see cref="M:System.Threading.Tasks.Task.Dispose" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The exception that is thrown if the <see cref="T:System.Threading.Tasks.Task" /> is not in one of the final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x06001909 RID: 6409 RVA: 0x0005F4D4 File Offset: 0x0005D6D4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if ((this.Options & (TaskCreationOptions)16384) != TaskCreationOptions.None)
				{
					return;
				}
				if (!this.IsCompleted)
				{
					throw new InvalidOperationException("A task may only be disposed if it is in a completion state (RanToCompletion, Faulted or Canceled).");
				}
				Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
				if (contingentProperties != null)
				{
					ManualResetEventSlim completionEvent = contingentProperties.m_completionEvent;
					if (completionEvent != null)
					{
						contingentProperties.m_completionEvent = null;
						if (!completionEvent.IsSet)
						{
							completionEvent.Set();
						}
						completionEvent.Dispose();
					}
				}
			}
			this.m_stateFlags |= 262144;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0005F554 File Offset: 0x0005D754
		internal void ScheduleAndStart(bool needsProtection)
		{
			if (needsProtection)
			{
				if (!this.MarkStarted())
				{
					return;
				}
			}
			else
			{
				this.m_stateFlags |= 65536;
			}
			DebuggerSupport.AddToActiveTasks(this);
			if (DebuggerSupport.LoggingOn && (this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
			{
				CausalityTraceLevel causalityTraceLevel = CausalityTraceLevel.Required;
				string text = "Task: ";
				Delegate action = this.m_action;
				DebuggerSupport.TraceOperationCreation(causalityTraceLevel, this, text + ((action != null) ? action.ToString() : null), 0UL);
			}
			try
			{
				this.m_taskScheduler.QueueTask(this);
			}
			catch (Exception ex)
			{
				TaskSchedulerException ex2 = new TaskSchedulerException(ex);
				this.AddException(ex2);
				this.Finish(false);
				if ((this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
				{
					this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
				}
				throw ex2;
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0005F61C File Offset: 0x0005D81C
		internal void AddException(object exceptionObject)
		{
			this.AddException(exceptionObject, false);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0005F628 File Offset: 0x0005D828
		internal void AddException(object exceptionObject, bool representsCancellation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			if (contingentProperties.m_exceptionsHolder == null)
			{
				TaskExceptionHolder taskExceptionHolder = new TaskExceptionHolder(this);
				if (Interlocked.CompareExchange<TaskExceptionHolder>(ref contingentProperties.m_exceptionsHolder, taskExceptionHolder, null) != null)
				{
					taskExceptionHolder.MarkAsHandled(false);
				}
			}
			Task.ContingentProperties contingentProperties2 = contingentProperties;
			lock (contingentProperties2)
			{
				contingentProperties.m_exceptionsHolder.Add(exceptionObject, representsCancellation);
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0005F69C File Offset: 0x0005D89C
		private AggregateException GetExceptions(bool includeTaskCanceledExceptions)
		{
			Exception ex = null;
			if (includeTaskCanceledExceptions && this.IsCanceled)
			{
				ex = new TaskCanceledException(this);
			}
			if (this.ExceptionRecorded)
			{
				return this.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, ex);
			}
			if (ex != null)
			{
				return new AggregateException(new Exception[] { ex });
			}
			return null;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0005F6F0 File Offset: 0x0005D8F0
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			if (!this.IsFaulted || !this.ExceptionRecorded)
			{
				return new ReadOnlyCollection<ExceptionDispatchInfo>(Array.Empty<ExceptionDispatchInfo>());
			}
			return this.m_contingentProperties.m_exceptionsHolder.GetExceptionDispatchInfos();
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0005F724 File Offset: 0x0005D924
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null)
			{
				return null;
			}
			TaskExceptionHolder exceptionsHolder = contingentProperties.m_exceptionsHolder;
			if (exceptionsHolder == null)
			{
				return null;
			}
			return exceptionsHolder.GetCancellationExceptionDispatchInfo();
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0005F754 File Offset: 0x0005D954
		internal void ThrowIfExceptional(bool includeTaskCanceledExceptions)
		{
			Exception exceptions = this.GetExceptions(includeTaskCanceledExceptions);
			if (exceptions != null)
			{
				this.UpdateExceptionObservedStatus();
				throw exceptions;
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0005F774 File Offset: 0x0005D974
		internal void UpdateExceptionObservedStatus()
		{
			if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && Task.InternalCurrent == this.m_parent)
			{
				this.m_stateFlags |= 524288;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0005F7C5 File Offset: 0x0005D9C5
		internal bool IsExceptionObservedByParent
		{
			get
			{
				return (this.m_stateFlags & 524288) != 0;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005F7D8 File Offset: 0x0005D9D8
		internal bool IsDelegateInvoked
		{
			get
			{
				return (this.m_stateFlags & 131072) != 0;
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0005F7EC File Offset: 0x0005D9EC
		internal void Finish(bool bUserDelegateExecuted)
		{
			if (!bUserDelegateExecuted)
			{
				this.FinishStageTwo();
				return;
			}
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null || contingentProperties.m_completionCountdown == 1 || Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
			else
			{
				this.AtomicStateUpdate(8388608, 23068672);
			}
			LowLevelListWithIList<Task> lowLevelListWithIList = ((contingentProperties != null) ? contingentProperties.m_exceptionalChildren : null);
			if (lowLevelListWithIList != null)
			{
				LowLevelListWithIList<Task> lowLevelListWithIList2 = lowLevelListWithIList;
				lock (lowLevelListWithIList2)
				{
					lowLevelListWithIList.RemoveAll(Task.s_IsExceptionObservedByParentPredicate);
				}
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0005F888 File Offset: 0x0005DA88
		internal void FinishStageTwo()
		{
			this.AddExceptionsFromChildren();
			int num;
			if (this.ExceptionRecorded)
			{
				num = 2097152;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(this);
			}
			else if (this.IsCancellationRequested && this.IsCancellationAcknowledged)
			{
				num = 4194304;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Canceled);
				}
				DebuggerSupport.RemoveFromActiveTasks(this);
			}
			else
			{
				num = 16777216;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
				}
				DebuggerSupport.RemoveFromActiveTasks(this);
			}
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | num);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.UnregisterCancellationCallback();
			}
			this.FinishStageThree();
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0005F93C File Offset: 0x0005DB3C
		internal void FinishStageThree()
		{
			this.m_action = null;
			if (this.m_parent != null && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && (this.m_stateFlags & 65535 & 4) != 0)
			{
				this.m_parent.ProcessChildCompletion(this);
			}
			this.FinishContinuations();
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0005F98C File Offset: 0x0005DB8C
		internal void ProcessChildCompletion(Task childTask)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (childTask.IsFaulted && !childTask.IsExceptionObservedByParent)
			{
				if (contingentProperties.m_exceptionalChildren == null)
				{
					Interlocked.CompareExchange<LowLevelListWithIList<Task>>(ref contingentProperties.m_exceptionalChildren, new LowLevelListWithIList<Task>(), null);
				}
				LowLevelListWithIList<Task> exceptionalChildren = contingentProperties.m_exceptionalChildren;
				if (exceptionalChildren != null)
				{
					LowLevelListWithIList<Task> lowLevelListWithIList = exceptionalChildren;
					lock (lowLevelListWithIList)
					{
						exceptionalChildren.Add(childTask);
					}
				}
			}
			if (Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0005FA1C File Offset: 0x0005DC1C
		internal void AddExceptionsFromChildren()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			LowLevelListWithIList<Task> lowLevelListWithIList = ((contingentProperties != null) ? contingentProperties.m_exceptionalChildren : null);
			if (lowLevelListWithIList != null)
			{
				LowLevelListWithIList<Task> lowLevelListWithIList2 = lowLevelListWithIList;
				lock (lowLevelListWithIList2)
				{
					foreach (Task task in ((IEnumerable<Task>)lowLevelListWithIList))
					{
						if (task.IsFaulted && !task.IsExceptionObservedByParent)
						{
							TaskExceptionHolder exceptionsHolder = task.m_contingentProperties.m_exceptionsHolder;
							this.AddException(exceptionsHolder.CreateExceptionObject(false, null));
						}
					}
				}
				contingentProperties.m_exceptionalChildren = null;
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0005FADC File Offset: 0x0005DCDC
		private void Execute()
		{
			try
			{
				this.InnerInvoke();
			}
			catch (Exception ex)
			{
				this.HandleException(ex);
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0005FB0C File Offset: 0x0005DD0C
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.ExecuteEntry(false);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0005FB18 File Offset: 0x0005DD18
		internal bool ExecuteEntry(bool bPreventDoubleExecution)
		{
			if (bPreventDoubleExecution)
			{
				int num = 0;
				if (!this.AtomicStateUpdate(131072, 23199744, ref num) && (num & 4194304) == 0)
				{
					return false;
				}
			}
			else
			{
				this.m_stateFlags |= 131072;
			}
			if (!this.IsCancellationRequested && !this.IsCanceled)
			{
				this.ExecuteWithThreadLocal(ref Task.t_currentTask);
			}
			else if (!this.IsCanceled && (Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304) & 4194304) == 0)
			{
				this.CancellationCleanupLogic();
			}
			return true;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0005FBAC File Offset: 0x0005DDAC
		private static void ExecutionContextCallback(object obj)
		{
			(obj as Task).Execute();
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0005FBBC File Offset: 0x0005DDBC
		internal virtual void InnerInvoke()
		{
			Action action = this.m_action as Action;
			if (action != null)
			{
				action();
				return;
			}
			Action<object> action2 = this.m_action as Action<object>;
			if (action2 != null)
			{
				action2(this.m_stateObject);
				return;
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0005FBFC File Offset: 0x0005DDFC
		private void HandleException(Exception unhandledException)
		{
			OperationCanceledException ex = unhandledException as OperationCanceledException;
			if (ex != null && this.IsCancellationRequested && this.m_contingentProperties.m_cancellationToken == ex.CancellationToken)
			{
				this.SetCancellationAcknowledged();
				this.AddException(ex, true);
				return;
			}
			this.AddException(unhandledException);
		}

		/// <summary>Gets an awaiter used to await this <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>An awaiter instance.</returns>
		// Token: 0x0600191F RID: 6431 RVA: 0x0005FC4B File Offset: 0x0005DE4B
		public TaskAwaiter GetAwaiter()
		{
			return new TaskAwaiter(this);
		}

		/// <summary>Configures an awaiter used to await this <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>An object used to await this task.</returns>
		/// <param name="continueOnCapturedContext">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
		// Token: 0x06001920 RID: 6432 RVA: 0x0005FC53 File Offset: 0x0005DE53
		public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredTaskAwaitable(this, continueOnCapturedContext);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0005FC5C File Offset: 0x0005DE5C
		internal void SetContinuationForAwait(Action continuationAction, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			TaskContinuation taskContinuation = null;
			if (continueOnCapturedContext)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					taskContinuation = new SynchronizationContextAwaitTaskContinuation(synchronizationContext, continuationAction, flowExecutionContext);
				}
				else
				{
					TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
					if (internalCurrent != null && internalCurrent != TaskScheduler.Default)
					{
						taskContinuation = new TaskSchedulerAwaitTaskContinuation(internalCurrent, continuationAction, flowExecutionContext);
					}
				}
			}
			if (taskContinuation != null)
			{
				if (!this.AddTaskContinuation(taskContinuation, false))
				{
					taskContinuation.Run(this, false);
					return;
				}
			}
			else if (!this.AddTaskContinuation(continuationAction, false))
			{
				AwaitTaskContinuation.UnsafeScheduleAction(continuationAction);
			}
		}

		/// <summary>Creates an awaitable task that asynchronously yields back to the current context when awaited.</summary>
		/// <returns>A context that, when awaited, will asynchronously transition back into the current context at the time of the await. If the current <see cref="T:System.Threading.SynchronizationContext" /> is non-null, it is treated as the current context. Otherwise, the task scheduler that is associated with the currently executing task is treated as the current context. </returns>
		// Token: 0x06001922 RID: 6434 RVA: 0x0005FCDC File Offset: 0x0005DEDC
		public static YieldAwaitable Yield()
		{
			return default(YieldAwaitable);
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The <see cref="T:System.Threading.Tasks.Task" /> was canceled -or- an exception was thrown during the execution of the <see cref="T:System.Threading.Tasks.Task" />. If the task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		// Token: 0x06001923 RID: 6435 RVA: 0x0005FCF4 File Offset: 0x0005DEF4
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution within a specified number of milliseconds.</summary>
		/// <returns>true if the <see cref="T:System.Threading.Tasks.Task" /> completed execution within the allotted time; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.AggregateException">The <see cref="T:System.Threading.Tasks.Task" /> was canceled -or- an exception was thrown during the execution of the <see cref="T:System.Threading.Tasks.Task" />. If the task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		// Token: 0x06001924 RID: 6436 RVA: 0x0005FD14 File Offset: 0x0005DF14
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Waits for the cancellable <see cref="T:System.Threading.Tasks.Task" /> to complete execution.</summary>
		/// <returns>true if the <see cref="T:System.Threading.Tasks.Task" /> completed execution within the allotted time; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for the task to complete.</param>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.AggregateException">The <see cref="T:System.Threading.Tasks.Task" /> was canceled -or- an exception was thrown during the execution of the <see cref="T:System.Threading.Tasks.Task" />. If the task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.</exception>
		// Token: 0x06001925 RID: 6437 RVA: 0x0005FD34 File Offset: 0x0005DF34
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				return true;
			}
			if (!this.InternalWait(millisecondsTimeout, cancellationToken))
			{
				return false;
			}
			if (this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				this.NotifyDebuggerOfWaitCompletionIfNecessary();
				if (this.IsCanceled)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				this.ThrowIfExceptional(true);
			}
			return true;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0005FD8C File Offset: 0x0005DF8C
		private bool WrappedTryRunInline()
		{
			if (this.m_taskScheduler == null)
			{
				return false;
			}
			bool flag;
			try
			{
				flag = this.m_taskScheduler.TryRunInline(this, true);
			}
			catch (Exception ex)
			{
				throw new TaskSchedulerException(ex);
			}
			return flag;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0005FDCC File Offset: 0x0005DFCC
		[MethodImpl(MethodImplOptions.NoOptimization)]
		internal bool InternalWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (TaskTrace.Enabled)
			{
				Task internalCurrent = Task.InternalCurrent;
				TaskTrace.TaskWaitBegin_Synchronous((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.Id);
			}
			bool flag = this.IsCompleted;
			if (!flag)
			{
				flag = (millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled && this.WrappedTryRunInline() && this.IsCompleted) || this.SpinThenBlockingWait(millisecondsTimeout, cancellationToken);
			}
			if (TaskTrace.Enabled)
			{
				Task internalCurrent2 = Task.InternalCurrent;
				if (internalCurrent2 != null)
				{
					TaskTrace.TaskWaitEnd(internalCurrent2.m_taskScheduler.Id, internalCurrent2.Id, this.Id);
				}
				else
				{
					TaskTrace.TaskWaitEnd(TaskScheduler.Default.Id, 0, this.Id);
				}
			}
			return flag;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0005FE90 File Offset: 0x0005E090
		private bool SpinThenBlockingWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = millisecondsTimeout == -1;
			uint num = (uint)(flag ? 0 : Environment.TickCount);
			bool flag2 = this.SpinWait(millisecondsTimeout);
			if (!flag2)
			{
				Task.SetOnInvokeMres setOnInvokeMres = new Task.SetOnInvokeMres();
				try
				{
					this.AddCompletionAction(setOnInvokeMres, true);
					if (flag)
					{
						flag2 = setOnInvokeMres.Wait(-1, cancellationToken);
					}
					else
					{
						uint num2 = (uint)(Environment.TickCount - (int)num);
						if ((ulong)num2 < (ulong)((long)millisecondsTimeout))
						{
							flag2 = setOnInvokeMres.Wait((int)((long)millisecondsTimeout - (long)((ulong)num2)), cancellationToken);
						}
					}
				}
				finally
				{
					if (!this.IsCompleted)
					{
						this.RemoveContinuation(setOnInvokeMres);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x0005FF18 File Offset: 0x0005E118
		private bool SpinWait(int millisecondsTimeout)
		{
			if (this.IsCompleted)
			{
				return true;
			}
			if (millisecondsTimeout == 0)
			{
				return false;
			}
			int spinCountforSpinBeforeWait = global::System.Threading.SpinWait.SpinCountforSpinBeforeWait;
			SpinWait spinWait = default(SpinWait);
			while (spinWait.Count < spinCountforSpinBeforeWait)
			{
				spinWait.SpinOnce(-1);
				if (this.IsCompleted)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x0005FF64 File Offset: 0x0005E164
		internal bool InternalCancel(bool bCancelNonExecutingOnly)
		{
			bool flag = false;
			bool flag2 = false;
			TaskSchedulerException ex = null;
			if ((this.m_stateFlags & 65536) != 0)
			{
				TaskScheduler taskScheduler = this.m_taskScheduler;
				try
				{
					flag = taskScheduler != null && taskScheduler.TryDequeue(this);
				}
				catch (Exception ex2)
				{
					ex = new TaskSchedulerException(ex2);
				}
				bool flag3 = taskScheduler != null && taskScheduler.RequiresAtomicStartTransition;
				if (!flag && bCancelNonExecutingOnly && flag3)
				{
					flag2 = this.AtomicStateUpdate(4194304, 4325376);
				}
			}
			if (!bCancelNonExecutingOnly || flag || flag2)
			{
				this.RecordInternalCancellationRequest();
				if (flag)
				{
					flag2 = this.AtomicStateUpdate(4194304, 4325376);
				}
				else if (!flag2 && (this.m_stateFlags & 65536) == 0)
				{
					flag2 = this.AtomicStateUpdate(4194304, 23265280);
				}
				if (flag2)
				{
					this.CancellationCleanupLogic();
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return flag2;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x0006003C File Offset: 0x0005E23C
		internal void RecordInternalCancellationRequest()
		{
			this.EnsureContingentPropertiesInitialized(true).m_internalCancellationRequested = 1;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00060050 File Offset: 0x0005E250
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord)
		{
			this.RecordInternalCancellationRequest();
			if (tokenToRecord != default(CancellationToken))
			{
				this.m_contingentProperties.m_cancellationToken = tokenToRecord;
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00060082 File Offset: 0x0005E282
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord, object cancellationException)
		{
			this.RecordInternalCancellationRequest(tokenToRecord);
			if (cancellationException != null)
			{
				this.AddException(cancellationException, true);
			}
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00060098 File Offset: 0x0005E298
		internal void CancellationCleanupLogic()
		{
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.UnregisterCancellationCallback();
			}
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Canceled);
			}
			DebuggerSupport.RemoveFromActiveTasks(this);
			this.FinishStageThree();
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x000600F2 File Offset: 0x0005E2F2
		private void SetCancellationAcknowledged()
		{
			this.m_stateFlags |= 1048576;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0006010C File Offset: 0x0005E30C
		internal void FinishContinuations()
		{
			object obj = Interlocked.Exchange(ref this.m_continuationObject, Task.s_taskCompletionSentinel);
			if (obj != null)
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this, CausalitySynchronousWork.CompletionNotification);
				}
				bool flag = (this.m_stateFlags & 134217728) == 0 && (this.m_stateFlags & 64) == 0;
				Action action = obj as Action;
				if (action != null)
				{
					AwaitTaskContinuation.RunOrScheduleAction(action, flag, ref Task.t_currentTask);
					this.LogFinishCompletionNotification();
					return;
				}
				ITaskCompletionAction taskCompletionAction = obj as ITaskCompletionAction;
				if (taskCompletionAction != null)
				{
					if (flag || !taskCompletionAction.InvokeMayRunArbitraryCode)
					{
						taskCompletionAction.Invoke(this);
					}
					else
					{
						ThreadPool.UnsafeQueueCustomWorkItem(new CompletionActionInvoker(taskCompletionAction, this), false);
					}
					this.LogFinishCompletionNotification();
					return;
				}
				TaskContinuation taskContinuation = obj as TaskContinuation;
				if (taskContinuation != null)
				{
					taskContinuation.Run(this, flag);
					this.LogFinishCompletionNotification();
					return;
				}
				LowLevelListWithIList<object> lowLevelListWithIList = obj as LowLevelListWithIList<object>;
				if (lowLevelListWithIList == null)
				{
					this.LogFinishCompletionNotification();
					return;
				}
				LowLevelListWithIList<object> lowLevelListWithIList2 = lowLevelListWithIList;
				lock (lowLevelListWithIList2)
				{
				}
				int count = lowLevelListWithIList.Count;
				for (int i = 0; i < count; i++)
				{
					StandardTaskContinuation standardTaskContinuation = lowLevelListWithIList[i] as StandardTaskContinuation;
					if (standardTaskContinuation != null && (standardTaskContinuation.m_options & TaskContinuationOptions.ExecuteSynchronously) == TaskContinuationOptions.None)
					{
						lowLevelListWithIList[i] = null;
						standardTaskContinuation.Run(this, flag);
					}
				}
				for (int j = 0; j < count; j++)
				{
					object obj2 = lowLevelListWithIList[j];
					if (obj2 != null)
					{
						lowLevelListWithIList[j] = null;
						Action action2 = obj2 as Action;
						if (action2 != null)
						{
							AwaitTaskContinuation.RunOrScheduleAction(action2, flag, ref Task.t_currentTask);
						}
						else
						{
							TaskContinuation taskContinuation2 = obj2 as TaskContinuation;
							if (taskContinuation2 != null)
							{
								taskContinuation2.Run(this, flag);
							}
							else
							{
								ITaskCompletionAction taskCompletionAction2 = (ITaskCompletionAction)obj2;
								if (flag || !taskCompletionAction2.InvokeMayRunArbitraryCode)
								{
									taskCompletionAction2.Invoke(this);
								}
								else
								{
									ThreadPool.UnsafeQueueCustomWorkItem(new CompletionActionInvoker(taskCompletionAction2, this), false);
								}
							}
						}
					}
				}
				this.LogFinishCompletionNotification();
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000602F4 File Offset: 0x0005E4F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void LogFinishCompletionNotification()
		{
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.CompletionNotification);
			}
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		// Token: 0x06001932 RID: 6450 RVA: 0x00060304 File Offset: 0x0005E504
		public Task ContinueWith(Action<Task> continuationAction)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes according to the specified <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run according to the specified <paramref name="continuationOptions" />. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06001933 RID: 6451 RVA: 0x00060328 File Offset: 0x0005E528
		public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		/// <summary>Creates a continuation that executes according to the specified <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run according to the specified <paramref name="continuationOptions" />. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.-or-The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06001934 RID: 6452 RVA: 0x0006034B File Offset: 0x0005E54B
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00060358 File Offset: 0x0005E558
		private Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task task = new ContinuationTaskFromTask(this, continuationAction, null, taskCreationOptions, internalTaskOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes.  When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is null.</exception>
		// Token: 0x06001936 RID: 6454 RVA: 0x000603A4 File Offset: 0x0005E5A4
		public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its  execution.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x06001937 RID: 6455 RVA: 0x000603C4 File Offset: 0x0005E5C4
		public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000603D4 File Offset: 0x0005E5D4
		private Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			Task task = new ContinuationTaskFromTask(this, continuationAction, state, taskCreationOptions, internalTaskOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00060420 File Offset: 0x0005E620
		internal static void CreationOptionsFromContinuationOptions(TaskContinuationOptions continuationOptions, out TaskCreationOptions creationOptions, out InternalTaskOptions internalOptions)
		{
			TaskContinuationOptions taskContinuationOptions = TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled;
			TaskContinuationOptions taskContinuationOptions2 = TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.RunContinuationsAsynchronously;
			TaskContinuationOptions taskContinuationOptions3 = TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously;
			if ((continuationOptions & taskContinuationOptions3) == taskContinuationOptions3)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", "The specified TaskContinuationOptions combined LongRunning and ExecuteSynchronously.  Synchronous continuations should not be long running.");
			}
			if ((continuationOptions & ~((taskContinuationOptions2 | taskContinuationOptions | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & taskContinuationOptions) == taskContinuationOptions)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", "The specified TaskContinuationOptions excluded all continuation kinds.");
			}
			creationOptions = (TaskCreationOptions)(continuationOptions & taskContinuationOptions2);
			internalOptions = InternalTaskOptions.ContinuationTask;
			if ((continuationOptions & TaskContinuationOptions.LazyCancellation) != TaskContinuationOptions.None)
			{
				internalOptions |= InternalTaskOptions.LazyCancellation;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000604A0 File Offset: 0x0005E6A0
		internal void ContinueWithCore(Task continuationTask, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions options)
		{
			TaskContinuation taskContinuation = new StandardTaskContinuation(continuationTask, options, scheduler);
			if (cancellationToken.CanBeCanceled)
			{
				if (this.IsCompleted || cancellationToken.IsCancellationRequested)
				{
					continuationTask.AssignCancellationToken(cancellationToken, null, null);
				}
				else
				{
					continuationTask.AssignCancellationToken(cancellationToken, this, taskContinuation);
				}
			}
			if (!continuationTask.IsCompleted && !this.AddTaskContinuation(taskContinuation, false))
			{
				taskContinuation.Run(this, true);
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x000604FF File Offset: 0x0005E6FF
		internal void AddCompletionAction(ITaskCompletionAction action)
		{
			this.AddCompletionAction(action, false);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00060509 File Offset: 0x0005E709
		private void AddCompletionAction(ITaskCompletionAction action, bool addBeforeOthers)
		{
			if (!this.AddTaskContinuation(action, addBeforeOthers))
			{
				action.Invoke(this);
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0006051C File Offset: 0x0005E71C
		private bool AddTaskContinuationComplex(object tc, bool addBeforeOthers)
		{
			object continuationObject = this.m_continuationObject;
			if (continuationObject != Task.s_taskCompletionSentinel && !(continuationObject is LowLevelListWithIList<object>))
			{
				Interlocked.CompareExchange(ref this.m_continuationObject, new LowLevelListWithIList<object> { continuationObject }, continuationObject);
			}
			LowLevelListWithIList<object> lowLevelListWithIList = this.m_continuationObject as LowLevelListWithIList<object>;
			if (lowLevelListWithIList != null)
			{
				LowLevelListWithIList<object> lowLevelListWithIList2 = lowLevelListWithIList;
				lock (lowLevelListWithIList2)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						if (lowLevelListWithIList.Count == lowLevelListWithIList.Capacity)
						{
							lowLevelListWithIList.RemoveAll(Task.s_IsTaskContinuationNullPredicate);
						}
						if (addBeforeOthers)
						{
							lowLevelListWithIList.Insert(0, tc);
						}
						else
						{
							lowLevelListWithIList.Add(tc);
						}
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x000605E0 File Offset: 0x0005E7E0
		private bool AddTaskContinuation(object tc, bool addBeforeOthers)
		{
			return !this.IsCompleted && ((this.m_continuationObject == null && Interlocked.CompareExchange(ref this.m_continuationObject, tc, null) == null) || this.AddTaskContinuationComplex(tc, addBeforeOthers));
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00060610 File Offset: 0x0005E810
		internal void RemoveContinuation(object continuationObject)
		{
			object continuationObject2 = this.m_continuationObject;
			if (continuationObject2 == Task.s_taskCompletionSentinel)
			{
				return;
			}
			LowLevelListWithIList<object> lowLevelListWithIList = continuationObject2 as LowLevelListWithIList<object>;
			if (lowLevelListWithIList == null)
			{
				if (Interlocked.CompareExchange(ref this.m_continuationObject, new LowLevelListWithIList<object>(), continuationObject) == continuationObject)
				{
					return;
				}
				lowLevelListWithIList = this.m_continuationObject as LowLevelListWithIList<object>;
			}
			if (lowLevelListWithIList != null)
			{
				LowLevelListWithIList<object> lowLevelListWithIList2 = lowLevelListWithIList;
				lock (lowLevelListWithIList2)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						int num = lowLevelListWithIList.IndexOf(continuationObject);
						if (num != -1)
						{
							lowLevelListWithIList[num] = null;
						}
					}
				}
			}
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that's completed successfully with the specified result.</summary>
		/// <returns>The successfully completed task.</returns>
		/// <param name="result">The result to store into the completed task.</param>
		/// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
		// Token: 0x06001940 RID: 6464 RVA: 0x000606B4 File Offset: 0x0005E8B4
		public static Task<TResult> FromResult<TResult>(TResult result)
		{
			return new Task<TResult>(result);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000606BC File Offset: 0x0005E8BC
		public static Task FromException(Exception exception)
		{
			return Task.FromException<VoidTaskResult>(exception);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x000606C4 File Offset: 0x0005E8C4
		public static Task<TResult> FromException<TResult>(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = new Task<TResult>();
			task.TrySetException(exception);
			return task;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000606E1 File Offset: 0x0005E8E1
		internal static Task FromCancellation(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				throw new ArgumentOutOfRangeException("cancellationToken");
			}
			return new Task(true, TaskCreationOptions.None, cancellationToken);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x000606FF File Offset: 0x0005E8FF
		public static Task FromCanceled(CancellationToken cancellationToken)
		{
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00060708 File Offset: 0x0005E908
		internal static Task<TResult> FromCancellation<TResult>(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				throw new ArgumentOutOfRangeException("cancellationToken");
			}
			return new Task<TResult>(true, default(TResult), TaskCreationOptions.None, cancellationToken);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0006073A File Offset: 0x0005E93A
		public static Task<TResult> FromCanceled<TResult>(CancellationToken cancellationToken)
		{
			return Task.FromCancellation<TResult>(cancellationToken);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00060742 File Offset: 0x0005E942
		internal static Task<TResult> FromCancellation<TResult>(OperationCanceledException exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = new Task<TResult>();
			task.TrySetCanceled(exception.CancellationToken, exception);
			return task;
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a task handle for that work.</summary>
		/// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
		/// <param name="action">The work to execute asynchronously</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> parameter was null.</exception>
		// Token: 0x06001948 RID: 6472 RVA: 0x00060768 File Offset: 0x0005E968
		public static Task Run(Action action)
		{
			return Task.InternalStartNew(null, action, null, default(CancellationToken), TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None);
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a Task(TResult) handle for that work.</summary>
		/// <returns>A Task(TResult) that represents the work queued to execute in the ThreadPool.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <typeparam name="TResult">The result type of the task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		// Token: 0x06001949 RID: 6473 RVA: 0x00060790 File Offset: 0x0005E990
		public static Task<TResult> Run<TResult>(Func<TResult> function)
		{
			return Task<TResult>.StartNew(null, function, default(CancellationToken), TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default);
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a proxy for the  task returned by <paramref name="function" />.</summary>
		/// <returns>A task that represents a proxy for the task returned by <paramref name="function" />.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		// Token: 0x0600194A RID: 6474 RVA: 0x000607B4 File Offset: 0x0005E9B4
		public static Task Run(Func<Task> function)
		{
			return Task.Run(function, default(CancellationToken));
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a proxy for the  task returned by <paramref name="function" />.</summary>
		/// <returns>A task that represents a proxy for the task returned by <paramref name="function" />.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x0600194B RID: 6475 RVA: 0x000607D0 File Offset: 0x0005E9D0
		public static Task Run(Func<Task> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			return new UnwrapPromise<VoidTaskResult>(Task.Factory.StartNew<Task>(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a proxy for the  Task(TResult) returned by <paramref name="function" />.</summary>
		/// <returns>A Task(TResult) that represents a proxy for the Task(TResult) returned by <paramref name="function" />.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <typeparam name="TResult">The type of the result returned by the proxy task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		// Token: 0x0600194C RID: 6476 RVA: 0x00060808 File Offset: 0x0005EA08
		public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
		{
			return Task.Run<TResult>(function, default(CancellationToken));
		}

		/// <summary>Queues the specified work to run on the ThreadPool and returns a proxy for the  Task(TResult) returned by <paramref name="function" />.</summary>
		/// <returns>A Task(TResult) that represents a proxy for the Task(TResult) returned by <paramref name="function" />.</returns>
		/// <param name="function">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
		/// <typeparam name="TResult">The type of the result returned by the proxy task.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was null.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x0600194D RID: 6477 RVA: 0x00060824 File Offset: 0x0005EA24
		public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<TResult>(cancellationToken);
			}
			return new UnwrapPromise<TResult>(Task.Factory.StartNew<Task<TResult>>(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
		}

		/// <summary>Creates a task that will complete after a time delay.</summary>
		/// <returns>A task that represents the time delay</returns>
		/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned task</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsDelay" /> is less than -1.</exception>
		// Token: 0x0600194E RID: 6478 RVA: 0x0006085C File Offset: 0x0005EA5C
		public static Task Delay(int millisecondsDelay)
		{
			return Task.Delay(millisecondsDelay, default(CancellationToken));
		}

		/// <summary>Creates a task that will complete after a time delay.</summary>
		/// <returns>A task that represents the time delay</returns>
		/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned task</param>
		/// <param name="cancellationToken">The cancellation token that will be checked prior to completing the returned task</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsDelay" /> is less than -1.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x0600194F RID: 6479 RVA: 0x00060878 File Offset: 0x0005EA78
		public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay", "The value needs to be either -1 (signifying an infinite timeout), 0 or a positive integer.");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			if (millisecondsDelay == 0)
			{
				return Task.CompletedTask;
			}
			Task.DelayPromise delayPromise = new Task.DelayPromise(cancellationToken);
			if (cancellationToken.CanBeCanceled)
			{
				delayPromise.Registration = cancellationToken.InternalRegisterWithoutEC(delegate(object state)
				{
					((Task.DelayPromise)state).Complete();
				}, delayPromise);
			}
			if (millisecondsDelay != -1)
			{
				delayPromise.Timer = new Timer(delegate(object state)
				{
					((Task.DelayPromise)state).Complete();
				}, delayPromise, millisecondsDelay, -1);
				delayPromise.Timer.KeepRootedWhileScheduled();
			}
			return delayPromise;
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06001950 RID: 6480 RVA: 0x0006092C File Offset: 0x0005EB2C
		public static Task<Task> WhenAny(params Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			int num = tasks.Length;
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				array[i] = task;
			}
			return TaskFactory.CommonCWAnyLogic(array);
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06001951 RID: 6481 RVA: 0x00060994 File Offset: 0x0005EB94
		public static Task<Task> WhenAny(IEnumerable<Task> tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			LowLevelListWithIList<Task> lowLevelListWithIList = new LowLevelListWithIList<Task>();
			foreach (Task task in tasks)
			{
				if (task == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				lowLevelListWithIList.Add(task);
			}
			if (lowLevelListWithIList.Count == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			return TaskFactory.CommonCWAnyLogic(lowLevelListWithIList);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00060A24 File Offset: 0x0005EC24
		[FriendAccessAllowed]
		internal static bool AddToActiveTasks(Task task)
		{
			object obj = Task.s_activeTasksLock;
			lock (obj)
			{
				Task.s_currentActiveTasks[task.Id] = task;
			}
			return true;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00060A70 File Offset: 0x0005EC70
		[FriendAccessAllowed]
		internal static void RemoveFromActiveTasks(int taskId)
		{
			object obj = Task.s_activeTasksLock;
			lock (obj)
			{
				Task.s_currentActiveTasks.Remove(taskId);
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00002C89 File Offset: 0x00000E89
		public void MarkAborted(ThreadAbortException e)
		{
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00060AB8 File Offset: 0x0005ECB8
		private void ExecuteWithThreadLocal(ref Task currentTaskSlot)
		{
			Task task = currentTaskSlot;
			try
			{
				currentTaskSlot = this;
				ExecutionContext capturedContext = this.CapturedContext;
				if (capturedContext == null)
				{
					this.Execute();
				}
				else
				{
					ContextCallback contextCallback = Task.s_ecCallback;
					if (contextCallback == null)
					{
						contextCallback = (Task.s_ecCallback = new ContextCallback(Task.ExecutionContextCallback));
					}
					ExecutionContext.Run(capturedContext, contextCallback, this, true);
				}
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
				}
				this.Finish(true);
			}
			finally
			{
				currentTaskSlot = task;
			}
		}

		// Token: 0x04000BCA RID: 3018
		internal static int s_taskIdCounter;

		// Token: 0x04000BCB RID: 3019
		private volatile int m_taskId;

		// Token: 0x04000BCC RID: 3020
		internal Delegate m_action;

		// Token: 0x04000BCD RID: 3021
		internal object m_stateObject;

		// Token: 0x04000BCE RID: 3022
		internal TaskScheduler m_taskScheduler;

		// Token: 0x04000BCF RID: 3023
		internal readonly Task m_parent;

		// Token: 0x04000BD0 RID: 3024
		internal volatile int m_stateFlags;

		// Token: 0x04000BD1 RID: 3025
		private volatile object m_continuationObject;

		// Token: 0x04000BD2 RID: 3026
		private static readonly object s_taskCompletionSentinel = new object();

		// Token: 0x04000BD3 RID: 3027
		internal static bool s_asyncDebuggingEnabled;

		// Token: 0x04000BD4 RID: 3028
		internal volatile Task.ContingentProperties m_contingentProperties;

		// Token: 0x04000BD5 RID: 3029
		private static readonly Action<object> s_taskCancelCallback = new Action<object>(Task.TaskCancelCallback);

		// Token: 0x04000BD6 RID: 3030
		[ThreadStatic]
		internal static Task t_currentTask;

		// Token: 0x04000BD7 RID: 3031
		[ThreadStatic]
		private static StackGuard t_stackGuard;

		// Token: 0x04000BD8 RID: 3032
		private static readonly Func<Task.ContingentProperties> s_createContingentProperties = () => new Task.ContingentProperties();

		// Token: 0x04000BDB RID: 3035
		private static readonly Predicate<Task> s_IsExceptionObservedByParentPredicate = (Task t) => t.IsExceptionObservedByParent;

		// Token: 0x04000BDC RID: 3036
		private static ContextCallback s_ecCallback;

		// Token: 0x04000BDD RID: 3037
		private static readonly Predicate<object> s_IsTaskContinuationNullPredicate = (object tc) => tc == null;

		// Token: 0x04000BDE RID: 3038
		private static readonly Dictionary<int, Task> s_currentActiveTasks = new Dictionary<int, Task>();

		// Token: 0x04000BDF RID: 3039
		private static readonly object s_activeTasksLock = new object();

		// Token: 0x020002AF RID: 687
		internal class ContingentProperties
		{
			// Token: 0x06001957 RID: 6487 RVA: 0x00060BD0 File Offset: 0x0005EDD0
			internal void SetCompleted()
			{
				ManualResetEventSlim completionEvent = this.m_completionEvent;
				if (completionEvent != null)
				{
					completionEvent.Set();
				}
			}

			// Token: 0x06001958 RID: 6488 RVA: 0x00060BF0 File Offset: 0x0005EDF0
			internal void UnregisterCancellationCallback()
			{
				if (this.m_cancellationRegistration != null)
				{
					try
					{
						((CancellationTokenRegistration)this.m_cancellationRegistration).Dispose();
					}
					catch (ObjectDisposedException)
					{
					}
					this.m_cancellationRegistration = null;
				}
			}

			// Token: 0x04000BE0 RID: 3040
			internal ExecutionContext m_capturedContext;

			// Token: 0x04000BE1 RID: 3041
			internal volatile ManualResetEventSlim m_completionEvent;

			// Token: 0x04000BE2 RID: 3042
			internal volatile TaskExceptionHolder m_exceptionsHolder;

			// Token: 0x04000BE3 RID: 3043
			internal CancellationToken m_cancellationToken;

			// Token: 0x04000BE4 RID: 3044
			internal object m_cancellationRegistration;

			// Token: 0x04000BE5 RID: 3045
			internal volatile int m_internalCancellationRequested;

			// Token: 0x04000BE6 RID: 3046
			internal volatile int m_completionCountdown = 1;

			// Token: 0x04000BE7 RID: 3047
			internal volatile LowLevelListWithIList<Task> m_exceptionalChildren;
		}

		// Token: 0x020002B0 RID: 688
		private sealed class SetOnInvokeMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x0600195A RID: 6490 RVA: 0x00060C45 File Offset: 0x0005EE45
			internal SetOnInvokeMres()
				: base(false, 0)
			{
			}

			// Token: 0x0600195B RID: 6491 RVA: 0x00060C4F File Offset: 0x0005EE4F
			public void Invoke(Task completingTask)
			{
				base.Set();
			}

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x0600195C RID: 6492 RVA: 0x00033991 File Offset: 0x00031B91
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020002B1 RID: 689
		private sealed class DelayPromise : Task<VoidTaskResult>
		{
			// Token: 0x0600195D RID: 6493 RVA: 0x00060C57 File Offset: 0x0005EE57
			internal DelayPromise(CancellationToken token)
			{
				this.Token = token;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "Task.Delay", 0UL);
				}
				DebuggerSupport.AddToActiveTasks(this);
			}

			// Token: 0x0600195E RID: 6494 RVA: 0x00060C84 File Offset: 0x0005EE84
			internal void Complete()
			{
				bool flag;
				if (this.Token.IsCancellationRequested)
				{
					flag = base.TrySetCanceled(this.Token);
				}
				else
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(this);
					flag = base.TrySetResult(default(VoidTaskResult));
				}
				if (flag)
				{
					if (this.Timer != null)
					{
						this.Timer.Dispose();
					}
					this.Registration.Dispose();
				}
			}

			// Token: 0x04000BE8 RID: 3048
			internal readonly CancellationToken Token;

			// Token: 0x04000BE9 RID: 3049
			internal CancellationTokenRegistration Registration;

			// Token: 0x04000BEA RID: 3050
			internal Timer Timer;
		}
	}
}
