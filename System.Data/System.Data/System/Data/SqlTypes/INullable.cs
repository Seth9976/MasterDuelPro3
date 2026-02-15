using System;

namespace System.Data.SqlTypes
{
	/// <summary>All the <see cref="N:System.Data.SqlTypes" /> objects and structures implement the INullable interface. </summary>
	// Token: 0x020000BD RID: 189
	public interface INullable
	{
		/// <summary>Indicates whether a structure is null. This property is read-only.</summary>
		/// <returns>
		///   <see cref="T:System.Data.SqlTypes.SqlBoolean" />true if the value of this object is null. Otherwise, false.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600093F RID: 2367
		bool IsNull { get; }
	}
}
