using System;

namespace System
{
	/// <summary>Indicates that a class can be serialized. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200013F RID: 319
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = false)]
	public sealed class SerializableAttribute : Attribute
	{
	}
}
