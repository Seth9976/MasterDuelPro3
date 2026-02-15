using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000A45 RID: 2629
	public class ActionSheetViewController : SelectDialogViewControllerBase<string, ActionSheetViewController.EntryData[], int>, IBokeSupported
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06004C9B RID: 19611 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ActionSheetViewController.EntryData[] arg2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Action<int> arg3
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06004C9D RID: 19613 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject embedObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string title, IReadOnlyList<string> entrys)
		{
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string title, IReadOnlyList<string> entrys, Action<int> callback)
		{
		}

		// Token: 0x06004CA0 RID: 19616 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string title, IReadOnlyList<string> entrys, Action<int> callback, Action onCancel)
		{
		}

		// Token: 0x06004CA1 RID: 19617 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string title, IReadOnlyList<string> entrys, int destructiveLength = 0, Action<int> callback = null, Action onCancel = null)
		{
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string title, ActionSheetViewController.EntryData[] entrys, Action<int> callback = null, Action onCancel = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenCustomSheet(string title, ActionSheetViewController.EntryData[] entrys, Dictionary<ActionSheetViewController.ButtonStyle, GameObject> customButtonMap, Action<ActionSheetViewController.ButtonStyle, ActionSheetViewController.EntryButtonWidget> customOnCreateCallback, Action<ActionSheetViewController.ButtonStyle, ActionSheetViewController.EntryButtonWidget, ActionSheetViewController.EntryData, int> customOnUpdateButtonCallback, Action<int> callback = null, Action onCancel = null, string message = null, GameObject embedObject = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenCustomToggleGroup(string title, ActionSheetViewController.EntryData[] entrys, string additionalFooterText, Action<bool[]> callback = null, Action onCancel = null, string message = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenMessage(string title, string message, IReadOnlyList<string> entrys, Action<int> callback = null, Action onCancel = null)
		{
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenMessage(string title, string message, IReadOnlyList<string> entrys, int destructiveLength = 0, Action<int> callback = null, Action onCancel = null)
		{
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenMessage(string title, string message, ActionSheetViewController.EntryData[] entrys, Action<int> callback = null, Action onCancel = null)
		{
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenWithEmbedObject(string title, GameObject embedObject, IReadOnlyList<string> entrys, int destructiveLength = 0, Action<int> callback = null, Action onCancel = null)
		{
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenWithEmbedObject(string title, GameObject embedObject, ActionSheetViewController.EntryData[] entrys, Action<int> callback = null, Action onCancel = null, Dictionary<string, object> args = null)
		{
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x000F4A5E File Offset: 0x000F2C5E
		public static bool TryGetLaunchedInstance(out ActionSheetViewController instance)
		{
			instance = null;
			return false;
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0000216A File Offset: 0x0000036A
		public ActionSheetViewController.EntryData GetEntry(string label)
		{
			return null;
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateEntries()
		{
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreateButtonEntity(GameObject buttonEntity)
		{
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateButtonEntity(GameObject buttonEntity, int idx)
		{
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CustomButtonEntityInnerTransition(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickButtonEntity(ActionSheetViewController.EntryButtonWidget buttonWidget)
		{
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCancel()
		{
		}

		// Token: 0x04008A5C RID: 35420
		private const string k_PrefPath = "Common/ActionSheet/ActionSheet";

		// Token: 0x04008A5D RID: 35421
		private const string k_ArgKeyMessage = "message";

		// Token: 0x04008A5E RID: 35422
		private const string k_ArgKeyEmbedObject = "embedObject";

		// Token: 0x04008A5F RID: 35423
		public const string k_ArgKeyOnCancel = "onCancelCallback";

		// Token: 0x04008A60 RID: 35424
		private const string k_ArgKeyCustomButtonMap = "customButtonMap";

		// Token: 0x04008A61 RID: 35425
		private const string k_ArgKeyCustomOnCreateButtonCallback = "customOnCreateButtonCallback";

		// Token: 0x04008A62 RID: 35426
		private const string k_ArgKeyCustomOnUpdateButtonCallback = "customOnUpdateButtonCallback";

		// Token: 0x04008A63 RID: 35427
		private const string k_ArgKeyAdditionalFooterOnClick = "additionalFooterOnClick";

		// Token: 0x04008A64 RID: 35428
		private const string k_ArgKeyAdditionalFooterText = "additionalFooterText";

		// Token: 0x04008A65 RID: 35429
		private const string k_ArgKeyOffCloseButton = "offCloseButton";

		// Token: 0x04008A66 RID: 35430
		private readonly string k_ELabelCloseButton;

		// Token: 0x04008A67 RID: 35431
		private readonly string k_ELabelTitleText;

		// Token: 0x04008A68 RID: 35432
		private readonly string k_ELabelMessageArea;

		// Token: 0x04008A69 RID: 35433
		private readonly string k_ELabelEmbedObjectArea;

		// Token: 0x04008A6A RID: 35434
		private readonly string k_ELabelEntryButtonsScrollView;

		// Token: 0x04008A6B RID: 35435
		private readonly string k_ELabelCancelButton;

		// Token: 0x04008A6C RID: 35436
		private readonly string k_ELabelText;

		// Token: 0x04008A6D RID: 35437
		private readonly string k_ELabelShortcutIcon;

		// Token: 0x04008A6E RID: 35438
		private readonly string k_SEFooterDecide;

		// Token: 0x04008A6F RID: 35439
		private ActionSheetViewController.EntryButtonsScrollWidget m_EntryButtonsScrollWidget;

		// Token: 0x04008A70 RID: 35440
		private Dictionary<GameObject, ActionSheetViewController.EntryButtonWidget> m_EntryButtonWidgetMap;

		// Token: 0x04008A71 RID: 35441
		private List<ActionSheetViewController.EntryData> m_EntryButtonDatas;

		// Token: 0x04008A72 RID: 35442
		private List<ActionSheetViewController.EntryData> m_DisplayEntryButtonDatas;

		// Token: 0x04008A73 RID: 35443
		private List<int> m_EntryButtonTemplateIdxs;

		// Token: 0x04008A74 RID: 35444
		private Action<ActionSheetViewController.ButtonStyle, ActionSheetViewController.EntryButtonWidget> m_CustomOnCreateCallback;

		// Token: 0x04008A75 RID: 35445
		private Action<ActionSheetViewController.ButtonStyle, ActionSheetViewController.EntryButtonWidget, ActionSheetViewController.EntryData, int> m_CustomOnUpdateButtonCallback;

		// Token: 0x02000A46 RID: 2630
		public enum ButtonStyle
		{
			// Token: 0x04008A77 RID: 35447
			Positive,
			// Token: 0x04008A78 RID: 35448
			Destructive,
			// Token: 0x04008A79 RID: 35449
			Disable,
			// Token: 0x04008A7A RID: 35450
			Toggle
		}

		// Token: 0x02000A47 RID: 2631
		[Serializable]
		public class EntryData
		{
			// Token: 0x06004CB6 RID: 19638 RVA: 0x00002739 File Offset: 0x00000939
			public EntryData()
			{
			}

			// Token: 0x06004CB7 RID: 19639 RVA: 0x00002739 File Offset: 0x00000939
			public EntryData(string text)
			{
			}

			// Token: 0x06004CB8 RID: 19640 RVA: 0x00002739 File Offset: 0x00000939
			public EntryData(string text, ActionSheetViewController.ButtonStyle buttonStyle)
			{
			}

			// Token: 0x06004CB9 RID: 19641 RVA: 0x00002739 File Offset: 0x00000939
			public EntryData(string text, ActionSheetViewController.ButtonStyle buttonStyle, string overrideSeClick)
			{
			}

			// Token: 0x06004CBA RID: 19642 RVA: 0x00002739 File Offset: 0x00000939
			public EntryData(string text, bool interactable, ActionSheetViewController.ButtonStyle buttonStyle)
			{
			}

			// Token: 0x06004CBB RID: 19643 RVA: 0x00002739 File Offset: 0x00000939
			public EntryData(string text, bool interactable = true, ActionSheetViewController.ButtonStyle buttonStyle = ActionSheetViewController.ButtonStyle.Positive, string overrideSeClick = null, bool active = true, string label = null, bool useCheckBox = false, bool isOn = false, string imagePath = null, bool badge = false)
			{
			}

			// Token: 0x06004CBC RID: 19644 RVA: 0x0000216A File Offset: 0x0000036A
			public static ActionSheetViewController.EntryData[] CreateEntrys(IReadOnlyList<string> entrys, int destructiveLength = 0)
			{
				return null;
			}

			// Token: 0x04008A7B RID: 35451
			public string text;

			// Token: 0x04008A7C RID: 35452
			public ActionSheetViewController.ButtonStyle buttonStyle;

			// Token: 0x04008A7D RID: 35453
			public bool interactable;

			// Token: 0x04008A7E RID: 35454
			public string overrideSeClick;

			// Token: 0x04008A7F RID: 35455
			public bool active;

			// Token: 0x04008A80 RID: 35456
			public string label;

			// Token: 0x04008A81 RID: 35457
			public bool useCheckBox;

			// Token: 0x04008A82 RID: 35458
			public bool isOn;

			// Token: 0x04008A83 RID: 35459
			public string imagePath;

			// Token: 0x04008A84 RID: 35460
			public bool badge;
		}

		// Token: 0x02000A48 RID: 2632
		public class EntryButtonsScrollWidget : ElementWidgetBase
		{
			// Token: 0x06004CBD RID: 19645 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public EntryButtonsScrollWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x04008A85 RID: 35461
			public readonly InfinityScrollView scrollView;
		}

		// Token: 0x02000A49 RID: 2633
		public class EntryButtonWidget : ElementWidgetBase
		{
			// Token: 0x06004CBE RID: 19646 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public EntryButtonWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x06004CBF RID: 19647 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnClick()
			{
			}

			// Token: 0x04008A86 RID: 35462
			public readonly string k_ELabelText;

			// Token: 0x04008A87 RID: 35463
			public readonly string k_ELabelToggle;

			// Token: 0x04008A88 RID: 35464
			public readonly string k_ELabelImage;

			// Token: 0x04008A89 RID: 35465
			public readonly string k_ELabelBadge;

			// Token: 0x04008A8A RID: 35466
			public readonly SelectionButton button;

			// Token: 0x04008A8B RID: 35467
			public readonly TMP_Text text;

			// Token: 0x04008A8C RID: 35468
			public readonly GameObject badge;

			// Token: 0x04008A8D RID: 35469
			public readonly string defaultSoundLabelClick;

			// Token: 0x04008A8E RID: 35470
			public Action<ActionSheetViewController.EntryButtonWidget> onClickCallback;
		}

		// Token: 0x02000A4A RID: 2634
		public class EntryButtonWidgetToggle : ActionSheetViewController.EntryButtonWidget
		{
			// Token: 0x06004CC0 RID: 19648 RVA: 0x000F4A6C File Offset: 0x000F2C6C
			public EntryButtonWidgetToggle(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x06004CC1 RID: 19649 RVA: 0x0000216D File Offset: 0x0000036D
			public void OnUpdate(ActionSheetViewController.EntryData entryData)
			{
			}

			// Token: 0x04008A8F RID: 35471
			public readonly string k_ELabelImageOff;

			// Token: 0x04008A90 RID: 35472
			public readonly string k_ELabelImageOn;

			// Token: 0x04008A91 RID: 35473
			public readonly string k_ELabelOff;

			// Token: 0x04008A92 RID: 35474
			public readonly string k_ELabelOn;

			// Token: 0x04008A93 RID: 35475
			public readonly string k_ELabelTextOff;

			// Token: 0x04008A94 RID: 35476
			public readonly string k_ELabelTextOn;

			// Token: 0x04008A95 RID: 35477
			public readonly string k_ELabelToggleOff;

			// Token: 0x04008A96 RID: 35478
			public readonly string k_ELabelToggleOn;

			// Token: 0x04008A97 RID: 35479
			private readonly string k_SEToggleOn;

			// Token: 0x04008A98 RID: 35480
			private readonly string k_SEToggleOff;
		}
	}
}
