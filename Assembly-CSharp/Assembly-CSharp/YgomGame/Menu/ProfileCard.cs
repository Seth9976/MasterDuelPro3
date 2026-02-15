using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu.Common;
using YgomGame.Utility;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Menu
{
	// Token: 0x02000ABF RID: 2751
	public class ProfileCard
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06005012 RID: 20498 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector selector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x00002739 File Offset: 0x00000939
		public ProfileCard(GameObject parent, Dictionary<string, object> args, bool playTween = true, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x00002739 File Offset: 0x00000939
		public ProfileCard(GameObject parent, long pcode, bool playTween = true)
		{
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateProfile(Dictionary<string, object> profileDic)
		{
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetName(string name, string platformName = null, bool isSamePlatform = false)
		{
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOfficialIcon(int iconType)
		{
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x0000216D File Offset: 0x0000036D
		public void HidePcode()
		{
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetProfileIcon(int baseId, int frameId)
		{
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIconFrame(int id)
		{
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIconBase(int id)
		{
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTag(List<object> list)
		{
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAvatar(int id)
		{
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetWallPaper(int id)
		{
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenGamerCard()
		{
		}

		// Token: 0x04008E07 RID: 36359
		public const string IMG_ICON_LABEL = "ImageIcon";

		// Token: 0x04008E08 RID: 36360
		public const string IMG_LEVELGAUGE_LABEL = "LevelGauge";

		// Token: 0x04008E09 RID: 36361
		public const string IMG_RANK_LABEL = "ImageRank";

		// Token: 0x04008E0A RID: 36362
		public const string IMG_AVATAR_LABEL = "ImageAvatar";

		// Token: 0x04008E0B RID: 36363
		public const string IMG_WALLPAPER_LABEL = "ImageWallPaper";

		// Token: 0x04008E0C RID: 36364
		public const string IMG_BACK_BG_LABEL = "BackBG";

		// Token: 0x04008E0D RID: 36365
		public const string TXT_FOLLOW_LABEL = "TextFollow";

		// Token: 0x04008E0E RID: 36366
		public const string TXT_FOLLOWER_LABEL = "TextFollower";

		// Token: 0x04008E0F RID: 36367
		public const string TXT_ID_LABEL = "TextID";

		// Token: 0x04008E10 RID: 36368
		public const string TXT_LEVEL_LABEL = "TextLevel";

		// Token: 0x04008E11 RID: 36369
		private readonly string TXT_PLATFORM_NAME_LABEL;

		// Token: 0x04008E12 RID: 36370
		private readonly string TXT_PLATFORM_ICON_LABEL;

		// Token: 0x04008E13 RID: 36371
		public const string ArgsKey_Name = "name";

		// Token: 0x04008E14 RID: 36372
		public const string ArgsKey_PlayerCode = "pcode";

		// Token: 0x04008E15 RID: 36373
		public const string ArgsKey_FollowNum = "follow_num";

		// Token: 0x04008E16 RID: 36374
		public const string ArgsKey_FollowerNum = "follower_num";

		// Token: 0x04008E17 RID: 36375
		public const string ArgsKey_Level = "level";

		// Token: 0x04008E18 RID: 36376
		public const string ArgsKey_Rank = "rank";

		// Token: 0x04008E19 RID: 36377
		public const string ArgsKey_Rate = "rate";

		// Token: 0x04008E1A RID: 36378
		public const string ArgsKey_IconID = "icon_id";

		// Token: 0x04008E1B RID: 36379
		public const string ArgsKey_FrameID = "icon_frame_id";

		// Token: 0x04008E1C RID: 36380
		public const string ArgsKey_TagID = "tag";

		// Token: 0x04008E1D RID: 36381
		public const string ArgsKey_AvatarID = "avatar_id";

		// Token: 0x04008E1E RID: 36382
		public const string ArgsKey_WallPaperID = "wallpaper";

		// Token: 0x04008E1F RID: 36383
		public const string ArgsKey_Exp = "exp";

		// Token: 0x04008E20 RID: 36384
		public const string ArgsKey_ExpNeed = "need_exp";

		// Token: 0x04008E21 RID: 36385
		public const string ArgsKey_OnlineId = "online_id";

		// Token: 0x04008E22 RID: 36386
		public const string ArgsKey_IsSameOS = "is_same_os";

		// Token: 0x04008E23 RID: 36387
		public const string ArgsKey_Xuid = "xuid";

		// Token: 0x04008E24 RID: 36388
		public const string ArgsKey_Edit = "edit";

		// Token: 0x04008E25 RID: 36389
		public const string ArgsKey_RankEvent = "rank_event";

		// Token: 0x04008E26 RID: 36390
		public const string ArgsKey_Official = "official";

		// Token: 0x04008E27 RID: 36391
		public BindingGameObjectEx bgoEX;

		// Token: 0x04008E28 RID: 36392
		public ElementObjectManager eom;

		// Token: 0x04008E29 RID: 36393
		private readonly string k_ELabelButtonGamerCard;

		// Token: 0x04008E2A RID: 36394
		private SelectionButton m_ButtonGamerCard;

		// Token: 0x04008E2B RID: 36395
		private bool isMine;

		// Token: 0x04008E2C RID: 36396
		private ulong xuid;

		// Token: 0x04008E2D RID: 36397
		private Selector m_Selector;

		// Token: 0x04008E2E RID: 36398
		private bool isEditButtonActive;

		// Token: 0x04008E2F RID: 36399
		private TextGroupLoadHolder m_TextGroupLoadHolder;

		// Token: 0x04008E30 RID: 36400
		private Dictionary<string, object> m_ProfileDic;
	}
}
