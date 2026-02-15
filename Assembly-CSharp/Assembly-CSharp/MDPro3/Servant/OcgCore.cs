using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;
using YgomSystem.ElementSystem;

namespace MDPro3.Servant
{
	// Token: 0x020012E4 RID: 4836
	public class OcgCore : Servant
	{
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06008D43 RID: 36163 RVA: 0x00129DB6 File Offset: 0x00127FB6
		public DuelBGManager DuelBGManager
		{
			get
			{
				return this.messageDispatcher.duel.duelBGManager;
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06008D44 RID: 36164 RVA: 0x00129DC8 File Offset: 0x00127FC8
		public List<GameObject> allGameObjects
		{
			get
			{
				return this.messageDispatcher.duel.duelBGManager.allGameObjects;
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06008D45 RID: 36165 RVA: 0x00129DDF File Offset: 0x00127FDF
		public List<PlaceSelector> places
		{
			get
			{
				return this.messageDispatcher.duel.duelBGManager.places;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06008D46 RID: 36166 RVA: 0x00129DF6 File Offset: 0x00127FF6
		// (set) Token: 0x06008D47 RID: 36167 RVA: 0x00129DFD File Offset: 0x00127FFD
		public static bool NoMoreWait
		{
			get
			{
				return OcgCore.noMoreWait;
			}
			set
			{
				OcgCore.noMoreWait = value;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06008D48 RID: 36168 RVA: 0x00129E05 File Offset: 0x00128005
		// (set) Token: 0x06008D49 RID: 36169 RVA: 0x00129E0C File Offset: 0x0012800C
		public static bool HideMyHandCard
		{
			get
			{
				return OcgCore.hideMyHandCard;
			}
			set
			{
				OcgCore.hideMyHandCard = value;
				Program.instance.ocgcore.RefreshMyHandCardPosition();
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06008D4A RID: 36170 RVA: 0x00129E23 File Offset: 0x00128023
		// (set) Token: 0x06008D4B RID: 36171 RVA: 0x00129E2A File Offset: 0x0012802A
		public static bool HideOpHandCard
		{
			get
			{
				return OcgCore.hideOpHandCard;
			}
			set
			{
				OcgCore.hideOpHandCard = value;
				Program.instance.ocgcore.RefreshOpHandCardPosition();
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06008D4C RID: 36172 RVA: 0x00036E4C File Offset: 0x0003504C
		public override int Depth
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06008D4D RID: 36173 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008D4E RID: 36174 RVA: 0x00129E41 File Offset: 0x00128041
		public override void Initialize()
		{
			base.Initialize();
			SystemEvent.OnResolutionChange += this.RefreshHandCardPositionInstant;
		}

		// Token: 0x06008D4F RID: 36175 RVA: 0x00129E5C File Offset: 0x0012805C
		public void LoadDuelButton()
		{
			if (OcgCore.btnConfirm == null)
			{
				OcgCore.btnConfirm = ABLoader.LoadMasterDuelGameObject("DuelButton").GetComponent<DuelButton>();
				OcgCore.btnConfirm.response.Add(-4);
				OcgCore.btnConfirm.hint = InterString.Get("确认", 0);
				OcgCore.btnConfirm.type = ButtonType.Decide;
				OcgCore.btnConfirm.Hide();
			}
			if (OcgCore.btnCancel == null)
			{
				OcgCore.btnCancel = ABLoader.LoadMasterDuelGameObject("DuelButton").GetComponent<DuelButton>();
				OcgCore.btnCancel.response.Add(-5);
				OcgCore.btnCancel.hint = InterString.Get("取消", 0);
				OcgCore.btnCancel.type = ButtonType.Cancel;
				OcgCore.btnCancel.Hide();
			}
		}

		// Token: 0x06008D50 RID: 36176 RVA: 0x00129F24 File Offset: 0x00128124
		protected override void ApplyShowArrangement(int preDepth)
		{
			this.servantUI.gameObject.SetActive(true);
			this.servantUI.Show(true);
			this.GetUI<OcgCoreUI>().CG.alpha = 0f;
			this.GetUI<OcgCoreUI>().CG.blocksRaycasts = false;
			this.messageDispatcher.duel.duelBGManager.LoadAssetsAsync();
			this.ProcessMessage();
			this.returnAction = null;
		}

		// Token: 0x06008D51 RID: 36177 RVA: 0x00129F98 File Offset: 0x00128198
		protected override void ApplyHideArrangement(int preDepth)
		{
			this.messageDispatcher.duel.duelBGManager.ExitDuelAsync();
		}

		// Token: 0x06008D52 RID: 36178 RVA: 0x00129FB0 File Offset: 0x001281B0
		public override void OnExit()
		{
			base.OnExit();
			this.CloseConnection();
			this.GetUI<OcgCoreUI>().OnNor();
		}

		// Token: 0x06008D53 RID: 36179 RVA: 0x00129FCC File Offset: 0x001281CC
		public void ReturnTo()
		{
			if (this.returnServant != null)
			{
				Debug.Log(string.Format("ReturnTo: {0}", this.returnServant));
				Program.instance.ShiftToServant(this.returnServant);
				return;
			}
			Debug.Log("ReturnTo: - Program.instance.online");
			Program.instance.ShiftToServant(Program.instance.online);
		}

		// Token: 0x06008D54 RID: 36180 RVA: 0x0012A02C File Offset: 0x0012822C
		public override void PerFrameFunction()
		{
			if (!this.showing)
			{
				return;
			}
			if (this.DuelBGManager == null)
			{
				return;
			}
			if (!EventSystem.current.IsPointerOverGameObject() && UserInput.HoverObject == null && UserInput.MouseLeftUp)
			{
				this.GetUI<OcgCoreUI>().CardDescription.Hide();
				this.GetUI<OcgCoreUI>().CardList.Hide();
			}
			if (this.DuelBGManager.HoveringField0() && UserInput.MouseLeftUp)
			{
				this.DuelBGManager.TapField0();
			}
			else if (this.DuelBGManager.HoveringField1() && UserInput.MouseLeftUp)
			{
				this.DuelBGManager.TapField1();
			}
			else if (this.DuelBGManager.HoveringMate0() && UserInput.MouseLeftUp)
			{
				this.DuelBGManager.TapMate0();
			}
			else if (this.DuelBGManager.HoveringMate1() && UserInput.MouseLeftUp)
			{
				this.DuelBGManager.TapMate1();
			}
			else
			{
				this.DuelBGManager.PlayMate0Random();
				this.DuelBGManager.PlayMate1Random();
			}
			if (this.GetMyHandCount() > 10)
			{
				if (UserInput.MouseLeftDown && UserInput.HoverObject != null && UserInput.HoverObject.name == "CardModel" && UserInput.HoverObject.GetComponent<GameCardMono>().cookieCard.p.controller == 0U && (UserInput.HoverObject.GetComponent<GameCardMono>().cookieCard.p.location & 2U) > 0U)
				{
					OcgCore.clickInPosition = UserInput.MousePos.x;
					OcgCore.clickingHandCard = true;
					OcgCore.handCount = this.GetMyHandCount();
				}
				if (OcgCore.clickingHandCard && UserInput.MouseLeftPressing)
				{
					float currentOffset = OcgCore.lastHandOffset + UserInput.MousePos.x - OcgCore.clickInPosition;
					float currentHandCellX = UIManager.ScreenLengthWithScalerX(OcgCore.handCellX);
					OcgCore.handOffset = ((currentOffset > (float)OcgCore.handCount * currentHandCellX) ? ((float)OcgCore.handCount * currentHandCellX) : ((Math.Abs(currentOffset) > (float)OcgCore.handCount * currentHandCellX) ? (-((float)OcgCore.handCount * currentHandCellX)) : currentOffset));
				}
				if (UserInput.MouseLeftUp)
				{
					OcgCore.handCardDraged = false;
					if (OcgCore.clickingHandCard)
					{
						OcgCore.clickingHandCard = false;
						if (OcgCore.lastHandOffset != OcgCore.handOffset)
						{
							OcgCore.handCardDraged = true;
							OcgCore.lastHandOffset = OcgCore.handOffset;
						}
					}
				}
			}
			else if (OcgCore.handOffset != 0f)
			{
				OcgCore.handOffset = 0f;
				OcgCore.lastHandOffset = 0f;
				this.RefreshHandCardPositionInstant();
			}
			if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
			{
				Action returnAction = this.returnAction;
				if (returnAction != null)
				{
					returnAction();
				}
			}
			if (UserInput.MouseLeftDown)
			{
				this.DuelBGManager.HideEquipLine();
				this.DuelBGManager.HideTargetLines();
			}
			if (UserInput.WasCancelPressed || UserInput.WasSubmitPressed)
			{
				this.ToChat();
			}
			if (Program.instance.ui_.chatPanel.showing || OcgCore.inputMode)
			{
				return;
			}
			if (Keyboard.current != null)
			{
				if (Keyboard.current.aKey.wasPressedThisFrame)
				{
					OcgCore.chainCondition = OcgCore.ChainCondition.No;
					this.GetUI<OcgCoreUI>().OnTiming();
				}
				else if (Keyboard.current.sKey.wasPressedThisFrame)
				{
					OcgCore.chainCondition = (OcgCore.ChainCondition)(-1);
					this.GetUI<OcgCoreUI>().OnTiming();
				}
				else if (Keyboard.current.dKey.wasPressedThisFrame)
				{
					OcgCore.chainCondition = OcgCore.ChainCondition.All;
					this.GetUI<OcgCoreUI>().OnTiming();
				}
				if (Keyboard.current.tabKey.wasPressedThisFrame)
				{
					this.GetUI<OcgCoreUI>().OnLog(false);
				}
				if (Keyboard.current.gKey.wasPressedThisFrame)
				{
					if (this.greenOn)
					{
						this.GreenBackgroundOff();
					}
					else
					{
						this.GreenBackgroundOn();
					}
				}
				if (this.greenOn)
				{
					if (Keyboard.current.numpad0Key.wasPressedThisFrame || Keyboard.current.digit0Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = Color.black;
						return;
					}
					if (Keyboard.current.numpad1Key.wasPressedThisFrame || Keyboard.current.digit1Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = Color.red;
						return;
					}
					if (Keyboard.current.numpad2Key.wasPressedThisFrame || Keyboard.current.digit2Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = new Color(1f, 0.5f, 0f);
						return;
					}
					if (Keyboard.current.numpad3Key.wasPressedThisFrame || Keyboard.current.digit3Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = new Color(1f, 1f, 0f);
						return;
					}
					if (Keyboard.current.numpad4Key.wasPressedThisFrame || Keyboard.current.digit4Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = Color.green;
						return;
					}
					if (Keyboard.current.numpad5Key.wasPressedThisFrame || Keyboard.current.digit5Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = new Color(0f, 1f, 1f);
						return;
					}
					if (Keyboard.current.numpad6Key.wasPressedThisFrame || Keyboard.current.digit6Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = Color.blue;
						return;
					}
					if (Keyboard.current.numpad7Key.wasPressedThisFrame || Keyboard.current.digit7Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = new Color(0.6f, 0f, 1f);
						return;
					}
					if (Keyboard.current.numpad8Key.wasPressedThisFrame || Keyboard.current.digit8Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = Color.gray;
						return;
					}
					if (Keyboard.current.numpad9Key.wasPressedThisFrame || Keyboard.current.digit9Key.wasPressedThisFrame)
					{
						this.greenBackground.material.color = Color.white;
					}
				}
			}
		}

		// Token: 0x06008D55 RID: 36181 RVA: 0x0012A618 File Offset: 0x00128818
		public void GreenBackgroundOn()
		{
			this.greenBackground.gameObject.SetActive(true);
			this.GetUI<OcgCoreUI>().CG.alpha = 0f;
			this.GetUI<OcgCoreUI>().CG.blocksRaycasts = false;
			CameraManager.DuelOverlay3DPlus();
			this.greenOn = true;
		}

		// Token: 0x06008D56 RID: 36182 RVA: 0x0012A668 File Offset: 0x00128868
		public void GreenBackgroundOff()
		{
			this.greenBackground.gameObject.SetActive(false);
			this.GetUI<OcgCoreUI>().CG.alpha = 1f;
			this.GetUI<OcgCoreUI>().CG.blocksRaycasts = true;
			CameraManager.DuelOverlay3DMinus();
			this.greenOn = false;
		}

		// Token: 0x06008D57 RID: 36183 RVA: 0x0012A6B8 File Offset: 0x001288B8
		public void ToChat()
		{
			if (OcgCore.condition == OcgCore.Condition.Replay || OcgCore.inPuzzle)
			{
				return;
			}
			Program.instance.ui_.chatPanel.Switch();
		}

		// Token: 0x06008D58 RID: 36184 RVA: 0x0012A6E0 File Offset: 0x001288E0
		public void OnAnnounceCard(string input)
		{
			List<Card> datas = CardsManager.AnnounceSearch(input, OcgCore.ES_searchCodes);
			int max = datas.Count;
			if (max > 49)
			{
				max = 40;
			}
			List<GameCard> cards = new List<GameCard>();
			for (int i = 0; i < max; i++)
			{
				GPS p = new GPS
				{
					controller = 0U,
					location = 2048U,
					sequence = (uint)i,
					position = 0
				};
				GameCard card = this.GCS_Create(p, false);
				card.SetData(datas[i]);
				cards.Add(card);
			}
			this.currentPopup.whenQuitDo = delegate
			{
				this.GetUI<OcgCoreUI>().ShowPopupSelectCard(InterString.Get("请选择需要宣言的卡片。", 0), cards, 1, 1, true, false);
			};
		}

		// Token: 0x06008D59 RID: 36185 RVA: 0x0012A790 File Offset: 0x00128990
		public void ClearAnnounceCards()
		{
			List<GameCard> needClean = new List<GameCard>();
			foreach (GameCard card in OcgCore.cards)
			{
				if (card.p.location == 2048U)
				{
					needClean.Add(card);
				}
			}
			foreach (GameCard card2 in needClean)
			{
				OcgCore.cards.Remove(card2);
				card2.Dispose();
			}
		}

		// Token: 0x06008D5A RID: 36186 RVA: 0x0012A844 File Offset: 0x00128A44
		public void CloseConnection()
		{
			if (TcpHelper.tcpClient != null)
			{
				if (TcpHelper.tcpClient.Connected)
				{
					TcpHelper.tcpClient.Client.Shutdown(SocketShutdown.Receive);
					TcpHelper.tcpClient.Close();
				}
				TcpHelper.tcpClient = null;
			}
		}

		// Token: 0x06008D5B RID: 36187 RVA: 0x0012A87C File Offset: 0x00128A7C
		private bool DuelEndNeedExit()
		{
			if (Program.instance.room.duelEnded)
			{
				return true;
			}
			if (TcpHelper.tcpClient == null)
			{
				return true;
			}
			if (!TcpHelper.tcpClient.Connected)
			{
				return true;
			}
			if (OcgCore.surrendered)
			{
				if (RoomServant.Mode == 0)
				{
					return true;
				}
				if (RoomServant.Mode == 1)
				{
					return false;
				}
				if (RoomServant.Mode == 2)
				{
					return OcgCore.tagSurrendered;
				}
			}
			return false;
		}

		// Token: 0x06008D5C RID: 36188 RVA: 0x0012A8DC File Offset: 0x00128ADC
		public void OnDuelResultConfirmed(bool manual = false)
		{
			RoomServant.JoinWithReconnect = false;
			if (OcgCore.condition != OcgCore.Condition.Watch && this.DuelEndNeedExit())
			{
				OcgCore.surrendered = false;
				RoomServant.NeedSide = false;
				RoomServant.SideWaitingObserver = false;
				if (Program.instance.currentSubServant != null)
				{
					Program.instance.currentSubServant.Hide(-1);
					Program.instance.currentSubServant = null;
				}
				this.OnExit();
				return;
			}
			if (RoomServant.NeedSide)
			{
				RoomServant.NeedSide = false;
				MessageManager.Cast(InterString.Get("卡片历史中为您准备了对手上一局使用过的卡。", 0));
				this.returnServant = Program.instance.deckEditor;
				Program.instance.deckEditor.SwitchCondition(DeckEditor.Condition.ChangeSide, "", null);
				this.ReturnTo();
				return;
			}
			if (OcgCore.condition != OcgCore.Condition.Watch)
			{
				UIManager.ShowPopupYesOrNo(new List<string>
				{
					InterString.Get("投降", 0),
					InterString.Get("您确定要投降吗？", 0),
					InterString.Get("是", 0),
					InterString.Get("否", 0)
				}, new Action(this.ActionSurrender), null);
				return;
			}
			if (manual)
			{
				OcgCore.surrendered = false;
				RoomServant.NeedSide = false;
				RoomServant.SideWaitingObserver = false;
				if (Program.instance.currentSubServant != null)
				{
					Program.instance.currentSubServant.Hide(-1);
					Program.instance.currentSubServant = null;
				}
				TcpHelper.CtosMessage_LeaveGame();
				this.OnExit();
				return;
			}
			if (!OcgCore.duelEnded)
			{
				this.DuelBGManager.ResetFields();
				return;
			}
			if (TcpHelper.tcpClient == null || !TcpHelper.tcpClient.Connected)
			{
				this.OnExit();
				return;
			}
			base.Hide(0);
		}

		// Token: 0x06008D5D RID: 36189 RVA: 0x0012AA78 File Offset: 0x00128C78
		private void ActionSurrender()
		{
			OcgCore.surrendered = true;
			if (TcpHelper.tcpClient != null && TcpHelper.tcpClient.Connected)
			{
				TcpHelper.CtosMessage_Surrender();
				Program.instance.ExitCurrentServant();
				if (RoomServant.Mode == 2 && !OcgCore.tagSurrendered)
				{
					MessageManager.Cast(InterString.Get("您发起了投降。", 0));
					return;
				}
			}
			else
			{
				this.OnExit();
			}
		}

		// Token: 0x06008D5E RID: 36190 RVA: 0x0012AAD3 File Offset: 0x00128CD3
		public void AddPackage(Package p)
		{
			TcpHelper.AddRecordLine(p);
			OcgCore.packages.Add(p);
			OcgCore.allPackages.Add(p);
			this.CheckMessageIsNeedInstanceResponse(p);
		}

		// Token: 0x06008D5F RID: 36191 RVA: 0x0012AAF8 File Offset: 0x00128CF8
		private void CheckMessageIsNeedInstanceResponse(Package p)
		{
			if (p.Function == 5)
			{
				this.messageDispatcher.playerResponed = true;
			}
		}

		// Token: 0x06008D60 RID: 36192 RVA: 0x0012AB10 File Offset: 0x00128D10
		public void FlushPackages(List<Package> packs)
		{
			OcgCore.packages.Clear();
			OcgCore.packages = null;
			OcgCore.packages = packs;
			OcgCore.allPackages.Clear();
			foreach (Package p in OcgCore.packages)
			{
				OcgCore.allPackages.Add(p);
			}
		}

		// Token: 0x06008D61 RID: 36193 RVA: 0x0012AB88 File Offset: 0x00128D88
		public void SendReturn(byte[] buffer, float delay = 0f)
		{
			OcgCore.ResponseHandler responseHandler = OcgCore.handler;
			if (responseHandler != null)
			{
				responseHandler(buffer);
			}
			this.DuelBGManager.ClearResponse();
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, delay).OnComplete(delegate
			{
				this.messageDispatcher.playerResponed = true;
			});
		}

		// Token: 0x06008D62 RID: 36194 RVA: 0x0012ABF4 File Offset: 0x00128DF4
		public void OnResend()
		{
			BinaryMaster binaryMaster = new BinaryMaster(null);
			binaryMaster.writer.Write(-1);
			this.SendReturn(binaryMaster.Get(), 0f);
		}

		// Token: 0x06008D63 RID: 36195 RVA: 0x0012AC25 File Offset: 0x00128E25
		public void StocMessage_TeammateSurrender()
		{
			if (OcgCore.surrendered)
			{
				return;
			}
			OcgCore.tagSurrendered = true;
			MessageManager.Cast(InterString.Get("队友发起了投降。", 0));
		}

		// Token: 0x06008D64 RID: 36196 RVA: 0x0012AC48 File Offset: 0x00128E48
		public void StocMessage_TimeLimit(BinaryReader r)
		{
			int player = OcgCore.LocalPlayer((int)r.ReadByte());
			r.ReadByte();
			int timeLimit = (int)r.ReadInt16();
			TcpHelper.CtosMessage_TimeConfirm();
			if (this.DuelBGManager == null)
			{
				return;
			}
			this.DuelBGManager.SetTimeLimit(player, timeLimit);
		}

		// Token: 0x06008D65 RID: 36197 RVA: 0x0012AC8A File Offset: 0x00128E8A
		public void StocMessage_Error(string error)
		{
			this.ShowErrorMessageAsync(error);
		}

		// Token: 0x06008D66 RID: 36198 RVA: 0x0012AC94 File Offset: 0x00128E94
		private async UniTask ShowErrorMessageAsync(string error)
		{
			await UniTask.WaitWhile(() => this.servantUI == null, PlayerLoopTiming.Update, default(CancellationToken), false);
			UniTask.ReturnToMainThread(default(CancellationToken));
			this.GetUI<OcgCoreUI>().DuelErrorLog.Show(error);
		}

		// Token: 0x06008D67 RID: 36199 RVA: 0x0012ACE0 File Offset: 0x00128EE0
		public bool GetMessageConfig(int player)
		{
			if (player < 4 || player == 7)
			{
				if (OcgCore.condition == OcgCore.Condition.Duel && !Config.GetBool("DuelPlayerMessage", true))
				{
					return false;
				}
				if (OcgCore.condition == OcgCore.Condition.Watch && !Config.GetBool("WatchPlayerMessage", true))
				{
					return false;
				}
				if (OcgCore.condition == OcgCore.Condition.Replay && !Config.GetBool("ReplayPlayerMessage", true))
				{
					return false;
				}
			}
			else
			{
				if (OcgCore.condition == OcgCore.Condition.Duel && !Config.GetBool("DuelSystemMessage", true))
				{
					return false;
				}
				if (OcgCore.condition == OcgCore.Condition.Watch && !Config.GetBool("WatchSystemMessage", true))
				{
					return false;
				}
				if (OcgCore.condition == OcgCore.Condition.Replay && !Config.GetBool("ReplaySystemMessage", true))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008D68 RID: 36200 RVA: 0x0012AD80 File Offset: 0x00128F80
		public void ForceMSquit()
		{
			Package p = new Package
			{
				Function = 236
			};
			OcgCore.packages.Add(p);
		}

		// Token: 0x06008D69 RID: 36201 RVA: 0x0012ADA9 File Offset: 0x00128FA9
		public bool InIgnoranceReplay()
		{
			return OcgCore.condition != OcgCore.Condition.Duel;
		}

		// Token: 0x06008D6A RID: 36202 RVA: 0x0012ADB8 File Offset: 0x00128FB8
		public static Package GetNextPackage()
		{
			int target = 1;
			while (OcgCore.packages.Count > target)
			{
				if (OcgCore.packages[target].Function != 6 && OcgCore.packages[target].Function != 7)
				{
					return OcgCore.packages[target];
				}
				target++;
			}
			return null;
		}

		// Token: 0x06008D6B RID: 36203 RVA: 0x0012AE10 File Offset: 0x00129010
		public static bool NextMessageIs(GameMessage message)
		{
			Package p = OcgCore.GetNextPackage();
			return p != null && p.Function == (int)message;
		}

		// Token: 0x06008D6C RID: 36204 RVA: 0x0012AE34 File Offset: 0x00129034
		public static CardLocation NextMessageIsMovingToLocation()
		{
			Package p = OcgCore.GetNextPackage();
			if (p == null)
			{
				return CardLocation.Unknown;
			}
			if (p.Function == 50)
			{
				BinaryReader reader = p.Data.reader;
				reader.BaseStream.Seek(0L, SeekOrigin.Begin);
				reader.ReadInt32();
				reader.ReadGPS();
				GPS gps = reader.ReadGPS();
				OcgCore.nextMoveMessageController = gps.controller;
				return (CardLocation)gps.location;
			}
			return CardLocation.Unknown;
		}

		// Token: 0x06008D6D RID: 36205 RVA: 0x0012AE98 File Offset: 0x00129098
		public static bool NextMessageIsMovingFrom(CardLocation location)
		{
			Package p = OcgCore.GetNextPackage();
			if (p == null)
			{
				return false;
			}
			if (p.Function == 50)
			{
				BinaryReader reader = p.Data.reader;
				reader.BaseStream.Seek(0L, SeekOrigin.Begin);
				reader.ReadInt32();
				if ((reader.ReadGPS().location & (uint)location) > 0U)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008D6E RID: 36206 RVA: 0x0012AEF0 File Offset: 0x001290F0
		public static bool NextMessageIsMovingTo(CardLocation location, uint player)
		{
			Package p = OcgCore.GetNextPackage();
			if (p == null)
			{
				return false;
			}
			if (p.Function == 50)
			{
				BinaryReader reader = p.Data.reader;
				reader.BaseStream.Seek(0L, SeekOrigin.Begin);
				reader.ReadInt32();
				reader.ReadGPS();
				GPS to = reader.ReadGPS();
				if (player == to.controller && (to.location & (uint)location) > 0U)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008D6F RID: 36207 RVA: 0x0012AF57 File Offset: 0x00129157
		public static bool NextMessageIsMovingToGrave(uint player)
		{
			return ((player == 0U) ? OcgCore.movingToMyGrave : OcgCore.movingToOpGrave) < 4 && OcgCore.NextMessageIsMovingTo(CardLocation.Grave, player);
		}

		// Token: 0x06008D70 RID: 36208 RVA: 0x0012AF7A File Offset: 0x0012917A
		public static bool NextMessageIsMovingToExclude(uint player)
		{
			return ((player == 0U) ? OcgCore.movingToMyExclude : OcgCore.movingToOpExclude) < 4 && OcgCore.NextMessageIsMovingTo(CardLocation.Removed, player);
		}

		// Token: 0x06008D71 RID: 36209 RVA: 0x0012AFA0 File Offset: 0x001291A0
		public static bool CanSyncNextMove(GPS from, GPS to)
		{
			if (!from.InLocation(CardLocation.Grave) && to.InLocation(CardLocation.Grave))
			{
				CardLocation location = OcgCore.NextMessageIsMovingToLocation();
				if (location == CardLocation.Grave)
				{
					return OcgCore.NextMessageIsMovingToGrave(OcgCore.nextMoveMessageController);
				}
				return location == CardLocation.Removed && OcgCore.NextMessageIsMovingToExclude(OcgCore.nextMoveMessageController);
			}
			else
			{
				if (from.InLocation(CardLocation.Removed) || !to.InLocation(CardLocation.Removed))
				{
					return false;
				}
				CardLocation location2 = OcgCore.NextMessageIsMovingToLocation();
				if (location2 == CardLocation.Removed)
				{
					return OcgCore.NextMessageIsMovingToExclude(OcgCore.nextMoveMessageController);
				}
				return location2 == CardLocation.Grave && OcgCore.NextMessageIsMovingToGrave(OcgCore.nextMoveMessageController);
			}
		}

		// Token: 0x06008D72 RID: 36210 RVA: 0x0012B028 File Offset: 0x00129228
		private async UniTask ProcessMessage()
		{
			this.messageDispatcher.Dispose();
			try
			{
				while (this.showing)
				{
					if (!this.messageDispatcher.duel.duelBGManager.loaded)
					{
						await UniTask.Yield();
					}
					else
					{
						await UniTask.WaitWhile(() => OcgCore.pause, PlayerLoopTiming.Update, default(CancellationToken), false);
						if (OcgCore.packages.Count == 0)
						{
							await UniTask.Yield();
						}
						else
						{
							OcgCore.NoMoreWait = false;
							OcgCore.currentMessage = (GameMessage)OcgCore.packages[0].Function;
							try
							{
								await UniTask.WhenAny(new UniTask[]
								{
									this.messageDispatcher.Process(OcgCore.packages[0]),
									UniTask.WaitUntil(() => OcgCore.NoMoreWait, PlayerLoopTiming.Update, default(CancellationToken), false)
								});
							}
							catch (Exception ex)
							{
								Debug.Log(ex);
							}
							OcgCore.lastMessage = OcgCore.currentMessage;
							if (OcgCore.packages.Count == 0)
							{
								break;
							}
							OcgCore.packages.RemoveAt(0);
							if (OcgCore.condition == OcgCore.Condition.Replay && OcgCore.packages.Count == 0)
							{
								MessageManager.Cast(InterString.Get("回放播放结束。", 0));
								break;
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Debug.Log(ex2);
			}
		}

		// Token: 0x06008D73 RID: 36211 RVA: 0x0012B06B File Offset: 0x0012926B
		public static int LocalPlayer(int p)
		{
			if (p != 0 && p != 1)
			{
				return p;
			}
			if (OcgCore.isFirst)
			{
				return p;
			}
			return 1 - p;
		}

		// Token: 0x06008D74 RID: 36212 RVA: 0x0012B084 File Offset: 0x00129284
		public static int[] GetSelectLevelSum(List<GameCard> cards)
		{
			int sum = 0;
			foreach (GameCard card in cards)
			{
				sum += card.levelForSelect_1;
			}
			int sum2 = 0;
			foreach (GameCard card2 in cards)
			{
				sum2 += card2.levelForSelect_2;
			}
			return new int[] { sum, sum2 };
		}

		// Token: 0x06008D75 RID: 36213 RVA: 0x0012B128 File Offset: 0x00129328
		public static bool CheckSelectableInSum(List<GameCard> cards, GameCard card, List<GameCard> selectedCards, int max)
		{
			if (selectedCards.Count >= max)
			{
				return false;
			}
			bool returnValue = false;
			int[] sum = OcgCore.GetSelectLevelSum(selectedCards);
			if (sum[0] + card.levelForSelect_1 == OcgCore.ES_level || sum[1] + card.levelForSelect_2 == OcgCore.ES_level)
			{
				return true;
			}
			if (sum[0] + card.levelForSelect_1 > OcgCore.ES_level || sum[1] + card.levelForSelect_2 > OcgCore.ES_level)
			{
				return false;
			}
			List<GameCard> newSelectedCards = new List<GameCard>(selectedCards) { card };
			foreach (GameCard c in cards)
			{
				if (!newSelectedCards.Contains(c))
				{
					returnValue = OcgCore.CheckSelectableInSum(cards, c, newSelectedCards, max);
					if (returnValue)
					{
						return true;
					}
				}
			}
			return returnValue;
		}

		// Token: 0x06008D76 RID: 36214 RVA: 0x0012B1FC File Offset: 0x001293FC
		public static bool TypeMatchReason(int type, int reason)
		{
			return (((long)type & 128L) > 0L && ((long)reason & 1048576L) > 0L) || (((long)type & 64L) > 0L && ((long)reason & 262144L) > 0L) || (((long)type & 8192L) > 0L && ((long)reason & 524288L) > 0L) || (((long)type & 8388608L) > 0L && ((long)reason & 2097152L) > 0L) || (((long)type & 67108864L) > 0L && ((long)reason & 268435456L) > 0L);
		}

		// Token: 0x06008D77 RID: 36215 RVA: 0x0012B294 File Offset: 0x00129494
		public GameCard GCS_Create(GPS p, bool temp = false)
		{
			GameCard c = Program.instance.container_3D.gameObject.AddComponent<GameCard>();
			c.p = p;
			OcgCore.cards.Add(c);
			if (temp)
			{
				OcgCore.tempCards.Add(c);
			}
			return c;
		}

		// Token: 0x06008D78 RID: 36216 RVA: 0x0012B2D8 File Offset: 0x001294D8
		public GameCard GCS_Get(GPS p)
		{
			GameCard c = null;
			if ((p.location & 128U) > 0U)
			{
				for (int i = 0; i < OcgCore.cards.Count; i++)
				{
					if (OcgCore.cards[i].p.location == p.location && OcgCore.cards[i].p.controller == p.controller && OcgCore.cards[i].p.sequence == p.sequence && OcgCore.cards[i].p.position == p.position)
					{
						c = OcgCore.cards[i];
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < OcgCore.cards.Count; j++)
				{
					if (OcgCore.cards[j].p.location == p.location && OcgCore.cards[j].p.controller == p.controller && OcgCore.cards[j].p.sequence == p.sequence)
					{
						c = OcgCore.cards[j];
						break;
					}
				}
			}
			if (p.location == 0U)
			{
				c = null;
			}
			return c;
		}

		// Token: 0x06008D79 RID: 36217 RVA: 0x0012B420 File Offset: 0x00129620
		public List<GameCard> GCS_GetLocationCards(int controller, int location)
		{
			List<GameCard> cardsInLocation = new List<GameCard>();
			for (int i = 0; i < OcgCore.cards.Count; i++)
			{
				if (!OcgCore.tempCards.Contains(OcgCore.cards[i]) && (ulong)OcgCore.cards[i].p.location == (ulong)((long)location) && (ulong)OcgCore.cards[i].p.controller == (ulong)((long)controller))
				{
					cardsInLocation.Add(OcgCore.cards[i]);
				}
			}
			return cardsInLocation;
		}

		// Token: 0x06008D7A RID: 36218 RVA: 0x0012B4A8 File Offset: 0x001296A8
		public List<GameCard> GCS_GetOverlays(GameCard c)
		{
			List<GameCard> overlays = new List<GameCard>();
			if (c != null && (c.p.location & 128U) == 0U)
			{
				for (int i = 0; i < OcgCore.cards.Count; i++)
				{
					if ((OcgCore.cards[i].p.location & 128U) > 0U && OcgCore.cards[i].p.controller == c.p.controller && (OcgCore.cards[i].p.location | 128U) == (c.p.location | 128U) && OcgCore.cards[i].p.sequence == c.p.sequence)
					{
						overlays.Add(OcgCore.cards[i]);
					}
				}
			}
			return overlays;
		}

		// Token: 0x06008D7B RID: 36219 RVA: 0x0012B5A0 File Offset: 0x001297A0
		public void GCS_CreateBundle(int count, int controller, CardLocation location)
		{
			for (int i = 0; i < count; i++)
			{
				this.GCS_Create(new GPS
				{
					controller = (uint)controller,
					location = (uint)location,
					position = 2,
					sequence = (uint)i
				}, false);
			}
		}

		// Token: 0x06008D7C RID: 36220 RVA: 0x0012B5E4 File Offset: 0x001297E4
		public List<GameCard> GCS_ResizeBundle(int count, int player, CardLocation location)
		{
			List<GameCard> cardBow = new List<GameCard>();
			List<GameCard> waterOutOfBow = new List<GameCard>();
			for (int i = 0; i < OcgCore.cards.Count; i++)
			{
				if ((OcgCore.cards[i].p.location & (uint)location) > 0U && (ulong)OcgCore.cards[i].p.controller == (ulong)((long)player))
				{
					if (cardBow.Count < count)
					{
						cardBow.Add(OcgCore.cards[i]);
					}
					else
					{
						waterOutOfBow.Add(OcgCore.cards[i]);
					}
				}
			}
			using (List<GameCard>.Enumerator enumerator = waterOutOfBow.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GameCard card = enumerator.Current;
					OcgCore.cards.Remove(card);
					if ((card.p.location & 2U) > 0U)
					{
						card.AnimationShuffle(0.15f);
					}
					else
					{
						card.Dispose();
					}
				}
				goto IL_0117;
			}
			IL_00E0:
			GameCard card2 = this.GCS_Create(new GPS
			{
				controller = (uint)player,
				location = (uint)location,
				position = 2,
				sequence = (uint)cardBow.Count
			}, false);
			cardBow.Add(card2);
			IL_0117:
			if (cardBow.Count >= count)
			{
				foreach (GameCard gameCard in cardBow)
				{
					gameCard.EraseData();
					gameCard.p.position = 2;
				}
				return cardBow;
			}
			goto IL_00E0;
		}

		// Token: 0x06008D7D RID: 36221 RVA: 0x0012B768 File Offset: 0x00129968
		public void ArrangeCards()
		{
			OcgCore.cards.Sort(delegate(GameCard left, GameCard right)
			{
				int a = 1;
				if (left.p.controller > right.p.controller)
				{
					a = 1;
				}
				else if (left.p.controller < right.p.controller)
				{
					a = -1;
				}
				else if (left.p.location == 2U && right.p.location != 2U)
				{
					a = -1;
				}
				else if (left.p.location != 2U && right.p.location == 2U)
				{
					a = 1;
				}
				else if ((left.p.location | 128U) > (right.p.location | 128U))
				{
					a = -1;
				}
				else if ((left.p.location | 128U) < (right.p.location | 128U))
				{
					a = 1;
				}
				else if (left.p.sequence > right.p.sequence)
				{
					a = 1;
				}
				else if (left.p.sequence < right.p.sequence)
				{
					a = -1;
				}
				else if ((left.p.location & 128U) > (right.p.location & 128U))
				{
					a = -1;
				}
				else if ((left.p.location & 128U) < (right.p.location & 128U))
				{
					a = 1;
				}
				else if (left.p.position > right.p.position)
				{
					a = 1;
				}
				else if (left.p.position < right.p.position)
				{
					a = -1;
				}
				return a;
			});
			uint preController = 9999U;
			uint preLocation = 9999U;
			uint preSequence = 9999U;
			uint sequenceWriter = 0U;
			int positionWriter = 0;
			for (int i = 0; i < OcgCore.cards.Count; i++)
			{
				if (OcgCore.cards[i])
				{
					if (preController != OcgCore.cards[i].p.controller)
					{
						sequenceWriter = 0U;
					}
					if ((preLocation | 128U) != (OcgCore.cards[i].p.location | 128U))
					{
						sequenceWriter = 0U;
					}
					if (preSequence != OcgCore.cards[i].p.sequence)
					{
						positionWriter = 0;
					}
					if ((OcgCore.cards[i].p.location & 4U) == 0U && (OcgCore.cards[i].p.location & 8U) == 0U)
					{
						OcgCore.cards[i].p.sequence = sequenceWriter;
					}
					if ((OcgCore.cards[i].p.location & 128U) > 0U)
					{
						OcgCore.cards[i].p.position = positionWriter;
						positionWriter++;
					}
					else
					{
						sequenceWriter += 1U;
					}
					preController = OcgCore.cards[i].p.controller;
					preLocation = OcgCore.cards[i].p.location;
					preSequence = OcgCore.cards[i].p.sequence;
				}
			}
		}

		// Token: 0x06008D7E RID: 36222 RVA: 0x0012B91C File Offset: 0x00129B1C
		public int GetMyHandCount()
		{
			if (OcgCore.needRefreshMyHand)
			{
				OcgCore.myHandCards = new List<GameCard>(OcgCore.myPreHandCards);
				foreach (GameCard card in OcgCore.cards)
				{
					if (card.p.controller == 0U && (card.p.location & 2U) > 0U && !OcgCore.myHandCards.Contains(card))
					{
						OcgCore.myHandCards.Add(card);
					}
				}
				OcgCore.needRefreshMyHand = false;
			}
			return OcgCore.myHandCards.Count;
		}

		// Token: 0x06008D7F RID: 36223 RVA: 0x0012B9C4 File Offset: 0x00129BC4
		public int GetOpHandCount()
		{
			if (OcgCore.needRefreshOpHand)
			{
				OcgCore.opHandCards = new List<GameCard>(OcgCore.opPreHandCards);
				foreach (GameCard card in OcgCore.cards)
				{
					if (card.p.controller != 0U && (card.p.location & 2U) > 0U && !OcgCore.opHandCards.Contains(card))
					{
						OcgCore.opHandCards.Add(card);
					}
				}
				OcgCore.needRefreshOpHand = false;
			}
			return OcgCore.opHandCards.Count;
		}

		// Token: 0x06008D80 RID: 36224 RVA: 0x0012BA6C File Offset: 0x00129C6C
		public int GetLocationCardCount(CardLocation location, uint controller)
		{
			int count = 0;
			foreach (GameCard card in OcgCore.cards)
			{
				if ((card.p.location & (uint)location) > 0U && card.p.controller == controller)
				{
					count++;
				}
			}
			return count;
		}

		// Token: 0x06008D81 RID: 36225 RVA: 0x0012BADC File Offset: 0x00129CDC
		public Package GetNamePacket()
		{
			Package package = new Package();
			package.Function = 235;
			package.Data = new BinaryMaster(null);
			package.Data.writer.WriteUnicode(OcgCore.name_0, 50);
			package.Data.writer.WriteUnicode(OcgCore.name_0_tag, 50);
			package.Data.writer.WriteUnicode((OcgCore.name_0_c != "") ? OcgCore.name_0_c : OcgCore.name_0, 50);
			package.Data.writer.WriteUnicode(OcgCore.name_1, 50);
			package.Data.writer.WriteUnicode(OcgCore.name_1_tag, 50);
			package.Data.writer.WriteUnicode((OcgCore.name_1_c != "") ? OcgCore.name_1_c : OcgCore.name_1, 50);
			package.Data.writer.Write(OcgCore.MasterRule);
			return package;
		}

		// Token: 0x06008D82 RID: 36226 RVA: 0x0012BBD4 File Offset: 0x00129DD4
		public bool GetAutoInfo()
		{
			return (OcgCore.condition != OcgCore.Condition.Duel || !(Config.Get("DuelAutoInfo", "0") == "0")) && (OcgCore.condition != OcgCore.Condition.Watch || !(Config.Get("WatchAutoInfo", "0") == "0")) && (OcgCore.condition != OcgCore.Condition.Replay || !(Config.Get("ReplayAutoInfo", "0") == "0"));
		}

		// Token: 0x06008D83 RID: 36227 RVA: 0x0012BC54 File Offset: 0x00129E54
		public void RefreshAllCardsLabel()
		{
			if (!this.showing)
			{
				return;
			}
			foreach (GameCard gameCard in OcgCore.cards)
			{
				gameCard.RefreshLabel();
			}
		}

		// Token: 0x06008D84 RID: 36228 RVA: 0x0012BCAC File Offset: 0x00129EAC
		public bool CurrentChainDisabled(int currentChain)
		{
			for (int i = 0; i < OcgCore.packages.Count; i++)
			{
				if (OcgCore.packages[i].Function == 76)
				{
					BinaryReader reader = OcgCore.packages[i].Data.reader;
					reader.BaseStream.Seek(0L, SeekOrigin.Begin);
					if ((int)reader.ReadByte() == currentChain)
					{
						return true;
					}
				}
				if (OcgCore.packages[i].Function == 73)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06008D85 RID: 36229 RVA: 0x0012BD28 File Offset: 0x00129F28
		public int GetNextConfirmedCardCode()
		{
			for (int i = 0; i < OcgCore.packages.Count; i++)
			{
				if (OcgCore.packages[i].Function == 31)
				{
					BinaryReader reader = OcgCore.packages[i].Data.reader;
					reader.BaseStream.Seek(0L, SeekOrigin.Begin);
					reader.ReadByte();
					if (OcgCore.condition != OcgCore.Condition.Replay || OcgCore.CurrentReplayUseYRP2)
					{
						reader.ReadByte();
					}
					reader.ReadByte();
					return reader.ReadInt32();
				}
			}
			return 0;
		}

		// Token: 0x06008D86 RID: 36230 RVA: 0x0012BDB0 File Offset: 0x00129FB0
		public int GetUpdateDataIdByGameCard(GameCard card)
		{
			for (int i = 0; i < OcgCore.packages.Count; i++)
			{
				if (OcgCore.packages[i].Function == 6)
				{
					BinaryReader reader = OcgCore.packages[i].Data.reader;
					reader.BaseStream.Seek(0L, SeekOrigin.Begin);
					int player = OcgCore.LocalPlayer((int)reader.ReadChar());
					char location = reader.ReadChar();
					if ((long)player == (long)((ulong)card.p.controller) && ((uint)location & card.p.location) != 0U)
					{
						int code;
						for (;;)
						{
							int len = reader.ReadInt32();
							if (len != 4)
							{
								long pos = reader.BaseStream.Position;
								int num = reader.ReadInt32();
								code = 0;
								if ((num & 1) != 0)
								{
									code = reader.ReadInt32();
								}
								if ((num & 2) != 0)
								{
									GPS gps = reader.ReadGPS();
									GameCard cardToRefresh = Program.instance.ocgcore.GCS_Get(gps);
									if (cardToRefresh != null && cardToRefresh == card)
									{
										break;
									}
									reader.BaseStream.Position = pos + (long)len - 4L;
								}
							}
						}
						return code;
					}
				}
			}
			return 0;
		}

		// Token: 0x06008D87 RID: 36231 RVA: 0x0012BEC5 File Offset: 0x0012A0C5
		public void Chat(int player, string content)
		{
			if (!this.GetMessageConfig(player))
			{
				return;
			}
			if (player == 7 || player < 4)
			{
				MessageManager.Cast(ChatPanel.GetPlayerName(player) + ": " + content);
				return;
			}
			MessageManager.Cast(content);
		}

		// Token: 0x06008D88 RID: 36232 RVA: 0x0012BEF6 File Offset: 0x0012A0F6
		public static void PrintDuelLog(string content)
		{
			OcgCore.lastDuelLog = content;
			MessageManager.Cast(content);
		}

		// Token: 0x06008D89 RID: 36233 RVA: 0x0012BF04 File Offset: 0x0012A104
		public void SetFace()
		{
			if (OcgCore.condition == OcgCore.Condition.Duel)
			{
				int selfType = RoomServant.SelfType;
				if (this.GetUI<OcgCoreUI>().TextPlayer0Name.text == OcgCore.name_0)
				{
					if (OcgCore.isTag)
					{
						if (selfType == 0 || selfType == 2)
						{
							this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.duelFrameMat0;
							this.SetFaceWhenCharaOff(Appearance.duelFace0, 0);
						}
						else
						{
							this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.duelFrameMat0Tag;
							this.SetFaceWhenCharaOff(Appearance.duelFace0Tag, 0);
						}
					}
					else
					{
						this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.duelFrameMat0;
						this.SetFaceWhenCharaOff(Appearance.duelFace0, 0);
					}
				}
				else if (selfType == 0 || selfType == 2)
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.duelFrameMat0Tag;
					this.SetFaceWhenCharaOff(Appearance.duelFace0Tag, 0);
				}
				else
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.duelFrameMat0;
					this.SetFaceWhenCharaOff(Appearance.duelFace0, 0);
				}
				if (this.GetUI<OcgCoreUI>().TextPlayer1Name.text == OcgCore.name_1)
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer1.material = Appearance.duelFrameMat1;
					this.SetFaceWhenCharaOff(Appearance.duelFace1, 1);
				}
				else
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer1.material = Appearance.duelFrameMat1Tag;
					this.SetFaceWhenCharaOff(Appearance.duelFace1Tag, 1);
				}
			}
			else if (OcgCore.condition == OcgCore.Condition.Watch)
			{
				if (this.GetUI<OcgCoreUI>().TextPlayer0Name.text == OcgCore.name_0)
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.watchFrameMat0;
					this.SetFaceWhenCharaOff(Appearance.watchFace0, 0);
				}
				else
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.watchFrameMat0Tag;
					this.SetFaceWhenCharaOff(Appearance.watchFace0Tag, 0);
				}
				if (this.GetUI<OcgCoreUI>().TextPlayer1Name.text == OcgCore.name_1)
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer1.material = Appearance.watchFrameMat1;
					this.SetFaceWhenCharaOff(Appearance.watchFace1, 1);
				}
				else
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer1.material = Appearance.watchFrameMat1Tag;
					this.SetFaceWhenCharaOff(Appearance.watchFace1Tag, 1);
				}
			}
			else if (OcgCore.condition == OcgCore.Condition.Replay)
			{
				if (this.GetUI<OcgCoreUI>().TextPlayer0Name.text == OcgCore.name_0)
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.replayFrameMat0;
					this.SetFaceWhenCharaOff(Appearance.replayFace0, 0);
				}
				else
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer0.material = Appearance.replayFrameMat0Tag;
					this.SetFaceWhenCharaOff(Appearance.replayFace0Tag, 0);
				}
				if (this.GetUI<OcgCoreUI>().TextPlayer1Name.text == OcgCore.name_1)
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer1.material = Appearance.replayFrameMat1;
					this.SetFaceWhenCharaOff(Appearance.replayFace1, 1);
				}
				else
				{
					this.GetUI<OcgCoreUI>().AvatarPlayer1.material = Appearance.replayFrameMat1Tag;
					this.SetFaceWhenCharaOff(Appearance.replayFace1Tag, 1);
				}
			}
			this.SetMyCardFace();
		}

		// Token: 0x06008D8A RID: 36234 RVA: 0x0012C210 File Offset: 0x0012A410
		private async UniTask SetMyCardFace()
		{
			if (MyCard.account != null && OcgCore.mycardDuel)
			{
				Texture2D avatar = await MyCard.GetAvatarAsync(this.GetUI<OcgCoreUI>().TextPlayer0Name.text);
				if (avatar != null)
				{
					this.SetFaceWhenCharaOff(TextureManager.Texture2Sprite(avatar), 0);
				}
				avatar = await MyCard.GetAvatarAsync(this.GetUI<OcgCoreUI>().TextPlayer1Name.text);
				if (avatar != null)
				{
					this.SetFaceWhenCharaOff(TextureManager.Texture2Sprite(avatar), 1);
				}
			}
		}

		// Token: 0x06008D8B RID: 36235 RVA: 0x0012C254 File Offset: 0x0012A454
		private void SetFaceWhenCharaOff(Sprite sprite, int player)
		{
			if (player == 0)
			{
				this.mySprite = sprite;
			}
			else
			{
				this.opSprite = sprite;
			}
			if (!this.charaFaceSetting)
			{
				this.GetUI<OcgCoreUI>().AvatarPlayer0.sprite = this.mySprite;
				this.GetUI<OcgCoreUI>().AvatarPlayer1.sprite = this.opSprite;
			}
		}

		// Token: 0x06008D8C RID: 36236 RVA: 0x0012C2A8 File Offset: 0x0012A4A8
		public void CloseCharaFace()
		{
			if (!this.charaFaceSetting)
			{
				return;
			}
			this.charaFaceSetting = false;
			this.SetFaceWhenCharaOff(this.mySprite, 0);
		}

		// Token: 0x06008D8D RID: 36237 RVA: 0x0012C2C7 File Offset: 0x0012A4C7
		public void CheckCharaFace()
		{
			if (!this.showing)
			{
				return;
			}
			if (this.NeedVoice())
			{
				this.SetCharacterDefaultFace();
				return;
			}
			this.CloseCharaFace();
		}

		// Token: 0x06008D8E RID: 36238 RVA: 0x0012C2E8 File Offset: 0x0012A4E8
		public void SetLP(int player, int val, bool first = false)
		{
			if (first)
			{
				this.GetUI<OcgCoreUI>().TextPlayer0LP.text = OcgCore.life0.ToString();
				this.GetUI<OcgCoreUI>().TextPlayer1LP.text = OcgCore.life1.ToString();
				return;
			}
			this.AnimationLpChange(player, val);
		}

		// Token: 0x06008D8F RID: 36239 RVA: 0x0012C338 File Offset: 0x0012A538
		private Sequence AnimationLpChange(int player, int val)
		{
			TextMeshProUGUI text;
			int targetLP;
			if (player == 0)
			{
				text = this.GetUI<OcgCoreUI>().TextPlayer0LP;
				targetLP = OcgCore.life0;
			}
			else
			{
				text = this.GetUI<OcgCoreUI>().TextPlayer1LP;
				targetLP = OcgCore.life1;
			}
			int origin = targetLP - val;
			Sequence sequence = DOTween.Sequence();
			GameObject obj = ABLoader.LoadMasterDuelGameObject("DuelLpText");
			obj.GetComponent<TextMeshProUGUI>().text = Math.Abs(val).ToString();
			obj.transform.SetParent(this.GetUI<OcgCoreUI>().RectPopup, false);
			float uiWidth = (float)Screen.width * 1080f / (float)Screen.height;
			Color color = DuelLog.damageColor;
			string seType = "COUNT";
			float fontSize = 120f;
			if (val > 0)
			{
				color = DuelLog.recoverColor;
				seType = "RECOVERY";
			}
			obj.GetComponent<TextMeshProUGUI>().color = color;
			if (player == 0)
			{
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -400f);
				obj.transform.localScale = Vector3.zero;
				float targetX = -(uiWidth / 2f - 325f);
				sequence.Append(obj.transform.DOScale(1f, 0.1f));
				sequence.AppendInterval(0.6f);
				sequence.Append(obj.GetComponent<RectTransform>().DOAnchorPosX(targetX, 0.2f, false));
				sequence.Join(DOTween.To(() => fontSize, delegate(float x)
				{
					fontSize = x;
					obj.GetComponent<TextMeshProUGUI>().fontSize = (float)((int)fontSize);
				}, 40f, 0.2f));
				sequence.Append(obj.GetComponent<RectTransform>().DOAnchorPosY(-490f, 0.2f, false));
			}
			else
			{
				obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 400f);
				obj.transform.localScale = Vector3.zero;
				float targetX2 = uiWidth / 2f - 225f;
				sequence.Append(obj.transform.DOScale(1f, 0.1f));
				sequence.AppendInterval(0.6f);
				sequence.Append(obj.GetComponent<RectTransform>().DOAnchorPosX(targetX2, 0.2f, false));
				sequence.Join(DOTween.To(() => fontSize, delegate(float x)
				{
					fontSize = x;
					obj.GetComponent<TextMeshProUGUI>().fontSize = (float)((int)fontSize);
				}, 40f, 0.2f));
				sequence.Append(obj.GetComponent<RectTransform>().DOAnchorPosY(450f, 0.2f, false));
			}
			sequence.Join(obj.GetComponent<TextMeshProUGUI>().DOFade(0f, 0.2f).OnComplete(delegate
			{
				AudioManager.PlaySE("SE_LP_" + seType + ((player == 0) ? "_PLAYER" : "_RIVAL"), 1f);
				float flp = (float)origin;
				DOTween.To(() => flp, delegate(float x)
				{
					flp = x;
					text.text = ((int)flp).ToString();
				}, (float)((targetLP < 0) ? 0 : targetLP), 1.2f);
				global::UnityEngine.Object.Destroy(obj);
			}));
			sequence.Append(text.DOColor(color, 0.1f));
			sequence.Join(text.transform.DOScale(1.3f, 0.2f));
			sequence.AppendInterval(0.8f);
			sequence.Append(text.transform.DOScale(1f, 0.2f));
			sequence.Join(text.DOColor(Color.white, 0.2f));
			return sequence;
		}

		// Token: 0x06008D90 RID: 36240 RVA: 0x0012C6D0 File Offset: 0x0012A8D0
		private void RefreshHandCardPositionInstant()
		{
			OcgCore.hideMyHandCard = false;
			if (this.showing)
			{
				foreach (GameCard gameCard in OcgCore.cards)
				{
					gameCard.SetHandDefault();
				}
			}
		}

		// Token: 0x06008D91 RID: 36241 RVA: 0x0012C730 File Offset: 0x0012A930
		public void RefreshHandCardPosition()
		{
			if (this.showing)
			{
				foreach (GameCard gameCard in OcgCore.cards)
				{
					gameCard.SetHandToDefault();
				}
			}
		}

		// Token: 0x06008D92 RID: 36242 RVA: 0x0012C788 File Offset: 0x0012A988
		public void RefreshMyHandCardPosition()
		{
			if (this.showing)
			{
				foreach (GameCard card in OcgCore.cards)
				{
					if (card.p.InMyControl())
					{
						card.SetHandToDefault();
					}
				}
			}
		}

		// Token: 0x06008D93 RID: 36243 RVA: 0x0012C7F0 File Offset: 0x0012A9F0
		public void RefreshOpHandCardPosition()
		{
			if (this.showing)
			{
				foreach (GameCard card in OcgCore.cards)
				{
					if (!card.p.InMyControl())
					{
						card.SetHandToDefault();
					}
				}
			}
		}

		// Token: 0x06008D94 RID: 36244 RVA: 0x0012C858 File Offset: 0x0012AA58
		public void FieldSelect(string hint, List<GameCard> cards, int min, int max, bool exitable, bool sendable)
		{
			foreach (PlaceSelector placeSelector in this.places)
			{
				placeSelector.InitializeSelectCardInThisZone(cards);
			}
			this.fieldHint = (string.IsNullOrEmpty(hint) ? InterString.Get("请选择卡片", 0) : hint);
			this.fieldMin = min;
			this.fieldMax = max;
			this.fieldExitable = exitable;
			this.fieldSendable = sendable;
			this.fieldCounterCount = 0;
			if (OcgCore.currentMessage == GameMessage.SelectCard || OcgCore.currentMessage == GameMessage.SelectCounter)
			{
				this.GetUI<OcgCoreUI>().SetHint(string.Concat(new string[]
				{
					this.fieldHint,
					": ",
					0.ToString(),
					"/",
					this.fieldMax.ToString()
				}));
			}
			else if (OcgCore.currentMessage == GameMessage.SelectSum)
			{
				if (!OcgCore.ES_overFlow)
				{
					foreach (PlaceSelector place in this.places)
					{
						if (place.cardSelecting && !place.cardSelected)
						{
							if (OcgCore.CheckSelectableInSum(OcgCore.cardsInSelection, place.cookieCard, OcgCore.cardsMustBeSelected, OcgCore.ES_max + OcgCore.cardsMustBeSelected.Count))
							{
								place.CardInThisZoneSelectable();
							}
							else
							{
								place.CardInThisZoneUnselectable();
							}
						}
					}
				}
				this.GetUI<OcgCoreUI>().SetHint(string.Concat(new string[]
				{
					this.fieldHint,
					": ",
					OcgCore.GetSelectLevelSum(OcgCore.cardsMustBeSelected)[0].ToString(),
					"/",
					OcgCore.ES_level.ToString()
				}));
			}
			else if (!string.IsNullOrEmpty(this.fieldHint))
			{
				this.GetUI<OcgCoreUI>().SetHint(this.fieldHint);
			}
			this.RefreshButton();
		}

		// Token: 0x06008D95 RID: 36245 RVA: 0x0012CA58 File Offset: 0x0012AC58
		public void FieldSelectRefresh(GameCard card)
		{
			List<GameCard> selected = new List<GameCard>();
			foreach (PlaceSelector place in this.places)
			{
				if (place.cardSelecting && place.cardSelected)
				{
					selected.Add(place.cookieCard);
				}
			}
			if (OcgCore.currentMessage == GameMessage.SelectSum)
			{
				int[] sum = OcgCore.GetSelectLevelSum(selected);
				if ((OcgCore.ES_overFlow && (OcgCore.ES_level <= sum[0] || OcgCore.ES_level <= sum[1])) || (!OcgCore.ES_overFlow && (OcgCore.ES_level == sum[0] || OcgCore.ES_level == sum[1])))
				{
					this.fieldSendable = true;
				}
				else
				{
					this.fieldSendable = false;
				}
				if (!OcgCore.ES_overFlow)
				{
					if (sum[0] == OcgCore.ES_level || sum[1] == OcgCore.ES_level)
					{
						this.FieldSelectedSend();
						return;
					}
					foreach (PlaceSelector place2 in this.places)
					{
						if (place2.cardSelecting && !place2.cardSelected)
						{
							if (OcgCore.CheckSelectableInSum(OcgCore.cardsInSelection, place2.cookieCard, selected, OcgCore.ES_max + OcgCore.cardsMustBeSelected.Count))
							{
								place2.CardInThisZoneSelectable();
							}
							else
							{
								place2.CardInThisZoneUnselectable();
							}
						}
					}
				}
				this.RefreshButton();
				this.GetUI<OcgCoreUI>().SetHint(string.Concat(new string[]
				{
					this.fieldHint,
					": ",
					OcgCore.GetSelectLevelSum(selected)[0].ToString(),
					"/",
					OcgCore.ES_level.ToString()
				}));
				return;
			}
			if (OcgCore.currentMessage == GameMessage.SelectCounter)
			{
				this.fieldCounterCount++;
				card.counterSelected++;
				this.GetUI<OcgCoreUI>().SetHint(string.Concat(new string[]
				{
					this.fieldHint,
					": ",
					this.fieldCounterCount.ToString(),
					"/",
					this.fieldMax.ToString()
				}));
				if (this.fieldCounterCount == OcgCore.ES_min)
				{
					this.FieldSelectedSend();
					return;
				}
				using (List<PlaceSelector>.Enumerator enumerator = this.places.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PlaceSelector place3 = enumerator.Current;
						if (place3.cardSelecting)
						{
							if (place3.cookieCard.counterCanCount > place3.cookieCard.counterSelected)
							{
								place3.CardInThisZoneSelectable();
							}
							else
							{
								place3.CardInThisZoneUnselectable();
							}
						}
					}
					return;
				}
			}
			if (OcgCore.currentMessage == GameMessage.SelectTribute)
			{
				int sum2 = 0;
				foreach (GameCard c in selected)
				{
					sum2 += c.levelForSelect_1;
				}
				if (selected.Count >= this.fieldMax)
				{
					this.FieldSelectedSend();
					return;
				}
				if (sum2 >= this.fieldMin)
				{
					this.fieldSendable = true;
					this.RefreshButton();
					return;
				}
			}
			else
			{
				if (selected.Count >= this.fieldMin)
				{
					this.fieldSendable = true;
				}
				else
				{
					this.fieldSendable = false;
				}
				if (selected.Count >= this.fieldMax)
				{
					this.FieldSelectedSend();
				}
				else
				{
					foreach (PlaceSelector place4 in this.places)
					{
						if (place4.cardSelecting && !place4.cardSelected)
						{
							place4.CardInThisZoneSelectable();
						}
					}
					this.RefreshButton();
				}
				if (OcgCore.currentMessage == GameMessage.SelectCard)
				{
					this.GetUI<OcgCoreUI>().SetHint(string.Concat(new string[]
					{
						this.fieldHint,
						": ",
						selected.Count.ToString(),
						"/",
						this.fieldMax.ToString()
					}));
				}
			}
		}

		// Token: 0x06008D96 RID: 36246 RVA: 0x0012CE80 File Offset: 0x0012B080
		private void RefreshButton()
		{
			if (this.fieldSendable)
			{
				OcgCore.btnConfirm.Show();
				if (OcgCore.currentMessage == GameMessage.SelectUnselect)
				{
					OcgCore.btnCancel.Hide();
				}
			}
			else
			{
				OcgCore.btnConfirm.Hide();
			}
			if (this.fieldExitable)
			{
				if (OcgCore.currentMessage != GameMessage.SelectUnselect || !this.fieldSendable)
				{
					OcgCore.btnCancel.Show();
					return;
				}
			}
			else
			{
				OcgCore.btnCancel.Hide();
			}
		}

		// Token: 0x06008D97 RID: 36247 RVA: 0x0012CEEC File Offset: 0x0012B0EC
		public void FieldSelectedSend()
		{
			List<GameCard> selected = new List<GameCard>();
			foreach (PlaceSelector place in this.places)
			{
				if (place.cardSelecting && place.cardSelected)
				{
					selected.Add(place.cookieCard);
				}
			}
			BinaryMaster binaryMaster = new BinaryMaster(null);
			if (OcgCore.currentMessage == GameMessage.SelectUnselect && selected.Count == 0)
			{
				binaryMaster.writer.Write(-1);
			}
			else if (OcgCore.currentMessage == GameMessage.SelectCounter)
			{
				for (int i = 0; i < OcgCore.cardsInSelection.Count; i++)
				{
					binaryMaster.writer.Write((short)OcgCore.cardsInSelection[i].counterSelected);
				}
			}
			else
			{
				if (OcgCore.currentMessage == GameMessage.SelectSum)
				{
					binaryMaster.writer.Write((byte)selected.Count);
					foreach (GameCard card in OcgCore.cardsMustBeSelected)
					{
						binaryMaster.writer.Write((byte)card.selectPtr);
					}
					using (List<GameCard>.Enumerator enumerator2 = selected.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							GameCard card2 = enumerator2.Current;
							if (!OcgCore.cardsMustBeSelected.Contains(card2))
							{
								binaryMaster.writer.Write((byte)card2.selectPtr);
							}
						}
						goto IL_01BD;
					}
				}
				binaryMaster.writer.Write((byte)selected.Count);
				foreach (GameCard card3 in selected)
				{
					binaryMaster.writer.Write((byte)card3.selectPtr);
				}
			}
			IL_01BD:
			this.SendReturn(binaryMaster.Get(), 0f);
		}

		// Token: 0x06008D98 RID: 36248 RVA: 0x0012D0FC File Offset: 0x0012B2FC
		public void FieldSelectedCancel()
		{
			if (OcgCore.currentMessage == GameMessage.SelectCounter)
			{
				foreach (GameCard gameCard in OcgCore.cardsInSelection)
				{
					gameCard.counterSelected = 0;
				}
				this.fieldCounterCount = 0;
				this.GetUI<OcgCoreUI>().SetHint(string.Concat(new string[]
				{
					this.fieldHint,
					": ",
					0.ToString(),
					"/",
					this.fieldMax.ToString()
				}));
				using (List<PlaceSelector>.Enumerator enumerator2 = this.places.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						PlaceSelector place = enumerator2.Current;
						if (place.cardSelecting)
						{
							place.CardInThisZoneSelectable();
						}
					}
					return;
				}
			}
			BinaryMaster binaryMaster = new BinaryMaster(null);
			binaryMaster.writer.Write(-1);
			this.SendReturn(binaryMaster.Get(), 0f);
		}

		// Token: 0x06008D99 RID: 36249 RVA: 0x0012D218 File Offset: 0x0012B418
		public void SetExDeckTop(GameCard card)
		{
			if (this.DuelBGManager == null)
			{
				return;
			}
			this.DuelBGManager.SetExDeckTop(card);
		}

		// Token: 0x06008D9A RID: 36250 RVA: 0x0012D22F File Offset: 0x0012B42F
		public void UpdateExDeckTop(uint controller)
		{
			if (this.DuelBGManager == null)
			{
				return;
			}
			this.DuelBGManager.UpdateExDeckTop(controller);
		}

		// Token: 0x06008D9B RID: 36251 RVA: 0x0012D246 File Offset: 0x0012B446
		public void SetBgTimeScale(float timeScale)
		{
			if (this.DuelBGManager == null)
			{
				return;
			}
			this.DuelBGManager.SetBgTimeScale(timeScale);
		}

		// Token: 0x06008D9C RID: 36252 RVA: 0x0012D25D File Offset: 0x0012B45D
		public void PlayGraveEffect(GPS p, bool isIn)
		{
			if (this.DuelBGManager == null)
			{
				return;
			}
			this.DuelBGManager.PlayGraveEffect(p, isIn);
		}

		// Token: 0x06008D9D RID: 36253 RVA: 0x0012D278 File Offset: 0x0012B478
		public int GetAllAtk(bool mySide)
		{
			int allAttack = 0;
			foreach (GameCard card in this.GCS_GetLocationCards(mySide ? 0 : 1, 4))
			{
				if (((long)card.p.position & 1L) > 0L)
				{
					allAttack += card.GetData().Attack;
				}
			}
			return allAttack;
		}

		// Token: 0x06008D9E RID: 36254 RVA: 0x0012D2F0 File Offset: 0x0012B4F0
		private bool PlayerLosing()
		{
			if (OcgCore.myTurn)
			{
				if (this.GetAllAtk(true) - this.GetAllAtk(false) > OcgCore.life1)
				{
					int defenseCount = 0;
					using (List<GameCard>.Enumerator enumerator = this.GCS_GetLocationCards(1, 4).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (((long)enumerator.Current.p.position & 12L) > 0L)
							{
								defenseCount++;
							}
						}
					}
					if (defenseCount == 0)
					{
						return true;
					}
				}
			}
			else if (!OcgCore.myTurn && this.GetAllAtk(false) - this.GetAllAtk(true) > OcgCore.life0)
			{
				int defenseCount2 = 0;
				using (List<GameCard>.Enumerator enumerator = this.GCS_GetLocationCards(0, 4).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((long)enumerator.Current.p.position & 12L) > 0L)
						{
							defenseCount2++;
						}
					}
				}
				if (defenseCount2 == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008D9F RID: 36255 RVA: 0x0012D3F4 File Offset: 0x0012B5F4
		public void ShowEquipLine(Vector3 start, Vector3 end)
		{
			if (this.DuelBGManager == null)
			{
				return;
			}
			this.DuelBGManager.ShowEquipLine(start, end);
		}

		// Token: 0x06008DA0 RID: 36256 RVA: 0x0012D40C File Offset: 0x0012B60C
		public void ShowTargetLines(Vector3 start, List<GameCard> targets)
		{
			if (this.DuelBGManager == null)
			{
				return;
			}
			this.DuelBGManager.ShowTargetLines(start, targets);
		}

		// Token: 0x06008DA1 RID: 36257 RVA: 0x0012D424 File Offset: 0x0012B624
		public Transform GetFieldTransform(uint player)
		{
			if (player != 0U)
			{
				return this.DuelBGManager.field1Manager.transform;
			}
			return this.DuelBGManager.field0Manager.transform;
		}

		// Token: 0x06008DA2 RID: 36258 RVA: 0x0012D44A File Offset: 0x0012B64A
		public ElementObjectManager GetDeckModel(uint player, CardLocation location)
		{
			if (location == CardLocation.Deck)
			{
				if (player != 0U)
				{
					return this.DuelBGManager.opDeck;
				}
				return this.DuelBGManager.myDeck;
			}
			else
			{
				if (player != 0U)
				{
					return this.DuelBGManager.opExtra;
				}
				return this.DuelBGManager.myExtra;
			}
		}

		// Token: 0x06008DA3 RID: 36259 RVA: 0x0012D485 File Offset: 0x0012B685
		public void SetDeckModelActive(ElementObjectManager deck, bool active)
		{
			deck.GetElement("CardShuffleTop").SetActive(active);
		}

		// Token: 0x06008DA4 RID: 36260 RVA: 0x0012D498 File Offset: 0x0012B698
		public bool NeedVoice()
		{
			return Config.GetBool(OcgCore.condition.ToString() + "Voice", false);
		}

		// Token: 0x06008DA5 RID: 36261 RVA: 0x0012D4BA File Offset: 0x0012B6BA
		public void SetCharacterFace(string chara, int id, bool isMe, float delay = 0f)
		{
			base.StartCoroutine(this.SetCharacterFaceAsync(chara, id, isMe, delay));
		}

		// Token: 0x06008DA6 RID: 36262 RVA: 0x0012D4CE File Offset: 0x0012B6CE
		private IEnumerator SetCharacterFaceAsync(string chara, int id, bool isMe, float delay = 0f)
		{
			this.charaFaceSetting = true;
			yield return new WaitForSeconds(delay);
			if (id == 0)
			{
				id = 1;
			}
			string address = "sn" + chara + "_3_" + id.ToString();
			Sprite sprite;
			if (!this.cachedCharaFaces.TryGetValue(address, out sprite))
			{
				AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>("sn" + chara + "_3_" + id.ToString());
				yield return handle;
				if (handle.Status != AsyncOperationStatus.Succeeded)
				{
					yield break;
				}
				if (handle.Result.texture.width != handle.Result.texture.height)
				{
					Texture2D croppingTex = TextureManager.GetCroppingTex(handle.Result.texture, 80, 0, 320, 240);
					TextureManager.ReplaceTransparentPixelsWithColor(croppingTex, Color.black);
					sprite = TextureManager.Texture2Sprite(croppingTex);
				}
				else
				{
					Texture2D texture2D = TextureManager.CreateCenteredTexture(handle.Result.texture, 280, 0, 10);
					TextureManager.ReplaceTransparentPixelsWithColor(texture2D, Color.black);
					sprite = TextureManager.Texture2Sprite(texture2D);
				}
				this.cachedCharaFaces[address] = sprite;
				handle = default(AsyncOperationHandle<Sprite>);
			}
			if (isMe)
			{
				this.GetUI<OcgCoreUI>().AvatarPlayer0.sprite = sprite;
			}
			else
			{
				this.GetUI<OcgCoreUI>().AvatarPlayer1.sprite = sprite;
			}
			yield break;
		}

		// Token: 0x06008DA7 RID: 36263 RVA: 0x0012D4FC File Offset: 0x0012B6FC
		public void SetCharacterDefaultFace()
		{
			string hero = Config.Get(OcgCore.condition.ToString() + "Character0", "0001");
			string rival = Config.Get(OcgCore.condition.ToString() + "Character1", "0001");
			base.StartCoroutine(this.SetCharacterFaceAsync(hero, 1, true, 0f));
			base.StartCoroutine(this.SetCharacterFaceAsync(rival, 1, false, 0f));
		}

		// Token: 0x0400CB3F RID: 52031
		public static List<GameCard> materialCards = new List<GameCard>();

		// Token: 0x0400CB40 RID: 52032
		public static bool inPuzzle;

		// Token: 0x0400CB41 RID: 52033
		public static bool isTag;

		// Token: 0x0400CB42 RID: 52034
		public static int playerType;

		// Token: 0x0400CB43 RID: 52035
		public static bool isFirst;

		// Token: 0x0400CB44 RID: 52036
		public static bool isObserver;

		// Token: 0x0400CB45 RID: 52037
		public static int MasterRule;

		// Token: 0x0400CB46 RID: 52038
		public static int life0;

		// Token: 0x0400CB47 RID: 52039
		public static int life1;

		// Token: 0x0400CB48 RID: 52040
		public static int lpLimit = 8000;

		// Token: 0x0400CB49 RID: 52041
		public static int timeLimit = 180;

		// Token: 0x0400CB4A RID: 52042
		public static int turns;

		// Token: 0x0400CB4B RID: 52043
		public static bool myTurn = true;

		// Token: 0x0400CB4C RID: 52044
		public static string name_0 = string.Empty;

		// Token: 0x0400CB4D RID: 52045
		public static string name_0_c = string.Empty;

		// Token: 0x0400CB4E RID: 52046
		public static string name_0_tag = string.Empty;

		// Token: 0x0400CB4F RID: 52047
		public static string name_1 = string.Empty;

		// Token: 0x0400CB50 RID: 52048
		public static string name_1_c = string.Empty;

		// Token: 0x0400CB51 RID: 52049
		public static string name_1_tag = string.Empty;

		// Token: 0x0400CB52 RID: 52050
		public static int cookie_matchKill;

		// Token: 0x0400CB53 RID: 52051
		public static string winReason = string.Empty;

		// Token: 0x0400CB54 RID: 52052
		public static bool duelEnded;

		// Token: 0x0400CB55 RID: 52053
		public static DuelPhase duelPhase = DuelPhase.Draw;

		// Token: 0x0400CB56 RID: 52054
		public static OcgCore.DuelResult duelResult = OcgCore.DuelResult.DisLink;

		// Token: 0x0400CB57 RID: 52055
		public static OcgCore.Condition condition = OcgCore.Condition.N;

		// Token: 0x0400CB58 RID: 52056
		public static OcgCore.ChainCondition chainCondition = OcgCore.ChainCondition.Smart;

		// Token: 0x0400CB59 RID: 52057
		public static string ES_hint;

		// Token: 0x0400CB5A RID: 52058
		public static int ES_max;

		// Token: 0x0400CB5B RID: 52059
		public static int ES_min;

		// Token: 0x0400CB5C RID: 52060
		public static int ES_level;

		// Token: 0x0400CB5D RID: 52061
		public static bool ES_overFlow;

		// Token: 0x0400CB5E RID: 52062
		public static string ES_selectHint;

		// Token: 0x0400CB5F RID: 52063
		public static int Es_selectMSGHintData;

		// Token: 0x0400CB60 RID: 52064
		public static int Es_selectMSGHintPlayer;

		// Token: 0x0400CB61 RID: 52065
		public static int Es_selectMSGHintType;

		// Token: 0x0400CB62 RID: 52066
		public static List<int> ES_searchCodes = new List<int>();

		// Token: 0x0400CB63 RID: 52067
		public static string ES_selectUnselectHint = string.Empty;

		// Token: 0x0400CB64 RID: 52068
		public static bool ES_selectCardFromFieldFirstFlag = false;

		// Token: 0x0400CB65 RID: 52069
		public static int ES_sortSum;

		// Token: 0x0400CB66 RID: 52070
		public static string ES_turnString = string.Empty;

		// Token: 0x0400CB67 RID: 52071
		public static bool surrendered;

		// Token: 0x0400CB68 RID: 52072
		public static bool tagSurrendered;

		// Token: 0x0400CB69 RID: 52073
		public static bool deckReserved;

		// Token: 0x0400CB6A RID: 52074
		public static bool cantCheckGrave;

		// Token: 0x0400CB6B RID: 52075
		public static bool inPendulumSummon;

		// Token: 0x0400CB6C RID: 52076
		public static List<GameCard> cards = new List<GameCard>();

		// Token: 0x0400CB6D RID: 52077
		public static List<GameCard> tempCards = new List<GameCard>();

		// Token: 0x0400CB6E RID: 52078
		public static int chainSolvingIndex;

		// Token: 0x0400CB6F RID: 52079
		public static GameCard chainSolvingCard;

		// Token: 0x0400CB70 RID: 52080
		public static List<GameCard> cardsInChain = new List<GameCard>();

		// Token: 0x0400CB71 RID: 52081
		public static List<int> codesInChain = new List<int>();

		// Token: 0x0400CB72 RID: 52082
		public static List<uint> controllerInChain = new List<uint>();

		// Token: 0x0400CB73 RID: 52083
		public static List<int> negatedInChain = new List<int>();

		// Token: 0x0400CB74 RID: 52084
		public static List<GameCard> cardsBeTarget = new List<GameCard>();

		// Token: 0x0400CB75 RID: 52085
		public static List<GameCard> cardsInSelection = new List<GameCard>();

		// Token: 0x0400CB76 RID: 52086
		public static List<GameCard> cardsMustBeSelected = new List<GameCard>();

		// Token: 0x0400CB77 RID: 52087
		public static List<string> confirmedCards = new List<string>();

		// Token: 0x0400CB78 RID: 52088
		public static GameCard attackingCard;

		// Token: 0x0400CB79 RID: 52089
		public static GameCard summonCard;

		// Token: 0x0400CB7A RID: 52090
		public static GameCard lastMoveCard;

		// Token: 0x0400CB7B RID: 52091
		public static GameCard lastConfirmedCard;

		// Token: 0x0400CB7C RID: 52092
		public static int mySummonCount;

		// Token: 0x0400CB7D RID: 52093
		public static int mySpSummonCount;

		// Token: 0x0400CB7E RID: 52094
		public static int opSummonCount;

		// Token: 0x0400CB7F RID: 52095
		public static int opSpSummonCount;

		// Token: 0x0400CB80 RID: 52096
		public static List<int> myActivated = new List<int>();

		// Token: 0x0400CB81 RID: 52097
		public static List<int> opActivated = new List<int>();

		// Token: 0x0400CB82 RID: 52098
		public static OcgCore.ResponseHandler handler = null;

		// Token: 0x0400CB83 RID: 52099
		public static GameMessage currentMessage = GameMessage.Waiting;

		// Token: 0x0400CB84 RID: 52100
		public static GameMessage lastMessage = GameMessage.Waiting;

		// Token: 0x0400CB85 RID: 52101
		public static List<Package> packages = new List<Package>();

		// Token: 0x0400CB86 RID: 52102
		public static List<Package> allPackages = new List<Package>();

		// Token: 0x0400CB87 RID: 52103
		public static bool pause;

		// Token: 0x0400CB88 RID: 52104
		private static bool noMoreWait;

		// Token: 0x0400CB89 RID: 52105
		public static Action endingAction;

		// Token: 0x0400CB8A RID: 52106
		public static Action<int> nextMoveAction;

		// Token: 0x0400CB8B RID: 52107
		public static float nextMoveActionDuration;

		// Token: 0x0400CB8C RID: 52108
		public static ElementObjectManager nextMoveManager;

		// Token: 0x0400CB8D RID: 52109
		public static Action nextEventAction;

		// Token: 0x0400CB8E RID: 52110
		public static Action nextNegateAction;

		// Token: 0x0400CB8F RID: 52111
		public static Action nextNegateAction_Additional;

		// Token: 0x0400CB90 RID: 52112
		public static float nextNegateAction_AdditionalTime;

		// Token: 0x0400CB91 RID: 52113
		public static ElementObjectManager nextNegateAction_AdditionalManager;

		// Token: 0x0400CB92 RID: 52114
		public static Action startCard;

		// Token: 0x0400CB93 RID: 52115
		public static Material myProtector;

		// Token: 0x0400CB94 RID: 52116
		public static Material opProtector;

		// Token: 0x0400CB95 RID: 52117
		public static bool needRefreshMyHand = true;

		// Token: 0x0400CB96 RID: 52118
		public static bool needRefreshOpHand = true;

		// Token: 0x0400CB97 RID: 52119
		public static List<GameCard> myHandCards = new List<GameCard>();

		// Token: 0x0400CB98 RID: 52120
		public static List<GameCard> opHandCards = new List<GameCard>();

		// Token: 0x0400CB99 RID: 52121
		public static List<GameCard> myPreHandCards = new List<GameCard>();

		// Token: 0x0400CB9A RID: 52122
		public static List<GameCard> opPreHandCards = new List<GameCard>();

		// Token: 0x0400CB9B RID: 52123
		public static int movingToMyGrave = 0;

		// Token: 0x0400CB9C RID: 52124
		public static int movingToMyExclude = 0;

		// Token: 0x0400CB9D RID: 52125
		public static int movingToOpGrave = 0;

		// Token: 0x0400CB9E RID: 52126
		public static int movingToOpExclude = 0;

		// Token: 0x0400CB9F RID: 52127
		public static uint nextMoveMessageController;

		// Token: 0x0400CBA0 RID: 52128
		public static bool nextMoveNeedCode;

		// Token: 0x0400CBA1 RID: 52129
		public static readonly Vector3 myPosition = new Vector3(0f, 15f, -25f);

		// Token: 0x0400CBA2 RID: 52130
		public static readonly Vector3 opPosition = new Vector3(0f, 15f, 25f);

		// Token: 0x0400CBA3 RID: 52131
		private static readonly float handCellX = 30f;

		// Token: 0x0400CBA4 RID: 52132
		private static float clickInPosition;

		// Token: 0x0400CBA5 RID: 52133
		private static int handCount;

		// Token: 0x0400CBA6 RID: 52134
		public static float handOffset;

		// Token: 0x0400CBA7 RID: 52135
		public static float lastHandOffset;

		// Token: 0x0400CBA8 RID: 52136
		public static bool clickingHandCard;

		// Token: 0x0400CBA9 RID: 52137
		public static bool handCardDraged;

		// Token: 0x0400CBAA RID: 52138
		private static bool hideMyHandCard;

		// Token: 0x0400CBAB RID: 52139
		private static bool hideOpHandCard;

		// Token: 0x0400CBAC RID: 52140
		public static DuelButton btnConfirm;

		// Token: 0x0400CBAD RID: 52141
		public static DuelButton btnCancel;

		// Token: 0x0400CBAE RID: 52142
		[Header("OcgCore")]
		public MeshRenderer greenBackground;

		// Token: 0x0400CBAF RID: 52143
		[HideInInspector]
		public PopupDuel currentPopup;

		// Token: 0x0400CBB0 RID: 52144
		public static bool mycardDuel;

		// Token: 0x0400CBB1 RID: 52145
		public static Deck sideReference = new Deck();

		// Token: 0x0400CBB2 RID: 52146
		public static bool inputMode;

		// Token: 0x0400CBB3 RID: 52147
		public static bool Accing;

		// Token: 0x0400CBB4 RID: 52148
		public static bool CurrentReplayUseYRP2;

		// Token: 0x0400CBB5 RID: 52149
		private bool greenOn;

		// Token: 0x0400CBB6 RID: 52150
		public MessageDispatcher messageDispatcher = new MessageDispatcher();

		// Token: 0x0400CBB7 RID: 52151
		public static string lastDuelLog;

		// Token: 0x0400CBB8 RID: 52152
		private Sprite mySprite;

		// Token: 0x0400CBB9 RID: 52153
		private Sprite opSprite;

		// Token: 0x0400CBBA RID: 52154
		private string fieldHint;

		// Token: 0x0400CBBB RID: 52155
		private int fieldMin;

		// Token: 0x0400CBBC RID: 52156
		private int fieldMax;

		// Token: 0x0400CBBD RID: 52157
		private bool fieldExitable;

		// Token: 0x0400CBBE RID: 52158
		private bool fieldSendable;

		// Token: 0x0400CBBF RID: 52159
		[HideInInspector]
		public int fieldCounterCount;

		// Token: 0x0400CBC0 RID: 52160
		public ChatItemHandler duelChat0;

		// Token: 0x0400CBC1 RID: 52161
		public ChatItemHandler duelChat1;

		// Token: 0x0400CBC2 RID: 52162
		public bool charaFaceSetting;

		// Token: 0x0400CBC3 RID: 52163
		public Dictionary<string, Sprite> cachedCharaFaces = new Dictionary<string, Sprite>();

		// Token: 0x020012E5 RID: 4837
		// (Invoke) Token: 0x06008DAD RID: 36269
		public delegate void ResponseHandler(byte[] buffer);

		// Token: 0x020012E6 RID: 4838
		public enum DuelResult
		{
			// Token: 0x0400CBC5 RID: 52165
			DisLink,
			// Token: 0x0400CBC6 RID: 52166
			Win,
			// Token: 0x0400CBC7 RID: 52167
			Lose,
			// Token: 0x0400CBC8 RID: 52168
			Draw
		}

		// Token: 0x020012E7 RID: 4839
		public enum Condition
		{
			// Token: 0x0400CBCA RID: 52170
			N,
			// Token: 0x0400CBCB RID: 52171
			Duel,
			// Token: 0x0400CBCC RID: 52172
			Watch,
			// Token: 0x0400CBCD RID: 52173
			Replay
		}

		// Token: 0x020012E8 RID: 4840
		public enum ChainCondition
		{
			// Token: 0x0400CBCF RID: 52175
			No,
			// Token: 0x0400CBD0 RID: 52176
			All,
			// Token: 0x0400CBD1 RID: 52177
			Smart
		}
	}
}
