using System;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013B9 RID: 5049
	public class SelectionToggle_CardCollectionTab : SelectionToggle
	{
		// Token: 0x0600925B RID: 37467 RVA: 0x0014662E File Offset: 0x0014482E
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.toggleWhenSelected = true;
		}

		// Token: 0x0600925C RID: 37468 RVA: 0x001479FB File Offset: 0x00145BFB
		private void Start()
		{
			if (Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.defaultArea == this.area)
			{
				this.SetToggleOn(true);
			}
		}

		// Token: 0x0600925D RID: 37469 RVA: 0x00147A28 File Offset: 0x00145C28
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.ShowArea(this.area);
			SelectionToggle_CardCollectionTab[] componentsInChildren = base.transform.parent.GetComponentsInChildren<SelectionToggle_CardCollectionTab>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetShortcut(this.area);
			}
		}

		// Token: 0x0600925E RID: 37470 RVA: 0x00147A88 File Offset: 0x00145C88
		protected void SetShortcut(CardCollectionView.Area area)
		{
			bool show = this.area == (area + 1) % (CardCollectionView.Area)3;
			base.Manager.GetElement<ShortcutIcon>("ShortcutIcon").Show = show;
		}

		// Token: 0x0600925F RID: 37471 RVA: 0x00147ABC File Offset: 0x00145CBC
		public void OnRightSelection()
		{
			AudioManager.PlaySE(this.SoundLabelClick, 1f);
			Selectable rightSelectable = base.Selectable.navigation.selectOnRight;
			if (this.isOn)
			{
				if (rightSelectable != null)
				{
					rightSelectable.GetComponent<SelectionToggle_CardCollectionTab>().SetToggleOn(true);
					return;
				}
			}
			else if (rightSelectable != null)
			{
				rightSelectable.GetComponent<SelectionToggle_CardCollectionTab>().OnRightSelection();
			}
		}

		// Token: 0x0400D0DB RID: 53467
		[Header("SelectionToggle CardCollectionTab")]
		[SerializeField]
		private CardCollectionView.Area area;
	}
}
