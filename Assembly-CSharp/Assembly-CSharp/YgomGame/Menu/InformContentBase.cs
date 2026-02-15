using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A96 RID: 2710
	public abstract class InformContentBase : MonoBehaviour
	{
		// Token: 0x06004F0F RID: 20239 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void OnPush()
		{
		}

		// Token: 0x04008CD5 RID: 36053
		public Dictionary<string, object> Args;
	}
}
