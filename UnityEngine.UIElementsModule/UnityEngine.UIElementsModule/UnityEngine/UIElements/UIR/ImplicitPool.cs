using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000528 RID: 1320
	internal class ImplicitPool<T> where T : class
	{
		// Token: 0x0600249E RID: 9374 RVA: 0x0008B81C File Offset: 0x00089A1C
		public ImplicitPool(Func<T> createAction, Action<T> resetAction, int startCapacity, int maxCapacity)
		{
			Debug.Assert(createAction != null);
			Debug.Assert(startCapacity > 0);
			Debug.Assert(startCapacity <= maxCapacity);
			Debug.Assert(maxCapacity > 0);
			this.m_List = new List<T>(0);
			this.m_StartCapacity = startCapacity;
			this.m_MaxCapacity = maxCapacity;
			this.m_CreateAction = createAction;
			this.m_ResetAction = resetAction;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0008B888 File Offset: 0x00089A88
		public T Get()
		{
			bool flag = this.m_UsedCount < this.m_List.Count;
			T t;
			if (flag)
			{
				List<T> list = this.m_List;
				int usedCount = this.m_UsedCount;
				this.m_UsedCount = usedCount + 1;
				t = list[usedCount];
			}
			else
			{
				bool flag2 = this.m_UsedCount < this.m_MaxCapacity;
				if (flag2)
				{
					int desiredAllocs = Mathf.Max(this.m_StartCapacity, this.m_UsedCount);
					int maxAllocs = this.m_MaxCapacity - this.m_UsedCount;
					int i = Mathf.Min(maxAllocs, desiredAllocs);
					this.m_List.Capacity = this.m_UsedCount + i;
					T result = this.m_CreateAction();
					this.m_List.Add(result);
					this.m_UsedCount++;
					for (int j = 1; j < i; j++)
					{
						this.m_List.Add(this.m_CreateAction());
					}
					t = result;
				}
				else
				{
					t = this.m_CreateAction();
				}
			}
			return t;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0008B994 File Offset: 0x00089B94
		public void ReturnAll()
		{
			Debug.Assert(this.m_List.Count <= this.m_MaxCapacity);
			bool flag = this.m_ResetAction != null;
			if (flag)
			{
				for (int i = 0; i < this.m_UsedCount; i++)
				{
					this.m_ResetAction(this.m_List[i]);
				}
			}
			this.m_UsedCount = 0;
		}

		// Token: 0x04001181 RID: 4481
		private readonly int m_StartCapacity;

		// Token: 0x04001182 RID: 4482
		private readonly int m_MaxCapacity;

		// Token: 0x04001183 RID: 4483
		private Func<T> m_CreateAction;

		// Token: 0x04001184 RID: 4484
		private Action<T> m_ResetAction;

		// Token: 0x04001185 RID: 4485
		private List<T> m_List;

		// Token: 0x04001186 RID: 4486
		private int m_UsedCount;
	}
}
