using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200136D RID: 4973
	public class CleanTextButton : MonoBehaviour
	{
		// Token: 0x06009019 RID: 36889 RVA: 0x0013AAF4 File Offset: 0x00138CF4
		private void Start()
		{
			this.button = base.GetComponent<Button>();
			this.image = base.GetComponent<Image>();
			this.button.onClick.AddListener(new UnityAction(this.CleanText));
			this.InputField.onValueChanged.AddListener(new UnityAction<string>(this.ShowOrNot));
			this.ShowOrNot("");
		}

		// Token: 0x0600901A RID: 36890 RVA: 0x0013AB5C File Offset: 0x00138D5C
		private void ShowOrNot(string value)
		{
			if (this.InputField.text == "")
			{
				this.image.color = new Color(1f, 1f, 1f, 0f);
				this.button.interactable = false;
				return;
			}
			this.image.color = new Color(1f, 1f, 1f, 1f);
			this.button.interactable = true;
		}

		// Token: 0x0600901B RID: 36891 RVA: 0x0013ABE1 File Offset: 0x00138DE1
		private void CleanText()
		{
			this.InputField.text = "";
			this.InputField.onEndEdit.Invoke("");
		}

		// Token: 0x0400CEBD RID: 52925
		public InputField InputField;

		// Token: 0x0400CEBE RID: 52926
		private Button button;

		// Token: 0x0400CEBF RID: 52927
		private Image image;
	}
}
