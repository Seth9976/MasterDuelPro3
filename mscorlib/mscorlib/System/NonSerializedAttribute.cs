using System;

namespace System
{
	/// <summary>Indicates that a field of a serializable class should not be serialized. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000124 RID: 292
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class NonSerializedAttribute : Attribute
	{
	}
}
