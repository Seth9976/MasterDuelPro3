using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.LocalizedFont;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x02000692 RID: 1682
	public class InputFieldWidget : ElementWidgetBehaviourBase<InputFieldWidget>, ILocalizedFontOwner
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool useTMP
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060034CE RID: 13518 RVA: 0x0000216D File Offset: 0x0000036D
		public bool interactable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060034D0 RID: 13520 RVA: 0x0000216D File Offset: 0x0000036D
		public bool submitOnClickOuter
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060034D2 RID: 13522 RVA: 0x0000216D File Offset: 0x0000036D
		public LocalizedFontSettingsBase.FontType localizedFontType
		{
			get
			{
				return LocalizedFontSettingsBase.FontType.Other;
			}
			set
			{
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060034D4 RID: 13524 RVA: 0x0000216D File Offset: 0x0000036D
		public int localizedFontMaterialIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x0000216A File Offset: 0x0000036A
		public InputFieldWrapper inputField
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedInputField uInputField
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x0000216A File Offset: 0x0000036A
		private TMP_InputField TMPInputField
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton inputButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060034D9 RID: 13529 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject editingCover
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060034DA RID: 13530 RVA: 0x0000216A File Offset: 0x0000036A
		public Button clearButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform mask
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x0000216A File Offset: 0x0000036A
		public DeviceIcon clearButtonRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x0000216A File Offset: 0x0000036A
		public DeviceIcon shortcutIconGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x0000216A File Offset: 0x0000036A
		public Selector inactivateButtonSelector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton inactivateButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060034E1 RID: 13537 RVA: 0x0000216D File Offset: 0x0000036D
		public string text
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Start()
		{
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClick()
		{
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickClear()
		{
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnValueChanged(string input)
		{
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEndEdit(string res)
		{
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateInputField()
		{
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x0000216D File Offset: 0x0000036D
		public void InactivateInputField()
		{
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateClearButton()
		{
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResizeClearButtonWidth()
		{
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayToEditOn()
		{
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayToEditOff()
		{
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetText(string text)
		{
		}

		// Token: 0x04003024 RID: 12324
		private const string k_ELabelInputField = "InputField";

		// Token: 0x04003025 RID: 12325
		private const string k_ELabelTMPInputField = "InputFieldTMP";

		// Token: 0x04003026 RID: 12326
		private const string k_ELabelInputButton = "InputButton";

		// Token: 0x04003027 RID: 12327
		private const string k_ELabelClearButton = "ClearButton";

		// Token: 0x04003028 RID: 12328
		private const string k_ELabelClearButtonRoot = "ClearButtonRoot";

		// Token: 0x04003029 RID: 12329
		private const string k_ELabelEditingCover = "EditingCover";

		// Token: 0x0400302A RID: 12330
		private const string k_ELabelEditingMask = "Mask";

		// Token: 0x0400302B RID: 12331
		private const string k_ELabelShortcutIconGroup = "ShortcutIconGroup";

		// Token: 0x0400302C RID: 12332
		private const string k_ELabelInactiveButtonRoot = "InactivateButtonRoot";

		// Token: 0x0400302D RID: 12333
		private const string k_ELabelInactiveButton = "InactivateButton";

		// Token: 0x0400302E RID: 12334
		private const string k_TweenToEditOn = "ToEditOn";

		// Token: 0x0400302F RID: 12335
		private const string k_TweenToEditOff = "ToEditOff";

		// Token: 0x04003030 RID: 12336
		private InputFieldWrapper inputFieldWrapper;

		// Token: 0x04003031 RID: 12337
		private ExtendedInputField m_InputFieldCache;

		// Token: 0x04003032 RID: 12338
		private TMP_InputField m_TMPInputFieldCache;

		// Token: 0x04003033 RID: 12339
		private SelectionButton m_InputButtonCache;

		// Token: 0x04003034 RID: 12340
		private Button m_ClearButtonCache;

		// Token: 0x04003035 RID: 12341
		private GameObject m_EditingCoverCache;

		// Token: 0x04003036 RID: 12342
		private RectTransform m_MaskCache;

		// Token: 0x04003037 RID: 12343
		private DeviceIcon m_ClearButtonRootCache;

		// Token: 0x04003038 RID: 12344
		private DeviceIcon m_ShortcutIconGroup;

		// Token: 0x04003039 RID: 12345
		private Selector m_InactivateButtonSelectorCache;

		// Token: 0x0400303A RID: 12346
		private SelectionButton m_InactivateButtonCache;

		// Token: 0x0400303B RID: 12347
		private bool m_IsEditEndFrame;

		// Token: 0x0400303C RID: 12348
		private bool m_requestSubmit;

		// Token: 0x0400303D RID: 12349
		private bool m_inputFieldActivated;

		// Token: 0x0400303E RID: 12350
		[SerializeField]
		private bool _submitOnClickOuter;

		// Token: 0x0400303F RID: 12351
		[SerializeField]
		public TMP_FontAsset fontAsset;

		// Token: 0x04003040 RID: 12352
		[SerializeField]
		private LocalizedFontSettingsBase.FontType m_localizedFontType;

		// Token: 0x04003041 RID: 12353
		[SerializeField]
		private int m_localizedFontMaterialIndex;

		// Token: 0x04003042 RID: 12354
		public readonly InputField.SubmitEvent onSubmitEdit;

		// Token: 0x04003043 RID: 12355
		public readonly InputField.OnChangeEvent onFilterdValueChanged;

		// Token: 0x04003044 RID: 12356
		public readonly UnityEvent onBeginEdit;

		// Token: 0x04003045 RID: 12357
		public readonly UnityEvent onEndEdit;
	}
}
