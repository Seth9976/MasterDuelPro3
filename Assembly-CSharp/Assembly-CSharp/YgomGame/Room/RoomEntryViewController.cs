using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.Network;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Room
{
	// Token: 0x020009EF RID: 2543
	public class RoomEntryViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06004A0C RID: 18956 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEntity(GameObject gob, int index)
		{
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetData()
		{
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetIDKeyName(RoomEntryViewController.Mode mode)
		{
			return null;
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIRoomEntry(int id, RoomEntryViewController.Mode mode)
		{
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x0000216A File Offset: 0x0000036A
		private Handle APIRoomEntry(int _id_, int _is_specter_, Dictionary<string, object> _options_)
		{
			return null;
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIRoomGetRoomList(Action<bool> onEnd = null)
		{
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIRoomGetRoomList2(Action<bool> onEnd)
		{
		}

		// Token: 0x040087D7 RID: 34775
		private readonly string SCROLL_LABEL;

		// Token: 0x040087D8 RID: 34776
		private readonly string BTN_RELOAD_LABEL;

		// Token: 0x040087D9 RID: 34777
		private readonly string INPUT_LABEL;

		// Token: 0x040087DA RID: 34778
		private readonly string TXT_TITLE_LABEL;

		// Token: 0x040087DB RID: 34779
		private readonly string TXT_PLACEHOLDER_LABEL;

		// Token: 0x040087DC RID: 34780
		private InfinityScrollView isv;

		// Token: 0x040087DD RID: 34781
		private List<RoomEntryViewController.Data> dataList;

		// Token: 0x040087DE RID: 34782
		private RoomEntryViewController.Mode mode;

		// Token: 0x020009F0 RID: 2544
		public enum Mode
		{
			// Token: 0x040087E0 RID: 34784
			NORMAL,
			// Token: 0x040087E1 RID: 34785
			SPECTER
		}

		// Token: 0x020009F1 RID: 2545
		internal class Data
		{
			// Token: 0x06004A17 RID: 18967 RVA: 0x00002739 File Offset: 0x00000939
			public Data(int id, string name, string regulation, int comment, int memberMax, int memberNum, string endDate)
			{
			}

			// Token: 0x040087E2 RID: 34786
			internal int id;

			// Token: 0x040087E3 RID: 34787
			internal string name;

			// Token: 0x040087E4 RID: 34788
			internal string regulation;

			// Token: 0x040087E5 RID: 34789
			internal int comment;

			// Token: 0x040087E6 RID: 34790
			internal int memberMax;

			// Token: 0x040087E7 RID: 34791
			internal int memberNum;

			// Token: 0x040087E8 RID: 34792
			internal string endDate;
		}
	}
}
