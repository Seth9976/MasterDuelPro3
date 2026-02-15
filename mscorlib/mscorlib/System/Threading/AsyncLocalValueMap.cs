using System;
using System.Collections.Generic;

namespace System.Threading
{
	// Token: 0x02000212 RID: 530
	internal static class AsyncLocalValueMap
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00052B70 File Offset: 0x00050D70
		public static IAsyncLocalValueMap Empty { get; } = new AsyncLocalValueMap.EmptyAsyncLocalValueMap();

		// Token: 0x02000213 RID: 531
		internal sealed class EmptyAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x0600144D RID: 5197 RVA: 0x00052B84 File Offset: 0x00050D84
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value)
			{
				if (value == null)
				{
					return this;
				}
				return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(key, value);
			}

			// Token: 0x0600144E RID: 5198 RVA: 0x00052BA1 File Offset: 0x00050DA1
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				value = null;
				return false;
			}
		}

		// Token: 0x02000214 RID: 532
		internal sealed class OneElementAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06001450 RID: 5200 RVA: 0x00052BA7 File Offset: 0x00050DA7
			public OneElementAsyncLocalValueMap(IAsyncLocal key, object value)
			{
				this._key1 = key;
				this._value1 = value;
			}

			// Token: 0x06001451 RID: 5201 RVA: 0x00052BC0 File Offset: 0x00050DC0
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value)
			{
				if (value != null)
				{
					if (key != this._key1)
					{
						return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key1, this._value1, key, value);
					}
					return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(key, value);
				}
				else
				{
					if (key != this._key1)
					{
						return this;
					}
					return AsyncLocalValueMap.Empty;
				}
			}

			// Token: 0x06001452 RID: 5202 RVA: 0x00052C0B File Offset: 0x00050E0B
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				if (key == this._key1)
				{
					value = this._value1;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x040009F3 RID: 2547
			private readonly IAsyncLocal _key1;

			// Token: 0x040009F4 RID: 2548
			private readonly object _value1;
		}

		// Token: 0x02000215 RID: 533
		private sealed class TwoElementAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06001453 RID: 5203 RVA: 0x00052C24 File Offset: 0x00050E24
			public TwoElementAsyncLocalValueMap(IAsyncLocal key1, object value1, IAsyncLocal key2, object value2)
			{
				this._key1 = key1;
				this._value1 = value1;
				this._key2 = key2;
				this._value2 = value2;
			}

			// Token: 0x06001454 RID: 5204 RVA: 0x00052C4C File Offset: 0x00050E4C
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value)
			{
				if (value != null)
				{
					if (key == this._key1)
					{
						return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(key, value, this._key2, this._value2);
					}
					if (key != this._key2)
					{
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._key1, this._value1, this._key2, this._value2, key, value);
					}
					return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key1, this._value1, key, value);
				}
				else
				{
					if (key == this._key1)
					{
						return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(this._key2, this._value2);
					}
					if (key != this._key2)
					{
						return this;
					}
					return new AsyncLocalValueMap.OneElementAsyncLocalValueMap(this._key1, this._value1);
				}
			}

			// Token: 0x06001455 RID: 5205 RVA: 0x00052CF9 File Offset: 0x00050EF9
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				if (key == this._key1)
				{
					value = this._value1;
					return true;
				}
				if (key == this._key2)
				{
					value = this._value2;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x040009F5 RID: 2549
			private readonly IAsyncLocal _key1;

			// Token: 0x040009F6 RID: 2550
			private readonly IAsyncLocal _key2;

			// Token: 0x040009F7 RID: 2551
			private readonly object _value1;

			// Token: 0x040009F8 RID: 2552
			private readonly object _value2;
		}

		// Token: 0x02000216 RID: 534
		private sealed class ThreeElementAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06001456 RID: 5206 RVA: 0x00052D25 File Offset: 0x00050F25
			public ThreeElementAsyncLocalValueMap(IAsyncLocal key1, object value1, IAsyncLocal key2, object value2, IAsyncLocal key3, object value3)
			{
				this._key1 = key1;
				this._value1 = value1;
				this._key2 = key2;
				this._value2 = value2;
				this._key3 = key3;
				this._value3 = value3;
			}

			// Token: 0x06001457 RID: 5207 RVA: 0x00052D5C File Offset: 0x00050F5C
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value)
			{
				if (value != null)
				{
					if (key == this._key1)
					{
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(key, value, this._key2, this._value2, this._key3, this._value3);
					}
					if (key == this._key2)
					{
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._key1, this._value1, key, value, this._key3, this._value3);
					}
					if (key == this._key3)
					{
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._key1, this._value1, this._key2, this._value2, key, value);
					}
					AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(4);
					multiElementAsyncLocalValueMap.UnsafeStore(0, this._key1, this._value1);
					multiElementAsyncLocalValueMap.UnsafeStore(1, this._key2, this._value2);
					multiElementAsyncLocalValueMap.UnsafeStore(2, this._key3, this._value3);
					multiElementAsyncLocalValueMap.UnsafeStore(3, key, value);
					return multiElementAsyncLocalValueMap;
				}
				else
				{
					if (key == this._key1)
					{
						return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key2, this._value2, this._key3, this._value3);
					}
					if (key == this._key2)
					{
						return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key1, this._value1, this._key3, this._value3);
					}
					if (key != this._key3)
					{
						return this;
					}
					return new AsyncLocalValueMap.TwoElementAsyncLocalValueMap(this._key1, this._value1, this._key2, this._value2);
				}
			}

			// Token: 0x06001458 RID: 5208 RVA: 0x00052EB1 File Offset: 0x000510B1
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				if (key == this._key1)
				{
					value = this._value1;
					return true;
				}
				if (key == this._key2)
				{
					value = this._value2;
					return true;
				}
				if (key == this._key3)
				{
					value = this._value3;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x040009F9 RID: 2553
			private readonly IAsyncLocal _key1;

			// Token: 0x040009FA RID: 2554
			private readonly IAsyncLocal _key2;

			// Token: 0x040009FB RID: 2555
			private readonly IAsyncLocal _key3;

			// Token: 0x040009FC RID: 2556
			private readonly object _value1;

			// Token: 0x040009FD RID: 2557
			private readonly object _value2;

			// Token: 0x040009FE RID: 2558
			private readonly object _value3;
		}

		// Token: 0x02000217 RID: 535
		private sealed class MultiElementAsyncLocalValueMap : IAsyncLocalValueMap
		{
			// Token: 0x06001459 RID: 5209 RVA: 0x00052EF0 File Offset: 0x000510F0
			internal MultiElementAsyncLocalValueMap(int count)
			{
				this._keyValues = new KeyValuePair<IAsyncLocal, object>[count];
			}

			// Token: 0x0600145A RID: 5210 RVA: 0x00052F04 File Offset: 0x00051104
			internal void UnsafeStore(int index, IAsyncLocal key, object value)
			{
				this._keyValues[index] = new KeyValuePair<IAsyncLocal, object>(key, value);
			}

			// Token: 0x0600145B RID: 5211 RVA: 0x00052F1C File Offset: 0x0005111C
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value)
			{
				int i = 0;
				while (i < this._keyValues.Length)
				{
					if (key == this._keyValues[i].Key)
					{
						if (value != null)
						{
							AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(this._keyValues.Length);
							Array.Copy(this._keyValues, 0, multiElementAsyncLocalValueMap._keyValues, 0, this._keyValues.Length);
							multiElementAsyncLocalValueMap._keyValues[i] = new KeyValuePair<IAsyncLocal, object>(key, value);
							return multiElementAsyncLocalValueMap;
						}
						if (this._keyValues.Length != 4)
						{
							AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap2 = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(this._keyValues.Length - 1);
							if (i != 0)
							{
								Array.Copy(this._keyValues, 0, multiElementAsyncLocalValueMap2._keyValues, 0, i);
							}
							if (i != this._keyValues.Length - 1)
							{
								Array.Copy(this._keyValues, i + 1, multiElementAsyncLocalValueMap2._keyValues, i, this._keyValues.Length - i - 1);
							}
							return multiElementAsyncLocalValueMap2;
						}
						if (i == 0)
						{
							return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._keyValues[1].Key, this._keyValues[1].Value, this._keyValues[2].Key, this._keyValues[2].Value, this._keyValues[3].Key, this._keyValues[3].Value);
						}
						if (i == 1)
						{
							return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._keyValues[0].Key, this._keyValues[0].Value, this._keyValues[2].Key, this._keyValues[2].Value, this._keyValues[3].Key, this._keyValues[3].Value);
						}
						if (i != 2)
						{
							return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._keyValues[0].Key, this._keyValues[0].Value, this._keyValues[1].Key, this._keyValues[1].Value, this._keyValues[2].Key, this._keyValues[2].Value);
						}
						return new AsyncLocalValueMap.ThreeElementAsyncLocalValueMap(this._keyValues[0].Key, this._keyValues[0].Value, this._keyValues[1].Key, this._keyValues[1].Value, this._keyValues[3].Key, this._keyValues[3].Value);
					}
					else
					{
						i++;
					}
				}
				if (value == null)
				{
					return this;
				}
				if (this._keyValues.Length < 16)
				{
					AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap3 = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(this._keyValues.Length + 1);
					Array.Copy(this._keyValues, 0, multiElementAsyncLocalValueMap3._keyValues, 0, this._keyValues.Length);
					multiElementAsyncLocalValueMap3._keyValues[this._keyValues.Length] = new KeyValuePair<IAsyncLocal, object>(key, value);
					return multiElementAsyncLocalValueMap3;
				}
				AsyncLocalValueMap.ManyElementAsyncLocalValueMap manyElementAsyncLocalValueMap = new AsyncLocalValueMap.ManyElementAsyncLocalValueMap(17);
				foreach (KeyValuePair<IAsyncLocal, object> keyValuePair in this._keyValues)
				{
					manyElementAsyncLocalValueMap[keyValuePair.Key] = keyValuePair.Value;
				}
				manyElementAsyncLocalValueMap[key] = value;
				return manyElementAsyncLocalValueMap;
			}

			// Token: 0x0600145C RID: 5212 RVA: 0x00053274 File Offset: 0x00051474
			public bool TryGetValue(IAsyncLocal key, out object value)
			{
				foreach (KeyValuePair<IAsyncLocal, object> keyValuePair in this._keyValues)
				{
					if (key == keyValuePair.Key)
					{
						value = keyValuePair.Value;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x040009FF RID: 2559
			internal const int MaxMultiElements = 16;

			// Token: 0x04000A00 RID: 2560
			private readonly KeyValuePair<IAsyncLocal, object>[] _keyValues;
		}

		// Token: 0x02000218 RID: 536
		private sealed class ManyElementAsyncLocalValueMap : Dictionary<IAsyncLocal, object>, IAsyncLocalValueMap
		{
			// Token: 0x0600145D RID: 5213 RVA: 0x000532B7 File Offset: 0x000514B7
			public ManyElementAsyncLocalValueMap(int capacity)
				: base(capacity)
			{
			}

			// Token: 0x0600145E RID: 5214 RVA: 0x000532C0 File Offset: 0x000514C0
			public IAsyncLocalValueMap Set(IAsyncLocal key, object value)
			{
				int count = base.Count;
				bool flag = base.ContainsKey(key);
				if (value != null)
				{
					AsyncLocalValueMap.ManyElementAsyncLocalValueMap manyElementAsyncLocalValueMap = new AsyncLocalValueMap.ManyElementAsyncLocalValueMap(count + (flag ? 0 : 1));
					foreach (KeyValuePair<IAsyncLocal, object> keyValuePair in this)
					{
						manyElementAsyncLocalValueMap[keyValuePair.Key] = keyValuePair.Value;
					}
					manyElementAsyncLocalValueMap[key] = value;
					return manyElementAsyncLocalValueMap;
				}
				if (!flag)
				{
					return this;
				}
				if (count == 17)
				{
					AsyncLocalValueMap.MultiElementAsyncLocalValueMap multiElementAsyncLocalValueMap = new AsyncLocalValueMap.MultiElementAsyncLocalValueMap(16);
					int num = 0;
					foreach (KeyValuePair<IAsyncLocal, object> keyValuePair2 in this)
					{
						if (key != keyValuePair2.Key)
						{
							multiElementAsyncLocalValueMap.UnsafeStore(num++, keyValuePair2.Key, keyValuePair2.Value);
						}
					}
					return multiElementAsyncLocalValueMap;
				}
				AsyncLocalValueMap.ManyElementAsyncLocalValueMap manyElementAsyncLocalValueMap2 = new AsyncLocalValueMap.ManyElementAsyncLocalValueMap(count - 1);
				foreach (KeyValuePair<IAsyncLocal, object> keyValuePair3 in this)
				{
					if (key != keyValuePair3.Key)
					{
						manyElementAsyncLocalValueMap2[keyValuePair3.Key] = keyValuePair3.Value;
					}
				}
				return manyElementAsyncLocalValueMap2;
			}
		}
	}
}
