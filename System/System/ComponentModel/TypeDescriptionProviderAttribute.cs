using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the custom type description provider for a class. This class cannot be inherited.</summary>
	// Token: 0x020002A4 RID: 676
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class TypeDescriptionProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProviderAttribute" /> class using the specified type name.</summary>
		/// <param name="typeName">The qualified name of the type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		// Token: 0x06001031 RID: 4145 RVA: 0x0004418B File Offset: 0x0004238B
		public TypeDescriptionProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.TypeName = typeName;
		}

		/// <summary>Gets the type name for the type description provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the qualified type name for the <see cref="T:System.ComponentModel.TypeDescriptionProvider" />.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x000441A8 File Offset: 0x000423A8
		public string TypeName { get; }
	}
}
