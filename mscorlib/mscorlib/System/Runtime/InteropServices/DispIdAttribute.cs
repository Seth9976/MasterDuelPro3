using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the COM dispatch identifier (DISPID) of a method, field, or property.</summary>
	// Token: 0x02000525 RID: 1317
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	[ComVisible(true)]
	public sealed class DispIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the DispIdAttribute class with the specified DISPID.</summary>
		/// <param name="dispId">The DISPID for the member. </param>
		// Token: 0x06002910 RID: 10512 RVA: 0x000A7EC6 File Offset: 0x000A60C6
		public DispIdAttribute(int dispId)
		{
			this._val = dispId;
		}

		// Token: 0x040014F9 RID: 5369
		internal int _val;
	}
}
