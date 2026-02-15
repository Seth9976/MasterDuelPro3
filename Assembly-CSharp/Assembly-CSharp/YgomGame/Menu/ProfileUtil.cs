using System;

namespace YgomGame.Menu
{
	// Token: 0x02000AD0 RID: 2768
	public class ProfileUtil
	{
		// Token: 0x060050A7 RID: 20647 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallFriendFollow(long pcode, int delete, Action endAction = null, Action errorAction = null)
		{
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetAvatarIconPath(int id)
		{
			return null;
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetWallPaperThumbPath(int id)
		{
			return null;
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PushProfileViewPlayer(int player)
		{
		}

		// Token: 0x060050AB RID: 20651 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PushProfileViewPcode(long pcode)
		{
		}

		// Token: 0x060050AC RID: 20652 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallBlockAPI(long pcode, int delete, Action endAction = null, Action errorAction = null)
		{
		}

		// Token: 0x060050AD RID: 20653 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckFriendIsBlock(long pcode)
		{
			return false;
		}

		// Token: 0x04008ED7 RID: 36567
		private const string AVATARICON_PATH = "Images/Menu/AvatarIcon/{0}";

		// Token: 0x04008ED8 RID: 36568
		private const string WALLPAPER_THUMB_PATH = "Images/WallPaper/WallPaper{0:D4}/WallPaperThumb{1}";

		// Token: 0x04008ED9 RID: 36569
		public const int MYTAG_QUANTITY = 4;
	}
}
