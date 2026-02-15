using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000102 RID: 258
	[GenerateTestsForBurstCompatibility]
	public static class NativeListUnsafeUtility
	{
		// Token: 0x06000ADE RID: 2782 RVA: 0x00021621 File Offset: 0x0001F821
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static T* GetUnsafePtr<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return list.m_ListData->Ptr;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00021621 File Offset: 0x0001F821
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static T* GetUnsafeReadOnlyPtr<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return list.m_ListData->Ptr;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002162E File Offset: 0x0001F82E
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void* GetInternalListDataPtrUnchecked<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(ref NativeList<T> list) where T : struct, ValueType
		{
			return (void*)list.m_ListData;
		}
	}
}
