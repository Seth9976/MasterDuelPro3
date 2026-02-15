using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>Manages the execution context for the current thread. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000258 RID: 600
	[Serializable]
	public sealed class ExecutionContext : IDisposable, ISerializable
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00057D2B File Offset: 0x00055F2B
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x00057D38 File Offset: 0x00055F38
		internal bool isNewCapture
		{
			get
			{
				return (this._flags & (ExecutionContext.Flags)5) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsNewCapture;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-2);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00057D5B File Offset: 0x00055F5B
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x00057D68 File Offset: 0x00055F68
		internal bool isFlowSuppressed
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsFlowSuppressed) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsFlowSuppressed;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-3);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x00057D8B File Offset: 0x00055F8B
		internal static ExecutionContext PreAllocatedDefault
		{
			get
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00057D92 File Offset: 0x00055F92
		internal bool IsPreAllocatedDefault
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsPreAllocatedDefault) != ExecutionContext.Flags.None;
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00003CE1 File Offset: 0x00001EE1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext()
		{
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00057DA1 File Offset: 0x00055FA1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext(bool isPreAllocatedDefault)
		{
			if (isPreAllocatedDefault)
			{
				this._flags = ExecutionContext.Flags.IsPreAllocatedDefault;
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00057DB4 File Offset: 0x00055FB4
		internal static object GetLocalValue(IAsyncLocal local)
		{
			return Thread.CurrentThread.GetExecutionContextReader().GetLocalValue(local);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00057DD4 File Offset: 0x00055FD4
		internal static void SetLocalValue(IAsyncLocal local, object newValue, bool needChangeNotifications)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			object obj = null;
			bool flag = mutableExecutionContext._localValues != null && mutableExecutionContext._localValues.TryGetValue(local, out obj);
			if (obj == newValue)
			{
				return;
			}
			if (mutableExecutionContext._localValues == null)
			{
				mutableExecutionContext._localValues = new Dictionary<IAsyncLocal, object>();
			}
			else
			{
				mutableExecutionContext._localValues = new Dictionary<IAsyncLocal, object>(mutableExecutionContext._localValues);
			}
			mutableExecutionContext._localValues[local] = newValue;
			if (needChangeNotifications)
			{
				if (!flag)
				{
					if (mutableExecutionContext._localChangeNotifications == null)
					{
						mutableExecutionContext._localChangeNotifications = new List<IAsyncLocal>();
					}
					else
					{
						mutableExecutionContext._localChangeNotifications = new List<IAsyncLocal>(mutableExecutionContext._localChangeNotifications);
					}
					mutableExecutionContext._localChangeNotifications.Add(local);
				}
				local.OnValueChanged(obj, newValue, false);
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00057E84 File Offset: 0x00056084
		[HandleProcessCorruptedStateExceptions]
		internal static void OnAsyncLocalContextChanged(ExecutionContext previous, ExecutionContext current)
		{
			List<IAsyncLocal> list = ((previous == null) ? null : previous._localChangeNotifications);
			if (list != null)
			{
				foreach (IAsyncLocal asyncLocal in list)
				{
					object obj = null;
					if (previous != null && previous._localValues != null)
					{
						previous._localValues.TryGetValue(asyncLocal, out obj);
					}
					object obj2 = null;
					if (current != null && current._localValues != null)
					{
						current._localValues.TryGetValue(asyncLocal, out obj2);
					}
					if (obj != obj2)
					{
						asyncLocal.OnValueChanged(obj, obj2, true);
					}
				}
			}
			List<IAsyncLocal> list2 = ((current == null) ? null : current._localChangeNotifications);
			if (list2 != null && list2 != list)
			{
				try
				{
					foreach (IAsyncLocal asyncLocal2 in list2)
					{
						object obj3 = null;
						if (previous == null || previous._localValues == null || !previous._localValues.TryGetValue(asyncLocal2, out obj3))
						{
							object obj4 = null;
							if (current != null && current._localValues != null)
							{
								current._localValues.TryGetValue(asyncLocal2, out obj4);
							}
							if (obj3 != obj4)
							{
								asyncLocal2.OnValueChanged(obj3, obj4, true);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Environment.FailFast(Environment.GetResourceString("An exception was not handled in an AsyncLocal<T> notification callback."), ex);
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00057FEC File Offset: 0x000561EC
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x00058007 File Offset: 0x00056207
		internal LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._logicalCallContext == null)
				{
					this._logicalCallContext = new LogicalCallContext();
				}
				return this._logicalCallContext;
			}
			set
			{
				this._logicalCallContext = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00058010 File Offset: 0x00056210
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0005802B File Offset: 0x0005622B
		internal IllogicalCallContext IllogicalCallContext
		{
			get
			{
				if (this._illogicalCallContext == null)
				{
					this._illogicalCallContext = new IllogicalCallContext();
				}
				return this._illogicalCallContext;
			}
			set
			{
				this._illogicalCallContext = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00058034 File Offset: 0x00056234
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0005803C File Offset: 0x0005623C
		internal SynchronizationContext SynchronizationContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContext;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContext = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00058045 File Offset: 0x00056245
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0005804D File Offset: 0x0005624D
		internal SynchronizationContext SynchronizationContextNoFlow
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContextNoFlow;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContextNoFlow = value;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ExecutionContext" /> class.</summary>
		// Token: 0x060015E8 RID: 5608 RVA: 0x00058056 File Offset: 0x00056256
		public void Dispose()
		{
			bool isPreAllocatedDefault = this.IsPreAllocatedDefault;
		}

		/// <summary>Runs a method in a specified execution context on the current thread.</summary>
		/// <param name="executionContext">The <see cref="T:System.Threading.ExecutionContext" /> to set.</param>
		/// <param name="callback">A <see cref="T:System.Threading.ContextCallback" /> delegate that represents the method to be run in the provided execution context.</param>
		/// <param name="state">The object to pass to the callback method.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="executionContext" /> is null.-or-<paramref name="executionContext" /> was not acquired through a capture operation. -or-<paramref name="executionContext" /> has already been used as the argument to a <see cref="M:System.Threading.ExecutionContext.Run(System.Threading.ExecutionContext,System.Threading.ContextCallback,System.Object)" /> call.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060015E9 RID: 5609 RVA: 0x0005805F File Offset: 0x0005625F
		public static void Run(ExecutionContext executionContext, ContextCallback callback, object state)
		{
			if (executionContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot call Set on a null context"));
			}
			if (!executionContext.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot apply a context that has been marshaled across AppDomains, that was not acquired through a Capture operation or that has already been the argument to a Set call."));
			}
			ExecutionContext.Run(executionContext, callback, state, false);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00058095 File Offset: 0x00056295
		[FriendAccessAllowed]
		internal static void Run(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			ExecutionContext.RunInternal(executionContext, callback, state, preserveSyncCtx);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000580A0 File Offset: 0x000562A0
		internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state)
		{
			ExecutionContext.RunInternal(executionContext, callback, state, false);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000580AC File Offset: 0x000562AC
		[HandleProcessCorruptedStateExceptions]
		internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			if (!executionContext.IsPreAllocatedDefault)
			{
				executionContext.isNewCapture = false;
			}
			Thread currentThread = Thread.CurrentThread;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
				if ((executionContextReader.IsNull || executionContextReader.IsDefaultFTContext(preserveSyncCtx)) && executionContext.IsDefaultFTContext(preserveSyncCtx) && executionContextReader.HasSameLocalValues(executionContext))
				{
					ExecutionContext.EstablishCopyOnWriteScope(currentThread, true, ref executionContextSwitcher);
				}
				else
				{
					if (executionContext.IsPreAllocatedDefault)
					{
						executionContext = new ExecutionContext();
					}
					executionContextSwitcher = ExecutionContext.SetExecutionContext(executionContext, preserveSyncCtx);
				}
				callback(state);
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0005814C File Offset: 0x0005634C
		internal static void RunInternal<TState>(ExecutionContext executionContext, ContextCallback<TState> callback, ref TState state)
		{
			ExecutionContext.RunInternal<TState>(executionContext, callback, ref state, false);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00058158 File Offset: 0x00056358
		[HandleProcessCorruptedStateExceptions]
		internal static void RunInternal<TState>(ExecutionContext executionContext, ContextCallback<TState> callback, ref TState state, bool preserveSyncCtx)
		{
			if (!executionContext.IsPreAllocatedDefault)
			{
				executionContext.isNewCapture = false;
			}
			Thread currentThread = Thread.CurrentThread;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
				if ((executionContextReader.IsNull || executionContextReader.IsDefaultFTContext(preserveSyncCtx)) && executionContext.IsDefaultFTContext(preserveSyncCtx) && executionContextReader.HasSameLocalValues(executionContext))
				{
					ExecutionContext.EstablishCopyOnWriteScope(currentThread, true, ref executionContextSwitcher);
				}
				else
				{
					if (executionContext.IsPreAllocatedDefault)
					{
						executionContext = new ExecutionContext();
					}
					executionContextSwitcher = ExecutionContext.SetExecutionContext(executionContext, preserveSyncCtx);
				}
				callback(ref state);
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000581F8 File Offset: 0x000563F8
		internal static void EstablishCopyOnWriteScope(ref ExecutionContextSwitcher ecsw)
		{
			ExecutionContext.EstablishCopyOnWriteScope(Thread.CurrentThread, false, ref ecsw);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00058206 File Offset: 0x00056406
		private static void EstablishCopyOnWriteScope(Thread currentThread, bool knownNullWindowsIdentity, ref ExecutionContextSwitcher ecsw)
		{
			ecsw.outerEC = currentThread.GetExecutionContextReader();
			ecsw.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			currentThread.ExecutionContextBelongsToCurrentScope = false;
			ecsw.thread = currentThread;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00058230 File Offset: 0x00056430
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContextSwitcher SetExecutionContext(ExecutionContext executionContext, bool preserveSyncCtx)
		{
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			Thread currentThread = Thread.CurrentThread;
			ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
			executionContextSwitcher.thread = currentThread;
			executionContextSwitcher.outerEC = executionContextReader;
			executionContextSwitcher.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			if (preserveSyncCtx)
			{
				executionContext.SynchronizationContext = executionContextReader.SynchronizationContext;
			}
			executionContext.SynchronizationContextNoFlow = executionContextReader.SynchronizationContextNoFlow;
			currentThread.SetExecutionContext(executionContext, true);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), executionContext);
			}
			catch
			{
				executionContextSwitcher.UndoNoThrow();
				throw;
			}
			return executionContextSwitcher;
		}

		/// <summary>Creates a copy of the current execution context.</summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object representing the current execution context.</returns>
		/// <exception cref="T:System.InvalidOperationException">This context cannot be copied because it is used. Only newly captured contexts can be copied.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015F2 RID: 5618 RVA: 0x000582C4 File Offset: 0x000564C4
		public ExecutionContext CreateCopy()
		{
			if (!this.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Only newly captured contexts can be copied"));
			}
			ExecutionContext executionContext = new ExecutionContext();
			executionContext.isNewCapture = true;
			executionContext._syncContext = ((this._syncContext == null) ? null : this._syncContext.CreateCopy());
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			return executionContext;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0005834C File Offset: 0x0005654C
		internal ExecutionContext CreateMutableCopy()
		{
			ExecutionContext executionContext = new ExecutionContext();
			executionContext._syncContext = this._syncContext;
			executionContext._syncContextNoFlow = this._syncContextNoFlow;
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			if (this._illogicalCallContext != null)
			{
				executionContext.IllogicalCallContext = this.IllogicalCallContext.CreateCopy();
			}
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			executionContext.isFlowSuppressed = this.isFlowSuppressed;
			return executionContext;
		}

		/// <summary>Suppresses the flow of the execution context across asynchronous threads.</summary>
		/// <returns>An <see cref="T:System.Threading.AsyncFlowControl" /> structure for restoring the flow.</returns>
		/// <exception cref="T:System.InvalidOperationException">The context flow is already suppressed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060015F4 RID: 5620 RVA: 0x000583D4 File Offset: 0x000565D4
		public static AsyncFlowControl SuppressFlow()
		{
			if (ExecutionContext.IsFlowSuppressed())
			{
				throw new InvalidOperationException(Environment.GetResourceString("Context flow is already suppressed."));
			}
			AsyncFlowControl asyncFlowControl = default(AsyncFlowControl);
			asyncFlowControl.Setup();
			return asyncFlowControl;
		}

		/// <summary>Restores the flow of the execution context across asynchronous threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The context flow cannot be restored because it is not being suppressed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F5 RID: 5621 RVA: 0x00058408 File Offset: 0x00056608
		public static void RestoreFlow()
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (!mutableExecutionContext.isFlowSuppressed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cannot restore context flow when it is not suppressed."));
			}
			mutableExecutionContext.isFlowSuppressed = false;
		}

		/// <summary>Indicates whether the flow of the execution context is currently suppressed.</summary>
		/// <returns>true if the flow is suppressed; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F6 RID: 5622 RVA: 0x00058434 File Offset: 0x00056634
		public static bool IsFlowSuppressed()
		{
			return Thread.CurrentThread.GetExecutionContextReader().IsFlowSuppressed;
		}

		/// <summary>Captures the execution context from the current thread.</summary>
		/// <returns>An <see cref="T:System.Threading.ExecutionContext" /> object representing the execution context for the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015F7 RID: 5623 RVA: 0x00058454 File Offset: 0x00056654
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ExecutionContext Capture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.None);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0005846C File Offset: 0x0005666C
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContext FastCapture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00058484 File Offset: 0x00056684
		internal static ExecutionContext Capture(ref StackCrawlMark stackMark, ExecutionContext.CaptureOptions options)
		{
			ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
			if (executionContextReader.IsFlowSuppressed)
			{
				return null;
			}
			SynchronizationContext synchronizationContext = null;
			LogicalCallContext logicalCallContext = null;
			if (!executionContextReader.IsNull)
			{
				if ((options & ExecutionContext.CaptureOptions.IgnoreSyncCtx) == ExecutionContext.CaptureOptions.None)
				{
					synchronizationContext = ((executionContextReader.SynchronizationContext == null) ? null : executionContextReader.SynchronizationContext.CreateCopy());
				}
				if (executionContextReader.LogicalCallContext.HasInfo)
				{
					logicalCallContext = executionContextReader.LogicalCallContext.Clone();
				}
			}
			Dictionary<IAsyncLocal, object> dictionary = null;
			List<IAsyncLocal> list = null;
			if (!executionContextReader.IsNull)
			{
				dictionary = executionContextReader.DangerousGetRawExecutionContext()._localValues;
				list = executionContextReader.DangerousGetRawExecutionContext()._localChangeNotifications;
			}
			if ((options & ExecutionContext.CaptureOptions.OptimizeDefaultCase) != ExecutionContext.CaptureOptions.None && synchronizationContext == null && (logicalCallContext == null || !logicalCallContext.HasInfo) && dictionary == null && list == null)
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
			return new ExecutionContext
			{
				_syncContext = synchronizationContext,
				LogicalCallContext = logicalCallContext,
				_localValues = dictionary,
				_localChangeNotifications = list,
				isNewCapture = true
			};
		}

		/// <summary>Sets the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of the current execution context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure representing the destination context of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015FA RID: 5626 RVA: 0x00058567 File Offset: 0x00056767
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this._logicalCallContext != null)
			{
				info.AddValue("LogicalCallContext", this._logicalCallContext, typeof(LogicalCallContext));
			}
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0005859C File Offset: 0x0005679C
		private ExecutionContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("LogicalCallContext"))
				{
					this._logicalCallContext = (LogicalCallContext)enumerator.Value;
				}
			}
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000585E3 File Offset: 0x000567E3
		internal bool IsDefaultFTContext(bool ignoreSyncCtx)
		{
			return (ignoreSyncCtx || this._syncContext == null) && (this._logicalCallContext == null || !this._logicalCallContext.HasInfo) && (this._illogicalCallContext == null || !this._illogicalCallContext.HasUserData);
		}

		// Token: 0x04000ABF RID: 2751
		private SynchronizationContext _syncContext;

		// Token: 0x04000AC0 RID: 2752
		private SynchronizationContext _syncContextNoFlow;

		// Token: 0x04000AC1 RID: 2753
		private LogicalCallContext _logicalCallContext;

		// Token: 0x04000AC2 RID: 2754
		private IllogicalCallContext _illogicalCallContext;

		// Token: 0x04000AC3 RID: 2755
		private ExecutionContext.Flags _flags;

		// Token: 0x04000AC4 RID: 2756
		private Dictionary<IAsyncLocal, object> _localValues;

		// Token: 0x04000AC5 RID: 2757
		private List<IAsyncLocal> _localChangeNotifications;

		// Token: 0x04000AC6 RID: 2758
		private static readonly ExecutionContext s_dummyDefaultEC = new ExecutionContext(true);

		// Token: 0x04000AC7 RID: 2759
		internal static readonly ExecutionContext Default = new ExecutionContext();

		// Token: 0x02000259 RID: 601
		private enum Flags
		{
			// Token: 0x04000AC9 RID: 2761
			None,
			// Token: 0x04000ACA RID: 2762
			IsNewCapture,
			// Token: 0x04000ACB RID: 2763
			IsFlowSuppressed,
			// Token: 0x04000ACC RID: 2764
			IsPreAllocatedDefault = 4
		}

		// Token: 0x0200025A RID: 602
		internal struct Reader
		{
			// Token: 0x060015FE RID: 5630 RVA: 0x00058638 File Offset: 0x00056838
			public Reader(ExecutionContext ec)
			{
				this.m_ec = ec;
			}

			// Token: 0x060015FF RID: 5631 RVA: 0x00058641 File Offset: 0x00056841
			public ExecutionContext DangerousGetRawExecutionContext()
			{
				return this.m_ec;
			}

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06001600 RID: 5632 RVA: 0x00058649 File Offset: 0x00056849
			public bool IsNull
			{
				get
				{
					return this.m_ec == null;
				}
			}

			// Token: 0x06001601 RID: 5633 RVA: 0x00058654 File Offset: 0x00056854
			public bool IsDefaultFTContext(bool ignoreSyncCtx)
			{
				return this.m_ec.IsDefaultFTContext(ignoreSyncCtx);
			}

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06001602 RID: 5634 RVA: 0x00058662 File Offset: 0x00056862
			public bool IsFlowSuppressed
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsNull && this.m_ec.isFlowSuppressed;
				}
			}

			// Token: 0x06001603 RID: 5635 RVA: 0x00058679 File Offset: 0x00056879
			public bool IsSame(ExecutionContext.Reader other)
			{
				return this.m_ec == other.m_ec;
			}

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x06001604 RID: 5636 RVA: 0x00058689 File Offset: 0x00056889
			public SynchronizationContext SynchronizationContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContext;
					}
					return null;
				}
			}

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06001605 RID: 5637 RVA: 0x000586A0 File Offset: 0x000568A0
			public SynchronizationContext SynchronizationContextNoFlow
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContextNoFlow;
					}
					return null;
				}
			}

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06001606 RID: 5638 RVA: 0x000586B7 File Offset: 0x000568B7
			public LogicalCallContext.Reader LogicalCallContext
			{
				get
				{
					return new LogicalCallContext.Reader(this.IsNull ? null : this.m_ec.LogicalCallContext);
				}
			}

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x06001607 RID: 5639 RVA: 0x000586D4 File Offset: 0x000568D4
			public IllogicalCallContext.Reader IllogicalCallContext
			{
				get
				{
					return new IllogicalCallContext.Reader(this.IsNull ? null : this.m_ec.IllogicalCallContext);
				}
			}

			// Token: 0x06001608 RID: 5640 RVA: 0x000586F4 File Offset: 0x000568F4
			public object GetLocalValue(IAsyncLocal local)
			{
				if (this.IsNull)
				{
					return null;
				}
				if (this.m_ec._localValues == null)
				{
					return null;
				}
				object obj;
				this.m_ec._localValues.TryGetValue(local, out obj);
				return obj;
			}

			// Token: 0x06001609 RID: 5641 RVA: 0x00058730 File Offset: 0x00056930
			public bool HasSameLocalValues(ExecutionContext other)
			{
				Dictionary<IAsyncLocal, object> dictionary = (this.IsNull ? null : this.m_ec._localValues);
				Dictionary<IAsyncLocal, object> dictionary2 = ((other == null) ? null : other._localValues);
				return dictionary == dictionary2;
			}

			// Token: 0x0600160A RID: 5642 RVA: 0x00058763 File Offset: 0x00056963
			public bool HasLocalValues()
			{
				return !this.IsNull && this.m_ec._localValues != null;
			}

			// Token: 0x04000ACD RID: 2765
			private ExecutionContext m_ec;
		}

		// Token: 0x0200025B RID: 603
		[Flags]
		internal enum CaptureOptions
		{
			// Token: 0x04000ACF RID: 2767
			None = 0,
			// Token: 0x04000AD0 RID: 2768
			IgnoreSyncCtx = 1,
			// Token: 0x04000AD1 RID: 2769
			OptimizeDefaultCase = 2
		}
	}
}
