using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000103 RID: 259
	[GenerateTestsForBurstCompatibility]
	public static class NativeReferenceUnsafeUtility
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00021636 File Offset: 0x0001F836
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static T* GetUnsafePtr<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return (T*)reference.m_Data;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00021636 File Offset: 0x0001F836
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static T* GetUnsafeReadOnlyPtr<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return (T*)reference.m_Data;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00021636 File Offset: 0x0001F836
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static T* GetUnsafePtrWithoutChecks<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return (T*)reference.m_Data;
		}
	}
}
