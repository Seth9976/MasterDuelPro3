using System;

namespace System.Reflection
{
	/// <summary>Attaches a modifier to parameters so that binding can work with parameter signatures in which the types have been modified.</summary>
	// Token: 0x02000613 RID: 1555
	[DefaultMember("Item")]
	public readonly struct ParameterModifier
	{
		// Token: 0x0400176A RID: 5994
		private readonly bool[] _byRef;
	}
}
