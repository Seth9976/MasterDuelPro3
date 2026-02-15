using System;

namespace System.Reflection
{
	/// <summary>Specifies the version of the assembly being attributed.</summary>
	// Token: 0x020005F0 RID: 1520
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AssemblyVersionAttribute class with the version number of the assembly being attributed.</summary>
		/// <param name="version">The version number of the attributed assembly. </param>
		// Token: 0x06002C95 RID: 11413 RVA: 0x000B183B File Offset: 0x000AFA3B
		public AssemblyVersionAttribute(string version)
		{
			this.Version = version;
		}

		/// <summary>Gets the version number of the attributed assembly.</summary>
		/// <returns>A string containing the assembly version number.</returns>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000B184A File Offset: 0x000AFA4A
		public string Version { get; }
	}
}
