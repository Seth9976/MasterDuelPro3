using System;

namespace System.Reflection
{
	/// <summary>Defines a trademark custom attribute for an assembly manifest.</summary>
	// Token: 0x020005EF RID: 1519
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTrademarkAttribute" /> class.</summary>
		/// <param name="trademark">The trademark information. </param>
		// Token: 0x06002C94 RID: 11412 RVA: 0x000B182C File Offset: 0x000AFA2C
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.<Trademark>k__BackingField = trademark;
		}
	}
}
