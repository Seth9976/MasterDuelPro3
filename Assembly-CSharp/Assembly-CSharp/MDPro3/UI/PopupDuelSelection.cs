using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001395 RID: 5013
	public class PopupDuelSelection : PopupDuel
	{
		// Token: 0x060090F2 RID: 37106 RVA: 0x0014010C File Offset: 0x0013E30C
		public override void InitializeSelections()
		{
			base.InitializeSelections();
			Program.instance.currentServant.returnAction = null;
			for (int i = 1; i < this.selections.Count; i++)
			{
				GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.item);
				gameObject.transform.SetParent(this.scrollRect.content, false);
				gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>()
					.text = this.selections[i];
				gameObject.transform.GetChild(0).name = this.responses[i - 1].ToString();
				gameObject.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
				{
					string selected = EventSystem.current.currentSelectedGameObject.name;
					if (selected != "-233")
					{
						BinaryMaster binaryMaster = new BinaryMaster(null);
						binaryMaster.writer.Write(int.Parse(selected));
						base.SendReturn(binaryMaster.Get());
					}
					this.Hide();
				});
				gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, (float)(-20 - 90 * (i - 1)));
			}
			this.scrollRect.content.sizeDelta = new Vector2(this.scrollRect.content.sizeDelta.x, (float)(25 + (this.selections.Count - 1) * 90));
			this.baseRect.sizeDelta = new Vector2(this.baseRect.sizeDelta.x, (this.scrollRect.content.sizeDelta.y + 50f > 800f) ? 800f : (this.scrollRect.content.sizeDelta.y + 50f));
		}

		// Token: 0x0400CFBF RID: 53183
		[Header("Popup Duel Select Reference")]
		public ScrollRect scrollRect;

		// Token: 0x0400CFC0 RID: 53184
		public GameObject item;

		// Token: 0x0400CFC1 RID: 53185
		public RectTransform baseRect;

		// Token: 0x0400CFC2 RID: 53186
		public List<int> responses;
	}
}
