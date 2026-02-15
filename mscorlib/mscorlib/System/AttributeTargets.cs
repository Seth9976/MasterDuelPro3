using System;

namespace System
{
	/// <summary>Specifies the application elements on which it is valid to apply an attribute.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C6 RID: 198
	[Flags]
	public enum AttributeTargets
	{
		/// <summary>Attribute can be applied to an assembly.</summary>
		// Token: 0x040002C3 RID: 707
		Assembly = 1,
		/// <summary>Attribute can be applied to a module.</summary>
		// Token: 0x040002C4 RID: 708
		Module = 2,
		/// <summary>Attribute can be applied to a class.</summary>
		// Token: 0x040002C5 RID: 709
		Class = 4,
		/// <summary>Attribute can be applied to a structure; that is, a value type.</summary>
		// Token: 0x040002C6 RID: 710
		Struct = 8,
		/// <summary>Attribute can be applied to an enumeration.</summary>
		// Token: 0x040002C7 RID: 711
		Enum = 16,
		/// <summary>Attribute can be applied to a constructor.</summary>
		// Token: 0x040002C8 RID: 712
		Constructor = 32,
		/// <summary>Attribute can be applied to a method.</summary>
		// Token: 0x040002C9 RID: 713
		Method = 64,
		/// <summary>Attribute can be applied to a property.</summary>
		// Token: 0x040002CA RID: 714
		Property = 128,
		/// <summary>Attribute can be applied to a field.</summary>
		// Token: 0x040002CB RID: 715
		Field = 256,
		/// <summary>Attribute can be applied to an event.</summary>
		// Token: 0x040002CC RID: 716
		Event = 512,
		/// <summary>Attribute can be applied to an interface.</summary>
		// Token: 0x040002CD RID: 717
		Interface = 1024,
		/// <summary>Attribute can be applied to a parameter.</summary>
		// Token: 0x040002CE RID: 718
		Parameter = 2048,
		/// <summary>Attribute can be applied to a delegate.</summary>
		// Token: 0x040002CF RID: 719
		Delegate = 4096,
		/// <summary>Attribute can be applied to a return value.</summary>
		// Token: 0x040002D0 RID: 720
		ReturnValue = 8192,
		/// <summary>Attribute can be applied to a generic parameter.</summary>
		// Token: 0x040002D1 RID: 721
		GenericParameter = 16384,
		/// <summary>Attribute can be applied to any application element.</summary>
		// Token: 0x040002D2 RID: 722
		All = 32767
	}
}
