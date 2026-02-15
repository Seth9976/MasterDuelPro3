using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001E4 RID: 484
	[VisibleToOtherModules]
	[NativeHeader("Runtime/NameFormatter/NameFormatter.h")]
	internal sealed class NameFormatter
	{
		// Token: 0x06001290 RID: 4752 RVA: 0x000274A0 File Offset: 0x000256A0
		[FreeFunction]
		public unsafe static string FormatVariableName(string name)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				NameFormatter.FormatVariableName_Injected(ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x06001291 RID: 4753
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FormatVariableName_Injected(ref ManagedSpanWrapper name, out ManagedSpanWrapper ret);
	}
}
