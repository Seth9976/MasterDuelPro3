using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002C4 RID: 708
	public class DebugUIHandlerValueTuple : DebugUIHandlerWidget
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x00047552 File Offset: 0x00045752
		protected override void OnEnable()
		{
			this.m_Timer = 0f;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0004755F File Offset: 0x0004575F
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00047573 File Offset: 0x00045773
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00047588 File Offset: 0x00045788
		internal override void SetWidget(DebugUI.Widget widget)
		{
			this.m_Widget = widget;
			this.m_Field = base.CastWidget<DebugUI.ValueTuple>();
			this.nameLabel.text = this.m_Field.displayName;
			int numElements = this.m_Field.numElements;
			this.valueElements = new Text[numElements];
			this.valueElements[0] = this.valueLabel;
			float columnOffset = 230f / (float)numElements;
			for (int index = 1; index < numElements; index++)
			{
				GameObject valueElement = Object.Instantiate<GameObject>(this.valueLabel.gameObject, base.transform);
				valueElement.AddComponent<LayoutElement>().ignoreLayout = true;
				RectTransform rectTransform = valueElement.transform as RectTransform;
				RectTransform originalTransform = this.nameLabel.transform as RectTransform;
				Vector2 vector = new Vector2(0f, 1f);
				rectTransform.anchorMin = vector;
				rectTransform.anchorMax = vector;
				rectTransform.sizeDelta = new Vector2(100f, 26f);
				Vector3 pos = originalTransform.anchoredPosition;
				pos.x += (float)(index + 1) * columnOffset + 200f;
				rectTransform.anchoredPosition = pos;
				rectTransform.pivot = new Vector2(0f, 1f);
				this.valueElements[index] = valueElement.GetComponent<Text>();
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000476CC File Offset: 0x000458CC
		internal virtual void UpdateValueLabels()
		{
			for (int index = 0; index < this.m_Field.numElements; index++)
			{
				if (index < this.valueElements.Length && this.valueElements[index] != null)
				{
					object value = this.m_Field.values[index].GetValue();
					this.valueElements[index].text = this.m_Field.values[index].FormatString(value);
					if (value is float)
					{
						this.valueElements[index].color = (((float)value == 0f) ? DebugUIHandlerValueTuple.k_ZeroColor : this.colorDefault);
					}
				}
			}
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00047774 File Offset: 0x00045974
		private void Update()
		{
			if (this.m_Field != null && this.m_Timer >= this.m_Field.refreshRate)
			{
				this.UpdateValueLabels();
				this.m_Timer -= this.m_Field.refreshRate;
			}
			this.m_Timer += Time.deltaTime;
		}

		// Token: 0x04000C7B RID: 3195
		public Text nameLabel;

		// Token: 0x04000C7C RID: 3196
		public Text valueLabel;

		// Token: 0x04000C7D RID: 3197
		protected internal DebugUI.ValueTuple m_Field;

		// Token: 0x04000C7E RID: 3198
		protected internal Text[] valueElements;

		// Token: 0x04000C7F RID: 3199
		private const float k_XOffset = 230f;

		// Token: 0x04000C80 RID: 3200
		private float m_Timer;

		// Token: 0x04000C81 RID: 3201
		private static readonly Color k_ZeroColor = Color.gray;
	}
}
