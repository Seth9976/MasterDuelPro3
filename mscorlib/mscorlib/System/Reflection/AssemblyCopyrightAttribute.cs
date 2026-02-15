using System;

namespace System.Reflection
{
	/// <summary>Defines a copyright custom attribute for an assembly manifest.</summary>
	// Token: 0x020005E4 RID: 1508
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCopyrightAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCopyrightAttribute" /> class.</summary>
		/// <param name="copyright">The copyright information. </param>
		// Token: 0x06002C88 RID: 11400 RVA: 0x000B1771 File Offset: 0x000AF971
		public AssemblyCopyrightAttribute(string copyright)
		{
			this.<Copyright>k__BackingField = copyright;
		}
	}
}
