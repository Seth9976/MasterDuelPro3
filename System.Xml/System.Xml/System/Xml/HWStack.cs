using System;

namespace System.Xml
{
	// Token: 0x020000FF RID: 255
	internal class HWStack : ICloneable
	{
		// Token: 0x06000D79 RID: 3449 RVA: 0x000431E2 File Offset: 0x000413E2
		internal HWStack(int GrowthRate)
			: this(GrowthRate, int.MaxValue)
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000431F0 File Offset: 0x000413F0
		internal HWStack(int GrowthRate, int limit)
		{
			this.growthRate = GrowthRate;
			this.used = 0;
			this.stack = new object[GrowthRate];
			this.size = GrowthRate;
			this.limit = limit;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00043220 File Offset: 0x00041420
		internal object Push()
		{
			if (this.used == this.size)
			{
				if (this.limit <= this.used)
				{
					throw new XmlException("Stack overflow.", string.Empty);
				}
				object[] array = new object[this.size + this.growthRate];
				if (this.used > 0)
				{
					Array.Copy(this.stack, 0, array, 0, this.used);
				}
				this.stack = array;
				this.size += this.growthRate;
			}
			object[] array2 = this.stack;
			int num = this.used;
			this.used = num + 1;
			return array2[num];
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000432BB File Offset: 0x000414BB
		internal object Pop()
		{
			if (0 < this.used)
			{
				this.used--;
				return this.stack[this.used];
			}
			return null;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000432E3 File Offset: 0x000414E3
		internal object Peek()
		{
			if (this.used <= 0)
			{
				return null;
			}
			return this.stack[this.used - 1];
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000432FF File Offset: 0x000414FF
		internal void AddToTop(object o)
		{
			if (this.used > 0)
			{
				this.stack[this.used - 1] = o;
			}
		}

		// Token: 0x1700034C RID: 844
		internal object this[int index]
		{
			get
			{
				if (index >= 0 && index < this.used)
				{
					return this.stack[index];
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < this.used)
				{
					this.stack[index] = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00043355 File Offset: 0x00041555
		internal int Length
		{
			get
			{
				return this.used;
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0004335D File Offset: 0x0004155D
		private HWStack(object[] stack, int growthRate, int used, int size)
		{
			this.stack = stack;
			this.growthRate = growthRate;
			this.used = used;
			this.size = size;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00043382 File Offset: 0x00041582
		public object Clone()
		{
			return new HWStack((object[])this.stack.Clone(), this.growthRate, this.used, this.size);
		}

		// Token: 0x04000685 RID: 1669
		private object[] stack;

		// Token: 0x04000686 RID: 1670
		private int growthRate;

		// Token: 0x04000687 RID: 1671
		private int used;

		// Token: 0x04000688 RID: 1672
		private int size;

		// Token: 0x04000689 RID: 1673
		private int limit;
	}
}
