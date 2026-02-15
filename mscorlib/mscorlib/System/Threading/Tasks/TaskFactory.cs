using System;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for creating and scheduling <see cref="T:System.Threading.Tasks.Task`1" /> objects.</summary>
	/// <typeparam name="TResult">The type of the results that are available through the <see cref="T:System.Threading.Tasks.Task`1" /> objects that are associated with the methods in this class.</typeparam>
	// Token: 0x020002A7 RID: 679
	public class TaskFactory<TResult>
	{
		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the default configuration.</summary>
		// Token: 0x060018BD RID: 6333 RVA: 0x0005E4D8 File Offset: 0x0005C6D8
		public TaskFactory()
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The default cancellation token that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another cancellation token is explicitly specified when calling the factory methods.</param>
		/// <param name="creationOptions">The default options to use when creating tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <param name="continuationOptions">The default options to use when creating continuation tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <param name="scheduler">The default scheduler to use to schedule any tasks created with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />. A null value indicates that <see cref="P:System.Threading.Tasks.TaskScheduler.Current" /> should be used.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationOptions" /> or <paramref name="continuationOptions" /> specifies an invalid value.</exception>
		// Token: 0x060018BE RID: 6334 RVA: 0x0005E4F7 File Offset: 0x0005C6F7
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		/// <summary>Creates and starts a task.</summary>
		/// <returns>The started task.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the created task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created<paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.-or-The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x060018BF RID: 6335 RVA: 0x0005E528 File Offset: 0x0005C728
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0005E540 File Offset: 0x0005C740
		private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
		{
			Exception ex = null;
			OperationCanceledException ex2 = null;
			TResult tresult = default(TResult);
			try
			{
				if (endFunction != null)
				{
					tresult = endFunction(iar);
				}
				else
				{
					endAction(iar);
				}
			}
			catch (OperationCanceledException ex2)
			{
			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (ex2 != null)
				{
					promise.TrySetCanceled(ex2.CancellationToken, ex2);
				}
				else if (ex != null)
				{
					promise.TrySetException(ex);
				}
				else
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(promise);
					if (requiresSynchronization)
					{
						promise.TrySetResult(tresult);
					}
					else
					{
						promise.DangerousSetResult(tresult);
					}
				}
			}
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060018C1 RID: 6337 RVA: 0x0005E5E8 File Offset: 0x0005C7E8
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0005E5FC File Offset: 0x0005C7FC
		internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is null.-or-The <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060018C3 RID: 6339 RVA: 0x0005E718 File Offset: 0x0005C918
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0005E72C File Offset: 0x0005C92C
		internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endFunction");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0005E84C File Offset: 0x0005CA4C
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0005E96C File Offset: 0x0005CB6C
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0005EA90 File Offset: 0x0005CC90
		internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
		{
			TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
			IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, fromAsyncTrimPromise);
			if (asyncResult.CompletedSynchronously)
			{
				fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
			}
			return fromAsyncTrimPromise;
		}

		// Token: 0x04000BAE RID: 2990
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04000BAF RID: 2991
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04000BB0 RID: 2992
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04000BB1 RID: 2993
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x020002A8 RID: 680
		private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
		{
			// Token: 0x060018C8 RID: 6344 RVA: 0x0005EAC7 File Offset: 0x0005CCC7
			internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
			{
				this.m_thisRef = thisRef;
				this.m_endMethod = endMethod;
			}

			// Token: 0x060018C9 RID: 6345 RVA: 0x0005EAE0 File Offset: 0x0005CCE0
			internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
				if (fromAsyncTrimPromise == null)
				{
					throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult.", "asyncResult");
				}
				TInstance thisRef = fromAsyncTrimPromise.m_thisRef;
				Func<TInstance, IAsyncResult, TResult> endMethod = fromAsyncTrimPromise.m_endMethod;
				fromAsyncTrimPromise.m_thisRef = default(TInstance);
				fromAsyncTrimPromise.m_endMethod = null;
				if (endMethod == null)
				{
					throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult.", "asyncResult");
				}
				if (!asyncResult.CompletedSynchronously)
				{
					fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, true);
				}
			}

			// Token: 0x060018CA RID: 6346 RVA: 0x0005EB60 File Offset: 0x0005CD60
			internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
			{
				try
				{
					TResult tresult = endMethod(thisRef, asyncResult);
					if (requiresSynchronization)
					{
						base.TrySetResult(tresult);
					}
					else
					{
						base.DangerousSetResult(tresult);
					}
				}
				catch (OperationCanceledException ex)
				{
					base.TrySetCanceled(ex.CancellationToken, ex);
				}
				catch (Exception ex2)
				{
					base.TrySetException(ex2);
				}
			}

			// Token: 0x04000BB2 RID: 2994
			internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);

			// Token: 0x04000BB3 RID: 2995
			private TInstance m_thisRef;

			// Token: 0x04000BB4 RID: 2996
			private Func<TInstance, IAsyncResult, TResult> m_endMethod;
		}
	}
}
