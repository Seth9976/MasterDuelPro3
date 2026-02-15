using System;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x0200129C RID: 4764
	public class SystemEvent : MonoBehaviour
	{
		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06008BAF RID: 35759 RVA: 0x0011F22C File Offset: 0x0011D42C
		// (remove) Token: 0x06008BB0 RID: 35760 RVA: 0x0011F260 File Offset: 0x0011D460
		public static event SystemEvent.SafeAreaUpdate OnSafeAreaUpdate;

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06008BB1 RID: 35761 RVA: 0x0011F294 File Offset: 0x0011D494
		// (remove) Token: 0x06008BB2 RID: 35762 RVA: 0x0011F2C8 File Offset: 0x0011D4C8
		public static event SystemEvent.ResolutionChange OnResolutionChange;

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06008BB3 RID: 35763 RVA: 0x0011F2FC File Offset: 0x0011D4FC
		// (remove) Token: 0x06008BB4 RID: 35764 RVA: 0x0011F330 File Offset: 0x0011D530
		public static event SystemEvent.LanguageChange OnLanguageChange;

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06008BB5 RID: 35765 RVA: 0x0011F364 File Offset: 0x0011D564
		// (remove) Token: 0x06008BB6 RID: 35766 RVA: 0x0011F398 File Offset: 0x0011D598
		public static event SystemEvent.VideoCardConfigChange OnVideoCardConfigChange;

		// Token: 0x06008BB7 RID: 35767 RVA: 0x0011F3CB File Offset: 0x0011D5CB
		private void Start()
		{
			this.safeArea = Screen.safeArea;
			this.screenWidth = Screen.width;
			this.screenHeight = Screen.height;
		}

		// Token: 0x06008BB8 RID: 35768 RVA: 0x0011F3F0 File Offset: 0x0011D5F0
		private void Update()
		{
			if (this.safeArea != Screen.safeArea)
			{
				this.safeArea = Screen.safeArea;
				SystemEvent.SafeAreaUpdate onSafeAreaUpdate = SystemEvent.OnSafeAreaUpdate;
				if (onSafeAreaUpdate != null)
				{
					onSafeAreaUpdate();
				}
			}
			if (this.screenWidth != Screen.width || this.screenHeight != Screen.height)
			{
				this.screenWidth = Screen.width;
				this.screenHeight = Screen.height;
				SystemEvent.ResolutionChange onResolutionChange = SystemEvent.OnResolutionChange;
				if (onResolutionChange == null)
				{
					return;
				}
				onResolutionChange();
			}
		}

		// Token: 0x06008BB9 RID: 35769 RVA: 0x0011F469 File Offset: 0x0011D669
		public static void CallLanguageChangeEvent()
		{
			SystemEvent.LanguageChange onLanguageChange = SystemEvent.OnLanguageChange;
			if (onLanguageChange == null)
			{
				return;
			}
			onLanguageChange();
		}

		// Token: 0x06008BBA RID: 35770 RVA: 0x0011F47A File Offset: 0x0011D67A
		public static void CallVideoCardConfigChangeEvent()
		{
			SystemEvent.VideoCardConfigChange onVideoCardConfigChange = SystemEvent.OnVideoCardConfigChange;
			if (onVideoCardConfigChange == null)
			{
				return;
			}
			onVideoCardConfigChange();
		}

		// Token: 0x0400C95E RID: 51550
		private Rect safeArea;

		// Token: 0x0400C95F RID: 51551
		private int screenWidth;

		// Token: 0x0400C960 RID: 51552
		private int screenHeight;

		// Token: 0x0200129D RID: 4765
		// (Invoke) Token: 0x06008BBD RID: 35773
		public delegate void SafeAreaUpdate();

		// Token: 0x0200129E RID: 4766
		// (Invoke) Token: 0x06008BC1 RID: 35777
		public delegate void ResolutionChange();

		// Token: 0x0200129F RID: 4767
		// (Invoke) Token: 0x06008BC5 RID: 35781
		public delegate void LanguageChange();

		// Token: 0x020012A0 RID: 4768
		// (Invoke) Token: 0x06008BC9 RID: 35785
		public delegate void VideoCardConfigChange();
	}
}
