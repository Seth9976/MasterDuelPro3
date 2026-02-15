using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020002B8 RID: 696
	public class DebugUIHandlerPanel : MonoBehaviour
	{
		// Token: 0x06001285 RID: 4741 RVA: 0x0004674F File Offset: 0x0004494F
		private void OnEnable()
		{
			this.m_ScrollTransform = this.scrollRect.GetComponent<RectTransform>();
			this.m_ContentTransform = base.GetComponent<DebugUIHandlerContainer>().contentHolder;
			this.m_MaskTransform = base.GetComponentInChildren<Mask>(true).rectTransform;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00046785 File Offset: 0x00044985
		internal void SetPanel(DebugUI.Panel panel)
		{
			this.m_Panel = panel;
			this.nameLabel.text = panel.displayName;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0004679F File Offset: 0x0004499F
		internal DebugUI.Panel GetPanel()
		{
			return this.m_Panel;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000467A7 File Offset: 0x000449A7
		public void SelectNextItem()
		{
			this.Canvas.SelectNextPanel();
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000467B4 File Offset: 0x000449B4
		public void SelectPreviousItem()
		{
			this.Canvas.SelectPreviousPanel();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000467C1 File Offset: 0x000449C1
		public void OnScrollbarClicked()
		{
			DebugManager.instance.SetScrollTarget(null);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000467CE File Offset: 0x000449CE
		internal void SetScrollTarget(DebugUIHandlerWidget target)
		{
			this.m_ScrollTarget = target;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000467D8 File Offset: 0x000449D8
		internal void UpdateScroll()
		{
			if (this.m_ScrollTarget == null)
			{
				return;
			}
			RectTransform targetTransform = this.m_ScrollTarget.GetComponent<RectTransform>();
			float itemY = this.GetYPosInScroll(targetTransform);
			float normalizedDiffY = (this.GetYPosInScroll(this.m_MaskTransform) - itemY) / (this.m_ContentTransform.rect.size.y - this.m_ScrollTransform.rect.size.y);
			float normalizedPosY = this.scrollRect.verticalNormalizedPosition - normalizedDiffY;
			normalizedPosY = Mathf.Clamp01(normalizedPosY);
			this.scrollRect.verticalNormalizedPosition = Mathf.Lerp(this.scrollRect.verticalNormalizedPosition, normalizedPosY, Time.deltaTime * 10f);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00046888 File Offset: 0x00044A88
		private float GetYPosInScroll(RectTransform target)
		{
			Vector3 pivotOffset = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
			Vector3 localPos = target.localPosition + pivotOffset;
			Vector3 worldPos = target.parent.TransformPoint(localPos);
			return this.m_ScrollTransform.TransformPoint(worldPos).y;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00046916 File Offset: 0x00044B16
		internal DebugUIHandlerWidget GetFirstItem()
		{
			return base.GetComponent<DebugUIHandlerContainer>().GetFirstItem();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00046923 File Offset: 0x00044B23
		public void ResetDebugManager()
		{
			DebugManager.instance.Reset();
		}

		// Token: 0x04000C54 RID: 3156
		public Text nameLabel;

		// Token: 0x04000C55 RID: 3157
		public ScrollRect scrollRect;

		// Token: 0x04000C56 RID: 3158
		public RectTransform viewport;

		// Token: 0x04000C57 RID: 3159
		public DebugUIHandlerCanvas Canvas;

		// Token: 0x04000C58 RID: 3160
		private RectTransform m_ScrollTransform;

		// Token: 0x04000C59 RID: 3161
		private RectTransform m_ContentTransform;

		// Token: 0x04000C5A RID: 3162
		private RectTransform m_MaskTransform;

		// Token: 0x04000C5B RID: 3163
		private DebugUIHandlerWidget m_ScrollTarget;

		// Token: 0x04000C5C RID: 3164
		protected internal DebugUI.Panel m_Panel;
	}
}
