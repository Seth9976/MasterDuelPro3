using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001363 RID: 4963
	[RequireComponent(typeof(RawImage))]
	public class ArtRawImageHandler : MonoBehaviour
	{
		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06008FEE RID: 36846 RVA: 0x001397AC File Offset: 0x001379AC
		private RawImage RawImage
		{
			get
			{
				return this.m_RawImage = ((this.m_RawImage != null) ? this.m_RawImage : base.GetComponent<RawImage>());
			}
		}

		// Token: 0x06008FEF RID: 36847 RVA: 0x001397DE File Offset: 0x001379DE
		private void OnDestroy()
		{
			this.CancelLoad();
			this.ReleaseArt();
		}

		// Token: 0x06008FF0 RID: 36848 RVA: 0x001397EC File Offset: 0x001379EC
		private void CancelLoad()
		{
			try
			{
				CancellationTokenSource cancellationTokenSource = this.cts;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				CancellationTokenSource cancellationTokenSource2 = this.cts;
				if (cancellationTokenSource2 != null)
				{
					cancellationTokenSource2.Dispose();
				}
			}
			finally
			{
				this.cts = null;
			}
		}

		// Token: 0x06008FF1 RID: 36849 RVA: 0x00139838 File Offset: 0x00137A38
		private void ReleaseArt()
		{
			if (this.loadedCode != 0)
			{
				CardImageLoader.ReleaseArt(this.loadedCode);
				this.loadedCode = 0;
			}
		}

		// Token: 0x06008FF2 RID: 36850 RVA: 0x00139854 File Offset: 0x00137A54
		public void SetArt(int art)
		{
			if (this.code == art)
			{
				return;
			}
			this.ReleaseArt();
			this.code = art;
			this.LoadArtsAsync();
		}

		// Token: 0x06008FF3 RID: 36851 RVA: 0x00139874 File Offset: 0x00137A74
		private async UniTask LoadArtsAsync()
		{
			this.CancelLoad();
			this.RawImage.texture = TextureManager.container.unknownArt.texture;
			if (this.code != 0)
			{
				this.cts = new CancellationTokenSource();
				Texture2D art = await CardImageLoader.LoadArtAsync(this.code, this.cache, this.cts.Token);
				if (art != null)
				{
					this.RawImage.texture = art;
					this.loadedCode = this.code;
				}
			}
		}

		// Token: 0x0400CE84 RID: 52868
		public bool cache = true;

		// Token: 0x0400CE85 RID: 52869
		private int code;

		// Token: 0x0400CE86 RID: 52870
		private int loadedCode;

		// Token: 0x0400CE87 RID: 52871
		private CancellationTokenSource cts;

		// Token: 0x0400CE88 RID: 52872
		private RawImage m_RawImage;
	}
}
