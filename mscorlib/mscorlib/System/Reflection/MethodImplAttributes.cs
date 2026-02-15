using System;

namespace System.Reflection
{
	/// <summary>Specifies flags for the attributes of a method implementation.</summary>
	// Token: 0x0200060C RID: 1548
	public enum MethodImplAttributes
	{
		/// <summary>Specifies flags about code type.</summary>
		// Token: 0x04001742 RID: 5954
		CodeTypeMask = 3,
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x04001743 RID: 5955
		IL = 0,
		/// <summary>Specifies that the method implementation is native.</summary>
		// Token: 0x04001744 RID: 5956
		Native,
		/// <summary>Specifies that the method implementation is in Optimized Intermediate Language (OPTIL).</summary>
		// Token: 0x04001745 RID: 5957
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x04001746 RID: 5958
		Runtime,
		/// <summary>Specifies whether the method is implemented in managed or unmanaged code.</summary>
		// Token: 0x04001747 RID: 5959
		ManagedMask,
		/// <summary>Specifies that the method is implemented in unmanaged code.</summary>
		// Token: 0x04001748 RID: 5960
		Unmanaged = 4,
		/// <summary>Specifies that the method is implemented in managed code. </summary>
		// Token: 0x04001749 RID: 5961
		Managed = 0,
		/// <summary>Specifies that the method is not defined.</summary>
		// Token: 0x0400174A RID: 5962
		ForwardRef = 16,
		/// <summary>Specifies that the method signature is exported exactly as declared.</summary>
		// Token: 0x0400174B RID: 5963
		PreserveSig = 128,
		/// <summary>Specifies an internal call.</summary>
		// Token: 0x0400174C RID: 5964
		InternalCall = 4096,
		/// <summary>Specifies that the method is single-threaded through the body. Static methods (Shared in Visual Basic) lock on the type, whereas instance methods lock on the instance. You can also use the C# lock statement or the Visual Basic SyncLock statement for this purpose. </summary>
		// Token: 0x0400174D RID: 5965
		Synchronized = 32,
		/// <summary>Specifies that the method cannot be inlined.</summary>
		// Token: 0x0400174E RID: 5966
		NoInlining = 8,
		/// <summary>Specifies that the method should be inlined wherever possible.</summary>
		// Token: 0x0400174F RID: 5967
		AggressiveInlining = 256,
		/// <summary>Specifies that the method is not optimized by the just-in-time (JIT) compiler or by native code generation (see Ngen.exe) when debugging possible code generation problems.</summary>
		// Token: 0x04001750 RID: 5968
		NoOptimization = 64,
		/// <summary>Specifies a range check value.</summary>
		// Token: 0x04001751 RID: 5969
		MaxMethodImplVal = 65535,
		// Token: 0x04001752 RID: 5970
		SecurityMitigations = 1024
	}
}
