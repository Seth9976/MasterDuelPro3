using System;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace YgomGame.Solo
{
	// Token: 0x020008F3 RID: 2291
	public class SoloClearViewController : BaseMenuViewController, IFadeSupported
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsLoadingEffect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(SoloClearViewController.ClearType type, Action callback = null)
		{
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x0000216D File Offset: 0x0000036D
		private void Play()
		{
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayEffect(string effectPath)
		{
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x000F4798 File Offset: 0x000F2998
		public Color FadeColor(ViewController.TransitionType type)
		{
			return default(Color);
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x000029CC File Offset: 0x00000BCC
		public SystemProgress.ProgressType FadeType(ViewController.TransitionType type)
		{
			return SystemProgress.ProgressType.None;
		}

		// Token: 0x0400816A RID: 33130
		private const string k_ArgKeyCallback = "callback";

		// Token: 0x0400816B RID: 33131
		private const string k_ArgKeyType = "type";

		// Token: 0x0400816C RID: 33132
		private readonly string k_ELabelBG3D;

		// Token: 0x0400816D RID: 33133
		private readonly string k_ELabelBackShortcutButton;

		// Token: 0x0400816E RID: 33134
		private readonly string k_ELabelButtonSkip;

		// Token: 0x0400816F RID: 33135
		private readonly string k_ELabelRootChapter;

		// Token: 0x04008170 RID: 33136
		private readonly string k_ELabelRootComplete;

		// Token: 0x04008171 RID: 33137
		private readonly string k_ELabelRootGoal;

		// Token: 0x04008172 RID: 33138
		private readonly string k_ELabelRoot;

		// Token: 0x04008173 RID: 33139
		private readonly string IMG_CLEAR_RENTAL_LABEL;

		// Token: 0x04008174 RID: 33140
		private readonly string IMG_BLANK_RENTAL_LABEL;

		// Token: 0x04008175 RID: 33141
		private readonly string IMG_CLEAR_MYDECK_LABEL;

		// Token: 0x04008176 RID: 33142
		private readonly string IMG_BLANK_MYDECK_LABEL;

		// Token: 0x04008177 RID: 33143
		private GameObject m_View3D;

		// Token: 0x04008178 RID: 33144
		private ElementObjectManager m_TargetEom;

		// Token: 0x04008179 RID: 33145
		private bool b_IsFinish;

		// Token: 0x0400817A RID: 33146
		private bool b_IsWhileTutorial;

		// Token: 0x0400817B RID: 33147
		private bool b_IsSkip;

		// Token: 0x0400817C RID: 33148
		private int m_LoadingEffectCount;

		// Token: 0x0400817D RID: 33149
		private SoloClearViewController.ClearType type;

		// Token: 0x020008F4 RID: 2292
		public enum ClearType
		{
			// Token: 0x0400817F RID: 33151
			RENTAL,
			// Token: 0x04008180 RID: 33152
			MYDECK,
			// Token: 0x04008181 RID: 33153
			COMPLETE,
			// Token: 0x04008182 RID: 33154
			GOAL
		}
	}
}
