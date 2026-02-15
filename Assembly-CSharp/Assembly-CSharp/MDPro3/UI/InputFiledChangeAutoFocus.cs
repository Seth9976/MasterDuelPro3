using System;
using TMPro;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x020013FF RID: 5119
	public class InputFiledChangeAutoFocus : MonoBehaviour
	{
		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x0600944A RID: 37962 RVA: 0x00152360 File Offset: 0x00150560
		private TMP_InputField InputField
		{
			get
			{
				return this.m_InputField = ((this.m_InputField != null) ? this.m_InputField : base.GetComponent<TMP_InputField>());
			}
		}

		// Token: 0x0600944B RID: 37963 RVA: 0x00152392 File Offset: 0x00150592
		private void Awake()
		{
			UserInput.OnMouseCursorHide += this.SetAutoFocus;
		}

		// Token: 0x0600944C RID: 37964 RVA: 0x001523A5 File Offset: 0x001505A5
		private void OnDestroy()
		{
			UserInput.OnMouseCursorHide -= this.SetAutoFocus;
		}

		// Token: 0x0600944D RID: 37965 RVA: 0x001523B8 File Offset: 0x001505B8
		private void SetAutoFocus()
		{
			if (this.InputField != null)
			{
				this.InputField.shouldActivateOnSelect = Cursor.lockState == CursorLockMode.None;
			}
		}

		// Token: 0x0400D28C RID: 53900
		private TMP_InputField m_InputField;
	}
}
