using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomGame.Dialog.CommonDialog;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Menu
{
	// Token: 0x02000AA4 RID: 2724
	public class LoginBonusMapWidet : ElementWidgetBase
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004F55 RID: 20309 RVA: 0x0000216D File Offset: 0x0000036D
		public int progress
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004F57 RID: 20311 RVA: 0x0000216D File Offset: 0x0000036D
		public int addProgress
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06004F58 RID: 20312 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004F59 RID: 20313 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isSentPresentBox
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06004F5A RID: 20314 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004F5B RID: 20315 RVA: 0x0000216D File Offset: 0x0000036D
		public bool startDirEnd
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06004F5C RID: 20316 RVA: 0x0000216A File Offset: 0x0000036A
		public LoginBonusSlotWidget[] slotWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public LoginBonusMapWidet(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0000216D File Offset: 0x0000036D
		public void Ready()
		{
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(Dictionary<string, object> source, bool obtaining)
		{
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowObtainedItemAndGoToNext(EntryItemListData itemList, bool isPresentSent, Action callback)
		{
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectSlot()
		{
		}

		// Token: 0x04008D44 RID: 36164
		private const string TWLABEL_STARTDIR = "StartDirection";

		// Token: 0x04008D45 RID: 36165
		private const string k_ELabelSlotGroup = "SlotGroup";

		// Token: 0x04008D46 RID: 36166
		private const string k_ELabelSlotTemplate = "SlotTemplate";

		// Token: 0x04008D47 RID: 36167
		private const string k_ELabelSlotCocatorFormat = "SlotLocator{0:D2}";

		// Token: 0x04008D48 RID: 36168
		private const string k_ELabelSpriteDayFormat = "day{0}";

		// Token: 0x04008D49 RID: 36169
		private const string k_ELabelHoldingDate = "TextDate";

		// Token: 0x04008D4A RID: 36170
		private readonly LoginBonusSlotWidget[] m_SlotWidgets;
	}
}
