using System;

namespace UnityEngine.UI
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public struct Navigation : IEquatable<Navigation>
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00010938 File Offset: 0x0000EB38
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00010940 File Offset: 0x0000EB40
		public Navigation.Mode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00010949 File Offset: 0x0000EB49
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00010951 File Offset: 0x0000EB51
		public bool wrapAround
		{
			get
			{
				return this.m_WrapAround;
			}
			set
			{
				this.m_WrapAround = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0001095A File Offset: 0x0000EB5A
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00010962 File Offset: 0x0000EB62
		public Selectable selectOnUp
		{
			get
			{
				return this.m_SelectOnUp;
			}
			set
			{
				this.m_SelectOnUp = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0001096B File Offset: 0x0000EB6B
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00010973 File Offset: 0x0000EB73
		public Selectable selectOnDown
		{
			get
			{
				return this.m_SelectOnDown;
			}
			set
			{
				this.m_SelectOnDown = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0001097C File Offset: 0x0000EB7C
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00010984 File Offset: 0x0000EB84
		public Selectable selectOnLeft
		{
			get
			{
				return this.m_SelectOnLeft;
			}
			set
			{
				this.m_SelectOnLeft = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0001098D File Offset: 0x0000EB8D
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00010995 File Offset: 0x0000EB95
		public Selectable selectOnRight
		{
			get
			{
				return this.m_SelectOnRight;
			}
			set
			{
				this.m_SelectOnRight = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000109A0 File Offset: 0x0000EBA0
		public static Navigation defaultNavigation
		{
			get
			{
				return new Navigation
				{
					m_Mode = Navigation.Mode.Automatic,
					m_WrapAround = false
				};
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000109C8 File Offset: 0x0000EBC8
		public bool Equals(Navigation other)
		{
			return this.mode == other.mode && this.selectOnUp == other.selectOnUp && this.selectOnDown == other.selectOnDown && this.selectOnLeft == other.selectOnLeft && this.selectOnRight == other.selectOnRight;
		}

		// Token: 0x040001A5 RID: 421
		[SerializeField]
		private Navigation.Mode m_Mode;

		// Token: 0x040001A6 RID: 422
		[Tooltip("Enables navigation to wrap around from last to first or first to last element. Does not work for automatic grid navigation")]
		[SerializeField]
		private bool m_WrapAround;

		// Token: 0x040001A7 RID: 423
		[SerializeField]
		private Selectable m_SelectOnUp;

		// Token: 0x040001A8 RID: 424
		[SerializeField]
		private Selectable m_SelectOnDown;

		// Token: 0x040001A9 RID: 425
		[SerializeField]
		private Selectable m_SelectOnLeft;

		// Token: 0x040001AA RID: 426
		[SerializeField]
		private Selectable m_SelectOnRight;

		// Token: 0x0200005D RID: 93
		[Flags]
		public enum Mode
		{
			// Token: 0x040001AC RID: 428
			None = 0,
			// Token: 0x040001AD RID: 429
			Horizontal = 1,
			// Token: 0x040001AE RID: 430
			Vertical = 2,
			// Token: 0x040001AF RID: 431
			Automatic = 3,
			// Token: 0x040001B0 RID: 432
			Explicit = 4
		}
	}
}
