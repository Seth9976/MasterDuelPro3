using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YgomSystem.UI.ElementWidget
{
	// Token: 0x02000693 RID: 1683
	public class InputFieldWrapper
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060034F4 RID: 13556 RVA: 0x0000216D File Offset: 0x0000036D
		public TextWrapper textComponent
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

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x0000216A File Offset: 0x0000036A
		private Selectable selectable
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060034F7 RID: 13559 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060034F9 RID: 13561 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject gameObject
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x0000216A File Offset: 0x0000036A
		public Graphic targetGraphic
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x0000216A File Offset: 0x0000036A
		public Transform transform
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060034FE RID: 13566 RVA: 0x0000216D File Offset: 0x0000036D
		public InputFieldWrapper.OnChangedEvent onValueChanged
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060034FF RID: 13567 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003500 RID: 13568 RVA: 0x0000216D File Offset: 0x0000036D
		public InputField.ContentType contentType
		{
			get
			{
				return InputField.ContentType.Standard;
			}
			set
			{
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06003501 RID: 13569 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003502 RID: 13570 RVA: 0x0000216D File Offset: 0x0000036D
		public int characterLimit
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x0000216A File Offset: 0x0000036A
		public Graphic placeholder
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003505 RID: 13573 RVA: 0x0000216D File Offset: 0x0000036D
		public int caretPosition
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x00002739 File Offset: 0x00000939
		public InputFieldWrapper(ExtendedInputField inputField)
		{
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x00002739 File Offset: 0x00000939
		public InputFieldWrapper(TMP_InputField inputField)
		{
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x0000216D File Offset: 0x0000036D
		public void InvokeOnValueChanged(string text)
		{
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTextWithoutNotify(string text)
		{
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateInputField()
		{
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeactivateInputField()
		{
		}

		// Token: 0x04003046 RID: 12358
		private InputFieldWrapper.Mode mode;

		// Token: 0x04003047 RID: 12359
		private ExtendedInputField inputField;

		// Token: 0x04003048 RID: 12360
		private TMP_InputField TMPInputField;

		// Token: 0x02000694 RID: 1684
		private enum Mode
		{
			// Token: 0x0400304A RID: 12362
			uGUI,
			// Token: 0x0400304B RID: 12363
			TMP
		}

		// Token: 0x02000695 RID: 1685
		public class OnChangedEvent : UnityEvent<string>
		{
		}
	}
}
