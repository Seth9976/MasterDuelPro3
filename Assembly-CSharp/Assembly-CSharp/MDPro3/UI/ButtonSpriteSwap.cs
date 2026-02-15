using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200137C RID: 4988
	public class ButtonSpriteSwap : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x0600905D RID: 36957 RVA: 0x0013BBA9 File Offset: 0x00139DA9
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.swap.sprite = this.hoverSprite;
			AudioManager.PlaySE(this.hoverAudio, 1f);
		}

		// Token: 0x0600905E RID: 36958 RVA: 0x0013BBCC File Offset: 0x00139DCC
		public void OnPointerExit(PointerEventData eventData)
		{
			this.swap.sprite = this.normalSprite;
		}

		// Token: 0x0600905F RID: 36959 RVA: 0x0013BBDF File Offset: 0x00139DDF
		public void OnPointerDown(PointerEventData eventData)
		{
			this.swap.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		}

		// Token: 0x06009060 RID: 36960 RVA: 0x0013BC05 File Offset: 0x00139E05
		public void OnPointerUp(PointerEventData eventData)
		{
			this.swap.color = Color.white;
		}

		// Token: 0x0400CF06 RID: 52998
		public Image swap;

		// Token: 0x0400CF07 RID: 52999
		public Sprite normalSprite;

		// Token: 0x0400CF08 RID: 53000
		public Sprite hoverSprite;

		// Token: 0x0400CF09 RID: 53001
		public string hoverAudio;
	}
}
