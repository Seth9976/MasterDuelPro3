using System;
using System.Collections.Generic;

namespace System.Dynamic
{
	// Token: 0x0200012D RID: 301
	internal class ExpandoClass
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x000269AA File Offset: 0x00024BAA
		internal ExpandoClass()
		{
			this._hashCode = 6551;
			this._keys = Array.Empty<string>();
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x000269C8 File Offset: 0x00024BC8
		internal ExpandoClass(string[] keys, int hashCode)
		{
			this._hashCode = hashCode;
			this._keys = keys;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000269E0 File Offset: 0x00024BE0
		internal ExpandoClass FindNewClass(string newKey)
		{
			int num = this._hashCode ^ newKey.GetHashCode();
			ExpandoClass expandoClass3;
			lock (this)
			{
				List<WeakReference> transitionList = this.GetTransitionList(num);
				for (int i = 0; i < transitionList.Count; i++)
				{
					ExpandoClass expandoClass = transitionList[i].Target as ExpandoClass;
					if (expandoClass == null)
					{
						transitionList.RemoveAt(i);
						i--;
					}
					else if (string.Equals(expandoClass._keys[expandoClass._keys.Length - 1], newKey, StringComparison.Ordinal))
					{
						return expandoClass;
					}
				}
				string[] array = new string[this._keys.Length + 1];
				Array.Copy(this._keys, 0, array, 0, this._keys.Length);
				array[this._keys.Length] = newKey;
				ExpandoClass expandoClass2 = new ExpandoClass(array, num);
				transitionList.Add(new WeakReference(expandoClass2));
				expandoClass3 = expandoClass2;
			}
			return expandoClass3;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00026ADC File Offset: 0x00024CDC
		private List<WeakReference> GetTransitionList(int hashCode)
		{
			if (this._transitions == null)
			{
				this._transitions = new Dictionary<int, List<WeakReference>>();
			}
			List<WeakReference> list;
			if (!this._transitions.TryGetValue(hashCode, out list))
			{
				list = (this._transitions[hashCode] = new List<WeakReference>());
			}
			return list;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00026B20 File Offset: 0x00024D20
		internal int GetValueIndex(string name, bool caseInsensitive, ExpandoObject obj)
		{
			if (caseInsensitive)
			{
				return this.GetValueIndexCaseInsensitive(name, obj);
			}
			return this.GetValueIndexCaseSensitive(name);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00026B38 File Offset: 0x00024D38
		internal int GetValueIndexCaseSensitive(string name)
		{
			for (int i = 0; i < this._keys.Length; i++)
			{
				if (string.Equals(this._keys[i], name, StringComparison.Ordinal))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00026B6C File Offset: 0x00024D6C
		private int GetValueIndexCaseInsensitive(string name, ExpandoObject obj)
		{
			int num = -1;
			object lockObject = obj.LockObject;
			lock (lockObject)
			{
				for (int i = this._keys.Length - 1; i >= 0; i--)
				{
					if (string.Equals(this._keys[i], name, StringComparison.OrdinalIgnoreCase) && !obj.IsDeletedMember(i))
					{
						if (num != -1)
						{
							return -2;
						}
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00026BEC File Offset: 0x00024DEC
		internal string[] Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x04000310 RID: 784
		private readonly string[] _keys;

		// Token: 0x04000311 RID: 785
		private readonly int _hashCode;

		// Token: 0x04000312 RID: 786
		private Dictionary<int, List<WeakReference>> _transitions;

		// Token: 0x04000313 RID: 787
		internal static readonly ExpandoClass Empty = new ExpandoClass();
	}
}
