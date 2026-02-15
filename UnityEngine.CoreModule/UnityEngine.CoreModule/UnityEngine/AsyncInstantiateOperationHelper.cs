using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000172 RID: 370
	[RequiredByNativeCode]
	internal class AsyncInstantiateOperationHelper
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x0002084D File Offset: 0x0001EA4D
		[RequiredByNativeCode]
		public static void SetAsyncInstantiateOperationResult(AsyncInstantiateOperation op, Object[] result)
		{
			op.m_Result = result;
		}
	}
}
