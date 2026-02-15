using System;
using System.Collections.Generic;
using YgomGame.Menu;

namespace YgomGame.Room
{
	// Token: 0x020009F2 RID: 2546
	public class RoomInfoViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06004A18 RID: 18968 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetArgs(RoomEntryViewController.Mode mode, string roomName, int roomID, int roomMemberCurrent, int roomMemberMax, string isPublic, string isSpectral, string isSpecter, string roomComment, int battleTimeId, string battleLP, string isReplay, int spectorID, int spectorMemberCurrent, int regulationID, string regulationName)
		{
			return null;
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x040087E9 RID: 34793
		private readonly string BTN_ROOM_COPY_LABEL;

		// Token: 0x040087EA RID: 34794
		private readonly string BTN_SPECTOR_COPY_LABEL;

		// Token: 0x040087EB RID: 34795
		private readonly string TXT_TITLE_LABEL;

		// Token: 0x040087EC RID: 34796
		private readonly string TXT_ROOM_LABEL;

		// Token: 0x040087ED RID: 34797
		private readonly string TXT_ROOM_MEMBER_LABEL;

		// Token: 0x040087EE RID: 34798
		private readonly string TXT_SPECTOR_LABEL;

		// Token: 0x040087EF RID: 34799
		private readonly string TXT_SPECTOR_MEMBER_LABEL;

		// Token: 0x040087F0 RID: 34800
		private readonly string TXT_ITEM_LABEL;

		// Token: 0x040087F1 RID: 34801
		private readonly string TXT_VALUE_LABEL;

		// Token: 0x040087F2 RID: 34802
		private readonly string ROOT_NORMAL_LABEL;

		// Token: 0x040087F3 RID: 34803
		private readonly string ROOT_SPECTOR_LABEL;

		// Token: 0x040087F4 RID: 34804
		private readonly string SCROLL_LABEL;

		// Token: 0x040087F5 RID: 34805
		private readonly string IMG_LINE_LABEL;

		// Token: 0x040087F6 RID: 34806
		private readonly string TXT_REGULATION_LABEL;

		// Token: 0x040087F7 RID: 34807
		private readonly string ICON_REGULATION_LABEL;

		// Token: 0x040087F8 RID: 34808
		private readonly string TXT_ROOM_DEADLINE;

		// Token: 0x040087F9 RID: 34809
		private const string KEY_ENTRY_MODE = "EntryMode";

		// Token: 0x040087FA RID: 34810
		private const string KEY_ROOM_NAME = "RoomName";

		// Token: 0x040087FB RID: 34811
		private const string KEY_ROOM_ID = "RoomID";

		// Token: 0x040087FC RID: 34812
		private const string KEY_SPECTOR_ID = "SpectorID";

		// Token: 0x040087FD RID: 34813
		private const string KEY_ROOM_MEMBER_CURRENT = "RoomMemberCurrent";

		// Token: 0x040087FE RID: 34814
		private const string KEY_SPECTOR_MEMBER_CURRENT = "RoomSpectorCurrent";

		// Token: 0x040087FF RID: 34815
		private const string KEY_ROOM_MEMBER_MAX = "RoomMemberMax";

		// Token: 0x04008800 RID: 34816
		private const string KEY_PUBLIC = "IsPublic";

		// Token: 0x04008801 RID: 34817
		private const string KEY_SPECTRAL = "IsSpectral";

		// Token: 0x04008802 RID: 34818
		private const string KEY_SPECTER = "IsSpecter";

		// Token: 0x04008803 RID: 34819
		private const string KEY_ROOM_COMMENT = "RoomComment";

		// Token: 0x04008804 RID: 34820
		private const string KEY_BATTLE_TIME_ID = "BattleTimeId";

		// Token: 0x04008805 RID: 34821
		private const string KEY_BATTLE_LP = "BattleLP";

		// Token: 0x04008806 RID: 34822
		private const string KEY_REPLAY = "IsReplay";

		// Token: 0x04008807 RID: 34823
		private const string KEY_REGNAME = "RegName";

		// Token: 0x04008808 RID: 34824
		private const string KEY_REGID = "RegID";

		// Token: 0x04008809 RID: 34825
		private List<RoomInfoViewController.Data> roomSettings;

		// Token: 0x0400880A RID: 34826
		private RoomEntryViewController.Mode mode;

		// Token: 0x0400880B RID: 34827
		private string roomName;

		// Token: 0x0400880C RID: 34828
		private int roomID;

		// Token: 0x0400880D RID: 34829
		private int roomMemberCurrent;

		// Token: 0x0400880E RID: 34830
		private int roomMemberMax;

		// Token: 0x0400880F RID: 34831
		private int spectorID;

		// Token: 0x04008810 RID: 34832
		private int spectorMemberCurrent;

		// Token: 0x04008811 RID: 34833
		private string regulationName;

		// Token: 0x04008812 RID: 34834
		private int regulationID;

		// Token: 0x04008813 RID: 34835
		private string roomDeadlineDate;

		// Token: 0x020009F3 RID: 2547
		private class Data
		{
			// Token: 0x06004A1D RID: 18973 RVA: 0x00002739 File Offset: 0x00000939
			public Data(string label, string value)
			{
			}

			// Token: 0x04008814 RID: 34836
			internal string label;

			// Token: 0x04008815 RID: 34837
			internal string value;
		}
	}
}
