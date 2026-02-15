using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the calling convention required to call methods implemented in unmanaged code.</summary>
	// Token: 0x0200053D RID: 1341
	[ComVisible(true)]
	[Serializable]
	public enum CallingConvention
	{
		/// <summary>This member is not actually a calling convention, but instead uses the default platform calling convention. For example, on Windows the default is <see cref="F:System.Runtime.InteropServices.CallingConvention.StdCall" /> and on Windows CE.NET it is <see cref="F:System.Runtime.InteropServices.CallingConvention.Cdecl" />.</summary>
		// Token: 0x0400157F RID: 5503
		Winapi = 1,
		/// <summary>The caller cleans the stack. This enables calling functions with varargs, which makes it appropriate to use for methods that accept a variable number of parameters, such as Printf.</summary>
		// Token: 0x04001580 RID: 5504
		Cdecl,
		/// <summary>The callee cleans the stack. This is the default convention for calling unmanaged functions with platform invoke.</summary>
		// Token: 0x04001581 RID: 5505
		StdCall,
		/// <summary>The first parameter is the this pointer and is stored in register ECX. Other parameters are pushed on the stack. This calling convention is used to call methods on classes exported from an unmanaged DLL.</summary>
		// Token: 0x04001582 RID: 5506
		ThisCall,
		/// <summary>This calling convention is not supported.</summary>
		// Token: 0x04001583 RID: 5507
		FastCall
	}
}
