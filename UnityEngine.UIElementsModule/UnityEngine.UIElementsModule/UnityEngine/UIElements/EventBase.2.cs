using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C6 RID: 454
	[EventCategory(EventCategory.Default)]
	public abstract class EventBase<T> : EventBase where T : EventBase<T>, new()
	{
		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003CEF4 File Offset: 0x0003B0F4
		internal static void SetCreateFunction(Func<T> createMethod)
		{
			EventBase<T>.s_Pool.CreateFunc = createMethod;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003CF02 File Offset: 0x0003B102
		protected EventBase()
			: base(EventBase<T>.EventCategory)
		{
			this.m_RefCount = 0;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003CF18 File Offset: 0x0003B118
		public static long TypeId()
		{
			return EventBase<T>.s_TypeId;
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0003CF30 File Offset: 0x0003B130
		protected override void Init()
		{
			base.Init();
			bool flag = this.m_RefCount != 0;
			if (flag)
			{
				Debug.Log("Event improperly released.");
				this.m_RefCount = 0;
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0003CF68 File Offset: 0x0003B168
		public static T GetPooled()
		{
			T t = EventBase<T>.s_Pool.Get();
			t.Init();
			t.pooled = true;
			t.Acquire();
			return t;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0003CFAC File Offset: 0x0003B1AC
		internal static T GetPooled(EventBase e)
		{
			T t = EventBase<T>.GetPooled();
			bool flag = e != null;
			if (flag)
			{
				t.SetTriggerEventId(e.eventId);
			}
			return t;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0003CFE4 File Offset: 0x0003B1E4
		private static void ReleasePooled(T evt)
		{
			bool pooled = evt.pooled;
			if (pooled)
			{
				evt.Init();
				EventBase<T>.s_Pool.Release(evt);
				evt.pooled = false;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0003D028 File Offset: 0x0003B228
		internal override void Acquire()
		{
			this.m_RefCount++;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0003D03C File Offset: 0x0003B23C
		public sealed override void Dispose()
		{
			int num = this.m_RefCount - 1;
			this.m_RefCount = num;
			bool flag = num == 0;
			if (flag)
			{
				EventBase<T>.ReleasePooled((T)((object)this));
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0003D070 File Offset: 0x0003B270
		public override long eventTypeId
		{
			get
			{
				return EventBase<T>.s_TypeId;
			}
		}

		// Token: 0x04000814 RID: 2068
		private static readonly long s_TypeId = EventBase.RegisterEventType();

		// Token: 0x04000815 RID: 2069
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => new T(), 100);

		// Token: 0x04000816 RID: 2070
		private int m_RefCount;

		// Token: 0x04000817 RID: 2071
		internal static readonly EventCategory EventCategory = EventInterestReflectionUtils.GetEventCategory(typeof(T));
	}
}
