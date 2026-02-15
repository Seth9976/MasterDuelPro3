using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls accessibility of an individual managed type or member, or of all types within an assembly, to COM.</summary>
	// Token: 0x0200052B RID: 1323
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComVisibleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the ComVisibleAttribute class.</summary>
		/// <param name="visibility">true to indicate that the type is visible to COM; otherwise, false. The default is true. </param>
		// Token: 0x06002914 RID: 10516 RVA: 0x000A7F02 File Offset: 0x000A6102
		public ComVisibleAttribute(bool visibility)
		{
			this._val = visibility;
		}

		/// <summary>Gets a value that indicates whether the COM type is visible.</summary>
		/// <returns>true if the type is visible; otherwise, false. The default value is true.</returns>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x000A7F11 File Offset: 0x000A6111
		public bool Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04001506 RID: 5382
		internal bool _val;
	}
}
