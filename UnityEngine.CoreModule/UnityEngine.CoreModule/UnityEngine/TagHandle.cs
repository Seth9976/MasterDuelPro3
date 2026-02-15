using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001B9 RID: 441
	[StaticAccessor("GetTagManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	public struct TagHandle
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x00024A11 File Offset: 0x00022C11
		public override string ToString()
		{
			return TagHandle.TagToString(this._tagIndex);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00024A20 File Offset: 0x00022C20
		private static string TagToString(uint tagIndex)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				TagHandle.TagToString_Injected(tagIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001121 RID: 4385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TagToString_Injected(uint tagIndex, out ManagedSpanWrapper ret);

		// Token: 0x04000684 RID: 1668
		private uint _tagIndex;
	}
}
