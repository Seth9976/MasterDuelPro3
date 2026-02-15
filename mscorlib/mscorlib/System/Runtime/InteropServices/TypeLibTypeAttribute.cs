using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> that were originally imported for this type from the COM type library.</summary>
	// Token: 0x0200052E RID: 1326
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the TypeLibTypeAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for the attributed type as found in the type library it was imported from. </param>
		// Token: 0x06002917 RID: 10519 RVA: 0x000A7F2D File Offset: 0x000A612D
		public TypeLibTypeAttribute(TypeLibTypeFlags flags)
		{
			this._val = flags;
		}

		// Token: 0x04001517 RID: 5399
		internal TypeLibTypeFlags _val;
	}
}
