using System;

namespace System.Xml
{
	// Token: 0x0200001D RID: 29
	internal class BitStack
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000A032 File Offset: 0x00008232
		public BitStack()
		{
			this.curr = 1U;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000A041 File Offset: 0x00008241
		public void PushBit(bool bit)
		{
			if ((this.curr & 2147483648U) != 0U)
			{
				this.PushCurr();
			}
			this.curr = (this.curr << 1) | (bit ? 1U : 0U);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000A06D File Offset: 0x0000826D
		public bool PopBit()
		{
			bool flag = (this.curr & 1U) > 0U;
			this.curr >>= 1;
			if (this.curr == 1U)
			{
				this.PopCurr();
			}
			return flag;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000A097 File Offset: 0x00008297
		public bool PeekBit()
		{
			return (this.curr & 1U) > 0U;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000A0A4 File Offset: 0x000082A4
		private void PushCurr()
		{
			if (this.bitStack == null)
			{
				this.bitStack = new uint[16];
			}
			uint[] array = this.bitStack;
			int num = this.stackPos;
			this.stackPos = num + 1;
			array[num] = this.curr;
			this.curr = 1U;
			int num2 = this.bitStack.Length;
			if (this.stackPos >= num2)
			{
				uint[] array2 = new uint[2 * num2];
				Array.Copy(this.bitStack, array2, num2);
				this.bitStack = array2;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A11C File Offset: 0x0000831C
		private void PopCurr()
		{
			if (this.stackPos > 0)
			{
				uint[] array = this.bitStack;
				int num = this.stackPos - 1;
				this.stackPos = num;
				this.curr = array[num];
			}
		}

		// Token: 0x040000E9 RID: 233
		private uint[] bitStack;

		// Token: 0x040000EA RID: 234
		private int stackPos;

		// Token: 0x040000EB RID: 235
		private uint curr;
	}
}
