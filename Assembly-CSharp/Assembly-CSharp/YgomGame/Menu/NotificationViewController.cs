using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.MDMarkup;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000AAC RID: 2732
	public class NotificationViewController : BaseBlurOverlayViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x0000216A File Offset: 0x0000036A
		protected override global::System.Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool defaultBlurOverlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x0000216A File Offset: 0x0000036A
		internal static string GetTypeLabel(NotificationViewController.Type type)
		{
			return null;
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnTransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIGetList(Action onSuccess = null)
		{
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPINotificationRead(int id, Action onFinish = null)
		{
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCode(Handle handle, Action onSuccess = null, Action<NotificationCode> onFailed = null)
		{
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetDatasCount(NotificationViewController.Type type)
		{
			return 0;
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateAll()
		{
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateNotification(NotificationViewController.Type type)
		{
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetBadge(NotificationViewController.Type type, bool isAlreadyReadType)
		{
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateScrollView(NotificationViewController.Type type)
		{
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEntityCallback(GameObject go, int index)
		{
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x04008D87 RID: 36231
		private readonly string SCROLL_LABEL;

		// Token: 0x04008D88 RID: 36232
		private readonly string TXT_CATEGORY_LABEL;

		// Token: 0x04008D89 RID: 36233
		private readonly string IMG_CATEGORY_LABEL;

		// Token: 0x04008D8A RID: 36234
		private readonly string ROOT_CATEGORY_LABEL;

		// Token: 0x04008D8B RID: 36235
		private readonly string ROOT_FOOTER_LABEL;

		// Token: 0x04008D8C RID: 36236
		private readonly string TXT_HEAD_LABEL;

		// Token: 0x04008D8D RID: 36237
		private readonly string TXT_BODY_LABEL;

		// Token: 0x04008D8E RID: 36238
		private readonly string TXT_EMPTY_LABEL;

		// Token: 0x04008D8F RID: 36239
		private readonly string BTN_LABEL;

		// Token: 0x04008D90 RID: 36240
		private readonly string BTN_NOTIFICATION_LABEL;

		// Token: 0x04008D91 RID: 36241
		private readonly string BTN_MAINTENANCE_LABEL;

		// Token: 0x04008D92 RID: 36242
		private readonly string BTN_BUG_LABEL;

		// Token: 0x04008D93 RID: 36243
		private readonly string BTN_CLOSE_LABEL;

		// Token: 0x04008D94 RID: 36244
		private readonly string BTN_TAP_BACK_LABEL;

		// Token: 0x04008D95 RID: 36245
		private readonly string IMG_BADGE_LABEL;

		// Token: 0x04008D96 RID: 36246
		private readonly string IMG_LABEL;

		// Token: 0x04008D97 RID: 36247
		private readonly string BTN_SC_L_LABEL;

		// Token: 0x04008D98 RID: 36248
		private readonly string BTN_SC_R_LABEL;

		// Token: 0x04008D99 RID: 36249
		public const string ARG_KEY_CLOSE_CALLBACK = "OnClosedCallback";

		// Token: 0x04008D9A RID: 36250
		private NotificationViewController.Type currentType;

		// Token: 0x04008D9B RID: 36251
		[SerializeField]
		private Color defaultCategoryColor;

		// Token: 0x04008D9C RID: 36252
		private InfinityScrollView isv;

		// Token: 0x04008D9D RID: 36253
		private List<NotificationViewController.Data> notificationDatas;

		// Token: 0x04008D9E RID: 36254
		private List<NotificationViewController.Data> maintenanceDatas;

		// Token: 0x04008D9F RID: 36255
		private List<NotificationViewController.Data> bugDatas;

		// Token: 0x02000AAD RID: 2733
		internal enum Type
		{
			// Token: 0x04008DA1 RID: 36257
			NOTIFICATION,
			// Token: 0x04008DA2 RID: 36258
			MAINTENANCE,
			// Token: 0x04008DA3 RID: 36259
			BUG
		}

		// Token: 0x02000AAE RID: 2734
		internal class Data
		{
			// Token: 0x06004FA0 RID: 20384 RVA: 0x00002739 File Offset: 0x00000939
			public Data(NotificationViewController.Type type, int id, string category, Color categoryColor, string date, string head, string body, int sort, bool isAlreadyRead, MDMarkupBannerContext bannerContext)
			{
			}

			// Token: 0x04008DA4 RID: 36260
			internal readonly NotificationViewController.Type type;

			// Token: 0x04008DA5 RID: 36261
			internal readonly int id;

			// Token: 0x04008DA6 RID: 36262
			internal readonly string category;

			// Token: 0x04008DA7 RID: 36263
			internal readonly Color categoryColor;

			// Token: 0x04008DA8 RID: 36264
			internal readonly string date;

			// Token: 0x04008DA9 RID: 36265
			internal readonly string head;

			// Token: 0x04008DAA RID: 36266
			internal readonly string body;

			// Token: 0x04008DAB RID: 36267
			internal readonly int sort;

			// Token: 0x04008DAC RID: 36268
			internal readonly MDMarkupBannerContext bannerContext;

			// Token: 0x04008DAD RID: 36269
			internal bool isAlreadyRead;
		}
	}
}
