using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000537 RID: 1335
	public class PropertyContainer : MonoBehaviour
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerable<string> Keys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerable<string> Values
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000029CC File Offset: 0x00000BCC
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x0000216A File Offset: 0x0000036A
		public string Item
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ContainsKey(string key)
		{
			return false;
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000F1C76 File Offset: 0x000EFE76
		public bool TryGetValue(string key, out string value)
		{
			value = null;
			return false;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetValueOrDefault(string key, float defaultVal = 0f)
		{
			return 0f;
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetValueOrDefault(string key, int defaultVal = 0)
		{
			return 0;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetValueOrDefault(string key, string defaultVal = null)
		{
			return null;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return null;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
		{
			return null;
		}

		// Token: 0x040029CF RID: 10703
		public List<PropertyContainer.PropertyInfo> propertyList;

		// Token: 0x02000538 RID: 1336
		[Serializable]
		public class PropertyInfo
		{
			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06002ABD RID: 10941 RVA: 0x000F1C7C File Offset: 0x000EFE7C
			public KeyValuePair<string, string> keyValuePair
			{
				get
				{
					return default(KeyValuePair<string, string>);
				}
			}

			// Token: 0x06002ABE RID: 10942 RVA: 0x0000216A File Offset: 0x0000036A
			public PropertyContainer.PropertyInfo Copy()
			{
				return null;
			}

			// Token: 0x040029D0 RID: 10704
			public string label;

			// Token: 0x040029D1 RID: 10705
			[Multiline]
			public string property;
		}
	}
}
