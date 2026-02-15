using System;
using System.Collections.Generic;
using UnityEngine;

namespace Willow
{
	// Token: 0x02001546 RID: 5446
	[CreateAssetMenu]
	public class AssetContainer : ScriptableObject
	{
		// Token: 0x06009DD2 RID: 40402 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06009DD3 RID: 40403 RVA: 0x0019B42E File Offset: 0x0019962E
		public int ObjectCount()
		{
			return this.m_container.Length;
		}

		// Token: 0x06009DD4 RID: 40404 RVA: 0x0019B438 File Offset: 0x00199638
		public string[] AllAssetNames()
		{
			List<string> list = new List<string>();
			foreach (string key in this.m_table.Keys)
			{
				list.Add(key);
			}
			return list.ToArray();
		}

		// Token: 0x06009DD5 RID: 40405 RVA: 0x0019B49C File Offset: 0x0019969C
		public T Get<T>(string name) where T : global::UnityEngine.Object
		{
			global::UnityEngine.Object asset;
			if (this.m_table.TryGetValue(name, out asset))
			{
				return asset as T;
			}
			return default(T);
		}

		// Token: 0x06009DD6 RID: 40406 RVA: 0x0019B4D0 File Offset: 0x001996D0
		public bool TryGet<T>(string name, out T asset) where T : global::UnityEngine.Object
		{
			global::UnityEngine.Object obj;
			if (this.m_table.TryGetValue(name, out obj))
			{
				asset = obj as T;
				return true;
			}
			asset = default(T);
			return false;
		}

		// Token: 0x0400DD9F RID: 56735
		[SerializeField]
		protected global::UnityEngine.Object[] m_container;

		// Token: 0x0400DDA0 RID: 56736
		protected readonly Dictionary<string, global::UnityEngine.Object> m_table = new Dictionary<string, global::UnityEngine.Object>();
	}
}
