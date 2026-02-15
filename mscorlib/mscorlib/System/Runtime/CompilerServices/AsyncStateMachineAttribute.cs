using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates whether a method is marked with either the Async (Visual Basic) or async (C# Reference) modifier.</summary>
	// Token: 0x02000578 RID: 1400
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class AsyncStateMachineAttribute : StateMachineAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.AsyncStateMachineAttribute" /> class.</summary>
		/// <param name="stateMachineType">The type object for the underlying state machine type that's used to implement a state machine method.</param>
		// Token: 0x06002AC9 RID: 10953 RVA: 0x000AA6CB File Offset: 0x000A88CB
		public AsyncStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}
