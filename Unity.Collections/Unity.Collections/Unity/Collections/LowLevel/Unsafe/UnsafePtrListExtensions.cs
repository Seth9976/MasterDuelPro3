using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000122 RID: 290
	[GenerateTestsForBurstCompatibility]
	internal static class UnsafePtrListExtensions
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x00024A1C File Offset: 0x00022C1C
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref UnsafeList<IntPtr> ListData<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafePtrList<T> from) where T : struct, ValueType
		{
			return UnsafeUtility.As<UnsafePtrList<T>, UnsafeList<IntPtr>>(ref from);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00024A24 File Offset: 0x00022C24
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static UnsafeList<IntPtr> ListDataRO<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafePtrList<T> from) where T : struct, ValueType
		{
			return *UnsafeUtility.As<UnsafePtrList<T>, UnsafeList<IntPtr>>(ref from);
		}
	}
}
