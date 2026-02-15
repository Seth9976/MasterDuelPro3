using System;
using System.Collections.Generic;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for creating and scheduling <see cref="T:System.Threading.Tasks.Task" /> objects.</summary>
	// Token: 0x020002C8 RID: 712
	public class TaskFactory
	{
		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the default configuration.</summary>
		// Token: 0x060019AA RID: 6570 RVA: 0x00061998 File Offset: 0x0005FB98
		public TaskFactory()
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The default <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another CancellationToken is explicitly specified while calling the factory methods.</param>
		/// <param name="creationOptions">The default <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> to use when creating tasks with this TaskFactory.</param>
		/// <param name="continuationOptions">The default <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> to use when creating continuation tasks with this TaskFactory.</param>
		/// <param name="scheduler">The default <see cref="T:System.Threading.Tasks.TaskScheduler" /> to use to schedule any Tasks created with this TaskFactory. A null value indicates that TaskScheduler.Current should be used.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument or the <paramref name="continuationOptions" /> argument specifies an invalid value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060019AB RID: 6571 RVA: 0x000619B7 File Offset: 0x0005FBB7
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000619E8 File Offset: 0x0005FBE8
		internal static void CheckCreationOptions(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060019AD RID: 6573 RVA: 0x000619FB File Offset: 0x0005FBFB
		public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060019AE RID: 6574 RVA: 0x00061A0F File Offset: 0x0005FC0F
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, state, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060019AF RID: 6575 RVA: 0x00061A25 File Offset: 0x0005FC25
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.-or-The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060019B0 RID: 6576 RVA: 0x00061A38 File Offset: 0x0005FC38
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060019B1 RID: 6577 RVA: 0x00061A4E File Offset: 0x0005FC4E
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state)
		{
			return this.FromAsync<TArg1>(beginMethod, endMethod, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060019B2 RID: 6578 RVA: 0x00061A61 File Offset: 0x0005FC61
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1>(beginMethod, null, endMethod, arg1, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060019B3 RID: 6579 RVA: 0x00061A70 File Offset: 0x0005FC70
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return this.FromAsync<TArg1, TArg2>(beginMethod, endMethod, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060019B4 RID: 6580 RVA: 0x00061A85 File Offset: 0x0005FC85
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, null, endMethod, arg1, arg2, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060019B5 RID: 6581 RVA: 0x00061A96 File Offset: 0x0005FC96
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return this.FromAsync<TArg1, TArg2, TArg3>(beginMethod, endMethod, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.-or-The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060019B6 RID: 6582 RVA: 0x00061AAD File Offset: 0x0005FCAD
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, null, endMethod, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00061AC0 File Offset: 0x0005FCC0
		internal static void CheckFromAsyncOptions(TaskCreationOptions creationOptions, bool hasBeginMethod)
		{
			if (hasBeginMethod)
			{
				if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", "It is invalid to specify TaskCreationOptions.LongRunning in calls to FromAsync.");
				}
				if ((creationOptions & TaskCreationOptions.PreferFairness) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", "It is invalid to specify TaskCreationOptions.PreferFairness in calls to FromAsync.");
				}
			}
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00061B00 File Offset: 0x0005FD00
		internal static Task<Task> CommonCWAnyLogic(IList<Task> tasks)
		{
			TaskFactory.CompleteOnInvokePromise completeOnInvokePromise = new TaskFactory.CompleteOnInvokePromise(tasks);
			bool flag = false;
			int count = tasks.Count;
			for (int i = 0; i < count; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				if (!flag)
				{
					if (completeOnInvokePromise.IsCompleted)
					{
						flag = true;
					}
					else if (task.IsCompleted)
					{
						completeOnInvokePromise.Invoke(task);
						flag = true;
					}
					else
					{
						task.AddCompletionAction(completeOnInvokePromise);
						if (completeOnInvokePromise.IsCompleted)
						{
							task.RemoveContinuation(completeOnInvokePromise);
						}
					}
				}
			}
			return completeOnInvokePromise;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00061B84 File Offset: 0x0005FD84
		internal static void CheckMultiTaskContinuationOptions(TaskContinuationOptions continuationOptions)
		{
			if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
			{
				throw new ArgumentOutOfRangeException("continuationOptions", "The specified TaskContinuationOptions combined LongRunning and ExecuteSynchronously.  Synchronous continuations should not be long running.");
			}
			if ((continuationOptions & ~(TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & (TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", "It is invalid to exclude specific continuation kinds for continuations off of multiple tasks.");
			}
		}

		// Token: 0x04000C2A RID: 3114
		private readonly CancellationToken m_defaultCancellationToken;

		// Token: 0x04000C2B RID: 3115
		private readonly TaskScheduler m_defaultScheduler;

		// Token: 0x04000C2C RID: 3116
		private readonly TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04000C2D RID: 3117
		private readonly TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x020002C9 RID: 713
		internal sealed class CompleteOnInvokePromise : Task<Task>, ITaskCompletionAction
		{
			// Token: 0x060019BA RID: 6586 RVA: 0x00061BDC File Offset: 0x0005FDDC
			public CompleteOnInvokePromise(IList<Task> tasks)
			{
				this._tasks = tasks;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "TaskFactory.ContinueWhenAny", 0UL);
				}
				DebuggerSupport.AddToActiveTasks(this);
			}

			// Token: 0x060019BB RID: 6587 RVA: 0x00061C08 File Offset: 0x0005FE08
			public void Invoke(Task completingTask)
			{
				if (base.TrySetResult(completingTask))
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, this, CausalityRelation.Choice);
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(this);
					IList<Task> tasks = this._tasks;
					int count = tasks.Count;
					for (int i = 0; i < count; i++)
					{
						Task task = tasks[i];
						if (task != null && !task.IsCompleted)
						{
							task.RemoveContinuation(this);
						}
					}
					this._tasks = null;
				}
			}

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x060019BC RID: 6588 RVA: 0x0000C091 File Offset: 0x0000A291
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04000C2E RID: 3118
			private IList<Task> _tasks;
		}
	}
}
