using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200000E RID: 14
	internal class XRLayoutStack : IDisposable
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002EF4 File Offset: 0x000010F4
		public XRLayout New()
		{
			XRLayout layout;
			GenericPool<XRLayout>.Get(out layout);
			this.m_Stack.Push(layout);
			return layout;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002F16 File Offset: 0x00001116
		public XRLayout top
		{
			get
			{
				return this.m_Stack.Peek();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F24 File Offset: 0x00001124
		public void Release()
		{
			XRLayout value;
			if (!this.m_Stack.TryPop(out value))
			{
				throw new InvalidOperationException("Calling Release without calling New first.");
			}
			value.Clear();
			GenericPool<XRLayout>.Release(value);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F57 File Offset: 0x00001157
		public void Dispose()
		{
			if (this.m_Stack.Count != 0)
			{
				throw new Exception("Stack is not empty. Did you skip a call to Release?");
			}
		}

		// Token: 0x04000036 RID: 54
		private readonly Stack<XRLayout> m_Stack = new Stack<XRLayout>();
	}
}
