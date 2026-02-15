using System;
using UnityEngine;
using UnityEngine.Playables;

namespace MDPro3.UI
{
	// Token: 0x0200136F RID: 4975
	public class DoWhenPlayableDirectorStop : MonoBehaviour
	{
		// Token: 0x0600901F RID: 36895 RVA: 0x0013AC1A File Offset: 0x00138E1A
		private void Start()
		{
			this.director = base.GetComponent<PlayableDirector>();
		}

		// Token: 0x06009020 RID: 36896 RVA: 0x0013AC28 File Offset: 0x00138E28
		private void Update()
		{
			if (this.director.state != PlayState.Playing)
			{
				Action action = this.action;
				if (action != null)
				{
					action();
				}
				base.enabled = false;
			}
		}

		// Token: 0x0400CEC1 RID: 52929
		public Action action;

		// Token: 0x0400CEC2 RID: 52930
		private PlayableDirector director;
	}
}
