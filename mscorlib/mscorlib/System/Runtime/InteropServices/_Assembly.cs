using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Reflection.Assembly" /> class to unmanaged code.</summary>
	// Token: 0x02000550 RID: 1360
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("17156360-2F1A-384A-BC52-FDE93C215C5B")]
	[ComVisible(true)]
	[TypeLibImportClass(typeof(Assembly))]
	public interface _Assembly
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x06002A76 RID: 10870
		Type GetType();
	}
}
