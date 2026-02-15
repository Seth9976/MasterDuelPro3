using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Marshals data of type VT_BSTR from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x0200051B RID: 1307
	public sealed class BStrWrapper
	{
		/// <summary>Gets the wrapped <see cref="T:System.String" /> object to marshal as type VT_BSTR.</summary>
		/// <returns>The object that is wrapped by <see cref="T:System.Runtime.InteropServices.BStrWrapper" />.</returns>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000A7E43 File Offset: 0x000A6043
		public string WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x040014EE RID: 5358
		private string m_WrappedObject;
	}
}
