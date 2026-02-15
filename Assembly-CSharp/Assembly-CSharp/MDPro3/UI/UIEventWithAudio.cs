using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x02001384 RID: 4996
	public class UIEventWithAudio : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler
	{
		// Token: 0x06009092 RID: 37010 RVA: 0x0013D0B4 File Offset: 0x0013B2B4
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				this.PlayAudio(this.clickAudio);
			}
		}

		// Token: 0x06009093 RID: 37011 RVA: 0x0013D0CA File Offset: 0x0013B2CA
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.PlayAudio(this.enterAudio);
		}

		// Token: 0x06009094 RID: 37012 RVA: 0x0013D0D8 File Offset: 0x0013B2D8
		public void OnPointerExit(PointerEventData eventData)
		{
			this.PlayAudio(this.exitAudio);
		}

		// Token: 0x06009095 RID: 37013 RVA: 0x0013D0E6 File Offset: 0x0013B2E6
		private void PlayAudio(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			if (this.audioType == UIEventWithAudio.AudioType.SE)
			{
				AudioManager.PlaySE(path, 1f);
				return;
			}
			if (this.audioType == UIEventWithAudio.AudioType.Voice)
			{
				AudioManager.PlayVoiceByResourcePath(path);
			}
		}

		// Token: 0x06009096 RID: 37014 RVA: 0x0013D114 File Offset: 0x0013B314
		public void OnSubmit(BaseEventData eventData)
		{
			this.PlayAudio(this.submitAudio);
		}

		// Token: 0x0400CF45 RID: 53061
		public AudioClip previewClip;

		// Token: 0x0400CF46 RID: 53062
		public string enterAudio;

		// Token: 0x0400CF47 RID: 53063
		public string clickAudio;

		// Token: 0x0400CF48 RID: 53064
		public string exitAudio;

		// Token: 0x0400CF49 RID: 53065
		public string submitAudio;

		// Token: 0x0400CF4A RID: 53066
		public UIEventWithAudio.AudioType audioType;

		// Token: 0x02001385 RID: 4997
		public enum AudioType
		{
			// Token: 0x0400CF4C RID: 53068
			SE,
			// Token: 0x0400CF4D RID: 53069
			BGM,
			// Token: 0x0400CF4E RID: 53070
			Voice
		}
	}
}
