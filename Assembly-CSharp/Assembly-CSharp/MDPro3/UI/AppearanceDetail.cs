using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.Servant;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x020013DF RID: 5087
	public class AppearanceDetail : MonoBehaviour
	{
		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06009354 RID: 37716 RVA: 0x0014CB04 File Offset: 0x0014AD04
		private ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06009355 RID: 37717 RVA: 0x0014CB38 File Offset: 0x0014AD38
		private CanvasGroup CG
		{
			get
			{
				return this.m_CG = ((this.m_CG != null) ? this.m_CG : base.GetComponent<CanvasGroup>());
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06009356 RID: 37718 RVA: 0x0014CB6C File Offset: 0x0014AD6C
		private TextMeshProUGUI TextSetting
		{
			get
			{
				return this.m_TextSetting = ((this.m_TextSetting != null) ? this.m_TextSetting : this.Manager.GetElement<TextMeshProUGUI>("TextDetailSetting"));
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06009357 RID: 37719 RVA: 0x0014CBA8 File Offset: 0x0014ADA8
		private TextMeshProUGUI TextDescription
		{
			get
			{
				return this.m_TextDescription = ((this.m_TextDescription != null) ? this.m_TextDescription : this.Manager.GetElement<TextMeshProUGUI>("TextDetailDescription"));
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06009358 RID: 37720 RVA: 0x0014CBE4 File Offset: 0x0014ADE4
		private Image Image
		{
			get
			{
				return this.m_Image = ((this.m_Image != null) ? this.m_Image : this.Manager.GetElement<Image>("Image"));
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06009359 RID: 37721 RVA: 0x0014CC20 File Offset: 0x0014AE20
		private RawImage RawImage
		{
			get
			{
				return this.m_RawImage = ((this.m_RawImage != null) ? this.m_RawImage : this.Manager.GetElement<RawImage>("RawImage"));
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x0600935A RID: 37722 RVA: 0x0014CC5C File Offset: 0x0014AE5C
		private Image WallpaperBG
		{
			get
			{
				return this.m_WallpaperBG = ((this.m_WallpaperBG != null) ? this.m_WallpaperBG : this.Manager.GetElement<Image>("WallpaperBG"));
			}
		}

		// Token: 0x0600935B RID: 37723 RVA: 0x0014CC98 File Offset: 0x0014AE98
		public void Show()
		{
			this.CG.alpha = 1f;
			this.CG.interactable = true;
			this.CG.blocksRaycasts = true;
		}

		// Token: 0x0600935C RID: 37724 RVA: 0x0014CCC2 File Offset: 0x0014AEC2
		public void Hide()
		{
			this.CG.alpha = 0f;
			this.CG.interactable = false;
			this.CG.blocksRaycasts = false;
		}

		// Token: 0x0600935D RID: 37725 RVA: 0x0014CCEC File Offset: 0x0014AEEC
		public void SetItem(int code, string itemName, string desc, bool isCard)
		{
			this.TextSetting.text = itemName;
			this.TextDescription.text = desc;
			this.CancelLoading();
			this.cts = new CancellationTokenSource();
			this.SetIconAsync(code, this.cts.Token, isCard);
		}

		// Token: 0x0600935E RID: 37726 RVA: 0x0014CD2C File Offset: 0x0014AF2C
		private async UniTask SetIconAsync(int code, CancellationToken token, bool isCard)
		{
			string codeString = code.ToString();
			this.Image.gameObject.SetActive(false);
			this.Image.material = null;
			this.RawImage.gameObject.SetActive(false);
			this.WallpaperBG.gameObject.SetActive(false);
			if (isCard)
			{
				Texture2D art = await CardImageLoader.LoadArtAsync(code, true, token);
				if (art != null)
				{
					this.Image.sprite = TextureManager.Texture2Sprite(art);
					this.Image.gameObject.SetActive(true);
				}
			}
			else if (codeString.StartsWith("107"))
			{
				RawImage rawImage = this.RawImage;
				rawImage.material = await ABLoader.LoadProtectorMaterial(code.ToString(), token);
				rawImage = null;
				this.RawImage.material.renderQueue = 3000;
				this.RawImage.gameObject.SetActive(true);
			}
			else
			{
				AsyncOperationHandle<Sprite> load = Addressables.LoadAssetAsync<Sprite>(Items.GetIconAddress(codeString, DeviceInfo.OnMobile() ? 1 : 2));
				while (!load.IsDone)
				{
					await TaskUtility.WaitOneFrame(base.gameObject, token);
				}
				if (load.Result != null)
				{
					if (codeString.StartsWith("103"))
					{
						Image image = this.Image;
						image.material = await ABLoader.LoadFrameMaterial(codeString);
						image = null;
						this.Image.material.SetTexture("_ProfileFrameTex", load.Result.texture);
						AsyncOperationHandle<Sprite> load2 = Addressables.LoadAssetAsync<Sprite>(Items.GetIconAddress(Config.Get(string.Format("{0}Face{1}", Appearance.condition, Appearance.player), Program.items.faces[0].id.ToString()), DeviceInfo.OnMobile() ? 1 : 2));
						while (!load2.IsDone)
						{
							await TaskUtility.WaitOneFrame(base.gameObject, token);
						}
						if (load2.Result != null)
						{
							this.Image.sprite = load2.Result;
						}
						else
						{
							this.Image.sprite = TextureManager.container.black;
						}
						load2 = default(AsyncOperationHandle<Sprite>);
					}
					if (!codeString.StartsWith("103"))
					{
						this.Image.sprite = load.Result;
					}
					this.Image.gameObject.SetActive(true);
					this.WallpaperBG.gameObject.SetActive(codeString.StartsWith("113"));
				}
				load = default(AsyncOperationHandle<Sprite>);
			}
		}

		// Token: 0x0600935F RID: 37727 RVA: 0x0014CD87 File Offset: 0x0014AF87
		private void CancelLoading()
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
			this.cts = null;
		}

		// Token: 0x0400D1A6 RID: 53670
		private ElementObjectManager m_Manager;

		// Token: 0x0400D1A7 RID: 53671
		private CanvasGroup m_CG;

		// Token: 0x0400D1A8 RID: 53672
		private const string LABEL_TXT_SETTING = "TextDetailSetting";

		// Token: 0x0400D1A9 RID: 53673
		private TextMeshProUGUI m_TextSetting;

		// Token: 0x0400D1AA RID: 53674
		private const string LABEL_TXT_DESCRIPTION = "TextDetailDescription";

		// Token: 0x0400D1AB RID: 53675
		private TextMeshProUGUI m_TextDescription;

		// Token: 0x0400D1AC RID: 53676
		private const string LABEL_IMG = "Image";

		// Token: 0x0400D1AD RID: 53677
		private Image m_Image;

		// Token: 0x0400D1AE RID: 53678
		private const string LABEL_RIMG = "RawImage";

		// Token: 0x0400D1AF RID: 53679
		private RawImage m_RawImage;

		// Token: 0x0400D1B0 RID: 53680
		private const string LABEL_IMG_WALLPAPER_BG = "WallpaperBG";

		// Token: 0x0400D1B1 RID: 53681
		private Image m_WallpaperBG;

		// Token: 0x0400D1B2 RID: 53682
		private CancellationTokenSource cts;
	}
}
