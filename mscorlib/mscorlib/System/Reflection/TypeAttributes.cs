using System;

namespace System.Reflection
{
	/// <summary>Specifies type attributes.</summary>
	// Token: 0x02000626 RID: 1574
	[Flags]
	public enum TypeAttributes
	{
		/// <summary>Specifies type visibility information.</summary>
		// Token: 0x04001793 RID: 6035
		VisibilityMask = 7,
		/// <summary>Specifies that the class is not public.</summary>
		// Token: 0x04001794 RID: 6036
		NotPublic = 0,
		/// <summary>Specifies that the class is public.</summary>
		// Token: 0x04001795 RID: 6037
		Public = 1,
		/// <summary>Specifies that the class is nested with public visibility.</summary>
		// Token: 0x04001796 RID: 6038
		NestedPublic = 2,
		/// <summary>Specifies that the class is nested with private visibility.</summary>
		// Token: 0x04001797 RID: 6039
		NestedPrivate = 3,
		/// <summary>Specifies that the class is nested with family visibility, and is thus accessible only by methods within its own type and any derived types.</summary>
		// Token: 0x04001798 RID: 6040
		NestedFamily = 4,
		/// <summary>Specifies that the class is nested with assembly visibility, and is thus accessible only by methods within its assembly.</summary>
		// Token: 0x04001799 RID: 6041
		NestedAssembly = 5,
		/// <summary>Specifies that the class is nested with assembly and family visibility, and is thus accessible only by methods lying in the intersection of its family and assembly.</summary>
		// Token: 0x0400179A RID: 6042
		NestedFamANDAssem = 6,
		/// <summary>Specifies that the class is nested with family or assembly visibility, and is thus accessible only by methods lying in the union of its family and assembly.</summary>
		// Token: 0x0400179B RID: 6043
		NestedFamORAssem = 7,
		/// <summary>Specifies class layout information.</summary>
		// Token: 0x0400179C RID: 6044
		LayoutMask = 24,
		/// <summary>Specifies that class fields are automatically laid out by the common language runtime.</summary>
		// Token: 0x0400179D RID: 6045
		AutoLayout = 0,
		/// <summary>Specifies that class fields are laid out sequentially, in the order that the fields were emitted to the metadata.</summary>
		// Token: 0x0400179E RID: 6046
		SequentialLayout = 8,
		/// <summary>Specifies that class fields are laid out at the specified offsets.</summary>
		// Token: 0x0400179F RID: 6047
		ExplicitLayout = 16,
		/// <summary>Specifies class semantics information; the current class is contextful (else agile).</summary>
		// Token: 0x040017A0 RID: 6048
		ClassSemanticsMask = 32,
		/// <summary>Specifies that the type is a class.</summary>
		// Token: 0x040017A1 RID: 6049
		Class = 0,
		/// <summary>Specifies that the type is an interface.</summary>
		// Token: 0x040017A2 RID: 6050
		Interface = 32,
		/// <summary>Specifies that the type is abstract.</summary>
		// Token: 0x040017A3 RID: 6051
		Abstract = 128,
		/// <summary>Specifies that the class is concrete and cannot be extended.</summary>
		// Token: 0x040017A4 RID: 6052
		Sealed = 256,
		/// <summary>Specifies that the class is special in a way denoted by the name.</summary>
		// Token: 0x040017A5 RID: 6053
		SpecialName = 1024,
		/// <summary>Specifies that the class or interface is imported from another module.</summary>
		// Token: 0x040017A6 RID: 6054
		Import = 4096,
		/// <summary>Specifies that the class can be serialized.</summary>
		// Token: 0x040017A7 RID: 6055
		Serializable = 8192,
		/// <summary>Specifies a Windows Runtime type.</summary>
		// Token: 0x040017A8 RID: 6056
		WindowsRuntime = 16384,
		/// <summary>Used to retrieve string information for native interoperability.</summary>
		// Token: 0x040017A9 RID: 6057
		StringFormatMask = 196608,
		/// <summary>LPTSTR is interpreted as ANSI.</summary>
		// Token: 0x040017AA RID: 6058
		AnsiClass = 0,
		/// <summary>LPTSTR is interpreted as UNICODE.</summary>
		// Token: 0x040017AB RID: 6059
		UnicodeClass = 65536,
		/// <summary>LPTSTR is interpreted automatically.</summary>
		// Token: 0x040017AC RID: 6060
		AutoClass = 131072,
		/// <summary>LPSTR is interpreted by some implementation-specific means, which includes the possibility of throwing a <see cref="T:System.NotSupportedException" />. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x040017AD RID: 6061
		CustomFormatClass = 196608,
		/// <summary>Used to retrieve non-standard encoding information for native interop. The meaning of the values of these 2 bits is unspecified. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x040017AE RID: 6062
		CustomFormatMask = 12582912,
		/// <summary>Specifies that calling static methods of the type does not force the system to initialize the type.</summary>
		// Token: 0x040017AF RID: 6063
		BeforeFieldInit = 1048576,
		/// <summary>Runtime should check name encoding.</summary>
		// Token: 0x040017B0 RID: 6064
		RTSpecialName = 2048,
		/// <summary>Type has security associate with it.</summary>
		// Token: 0x040017B1 RID: 6065
		HasSecurity = 262144,
		/// <summary>Attributes reserved for runtime use.</summary>
		// Token: 0x040017B2 RID: 6066
		ReservedMask = 264192
	}
}
