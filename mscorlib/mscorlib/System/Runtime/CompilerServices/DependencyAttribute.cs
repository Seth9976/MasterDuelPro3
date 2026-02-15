using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates when a dependency is to be loaded by the referring assembly. This class cannot be inherited. </summary>
	// Token: 0x020005B3 RID: 1459
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value. </summary>
		/// <param name="dependentAssemblyArgument">The dependent assembly to bind to.</param>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</param>
		// Token: 0x06002B77 RID: 11127 RVA: 0x000ABE61 File Offset: 0x000AA061
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		// Token: 0x04001605 RID: 5637
		private string dependentAssembly;

		// Token: 0x04001606 RID: 5638
		private LoadHint loadHint;
	}
}
