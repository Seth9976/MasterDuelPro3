using System;

namespace System.Configuration
{
	/// <summary>Specifies the level in the configuration hierarchy where a configuration property value originated.</summary>
	// Token: 0x02000037 RID: 55
	public enum PropertyValueOrigin
	{
		/// <summary>The configuration property value originates from the <see cref="P:System.Configuration.ConfigurationProperty.DefaultValue" /> property.</summary>
		// Token: 0x040000BA RID: 186
		Default,
		/// <summary>The configuration property value is inherited from a parent level in the configuration.</summary>
		// Token: 0x040000BB RID: 187
		Inherited,
		/// <summary>The configuration property value is defined at the current level of the hierarchy.</summary>
		// Token: 0x040000BC RID: 188
		SetHere
	}
}
