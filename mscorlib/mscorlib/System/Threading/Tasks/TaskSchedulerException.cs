using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	/// <summary>Represents an exception used to communicate an invalid operation by a <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
	// Token: 0x02000298 RID: 664
	[Serializable]
	public class TaskSchedulerException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x0600185C RID: 6236 RVA: 0x0005D690 File Offset: 0x0005B890
		public TaskSchedulerException()
			: base("An exception was thrown by a TaskScheduler.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> class using the default error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x0600185D RID: 6237 RVA: 0x0005D69D File Offset: 0x0005B89D
		public TaskSchedulerException(Exception innerException)
			: base("An exception was thrown by a TaskScheduler.", innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.TaskSchedulerException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600185E RID: 6238 RVA: 0x0001876A File Offset: 0x0001696A
		protected TaskSchedulerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
