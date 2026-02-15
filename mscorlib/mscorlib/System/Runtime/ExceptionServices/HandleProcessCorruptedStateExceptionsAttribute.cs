using System;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Enables managed code to handle exceptions that indicate a corrupted process state.</summary>
	// Token: 0x02000570 RID: 1392
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class HandleProcessCorruptedStateExceptionsAttribute : Attribute
	{
	}
}
