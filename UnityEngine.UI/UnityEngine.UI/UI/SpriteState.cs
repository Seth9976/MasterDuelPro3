using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	public struct SpriteState : IEquatable<SpriteState>
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001509A File Offset: 0x0001329A
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000150A2 File Offset: 0x000132A2
		public Sprite highlightedSprite
		{
			get
			{
				return this.m_HighlightedSprite;
			}
			set
			{
				this.m_HighlightedSprite = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x000150AB File Offset: 0x000132AB
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x000150B3 File Offset: 0x000132B3
		public Sprite pressedSprite
		{
			get
			{
				return this.m_PressedSprite;
			}
			set
			{
				this.m_PressedSprite = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000150BC File Offset: 0x000132BC
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x000150C4 File Offset: 0x000132C4
		public Sprite selectedSprite
		{
			get
			{
				return this.m_SelectedSprite;
			}
			set
			{
				this.m_SelectedSprite = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000150CD File Offset: 0x000132CD
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x000150D5 File Offset: 0x000132D5
		public Sprite disabledSprite
		{
			get
			{
				return this.m_DisabledSprite;
			}
			set
			{
				this.m_DisabledSprite = value;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000150E0 File Offset: 0x000132E0
		public bool Equals(SpriteState other)
		{
			return this.highlightedSprite == other.highlightedSprite && this.pressedSprite == other.pressedSprite && this.selectedSprite == other.selectedSprite && this.disabledSprite == other.disabledSprite;
		}

		// Token: 0x04000238 RID: 568
		[SerializeField]
		private Sprite m_HighlightedSprite;

		// Token: 0x04000239 RID: 569
		[SerializeField]
		private Sprite m_PressedSprite;

		// Token: 0x0400023A RID: 570
		[FormerlySerializedAs("m_HighlightedSprite")]
		[SerializeField]
		private Sprite m_SelectedSprite;

		// Token: 0x0400023B RID: 571
		[SerializeField]
		private Sprite m_DisabledSprite;
	}
}
