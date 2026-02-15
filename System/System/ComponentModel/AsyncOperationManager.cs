using System;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides concurrency management for classes that support asynchronous method calls. This class cannot be inherited.</summary>
	// Token: 0x0200023B RID: 571
	public static class AsyncOperationManager
	{
		/// <summary>Returns an <see cref="T:System.ComponentModel.AsyncOperation" /> for tracking the duration of a particular asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AsyncOperation" /> that you can use to track the duration of an asynchronous method invocation.</returns>
		/// <param name="userSuppliedState">An object used to associate a piece of client state, such as a task ID, with a particular asynchronous operation. </param>
		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003DF4F File Offset: 0x0003C14F
		public static AsyncOperation CreateOperation(object userSuppliedState)
		{
			return AsyncOperation.CreateOperation(userSuppliedState, AsyncOperationManager.SynchronizationContext);
		}

		/// <summary>Gets or sets the synchronization context for the asynchronous operation.</summary>
		/// <returns>The synchronization context for the asynchronous operation.</returns>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0003DF5C File Offset: 0x0003C15C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static SynchronizationContext SynchronizationContext
		{
			get
			{
				if (SynchronizationContext.Current == null)
				{
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				}
				return SynchronizationContext.Current;
			}
		}
	}
}
