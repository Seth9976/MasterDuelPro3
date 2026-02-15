using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013FE RID: 5118
	public class InputFieldSubmit : MonoBehaviour
	{
		// Token: 0x06009445 RID: 37957 RVA: 0x00152218 File Offset: 0x00150418
		private void Awake()
		{
			if (base.TryGetComponent<InputField>(out this.inputField))
			{
				this.inputField.lineType = InputField.LineType.MultiLineNewline;
			}
			if (base.TryGetComponent<TMP_InputField>(out this.tmpInput))
			{
				this.tmpInput.lineType = TMP_InputField.LineType.MultiLineNewline;
			}
		}

		// Token: 0x06009446 RID: 37958 RVA: 0x00152250 File Offset: 0x00150450
		private void OnEnable()
		{
			if (this.inputField != null)
			{
				InputField inputField = this.inputField;
				inputField.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(inputField.onValidateInput, new InputField.OnValidateInput(this.CheckForEnter));
			}
			if (this.tmpInput != null)
			{
				TMP_InputField tmp_InputField = this.tmpInput;
				tmp_InputField.onValidateInput = (TMP_InputField.OnValidateInput)Delegate.Combine(tmp_InputField.onValidateInput, new TMP_InputField.OnValidateInput(this.CheckForEnter));
			}
		}

		// Token: 0x06009447 RID: 37959 RVA: 0x001522C8 File Offset: 0x001504C8
		private void OnDisable()
		{
			if (this.inputField != null)
			{
				InputField inputField = this.inputField;
				inputField.onValidateInput = (InputField.OnValidateInput)Delegate.Remove(inputField.onValidateInput, new InputField.OnValidateInput(this.CheckForEnter));
			}
			if (this.tmpInput != null)
			{
				TMP_InputField tmp_InputField = this.tmpInput;
				tmp_InputField.onValidateInput = (TMP_InputField.OnValidateInput)Delegate.Remove(tmp_InputField.onValidateInput, new TMP_InputField.OnValidateInput(this.CheckForEnter));
			}
		}

		// Token: 0x06009448 RID: 37960 RVA: 0x0015233F File Offset: 0x0015053F
		private char CheckForEnter(string text, int charIndex, char addedChar)
		{
			if (addedChar == '\n' && this.onSubmit != null)
			{
				this.onSubmit.Invoke(text);
				return '\0';
			}
			return addedChar;
		}

		// Token: 0x0400D289 RID: 53897
		public StringUnityEvent onSubmit;

		// Token: 0x0400D28A RID: 53898
		private InputField inputField;

		// Token: 0x0400D28B RID: 53899
		private TMP_InputField tmpInput;
	}
}
