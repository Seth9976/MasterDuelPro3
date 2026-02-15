using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates whether a method in Visual Basic is marked with the Iterator modifier.</summary>
	// Token: 0x02000592 RID: 1426
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class IteratorStateMachineAttribute : StateMachineAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.IteratorStateMachineAttribute" /> class.</summary>
		/// <param name="stateMachineType">The type object for the underlying state machine type that's used to implement a state machine method.</param>
		// Token: 0x06002B02 RID: 11010 RVA: 0x000AA6CB File Offset: 0x000A88CB
		public IteratorStateMachineAttribute(Type stateMachineType)
			: base(stateMachineType)
		{
		}
	}
}
