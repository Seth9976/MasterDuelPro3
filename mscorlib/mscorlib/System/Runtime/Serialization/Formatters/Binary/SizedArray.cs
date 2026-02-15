using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000507 RID: 1287
	[Serializable]
	internal sealed class SizedArray : ICloneable
	{
		// Token: 0x060028B8 RID: 10424 RVA: 0x000A70B2 File Offset: 0x000A52B2
		internal SizedArray()
		{
			this.objects = new object[16];
			this.negObjects = new object[4];
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000A70D3 File Offset: 0x000A52D3
		internal SizedArray(int length)
		{
			this.objects = new object[length];
			this.negObjects = new object[length];
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000A70F4 File Offset: 0x000A52F4
		private SizedArray(SizedArray sizedArray)
		{
			this.objects = new object[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new object[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000A7151 File Offset: 0x000A5351
		public object Clone()
		{
			return new SizedArray(this);
		}

		// Token: 0x17000556 RID: 1366
		internal object this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return null;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return null;
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
				object obj = this.objects[index];
				this.objects[index] = value;
			}
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000A71E8 File Offset: 0x000A53E8
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					object[] array = new object[Math.Max(this.negObjects.Length * 2, -index + 1)];
					Array.Copy(this.negObjects, 0, array, 0, this.negObjects.Length);
					this.negObjects = array;
				}
				else
				{
					object[] array2 = new object[Math.Max(this.objects.Length * 2, index + 1)];
					Array.Copy(this.objects, 0, array2, 0, this.objects.Length);
					this.objects = array2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Invalid BinaryFormatter stream."));
			}
		}

		// Token: 0x04001497 RID: 5271
		internal object[] objects;

		// Token: 0x04001498 RID: 5272
		internal object[] negObjects;
	}
}
