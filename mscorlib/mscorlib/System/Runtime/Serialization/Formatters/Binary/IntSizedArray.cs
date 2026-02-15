using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000508 RID: 1288
	[Serializable]
	internal sealed class IntSizedArray : ICloneable
	{
		// Token: 0x060028BF RID: 10431 RVA: 0x000A728C File Offset: 0x000A548C
		public IntSizedArray()
		{
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000A72B0 File Offset: 0x000A54B0
		private IntSizedArray(IntSizedArray sizedArray)
		{
			this.objects = new int[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new int[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000A7326 File Offset: 0x000A5526
		public object Clone()
		{
			return new IntSizedArray(this);
		}

		// Token: 0x17000557 RID: 1367
		internal int this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return 0;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return 0;
					}
					return this.objects[index];
				}
			}
			set
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						this.IncreaseCapacity(index);
					}
					this.negObjects[-index] = value;
					return;
				}
				if (index > this.objects.Length - 1)
				{
					this.IncreaseCapacity(index);
				}
				this.objects[index] = value;
			}
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000A73B8 File Offset: 0x000A55B8
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					int[] array = new int[Math.Max(this.negObjects.Length * 2, -index + 1)];
					Array.Copy(this.negObjects, 0, array, 0, this.negObjects.Length);
					this.negObjects = array;
				}
				else
				{
					int[] array2 = new int[Math.Max(this.objects.Length * 2, index + 1)];
					Array.Copy(this.objects, 0, array2, 0, this.objects.Length);
					this.objects = array2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
			}
		}

		// Token: 0x04001499 RID: 5273
		internal int[] objects = new int[16];

		// Token: 0x0400149A RID: 5274
		internal int[] negObjects = new int[4];
	}
}
