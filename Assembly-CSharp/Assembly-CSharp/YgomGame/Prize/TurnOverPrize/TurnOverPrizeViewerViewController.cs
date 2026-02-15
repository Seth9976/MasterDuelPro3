using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Menu;
using YgomSystem.ElementSystem;

namespace YgomGame.Prize.TurnOverPrize
{
	// Token: 0x02000A1B RID: 2587
	public class TurnOverPrizeViewerViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06004B19 RID: 19225 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsInfo(int prizeId = 1)
		{
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenAsResult(int prizeId = 1)
		{
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x0000216D File Offset: 0x0000036D
		private static void InnerOpen(TurnOverPrizeViewerViewController.Mode mode, int prizeId, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateEntities()
		{
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x04008936 RID: 35126
		private const string k_VCPath = "Prize/TurnOverPrize/TurnOverPrizeViewer";

		// Token: 0x04008937 RID: 35127
		private const string k_ArgKeyPrizeId = "prizeId";

		// Token: 0x04008938 RID: 35128
		private const string k_ArgKeyMode = "mode";

		// Token: 0x04008939 RID: 35129
		private const string k_VLabel_Info = "InfoView";

		// Token: 0x0400893A RID: 35130
		private const string k_VLabel_Result = "ResultView";

		// Token: 0x0400893B RID: 35131
		private const string k_ELabelPackGroup = "PackGroup";

		// Token: 0x0400893C RID: 35132
		private const string k_ELabelPackGroup_Template = "Template";

		// Token: 0x0400893D RID: 35133
		private const string k_ELabelPackGroup_LocaterFormat = "Locater{0}";

		// Token: 0x0400893E RID: 35134
		private Sprite m_CoverSprite;

		// Token: 0x0400893F RID: 35135
		private int m_ShopId;

		// Token: 0x04008940 RID: 35136
		private List<object> m_PrizeDatas;

		// Token: 0x04008941 RID: 35137
		private TurnOverPrizeViewerViewController.IBehaviour m_Behaviour;

		// Token: 0x04008942 RID: 35138
		private ElementObjectManager[] m_Entities;

		// Token: 0x02000A1C RID: 2588
		private class InfoBehaviour : TurnOverPrizeViewerViewController.IBehaviour
		{
			// Token: 0x06004B23 RID: 19235 RVA: 0x00002739 File Offset: 0x00000939
			public InfoBehaviour(TurnOverPrizeViewerViewController owner)
			{
			}

			// Token: 0x06004B24 RID: 19236 RVA: 0x0000216D File Offset: 0x0000036D
			public void NotificationStackEntry()
			{
			}

			// Token: 0x06004B25 RID: 19237 RVA: 0x0000216D File Offset: 0x0000036D
			public void NotificationStackRemove()
			{
			}

			// Token: 0x06004B26 RID: 19238 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnCreatedView()
			{
			}

			// Token: 0x06004B27 RID: 19239 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnStart()
			{
			}

			// Token: 0x06004B28 RID: 19240 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClick(int dataIdx)
			{
			}

			// Token: 0x04008943 RID: 35139
			private const string k_ELabel_PackImage = "PackImage";

			// Token: 0x04008944 RID: 35140
			private readonly TurnOverPrizeViewerViewController m_Owner;

			// Token: 0x04008945 RID: 35141
			private List<string> m_InfoSheetEntries;
		}

		// Token: 0x02000A1D RID: 2589
		private class ResultBehaviour : TurnOverPrizeViewerViewController.IBehaviour
		{
			// Token: 0x06004B29 RID: 19241 RVA: 0x00002739 File Offset: 0x00000939
			public ResultBehaviour(TurnOverPrizeViewerViewController owner)
			{
			}

			// Token: 0x06004B2A RID: 19242 RVA: 0x0000216D File Offset: 0x0000036D
			public void NotificationStackEntry()
			{
			}

			// Token: 0x06004B2B RID: 19243 RVA: 0x0000216D File Offset: 0x0000036D
			public void NotificationStackRemove()
			{
			}

			// Token: 0x06004B2C RID: 19244 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnCreatedView()
			{
			}

			// Token: 0x06004B2D RID: 19245 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnStart()
			{
			}

			// Token: 0x06004B2E RID: 19246 RVA: 0x0000216D File Offset: 0x0000036D
			public void PackToDefault(Image packImage)
			{
			}

			// Token: 0x06004B2F RID: 19247 RVA: 0x0000216D File Offset: 0x0000036D
			public void PackToShadow(Image packImage)
			{
			}

			// Token: 0x06004B30 RID: 19248 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClick(int dataIdx)
			{
			}

			// Token: 0x04008946 RID: 35142
			private const string k_ELabel_CardImage = "CardImage";

			// Token: 0x04008947 RID: 35143
			private const string k_ELabel_PackImage = "PackImage";

			// Token: 0x04008948 RID: 35144
			private const string k_TLabelPackDefault = "Default";

			// Token: 0x04008949 RID: 35145
			private const string k_TLabelPackShadow = "Shadow";

			// Token: 0x0400894A RID: 35146
			private readonly TurnOverPrizeViewerViewController m_Owner;

			// Token: 0x0400894B RID: 35147
			private List<int> m_CardDetailMrks;

			// Token: 0x0400894C RID: 35148
			private List<int> m_CardDetailStyles;

			// Token: 0x0400894D RID: 35149
			private Dictionary<int, int> m_CardDetailPosMap;
		}

		// Token: 0x02000A1E RID: 2590
		private interface IBehaviour
		{
			// Token: 0x06004B31 RID: 19249
			void NotificationStackEntry();

			// Token: 0x06004B32 RID: 19250
			void OnCreatedView();

			// Token: 0x06004B33 RID: 19251
			void OnStart();

			// Token: 0x06004B34 RID: 19252
			void NotificationStackRemove();
		}

		// Token: 0x02000A1F RID: 2591
		public enum Mode
		{
			// Token: 0x0400894F RID: 35151
			Info,
			// Token: 0x04008950 RID: 35152
			Result
		}
	}
}
