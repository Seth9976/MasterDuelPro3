using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B13 RID: 2835
	public class BindingComponentsJson : MonoBehaviour, IBindingModifiyByArgsHandler
	{
		// Token: 0x06005249 RID: 21065 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnPostModifiyByArgs(Dictionary<string, object> args)
		{
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x0000216A File Offset: 0x0000036A
		private string GenerateJson(string path, object value)
		{
			return null;
		}

		// Token: 0x0400909D RID: 37021
		[SerializeField]
		private List<BindingComponentsJson.BindingData> m_BindingDatas;

		// Token: 0x02000B14 RID: 2836
		[Serializable]
		public class BindingData
		{
			// Token: 0x0400909E RID: 37022
			public string label;

			// Token: 0x0400909F RID: 37023
			public MonoBehaviour component;

			// Token: 0x040090A0 RID: 37024
			public string writePath;
		}
	}
}
