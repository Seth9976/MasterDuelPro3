using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents an awaiter that schedules continuations when an await operation completes.</summary>
	// Token: 0x0200058C RID: 1420
	public interface ICriticalNotifyCompletion : INotifyCompletion
	{
		/// <summary>Schedules the continuation action that's invoked when the instance completes.</summary>
		/// <param name="continuation">The action to invoke when the operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null (Nothing in Visual Basic).</exception>
		// Token: 0x06002AFD RID: 11005
		void UnsafeOnCompleted(Action continuation);
	}
}
