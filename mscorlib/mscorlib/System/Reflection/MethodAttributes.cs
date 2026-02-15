using System;

namespace System.Reflection
{
	/// <summary>Specifies flags for method attributes. These flags are defined in the corhdr.h file.</summary>
	// Token: 0x0200060A RID: 1546
	[Flags]
	public enum MethodAttributes
	{
		/// <summary>Retrieves accessibility information.</summary>
		// Token: 0x04001729 RID: 5929
		MemberAccessMask = 7,
		/// <summary>Indicates that the member cannot be referenced.</summary>
		// Token: 0x0400172A RID: 5930
		PrivateScope = 0,
		/// <summary>Indicates that the method is accessible only to the current class.</summary>
		// Token: 0x0400172B RID: 5931
		Private = 1,
		/// <summary>Indicates that the method is accessible to members of this type and its derived types that are in this assembly only.</summary>
		// Token: 0x0400172C RID: 5932
		FamANDAssem = 2,
		/// <summary>Indicates that the method is accessible to any class of this assembly.</summary>
		// Token: 0x0400172D RID: 5933
		Assembly = 3,
		/// <summary>Indicates that the method is accessible only to members of this class and its derived classes.</summary>
		// Token: 0x0400172E RID: 5934
		Family = 4,
		/// <summary>Indicates that the method is accessible to derived classes anywhere, as well as to any class in the assembly.</summary>
		// Token: 0x0400172F RID: 5935
		FamORAssem = 5,
		/// <summary>Indicates that the method is accessible to any object for which this object is in scope.</summary>
		// Token: 0x04001730 RID: 5936
		Public = 6,
		/// <summary>Indicates that the method is defined on the type; otherwise, it is defined per instance.</summary>
		// Token: 0x04001731 RID: 5937
		Static = 16,
		/// <summary>Indicates that the method cannot be overridden.</summary>
		// Token: 0x04001732 RID: 5938
		Final = 32,
		/// <summary>Indicates that the method is virtual.</summary>
		// Token: 0x04001733 RID: 5939
		Virtual = 64,
		/// <summary>Indicates that the method hides by name and signature; otherwise, by name only.</summary>
		// Token: 0x04001734 RID: 5940
		HideBySig = 128,
		/// <summary>Indicates that the method can only be overridden when it is also accessible.</summary>
		// Token: 0x04001735 RID: 5941
		CheckAccessOnOverride = 512,
		/// <summary>Retrieves vtable attributes.</summary>
		// Token: 0x04001736 RID: 5942
		VtableLayoutMask = 256,
		/// <summary>Indicates that the method will reuse an existing slot in the vtable. This is the default behavior.</summary>
		// Token: 0x04001737 RID: 5943
		ReuseSlot = 0,
		/// <summary>Indicates that the method always gets a new slot in the vtable.</summary>
		// Token: 0x04001738 RID: 5944
		NewSlot = 256,
		/// <summary>Indicates that the class does not provide an implementation of this method.</summary>
		// Token: 0x04001739 RID: 5945
		Abstract = 1024,
		/// <summary>Indicates that the method is special. The name describes how this method is special.</summary>
		// Token: 0x0400173A RID: 5946
		SpecialName = 2048,
		/// <summary>Indicates that the method implementation is forwarded through PInvoke (Platform Invocation Services).</summary>
		// Token: 0x0400173B RID: 5947
		PinvokeImpl = 8192,
		/// <summary>Indicates that the managed method is exported by thunk to unmanaged code.</summary>
		// Token: 0x0400173C RID: 5948
		UnmanagedExport = 8,
		/// <summary>Indicates that the common language runtime checks the name encoding.</summary>
		// Token: 0x0400173D RID: 5949
		RTSpecialName = 4096,
		/// <summary>Indicates that the method has security associated with it. Reserved flag for runtime use only.</summary>
		// Token: 0x0400173E RID: 5950
		HasSecurity = 16384,
		/// <summary>Indicates that the method calls another method containing security code. Reserved flag for runtime use only.</summary>
		// Token: 0x0400173F RID: 5951
		RequireSecObject = 32768,
		/// <summary>Indicates a reserved flag for runtime use only.</summary>
		// Token: 0x04001740 RID: 5952
		ReservedMask = 53248
	}
}
