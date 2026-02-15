using System;

namespace System.Reflection
{
	/// <summary>Specifies the build configuration, such as retail or debug, for an assembly.</summary>
	// Token: 0x020005E2 RID: 1506
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyConfigurationAttribute" /> class.</summary>
		/// <param name="configuration">The assembly configuration. </param>
		// Token: 0x06002C87 RID: 11399 RVA: 0x000B1762 File Offset: 0x000AF962
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.<Configuration>k__BackingField = configuration;
		}
	}
}
