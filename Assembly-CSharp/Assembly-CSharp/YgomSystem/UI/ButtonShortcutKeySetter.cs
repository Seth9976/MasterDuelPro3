using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000575 RID: 1397
	public class ButtonShortcutKeySetter : MonoBehaviour
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x0000216A File Offset: 0x0000036A
		private SelectionButton button
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnSelectedKeyUp()
		{
			return false;
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedMounseUp(bool pointerEnter)
		{
		}

		// Token: 0x04002AB9 RID: 10937
		[SerializeField]
		private ButtonShortcutKeySetter.Setting[] m_Settings;

		// Token: 0x04002ABA RID: 10938
		[SerializeField]
		private ButtonShortcutKeySetter.MouseSetting[] m_MouseSettings;

		// Token: 0x04002ABB RID: 10939
		private SelectionButton m_ButtonCache;

		// Token: 0x02000576 RID: 1398
		public enum InputType
		{
			// Token: 0x04002ABD RID: 10941
			Always,
			// Token: 0x04002ABE RID: 10942
			OnSelected
		}

		// Token: 0x02000577 RID: 1399
		[Serializable]
		public class Setting
		{
			// Token: 0x17000224 RID: 548
			// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06002C7F RID: 11391 RVA: 0x0000216D File Offset: 0x0000036D
			public ButtonShortcutKeySetter.InputType inputType
			{
				get
				{
					return ButtonShortcutKeySetter.InputType.Always;
				}
				set
				{
				}
			}

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06002C81 RID: 11393 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06002C83 RID: 11395 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x04002ABF RID: 10943
			[SerializeField]
			private ButtonShortcutKeySetter.InputType m_InputType;

			// Token: 0x04002AC0 RID: 10944
			[SerializeField]
			private SelectorManager.KeyType m_KeyType;

			// Token: 0x04002AC1 RID: 10945
			[SerializeField]
			private SelectorManager.KeyType m_KeyTypeSub;
		}

		// Token: 0x02000578 RID: 1400
		[Serializable]
		public class MouseSetting
		{
			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06002C85 RID: 11397 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06002C86 RID: 11398 RVA: 0x0000216D File Offset: 0x0000036D
			public ButtonShortcutKeySetter.InputType inputType
			{
				get
				{
					return ButtonShortcutKeySetter.InputType.Always;
				}
				set
				{
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06002C87 RID: 11399 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06002C88 RID: 11400 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x04002AC2 RID: 10946
			[SerializeField]
			private ButtonShortcutKeySetter.InputType m_InputType;

			// Token: 0x04002AC3 RID: 10947
			[SerializeField]
			private SelectorManager.MouseType m_MouseType;
		}
	}
}
