using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	/// <summary>Represents an exception used to communicate task cancellation.</summary>
	// Token: 0x02000296 RID: 662
	[Serializable]
	public class TaskCanceledException : OperationCanceledException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x0600184C RID: 6220 RVA: 0x0005D481 File Offset: 0x0005B681
		public TaskCanceledException()
			: base("A task was canceled.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with a reference to the <see cref="T:System.Threading.Tasks.Task" /> that has been canceled.</summary>
		/// <param name="task">A task that has been canceled.</param>
		// Token: 0x0600184D RID: 6221 RVA: 0x0005D490 File Offset: 0x0005B690
		public TaskCanceledException(Task task)
			: base("A task was canceled.", (task != null) ? task.CancellationToken : default(CancellationToken))
		{
			this._canceledTask = task;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskCanceledException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600184E RID: 6222 RVA: 0x0005D4C3 File Offset: 0x0005B6C3
		protected TaskCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000B91 RID: 2961
		[NonSerialized]
		private readonly Task _canceledTask;
	}
}
