using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002AE RID: 686
	public class DebugUIHandlerFoldout : DebugUIHandlerWidget
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x00045CF4 File Offset: 0x00043EF4
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Foldout>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			string[] columnLabels = this.m_Field.columnLabels;
			int columnNumber = ((columnLabels != null) ? columnLabels.Length : 0);
			float columnOffset = ((columnNumber > 0) ? (230f / (float)columnNumber) : 0f);
			for (int index = 0; index < columnNumber; index++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.nameLabel.gameObject, base.GetComponent<DebugUIHandlerContainer>().contentHolder);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				RectTransform rectTransform = gameObject.transform as RectTransform;
				RectTransform originalTransform = this.nameLabel.transform as RectTransform;
				Vector2 vector = new Vector2(0f, 1f);
				rectTransform.anchorMin = vector;
				rectTransform.anchorMax = vector;
				rectTransform.sizeDelta = new Vector2(100f, 26f);
				Vector3 pos = originalTransform.anchoredPosition;
				pos.x += (float)(index + 1) * columnOffset + 215f;
				rectTransform.anchoredPosition = pos;
				rectTransform.pivot = new Vector2(0f, 0.5f);
				rectTransform.eulerAngles = new Vector3(0f, 0f, 13f);
				Text component = gameObject.GetComponent<Text>();
				component.fontSize = 15;
				component.text = this.m_Field.columnLabels[index];
			}
			this.UpdateValue();
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00045E70 File Offset: 0x00044070
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00045EE7 File Offset: 0x000440E7
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00045EFA File Offset: 0x000440FA
		public override void OnIncrement(bool fast)
		{
			this.m_Field.SetValue(true);
			this.UpdateValue();
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00045F0E File Offset: 0x0004410E
		public override void OnDecrement(bool fast)
		{
			this.m_Field.SetValue(false);
			this.UpdateValue();
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00045F24 File Offset: 0x00044124
		public override void OnAction()
		{
			bool value = !this.m_Field.GetValue();
			this.m_Field.SetValue(value);
			this.UpdateValue();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00045F52 File Offset: 0x00044152
		private void UpdateValue()
		{
			this.valueToggle.isOn = this.m_Field.GetValue();
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00045F6C File Offset: 0x0004416C
		public override DebugUIHandlerWidget Next()
		{
			if (!this.m_Field.GetValue() || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstChild = this.m_Container.GetFirstItem();
			if (firstChild == null)
			{
				return base.Next();
			}
			return firstChild;
		}

		// Token: 0x04000C2F RID: 3119
		public Text nameLabel;

		// Token: 0x04000C30 RID: 3120
		public UIFoldout valueToggle;

		// Token: 0x04000C31 RID: 3121
		private DebugUI.Foldout m_Field;

		// Token: 0x04000C32 RID: 3122
		private DebugUIHandlerContainer m_Container;

		// Token: 0x04000C33 RID: 3123
		private const float k_FoldoutXOffset = 215f;

		// Token: 0x04000C34 RID: 3124
		private const float k_XOffset = 230f;
	}
}
