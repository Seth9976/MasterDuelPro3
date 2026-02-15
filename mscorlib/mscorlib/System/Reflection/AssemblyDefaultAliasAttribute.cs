using System;

namespace System.Reflection
{
	/// <summary>Defines a friendly default alias for an assembly manifest.</summary>
	// Token: 0x020005E5 RID: 1509
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDefaultAliasAttribute" /> class.</summary>
		/// <param name="defaultAlias">The assembly default alias information. </param>
		// Token: 0x06002C89 RID: 11401 RVA: 0x000B1780 File Offset: 0x000AF980
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.<DefaultAlias>k__BackingField = defaultAlias;
		}
	}
}
