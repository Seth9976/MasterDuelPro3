using System;
using System.Collections;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002AA RID: 682
	public class DebugUIHandlerEnumHistory : DebugUIHandlerEnumField
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x00045854 File Offset: 0x00043A54
		internal override void SetWidget(DebugUI.Widget widget)
		{
			DebugUI.HistoryEnumField historyEnumField = widget as DebugUI.HistoryEnumField;
			int historyDepth = ((historyEnumField != null) ? historyEnumField.historyDepth : 0);
			this.historyValues = new Text[historyDepth];
			float columnOffset = ((historyDepth > 0) ? (230f / (float)historyDepth) : 0f);
			for (int index = 0; index < historyDepth; index++)
			{
				Text text2 = Object.Instantiate<Text>(this.valueLabel, base.transform);
				Vector3 pos = text2.transform.position;
				pos.x += (float)(index + 1) * columnOffset;
				text2.transform.position = pos;
				Text text = text2.GetComponent<Text>();
				text.color = new Color32(110, 110, 110, byte.MaxValue);
				this.historyValues[index] = text;
			}
			base.SetWidget(widget);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00045910 File Offset: 0x00043B10
		public override void UpdateValueLabel()
		{
			int index = this.m_Field.currentIndex;
			if (index < 0)
			{
				index = 0;
			}
			this.valueLabel.text = this.m_Field.enumNames[index].text;
			DebugUI.HistoryEnumField field = this.m_Field as DebugUI.HistoryEnumField;
			int historyDepth = ((field != null) ? field.historyDepth : 0);
			for (int indexHistory = 0; indexHistory < historyDepth; indexHistory++)
			{
				if (indexHistory < this.historyValues.Length && this.historyValues[indexHistory] != null)
				{
					this.historyValues[indexHistory].text = field.enumNames[field.GetHistoryValue(indexHistory)].text;
				}
			}
			if (base.isActiveAndEnabled)
			{
				base.StartCoroutine(this.RefreshAfterSanitization());
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000459C1 File Offset: 0x00043BC1
		private IEnumerator RefreshAfterSanitization()
		{
			yield return null;
			this.m_Field.currentIndex = this.m_Field.getIndex();
			this.valueLabel.text = this.m_Field.enumNames[this.m_Field.currentIndex].text;
			yield break;
		}

		// Token: 0x04000C22 RID: 3106
		private Text[] historyValues;

		// Token: 0x04000C23 RID: 3107
		private const float k_XOffset = 230f;
	}
}
