using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200025B RID: 603
	internal class ImmutableList<T>
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0002EB9B File Offset: 0x0002CD9B
		public T[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0002EBA3 File Offset: 0x0002CDA3
		private ImmutableList()
		{
			this.data = new T[0];
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0002EBB7 File Offset: 0x0002CDB7
		public ImmutableList(T[] data)
		{
			this.data = data;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002EBC8 File Offset: 0x0002CDC8
		public ImmutableList<T> Add(T value)
		{
			T[] newData = new T[this.data.Length + 1];
			Array.Copy(this.data, newData, this.data.Length);
			newData[this.data.Length] = value;
			return new ImmutableList<T>(newData);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002EC10 File Offset: 0x0002CE10
		public ImmutableList<T> Remove(T value)
		{
			int i = this.IndexOf(value);
			if (i < 0)
			{
				return this;
			}
			int length = this.data.Length;
			if (length == 1)
			{
				return ImmutableList<T>.Empty;
			}
			T[] newData = new T[length - 1];
			Array.Copy(this.data, 0, newData, 0, i);
			Array.Copy(this.data, i + 1, newData, i, length - i - 1);
			return new ImmutableList<T>(newData);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002EC74 File Offset: 0x0002CE74
		public int IndexOf(T value)
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				if (object.Equals(this.data[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040006C7 RID: 1735
		public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

		// Token: 0x040006C8 RID: 1736
		private T[] data;
	}
}
