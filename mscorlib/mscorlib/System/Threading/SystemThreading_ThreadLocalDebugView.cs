using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x02000252 RID: 594
	internal sealed class SystemThreading_ThreadLocalDebugView<T>
	{
		// Token: 0x060015B1 RID: 5553 RVA: 0x000577C8 File Offset: 0x000559C8
		public SystemThreading_ThreadLocalDebugView(ThreadLocal<T> tlocal)
		{
			this.m_tlocal = tlocal;
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x000577D7 File Offset: 0x000559D7
		public bool IsValueCreated
		{
			get
			{
				return this.m_tlocal.IsValueCreated;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000577E4 File Offset: 0x000559E4
		public T Value
		{
			get
			{
				return this.m_tlocal.ValueForDebugDisplay;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x000577F1 File Offset: 0x000559F1
		public List<T> Values
		{
			get
			{
				return this.m_tlocal.ValuesForDebugDisplay;
			}
		}

		// Token: 0x04000AB7 RID: 2743
		private readonly ThreadLocal<T> m_tlocal;
	}
}
