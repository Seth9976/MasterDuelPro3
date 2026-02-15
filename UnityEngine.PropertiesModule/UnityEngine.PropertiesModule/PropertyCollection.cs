using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000042 RID: 66
	public readonly struct PropertyCollection<TContainer> : IEnumerable<IProperty<TContainer>>, IEnumerable
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005007 File Offset: 0x00003207
		public static PropertyCollection<TContainer> Empty { get; } = default(PropertyCollection<TContainer>);

		// Token: 0x06000105 RID: 261 RVA: 0x0000500E File Offset: 0x0000320E
		public PropertyCollection(IEnumerable<IProperty<TContainer>> enumerable)
		{
			this.m_Type = PropertyCollection<TContainer>.EnumeratorType.Enumerable;
			this.m_Enumerable = enumerable;
			this.m_Properties = null;
			this.m_IndexedCollectionPropertyBag = default(IndexedCollectionPropertyBagEnumerable<TContainer>);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005032 File Offset: 0x00003232
		public PropertyCollection(List<IProperty<TContainer>> properties)
		{
			this.m_Type = PropertyCollection<TContainer>.EnumeratorType.List;
			this.m_Enumerable = null;
			this.m_Properties = properties;
			this.m_IndexedCollectionPropertyBag = default(IndexedCollectionPropertyBagEnumerable<TContainer>);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005056 File Offset: 0x00003256
		internal PropertyCollection(IndexedCollectionPropertyBagEnumerable<TContainer> enumerable)
		{
			this.m_Type = PropertyCollection<TContainer>.EnumeratorType.IndexedCollectionPropertyBag;
			this.m_Enumerable = null;
			this.m_Properties = null;
			this.m_IndexedCollectionPropertyBag = enumerable;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005078 File Offset: 0x00003278
		public PropertyCollection<TContainer>.Enumerator GetEnumerator()
		{
			PropertyCollection<TContainer>.Enumerator enumerator;
			switch (this.m_Type)
			{
			case PropertyCollection<TContainer>.EnumeratorType.Empty:
				enumerator = default(PropertyCollection<TContainer>.Enumerator);
				break;
			case PropertyCollection<TContainer>.EnumeratorType.Enumerable:
				enumerator = new PropertyCollection<TContainer>.Enumerator(this.m_Enumerable.GetEnumerator());
				break;
			case PropertyCollection<TContainer>.EnumeratorType.List:
				enumerator = new PropertyCollection<TContainer>.Enumerator(this.m_Properties.GetEnumerator());
				break;
			case PropertyCollection<TContainer>.EnumeratorType.IndexedCollectionPropertyBag:
				enumerator = new PropertyCollection<TContainer>.Enumerator(this.m_IndexedCollectionPropertyBag.GetEnumerator());
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return enumerator;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000050F3 File Offset: 0x000032F3
		IEnumerator<IProperty<TContainer>> IEnumerable<IProperty<TContainer>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000050F3 File Offset: 0x000032F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000060 RID: 96
		private readonly PropertyCollection<TContainer>.EnumeratorType m_Type;

		// Token: 0x04000061 RID: 97
		private readonly IEnumerable<IProperty<TContainer>> m_Enumerable;

		// Token: 0x04000062 RID: 98
		private readonly List<IProperty<TContainer>> m_Properties;

		// Token: 0x04000063 RID: 99
		private readonly IndexedCollectionPropertyBagEnumerable<TContainer> m_IndexedCollectionPropertyBag;

		// Token: 0x02000043 RID: 67
		private enum EnumeratorType
		{
			// Token: 0x04000066 RID: 102
			Empty,
			// Token: 0x04000067 RID: 103
			Enumerable,
			// Token: 0x04000068 RID: 104
			List,
			// Token: 0x04000069 RID: 105
			IndexedCollectionPropertyBag
		}

		// Token: 0x02000044 RID: 68
		public struct Enumerator : IEnumerator<IProperty<TContainer>>, IEnumerator, IDisposable
		{
			// Token: 0x17000034 RID: 52
			// (get) Token: 0x0600010C RID: 268 RVA: 0x0000510D File Offset: 0x0000330D
			// (set) Token: 0x0600010D RID: 269 RVA: 0x00005115 File Offset: 0x00003315
			public IProperty<TContainer> Current { readonly get; private set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600010E RID: 270 RVA: 0x0000511E File Offset: 0x0000331E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00005126 File Offset: 0x00003326
			internal Enumerator(IEnumerator<IProperty<TContainer>> enumerator)
			{
				this.m_Type = PropertyCollection<TContainer>.EnumeratorType.Enumerable;
				this.m_Enumerator = enumerator;
				this.m_Properties = default(List<IProperty<TContainer>>.Enumerator);
				this.m_IndexedCollectionPropertyBag = default(IndexedCollectionPropertyBagEnumerator<TContainer>);
				this.Current = null;
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00005157 File Offset: 0x00003357
			internal Enumerator(List<IProperty<TContainer>>.Enumerator properties)
			{
				this.m_Type = PropertyCollection<TContainer>.EnumeratorType.List;
				this.m_Enumerator = null;
				this.m_Properties = properties;
				this.m_IndexedCollectionPropertyBag = default(IndexedCollectionPropertyBagEnumerator<TContainer>);
				this.Current = null;
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00005183 File Offset: 0x00003383
			internal Enumerator(IndexedCollectionPropertyBagEnumerator<TContainer> enumerator)
			{
				this.m_Type = PropertyCollection<TContainer>.EnumeratorType.IndexedCollectionPropertyBag;
				this.m_Enumerator = null;
				this.m_Properties = default(List<IProperty<TContainer>>.Enumerator);
				this.m_IndexedCollectionPropertyBag = enumerator;
				this.Current = null;
			}

			// Token: 0x06000112 RID: 274 RVA: 0x000051B0 File Offset: 0x000033B0
			public bool MoveNext()
			{
				bool result;
				switch (this.m_Type)
				{
				case PropertyCollection<TContainer>.EnumeratorType.Empty:
					return false;
				case PropertyCollection<TContainer>.EnumeratorType.Enumerable:
					result = this.m_Enumerator.MoveNext();
					this.Current = this.m_Enumerator.Current;
					break;
				case PropertyCollection<TContainer>.EnumeratorType.List:
					result = this.m_Properties.MoveNext();
					this.Current = this.m_Properties.Current;
					break;
				case PropertyCollection<TContainer>.EnumeratorType.IndexedCollectionPropertyBag:
					result = this.m_IndexedCollectionPropertyBag.MoveNext();
					this.Current = this.m_IndexedCollectionPropertyBag.Current;
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				return result;
			}

			// Token: 0x06000113 RID: 275 RVA: 0x00005250 File Offset: 0x00003450
			public void Reset()
			{
				switch (this.m_Type)
				{
				case PropertyCollection<TContainer>.EnumeratorType.Empty:
					break;
				case PropertyCollection<TContainer>.EnumeratorType.Enumerable:
					this.m_Enumerator.Reset();
					break;
				case PropertyCollection<TContainer>.EnumeratorType.List:
					((IEnumerator)this.m_Properties).Reset();
					break;
				case PropertyCollection<TContainer>.EnumeratorType.IndexedCollectionPropertyBag:
					this.m_IndexedCollectionPropertyBag.Reset();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}

			// Token: 0x06000114 RID: 276 RVA: 0x000052B8 File Offset: 0x000034B8
			public void Dispose()
			{
				switch (this.m_Type)
				{
				case PropertyCollection<TContainer>.EnumeratorType.Empty:
					break;
				case PropertyCollection<TContainer>.EnumeratorType.Enumerable:
					this.m_Enumerator.Dispose();
					break;
				case PropertyCollection<TContainer>.EnumeratorType.List:
					break;
				case PropertyCollection<TContainer>.EnumeratorType.IndexedCollectionPropertyBag:
					this.m_IndexedCollectionPropertyBag.Dispose();
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
			}

			// Token: 0x0400006A RID: 106
			private readonly PropertyCollection<TContainer>.EnumeratorType m_Type;

			// Token: 0x0400006B RID: 107
			private IEnumerator<IProperty<TContainer>> m_Enumerator;

			// Token: 0x0400006C RID: 108
			private List<IProperty<TContainer>>.Enumerator m_Properties;

			// Token: 0x0400006D RID: 109
			private IndexedCollectionPropertyBagEnumerator<TContainer> m_IndexedCollectionPropertyBag;
		}
	}
}
