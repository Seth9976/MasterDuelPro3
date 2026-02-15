using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.Network;
using YgomSystem.UI;
using YgomSystem.Utility;

namespace YgomGame.Menu
{
	// Token: 0x02000AA8 RID: 2728
	public class LoginBonusViewController : BaseMenuViewController
	{
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(ViewControllerManager manager, LoginBonusViewController.Mode mode, Dictionary<string, object> args)
		{
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x0000216D File Offset: 0x0000036D
		private static void RequestServer(LoginBonusViewController.Mode mode, int loginBonusID, Action<Handle, LoginBonusCode> onEnd)
		{
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator Start()
		{
			return null;
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ObtainRewards()
		{
			return null;
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadDataAsync()
		{
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupButtons()
		{
		}

		// Token: 0x04008D5C RID: 36188
		private const string VC_PATH = "LoginBonus/LoginBonus";

		// Token: 0x04008D5D RID: 36189
		private const string k_ArgKeyMode = "mode";

		// Token: 0x04008D5E RID: 36190
		internal const string k_ArgKeyLaunchId = "id";

		// Token: 0x04008D5F RID: 36191
		internal const string k_ArgKeyCallback = "callback";

		// Token: 0x04008D60 RID: 36192
		private const string k_ArgKeyGetListAPICalling = "get_list_api_calling";

		// Token: 0x04008D61 RID: 36193
		private const string k_ELabelMapLocator = "MapLocator";

		// Token: 0x04008D62 RID: 36194
		private const string k_ELabelOKButton = "OKButton";

		// Token: 0x04008D63 RID: 36195
		private const string k_ELabelButton1 = "Button1";

		// Token: 0x04008D64 RID: 36196
		private const string k_ELabelButton2 = "Button2";

		// Token: 0x04008D65 RID: 36197
		private const string MAP_DATA_PATH = "Prefabs/LoginBonus/Maps/LoginBonusMapData";

		// Token: 0x04008D66 RID: 36198
		private static bool c_IsTextLoadedExternal;

		// Token: 0x04008D67 RID: 36199
		private LoginBonusViewController.Mode m_Mode;

		// Token: 0x04008D68 RID: 36200
		private int m_LaunchId;

		// Token: 0x04008D69 RID: 36201
		private GameObject m_MapPref;

		// Token: 0x04008D6A RID: 36202
		private LoginBonusMapWidet m_MapWidget;

		// Token: 0x04008D6B RID: 36203
		private Dictionary<string, object> m_SourceData;

		// Token: 0x04008D6C RID: 36204
		private LoginBonusViewController.Button m_OKButton;

		// Token: 0x04008D6D RID: 36205
		private LoginBonusViewController.Button m_Button1;

		// Token: 0x04008D6E RID: 36206
		private LoginBonusViewController.Button m_Button2;

		// Token: 0x04008D6F RID: 36207
		private Action m_Callback;

		// Token: 0x04008D70 RID: 36208
		private AssetLinkContainer _mapAssetInfo;

		// Token: 0x02000AA9 RID: 2729
		public enum Mode
		{
			// Token: 0x04008D72 RID: 36210
			View,
			// Token: 0x04008D73 RID: 36211
			Obtain
		}

		// Token: 0x02000AAA RID: 2730
		private class Button
		{
			// Token: 0x17000770 RID: 1904
			// (set) Token: 0x06004F7B RID: 20347 RVA: 0x0000216D File Offset: 0x0000036D
			internal UnityAction onSelected
			{
				set
				{
				}
			}

			// Token: 0x06004F7C RID: 20348 RVA: 0x00002739 File Offset: 0x00000939
			internal Button(SelectionButton btn, bool enable = false)
			{
			}

			// Token: 0x06004F7D RID: 20349 RVA: 0x0000216D File Offset: 0x0000036D
			internal void SetData(Dictionary<string, object> src)
			{
			}

			// Token: 0x06004F7E RID: 20350 RVA: 0x0000216D File Offset: 0x0000036D
			internal void Activate()
			{
			}

			// Token: 0x04008D74 RID: 36212
			private SelectionButton _btn;

			// Token: 0x04008D75 RID: 36213
			private bool _enable;
		}
	}
}
