using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted use of isolated storage.</summary>
	// Token: 0x02000353 RID: 851
	public enum IsolatedStorageContainment
	{
		/// <summary>Use of isolated storage is not allowed.</summary>
		// Token: 0x04000DC5 RID: 3525
		None,
		/// <summary>Storage is isolated first by user and then by domain and assembly. Storage is also isolated by computer. Data can only be accessed within the context of the same application and only when run by the same user. This is helpful when a third-party assembly wants to keep a private data store.</summary>
		// Token: 0x04000DC6 RID: 3526
		DomainIsolationByUser = 16,
		/// <summary>Storage is isolated first by user and then by application. Storage is also isolated by computer. This provides a data store for the application that is accessible in any domain context. The per-application data compartment requires additional trust because it potentially provides a "tunnel" between applications that could compromise the data isolation of applications in particular Web sites.</summary>
		// Token: 0x04000DC7 RID: 3527
		ApplicationIsolationByUser = 21,
		/// <summary>Storage is isolated first by user and then by code assembly. Storage is also isolated by computer. This provides a data store for the assembly that is accessible in any domain context. The per-assembly data compartment requires additional trust because it potentially provides a "tunnel" between applications that could compromise the data isolation of applications in particular Web sites.</summary>
		// Token: 0x04000DC8 RID: 3528
		AssemblyIsolationByUser = 32,
		/// <summary>Storage is isolated first by computer and then by domain and assembly. Data can only be accessed within the context of the same application and only when run on the same computer. This is helpful when a third-party assembly wants to keep a private data store.</summary>
		// Token: 0x04000DC9 RID: 3529
		DomainIsolationByMachine = 48,
		/// <summary>Storage is isolated first by computer and then by code assembly. This provides a data store for the assembly that is accessible in any domain context. The per-assembly data compartment requires additional trust because it potentially provides a "tunnel" between applications that could compromise the data isolation of applications in particular Web sites.</summary>
		// Token: 0x04000DCA RID: 3530
		AssemblyIsolationByMachine = 64,
		/// <summary>Storage is isolated first by computer and then by application. This provides a data store for the application that is accessible in any domain context. The per-application data compartment requires additional trust because it potentially provides a "tunnel" between applications that could compromise the data isolation of applications in particular Web sites.</summary>
		// Token: 0x04000DCB RID: 3531
		ApplicationIsolationByMachine = 69,
		/// <summary>Storage is isolated first by user and then by domain and assembly. Storage will roam if Windows user data roaming is enabled. Data can only be accessed within the context of the same application and only when run by the same user. This is helpful when a third-party assembly wants to keep a private data store.</summary>
		// Token: 0x04000DCC RID: 3532
		DomainIsolationByRoamingUser = 80,
		/// <summary>Storage is isolated first by user and then by assembly evidence. Storage will roam if Windows user data roaming is enabled. This provides a data store for the assembly that is accessible in any domain context. The per-assembly data compartment requires additional trust because it potentially provides a "tunnel" between applications that could compromise the data isolation of applications in particular Web sites.</summary>
		// Token: 0x04000DCD RID: 3533
		AssemblyIsolationByRoamingUser = 96,
		/// <summary>Storage is isolated first by user and then by application evidence. Storage will roam if Windows user data roaming is enabled. This provides a data store for the application that is accessible in any domain context. The per-application data compartment requires additional trust because it potentially provides a "tunnel" between applications that could compromise the data isolation of applications in particular Web sites.</summary>
		// Token: 0x04000DCE RID: 3534
		ApplicationIsolationByRoamingUser = 101,
		/// <summary>Unlimited administration ability for the user store. Allows browsing and deletion of the entire user store, but not read access other than the user's own domain/assembly identity.</summary>
		// Token: 0x04000DCF RID: 3535
		AdministerIsolatedStorageByUser = 112,
		/// <summary>Use of isolated storage is allowed without restriction. Code has full access to any part of the user store, regardless of the identity of the domain or assembly. This use of isolated storage includes the ability to enumerate the contents of the isolated storage data store.</summary>
		// Token: 0x04000DD0 RID: 3536
		UnrestrictedIsolatedStorage = 240
	}
}
