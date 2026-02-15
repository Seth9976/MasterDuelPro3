using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.UI;

namespace YgomGame.Deck
{
	// Token: 0x02000FF0 RID: 4080
	public class ScrollableInputField : MonoBehaviour
	{
		// Token: 0x06007B26 RID: 31526 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0400B257 RID: 45655
		public ExtendedInputField targetInputField;

		// Token: 0x0400B258 RID: 45656
		public MDText cardNameText;

		// Token: 0x0400B259 RID: 45657
		[SerializeField]
		private Button _maskButton;

		// Token: 0x0400B25A RID: 45658
		public UnityEvent OnFocus;
	}
}
