using System;
using UnityEngine;
using YgomSystem.Utility;

namespace YgomSystem.UI.PropertyOverrider
{
	// Token: 0x02000678 RID: 1656
	public class PlatformRectTransformOverrider : PropertyOverriderBase<RectTransform>
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x0000216A File Offset: 0x0000036A
		public OverrideVector2Property sizeDelta
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Import(RectTransform target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Export(RectTransform target, DeviceInfo.PlatformType platformType)
		{
		}

		// Token: 0x04002F8F RID: 12175
		[SerializeField]
		private bool m_AddCanvasExpandSizeY;

		// Token: 0x04002F90 RID: 12176
		[SerializeField]
		private OverrideVector2Property m_AnchorMin;

		// Token: 0x04002F91 RID: 12177
		[SerializeField]
		private OverrideVector2Property m_AnchorMax;

		// Token: 0x04002F92 RID: 12178
		[SerializeField]
		private OverrideVector2Property m_AnchoredPosition;

		// Token: 0x04002F93 RID: 12179
		[SerializeField]
		private OverrideVector2Property m_SizeDelta;

		// Token: 0x04002F94 RID: 12180
		[SerializeField]
		private OverrideVector2Property m_Pivot;
	}
}
