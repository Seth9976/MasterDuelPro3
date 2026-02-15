using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200004B RID: 75
	[AddComponentMenu("UI/TextMeshPro - Input Field", 11)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/index.html")]
	public class TMP_InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICancelHandler, ICanvasElement, ILayoutElement, IScrollHandler
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009DF3 File Offset: 0x00007FF3
		private BaseInput inputSystem
		{
			get
			{
				if (EventSystem.current && EventSystem.current.currentInputModule)
				{
					return EventSystem.current.currentInputModule.input;
				}
				return null;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00009E23 File Offset: 0x00008023
		private string compositionString
		{
			get
			{
				if (!(this.inputSystem != null))
				{
					return Input.compositionString;
				}
				return this.inputSystem.compositionString;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009E44 File Offset: 0x00008044
		private int compositionLength
		{
			get
			{
				if (this.m_ReadOnly)
				{
					return 0;
				}
				return this.compositionString.Length;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009E5C File Offset: 0x0000805C
		protected TMP_InputField()
		{
			this.SetTextComponentWrapMode();
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009F95 File Offset: 0x00008195
		protected Mesh mesh
		{
			get
			{
				if (this.m_Mesh == null)
				{
					this.m_Mesh = new Mesh();
				}
				return this.m_Mesh;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00009FBF File Offset: 0x000081BF
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00009FB6 File Offset: 0x000081B6
		public virtual bool shouldActivateOnSelect
		{
			get
			{
				return this.m_ShouldActivateOnSelect && Application.platform != RuntimePlatform.tvOS;
			}
			set
			{
				this.m_ShouldActivateOnSelect = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00009FD8 File Offset: 0x000081D8
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000A010 File Offset: 0x00008210
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.Android)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android)
					{
						return true;
					}
				}
				else if (platform != RuntimePlatform.WebGLPlayer && platform != RuntimePlatform.tvOS)
				{
					return true;
				}
				return this.m_HideMobileInput;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.Android)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android)
					{
						goto IL_002E;
					}
				}
				else if (platform != RuntimePlatform.WebGLPlayer && platform != RuntimePlatform.tvOS)
				{
					goto IL_002E;
				}
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
				return;
				IL_002E:
				this.m_HideMobileInput = true;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A054 File Offset: 0x00008254
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000A0A8 File Offset: 0x000082A8
		public bool shouldHideSoftKeyboard
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.MetroPlayerARM)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform - RuntimePlatform.WebGLPlayer > 3)
					{
						return true;
					}
				}
				else if (platform <= RuntimePlatform.Switch)
				{
					if (platform != RuntimePlatform.PS4 && platform - RuntimePlatform.tvOS > 1)
					{
						return true;
					}
				}
				else if (platform - RuntimePlatform.GameCoreXboxSeries > 2 && platform != RuntimePlatform.VisionOS)
				{
					return true;
				}
				return this.m_HideSoftKeyboard;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.MetroPlayerARM)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform - RuntimePlatform.WebGLPlayer > 3)
					{
						goto IL_004B;
					}
				}
				else if (platform <= RuntimePlatform.Switch)
				{
					if (platform != RuntimePlatform.PS4 && platform - RuntimePlatform.tvOS > 1)
					{
						goto IL_004B;
					}
				}
				else if (platform - RuntimePlatform.GameCoreXboxSeries > 2 && platform != RuntimePlatform.VisionOS)
				{
					goto IL_004B;
				}
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideSoftKeyboard, value);
				goto IL_0052;
				IL_004B:
				this.m_HideSoftKeyboard = true;
				IL_0052:
				if (this.m_HideSoftKeyboard && this.m_SoftKeyboard != null && TouchScreenKeyboard.isSupported && this.m_SoftKeyboard.active)
				{
					this.m_SoftKeyboard.active = false;
					this.m_SoftKeyboard = null;
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A140 File Offset: 0x00008340
		private bool isKeyboardUsingEvents()
		{
			RuntimePlatform platform = Application.platform;
			if (platform > RuntimePlatform.WebGLPlayer)
			{
				if (platform != RuntimePlatform.PS4)
				{
					switch (platform)
					{
					case RuntimePlatform.tvOS:
						goto IL_0061;
					case RuntimePlatform.Switch:
					case RuntimePlatform.GameCoreXboxSeries:
					case RuntimePlatform.GameCoreXboxOne:
					case RuntimePlatform.PS5:
						break;
					case RuntimePlatform.Lumin:
					case RuntimePlatform.Stadia:
					case RuntimePlatform.LinuxHeadlessSimulation:
						return true;
					default:
						if (platform != RuntimePlatform.VisionOS)
						{
							return true;
						}
						goto IL_0061;
					}
				}
				return false;
			}
			if (platform != RuntimePlatform.IPhonePlayer)
			{
				if (platform == RuntimePlatform.Android)
				{
					return this.InPlaceEditing() && this.m_HideSoftKeyboard;
				}
				if (platform != RuntimePlatform.WebGLPlayer)
				{
					return true;
				}
				return this.m_SoftKeyboard == null || !this.m_SoftKeyboard.active;
			}
			IL_0061:
			return this.m_HideSoftKeyboard;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A1D1 File Offset: 0x000083D1
		private bool isUWP()
		{
			return Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A1F0 File Offset: 0x000083F0
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.SetText(value, true);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A202 File Offset: 0x00008402
		public void SetTextWithoutNotify(string input)
		{
			this.SetText(input, false);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A20C File Offset: 0x0000840C
		private void SetText(string value, bool sendCallback = true)
		{
			if (this.text == value)
			{
				return;
			}
			if (value == null)
			{
				value = "";
			}
			value = value.Replace("\0", string.Empty);
			this.m_Text = value;
			if (this.m_SoftKeyboard != null)
			{
				this.m_SoftKeyboard.text = this.m_Text;
			}
			if (this.m_StringPosition > this.m_Text.Length)
			{
				this.m_StringPosition = (this.m_StringSelectPosition = this.m_Text.Length);
			}
			else if (this.m_StringSelectPosition > this.m_Text.Length)
			{
				this.m_StringSelectPosition = this.m_Text.Length;
			}
			this.m_forceRectTransformAdjustment = true;
			this.m_IsTextComponentUpdateRequired = true;
			this.UpdateLabel();
			if (sendCallback)
			{
				this.SendOnValueChanged();
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000A2DC File Offset: 0x000084DC
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000A2E4 File Offset: 0x000084E4
		public float caretBlinkRate
		{
			get
			{
				return this.m_CaretBlinkRate;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_CaretBlinkRate, value) && this.m_AllowInput)
				{
					this.SetCaretActive();
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A302 File Offset: 0x00008502
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000A30A File Offset: 0x0000850A
		public int caretWidth
		{
			get
			{
				return this.m_CaretWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_CaretWidth, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A320 File Offset: 0x00008520
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000A328 File Offset: 0x00008528
		public RectTransform textViewport
		{
			get
			{
				return this.m_TextViewport;
			}
			set
			{
				SetPropertyUtility.SetClass<RectTransform>(ref this.m_TextViewport, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000A337 File Offset: 0x00008537
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000A33F File Offset: 0x0000853F
		public TMP_Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_Text>(ref this.m_TextComponent, value))
				{
					this.SetTextComponentWrapMode();
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000A355 File Offset: 0x00008555
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000A35D File Offset: 0x0000855D
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				SetPropertyUtility.SetClass<Graphic>(ref this.m_Placeholder, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000A36C File Offset: 0x0000856C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000A374 File Offset: 0x00008574
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
				SetPropertyUtility.SetClass<Scrollbar>(ref this.m_VerticalScrollbar, value);
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000A3E1 File Offset: 0x000085E1
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000A3E9 File Offset: 0x000085E9
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_ScrollSensitivity, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000A3FF File Offset: 0x000085FF
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000A41B File Offset: 0x0000861B
		public Color caretColor
		{
			get
			{
				if (!this.customCaretColor)
				{
					return this.textComponent.color;
				}
				return this.m_CaretColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_CaretColor, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A431 File Offset: 0x00008631
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000A439 File Offset: 0x00008639
		public bool customCaretColor
		{
			get
			{
				return this.m_CustomCaretColor;
			}
			set
			{
				if (this.m_CustomCaretColor != value)
				{
					this.m_CustomCaretColor = value;
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000A451 File Offset: 0x00008651
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000A459 File Offset: 0x00008659
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_SelectionColor, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000A46F File Offset: 0x0000866F
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000A477 File Offset: 0x00008677
		public TMP_InputField.SubmitEvent onEndEdit
		{
			get
			{
				return this.m_OnEndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SubmitEvent>(ref this.m_OnEndEdit, value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A486 File Offset: 0x00008686
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000A48E File Offset: 0x0000868E
		public TMP_InputField.SubmitEvent onSubmit
		{
			get
			{
				return this.m_OnSubmit;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SubmitEvent>(ref this.m_OnSubmit, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A49D File Offset: 0x0000869D
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000A4A5 File Offset: 0x000086A5
		public TMP_InputField.SelectionEvent onSelect
		{
			get
			{
				return this.m_OnSelect;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SelectionEvent>(ref this.m_OnSelect, value);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000A4B4 File Offset: 0x000086B4
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000A4BC File Offset: 0x000086BC
		public TMP_InputField.SelectionEvent onDeselect
		{
			get
			{
				return this.m_OnDeselect;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SelectionEvent>(ref this.m_OnDeselect, value);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000A4CB File Offset: 0x000086CB
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000A4D3 File Offset: 0x000086D3
		public TMP_InputField.TextSelectionEvent onTextSelection
		{
			get
			{
				return this.m_OnTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TextSelectionEvent>(ref this.m_OnTextSelection, value);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A4E2 File Offset: 0x000086E2
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000A4EA File Offset: 0x000086EA
		public TMP_InputField.TextSelectionEvent onEndTextSelection
		{
			get
			{
				return this.m_OnEndTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TextSelectionEvent>(ref this.m_OnEndTextSelection, value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000A4F9 File Offset: 0x000086F9
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000A501 File Offset: 0x00008701
		public TMP_InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.OnChangeEvent>(ref this.m_OnValueChanged, value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000A510 File Offset: 0x00008710
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000A518 File Offset: 0x00008718
		public TMP_InputField.TouchScreenKeyboardEvent onTouchScreenKeyboardStatusChanged
		{
			get
			{
				return this.m_OnTouchScreenKeyboardStatusChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TouchScreenKeyboardEvent>(ref this.m_OnTouchScreenKeyboardStatusChanged, value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000A527 File Offset: 0x00008727
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000A52F File Offset: 0x0000872F
		public TMP_InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000A53E File Offset: 0x0000873E
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000A546 File Offset: 0x00008746
		public int characterLimit
		{
			get
			{
				return this.m_CharacterLimit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_CharacterLimit, Math.Max(0, value)))
				{
					this.UpdateLabel();
					if (this.m_SoftKeyboard != null)
					{
						this.m_SoftKeyboard.characterLimit = value;
					}
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000A576 File Offset: 0x00008776
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000A57E File Offset: 0x0000877E
		public float pointSize
		{
			get
			{
				return this.m_GlobalPointSize;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_GlobalPointSize, Math.Max(0f, value)))
				{
					this.SetGlobalPointSize(this.m_GlobalPointSize);
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000A5AA File Offset: 0x000087AA
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000A5B2 File Offset: 0x000087B2
		public TMP_FontAsset fontAsset
		{
			get
			{
				return this.m_GlobalFontAsset;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_FontAsset>(ref this.m_GlobalFontAsset, value))
				{
					this.SetGlobalFontAsset(this.m_GlobalFontAsset);
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000A5D4 File Offset: 0x000087D4
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000A5DC File Offset: 0x000087DC
		public bool onFocusSelectAll
		{
			get
			{
				return this.m_OnFocusSelectAll;
			}
			set
			{
				this.m_OnFocusSelectAll = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A5E5 File Offset: 0x000087E5
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000A5ED File Offset: 0x000087ED
		public bool resetOnDeActivation
		{
			get
			{
				return this.m_ResetOnDeActivation;
			}
			set
			{
				this.m_ResetOnDeActivation = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A5F6 File Offset: 0x000087F6
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000A5FE File Offset: 0x000087FE
		public bool keepTextSelectionVisible
		{
			get
			{
				return this.m_KeepTextSelectionVisible;
			}
			set
			{
				this.m_KeepTextSelectionVisible = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A607 File Offset: 0x00008807
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000A60F File Offset: 0x0000880F
		public bool restoreOriginalTextOnEscape
		{
			get
			{
				return this.m_RestoreOriginalTextOnEscape;
			}
			set
			{
				this.m_RestoreOriginalTextOnEscape = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A618 File Offset: 0x00008818
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000A620 File Offset: 0x00008820
		public bool isRichTextEditingAllowed
		{
			get
			{
				return this.m_isRichTextEditingAllowed;
			}
			set
			{
				this.m_isRichTextEditingAllowed = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A629 File Offset: 0x00008829
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000A631 File Offset: 0x00008831
		public TMP_InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A647 File Offset: 0x00008847
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000A64F File Offset: 0x0000884F
		public TMP_InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new TMP_InputField.ContentType[]
					{
						TMP_InputField.ContentType.Standard,
						TMP_InputField.ContentType.Autocorrected
					});
					this.SetTextComponentWrapMode();
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A675 File Offset: 0x00008875
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000A67D File Offset: 0x0000887D
		public int lineLimit
		{
			get
			{
				return this.m_LineLimit;
			}
			set
			{
				if (this.m_LineType == TMP_InputField.LineType.SingleLine)
				{
					this.m_LineLimit = 1;
					return;
				}
				SetPropertyUtility.SetStruct<int>(ref this.m_LineLimit, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A69C File Offset: 0x0000889C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000A6A4 File Offset: 0x000088A4
		public TMP_InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A6BA File Offset: 0x000088BA
		public TouchScreenKeyboard touchScreenKeyboard
		{
			get
			{
				return this.m_SoftKeyboard;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A6C2 File Offset: 0x000088C2
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000A6CA File Offset: 0x000088CA
		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.m_KeyboardType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TouchScreenKeyboardType>(ref this.m_KeyboardType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A6E0 File Offset: 0x000088E0
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000A6E8 File Offset: 0x000088E8
		public TMP_InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A6FE File Offset: 0x000088FE
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000A706 File Offset: 0x00008906
		public TMP_InputValidator inputValidator
		{
			get
			{
				return this.m_InputValidator;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_InputValidator>(ref this.m_InputValidator, value))
				{
					this.SetToCustom(TMP_InputField.CharacterValidation.CustomValidator);
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A71D File Offset: 0x0000891D
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000A725 File Offset: 0x00008925
		public bool readOnly
		{
			get
			{
				return this.m_ReadOnly;
			}
			set
			{
				this.m_ReadOnly = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A72E File Offset: 0x0000892E
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000A736 File Offset: 0x00008936
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
				this.SetTextComponentRichTextMode();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A745 File Offset: 0x00008945
		public bool multiLine
		{
			get
			{
				return this.m_LineType == TMP_InputField.LineType.MultiLineNewline || this.lineType == TMP_InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A75B File Offset: 0x0000895B
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000A763 File Offset: 0x00008963
		public char asteriskChar
		{
			get
			{
				return this.m_AsteriskChar;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<char>(ref this.m_AsteriskChar, value))
				{
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A779 File Offset: 0x00008979
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A781 File Offset: 0x00008981
		protected void ClampStringPos(ref int pos)
		{
			if (pos <= 0)
			{
				pos = 0;
				return;
			}
			if (pos > this.text.Length)
			{
				pos = this.text.Length;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A7A8 File Offset: 0x000089A8
		protected void ClampCaretPos(ref int pos)
		{
			if (pos > this.m_TextComponent.textInfo.characterCount - 1)
			{
				pos = this.m_TextComponent.textInfo.characterCount - 1;
			}
			if (pos <= 0)
			{
				pos = 0;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A7DC File Offset: 0x000089DC
		private int ClampArrayIndex(int index)
		{
			if (index < 0)
			{
				return 0;
			}
			return index;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A7E5 File Offset: 0x000089E5
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000A7F4 File Offset: 0x000089F4
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + this.compositionLength;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampCaretPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000A809 File Offset: 0x00008A09
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000A818 File Offset: 0x00008A18
		protected int stringPositionInternal
		{
			get
			{
				return this.m_StringPosition + this.compositionLength;
			}
			set
			{
				this.m_StringPosition = value;
				this.ClampStringPos(ref this.m_StringPosition);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A82D File Offset: 0x00008A2D
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000A83C File Offset: 0x00008A3C
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionLength;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampCaretPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000A851 File Offset: 0x00008A51
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000A860 File Offset: 0x00008A60
		protected int stringSelectPositionInternal
		{
			get
			{
				return this.m_StringSelectPosition + this.compositionLength;
			}
			set
			{
				this.m_StringSelectPosition = value;
				this.ClampStringPos(ref this.m_StringSelectPosition);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A875 File Offset: 0x00008A75
		private bool hasSelection
		{
			get
			{
				return this.stringPositionInternal != this.stringSelectPositionInternal;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A888 File Offset: 0x00008A88
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000A890 File Offset: 0x00008A90
		public int caretPosition
		{
			get
			{
				return this.caretSelectPositionInternal;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
				this.UpdateStringIndexFromCaretPosition();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000A8AE File Offset: 0x00008AAE
		public int selectionAnchorPosition
		{
			get
			{
				return this.caretPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.caretPositionInternal = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A888 File Offset: 0x00008A88
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000A8C7 File Offset: 0x00008AC7
		public int selectionFocusPosition
		{
			get
			{
				return this.caretSelectPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.caretSelectPositionInternal = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public int stringPosition
		{
			get
			{
				return this.stringSelectPositionInternal;
			}
			set
			{
				this.selectionStringAnchorPosition = value;
				this.selectionStringFocusPosition = value;
				this.UpdateCaretPositionFromStringIndex();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A8FE File Offset: 0x00008AFE
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000A906 File Offset: 0x00008B06
		public int selectionStringAnchorPosition
		{
			get
			{
				return this.stringPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.stringPositionInternal = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000A91F File Offset: 0x00008B1F
		public int selectionStringFocusPosition
		{
			get
			{
				return this.stringSelectPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.stringSelectPositionInternal = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A938 File Offset: 0x00008B38
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			this.m_IsApplePlatform = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX || SystemInfo.operatingSystem.Contains("iOS") || SystemInfo.operatingSystem.Contains("tvOS");
			if (base.GetComponent<ILayoutController>() != null)
			{
				this.m_IsDrivenByLayoutComponents = true;
				this.m_LayoutGroup = base.GetComponent<LayoutGroup>();
			}
			else
			{
				this.m_IsDrivenByLayoutComponents = false;
			}
			if (Application.isPlaying && this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject go = new GameObject("Caret", new Type[] { typeof(TMP_SelectionCaret) });
				go.hideFlags = HideFlags.DontSave;
				go.transform.SetParent(this.m_TextComponent.transform.parent);
				go.transform.SetAsFirstSibling();
				go.layer = base.gameObject.layer;
				this.caretRectTrans = go.GetComponent<RectTransform>();
				this.m_CachedInputRenderer = go.GetComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
				go.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			this.m_RectTransform = base.GetComponent<RectTransform>();
			IScrollHandler[] scrollHandlers = base.GetComponentsInParent<IScrollHandler>();
			if (scrollHandlers.Length > 1)
			{
				this.m_IScrollHandlerParent = scrollHandlers[1] as ScrollRect;
			}
			if (this.m_TextViewport != null)
			{
				this.m_TextViewportRectMask = this.m_TextViewport.GetComponent<RectMask2D>();
				this.UpdateMaskRegions();
			}
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
				this.UpdateLabel();
			}
			this.m_TouchKeyboardAllowsInPlaceEditing = TouchScreenKeyboard.isInPlaceEditingAllowed;
			TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<global::UnityEngine.Object>(this.ON_TEXT_CHANGED));
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000AB78 File Offset: 0x00008D78
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField(false);
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
			}
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.Clear();
			}
			if (this.m_Mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_Mesh);
			}
			this.m_Mesh = null;
			TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<global::UnityEngine.Object>(this.ON_TEXT_CHANGED));
			base.OnDisable();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000AC54 File Offset: 0x00008E54
		private void ON_TEXT_CHANGED(global::UnityEngine.Object obj)
		{
			if (obj == this.m_TextComponent && !this.m_IsStringPositionDirty)
			{
				if (Application.isPlaying && this.compositionLength == 0)
				{
					this.UpdateCaretPositionFromStringIndex();
				}
				if (this.m_VerticalScrollbar)
				{
					this.UpdateScrollbar();
				}
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000AC94 File Offset: 0x00008E94
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while ((this.isFocused || this.m_SelectionStillActive) && this.m_CaretBlinkRate > 0f)
			{
				float blinkPeriod = 1f / this.m_CaretBlinkRate;
				bool blinkState = (Time.unscaledTime - this.m_BlinkStartTime) % blinkPeriod < blinkPeriod / 2f;
				if (this.m_CaretVisible != blinkState)
				{
					this.m_CaretVisible = blinkState;
					if (!this.hasSelection)
					{
						this.MarkGeometryAsDirty();
					}
				}
				yield return null;
			}
			this.m_BlinkCoroutine = null;
			yield break;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000ACA3 File Offset: 0x00008EA3
		private void SetCaretVisible()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_CaretVisible = true;
			this.m_BlinkStartTime = Time.unscaledTime;
			this.SetCaretActive();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000ACC6 File Offset: 0x00008EC6
		private void SetCaretActive()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			if (this.m_CaretBlinkRate > 0f)
			{
				if (this.m_BlinkCoroutine == null)
				{
					this.m_BlinkCoroutine = base.StartCoroutine(this.CaretBlink());
					return;
				}
			}
			else
			{
				this.m_CaretVisible = true;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000AD00 File Offset: 0x00008F00
		protected void OnFocus()
		{
			if (this.m_OnFocusSelectAll)
			{
				this.SelectAll();
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000AD10 File Offset: 0x00008F10
		protected void SelectAll()
		{
			this.m_isSelectAll = true;
			this.stringPositionInternal = this.text.Length;
			this.stringSelectPositionInternal = 0;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000AD34 File Offset: 0x00008F34
		public void MoveTextEnd(bool shift)
		{
			if (this.m_isRichTextEditingAllowed)
			{
				int position = this.text.Length;
				if (shift)
				{
					this.stringSelectPositionInternal = position;
				}
				else
				{
					this.stringPositionInternal = position;
					this.stringSelectPositionInternal = this.stringPositionInternal;
				}
			}
			else
			{
				int position2 = this.m_TextComponent.textInfo.characterCount - 1;
				if (shift)
				{
					this.caretSelectPositionInternal = position2;
					this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(position2);
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = position2);
					this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(position2));
				}
			}
			this.UpdateLabel();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000ADD0 File Offset: 0x00008FD0
		public void MoveTextStart(bool shift)
		{
			if (this.m_isRichTextEditingAllowed)
			{
				int position = 0;
				if (shift)
				{
					this.stringSelectPositionInternal = position;
				}
				else
				{
					this.stringPositionInternal = position;
					this.stringSelectPositionInternal = this.stringPositionInternal;
				}
			}
			else
			{
				int position2 = 0;
				if (shift)
				{
					this.caretSelectPositionInternal = position2;
					this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(position2);
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = position2);
					this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(position2));
				}
			}
			this.UpdateLabel();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AE50 File Offset: 0x00009050
		public void MoveToEndOfLine(bool shift, bool ctrl)
		{
			int currentLine = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			int characterIndex = (ctrl ? (this.m_TextComponent.textInfo.characterCount - 1) : this.m_TextComponent.textInfo.lineInfo[currentLine].lastCharacterIndex);
			int position = this.m_TextComponent.textInfo.characterInfo[characterIndex].index;
			if (shift)
			{
				this.stringSelectPositionInternal = position;
				this.caretSelectPositionInternal = characterIndex;
			}
			else
			{
				this.stringPositionInternal = position;
				this.stringSelectPositionInternal = this.stringPositionInternal;
				this.caretSelectPositionInternal = (this.caretPositionInternal = characterIndex);
			}
			this.UpdateLabel();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000AF0C File Offset: 0x0000910C
		public void MoveToStartOfLine(bool shift, bool ctrl)
		{
			int currentLine = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			int characterIndex = (ctrl ? 0 : this.m_TextComponent.textInfo.lineInfo[currentLine].firstCharacterIndex);
			int position = 0;
			if (characterIndex > 0)
			{
				position = this.m_TextComponent.textInfo.characterInfo[characterIndex - 1].index + this.m_TextComponent.textInfo.characterInfo[characterIndex - 1].stringLength;
			}
			if (shift)
			{
				this.stringSelectPositionInternal = position;
				this.caretSelectPositionInternal = characterIndex;
			}
			else
			{
				this.stringPositionInternal = position;
				this.stringSelectPositionInternal = this.stringPositionInternal;
				this.caretSelectPositionInternal = (this.caretPositionInternal = characterIndex);
			}
			this.UpdateLabel();
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000AFDA File Offset: 0x000091DA
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000AFE1 File Offset: 0x000091E1
		private static string clipboard
		{
			get
			{
				return GUIUtility.systemCopyBuffer;
			}
			set
			{
				GUIUtility.systemCopyBuffer = value;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AFEC File Offset: 0x000091EC
		private bool InPlaceEditing()
		{
			if (this.m_TouchKeyboardAllowsInPlaceEditing)
			{
				return true;
			}
			if (this.isUWP())
			{
				return !TouchScreenKeyboard.isSupported;
			}
			return (TouchScreenKeyboard.isSupported && this.shouldHideSoftKeyboard) || !TouchScreenKeyboard.isSupported || this.shouldHideSoftKeyboard || this.shouldHideMobileInput;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B03F File Offset: 0x0000923F
		private bool InPlaceEditingChanged()
		{
			return !TMP_InputField.s_IsQuestDevice && this.m_TouchKeyboardAllowsInPlaceEditing != TouchScreenKeyboard.isInPlaceEditingAllowed;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B05A File Offset: 0x0000925A
		private bool TouchScreenKeyboardShouldBeUsed()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return TouchScreenKeyboard.isSupported;
			}
			if (TMP_InputField.s_IsQuestDevice)
			{
				return TouchScreenKeyboard.isSupported;
			}
			return !TouchScreenKeyboard.isInPlaceEditingAllowed;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B080 File Offset: 0x00009280
		private void UpdateKeyboardStringPosition()
		{
			if (this.m_HideMobileInput && this.m_SoftKeyboard != null && this.m_SoftKeyboard.canSetSelection && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS))
			{
				int selectionStart = Mathf.Min(this.caretSelectPositionInternal, this.caretPositionInternal);
				int selectionLength = Mathf.Abs(this.caretSelectPositionInternal - this.caretPositionInternal);
				this.m_SoftKeyboard.selection = new RangeInt(selectionStart, selectionLength);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B0F4 File Offset: 0x000092F4
		private void UpdateStringPositionFromKeyboard()
		{
			RangeInt selectionRange = this.m_SoftKeyboard.selection;
			int selectionStart = selectionRange.start;
			int selectionEnd = selectionRange.end;
			bool stringPositionChanged = false;
			if (this.stringPositionInternal != selectionStart)
			{
				stringPositionChanged = true;
				this.stringPositionInternal = selectionStart;
				this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			}
			if (this.stringSelectPositionInternal != selectionEnd)
			{
				this.stringSelectPositionInternal = selectionEnd;
				stringPositionChanged = true;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			}
			if (stringPositionChanged)
			{
				this.m_BlinkStartTime = Time.unscaledTime;
				this.UpdateLabel();
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B17C File Offset: 0x0000937C
		protected virtual void LateUpdate()
		{
			if (this.m_ShouldActivateNextUpdate)
			{
				if (!this.isFocused)
				{
					this.ActivateInputFieldInternal();
					this.m_ShouldActivateNextUpdate = false;
					return;
				}
				this.m_ShouldActivateNextUpdate = false;
			}
			if (this.isFocused && this.InPlaceEditingChanged())
			{
				this.DeactivateInputField(false);
			}
			if (!this.isFocused && this.m_SelectionStillActive)
			{
				GameObject selectedObject = ((EventSystem.current != null) ? EventSystem.current.currentSelectedGameObject : null);
				if (selectedObject == null && this.m_ResetOnDeActivation)
				{
					this.ReleaseSelection();
					return;
				}
				if (selectedObject != null && selectedObject != base.gameObject)
				{
					if (selectedObject == this.m_PreviouslySelectedObject)
					{
						return;
					}
					this.m_PreviouslySelectedObject = selectedObject;
					if (this.m_VerticalScrollbar && selectedObject == this.m_VerticalScrollbar.gameObject)
					{
						return;
					}
					if (this.m_ResetOnDeActivation)
					{
						this.ReleaseSelection();
						return;
					}
					if (!this.m_KeepTextSelectionVisible && selectedObject.GetComponent<TMP_InputField>() != null)
					{
						this.ReleaseSelection();
					}
					return;
				}
				else if (this.m_ProcessingEvent != null && this.m_ProcessingEvent.rawType == EventType.MouseDown && this.m_ProcessingEvent.button == 0)
				{
					bool isDoubleClick = false;
					float timeStamp = Time.unscaledTime;
					if (this.m_KeyDownStartTime + this.m_DoubleClickDelay > timeStamp)
					{
						isDoubleClick = true;
					}
					this.m_KeyDownStartTime = timeStamp;
					if (isDoubleClick)
					{
						this.ReleaseSelection();
						return;
					}
				}
			}
			this.UpdateMaskRegions();
			if ((this.InPlaceEditing() && this.isKeyboardUsingEvents()) || !this.isFocused)
			{
				return;
			}
			this.AssignPositioningIfNeeded();
			if (this.m_SoftKeyboard == null || this.m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_SoftKeyboard != null)
				{
					if (!this.m_ReadOnly)
					{
						this.text = this.m_SoftKeyboard.text;
					}
					TouchScreenKeyboard.Status status = this.m_SoftKeyboard.status;
					if (this.m_LastKeyCode != KeyCode.Return && status == TouchScreenKeyboard.Status.Done && this.isUWP())
					{
						status = TouchScreenKeyboard.Status.Canceled;
						this.m_IsKeyboardBeingClosedInHoloLens = true;
					}
					switch (status)
					{
					case TouchScreenKeyboard.Status.Done:
						this.m_ReleaseSelection = true;
						this.SendTouchScreenKeyboardStatusChanged();
						this.OnSubmit(null);
						break;
					case TouchScreenKeyboard.Status.Canceled:
						this.m_ReleaseSelection = true;
						this.m_WasCanceled = true;
						this.SendTouchScreenKeyboardStatusChanged();
						break;
					case TouchScreenKeyboard.Status.LostFocus:
						this.SendTouchScreenKeyboardStatusChanged();
						break;
					}
				}
				this.OnDeselect(null);
				return;
			}
			string val = this.m_SoftKeyboard.text;
			if (this.m_Text != val)
			{
				if (this.m_ReadOnly)
				{
					this.m_SoftKeyboard.text = this.m_Text;
				}
				else
				{
					this.m_Text = "";
					foreach (char c in val)
					{
						bool hasValidateUpdatedText = false;
						if (c == '\r' || c == '\u0003')
						{
							c = '\n';
						}
						if (this.onValidateInput != null)
						{
							c = this.onValidateInput(this.m_Text, this.m_Text.Length, c);
						}
						else if (this.characterValidation != TMP_InputField.CharacterValidation.None)
						{
							string text = this.m_Text;
							c = this.Validate(this.m_Text, this.m_Text.Length, c);
							hasValidateUpdatedText = text != this.m_Text;
						}
						if (this.lineType != TMP_InputField.LineType.MultiLineNewline && c == '\n')
						{
							this.UpdateLabel();
							this.OnSubmit(null);
							this.OnDeselect(null);
							return;
						}
						if (c != '\0' && (this.characterValidation != TMP_InputField.CharacterValidation.CustomValidator || !hasValidateUpdatedText))
						{
							this.m_Text += c.ToString();
						}
					}
					if (this.characterLimit > 0 && this.m_Text.Length > this.characterLimit)
					{
						this.m_Text = this.m_Text.Substring(0, this.characterLimit);
					}
					this.UpdateStringPositionFromKeyboard();
					if (this.m_Text != val)
					{
						this.m_SoftKeyboard.text = this.m_Text;
					}
					this.SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (this.m_HideMobileInput && this.m_SoftKeyboard != null && this.m_SoftKeyboard.canSetSelection && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.tvOS)
			{
				int selectionStart = Mathf.Min(this.caretSelectPositionInternal, this.caretPositionInternal);
				int selectionLength = Mathf.Abs(this.caretSelectPositionInternal - this.caretPositionInternal);
				this.m_SoftKeyboard.selection = new RangeInt(selectionStart, selectionLength);
			}
			else if ((this.m_HideMobileInput && Application.platform == RuntimePlatform.Android) || (this.m_SoftKeyboard.canSetSelection && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS)))
			{
				this.UpdateStringPositionFromKeyboard();
			}
			if (this.m_SoftKeyboard != null && this.m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B614 File Offset: 0x00009814
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && (this.m_SoftKeyboard == null || this.shouldHideSoftKeyboard || this.shouldHideMobileInput);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B661 File Offset: 0x00009861
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B674 File Offset: 0x00009874
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			CaretPosition insertionSide;
			int insertionIndex = TMP_TextUtilities.GetCursorIndexFromPosition(this.m_TextComponent, eventData.position, eventData.pressEventCamera, out insertionSide);
			if (this.m_isRichTextEditingAllowed)
			{
				if (insertionSide == CaretPosition.Left)
				{
					this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index;
				}
				else if (insertionSide == CaretPosition.Right)
				{
					this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex].stringLength;
				}
			}
			else if (insertionSide == CaretPosition.Left)
			{
				this.stringSelectPositionInternal = ((insertionIndex == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[insertionIndex - 1].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex - 1].stringLength));
			}
			else if (insertionSide == CaretPosition.Right)
			{
				this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex].stringLength;
			}
			this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textViewport, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			this.UpdateKeyboardStringPosition();
			eventData.Use();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B834 File Offset: 0x00009A34
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 localMousePos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textViewport, eventData.position, eventData.pressEventCamera, out localMousePos);
				Rect rect = this.textViewport.rect;
				if (this.multiLine)
				{
					if (localMousePos.y > rect.yMax)
					{
						this.MoveUp(true, true);
					}
					else if (localMousePos.y < rect.yMin)
					{
						this.MoveDown(true, true);
					}
				}
				else if (localMousePos.x < rect.xMin)
				{
					this.MoveLeft(true, false);
				}
				else if (localMousePos.x > rect.xMax)
				{
					this.MoveRight(true, false);
				}
				this.UpdateLabel();
				float delay = (this.multiLine ? 0.1f : 0.05f);
				if (this.m_WaitForSecondsRealtime == null)
				{
					this.m_WaitForSecondsRealtime = new WaitForSecondsRealtime(delay);
				}
				else
				{
					this.m_WaitForSecondsRealtime.waitTime = delay;
				}
				yield return this.m_WaitForSecondsRealtime;
			}
			this.m_DragCoroutine = null;
			yield break;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B84A File Offset: 0x00009A4A
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B860 File Offset: 0x00009A60
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool hadFocusBefore = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (this.m_SoftKeyboard == null || !this.m_SoftKeyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			Event.PopEvent(this.m_ProcessingEvent);
			bool shift = this.m_ProcessingEvent != null && (this.m_ProcessingEvent.modifiers & EventModifiers.Shift) > EventModifiers.None;
			bool isDoubleClick = false;
			float timeStamp = Time.unscaledTime;
			if (this.m_PointerDownClickStartTime + this.m_DoubleClickDelay > timeStamp)
			{
				isDoubleClick = true;
			}
			this.m_PointerDownClickStartTime = timeStamp;
			if (hadFocusBefore || !this.m_OnFocusSelectAll)
			{
				CaretPosition insertionSide;
				int insertionIndex = TMP_TextUtilities.GetCursorIndexFromPosition(this.m_TextComponent, eventData.position, eventData.pressEventCamera, out insertionSide);
				if (shift)
				{
					if (this.m_isRichTextEditingAllowed)
					{
						if (insertionSide == CaretPosition.Left)
						{
							this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index;
						}
						else if (insertionSide == CaretPosition.Right)
						{
							this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex].stringLength;
						}
					}
					else if (insertionSide == CaretPosition.Left)
					{
						this.stringSelectPositionInternal = ((insertionIndex == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[insertionIndex - 1].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex - 1].stringLength));
					}
					else if (insertionSide == CaretPosition.Right)
					{
						this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex].stringLength;
					}
				}
				else if (this.m_isRichTextEditingAllowed)
				{
					if (insertionSide == CaretPosition.Left)
					{
						this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index);
					}
					else if (insertionSide == CaretPosition.Right)
					{
						this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex].stringLength);
					}
				}
				else if (insertionSide == CaretPosition.Left)
				{
					this.stringPositionInternal = (this.stringSelectPositionInternal = ((insertionIndex == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[insertionIndex - 1].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex - 1].stringLength)));
				}
				else if (insertionSide == CaretPosition.Right)
				{
					this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index + this.m_TextComponent.textInfo.characterInfo[insertionIndex].stringLength);
				}
				if (isDoubleClick)
				{
					int wordIndex = TMP_TextUtilities.FindIntersectingWord(this.m_TextComponent, eventData.position, eventData.pressEventCamera);
					if (wordIndex != -1)
					{
						this.caretPositionInternal = this.m_TextComponent.textInfo.wordInfo[wordIndex].firstCharacterIndex;
						this.caretSelectPositionInternal = this.m_TextComponent.textInfo.wordInfo[wordIndex].lastCharacterIndex + 1;
						this.stringPositionInternal = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index;
						this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].stringLength;
					}
					else
					{
						this.caretPositionInternal = insertionIndex;
						this.caretSelectPositionInternal = this.caretPositionInternal + 1;
						this.stringPositionInternal = this.m_TextComponent.textInfo.characterInfo[insertionIndex].index;
						this.stringSelectPositionInternal = this.stringPositionInternal + this.m_TextComponent.textInfo.characterInfo[insertionIndex].stringLength;
					}
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal));
				}
				this.m_isSelectAll = false;
			}
			this.UpdateLabel();
			this.UpdateKeyboardStringPosition();
			eventData.Use();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000BD54 File Offset: 0x00009F54
		protected TMP_InputField.EditState KeyPressed(Event evt)
		{
			EventModifiers currentEventModifiers = evt.modifiers;
			bool ctrl = (this.m_IsApplePlatform ? ((currentEventModifiers & EventModifiers.Command) > EventModifiers.None) : ((currentEventModifiers & EventModifiers.Control) > EventModifiers.None));
			bool shift = (currentEventModifiers & EventModifiers.Shift) > EventModifiers.None;
			bool alt = (currentEventModifiers & EventModifiers.Alt) > EventModifiers.None;
			bool ctrlOnly = ctrl && !alt && !shift;
			this.m_LastKeyCode = evt.keyCode;
			KeyCode keyCode = evt.keyCode;
			if (keyCode <= KeyCode.A)
			{
				if (keyCode <= KeyCode.Return)
				{
					if (keyCode == KeyCode.Backspace)
					{
						this.Backspace();
						return TMP_InputField.EditState.Continue;
					}
					if (keyCode != KeyCode.Return)
					{
						goto IL_0229;
					}
				}
				else
				{
					if (keyCode == KeyCode.Escape)
					{
						this.m_ReleaseSelection = true;
						this.m_WasCanceled = true;
						return TMP_InputField.EditState.Finish;
					}
					if (keyCode != KeyCode.A)
					{
						goto IL_0229;
					}
					if (ctrlOnly)
					{
						this.SelectAll();
						return TMP_InputField.EditState.Continue;
					}
					goto IL_0229;
				}
			}
			else if (keyCode <= KeyCode.V)
			{
				if (keyCode != KeyCode.C)
				{
					if (keyCode != KeyCode.V)
					{
						goto IL_0229;
					}
					if (ctrlOnly)
					{
						this.Append(TMP_InputField.clipboard);
						return TMP_InputField.EditState.Continue;
					}
					goto IL_0229;
				}
				else
				{
					if (ctrlOnly)
					{
						if (this.inputType != TMP_InputField.InputType.Password)
						{
							TMP_InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							TMP_InputField.clipboard = "";
						}
						return TMP_InputField.EditState.Continue;
					}
					goto IL_0229;
				}
			}
			else if (keyCode != KeyCode.X)
			{
				if (keyCode == KeyCode.Delete)
				{
					this.DeleteKey();
					return TMP_InputField.EditState.Continue;
				}
				switch (keyCode)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
				case KeyCode.Insert:
					goto IL_0229;
				case KeyCode.UpArrow:
					this.MoveUp(shift);
					return TMP_InputField.EditState.Continue;
				case KeyCode.DownArrow:
					this.MoveDown(shift);
					return TMP_InputField.EditState.Continue;
				case KeyCode.RightArrow:
					this.MoveRight(shift, ctrl);
					return TMP_InputField.EditState.Continue;
				case KeyCode.LeftArrow:
					this.MoveLeft(shift, ctrl);
					return TMP_InputField.EditState.Continue;
				case KeyCode.Home:
					this.MoveToStartOfLine(shift, ctrl);
					return TMP_InputField.EditState.Continue;
				case KeyCode.End:
					this.MoveToEndOfLine(shift, ctrl);
					return TMP_InputField.EditState.Continue;
				case KeyCode.PageUp:
					this.MovePageUp(shift);
					return TMP_InputField.EditState.Continue;
				case KeyCode.PageDown:
					this.MovePageDown(shift);
					return TMP_InputField.EditState.Continue;
				default:
					goto IL_0229;
				}
			}
			else
			{
				if (ctrlOnly)
				{
					if (this.inputType != TMP_InputField.InputType.Password)
					{
						TMP_InputField.clipboard = this.GetSelectedString();
					}
					else
					{
						TMP_InputField.clipboard = "";
					}
					this.Delete();
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return TMP_InputField.EditState.Continue;
				}
				goto IL_0229;
			}
			if (this.lineType != TMP_InputField.LineType.MultiLineNewline)
			{
				this.m_ReleaseSelection = true;
				return TMP_InputField.EditState.Finish;
			}
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			if (this.m_LineLimit > 0 && textInfo != null && textInfo.lineCount >= this.m_LineLimit)
			{
				this.m_ReleaseSelection = true;
				return TMP_InputField.EditState.Finish;
			}
			IL_0229:
			char c = evt.character;
			if (!this.multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return TMP_InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (shift && c == '\n')
			{
				c = '\v';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && this.compositionLength > 0)
			{
				this.UpdateLabel();
			}
			return TMP_InputField.EditState.Continue;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		protected virtual bool IsValidChar(char c)
		{
			return c != '\u007f' && (c == '\t' || c == '\n' || c >= ' ');
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C00D File Offset: 0x0000A20D
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C018 File Offset: 0x0000A218
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool consumedEvent = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				EventType eventType = this.m_ProcessingEvent.rawType;
				if (eventType != EventType.KeyUp)
				{
					if (eventType == EventType.KeyDown)
					{
						consumedEvent = true;
						if (!this.m_IsCompositionActive || this.compositionLength != 0 || this.m_ProcessingEvent.character != '\0' || this.m_ProcessingEvent.modifiers != EventModifiers.None)
						{
							if (this.KeyPressed(this.m_ProcessingEvent) == TMP_InputField.EditState.Finish)
							{
								if (!this.m_WasCanceled)
								{
									this.SendOnSubmit();
								}
								this.DeactivateInputField(false);
								break;
							}
							this.m_IsTextComponentUpdateRequired = true;
							this.UpdateLabel();
						}
					}
					else if (eventType - EventType.ValidateCommand <= 1 && this.m_ProcessingEvent.commandName == "SelectAll")
					{
						this.SelectAll();
						consumedEvent = true;
					}
				}
			}
			if (consumedEvent)
			{
				this.UpdateLabel();
				eventData.Use();
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		public virtual void OnScroll(PointerEventData eventData)
		{
			if (this.m_LineType == TMP_InputField.LineType.SingleLine)
			{
				if (this.m_IScrollHandlerParent != null)
				{
					this.m_IScrollHandlerParent.OnScroll(eventData);
				}
				return;
			}
			if (this.m_TextComponent.preferredHeight < this.m_TextViewport.rect.height)
			{
				return;
			}
			float scrollDirection = -eventData.scrollDelta.y;
			this.m_ScrollPosition = this.GetScrollPositionRelativeToViewport();
			this.m_ScrollPosition += 1f / (float)this.m_TextComponent.textInfo.lineCount * scrollDirection * this.m_ScrollSensitivity;
			this.m_ScrollPosition = Mathf.Clamp01(this.m_ScrollPosition);
			this.AdjustTextPositionRelativeToViewport(this.m_ScrollPosition);
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.value = this.m_ScrollPosition;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		private float GetScrollPositionRelativeToViewport()
		{
			Rect viewportRect = this.m_TextViewport.rect;
			return (float)((int)((this.m_TextComponent.textInfo.lineInfo[0].ascender + this.m_TextComponent.margin.y + this.m_TextComponent.margin.w - viewportRect.yMax + this.m_TextComponent.rectTransform.anchoredPosition.y) / (this.m_TextComponent.preferredHeight - viewportRect.height) * 1000f + 0.5f)) / 1000f;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C25C File Offset: 0x0000A45C
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return "";
			}
			int startPos = this.stringPositionInternal;
			int endPos = this.stringSelectPositionInternal;
			if (startPos > endPos)
			{
				int num = startPos;
				startPos = endPos;
				endPos = num;
			}
			return this.text.Substring(startPos, endPos - startPos);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C29C File Offset: 0x0000A49C
		private int FindNextWordBegin()
		{
			if (this.stringSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int spaceLoc = this.text.IndexOfAny(TMP_InputField.kSeparators, this.stringSelectPositionInternal + 1);
			if (spaceLoc == -1)
			{
				spaceLoc = this.text.Length;
			}
			else
			{
				spaceLoc++;
			}
			return spaceLoc;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.stringPositionInternal = (this.stringSelectPositionInternal = Mathf.Max(this.stringPositionInternal, this.stringSelectPositionInternal));
				this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
				return;
			}
			int position;
			if (ctrl)
			{
				position = this.FindNextWordBegin();
			}
			else if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringSelectPositionInternal < this.text.Length && char.IsHighSurrogate(this.text[this.stringSelectPositionInternal]))
				{
					position = this.stringSelectPositionInternal + 2;
				}
				else
				{
					position = this.stringSelectPositionInternal + 1;
				}
			}
			else if (this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].character == '\r' && this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal + 1].character == '\n')
			{
				position = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal + 1].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal + 1].stringLength;
			}
			else
			{
				position = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].stringLength;
			}
			if (shift)
			{
				this.stringSelectPositionInternal = position;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				return;
			}
			this.stringSelectPositionInternal = (this.stringPositionInternal = position);
			if (this.stringPositionInternal >= this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index + this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].stringLength)
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C514 File Offset: 0x0000A714
		private int FindPrevWordBegin()
		{
			if (this.stringSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int spaceLoc = this.text.LastIndexOfAny(TMP_InputField.kSeparators, this.stringSelectPositionInternal - 2);
			if (spaceLoc == -1)
			{
				spaceLoc = 0;
			}
			else
			{
				spaceLoc++;
			}
			return spaceLoc;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C554 File Offset: 0x0000A754
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.stringPositionInternal = (this.stringSelectPositionInternal = Mathf.Min(this.stringPositionInternal, this.stringSelectPositionInternal));
				this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
				return;
			}
			int position;
			if (ctrl)
			{
				position = this.FindPrevWordBegin();
			}
			else if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringSelectPositionInternal > 0 && char.IsLowSurrogate(this.text[this.stringSelectPositionInternal - 1]))
				{
					position = this.stringSelectPositionInternal - 2;
				}
				else
				{
					position = this.stringSelectPositionInternal - 1;
				}
			}
			else
			{
				position = ((this.caretSelectPositionInternal < 1) ? this.m_TextComponent.textInfo.characterInfo[0].index : this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].index);
				if (position > 0 && this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].character == '\n' && this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 2].character == '\r')
				{
					position = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 2].index;
				}
			}
			if (shift)
			{
				this.stringSelectPositionInternal = position;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				return;
			}
			this.stringSelectPositionInternal = (this.stringPositionInternal = position);
			if (this.caretPositionInternal > 0 && this.stringPositionInternal <= this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].index)
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C738 File Offset: 0x0000A938
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo originChar = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int originLine = originChar.lineNumber;
			if (originLine - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			else
			{
				int endCharIdx = this.m_TextComponent.textInfo.lineInfo[originLine].firstCharacterIndex - 1;
				int closest = -1;
				float distance = 32767f;
				float range = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[originLine - 1].firstCharacterIndex;
				while (i < endCharIdx)
				{
					TMP_CharacterInfo currentChar = this.m_TextComponent.textInfo.characterInfo[i];
					float d = originChar.origin - currentChar.origin;
					float r = d / (currentChar.xAdvance - currentChar.origin);
					if (r >= 0f && r <= 1f)
					{
						if (r < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						d = Mathf.Abs(d);
						if (d < distance)
						{
							closest = i;
							distance = d;
							range = r;
						}
						i++;
					}
				}
				if (closest == -1)
				{
					return endCharIdx;
				}
				if (range < 0.5f)
				{
					return closest;
				}
				return closest + 1;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C878 File Offset: 0x0000AA78
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo originChar = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int originLine = originChar.lineNumber;
			if (originLine + 1 >= this.m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			else
			{
				int endCharIdx = this.m_TextComponent.textInfo.lineInfo[originLine + 1].lastCharacterIndex;
				int closest = -1;
				float distance = 32767f;
				float range = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[originLine + 1].firstCharacterIndex;
				while (i < endCharIdx)
				{
					TMP_CharacterInfo currentChar = this.m_TextComponent.textInfo.characterInfo[i];
					float d = originChar.origin - currentChar.origin;
					float r = d / (currentChar.xAdvance - currentChar.origin);
					if (r >= 0f && r <= 1f)
					{
						if (r < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						d = Mathf.Abs(d);
						if (d < distance)
						{
							closest = i;
							distance = d;
							range = r;
						}
						i++;
					}
				}
				if (closest == -1)
				{
					return endCharIdx;
				}
				if (range < 0.5f)
				{
					return closest;
				}
				return closest + 1;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		private int PageUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo originChar = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int originLine = originChar.lineNumber;
			if (originLine - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			else
			{
				float viewportHeight = this.m_TextViewport.rect.height;
				int newLine = originLine - 1;
				while (newLine > 0 && this.m_TextComponent.textInfo.lineInfo[newLine].baseline <= this.m_TextComponent.textInfo.lineInfo[originLine].baseline + viewportHeight)
				{
					newLine--;
				}
				int endCharIdx = this.m_TextComponent.textInfo.lineInfo[newLine].lastCharacterIndex;
				int closest = -1;
				float distance = 32767f;
				float range = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[newLine].firstCharacterIndex;
				while (i < endCharIdx)
				{
					TMP_CharacterInfo currentChar = this.m_TextComponent.textInfo.characterInfo[i];
					float d = originChar.origin - currentChar.origin;
					float r = d / (currentChar.xAdvance - currentChar.origin);
					if (r >= 0f && r <= 1f)
					{
						if (r < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						d = Mathf.Abs(d);
						if (d < distance)
						{
							closest = i;
							distance = d;
							range = r;
						}
						i++;
					}
				}
				if (closest == -1)
				{
					return endCharIdx;
				}
				if (range < 0.5f)
				{
					return closest;
				}
				return closest + 1;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000CB84 File Offset: 0x0000AD84
		private int PageDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo originChar = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int originLine = originChar.lineNumber;
			if (originLine + 1 >= this.m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			else
			{
				float viewportHeight = this.m_TextViewport.rect.height;
				int newLine = originLine + 1;
				while (newLine < this.m_TextComponent.textInfo.lineCount - 1 && this.m_TextComponent.textInfo.lineInfo[newLine].baseline >= this.m_TextComponent.textInfo.lineInfo[originLine].baseline - viewportHeight)
				{
					newLine++;
				}
				int endCharIdx = this.m_TextComponent.textInfo.lineInfo[newLine].lastCharacterIndex;
				int closest = -1;
				float distance = 32767f;
				float range = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[newLine].firstCharacterIndex;
				while (i < endCharIdx)
				{
					TMP_CharacterInfo currentChar = this.m_TextComponent.textInfo.characterInfo[i];
					float d = originChar.origin - currentChar.origin;
					float r = d / (currentChar.xAdvance - currentChar.origin);
					if (r >= 0f && r <= 1f)
					{
						if (r < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						d = Mathf.Abs(d);
						if (d < distance)
						{
							closest = i;
							distance = d;
							range = r;
						}
						i++;
					}
				}
				if (closest == -1)
				{
					return endCharIdx;
				}
				if (range < 0.5f)
				{
					return closest;
				}
				return closest + 1;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000CD62 File Offset: 0x0000AF62
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int position = (this.multiLine ? this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : (this.m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				this.caretSelectPositionInternal = position;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = position);
			this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000CE16 File Offset: 0x0000B016
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000CE20 File Offset: 0x0000B020
		private void MoveUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int position = (this.multiLine ? this.LineUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				this.caretSelectPositionInternal = position;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = position);
			this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CEB9 File Offset: 0x0000B0B9
		private void MovePageUp(bool shift)
		{
			this.MovePageUp(shift, true);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		private void MovePageUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int position = (this.multiLine ? this.PageUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar) : 0);
			if (shift)
			{
				this.caretSelectPositionInternal = position;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
			}
			else
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = position);
				this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float offset = this.m_TextViewport.rect.height;
				float topTextBounds = this.m_TextComponent.rectTransform.position.y + this.m_TextComponent.textBounds.max.y;
				float topViewportBounds = this.m_TextViewport.position.y + this.m_TextViewport.rect.yMax;
				offset = ((topViewportBounds > topTextBounds + offset) ? offset : (topViewportBounds - topTextBounds));
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, offset);
				this.AssignPositioningIfNeeded();
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000D010 File Offset: 0x0000B210
		private void MovePageDown(bool shift)
		{
			this.MovePageDown(shift, true);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D01C File Offset: 0x0000B21C
		private void MovePageDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int position = (this.multiLine ? this.PageDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : (this.m_TextComponent.textInfo.characterCount - 1));
			if (shift)
			{
				this.caretSelectPositionInternal = position;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
			}
			else
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = position);
				this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float offset = this.m_TextViewport.rect.height;
				float bottomTextBounds = this.m_TextComponent.rectTransform.position.y + this.m_TextComponent.textBounds.min.y;
				float bottomViewportBounds = this.m_TextViewport.position.y + this.m_TextViewport.rect.yMin;
				offset = ((bottomViewportBounds > bottomTextBounds + offset) ? offset : (bottomViewportBounds - bottomTextBounds));
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, offset);
				this.AssignPositioningIfNeeded();
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000D17C File Offset: 0x0000B37C
		private void Delete()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.m_StringPosition == this.m_StringSelectPosition)
			{
				return;
			}
			if (this.m_isRichTextEditingAllowed || this.m_isSelectAll)
			{
				if (this.m_StringPosition < this.m_StringSelectPosition)
				{
					this.m_Text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					this.m_StringSelectPosition = this.m_StringPosition;
				}
				else
				{
					this.m_Text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
					this.m_StringPosition = this.m_StringSelectPosition;
				}
				if (this.m_isSelectAll)
				{
					this.m_CaretPosition = (this.m_CaretSelectPosition = 0);
					this.m_isSelectAll = false;
					return;
				}
			}
			else
			{
				if (this.m_CaretPosition < this.m_CaretSelectPosition)
				{
					int index = this.ClampArrayIndex(this.m_CaretSelectPosition - 1);
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition].index;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[index].index + this.m_TextComponent.textInfo.characterInfo[index].stringLength;
					this.m_Text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					this.m_StringSelectPosition = this.m_StringPosition;
					this.m_CaretSelectPosition = this.m_CaretPosition;
					return;
				}
				int index2 = this.ClampArrayIndex(this.m_CaretPosition - 1);
				this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[index2].index + this.m_TextComponent.textInfo.characterInfo[index2].stringLength;
				this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition].index;
				this.m_Text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
				this.m_StringPosition = this.m_StringSelectPosition;
				this.m_CaretPosition = this.m_CaretSelectPosition;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		private void DeleteKey()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.m_HasTextBeenRemoved = true;
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringPositionInternal < this.text.Length)
				{
					if (char.IsHighSurrogate(this.text[this.stringPositionInternal]))
					{
						this.m_Text = this.text.Remove(this.stringPositionInternal, 2);
					}
					else
					{
						this.m_Text = this.text.Remove(this.stringPositionInternal, 1);
					}
					this.m_HasTextBeenRemoved = true;
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return;
				}
			}
			else if (this.caretPositionInternal < this.m_TextComponent.textInfo.characterCount - 1)
			{
				int numberOfCharactersToRemove = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].stringLength;
				if (this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].character == '\r' && this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal + 1].character == '\n')
				{
					numberOfCharactersToRemove += this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal + 1].stringLength;
				}
				int nextCharacterStringPosition = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index;
				this.m_Text = this.text.Remove(nextCharacterStringPosition, numberOfCharactersToRemove);
				this.m_HasTextBeenRemoved = true;
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000D55C File Offset: 0x0000B75C
		private void Backspace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.m_HasTextBeenRemoved = true;
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringPositionInternal > 0)
				{
					int numberOfCharactersToRemove = 1;
					if (char.IsLowSurrogate(this.text[this.stringPositionInternal - 1]))
					{
						numberOfCharactersToRemove = 2;
					}
					this.stringSelectPositionInternal = (this.stringPositionInternal -= numberOfCharactersToRemove);
					this.m_Text = this.text.Remove(this.stringPositionInternal, numberOfCharactersToRemove);
					this.caretSelectPositionInternal = --this.caretPositionInternal;
					this.m_HasTextBeenRemoved = true;
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return;
				}
			}
			else
			{
				if (this.caretPositionInternal > 0)
				{
					int caretPositionIndex = this.caretPositionInternal - 1;
					int numberOfCharactersToRemove2 = this.m_TextComponent.textInfo.characterInfo[caretPositionIndex].stringLength;
					if (caretPositionIndex > 0 && this.m_TextComponent.textInfo.characterInfo[caretPositionIndex].character == '\n' && this.m_TextComponent.textInfo.characterInfo[caretPositionIndex - 1].character == '\r')
					{
						numberOfCharactersToRemove2 += this.m_TextComponent.textInfo.characterInfo[caretPositionIndex - 1].stringLength;
						caretPositionIndex--;
					}
					this.m_Text = this.text.Remove(this.m_TextComponent.textInfo.characterInfo[caretPositionIndex].index, numberOfCharactersToRemove2);
					this.stringSelectPositionInternal = (this.stringPositionInternal = ((this.caretPositionInternal < 1) ? this.m_TextComponent.textInfo.characterInfo[0].index : this.m_TextComponent.textInfo.characterInfo[caretPositionIndex].index));
					this.caretSelectPositionInternal = (this.caretPositionInternal = caretPositionIndex);
				}
				this.m_HasTextBeenRemoved = true;
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000D75C File Offset: 0x0000B95C
		protected virtual void Append(string input)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int i = 0;
			int imax = input.Length;
			while (i < imax)
			{
				char c = input[i];
				if (c >= ' ' || c == '\t' || c == '\r' || c == '\n')
				{
					this.Append(c);
				}
				i++;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		protected virtual void Append(char input)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int insertionPosition = Mathf.Min(this.stringPositionInternal, this.stringSelectPositionInternal);
			string validateText = this.text;
			if (this.selectionFocusPosition != this.selectionAnchorPosition)
			{
				this.m_HasTextBeenRemoved = true;
				if (this.m_isRichTextEditingAllowed || this.m_isSelectAll)
				{
					if (this.m_StringPosition < this.m_StringSelectPosition)
					{
						validateText = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					}
					else
					{
						validateText = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
					}
				}
				else if (this.m_CaretPosition < this.m_CaretSelectPosition)
				{
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition].index;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].stringLength;
					validateText = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
				}
				else
				{
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].stringLength;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition].index;
					validateText = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
				}
			}
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(validateText, insertionPosition, input);
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.CustomValidator)
			{
				input = this.Validate(validateText, insertionPosition, input);
				if (input == '\0')
				{
					return;
				}
				this.SendOnValueChanged();
				this.UpdateLabel();
				return;
			}
			else if (this.characterValidation != TMP_InputField.CharacterValidation.None)
			{
				input = this.Validate(validateText, insertionPosition, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000DA00 File Offset: 0x0000BC00
		private void Insert(char c)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			string replaceString = c.ToString();
			this.Delete();
			if (this.characterLimit > 0 && this.text.Length >= this.characterLimit)
			{
				return;
			}
			this.m_Text = this.text.Insert(this.m_StringPosition, replaceString);
			if (!char.IsHighSurrogate(c))
			{
				this.m_CaretSelectPosition = ++this.m_CaretPosition;
			}
			this.m_StringSelectPosition = ++this.m_StringPosition;
			this.UpdateTouchKeyboardFromEditChanges();
			this.SendOnValueChanged();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000DA9B File Offset: 0x0000BC9B
		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (this.m_SoftKeyboard != null && this.InPlaceEditing())
			{
				this.m_SoftKeyboard.text = this.m_Text;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000DABE File Offset: 0x0000BCBE
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.UpdateLabel();
			this.SendOnValueChanged();
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000DACC File Offset: 0x0000BCCC
		private void SendOnValueChanged()
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.text);
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000DAE7 File Offset: 0x0000BCE7
		protected void SendOnEndEdit()
		{
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000DB02 File Offset: 0x0000BD02
		protected void SendOnSubmit()
		{
			if (this.onSubmit != null)
			{
				this.onSubmit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000DB1D File Offset: 0x0000BD1D
		protected void SendOnFocus()
		{
			if (this.onSelect != null)
			{
				this.onSelect.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000DB38 File Offset: 0x0000BD38
		protected void SendOnFocusLost()
		{
			if (this.onDeselect != null)
			{
				this.onDeselect.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000DB53 File Offset: 0x0000BD53
		protected void SendOnTextSelection()
		{
			this.m_isSelected = true;
			if (this.onTextSelection != null)
			{
				this.onTextSelection.Invoke(this.m_Text, this.stringPositionInternal, this.stringSelectPositionInternal);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000DB81 File Offset: 0x0000BD81
		protected void SendOnEndTextSelection()
		{
			if (!this.m_isSelected)
			{
				return;
			}
			if (this.onEndTextSelection != null)
			{
				this.onEndTextSelection.Invoke(this.m_Text, this.stringPositionInternal, this.stringSelectPositionInternal);
			}
			this.m_isSelected = false;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
		protected void SendTouchScreenKeyboardStatusChanged()
		{
			if (this.m_SoftKeyboard != null && this.onTouchScreenKeyboardStatusChanged != null)
			{
				this.onTouchScreenKeyboardStatusChanged.Invoke(this.m_SoftKeyboard.status);
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventCallback)
			{
				this.m_PreventCallback = true;
				string fullText;
				if (this.compositionLength > 0 && !this.m_ReadOnly)
				{
					this.Delete();
					if (this.m_RichText)
					{
						fullText = string.Concat(new string[]
						{
							this.text.Substring(0, this.m_StringPosition),
							"<u>",
							this.compositionString,
							"</u>",
							this.text.Substring(this.m_StringPosition)
						});
					}
					else
					{
						fullText = this.text.Substring(0, this.m_StringPosition) + this.compositionString + this.text.Substring(this.m_StringPosition);
					}
					this.m_IsCompositionActive = true;
				}
				else
				{
					fullText = this.text;
					this.m_IsCompositionActive = false;
					this.m_ShouldUpdateIMEWindowPosition = true;
				}
				string processed;
				if (this.inputType == TMP_InputField.InputType.Password)
				{
					processed = new string(this.asteriskChar, fullText.Length);
				}
				else
				{
					processed = fullText;
				}
				bool isEmpty = string.IsNullOrEmpty(fullText);
				if (this.m_Placeholder != null)
				{
					this.m_Placeholder.enabled = isEmpty;
				}
				if (!isEmpty && !this.m_ReadOnly)
				{
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = processed + "\u200b";
				if (this.m_IsDrivenByLayoutComponents)
				{
					LayoutRebuilder.MarkLayoutForRebuild(this.m_RectTransform);
				}
				if (this.m_LineLimit > 0)
				{
					this.m_TextComponent.ForceMeshUpdate(false, false);
					TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
					if (textInfo != null && textInfo.lineCount > this.m_LineLimit)
					{
						int lastValidCharacterIndex = textInfo.lineInfo[this.m_LineLimit - 1].lastCharacterIndex;
						int characterStringIndex = textInfo.characterInfo[lastValidCharacterIndex].index + textInfo.characterInfo[lastValidCharacterIndex].stringLength;
						this.text = processed.Remove(characterStringIndex, processed.Length - characterStringIndex);
						this.m_TextComponent.text = this.text + "\u200b";
					}
				}
				if (this.m_IsTextComponentUpdateRequired || (this.m_VerticalScrollbar && (!this.m_IsCaretPositionDirty || !this.m_IsStringPositionDirty)))
				{
					this.m_IsTextComponentUpdateRequired = false;
					this.m_TextComponent.ForceMeshUpdate(false, false);
				}
				this.MarkGeometryAsDirty();
				this.m_PreventCallback = false;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000DE54 File Offset: 0x0000C054
		private void UpdateScrollbar()
		{
			if (this.m_VerticalScrollbar)
			{
				float size = this.m_TextViewport.rect.height / this.m_TextComponent.preferredHeight;
				this.m_VerticalScrollbar.size = size;
				this.m_VerticalScrollbar.value = this.GetScrollPositionRelativeToViewport();
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000DEAB File Offset: 0x0000C0AB
		private void OnScrollbarValueChange(float value)
		{
			if (value < 0f || value > 1f)
			{
				return;
			}
			this.AdjustTextPositionRelativeToViewport(value);
			this.m_ScrollPosition = value;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00002AAB File Offset: 0x00000CAB
		private void UpdateMaskRegions()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000DECC File Offset: 0x0000C0CC
		private void AdjustTextPositionRelativeToViewport(float relativePosition)
		{
			if (this.m_TextViewport == null)
			{
				return;
			}
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			if (textInfo == null || textInfo.lineInfo == null || textInfo.lineCount == 0 || textInfo.lineCount > textInfo.lineInfo.Length)
			{
				return;
			}
			float verticalAlignmentOffset = 0f;
			float textHeight = this.m_TextComponent.preferredHeight;
			VerticalAlignmentOptions verticalAlignment = this.m_TextComponent.verticalAlignment;
			if (verticalAlignment <= VerticalAlignmentOptions.Bottom)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Top)
				{
					if (verticalAlignment != VerticalAlignmentOptions.Middle)
					{
						if (verticalAlignment == VerticalAlignmentOptions.Bottom)
						{
							verticalAlignmentOffset = 1f;
						}
					}
					else
					{
						verticalAlignmentOffset = 0.5f;
					}
				}
				else
				{
					verticalAlignmentOffset = 0f;
				}
			}
			else if (verticalAlignment != VerticalAlignmentOptions.Baseline)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Geometry)
				{
					if (verticalAlignment == VerticalAlignmentOptions.Capline)
					{
						verticalAlignmentOffset = 0.5f;
					}
				}
				else
				{
					verticalAlignmentOffset = 0.5f;
					textHeight = this.m_TextComponent.bounds.size.y;
				}
			}
			this.m_TextComponent.rectTransform.anchoredPosition = new Vector2(this.m_TextComponent.rectTransform.anchoredPosition.x, (textHeight - this.m_TextViewport.rect.height) * (relativePosition - verticalAlignmentOffset));
			this.AssignPositioningIfNeeded();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		private int GetCaretPositionFromStringIndex(int stringIndex)
		{
			int count = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < count; i++)
			{
				if (this.m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return count;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000E048 File Offset: 0x0000C248
		private int GetMinCaretPositionFromStringIndex(int stringIndex)
		{
			int count = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < count; i++)
			{
				if (stringIndex < this.m_TextComponent.textInfo.characterInfo[i].index + this.m_TextComponent.textInfo.characterInfo[i].stringLength)
				{
					return i;
				}
			}
			return count;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		private int GetMaxCaretPositionFromStringIndex(int stringIndex)
		{
			int count = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < count; i++)
			{
				if (this.m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return count;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000E0FB File Offset: 0x0000C2FB
		private int GetStringIndexFromCaretPosition(int caretPosition)
		{
			this.ClampCaretPos(ref caretPosition);
			return this.m_TextComponent.textInfo.characterInfo[caretPosition].index;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E120 File Offset: 0x0000C320
		private void UpdateStringIndexFromCaretPosition()
		{
			this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.m_CaretPosition);
			this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.m_CaretSelectPosition);
			this.m_IsStringPositionDirty = false;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000E14D File Offset: 0x0000C34D
		private void UpdateCaretPositionFromStringIndex()
		{
			this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			this.m_IsCaretPositionDirty = false;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E17A File Offset: 0x0000C37A
		public void ForceLabelUpdate()
		{
			this.UpdateLabel();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E182 File Offset: 0x0000C382
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000E18A File Offset: 0x0000C38A
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000E196 File Offset: 0x0000C396
		private void UpdateGeometry()
		{
			if (!this.InPlaceEditing() && !this.isUWP())
			{
				return;
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.mesh);
			this.m_CachedInputRenderer.SetMesh(this.mesh);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		private void AssignPositioningIfNeeded()
		{
			if (this.m_TextComponent != null && this.caretRectTrans != null && (this.caretRectTrans.localPosition != this.m_TextComponent.rectTransform.localPosition || this.caretRectTrans.localRotation != this.m_TextComponent.rectTransform.localRotation || this.caretRectTrans.localScale != this.m_TextComponent.rectTransform.localScale || this.caretRectTrans.anchorMin != this.m_TextComponent.rectTransform.anchorMin || this.caretRectTrans.anchorMax != this.m_TextComponent.rectTransform.anchorMax || this.caretRectTrans.anchoredPosition != this.m_TextComponent.rectTransform.anchoredPosition || this.caretRectTrans.sizeDelta != this.m_TextComponent.rectTransform.sizeDelta || this.caretRectTrans.pivot != this.m_TextComponent.rectTransform.pivot))
			{
				this.caretRectTrans.localPosition = this.m_TextComponent.rectTransform.localPosition;
				this.caretRectTrans.localRotation = this.m_TextComponent.rectTransform.localRotation;
				this.caretRectTrans.localScale = this.m_TextComponent.rectTransform.localScale;
				this.caretRectTrans.anchorMin = this.m_TextComponent.rectTransform.anchorMin;
				this.caretRectTrans.anchorMax = this.m_TextComponent.rectTransform.anchorMax;
				this.caretRectTrans.anchoredPosition = this.m_TextComponent.rectTransform.anchoredPosition;
				this.caretRectTrans.sizeDelta = this.m_TextComponent.rectTransform.sizeDelta;
				this.caretRectTrans.pivot = this.m_TextComponent.rectTransform.pivot;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E400 File Offset: 0x0000C600
		private void OnFillVBO(Mesh vbo)
		{
			using (VertexHelper helper = new VertexHelper())
			{
				if (!this.isFocused && !this.m_SelectionStillActive)
				{
					helper.FillMesh(vbo);
				}
				else
				{
					if (this.m_IsStringPositionDirty)
					{
						this.UpdateStringIndexFromCaretPosition();
					}
					if (this.m_IsCaretPositionDirty)
					{
						this.UpdateCaretPositionFromStringIndex();
					}
					if (!this.hasSelection)
					{
						this.GenerateCaret(helper, Vector2.zero);
						this.SendOnEndTextSelection();
					}
					else
					{
						this.GenerateHighlight(helper, Vector2.zero);
						this.SendOnTextSelection();
					}
					helper.FillMesh(vbo);
				}
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E49C File Offset: 0x0000C69C
		private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
		{
			if (!this.m_CaretVisible || this.m_TextComponent.canvas == null || this.m_ReadOnly)
			{
				return;
			}
			if (this.m_CursorVerts == null)
			{
				this.CreateCursorVerts();
			}
			Vector2 startPosition = Vector2.zero;
			if (this.caretPositionInternal >= this.m_TextComponent.textInfo.characterInfo.Length || this.caretPositionInternal < 0)
			{
				return;
			}
			int currentLine = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			TMP_CharacterInfo currentCharacter;
			float height;
			if (this.caretPositionInternal == this.m_TextComponent.textInfo.lineInfo[currentLine].firstCharacterIndex)
			{
				currentCharacter = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal];
				height = currentCharacter.ascender - currentCharacter.descender;
				if (this.m_TextComponent.verticalAlignment == VerticalAlignmentOptions.Geometry)
				{
					startPosition = new Vector2(currentCharacter.origin, 0f - height / 2f);
				}
				else
				{
					startPosition = new Vector2(currentCharacter.origin, currentCharacter.descender);
				}
			}
			else
			{
				currentCharacter = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1];
				height = currentCharacter.ascender - currentCharacter.descender;
				if (this.m_TextComponent.verticalAlignment == VerticalAlignmentOptions.Geometry)
				{
					startPosition = new Vector2(currentCharacter.xAdvance, 0f - height / 2f);
				}
				else
				{
					startPosition = new Vector2(currentCharacter.xAdvance, currentCharacter.descender);
				}
			}
			if (this.m_SoftKeyboard != null && this.compositionLength == 0)
			{
				int selectionStart = this.m_StringPosition;
				int softKeyboardStringLength = ((this.m_SoftKeyboard.text == null) ? 0 : this.m_SoftKeyboard.text.Length);
				if (selectionStart < 0)
				{
					selectionStart = 0;
				}
				if (selectionStart > softKeyboardStringLength)
				{
					selectionStart = softKeyboardStringLength;
				}
				this.m_SoftKeyboard.selection = new RangeInt(selectionStart, 0);
			}
			if ((this.isFocused && startPosition != this.m_LastPosition) || this.m_forceRectTransformAdjustment || this.m_HasTextBeenRemoved)
			{
				this.AdjustRectTransformRelativeToViewport(startPosition, height, currentCharacter.isVisible);
			}
			this.m_LastPosition = startPosition;
			float top = startPosition.y + height;
			float bottom = top - height;
			TMP_FontAsset fontAsset = this.m_TextComponent.font;
			float baseScale = this.m_TextComponent.fontSize / fontAsset.m_FaceInfo.pointSize * fontAsset.m_FaceInfo.scale;
			float width = (float)this.m_CaretWidth * fontAsset.faceInfo.lineHeight * baseScale * 0.05f;
			this.m_CursorVerts[0].position = new Vector3(startPosition.x, bottom, 0f);
			this.m_CursorVerts[1].position = new Vector3(startPosition.x, top, 0f);
			this.m_CursorVerts[2].position = new Vector3(startPosition.x + width, top, 0f);
			this.m_CursorVerts[3].position = new Vector3(startPosition.x + width, bottom, 0f);
			this.m_CursorVerts[0].color = this.caretColor;
			this.m_CursorVerts[1].color = this.caretColor;
			this.m_CursorVerts[2].color = this.caretColor;
			this.m_CursorVerts[3].color = this.caretColor;
			vbo.AddUIVertexQuad(this.m_CursorVerts);
			if (this.m_ShouldUpdateIMEWindowPosition || currentLine != this.m_PreviousIMEInsertionLine)
			{
				this.m_ShouldUpdateIMEWindowPosition = false;
				this.m_PreviousIMEInsertionLine = currentLine;
				Camera cameraRef;
				if (this.m_TextComponent.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					cameraRef = null;
				}
				else
				{
					cameraRef = this.m_TextComponent.canvas.worldCamera;
					if (cameraRef == null)
					{
						cameraRef = Camera.current;
					}
				}
				Vector3 cursorPosition = this.m_CachedInputRenderer.gameObject.transform.TransformPoint(this.m_CursorVerts[0].position);
				Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(cameraRef, cursorPosition);
				screenPosition.y = (float)Screen.height - screenPosition.y;
				if (this.inputSystem != null)
				{
					this.inputSystem.compositionCursorPos = screenPosition;
				}
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E954 File Offset: 0x0000CB54
		private void GenerateHighlight(VertexHelper vbo, Vector2 roundingOffset)
		{
			this.UpdateMaskRegions();
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			if (textInfo.characterCount == 0)
			{
				return;
			}
			this.m_CaretPosition = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			this.m_CaretSelectPosition = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			if (this.m_SoftKeyboard != null && this.compositionLength == 0)
			{
				int stringPosition = ((this.m_CaretPosition < this.m_CaretSelectPosition) ? textInfo.characterInfo[this.m_CaretPosition].index : textInfo.characterInfo[this.m_CaretSelectPosition].index);
				int length = ((this.m_CaretPosition < this.m_CaretSelectPosition) ? (this.stringSelectPositionInternal - stringPosition) : (this.stringPositionInternal - stringPosition));
				this.m_SoftKeyboard.selection = new RangeInt(stringPosition, length);
			}
			Vector2 caretPosition;
			float height;
			if (this.m_CaretSelectPosition < textInfo.characterCount)
			{
				caretPosition = new Vector2(textInfo.characterInfo[this.m_CaretSelectPosition].origin, textInfo.characterInfo[this.m_CaretSelectPosition].descender);
				height = textInfo.characterInfo[this.m_CaretSelectPosition].ascender - textInfo.characterInfo[this.m_CaretSelectPosition].descender;
			}
			else
			{
				caretPosition = new Vector2(textInfo.characterInfo[this.m_CaretSelectPosition - 1].xAdvance, textInfo.characterInfo[this.m_CaretSelectPosition - 1].descender);
				height = textInfo.characterInfo[this.m_CaretSelectPosition - 1].ascender - textInfo.characterInfo[this.m_CaretSelectPosition - 1].descender;
			}
			this.AdjustRectTransformRelativeToViewport(caretPosition, height, true);
			int startChar = Mathf.Max(0, this.m_CaretPosition);
			int endChar = Mathf.Max(0, this.m_CaretSelectPosition);
			if (startChar > endChar)
			{
				int num = startChar;
				startChar = endChar;
				endChar = num;
			}
			endChar--;
			int currentLineIndex = textInfo.characterInfo[startChar].lineNumber;
			int nextLineStartIdx = textInfo.lineInfo[currentLineIndex].lastCharacterIndex;
			UIVertex vert = UIVertex.simpleVert;
			vert.uv0 = Vector2.zero;
			vert.color = this.selectionColor;
			int currentChar = startChar;
			while (currentChar <= endChar && currentChar < textInfo.characterCount)
			{
				if (currentChar == nextLineStartIdx || currentChar == endChar)
				{
					TMP_CharacterInfo startCharInfo = textInfo.characterInfo[startChar];
					TMP_CharacterInfo endCharInfo = textInfo.characterInfo[currentChar];
					if (currentChar > 0 && endCharInfo.character == '\n' && textInfo.characterInfo[currentChar - 1].character == '\r')
					{
						endCharInfo = textInfo.characterInfo[currentChar - 1];
					}
					Vector2 startPosition = new Vector2(startCharInfo.origin, textInfo.lineInfo[currentLineIndex].ascender);
					Vector2 endPosition = new Vector2(endCharInfo.xAdvance, textInfo.lineInfo[currentLineIndex].descender);
					int startIndex = vbo.currentVertCount;
					vert.position = new Vector3(startPosition.x, endPosition.y, 0f);
					vbo.AddVert(vert);
					vert.position = new Vector3(endPosition.x, endPosition.y, 0f);
					vbo.AddVert(vert);
					vert.position = new Vector3(endPosition.x, startPosition.y, 0f);
					vbo.AddVert(vert);
					vert.position = new Vector3(startPosition.x, startPosition.y, 0f);
					vbo.AddVert(vert);
					vbo.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
					vbo.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
					startChar = currentChar + 1;
					currentLineIndex++;
					if (currentLineIndex < textInfo.lineCount)
					{
						nextLineStartIdx = textInfo.lineInfo[currentLineIndex].lastCharacterIndex;
					}
				}
				currentChar++;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000ED40 File Offset: 0x0000CF40
		private void AdjustRectTransformRelativeToViewport(Vector2 startPosition, float height, bool isCharVisible)
		{
			if (this.m_TextViewport == null)
			{
				return;
			}
			Vector3 localPosition = base.transform.localPosition;
			Vector3 textComponentLocalPosition = this.m_TextComponent.rectTransform.localPosition;
			Vector3 textViewportLocalPosition = this.m_TextViewport.localPosition;
			Rect textViewportRect = this.m_TextViewport.rect;
			Vector2 caretPosition = new Vector2(startPosition.x + textComponentLocalPosition.x + textViewportLocalPosition.x + localPosition.x, startPosition.y + textComponentLocalPosition.y + textViewportLocalPosition.y + localPosition.y);
			Rect viewportWSRect = new Rect(localPosition.x + textViewportLocalPosition.x + textViewportRect.x, localPosition.y + textViewportLocalPosition.y + textViewportRect.y, textViewportRect.width, textViewportRect.height);
			float rightOffset = viewportWSRect.xMax - (caretPosition.x + this.m_TextComponent.margin.z + (float)this.m_CaretWidth);
			if (rightOffset < 0f && (!this.multiLine || (this.multiLine && isCharVisible)))
			{
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(rightOffset, 0f);
				this.AssignPositioningIfNeeded();
			}
			float leftOffset = caretPosition.x - this.m_TextComponent.margin.x - viewportWSRect.xMin;
			if (leftOffset < 0f)
			{
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(-leftOffset, 0f);
				this.AssignPositioningIfNeeded();
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float topOffset = viewportWSRect.yMax - (caretPosition.y + height);
				if (topOffset < -0.0001f)
				{
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, topOffset);
					this.AssignPositioningIfNeeded();
				}
				float bottomOffset = caretPosition.y - viewportWSRect.yMin;
				if (bottomOffset < 0f)
				{
					this.m_TextComponent.rectTransform.anchoredPosition -= new Vector2(0f, bottomOffset);
					this.AssignPositioningIfNeeded();
				}
			}
			if (this.m_HasTextBeenRemoved)
			{
				float anchoredPositionX = this.m_TextComponent.rectTransform.anchoredPosition.x;
				float firstCharPosition = localPosition.x + textViewportLocalPosition.x + textComponentLocalPosition.x + this.m_TextComponent.textInfo.characterInfo[0].origin - this.m_TextComponent.margin.x;
				int lastCharacterIndex = this.ClampArrayIndex(this.m_TextComponent.textInfo.characterCount - 1);
				float lastCharPosition = localPosition.x + textViewportLocalPosition.x + textComponentLocalPosition.x + this.m_TextComponent.textInfo.characterInfo[lastCharacterIndex].origin + this.m_TextComponent.margin.z + (float)this.m_CaretWidth;
				if (anchoredPositionX > 0.0001f && firstCharPosition > viewportWSRect.xMin)
				{
					float offset = viewportWSRect.xMin - firstCharPosition;
					if (anchoredPositionX < -offset)
					{
						offset = -anchoredPositionX;
					}
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(offset, 0f);
					this.AssignPositioningIfNeeded();
				}
				else if (anchoredPositionX < -0.0001f && lastCharPosition < viewportWSRect.xMax)
				{
					float offset2 = viewportWSRect.xMax - lastCharPosition;
					if (-anchoredPositionX < offset2)
					{
						offset2 = -anchoredPositionX;
					}
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(offset2, 0f);
					this.AssignPositioningIfNeeded();
				}
				this.m_HasTextBeenRemoved = false;
			}
			this.m_forceRectTransformAdjustment = false;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000F100 File Offset: 0x0000D300
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == TMP_InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == TMP_InputField.CharacterValidation.Integer || this.characterValidation == TMP_InputField.CharacterValidation.Decimal)
			{
				bool flag = pos == 0 && text.Length > 0 && text[0] == '-';
				bool selectionAtStart = this.stringPositionInternal == 0 || this.stringSelectPositionInternal == 0;
				if (!flag)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || selectionAtStart))
					{
						return ch;
					}
					string separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
					if (ch == Convert.ToChar(separator) && this.characterValidation == TMP_InputField.CharacterValidation.Decimal && !text.Contains(separator))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Digit)
			{
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Alphanumeric)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Name)
			{
				char prevChar = ((text.Length > 0) ? text[Mathf.Clamp(pos - 1, 0, text.Length - 1)] : ' ');
				char lastChar = ((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
				char nextChar = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && pos == 0)
					{
						return char.ToUpper(ch);
					}
					if (char.IsLower(ch) && (prevChar == ' ' || prevChar == '-'))
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && pos > 0 && prevChar != ' ' && prevChar != '\'' && prevChar != '-' && !char.IsLower(prevChar))
					{
						return char.ToLower(ch);
					}
					if (char.IsUpper(ch) && char.IsUpper(lastChar))
					{
						return '\0';
					}
					return ch;
				}
				else
				{
					if (ch == '\'' && lastChar != ' ' && lastChar != '\'' && nextChar != '\'' && !text.Contains("'"))
					{
						return ch;
					}
					if (char.IsLetter(prevChar) && ch == '-' && lastChar != '-')
					{
						return ch;
					}
					if ((ch == ' ' || ch == '-') && pos != 0 && prevChar != ' ' && prevChar != '\'' && prevChar != '-' && lastChar != ' ' && lastChar != '\'' && lastChar != '-' && nextChar != ' ' && nextChar != '\'' && nextChar != '-')
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.EmailAddress)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (ch == '@' && text.IndexOf('@') == -1)
				{
					return ch;
				}
				if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
				{
					return ch;
				}
				if (ch == '.')
				{
					int num = (int)((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char nextChar2 = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
					if (num != 46 && nextChar2 != '.')
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Regex)
			{
				if (Regex.IsMatch(ch.ToString(), this.m_RegexValue))
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.CustomValidator && this.m_InputValidator != null)
			{
				char c = this.m_InputValidator.Validate(ref text, ref pos, ch);
				this.m_Text = text;
				this.stringSelectPositionInternal = (this.stringPositionInternal = pos);
				return c;
			}
			return '\0';
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && this.m_SoftKeyboard != null && !this.m_SoftKeyboard.active)
			{
				this.m_SoftKeyboard.active = true;
				this.m_SoftKeyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000F520 File Offset: 0x0000D720
		private void ActivateInputFieldInternal()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			if (EventSystem.current.currentSelectedGameObject != base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
			this.m_TouchKeyboardAllowsInPlaceEditing = !TMP_InputField.s_IsQuestDevice && TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (this.TouchScreenKeyboardShouldBeUsed() && !this.shouldHideSoftKeyboard)
			{
				if (this.inputSystem != null && this.inputSystem.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				if (!this.shouldHideSoftKeyboard && !this.m_ReadOnly)
				{
					this.m_SoftKeyboard = ((this.inputType == TMP_InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true, this.isAlert, "", this.characterLimit) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == TMP_InputField.InputType.AutoCorrect, this.multiLine, false, this.isAlert, "", this.characterLimit));
					this.OnFocus();
					if (this.m_SoftKeyboard != null && this.m_SoftKeyboard.canSetSelection)
					{
						int length = ((this.stringPositionInternal < this.stringSelectPositionInternal) ? (this.stringSelectPositionInternal - this.stringPositionInternal) : (this.stringPositionInternal - this.stringSelectPositionInternal));
						this.m_SoftKeyboard.selection = new RangeInt((this.stringPositionInternal < this.stringSelectPositionInternal) ? this.stringPositionInternal : this.stringSelectPositionInternal, length);
					}
				}
			}
			else
			{
				if (!this.TouchScreenKeyboardShouldBeUsed() && !this.m_ReadOnly && this.inputSystem != null)
				{
					this.inputSystem.imeCompositionMode = IMECompositionMode.On;
				}
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000F707 File Offset: 0x0000D907
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.SendOnFocus();
			if (this.shouldActivateOnSelect)
			{
				this.ActivateInputField();
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000F724 File Offset: 0x0000D924
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00002AAB File Offset: 0x00000CAB
		public void OnControlClick()
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000F735 File Offset: 0x0000D935
		public void ReleaseSelection()
		{
			this.m_SelectionStillActive = false;
			this.m_ReleaseSelection = false;
			this.m_PreviouslySelectedObject = null;
			this.MarkGeometryAsDirty();
			this.SendOnEndEdit();
			this.SendOnEndTextSelection();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000F760 File Offset: 0x0000D960
		public void DeactivateInputField(bool clearSelection = false)
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_HasDoneFocusTransition = false;
			this.m_AllowInput = false;
			if (this.m_Placeholder != null)
			{
				this.m_Placeholder.enabled = string.IsNullOrEmpty(this.m_Text);
			}
			if (this.m_TextComponent != null && this.IsInteractable())
			{
				if (this.m_WasCanceled && this.m_RestoreOriginalTextOnEscape && !this.m_IsKeyboardBeingClosedInHoloLens)
				{
					this.text = this.m_OriginalText;
				}
				if (this.m_SoftKeyboard != null)
				{
					this.m_SoftKeyboard.active = false;
					this.m_SoftKeyboard = null;
				}
				this.m_SelectionStillActive = true;
				if ((this.m_ResetOnDeActivation || this.m_ReleaseSelection || clearSelection) && this.m_VerticalScrollbar == null)
				{
					this.ReleaseSelection();
				}
				if (this.inputSystem != null)
				{
					this.inputSystem.imeCompositionMode = IMECompositionMode.Auto;
				}
				this.m_IsKeyboardBeingClosedInHoloLens = false;
			}
			this.MarkGeometryAsDirty();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000F85A File Offset: 0x0000DA5A
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField(false);
			base.OnDeselect(eventData);
			this.SendOnFocusLost();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000F870 File Offset: 0x0000DA70
		public virtual void OnSubmit(BaseEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (!this.isFocused)
			{
				this.m_ShouldActivateNextUpdate = true;
			}
			this.SendOnSubmit();
			this.DeactivateInputField(false);
			if (eventData != null)
			{
				eventData.Use();
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public virtual void OnCancel(BaseEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (!this.isFocused)
			{
				this.m_ShouldActivateNextUpdate = true;
			}
			this.m_WasCanceled = true;
			this.DeactivateInputField(false);
			eventData.Use();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000F8DE File Offset: 0x0000DADE
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.m_AllowInput)
			{
				base.OnMove(eventData);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case TMP_InputField.ContentType.Standard:
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.Autocorrected:
				this.m_InputType = TMP_InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.IntegerNumber:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Integer;
				break;
			case TMP_InputField.ContentType.DecimalNumber:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Decimal;
				break;
			case TMP_InputField.ContentType.Alphanumeric:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Alphanumeric;
				break;
			case TMP_InputField.ContentType.Name:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Name;
				break;
			case TMP_InputField.ContentType.EmailAddress:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.EmailAddress;
				break;
			case TMP_InputField.ContentType.Password:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.Pin:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Digit;
				break;
			}
			this.SetTextComponentWrapMode();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000FA43 File Offset: 0x0000DC43
		private void SetTextComponentWrapMode()
		{
			if (this.m_TextComponent == null)
			{
				return;
			}
			if (this.multiLine)
			{
				this.m_TextComponent.textWrappingMode = TextWrappingModes.Normal;
				return;
			}
			this.m_TextComponent.textWrappingMode = TextWrappingModes.PreserveWhitespaceNoWrap;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000FA75 File Offset: 0x0000DC75
		private void SetTextComponentRichTextMode()
		{
			if (this.m_TextComponent == null)
			{
				return;
			}
			this.m_TextComponent.richText = this.m_RichText;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000FA98 File Offset: 0x0000DC98
		private void SetToCustomIfContentTypeIsNot(params TMP_InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			for (int i = 0; i < allowedContentTypes.Length; i++)
			{
				if (this.contentType == allowedContentTypes[i])
				{
					return;
				}
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000FAD2 File Offset: 0x0000DCD2
		private void SetToCustom()
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000FAE7 File Offset: 0x0000DCE7
		private void SetToCustom(TMP_InputField.CharacterValidation characterValidation)
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000FB02 File Offset: 0x0000DD02
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.m_HasDoneFocusTransition)
			{
				state = Selectable.SelectionState.Selected;
			}
			else if (state == Selectable.SelectionState.Pressed)
			{
				this.m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00002AAB File Offset: 0x00000CAB
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000FB24 File Offset: 0x0000DD24
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		public virtual float preferredWidth
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				float horizontalPadding = 0f;
				if (this.m_LayoutGroup != null)
				{
					horizontalPadding = (float)this.m_LayoutGroup.padding.horizontal;
				}
				if (this.m_TextViewport != null)
				{
					horizontalPadding += this.m_TextViewport.offsetMin.x - this.m_TextViewport.offsetMax.x;
				}
				return this.m_TextComponent.preferredWidth + horizontalPadding;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000FBB2 File Offset: 0x0000DDB2
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000FB24 File Offset: 0x0000DD24
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		public virtual float preferredHeight
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				float verticalPadding = 0f;
				if (this.m_LayoutGroup != null)
				{
					verticalPadding = (float)this.m_LayoutGroup.padding.vertical;
				}
				if (this.m_TextViewport != null)
				{
					verticalPadding += this.m_TextViewport.offsetMin.y - this.m_TextViewport.offsetMax.y;
				}
				return this.m_TextComponent.preferredHeight + verticalPadding;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000FBB2 File Offset: 0x0000DDB2
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000FC42 File Offset: 0x0000DE42
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000FC48 File Offset: 0x0000DE48
		public void SetGlobalPointSize(float pointSize)
		{
			TMP_Text placeholderTextComponent = this.m_Placeholder as TMP_Text;
			if (placeholderTextComponent != null)
			{
				placeholderTextComponent.fontSize = pointSize;
			}
			this.textComponent.fontSize = pointSize;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000FC80 File Offset: 0x0000DE80
		public void SetGlobalFontAsset(TMP_FontAsset fontAsset)
		{
			TMP_Text placeholderTextComponent = this.m_Placeholder as TMP_Text;
			if (placeholderTextComponent != null)
			{
				placeholderTextComponent.font = fontAsset;
			}
			this.textComponent.font = fontAsset;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000FCD3 File Offset: 0x0000DED3
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000188 RID: 392
		protected TouchScreenKeyboard m_SoftKeyboard;

		// Token: 0x04000189 RID: 393
		private static readonly char[] kSeparators = new char[] { ' ', '.', ',', '\t', '\r', '\n' };

		// Token: 0x0400018A RID: 394
		private static bool s_IsQuestDevice = false;

		// Token: 0x0400018B RID: 395
		protected RectTransform m_RectTransform;

		// Token: 0x0400018C RID: 396
		[SerializeField]
		protected RectTransform m_TextViewport;

		// Token: 0x0400018D RID: 397
		protected RectMask2D m_TextComponentRectMask;

		// Token: 0x0400018E RID: 398
		protected RectMask2D m_TextViewportRectMask;

		// Token: 0x0400018F RID: 399
		[SerializeField]
		protected TMP_Text m_TextComponent;

		// Token: 0x04000190 RID: 400
		protected RectTransform m_TextComponentRectTransform;

		// Token: 0x04000191 RID: 401
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x04000192 RID: 402
		[SerializeField]
		protected Scrollbar m_VerticalScrollbar;

		// Token: 0x04000193 RID: 403
		[SerializeField]
		protected TMP_ScrollbarEventHandler m_VerticalScrollbarEventHandler;

		// Token: 0x04000194 RID: 404
		private bool m_IsDrivenByLayoutComponents;

		// Token: 0x04000195 RID: 405
		[SerializeField]
		private LayoutGroup m_LayoutGroup;

		// Token: 0x04000196 RID: 406
		private IScrollHandler m_IScrollHandlerParent;

		// Token: 0x04000197 RID: 407
		private float m_ScrollPosition;

		// Token: 0x04000198 RID: 408
		[SerializeField]
		protected float m_ScrollSensitivity = 1f;

		// Token: 0x04000199 RID: 409
		[SerializeField]
		private TMP_InputField.ContentType m_ContentType;

		// Token: 0x0400019A RID: 410
		[SerializeField]
		private TMP_InputField.InputType m_InputType;

		// Token: 0x0400019B RID: 411
		[SerializeField]
		private char m_AsteriskChar = '*';

		// Token: 0x0400019C RID: 412
		[SerializeField]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x0400019D RID: 413
		[SerializeField]
		private TMP_InputField.LineType m_LineType;

		// Token: 0x0400019E RID: 414
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x0400019F RID: 415
		[SerializeField]
		private bool m_HideSoftKeyboard;

		// Token: 0x040001A0 RID: 416
		[SerializeField]
		private TMP_InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x040001A1 RID: 417
		[SerializeField]
		private string m_RegexValue = string.Empty;

		// Token: 0x040001A2 RID: 418
		[SerializeField]
		private float m_GlobalPointSize = 14f;

		// Token: 0x040001A3 RID: 419
		[SerializeField]
		private int m_CharacterLimit;

		// Token: 0x040001A4 RID: 420
		[SerializeField]
		private TMP_InputField.SubmitEvent m_OnEndEdit = new TMP_InputField.SubmitEvent();

		// Token: 0x040001A5 RID: 421
		[SerializeField]
		private TMP_InputField.SubmitEvent m_OnSubmit = new TMP_InputField.SubmitEvent();

		// Token: 0x040001A6 RID: 422
		[SerializeField]
		private TMP_InputField.SelectionEvent m_OnSelect = new TMP_InputField.SelectionEvent();

		// Token: 0x040001A7 RID: 423
		[SerializeField]
		private TMP_InputField.SelectionEvent m_OnDeselect = new TMP_InputField.SelectionEvent();

		// Token: 0x040001A8 RID: 424
		[SerializeField]
		private TMP_InputField.TextSelectionEvent m_OnTextSelection = new TMP_InputField.TextSelectionEvent();

		// Token: 0x040001A9 RID: 425
		[SerializeField]
		private TMP_InputField.TextSelectionEvent m_OnEndTextSelection = new TMP_InputField.TextSelectionEvent();

		// Token: 0x040001AA RID: 426
		[SerializeField]
		private TMP_InputField.OnChangeEvent m_OnValueChanged = new TMP_InputField.OnChangeEvent();

		// Token: 0x040001AB RID: 427
		[SerializeField]
		private TMP_InputField.TouchScreenKeyboardEvent m_OnTouchScreenKeyboardStatusChanged = new TMP_InputField.TouchScreenKeyboardEvent();

		// Token: 0x040001AC RID: 428
		[SerializeField]
		private TMP_InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x040001AD RID: 429
		[SerializeField]
		private Color m_CaretColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x040001AE RID: 430
		[SerializeField]
		private bool m_CustomCaretColor;

		// Token: 0x040001AF RID: 431
		[SerializeField]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x040001B0 RID: 432
		[SerializeField]
		[TextArea(5, 10)]
		protected string m_Text = string.Empty;

		// Token: 0x040001B1 RID: 433
		[SerializeField]
		[Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		// Token: 0x040001B2 RID: 434
		[SerializeField]
		[Range(1f, 5f)]
		private int m_CaretWidth = 1;

		// Token: 0x040001B3 RID: 435
		[SerializeField]
		private bool m_ReadOnly;

		// Token: 0x040001B4 RID: 436
		[SerializeField]
		private bool m_RichText = true;

		// Token: 0x040001B5 RID: 437
		protected int m_StringPosition;

		// Token: 0x040001B6 RID: 438
		protected int m_StringSelectPosition;

		// Token: 0x040001B7 RID: 439
		protected int m_CaretPosition;

		// Token: 0x040001B8 RID: 440
		protected int m_CaretSelectPosition;

		// Token: 0x040001B9 RID: 441
		private RectTransform caretRectTrans;

		// Token: 0x040001BA RID: 442
		protected UIVertex[] m_CursorVerts;

		// Token: 0x040001BB RID: 443
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x040001BC RID: 444
		private Vector2 m_LastPosition;

		// Token: 0x040001BD RID: 445
		[NonSerialized]
		protected Mesh m_Mesh;

		// Token: 0x040001BE RID: 446
		private bool m_AllowInput;

		// Token: 0x040001BF RID: 447
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x040001C0 RID: 448
		private bool m_UpdateDrag;

		// Token: 0x040001C1 RID: 449
		private bool m_DragPositionOutOfBounds;

		// Token: 0x040001C2 RID: 450
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x040001C3 RID: 451
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x040001C4 RID: 452
		protected bool m_CaretVisible;

		// Token: 0x040001C5 RID: 453
		private Coroutine m_BlinkCoroutine;

		// Token: 0x040001C6 RID: 454
		private float m_BlinkStartTime;

		// Token: 0x040001C7 RID: 455
		private Coroutine m_DragCoroutine;

		// Token: 0x040001C8 RID: 456
		private string m_OriginalText = "";

		// Token: 0x040001C9 RID: 457
		private bool m_WasCanceled;

		// Token: 0x040001CA RID: 458
		private bool m_HasDoneFocusTransition;

		// Token: 0x040001CB RID: 459
		private WaitForSecondsRealtime m_WaitForSecondsRealtime;

		// Token: 0x040001CC RID: 460
		private bool m_PreventCallback;

		// Token: 0x040001CD RID: 461
		private bool m_TouchKeyboardAllowsInPlaceEditing;

		// Token: 0x040001CE RID: 462
		private bool m_IsTextComponentUpdateRequired;

		// Token: 0x040001CF RID: 463
		private bool m_HasTextBeenRemoved;

		// Token: 0x040001D0 RID: 464
		private float m_PointerDownClickStartTime;

		// Token: 0x040001D1 RID: 465
		private float m_KeyDownStartTime;

		// Token: 0x040001D2 RID: 466
		private float m_DoubleClickDelay = 0.5f;

		// Token: 0x040001D3 RID: 467
		private bool m_IsApplePlatform;

		// Token: 0x040001D4 RID: 468
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x040001D5 RID: 469
		private const string kOculusQuestDeviceModel = "Oculus Quest";

		// Token: 0x040001D6 RID: 470
		private bool m_IsCompositionActive;

		// Token: 0x040001D7 RID: 471
		private bool m_ShouldUpdateIMEWindowPosition;

		// Token: 0x040001D8 RID: 472
		private int m_PreviousIMEInsertionLine;

		// Token: 0x040001D9 RID: 473
		[SerializeField]
		protected TMP_FontAsset m_GlobalFontAsset;

		// Token: 0x040001DA RID: 474
		[SerializeField]
		protected bool m_OnFocusSelectAll = true;

		// Token: 0x040001DB RID: 475
		protected bool m_isSelectAll;

		// Token: 0x040001DC RID: 476
		[SerializeField]
		protected bool m_ResetOnDeActivation = true;

		// Token: 0x040001DD RID: 477
		private bool m_SelectionStillActive;

		// Token: 0x040001DE RID: 478
		private bool m_ReleaseSelection;

		// Token: 0x040001DF RID: 479
		private KeyCode m_LastKeyCode;

		// Token: 0x040001E0 RID: 480
		private GameObject m_PreviouslySelectedObject;

		// Token: 0x040001E1 RID: 481
		[SerializeField]
		private bool m_KeepTextSelectionVisible;

		// Token: 0x040001E2 RID: 482
		[SerializeField]
		private bool m_RestoreOriginalTextOnEscape = true;

		// Token: 0x040001E3 RID: 483
		[SerializeField]
		protected bool m_isRichTextEditingAllowed;

		// Token: 0x040001E4 RID: 484
		[SerializeField]
		protected int m_LineLimit;

		// Token: 0x040001E5 RID: 485
		public bool isAlert;

		// Token: 0x040001E6 RID: 486
		[SerializeField]
		protected TMP_InputValidator m_InputValidator;

		// Token: 0x040001E7 RID: 487
		[SerializeField]
		private bool m_ShouldActivateOnSelect = true;

		// Token: 0x040001E8 RID: 488
		private bool m_isSelected;

		// Token: 0x040001E9 RID: 489
		private bool m_IsStringPositionDirty;

		// Token: 0x040001EA RID: 490
		private bool m_IsCaretPositionDirty;

		// Token: 0x040001EB RID: 491
		private bool m_forceRectTransformAdjustment;

		// Token: 0x040001EC RID: 492
		private bool m_IsKeyboardBeingClosedInHoloLens;

		// Token: 0x040001ED RID: 493
		private Event m_ProcessingEvent = new Event();

		// Token: 0x0200004C RID: 76
		public enum ContentType
		{
			// Token: 0x040001EF RID: 495
			Standard,
			// Token: 0x040001F0 RID: 496
			Autocorrected,
			// Token: 0x040001F1 RID: 497
			IntegerNumber,
			// Token: 0x040001F2 RID: 498
			DecimalNumber,
			// Token: 0x040001F3 RID: 499
			Alphanumeric,
			// Token: 0x040001F4 RID: 500
			Name,
			// Token: 0x040001F5 RID: 501
			EmailAddress,
			// Token: 0x040001F6 RID: 502
			Password,
			// Token: 0x040001F7 RID: 503
			Pin,
			// Token: 0x040001F8 RID: 504
			Custom
		}

		// Token: 0x0200004D RID: 77
		public enum InputType
		{
			// Token: 0x040001FA RID: 506
			Standard,
			// Token: 0x040001FB RID: 507
			AutoCorrect,
			// Token: 0x040001FC RID: 508
			Password
		}

		// Token: 0x0200004E RID: 78
		public enum CharacterValidation
		{
			// Token: 0x040001FE RID: 510
			None,
			// Token: 0x040001FF RID: 511
			Digit,
			// Token: 0x04000200 RID: 512
			Integer,
			// Token: 0x04000201 RID: 513
			Decimal,
			// Token: 0x04000202 RID: 514
			Alphanumeric,
			// Token: 0x04000203 RID: 515
			Name,
			// Token: 0x04000204 RID: 516
			Regex,
			// Token: 0x04000205 RID: 517
			EmailAddress,
			// Token: 0x04000206 RID: 518
			CustomValidator
		}

		// Token: 0x0200004F RID: 79
		public enum LineType
		{
			// Token: 0x04000208 RID: 520
			SingleLine,
			// Token: 0x04000209 RID: 521
			MultiLineSubmit,
			// Token: 0x0400020A RID: 522
			MultiLineNewline
		}

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x060002F2 RID: 754
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		// Token: 0x02000051 RID: 81
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
		}

		// Token: 0x02000052 RID: 82
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
		}

		// Token: 0x02000053 RID: 83
		[Serializable]
		public class SelectionEvent : UnityEvent<string>
		{
		}

		// Token: 0x02000054 RID: 84
		[Serializable]
		public class TextSelectionEvent : UnityEvent<string, int, int>
		{
		}

		// Token: 0x02000055 RID: 85
		[Serializable]
		public class TouchScreenKeyboardEvent : UnityEvent<TouchScreenKeyboard.Status>
		{
		}

		// Token: 0x02000056 RID: 86
		protected enum EditState
		{
			// Token: 0x0400020C RID: 524
			Continue,
			// Token: 0x0400020D RID: 525
			Finish
		}
	}
}
