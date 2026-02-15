using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001367 RID: 4967
	[RequireComponent(typeof(RawImage))]
	public class CardRawImageHandler : MonoBehaviour
	{
		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06008FFE RID: 36862 RVA: 0x00139BD0 File Offset: 0x00137DD0
		public bool Refreshed
		{
			get
			{
				return this.m_Refreshed;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06008FFF RID: 36863 RVA: 0x00139BD8 File Offset: 0x00137DD8
		public RawImage RawImage
		{
			get
			{
				return this.m_RawImage = ((this.m_RawImage != null) ? this.m_RawImage : base.GetComponent<RawImage>());
			}
		}

		// Token: 0x06009000 RID: 36864 RVA: 0x00139C0A File Offset: 0x00137E0A
		private void Awake()
		{
			SystemEvent.OnVideoCardConfigChange += this.OnVideoCardConfigChange;
		}

		// Token: 0x06009001 RID: 36865 RVA: 0x00139C1D File Offset: 0x00137E1D
		protected void OnDestroy()
		{
			this.CancelCurrentLoad();
			global::UnityEngine.Object.Destroy(this.normalMat);
			global::UnityEngine.Object.Destroy(this.tempMat);
			this.ReleaseCard();
			SystemEvent.OnVideoCardConfigChange -= this.OnVideoCardConfigChange;
		}

		// Token: 0x06009002 RID: 36866 RVA: 0x00139C52 File Offset: 0x00137E52
		public void SetCard(int code)
		{
			if (code <= 0)
			{
				this.card = null;
				this.SetProtector(this.protectorCode);
				return;
			}
			this.SetCard(CardsManager.Get(code, false));
		}

		// Token: 0x06009003 RID: 36867 RVA: 0x00139C7C File Offset: 0x00137E7C
		public void SetCard(Card data)
		{
			if (data == null)
			{
				this.ReleaseCard();
				return;
			}
			if (this.card != null && this.card.Id == data.Id && this.m_Refreshed)
			{
				return;
			}
			this.CancelCurrentLoad();
			this.card = data;
			if (this.card != null)
			{
				this.LoadCardPicAsync();
				return;
			}
			this.ReleaseCard();
		}

		// Token: 0x06009004 RID: 36868 RVA: 0x00139CDC File Offset: 0x00137EDC
		public void CancelCurrentLoad()
		{
			try
			{
				this.currentLoadId++;
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

		// Token: 0x06009005 RID: 36869 RVA: 0x00139D34 File Offset: 0x00137F34
		public void RefreshRarity(int code)
		{
			if (this.card == null || this.card.Id != code)
			{
				return;
			}
			if (this.tempMat != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.tempMat);
			}
			if (CardRarity.GetRarity(this.card.Id) == CardRarity.Rarity.Normal)
			{
				if (this.normalMat != null)
				{
					this.normalMat.SetFloat("_LoadingBlend", 0f);
					this.RawImage.material = this.normalMat;
					return;
				}
			}
			else
			{
				this.LoadMatAsync(0f, default(CancellationToken));
			}
		}

		// Token: 0x06009006 RID: 36870 RVA: 0x00139DCE File Offset: 0x00137FCE
		public void SetProtector(int code)
		{
			this.protectorCode = code;
			this.LoadProtectorAsync(code);
		}

		// Token: 0x06009007 RID: 36871 RVA: 0x00139DDF File Offset: 0x00137FDF
		private void ReleaseCard()
		{
			if (this.loadedCardId != 0)
			{
				CardImageLoader.ReleaseCard(this.loadedCardId);
				this.loadedCardId = 0;
			}
			this.RawImage.texture = null;
			this.m_Refreshed = false;
		}

		// Token: 0x06009008 RID: 36872 RVA: 0x00139E10 File Offset: 0x00138010
		private async UniTaskVoid LoadCardPicAsync()
		{
			if (this.card != null)
			{
				int targetCardId = this.card.Id;
				int num = this.currentLoadId + 1;
				this.currentLoadId = num;
				int loadId = num;
				this.m_Refreshed = false;
				try
				{
					this.cts = new CancellationTokenSource();
					CancellationToken token = this.cts.Token;
					await UniTask.Yield(PlayerLoopTiming.Update, token, false);
					if (this.card != null && this.card.Id == targetCardId && loadId == this.currentLoadId)
					{
						if (this.normalMat == null)
						{
							this.normalMat = MaterialLoader.GetCardMaterial(-1, false);
						}
						this.normalMat.SetTexture("_LoadingTex", TextureManager.container.GetCardLoadingTexture(CardsManager.Get(this.card.Id, false)));
						this.normalMat.SetFloat("_LoadingBlend", 1f);
						this.RawImage.material = this.normalMat;
						if (this.tempMat != null)
						{
							global::UnityEngine.Object.Destroy(this.tempMat);
							this.tempMat = null;
						}
						await UniTask.Yield(PlayerLoopTiming.Update, token, false);
						if (this.card != null && this.card.Id == targetCardId && loadId == this.currentLoadId)
						{
							Texture cardTex = await CardImageLoader.LoadCardAsync(this.card.Id, this.cache, token, false);
							if (this.card == null || this.card.Id != targetCardId || loadId != this.currentLoadId)
							{
								if (cardTex != null)
								{
									CardImageLoader.ReleaseCard(this.card.Id);
								}
							}
							else if (cardTex == null)
							{
								Debug.LogError(string.Format("Failed to load texture for card {0}", this.card.Id));
							}
							else
							{
								if (this.loadedCardId != 0 && this.loadedCardId != this.card.Id)
								{
									CardImageLoader.ReleaseCard(this.loadedCardId);
								}
								this.loadedCardId = this.card.Id;
								this.RawImage.texture = cardTex;
								this.isRenderTexture = cardTex is RenderTexture;
								if (CardRarity.GetRarity(this.card.Id) == CardRarity.Rarity.Normal)
								{
									await this.SetMaterialFloatAsync(this.normalMat, "_LoadingBlend", 0f, 0.1f, token);
								}
								else
								{
									await this.LoadMatAsync(0.1f, token);
								}
								if (this.card != null && this.card.Id == targetCardId && loadId == this.currentLoadId)
								{
									this.m_Refreshed = true;
									token = default(CancellationToken);
								}
							}
						}
					}
				}
				catch (OperationCanceledException)
				{
				}
				catch (Exception e)
				{
					Debug.LogError("Load card picture failed: " + e.Message);
				}
			}
		}

		// Token: 0x06009009 RID: 36873 RVA: 0x00139E54 File Offset: 0x00138054
		protected async UniTask LoadMatAsync(float fadeTime, CancellationToken token)
		{
			if (this.card != null)
			{
				try
				{
					this.tempMat = MaterialLoader.GetCardMaterial(this.card.Id, false);
					if (!this.cts.Token.IsCancellationRequested && this.card != null)
					{
						this.tempMat.SetFloat("_LoadingBlend", 1f);
						this.tempMat.SetTexture("_LoadingTex", this.normalMat.GetTexture("_LoadingTex"));
						this.RawImage.material = this.tempMat;
						await this.SetMaterialFloatAsync(this.tempMat, "_LoadingBlend", 0f, fadeTime, token);
					}
				}
				catch (OperationCanceledException)
				{
				}
			}
		}

		// Token: 0x0600900A RID: 36874 RVA: 0x00139EA8 File Offset: 0x001380A8
		private async UniTask LoadProtectorAsync(int code)
		{
			this.m_Refreshed = false;
			this.CancelCurrentLoad();
			this.ReleaseCard();
			this.card = null;
			try
			{
				Material material = await ABLoader.LoadProtectorMaterial(code.ToString(), base.destroyCancellationToken);
				if (material != null)
				{
					this.RawImage.material = material;
					this.RawImage.texture = null;
					this.m_Refreshed = true;
				}
			}
			catch (Exception e)
			{
				Debug.LogError("Load protector failed: " + e.Message);
			}
		}

		// Token: 0x0600900B RID: 36875 RVA: 0x00139EF4 File Offset: 0x001380F4
		private async UniTask SetMaterialFloatAsync(Material mat, string propertyName, float endValue, float duration, CancellationToken token)
		{
			if (!(mat == null))
			{
				float startValue = mat.GetFloat(propertyName);
				float elapsedTime = 0f;
				while (elapsedTime < duration && !token.IsCancellationRequested)
				{
					await UniTask.Yield(token, false);
					if (mat == null || token.IsCancellationRequested)
					{
						return;
					}
					float t = elapsedTime / duration;
					float currentValue = Mathf.Lerp(startValue, endValue, t);
					mat.SetFloat(propertyName, currentValue);
					elapsedTime += Time.deltaTime;
				}
				if (mat != null && !token.IsCancellationRequested)
				{
					mat.SetFloat(propertyName, endValue);
				}
			}
		}

		// Token: 0x0600900C RID: 36876 RVA: 0x00139F5C File Offset: 0x0013815C
		private void OnVideoCardConfigChange()
		{
			bool config = Config.GetBool("VideoCard", true);
			if (config && CardImageLoader.CardHasVideoArt(this.card.Id))
			{
				this.ReloadAsync();
				return;
			}
			if (!config && this.isRenderTexture)
			{
				this.ReloadAsync();
			}
		}

		// Token: 0x0600900D RID: 36877 RVA: 0x00139FA4 File Offset: 0x001381A4
		private async UniTask ReloadAsync()
		{
			await UniTask.Yield();
			this.CancelCurrentLoad();
			this.LoadCardPicAsync();
		}

		// Token: 0x0400CE91 RID: 52881
		public bool cache;

		// Token: 0x0400CE92 RID: 52882
		public Card card;

		// Token: 0x0400CE93 RID: 52883
		public int protectorCode = 1070001;

		// Token: 0x0400CE94 RID: 52884
		protected bool m_Refreshed;

		// Token: 0x0400CE95 RID: 52885
		private RawImage m_RawImage;

		// Token: 0x0400CE96 RID: 52886
		protected Material normalMat;

		// Token: 0x0400CE97 RID: 52887
		protected Material tempMat;

		// Token: 0x0400CE98 RID: 52888
		private CancellationTokenSource cts;

		// Token: 0x0400CE99 RID: 52889
		private int currentLoadId;

		// Token: 0x0400CE9A RID: 52890
		private int loadedCardId;

		// Token: 0x0400CE9B RID: 52891
		private bool isRenderTexture;
	}
}
