using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x0200137A RID: 4986
	public class ButtonListManager : MonoBehaviour
	{
		// Token: 0x06009055 RID: 36949 RVA: 0x0013B7E4 File Offset: 0x001399E4
		private void Start()
		{
			this.buttons = base.transform.GetComponentsInChildren<ButtonList>(true);
		}

		// Token: 0x06009056 RID: 36950 RVA: 0x0013B7F8 File Offset: 0x001399F8
		public ButtonList GetButtonListByName(string name)
		{
			foreach (ButtonList button in this.buttons)
			{
				if (button.gameObject.name == name)
				{
					return button;
				}
			}
			return null;
		}

		// Token: 0x0400CEFD RID: 52989
		private ButtonList[] buttons;
	}
}
