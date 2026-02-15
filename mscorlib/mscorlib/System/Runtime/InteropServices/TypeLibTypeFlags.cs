using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> in the COM type library from which the type was imported.</summary>
	// Token: 0x0200052D RID: 1325
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibTypeFlags
	{
		/// <summary>A type description that describes an Application object.</summary>
		// Token: 0x04001509 RID: 5385
		FAppObject = 1,
		/// <summary>Instances of the type can be created by ITypeInfo::CreateInstance.</summary>
		// Token: 0x0400150A RID: 5386
		FCanCreate = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x0400150B RID: 5387
		FLicensed = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x0400150C RID: 5388
		FPreDeclId = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x0400150D RID: 5389
		FHidden = 16,
		/// <summary>The type is a control from which other types will be derived, and should not be displayed to users.</summary>
		// Token: 0x0400150E RID: 5390
		FControl = 32,
		/// <summary>The interface supplies both IDispatch and V-table binding.</summary>
		// Token: 0x0400150F RID: 5391
		FDual = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x04001510 RID: 5392
		FNonExtensible = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including vtable binding support.</summary>
		// Token: 0x04001511 RID: 5393
		FOleAutomation = 256,
		/// <summary>This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04001512 RID: 5394
		FRestricted = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04001513 RID: 5395
		FAggregatable = 1024,
		/// <summary>The object supports IConnectionPointWithDefault, and has default behaviors.</summary>
		// Token: 0x04001514 RID: 5396
		FReplaceable = 2048,
		/// <summary>Indicates that the interface derives from IDispatch, either directly or indirectly.</summary>
		// Token: 0x04001515 RID: 5397
		FDispatchable = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking child interfaces. This is the reverse of the default behavior.</summary>
		// Token: 0x04001516 RID: 5398
		FReverseBind = 8192
	}
}
