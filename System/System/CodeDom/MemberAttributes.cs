using System;

namespace System.CodeDom
{
	/// <summary>Defines member attribute identifiers for class members.</summary>
	// Token: 0x02000227 RID: 551
	public enum MemberAttributes
	{
		/// <summary>An abstract member.</summary>
		// Token: 0x04000916 RID: 2326
		Abstract = 1,
		/// <summary>A member that cannot be overridden in a derived class.</summary>
		// Token: 0x04000917 RID: 2327
		Final,
		/// <summary>A static member. In Visual Basic, this is equivalent to the Shared keyword.</summary>
		// Token: 0x04000918 RID: 2328
		Static,
		/// <summary>A member that overrides a base class member.</summary>
		// Token: 0x04000919 RID: 2329
		Override,
		/// <summary>A constant member.</summary>
		// Token: 0x0400091A RID: 2330
		Const,
		/// <summary>A new member.</summary>
		// Token: 0x0400091B RID: 2331
		New = 16,
		/// <summary>An overloaded member. Some languages, such as Visual Basic, require overloaded members to be explicitly indicated.</summary>
		// Token: 0x0400091C RID: 2332
		Overloaded = 256,
		/// <summary>A member that is accessible to any class within the same assembly.</summary>
		// Token: 0x0400091D RID: 2333
		Assembly = 4096,
		/// <summary>A member that is accessible within its class, and derived classes in the same assembly.</summary>
		// Token: 0x0400091E RID: 2334
		FamilyAndAssembly = 8192,
		/// <summary>A member that is accessible within the family of its class and derived classes.</summary>
		// Token: 0x0400091F RID: 2335
		Family = 12288,
		/// <summary>A member that is accessible within its class, its derived classes in any assembly, and any class in the same assembly.</summary>
		// Token: 0x04000920 RID: 2336
		FamilyOrAssembly = 16384,
		/// <summary>A private member.</summary>
		// Token: 0x04000921 RID: 2337
		Private = 20480,
		/// <summary>A public member.</summary>
		// Token: 0x04000922 RID: 2338
		Public = 24576,
		/// <summary>An access mask.</summary>
		// Token: 0x04000923 RID: 2339
		AccessMask = 61440,
		/// <summary>A scope mask.</summary>
		// Token: 0x04000924 RID: 2340
		ScopeMask = 15,
		/// <summary>A VTable mask.</summary>
		// Token: 0x04000925 RID: 2341
		VTableMask = 240
	}
}
