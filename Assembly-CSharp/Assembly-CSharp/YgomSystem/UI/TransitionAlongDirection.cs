using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x0200060C RID: 1548
	public class TransitionAlongDirection : MonoBehaviour
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x0000216A File Offset: 0x0000036A
		public TransitionAlongDirection.Setting setting
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x0000216D File Offset: 0x0000036D
		public void ApplyTransition()
		{
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelectInput(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnInputUp()
		{
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnInputDown()
		{
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnInputLeft()
		{
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnInputRight()
		{
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool TrySelectNearItem(Vector2 dir)
		{
			return false;
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool TrySelectNearItem(SelectionItem selectionItem, Vector2 dir)
		{
			return false;
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryProvisinalSelectItem(SelectionItem item)
		{
			return false;
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryProvisinalSelectSelector(Selector selector)
		{
			return false;
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryProvisinalSelectClustor(SelectorCluster cluster)
		{
			return false;
		}

		// Token: 0x04002DD3 RID: 11731
		[SerializeField]
		private TransitionAlongDirection.Setting m_Setting;

		// Token: 0x04002DD4 RID: 11732
		protected SelectionItem m_SelectionItem;

		// Token: 0x04002DD5 RID: 11733
		private TransitionAlongDirection.NearItemSearcher m_NearItemSearcher;

		// Token: 0x0200060D RID: 1549
		[Serializable]
		public class Setting
		{
			// Token: 0x170002EF RID: 751
			// (get) Token: 0x0600316C RID: 12652 RVA: 0x0000216A File Offset: 0x0000036A
			public List<string> reserveGroup
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x0600316D RID: 12653 RVA: 0x0000216A File Offset: 0x0000036A
			public List<string> ignoreGroup
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x0600316E RID: 12654 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool containGoThrough
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x0600316F RID: 12655 RVA: 0x0000216A File Offset: 0x0000036A
			public TransitionAlongDirection.SelectTarget[] selectTargetOrder
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06003170 RID: 12656 RVA: 0x000029C5 File Offset: 0x00000BC5
			public float selectionAngle
			{
				get
				{
					return 0f;
				}
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06003171 RID: 12657 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool maskSelectorRect
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003172 RID: 12658 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(TransitionAlongDirection.Setting other)
			{
			}

			// Token: 0x06003173 RID: 12659 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsDirection(PadInputDirection direction)
			{
				return false;
			}

			// Token: 0x06003174 RID: 12660 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDirection(PadInputDirection direction)
			{
			}

			// Token: 0x06003175 RID: 12661 RVA: 0x0000216D File Offset: 0x0000036D
			public void UnsetDirection(PadInputDirection direction)
			{
			}

			// Token: 0x04002DD6 RID: 11734
			[SerializeField]
			[EnumFlags]
			protected TransitionAlongDirection.DirectionFlag m_Directions;

			// Token: 0x04002DD7 RID: 11735
			[SerializeField]
			private List<string> m_ReserveGroup;

			// Token: 0x04002DD8 RID: 11736
			[SerializeField]
			private List<string> m_IgnoreGroup;

			// Token: 0x04002DD9 RID: 11737
			[SerializeField]
			private bool m_ContainGoThrough;

			// Token: 0x04002DDA RID: 11738
			[SerializeField]
			private bool m_MaskSelectorRect;

			// Token: 0x04002DDB RID: 11739
			[SerializeField]
			public TransitionAlongDirection.SelectTarget[] m_SelectTargetOrder;

			// Token: 0x04002DDC RID: 11740
			[SerializeField]
			private float m_SelectionAngle;
		}

		// Token: 0x0200060E RID: 1550
		public enum DirectionFlag
		{
			// Token: 0x04002DDE RID: 11742
			Up = 1,
			// Token: 0x04002DDF RID: 11743
			Down,
			// Token: 0x04002DE0 RID: 11744
			Left = 4,
			// Token: 0x04002DE1 RID: 11745
			Right = 8
		}

		// Token: 0x0200060F RID: 1551
		public enum SelectTarget
		{
			// Token: 0x04002DE3 RID: 11747
			Item,
			// Token: 0x04002DE4 RID: 11748
			Selector,
			// Token: 0x04002DE5 RID: 11749
			Clustor
		}

		// Token: 0x02000610 RID: 1552
		private class NearItemSearcher
		{
			// Token: 0x06003177 RID: 12663 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x06003178 RID: 12664 RVA: 0x0000216A File Offset: 0x0000036A
			public List<SelectionItem> Search(SelectionItem fromItem, TransitionAlongDirection.Setting setting, Vector2 dir)
			{
				return null;
			}

			// Token: 0x04002DE6 RID: 11750
			private List<SelectionItem> m_SearchItemList;

			// Token: 0x04002DE7 RID: 11751
			private Dictionary<SelectionItem, float> m_SearchDistanceMap;
		}
	}
}
