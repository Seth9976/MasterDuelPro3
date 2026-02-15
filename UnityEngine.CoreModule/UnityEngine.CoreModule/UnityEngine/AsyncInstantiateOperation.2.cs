using System;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	// Token: 0x02000171 RID: 369
	public class AsyncInstantiateOperation<T> : AsyncInstantiateOperation
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x0002081D File Offset: 0x0001EA1D
		internal AsyncInstantiateOperation(IntPtr ptr, CancellationToken cancellationToken)
			: base(ptr, cancellationToken)
		{
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0002082C File Offset: 0x0001EA2C
		public new unsafe T[] Result
		{
			get
			{
				Object[] objArr = base.Result;
				return *UnsafeUtility.As<Object[], T[]>(ref objArr);
			}
		}
	}
}
