using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomSystem.Utility
{
	// Token: 0x02000559 RID: 1369
	public class TweenRunner : MonoBehaviour
	{
		// Token: 0x06002BCF RID: 11215 RVA: 0x0000216D File Offset: 0x0000036D
		public void TweenCollect(bool includeChildren = false)
		{
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0000216A File Offset: 0x0000036A
		public List<Tween> TryGetTweens(string label)
		{
			return null;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0000216D File Offset: 0x0000036D
		public void Evaluate(string playLabel)
		{
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayOverride(string playLabel)
		{
		}

		// Token: 0x04002A5A RID: 10842
		[SerializeField]
		private List<TweenRunner.TweenGroup> m_TweenGroups;

		// Token: 0x04002A5B RID: 10843
		[SerializeField]
		private string m_PlayLabel;

		// Token: 0x0200055A RID: 1370
		[Serializable]
		public class TweenGroup
		{
			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06002BD5 RID: 11221 RVA: 0x0000216D File Offset: 0x0000036D
			public string label
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x0000216A File Offset: 0x0000036A
			public List<Tween> tweens
			{
				get
				{
					return null;
				}
			}

			// Token: 0x04002A5C RID: 10844
			[SerializeField]
			private string m_Label;

			// Token: 0x04002A5D RID: 10845
			[SerializeField]
			private List<Tween> m_Tweens;
		}
	}
}
