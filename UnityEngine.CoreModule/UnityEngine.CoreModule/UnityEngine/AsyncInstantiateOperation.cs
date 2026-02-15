using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000170 RID: 368
	[NativeHeader("Runtime/GameCode/AsyncInstantiate/AsyncInstantiateOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncInstantiateOperation : AsyncOperation
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x000207D4 File Offset: 0x0001E9D4
		public Object[] Result
		{
			get
			{
				return this.m_Result;
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000207EC File Offset: 0x0001E9EC
		protected AsyncInstantiateOperation(IntPtr ptr, CancellationToken cancellationToken)
			: base(ptr)
		{
			this.m_CancellationToken = cancellationToken;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00020800 File Offset: 0x0001EA00
		[RequiredByNativeCode(GenerateProxy = true)]
		private bool IsCancellationRequested()
		{
			return this.m_CancellationToken.IsCancellationRequested;
		}

		// Token: 0x04000612 RID: 1554
		internal Object[] m_Result;

		// Token: 0x04000613 RID: 1555
		private CancellationToken m_CancellationToken;
	}
}
