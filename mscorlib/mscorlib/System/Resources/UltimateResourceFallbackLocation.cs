using System;

namespace System.Resources
{
	/// <summary>Specifies whether a <see cref="T:System.Resources.ResourceManager" /> object looks for the resources of the app's default culture in the main assembly or in a satellite assembly. </summary>
	// Token: 0x020005CA RID: 1482
	public enum UltimateResourceFallbackLocation
	{
		/// <summary>Fallback resources are located in the main assembly.</summary>
		// Token: 0x04001648 RID: 5704
		MainAssembly,
		/// <summary>Fallback resources are located in a satellite assembly. </summary>
		// Token: 0x04001649 RID: 5705
		Satellite
	}
}
