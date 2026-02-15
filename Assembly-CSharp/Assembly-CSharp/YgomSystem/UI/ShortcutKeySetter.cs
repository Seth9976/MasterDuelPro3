using System;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.Utility;

namespace YgomSystem.UI
{
	// Token: 0x020005FB RID: 1531
	public class ShortcutKeySetter : MonoBehaviour
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLoading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(Action onComplete = null)
		{
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ElementObjectManager eom, Action onComplete = null)
		{
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SetShortcut(ElementObjectManager eom, ShortcutKeySetter.Setting setting, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SetShortcut(ElementObjectManager eom, string buttonLabel, string iconLabel, SelectorManager.KeyType keyType, SelectorManager.KeyType keyTypeSub = SelectorManager.KeyType.None, GamePadIconUtil.Variation iconVariation = GamePadIconUtil.Variation.Var00, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SetShortcutKey(ElementObjectManager eom, string buttonLabel, SelectorManager.KeyType keyType, SelectorManager.KeyType keyTypeSub = SelectorManager.KeyType.None)
		{
			return false;
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SetShortcutIcon(ElementObjectManager eom, string iconLabel, SelectorManager.KeyType keyType, GamePadIconUtil.Variation variation = GamePadIconUtil.Variation.Var00, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SetShortcutIcon(ElementObjectManager eom, string iconLabelMain, string iconLabelSub, string iconLabelPlus, SelectorManager.KeyType keyTypeMain, SelectorManager.KeyType keyTypeSub, GamePadIconUtil.Variation variationMain = GamePadIconUtil.Variation.Var00, GamePadIconUtil.Variation variationSub = GamePadIconUtil.Variation.Var00, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SetMouseCancelShortcutKey(ElementObjectManager eom, string buttonLabel)
		{
			return false;
		}

		// Token: 0x04002D6D RID: 11629
		[SerializeField]
		private string m_MouseCancelLabel;

		// Token: 0x04002D6E RID: 11630
		[SerializeField]
		private string[] m_MouseCancelAdditionalLabels;

		// Token: 0x04002D6F RID: 11631
		[SerializeField]
		private ShortcutKeySetter.Setting[] m_Settings;

		// Token: 0x04002D70 RID: 11632
		private int m_LoadingCount;

		// Token: 0x020005FC RID: 1532
		[Serializable]
		public class Setting
		{
			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x060030FE RID: 12542 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060030FF RID: 12543 RVA: 0x0000216D File Offset: 0x0000036D
			public string buttonLabel
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x170002E1 RID: 737
			// (get) Token: 0x06003100 RID: 12544 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003101 RID: 12545 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectorManager.KeyType keyType
			{
				get
				{
					return SelectorManager.KeyType.None;
				}
				set
				{
				}
			}

			// Token: 0x170002E2 RID: 738
			// (get) Token: 0x06003102 RID: 12546 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003103 RID: 12547 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectorManager.KeyType keyTypeSub
			{
				get
				{
					return SelectorManager.KeyType.None;
				}
				set
				{
				}
			}

			// Token: 0x170002E3 RID: 739
			// (get) Token: 0x06003104 RID: 12548 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06003105 RID: 12549 RVA: 0x0000216D File Offset: 0x0000036D
			public string iconLabel
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06003106 RID: 12550 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06003107 RID: 12551 RVA: 0x0000216D File Offset: 0x0000036D
			public GamePadIconUtil.Variation iconVariation
			{
				get
				{
					return GamePadIconUtil.Variation.Var00;
				}
				set
				{
				}
			}

			// Token: 0x04002D71 RID: 11633
			[SerializeField]
			private string m_ButtonLabel;

			// Token: 0x04002D72 RID: 11634
			[SerializeField]
			private SelectorManager.KeyType m_KeyType;

			// Token: 0x04002D73 RID: 11635
			[SerializeField]
			private SelectorManager.KeyType m_KeyTypeSub;

			// Token: 0x04002D74 RID: 11636
			[SerializeField]
			private string m_IconLabel;

			// Token: 0x04002D75 RID: 11637
			[SerializeField]
			private GamePadIconUtil.Variation m_IconVariation;
		}

		// Token: 0x020005FD RID: 1533
		[Serializable]
		public class MouseSetting
		{
			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06003109 RID: 12553 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600310A RID: 12554 RVA: 0x0000216D File Offset: 0x0000036D
			public string buttonLabel
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x0600310B RID: 12555 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x0600310C RID: 12556 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectorManager.MouseType mouseType
			{
				get
				{
					return SelectorManager.MouseType.None;
				}
				set
				{
				}
			}

			// Token: 0x04002D76 RID: 11638
			[SerializeField]
			private string m_ButtonLabel;

			// Token: 0x04002D77 RID: 11639
			[SerializeField]
			private SelectorManager.MouseType m_MouseType;
		}
	}
}
