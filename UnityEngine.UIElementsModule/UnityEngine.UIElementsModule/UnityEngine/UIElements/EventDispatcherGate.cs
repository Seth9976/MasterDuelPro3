using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AC RID: 428
	public struct EventDispatcherGate : IDisposable, IEquatable<EventDispatcherGate>
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x0003B820 File Offset: 0x00039A20
		public EventDispatcherGate(EventDispatcher d)
		{
			bool flag = d == null;
			if (flag)
			{
				throw new ArgumentNullException("d");
			}
			this.m_Dispatcher = d;
			this.m_Dispatcher.CloseGate();
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0003B855 File Offset: 0x00039A55
		public void Dispose()
		{
			this.m_Dispatcher.OpenGate();
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0003B864 File Offset: 0x00039A64
		public bool Equals(EventDispatcherGate other)
		{
			return object.Equals(this.m_Dispatcher, other.m_Dispatcher);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0003B888 File Offset: 0x00039A88
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is EventDispatcherGate && this.Equals((EventDispatcherGate)obj);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0003B8C0 File Offset: 0x00039AC0
		public override int GetHashCode()
		{
			return (this.m_Dispatcher != null) ? this.m_Dispatcher.GetHashCode() : 0;
		}

		// Token: 0x040007D1 RID: 2001
		private readonly EventDispatcher m_Dispatcher;
	}
}
