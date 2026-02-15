using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines a constant value that a compiler can persist for a field or method parameter.</summary>
	// Token: 0x02000583 RID: 1411
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public abstract class CustomConstantAttribute : Attribute
	{
		/// <summary>Gets the constant value stored by this attribute.</summary>
		/// <returns>The constant value stored by this attribute.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002AEA RID: 10986
		public abstract object Value { get; }
	}
}
