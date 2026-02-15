using System;
using System.Collections;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002BF RID: 703
	public class DebugUIHandlerToggleHistory : DebugUIHandlerToggle
	{
		// Token: 0x060012AE RID: 4782 RVA: 0x00047040 File Offset: 0x00045240
		internal override void SetWidget(DebugUI.Widget widget)
		{
			DebugUI.HistoryBoolField historyBoolField = widget as DebugUI.HistoryBoolField;
			int historyDepth = ((historyBoolField != null) ? historyBoolField.historyDepth : 0);
			this.historyToggles = new Toggle[historyDepth];
			float columnOffset = ((historyDepth > 0) ? (230f / (float)historyDepth) : 0f);
			for (int index = 0; index < historyDepth; index++)
			{
				Toggle historyToggle = Object.Instantiate<Toggle>(this.valueToggle, base.transform);
				Vector3 pos = historyToggle.transform.position;
				pos.x += (float)(index + 1) * columnOffset;
				historyToggle.transform.position = pos;
				Image component = historyToggle.transform.GetChild(0).GetComponent<Image>();
				component.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(-1f, -1f, 2f, 2f), Vector2.zero);
				component.color = new Color32(50, 50, 50, 120);
				component.transform.GetChild(0).GetComponent<Image>().color = new Color32(110, 110, 110, byte.MaxValue);
				this.historyToggles[index] = historyToggle.GetComponent<Toggle>();
			}
			base.SetWidget(widget);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00047168 File Offset: 0x00045368
		protected internal override void UpdateValueLabel()
		{
			base.UpdateValueLabel();
			DebugUI.HistoryBoolField field = this.m_Field as DebugUI.HistoryBoolField;
			int historyDepth = ((field != null) ? field.historyDepth : 0);
			for (int index = 0; index < historyDepth; index++)
			{
				if (index < this.historyToggles.Length && this.historyToggles[index] != null)
				{
					this.historyToggles[index].isOn = field.GetHistoryValue(index);
				}
			}
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.RefreshAfterSanitization());
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000471E4 File Offset: 0x000453E4
		private IEnumerator RefreshAfterSanitization()
		{
			yield return null;
			this.valueToggle.isOn = this.m_Field.getter();
			yield break;
		}

		// Token: 0x04000C6D RID: 3181
		private Toggle[] historyToggles;

		// Token: 0x04000C6E RID: 3182
		private const float k_XOffset = 230f;
	}
}
