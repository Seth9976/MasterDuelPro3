using System;

namespace System.Reflection
{
	/// <summary>Provides a text description for an assembly.</summary>
	// Token: 0x020005E7 RID: 1511
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDescriptionAttribute" /> class.</summary>
		/// <param name="description">The assembly description. </param>
		// Token: 0x06002C8B RID: 11403 RVA: 0x000B179E File Offset: 0x000AF99E
		public AssemblyDescriptionAttribute(string description)
		{
			this.<Description>k__BackingField = description;
		}
	}
}
