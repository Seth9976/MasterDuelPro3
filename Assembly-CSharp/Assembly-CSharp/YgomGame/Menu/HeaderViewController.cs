using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Menu
{
	// Token: 0x02000A7E RID: 2686
	public class HeaderViewController : ViewController
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06004E97 RID: 20119 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004E98 RID: 20120 RVA: 0x0000216D File Offset: 0x0000036D
		public static HeaderViewController instance
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InitHeader()
		{
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStack(ViewControllerManager vcm, ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAnimBtnHideCallback(string label)
		{
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnBackButton()
		{
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCommonButton(HeaderViewController.IsDispHeader isDispHeader, Action onClick)
		{
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsReadyClickAction(HeaderViewController.IsDispHeader isDispHeader)
		{
			return false;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDirty()
		{
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideButton()
		{
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowButtonTemp()
		{
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideButtonTemp()
		{
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInteractableAllContents(bool interactable)
		{
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetShortCutBackButton(bool isSet)
		{
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x0000216D File Offset: 0x0000036D
		public void SwapBeforeParent(ViewController vc, bool isEntry)
		{
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject GetTopFrontGameObject(HeaderViewController.IsDispHeader isDispHeader)
		{
			return null;
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResetOption()
		{
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x0000216D File Offset: 0x0000036D
		private void StateChange(ViewController vc, bool forceHide = false)
		{
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x0000216D File Offset: 0x0000036D
		private void doHome()
		{
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSupportedView(ViewController vc)
		{
			return false;
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetIsDisp(HeaderViewController.IsDispHeader disp, bool flag)
		{
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDispPart(HeaderViewController.Part part)
		{
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsPlaying()
		{
			return false;
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDispAllContents(bool flag)
		{
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsDispAnyContent(HeaderViewController.Part part = HeaderViewController.Part.ALL)
		{
			return false;
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x0000216D File Offset: 0x0000036D
		private void StateNotificator(object mes)
		{
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x0000216D File Offset: 0x0000036D
		private void NotifyTransition(ViewController.TransitionType tt, ViewController vc, ViewController preVc)
		{
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnBackEvent(ViewControllerManager vcm)
		{
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeSelector()
		{
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinalizeSelector()
		{
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSelectorEnabled(bool enabled)
		{
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x0000216A File Offset: 0x0000036A
		private ViewControllerManager GetContentManager()
		{
			return null;
		}

		// Token: 0x04008C5F RID: 35935
		[SerializeField]
		private GameObject prefabUI;

		// Token: 0x04008C60 RID: 35936
		private ElementObjectManager ui;

		// Token: 0x04008C61 RID: 35937
		private readonly string ROOT_LABEL;

		// Token: 0x04008C62 RID: 35938
		private readonly string ROOT_TOP_LABEL;

		// Token: 0x04008C63 RID: 35939
		private readonly string BTN_BACK_LABEL;

		// Token: 0x04008C64 RID: 35940
		private readonly string BTN_CONFIG_LABEL;

		// Token: 0x04008C65 RID: 35941
		private readonly string BTN_FRIEND_LABEL;

		// Token: 0x04008C66 RID: 35942
		private readonly string BTN_NOTICE_LABEL;

		// Token: 0x04008C67 RID: 35943
		private readonly string BTN_PRESENT_LABEL;

		// Token: 0x04008C68 RID: 35944
		private readonly string BTN_MISSION_LABEL;

		// Token: 0x04008C69 RID: 35945
		private readonly string BTN_GEM_LABEL;

		// Token: 0x04008C6A RID: 35946
		private readonly string BTN_DUELLIVE;

		// Token: 0x04008C6B RID: 35947
		private readonly string TXT_GEM_LABEL;

		// Token: 0x04008C6C RID: 35948
		private readonly string TXT_LABEL;

		// Token: 0x04008C6D RID: 35949
		private readonly string IMG_BORDER_LABEL;

		// Token: 0x04008C6E RID: 35950
		private readonly string ROOT_TWEEN_LABEL;

		// Token: 0x04008C6F RID: 35951
		private readonly string STATE_NOTICE_PATH;

		// Token: 0x04008C70 RID: 35952
		private ElementObjectManager topEOM;

		// Token: 0x04008C71 RID: 35953
		private Selector topSelector;

		// Token: 0x04008C72 RID: 35954
		private GameObject root;

		// Token: 0x04008C73 RID: 35955
		private GameObject topRoot;

		// Token: 0x04008C74 RID: 35956
		private HeaderViewController.FrontGroup topFronts;

		// Token: 0x04008C75 RID: 35957
		private bool isDispTop;

		// Token: 0x04008C76 RID: 35958
		private ExtendedTextMeshProUGUI gemButtonText;

		// Token: 0x04008C77 RID: 35959
		private ExtendedTextMeshProUGUI gemText;

		// Token: 0x04008C78 RID: 35960
		private ViewControllerManager content;

		// Token: 0x04008C79 RID: 35961
		private ViewController crntViewController;

		// Token: 0x04008C7A RID: 35962
		private bool isDirty;

		// Token: 0x04008C7B RID: 35963
		private bool shouldResetShortCutBack;

		// Token: 0x04008C7C RID: 35964
		private HeaderViewController.IsDispHeader isDisp;

		// Token: 0x02000A7F RID: 2687
		[Flags]
		public enum IsDispHeader
		{
			// Token: 0x04008C7E RID: 35966
			BACK = 1,
			// Token: 0x04008C7F RID: 35967
			GEM_QUANTITY = 2,
			// Token: 0x04008C80 RID: 35968
			FRIEND = 4,
			// Token: 0x04008C81 RID: 35969
			CONFIG = 8,
			// Token: 0x04008C82 RID: 35970
			PRESENT = 16,
			// Token: 0x04008C83 RID: 35971
			NOTICE = 32,
			// Token: 0x04008C84 RID: 35972
			MISSION = 64,
			// Token: 0x04008C85 RID: 35973
			GEM_QUANTITY_TEXT = 128,
			// Token: 0x04008C86 RID: 35974
			BORDER = 256,
			// Token: 0x04008C87 RID: 35975
			DUELLIVE = 512
		}

		// Token: 0x02000A80 RID: 2688
		public enum Part
		{
			// Token: 0x04008C89 RID: 35977
			ALL,
			// Token: 0x04008C8A RID: 35978
			TOP
		}

		// Token: 0x02000A81 RID: 2689
		private class FrontGroup
		{
			// Token: 0x06004EBE RID: 20158 RVA: 0x00002739 File Offset: 0x00000939
			public FrontGroup(HeaderViewController.Part part)
			{
			}

			// Token: 0x06004EBF RID: 20159 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetFront(HeaderViewController.IsDispHeader isDispHeader, GameObject gameObject)
			{
			}

			// Token: 0x06004EC0 RID: 20160 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject GetFront(HeaderViewController.IsDispHeader isDispHeader)
			{
				return null;
			}

			// Token: 0x06004EC1 RID: 20161 RVA: 0x0000216D File Offset: 0x0000036D
			public void InitTween()
			{
			}

			// Token: 0x04008C8B RID: 35979
			private readonly HeaderViewController.Part part;

			// Token: 0x04008C8C RID: 35980
			private Dictionary<HeaderViewController.IsDispHeader, GameObject> fronts;
		}
	}
}
