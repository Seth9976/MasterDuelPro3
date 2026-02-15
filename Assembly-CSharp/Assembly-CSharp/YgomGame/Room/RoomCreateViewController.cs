using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using YgomGame.ActionSheet;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Room
{
	// Token: 0x020009E9 RID: 2537
	public class RoomCreateViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060049E8 RID: 18920 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetDefaultRoomSettings(int meberMax = 6, bool isPublic = true, bool isSpectral = true, bool isSpecter = true, int roomComment = 0, int battleTimeId = 1, RoomUtil.LPType battleLp = RoomUtil.LPType.LP_8000, bool isReplay = true)
		{
			return null;
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject go, int index)
		{
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDefaultIdxSettings()
		{
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDefaultIdx(string key, int defaultIdx)
		{
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetData()
		{
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetMemberMax()
		{
		}

		// Token: 0x060049F1 RID: 18929 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIsPublic()
		{
		}

		// Token: 0x060049F2 RID: 18930 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIsSpectral()
		{
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIsSpecter()
		{
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBattleRule()
		{
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetRoomComment()
		{
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBattleTime()
		{
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBattleLP()
		{
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIsReplay()
		{
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIRoomCreate()
		{
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x0000216D File Offset: 0x0000036D
		private void ErrorRoomCreate(RoomCode roomCode)
		{
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x0000216A File Offset: 0x0000036A
		private Handle APIRoomCreate(Dictionary<string, object> _room_settings_)
		{
			return null;
		}

		// Token: 0x040087BE RID: 34750
		private readonly string SCROLL_LABEL;

		// Token: 0x040087BF RID: 34751
		private readonly string BTN_OK_LABEL;

		// Token: 0x040087C0 RID: 34752
		private List<RoomCreateViewController.TemplateInfo> infos;

		// Token: 0x040087C1 RID: 34753
		private InfinityScrollView isv;

		// Token: 0x040087C2 RID: 34754
		private const string KEY_MEMBER_MAX = "member_max";

		// Token: 0x040087C3 RID: 34755
		private const string KEY_PUBLIC = "is_public";

		// Token: 0x040087C4 RID: 34756
		private const string KEY_SPECTRAL = "is_spectral";

		// Token: 0x040087C5 RID: 34757
		private const string KEY_SPECTER = "is_specter";

		// Token: 0x040087C6 RID: 34758
		private const string KEY_COMMENT = "room_comment";

		// Token: 0x040087C7 RID: 34759
		private const string KEY_TIME = "battle_time";

		// Token: 0x040087C8 RID: 34760
		private const string KEY_LP = "battle_lp";

		// Token: 0x040087C9 RID: 34761
		private const string KEY_REPLAY = "is_replay";

		// Token: 0x040087CA RID: 34762
		private Dictionary<string, int> defaultIdxSettings;

		// Token: 0x040087CB RID: 34763
		private RegulationSelectSheet _regulationSelectSheet;

		// Token: 0x020009EA RID: 2538
		private struct DuelTimeSetting
		{
			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x060049FD RID: 18941 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060049FE RID: 18942 RVA: 0x0000216D File Offset: 0x0000036D
			public int id
			{
				[CompilerGenerated]
				readonly get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x060049FF RID: 18943 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06004A00 RID: 18944 RVA: 0x0000216D File Offset: 0x0000036D
			public string name
			{
				[CompilerGenerated]
				readonly get
				{
					return null;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06004A01 RID: 18945 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004A02 RID: 18946 RVA: 0x0000216D File Offset: 0x0000036D
			public int duration
			{
				[CompilerGenerated]
				readonly get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06004A03 RID: 18947 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x06004A04 RID: 18948 RVA: 0x0000216D File Offset: 0x0000036D
			public int order
			{
				[CompilerGenerated]
				readonly get
				{
					return 0;
				}
				[CompilerGenerated]
				set
				{
				}
			}
		}

		// Token: 0x020009EB RID: 2539
		internal class TemplateInfo
		{
			// Token: 0x06004A05 RID: 18949 RVA: 0x00002739 File Offset: 0x00000939
			public TemplateInfo(string settingLabel)
			{
			}

			// Token: 0x040087CC RID: 34764
			internal int templateType;

			// Token: 0x040087CD RID: 34765
			internal readonly string settingLabel;
		}

		// Token: 0x020009EC RID: 2540
		internal class LabelInfo : RoomCreateViewController.TemplateInfo
		{
			// Token: 0x06004A06 RID: 18950 RVA: 0x000F49D6 File Offset: 0x000F2BD6
			internal LabelInfo(string settingLabel)
				: base(null)
			{
			}
		}

		// Token: 0x020009ED RID: 2541
		internal class ButtonInfo : RoomCreateViewController.TemplateInfo
		{
			// Token: 0x06004A07 RID: 18951 RVA: 0x000F49D6 File Offset: 0x000F2BD6
			internal ButtonInfo(InfinityScrollView isv, string settingLabel, string title, string[] settingStrings, object[] settings, bool isActionSheet = true, int defaultSettings = 0)
				: base(null)
			{
			}

			// Token: 0x06004A08 RID: 18952 RVA: 0x0000216D File Offset: 0x0000036D
			internal void OnClick()
			{
			}

			// Token: 0x06004A09 RID: 18953 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void ProcessOnClicked()
			{
			}

			// Token: 0x040087CE RID: 34766
			internal readonly string title;

			// Token: 0x040087CF RID: 34767
			internal readonly string[] settingStrings;

			// Token: 0x040087D0 RID: 34768
			internal readonly object[] settings;

			// Token: 0x040087D1 RID: 34769
			internal readonly InfinityScrollView isv;

			// Token: 0x040087D2 RID: 34770
			internal int currentSetting;

			// Token: 0x040087D3 RID: 34771
			internal bool interactable;

			// Token: 0x040087D4 RID: 34772
			internal bool isActionSheet;

			// Token: 0x040087D5 RID: 34773
			internal UnityAction onFinishSetting;
		}

		// Token: 0x020009EE RID: 2542
		internal class BtnInfoForRule : RoomCreateViewController.ButtonInfo
		{
			// Token: 0x06004A0A RID: 18954 RVA: 0x000F49DF File Offset: 0x000F2BDF
			internal BtnInfoForRule(RegulationSelectSheet sheet, InfinityScrollView isv, string settingLabel, string title, string[] settingStrings, object[] settings, int defaultSettings = 0)
				: base(null, null, null, null, null, false, 0)
			{
			}

			// Token: 0x06004A0B RID: 18955 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void ProcessOnClicked()
			{
			}

			// Token: 0x040087D6 RID: 34774
			private RegulationSelectSheet _sheet;
		}
	}
}
