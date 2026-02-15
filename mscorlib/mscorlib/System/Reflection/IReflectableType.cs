using System;

namespace System.Reflection
{
	/// <summary>Represents a type that you can reflect over.</summary>
	// Token: 0x02000601 RID: 1537
	public interface IReflectableType
	{
		/// <summary>Retrieves an object that represents this type.</summary>
		/// <returns>An object that represents this type.</returns>
		// Token: 0x06002CE2 RID: 11490
		TypeInfo GetTypeInfo();
	}
}
