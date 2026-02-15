using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.Menu;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Room
{
	// Token: 0x020009FE RID: 2558
	public class RoomViewController : BaseMenuViewController, IBackButtonWithoutSCSupported, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x000029CC File Offset: 0x00000BCC
		protected bool existDialog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEntity(GameObject gob, int index)
		{
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject CreateEmbedObj(int deckId)
		{
			return null;
		}

		// Token: 0x04008855 RID: 34901
		private readonly string SCROLL_LABEL;

		// Token: 0x04008856 RID: 34902
		private readonly string SC_BACK_LABEL;

		// Token: 0x04008857 RID: 34903
		[SerializeField]
		private ElementObjectManager _deckOverview;

		// Token: 0x04008858 RID: 34904
		private RoomViewController.RoomBehaviour roomBehaviour;

		// Token: 0x04008859 RID: 34905
		private InfinityScrollView isv;

		// Token: 0x0400885A RID: 34906
		private RoomEntryViewController.Mode mode;

		// Token: 0x0400885B RID: 34907
		private float pastSec;

		// Token: 0x020009FF RID: 2559
		internal abstract class RoomBehaviour
		{
			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x06004A54 RID: 19028 RVA: 0x000029CC File Offset: 0x00000BCC
			internal bool isCallingAPI
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004A55 RID: 19029 RVA: 0x00002739 File Offset: 0x00000939
			internal RoomBehaviour(ViewControllerManager manager, RoomViewController vc, ElementObjectManager viewEom, InfinityScrollView isv)
			{
			}

			// Token: 0x06004A56 RID: 19030 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void OnTerminal()
			{
			}

			// Token: 0x06004A57 RID: 19031
			internal abstract void InitRoom();

			// Token: 0x06004A58 RID: 19032 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void UpdateTable(ElementObjectManager eom, int index)
			{
			}

			// Token: 0x06004A59 RID: 19033 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void OnCreatedEntity(GameObject go)
			{
			}

			// Token: 0x06004A5A RID: 19034 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetComment(ElementObjectManager playerEom, RoomViewController.RoomBehaviour.MemberData member)
			{
			}

			// Token: 0x06004A5B RID: 19035 RVA: 0x0000216D File Offset: 0x0000036D
			private void ForceSetComment(ElementObjectManager playerEom, bool isSetShow)
			{
			}

			// Token: 0x06004A5C RID: 19036 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool OnClickActionWithCheckSitting(UnityAction onFinish)
			{
				return false;
			}

			// Token: 0x06004A5D RID: 19037
			protected abstract void CreateMenuButtons();

			// Token: 0x06004A5E RID: 19038 RVA: 0x0000216A File Offset: 0x0000036A
			protected SelectionButton CreateMenuButton(Selector selector, GameObject template, string label, UnityAction onClick = null, bool isDefaultItem = false)
			{
				return null;
			}

			// Token: 0x06004A5F RID: 19039 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetRoomInfo()
			{
			}

			// Token: 0x06004A60 RID: 19040 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void UpdateRoom()
			{
			}

			// Token: 0x06004A61 RID: 19041 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void SetTable(bool isUpdateDataCount = false)
			{
			}

			// Token: 0x06004A62 RID: 19042 RVA: 0x0000216D File Offset: 0x0000036D
			protected void OnClickExitButton()
			{
			}

			// Token: 0x06004A63 RID: 19043 RVA: 0x0000216D File Offset: 0x0000036D
			protected void AddCallingCount()
			{
			}

			// Token: 0x06004A64 RID: 19044 RVA: 0x0000216D File Offset: 0x0000036D
			protected void DecCallingCount()
			{
			}

			// Token: 0x06004A65 RID: 19045 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void CallAPIRoomExit()
			{
			}

			// Token: 0x06004A66 RID: 19046 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPIRoomTablePoling(Action onFinish = null, bool isInit = false)
			{
			}

			// Token: 0x06004A67 RID: 19047 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPIRoomTableArrive(int tableNo)
			{
			}

			// Token: 0x06004A68 RID: 19048 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPIRoomTableLeave(UnityAction onSuccess = null)
			{
			}

			// Token: 0x06004A69 RID: 19049 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPIRoomBattleReady(bool isReady, long rivalPcode = 0L)
			{
			}

			// Token: 0x06004A6A RID: 19050 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPIRoomSetUserComment(int commentID)
			{
			}

			// Token: 0x06004A6B RID: 19051 RVA: 0x0000216D File Offset: 0x0000036D
			protected void CallAPIPvPWatchDuel(long pcode)
			{
			}

			// Token: 0x06004A6C RID: 19052 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OnErrorCallAPI(RoomCode roomCode)
			{
			}

			// Token: 0x0400885C RID: 34908
			protected readonly string BTN_LABEL;

			// Token: 0x0400885D RID: 34909
			protected readonly string ROOT_MENU_LABEL;

			// Token: 0x0400885E RID: 34910
			protected readonly string TMP_BTN_MENU_LABEL;

			// Token: 0x0400885F RID: 34911
			protected readonly string TXT_LABEL;

			// Token: 0x04008860 RID: 34912
			protected readonly string BTN_EXIT_LABEL;

			// Token: 0x04008861 RID: 34913
			protected readonly string BTN_DECK_LABEL;

			// Token: 0x04008862 RID: 34914
			protected readonly string BTN_DECK_READONLY_LABEL;

			// Token: 0x04008863 RID: 34915
			protected readonly string BTN_ENTRY_LABEL;

			// Token: 0x04008864 RID: 34916
			protected readonly string BTN_COMMENT_RIGHT_LABEL;

			// Token: 0x04008865 RID: 34917
			protected readonly string BTN_COMMENT_LEFT_LABEL;

			// Token: 0x04008866 RID: 34918
			protected readonly string BTN_LEAVE_LABEL;

			// Token: 0x04008867 RID: 34919
			protected readonly string TXT_ROOM_MEMBER_LABEL;

			// Token: 0x04008868 RID: 34920
			protected readonly string TXT_ROOM_NAME_LABEL;

			// Token: 0x04008869 RID: 34921
			protected readonly string TXT_ROOM_WATCHER_LABEL;

			// Token: 0x0400886A RID: 34922
			protected readonly string ROOT_ROOM_WATCHER_LABEL;

			// Token: 0x0400886B RID: 34923
			protected readonly string IMG_ICON_LABEL;

			// Token: 0x0400886C RID: 34924
			protected readonly string TXT_TITLE_LABEL;

			// Token: 0x0400886D RID: 34925
			protected readonly string TXT_REGULATION_LABEL;

			// Token: 0x0400886E RID: 34926
			protected readonly string ICON_REGULATION_LABEL;

			// Token: 0x0400886F RID: 34927
			protected readonly string PLATFORM_NAME_LABEL;

			// Token: 0x04008870 RID: 34928
			protected readonly string PLATFORM_ICON_LABEL;

			// Token: 0x04008871 RID: 34929
			protected readonly string TXT_STATUS_LABEL;

			// Token: 0x04008872 RID: 34930
			protected readonly string TXT_COMMENT_LABEL;

			// Token: 0x04008873 RID: 34931
			protected readonly string OBJ_COMMENT_LABEL;

			// Token: 0x04008874 RID: 34932
			private int callingApiCount;

			// Token: 0x04008875 RID: 34933
			internal readonly ViewControllerManager manager;

			// Token: 0x04008876 RID: 34934
			internal readonly RoomViewController vc;

			// Token: 0x04008877 RID: 34935
			internal readonly ElementObjectManager viewEom;

			// Token: 0x04008878 RID: 34936
			internal readonly InfinityScrollView isv;

			// Token: 0x04008879 RID: 34937
			internal RoomViewController.RoomBehaviour.RoomInfo roomInfo;

			// Token: 0x0400887A RID: 34938
			internal List<RoomViewController.RoomBehaviour.TableData> tableDataList;

			// Token: 0x0400887B RID: 34939
			protected long myPcode;

			// Token: 0x0400887C RID: 34940
			protected bool isSittingPlayer;

			// Token: 0x0400887D RID: 34941
			protected readonly string[] tableComments;

			// Token: 0x0400887E RID: 34942
			protected string beforeRoomName;

			// Token: 0x02000A00 RID: 2560
			internal enum TableStatus
			{
				// Token: 0x04008880 RID: 34944
				WAIT = 1,
				// Token: 0x04008881 RID: 34945
				READY_RIGHT,
				// Token: 0x04008882 RID: 34946
				READY_LEFT,
				// Token: 0x04008883 RID: 34947
				DUEL,
				// Token: 0x04008884 RID: 34948
				SPECTATE
			}

			// Token: 0x02000A01 RID: 2561
			internal class TableData
			{
				// Token: 0x170006CA RID: 1738
				// (get) Token: 0x06004A6D RID: 19053 RVA: 0x000029CC File Offset: 0x00000BCC
				// (set) Token: 0x06004A6E RID: 19054 RVA: 0x0000216D File Offset: 0x0000036D
				internal bool myPlayerEntry
				{
					[CompilerGenerated]
					get
					{
						return false;
					}
					[CompilerGenerated]
					private set
					{
					}
				}

				// Token: 0x06004A6F RID: 19055 RVA: 0x00002739 File Offset: 0x00000939
				public TableData(RoomViewController.RoomBehaviour.MemberData[] members, RoomViewController.RoomBehaviour.TableStatus status, bool myPlayerEntry)
				{
				}

				// Token: 0x04008885 RID: 34949
				internal RoomViewController.RoomBehaviour.MemberData[] members;

				// Token: 0x04008886 RID: 34950
				internal RoomViewController.RoomBehaviour.TableStatus status;
			}

			// Token: 0x02000A02 RID: 2562
			internal class MemberData
			{
				// Token: 0x04008887 RID: 34951
				internal long pcode;

				// Token: 0x04008888 RID: 34952
				internal string name;

				// Token: 0x04008889 RID: 34953
				internal int iconID;

				// Token: 0x0400888A RID: 34954
				internal int iconFrameID;

				// Token: 0x0400888B RID: 34955
				internal int commentID;

				// Token: 0x0400888C RID: 34956
				internal bool isResistedPlatform;

				// Token: 0x0400888D RID: 34957
				internal bool isSamePlatform;

				// Token: 0x0400888E RID: 34958
				internal string platformName;
			}

			// Token: 0x02000A03 RID: 2563
			internal class RoomInfo
			{
				// Token: 0x170006CB RID: 1739
				// (get) Token: 0x06004A71 RID: 19057 RVA: 0x000029CC File Offset: 0x00000BCC
				internal int regID_in_client
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x0400888F RID: 34959
				internal int roomID;

				// Token: 0x04008890 RID: 34960
				internal string roomName;

				// Token: 0x04008891 RID: 34961
				internal long roomMasterID;

				// Token: 0x04008892 RID: 34962
				internal int memberNum;

				// Token: 0x04008893 RID: 34963
				internal int roomSpecterID;

				// Token: 0x04008894 RID: 34964
				internal int specterNum;

				// Token: 0x04008895 RID: 34965
				internal int memberMax;

				// Token: 0x04008896 RID: 34966
				internal int regID;

				// Token: 0x04008897 RID: 34967
				internal string regulation;

				// Token: 0x04008898 RID: 34968
				internal int roomComment;

				// Token: 0x04008899 RID: 34969
				internal bool canEnterFree;

				// Token: 0x0400889A RID: 34970
				internal bool canWatchRoom;

				// Token: 0x0400889B RID: 34971
				internal bool canWatchEnterFree;

				// Token: 0x0400889C RID: 34972
				internal int havePlayTime;

				// Token: 0x0400889D RID: 34973
				internal int LP;

				// Token: 0x0400889E RID: 34974
				internal bool canWatchReplay;
			}
		}

		// Token: 0x02000A04 RID: 2564
		internal class RoomBehaviourNormal : RoomViewController.RoomBehaviour
		{
			// Token: 0x06004A73 RID: 19059 RVA: 0x000F49EE File Offset: 0x000F2BEE
			public RoomBehaviourNormal(ViewControllerManager manager, RoomViewController vc, ElementObjectManager viewEom, InfinityScrollView isv)
				: base(null, null, null, null)
			{
			}

			// Token: 0x06004A74 RID: 19060 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnTerminal()
			{
			}

			// Token: 0x06004A75 RID: 19061 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void CreateMenuButtons()
			{
			}

			// Token: 0x06004A76 RID: 19062 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitRoom()
			{
			}

			// Token: 0x06004A77 RID: 19063 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateTable(ElementObjectManager eom, int index)
			{
			}

			// Token: 0x06004A78 RID: 19064 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnCreatedEntity(GameObject go)
			{
			}

			// Token: 0x06004A79 RID: 19065 RVA: 0x0000216D File Offset: 0x0000036D
			private void UpdateDeck()
			{
			}

			// Token: 0x06004A7A RID: 19066 RVA: 0x000029CC File Offset: 0x00000BCC
			internal virtual bool SetDeck(int did)
			{
				return false;
			}

			// Token: 0x06004A7B RID: 19067 RVA: 0x0000216D File Offset: 0x0000036D
			internal virtual void CallAPIDeckCheck(Action onFinish = null)
			{
			}

			// Token: 0x06004A7C RID: 19068 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClickDeck()
			{
			}

			// Token: 0x06004A7D RID: 19069 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectionSelectionItems(GameObject entity)
			{
				return null;
			}

			// Token: 0x06004A7E RID: 19070 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void SetTable(bool isUpdateDataCount = false)
			{
			}

			// Token: 0x06004A7F RID: 19071 RVA: 0x0000216D File Offset: 0x0000036D
			private void RestrictMenuButtons(bool on)
			{
			}

			// Token: 0x06004A80 RID: 19072 RVA: 0x0000216D File Offset: 0x0000036D
			private void GotoDeckSelect()
			{
			}

			// Token: 0x06004A81 RID: 19073 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnSelectInActionSheet(int select)
			{
			}

			// Token: 0x0400889F RID: 34975
			private Dictionary<int, SelectionButton> _menuButtonmap;

			// Token: 0x040088A0 RID: 34976
			protected DeckCaseWidget deckCase;

			// Token: 0x040088A1 RID: 34977
			protected StringBuilder deckNameBuf;

			// Token: 0x040088A2 RID: 34978
			private int currentDeckId;

			// Token: 0x02000A05 RID: 2565
			private enum MenuBtn
			{
				// Token: 0x040088A4 RID: 34980
				NONE,
				// Token: 0x040088A5 RID: 34981
				INFO,
				// Token: 0x040088A6 RID: 34982
				MEMBER,
				// Token: 0x040088A7 RID: 34983
				REPLAY,
				// Token: 0x040088A8 RID: 34984
				INVITE,
				// Token: 0x040088A9 RID: 34985
				DECK,
				// Token: 0x040088AA RID: 34986
				DECK_READONLY
			}

			// Token: 0x02000A06 RID: 2566
			private enum DeckSelectKind
			{
				// Token: 0x040088AC RID: 34988
				EDIT,
				// Token: 0x040088AD RID: 34989
				CHANGE,
				// Token: 0x040088AE RID: 34990
				CONFIRM
			}
		}

		// Token: 0x02000A07 RID: 2567
		internal class RoomBehaviourSpecter : RoomViewController.RoomBehaviour
		{
			// Token: 0x06004A82 RID: 19074 RVA: 0x000F49EE File Offset: 0x000F2BEE
			internal RoomBehaviourSpecter(ViewControllerManager manager, RoomViewController vc, ElementObjectManager viewEom, InfinityScrollView isv)
				: base(null, null, null, null)
			{
			}

			// Token: 0x06004A83 RID: 19075 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void CreateMenuButtons()
			{
			}

			// Token: 0x06004A84 RID: 19076 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitRoom()
			{
			}

			// Token: 0x06004A85 RID: 19077 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateTable(ElementObjectManager eom, int index)
			{
			}

			// Token: 0x06004A86 RID: 19078 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectionSelectionItems(GameObject entity)
			{
				return null;
			}
		}

		// Token: 0x02000A08 RID: 2568
		private static class Util
		{
			// Token: 0x06004A87 RID: 19079 RVA: 0x000029CC File Offset: 0x00000BCC
			internal static int GetValueFromAccessory(Dictionary<string, object> accessory, string keyName)
			{
				return 0;
			}
		}
	}
}
