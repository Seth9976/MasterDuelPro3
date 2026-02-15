using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents state machines that are generated for asynchronous methods. This type is intended for compiler use only.</summary>
	// Token: 0x0200058A RID: 1418
	public interface IAsyncStateMachine
	{
		/// <summary>Moves the state machine to its next state.</summary>
		// Token: 0x06002AFA RID: 11002
		void MoveNext();

		/// <summary>Configures the state machine with a heap-allocated replica.</summary>
		/// <param name="stateMachine">The heap-allocated replica.</param>
		// Token: 0x06002AFB RID: 11003
		void SetStateMachine(IAsyncStateMachine stateMachine);
	}
}
