using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;
using YgomGame.Bg;

namespace MDPro3.Duel
{
	// Token: 0x020014BD RID: 5309
	public class GraveBehaviour : MonoBehaviour
	{
		// Token: 0x06009B07 RID: 39687 RVA: 0x0017F490 File Offset: 0x0017D690
		private void Start()
		{
			this.manager = base.GetComponent<BgEffectManager>();
			this.grave = this.manager.GetElement<Transform>("GraveIn").parent.gameObject;
			this.graveCollider = this.grave.AddComponent<BoxCollider>();
			this.graveCollider.center = new Vector3(0f, 1f, 0f);
			this.graveCollider.size = new Vector3(6f, 2f, 6f);
			this.exclude = this.manager.GetElement<Transform>("ExcludeIn").parent.gameObject;
			this.excludeCollider = this.exclude.AddComponent<BoxCollider>();
			this.excludeCollider.center = new Vector3(0f, 1f, 0f);
			this.excludeCollider.size = new Vector3(6f, 2f, 6f);
		}

		// Token: 0x06009B08 RID: 39688 RVA: 0x0017F588 File Offset: 0x0017D788
		private void Update()
		{
			if (UserInput.HoverObject == this.grave)
			{
				this.manager.GetElement<Renderer>("Material01").material.SetFloat("_GraveMouseOver", 1f);
				if (UserInput.MouseLeftPressing)
				{
					this.manager.GetElement<Renderer>("Material01").material.SetFloat("_GravePressButton", 1f);
				}
				else
				{
					this.manager.GetElement<Renderer>("Material01").material.SetFloat("_GravePressButton", 0f);
				}
				if (UserInput.MouseLeftUp)
				{
					this.GraveOnClick();
				}
			}
			else
			{
				this.manager.GetElement<Renderer>("Material01").material.SetFloat("_GraveMouseOver", 0f);
				this.manager.GetElement<Renderer>("Material01").material.SetFloat("_GravePressButton", 0f);
				if (UserInput.MouseLeftUp)
				{
					this.HideGraveButtons();
				}
				if (this.graveCountShowing)
				{
					this.graveCountShowing = false;
					Program.instance.ocgcore.GetUI<OcgCoreUI>().HidePlaceCount();
				}
			}
			if (UserInput.HoverObject == this.exclude)
			{
				this.manager.GetElement<Renderer>("Material01").material.SetFloat("_ExcludeMouseOver", 1f);
				if (UserInput.MouseLeftPressing)
				{
					this.manager.GetElement<Renderer>("Material01").material.SetFloat("_ExcludePressButton", 1f);
				}
				else
				{
					this.manager.GetElement<Renderer>("Material01").material.SetFloat("_ExcludePressButton", 0f);
				}
				if (UserInput.MouseLeftUp)
				{
					this.ExcludeOnClick();
				}
			}
			else
			{
				this.manager.GetElement<Renderer>("Material01").material.SetFloat("_ExcludeMouseOver", 0f);
				this.manager.GetElement<Renderer>("Material01").material.SetFloat("_ExcludePressButton", 0f);
				if (UserInput.MouseLeftUp)
				{
					this.HideExcludeButtons();
				}
				if (this.excludeCountShowing)
				{
					this.excludeCountShowing = false;
					Program.instance.ocgcore.GetUI<OcgCoreUI>().HidePlaceCount();
				}
			}
			if (UserInput.HoverObject == this.grave && !this.graveCountShowing)
			{
				this.graveCountShowing = true;
				Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowLocationCount(new GPS
				{
					location = 16U,
					controller = (uint)this.controller
				});
			}
			if (UserInput.HoverObject == this.exclude && !this.excludeCountShowing)
			{
				this.excludeCountShowing = true;
				Program.instance.ocgcore.GetUI<OcgCoreUI>().ShowLocationCount(new GPS
				{
					location = 32U,
					controller = (uint)this.controller
				});
			}
		}

		// Token: 0x06009B09 RID: 39689 RVA: 0x0017F85C File Offset: 0x0017DA5C
		private void GraveOnClick()
		{
			AudioManager.PlaySE("SE_DUEL_SELECT", 1f);
			List<GameCard> cards = Program.instance.ocgcore.GCS_GetLocationCards(this.controller, 16);
			Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Show(cards, CardLocation.Grave, this.controller);
			if (Program.instance.ocgcore.returnAction != null)
			{
				return;
			}
			if (!this.graveButtonsCreated)
			{
				bool spsummmon = false;
				bool activate = false;
				foreach (GameCard card in OcgCore.cards)
				{
					if ((card.p.location & 16U) > 0U && (ulong)card.p.controller == (ulong)((long)this.controller))
					{
						foreach (GameCard.DuelButtonInfo duelButtonInfo in card.buttons)
						{
							if (duelButtonInfo.type == ButtonType.Activate)
							{
								activate = true;
							}
							if (duelButtonInfo.type == ButtonType.SpSummon)
							{
								spsummmon = true;
							}
						}
					}
				}
				if (activate)
				{
					int response = -1;
					this.graveButtons.Add(new GameCard.DuelButtonInfo
					{
						response = new List<int> { response },
						hint = InterString.Get("发动效果", 0),
						type = ButtonType.Activate
					});
				}
				if (spsummmon)
				{
					int response2 = -2;
					this.graveButtons.Add(new GameCard.DuelButtonInfo
					{
						response = new List<int> { response2 },
						hint = InterString.Get("特殊召唤", 0),
						type = ButtonType.SpSummon
					});
				}
				this.CreateGraveButtons();
				return;
			}
			this.ShowGraveButtons();
		}

		// Token: 0x06009B0A RID: 39690 RVA: 0x0017FA2C File Offset: 0x0017DC2C
		private void ExcludeOnClick()
		{
			AudioManager.PlaySE("SE_DUEL_SELECT", 1f);
			List<GameCard> cards = Program.instance.ocgcore.GCS_GetLocationCards(this.controller, 32);
			Program.instance.ocgcore.GetUI<OcgCoreUI>().CardList.Show(cards, CardLocation.Removed, this.controller);
			if (Program.instance.ocgcore.returnAction != null)
			{
				return;
			}
			if (!this.excludeButtonsCreated)
			{
				bool spsummmon = false;
				bool activate = false;
				foreach (GameCard card in OcgCore.cards)
				{
					if ((card.p.location & 32U) > 0U && (ulong)card.p.controller == (ulong)((long)this.controller))
					{
						foreach (GameCard.DuelButtonInfo duelButtonInfo in card.buttons)
						{
							if (duelButtonInfo.type == ButtonType.Activate)
							{
								activate = true;
							}
							if (duelButtonInfo.type == ButtonType.SpSummon)
							{
								spsummmon = true;
							}
						}
					}
				}
				if (activate)
				{
					int response = -1;
					this.excludeButtons.Add(new GameCard.DuelButtonInfo
					{
						response = new List<int> { response },
						hint = InterString.Get("发动效果", 0),
						type = ButtonType.Activate
					});
				}
				if (spsummmon)
				{
					int response2 = -2;
					this.excludeButtons.Add(new GameCard.DuelButtonInfo
					{
						response = new List<int> { response2 },
						hint = InterString.Get("特殊召唤", 0),
						type = ButtonType.SpSummon
					});
				}
				this.CreateExcludeButtons();
				return;
			}
			this.ShowExcludeButtons();
		}

		// Token: 0x06009B0B RID: 39691 RVA: 0x0017FBFC File Offset: 0x0017DDFC
		private void ShowGraveButtons()
		{
			foreach (DuelButton duelButton in this.graveButtonObjs)
			{
				duelButton.Show();
			}
		}

		// Token: 0x06009B0C RID: 39692 RVA: 0x0017FC4C File Offset: 0x0017DE4C
		private void ShowExcludeButtons()
		{
			foreach (DuelButton duelButton in this.excludeButtonObjs)
			{
				duelButton.Show();
			}
		}

		// Token: 0x06009B0D RID: 39693 RVA: 0x0017FC9C File Offset: 0x0017DE9C
		private void HideGraveButtons()
		{
			foreach (DuelButton duelButton in this.graveButtonObjs)
			{
				duelButton.Hide();
			}
		}

		// Token: 0x06009B0E RID: 39694 RVA: 0x0017FCEC File Offset: 0x0017DEEC
		private void HideExcludeButtons()
		{
			foreach (DuelButton duelButton in this.excludeButtonObjs)
			{
				duelButton.Hide();
			}
		}

		// Token: 0x06009B0F RID: 39695 RVA: 0x0017FD3C File Offset: 0x0017DF3C
		private void CreateGraveButtons()
		{
			if (this.graveButtonsCreated || Program.instance.ocgcore.returnAction != null || this.graveButtons.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this.graveButtons.Count; i++)
			{
				DuelButton mono = ABLoader.LoadMasterDuelGameObject("DuelButton").GetComponent<DuelButton>();
				this.graveButtonObjs.Add(mono);
				mono.response = this.graveButtons[i].response;
				mono.hint = this.graveButtons[i].hint;
				mono.type = this.graveButtons[i].type;
				mono.id = i;
				mono.buttonsCount = this.graveButtons.Count;
				mono.cookieCard = null;
				mono.location = 16U;
				mono.controller = (uint)this.controller;
				mono.Show();
			}
			this.graveButtonsCreated = true;
		}

		// Token: 0x06009B10 RID: 39696 RVA: 0x0017FE30 File Offset: 0x0017E030
		private void CreateExcludeButtons()
		{
			if (this.excludeButtonsCreated || Program.instance.ocgcore.returnAction != null || this.excludeButtons.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this.excludeButtons.Count; i++)
			{
				DuelButton mono = ABLoader.LoadMasterDuelGameObject("DuelButton").GetComponent<DuelButton>();
				this.excludeButtonObjs.Add(mono);
				mono.response = this.excludeButtons[i].response;
				mono.hint = this.excludeButtons[i].hint;
				mono.type = this.excludeButtons[i].type;
				mono.id = i;
				mono.buttonsCount = this.excludeButtons.Count;
				mono.cookieCard = null;
				mono.location = 32U;
				mono.controller = (uint)this.controller;
				mono.Show();
			}
			this.excludeButtonsCreated = true;
		}

		// Token: 0x06009B11 RID: 39697 RVA: 0x0017FF24 File Offset: 0x0017E124
		public void ClearGraveButtons()
		{
			foreach (DuelButton duelButton in this.graveButtonObjs)
			{
				global::UnityEngine.Object.Destroy(duelButton.gameObject);
			}
			this.graveButtonObjs.Clear();
			this.graveButtons.Clear();
			this.graveButtonsCreated = false;
		}

		// Token: 0x06009B12 RID: 39698 RVA: 0x0017FF98 File Offset: 0x0017E198
		public void ClearExcludeButtons()
		{
			foreach (DuelButton duelButton in this.excludeButtonObjs)
			{
				global::UnityEngine.Object.Destroy(duelButton.gameObject);
			}
			this.excludeButtonObjs.Clear();
			this.excludeButtons.Clear();
			this.excludeButtonsCreated = false;
		}

		// Token: 0x0400D900 RID: 55552
		public int controller;

		// Token: 0x0400D901 RID: 55553
		private BgEffectManager manager;

		// Token: 0x0400D902 RID: 55554
		private BoxCollider graveCollider;

		// Token: 0x0400D903 RID: 55555
		private BoxCollider excludeCollider;

		// Token: 0x0400D904 RID: 55556
		private GameObject grave;

		// Token: 0x0400D905 RID: 55557
		private GameObject exclude;

		// Token: 0x0400D906 RID: 55558
		private bool graveCountShowing;

		// Token: 0x0400D907 RID: 55559
		private bool excludeCountShowing;

		// Token: 0x0400D908 RID: 55560
		private bool graveButtonsCreated;

		// Token: 0x0400D909 RID: 55561
		private bool excludeButtonsCreated;

		// Token: 0x0400D90A RID: 55562
		public List<GameCard.DuelButtonInfo> graveButtons = new List<GameCard.DuelButtonInfo>();

		// Token: 0x0400D90B RID: 55563
		public List<DuelButton> graveButtonObjs = new List<DuelButton>();

		// Token: 0x0400D90C RID: 55564
		public List<GameCard.DuelButtonInfo> excludeButtons = new List<GameCard.DuelButtonInfo>();

		// Token: 0x0400D90D RID: 55565
		public List<DuelButton> excludeButtonObjs = new List<DuelButton>();
	}
}
