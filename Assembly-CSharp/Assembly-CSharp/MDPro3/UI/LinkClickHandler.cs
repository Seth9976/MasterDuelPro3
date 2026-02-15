using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x02001402 RID: 5122
	[RequireComponent(typeof(TMP_Text))]
	public class LinkClickHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x06009453 RID: 37971 RVA: 0x00152554 File Offset: 0x00150754
		public void OnPointerClick(PointerEventData eventData)
		{
			TMP_Text tmp = base.GetComponent<TMP_Text>();
			int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmp, eventData.position, Program.instance.camera_.cameraUI);
			if (linkIndex != -1)
			{
				TMP_LinkInfo linkInfo = tmp.textInfo.linkInfo[linkIndex];
				Application.OpenURL(linkInfo.GetLinkID());
			}
		}
	}
}
