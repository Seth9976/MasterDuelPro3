using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013FC RID: 5116
	public class InputFieldCleaner : MonoBehaviour
	{
		// Token: 0x06009440 RID: 37952 RVA: 0x00152174 File Offset: 0x00150374
		private void Awake()
		{
			this.button.onClick.AddListener(new UnityAction(this.CleanText));
			this.button.gameObject.SetActive(false);
			this.InputField.onValueChanged.AddListener(new UnityAction<string>(this.ShowCleanButton));
		}

		// Token: 0x06009441 RID: 37953 RVA: 0x001521CA File Offset: 0x001503CA
		private void ShowCleanButton(string value)
		{
			this.button.gameObject.SetActive(this.InputField.text != string.Empty);
		}

		// Token: 0x06009442 RID: 37954 RVA: 0x001521F1 File Offset: 0x001503F1
		private void CleanText()
		{
			this.InputField.text = string.Empty;
			this.InputField.onEndEdit.Invoke(string.Empty);
		}

		// Token: 0x0400D287 RID: 53895
		public TMP_InputField InputField;

		// Token: 0x0400D288 RID: 53896
		public Button button;
	}
}
