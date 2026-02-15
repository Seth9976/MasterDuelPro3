using System;

namespace System.Configuration
{
	/// <summary>Specifies the options to apply to a property.</summary>
	// Token: 0x0200001D RID: 29
	[Flags]
	public enum ConfigurationPropertyOptions
	{
		/// <summary>Indicates that no option applies to the property.</summary>
		// Token: 0x04000073 RID: 115
		None = 0,
		/// <summary>Indicates that the property is a default collection. </summary>
		// Token: 0x04000074 RID: 116
		IsDefaultCollection = 1,
		/// <summary>Indicates that the property is required. </summary>
		// Token: 0x04000075 RID: 117
		IsRequired = 2,
		/// <summary>Indicates that the property is a collection key.</summary>
		// Token: 0x04000076 RID: 118
		IsKey = 4,
		/// <summary>Indicates whether the type name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		// Token: 0x04000077 RID: 119
		IsTypeStringTransformationRequired = 8,
		/// <summary>Indicates whether the assembly name for the configuration property requires transformation when it is serialized for an earlier version of the .NET Framework.</summary>
		// Token: 0x04000078 RID: 120
		IsAssemblyStringTransformationRequired = 16,
		/// <summary>Indicates whether the configuration property's parent configuration section should be queried at serialization time to determine whether the configuration property should be serialized into XML.</summary>
		// Token: 0x04000079 RID: 121
		IsVersionCheckRequired = 32
	}
}
