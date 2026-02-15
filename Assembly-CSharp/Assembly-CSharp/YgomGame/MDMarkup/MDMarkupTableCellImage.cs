using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC7 RID: 3015
	public class MDMarkupTableCellImage : ElementWidgetBase, IMDMarkupAsyncWidget
	{
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06005606 RID: 22022 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005607 RID: 22023 RVA: 0x0000216D File Offset: 0x0000036D
		public bool borderVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06005608 RID: 22024 RVA: 0x0000216A File Offset: 0x0000036A
		public Image image
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06005609 RID: 22025 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600560A RID: 22026 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isReady
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

		// Token: 0x0600560B RID: 22027 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupTableCellImage(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSprite(string imagePath, float overrideHeight = 0f)
		{
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlignment(TextAlignmentOptions alignment)
		{
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSizeRate(float sizeRate)
		{
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnReady()
		{
		}

		// Token: 0x04009303 RID: 37635
		private readonly string k_ELabelImage;

		// Token: 0x04009304 RID: 37636
		private readonly Image m_Image;

		// Token: 0x04009305 RID: 37637
		public readonly LayoutElement m_LayoutElement;
	}
}
