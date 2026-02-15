using System;
using UnityEngine;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000EC3 RID: 3779
	public class LogShowTag : LogItemBase
	{
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06006E35 RID: 28213 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006E36 RID: 28214 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowTagType type)
		{
		}

		// Token: 0x06006E37 RID: 28215 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetTargetTag()
		{
		}

		// Token: 0x06006E38 RID: 28216 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCostTag()
		{
		}

		// Token: 0x0400A908 RID: 43272
		[SerializeField]
		protected string m_Label_TagName;

		// Token: 0x0400A909 RID: 43273
		private ElementObjectManager m_EOManager_Origin;
	}
}
