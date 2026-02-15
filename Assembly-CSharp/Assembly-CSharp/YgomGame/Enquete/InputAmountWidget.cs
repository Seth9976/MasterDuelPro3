using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Enquete
{
	// Token: 0x02000C1F RID: 3103
	public class InputAmountWidget : SheetContentWidget, ISheetContentCompleteCheckWidget
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06005889 RID: 22665 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600588A RID: 22666 RVA: 0x0000216D File Offset: 0x0000036D
		public int currentAmount
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

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600588B RID: 22667 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text minText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600588C RID: 22668 RVA: 0x0000216A File Offset: 0x0000036A
		public TMP_Text maxText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600588D RID: 22669 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isInputComplete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x0600588E RID: 22670 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600588F RID: 22671 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onChangeComplete
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

		// Token: 0x06005890 RID: 22672 RVA: 0x000F4D13 File Offset: 0x000F2F13
		public InputAmountWidget(ElementObjectManager eom, string label, int amountLength)
			: base(null, null)
		{
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnToggleChangeValue(InputAmountWidget.ToggleWidget toggle)
		{
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CollectInputValues(Dictionary<string, object> resultValues)
		{
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CollectSelectionItems(SheetSelectionItemMap sheetSelectionItemMap)
		{
		}

		// Token: 0x040094CB RID: 38091
		public readonly InputAmountWidget.ToggleWidget[] toggleWidgets;

		// Token: 0x02000C20 RID: 3104
		public class ToggleWidget : SheetContentWidget
		{
			// Token: 0x14000092 RID: 146
			// (add) Token: 0x06005894 RID: 22676 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x06005895 RID: 22677 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<InputAmountWidget.ToggleWidget> onChangedValue
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

			// Token: 0x06005896 RID: 22678 RVA: 0x000F4D13 File Offset: 0x000F2F13
			public ToggleWidget(ElementObjectManager eom, int idx)
				: base(null, null)
			{
			}

			// Token: 0x06005897 RID: 22679 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnChangeValue(bool isOn)
			{
			}

			// Token: 0x040094CC RID: 38092
			public readonly YgomSystem.UI.ElementWidget.ToggleWidget toggle;

			// Token: 0x040094CD RID: 38093
			public readonly int idx;
		}
	}
}
