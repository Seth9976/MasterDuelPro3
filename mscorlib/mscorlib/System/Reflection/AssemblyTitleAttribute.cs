using System;

namespace System.Reflection
{
	/// <summary>Specifies a description for an assembly.</summary>
	// Token: 0x020005EE RID: 1518
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTitleAttribute" /> class.</summary>
		/// <param name="title">The assembly title. </param>
		// Token: 0x06002C93 RID: 11411 RVA: 0x000B181D File Offset: 0x000AFA1D
		public AssemblyTitleAttribute(string title)
		{
			this.<Title>k__BackingField = title;
		}
	}
}
