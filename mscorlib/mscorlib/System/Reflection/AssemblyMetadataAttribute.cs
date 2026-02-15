using System;

namespace System.Reflection
{
	/// <summary>Defines a key/value metadata pair for the decorated assembly.</summary>
	// Token: 0x020005EB RID: 1515
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyMetadataAttribute" /> class by using the specified metadata key and value.</summary>
		/// <param name="key">The metadata key.</param>
		/// <param name="value">The metadata value.</param>
		// Token: 0x06002C90 RID: 11408 RVA: 0x000B17F0 File Offset: 0x000AF9F0
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.<Key>k__BackingField = key;
			this.<Value>k__BackingField = value;
		}
	}
}
