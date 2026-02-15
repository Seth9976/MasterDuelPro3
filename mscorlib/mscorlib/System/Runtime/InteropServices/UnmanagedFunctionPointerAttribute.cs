using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls the marshaling behavior of a delegate signature passed as an unmanaged function pointer to or from unmanaged code. This class cannot be inherited. </summary>
	// Token: 0x02000524 RID: 1316
	[AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class UnmanagedFunctionPointerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute" /> class with the specified calling convention. </summary>
		/// <param name="callingConvention">The specified calling convention.</param>
		// Token: 0x0600290E RID: 10510 RVA: 0x000A7EAF File Offset: 0x000A60AF
		public UnmanagedFunctionPointerAttribute(CallingConvention callingConvention)
		{
			this.m_callingConvention = callingConvention;
		}

		/// <summary>Gets the value of the calling convention.</summary>
		/// <returns>The value of the calling convention specified by the <see cref="M:System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute.#ctor(System.Runtime.InteropServices.CallingConvention)" /> constructor.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600290F RID: 10511 RVA: 0x000A7EBE File Offset: 0x000A60BE
		public CallingConvention CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x040014F8 RID: 5368
		private CallingConvention m_callingConvention;
	}
}
