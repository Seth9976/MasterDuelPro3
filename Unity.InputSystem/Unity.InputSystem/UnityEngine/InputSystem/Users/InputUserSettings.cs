using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	internal class InputUserSettings
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00041ED8 File Offset: 0x000400D8
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x00041EE0 File Offset: 0x000400E0
		public string customBindings { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00041EE9 File Offset: 0x000400E9
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00041EF1 File Offset: 0x000400F1
		public bool invertMouseX { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00041EFA File Offset: 0x000400FA
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00041F02 File Offset: 0x00040102
		public bool invertMouseY { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00041F0B File Offset: 0x0004010B
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00041F13 File Offset: 0x00040113
		public float? mouseSmoothing { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00041F1C File Offset: 0x0004011C
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00041F24 File Offset: 0x00040124
		public float? mouseSensitivity { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00041F2D File Offset: 0x0004012D
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00041F35 File Offset: 0x00040135
		public bool invertStickX { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00041F3E File Offset: 0x0004013E
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x00041F46 File Offset: 0x00040146
		public bool invertStickY { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00041F4F File Offset: 0x0004014F
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00041F57 File Offset: 0x00040157
		public bool swapSticks { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00041F60 File Offset: 0x00040160
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x00041F68 File Offset: 0x00040168
		public bool swapBumpers { get; set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00041F71 File Offset: 0x00040171
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x00041F79 File Offset: 0x00040179
		public bool swapTriggers { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00041F82 File Offset: 0x00040182
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00041F8A File Offset: 0x0004018A
		public bool swapDpadAndLeftStick { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00041F93 File Offset: 0x00040193
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x00041F9B File Offset: 0x0004019B
		public float vibrationStrength { get; set; }

		// Token: 0x06000D0B RID: 3339 RVA: 0x000049FE File Offset: 0x00002BFE
		public virtual void Apply(IInputActionCollection actions)
		{
		}

		// Token: 0x04000641 RID: 1601
		[SerializeField]
		private string m_CustomBindings;
	}
}
