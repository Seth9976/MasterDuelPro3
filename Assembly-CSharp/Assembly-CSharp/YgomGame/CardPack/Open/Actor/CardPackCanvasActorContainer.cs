using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomGame.HeaderFooter;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.CardPack.Open.Actor
{
	// Token: 0x020010DC RID: 4316
	public class CardPackCanvasActorContainer : ActorContainerBase<CardPackCanvasActorContainer>
	{
		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600801A RID: 32794 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton touchArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600801B RID: 32795 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton openAllCardButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001045 RID: 4165
		// (set) Token: 0x0600801C RID: 32796 RVA: 0x0000216D File Offset: 0x0000036D
		public bool skipButtonAble
		{
			set
			{
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x0600801D RID: 32797 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600801E RID: 32798 RVA: 0x0000216D File Offset: 0x0000036D
		public TMP_Text progressText
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x0600801F RID: 32799 RVA: 0x0000216A File Offset: 0x0000036A
		public OutGameFooter footer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06008020 RID: 32800 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008021 RID: 32801 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onDownTouchAreaEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06008022 RID: 32802 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008023 RID: 32803 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onUpTouchAreaEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06008024 RID: 32804 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008025 RID: 32805 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onClickTouchAreaEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06008026 RID: 32806 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008027 RID: 32807 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onInputAcceptEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06008028 RID: 32808 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06008029 RID: 32809 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onInputAcceptKeyEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x0600802A RID: 32810 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600802B RID: 32811 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onInputSub2KeyEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x0600802C RID: 32812 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600802D RID: 32813 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<SelectionItem.DragStatus, Vector2> onDragEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x0600802E RID: 32814 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600802F RID: 32815 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onInputSkipEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06008030 RID: 32816 RVA: 0x0000216A File Offset: 0x0000036A
		public static CardPackCanvasActorContainer Create(ElementObjectManager eom)
		{
			return null;
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CollectComponents()
		{
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x0000216D File Offset: 0x0000036D
		public void AssignFooter(OutGameFooter footer)
		{
		}

		// Token: 0x06008033 RID: 32819 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveParent(Transform newParent)
		{
		}

		// Token: 0x06008034 RID: 32820 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabelToOpen()
		{
		}

		// Token: 0x06008035 RID: 32821 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabelToNext()
		{
		}

		// Token: 0x06008036 RID: 32822 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabelToFinish()
		{
		}

		// Token: 0x06008037 RID: 32823 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDownTouchArea()
		{
		}

		// Token: 0x06008038 RID: 32824 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpTouchArea()
		{
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickTouchArea()
		{
		}

		// Token: 0x0600803A RID: 32826 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAcceptKey()
		{
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputSub2Key()
		{
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSkipButton()
		{
		}

		// Token: 0x0400B8A2 RID: 47266
		private readonly string k_ELabelTouchArea;

		// Token: 0x0400B8A3 RID: 47267
		private readonly string k_ELabelOpenLabel;

		// Token: 0x0400B8A4 RID: 47268
		private readonly string k_ELabelOpenAllCardButton;

		// Token: 0x0400B8A5 RID: 47269
		private readonly string k_ELabelNextLabel;

		// Token: 0x0400B8A6 RID: 47270
		private readonly string k_ELabelNextButton;

		// Token: 0x0400B8A7 RID: 47271
		private readonly string k_ELabelFinishLabel;

		// Token: 0x0400B8A8 RID: 47272
		private readonly string k_ELabelFinishButton;

		// Token: 0x0400B8A9 RID: 47273
		private readonly string k_ELabelSkipButton;

		// Token: 0x0400B8AA RID: 47274
		private readonly string k_ELabelProgressText;

		// Token: 0x0400B8AB RID: 47275
		private OutGameFooter m_Footer;

		// Token: 0x0400B8AC RID: 47276
		private SelectionButton m_TouchArea;

		// Token: 0x0400B8AD RID: 47277
		private SelectionButton m_OpenAllCardButton;

		// Token: 0x0400B8AE RID: 47278
		private GameObject m_OpenLabel;

		// Token: 0x0400B8AF RID: 47279
		private GameObject m_NextLabel;

		// Token: 0x0400B8B0 RID: 47280
		private GameObject m_FinishLabel;

		// Token: 0x0400B8B1 RID: 47281
		private SelectionButton m_SkipButton;
	}
}
