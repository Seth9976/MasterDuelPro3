using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Enquete
{
	// Token: 0x02000C21 RID: 3105
	public class InputCheckBoxWidget : SheetContentWidget, ISheetContentCompleteCheckWidget
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06005898 RID: 22680 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005899 RID: 22681 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInputComplete
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

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x0600589A RID: 22682 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600589B RID: 22683 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x0600589C RID: 22684 RVA: 0x000F4D13 File Offset: 0x000F2F13
		public InputCheckBoxWidget(ElementObjectManager eom, string label, int entityLength, int checkMin, int checkMax)
			: base(null, null)
		{
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEntityChangeValue(InputCheckBoxWidget.EntityWidget entityWidget)
		{
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CollectSelectionItems(SheetSelectionItemMap sheetSelectionItemMap)
		{
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x0000216D File Offset: 0x0000036D
		public override void CollectInputValues(Dictionary<string, object> resultValues)
		{
		}

		// Token: 0x040094CE RID: 38094
		public readonly InputCheckBoxWidget.EntityWidget[] entityWidgets;

		// Token: 0x040094CF RID: 38095
		public readonly int checkMin;

		// Token: 0x040094D0 RID: 38096
		public readonly int checkMax;

		// Token: 0x040094D1 RID: 38097
		public int m_LastSelectIdx;

		// Token: 0x02000C22 RID: 3106
		public class EntityWidget : SheetContentWidget
		{
			// Token: 0x170008EC RID: 2284
			// (get) Token: 0x060058A0 RID: 22688 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text text
			{
				get
				{
					return null;
				}
			}

			// Token: 0x14000094 RID: 148
			// (add) Token: 0x060058A1 RID: 22689 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060058A2 RID: 22690 RVA: 0x0000216D File Offset: 0x0000036D
			public event Action<InputCheckBoxWidget.EntityWidget> onChangedValue
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

			// Token: 0x060058A3 RID: 22691 RVA: 0x000F4D13 File Offset: 0x000F2F13
			public EntityWidget(ElementObjectManager eom, string label, int idx)
				: base(null, null)
			{
			}

			// Token: 0x060058A4 RID: 22692 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnChangeValue(bool isOn)
			{
			}

			// Token: 0x060058A5 RID: 22693 RVA: 0x0000216D File Offset: 0x0000036D
			public override void CollectInputValues(Dictionary<string, object> resultValues)
			{
			}

			// Token: 0x040094D2 RID: 38098
			public readonly int idx;

			// Token: 0x040094D3 RID: 38099
			public readonly ToggleWidget toggle;
		}
	}
}
