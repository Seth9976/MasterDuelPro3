using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates whether a managed interface is dual, dispatch-only, or IUnknown -only when exposed to COM.</summary>
	// Token: 0x02000527 RID: 1319
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InterfaceTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> enumeration member.</summary>
		/// <param name="interfaceType">One of the <see cref="T:System.Runtime.InteropServices.ComInterfaceType" /> values that describes how the interface should be exposed to COM clients. </param>
		// Token: 0x06002911 RID: 10513 RVA: 0x000A7ED5 File Offset: 0x000A60D5
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this._val = interfaceType;
		}

		// Token: 0x040014FF RID: 5375
		internal ComInterfaceType _val;
	}
}
