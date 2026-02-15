using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000070 RID: 112
	[MovedFrom(false, "UnityEngine.Experimental.Rendering.Universal", "com.unity.render-pipelines.universal", null)]
	public abstract class ShadowCasterGroup2D : MonoBehaviour
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0001570C File Offset: 0x0001390C
		internal virtual void CacheValues()
		{
			if (this.m_ShadowCasters != null)
			{
				for (int i = 0; i < this.m_ShadowCasters.Count; i++)
				{
					this.m_ShadowCasters[i].CacheValues();
				}
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00015748 File Offset: 0x00013948
		public List<ShadowCaster2D> GetShadowCasters()
		{
			return this.m_ShadowCasters;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00015750 File Offset: 0x00013950
		public int GetShadowGroup()
		{
			return this.m_ShadowGroup;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00015758 File Offset: 0x00013958
		public void RegisterShadowCaster2D(ShadowCaster2D shadowCaster2D)
		{
			if (this.m_ShadowCasters == null)
			{
				this.m_ShadowCasters = new List<ShadowCaster2D>();
			}
			int insertAtIndex = 0;
			while (insertAtIndex < this.m_ShadowCasters.Count && shadowCaster2D.m_Priority < this.m_ShadowCasters[insertAtIndex].m_Priority)
			{
				insertAtIndex++;
			}
			this.m_ShadowCasters.Insert(insertAtIndex, shadowCaster2D);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000157B6 File Offset: 0x000139B6
		public void UnregisterShadowCaster2D(ShadowCaster2D shadowCaster2D)
		{
			if (this.m_ShadowCasters != null)
			{
				this.m_ShadowCasters.Remove(shadowCaster2D);
			}
		}

		// Token: 0x0400028D RID: 653
		[SerializeField]
		internal int m_ShadowGroup;

		// Token: 0x0400028E RID: 654
		[SerializeField]
		internal int m_Priority;

		// Token: 0x0400028F RID: 655
		private List<ShadowCaster2D> m_ShadowCasters;
	}
}
