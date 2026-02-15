using System;
using UnityEngine;

namespace Willow
{
	// Token: 0x0200154E RID: 5454
	[CreateAssetMenu]
	public class NamedAssetContainer : AssetContainer
	{
		// Token: 0x06009E35 RID: 40501 RVA: 0x0019B52C File Offset: 0x0019972C
		private void OnEnable()
		{
			for (int i = 0; i < this.m_keys.Length; i++)
			{
				this.m_table.Add(this.m_keys[i], this.m_container[i]);
			}
		}

		// Token: 0x06009E36 RID: 40502 RVA: 0x0019B567 File Offset: 0x00199767
		public string[] AllNamedAssetNames()
		{
			return this.m_keys;
		}

		// Token: 0x06009E37 RID: 40503 RVA: 0x0000216D File Offset: 0x0000036D
		public void Set<T>(string name, T value) where T : global::UnityEngine.Object
		{
		}

		// Token: 0x0400DDC6 RID: 56774
		[SerializeField]
		private string[] m_keys;
	}
}
