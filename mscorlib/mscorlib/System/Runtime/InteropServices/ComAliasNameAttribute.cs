using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the COM alias for a parameter or field type.</summary>
	// Token: 0x02000539 RID: 1337
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComAliasNameAttribute" /> class with the alias for the attributed field or parameter.</summary>
		/// <param name="alias">The alias for the field or parameter as found in the type library when it was imported. </param>
		// Token: 0x06002924 RID: 10532 RVA: 0x000A80F6 File Offset: 0x000A62F6
		public ComAliasNameAttribute(string alias)
		{
			this._val = alias;
		}

		// Token: 0x04001577 RID: 5495
		internal string _val;
	}
}
