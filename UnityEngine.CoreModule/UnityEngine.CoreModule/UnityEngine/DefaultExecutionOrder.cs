using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200017F RID: 383
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Class)]
	public class DefaultExecutionOrder : Attribute
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x00020DAD File Offset: 0x0001EFAD
		public DefaultExecutionOrder(int order)
		{
			this.m_Order = order;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00020DC0 File Offset: 0x0001EFC0
		public int order
		{
			get
			{
				return this.m_Order;
			}
		}

		// Token: 0x04000624 RID: 1572
		private int m_Order;
	}
}
