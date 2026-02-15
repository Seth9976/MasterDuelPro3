using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel;
using MDPro3.Duel.YGOSharp;
using MDPro3.UI;
using MDPro3.UI.Popup;
using MDPro3.UI.PropertyOverride;
using MDPro3.Utility;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3
{
	// Token: 0x02001279 RID: 4729
	public class UIManager : Manager
	{
		// Token: 0x06008AFF RID: 35583 RVA: 0x00118FF0 File Offset: 0x001171F0
		public override void Initialize()
		{
			base.Initialize();
			this.handlers = new List<UIHandler> { this.fps };
			foreach (UIHandler uihandler in this.handlers)
			{
				uihandler.Initialize();
			}
			try
			{
				UIManager.currentWallpaper = Config.Get("Wallpaper", Program.items.wallpapers[0].id.ToString());
				this.ChangeWallpaper(UIManager.currentWallpaper);
				UIManager.InitializeLanguage();
			}
			catch (Exception e)
			{
				Debug.LogError(string.Format("UIManager Initialize Error: {0}", e));
			}
		}

		// Token: 0x06008B00 RID: 35584 RVA: 0x001190BC File Offset: 0x001172BC
		public override void PerFrameFunction()
		{
			base.PerFrameFunction();
			foreach (UIHandler uihandler in this.handlers)
			{
				uihandler.PerframeFunction();
			}
		}

		// Token: 0x06008B01 RID: 35585 RVA: 0x00119114 File Offset: 0x00117314
		public static void Translate(GameObject go)
		{
			foreach (Text text in go.GetComponentsInChildren<Text>(true))
			{
				if (text.name.StartsWith("#Text"))
				{
					text.text = InterString.Get(text.text.Replace("\r\n", "@n").Replace("\n", "@n"), 0);
				}
			}
			foreach (TextMeshProUGUI tmp in go.GetComponentsInChildren<TextMeshProUGUI>(true))
			{
				if (tmp.name.StartsWith("#Text"))
				{
					tmp.text = InterString.Get(tmp.text.Replace("\r\n", "@n").Replace("\n", "@n"), 0);
				}
				if (tmp.name.EndsWith("Menu"))
				{
					UIManager instance = Program.instance.ui_;
					if (Language.GetConfig() == "en-US" || Language.GetConfig() == "ja-JP")
					{
						tmp.font = instance.jpMenuTmpFont;
						tmp.fontSize = 64f;
					}
					else if (Language.GetConfig() == "zh-CN")
					{
						tmp.font = instance.cnMenuTmpFont;
						tmp.fontSize = 62f;
					}
					else
					{
						tmp.font = instance.tmpFont;
						tmp.fontSize = 60f;
					}
				}
			}
		}

		// Token: 0x06008B02 RID: 35586 RVA: 0x0011928C File Offset: 0x0011748C
		public static void InitializeLanguage()
		{
			InterString.Initialize();
			StringHelper.Initialize();
			CardsManager.Initialize();
			Program.items.Initialize();
			UIManager instance = Program.instance.ui_;
			foreach (Transform t in instance.GetComponentsInChildren<Transform>(true))
			{
				if (t.name.StartsWith("#Text"))
				{
					Text text;
					TextMeshProUGUI tmp;
					if (t.TryGetComponent<Text>(out text))
					{
						text.text = InterString.Get(text.text, 0);
					}
					else if (t.TryGetComponent<TextMeshProUGUI>(out tmp))
					{
						tmp.text = InterString.Get(tmp.text, 0);
						if (tmp.name.EndsWith("Menu"))
						{
							if (Language.GetConfig() == "en-US" || Language.GetConfig() == "ja-JP")
							{
								tmp.font = instance.jpMenuTmpFont;
								tmp.fontSize = 64f;
							}
							else if (Language.GetConfig() == "zh-CN")
							{
								tmp.font = instance.cnMenuTmpFont;
								tmp.fontSize = 62f;
							}
							else
							{
								tmp.font = instance.tmpFont;
								tmp.fontSize = 60f;
							}
						}
					}
				}
			}
		}

		// Token: 0x06008B03 RID: 35587 RVA: 0x001193D0 File Offset: 0x001175D0
		public static void ChangeLanguage()
		{
			foreach (Transform t in Program.instance.ui_.GetComponentsInChildren<Transform>(true))
			{
				if (t.name.StartsWith("#Text"))
				{
					Text text;
					TextMeshProUGUI tmp;
					if (t.TryGetComponent<Text>(out text))
					{
						text.text = InterString.GetOriginal(text.text);
					}
					else if (t.TryGetComponent<TextMeshProUGUI>(out tmp))
					{
						tmp.text = InterString.GetOriginal(tmp.text);
					}
				}
			}
			CardImageLoader.ClearCache();
			Program.instance.UnloadUnusedAssets();
			UIManager.InitializeLanguage();
			Program.instance.cutin.LoadCutins();
			Program.instance.mate.LoadMates();
			Program.instance.solo.LoadBots();
			Program.instance.character.LoadCharacters();
			Program.instance.setting.RefreshCharacterName();
			SystemEvent.CallLanguageChangeEvent();
			SystemEvent.CallVideoCardConfigChangeEvent();
		}

		// Token: 0x06008B04 RID: 35588 RVA: 0x001194B8 File Offset: 0x001176B8
		public static void ChangeLayout()
		{
			PropertyOverrider[] componentsInChildren = Program.instance.ui_.GetComponentsInChildren<PropertyOverrider>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Override();
			}
			UIManager.ShowExitButton(0f, Ease.Linear);
			Program.instance.cutin.LoadCutins();
			Program.instance.mate.LoadMates();
			Program.instance.solo.LoadBots();
			Program.instance.puzzle.PrintPuzzles();
		}

		// Token: 0x06008B05 RID: 35589 RVA: 0x00119534 File Offset: 0x00117734
		private async UniTask LoadDiyWallpaperAsync(string path, Transform parent)
		{
			(await ABLoader.LoadFromFileAsync(path, false, true)).transform.SetParent(parent, false);
		}

		// Token: 0x06008B06 RID: 35590 RVA: 0x00119580 File Offset: 0x00117780
		public Transform ChangeWallpaper(string path)
		{
			if (this.wallpaper.transform.childCount > 0)
			{
				global::UnityEngine.Object.Destroy(this.wallpaper.transform.GetChild(0).gameObject);
			}
			if (path == 0.ToString())
			{
				return null;
			}
			path = "MasterDuel/" + Program.items.GetWallpaperPath(path);
			if (!path.ToLower().Contains("front"))
			{
				Transform frontback = this.ChangeWallpaper("1130002");
				global::UnityEngine.Object.Destroy(frontback.GetChild(1).gameObject);
				this.LoadDiyWallpaperAsync(path, frontback);
				return frontback;
			}
			Transform front = global::UnityEngine.Object.Instantiate<GameObject>(ABLoader.LoadFromFolder<RectTransform>(path, false, true)).transform;
			front.SetParent(this.wallpaper.transform, false);
			for (int i = 0; i < front.transform.childCount; i++)
			{
				front.transform.GetChild(i).gameObject.AddComponent<RectLoopMoveY>();
			}
			ParticleSystem[] componentsInChildren = front.GetComponentsInChildren<ParticleSystem>(true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].Play();
			}
			return front;
		}

		// Token: 0x06008B07 RID: 35591 RVA: 0x00119694 File Offset: 0x00117894
		public static void ShowWallpaper(float time)
		{
			UIManager instance = Program.instance.ui_;
			instance.wallpaper.gameObject.SetActive(true);
			DOTween.To(() => instance.wallpaper.alpha, delegate(float x)
			{
				instance.wallpaper.alpha = x;
			}, 1f, time);
			foreach (ParticleSystem particleSystem in instance.wallpaper.transform.GetComponentsInChildren<ParticleSystem>(true))
			{
				particleSystem.gameObject.SetActive(true);
				particleSystem.Play();
			}
			SkeletonAnimation[] componentsInChildren2 = instance.wallpaper.transform.GetComponentsInChildren<SkeletonAnimation>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				componentsInChildren2[i].GetComponent<Renderer>().material.DOFade(1f, time - 0.1f).OnComplete(delegate
				{
				});
			}
		}

		// Token: 0x06008B08 RID: 35592 RVA: 0x00119794 File Offset: 0x00117994
		public static void HideWallpaper(float time)
		{
			DOTween.To(() => Program.instance.ui_.wallpaper.alpha, delegate(float x)
			{
				Program.instance.ui_.wallpaper.alpha = x;
			}, 0f, time).OnComplete(delegate
			{
				Program.instance.ui_.wallpaper.gameObject.SetActive(false);
			});
			ParticleSystem[] componentsInChildren = Program.instance.ui_.wallpaper.transform.GetComponentsInChildren<ParticleSystem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.SetActive(false);
			}
			SkeletonAnimation[] componentsInChildren2 = Program.instance.ui_.wallpaper.transform.GetComponentsInChildren<SkeletonAnimation>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				componentsInChildren2[i].GetComponent<Renderer>().material.DOFade(0f, time - 0.1f).OnComplete(delegate
				{
				});
			}
		}

		// Token: 0x06008B09 RID: 35593 RVA: 0x001198B1 File Offset: 0x00117AB1
		public static void ShowExitButton(float time, Ease ease = Ease.Linear)
		{
			Program.instance.ui_.btnExit.GetComponent<RectTransform>().DOAnchorPosY(PropertyOverrider.NeedMobileLayout() ? (-65f) : (-60f), time, false).SetEase(ease);
		}

		// Token: 0x06008B0A RID: 35594 RVA: 0x001198E8 File Offset: 0x00117AE8
		public static void HideExitButton(float time, Ease ease = Ease.Linear)
		{
			Program.instance.ui_.btnExit.GetComponent<RectTransform>().DOAnchorPosY(PropertyOverrider.NeedMobileLayout() ? 65f : 60f, time, false).SetEase(ease);
		}

		// Token: 0x06008B0B RID: 35595 RVA: 0x0011991F File Offset: 0x00117B1F
		public static void ShowLine(float time)
		{
			Program.instance.ui_.line.DOFade(1f, time);
		}

		// Token: 0x06008B0C RID: 35596 RVA: 0x0011993C File Offset: 0x00117B3C
		public static void HideLine(float time)
		{
			Program.instance.ui_.line.DOFade(0f, time);
		}

		// Token: 0x06008B0D RID: 35597 RVA: 0x00119959 File Offset: 0x00117B59
		public static void ShowFPS()
		{
			Program.instance.ui_.fps.gameObject.SetActive(true);
		}

		// Token: 0x06008B0E RID: 35598 RVA: 0x00119975 File Offset: 0x00117B75
		public static void HideFPS()
		{
			Program.instance.ui_.fps.gameObject.SetActive(false);
		}

		// Token: 0x06008B0F RID: 35599 RVA: 0x00119994 File Offset: 0x00117B94
		public static void ShowFPSLeft()
		{
			Program.instance.ui_.fps.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
			Program.instance.ui_.fps.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
			Program.instance.ui_.fps.GetComponent<RectTransform>().anchoredPosition = new Vector2(120f, 0f);
		}

		// Token: 0x06008B10 RID: 35600 RVA: 0x00119A1C File Offset: 0x00117C1C
		public static void ShowFPSRight()
		{
			Program.instance.ui_.fps.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
			Program.instance.ui_.fps.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
			Program.instance.ui_.fps.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		}

		// Token: 0x06008B11 RID: 35601 RVA: 0x00119AA1 File Offset: 0x00117CA1
		public static void SetFpsSize(int size)
		{
			Program.instance.ui_.fps.text.fontSize = size;
		}

		// Token: 0x06008B12 RID: 35602 RVA: 0x00119AC0 File Offset: 0x00117CC0
		public static void ShowPopupSelection(List<string> selections, Action decideAction, Action cancelAction = null)
		{
			Addressables.InstantiateAsync("Popup/PopupSelection.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				PopupSelection component = result.Result.GetComponent<PopupSelection>();
				component.args = selections;
				component.decideAction = decideAction;
				component.quitAction = cancelAction;
				component.Show();
			};
		}

		// Token: 0x06008B13 RID: 35603 RVA: 0x00119B0C File Offset: 0x00117D0C
		public static void ShowPopupYesOrNo(List<string> selections, Action decideAction, Action cancelAction)
		{
			Addressables.InstantiateAsync("Popup/PopupYesOrNo.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				MDPro3.UI.Popup.PopupYesOrNo component = result.Result.GetComponent<MDPro3.UI.Popup.PopupYesOrNo>();
				component.args = selections;
				component.decideAction = decideAction;
				component.cancelAction = cancelAction;
				component.Show();
			};
		}

		// Token: 0x06008B14 RID: 35604 RVA: 0x00119B58 File Offset: 0x00117D58
		public static void ShowPopupConfirm(List<string> selections)
		{
			Addressables.InstantiateAsync("Popup/PopupConfirm.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				MDPro3.UI.Popup.PopupConfirm component = result.Result.GetComponent<MDPro3.UI.Popup.PopupConfirm>();
				component.args = selections;
				component.Show();
			};
		}

		// Token: 0x06008B15 RID: 35605 RVA: 0x00119B94 File Offset: 0x00117D94
		public static void ShowPopupInput(List<string> selections, Action<string> decideAction, Action cancelAction, TmpInputValidation.ValidationType type = TmpInputValidation.ValidationType.None)
		{
			Addressables.InstantiateAsync("Popup/PopupInput.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				MDPro3.UI.Popup.PopupInput component = result.Result.GetComponent<MDPro3.UI.Popup.PopupInput>();
				component.args = selections;
				component.decideAction = decideAction;
				component.cancelAction = cancelAction;
				component.validationType = type;
				component.Show();
			};
		}

		// Token: 0x06008B16 RID: 35606 RVA: 0x00119BE4 File Offset: 0x00117DE4
		public static void ShowPopupFilter()
		{
			Addressables.InstantiateAsync("Popup/PopupSearchFilter.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				result.Result.GetComponent<PopupSearchFilter>().Show();
			};
		}

		// Token: 0x06008B17 RID: 35607 RVA: 0x00119C28 File Offset: 0x00117E28
		public static void ShowPopupText(List<string> selections, HorizontalAlignmentOptions alignment = HorizontalAlignmentOptions.Center)
		{
			Addressables.InstantiateAsync("Popup/PopupText.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				MDPro3.UI.Popup.PopupText component = result.Result.GetComponent<MDPro3.UI.Popup.PopupText>();
				component.alignment = alignment;
				component.args = selections;
				component.Show();
			};
		}

		// Token: 0x06008B18 RID: 35608 RVA: 0x00119C6C File Offset: 0x00117E6C
		public static void UIBlackIn(float time)
		{
			if (UIManager.duelTransition == null)
			{
				UIManager.duelTransition = ABLoader.LoadMasterDuelOutDuelObject("DuelEndTransition");
				UIManager.duelTransition.transform.SetParent(Program.instance.container_2D, false);
				UIManager.duelTransition.GetComponent<PlayableDirector>().Play();
			}
		}

		// Token: 0x06008B19 RID: 35609 RVA: 0x00119CBE File Offset: 0x00117EBE
		public static void UIBlackOut(float time)
		{
			if (UIManager.duelTransition != null)
			{
				UIManager.duelTransition.GetComponent<LoopTrackManager>().StopLoop();
				global::UnityEngine.Object.Destroy(UIManager.duelTransition, 1f);
				UIManager.duelTransition = null;
			}
		}

		// Token: 0x06008B1A RID: 35610 RVA: 0x00119CF4 File Offset: 0x00117EF4
		public static void ShowBlackBack(float alpha, float time, Action action = null)
		{
			Program.instance.ui_.blackBack.raycastTarget = true;
			Program.instance.ui_.blackBack.DOFade(alpha, time).SetUpdate(true).OnComplete(delegate
			{
				Action action2 = action;
				if (action2 == null)
				{
					return;
				}
				action2();
			});
		}

		// Token: 0x06008B1B RID: 35611 RVA: 0x00119D54 File Offset: 0x00117F54
		public static void HideBlackBack(float time)
		{
			Program.instance.ui_.blackBack.DOFade(0f, time).SetUpdate(true).OnComplete(delegate
			{
				Program.instance.ui_.blackBack.raycastTarget = false;
			});
		}

		// Token: 0x06008B1C RID: 35612 RVA: 0x00119DA8 File Offset: 0x00117FA8
		public static void SetCanvasMatch(float match, float duration)
		{
			UIManager instance = Program.instance.ui_;
			CanvasScaler scaler = instance.GetComponent<CanvasScaler>();
			DOTween.To(() => scaler.matchWidthOrHeight, delegate(float x)
			{
				scaler.matchWidthOrHeight = x;
			}, match, duration);
		}

		// Token: 0x06008B1D RID: 35613 RVA: 0x00119DF4 File Offset: 0x00117FF4
		public static void ShowCardExpand(Card data)
		{
			Addressables.InstantiateAsync("UIWidges/CardExpand.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				result.Result.GetComponent<CardExpand>().Show(data);
			};
		}

		// Token: 0x06008B1E RID: 35614 RVA: 0x00119E30 File Offset: 0x00118030
		public static void ShowCardInfoDetail(Card data)
		{
			Addressables.InstantiateAsync("UIWidges/CardInfoDetail.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				result.Result.GetComponent<CardInfoDetail>().Show(data);
			};
		}

		// Token: 0x06008B1F RID: 35615 RVA: 0x00119E6C File Offset: 0x0011806C
		public static void ShowCardInfoDetail(List<int> cards, int index)
		{
			Addressables.InstantiateAsync("UIWidges/CardInfoDetail.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				result.Result.GetComponent<CardInfoDetail>().Show(cards, index);
			};
		}

		// Token: 0x06008B20 RID: 35616 RVA: 0x00119EB0 File Offset: 0x001180B0
		public static void ShowSubMenu(List<string> menus, List<Action> actions)
		{
			Addressables.InstantiateAsync("UIWidges/SubMenuUI.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.sidePanel, false);
				(UIManager.InputBlocker = result.Result.GetComponent<SubMenu>()).Show(menus, actions);
			};
		}

		// Token: 0x06008B21 RID: 35617 RVA: 0x00119EF4 File Offset: 0x001180F4
		public static Vector2 WorldToScreenPoint(Camera camera, Vector3 positon)
		{
			Vector3 screenPosition = camera.WorldToScreenPoint(positon);
			Vector2 sizeDelta = Program.instance.ui_.GetComponent<RectTransform>().sizeDelta;
			return new Vector2(screenPosition.x * sizeDelta.x / (float)Screen.width, screenPosition.y * sizeDelta.y / (float)Screen.height);
		}

		// Token: 0x06008B22 RID: 35618 RVA: 0x00119F4C File Offset: 0x0011814C
		public static Vector2 ScreenToNoScalerScreenPoint(Vector2 position)
		{
			Vector2 sizeDelta = Program.instance.ui_.GetComponent<RectTransform>().sizeDelta;
			return new Vector2(position.x * (float)Screen.width / sizeDelta.x, position.y * (float)Screen.height / sizeDelta.y);
		}

		// Token: 0x06008B23 RID: 35619 RVA: 0x00119F9C File Offset: 0x0011819C
		public static Vector3 ScreenToWorldPoint(Camera camera, Vector2 positon)
		{
			Vector2 screenPosition = UIManager.ScreenToNoScalerScreenPoint(positon);
			return camera.ScreenToWorldPoint(screenPosition);
		}

		// Token: 0x06008B24 RID: 35620 RVA: 0x00119FBC File Offset: 0x001181BC
		public static float ScreenLengthWithoutScalerX(float length)
		{
			Vector2 sizeDelta = Program.instance.ui_.GetComponent<RectTransform>().sizeDelta;
			return length * sizeDelta.x / (float)Screen.width;
		}

		// Token: 0x06008B25 RID: 35621 RVA: 0x00119FF0 File Offset: 0x001181F0
		public static float ScreenLengthWithScalerX(float length)
		{
			Vector2 sizeDelta = Program.instance.ui_.GetComponent<RectTransform>().sizeDelta;
			return length * (float)Screen.width / sizeDelta.x;
		}

		// Token: 0x06008B26 RID: 35622 RVA: 0x0011A024 File Offset: 0x00118224
		public static float ScreenLengthWithoutScalerY(float length)
		{
			Vector2 sizeDelta = Program.instance.ui_.GetComponent<RectTransform>().sizeDelta;
			return length * sizeDelta.y / (float)Screen.height;
		}

		// Token: 0x06008B27 RID: 35623 RVA: 0x0011A058 File Offset: 0x00118258
		public static Vector2 GetMousePositionToAnchorPosition()
		{
			Vector2 returnValue = UserInput.MousePos;
			float uiWidth = 1080f * (float)Screen.width / (float)Screen.height;
			returnValue.x = returnValue.x * uiWidth / (float)Screen.width;
			returnValue.y = returnValue.y * 1080f / (float)Screen.height;
			returnValue.x -= uiWidth / 2f;
			returnValue.y -= 540f;
			return returnValue;
		}

		// Token: 0x0400C69C RID: 50844
		private const string TRANSLATE_PREFIX = "#Text";

		// Token: 0x0400C69D RID: 50845
		[Header("Public Reference")]
		public CanvasGroup wallpaper;

		// Token: 0x0400C69E RID: 50846
		public Button btnExit;

		// Token: 0x0400C69F RID: 50847
		public CanvasGroup line;

		// Token: 0x0400C6A0 RID: 50848
		public Image blackBack;

		// Token: 0x0400C6A1 RID: 50849
		public RectTransform popup;

		// Token: 0x0400C6A2 RID: 50850
		public RectTransform sidePanel;

		// Token: 0x0400C6A3 RID: 50851
		public RectTransform duelButton;

		// Token: 0x0400C6A4 RID: 50852
		public static string currentWallpaper;

		// Token: 0x0400C6A5 RID: 50853
		[Header("UI Handler")]
		public FPSHandler fps;

		// Token: 0x0400C6A6 RID: 50854
		private List<UIHandler> handlers;

		// Token: 0x0400C6A7 RID: 50855
		[Header("Side Panel")]
		public ChatPanel chatPanel;

		// Token: 0x0400C6A8 RID: 50856
		[Header("Source Reference")]
		public Font cnFont;

		// Token: 0x0400C6A9 RID: 50857
		public Font jpFont;

		// Token: 0x0400C6AA RID: 50858
		public Font cnMenuFont;

		// Token: 0x0400C6AB RID: 50859
		public TMP_FontAsset tmpFont;

		// Token: 0x0400C6AC RID: 50860
		public TMP_FontAsset jpMenuTmpFont;

		// Token: 0x0400C6AD RID: 50861
		public TMP_FontAsset cnMenuTmpFont;

		// Token: 0x0400C6AE RID: 50862
		[HideInInspector]
		public PopupBase currentPopup;

		// Token: 0x0400C6AF RID: 50863
		[HideInInspector]
		public Popup currentPopupB;

		// Token: 0x0400C6B0 RID: 50864
		[HideInInspector]
		public SidePanel currentSidePanel;

		// Token: 0x0400C6B1 RID: 50865
		[HideInInspector]
		public static MonoBehaviour InputBlocker;

		// Token: 0x0400C6B2 RID: 50866
		private static GameObject duelTransition;
	}
}
