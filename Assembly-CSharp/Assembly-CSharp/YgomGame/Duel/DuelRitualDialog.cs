using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.YGomTMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000D93 RID: 3475
	public class DuelRitualDialog : DuelInfoDialogBase
	{
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600662C RID: 26156 RVA: 0x0000216D File Offset: 0x0000036D
		public Engine.DialogRitualType ritualType
		{
			[CompilerGenerated]
			get
			{
				return Engine.DialogRitualType.Ritual;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600662D RID: 26157 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600662E RID: 26158 RVA: 0x0000216D File Offset: 0x0000036D
		public int remainNum
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600662F RID: 26159 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006630 RID: 26160 RVA: 0x0000216D File Offset: 0x0000036D
		public int maxNum
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06006631 RID: 26161 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006632 RID: 26162 RVA: 0x0000216D File Offset: 0x0000036D
		public string message
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06006633 RID: 26163 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006634 RID: 26164 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isReady
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqOpen(DuelInfoDialogBase.Place place)
		{
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqOpen()
		{
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<DuelRitualDialog> finishCallback)
		{
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CreateUI()
		{
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x0000216D File Offset: 0x0000036D
		public void Begin(string message, Engine.DialogRitualType type, int remainNum, int maxNum)
		{
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x0000216D File Offset: 0x0000036D
		public void End()
		{
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetStarFadeEnable(bool enable, int uniqueId)
		{
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x0000216D File Offset: 0x0000036D
		private void Open(DuelInfoDialogBase.Place place)
		{
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x0000216D File Offset: 0x0000036D
		private void Open()
		{
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupCountGroup(DuelRitualDialog.Mode mode, int maxNum)
		{
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCount(int remainNum)
		{
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x0000216D File Offset: 0x0000036D
		private void DestroyCountObjectList()
		{
		}

		// Token: 0x0400A046 RID: 41030
		private GameObject starGroup;

		// Token: 0x0400A047 RID: 41031
		private GameObject linkGroup;

		// Token: 0x0400A048 RID: 41032
		private GameObject atkGroup;

		// Token: 0x0400A049 RID: 41033
		private ElementObjectManager starTemplate;

		// Token: 0x0400A04A RID: 41034
		private ElementObjectManager linkTemplate;

		// Token: 0x0400A04B RID: 41035
		private ExtendedTextMeshProUGUI textRequireParam;

		// Token: 0x0400A04C RID: 41036
		private ExtendedTextMeshProUGUI textCurrentParam;

		// Token: 0x0400A04D RID: 41037
		private DuelRitualDialog.Mode currentMode;

		// Token: 0x0400A04E RID: 41038
		private List<ElementObjectManager> countObjects;

		// Token: 0x0400A04F RID: 41039
		private const string prefabPath = "Prefabs/Duel/DuelRitualDialog";

		// Token: 0x02000D94 RID: 3476
		public enum Mode
		{
			// Token: 0x0400A051 RID: 41041
			None,
			// Token: 0x0400A052 RID: 41042
			Star,
			// Token: 0x0400A053 RID: 41043
			Link,
			// Token: 0x0400A054 RID: 41044
			Atk
		}

		// Token: 0x02000D95 RID: 3477
		private class RitualDialogOperationInfo : DuelInfoDialogBase.OperationInfo
		{
			// Token: 0x06006642 RID: 26178 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelInfoDialogBase.OperationInfo OpenOperation(DuelRitualDialog dialog, DuelInfoDialogBase.Place place)
			{
				return null;
			}

			// Token: 0x06006643 RID: 26179 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelInfoDialogBase.OperationInfo OpenOperation(DuelRitualDialog dialog)
			{
				return null;
			}
		}
	}
}
