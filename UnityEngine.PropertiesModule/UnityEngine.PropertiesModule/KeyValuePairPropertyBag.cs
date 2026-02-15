using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200003C RID: 60
	public class KeyValuePairPropertyBag<TKey, TValue> : PropertyBag<KeyValuePair<TKey, TValue>>, INamedProperties<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00004B38 File Offset: 0x00002D38
		public override PropertyCollection<KeyValuePair<TKey, TValue>> GetProperties()
		{
			return new PropertyCollection<KeyValuePair<TKey, TValue>>(KeyValuePairPropertyBag<TKey, TValue>.GetPropertiesEnumerable());
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004B54 File Offset: 0x00002D54
		public override PropertyCollection<KeyValuePair<TKey, TValue>> GetProperties(ref KeyValuePair<TKey, TValue> container)
		{
			return new PropertyCollection<KeyValuePair<TKey, TValue>>(KeyValuePairPropertyBag<TKey, TValue>.GetPropertiesEnumerable());
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004B70 File Offset: 0x00002D70
		private static IEnumerable<IProperty<KeyValuePair<TKey, TValue>>> GetPropertiesEnumerable()
		{
			yield return KeyValuePairPropertyBag<TKey, TValue>.s_KeyProperty;
			yield return KeyValuePairPropertyBag<TKey, TValue>.s_ValueProperty;
			yield break;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004B7C File Offset: 0x00002D7C
		public bool TryGetProperty(ref KeyValuePair<TKey, TValue> container, string name, out IProperty<KeyValuePair<TKey, TValue>> property)
		{
			bool flag = name == "Key";
			bool flag2;
			if (flag)
			{
				property = KeyValuePairPropertyBag<TKey, TValue>.s_KeyProperty;
				flag2 = true;
			}
			else
			{
				bool flag3 = name == "Value";
				if (flag3)
				{
					property = KeyValuePairPropertyBag<TKey, TValue>.s_ValueProperty;
					flag2 = true;
				}
				else
				{
					property = null;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x04000059 RID: 89
		private static readonly DelegateProperty<KeyValuePair<TKey, TValue>, TKey> s_KeyProperty = new DelegateProperty<KeyValuePair<TKey, TValue>, TKey>("Key", delegate(ref KeyValuePair<TKey, TValue> container)
		{
			return container.Key;
		}, null);

		// Token: 0x0400005A RID: 90
		private static readonly DelegateProperty<KeyValuePair<TKey, TValue>, TValue> s_ValueProperty = new DelegateProperty<KeyValuePair<TKey, TValue>, TValue>("Value", delegate(ref KeyValuePair<TKey, TValue> container)
		{
			return container.Value;
		}, null);
	}
}
