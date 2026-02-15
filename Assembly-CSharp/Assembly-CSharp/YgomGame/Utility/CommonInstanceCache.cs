using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Utility
{
	// Token: 0x0200081E RID: 2078
	public class CommonInstanceCache : MonoBehaviour
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x0000216A File Offset: 0x0000036A
		private static CommonInstanceCache instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x000F46C4 File Offset: 0x000F28C4
		public static T GetCache<T>(string key) where T : class
		{
			return default(T);
		}

		// Token: 0x06004023 RID: 16419 RVA: 0x0000216A File Offset: 0x0000036A
		public static object GetCache(string key)
		{
			return null;
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AssignCache(string key, object value)
		{
		}

		// Token: 0x0400393E RID: 14654
		internal const string k_HelpMapping = "HelpMapping";

		// Token: 0x0400393F RID: 14655
		private static CommonInstanceCache s_Instance;

		// Token: 0x04003940 RID: 14656
		private Dictionary<string, object> m_CacheMap;
	}
}
