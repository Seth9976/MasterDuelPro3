using System;

namespace System.Reflection
{
	/// <summary>Defines a product name custom attribute for an assembly manifest.</summary>
	// Token: 0x020005ED RID: 1517
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyProductAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyProductAttribute" /> class.</summary>
		/// <param name="product">The product name information. </param>
		// Token: 0x06002C91 RID: 11409 RVA: 0x000B1806 File Offset: 0x000AFA06
		public AssemblyProductAttribute(string product)
		{
			this.Product = product;
		}

		/// <summary>Gets product name information.</summary>
		/// <returns>A string containing the product name.</returns>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000B1815 File Offset: 0x000AFA15
		public string Product { get; }
	}
}
