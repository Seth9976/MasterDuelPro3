using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	/// <summary>Represents an object whose members can be dynamically added and removed at run time.</summary>
	// Token: 0x0200012E RID: 302
	public sealed class ExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, INotifyPropertyChanged
	{
		/// <summary>Initializes a new ExpandoObject that does not have members.</summary>
		// Token: 0x060009FC RID: 2556 RVA: 0x00026C00 File Offset: 0x00024E00
		public ExpandoObject()
		{
			this._data = ExpandoObject.ExpandoData.Empty;
			this.LockObject = new object();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00026C20 File Offset: 0x00024E20
		internal bool TryGetValue(object indexClass, int index, string name, bool ignoreCase, out object value)
		{
			ExpandoObject.ExpandoData data = this._data;
			if (data.Class != indexClass || ignoreCase)
			{
				index = data.Class.GetValueIndex(name, ignoreCase, this);
				if (index == -2)
				{
					throw Error.AmbiguousMatchInExpandoObject(name);
				}
			}
			if (index == -1)
			{
				value = null;
				return false;
			}
			object obj = data[index];
			if (obj == ExpandoObject.Uninitialized)
			{
				value = null;
				return false;
			}
			value = obj;
			return true;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00026C88 File Offset: 0x00024E88
		internal void TrySetValue(object indexClass, int index, object value, string name, bool ignoreCase, bool add)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData expandoData;
			object obj;
			lock (lockObject)
			{
				expandoData = this._data;
				if (expandoData.Class != indexClass || ignoreCase)
				{
					index = expandoData.Class.GetValueIndex(name, ignoreCase, this);
					if (index == -2)
					{
						throw Error.AmbiguousMatchInExpandoObject(name);
					}
					if (index == -1)
					{
						int num = (ignoreCase ? expandoData.Class.GetValueIndexCaseSensitive(name) : index);
						if (num != -1)
						{
							index = num;
						}
						else
						{
							ExpandoClass expandoClass = expandoData.Class.FindNewClass(name);
							expandoData = this.PromoteClassCore(expandoData.Class, expandoClass);
							index = expandoData.Class.GetValueIndexCaseSensitive(name);
						}
					}
				}
				obj = expandoData[index];
				if (obj == ExpandoObject.Uninitialized)
				{
					this._count++;
				}
				else if (add)
				{
					throw Error.SameKeyExistsInExpando(name);
				}
				expandoData[index] = value;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null && value != obj)
			{
				propertyChanged(this, new PropertyChangedEventArgs(expandoData.Class.Keys[index]));
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00026DA8 File Offset: 0x00024FA8
		internal bool TryDeleteValue(object indexClass, int index, string name, bool ignoreCase, object deleteValue)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				data = this._data;
				if (data.Class != indexClass || ignoreCase)
				{
					index = data.Class.GetValueIndex(name, ignoreCase, this);
					if (index == -2)
					{
						throw Error.AmbiguousMatchInExpandoObject(name);
					}
				}
				if (index == -1)
				{
					return false;
				}
				object obj = data[index];
				if (obj == ExpandoObject.Uninitialized)
				{
					return false;
				}
				if (deleteValue != ExpandoObject.Uninitialized && !object.Equals(obj, deleteValue))
				{
					return false;
				}
				data[index] = ExpandoObject.Uninitialized;
				this._count--;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(data.Class.Keys[index]));
			}
			return true;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00026E90 File Offset: 0x00025090
		internal bool IsDeletedMember(int index)
		{
			return index != this._data.Length && this._data[index] == ExpandoObject.Uninitialized;
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00026EB5 File Offset: 0x000250B5
		internal ExpandoClass Class
		{
			get
			{
				return this._data.Class;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00026EC2 File Offset: 0x000250C2
		private ExpandoObject.ExpandoData PromoteClassCore(ExpandoClass oldClass, ExpandoClass newClass)
		{
			if (this._data.Class == oldClass)
			{
				this._data = this._data.UpdateClass(newClass);
			}
			return this._data;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00026EEC File Offset: 0x000250EC
		internal void PromoteClass(object oldClass, object newClass)
		{
			object lockObject = this.LockObject;
			lock (lockObject)
			{
				this.PromoteClassCore((ExpandoClass)oldClass, (ExpandoClass)newClass);
			}
		}

		/// <summary>The provided MetaObject will dispatch to the dynamic virtual methods. The object can be encapsulated inside another MetaObject to provide custom behavior for individual actions.</summary>
		/// <returns>The object of the <see cref="T:System.Dynamic.DynamicMetaObject" /> type.</returns>
		/// <param name="parameter">The expression that represents the MetaObject to dispatch to the Dynamic virtual methods.</param>
		// Token: 0x06000A04 RID: 2564 RVA: 0x00026F3C File Offset: 0x0002513C
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new ExpandoObject.MetaExpando(parameter, this);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00026F45 File Offset: 0x00025145
		private void TryAddMember(string key, object value)
		{
			ContractUtils.RequiresNotNull(key, "key");
			this.TrySetValue(null, -1, value, key, false, true);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00026F5E File Offset: 0x0002515E
		private bool TryGetValueForKey(string key, out object value)
		{
			return this.TryGetValue(null, -1, key, false, out value);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00026F6B File Offset: 0x0002516B
		private bool ExpandoContainsKey(string key)
		{
			return this._data.Class.GetValueIndexCaseSensitive(key) >= 0;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00026F84 File Offset: 0x00025184
		ICollection<string> IDictionary<string, object>.Keys
		{
			get
			{
				return new ExpandoObject.KeyCollection(this);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00026F8C File Offset: 0x0002518C
		ICollection<object> IDictionary<string, object>.Values
		{
			get
			{
				return new ExpandoObject.ValueCollection(this);
			}
		}

		// Token: 0x170001AE RID: 430
		object IDictionary<string, object>.this[string key]
		{
			get
			{
				object obj;
				if (!this.TryGetValueForKey(key, out obj))
				{
					throw Error.KeyDoesNotExistInExpando(key);
				}
				return obj;
			}
			set
			{
				ContractUtils.RequiresNotNull(key, "key");
				this.TrySetValue(null, -1, value, key, false, false);
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00026FCD File Offset: 0x000251CD
		void IDictionary<string, object>.Add(string key, object value)
		{
			this.TryAddMember(key, value);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00026FD8 File Offset: 0x000251D8
		bool IDictionary<string, object>.ContainsKey(string key)
		{
			ContractUtils.RequiresNotNull(key, "key");
			ExpandoObject.ExpandoData data = this._data;
			int valueIndexCaseSensitive = data.Class.GetValueIndexCaseSensitive(key);
			return valueIndexCaseSensitive >= 0 && data[valueIndexCaseSensitive] != ExpandoObject.Uninitialized;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002701B File Offset: 0x0002521B
		bool IDictionary<string, object>.Remove(string key)
		{
			ContractUtils.RequiresNotNull(key, "key");
			return this.TryDeleteValue(null, -1, key, false, ExpandoObject.Uninitialized);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00027037 File Offset: 0x00025237
		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return this.TryGetValueForKey(key, out value);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00027041 File Offset: 0x00025241
		int ICollection<KeyValuePair<string, object>>.Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0000B252 File Offset: 0x00009452
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00027049 File Offset: 0x00025249
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			this.TryAddMember(item.Key, item.Value);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00027060 File Offset: 0x00025260
		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				data = this._data;
				this._data = ExpandoObject.ExpandoData.Empty;
				this._count = 0;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null)
			{
				int i = 0;
				int num = data.Class.Keys.Length;
				while (i < num)
				{
					if (data[i] != ExpandoObject.Uninitialized)
					{
						propertyChanged(this, new PropertyChangedEventArgs(data.Class.Keys[i]));
					}
					i++;
				}
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00027108 File Offset: 0x00025308
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			object obj;
			return this.TryGetValueForKey(item.Key, out obj) && object.Equals(obj, item.Value);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00027138 File Offset: 0x00025338
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			ContractUtils.RequiresNotNull(array, "array");
			object lockObject = this.LockObject;
			lock (lockObject)
			{
				ContractUtils.RequiresArrayRange<KeyValuePair<string, object>>(array, arrayIndex, this._count, "arrayIndex", "Count");
				foreach (KeyValuePair<string, object> keyValuePair in ((IEnumerable<KeyValuePair<string, object>>)this))
				{
					array[arrayIndex++] = keyValuePair;
				}
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000271D0 File Offset: 0x000253D0
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return this.TryDeleteValue(null, -1, item.Key, false, item.Value);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000271EC File Offset: 0x000253EC
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			ExpandoObject.ExpandoData data = this._data;
			return this.GetExpandoEnumerator(data, data.Version);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06000A18 RID: 2584 RVA: 0x00027210 File Offset: 0x00025410
		IEnumerator IEnumerable.GetEnumerator()
		{
			ExpandoObject.ExpandoData data = this._data;
			return this.GetExpandoEnumerator(data, data.Version);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00027231 File Offset: 0x00025431
		private IEnumerator<KeyValuePair<string, object>> GetExpandoEnumerator(ExpandoObject.ExpandoData data, int version)
		{
			int num;
			for (int i = 0; i < data.Class.Keys.Length; i = num + 1)
			{
				if (this._data.Version != version || data != this._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				object obj = data[i];
				if (obj != ExpandoObject.Uninitialized)
				{
					yield return new KeyValuePair<string, object>(data.Class.Keys[i], obj);
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x04000314 RID: 788
		private static readonly MethodInfo s_expandoTryGetValue = typeof(RuntimeOps).GetMethod("ExpandoTryGetValue");

		// Token: 0x04000315 RID: 789
		private static readonly MethodInfo s_expandoTrySetValue = typeof(RuntimeOps).GetMethod("ExpandoTrySetValue");

		// Token: 0x04000316 RID: 790
		private static readonly MethodInfo s_expandoTryDeleteValue = typeof(RuntimeOps).GetMethod("ExpandoTryDeleteValue");

		// Token: 0x04000317 RID: 791
		private static readonly MethodInfo s_expandoPromoteClass = typeof(RuntimeOps).GetMethod("ExpandoPromoteClass");

		// Token: 0x04000318 RID: 792
		private static readonly MethodInfo s_expandoCheckVersion = typeof(RuntimeOps).GetMethod("ExpandoCheckVersion");

		// Token: 0x04000319 RID: 793
		internal readonly object LockObject;

		// Token: 0x0400031A RID: 794
		private ExpandoObject.ExpandoData _data;

		// Token: 0x0400031B RID: 795
		private int _count;

		// Token: 0x0400031C RID: 796
		internal static readonly object Uninitialized = new object();

		// Token: 0x0400031D RID: 797
		private PropertyChangedEventHandler _propertyChanged;

		// Token: 0x0200012F RID: 303
		private sealed class KeyCollectionDebugView
		{
		}

		// Token: 0x02000130 RID: 304
		[DebuggerTypeProxy(typeof(ExpandoObject.KeyCollectionDebugView))]
		[DebuggerDisplay("Count = {Count}")]
		private class KeyCollection : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x06000A1B RID: 2587 RVA: 0x000272E4 File Offset: 0x000254E4
			internal KeyCollection(ExpandoObject expando)
			{
				object lockObject = expando.LockObject;
				lock (lockObject)
				{
					this._expando = expando;
					this._expandoVersion = expando._data.Version;
					this._expandoCount = expando._count;
					this._expandoData = expando._data;
				}
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x00027354 File Offset: 0x00025554
			private void CheckVersion()
			{
				if (this._expando._data.Version != this._expandoVersion || this._expandoData != this._expando._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x00027387 File Offset: 0x00025587
			public void Add(string item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x00027387 File Offset: 0x00025587
			public void Clear()
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x00027390 File Offset: 0x00025590
			public bool Contains(string item)
			{
				object lockObject = this._expando.LockObject;
				bool flag2;
				lock (lockObject)
				{
					this.CheckVersion();
					flag2 = this._expando.ExpandoContainsKey(item);
				}
				return flag2;
			}

			// Token: 0x06000A20 RID: 2592 RVA: 0x000273E4 File Offset: 0x000255E4
			public void CopyTo(string[] array, int arrayIndex)
			{
				ContractUtils.RequiresNotNull(array, "array");
				ContractUtils.RequiresArrayRange<string>(array, arrayIndex, this._expandoCount, "arrayIndex", "Count");
				object lockObject = this._expando.LockObject;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (data[i] != ExpandoObject.Uninitialized)
						{
							array[arrayIndex++] = data.Class.Keys[i];
						}
					}
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00027494 File Offset: 0x00025694
			public int Count
			{
				get
				{
					this.CheckVersion();
					return this._expandoCount;
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00009F9F File Offset: 0x0000819F
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000A23 RID: 2595 RVA: 0x00027387 File Offset: 0x00025587
			public bool Remove(string item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06000A24 RID: 2596 RVA: 0x000274A2 File Offset: 0x000256A2
			public IEnumerator<string> GetEnumerator()
			{
				int i = 0;
				int j = this._expandoData.Class.Keys.Length;
				while (i < j)
				{
					this.CheckVersion();
					if (this._expandoData[i] != ExpandoObject.Uninitialized)
					{
						yield return this._expandoData.Class.Keys[i];
					}
					int num = i;
					i = num + 1;
				}
				yield break;
			}

			// Token: 0x06000A25 RID: 2597 RVA: 0x000274B1 File Offset: 0x000256B1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400031E RID: 798
			private readonly ExpandoObject _expando;

			// Token: 0x0400031F RID: 799
			private readonly int _expandoVersion;

			// Token: 0x04000320 RID: 800
			private readonly int _expandoCount;

			// Token: 0x04000321 RID: 801
			private readonly ExpandoObject.ExpandoData _expandoData;
		}

		// Token: 0x02000132 RID: 306
		private sealed class ValueCollectionDebugView
		{
		}

		// Token: 0x02000133 RID: 307
		[DebuggerTypeProxy(typeof(ExpandoObject.ValueCollectionDebugView))]
		[DebuggerDisplay("Count = {Count}")]
		private class ValueCollection : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			// Token: 0x06000A2C RID: 2604 RVA: 0x00027588 File Offset: 0x00025788
			internal ValueCollection(ExpandoObject expando)
			{
				object lockObject = expando.LockObject;
				lock (lockObject)
				{
					this._expando = expando;
					this._expandoVersion = expando._data.Version;
					this._expandoCount = expando._count;
					this._expandoData = expando._data;
				}
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x000275F8 File Offset: 0x000257F8
			private void CheckVersion()
			{
				if (this._expando._data.Version != this._expandoVersion || this._expandoData != this._expando._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x00027387 File Offset: 0x00025587
			public void Add(object item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x00027387 File Offset: 0x00025587
			public void Clear()
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06000A30 RID: 2608 RVA: 0x0002762C File Offset: 0x0002582C
			public bool Contains(object item)
			{
				object lockObject = this._expando.LockObject;
				bool flag2;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (object.Equals(data[i], item))
						{
							return true;
						}
					}
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x000276B0 File Offset: 0x000258B0
			public void CopyTo(object[] array, int arrayIndex)
			{
				ContractUtils.RequiresNotNull(array, "array");
				ContractUtils.RequiresArrayRange<object>(array, arrayIndex, this._expandoCount, "arrayIndex", "Count");
				object lockObject = this._expando.LockObject;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (data[i] != ExpandoObject.Uninitialized)
						{
							array[arrayIndex++] = data[i];
						}
					}
				}
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00027758 File Offset: 0x00025958
			public int Count
			{
				get
				{
					this.CheckVersion();
					return this._expandoCount;
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00009F9F File Offset: 0x0000819F
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x00027387 File Offset: 0x00025587
			public bool Remove(object item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x00027766 File Offset: 0x00025966
			public IEnumerator<object> GetEnumerator()
			{
				ExpandoObject.ExpandoData data = this._expando._data;
				int num;
				for (int i = 0; i < data.Class.Keys.Length; i = num + 1)
				{
					this.CheckVersion();
					object obj = data[i];
					if (obj != ExpandoObject.Uninitialized)
					{
						yield return obj;
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x00027775 File Offset: 0x00025975
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000327 RID: 807
			private readonly ExpandoObject _expando;

			// Token: 0x04000328 RID: 808
			private readonly int _expandoVersion;

			// Token: 0x04000329 RID: 809
			private readonly int _expandoCount;

			// Token: 0x0400032A RID: 810
			private readonly ExpandoObject.ExpandoData _expandoData;
		}

		// Token: 0x02000135 RID: 309
		private class MetaExpando : DynamicMetaObject
		{
			// Token: 0x06000A3D RID: 2621 RVA: 0x0002783B File Offset: 0x00025A3B
			public MetaExpando(Expression expression, ExpandoObject value)
				: base(expression, BindingRestrictions.Empty, value)
			{
			}

			// Token: 0x06000A3E RID: 2622 RVA: 0x0002784C File Offset: 0x00025A4C
			private DynamicMetaObject BindGetOrInvokeMember(DynamicMetaObjectBinder binder, string name, bool ignoreCase, DynamicMetaObject fallback, Func<DynamicMetaObject, DynamicMetaObject> fallbackInvoke)
			{
				ExpandoClass @class = this.Value.Class;
				int valueIndex = @class.GetValueIndex(name, ignoreCase, this.Value);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "value");
				Expression expression = Expression.Call(ExpandoObject.s_expandoTryGetValue, new Expression[]
				{
					this.GetLimitedSelf(),
					Expression.Constant(@class, typeof(object)),
					Utils.Constant(valueIndex),
					Expression.Constant(name),
					Utils.Constant(ignoreCase),
					parameterExpression
				});
				DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
				if (fallbackInvoke != null)
				{
					dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
				}
				dynamicMetaObject = new DynamicMetaObject(Expression.Block(new TrueReadOnlyCollection<ParameterExpression>(new ParameterExpression[] { parameterExpression }), new TrueReadOnlyCollection<Expression>(new Expression[] { Expression.Condition(expression, dynamicMetaObject.Expression, fallback.Expression, typeof(object)) })), dynamicMetaObject.Restrictions.Merge(fallback.Restrictions));
				return this.AddDynamicTestAndDefer(binder, this.Value.Class, null, dynamicMetaObject);
			}

			// Token: 0x06000A3F RID: 2623 RVA: 0x00027960 File Offset: 0x00025B60
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				return this.BindGetOrInvokeMember(binder, binder.Name, binder.IgnoreCase, binder.FallbackGetMember(this), null);
			}

			// Token: 0x06000A40 RID: 2624 RVA: 0x00027988 File Offset: 0x00025B88
			public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				return this.BindGetOrInvokeMember(binder, binder.Name, binder.IgnoreCase, binder.FallbackInvokeMember(this, args), (DynamicMetaObject value) => binder.FallbackInvoke(value, args, null));
			}

			// Token: 0x06000A41 RID: 2625 RVA: 0x000279FC File Offset: 0x00025BFC
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				ContractUtils.RequiresNotNull(value, "value");
				ExpandoClass expandoClass;
				int num;
				ExpandoClass classEnsureIndex = this.GetClassEnsureIndex(binder.Name, binder.IgnoreCase, this.Value, out expandoClass, out num);
				return this.AddDynamicTestAndDefer(binder, expandoClass, classEnsureIndex, new DynamicMetaObject(Expression.Call(ExpandoObject.s_expandoTrySetValue, new Expression[]
				{
					this.GetLimitedSelf(),
					Expression.Constant(expandoClass, typeof(object)),
					Utils.Constant(num),
					Expression.Convert(value.Expression, typeof(object)),
					Expression.Constant(binder.Name),
					Utils.Constant(binder.IgnoreCase)
				}), BindingRestrictions.Empty));
			}

			// Token: 0x06000A42 RID: 2626 RVA: 0x00027AB8 File Offset: 0x00025CB8
			public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				int valueIndex = this.Value.Class.GetValueIndex(binder.Name, binder.IgnoreCase, this.Value);
				Expression expression = Expression.Call(ExpandoObject.s_expandoTryDeleteValue, this.GetLimitedSelf(), Expression.Constant(this.Value.Class, typeof(object)), Utils.Constant(valueIndex), Expression.Constant(binder.Name), Utils.Constant(binder.IgnoreCase));
				DynamicMetaObject dynamicMetaObject = binder.FallbackDeleteMember(this);
				DynamicMetaObject dynamicMetaObject2 = new DynamicMetaObject(Expression.IfThen(Expression.Not(expression), dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
				return this.AddDynamicTestAndDefer(binder, this.Value.Class, null, dynamicMetaObject2);
			}

			// Token: 0x06000A43 RID: 2627 RVA: 0x00027B71 File Offset: 0x00025D71
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				ExpandoObject.ExpandoData expandoData = this.Value._data;
				ExpandoClass klass = expandoData.Class;
				int num;
				for (int i = 0; i < klass.Keys.Length; i = num + 1)
				{
					if (expandoData[i] != ExpandoObject.Uninitialized)
					{
						yield return klass.Keys[i];
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06000A44 RID: 2628 RVA: 0x00027B84 File Offset: 0x00025D84
			private DynamicMetaObject AddDynamicTestAndDefer(DynamicMetaObjectBinder binder, ExpandoClass klass, ExpandoClass originalClass, DynamicMetaObject succeeds)
			{
				Expression expression = succeeds.Expression;
				if (originalClass != null)
				{
					expression = Expression.Block(Expression.Call(null, ExpandoObject.s_expandoPromoteClass, this.GetLimitedSelf(), Expression.Constant(originalClass, typeof(object)), Expression.Constant(klass, typeof(object))), succeeds.Expression);
				}
				return new DynamicMetaObject(Expression.Condition(Expression.Call(null, ExpandoObject.s_expandoCheckVersion, this.GetLimitedSelf(), Expression.Constant(originalClass ?? klass, typeof(object))), expression, binder.GetUpdateExpression(expression.Type)), this.GetRestrictions().Merge(succeeds.Restrictions));
			}

			// Token: 0x06000A45 RID: 2629 RVA: 0x00027C2C File Offset: 0x00025E2C
			private ExpandoClass GetClassEnsureIndex(string name, bool caseInsensitive, ExpandoObject obj, out ExpandoClass klass, out int index)
			{
				ExpandoClass @class = this.Value.Class;
				index = @class.GetValueIndex(name, caseInsensitive, obj);
				if (index == -2)
				{
					klass = @class;
					return null;
				}
				if (index == -1)
				{
					ExpandoClass expandoClass = @class.FindNewClass(name);
					klass = expandoClass;
					index = expandoClass.GetValueIndexCaseSensitive(name);
					return @class;
				}
				klass = @class;
				return null;
			}

			// Token: 0x06000A46 RID: 2630 RVA: 0x00027C81 File Offset: 0x00025E81
			private Expression GetLimitedSelf()
			{
				if (TypeUtils.AreEquivalent(base.Expression.Type, base.LimitType))
				{
					return base.Expression;
				}
				return Expression.Convert(base.Expression, base.LimitType);
			}

			// Token: 0x06000A47 RID: 2631 RVA: 0x00027CB3 File Offset: 0x00025EB3
			private BindingRestrictions GetRestrictions()
			{
				return BindingRestrictions.GetTypeRestriction(this);
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00027CBB File Offset: 0x00025EBB
			public new ExpandoObject Value
			{
				get
				{
					return (ExpandoObject)base.Value;
				}
			}
		}

		// Token: 0x02000138 RID: 312
		private class ExpandoData
		{
			// Token: 0x170001BC RID: 444
			internal object this[int index]
			{
				get
				{
					return this._dataArray[index];
				}
				set
				{
					this._version++;
					this._dataArray[index] = value;
				}
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00027E2A File Offset: 0x0002602A
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00027E32 File Offset: 0x00026032
			internal int Length
			{
				get
				{
					return this._dataArray.Length;
				}
			}

			// Token: 0x06000A57 RID: 2647 RVA: 0x00027E3C File Offset: 0x0002603C
			private ExpandoData()
			{
				this.Class = ExpandoClass.Empty;
				this._dataArray = Array.Empty<object>();
			}

			// Token: 0x06000A58 RID: 2648 RVA: 0x00027E5A File Offset: 0x0002605A
			internal ExpandoData(ExpandoClass klass, object[] data, int version)
			{
				this.Class = klass;
				this._dataArray = data;
				this._version = version;
			}

			// Token: 0x06000A59 RID: 2649 RVA: 0x00027E78 File Offset: 0x00026078
			internal ExpandoObject.ExpandoData UpdateClass(ExpandoClass newClass)
			{
				if (this._dataArray.Length >= newClass.Keys.Length)
				{
					this[newClass.Keys.Length - 1] = ExpandoObject.Uninitialized;
					return new ExpandoObject.ExpandoData(newClass, this._dataArray, this._version);
				}
				int num = this._dataArray.Length;
				object[] array = new object[ExpandoObject.ExpandoData.GetAlignedSize(newClass.Keys.Length)];
				Array.Copy(this._dataArray, 0, array, 0, this._dataArray.Length);
				ExpandoObject.ExpandoData expandoData = new ExpandoObject.ExpandoData(newClass, array, this._version);
				expandoData[num] = ExpandoObject.Uninitialized;
				return expandoData;
			}

			// Token: 0x06000A5A RID: 2650 RVA: 0x00027F0A File Offset: 0x0002610A
			private static int GetAlignedSize(int len)
			{
				return (len + 7) & -8;
			}

			// Token: 0x04000339 RID: 825
			internal static ExpandoObject.ExpandoData Empty = new ExpandoObject.ExpandoData();

			// Token: 0x0400033A RID: 826
			internal readonly ExpandoClass Class;

			// Token: 0x0400033B RID: 827
			private readonly object[] _dataArray;

			// Token: 0x0400033C RID: 828
			private int _version;
		}
	}
}
