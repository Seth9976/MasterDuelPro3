using System;

namespace System.Reflection
{
	/// <summary>Specifies that the assembly is not fully signed when created.</summary>
	// Token: 0x020005E6 RID: 1510
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDelaySignAttribute" /> class.</summary>
		/// <param name="delaySign">true if the feature this attribute represents is activated; otherwise, false. </param>
		// Token: 0x06002C8A RID: 11402 RVA: 0x000B178F File Offset: 0x000AF98F
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.<DelaySign>k__BackingField = delaySign;
		}
	}
}
