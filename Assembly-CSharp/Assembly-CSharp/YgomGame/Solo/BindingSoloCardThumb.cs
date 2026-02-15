using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu.Common;

namespace YgomGame.Solo
{
	// Token: 0x020008EC RID: 2284
	public class BindingSoloCardThumb : MonoBehaviour, IAsyncProgressContent
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060042E8 RID: 17128 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x060042E9 RID: 17129 RVA: 0x0000216A File Offset: 0x0000036A
		private static BindingSoloCardThumb Binding(RectTransform root, SoloCardThumbSettings.ThumbSetting thumbSetting, BindingSoloCardThumb.BindTargetType bindTargetType)
		{
			return null;
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingSoloCardThumb Binding(RectTransform root, SoloCardThumbSettings.ThumbSetting thumbSetting)
		{
			return null;
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingSoloCardThumb BindingOther(RectTransform root, SoloCardThumbSettings.ThumbSetting thumbSetting)
		{
			return null;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerBinding(SoloCardThumbSettings.ThumbSetting thumbSetting, BindingSoloCardThumb.BindTargetType bindTargetType)
		{
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLoadCardTextureComplete(BindingSoloCardThumb.BindTargetType bindTargetType)
		{
		}

		// Token: 0x0400814F RID: 33103
		private RectTransform m_RectTransform;

		// Token: 0x04008150 RID: 33104
		private RectTransform m_RectMask;

		// Token: 0x04008151 RID: 33105
		private RawImage m_CardTextureImage;

		// Token: 0x04008152 RID: 33106
		private AspectRatioFitter m_AspectRatioFitter;

		// Token: 0x04008153 RID: 33107
		private SoloCardThumbSettings.ThumbSetting m_ThumbSetting;

		// Token: 0x020008ED RID: 2285
		private enum BindTargetType
		{
			// Token: 0x04008155 RID: 33109
			DEFAULT,
			// Token: 0x04008156 RID: 33110
			OTHER
		}
	}
}
