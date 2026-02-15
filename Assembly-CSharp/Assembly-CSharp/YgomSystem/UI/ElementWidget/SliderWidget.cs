using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x02000697 RID: 1687
	public class SliderWidget : ElementWidgetBehaviourBase<SliderWidget>
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x0000216A File Offset: 0x0000036A
		public Slider slider
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton inputButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x0400305B RID: 12379
		[SerializeField]
		private string k_ELabelSlider;

		// Token: 0x0400305C RID: 12380
		[SerializeField]
		private string k_ELabelInputButton;

		// Token: 0x0400305D RID: 12381
		private Slider m_SliderCache;

		// Token: 0x0400305E RID: 12382
		private SelectionButton m_InputButtonCache;

		// Token: 0x0400305F RID: 12383
		private bool startSlide;
	}
}
