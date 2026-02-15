using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu.Common;
using YgomSystem.UI;

namespace YgomGame.Shop
{
	// Token: 0x0200091B RID: 2331
	public class BindingCardPackThumb : Binding, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x0000216A File Offset: 0x0000036A
		public Image targetImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060043E9 RID: 17385 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool visible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060043EA RID: 17386 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060043EB RID: 17387 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReloadEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnRebind()
		{
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBinding()
		{
			return false;
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x040082AF RID: 33455
		[SerializeField]
		private string m_ThumbName;

		// Token: 0x040082B0 RID: 33456
		private Image m_ImageCache;

		// Token: 0x040082B1 RID: 33457
		private IAsyncProgressContent m_AsyncProgressContent;
	}
}
