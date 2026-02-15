using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides a hint to the common language runtime (CLR) indicating how likely a dependency is to be loaded. This class is used in a dependent assembly to indicate what hint should be used when the parent does not specify the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> attribute.  This class cannot be inherited. </summary>
	// Token: 0x020005B2 RID: 1458
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DefaultDependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> binding. </summary>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values that indicates the default binding preference.</param>
		// Token: 0x06002B76 RID: 11126 RVA: 0x000ABE52 File Offset: 0x000AA052
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.loadHint = loadHintArgument;
		}

		// Token: 0x04001604 RID: 5636
		private LoadHint loadHint;
	}
}
