using System;

namespace System.Reflection
{
	/// <summary>Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.</summary>
	// Token: 0x020005F2 RID: 1522
	[Flags]
	public enum BindingFlags
	{
		/// <summary>Specifies no binding flag.</summary>
		// Token: 0x040016CC RID: 5836
		Default = 0,
		/// <summary>Specifies that the case of the member name should not be considered when binding.</summary>
		// Token: 0x040016CD RID: 5837
		IgnoreCase = 1,
		/// <summary>Specifies that only members declared at the level of the supplied type's hierarchy should be considered. Inherited members are not considered.</summary>
		// Token: 0x040016CE RID: 5838
		DeclaredOnly = 2,
		/// <summary>Specifies that instance members are to be included in the search.</summary>
		// Token: 0x040016CF RID: 5839
		Instance = 4,
		/// <summary>Specifies that static members are to be included in the search.</summary>
		// Token: 0x040016D0 RID: 5840
		Static = 8,
		/// <summary>Specifies that public members are to be included in the search.</summary>
		// Token: 0x040016D1 RID: 5841
		Public = 16,
		/// <summary>Specifies that non-public members are to be included in the search.</summary>
		// Token: 0x040016D2 RID: 5842
		NonPublic = 32,
		/// <summary>Specifies that public and protected static members up the hierarchy should be returned. Private static members in inherited classes are not returned. Static members include fields, methods, events, and properties. Nested types are not returned.</summary>
		// Token: 0x040016D3 RID: 5843
		FlattenHierarchy = 64,
		/// <summary>Specifies that a method is to be invoked. This must not be a constructor or a type initializer.</summary>
		// Token: 0x040016D4 RID: 5844
		InvokeMethod = 256,
		/// <summary>Specifies that Reflection should create an instance of the specified type. Calls the constructor that matches the given arguments. The supplied member name is ignored. If the type of lookup is not specified, (Instance | Public) will apply. It is not possible to call a type initializer.</summary>
		// Token: 0x040016D5 RID: 5845
		CreateInstance = 512,
		/// <summary>Specifies that the value of the specified field should be returned.</summary>
		// Token: 0x040016D6 RID: 5846
		GetField = 1024,
		/// <summary>Specifies that the value of the specified field should be set.</summary>
		// Token: 0x040016D7 RID: 5847
		SetField = 2048,
		/// <summary>Specifies that the value of the specified property should be returned.</summary>
		// Token: 0x040016D8 RID: 5848
		GetProperty = 4096,
		/// <summary>Specifies that the value of the specified property should be set. For COM properties, specifying this binding flag is equivalent to specifying PutDispProperty and PutRefDispProperty.</summary>
		// Token: 0x040016D9 RID: 5849
		SetProperty = 8192,
		/// <summary>Specifies that the PROPPUT member on a COM object should be invoked. PROPPUT specifies a property-setting function that uses a value. Use PutDispProperty if a property has both PROPPUT and PROPPUTREF and you need to distinguish which one is called.</summary>
		// Token: 0x040016DA RID: 5850
		PutDispProperty = 16384,
		/// <summary>Specifies that the PROPPUTREF member on a COM object should be invoked. PROPPUTREF specifies a property-setting function that uses a reference instead of a value. Use PutRefDispProperty if a property has both PROPPUT and PROPPUTREF and you need to distinguish which one is called.</summary>
		// Token: 0x040016DB RID: 5851
		PutRefDispProperty = 32768,
		/// <summary>Specifies that types of the supplied arguments must exactly match the types of the corresponding formal parameters. Reflection throws an exception if the caller supplies a non-null Binder object, since that implies that the caller is supplying BindToXXX implementations that will pick the appropriate method.</summary>
		// Token: 0x040016DC RID: 5852
		ExactBinding = 65536,
		/// <summary>Not implemented.</summary>
		// Token: 0x040016DD RID: 5853
		SuppressChangeType = 131072,
		/// <summary>Returns the set of members whose parameter count matches the number of supplied arguments. This binding flag is used for methods with parameters that have default values and methods with variable arguments (varargs). This flag should only be used with <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		// Token: 0x040016DE RID: 5854
		OptionalParamBinding = 262144,
		/// <summary>Used in COM interop to specify that the return value of the member can be ignored.</summary>
		// Token: 0x040016DF RID: 5855
		IgnoreReturn = 16777216,
		// Token: 0x040016E0 RID: 5856
		DoNotWrapExceptions = 33554432
	}
}
