using System;
using System.Diagnostics;

namespace TMPro
{
	// Token: 0x0200009E RID: 158
	[DebuggerDisplay("Item count = {m_Count}")]
	public struct TMP_TextProcessingStack<T>
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0002C0BA File Offset: 0x0002A2BA
		public TMP_TextProcessingStack(T[] stack)
		{
			this.itemStack = stack;
			this.m_Capacity = stack.Length;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0002C0ED File Offset: 0x0002A2ED
		public TMP_TextProcessingStack(int capacity)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002C123 File Offset: 0x0002A323
		public TMP_TextProcessingStack(int capacity, int rolloverSize)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = rolloverSize;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0002C159 File Offset: 0x0002A359
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0002C161 File Offset: 0x0002A361
		public T current
		{
			get
			{
				if (this.index > 0)
				{
					return this.itemStack[this.index - 1];
				}
				return this.itemStack[0];
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0002C18C File Offset: 0x0002A38C
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0002C194 File Offset: 0x0002A394
		public int rolloverSize
		{
			get
			{
				return this.m_RolloverSize;
			}
			set
			{
				this.m_RolloverSize = value;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
		internal static void SetDefault(TMP_TextProcessingStack<T>[] stack, T item)
		{
			for (int i = 0; i < stack.Length; i++)
			{
				stack[i].SetDefault(item);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0002C1C8 File Offset: 0x0002A3C8
		public void Clear()
		{
			this.index = 0;
			this.m_Count = 0;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0002C1D8 File Offset: 0x0002A3D8
		public void SetDefault(T item)
		{
			if (this.itemStack == null)
			{
				this.m_Capacity = 4;
				this.itemStack = new T[this.m_Capacity];
				this.m_DefaultItem = default(T);
			}
			this.itemStack[0] = item;
			this.index = 1;
			this.m_Count = 1;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002C22C File Offset: 0x0002A42C
		public void Add(T item)
		{
			if (this.index < this.itemStack.Length)
			{
				this.itemStack[this.index] = item;
				this.index++;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002C260 File Offset: 0x0002A460
		public T Remove()
		{
			this.index--;
			this.m_Count--;
			if (this.index <= 0)
			{
				this.m_Count = 0;
				this.index = 1;
				return this.itemStack[0];
			}
			return this.itemStack[this.index - 1];
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0002C2C0 File Offset: 0x0002A4C0
		public void Push(T item)
		{
			if (this.index == this.m_Capacity)
			{
				this.m_Capacity *= 2;
				if (this.m_Capacity == 0)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.itemStack, this.m_Capacity);
			}
			this.itemStack[this.index] = item;
			if (this.m_RolloverSize == 0)
			{
				this.index++;
				this.m_Count++;
				return;
			}
			this.index = (this.index + 1) % this.m_RolloverSize;
			this.m_Count = ((this.m_Count < this.m_RolloverSize) ? (this.m_Count + 1) : this.m_RolloverSize);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0002C37C File Offset: 0x0002A57C
		public T Pop()
		{
			if (this.index == 0 && this.m_RolloverSize == 0)
			{
				return default(T);
			}
			if (this.m_RolloverSize == 0)
			{
				this.index--;
			}
			else
			{
				this.index = (this.index - 1) % this.m_RolloverSize;
				this.index = ((this.index < 0) ? (this.index + this.m_RolloverSize) : this.index);
			}
			T t = this.itemStack[this.index];
			this.itemStack[this.index] = this.m_DefaultItem;
			this.m_Count = ((this.m_Count > 0) ? (this.m_Count - 1) : 0);
			return t;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0002C436 File Offset: 0x0002A636
		public T Peek()
		{
			if (this.index == 0)
			{
				return this.m_DefaultItem;
			}
			return this.itemStack[this.index - 1];
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0002C161 File Offset: 0x0002A361
		public T CurrentItem()
		{
			if (this.index > 0)
			{
				return this.itemStack[this.index - 1];
			}
			return this.itemStack[0];
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0002C45A File Offset: 0x0002A65A
		public T PreviousItem()
		{
			if (this.index > 1)
			{
				return this.itemStack[this.index - 2];
			}
			return this.itemStack[0];
		}

		// Token: 0x04000577 RID: 1399
		public T[] itemStack;

		// Token: 0x04000578 RID: 1400
		public int index;

		// Token: 0x04000579 RID: 1401
		private T m_DefaultItem;

		// Token: 0x0400057A RID: 1402
		private int m_Capacity;

		// Token: 0x0400057B RID: 1403
		private int m_RolloverSize;

		// Token: 0x0400057C RID: 1404
		private int m_Count;

		// Token: 0x0400057D RID: 1405
		private const int k_DefaultCapacity = 4;
	}
}
