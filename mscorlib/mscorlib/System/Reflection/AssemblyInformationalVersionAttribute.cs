using System;

namespace System.Reflection
{
	/// <summary>Defines additional version information for an assembly manifest.</summary>
	// Token: 0x020005E9 RID: 1513
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyInformationalVersionAttribute" /> class.</summary>
		/// <param name="informationalVersion">The assembly version information. </param>
		// Token: 0x06002C8D RID: 11405 RVA: 0x000B17CA File Offset: 0x000AF9CA
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.InformationalVersion = informationalVersion;
		}

		/// <summary>Gets version information.</summary>
		/// <returns>A string containing the version information.</returns>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000B17D9 File Offset: 0x000AF9D9
		public string InformationalVersion { get; }
	}
}
