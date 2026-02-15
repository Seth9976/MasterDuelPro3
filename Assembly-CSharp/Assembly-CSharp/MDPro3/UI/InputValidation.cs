using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001400 RID: 5120
	[RequireComponent(typeof(InputField))]
	public class InputValidation : MonoBehaviour
	{
		// Token: 0x0600944F RID: 37967 RVA: 0x001523DB File Offset: 0x001505DB
		private void Awake()
		{
			this.m_InputFied = base.GetComponent<InputField>();
			this.m_InputFied.onValueChanged.AddListener(new UnityAction<string>(this.OnInputFieldValueChange));
		}

		// Token: 0x06009450 RID: 37968 RVA: 0x00152408 File Offset: 0x00150608
		private void OnInputFieldValueChange(string inputInfo)
		{
			if (this.type == InputValidation.ValidationType.Path)
			{
				foreach (char c in inputInfo)
				{
					if (InputValidation.InvalidPathChars.Contains(c))
					{
						this.m_InputFied.text = this.m_InputFied.text.Replace(c.ToString(), string.Empty);
					}
				}
			}
			else if (this.type == InputValidation.ValidationType.NoSpace && inputInfo.Length > 0 && inputInfo.Contains(" "))
			{
				this.m_InputFied.text = this.m_InputFied.text.Replace(" ", string.Empty);
			}
			if (this.maxLenght > 0 && inputInfo.Length > this.maxLenght)
			{
				this.m_InputFied.text = inputInfo.Substring(0, this.maxLenght);
			}
		}

		// Token: 0x0400D28D RID: 53901
		public int maxLenght;

		// Token: 0x0400D28E RID: 53902
		private InputField m_InputFied;

		// Token: 0x0400D28F RID: 53903
		public InputValidation.ValidationType type = InputValidation.ValidationType.Path;

		// Token: 0x0400D290 RID: 53904
		private static readonly List<char> InvalidPathChars = new List<char> { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };

		// Token: 0x02001401 RID: 5121
		public enum ValidationType
		{
			// Token: 0x0400D292 RID: 53906
			None,
			// Token: 0x0400D293 RID: 53907
			Path,
			// Token: 0x0400D294 RID: 53908
			NoSpace
		}
	}
}
