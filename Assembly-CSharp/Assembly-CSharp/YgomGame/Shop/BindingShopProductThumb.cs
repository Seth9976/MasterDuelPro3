using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu.Common;
using YgomSystem.UI;

namespace YgomGame.Shop
{
	// Token: 0x0200091C RID: 2332
	public class BindingShopProductThumb : MonoBehaviour, IAsyncProgressContent, ILoadingIconHandler
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x0000216A File Offset: 0x0000036A
		public BindingShopProductThumb.Context context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x0000216D File Offset: 0x0000036D
		public bool enabledAspectRatio
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060043F7 RID: 17399 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060043F8 RID: 17400 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x060043F9 RID: 17401 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingShopProductThumb Attach(RectTransform root)
		{
			return null;
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x0000216A File Offset: 0x0000036A
		public static BindingShopProductThumb Binding(RectTransform root, BindingShopProductThumb.Context context)
		{
			return null;
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSourceChange()
		{
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdatedRawImage()
		{
		}

		// Token: 0x06004400 RID: 17408 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnAbort()
		{
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnReady()
		{
		}

		// Token: 0x06004402 RID: 17410 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshVisible()
		{
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x0000216D File Offset: 0x0000036D
		public void Binding()
		{
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerCardIllustBinding()
		{
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerImageBinding()
		{
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerPrefabBinding()
		{
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x0000216D File Offset: 0x0000036D
		public void CaptureTo(BindingShopProductThumb target)
		{
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x0000216D File Offset: 0x0000036D
		public void ProgressUpdate()
		{
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLoadCardTextureComplete(int mrk, bool onSettingsComplete = false)
		{
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLoadImageTextureComplete(Texture2D texture)
		{
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLoadPrefabComplete(GameObject bindedObject)
		{
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x040082B2 RID: 33458
		private ShopCardThumbSettings m_ShopCardThumbSettings;

		// Token: 0x040082B3 RID: 33459
		private IAsyncProgressContent m_AsyncProgressContent;

		// Token: 0x040082B4 RID: 33460
		private RectTransform m_RectTransform;

		// Token: 0x040082B5 RID: 33461
		private RectTransform m_RectMask;

		// Token: 0x040082B6 RID: 33462
		private AspectRatioFitter m_RectAspectRatioFitter;

		// Token: 0x040082B7 RID: 33463
		private RawImage m_RawImage;

		// Token: 0x040082B8 RID: 33464
		private Texture m_RawImageTex;

		// Token: 0x040082B9 RID: 33465
		private Rect m_RawImageUV;

		// Token: 0x040082BA RID: 33466
		private AspectRatioFitter m_ImageAspectRatioFitter;

		// Token: 0x040082BB RID: 33467
		private GameObject m_BindiedObject;

		// Token: 0x040082BC RID: 33468
		[SerializeField]
		private BindingShopProductThumb.Context m_Context;

		// Token: 0x040082BD RID: 33469
		private Action m_OnLoadedSettingCallback;

		// Token: 0x040082BE RID: 33470
		private bool m_Visible;

		// Token: 0x040082BF RID: 33471
		private bool m_Initialized;

		// Token: 0x0200091D RID: 2333
		public enum ThumbType
		{
			// Token: 0x040082C1 RID: 33473
			CardIllust = 1,
			// Token: 0x040082C2 RID: 33474
			Image,
			// Token: 0x040082C3 RID: 33475
			Prefab
		}

		// Token: 0x0200091E RID: 2334
		[Serializable]
		public class Context
		{
			// Token: 0x0600440F RID: 17423 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportWork(int thumbType, string thumbData)
			{
			}

			// Token: 0x06004410 RID: 17424 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsEqualParams(BindingShopProductThumb.Context other)
			{
				return false;
			}

			// Token: 0x06004411 RID: 17425 RVA: 0x0000216D File Offset: 0x0000036D
			public void Import(BindingShopProductThumb.Context other)
			{
			}

			// Token: 0x040082C4 RID: 33476
			public BindingShopProductThumb.ThumbType thumbType;

			// Token: 0x040082C5 RID: 33477
			public ShopCardThumbSettings.Format format;

			// Token: 0x040082C6 RID: 33478
			public int mrk;

			// Token: 0x040082C7 RID: 33479
			public string path;
		}
	}
}
