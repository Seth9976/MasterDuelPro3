using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B0 RID: 432
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class RuntimeInitializeOnLoadMethodAttribute : PreserveAttribute
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x000243FC File Offset: 0x000225FC
		public RuntimeInitializeOnLoadMethodAttribute()
		{
			this.loadType = RuntimeInitializeLoadType.AfterSceneLoad;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0002440E File Offset: 0x0002260E
		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType)
		{
			this.loadType = loadType;
		}

		// Token: 0x170002A7 RID: 679
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x00024420 File Offset: 0x00022620
		private RuntimeInitializeLoadType loadType
		{
			set
			{
				this.m_LoadType = value;
			}
		}

		// Token: 0x04000681 RID: 1665
		private RuntimeInitializeLoadType m_LoadType;
	}
}
