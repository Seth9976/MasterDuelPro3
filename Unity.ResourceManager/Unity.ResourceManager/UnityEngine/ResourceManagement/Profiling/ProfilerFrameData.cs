using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.Profiling
{
	// Token: 0x02000073 RID: 115
	internal class ProfilerFrameData<T1, T2>
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00009FDF File Offset: 0x000081DF
		internal Dictionary<T1, T2> Data
		{
			get
			{
				return this.m_Data;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009FE7 File Offset: 0x000081E7
		public ProfilerFrameData()
		{
			this.m_Data = new Dictionary<T1, T2>(32);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009FFC File Offset: 0x000081FC
		public ProfilerFrameData(int count)
		{
			this.m_Data = new Dictionary<T1, T2>(count);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A010 File Offset: 0x00008210
		public bool Add(T1 key, T2 value)
		{
			int num = (this.m_Data.ContainsKey(key) ? 1 : 0);
			this.m_Data[key] = value;
			this.m_Version += 1U;
			return num == 0;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A03C File Offset: 0x0000823C
		internal bool Remove(T1 key)
		{
			bool flag = this.m_Data.Remove(key);
			if (flag)
			{
				this.m_Version += 1U;
			}
			return flag;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A05C File Offset: 0x0000825C
		public T2[] Values
		{
			get
			{
				if (this.m_ArrayVersion == this.m_Version)
				{
					return this.m_Array ?? Array.Empty<T2>();
				}
				this.m_Array = new T2[this.m_Data.Count];
				this.m_Data.Values.CopyTo(this.m_Array, 0);
				this.m_ArrayVersion = this.m_Version;
				return this.m_Array;
			}
		}

		// Token: 0x1700007C RID: 124
		public T2 this[T1 key]
		{
			get
			{
				T2 value;
				if (!this.m_Data.TryGetValue(key, out value))
				{
					throw new ArgumentOutOfRangeException("Key " + key.ToString() + " not found for FrameData");
				}
				return value;
			}
			set
			{
				T2 oldValue;
				if (this.m_Array != null && this.m_Data.TryGetValue(key, out oldValue))
				{
					for (int i = 0; i < this.m_Array.Length; i++)
					{
						if (this.m_Array[i].Equals(oldValue))
						{
							this.m_Array[i] = value;
							break;
						}
					}
				}
				this.m_Data[key] = value;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A17D File Offset: 0x0000837D
		public bool TryGetValue(T1 key, out T2 value)
		{
			return this.m_Data.TryGetValue(key, out value);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A18C File Offset: 0x0000838C
		public bool ContainsKey(T1 key)
		{
			return this.m_Data.ContainsKey(key);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A19A File Offset: 0x0000839A
		public IEnumerable<KeyValuePair<T1, T2>> Enumerate()
		{
			foreach (KeyValuePair<T1, T2> pair in this.m_Data)
			{
				yield return pair;
			}
			Dictionary<T1, T2>.Enumerator enumerator = default(Dictionary<T1, T2>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400012E RID: 302
		private Dictionary<T1, T2> m_Data;

		// Token: 0x0400012F RID: 303
		private T2[] m_Array;

		// Token: 0x04000130 RID: 304
		private uint m_Version;

		// Token: 0x04000131 RID: 305
		private uint m_ArrayVersion;
	}
}
