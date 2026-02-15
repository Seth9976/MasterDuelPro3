using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_DISPATCH.</summary>
	// Token: 0x0200053F RID: 1343
	[ComVisible(true)]
	[Serializable]
	public sealed class DispatchWrapper
	{
		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</returns>
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x000A81EA File Offset: 0x000A63EA
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04001586 RID: 5510
		private object m_WrappedObject;
	}
}
