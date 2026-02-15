using System;
using System.Collections.Generic;
using DG.Tweening;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using YgomSystem.ElementSystem;

namespace MDPro3.UI
{
	// Token: 0x020013ED RID: 5101
	public class PhaseButtonHandler : MonoBehaviour
	{
		// Token: 0x060093D6 RID: 37846 RVA: 0x0014F260 File Offset: 0x0014D460
		private void Start()
		{
			PhaseButtonHandler.manager = base.GetComponent<ElementObjectManager>();
			PhaseButtonHandler.commonPart = PhaseButtonHandler.manager.GetElement<Transform>("Button");
			PhaseButtonHandler.textMain = PhaseButtonHandler.manager.GetElement<TextMeshPro>("Text");
			PhaseButtonHandler.textMain.font = Program.instance.ui_.jpMenuTmpFont;
			PhaseButtonHandler.textMain.text = "Main1";
			PhaseButtonHandler.textAbove = PhaseButtonHandler.manager.GetElement<TextMeshPro>("Text03");
			PhaseButtonHandler.textAbove.font = Program.instance.ui_.jpMenuTmpFont;
			PhaseButtonHandler.textAbove.text = "Turn 1";
			PhaseButtonHandler.textBelow = PhaseButtonHandler.manager.GetElement<TextMeshPro>("Text02");
			PhaseButtonHandler.textBelow.font = Program.instance.ui_.jpMenuTmpFont;
			PhaseButtonHandler.textBelow.fontSize = 10f;
			PhaseButtonHandler.textBelow.text = "";
			PhaseButtonHandler.playerPart = PhaseButtonHandler.manager.GetElement<Transform>("PlayerBase");
			PhaseButtonHandler.opponentPart = PhaseButtonHandler.manager.GetElement<Transform>("OpponentBase");
			this.playerMaterial = PhaseButtonHandler.playerPart.GetComponent<Renderer>().material;
			this.opponentMaterial = PhaseButtonHandler.opponentPart.GetComponent<Renderer>().material;
			this.opponentMaterial.SetFloat("_SwitchTurn", 1f);
			this.collider_ = PhaseButtonHandler.commonPart.GetComponent<Collider>();
		}

		// Token: 0x060093D7 RID: 37847 RVA: 0x0014F3C8 File Offset: 0x0014D5C8
		private void Update()
		{
			if (PhaseButtonHandler.battlePhase || PhaseButtonHandler.main2Phase || PhaseButtonHandler.endPhase)
			{
				this.playerMaterial.SetFloat("_Active", 1f);
				if (!EventSystem.current.IsPointerOverGameObject() && UserInput.HoverObject == this.collider_.gameObject && UserInput.MouseLeftUp && Program.instance.ocgcore.currentPopup == null)
				{
					List<string> tasks = new List<string> { OcgCore.duelPhase.ToString() };
					if (PhaseButtonHandler.battlePhase)
					{
						tasks.Add(DuelPhase.BattleStart.ToString());
					}
					if (PhaseButtonHandler.main2Phase)
					{
						tasks.Add(DuelPhase.Main2.ToString());
					}
					if (PhaseButtonHandler.endPhase)
					{
						tasks.Add(DuelPhase.End.ToString());
					}
					Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowPopupPhase(tasks);
					Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Hide();
				}
				if (!EventSystem.current.IsPointerOverGameObject() && UserInput.HoverObject == this.collider_.gameObject && UserInput.MouseLeftPressing)
				{
					this.playerMaterial.SetFloat("_PressButton", 1f);
				}
				else
				{
					this.playerMaterial.SetFloat("_PressButton", 0f);
				}
				if (!EventSystem.current.IsPointerOverGameObject() && UserInput.HoverObject == this.collider_.gameObject && !this.hover)
				{
					this.hover = true;
					DOTween.To(() => this.mouseOver, delegate(float x)
					{
						this.mouseOver = x;
					}, 1f, 0.2f);
				}
				else if (!EventSystem.current.IsPointerOverGameObject() && UserInput.HoverObject != this.collider_ && this.hover)
				{
					this.hover = false;
					DOTween.To(() => this.mouseOver, delegate(float x)
					{
						this.mouseOver = x;
					}, 0f, 0.2f);
				}
				this.playerMaterial.SetFloat("_MouseOver", this.mouseOver);
				return;
			}
			this.playerMaterial.SetFloat("_Active", 0f);
		}

		// Token: 0x060093D8 RID: 37848 RVA: 0x0014F628 File Offset: 0x0014D828
		public static void TurnChange(bool me, int turns)
		{
			PhaseButtonHandler.SetTextAbove("Turn " + turns.ToString());
			PhaseButtonHandler.commonPart.localScale = Vector3.zero;
			PhaseButtonHandler.commonPart.DOScale(Vector3.one, 0.3f);
			if (me)
			{
				PhaseButtonHandler.playerPart.DOScale(Vector3.one, 0.3f);
				PhaseButtonHandler.opponentPart.DOScale(Vector3.one * 0.1f, 0.3f);
				return;
			}
			PhaseButtonHandler.playerPart.DOScale(Vector3.one * 0.1f, 0.3f);
			PhaseButtonHandler.opponentPart.DOScale(Vector3.one, 0.3f);
		}

		// Token: 0x060093D9 RID: 37849 RVA: 0x0014F6DB File Offset: 0x0014D8DB
		public static void SetTextMain(string text)
		{
			PhaseButtonHandler.textMain.text = text;
		}

		// Token: 0x060093DA RID: 37850 RVA: 0x0014F6E8 File Offset: 0x0014D8E8
		public static void SetTextAbove(string text)
		{
			PhaseButtonHandler.textAbove.text = text;
		}

		// Token: 0x060093DB RID: 37851 RVA: 0x0014F6F5 File Offset: 0x0014D8F5
		public static void SetTextBelow(string text)
		{
			PhaseButtonHandler.textBelow.text = text;
		}

		// Token: 0x060093DC RID: 37852 RVA: 0x0014F702 File Offset: 0x0014D902
		public static void SetHint()
		{
			PhaseButtonHandler.manager.GetElement("HintEffect").SetActive(true);
		}

		// Token: 0x060093DD RID: 37853 RVA: 0x0014F719 File Offset: 0x0014D919
		public static void CloseHint()
		{
			PhaseButtonHandler.manager.GetElement("HintEffect").SetActive(false);
		}

		// Token: 0x0400D232 RID: 53810
		private static Transform commonPart;

		// Token: 0x0400D233 RID: 53811
		private static Transform playerPart;

		// Token: 0x0400D234 RID: 53812
		private static Transform opponentPart;

		// Token: 0x0400D235 RID: 53813
		private static TextMeshPro textMain;

		// Token: 0x0400D236 RID: 53814
		private static TextMeshPro textBelow;

		// Token: 0x0400D237 RID: 53815
		private static TextMeshPro textAbove;

		// Token: 0x0400D238 RID: 53816
		private Material playerMaterial;

		// Token: 0x0400D239 RID: 53817
		private Material opponentMaterial;

		// Token: 0x0400D23A RID: 53818
		private Collider collider_;

		// Token: 0x0400D23B RID: 53819
		private bool hover;

		// Token: 0x0400D23C RID: 53820
		private float mouseOver;

		// Token: 0x0400D23D RID: 53821
		private int turns = -1;

		// Token: 0x0400D23E RID: 53822
		public static bool battlePhase;

		// Token: 0x0400D23F RID: 53823
		public static bool main2Phase;

		// Token: 0x0400D240 RID: 53824
		public static bool endPhase;

		// Token: 0x0400D241 RID: 53825
		private static ElementObjectManager manager;
	}
}
