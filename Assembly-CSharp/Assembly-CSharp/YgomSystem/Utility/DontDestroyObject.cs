using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x0200051B RID: 1307
	public class DontDestroyObject : MonoBehaviour
	{
		// Token: 0x06002A06 RID: 10758 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetObjectPath(Transform objtrns)
		{
			return null;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x04002972 RID: 10610
		private static Dictionary<string, GameObject> path2obj;
	}
}
