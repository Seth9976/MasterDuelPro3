using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200002E RID: 46
	[AddComponentMenu("UI/Legacy/Input Field", 103)]
	public class InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement, ILayoutElement
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000098DF File Offset: 0x00007ADF
		private BaseInput input
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000990F File Offset: 0x00007B0F
		private string compositionString
		{
			get
			{
				if (!(this.input != null))
				{
					return Input.compositionString;
				}
				return this.input.compositionString;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009930 File Offset: 0x00007B30
		protected InputField()
		{
			this.EnforceTextHOverflow();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000099EA File Offset: 0x00007BEA
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009A0B File Offset: 0x00007C0B
		protected TextGenerator cachedInputTextGenerator
		{
			get
			{
				if (this.m_InputTextCache == null)
				{
					this.m_InputTextCache = new TextGenerator();
				}
				return this.m_InputTextCache;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009A38 File Offset: 0x00007C38
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00009A26 File Offset: 0x00007C26
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform != RuntimePlatform.tvOS) || this.m_HideMobileInput;
			}
			set
			{
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00009A6A File Offset: 0x00007C6A
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009A61 File Offset: 0x00007C61
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009A82 File Offset: 0x00007C82
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009A8A File Offset: 0x00007C8A
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

		// Token: 0x06000196 RID: 406 RVA: 0x00009A94 File Offset: 0x00007C94
		public void SetTextWithoutNotify(string input)
		{
			this.SetText(input, false);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009AA0 File Offset: 0x00007CA0
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
			if (this.m_LineType == InputField.LineType.SingleLine)
			{
				value = value.Replace("\n", "").Replace("\t", "");
			}
			if (this.onValidateInput != null || this.characterValidation != InputField.CharacterValidation.None)
			{
				this.m_Text = "";
				InputField.OnValidateInput validatorMethod = this.onValidateInput ?? new InputField.OnValidateInput(this.Validate);
				this.m_CaretPosition = (this.m_CaretSelectPosition = value.Length);
				int charactersToCheck = ((this.characterLimit > 0) ? Math.Min(this.characterLimit, value.Length) : value.Length);
				for (int i = 0; i < charactersToCheck; i++)
				{
					char c = validatorMethod(this.m_Text, this.m_Text.Length, value[i]);
					if (c != '\0')
					{
						this.m_Text += c.ToString();
					}
				}
			}
			else
			{
				this.m_Text = ((this.characterLimit > 0 && value.Length > this.characterLimit) ? value.Substring(0, this.characterLimit) : value);
			}
			if (this.m_Keyboard != null)
			{
				this.m_Keyboard.text = this.m_Text;
			}
			if (this.m_CaretPosition > this.m_Text.Length)
			{
				this.m_CaretPosition = (this.m_CaretSelectPosition = this.m_Text.Length);
			}
			else if (this.m_CaretSelectPosition > this.m_Text.Length)
			{
				this.m_CaretSelectPosition = this.m_Text.Length;
			}
			if (sendCallback)
			{
				this.SendOnValueChanged();
			}
			this.UpdateLabel();
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00009C60 File Offset: 0x00007E60
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009C68 File Offset: 0x00007E68
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00009C70 File Offset: 0x00007E70
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00009C8E File Offset: 0x00007E8E
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00009C96 File Offset: 0x00007E96
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009CAC File Offset: 0x00007EAC
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				if (this.m_TextComponent != null)
				{
					this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
					this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
					this.m_TextComponent.UnregisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
				}
				if (SetPropertyUtility.SetClass<Text>(ref this.m_TextComponent, value))
				{
					this.EnforceTextHOverflow();
					if (this.m_TextComponent != null)
					{
						this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
						this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
						this.m_TextComponent.RegisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
					}
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00009D7B File Offset: 0x00007F7B
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00009D83 File Offset: 0x00007F83
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009D92 File Offset: 0x00007F92
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00009DAE File Offset: 0x00007FAE
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00009DC4 File Offset: 0x00007FC4
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00009DCC File Offset: 0x00007FCC
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00009DE4 File Offset: 0x00007FE4
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00009DEC File Offset: 0x00007FEC
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00009E02 File Offset: 0x00008002
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00009E0A File Offset: 0x0000800A
		public InputField.EndEditEvent onEndEdit
		{
			get
			{
				return this.m_OnDidEndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.EndEditEvent>(ref this.m_OnDidEndEdit, value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009E19 File Offset: 0x00008019
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009E21 File Offset: 0x00008021
		public InputField.SubmitEvent onSubmit
		{
			get
			{
				return this.m_OnSubmit;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.SubmitEvent>(ref this.m_OnSubmit, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009E30 File Offset: 0x00008030
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00009E38 File Offset: 0x00008038
		[Obsolete("onValueChange has been renamed to onValueChanged")]
		public InputField.OnChangeEvent onValueChange
		{
			get
			{
				return this.onValueChanged;
			}
			set
			{
				this.onValueChanged = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009E41 File Offset: 0x00008041
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00009E49 File Offset: 0x00008049
		public InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnChangeEvent>(ref this.m_OnValueChanged, value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009E58 File Offset: 0x00008058
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00009E60 File Offset: 0x00008060
		public InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009E6F File Offset: 0x0000806F
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00009E77 File Offset: 0x00008077
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
					if (this.m_Keyboard != null)
					{
						this.m_Keyboard.characterLimit = value;
					}
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00009EA7 File Offset: 0x000080A7
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00009EAF File Offset: 0x000080AF
		public InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00009EC5 File Offset: 0x000080C5
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00009ECD File Offset: 0x000080CD
		public InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new InputField.ContentType[]
					{
						InputField.ContentType.Standard,
						InputField.ContentType.Autocorrected
					});
					this.EnforceTextHOverflow();
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009EF3 File Offset: 0x000080F3
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00009EFB File Offset: 0x000080FB
		public InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00009F11 File Offset: 0x00008111
		public TouchScreenKeyboard touchScreenKeyboard
		{
			get
			{
				return this.m_Keyboard;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009F19 File Offset: 0x00008119
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009F21 File Offset: 0x00008121
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009F37 File Offset: 0x00008137
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009F3F File Offset: 0x0000813F
		public InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009F55 File Offset: 0x00008155
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00009F5D File Offset: 0x0000815D
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00009F66 File Offset: 0x00008166
		public bool multiLine
		{
			get
			{
				return this.m_LineType == InputField.LineType.MultiLineNewline || this.lineType == InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00009F7C File Offset: 0x0000817C
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00009F84 File Offset: 0x00008184
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00009F9A File Offset: 0x0000819A
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009FA2 File Offset: 0x000081A2
		protected void ClampPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
				return;
			}
			if (pos > this.text.Length)
			{
				pos = this.text.Length;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00009FC9 File Offset: 0x000081C9
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00009FDD File Offset: 0x000081DD
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + this.compositionString.Length;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00009FF2 File Offset: 0x000081F2
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000A006 File Offset: 0x00008206
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A01B File Offset: 0x0000821B
		private bool hasSelection
		{
			get
			{
				return this.caretPositionInternal != this.caretSelectPositionInternal;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00009FF2 File Offset: 0x000081F2
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000A02E File Offset: 0x0000822E
		public int caretPosition
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00009FC9 File Offset: 0x000081C9
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000A03E File Offset: 0x0000823E
		public int selectionAnchorPosition
		{
			get
			{
				return this.m_CaretPosition + this.compositionString.Length;
			}
			set
			{
				if (this.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00009FF2 File Offset: 0x000081F2
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000A061 File Offset: 0x00008261
		public int selectionFocusPosition
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				if (this.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A084 File Offset: 0x00008284
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			this.m_DrawStart = 0;
			this.m_DrawEnd = this.m_Text.Length;
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				this.m_TextComponent.RegisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
				this.UpdateLabel();
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A14C File Offset: 0x0000834C
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField();
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				this.m_TextComponent.UnregisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
			}
			CanvasUpdateRegistry.DisableCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.Clear();
			}
			if (this.m_Mesh != null)
			{
				Object.DestroyImmediate(this.m_Mesh);
			}
			this.m_Mesh = null;
			base.OnDisable();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A1FE File Offset: 0x000083FE
		protected override void OnDestroy()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			base.OnDestroy();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A20C File Offset: 0x0000840C
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while (this.isFocused && this.m_CaretBlinkRate > 0f)
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

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A21B File Offset: 0x0000841B
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

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A23E File Offset: 0x0000843E
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

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A278 File Offset: 0x00008478
		private void UpdateCaretMaterial()
		{
			if (this.m_TextComponent != null && this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A2B6 File Offset: 0x000084B6
		protected void OnFocus()
		{
			this.SelectAll();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A2BE File Offset: 0x000084BE
		protected void SelectAll()
		{
			this.caretPositionInternal = this.text.Length;
			this.caretSelectPositionInternal = 0;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A2D8 File Offset: 0x000084D8
		public void MoveTextEnd(bool shift)
		{
			int position = this.text.Length;
			if (shift)
			{
				this.caretSelectPositionInternal = position;
			}
			else
			{
				this.caretPositionInternal = position;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A318 File Offset: 0x00008518
		public void MoveTextStart(bool shift)
		{
			int position = 0;
			if (shift)
			{
				this.caretSelectPositionInternal = position;
			}
			else
			{
				this.caretPositionInternal = position;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A34C File Offset: 0x0000854C
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000A353 File Offset: 0x00008553
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

		// Token: 0x060001DD RID: 477 RVA: 0x0000A35C File Offset: 0x0000855C
		private bool TouchScreenKeyboardShouldBeUsed()
		{
			RuntimePlatform platform = Application.platform;
			if (platform != RuntimePlatform.Android)
			{
				if (platform != RuntimePlatform.WebGLPlayer)
				{
					return TouchScreenKeyboard.isSupported;
				}
				return !TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
			else
			{
				if (InputField.s_IsQuestDevice)
				{
					return TouchScreenKeyboard.isSupported;
				}
				return !TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A39F File Offset: 0x0000859F
		private bool InPlaceEditing()
		{
			return !TouchScreenKeyboard.isSupported || this.m_TouchKeyboardAllowsInPlaceEditing;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A3B0 File Offset: 0x000085B0
		private bool InPlaceEditingChanged()
		{
			return !InputField.s_IsQuestDevice && this.m_TouchKeyboardAllowsInPlaceEditing != TouchScreenKeyboard.isInPlaceEditingAllowed;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A3CC File Offset: 0x000085CC
		private RangeInt GetInternalSelection()
		{
			int num = Mathf.Min(this.caretSelectPositionInternal, this.caretPositionInternal);
			int selectionLength = Mathf.Abs(this.caretSelectPositionInternal - this.caretPositionInternal);
			return new RangeInt(num, selectionLength);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A404 File Offset: 0x00008604
		private void UpdateKeyboardCaret()
		{
			if (this.m_HideMobileInput && this.m_Keyboard != null && this.m_Keyboard.canSetSelection && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS))
			{
				this.m_Keyboard.selection = this.GetInternalSelection();
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A450 File Offset: 0x00008650
		private void UpdateCaretFromKeyboard()
		{
			RangeInt selectionRange = this.m_Keyboard.selection;
			int selectionStart = selectionRange.start;
			int selectionEnd = selectionRange.end;
			bool caretChanged = false;
			if (this.caretPositionInternal != selectionStart)
			{
				caretChanged = true;
				this.caretPositionInternal = selectionStart;
			}
			if (this.caretSelectPositionInternal != selectionEnd)
			{
				this.caretSelectPositionInternal = selectionEnd;
				caretChanged = true;
			}
			if (caretChanged)
			{
				this.m_BlinkStartTime = Time.unscaledTime;
				this.UpdateLabel();
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A4B4 File Offset: 0x000086B4
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
			this.AssignPositioningIfNeeded();
			if (this.isFocused && this.InPlaceEditingChanged())
			{
				if (this.m_CachedInputRenderer != null)
				{
					using (VertexHelper helper = new VertexHelper())
					{
						helper.FillMesh(this.mesh);
					}
					this.m_CachedInputRenderer.SetMesh(this.mesh);
				}
				this.DeactivateInputField();
			}
			if (!this.isFocused || this.InPlaceEditing())
			{
				return;
			}
			if (this.m_Keyboard == null || this.m_Keyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_Keyboard != null)
				{
					if (!this.m_ReadOnly)
					{
						this.text = this.m_Keyboard.text;
					}
					if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Canceled)
					{
						this.m_WasCanceled = true;
					}
					else if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Done)
					{
						this.SendOnSubmit();
					}
				}
				this.OnDeselect(null);
				return;
			}
			string val = this.m_Keyboard.text;
			if (this.m_Text != val)
			{
				if (this.m_ReadOnly)
				{
					this.m_Keyboard.text = this.m_Text;
				}
				else
				{
					this.m_Text = "";
					foreach (char c in val)
					{
						if (c == '\r' || c == '\u0003')
						{
							c = '\n';
						}
						if (this.onValidateInput != null)
						{
							c = this.onValidateInput(this.m_Text, this.m_Text.Length, c);
						}
						else if (this.characterValidation != InputField.CharacterValidation.None)
						{
							c = this.Validate(this.m_Text, this.m_Text.Length, c);
						}
						if (this.lineType != InputField.LineType.MultiLineNewline && c == '\n')
						{
							this.UpdateLabel();
							this.SendOnSubmit();
							this.OnDeselect(null);
							return;
						}
						if (c != '\0')
						{
							this.m_Text += c.ToString();
						}
					}
					if (this.characterLimit > 0 && this.m_Text.Length > this.characterLimit)
					{
						this.m_Text = this.m_Text.Substring(0, this.characterLimit);
					}
					if (this.m_Keyboard.canGetSelection)
					{
						this.UpdateCaretFromKeyboard();
					}
					else
					{
						this.caretPositionInternal = (this.caretSelectPositionInternal = this.m_Text.Length);
					}
					if (this.m_Text != val)
					{
						this.m_Keyboard.text = this.m_Text;
					}
					this.SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (this.m_HideMobileInput && this.m_Keyboard != null && this.m_Keyboard.canSetSelection && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.tvOS)
			{
				this.m_Keyboard.selection = this.GetInternalSelection();
			}
			else if (this.m_Keyboard != null && this.m_Keyboard.canGetSelection)
			{
				this.UpdateCaretFromKeyboard();
			}
			if (this.m_Keyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					this.m_WasCanceled = true;
				}
				else if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Done)
				{
					this.SendOnSubmit();
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A7E8 File Offset: 0x000089E8
		[Obsolete("This function is no longer used. Please use RectTransformUtility.ScreenPointToLocalPointInRectangle() instead.")]
		public Vector2 ScreenToLocal(Vector2 screen)
		{
			Canvas theCanvas = this.m_TextComponent.canvas;
			if (theCanvas == null)
			{
				return screen;
			}
			Vector3 pos = Vector3.zero;
			if (theCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				pos = this.m_TextComponent.transform.InverseTransformPoint(screen);
			}
			else if (theCanvas.worldCamera != null)
			{
				Ray mouseRay = theCanvas.worldCamera.ScreenPointToRay(screen);
				Plane plane = new Plane(this.m_TextComponent.transform.forward, this.m_TextComponent.transform.position);
				float dist;
				plane.Raycast(mouseRay, out dist);
				pos = this.m_TextComponent.transform.InverseTransformPoint(mouseRay.GetPoint(dist));
			}
			return new Vector2(pos.x, pos.y);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		private int GetUnclampedCharacterLineFromPosition(Vector2 pos, TextGenerator generator)
		{
			if (!this.multiLine)
			{
				return 0;
			}
			float y = pos.y * this.m_TextComponent.pixelsPerUnit;
			float lastBottomY = 0f;
			int i = 0;
			while (i < generator.lineCount)
			{
				float topY = generator.lines[i].topY;
				float bottomY = topY - (float)generator.lines[i].height;
				if (y > topY)
				{
					float leading = topY - lastBottomY;
					if (y > topY - 0.5f * leading)
					{
						return i - 1;
					}
					return i;
				}
				else
				{
					if (y > bottomY)
					{
						return i;
					}
					lastBottomY = bottomY;
					i++;
				}
			}
			return generator.lineCount;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A948 File Offset: 0x00008B48
		protected int GetCharacterIndexFromPosition(Vector2 pos)
		{
			TextGenerator gen = this.m_TextComponent.cachedTextGenerator;
			if (gen.lineCount == 0)
			{
				return 0;
			}
			int line = this.GetUnclampedCharacterLineFromPosition(pos, gen);
			if (line < 0)
			{
				return 0;
			}
			if (line >= gen.lineCount)
			{
				return gen.characterCountVisible;
			}
			int startCharIdx = gen.lines[line].startCharIdx;
			int endCharIndex = InputField.GetLineEndPosition(gen, line);
			int i = startCharIdx;
			while (i < endCharIndex && i < gen.characterCountVisible)
			{
				UICharInfo charInfo = gen.characters[i];
				Vector2 charPos = charInfo.cursorPos / this.m_TextComponent.pixelsPerUnit;
				float num = pos.x - charPos.x;
				float distToCharEnd = charPos.x + charInfo.charWidth / this.m_TextComponent.pixelsPerUnit - pos.x;
				if (num < distToCharEnd)
				{
					return i;
				}
				i++;
			}
			return endCharIndex;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000AA17 File Offset: 0x00008C17
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && (this.InPlaceEditing() || this.m_HideMobileInput);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000AA51 File Offset: 0x00008C51
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000AA64 File Offset: 0x00008C64
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			Vector2 position = Vector2.zero;
			if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position))
			{
				return;
			}
			Vector2 localMousePos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, position, eventData.pressEventCamera, out localMousePos);
			this.caretSelectPositionInternal = this.GetCharacterIndexFromPosition(localMousePos) + this.m_DrawStart;
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			this.UpdateKeyboardCaret();
			eventData.Use();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000AB14 File Offset: 0x00008D14
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 position = Vector2.zero;
				if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref position))
				{
					break;
				}
				Vector2 localMousePos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, position, eventData.pressEventCamera, out localMousePos);
				Rect rect = this.textComponent.rectTransform.rect;
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

		// Token: 0x060001EB RID: 491 RVA: 0x0000AB2A File Offset: 0x00008D2A
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000AB40 File Offset: 0x00008D40
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool hadFocusBefore = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (this.m_Keyboard == null || !this.m_Keyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			if (hadFocusBefore)
			{
				Vector2 localMousePos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out localMousePos);
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCharacterIndexFromPosition(localMousePos) + this.m_DrawStart);
			}
			this.UpdateLabel();
			this.UpdateKeyboardCaret();
			eventData.Use();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		protected InputField.EditState KeyPressed(Event evt)
		{
			EventModifiers currentEventModifiers = evt.modifiers;
			bool ctrl = ((SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX) ? ((currentEventModifiers & EventModifiers.Command) > EventModifiers.None) : ((currentEventModifiers & EventModifiers.Control) > EventModifiers.None));
			bool shift = (currentEventModifiers & EventModifiers.Shift) > EventModifiers.None;
			bool alt = (currentEventModifiers & EventModifiers.Alt) > EventModifiers.None;
			bool ctrlOnly = ctrl && !alt && !shift;
			bool shiftOnly = shift && !ctrl && !alt;
			KeyCode keyCode = evt.keyCode;
			if (keyCode <= KeyCode.A)
			{
				if (keyCode <= KeyCode.Return)
				{
					if (keyCode == KeyCode.Backspace)
					{
						this.Backspace();
						return InputField.EditState.Continue;
					}
					if (keyCode != KeyCode.Return)
					{
						goto IL_0213;
					}
				}
				else
				{
					if (keyCode == KeyCode.Escape)
					{
						this.m_WasCanceled = true;
						return InputField.EditState.Finish;
					}
					if (keyCode != KeyCode.A)
					{
						goto IL_0213;
					}
					if (ctrlOnly)
					{
						this.SelectAll();
						return InputField.EditState.Continue;
					}
					goto IL_0213;
				}
			}
			else if (keyCode <= KeyCode.V)
			{
				if (keyCode != KeyCode.C)
				{
					if (keyCode != KeyCode.V)
					{
						goto IL_0213;
					}
					if (ctrlOnly)
					{
						this.Append(InputField.clipboard);
						this.UpdateLabel();
						return InputField.EditState.Continue;
					}
					goto IL_0213;
				}
				else
				{
					if (ctrlOnly)
					{
						if (this.inputType != InputField.InputType.Password)
						{
							InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							InputField.clipboard = "";
						}
						return InputField.EditState.Continue;
					}
					goto IL_0213;
				}
			}
			else if (keyCode != KeyCode.X)
			{
				if (keyCode == KeyCode.Delete)
				{
					this.ForwardSpace();
					return InputField.EditState.Continue;
				}
				switch (keyCode)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
					goto IL_0213;
				case KeyCode.UpArrow:
					this.MoveUp(shift);
					return InputField.EditState.Continue;
				case KeyCode.DownArrow:
					this.MoveDown(shift);
					return InputField.EditState.Continue;
				case KeyCode.RightArrow:
					this.MoveRight(shift, ctrl);
					return InputField.EditState.Continue;
				case KeyCode.LeftArrow:
					this.MoveLeft(shift, ctrl);
					return InputField.EditState.Continue;
				case KeyCode.Insert:
					if (ctrlOnly)
					{
						if (this.inputType != InputField.InputType.Password)
						{
							InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							InputField.clipboard = "";
						}
						return InputField.EditState.Continue;
					}
					if (shiftOnly)
					{
						this.Append(InputField.clipboard);
						this.UpdateLabel();
						return InputField.EditState.Continue;
					}
					goto IL_0213;
				case KeyCode.Home:
					this.MoveTextStart(shift);
					return InputField.EditState.Continue;
				case KeyCode.End:
					this.MoveTextEnd(shift);
					return InputField.EditState.Continue;
				default:
					goto IL_0213;
				}
			}
			else
			{
				if (ctrlOnly)
				{
					if (this.inputType != InputField.InputType.Password)
					{
						InputField.clipboard = this.GetSelectedString();
					}
					else
					{
						InputField.clipboard = "";
					}
					this.Delete();
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return InputField.EditState.Continue;
				}
				goto IL_0213;
			}
			if (this.lineType != InputField.LineType.MultiLineNewline)
			{
				return InputField.EditState.Finish;
			}
			IL_0213:
			char c = evt.character;
			if (!this.multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && this.compositionString.Length > 0)
			{
				this.UpdateLabel();
			}
			return InputField.EditState.Continue;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000AE72 File Offset: 0x00009072
		private bool IsValidChar(char c)
		{
			return c != '\0' && c != '\u007f' && (c == '\t' || c == '\n' || this.m_TextComponent.font.HasCharacter(c));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000AE9D File Offset: 0x0000909D
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AEA8 File Offset: 0x000090A8
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool consumedEvent = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				if (this.m_ProcessingEvent.rawType == EventType.KeyDown)
				{
					consumedEvent = true;
					if (this.m_IsCompositionActive && this.compositionString.Length == 0 && this.m_ProcessingEvent.character == '\0' && this.m_ProcessingEvent.modifiers == EventModifiers.None)
					{
						continue;
					}
					if (this.KeyPressed(this.m_ProcessingEvent) == InputField.EditState.Finish)
					{
						if (!this.m_WasCanceled)
						{
							this.SendOnSubmit();
						}
						this.DeactivateInputField();
						continue;
					}
					this.UpdateLabel();
				}
				EventType type = this.m_ProcessingEvent.type;
				if (type - EventType.ValidateCommand <= 1 && this.m_ProcessingEvent.commandName == "SelectAll")
				{
					this.SelectAll();
					consumedEvent = true;
				}
			}
			if (consumedEvent)
			{
				this.UpdateLabel();
			}
			eventData.Use();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000AF80 File Offset: 0x00009180
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return "";
			}
			int startPos = this.caretPositionInternal;
			int endPos = this.caretSelectPositionInternal;
			if (startPos > endPos)
			{
				int num = startPos;
				startPos = endPos;
				endPos = num;
			}
			return this.text.Substring(startPos, endPos - startPos);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000AFC0 File Offset: 0x000091C0
		private int FindtNextWordBegin()
		{
			if (this.caretSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int spaceLoc = this.text.IndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal + 1);
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

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B020 File Offset: 0x00009220
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
				return;
			}
			int position;
			if (ctrl)
			{
				position = this.FindtNextWordBegin();
			}
			else
			{
				position = this.caretSelectPositionInternal + 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = position;
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = position);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B08C File Offset: 0x0000928C
		private int FindtPrevWordBegin()
		{
			if (this.caretSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int spaceLoc = this.text.LastIndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal - 2);
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

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B0CC File Offset: 0x000092CC
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
				return;
			}
			int position;
			if (ctrl)
			{
				position = this.FindtPrevWordBegin();
			}
			else
			{
				position = this.caretSelectPositionInternal - 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = position;
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = position);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B138 File Offset: 0x00009338
		private int DetermineCharacterLine(int charPos, TextGenerator generator)
		{
			for (int i = 0; i < generator.lineCount - 1; i++)
			{
				if (generator.lines[i + 1].startCharIdx > charPos)
				{
					return i;
				}
			}
			return generator.lineCount - 1;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B178 File Offset: 0x00009378
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characters.Count)
			{
				return 0;
			}
			UICharInfo originChar = this.cachedInputTextGenerator.characters[originalPos];
			int originLine = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (originLine > 0)
			{
				int endCharIdx = this.cachedInputTextGenerator.lines[originLine].startCharIdx - 1;
				for (int i = this.cachedInputTextGenerator.lines[originLine - 1].startCharIdx; i < endCharIdx; i++)
				{
					if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= originChar.cursorPos.x)
					{
						return i;
					}
				}
				return endCharIdx;
			}
			if (!goToFirstChar)
			{
				return originalPos;
			}
			return 0;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B22C File Offset: 0x0000942C
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characterCountVisible)
			{
				return this.text.Length;
			}
			UICharInfo originChar = this.cachedInputTextGenerator.characters[originalPos];
			int originLine = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (originLine + 1 < this.cachedInputTextGenerator.lineCount)
			{
				int endCharIdx = InputField.GetLineEndPosition(this.cachedInputTextGenerator, originLine + 1);
				for (int i = this.cachedInputTextGenerator.lines[originLine + 1].startCharIdx; i < endCharIdx; i++)
				{
					if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= originChar.cursorPos.x)
					{
						return i;
					}
				}
				return endCharIdx;
			}
			if (!goToLastChar)
			{
				return originalPos;
			}
			return this.text.Length;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B2F1 File Offset: 0x000094F1
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B2FC File Offset: 0x000094FC
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int position = (this.multiLine ? this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : this.text.Length);
			if (shift)
			{
				this.caretSelectPositionInternal = position;
				return;
			}
			this.caretPositionInternal = (this.caretSelectPositionInternal = position);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B372 File Offset: 0x00009572
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B37C File Offset: 0x0000957C
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
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = position);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B3E8 File Offset: 0x000095E8
		private void Delete()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.caretPositionInternal == this.caretSelectPositionInternal)
			{
				return;
			}
			if (this.caretPositionInternal < this.caretSelectPositionInternal)
			{
				this.m_Text = this.text.Substring(0, this.caretPositionInternal) + this.text.Substring(this.caretSelectPositionInternal, this.text.Length - this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = this.caretPositionInternal;
				return;
			}
			this.m_Text = this.text.Substring(0, this.caretSelectPositionInternal) + this.text.Substring(this.caretPositionInternal, this.text.Length - this.caretPositionInternal);
			this.caretPositionInternal = this.caretSelectPositionInternal;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B4B4 File Offset: 0x000096B4
		private void ForwardSpace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.caretPositionInternal < this.text.Length)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal, 1);
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000B51C File Offset: 0x0000971C
		private void Backspace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.caretPositionInternal > 0 && this.caretPositionInternal - 1 < this.text.Length)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal - 1, 1);
				this.caretSelectPositionInternal = --this.caretPositionInternal;
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000B5A8 File Offset: 0x000097A8
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
			this.m_Text = this.text.Insert(this.m_CaretPosition, replaceString);
			this.caretSelectPositionInternal = (this.caretPositionInternal += replaceString.Length);
			this.UpdateTouchKeyboardFromEditChanges();
			this.SendOnValueChanged();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000B629 File Offset: 0x00009829
		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (this.m_Keyboard != null && this.InPlaceEditing())
			{
				this.m_Keyboard.text = this.m_Text;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000B64C File Offset: 0x0000984C
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.SendOnValueChanged();
			this.UpdateLabel();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000B65A File Offset: 0x0000985A
		private void SendOnValueChanged()
		{
			UISystemProfilerApi.AddMarker("InputField.value", this);
			if (this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.text);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B680 File Offset: 0x00009880
		protected void SendOnEndEdit()
		{
			UISystemProfilerApi.AddMarker("InputField.onEndEdit", this);
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B6A6 File Offset: 0x000098A6
		protected void SendOnSubmit()
		{
			UISystemProfilerApi.AddMarker("InputField.onSubmit", this);
			if (this.onSubmit != null)
			{
				this.onSubmit.Invoke(this.m_Text);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B6CC File Offset: 0x000098CC
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
				if (c >= ' ' || c == '\t' || c == '\r' || c == '\n' || c == '\n')
				{
					this.Append(c);
				}
				i++;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B728 File Offset: 0x00009928
		protected virtual void Append(char input)
		{
			if (char.IsSurrogate(input))
			{
				return;
			}
			if (this.m_ReadOnly || this.text.Length >= 16382)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int insertionPoint = Math.Min(this.selectionFocusPosition, this.selectionAnchorPosition);
			string validateText = this.text;
			if (this.selectionFocusPosition != this.selectionAnchorPosition)
			{
				if (this.caretPositionInternal < this.caretSelectPositionInternal)
				{
					validateText = this.text.Substring(0, this.caretPositionInternal) + this.text.Substring(this.caretSelectPositionInternal, this.text.Length - this.caretSelectPositionInternal);
				}
				else
				{
					validateText = this.text.Substring(0, this.caretSelectPositionInternal) + this.text.Substring(this.caretPositionInternal, this.text.Length - this.caretPositionInternal);
				}
			}
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(validateText, insertionPoint, input);
			}
			else if (this.characterValidation != InputField.CharacterValidation.None)
			{
				input = this.Validate(validateText, insertionPoint, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B84C File Offset: 0x00009A4C
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventFontCallback)
			{
				this.m_PreventFontCallback = true;
				string fullText;
				if (EventSystem.current != null && base.gameObject == EventSystem.current.currentSelectedGameObject && this.compositionString.Length > 0)
				{
					this.m_IsCompositionActive = true;
					fullText = this.text.Substring(0, this.m_CaretPosition) + this.compositionString + this.text.Substring(this.m_CaretPosition);
				}
				else
				{
					this.m_IsCompositionActive = false;
					fullText = this.text;
				}
				string processed;
				if (this.inputType == InputField.InputType.Password)
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
				if (!this.m_AllowInput)
				{
					this.m_DrawStart = 0;
					this.m_DrawEnd = this.m_Text.Length;
				}
				this.textComponent.SetLayoutDirty();
				if (!isEmpty)
				{
					Vector2 extents = this.m_TextComponent.rectTransform.rect.size;
					TextGenerationSettings settings = this.m_TextComponent.GetGenerationSettings(extents);
					settings.generateOutOfBounds = true;
					this.cachedInputTextGenerator.PopulateWithErrors(processed, settings, base.gameObject);
					this.SetDrawRangeToContainCaretPosition(this.caretSelectPositionInternal);
					processed = processed.Substring(this.m_DrawStart, Mathf.Min(this.m_DrawEnd, processed.Length) - this.m_DrawStart);
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = processed;
				this.MarkGeometryAsDirty();
				this.m_PreventFontCallback = false;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000BA0D File Offset: 0x00009C0D
		private bool IsSelectionVisible()
		{
			return this.m_DrawStart <= this.caretPositionInternal && this.m_DrawStart <= this.caretSelectPositionInternal && this.m_DrawEnd >= this.caretPositionInternal && this.m_DrawEnd >= this.caretSelectPositionInternal;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000BA4C File Offset: 0x00009C4C
		private static int GetLineStartPosition(TextGenerator gen, int line)
		{
			line = Mathf.Clamp(line, 0, gen.lines.Count - 1);
			return gen.lines[line].startCharIdx;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000BA75 File Offset: 0x00009C75
		private static int GetLineEndPosition(TextGenerator gen, int line)
		{
			line = Mathf.Max(line, 0);
			if (line + 1 < gen.lines.Count)
			{
				return gen.lines[line + 1].startCharIdx - 1;
			}
			return gen.characterCountVisible;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000BAAC File Offset: 0x00009CAC
		private void SetDrawRangeToContainCaretPosition(int caretPos)
		{
			if (this.cachedInputTextGenerator.lineCount <= 0)
			{
				return;
			}
			Vector2 extents = this.cachedInputTextGenerator.rectExtents.size;
			if (!this.multiLine)
			{
				IList<UICharInfo> characters = this.cachedInputTextGenerator.characters;
				if (this.m_DrawEnd > this.cachedInputTextGenerator.characterCountVisible)
				{
					this.m_DrawEnd = this.cachedInputTextGenerator.characterCountVisible;
				}
				float width = 0f;
				if (caretPos > this.m_DrawEnd || (caretPos == this.m_DrawEnd && this.m_DrawStart > 0))
				{
					this.m_DrawEnd = caretPos;
					this.m_DrawStart = this.m_DrawEnd - 1;
					while (this.m_DrawStart >= 0 && width + characters[this.m_DrawStart].charWidth <= extents.x)
					{
						width += characters[this.m_DrawStart].charWidth;
						this.m_DrawStart--;
					}
					this.m_DrawStart++;
				}
				else
				{
					if (caretPos < this.m_DrawStart)
					{
						this.m_DrawStart = caretPos;
					}
					this.m_DrawEnd = this.m_DrawStart;
				}
				while (this.m_DrawEnd < this.cachedInputTextGenerator.characterCountVisible)
				{
					width += characters[this.m_DrawEnd].charWidth;
					if (width > extents.x)
					{
						break;
					}
					this.m_DrawEnd++;
				}
				return;
			}
			IList<UILineInfo> lines = this.cachedInputTextGenerator.lines;
			int caretLine = this.DetermineCharacterLine(caretPos, this.cachedInputTextGenerator);
			if (caretPos > this.m_DrawEnd)
			{
				this.m_DrawEnd = InputField.GetLineEndPosition(this.cachedInputTextGenerator, caretLine);
				float bottomY = lines[caretLine].topY - (float)lines[caretLine].height;
				if (caretLine == lines.Count - 1)
				{
					bottomY += lines[caretLine].leading;
				}
				int startLine = caretLine;
				while (startLine > 0 && lines[startLine - 1].topY - bottomY <= extents.y)
				{
					startLine--;
				}
				this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, startLine);
				return;
			}
			if (caretPos < this.m_DrawStart)
			{
				this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, caretLine);
			}
			int startLine2 = this.DetermineCharacterLine(this.m_DrawStart, this.cachedInputTextGenerator);
			int endLine = startLine2;
			float topY = lines[startLine2].topY;
			float bottomY2 = lines[endLine].topY - (float)lines[endLine].height;
			if (endLine == lines.Count - 1)
			{
				bottomY2 += lines[endLine].leading;
			}
			while (endLine < lines.Count - 1)
			{
				bottomY2 = lines[endLine + 1].topY - (float)lines[endLine + 1].height;
				if (endLine + 1 == lines.Count - 1)
				{
					bottomY2 += lines[endLine + 1].leading;
				}
				if (topY - bottomY2 > extents.y)
				{
					break;
				}
				endLine++;
			}
			this.m_DrawEnd = InputField.GetLineEndPosition(this.cachedInputTextGenerator, endLine);
			while (startLine2 > 0)
			{
				topY = lines[startLine2 - 1].topY;
				if (topY - bottomY2 > extents.y)
				{
					break;
				}
				startLine2--;
			}
			this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, startLine2);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000BDEF File Offset: 0x00009FEF
		public void ForceLabelUpdate()
		{
			this.UpdateLabel();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000BDF7 File Offset: 0x00009FF7
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000BDFF File Offset: 0x00009FFF
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000BE0C File Offset: 0x0000A00C
		private void UpdateGeometry()
		{
			if (!this.InPlaceEditing() && !this.shouldHideMobileInput)
			{
				return;
			}
			if (this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject go = new GameObject(base.transform.name + " Input Caret", new Type[]
				{
					typeof(RectTransform),
					typeof(CanvasRenderer)
				});
				go.hideFlags = HideFlags.DontSave;
				go.transform.SetParent(this.m_TextComponent.transform.parent);
				go.transform.SetAsFirstSibling();
				go.layer = base.gameObject.layer;
				this.caretRectTrans = go.GetComponent<RectTransform>();
				this.m_CachedInputRenderer = go.GetComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
				go.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.mesh);
			this.m_CachedInputRenderer.SetMesh(this.mesh);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000BF3C File Offset: 0x0000A13C
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

		// Token: 0x06000214 RID: 532 RVA: 0x0000C164 File Offset: 0x0000A364
		private void OnFillVBO(Mesh vbo)
		{
			using (VertexHelper helper = new VertexHelper())
			{
				if (!this.isFocused)
				{
					helper.FillMesh(vbo);
				}
				else
				{
					Vector2 roundingOffset = this.m_TextComponent.PixelAdjustPoint(Vector2.zero);
					if (!this.hasSelection)
					{
						this.GenerateCaret(helper, roundingOffset);
					}
					else
					{
						this.GenerateHighlight(helper, roundingOffset);
					}
					helper.FillMesh(vbo);
				}
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
		{
			if (!this.m_CaretVisible)
			{
				return;
			}
			if (this.m_CursorVerts == null)
			{
				this.CreateCursorVerts();
			}
			float width = (float)this.m_CaretWidth;
			int adjustedPos = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			TextGenerator gen = this.m_TextComponent.cachedTextGenerator;
			if (gen == null)
			{
				return;
			}
			if (gen.lineCount == 0)
			{
				return;
			}
			Vector2 startPosition = Vector2.zero;
			if (adjustedPos < gen.characters.Count)
			{
				UICharInfo cursorChar = gen.characters[adjustedPos];
				startPosition.x = cursorChar.cursorPos.x;
			}
			startPosition.x /= this.m_TextComponent.pixelsPerUnit;
			if (startPosition.x > this.m_TextComponent.rectTransform.rect.xMax)
			{
				startPosition.x = this.m_TextComponent.rectTransform.rect.xMax;
			}
			int characterLine = this.DetermineCharacterLine(adjustedPos, gen);
			startPosition.y = gen.lines[characterLine].topY / this.m_TextComponent.pixelsPerUnit;
			float height = (float)gen.lines[characterLine].height / this.m_TextComponent.pixelsPerUnit;
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i].color = this.caretColor;
			}
			this.m_CursorVerts[0].position = new Vector3(startPosition.x, startPosition.y - height, 0f);
			this.m_CursorVerts[1].position = new Vector3(startPosition.x + width, startPosition.y - height, 0f);
			this.m_CursorVerts[2].position = new Vector3(startPosition.x + width, startPosition.y, 0f);
			this.m_CursorVerts[3].position = new Vector3(startPosition.x, startPosition.y, 0f);
			if (roundingOffset != Vector2.zero)
			{
				for (int j = 0; j < this.m_CursorVerts.Length; j++)
				{
					UIVertex uiv = this.m_CursorVerts[j];
					uiv.position.x = uiv.position.x + roundingOffset.x;
					uiv.position.y = uiv.position.y + roundingOffset.y;
				}
			}
			vbo.AddUIVertexQuad(this.m_CursorVerts);
			int screenHeight = Screen.height;
			int displayIndex = this.m_TextComponent.canvas.targetDisplay;
			if (displayIndex > 0 && displayIndex < Display.displays.Length)
			{
				screenHeight = Display.displays[displayIndex].renderingHeight;
			}
			Camera cameraRef;
			if (this.m_TextComponent.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				cameraRef = null;
			}
			else
			{
				cameraRef = this.m_TextComponent.canvas.worldCamera;
			}
			Vector3 cursorPosition = this.m_CachedInputRenderer.gameObject.transform.TransformPoint(this.m_CursorVerts[0].position);
			Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(cameraRef, cursorPosition);
			screenPosition.y = (float)screenHeight - screenPosition.y;
			if (this.input != null)
			{
				this.input.compositionCursorPos = screenPosition;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000C514 File Offset: 0x0000A714
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C56C File Offset: 0x0000A76C
		private void GenerateHighlight(VertexHelper vbo, Vector2 roundingOffset)
		{
			int startChar = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			int endChar = Mathf.Max(0, this.caretSelectPositionInternal - this.m_DrawStart);
			if (startChar > endChar)
			{
				int num = startChar;
				startChar = endChar;
				endChar = num;
			}
			endChar--;
			TextGenerator gen = this.m_TextComponent.cachedTextGenerator;
			if (gen.lineCount <= 0)
			{
				return;
			}
			int currentLineIndex = this.DetermineCharacterLine(startChar, gen);
			int lastCharInLineIndex = InputField.GetLineEndPosition(gen, currentLineIndex);
			UIVertex vert = UIVertex.simpleVert;
			vert.uv0 = Vector2.zero;
			vert.color = this.selectionColor;
			int currentChar = startChar;
			while (currentChar <= endChar && currentChar < gen.characterCount)
			{
				if (currentChar == lastCharInLineIndex || currentChar == endChar)
				{
					UICharInfo startCharInfo = gen.characters[startChar];
					UICharInfo endCharInfo = gen.characters[currentChar];
					Vector2 startPosition = new Vector2(startCharInfo.cursorPos.x / this.m_TextComponent.pixelsPerUnit, gen.lines[currentLineIndex].topY / this.m_TextComponent.pixelsPerUnit);
					Vector2 endPosition = new Vector2((endCharInfo.cursorPos.x + endCharInfo.charWidth) / this.m_TextComponent.pixelsPerUnit, startPosition.y - (float)gen.lines[currentLineIndex].height / this.m_TextComponent.pixelsPerUnit);
					if (endPosition.x > this.m_TextComponent.rectTransform.rect.xMax || endPosition.x < this.m_TextComponent.rectTransform.rect.xMin)
					{
						endPosition.x = this.m_TextComponent.rectTransform.rect.xMax;
					}
					int startIndex = vbo.currentVertCount;
					vert.position = new Vector3(startPosition.x, endPosition.y, 0f) + roundingOffset;
					vbo.AddVert(vert);
					vert.position = new Vector3(endPosition.x, endPosition.y, 0f) + roundingOffset;
					vbo.AddVert(vert);
					vert.position = new Vector3(endPosition.x, startPosition.y, 0f) + roundingOffset;
					vbo.AddVert(vert);
					vert.position = new Vector3(startPosition.x, startPosition.y, 0f) + roundingOffset;
					vbo.AddVert(vert);
					vbo.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
					vbo.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
					startChar = currentChar + 1;
					currentLineIndex++;
					lastCharInLineIndex = InputField.GetLineEndPosition(gen, currentLineIndex);
				}
				currentChar++;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C83C File Offset: 0x0000AA3C
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == InputField.CharacterValidation.Integer || this.characterValidation == InputField.CharacterValidation.Decimal)
			{
				int num = ((pos == 0 && text.Length > 0 && text[0] == '-') ? 1 : 0);
				bool dashInSelection = text.Length > 0 && text[0] == '-' && ((this.caretPositionInternal == 0 && this.caretSelectPositionInternal > 0) || (this.caretSelectPositionInternal == 0 && this.caretPositionInternal > 0));
				bool selectionAtStart = this.caretPositionInternal == 0 || this.caretSelectPositionInternal == 0;
				if (num == 0 || dashInSelection)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || selectionAtStart))
					{
						return ch;
					}
					if ((ch == '.' || ch == ',') && this.characterValidation == InputField.CharacterValidation.Decimal && text.IndexOfAny(new char[] { '.', ',' }) == -1)
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.Alphanumeric)
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
			else if (this.characterValidation == InputField.CharacterValidation.Name)
			{
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && (pos == 0 || text[pos - 1] == ' ' || text[pos - 1] == '-'))
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && pos > 0 && text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')
					{
						return char.ToLower(ch);
					}
					return ch;
				}
				else
				{
					if (ch == '\'' && !text.Contains("'") && (pos <= 0 || (text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')) && (pos >= text.Length || (text[pos] != ' ' && text[pos] != '\'' && text[pos] != '-')))
					{
						return ch;
					}
					if ((ch == ' ' || ch == '-') && pos != 0 && (pos <= 0 || (text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')) && (pos >= text.Length || (text[pos] != ' ' && text[pos] != '\'' && text[pos - 1] != '-')))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.EmailAddress)
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
					int num2 = (int)((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char nextChar = ((text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n');
					if (num2 != 46 && nextChar != '.')
					{
						return ch;
					}
				}
			}
			return '\0';
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000CB7C File Offset: 0x0000AD7C
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && this.m_Keyboard != null && !this.m_Keyboard.active)
			{
				this.m_Keyboard.active = true;
				this.m_Keyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000CBFC File Offset: 0x0000ADFC
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
			this.m_TouchKeyboardAllowsInPlaceEditing = !InputField.s_IsQuestDevice && TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (this.TouchScreenKeyboardShouldBeUsed())
			{
				if (this.input != null && this.input.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				this.m_Keyboard = ((this.inputType == InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true, false, "", this.characterLimit) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == InputField.InputType.AutoCorrect, this.multiLine, false, false, "", this.characterLimit));
				if (!this.m_TouchKeyboardAllowsInPlaceEditing)
				{
					this.MoveTextEnd(false);
				}
			}
			if (!TouchScreenKeyboard.isSupported || this.m_TouchKeyboardAllowsInPlaceEditing)
			{
				if (this.input != null)
				{
					this.input.imeCompositionMode = IMECompositionMode.On;
				}
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			if (this.shouldActivateOnSelect)
			{
				this.ActivateInputField();
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000CD62 File Offset: 0x0000AF62
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public void DeactivateInputField()
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
				if (this.m_WasCanceled)
				{
					this.text = this.m_OriginalText;
				}
				this.SendOnEndEdit();
				if (this.m_Keyboard != null)
				{
					this.m_Keyboard.active = false;
					this.m_Keyboard = null;
				}
				this.m_CaretPosition = (this.m_CaretSelectPosition = 0);
				if (this.input != null)
				{
					this.input.imeCompositionMode = IMECompositionMode.Auto;
				}
			}
			this.MarkGeometryAsDirty();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000CE37 File Offset: 0x0000B037
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField();
			base.OnDeselect(eventData);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000CE46 File Offset: 0x0000B046
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
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CE68 File Offset: 0x0000B068
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case InputField.ContentType.Standard:
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.Autocorrected:
				this.m_InputType = InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.IntegerNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				break;
			case InputField.ContentType.DecimalNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = InputField.CharacterValidation.Decimal;
				break;
			case InputField.ContentType.Alphanumeric:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = InputField.CharacterValidation.Alphanumeric;
				break;
			case InputField.ContentType.Name:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NamePhonePad;
				this.m_CharacterValidation = InputField.CharacterValidation.Name;
				break;
			case InputField.ContentType.EmailAddress:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = InputField.CharacterValidation.EmailAddress;
				break;
			case InputField.ContentType.Password:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.Pin:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				break;
			}
			this.EnforceTextHOverflow();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CFBB File Offset: 0x0000B1BB
		private void EnforceTextHOverflow()
		{
			if (this.m_TextComponent != null)
			{
				if (this.multiLine)
				{
					this.m_TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
					return;
				}
				this.m_TextComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		private void SetToCustomIfContentTypeIsNot(params InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == InputField.ContentType.Custom)
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
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000D026 File Offset: 0x0000B226
		private void SetToCustom()
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000D03B File Offset: 0x0000B23B
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

		// Token: 0x06000225 RID: 549 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000D05D File Offset: 0x0000B25D
		public virtual float minWidth
		{
			get
			{
				return 5f;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000D064 File Offset: 0x0000B264
		public virtual float preferredWidth
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				TextGenerationSettings settings = this.textComponent.GetGenerationSettings(Vector2.zero);
				return this.textComponent.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, settings) / this.textComponent.pixelsPerUnit;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000092F6 File Offset: 0x000074F6
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		public virtual float preferredHeight
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				TextGenerationSettings settings = this.textComponent.GetGenerationSettings(new Vector2(this.textComponent.rectTransform.rect.size.x, 0f));
				return this.textComponent.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, settings) / this.textComponent.pixelsPerUnit;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000936A File Offset: 0x0000756A
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000D133 File Offset: 0x0000B333
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00006250 File Offset: 0x00004450
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040000CB RID: 203
		protected TouchScreenKeyboard m_Keyboard;

		// Token: 0x040000CC RID: 204
		private static readonly char[] kSeparators = new char[] { ' ', '.', ',', '\t', '\r', '\n' };

		// Token: 0x040000CD RID: 205
		private static bool s_IsQuestDevice = false;

		// Token: 0x040000CE RID: 206
		[SerializeField]
		[FormerlySerializedAs("text")]
		protected Text m_TextComponent;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		private InputField.ContentType m_ContentType;

		// Token: 0x040000D1 RID: 209
		[FormerlySerializedAs("inputType")]
		[SerializeField]
		private InputField.InputType m_InputType;

		// Token: 0x040000D2 RID: 210
		[FormerlySerializedAs("asteriskChar")]
		[SerializeField]
		private char m_AsteriskChar = '*';

		// Token: 0x040000D3 RID: 211
		[FormerlySerializedAs("keyboardType")]
		[SerializeField]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x040000D4 RID: 212
		[SerializeField]
		private InputField.LineType m_LineType;

		// Token: 0x040000D5 RID: 213
		[FormerlySerializedAs("hideMobileInput")]
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x040000D6 RID: 214
		[FormerlySerializedAs("validation")]
		[SerializeField]
		private InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x040000D7 RID: 215
		[FormerlySerializedAs("characterLimit")]
		[SerializeField]
		private int m_CharacterLimit;

		// Token: 0x040000D8 RID: 216
		[FormerlySerializedAs("onSubmit")]
		[FormerlySerializedAs("m_OnSubmit")]
		[FormerlySerializedAs("m_EndEdit")]
		[FormerlySerializedAs("m_OnEndEdit")]
		[SerializeField]
		private InputField.SubmitEvent m_OnSubmit = new InputField.SubmitEvent();

		// Token: 0x040000D9 RID: 217
		[SerializeField]
		private InputField.EndEditEvent m_OnDidEndEdit = new InputField.EndEditEvent();

		// Token: 0x040000DA RID: 218
		[FormerlySerializedAs("onValueChange")]
		[FormerlySerializedAs("m_OnValueChange")]
		[SerializeField]
		private InputField.OnChangeEvent m_OnValueChanged = new InputField.OnChangeEvent();

		// Token: 0x040000DB RID: 219
		[FormerlySerializedAs("onValidateInput")]
		[SerializeField]
		private InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x040000DC RID: 220
		[FormerlySerializedAs("selectionColor")]
		[SerializeField]
		private Color m_CaretColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x040000DD RID: 221
		[SerializeField]
		private bool m_CustomCaretColor;

		// Token: 0x040000DE RID: 222
		[SerializeField]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x040000DF RID: 223
		[SerializeField]
		[Multiline]
		[FormerlySerializedAs("mValue")]
		protected string m_Text = string.Empty;

		// Token: 0x040000E0 RID: 224
		[SerializeField]
		[Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		// Token: 0x040000E1 RID: 225
		[SerializeField]
		[Range(1f, 5f)]
		private int m_CaretWidth = 1;

		// Token: 0x040000E2 RID: 226
		[SerializeField]
		private bool m_ReadOnly;

		// Token: 0x040000E3 RID: 227
		[SerializeField]
		private bool m_ShouldActivateOnSelect = true;

		// Token: 0x040000E4 RID: 228
		protected int m_CaretPosition;

		// Token: 0x040000E5 RID: 229
		protected int m_CaretSelectPosition;

		// Token: 0x040000E6 RID: 230
		private RectTransform caretRectTrans;

		// Token: 0x040000E7 RID: 231
		protected UIVertex[] m_CursorVerts;

		// Token: 0x040000E8 RID: 232
		private TextGenerator m_InputTextCache;

		// Token: 0x040000E9 RID: 233
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x040000EA RID: 234
		private bool m_PreventFontCallback;

		// Token: 0x040000EB RID: 235
		[NonSerialized]
		protected Mesh m_Mesh;

		// Token: 0x040000EC RID: 236
		private bool m_AllowInput;

		// Token: 0x040000ED RID: 237
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x040000EE RID: 238
		private bool m_UpdateDrag;

		// Token: 0x040000EF RID: 239
		private bool m_DragPositionOutOfBounds;

		// Token: 0x040000F0 RID: 240
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x040000F1 RID: 241
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x040000F2 RID: 242
		protected bool m_CaretVisible;

		// Token: 0x040000F3 RID: 243
		private Coroutine m_BlinkCoroutine;

		// Token: 0x040000F4 RID: 244
		private float m_BlinkStartTime;

		// Token: 0x040000F5 RID: 245
		protected int m_DrawStart;

		// Token: 0x040000F6 RID: 246
		protected int m_DrawEnd;

		// Token: 0x040000F7 RID: 247
		private Coroutine m_DragCoroutine;

		// Token: 0x040000F8 RID: 248
		private string m_OriginalText = "";

		// Token: 0x040000F9 RID: 249
		private bool m_WasCanceled;

		// Token: 0x040000FA RID: 250
		private bool m_HasDoneFocusTransition;

		// Token: 0x040000FB RID: 251
		private WaitForSecondsRealtime m_WaitForSecondsRealtime;

		// Token: 0x040000FC RID: 252
		private bool m_TouchKeyboardAllowsInPlaceEditing;

		// Token: 0x040000FD RID: 253
		private bool m_IsCompositionActive;

		// Token: 0x040000FE RID: 254
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x040000FF RID: 255
		private const string kOculusQuestDeviceModel = "Oculus Quest";

		// Token: 0x04000100 RID: 256
		private Event m_ProcessingEvent = new Event();

		// Token: 0x04000101 RID: 257
		private const int k_MaxTextLength = 16382;

		// Token: 0x0200002F RID: 47
		public enum ContentType
		{
			// Token: 0x04000103 RID: 259
			Standard,
			// Token: 0x04000104 RID: 260
			Autocorrected,
			// Token: 0x04000105 RID: 261
			IntegerNumber,
			// Token: 0x04000106 RID: 262
			DecimalNumber,
			// Token: 0x04000107 RID: 263
			Alphanumeric,
			// Token: 0x04000108 RID: 264
			Name,
			// Token: 0x04000109 RID: 265
			EmailAddress,
			// Token: 0x0400010A RID: 266
			Password,
			// Token: 0x0400010B RID: 267
			Pin,
			// Token: 0x0400010C RID: 268
			Custom
		}

		// Token: 0x02000030 RID: 48
		public enum InputType
		{
			// Token: 0x0400010E RID: 270
			Standard,
			// Token: 0x0400010F RID: 271
			AutoCorrect,
			// Token: 0x04000110 RID: 272
			Password
		}

		// Token: 0x02000031 RID: 49
		public enum CharacterValidation
		{
			// Token: 0x04000112 RID: 274
			None,
			// Token: 0x04000113 RID: 275
			Integer,
			// Token: 0x04000114 RID: 276
			Decimal,
			// Token: 0x04000115 RID: 277
			Alphanumeric,
			// Token: 0x04000116 RID: 278
			Name,
			// Token: 0x04000117 RID: 279
			EmailAddress
		}

		// Token: 0x02000032 RID: 50
		public enum LineType
		{
			// Token: 0x04000119 RID: 281
			SingleLine,
			// Token: 0x0400011A RID: 282
			MultiLineSubmit,
			// Token: 0x0400011B RID: 283
			MultiLineNewline
		}

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x06000231 RID: 561
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		// Token: 0x02000034 RID: 52
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
		}

		// Token: 0x02000035 RID: 53
		[Serializable]
		public class EndEditEvent : UnityEvent<string>
		{
		}

		// Token: 0x02000036 RID: 54
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
		}

		// Token: 0x02000037 RID: 55
		protected enum EditState
		{
			// Token: 0x0400011D RID: 285
			Continue,
			// Token: 0x0400011E RID: 286
			Finish
		}
	}
}
