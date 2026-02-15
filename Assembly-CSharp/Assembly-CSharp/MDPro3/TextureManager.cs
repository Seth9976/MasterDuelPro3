using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace MDPro3
{
	// Token: 0x0200126F RID: 4719
	public class TextureManager : Manager
	{
		// Token: 0x06008ACB RID: 35531 RVA: 0x00116660 File Offset: 0x00114860
		public override void Initialize()
		{
			TextureManager.instance = this;
			base.Initialize();
			Addressables.LoadAssetAsync<TextureContainer>("ScriptableObjects/TextureContainer.asset").Completed += delegate(AsyncOperationHandle<TextureContainer> result)
			{
				TextureManager.container = result.Result;
			};
			this.LoadMaterials();
		}

		// Token: 0x06008ACC RID: 35532 RVA: 0x001166B4 File Offset: 0x001148B4
		private async UniTask LoadMaterials()
		{
			await UniTask.WaitUntil(() => TextureManager.container != null, PlayerLoopTiming.Update, default(CancellationToken), false);
			this.commonShopButtonMat = await ABLoader.LoadMaterialAsync("MasterDuel/Material/GUI_CommonShopButton_N", default(CancellationToken));
			this.SetCommonShopButtonMaterial(this.commonShopButtonMat);
			this.commonShopButtonOverMat = await ABLoader.LoadMaterialAsync("MasterDuel/Material/GUI_CommonShopButton_N_Over", default(CancellationToken));
			this.SetCommonShopButtonMaterial(this.commonShopButtonOverMat);
			TextureManager.loaded = true;
		}

		// Token: 0x06008ACD RID: 35533 RVA: 0x001166F8 File Offset: 0x001148F8
		public static async UniTask<Texture2D> LoadPicFromFileAsync(string path)
		{
			Texture2D texture2D;
			if (!File.Exists(path))
			{
				texture2D = null;
			}
			else
			{
				string fullPath = Environment.CurrentDirectory + "/" + path;
				using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(fullPath))
				{
					await request.SendWebRequest().WithCancellation(Application.exitCancellationToken);
					if (request.result == UnityWebRequest.Result.Success)
					{
						texture2D = DownloadHandlerTexture.GetContent(request);
					}
					else
					{
						Debug.LogWarningFormat("Pic File [{0}] not fount.", new object[] { path });
						texture2D = null;
					}
				}
			}
			return texture2D;
		}

		// Token: 0x06008ACE RID: 35534 RVA: 0x0011673C File Offset: 0x0011493C
		public async UniTask LoadCardToRawImageWithoutMaterialAsync(RawImage rawImage, int code, bool cache = true)
		{
			RawImage rawImage2 = rawImage;
			Texture texture = await CardImageLoader.LoadCardAsync(code, cache, rawImage.destroyCancellationToken, false);
			rawImage2.texture = texture;
			rawImage2 = null;
		}

		// Token: 0x06008ACF RID: 35535 RVA: 0x00116790 File Offset: 0x00114990
		public async UniTask LoadCardToRendererWithMaterialAsync(Renderer renderer, int code, bool cache = true)
		{
			Material mat = MaterialLoader.GetCardMaterial(code, true);
			Material material = mat;
			Texture texture = await CardImageLoader.LoadCardAsync(code, cache, renderer.GetCancellationTokenOnDestroy(), false);
			material.mainTexture = texture;
			material = null;
			if (renderer != null)
			{
				renderer.material = mat;
			}
		}

		// Token: 0x06008AD0 RID: 35536 RVA: 0x001167E4 File Offset: 0x001149E4
		public async UniTask LoadDummyCard(ElementObjectManager manager, int code, uint player, bool active = false, Renderer attachRenderer = null, Renderer attachRenderer2 = null)
		{
			if (active)
			{
				manager.gameObject.SetActive(false);
			}
			manager.GetElement<Renderer>("DummyCardModel_back").material = ((player == 0U) ? OcgCore.myProtector : OcgCore.opProtector);
			Renderer renderer = manager.GetElement<Renderer>("DummyCardModel_front");
			renderer.material = MaterialLoader.GetCardMaterial(code, true);
			Material material = renderer.material;
			Texture texture = await CardImageLoader.LoadCardAsync(code, false, manager.destroyCancellationToken, false);
			material.mainTexture = texture;
			material = null;
			if (attachRenderer != null)
			{
				attachRenderer.material.mainTexture = renderer.material.mainTexture;
			}
			if (attachRenderer2 != null)
			{
				attachRenderer2.material.mainTexture = renderer.material.mainTexture;
			}
			if (active)
			{
				manager.gameObject.SetActive(true);
			}
		}

		// Token: 0x06008AD1 RID: 35537 RVA: 0x00116854 File Offset: 0x00114A54
		private void SetCommonShopButtonMaterial(Material mat)
		{
			mat.SetFloat("_NoiseSize", 500f);
			mat.SetFloat("_NoiseSpeed", 0.5f);
			mat.SetVector("_TilingOffset", new Vector4(1f, 1f, 0f, 0f));
			mat.SetVector("_MainTexMinMax", new Vector4(-0.5f, 1f, -0.5f, 1f));
		}

		// Token: 0x06008AD2 RID: 35538 RVA: 0x001168CC File Offset: 0x00114ACC
		public async UniTask SetCommonShopButtonMaterial(Image image, bool hover)
		{
			if (hover)
			{
				await UniTask.WaitUntil(() => this.commonShopButtonOverMat != null, PlayerLoopTiming.Update, image.destroyCancellationToken, false);
				image.material = this.commonShopButtonOverMat;
			}
			else
			{
				await UniTask.WaitUntil(() => this.commonShopButtonMat != null, PlayerLoopTiming.Update, image.destroyCancellationToken, false);
				image.material = this.commonShopButtonMat;
			}
		}

		// Token: 0x06008AD3 RID: 35539 RVA: 0x00116920 File Offset: 0x00114B20
		public async UniTask<Texture2D> LoadCloseupAsync(int code, MeshRenderer renderer = null)
		{
			if (renderer != null)
			{
				renderer.gameObject.SetActive(false);
			}
			Texture2D returenValue;
			Texture2D texture2D;
			if (TextureManager.cachedCloseups.TryGetValue(code, out returenValue))
			{
				if (renderer != null)
				{
					this.ResizeCloseup(renderer, returenValue);
				}
				texture2D = returenValue;
			}
			else
			{
				if (!Directory.Exists("Picture/Closeup/"))
				{
					Directory.CreateDirectory("Picture/Closeup/");
				}
				string path = "Picture/Closeup/" + code.ToString() + ".png";
				if (!File.Exists(path))
				{
					texture2D = null;
				}
				else
				{
					returenValue = await TextureManager.LoadPicFromFileAsync(path);
					returenValue.name = "Closeup_" + code.ToString();
					if (TextureManager.cachedCloseups.ContainsKey(code))
					{
						global::UnityEngine.Object.Destroy(returenValue);
						returenValue = TextureManager.cachedCloseups[code];
					}
					else
					{
						TextureManager.cachedCloseups.Add(code, returenValue);
					}
					if (renderer != null)
					{
						this.ResizeCloseup(renderer, returenValue);
					}
					texture2D = returenValue;
				}
			}
			return texture2D;
		}

		// Token: 0x06008AD4 RID: 35540 RVA: 0x00116974 File Offset: 0x00114B74
		private void ResizeCloseup(MeshRenderer renderer, Texture2D tex)
		{
			renderer.material.mainTexture = tex;
			float aspect = (float)tex.width / (float)tex.height;
			renderer.transform.localScale = new Vector3(8f * aspect, 8f, 1f);
			renderer.gameObject.SetActive(true);
			DOTween.To(() => 0f, delegate(float x)
			{
				renderer.transform.localScale = new Vector3(x * aspect, x, 1f);
			}, 8f, 0.3f);
		}

		// Token: 0x06008AD5 RID: 35541 RVA: 0x00116A2C File Offset: 0x00114C2C
		public static Sprite GetCardLocationIcon(GPS p)
		{
			if ((p.location & 2U) > 0U)
			{
				return TextureManager.container.locationHand;
			}
			if ((p.location & 1U) > 0U)
			{
				return TextureManager.container.locationDeck;
			}
			if ((p.location & 64U) > 0U)
			{
				return TextureManager.container.locationExtra;
			}
			if ((p.location & 16U) > 0U)
			{
				return TextureManager.container.locationGrave;
			}
			if ((p.location & 32U) > 0U)
			{
				return TextureManager.container.locationRemoved;
			}
			if ((p.location & 128U) > 0U)
			{
				return TextureManager.container.locationOverlay;
			}
			if ((p.location & 12U) > 0U)
			{
				if (p.controller == 0U)
				{
					return TextureManager.container.locationMyField;
				}
				return TextureManager.container.locationOpField;
			}
			else
			{
				if ((p.location & 2048U) > 0U)
				{
					return TextureManager.container.locationSearch;
				}
				return TextureManager.container.typeNone;
			}
		}

		// Token: 0x06008AD6 RID: 35542 RVA: 0x00116B14 File Offset: 0x00114D14
		public static Sprite GetCardRaceIcon(int race)
		{
			if (((long)race & 1L) > 0L)
			{
				return TextureManager.container.raceWarrior;
			}
			if (((long)race & 2L) > 0L)
			{
				return TextureManager.container.raceSpellCaster;
			}
			if (((long)race & 4L) > 0L)
			{
				return TextureManager.container.raceFairy;
			}
			if (((long)race & 8L) > 0L)
			{
				return TextureManager.container.raceFiend;
			}
			if (((long)race & 16L) > 0L)
			{
				return TextureManager.container.raceZombie;
			}
			if (((long)race & 32L) > 0L)
			{
				return TextureManager.container.raceMachine;
			}
			if (((long)race & 64L) > 0L)
			{
				return TextureManager.container.raceAqua;
			}
			if (((long)race & 128L) > 0L)
			{
				return TextureManager.container.racePyro;
			}
			if (((long)race & 256L) > 0L)
			{
				return TextureManager.container.raceRock;
			}
			if (((long)race & 512L) > 0L)
			{
				return TextureManager.container.raceWindBeast;
			}
			if (((long)race & 1024L) > 0L)
			{
				return TextureManager.container.racePlant;
			}
			if (((long)race & 2048L) > 0L)
			{
				return TextureManager.container.raceInsect;
			}
			if (((long)race & 4096L) > 0L)
			{
				return TextureManager.container.raceThunder;
			}
			if (((long)race & 8192L) > 0L)
			{
				return TextureManager.container.raceDragon;
			}
			if (((long)race & 16384L) > 0L)
			{
				return TextureManager.container.raceBeast;
			}
			if (((long)race & 32768L) > 0L)
			{
				return TextureManager.container.raceBeastWarrior;
			}
			if (((long)race & 65536L) > 0L)
			{
				return TextureManager.container.raceDinosaur;
			}
			if (((long)race & 131072L) > 0L)
			{
				return TextureManager.container.raceFish;
			}
			if (((long)race & 262144L) > 0L)
			{
				return TextureManager.container.raceSeaSerpent;
			}
			if (((long)race & 524288L) > 0L)
			{
				return TextureManager.container.raceReptile;
			}
			if (((long)race & 1048576L) > 0L)
			{
				return TextureManager.container.racePsycho;
			}
			if (((long)race & 2097152L) > 0L)
			{
				return TextureManager.container.raceDivineBeast;
			}
			if (((long)race & 4194304L) > 0L)
			{
				return TextureManager.container.raceCreatorGod;
			}
			if (((long)race & 8388608L) > 0L)
			{
				return TextureManager.container.raceWyrm;
			}
			if (((long)race & 16777216L) > 0L)
			{
				return TextureManager.container.raceCyberse;
			}
			if (((long)race & 33554432L) > 0L)
			{
				return TextureManager.container.raceIllustion;
			}
			return TextureManager.container.typeNone;
		}

		// Token: 0x06008AD7 RID: 35543 RVA: 0x00116D84 File Offset: 0x00114F84
		public static Sprite GetSpellTrapTypeIcon(Card data)
		{
			if (data.HasType(CardType.Counter))
			{
				return TextureManager.container.typeCounter;
			}
			if (data.HasType(CardType.Field))
			{
				return TextureManager.container.typeField;
			}
			if (data.HasType(CardType.Equip))
			{
				return TextureManager.container.typeEquip;
			}
			if (data.HasType(CardType.Continuous))
			{
				return TextureManager.container.typeContinuous;
			}
			if (data.HasType(CardType.QuickPlay))
			{
				return TextureManager.container.typeQuickPlay;
			}
			if (data.HasType(CardType.Ritual))
			{
				return TextureManager.container.typeRitual;
			}
			return TextureManager.container.typeNone;
		}

		// Token: 0x06008AD8 RID: 35544 RVA: 0x00116E2B File Offset: 0x0011502B
		public static Sprite GetCardLevelIcon(Card data)
		{
			if (data.HasType(CardType.Link))
			{
				return TextureManager.container.typeLink;
			}
			if (data.HasType(CardType.Xyz))
			{
				return TextureManager.container.typeRank;
			}
			return TextureManager.container.typeLevel;
		}

		// Token: 0x06008AD9 RID: 35545 RVA: 0x00116E68 File Offset: 0x00115068
		public static Sprite GetCardCounterIcon(int counter)
		{
			if (counter <= 4132)
			{
				if (counter <= 4098)
				{
					if (counter <= 94)
					{
						switch (counter)
						{
						case 1:
							return TextureManager.container.counterMagic;
						case 2:
						case 7:
						case 9:
						case 11:
						case 14:
						case 20:
						case 21:
						case 25:
						case 31:
						case 33:
						case 36:
						case 38:
						case 39:
						case 40:
						case 41:
						case 42:
						case 45:
						case 52:
						case 56:
						case 57:
						case 58:
						case 59:
						case 60:
						case 61:
						case 62:
						case 63:
						case 64:
						case 65:
						case 66:
						case 68:
						case 69:
						case 70:
						case 71:
						case 72:
						case 73:
							break;
						case 3:
							return TextureManager.container.counterBushido;
						case 4:
							return TextureManager.container.counterPsycho;
						case 5:
							return TextureManager.container.counterShine;
						case 6:
							return TextureManager.container.counterGem;
						case 8:
							return TextureManager.container.counterDeformer;
						case 10:
							return TextureManager.container.counterGenex;
						case 12:
							return TextureManager.container.counterThunder;
						case 13:
							return TextureManager.container.counterGreed;
						case 15:
							return TextureManager.container.counterWorm;
						case 16:
							return TextureManager.container.counterBF;
						case 17:
							return TextureManager.container.counterHyper;
						case 18:
							return TextureManager.container.counterKarakuri;
						case 19:
							return TextureManager.container.counterChaos;
						case 22:
							return TextureManager.container.counterStone;
						case 23:
							return TextureManager.container.counterDonguri;
						case 24:
							return TextureManager.container.counterFlower;
						case 26:
							return TextureManager.container.counterDouble;
						case 27:
							return TextureManager.container.counterClock;
						case 28:
							return TextureManager.container.counterD;
						case 29:
							return TextureManager.container.counterJunk;
						case 30:
							return TextureManager.container.counterGate;
						case 32:
							return TextureManager.container.counterPlant;
						case 34:
							return TextureManager.container.counterDragonic;
						case 35:
							return TextureManager.container.counterOcean;
						case 37:
							return TextureManager.container.counterChronicle;
						case 43:
							return TextureManager.container.counterDestiny;
						case 44:
							return TextureManager.container.counterOrbital;
						case 46:
							return TextureManager.container.counterShark;
						case 47:
							return TextureManager.container.counterPumpkin;
						case 48:
							return TextureManager.container.counterKattobing;
						case 49:
							return TextureManager.container.counterHopeSlash;
						case 50:
							return TextureManager.container.counterBalloon;
						case 51:
							return TextureManager.container.counterYosen;
						case 53:
							return TextureManager.container.counterSound;
						case 54:
							return TextureManager.container.counterEM;
						case 55:
							return TextureManager.container.counterKaiju;
						case 67:
							return TextureManager.container.counterDefect;
						case 74:
							return TextureManager.container.counterAthlete;
						case 75:
							return TextureManager.container.counterBarrel;
						case 76:
							return TextureManager.container.counterSummon;
						default:
							switch (counter)
							{
							case 86:
								return TextureManager.container.counterFireStar;
							case 87:
								return TextureManager.container.counterPhantasm;
							case 88:
								break;
							case 89:
								return TextureManager.container.counterOtoshidama;
							default:
								if (counter == 94)
								{
									return TextureManager.container.counterOunokagi;
								}
								break;
							}
							break;
						}
					}
					else
					{
						if (counter == 95)
						{
							return TextureManager.container.counterPiece;
						}
						switch (counter)
						{
						case 100:
							return TextureManager.container.counterGG;
						case 101:
						case 102:
						case 103:
						case 104:
						case 105:
						case 107:
						case 114:
							break;
						case 106:
							return TextureManager.container.counterKyoumei;
						case 108:
							return TextureManager.container.counterAccess;
						case 109:
							return TextureManager.container.counterShukudai;
						case 110:
							return TextureManager.container.counterShiki;
						case 111:
							return TextureManager.container.counterC;
						case 112:
							return TextureManager.container.counterDish;
						case 113:
							return TextureManager.container.counterKyuzai;
						case 115:
							return TextureManager.container.counterT;
						default:
							if (counter == 4098)
							{
								return TextureManager.container.counterWedge;
							}
							break;
						}
					}
				}
				else if (counter <= 4117)
				{
					if (counter == 4105)
					{
						return TextureManager.container.counterVenom;
					}
					if (counter == 4110)
					{
						return TextureManager.container.counterAlien;
					}
					if (counter == 4117)
					{
						return TextureManager.container.counterIce;
					}
				}
				else
				{
					if (counter == 4121)
					{
						return TextureManager.container.counterFog;
					}
					if (counter == 4129)
					{
						return TextureManager.container.counterGuard2;
					}
					if (counter == 4132)
					{
						return TextureManager.container.counterString;
					}
				}
			}
			else if (counter <= 4169)
			{
				if (counter <= 4153)
				{
					if (counter == 4138)
					{
						return TextureManager.container.counterGardna;
					}
					if (counter == 4152)
					{
						return TextureManager.container.counterHoukai;
					}
					if (counter == 4153)
					{
						return TextureManager.container.counterZushin;
					}
				}
				else
				{
					if (counter == 4161)
					{
						return TextureManager.container.counterPredator;
					}
					if (counter == 4165)
					{
						return TextureManager.container.counterScales;
					}
					if (counter == 4169)
					{
						return TextureManager.container.counterPolice;
					}
				}
			}
			else if (counter <= 4188)
			{
				if (counter == 4173)
				{
					return TextureManager.container.counterSignal;
				}
				if (counter == 4175)
				{
					return TextureManager.container.counterVenemy;
				}
				if (counter == 4188)
				{
					return TextureManager.container.counterBurn;
				}
			}
			else if (counter <= 4197)
			{
				if (counter == 4195)
				{
					return TextureManager.container.counterIllusion;
				}
				if (counter == 4197)
				{
					return TextureManager.container.counterRabbit;
				}
			}
			else
			{
				if (counter == 4203)
				{
					return TextureManager.container.counterKyouai;
				}
				if (counter == 4210)
				{
					return TextureManager.container.counterGirl;
				}
			}
			return TextureManager.container.counterNormal;
		}

		// Token: 0x06008ADA RID: 35546 RVA: 0x00117610 File Offset: 0x00115810
		public static Texture2D ResizeTexture2D(Texture2D texture, int newWidth, int newHeight)
		{
			Texture2D texture2D = new Texture2D(newWidth, newHeight);
			Color[] resizePixels = TextureManager.ResizePixelsBilinear(texture.GetPixels(), texture.width, texture.height, newWidth, newHeight);
			texture2D.SetPixels(resizePixels);
			texture2D.Apply();
			global::UnityEngine.Object.Destroy(texture);
			return texture2D;
		}

		// Token: 0x06008ADB RID: 35547 RVA: 0x00117654 File Offset: 0x00115854
		public static Color[] ResizePixelsNearest(Color[] originalPixels, int originalWidth, int originalHeight, int newWidth, int newHeight)
		{
			Color[] newPixels = new Color[newWidth * newHeight];
			for (int y = 0; y < newHeight; y++)
			{
				for (int x = 0; x < newWidth; x++)
				{
					int origX = (int)((float)x / (float)newWidth * (float)originalWidth);
					int origY = (int)((float)y / (float)newHeight * (float)originalHeight);
					newPixels[y * newWidth + x] = originalPixels[origY * originalWidth + origX];
				}
			}
			return newPixels;
		}

		// Token: 0x06008ADC RID: 35548 RVA: 0x001176B4 File Offset: 0x001158B4
		public static Color BilinearInterpolation(Color c1, Color c2, Color c3, Color c4, float u, float v)
		{
			Color c5 = new Color(c1.r * (1f - u) + c2.r * u, c1.g * (1f - u) + c2.g * u, c1.b * (1f - u) + c2.b * u, c1.a * (1f - u) + c2.a * u);
			Color c6 = new Color(c3.r * (1f - u) + c4.r * u, c3.g * (1f - u) + c4.g * u, c3.b * (1f - u) + c4.b * u, c3.a * (1f - u) + c4.a * u);
			return new Color(c5.r * (1f - v) + c6.r * v, c5.g * (1f - v) + c6.g * v, c5.b * (1f - v) + c6.b * v, c5.a * (1f - v) + c6.a * v);
		}

		// Token: 0x06008ADD RID: 35549 RVA: 0x00117800 File Offset: 0x00115A00
		public static Color[] ResizePixelsBilinear(Color[] originalPixels, int originalWidth, int originalHeight, int newWidth, int newHeight)
		{
			Color[] newPixels = new Color[newWidth * newHeight];
			for (int y = 0; y < newHeight; y++)
			{
				for (int x = 0; x < newWidth; x++)
				{
					float origX = (float)x / (float)newWidth * (float)originalWidth;
					float origY = (float)y / (float)newHeight * (float)originalHeight;
					int floorX = (int)Math.Floor((double)origX);
					int floorY = (int)Math.Floor((double)origY);
					int ceilX = Math.Min(floorX + 1, originalWidth - 1);
					int ceilY = Math.Min(floorY + 1, originalHeight - 1);
					if (floorX == ceilX || floorY == ceilY)
					{
						newPixels[y * newWidth + x] = originalPixels[floorY * originalWidth + floorX];
					}
					else
					{
						Color c = originalPixels[floorY * originalWidth + floorX];
						Color c2 = originalPixels[floorY * originalWidth + ceilX];
						Color c3 = originalPixels[ceilY * originalWidth + floorX];
						Color c4 = originalPixels[ceilY * originalWidth + ceilX];
						float u = origX - (float)floorX;
						float v = origY - (float)floorY;
						newPixels[y * newWidth + x] = TextureManager.BilinearInterpolation(c, c2, c3, c4, u, v);
					}
				}
			}
			return newPixels;
		}

		// Token: 0x06008ADE RID: 35550 RVA: 0x00117910 File Offset: 0x00115B10
		public static Color BicubicInterpolation(Color c00, Color c01, Color c02, Color c03, Color c10, Color c11, Color c12, Color c13, Color c20, Color c21, Color c22, Color c23, Color c30, Color c31, Color c32, Color c33, float u, float v)
		{
			float b = -0.5f;
			float c34 = 1.5f;
			float d = -1.5f;
			float e = 1f;
			float f = -0.5f;
			float g = 0.5f;
			float h = -0.5f;
			float[] array = new float[8];
			array[0] = b;
			array[1] = c34;
			array[2] = d;
			array[3] = e;
			array[4] = f;
			array[5] = g;
			array[6] = h;
			float[] i = array;
			float[] uMat = new float[]
			{
				u * u * u,
				u * u,
				u,
				1f
			};
			float[] vMat = new float[]
			{
				v * v * v,
				v * v,
				v,
				1f
			};
			Color c35 = new Color(TextureManager.Clamp(uMat[0] * i[0] * c00.r + uMat[1] * i[1] * c00.r + uMat[2] * i[2] * c00.r + uMat[3] * i[3] * c00.r, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c00.g + uMat[1] * i[1] * c00.g + uMat[2] * i[2] * c00.g + uMat[3] * i[3] * c00.g, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c00.b + uMat[1] * i[1] * c00.b + uMat[2] * i[2] * c00.b + uMat[3] * i[3] * c00.b, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c00.a + uMat[1] * i[1] * c00.a + uMat[2] * i[2] * c00.a + uMat[3] * i[3] * c00.a, 0f, 1f));
			new Color(TextureManager.Clamp(uMat[0] * i[0] * c10.r + uMat[1] * i[1] * c10.r + uMat[2] * i[2] * c10.r + uMat[3] * i[3] * c10.r, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c10.g + uMat[1] * i[1] * c10.g + uMat[2] * i[2] * c10.g + uMat[3] * i[3] * c10.g, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c10.b + uMat[1] * i[1] * c10.b + uMat[2] * i[2] * c10.b + uMat[3] * i[3] * c10.b, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c10.a + uMat[1] * i[1] * c10.a + uMat[2] * i[2] * c10.a + uMat[3] * i[3] * c10.a, 0f, 1f));
			new Color(TextureManager.Clamp(uMat[0] * i[0] * c20.r + uMat[1] * i[1] * c20.r + uMat[2] * i[2] * c20.r + uMat[3] * i[3] * c20.r, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c20.g + uMat[1] * i[1] * c20.g + uMat[2] * i[2] * c20.g + uMat[3] * i[3] * c20.g, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c20.b + uMat[1] * i[1] * c20.b + uMat[2] * i[2] * c20.b + uMat[3] * i[3] * c20.b, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c20.a + uMat[1] * i[1] * c20.a + uMat[2] * i[2] * c20.a + uMat[3] * i[3] * c20.a, 0f, 1f));
			new Color(TextureManager.Clamp(uMat[0] * i[0] * c30.r + uMat[1] * i[1] * c30.r + uMat[2] * i[2] * c30.r + uMat[3] * i[3] * c30.r, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c30.g + uMat[1] * i[1] * c30.g + uMat[2] * i[2] * c30.g + uMat[3] * i[3] * c30.g, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c30.b + uMat[1] * i[1] * c30.b + uMat[2] * i[2] * c30.b + uMat[3] * i[3] * c30.b, 0f, 1f), TextureManager.Clamp(uMat[0] * i[0] * c30.a + uMat[1] * i[1] * c30.a + uMat[2] * i[2] * c30.a + uMat[3] * i[3] * c30.a, 0f, 1f));
			return new Color(TextureManager.Clamp(vMat[0] * i[0] * c35.r + vMat[1] * i[1] * c35.r + vMat[2] * i[2] * c35.r + vMat[3] * i[3] * c35.r, 0f, 1f), TextureManager.Clamp(vMat[0] * i[0] * c35.g + vMat[1] * i[1] * c35.g + vMat[2] * i[2] * c35.g + vMat[3] * i[3] * c35.g, 0f, 1f), TextureManager.Clamp(vMat[0] * i[0] * c35.b + vMat[1] * i[1] * c35.b + vMat[2] * i[2] * c35.b + vMat[3] * i[3] * c35.b, 0f, 1f), TextureManager.Clamp(vMat[0] * i[0] * c35.a + vMat[1] * i[1] * c35.a + vMat[2] * i[2] * c35.a + vMat[3] * i[3] * c35.a, 0f, 1f));
		}

		// Token: 0x06008ADF RID: 35551 RVA: 0x00118087 File Offset: 0x00116287
		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				return min;
			}
			if (value <= max)
			{
				return value;
			}
			return max;
		}

		// Token: 0x06008AE0 RID: 35552 RVA: 0x00118098 File Offset: 0x00116298
		public static Color[] ResizePixelsBicubic(Color[] originalPixels, int originalWidth, int originalHeight, int newWidth, int newHeight)
		{
			Color[] newPixels = new Color[newWidth * newHeight];
			for (int y = 0; y < newHeight; y++)
			{
				for (int x = 0; x < newWidth; x++)
				{
					float origX = (float)x / (float)newWidth * (float)originalWidth;
					float origY = (float)y / (float)newHeight * (float)originalHeight;
					int floorX = (int)Math.Floor((double)origX);
					int floorY = (int)Math.Floor((double)origY);
					int ceilX = Math.Min(floorX + 3, originalWidth - 1);
					int ceilY = Math.Min(floorY + 3, originalHeight - 1);
					if (floorX >= ceilX - 1 || floorY >= ceilY - 1)
					{
						newPixels[y * newWidth + x] = originalPixels[floorY * originalWidth + floorX];
					}
					else
					{
						Color[,] colors = new Color[4, 4];
						for (int row = 0; row < 4; row++)
						{
							for (int col = 0; col < 4; col++)
							{
								colors[row, col] = originalPixels[(floorY + row) * originalWidth + floorX + col];
							}
						}
						float u = origX - (float)floorX;
						float v = origY - (float)floorY;
						newPixels[y * newWidth + x] = TextureManager.BicubicInterpolation(colors[0, 0], colors[0, 1], colors[0, 2], colors[0, 3], colors[1, 0], colors[1, 1], colors[1, 2], colors[1, 3], colors[2, 0], colors[2, 1], colors[2, 2], colors[2, 3], colors[3, 0], colors[3, 1], colors[3, 2], colors[3, 3], u, v);
					}
				}
			}
			return newPixels;
		}

		// Token: 0x06008AE1 RID: 35553 RVA: 0x00118241 File Offset: 0x00116441
		public static Sprite Texture2Sprite(Texture2D texture)
		{
			if (texture == null)
			{
				return null;
			}
			return Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f));
		}

		// Token: 0x06008AE2 RID: 35554 RVA: 0x00118280 File Offset: 0x00116480
		public static void ReplaceTransparentPixelsWithColor(Texture2D texture, Color replacementColor)
		{
			Color32[] pixels = texture.GetPixels32();
			for (int i = 0; i < pixels.Length; i++)
			{
				if (pixels[i].a == 0)
				{
					pixels[i] = replacementColor;
				}
			}
			texture.SetPixels32(pixels);
			texture.Apply();
		}

		// Token: 0x06008AE3 RID: 35555 RVA: 0x001182CC File Offset: 0x001164CC
		public static Texture2D CreateCenteredTexture(Texture2D originalTexture, int newSize, int offsetX, int offsetY)
		{
			if (originalTexture == null)
			{
				throw new ArgumentNullException("originalTexture", "Original texture cannot be null.");
			}
			Texture2D newTexture = new Texture2D(newSize, newSize, originalTexture.format, false);
			for (int y = 0; y < newSize; y++)
			{
				for (int x = 0; x < newSize; x++)
				{
					newTexture.SetPixel(x, y, Color.clear);
				}
			}
			newTexture.Apply();
			int num = newSize / 2;
			int centerY = newSize / 2;
			int startX = num - originalTexture.width / 2 + offsetX;
			int startY = centerY - originalTexture.height / 2 + offsetY;
			for (int y2 = 0; y2 < originalTexture.height; y2++)
			{
				for (int x2 = 0; x2 < originalTexture.width; x2++)
				{
					Color pixelColor = originalTexture.GetPixel(x2, y2);
					int newX = startX + x2;
					int newY = startY + y2;
					if (newX >= 0 && newX < newSize && newY >= 0 && newY < newSize)
					{
						newTexture.SetPixel(newX, newY, pixelColor);
					}
				}
			}
			newTexture.Apply();
			return newTexture;
		}

		// Token: 0x06008AE4 RID: 35556 RVA: 0x001183C0 File Offset: 0x001165C0
		public static void ChangeProfileFrameMaterialWrapMode(Material mat)
		{
		}

		// Token: 0x06008AE5 RID: 35557 RVA: 0x001183D0 File Offset: 0x001165D0
		public static Texture2D GetCroppingTex(Texture2D texture, int startX, int startY, int width, int height)
		{
			Texture2D returnValue = new Texture2D(width - startX, height - startY);
			Color[] pix = new Color[returnValue.width * returnValue.height];
			int index = 0;
			for (int y = startY; y < height; y++)
			{
				for (int x = startX; x < width; x++)
				{
					pix[index++] = texture.GetPixel(x, y);
				}
			}
			returnValue.SetPixels(pix);
			returnValue.Apply();
			return returnValue;
		}

		// Token: 0x0400C660 RID: 50784
		public static TextureManager instance;

		// Token: 0x0400C661 RID: 50785
		public static TextureContainer container;

		// Token: 0x0400C662 RID: 50786
		private Material commonShopButtonMat;

		// Token: 0x0400C663 RID: 50787
		private Material commonShopButtonOverMat;

		// Token: 0x0400C664 RID: 50788
		public static bool loaded;

		// Token: 0x0400C665 RID: 50789
		private static Dictionary<int, Texture2D> cachedCloseups = new Dictionary<int, Texture2D>();
	}
}
