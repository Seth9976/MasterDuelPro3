using System;
using MDPro3;
using UnityEngine;

namespace Willow
{
	// Token: 0x02001551 RID: 5457
	public class SoundEffectFromName : MonoBehaviour
	{
		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06009E45 RID: 40517 RVA: 0x0019B57F File Offset: 0x0019977F
		// (set) Token: 0x06009E46 RID: 40518 RVA: 0x0000216D File Offset: 0x0000036D
		public string targetName
		{
			private get
			{
				return "SE_" + base.gameObject.name.Replace("(Clone)", "");
			}
			set
			{
			}
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x06009E47 RID: 40519 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E48 RID: 40520 RVA: 0x0000216D File Offset: 0x0000036D
		public bool playOneTime
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06009E49 RID: 40521 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E4A RID: 40522 RVA: 0x0000216D File Offset: 0x0000036D
		public bool donePlay
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06009E4B RID: 40523 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06009E4C RID: 40524 RVA: 0x0019B5A5 File Offset: 0x001997A5
		private void Start()
		{
			AudioManager.PlaySE(this.targetName.ToUpper(), 1f);
		}

		// Token: 0x06009E4D RID: 40525 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06009E4E RID: 40526 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayEffect(Action<int[]> callback)
		{
		}

		// Token: 0x06009E4F RID: 40527 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayEffect(int[] ids)
		{
		}

		// Token: 0x0400DDE5 RID: 56805
		private float m_timeScale;

		// Token: 0x0400DDE6 RID: 56806
		private int[] m_soundIds;

		// Token: 0x0400DDE7 RID: 56807
		[SerializeField]
		private bool m_playOneTime;

		// Token: 0x0400DDE8 RID: 56808
		[SerializeField]
		private bool m_donePlay;

		// Token: 0x0400DDE9 RID: 56809
		[SerializeField]
		private bool m_useCustomTarget;

		// Token: 0x0400DDEA RID: 56810
		[SerializeField]
		private GameObject m_customTarget;

		// Token: 0x0400DDEB RID: 56811
		private string m_targetName;

		// Token: 0x0400DDEC RID: 56812
		private string m_prevTargetName;
	}
}
