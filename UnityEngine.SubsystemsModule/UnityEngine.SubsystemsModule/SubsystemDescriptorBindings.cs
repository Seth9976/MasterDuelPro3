using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	internal static class SubsystemDescriptorBindings
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020EC File Offset: 0x000002EC
		public static string GetId(IntPtr descriptorPtr)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				SubsystemDescriptorBindings.GetId_Injected(descriptorPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetId_Injected(IntPtr descriptorPtr, out ManagedSpanWrapper ret);
	}
}
