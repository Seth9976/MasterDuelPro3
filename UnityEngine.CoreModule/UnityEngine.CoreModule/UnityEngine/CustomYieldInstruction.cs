using System;
using System.Collections;

namespace UnityEngine
{
	// Token: 0x0200019A RID: 410
	public abstract class CustomYieldInstruction : IEnumerator
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600101D RID: 4125
		public abstract bool keepWaiting { get; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00022070 File Offset: 0x00020270
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00022084 File Offset: 0x00020284
		public bool MoveNext()
		{
			return this.keepWaiting;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00003D56 File Offset: 0x00001F56
		public virtual void Reset()
		{
		}
	}
}
