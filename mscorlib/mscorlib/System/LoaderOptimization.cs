using System;

namespace System
{
	/// <summary>An enumeration used with the <see cref="T:System.LoaderOptimizationAttribute" /> class to specify loader optimizations for an executable.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017B RID: 379
	public enum LoaderOptimization
	{
		/// <summary>Ignored by the common language runtime.</summary>
		// Token: 0x0400059B RID: 1435
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		DisallowBindings = 4,
		/// <summary>Do not use. This mask selects the domain-related values, screening out the unused <see cref="F:System.LoaderOptimization.DisallowBindings" /> flag.</summary>
		// Token: 0x0400059C RID: 1436
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		DomainMask = 3,
		/// <summary>Indicates that the application will probably have many domains that use the same code, and the loader must share maximal internal resources across application domains. </summary>
		// Token: 0x0400059D RID: 1437
		MultiDomain = 2,
		/// <summary>Indicates that the application will probably host unique code in multiple domains, and the loader must share resources across application domains only for globally available (strong-named) assemblies that have been added to the global assembly cache. </summary>
		// Token: 0x0400059E RID: 1438
		MultiDomainHost,
		/// <summary>Indicates that no optimizations for sharing internal resources are specified. If the default domain or hosting interface specified an optimization, then the loader uses that; otherwise, the loader uses <see cref="F:System.LoaderOptimization.SingleDomain" />.</summary>
		// Token: 0x0400059F RID: 1439
		NotSpecified = 0,
		/// <summary>Indicates that the application will probably have a single domain, and loader must not share internal resources across application domains. </summary>
		// Token: 0x040005A0 RID: 1440
		SingleDomain
	}
}
