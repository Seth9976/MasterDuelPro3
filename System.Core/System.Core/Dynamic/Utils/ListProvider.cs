using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x0200014B RID: 331
	internal abstract class ListProvider<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000AD4 RID: 2772
		protected abstract T First { get; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000AD5 RID: 2773
		protected abstract int ElementCount { get; }

		// Token: 0x06000AD6 RID: 2774
		protected abstract T GetElement(int index);

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002AE6C File Offset: 0x0002906C
		public int IndexOf(T item)
		{
			if (this.First == item)
			{
				return 0;
			}
			int i = 1;
			int elementCount = this.ElementCount;
			while (i < elementCount)
			{
				if (this.GetElement(i) == item)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public void Insert(int index, T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public void RemoveAt(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170001CF RID: 463
		public T this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.First;
				}
				return this.GetElement(index);
			}
			[ExcludeFromCodeCoverage]
			set
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public void Add(T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public void Clear()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002AECB File Offset: 0x000290CB
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002AEDC File Offset: 0x000290DC
		public void CopyTo(T[] array, int index)
		{
			ContractUtils.RequiresNotNull(array, "array");
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			int elementCount = this.ElementCount;
			if (index + elementCount > array.Length)
			{
				throw new ArgumentException();
			}
			array[index++] = this.First;
			for (int i = 1; i < elementCount; i++)
			{
				array[index++] = this.GetElement(i);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0002AF49 File Offset: 0x00029149
		public int Count
		{
			get
			{
				return this.ElementCount;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00009F9F File Offset: 0x0000819F
		[ExcludeFromCodeCoverage]
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public bool Remove(T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002AF51 File Offset: 0x00029151
		public IEnumerator<T> GetEnumerator()
		{
			yield return this.First;
			int i = 1;
			int j = this.ElementCount;
			while (i < j)
			{
				yield return this.GetElement(i);
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002AF60 File Offset: 0x00029160
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
