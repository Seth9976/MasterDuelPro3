using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x0200000A RID: 10
	[UsedByNativeCode]
	[NativeHeader("UnityWebRequestScriptingClasses.h")]
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequestAsyncOperation.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UnityWebRequestAsyncOperation : AsyncOperation
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002E7C File Offset: 0x0000107C
		private UnityWebRequestAsyncOperation(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002E87 File Offset: 0x00001087
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002E8F File Offset: 0x0000108F
		public UnityWebRequest webRequest { get; internal set; }

		// Token: 0x0200000B RID: 11
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600003F RID: 63 RVA: 0x00002E98 File Offset: 0x00001098
			public static UnityWebRequestAsyncOperation ConvertToManaged(IntPtr ptr)
			{
				return new UnityWebRequestAsyncOperation(ptr);
			}
		}
	}
}
