using System;

namespace System
{
	/// <summary>Indicates that a method will allow a variable number of arguments in its invocation. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000132 RID: 306
	[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class ParamArrayAttribute : Attribute
	{
	}
}
