using System;
using UnityEngine;
using UnityEngine.UI;

namespace YgomGame.Scenario
{
	// Token: 0x020009DE RID: 2526
	public class ScenarioRootScreen : MonoBehaviour
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isPlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioRootScreen.State state
		{
			get
			{
				return ScenarioRootScreen.State.None;
			}
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x0000216A File Offset: 0x0000036A
		public static ScenarioRootScreen Create(Image target)
		{
			return null;
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFadeIn(float duration)
		{
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x0000216D File Offset: 0x0000036D
		public void PlayFadeOut(float duration)
		{
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x0000216D File Offset: 0x0000036D
		private void Setup(float toAlpha, float duration)
		{
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x0400875D RID: 34653
		private Image m_Target;

		// Token: 0x0400875E RID: 34654
		private float m_FadeDuration;

		// Token: 0x0400875F RID: 34655
		private float m_CurrentSec;

		// Token: 0x04008760 RID: 34656
		private float m_FromAlpha;

		// Token: 0x04008761 RID: 34657
		private float m_ToAlpha;

		// Token: 0x04008762 RID: 34658
		private ScenarioRootScreen.State m_State;

		// Token: 0x020009DF RID: 2527
		public enum State
		{
			// Token: 0x04008764 RID: 34660
			None,
			// Token: 0x04008765 RID: 34661
			FadeIn,
			// Token: 0x04008766 RID: 34662
			FadeOut
		}
	}
}
