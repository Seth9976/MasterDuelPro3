using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the defined native object types.</summary>
	// Token: 0x02000408 RID: 1032
	public enum ResourceType
	{
		/// <summary>An unknown object type.</summary>
		// Token: 0x040010C8 RID: 4296
		Unknown,
		/// <summary>A file or directory.</summary>
		// Token: 0x040010C9 RID: 4297
		FileObject,
		/// <summary>A Windows service.</summary>
		// Token: 0x040010CA RID: 4298
		Service,
		/// <summary>A printer.</summary>
		// Token: 0x040010CB RID: 4299
		Printer,
		/// <summary>A registry key.</summary>
		// Token: 0x040010CC RID: 4300
		RegistryKey,
		/// <summary>A network share.</summary>
		// Token: 0x040010CD RID: 4301
		LMShare,
		/// <summary>A local kernel object.</summary>
		// Token: 0x040010CE RID: 4302
		KernelObject,
		/// <summary>A window station or desktop object on the local computer.</summary>
		// Token: 0x040010CF RID: 4303
		WindowObject,
		/// <summary>A directory service (DS) object or a property set or property of a directory service object.</summary>
		// Token: 0x040010D0 RID: 4304
		DSObject,
		/// <summary>A directory service object and all of its property sets and properties.</summary>
		// Token: 0x040010D1 RID: 4305
		DSObjectAll,
		/// <summary>An object defined by a provider.</summary>
		// Token: 0x040010D2 RID: 4306
		ProviderDefined,
		/// <summary>A Windows Management Instrumentation (WMI) object.</summary>
		// Token: 0x040010D3 RID: 4307
		WmiGuidObject,
		/// <summary>An object for a registry entry under WOW64.</summary>
		// Token: 0x040010D4 RID: 4308
		RegistryWow6432Key
	}
}
