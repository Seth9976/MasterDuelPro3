using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000099 RID: 153
	internal class ButtonStripField : BaseField<int>
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x0001B857 File Offset: 0x00019A57
		public ButtonStripField()
			: base(null)
		{
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001B86D File Offset: 0x00019A6D
		public override void SetValueWithoutNotify(int newValue)
		{
			newValue = Mathf.Clamp(newValue, 0, this.m_Buttons.Count - 1);
			base.SetValueWithoutNotify(newValue);
			this.RefreshButtonsState();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001B898 File Offset: 0x00019A98
		private void RefreshButtonsState()
		{
			for (int i = 0; i < this.m_Buttons.Count; i++)
			{
				bool flag = i == this.value;
				if (flag)
				{
					this.m_Buttons[i].pseudoStates |= PseudoStates.Checked;
				}
				else
				{
					this.m_Buttons[i].pseudoStates &= ~PseudoStates.Checked;
				}
			}
		}

		// Token: 0x04000353 RID: 851
		private readonly List<Button> m_Buttons = new List<Button>();

		// Token: 0x0200009A RID: 154
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ButtonStripField, ButtonStripField.UxmlTraits>
		{
		}

		// Token: 0x0200009B RID: 155
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<int>.UxmlTraits
		{
		}
	}
}
