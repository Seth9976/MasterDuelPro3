using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MDPro3.UI
{
	// Token: 0x02001411 RID: 5137
	[RequireComponent(typeof(TMP_InputField))]
	public class TmpInputValidation : MonoBehaviour
	{
		// Token: 0x0600948A RID: 38026 RVA: 0x0015300C File Offset: 0x0015120C
		private void Awake()
		{
			this.m_InputFied = base.GetComponent<TMP_InputField>();
			this.m_InputFied.onValueChanged.AddListener(new UnityAction<string>(this.OnInputFieldValueChange));
		}

		// Token: 0x0600948B RID: 38027 RVA: 0x00153038 File Offset: 0x00151238
		private void OnInputFieldValueChange(string inputInfo)
		{
			if (this.type == TmpInputValidation.ValidationType.Path)
			{
				foreach (char c in inputInfo)
				{
					if (TmpInputValidation.InvalidPathChars.Contains(c))
					{
						this.m_InputFied.text = this.m_InputFied.text.Replace(c.ToString(), string.Empty);
					}
				}
			}
			else if (this.type == TmpInputValidation.ValidationType.NoSpace && inputInfo.Length > 0 && inputInfo.Contains(" "))
			{
				this.m_InputFied.text = this.m_InputFied.text.Replace(" ", string.Empty);
			}
			if (this.maxLenght > 0 && inputInfo.Length > this.maxLenght)
			{
				this.m_InputFied.text = inputInfo.Substring(0, this.maxLenght);
			}
		}

		// Token: 0x0400D2D3 RID: 53971
		public int maxLenght;

		// Token: 0x0400D2D4 RID: 53972
		private TMP_InputField m_InputFied;

		// Token: 0x0400D2D5 RID: 53973
		public TmpInputValidation.ValidationType type = TmpInputValidation.ValidationType.Path;

		// Token: 0x0400D2D6 RID: 53974
		private static readonly List<char> InvalidPathChars = new List<char> { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };

		// Token: 0x02001412 RID: 5138
		public enum ValidationType
		{
			// Token: 0x0400D2D8 RID: 53976
			None,
			// Token: 0x0400D2D9 RID: 53977
			Path,
			// Token: 0x0400D2DA RID: 53978
			NoSpace
		}
	}
}
