using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.Video;

namespace MDPro3
{
	// Token: 0x020011F2 RID: 4594
	public class CardRenderer : MonoBehaviour
	{
		// Token: 0x06008858 RID: 34904 RVA: 0x000FA7E8 File Offset: 0x000F89E8
		private static async UniTask LoadFontsAsync()
		{
			if (!CardRenderer.fontsLoaded)
			{
				UniTask<Font>.Awaiter awaiter = Addressables.LoadAssetAsync<Font>("RenderFontChineseSimplified").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				UniTask<Font>.Awaiter awaiter2;
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<Font>.Awaiter);
				}
				CardRenderer.fontChineseSimplified = awaiter.GetResult();
				UniTask<TMP_FontAsset>.Awaiter awaiter3 = Addressables.LoadAssetAsync<TMP_FontAsset>("RenderFontChineseSimplified").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				UniTask<TMP_FontAsset>.Awaiter awaiter4;
				if (!awaiter3.IsCompleted)
				{
					await awaiter3;
					awaiter3 = awaiter4;
					awaiter4 = default(UniTask<TMP_FontAsset>.Awaiter);
				}
				CardRenderer.tmpFontChineseSimplified = awaiter3.GetResult();
				awaiter = Addressables.LoadAssetAsync<Font>("RenderFontChineseTraditional").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<Font>.Awaiter);
				}
				CardRenderer.fontChineseTraditional = awaiter.GetResult();
				awaiter3 = Addressables.LoadAssetAsync<TMP_FontAsset>("RenderFontChineseTraditional").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				if (!awaiter3.IsCompleted)
				{
					await awaiter3;
					awaiter3 = awaiter4;
					awaiter4 = default(UniTask<TMP_FontAsset>.Awaiter);
				}
				CardRenderer.tmpFontChineseTraditional = awaiter3.GetResult();
				awaiter = Addressables.LoadAssetAsync<Font>("RenderFontKorean").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<Font>.Awaiter);
				}
				CardRenderer.fontKorean = awaiter.GetResult();
				awaiter3 = Addressables.LoadAssetAsync<TMP_FontAsset>("RenderFontKorean").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				if (!awaiter3.IsCompleted)
				{
					await awaiter3;
					awaiter3 = awaiter4;
					awaiter4 = default(UniTask<TMP_FontAsset>.Awaiter);
				}
				CardRenderer.tmpFontKorean = awaiter3.GetResult();
				awaiter = Addressables.LoadAssetAsync<Font>("RenderFontJapanese").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<Font>.Awaiter);
				}
				CardRenderer.fontJapanese = awaiter.GetResult();
				awaiter3 = Addressables.LoadAssetAsync<TMP_FontAsset>("RenderFontJapanese").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				if (!awaiter3.IsCompleted)
				{
					await awaiter3;
					awaiter3 = awaiter4;
					awaiter4 = default(UniTask<TMP_FontAsset>.Awaiter);
				}
				CardRenderer.tmpFontJapanese = awaiter3.GetResult();
				awaiter = Addressables.LoadAssetAsync<Font>("RenderFontEnglish").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					await awaiter;
					awaiter = awaiter2;
					awaiter2 = default(UniTask<Font>.Awaiter);
				}
				CardRenderer.fontEnglish = awaiter.GetResult();
				awaiter3 = Addressables.LoadAssetAsync<TMP_FontAsset>("RenderFontEnglish").ToUniTask(null, PlayerLoopTiming.Update, default(CancellationToken), false, false).GetAwaiter();
				if (!awaiter3.IsCompleted)
				{
					await awaiter3;
					awaiter3 = awaiter4;
					awaiter4 = default(UniTask<TMP_FontAsset>.Awaiter);
				}
				CardRenderer.tmpFontEnglish = awaiter3.GetResult();
				CardRenderer.fontsLoaded = true;
			}
		}

		// Token: 0x06008859 RID: 34905 RVA: 0x000FA824 File Offset: 0x000F8A24
		private void SetFonts(Font font, TMP_FontAsset tmpFont)
		{
			this.cardDescription.font = font;
			this.cardDescriptionRD.font = font;
			this.cardDescriptionPendulum.font = font;
			this.cardDescriptionPendulumRD.font = font;
			this.cardAuther.font = font;
			this.cardAutherRD.font = font;
			this.cardName.font = tmpFont;
			this.cardNameRD.font = tmpFont;
			this.spellType.font = tmpFont;
			this.cardTypeRD.font = tmpFont;
			this.attrRuby.font = tmpFont;
			this.attrRubyRD.font = tmpFont;
		}

		// Token: 0x0600885A RID: 34906 RVA: 0x000FA8C1 File Offset: 0x000F8AC1
		private void Awake()
		{
			CardRenderer.LoadFontsAsync();
			CardRenderer.prefabIndex++;
			base.transform.position = new Vector3(0f, 200f * (float)CardRenderer.prefabIndex, 0f);
		}

		// Token: 0x0600885B RID: 34907 RVA: 0x000FA8FC File Offset: 0x000F8AFC
		public void SwitchLanguage(string language = null)
		{
			if (!CardRenderer.fontsLoaded)
			{
				return;
			}
			if (language == null)
			{
				language = Language.GetCardConfig();
			}
			if (this.currentFontLanguage == language)
			{
				return;
			}
			this.currentFontLanguage = language;
			this.LoadText(language);
			if (language == "zh-CN")
			{
				this.cardName.fontSize = 50f;
				this.cardNameRD.fontSize = 50f;
				this.spellType.fontSize = 40f;
				this.cardTypeRD.fontSizeMax = 27f;
				this.SetFonts(CardRenderer.fontChineseSimplified, CardRenderer.tmpFontChineseSimplified);
			}
			else if (language == "zh-TW")
			{
				this.cardName.fontSize = 55f;
				this.cardNameRD.fontSize = 55f;
				this.spellType.fontSize = 40f;
				this.cardTypeRD.fontSizeMax = 28f;
				this.SetFonts(CardRenderer.fontChineseTraditional, CardRenderer.tmpFontChineseTraditional);
			}
			else if (language == "ko-KR")
			{
				this.cardName.fontSize = 50f;
				this.cardNameRD.fontSize = 50f;
				this.spellType.fontSize = 40f;
				this.cardTypeRD.fontSizeMax = 27f;
				this.SetFonts(CardRenderer.fontKorean, CardRenderer.tmpFontKorean);
			}
			else if (language == "ja-JP")
			{
				this.cardName.fontSize = 55f;
				this.cardNameRD.fontSize = 55f;
				this.spellType.fontSize = 40f;
				this.cardTypeRD.fontSizeMax = 29f;
				this.SetFonts(CardRenderer.fontJapanese, CardRenderer.tmpFontJapanese);
			}
			else
			{
				this.cardName.fontSize = 63f;
				this.cardNameRD.fontSize = 63f;
				this.spellType.fontSize = 43f;
				this.cardTypeRD.fontSizeMax = 30f;
				this.SetFonts(CardRenderer.fontEnglish, CardRenderer.tmpFontEnglish);
			}
			if (Language.CardUseLatin())
			{
				this.cardName.fontStyle = FontStyles.SmallCaps;
				this.cardNameRD.fontStyle = FontStyles.SmallCaps;
				return;
			}
			this.cardName.fontStyle = FontStyles.Normal;
			this.cardNameRD.fontStyle = FontStyles.Normal;
		}

		// Token: 0x0600885C RID: 34908 RVA: 0x000FAB48 File Offset: 0x000F8D48
		public static bool NeedRushDuelStyle(int code)
		{
			return Config.Get("CardStyle", CardRenderer.CardStyle.OCG_TCG.ToString()) == CardRenderer.CardStyle.RUSH_DUEL.ToString() || (code >= 120000000 && code < 130000000);
		}

		// Token: 0x0600885D RID: 34909 RVA: 0x000FAB9C File Offset: 0x000F8D9C
		public void RenderName(int code)
		{
			Card data = CardsManager.GetRenderCard(code);
			if (data.Id == 0)
			{
				return;
			}
			if (data.isPre)
			{
				this.SwitchLanguage(Language.GetPrereleaseConfig());
			}
			else
			{
				this.SwitchLanguage(null);
			}
			if (CardRenderer.NeedRushDuelStyle(code))
			{
				this.SetRushDuelCardName(data);
			}
			else
			{
				this.SetOcgCardName(data);
			}
			this.renderCamera.Render();
		}

		// Token: 0x0600885E RID: 34910 RVA: 0x000FABF8 File Offset: 0x000F8DF8
		private void SetRushDuelCardName(Card data)
		{
			this.ocg.SetActive(false);
			this.rd.SetActive(true);
			this.cardNameRD.GetComponent<RectTransform>().localScale = Vector3.one;
			this.cardNameRD.text = data.Name;
			this.cardNameRD.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
			float nameWidth = this.cardNameRD.GetComponent<RectTransform>().rect.width;
			if (nameWidth > CardRenderer.cardNameLabelWidthRushDuel)
			{
				this.cardNameRD.GetComponent<RectTransform>().localScale = new Vector3(CardRenderer.cardNameLabelWidthRushDuel / nameWidth, 1f, 1f);
			}
			this.cardNameRD.color = Color.white;
			this.attrRubyRD.text = this.GetAttributeText(data);
			this.cardArtRD.gameObject.SetActive(false);
			this.cardArtPendulumRD.gameObject.SetActive(false);
			this.cardArtPendulumWidthRD.gameObject.SetActive(false);
			this.cardFrameRD.gameObject.SetActive(false);
			this.attrIconRD.gameObject.SetActive(false);
			this.cardLegendRD.SetActive(false);
		}

		// Token: 0x0600885F RID: 34911 RVA: 0x000FAD20 File Offset: 0x000F8F20
		private void SetOcgCardName(Card data)
		{
			this.ocg.SetActive(true);
			this.rd.SetActive(false);
			this.cardName.GetComponent<RectTransform>().localScale = Vector3.one;
			this.cardName.text = data.Name;
			this.cardName.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
			float nameWidth = this.cardName.GetComponent<RectTransform>().rect.width;
			if (nameWidth > CardRenderer.cardNameLabelWidthOCG)
			{
				this.cardName.GetComponent<RectTransform>().localScale = new Vector3(CardRenderer.cardNameLabelWidthOCG / nameWidth, 1f, 1f);
			}
			this.cardName.color = Color.white;
			this.attrRuby.text = this.GetAttributeText(data);
			this.cardFrame.gameObject.SetActive(false);
			this.cardArt.gameObject.SetActive(false);
			this.cardArtPendulum.gameObject.SetActive(false);
			this.cardArtPendulumSquare.gameObject.SetActive(false);
			this.cardArtPendulumWidth.gameObject.SetActive(false);
			this.levels.SetActive(false);
			this.ranks.SetActive(false);
			this.rank13.SetActive(false);
			this.attrIcon.gameObject.SetActive(false);
			this.levelsMask.SetActive(false);
			this.ranksMask.SetActive(false);
			this.rank13Mask.SetActive(false);
			this.linkMarkers.SetActive(false);
			this.spellType.text = string.Empty;
			data = CardRenderer.AdjustLevelForRender(data);
			if (!data.HasType(CardType.Xyz))
			{
				if (data.HasType(CardType.Monster) && !data.HasType(CardType.Link))
				{
					this.levelsMask.SetActive(true);
					for (int i = 0; i < 12; i++)
					{
						if (i < data.Level)
						{
							this.levelsMask.transform.GetChild(i).gameObject.SetActive(true);
						}
						else
						{
							this.levelsMask.transform.GetChild(i).gameObject.SetActive(false);
						}
					}
				}
				return;
			}
			if (data.Level == 13)
			{
				this.rank13Mask.SetActive(true);
				return;
			}
			this.ranksMask.SetActive(true);
			for (int j = 0; j < 12; j++)
			{
				if (j < data.Level)
				{
					this.ranksMask.transform.GetChild(j).gameObject.SetActive(true);
				}
				else
				{
					this.ranksMask.transform.GetChild(j).gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06008860 RID: 34912 RVA: 0x000FAFAC File Offset: 0x000F91AC
		public bool RenderCard(int code, Texture2D art)
		{
			Card data = CardsManager.GetRenderCard(code);
			if (data == null || data.Id == 0)
			{
				return false;
			}
			if (data.isPre)
			{
				this.SwitchLanguage(Language.GetPrereleaseConfig());
			}
			else
			{
				this.SwitchLanguage(null);
			}
			if (CardRenderer.NeedRushDuelStyle(code))
			{
				this.SetRushDuelCard(data, art);
			}
			else
			{
				this.SetOcgCard(data, art);
			}
			this.renderCamera.Render();
			return true;
		}

		// Token: 0x06008861 RID: 34913 RVA: 0x000FB010 File Offset: 0x000F9210
		private void SetRushDuelCard(Card data, Texture2D art)
		{
			this.ocg.SetActive(false);
			this.rd.SetActive(true);
			if (Settings.Data.CardRenderPassword)
			{
				this.cardPasswordRD.text = data.Id.ToString("D8");
			}
			else
			{
				this.cardPasswordRD.text = string.Empty;
			}
			this.cardNameRD.GetComponent<RectTransform>().localScale = Vector3.one;
			this.cardNameRD.text = data.Name;
			this.cardNameRD.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
			float nameWidth = this.cardNameRD.GetComponent<RectTransform>().rect.width;
			if (nameWidth > CardRenderer.cardNameLabelWidthRushDuel)
			{
				this.cardNameRD.GetComponent<RectTransform>().localScale = new Vector3(CardRenderer.cardNameLabelWidthRushDuel / nameWidth, 1f, 1f);
			}
			this.cardNameRD.color = Color.black;
			this.cardTypeRD.color = Color.black;
			this.cardArtRD.gameObject.SetActive(false);
			this.cardArtPendulumRD.gameObject.SetActive(false);
			this.cardArtPendulumWidthRD.gameObject.SetActive(false);
			this.cardFrameRD.gameObject.SetActive(true);
			this.attrIconRD.gameObject.SetActive(true);
			this.cardDescriptionPendulumRD.text = string.Empty;
			this.lScaleRD.text = string.Empty;
			this.rScaleRD.text = string.Empty;
			this.levelRD.SetActive(false);
			this.rankRD.SetActive(false);
			this.linkRD.SetActive(false);
			this.levelNumRD.gameObject.SetActive(false);
			this.rankNumRD.gameObject.SetActive(false);
			this.atkNumRD.text = data.GetAttackString();
			this.defNumRD.text = data.GetDefenseString();
			this.atkRD.SetActive(true);
			this.defRD.SetActive(true);
			this.movePartsRD.gameObject.SetActive(true);
			this.movePartsRD.anchoredPosition = Vector2.zero;
			this.attrIconRD.sprite = TextureManager.container.GetCardAttributeIcon(data, true);
			this.attrRubyRD.text = this.GetAttributeText(data);
			this.cardTypeRD.text = data.GetTypeForRushDuelRender();
			if (data.HasType(CardType.Pendulum))
			{
				this.movePartsRD.anchoredPosition = new Vector2(0f, 133f);
				if (art.width == art.height)
				{
					this.cardArtRD.gameObject.SetActive(true);
					this.cardArtRD.texture = art;
				}
				else if (art.width > art.height)
				{
					this.cardArtPendulumWidthRD.gameObject.SetActive(true);
					this.cardArtPendulumWidthRD.texture = art;
				}
				else
				{
					this.cardArtPendulumRD.gameObject.SetActive(true);
					this.cardArtPendulumRD.texture = art;
				}
				this.cardDescriptionPendulumRD.text = this.TextForRender(data.GetPendulumDescription(true), data.isPre);
				List<string> authorSplit = CardRenderer.GetAuthorFromDescription(data.GetMonsterDescription(true));
				this.cardAutherRD.text = authorSplit[1];
				this.cardDescriptionRD.text = this.TextForRender(authorSplit[0], data.isPre);
				this.lScaleRD.text = data.LScale.ToString();
				this.rScaleRD.text = data.RScale.ToString();
				if (data.HasType(CardType.Xyz))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_PendulumXyz;
				}
				else if (data.HasType(CardType.Synchro))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_PendulumSynchro;
				}
				else if (data.HasType(CardType.Fusion))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_PendulumFusion;
				}
				else if (data.HasType(CardType.Ritual))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_PendulumRitual;
				}
				else if (data.HasType(CardType.Link))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_PendulumLink;
				}
				else if (data.HasType(CardType.Normal))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_PendulumNormal;
				}
				else
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_PendulumEffect;
				}
			}
			else
			{
				this.cardArtRD.gameObject.SetActive(true);
				this.cardArtRD.texture = art;
				List<string> authorSplit2 = CardRenderer.GetAuthorFromDescription(data.Desc);
				this.cardDescriptionRD.text = this.TextForRender(authorSplit2[0], data.isPre);
				this.cardAutherRD.text = this.TextForRender(authorSplit2[1], data.isPre);
				this.cardDescriptionPendulumRD.text = string.Empty;
				if (data.Id == 10000000)
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Obelisk;
				}
				else if (data.Id == 10000010)
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Ra;
				}
				else if (data.Id == 10000020)
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Slifer;
				}
				else if (data.HasType(CardType.Link))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Link;
				}
				else if (data.HasType(CardType.Xyz))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Xyz;
				}
				else if (data.HasType(CardType.Synchro))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Synchro;
				}
				else if (data.HasType(CardType.Fusion))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Fusion;
				}
				else if (data.HasType(CardType.Ritual) && data.HasType(CardType.Monster))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Ritual;
				}
				else if (data.HasType(CardType.Token))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Token;
				}
				else if (data.HasType(CardType.Normal))
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Normal;
				}
				else if (((long)data.Type & 6L) > 0L)
				{
					this.atkRD.SetActive(false);
					this.defRD.SetActive(false);
					this.atkNumRD.text = string.Empty;
					this.defNumRD.text = string.Empty;
					if (data.HasType(CardType.Spell))
					{
						this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Spell;
					}
					else
					{
						this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Trap;
					}
				}
				else
				{
					this.cardFrameRD.sprite = TextureManager.container.rd_Frame_Effect;
				}
			}
			data = CardRenderer.AdjustLevelForRender(data);
			if (data.HasType(CardType.Link))
			{
				this.cardNameRD.color = Color.white;
				this.defRD.SetActive(false);
				this.defNumRD.text = string.Empty;
				this.levelNumRD.gameObject.SetActive(true);
				this.levelNumRD.text = data.GetLinkCount().ToString();
				this.linkRD.SetActive(true);
				for (int i = 0; i < 8; i++)
				{
					if (i < 4)
					{
						if ((data.LinkMarker & (1 << i)) > 0)
						{
							this.linkRD.transform.GetChild(i).gameObject.SetActive(true);
						}
						else
						{
							this.linkRD.transform.GetChild(i).gameObject.SetActive(false);
						}
					}
					else if ((data.LinkMarker & (1 << i + 1)) > 0)
					{
						this.linkRD.transform.GetChild(i).gameObject.SetActive(true);
					}
					else
					{
						this.linkRD.transform.GetChild(i).gameObject.SetActive(false);
					}
				}
				return;
			}
			if (data.HasType(CardType.Xyz))
			{
				this.cardNameRD.color = Color.white;
				if (!data.HasType(CardType.Pendulum))
				{
					this.cardTypeRD.color = Color.white;
				}
				this.rankRD.SetActive(true);
				this.rankNumRD.gameObject.SetActive(true);
				this.rankNumRD.text = data.Level.ToString();
				return;
			}
			if (data.HasType(CardType.Monster))
			{
				this.levelRD.SetActive(true);
				this.levelNumRD.gameObject.SetActive(true);
				this.levelNumRD.text = data.Level.ToString();
			}
		}

		// Token: 0x06008862 RID: 34914 RVA: 0x000FB900 File Offset: 0x000F9B00
		private void SetOcgCard(Card data, Texture2D art)
		{
			this.ocg.SetActive(true);
			this.rd.SetActive(false);
			if (Settings.Data.CardRenderPassword)
			{
				this.cardPassword.text = data.Id.ToString("D8");
			}
			else
			{
				this.cardPassword.text = string.Empty;
			}
			this.cardName.GetComponent<RectTransform>().localScale = Vector3.one;
			this.cardName.text = data.Name;
			this.cardName.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
			float nameWidth = this.cardName.GetComponent<RectTransform>().rect.width;
			if (nameWidth > CardRenderer.cardNameLabelWidthOCG)
			{
				this.cardName.GetComponent<RectTransform>().localScale = new Vector3(CardRenderer.cardNameLabelWidthOCG / nameWidth, 1f, 1f);
			}
			this.cardName.color = Color.black;
			this.cardPassword.color = Color.black;
			this.cardAuther.color = Color.black;
			this.cardArt.gameObject.SetActive(false);
			this.cardArtPendulum.gameObject.SetActive(false);
			this.cardArtPendulumSquare.gameObject.SetActive(false);
			this.cardArtPendulumWidth.gameObject.SetActive(false);
			this.cardFrame.gameObject.SetActive(true);
			this.attrIcon.gameObject.SetActive(true);
			this.cardDescriptionPendulum.text = string.Empty;
			this.lScale.text = string.Empty;
			this.rScale.text = string.Empty;
			this.levels.SetActive(false);
			this.ranks.SetActive(false);
			this.rank13.SetActive(false);
			this.levelsMask.SetActive(false);
			this.ranksMask.SetActive(false);
			this.rank13Mask.SetActive(false);
			this.linkMarkers.SetActive(false);
			this.line.SetActive(true);
			this.textATK.SetActive(true);
			this.textDEF.SetActive(true);
			this.numATK.text = ((data.Attack == -2) ? "?" : data.Attack.ToString());
			this.numDEF.text = ((data.Defense == -2) ? "?" : data.Defense.ToString());
			this.linkCount.gameObject.SetActive(false);
			this.spellType.text = string.Empty;
			this.cardDescription.GetComponent<RectTransform>().sizeDelta = new Vector2(590f, 160f);
			this.attrIcon.sprite = TextureManager.container.GetCardAttributeIcon(data, true);
			this.attrRuby.text = this.GetAttributeText(data);
			if (data.HasType(CardType.Pendulum))
			{
				if (art.width == art.height)
				{
					this.cardArtPendulumSquare.gameObject.SetActive(true);
					this.cardArtPendulumSquare.texture = art;
				}
				else if (art.width > art.height)
				{
					this.cardArtPendulumWidth.gameObject.SetActive(true);
					this.cardArtPendulumWidth.texture = art;
				}
				else
				{
					this.cardArtPendulum.gameObject.SetActive(true);
					this.cardArtPendulum.texture = art;
				}
				string[] pendulumDescription = data.GetDescriptionSplit(true);
				this.cardDescription.text = data.GetTypeForRushDuelRender();
				this.cardDescriptionPendulum.text = this.TextForRender(pendulumDescription[0], data.isPre);
				List<string> authorSplit = CardRenderer.GetAuthorFromDescription(pendulumDescription[1]);
				Text text = this.cardDescription;
				text.text = text.text + "\r\n" + this.TextForRender(authorSplit[0], data.isPre);
				this.cardAuther.text = authorSplit[1];
				this.lScale.text = data.LScale.ToString();
				this.rScale.text = data.RScale.ToString();
				if (data.HasType(CardType.Xyz))
				{
					this.cardFrame.sprite = TextureManager.container.cardFramePendulumXyzOF;
				}
				else if (data.HasType(CardType.Synchro))
				{
					this.cardFrame.sprite = TextureManager.container.cardFramePendulumSynchroOF;
				}
				else if (data.HasType(CardType.Fusion))
				{
					this.cardFrame.sprite = TextureManager.container.cardFramePendulumFusionOF;
				}
				else if (data.HasType(CardType.Ritual))
				{
					this.cardFrame.sprite = TextureManager.container.cardFramePendulumRitualOF;
				}
				else if (data.HasType(CardType.Normal))
				{
					this.cardFrame.sprite = TextureManager.container.cardFramePendulumNormalOF;
				}
				else
				{
					this.cardFrame.sprite = TextureManager.container.cardFramePendulumEffectOF;
				}
			}
			else
			{
				this.cardArt.gameObject.SetActive(true);
				this.cardArt.texture = art;
				string description = string.Empty;
				if (data.HasType(CardType.Monster))
				{
					description = data.GetTypeForRushDuelRender() + "\r\n";
				}
				List<string> authorSplit2 = CardRenderer.GetAuthorFromDescription(data.Desc);
				description += this.TextForRender(authorSplit2[0], data.isPre);
				this.cardDescription.text = description;
				this.cardAuther.text = authorSplit2[1];
				if (data.Id == 10000000)
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameObeliskOF;
				}
				else if (data.Id == 10000010)
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameRaOF;
				}
				else if (data.Id == 10000020)
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameOsirisOF;
				}
				else if (data.HasType(CardType.Link))
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameLinkOF;
				}
				else if (data.HasType(CardType.Xyz))
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameXyzOF;
				}
				else if (data.HasType(CardType.Synchro))
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameSynchroOF;
				}
				else if (data.HasType(CardType.Fusion))
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameFusionOF;
				}
				else if (data.HasType(CardType.Ritual) && data.HasType(CardType.Monster))
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameRitualOF;
				}
				else if (data.HasType(CardType.Token))
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameTokenOF;
				}
				else if (data.HasType(CardType.Normal))
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameNormalOF;
				}
				else if (((long)data.Type & 6L) > 0L)
				{
					this.cardDescription.GetComponent<RectTransform>().sizeDelta = new Vector2(590f, 185f);
					this.cardName.color = Color.white;
					this.line.SetActive(false);
					this.textATK.SetActive(false);
					this.textDEF.SetActive(false);
					this.numATK.text = string.Empty;
					this.numDEF.text = string.Empty;
					this.spellType.text = data.GetSpellTypeForOCGRender();
					if (data.HasType(CardType.Spell))
					{
						this.cardFrame.sprite = TextureManager.container.cardFrameSpellOF;
					}
					else
					{
						this.cardFrame.sprite = TextureManager.container.cardFrameTrapOF;
					}
				}
				else
				{
					this.cardFrame.sprite = TextureManager.container.cardFrameEffectOF;
				}
			}
			data = CardRenderer.AdjustLevelForRender(data);
			if (data.HasType(CardType.Link))
			{
				this.cardName.color = Color.white;
				this.linkMarkers.SetActive(true);
				this.textDEF.SetActive(false);
				this.numDEF.text = string.Empty;
				this.linkCount.gameObject.SetActive(true);
				switch (data.GetLinkCount())
				{
				case 1:
					this.linkCount.sprite = TextureManager.container.link1R;
					break;
				case 2:
					this.linkCount.sprite = TextureManager.container.link2R;
					break;
				case 3:
					this.linkCount.sprite = TextureManager.container.link3R;
					break;
				case 4:
					this.linkCount.sprite = TextureManager.container.link4R;
					break;
				case 5:
					this.linkCount.sprite = TextureManager.container.link5R;
					break;
				case 6:
					this.linkCount.sprite = TextureManager.container.link6R;
					break;
				case 7:
					this.linkCount.sprite = TextureManager.container.link7R;
					break;
				case 8:
					this.linkCount.sprite = TextureManager.container.link8R;
					break;
				}
				for (int i = 0; i < 8; i++)
				{
					if (i < 4)
					{
						if ((data.LinkMarker & (1 << i)) > 0)
						{
							this.linkMarkers.transform.GetChild(i).gameObject.SetActive(true);
						}
						else
						{
							this.linkMarkers.transform.GetChild(i).gameObject.SetActive(false);
						}
					}
					else if ((data.LinkMarker & (1 << i + 1)) > 0)
					{
						this.linkMarkers.transform.GetChild(i).gameObject.SetActive(true);
					}
					else
					{
						this.linkMarkers.transform.GetChild(i).gameObject.SetActive(false);
					}
				}
				return;
			}
			if (!data.HasType(CardType.Xyz))
			{
				if (data.HasType(CardType.Monster))
				{
					this.levels.SetActive(true);
					for (int j = 0; j < 12; j++)
					{
						if (j < data.Level)
						{
							this.levels.transform.GetChild(j).gameObject.SetActive(true);
						}
						else
						{
							this.levels.transform.GetChild(j).gameObject.SetActive(false);
						}
					}
				}
				return;
			}
			this.cardName.color = Color.white;
			if (!data.HasType(CardType.Pendulum))
			{
				this.cardPassword.color = Color.white;
				this.cardAuther.color = Color.white;
			}
			if (data.Level == 13)
			{
				this.rank13.SetActive(true);
				return;
			}
			this.ranks.SetActive(true);
			for (int k = 0; k < 12; k++)
			{
				if (k < data.Level)
				{
					this.ranks.transform.GetChild(k).gameObject.SetActive(true);
				}
				else
				{
					this.ranks.transform.GetChild(k).gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06008863 RID: 34915 RVA: 0x000FC40C File Offset: 0x000FA60C
		private static Card AdjustLevelForRender(Card data)
		{
			int code = data.Id;
			if (code == 1686814)
			{
				data.Level = 0;
			}
			else if (code == 90884403)
			{
				data.Level = 0;
			}
			else if (code == 26973555)
			{
				data.Level = 0;
			}
			else if (code == 43490025)
			{
				data.Level = 0;
			}
			else if (code == 65305468)
			{
				data.Level = 0;
			}
			else if (code == 52653092)
			{
				data.Level = 0;
			}
			return data;
		}

		// Token: 0x06008864 RID: 34916 RVA: 0x000FC488 File Offset: 0x000FA688
		private string TextForRender(string description, bool isPre)
		{
			if (string.IsNullOrEmpty(description))
			{
				return string.Empty;
			}
			object obj = (isPre ? Language.GetPrereleaseConfig() : Language.GetCardConfig());
			description = description.Replace("\t\r\n", "\f\f\f");
			description = description.Replace("\r\n●", "●●●");
			description = description.Replace("\r", string.Empty);
			description = description.Replace("\n", string.Empty);
			description = description.Replace("\f\f\f", "\r\n");
			description = description.Replace("●●●", "\r\n●");
			object obj2 = obj;
			if (!Language.UseLatin(obj2))
			{
				description = description.Replace("/", "／");
			}
			else
			{
				description = description.Replace("/", " / ");
			}
			if (!Language.UseLatin(obj2))
			{
				description = description.Replace(" ", "\u00a0");
			}
			description = description.Replace("\r\n\r\n", "\r\n");
			return description;
		}

		// Token: 0x06008865 RID: 34917 RVA: 0x000FC578 File Offset: 0x000FA778
		private static List<string> GetAuthorFromDescription(string description)
		{
			string[] array = description.Split("\r\n", StringSplitOptions.None);
			List<string> returnValue = new List<string>();
			StringBuilder beforeDiySymbol = new StringBuilder();
			bool foundDIY = false;
			foreach (string line in array)
			{
				if (!foundDIY && line.StartsWith(Settings.Data.DiySymbol))
				{
					string beforeDiySymbolText = beforeDiySymbol.ToString();
					returnValue.Add(beforeDiySymbolText);
					returnValue.Add(line);
					foundDIY = true;
				}
				else if (!foundDIY && !string.IsNullOrEmpty(line))
				{
					beforeDiySymbol.Append(line);
				}
				if (foundDIY)
				{
					break;
				}
			}
			if (!foundDIY)
			{
				returnValue.Add(description);
				returnValue.Add(string.Empty);
			}
			return returnValue;
		}

		// Token: 0x06008866 RID: 34918 RVA: 0x000FC618 File Offset: 0x000FA818
		public static bool CardHasVideoArt(int code)
		{
			return Config.GetBool("VideoCard", true) && File.Exists("Video/Art/" + code.ToString() + ".mp4");
		}

		// Token: 0x06008867 RID: 34919 RVA: 0x000FC649 File Offset: 0x000FA849
		private static string GetVideoURL(int code)
		{
			return Tools.FormatPlatformUrl(Tools.GetPlatformPath("Video/Art/" + code.ToString() + ".mp4"));
		}

		// Token: 0x06008868 RID: 34920 RVA: 0x000FC66C File Offset: 0x000FA86C
		public async UniTask<Texture> GetVideoCardAsync(int code)
		{
			Texture texture;
			if (!CardRenderer.CardHasVideoArt(code))
			{
				texture = null;
			}
			else
			{
				Card data = CardsManager.GetRenderCard(code);
				if (data == null || data.Id == 0)
				{
					texture = null;
				}
				else
				{
					if (data.isPre)
					{
						this.SwitchLanguage(Language.GetPrereleaseConfig());
					}
					else
					{
						this.SwitchLanguage(null);
					}
					bool flag = CardRenderer.NeedRushDuelStyle(data.Id);
					bool isPendulum = data.HasType(CardType.Pendulum);
					if (flag)
					{
						this.SetRushDuelCard(data, null);
						this.cardArtRD.gameObject.SetActive(false);
						this.cardArtPendulumRD.gameObject.SetActive(false);
						this.cardArtPendulumWidthRD.gameObject.SetActive(false);
					}
					else
					{
						this.SetOcgCard(data, null);
						this.cardArt.gameObject.SetActive(false);
						this.cardArtPendulum.gameObject.SetActive(false);
						this.cardArtPendulumSquare.gameObject.SetActive(false);
						this.cardArtPendulumWidth.gameObject.SetActive(false);
					}
					this.videoPlayer.gameObject.SetActive(true);
					this.videoPlayer.url = CardRenderer.GetVideoURL(code);
					this.videoPlayer.targetTexture = global::UnityEngine.Object.Instantiate<RenderTexture>(this.videoPlayer.targetTexture);
					RawImage targetImage;
					if (flag)
					{
						if (isPendulum)
						{
							targetImage = this.cardArtPendulumRD;
						}
						else
						{
							targetImage = this.cardArtRD;
						}
					}
					else if (isPendulum)
					{
						targetImage = this.cardArtPendulumSquare;
					}
					else
					{
						targetImage = this.cardArt;
					}
					this.renderCamera.Render();
					RenderTexture.active = this.renderTexture;
					Texture2D onlyFrame = new Texture2D(RenderTexture.active.width, RenderTexture.active.height, TextureFormat.RGBA32, true);
					onlyFrame.ReadPixels(new Rect(0f, 0f, (float)RenderTexture.active.width, (float)RenderTexture.active.height), 0, 0);
					onlyFrame.Apply();
					onlyFrame.name = "Card_" + code.ToString();
					this.renderedCardFrame.texture = onlyFrame;
					this.renderedCardFrame.gameObject.SetActive(true);
					targetImage.gameObject.SetActive(true);
					targetImage.texture = this.videoPlayer.targetTexture;
					targetImage.transform.SetParent(base.transform);
					this.renderedCardFrame.transform.SetAsLastSibling();
					global::UnityEngine.Object.Destroy(this.ocg);
					global::UnityEngine.Object.Destroy(this.rd);
					this.videoPlayer.Prepare();
					await UniTask.WaitUntil(() => this.videoPlayer.isPrepared, PlayerLoopTiming.Update, default(CancellationToken), false);
					this.renderCamera.gameObject.SetActive(true);
					this.renderCamera.targetTexture = global::UnityEngine.Object.Instantiate<RenderTexture>(this.renderTexture);
					this.renderCamera.SetVolumeFrameworkUpdateMode(VolumeFrameworkUpdateMode.EveryFrame);
					this.renderTexture = this.renderCamera.targetTexture;
					texture = this.renderTexture;
				}
			}
			return texture;
		}

		// Token: 0x06008869 RID: 34921 RVA: 0x000FC6B7 File Offset: 0x000FA8B7
		public void PauseVideo()
		{
			this.renderCamera.gameObject.SetActive(false);
			this.videoPlayer.Pause();
		}

		// Token: 0x0600886A RID: 34922 RVA: 0x000FC6D5 File Offset: 0x000FA8D5
		public void PlayVideo()
		{
			this.renderCamera.gameObject.SetActive(true);
			this.videoPlayer.Play();
		}

		// Token: 0x0600886B RID: 34923 RVA: 0x000FC6F3 File Offset: 0x000FA8F3
		public void Dispose()
		{
			global::UnityEngine.Object.Destroy(this.renderTexture);
			global::UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600886C RID: 34924 RVA: 0x000FC70C File Offset: 0x000FA90C
		private void LoadText(string language)
		{
			this.idsSysText.Clear();
			string path = "Data/locales/" + language + "/IDS/IDS_SYS.txt";
			if (!File.Exists(path))
			{
				return;
			}
			string[] array = File.ReadAllText(path).Replace("\r", string.Empty).Split('\n', StringSplitOptions.None);
			string currentKey = null;
			string currentValue = null;
			foreach (string line in array)
			{
				Match match = Regex.Match(line, "(?<=\\[IDS_SYS\\.).*?(?=\\])");
				if (match.Success)
				{
					if (currentValue != null)
					{
						this.idsSysText[currentKey] = currentValue;
					}
					currentKey = match.Value;
				}
				else
				{
					currentValue = line;
				}
			}
			if (currentKey != null && currentValue != null)
			{
				this.idsSysText[currentKey] = currentValue;
			}
		}

		// Token: 0x0600886D RID: 34925 RVA: 0x000FC7C0 File Offset: 0x000FA9C0
		private string GetIdsSysText(string key)
		{
			string value;
			if (this.idsSysText.TryGetValue(key, out value))
			{
				return value;
			}
			return string.Empty;
		}

		// Token: 0x0600886E RID: 34926 RVA: 0x000FC7E4 File Offset: 0x000FA9E4
		private string GetAttributeText(Card data)
		{
			if (data.HasType(CardType.Spell))
			{
				return this.GetIdsSysText("ATTR_MAGIC_RUBY");
			}
			if (data.HasType(CardType.Trap))
			{
				return this.GetIdsSysText("ATTR_TRAP_RUBY");
			}
			if (data.IsAttribute(CardAttribute.Light))
			{
				return this.GetIdsSysText("ATTR_LIGHT_RUBY");
			}
			if (data.IsAttribute(CardAttribute.Dark))
			{
				return this.GetIdsSysText("ATTR_DARK_RUBY");
			}
			if (data.IsAttribute(CardAttribute.Water))
			{
				return this.GetIdsSysText("ATTR_WATER_RUBY");
			}
			if (data.IsAttribute(CardAttribute.Fire))
			{
				return this.GetIdsSysText("ATTR_FIRE_RUBY");
			}
			if (data.IsAttribute(CardAttribute.Earth))
			{
				return this.GetIdsSysText("ATTR_EARTH_RUBY");
			}
			if (data.IsAttribute(CardAttribute.Wind))
			{
				return this.GetIdsSysText("ATTR_WIND_RUBY");
			}
			if (data.IsAttribute(CardAttribute.Divine))
			{
				return this.GetIdsSysText("ATTR_GOD_RUBY");
			}
			return string.Empty;
		}

		// Token: 0x0400C333 RID: 49971
		public const string BIG_SLASH = "／";

		// Token: 0x0400C334 RID: 49972
		public const string SMALL_SLASH = " / ";

		// Token: 0x0400C335 RID: 49973
		private static readonly float cardNameLabelWidthOCG = 520f;

		// Token: 0x0400C336 RID: 49974
		private static readonly float cardNameLabelWidthRushDuel = 520f;

		// Token: 0x0400C337 RID: 49975
		private string currentFontLanguage;

		// Token: 0x0400C338 RID: 49976
		private static bool fontsLoaded;

		// Token: 0x0400C339 RID: 49977
		private static int prefabIndex = 0;

		// Token: 0x0400C33A RID: 49978
		[Header("CardRenderer")]
		[SerializeField]
		private GameObject ocg;

		// Token: 0x0400C33B RID: 49979
		[SerializeField]
		private GameObject rd;

		// Token: 0x0400C33C RID: 49980
		[SerializeField]
		private Camera renderCamera;

		// Token: 0x0400C33D RID: 49981
		[SerializeField]
		private VideoPlayer videoPlayer;

		// Token: 0x0400C33E RID: 49982
		[SerializeField]
		private RawImage renderedCardFrame;

		// Token: 0x0400C33F RID: 49983
		public RenderTexture renderTexture;

		// Token: 0x0400C340 RID: 49984
		[Header("OCG")]
		public RawImage cardArt;

		// Token: 0x0400C341 RID: 49985
		public RawImage cardArtPendulum;

		// Token: 0x0400C342 RID: 49986
		public RawImage cardArtPendulumSquare;

		// Token: 0x0400C343 RID: 49987
		public RawImage cardArtPendulumWidth;

		// Token: 0x0400C344 RID: 49988
		public Image cardFrame;

		// Token: 0x0400C345 RID: 49989
		public Image attrIcon;

		// Token: 0x0400C346 RID: 49990
		public TextMeshProUGUI attrRuby;

		// Token: 0x0400C347 RID: 49991
		public TextMeshProUGUI cardName;

		// Token: 0x0400C348 RID: 49992
		public Text cardDescription;

		// Token: 0x0400C349 RID: 49993
		public Text cardDescriptionPendulum;

		// Token: 0x0400C34A RID: 49994
		public Text lScale;

		// Token: 0x0400C34B RID: 49995
		public Text rScale;

		// Token: 0x0400C34C RID: 49996
		public GameObject levels;

		// Token: 0x0400C34D RID: 49997
		public GameObject ranks;

		// Token: 0x0400C34E RID: 49998
		public GameObject rank13;

		// Token: 0x0400C34F RID: 49999
		public GameObject levelsMask;

		// Token: 0x0400C350 RID: 50000
		public GameObject ranksMask;

		// Token: 0x0400C351 RID: 50001
		public GameObject rank13Mask;

		// Token: 0x0400C352 RID: 50002
		public GameObject linkMarkers;

		// Token: 0x0400C353 RID: 50003
		public GameObject line;

		// Token: 0x0400C354 RID: 50004
		public GameObject textATK;

		// Token: 0x0400C355 RID: 50005
		public GameObject textDEF;

		// Token: 0x0400C356 RID: 50006
		public Text numATK;

		// Token: 0x0400C357 RID: 50007
		public Text numDEF;

		// Token: 0x0400C358 RID: 50008
		public Image linkCount;

		// Token: 0x0400C359 RID: 50009
		public TextMeshProUGUI spellType;

		// Token: 0x0400C35A RID: 50010
		public Font atkDef;

		// Token: 0x0400C35B RID: 50011
		public Text cardPassword;

		// Token: 0x0400C35C RID: 50012
		public Text cardAuther;

		// Token: 0x0400C35D RID: 50013
		[Header("RD")]
		public RawImage cardArtRD;

		// Token: 0x0400C35E RID: 50014
		public RawImage cardArtPendulumRD;

		// Token: 0x0400C35F RID: 50015
		public RawImage cardArtPendulumWidthRD;

		// Token: 0x0400C360 RID: 50016
		public Image cardFrameRD;

		// Token: 0x0400C361 RID: 50017
		public Image attrIconRD;

		// Token: 0x0400C362 RID: 50018
		public TextMeshProUGUI attrRubyRD;

		// Token: 0x0400C363 RID: 50019
		public GameObject cardLegendRD;

		// Token: 0x0400C364 RID: 50020
		public RectTransform movePartsRD;

		// Token: 0x0400C365 RID: 50021
		public TextMeshProUGUI cardNameRD;

		// Token: 0x0400C366 RID: 50022
		public TextMeshProUGUI cardTypeRD;

		// Token: 0x0400C367 RID: 50023
		public Text cardDescriptionRD;

		// Token: 0x0400C368 RID: 50024
		public Text cardDescriptionPendulumRD;

		// Token: 0x0400C369 RID: 50025
		public Text lScaleRD;

		// Token: 0x0400C36A RID: 50026
		public Text rScaleRD;

		// Token: 0x0400C36B RID: 50027
		public GameObject maxAtkRD;

		// Token: 0x0400C36C RID: 50028
		public TextMeshProUGUI maxAtkNumRD;

		// Token: 0x0400C36D RID: 50029
		public GameObject atkRD;

		// Token: 0x0400C36E RID: 50030
		public TextMeshProUGUI atkNumRD;

		// Token: 0x0400C36F RID: 50031
		public GameObject defRD;

		// Token: 0x0400C370 RID: 50032
		public TextMeshProUGUI defNumRD;

		// Token: 0x0400C371 RID: 50033
		public GameObject levelRD;

		// Token: 0x0400C372 RID: 50034
		public TextMeshProUGUI levelNumRD;

		// Token: 0x0400C373 RID: 50035
		public GameObject rankRD;

		// Token: 0x0400C374 RID: 50036
		public TextMeshProUGUI rankNumRD;

		// Token: 0x0400C375 RID: 50037
		public GameObject linkRD;

		// Token: 0x0400C376 RID: 50038
		public GameObject linkUL;

		// Token: 0x0400C377 RID: 50039
		public GameObject linkU;

		// Token: 0x0400C378 RID: 50040
		public GameObject linkUR;

		// Token: 0x0400C379 RID: 50041
		public GameObject linkR;

		// Token: 0x0400C37A RID: 50042
		public GameObject linkBR;

		// Token: 0x0400C37B RID: 50043
		public GameObject linkB;

		// Token: 0x0400C37C RID: 50044
		public GameObject linkBL;

		// Token: 0x0400C37D RID: 50045
		public GameObject linkL;

		// Token: 0x0400C37E RID: 50046
		public Text cardPasswordRD;

		// Token: 0x0400C37F RID: 50047
		public Text cardAutherRD;

		// Token: 0x0400C380 RID: 50048
		private static Font fontChineseSimplified;

		// Token: 0x0400C381 RID: 50049
		private static Font fontChineseTraditional;

		// Token: 0x0400C382 RID: 50050
		private static Font fontKorean;

		// Token: 0x0400C383 RID: 50051
		private static Font fontJapanese;

		// Token: 0x0400C384 RID: 50052
		private static Font fontEnglish;

		// Token: 0x0400C385 RID: 50053
		private static TMP_FontAsset tmpFontChineseSimplified;

		// Token: 0x0400C386 RID: 50054
		private static TMP_FontAsset tmpFontChineseTraditional;

		// Token: 0x0400C387 RID: 50055
		private static TMP_FontAsset tmpFontKorean;

		// Token: 0x0400C388 RID: 50056
		private static TMP_FontAsset tmpFontJapanese;

		// Token: 0x0400C389 RID: 50057
		private static TMP_FontAsset tmpFontEnglish;

		// Token: 0x0400C38A RID: 50058
		private readonly Dictionary<string, string> idsSysText = new Dictionary<string, string>();

		// Token: 0x020011F3 RID: 4595
		public enum CardStyle
		{
			// Token: 0x0400C38C RID: 50060
			OCG_TCG,
			// Token: 0x0400C38D RID: 50061
			RUSH_DUEL
		}
	}
}
