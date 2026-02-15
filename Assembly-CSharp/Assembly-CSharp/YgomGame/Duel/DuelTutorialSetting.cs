using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000DA9 RID: 3497
	public class DuelTutorialSetting : ScriptableObject
	{
		// Token: 0x060066B5 RID: 26293 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTutorialChapter(int chapter)
		{
			return false;
		}

		// Token: 0x0400A108 RID: 41224
		public List<DuelTutorialSetting.TutorialChapter> tutorialChapterList;

		// Token: 0x02000DAA RID: 3498
		[Serializable]
		public class TutorialChapter
		{
			// Token: 0x0400A109 RID: 41225
			public int chapter;
		}
	}
}
