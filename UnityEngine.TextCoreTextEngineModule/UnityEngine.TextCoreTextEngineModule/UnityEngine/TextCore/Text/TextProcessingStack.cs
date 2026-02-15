using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000064 RID: 100
	[DebuggerDisplay("Item count = {m_Count}")]
	internal struct TextProcessingStack<T>
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0002F24B File Offset: 0x0002D44B
		public TextProcessingStack(T[] stack)
		{
			this.itemStack = stack;
			this.m_Capacity = stack.Length;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0002F27F File Offset: 0x0002D47F
		public TextProcessingStack(int capacity)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0002F2B6 File Offset: 0x0002D4B6
		public TextProcessingStack(int capacity, int rolloverSize)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = rolloverSize;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0002F308 File Offset: 0x0002D508
		public T current
		{
			get
			{
				bool flag = this.index > 0;
				T t;
				if (flag)
				{
					t = this.itemStack[this.index - 1];
				}
				else
				{
					t = this.itemStack[0];
				}
				return t;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0002F34C File Offset: 0x0002D54C
		internal static void SetDefault(TextProcessingStack<T>[] stack, T item)
		{
			for (int i = 0; i < stack.Length; i++)
			{
				stack[i].SetDefault(item);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0002F37A File Offset: 0x0002D57A
		public void Clear()
		{
			this.index = 0;
			this.m_Count = 0;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0002F38C File Offset: 0x0002D58C
		public void SetDefault(T item)
		{
			bool flag = this.itemStack == null;
			if (flag)
			{
				this.m_Capacity = 4;
				this.itemStack = new T[this.m_Capacity];
				this.m_DefaultItem = default(T);
			}
			this.itemStack[0] = item;
			this.index = 1;
			this.m_Count = 1;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0002F3E8 File Offset: 0x0002D5E8
		public void Add(T item)
		{
			bool flag = this.index < this.itemStack.Length;
			if (flag)
			{
				this.itemStack[this.index] = item;
				this.index++;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0002F42C File Offset: 0x0002D62C
		public T Remove()
		{
			this.index--;
			this.m_Count--;
			bool flag = this.index <= 0;
			T t;
			if (flag)
			{
				this.m_Count = 0;
				this.index = 1;
				t = this.itemStack[0];
			}
			else
			{
				t = this.itemStack[this.index - 1];
			}
			return t;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0002F49C File Offset: 0x0002D69C
		public void Push(T item)
		{
			bool flag = this.index == this.m_Capacity;
			if (flag)
			{
				this.m_Capacity *= 2;
				bool flag2 = this.m_Capacity == 0;
				if (flag2)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.itemStack, this.m_Capacity);
			}
			this.itemStack[this.index] = item;
			bool flag3 = this.m_RolloverSize == 0;
			if (flag3)
			{
				this.index++;
				this.m_Count++;
			}
			else
			{
				this.index = (this.index + 1) % this.m_RolloverSize;
				this.m_Count = ((this.m_Count < this.m_RolloverSize) ? (this.m_Count + 1) : this.m_RolloverSize);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0002F56C File Offset: 0x0002D76C
		public T Pop()
		{
			bool flag = this.index == 0 && this.m_RolloverSize == 0;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				bool flag2 = this.m_RolloverSize == 0;
				if (flag2)
				{
					this.index--;
				}
				else
				{
					this.index = (this.index - 1) % this.m_RolloverSize;
					this.index = ((this.index < 0) ? (this.index + this.m_RolloverSize) : this.index);
				}
				T item = this.itemStack[this.index];
				this.itemStack[this.index] = this.m_DefaultItem;
				this.m_Count = ((this.m_Count > 0) ? (this.m_Count - 1) : 0);
				t = item;
			}
			return t;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0002F644 File Offset: 0x0002D844
		public T Peek()
		{
			bool flag = this.index == 0;
			T t;
			if (flag)
			{
				t = this.m_DefaultItem;
			}
			else
			{
				t = this.itemStack[this.index - 1];
			}
			return t;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0002F680 File Offset: 0x0002D880
		public T CurrentItem()
		{
			bool flag = this.index > 0;
			T t;
			if (flag)
			{
				t = this.itemStack[this.index - 1];
			}
			else
			{
				t = this.itemStack[0];
			}
			return t;
		}

		// Token: 0x04000443 RID: 1091
		public T[] itemStack;

		// Token: 0x04000444 RID: 1092
		public int index;

		// Token: 0x04000445 RID: 1093
		private T m_DefaultItem;

		// Token: 0x04000446 RID: 1094
		private int m_Capacity;

		// Token: 0x04000447 RID: 1095
		private int m_RolloverSize;

		// Token: 0x04000448 RID: 1096
		private int m_Count;

		// Token: 0x04000449 RID: 1097
		private const int k_DefaultCapacity = 4;
	}
}
