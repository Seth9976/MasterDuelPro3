using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Enquete
{
	// Token: 0x02000C18 RID: 3096
	public class EnqueteViewController : BaseMenuViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06005852 RID: 22610 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06005853 RID: 22611 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenEnquete(int enqueteId, Action<Dictionary<string, object>> callback = null, bool openThanksOnFinish = true, bool closeOnFinish = true, Action onLaunchCallback = null, bool openOnHome = false)
		{
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x0000216D File Offset: 0x0000036D
		private static void InnerOpenEnquete(int enqueteId, Action<Dictionary<string, object>> callback = null, bool openThanksOnFinish = true, bool closeOnFinish = true, Action onLaunchCallback = null, bool openOnHome = false)
		{
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x0000216D File Offset: 0x0000036D
		public static void PushFirstEnquete(ViewControllerManager manager, Action<Dictionary<string, object>> callback, bool closeOnFinish = true)
		{
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ProgressResultSequence(Dictionary<string, object> result, Action<Dictionary<string, object>> callback = null, bool openReward = true, bool openThanksOnFinish = true, bool closeOnFinish = true)
		{
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnFailedAnswers(Dictionary<string, object> result, Action<Dictionary<string, object>> callback = null, bool closeOnFinish = true)
		{
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x0000216D File Offset: 0x0000036D
		private void ImportJsonStr(string jsonStr)
		{
		}

		// Token: 0x0600585C RID: 22620 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600585F RID: 22623 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePage(int page, bool forceUpdate = false)
		{
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateNextButton()
		{
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator ConfirmInputValues(Action<bool> onComplete)
		{
			return null;
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x0000216D File Offset: 0x0000036D
		private void ApplyViewMode()
		{
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToDecide()
		{
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryFocusScroll(MonoBehaviour target)
		{
			return false;
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TryFocusScroll(RectTransform target)
		{
			return false;
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputUp()
		{
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputDown()
		{
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputLeft()
		{
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputRight()
		{
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBackPage()
		{
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickNextPage()
		{
		}

		// Token: 0x0600586C RID: 22636 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickBackEdit()
		{
		}

		// Token: 0x0600586D RID: 22637 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickDecide()
		{
		}

		// Token: 0x0600586E RID: 22638 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x040094A7 RID: 38055
		public const string k_BoardImageSPath = "Images/Enquete/EnqueteBoard_S";

		// Token: 0x040094A8 RID: 38056
		public const string k_BoardImageMPath = "Images/Enquete/EnqueteBoard_M";

		// Token: 0x040094A9 RID: 38057
		private const string k_ArgKeySourceJsonLinkerLabel = "sourceJsonLabel";

		// Token: 0x040094AA RID: 38058
		private const string k_ArgKeySourceJson = "sourceJson";

		// Token: 0x040094AB RID: 38059
		private const string k_ArgKeyCallback = "callback";

		// Token: 0x040094AC RID: 38060
		private const string k_ArgKeyCloseOnFinish = "closeOnFinish";

		// Token: 0x040094AD RID: 38061
		private const string k_ArgKeyDispHeader = "dispHeader";

		// Token: 0x040094AE RID: 38062
		private Selector m_FooterSelector;

		// Token: 0x040094AF RID: 38063
		private RootContext m_RootContext;

		// Token: 0x040094B0 RID: 38064
		private List<string> m_LoadTextGroups;

		// Token: 0x040094B1 RID: 38065
		private RectTransform m_SheetTemplate;

		// Token: 0x040094B2 RID: 38066
		private SheetWidgetFactory m_SheetWidgetFactory;

		// Token: 0x040094B3 RID: 38067
		private int m_PageLength;

		// Token: 0x040094B4 RID: 38068
		private int m_CurrentPage;

		// Token: 0x040094B5 RID: 38069
		private ExtendedScrollRect m_ScrollRect;

		// Token: 0x040094B6 RID: 38070
		private TMP_Text m_PageText;

		// Token: 0x040094B7 RID: 38071
		private SelectionButton m_BackPageButton;

		// Token: 0x040094B8 RID: 38072
		private SelectionButton m_NextPageButton;

		// Token: 0x040094B9 RID: 38073
		private SelectionButton m_BackEditButton;

		// Token: 0x040094BA RID: 38074
		private SelectionButton m_DecideButton;

		// Token: 0x040094BB RID: 38075
		private SheetWidget[] m_SheetWidgets;

		// Token: 0x040094BC RID: 38076
		private SheetWidget m_FreeWordConfirmSheetWidget;

		// Token: 0x040094BD RID: 38077
		private Coroutine m_ConfirmInputValuesRoutine;

		// Token: 0x040094BE RID: 38078
		private Dictionary<string, object> m_InputValues;

		// Token: 0x040094BF RID: 38079
		private List<string> m_ReplacedInputKeys;

		// Token: 0x040094C0 RID: 38080
		private EnqueteViewController.Mode m_Mode;

		// Token: 0x040094C1 RID: 38081
		private GameObject m_BackKeyShortcut;

		// Token: 0x040094C2 RID: 38082
		private Selector m_ScrollSelector;

		// Token: 0x02000C19 RID: 3097
		private enum Mode
		{
			// Token: 0x040094C4 RID: 38084
			Input,
			// Token: 0x040094C5 RID: 38085
			FreeWordConfirm,
			// Token: 0x040094C6 RID: 38086
			Decided
		}
	}
}
