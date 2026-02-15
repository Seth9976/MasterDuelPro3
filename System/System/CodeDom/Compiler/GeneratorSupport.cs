using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers used to determine whether a code generator supports certain types of code elements.</summary>
	// Token: 0x02000233 RID: 563
	[Flags]
	public enum GeneratorSupport
	{
		/// <summary>Indicates the generator supports arrays of arrays.</summary>
		// Token: 0x04000953 RID: 2387
		ArraysOfArrays = 1,
		/// <summary>Indicates the generator supports a program entry point method designation. This is used when building executables.</summary>
		// Token: 0x04000954 RID: 2388
		EntryPointMethod = 2,
		/// <summary>Indicates the generator supports goto statements.</summary>
		// Token: 0x04000955 RID: 2389
		GotoStatements = 4,
		/// <summary>Indicates the generator supports referencing multidimensional arrays. Currently, the CodeDom cannot be used to instantiate multidimensional arrays.</summary>
		// Token: 0x04000956 RID: 2390
		MultidimensionalArrays = 8,
		/// <summary>Indicates the generator supports static constructors.</summary>
		// Token: 0x04000957 RID: 2391
		StaticConstructors = 16,
		/// <summary>Indicates the generator supports try...catch statements.</summary>
		// Token: 0x04000958 RID: 2392
		TryCatchStatements = 32,
		/// <summary>Indicates the generator supports return type attribute declarations.</summary>
		// Token: 0x04000959 RID: 2393
		ReturnTypeAttributes = 64,
		/// <summary>Indicates the generator supports value type declarations.</summary>
		// Token: 0x0400095A RID: 2394
		DeclareValueTypes = 128,
		/// <summary>Indicates the generator supports enumeration declarations.</summary>
		// Token: 0x0400095B RID: 2395
		DeclareEnums = 256,
		/// <summary>Indicates the generator supports delegate declarations.</summary>
		// Token: 0x0400095C RID: 2396
		DeclareDelegates = 512,
		/// <summary>Indicates the generator supports interface declarations.</summary>
		// Token: 0x0400095D RID: 2397
		DeclareInterfaces = 1024,
		/// <summary>Indicates the generator supports event declarations.</summary>
		// Token: 0x0400095E RID: 2398
		DeclareEvents = 2048,
		/// <summary>Indicates the generator supports assembly attributes.</summary>
		// Token: 0x0400095F RID: 2399
		AssemblyAttributes = 4096,
		/// <summary>Indicates the generator supports parameter attributes.</summary>
		// Token: 0x04000960 RID: 2400
		ParameterAttributes = 8192,
		/// <summary>Indicates the generator supports reference and out parameters.</summary>
		// Token: 0x04000961 RID: 2401
		ReferenceParameters = 16384,
		/// <summary>Indicates the generator supports chained constructor arguments.</summary>
		// Token: 0x04000962 RID: 2402
		ChainedConstructorArguments = 32768,
		/// <summary>Indicates the generator supports the declaration of nested types.</summary>
		// Token: 0x04000963 RID: 2403
		NestedTypes = 65536,
		/// <summary>Indicates the generator supports the declaration of members that implement multiple interfaces.</summary>
		// Token: 0x04000964 RID: 2404
		MultipleInterfaceMembers = 131072,
		/// <summary>Indicates the generator supports public static members.</summary>
		// Token: 0x04000965 RID: 2405
		PublicStaticMembers = 262144,
		/// <summary>Indicates the generator supports complex expressions.</summary>
		// Token: 0x04000966 RID: 2406
		ComplexExpressions = 524288,
		/// <summary>Indicates the generator supports compilation with Win32 resources.</summary>
		// Token: 0x04000967 RID: 2407
		Win32Resources = 1048576,
		/// <summary>Indicates the generator supports compilation with .NET Framework resources. These can be default resources compiled directly into an assembly, or resources referenced in a satellite assembly.</summary>
		// Token: 0x04000968 RID: 2408
		Resources = 2097152,
		/// <summary>Indicates the generator supports partial type declarations.</summary>
		// Token: 0x04000969 RID: 2409
		PartialTypes = 4194304,
		/// <summary>Indicates the generator supports generic type references.</summary>
		// Token: 0x0400096A RID: 2410
		GenericTypeReference = 8388608,
		/// <summary>Indicates the generator supports generic type declarations.</summary>
		// Token: 0x0400096B RID: 2411
		GenericTypeDeclaration = 16777216,
		/// <summary>Indicates the generator supports the declaration of indexer properties.</summary>
		// Token: 0x0400096C RID: 2412
		DeclareIndexerProperties = 33554432
	}
}
