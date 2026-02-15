using System;

namespace System.Data.Common
{
	/// <summary>Identifies which provider-specific property in the strongly typed parameter classes is to be used when setting a provider-specific type.</summary>
	// Token: 0x02000100 RID: 256
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[Serializable]
	public sealed class DbProviderSpecificTypePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DbProviderSpecificTypePropertyAttribute" /> class.</summary>
		/// <param name="isProviderSpecificTypeProperty">Specifies whether this property is a provider-specific property.</param>
		// Token: 0x06000D79 RID: 3449 RVA: 0x000453E2 File Offset: 0x000435E2
		public DbProviderSpecificTypePropertyAttribute(bool isProviderSpecificTypeProperty)
		{
			this.<IsProviderSpecificTypeProperty>k__BackingField = isProviderSpecificTypeProperty;
		}
	}
}
