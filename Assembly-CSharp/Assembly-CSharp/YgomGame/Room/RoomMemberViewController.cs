using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Room
{
	// Token: 0x020009F6 RID: 2550
	public class RoomMemberViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEntity(GameObject gob, int index)
		{
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetData()
		{
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIRoomTablePoling()
		{
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnErrorCallAPI(RoomCode roomCode)
		{
		}

		// Token: 0x04008824 RID: 34852
		private readonly string SCROLL_LABEL;

		// Token: 0x04008825 RID: 34853
		private readonly string TXT_WIN_NUM_LABEL;

		// Token: 0x04008826 RID: 34854
		private readonly string TXT_LOSE_NUM_LABEL;

		// Token: 0x04008827 RID: 34855
		private readonly string TXT_DRAW_NUM_LABEL;

		// Token: 0x04008828 RID: 34856
		private readonly string IMG_ICON_LABEL;

		// Token: 0x04008829 RID: 34857
		private readonly string PLATFORM_NAME_LABEL;

		// Token: 0x0400882A RID: 34858
		private readonly string PLATFORM_ICON_LABEL;

		// Token: 0x0400882B RID: 34859
		private InfinityScrollView isv;

		// Token: 0x0400882C RID: 34860
		private List<RoomMemberViewController.Data> dataList;

		// Token: 0x0400882D RID: 34861
		private float pastSec;

		// Token: 0x0400882E RID: 34862
		private bool isCallingAPI;

		// Token: 0x020009F7 RID: 2551
		internal class Data
		{
			// Token: 0x06004A34 RID: 18996 RVA: 0x00002739 File Offset: 0x00000939
			public Data(long pcode, string name, int win, int lose, int draw, int iconID, int iconFrameID, bool isResistedPlatform = false, bool isSamePlatform = false, string platformName = "")
			{
			}

			// Token: 0x0400882F RID: 34863
			internal long pcode;

			// Token: 0x04008830 RID: 34864
			internal string name;

			// Token: 0x04008831 RID: 34865
			internal int win;

			// Token: 0x04008832 RID: 34866
			internal int lose;

			// Token: 0x04008833 RID: 34867
			internal int draw;

			// Token: 0x04008834 RID: 34868
			internal int iconID;

			// Token: 0x04008835 RID: 34869
			internal int iconFrameID;

			// Token: 0x04008836 RID: 34870
			internal bool isResistedPlatform;

			// Token: 0x04008837 RID: 34871
			internal bool isSamePlatform;

			// Token: 0x04008838 RID: 34872
			internal string platformName;
		}
	}
}
