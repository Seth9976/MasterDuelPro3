using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps a managed object holding a handle to a resource that is passed to unmanaged code using platform invoke.</summary>
	// Token: 0x02000511 RID: 1297
	public readonly struct HandleRef
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.HandleRef" /> class with the object to wrap and a handle to the resource used by unmanaged code.</summary>
		/// <param name="wrapper">A managed object that should not be finalized until the platform invoke call returns. </param>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that indicates a handle to a resource. </param>
		// Token: 0x060028DB RID: 10459 RVA: 0x000A79EF File Offset: 0x000A5BEF
		public HandleRef(object wrapper, IntPtr handle)
		{
			this._wrapper = wrapper;
			this._handle = handle;
		}

		/// <summary>Gets the handle to a resource.</summary>
		/// <returns>The handle to a resource.</returns>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x000A79FF File Offset: 0x000A5BFF
		public IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x040014D7 RID: 5335
		private readonly object _wrapper;

		// Token: 0x040014D8 RID: 5336
		private readonly IntPtr _handle;
	}
}
