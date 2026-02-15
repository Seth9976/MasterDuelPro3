using System;

namespace System.ComponentModel
{
	/// <summary>Enables attribute redirection. This class cannot be inherited.</summary>
	// Token: 0x02000257 RID: 599
	[AttributeUsage(AttributeTargets.Property)]
	public class AttributeProviderAttribute : Attribute
	{
		/// <summary>Gets the assembly qualified type name passed into the constructor.</summary>
		/// <returns>The assembly qualified name of the type specified in the constructor.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0003F084 File Offset: 0x0003D284
		public string TypeName { get; }

		/// <summary>Gets the name of the property for which attributes will be retrieved.</summary>
		/// <returns>The name of the property for which attributes will be retrieved.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0003F08C File Offset: 0x0003D28C
		public string PropertyName { get; }
	}
}
