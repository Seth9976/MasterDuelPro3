using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a VT_UNKNOWN.</summary>
	// Token: 0x02000523 RID: 1315
	public sealed class UnknownWrapper
	{
		/// <summary>Gets the object contained by this wrapper.</summary>
		/// <returns>The wrapped object.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x000A7EA7 File Offset: 0x000A60A7
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x040014F7 RID: 5367
		private object m_WrappedObject;
	}
}
