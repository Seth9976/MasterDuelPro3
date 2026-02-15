using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace Unity.Properties
{
	// Token: 0x02000038 RID: 56
	public class KeyValueCollectionPropertyBag<TDictionary, TKey, TValue> : PropertyBag<TDictionary>, IDictionaryPropertyBag<TDictionary, TKey, TValue>, ICollectionPropertyBag<TDictionary, KeyValuePair<TKey, TValue>>, IPropertyBag<TDictionary>, IPropertyBag, ICollectionPropertyBagAccept<TDictionary>, IDictionaryPropertyBagAccept<TDictionary>, IKeyedProperties<TDictionary, object> where TDictionary : IDictionary<TKey, TValue>
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000048B0 File Offset: 0x00002AB0
		public override PropertyCollection<TDictionary> GetProperties()
		{
			return PropertyCollection<TDictionary>.Empty;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000048C8 File Offset: 0x00002AC8
		public override PropertyCollection<TDictionary> GetProperties(ref TDictionary container)
		{
			return new PropertyCollection<TDictionary>(new KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.Enumerable(container, this.m_KeyValuePairProperty));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000048F5 File Offset: 0x00002AF5
		void ICollectionPropertyBagAccept<TDictionary>.Accept(ICollectionPropertyBagVisitor visitor, ref TDictionary container)
		{
			visitor.Visit<TDictionary, KeyValuePair<TKey, TValue>>(this, ref container);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004901 File Offset: 0x00002B01
		void IDictionaryPropertyBagAccept<TDictionary>.Accept(IDictionaryPropertyBagVisitor visitor, ref TDictionary container)
		{
			visitor.Visit<TDictionary, TKey, TValue>(this, ref container);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004910 File Offset: 0x00002B10
		bool IKeyedProperties<TDictionary, object>.TryGetProperty(ref TDictionary container, object key, out IProperty<TDictionary> property)
		{
			bool flag = container.ContainsKey((TKey)((object)key));
			bool flag2;
			if (flag)
			{
				property = new KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.KeyValuePairProperty
				{
					Key = (TKey)((object)key)
				};
				flag2 = true;
			}
			else
			{
				property = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x04000050 RID: 80
		private readonly KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.KeyValuePairProperty m_KeyValuePairProperty = new KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.KeyValuePairProperty();

		// Token: 0x02000039 RID: 57
		private class KeyValuePairProperty : Property<TDictionary, KeyValuePair<TKey, TValue>>, IDictionaryElementProperty
		{
			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000496C File Offset: 0x00002B6C
			public override string Name
			{
				get
				{
					TKey key = this.Key;
					return key.ToString();
				}
			}

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000498D File Offset: 0x00002B8D
			public override bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00004990 File Offset: 0x00002B90
			public override KeyValuePair<TKey, TValue> GetValue(ref TDictionary container)
			{
				return new KeyValuePair<TKey, TValue>(this.Key, container[this.Key]);
			}

			// Token: 0x060000CB RID: 203 RVA: 0x000049BF File Offset: 0x00002BBF
			public override void SetValue(ref TDictionary container, KeyValuePair<TKey, TValue> value)
			{
				container[value.Key] = value.Value;
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000CC RID: 204 RVA: 0x000049DD File Offset: 0x00002BDD
			// (set) Token: 0x060000CD RID: 205 RVA: 0x000049E5 File Offset: 0x00002BE5
			public TKey Key { get; internal set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000CE RID: 206 RVA: 0x000049EE File Offset: 0x00002BEE
			public object ObjectKey
			{
				get
				{
					return this.Key;
				}
			}
		}

		// Token: 0x0200003A RID: 58
		private readonly struct Enumerable : IEnumerable<IProperty<TDictionary>>, IEnumerable
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x00004A04 File Offset: 0x00002C04
			public Enumerable(TDictionary dictionary, KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.KeyValuePairProperty property)
			{
				this.m_Dictionary = dictionary;
				this.m_Property = property;
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00004A15 File Offset: 0x00002C15
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.Enumerable.Enumerator(this.m_Dictionary, this.m_Property);
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00004A15 File Offset: 0x00002C15
			IEnumerator<IProperty<TDictionary>> IEnumerable<IProperty<TDictionary>>.GetEnumerator()
			{
				return new KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.Enumerable.Enumerator(this.m_Dictionary, this.m_Property);
			}

			// Token: 0x04000052 RID: 82
			private readonly TDictionary m_Dictionary;

			// Token: 0x04000053 RID: 83
			private readonly KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.KeyValuePairProperty m_Property;

			// Token: 0x0200003B RID: 59
			private class Enumerator : IEnumerator<IProperty<TDictionary>>, IEnumerator, IDisposable
			{
				// Token: 0x060000D3 RID: 211 RVA: 0x00004A28 File Offset: 0x00002C28
				public Enumerator(TDictionary dictionary, KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.KeyValuePairProperty property)
				{
					this.m_Dictionary = dictionary;
					this.m_Property = property;
					this.m_Previous = property.Key;
					this.m_Position = -1;
					this.m_Keys = CollectionPool<List<TKey>, TKey>.Get();
					this.m_Keys.AddRange(this.m_Dictionary.Keys);
				}

				// Token: 0x1700002C RID: 44
				// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004A86 File Offset: 0x00002C86
				public IProperty<TDictionary> Current
				{
					get
					{
						return this.m_Property;
					}
				}

				// Token: 0x1700002D RID: 45
				// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004A8E File Offset: 0x00002C8E
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060000D6 RID: 214 RVA: 0x00004A98 File Offset: 0x00002C98
				public bool MoveNext()
				{
					this.m_Position++;
					int position = this.m_Position;
					TDictionary dictionary = this.m_Dictionary;
					bool flag = position < dictionary.Count;
					bool flag2;
					if (flag)
					{
						this.m_Property.Key = this.m_Keys[this.m_Position];
						flag2 = true;
					}
					else
					{
						this.m_Property.Key = this.m_Previous;
						flag2 = false;
					}
					return flag2;
				}

				// Token: 0x060000D7 RID: 215 RVA: 0x00004B0D File Offset: 0x00002D0D
				public void Reset()
				{
					this.m_Position = -1;
					this.m_Property.Key = this.m_Previous;
				}

				// Token: 0x060000D8 RID: 216 RVA: 0x00004B29 File Offset: 0x00002D29
				public void Dispose()
				{
					CollectionPool<List<TKey>, TKey>.Release(this.m_Keys);
				}

				// Token: 0x04000054 RID: 84
				private readonly TDictionary m_Dictionary;

				// Token: 0x04000055 RID: 85
				private readonly KeyValueCollectionPropertyBag<TDictionary, TKey, TValue>.KeyValuePairProperty m_Property;

				// Token: 0x04000056 RID: 86
				private readonly TKey m_Previous;

				// Token: 0x04000057 RID: 87
				private readonly List<TKey> m_Keys;

				// Token: 0x04000058 RID: 88
				private int m_Position;
			}
		}
	}
}
