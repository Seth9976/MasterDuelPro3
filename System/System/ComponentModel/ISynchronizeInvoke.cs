using System;

namespace System.ComponentModel
{
	/// <summary>Provides a way to synchronously or asynchronously execute a delegate.</summary>
	// Token: 0x02000249 RID: 585
	public interface ISynchronizeInvoke
	{
		/// <summary>Gets a value indicating whether the caller must call <see cref="M:System.ComponentModel.ISynchronizeInvoke.Invoke(System.Delegate,System.Object[])" /> when calling an object that implements this interface.</summary>
		/// <returns>true if the caller must call <see cref="M:System.ComponentModel.ISynchronizeInvoke.Invoke(System.Delegate,System.Object[])" />; otherwise, false.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000E0C RID: 3596
		bool InvokeRequired { get; }

		/// <summary>Asynchronously executes the delegate on the thread that created this object.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> interface that represents the asynchronous operation started by calling this method.</returns>
		/// <param name="method">A <see cref="T:System.Delegate" /> to a method that takes parameters of the same number and type that are contained in <paramref name="args" />. </param>
		/// <param name="args">An array of type <see cref="T:System.Object" /> to pass as arguments to the given method. This can be null if no arguments are needed. </param>
		// Token: 0x06000E0D RID: 3597
		IAsyncResult BeginInvoke(Delegate method, object[] args);

		/// <summary>Synchronously executes the delegate on the thread that created this object and marshals the call to the creating thread.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the return value from the delegate being invoked, or null if the delegate has no return value.</returns>
		/// <param name="method">A <see cref="T:System.Delegate" /> that contains a method to call, in the context of the thread for the control. </param>
		/// <param name="args">An array of type <see cref="T:System.Object" /> that represents the arguments to pass to the given method. This can be null if no arguments are needed. </param>
		// Token: 0x06000E0E RID: 3598
		object Invoke(Delegate method, object[] args);
	}
}
