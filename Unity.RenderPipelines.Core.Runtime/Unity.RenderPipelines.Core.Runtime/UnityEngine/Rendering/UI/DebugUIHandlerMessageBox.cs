using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B4 RID: 692
	public class DebugUIHandlerMessageBox : DebugUIHandlerWidget
	{
		// Token: 0x06001271 RID: 4721 RVA: 0x00046444 File Offset: 0x00044644
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.MessageBox>();
			this.nameLabel.text = this.m_Field.displayName;
			Image image = base.GetComponent<Image>();
			DebugUI.MessageBox.Style style = this.m_Field.style;
			if (style == DebugUI.MessageBox.Style.Warning)
			{
				image.color = DebugUIHandlerMessageBox.k_WarningBackgroundColor;
				return;
			}
			if (style != DebugUI.MessageBox.Style.Error)
			{
				return;
			}
			image.color = DebugUIHandlerMessageBox.k_ErrorBackgroundColor;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000464B7 File Offset: 0x000446B7
		private void Update()
		{
			this.nameLabel.text = this.m_Field.message;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000090C6 File Offset: 0x000072C6
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			return false;
		}

		// Token: 0x04000C4A RID: 3146
		public Text nameLabel;

		// Token: 0x04000C4B RID: 3147
		private DebugUI.MessageBox m_Field;

		// Token: 0x04000C4C RID: 3148
		private static Color32 k_WarningBackgroundColor = new Color32(231, 180, 3, 30);

		// Token: 0x04000C4D RID: 3149
		private static Color32 k_WarningTextColor = new Color32(231, 180, 3, byte.MaxValue);

		// Token: 0x04000C4E RID: 3150
		private static Color32 k_ErrorBackgroundColor = new Color32(231, 75, 3, 30);

		// Token: 0x04000C4F RID: 3151
		private static Color32 k_ErrorTextColor = new Color32(231, 75, 3, byte.MaxValue);
	}
}
