using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Colosseum
{
	// Token: 0x02001089 RID: 4233
	public class ColosseumViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06007E59 RID: 32345 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007E5A RID: 32346 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06007E5B RID: 32347 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007E5C RID: 32348 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06007E5D RID: 32349 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007E5E RID: 32350 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateWcsBG(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIDuelMenuInfo(Action onSuccess = null)
		{
		}

		// Token: 0x06007E61 RID: 32353 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMenu()
		{
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x0000216D File Offset: 0x0000036D
		internal void OnItemSetData(GameObject go, int index)
		{
		}

		// Token: 0x0400B729 RID: 46889
		private readonly string BTN_LABEL;

		// Token: 0x0400B72A RID: 46890
		private readonly string ROOT_EMPTY_EVENTS_LABEL;

		// Token: 0x0400B72B RID: 46891
		private readonly string IMG_EMPTY_EVENTS_LABEL;

		// Token: 0x0400B72C RID: 46892
		private readonly string TXT_NAME_LABEL;

		// Token: 0x0400B72D RID: 46893
		private readonly string SCROLL_LABEL;

		// Token: 0x0400B72E RID: 46894
		private InfinityScrollView isv;

		// Token: 0x0400B72F RID: 46895
		private ColosseumViewController.ColosseumMenuManager colosseumManager;

		// Token: 0x0400B730 RID: 46896
		private bool isFirstFade;

		// Token: 0x0400B731 RID: 46897
		private bool isStackWcsBG;

		// Token: 0x0200108A RID: 4234
		internal interface ICustomButtonAction
		{
			// Token: 0x06007E64 RID: 32356
			void OnCustomClick(ViewControllerManager manager, Action onFailed);
		}

		// Token: 0x0200108B RID: 4235
		internal abstract class ColosseumMenuBase
		{
			// Token: 0x06007E65 RID: 32357 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void Init(ElementObjectManager m_View, ViewControllerManager manager)
			{
			}

			// Token: 0x06007E66 RID: 32358
			internal abstract void Update();

			// Token: 0x06007E67 RID: 32359
			internal abstract ColosseumUtil.PlayMode GetPlayMode();

			// Token: 0x0400B732 RID: 46898
			protected readonly string BTN_LABEL;

			// Token: 0x0400B733 RID: 46899
			protected readonly string ROOT_RANK_LABEL;

			// Token: 0x0400B734 RID: 46900
			protected readonly string ROOT_FREE_LABEL;

			// Token: 0x0400B735 RID: 46901
			protected readonly string IMG_LABEL;

			// Token: 0x0400B736 RID: 46902
			protected readonly string TXT_TIME_LIMIT_LABEL;

			// Token: 0x0400B737 RID: 46903
			protected readonly string TXT_LABEL;

			// Token: 0x0400B738 RID: 46904
			protected readonly string TXT_NAME_LABEL;

			// Token: 0x0400B739 RID: 46905
			protected readonly string IMG_BG_LABEL;
		}

		// Token: 0x0200108C RID: 4236
		protected internal class ButtonInfo
		{
			// Token: 0x06007E69 RID: 32361 RVA: 0x00002739 File Offset: 0x00000939
			internal ButtonInfo(string name, ColosseumUtil.PlayMode playMode, int identifier = 0)
			{
			}

			// Token: 0x0400B73A RID: 46906
			internal string name;

			// Token: 0x0400B73B RID: 46907
			internal readonly int identifier;

			// Token: 0x0400B73C RID: 46908
			internal readonly ColosseumUtil.PlayMode playMode;
		}

		// Token: 0x0200108D RID: 4237
		internal abstract class ButtonEvent : ColosseumViewController.ButtonInfo
		{
			// Token: 0x17000FF2 RID: 4082
			// (get) Token: 0x06007E6A RID: 32362 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007E6B RID: 32363 RVA: 0x0000216D File Offset: 0x0000036D
			public string VcPrefabPath
			{
				get
				{
					return null;
				}
				protected set
				{
				}
			}

			// Token: 0x06007E6C RID: 32364 RVA: 0x000F6839 File Offset: 0x000F4A39
			internal ButtonEvent(string name, ColosseumUtil.PlayMode playMode, int identifier = 0, int sort = 0)
				: base(null, ColosseumUtil.PlayMode.NONE, 0)
			{
			}

			// Token: 0x06007E6D RID: 32365
			internal abstract void SetStatus(ElementObjectManager eom);

			// Token: 0x06007E6E RID: 32366
			internal abstract void SetLogo(ElementObjectManager eom);

			// Token: 0x06007E6F RID: 32367
			internal abstract void SetStatusText(ElementObjectManager eom);

			// Token: 0x06007E70 RID: 32368 RVA: 0x0000216A File Offset: 0x0000036A
			internal virtual Dictionary<string, object> GetAdditionalArgs()
			{
				return null;
			}

			// Token: 0x06007E71 RID: 32369 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string GetEventCategoryPath()
			{
				return null;
			}

			// Token: 0x0400B73D RID: 46909
			protected readonly string TXT_BEFORE_LABEL;

			// Token: 0x0400B73E RID: 46910
			protected readonly string TXT_AFTER_LABEL;

			// Token: 0x0400B73F RID: 46911
			protected readonly string TXT_OPEN_LABEL;

			// Token: 0x0400B740 RID: 46912
			protected readonly string TXT_TIME_LABEL;

			// Token: 0x0400B741 RID: 46913
			protected readonly string IMG_BEFORE_LABEL;

			// Token: 0x0400B742 RID: 46914
			protected readonly string IMG_AFTER_LABEL;

			// Token: 0x0400B743 RID: 46915
			protected readonly string IMG_OPEN_LABEL;

			// Token: 0x0400B744 RID: 46916
			protected readonly string IMG_LOGO_LABEL;

			// Token: 0x0400B745 RID: 46917
			protected readonly string IMG_LOGO_BG_LABEL;

			// Token: 0x0400B746 RID: 46918
			protected readonly string IMG_ATTENTION_LABEL;

			// Token: 0x0400B747 RID: 46919
			protected readonly string IMG_TIME_LABEL;

			// Token: 0x0400B748 RID: 46920
			protected readonly string IMG_EVENT_CATEGORY;

			// Token: 0x0400B749 RID: 46921
			public ColosseumViewController.ButtonEvent.TemplateType type;

			// Token: 0x0400B74A RID: 46922
			private string vcPrefabPath;

			// Token: 0x0400B74B RID: 46923
			internal string endDate;

			// Token: 0x0400B74C RID: 46924
			internal string startDate;

			// Token: 0x0400B74D RID: 46925
			internal int sort;

			// Token: 0x0200108E RID: 4238
			public enum TemplateType
			{
				// Token: 0x0400B74F RID: 46927
				NORMAL,
				// Token: 0x0400B750 RID: 46928
				REGULATION,
				// Token: 0x0400B751 RID: 46929
				DUELTRIAL
			}
		}

		// Token: 0x0200108F RID: 4239
		internal class ColosseumMenuManager
		{
			// Token: 0x06007E72 RID: 32370 RVA: 0x00002739 File Offset: 0x00000939
			internal ColosseumMenuManager()
			{
			}

			// Token: 0x06007E73 RID: 32371 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Init(ElementObjectManager m_View, ViewControllerManager manager)
			{
			}

			// Token: 0x06007E74 RID: 32372 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Update()
			{
			}

			// Token: 0x0400B752 RID: 46930
			private List<ColosseumViewController.ColosseumMenuBase> menuBases;

			// Token: 0x0400B753 RID: 46931
			internal List<ColosseumViewController.ButtonEvent> eventList;
		}

		// Token: 0x02001090 RID: 4240
		internal abstract class ColosseumMenuEventDuel : ColosseumViewController.ColosseumMenuBase
		{
			// Token: 0x0400B754 RID: 46932
			internal List<ColosseumViewController.ButtonEvent> eventList;
		}

		// Token: 0x02001091 RID: 4241
		internal class ColosseumMenuExhibition : ColosseumViewController.ColosseumMenuEventDuel
		{
			// Token: 0x06007E76 RID: 32374 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007E77 RID: 32375 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x02001092 RID: 4242
			internal class ButtonExhibition : ColosseumViewController.ButtonEvent
			{
				// Token: 0x06007E79 RID: 32377 RVA: 0x000F6854 File Offset: 0x000F4A54
				internal ButtonExhibition(string name, ColosseumUtil.PlayMode playMode, int identifier = 0, int sort = 0)
					: base(null, ColosseumUtil.PlayMode.NONE, 0, 0)
				{
				}

				// Token: 0x06007E7A RID: 32378 RVA: 0x0000216D File Offset: 0x0000036D
				internal void SetInfo(ColosseumUtil.StatusExhibition status, int logoId, string startDate, string endDate, bool isReward)
				{
				}

				// Token: 0x06007E7B RID: 32379 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatus(ElementObjectManager eom)
				{
				}

				// Token: 0x06007E7C RID: 32380 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetLogo(ElementObjectManager eom)
				{
				}

				// Token: 0x06007E7D RID: 32381 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatusText(ElementObjectManager eom)
				{
				}

				// Token: 0x0400B755 RID: 46933
				internal ColosseumUtil.StatusExhibition status;

				// Token: 0x0400B756 RID: 46934
				internal int logoId;

				// Token: 0x0400B757 RID: 46935
				internal bool isReward;
			}
		}

		// Token: 0x02001093 RID: 4243
		internal class ColosseumMenuTournament : ColosseumViewController.ColosseumMenuEventDuel
		{
			// Token: 0x06007E7E RID: 32382 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007E7F RID: 32383 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x02001094 RID: 4244
			internal class ButtonTournament : ColosseumViewController.ButtonEvent
			{
				// Token: 0x06007E81 RID: 32385 RVA: 0x000F6854 File Offset: 0x000F4A54
				internal ButtonTournament(string name, ColosseumUtil.PlayMode playMode, int identifier = 0, int sort = 0)
					: base(null, ColosseumUtil.PlayMode.NONE, 0, 0)
				{
				}

				// Token: 0x06007E82 RID: 32386 RVA: 0x0000216D File Offset: 0x0000036D
				internal void SetInfo(ColosseumUtil.StatusTournament status, ColosseumUtil.StatusTournamentHolding statusHolding, int logoId, string startDate, string endDate)
				{
				}

				// Token: 0x06007E83 RID: 32387 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatus(ElementObjectManager eom)
				{
				}

				// Token: 0x06007E84 RID: 32388 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetLogo(ElementObjectManager eom)
				{
				}

				// Token: 0x06007E85 RID: 32389 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatusText(ElementObjectManager eom)
				{
				}

				// Token: 0x0400B758 RID: 46936
				internal ColosseumUtil.StatusTournament status;

				// Token: 0x0400B759 RID: 46937
				internal ColosseumUtil.StatusTournamentHolding statusHolding;

				// Token: 0x0400B75A RID: 46938
				internal int logoId;
			}
		}

		// Token: 0x02001095 RID: 4245
		internal class ColosseumMenuRegulationDuel : ColosseumViewController.ColosseumMenuEventDuel
		{
			// Token: 0x06007E86 RID: 32390 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007E87 RID: 32391 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x02001096 RID: 4246
			internal class ButtonRankEvent : ColosseumViewController.ButtonEvent
			{
				// Token: 0x06007E89 RID: 32393 RVA: 0x000F6854 File Offset: 0x000F4A54
				internal ButtonRankEvent(string name, ColosseumUtil.PlayMode playMode, int identifier = 0, int sort = 0)
					: base(null, ColosseumUtil.PlayMode.NONE, 0, 0)
				{
				}

				// Token: 0x06007E8A RID: 32394 RVA: 0x0000216D File Offset: 0x0000036D
				internal void SetInfo(ColosseumUtil.StatusRankEvent status, int logoId, string startDate, string endDate, bool isReward)
				{
				}

				// Token: 0x06007E8B RID: 32395 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatus(ElementObjectManager eom)
				{
				}

				// Token: 0x06007E8C RID: 32396 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetLogo(ElementObjectManager eom)
				{
				}

				// Token: 0x06007E8D RID: 32397 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatusText(ElementObjectManager eom)
				{
				}

				// Token: 0x0400B75B RID: 46939
				internal ColosseumUtil.StatusRankEvent status;

				// Token: 0x0400B75C RID: 46940
				internal int logoId;

				// Token: 0x0400B75D RID: 46941
				internal bool isReward;
			}
		}

		// Token: 0x02001097 RID: 4247
		internal class ColosseumMenuCup : ColosseumViewController.ColosseumMenuEventDuel
		{
			// Token: 0x06007E8E RID: 32398 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007E8F RID: 32399 RVA: 0x0000216A File Offset: 0x0000036A
			internal virtual string GetIDPath()
			{
				return null;
			}

			// Token: 0x06007E90 RID: 32400 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x06007E91 RID: 32401 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void AddEventList(int cid, string eventName, int logoId, ColosseumUtil.StatusDuelistCup status, int stage, string startDate, string endDate, int sort)
			{
			}

			// Token: 0x02001098 RID: 4248
			internal class ButtonDuelistCup : ColosseumViewController.ButtonEvent
			{
				// Token: 0x06007E93 RID: 32403 RVA: 0x000F6854 File Offset: 0x000F4A54
				internal ButtonDuelistCup(string name, ColosseumUtil.PlayMode playMode, int identifier = 0, int sort = 0)
					: base(null, ColosseumUtil.PlayMode.NONE, 0, 0)
				{
				}

				// Token: 0x06007E94 RID: 32404 RVA: 0x0000216D File Offset: 0x0000036D
				internal void SetInfo(ColosseumUtil.StatusDuelistCup status, int logoId, string startDate, string endDate, int stage)
				{
				}

				// Token: 0x06007E95 RID: 32405 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatus(ElementObjectManager eom)
				{
				}

				// Token: 0x06007E96 RID: 32406 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetLogo(ElementObjectManager eom)
				{
				}

				// Token: 0x06007E97 RID: 32407 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatusText(ElementObjectManager eom)
				{
				}

				// Token: 0x0400B75E RID: 46942
				internal ColosseumUtil.StatusDuelistCup status;

				// Token: 0x0400B75F RID: 46943
				internal int logoId;

				// Token: 0x0400B760 RID: 46944
				internal int stage;
			}
		}

		// Token: 0x02001099 RID: 4249
		internal class ColosseumMenuWCS : ColosseumViewController.ColosseumMenuCup
		{
			// Token: 0x06007E98 RID: 32408 RVA: 0x0000216A File Offset: 0x0000036A
			internal override string GetIDPath()
			{
				return null;
			}

			// Token: 0x06007E99 RID: 32409 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007E9A RID: 32410 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void AddEventList(int cid, string eventName, int logoId, ColosseumUtil.StatusDuelistCup status, int stage, string startDate, string endDate, int sort)
			{
			}

			// Token: 0x0200109A RID: 4250
			internal class ButtonWCS : ColosseumViewController.ColosseumMenuCup.ButtonDuelistCup
			{
				// Token: 0x06007E9C RID: 32412 RVA: 0x000F6868 File Offset: 0x000F4A68
				internal ButtonWCS(string name, ColosseumUtil.PlayMode playMode, int identifier = 0, int sort = 0)
					: base(null, ColosseumUtil.PlayMode.NONE, 0, 0)
				{
				}
			}
		}

		// Token: 0x0200109B RID: 4251
		internal class ColosseumMenuDuelTrial : ColosseumViewController.ColosseumMenuEventDuel
		{
			// Token: 0x06007E9D RID: 32413 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007E9E RID: 32414 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x0200109C RID: 4252
			internal class ButtonDuelTrial : ColosseumViewController.ButtonEvent
			{
				// Token: 0x06007EA0 RID: 32416 RVA: 0x000F6854 File Offset: 0x000F4A54
				internal ButtonDuelTrial(string name, ColosseumUtil.PlayMode playMode, int identifier = 0, int sort = 0)
					: base(null, ColosseumUtil.PlayMode.NONE, 0, 0)
				{
				}

				// Token: 0x06007EA1 RID: 32417 RVA: 0x0000216D File Offset: 0x0000036D
				internal void SetInfo(ColosseumUtil.StatusDuelTrial status, int logoId, string startDate, string endDate, bool isReward)
				{
				}

				// Token: 0x06007EA2 RID: 32418 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatus(ElementObjectManager eom)
				{
				}

				// Token: 0x06007EA3 RID: 32419 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetLogo(ElementObjectManager eom)
				{
				}

				// Token: 0x06007EA4 RID: 32420 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatusText(ElementObjectManager eom)
				{
				}

				// Token: 0x0400B761 RID: 46945
				internal ColosseumUtil.StatusDuelTrial status;

				// Token: 0x0400B762 RID: 46946
				internal int logoId;

				// Token: 0x0400B763 RID: 46947
				internal bool isReward;
			}
		}

		// Token: 0x0200109D RID: 4253
		internal class ColosseumMenuVersus : ColosseumViewController.ColosseumMenuEventDuel
		{
			// Token: 0x06007EA5 RID: 32421 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007EA6 RID: 32422 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x0200109E RID: 4254
			internal class ButtonVersus : ColosseumViewController.ButtonEvent
			{
				// Token: 0x06007EA8 RID: 32424 RVA: 0x000F6854 File Offset: 0x000F4A54
				internal ButtonVersus(string name, ColosseumUtil.PlayMode playMode, int identifier = 0, int sort = 0)
					: base(null, ColosseumUtil.PlayMode.NONE, 0, 0)
				{
				}

				// Token: 0x06007EA9 RID: 32425 RVA: 0x0000216D File Offset: 0x0000036D
				internal void SetInfo(ColosseumUtil.StatusVersus status, int logoId, string startDate, string endDate, bool isReward)
				{
				}

				// Token: 0x06007EAA RID: 32426 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatus(ElementObjectManager eom)
				{
				}

				// Token: 0x06007EAB RID: 32427 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetLogo(ElementObjectManager eom)
				{
				}

				// Token: 0x06007EAC RID: 32428 RVA: 0x0000216D File Offset: 0x0000036D
				internal override void SetStatusText(ElementObjectManager eom)
				{
				}

				// Token: 0x06007EAD RID: 32429 RVA: 0x0000216D File Offset: 0x0000036D
				private void YgomGame_002EColosseum_002EColosseumViewController_002EICustomButtonAction_002EOnCustomClick(ViewControllerManager manager, Action onFailed)
				{
				}

				// Token: 0x0400B764 RID: 46948
				internal ColosseumUtil.StatusVersus status;

				// Token: 0x0400B765 RID: 46949
				internal int logoId;

				// Token: 0x0400B766 RID: 46950
				internal bool isReward;
			}
		}

		// Token: 0x0200109F RID: 4255
		internal abstract class ColosseumMenuFreeDuel : ColosseumViewController.ColosseumMenuBase
		{
			// Token: 0x0400B767 RID: 46951
			protected GameObject selfGo;

			// Token: 0x0400B768 RID: 46952
			protected SelectionButton selfBtn;
		}

		// Token: 0x020010A0 RID: 4256
		internal class ColosseumMenuRoom : ColosseumViewController.ColosseumMenuFreeDuel
		{
			// Token: 0x06007EAF RID: 32431 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007EB0 RID: 32432 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Init(ElementObjectManager m_View, ViewControllerManager manager)
			{
			}

			// Token: 0x06007EB1 RID: 32433 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x0400B769 RID: 46953
			private readonly string E_RoomImageOn;

			// Token: 0x0400B76A RID: 46954
			private readonly string E_RoomImageOff;

			// Token: 0x0400B76B RID: 46955
			private readonly string SCROLL_LABEL;
		}

		// Token: 0x020010A1 RID: 4257
		internal class ColosseumMenuFree : ColosseumViewController.ColosseumMenuFreeDuel
		{
			// Token: 0x06007EB3 RID: 32435 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007EB4 RID: 32436 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Init(ElementObjectManager m_View, ViewControllerManager manager)
			{
			}

			// Token: 0x06007EB5 RID: 32437 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x0400B76C RID: 46956
			private readonly string BTN_FREE_LABEL;

			// Token: 0x0400B76D RID: 46957
			private readonly string OBJ_FREE_STATE_LABEL;

			// Token: 0x0400B76E RID: 46958
			private readonly string SCROLL_LABEL;
		}

		// Token: 0x020010A2 RID: 4258
		internal class ColosseumMenuTeam : ColosseumViewController.ColosseumMenuFreeDuel
		{
			// Token: 0x06007EB7 RID: 32439 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007EB8 RID: 32440 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Init(ElementObjectManager m_View, ViewControllerManager manager)
			{
			}

			// Token: 0x06007EB9 RID: 32441 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}

			// Token: 0x0400B76F RID: 46959
			private readonly string BTN_TEAM_LABEL;

			// Token: 0x0400B770 RID: 46960
			private readonly string OBJ_TEAM_STATE_LABEL;
		}

		// Token: 0x020010A3 RID: 4259
		internal abstract class ColosseumMenuRankDuel : ColosseumViewController.ColosseumMenuBase
		{
			// Token: 0x0400B771 RID: 46961
			protected GameObject selfGo;

			// Token: 0x0400B772 RID: 46962
			protected SelectionButton selfBtn;
		}

		// Token: 0x020010A4 RID: 4260
		internal class ColosseumMenuStandard : ColosseumViewController.ColosseumMenuRankDuel
		{
			// Token: 0x06007EBC RID: 32444 RVA: 0x000029CC File Offset: 0x00000BCC
			internal override ColosseumUtil.PlayMode GetPlayMode()
			{
				return ColosseumUtil.PlayMode.NONE;
			}

			// Token: 0x06007EBD RID: 32445 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Init(ElementObjectManager m_View, ViewControllerManager manager)
			{
			}

			// Token: 0x06007EBE RID: 32446 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void Update()
			{
			}
		}
	}
}
