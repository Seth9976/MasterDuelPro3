using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI.InfinityScroll
{
	// Token: 0x02000685 RID: 1669
	[Serializable]
	public class EntityPoolSettings
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000029CC File Offset: 0x00000BCC
		public GridLayoutGroup.Axis scrollAxis
		{
			get
			{
				return GridLayoutGroup.Axis.Horizontal;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000029CC File Offset: 0x00000BCC
		public EntityPoolSettings.Alignment alignment
		{
			get
			{
				return EntityPoolSettings.Alignment.Begin;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003412 RID: 13330 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInstaintiateAllTemplates
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x04002FD5 RID: 12245
		[SerializeField]
		private GridLayoutGroup.Axis m_ScrollAxis;

		// Token: 0x04002FD6 RID: 12246
		[SerializeField]
		private EntityPoolSettings.Alignment m_Alignment;

		// Token: 0x04002FD7 RID: 12247
		[SerializeField]
		private bool m_InstantiateAllTemplates;

		// Token: 0x02000686 RID: 1670
		public enum Alignment
		{
			// Token: 0x04002FD9 RID: 12249
			Begin,
			// Token: 0x04002FDA RID: 12250
			Center,
			// Token: 0x04002FDB RID: 12251
			End
		}
	}
}
