using System;
using System.Reflection;
using JetBrains.Annotations;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D0 RID: 464
	[DefaultMember("Item")]
	internal class EventCallbackList
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x0003D35B File Offset: 0x0003B55B
		public EventCallbackList()
		{
			this.m_Array = EventCallbackList.EmptyArray;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0003D370 File Offset: 0x0003B570
		public EventCallbackList(EventCallbackList source)
		{
			this.m_Count = source.m_Count;
			this.m_Array = new EventCallbackFunctorBase[this.m_Count];
			Array.Copy(source.m_Array, this.m_Array, this.m_Count);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003D3B0 File Offset: 0x0003B5B0
		public EventCallbackFunctorBase Find(long eventTypeId, [NotNull] Delegate callback)
		{
			for (int i = 0; i < this.m_Count; i++)
			{
				bool flag = this.m_Array[i].IsEquivalentTo(eventTypeId, callback);
				if (flag)
				{
					return this.m_Array[i];
				}
			}
			return null;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003D3FC File Offset: 0x0003B5FC
		public bool Remove(long eventTypeId, [NotNull] Delegate callback, out EventCallbackFunctorBase removedFunctor)
		{
			for (int i = 0; i < this.m_Count; i++)
			{
				bool flag = this.m_Array[i].IsEquivalentTo(eventTypeId, callback);
				if (flag)
				{
					removedFunctor = this.m_Array[i];
					this.m_Count--;
					Array.Copy(this.m_Array, i + 1, this.m_Array, i, this.m_Count - i);
					this.m_Array[this.m_Count] = null;
					return true;
				}
			}
			removedFunctor = null;
			return false;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003D488 File Offset: 0x0003B688
		public void Add(EventCallbackFunctorBase item)
		{
			bool flag = this.m_Count >= this.m_Array.Length;
			if (flag)
			{
				Array.Resize<EventCallbackFunctorBase>(ref this.m_Array, Mathf.NextPowerOfTwo(this.m_Count + 4));
			}
			EventCallbackFunctorBase[] array = this.m_Array;
			int count = this.m_Count;
			this.m_Count = count + 1;
			array[count] = item;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003D4E0 File Offset: 0x0003B6E0
		public void AddRange(EventCallbackList list)
		{
			bool flag = this.m_Count + list.m_Count > this.m_Array.Length;
			if (flag)
			{
				Array.Resize<EventCallbackFunctorBase>(ref this.m_Array, Mathf.NextPowerOfTwo(this.m_Count + list.m_Count));
			}
			Array.Copy(list.m_Array, 0, this.m_Array, this.m_Count, list.m_Count);
			this.m_Count += list.m_Count;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0003D559 File Offset: 0x0003B759
		public Span<EventCallbackFunctorBase> Span
		{
			get
			{
				return new Span<EventCallbackFunctorBase>(this.m_Array, 0, this.m_Count);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003D56D File Offset: 0x0003B76D
		public void Clear()
		{
			Array.Clear(this.m_Array, 0, this.m_Count);
			this.m_Count = 0;
		}

		// Token: 0x04000826 RID: 2086
		public static readonly EventCallbackList EmptyList = new EventCallbackList();

		// Token: 0x04000827 RID: 2087
		private static readonly EventCallbackFunctorBase[] EmptyArray = new EventCallbackFunctorBase[0];

		// Token: 0x04000828 RID: 2088
		private EventCallbackFunctorBase[] m_Array;

		// Token: 0x04000829 RID: 2089
		private int m_Count;
	}
}
