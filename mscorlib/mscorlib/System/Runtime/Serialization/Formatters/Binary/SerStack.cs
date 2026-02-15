using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000506 RID: 1286
	internal sealed class SerStack
	{
		// Token: 0x060028B1 RID: 10417 RVA: 0x000A6F92 File Offset: 0x000A5192
		internal SerStack(string stackId)
		{
			this.stackId = stackId;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000A6FB4 File Offset: 0x000A51B4
		internal void Push(object obj)
		{
			if (this.top == this.objects.Length - 1)
			{
				this.IncreaseCapacity();
			}
			object[] array = this.objects;
			int num = this.top + 1;
			this.top = num;
			array[num] = obj;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x000A6FF4 File Offset: 0x000A51F4
		internal object Pop()
		{
			if (this.top < 0)
			{
				return null;
			}
			object obj = this.objects[this.top];
			object[] array = this.objects;
			int num = this.top;
			this.top = num - 1;
			array[num] = null;
			return obj;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000A7034 File Offset: 0x000A5234
		internal void IncreaseCapacity()
		{
			object[] array = new object[this.objects.Length * 2];
			Array.Copy(this.objects, 0, array, 0, this.objects.Length);
			this.objects = array;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000A706E File Offset: 0x000A526E
		internal object Peek()
		{
			if (this.top < 0)
			{
				return null;
			}
			return this.objects[this.top];
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000A7088 File Offset: 0x000A5288
		internal object PeekPeek()
		{
			if (this.top < 1)
			{
				return null;
			}
			return this.objects[this.top - 1];
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000A70A4 File Offset: 0x000A52A4
		internal bool IsEmpty()
		{
			return this.top <= 0;
		}

		// Token: 0x04001494 RID: 5268
		internal object[] objects = new object[5];

		// Token: 0x04001495 RID: 5269
		internal string stackId;

		// Token: 0x04001496 RID: 5270
		internal int top = -1;
	}
}
