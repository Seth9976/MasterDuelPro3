using System;

namespace System.Xml
{
	// Token: 0x0200001F RID: 31
	internal class ByteStack
	{
		// Token: 0x0600011E RID: 286 RVA: 0x0000A1FD File Offset: 0x000083FD
		public ByteStack(int growthRate)
		{
			this.growthRate = growthRate;
			this.top = 0;
			this.stack = new byte[growthRate];
			this.size = growthRate;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A228 File Offset: 0x00008428
		public void Push(byte data)
		{
			if (this.size == this.top)
			{
				byte[] array = new byte[this.size + this.growthRate];
				if (this.top > 0)
				{
					Buffer.BlockCopy(this.stack, 0, array, 0, this.top);
				}
				this.stack = array;
				this.size += this.growthRate;
			}
			byte[] array2 = this.stack;
			int num = this.top;
			this.top = num + 1;
			array2[num] = data;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A2A8 File Offset: 0x000084A8
		public byte Pop()
		{
			if (this.top > 0)
			{
				byte[] array = this.stack;
				int num = this.top - 1;
				this.top = num;
				return array[num];
			}
			return 0;
		}

		// Token: 0x040000F1 RID: 241
		private byte[] stack;

		// Token: 0x040000F2 RID: 242
		private int growthRate;

		// Token: 0x040000F3 RID: 243
		private int top;

		// Token: 0x040000F4 RID: 244
		private int size;
	}
}
