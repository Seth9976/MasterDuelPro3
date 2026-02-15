using System;

namespace System.Reflection
{
	/// <summary>Defines a company name custom attribute for an assembly manifest.</summary>
	// Token: 0x020005E1 RID: 1505
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCompanyAttribute" /> class.</summary>
		/// <param name="company">The company name information. </param>
		// Token: 0x06002C86 RID: 11398 RVA: 0x000B1753 File Offset: 0x000AF953
		public AssemblyCompanyAttribute(string company)
		{
			this.<Company>k__BackingField = company;
		}
	}
}
