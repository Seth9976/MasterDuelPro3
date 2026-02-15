using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Supplies an explicit <see cref="T:System.Guid" /> when an automatic GUID is undesirable.</summary>
	// Token: 0x02000532 RID: 1330
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[ComVisible(true)]
	public sealed class GuidAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.GuidAttribute" /> class with the specified GUID.</summary>
		/// <param name="guid">The <see cref="T:System.Guid" /> to be assigned. </param>
		// Token: 0x06002919 RID: 10521 RVA: 0x000A7F3C File Offset: 0x000A613C
		public GuidAttribute(string guid)
		{
			this._val = guid;
		}

		// Token: 0x0400156C RID: 5484
		internal string _val;
	}
}
