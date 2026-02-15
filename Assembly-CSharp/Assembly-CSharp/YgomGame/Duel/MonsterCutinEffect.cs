using System;
using UnityEngine;
using UnityEngine.Playables;
using YgomGame.Card;

namespace YgomGame.Duel
{
	// Token: 0x02000ED6 RID: 3798
	public class MonsterCutinEffect
	{
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06006EA5 RID: 28325 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLoaded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06006EA6 RID: 28326 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool finished
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006EA7 RID: 28327 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsStrongSummonTarget(int cardID)
		{
			return false;
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x0000216A File Offset: 0x0000036A
		public static MonsterCutinEffect Create()
		{
			return null;
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(int cardID, int rareID, bool isMyself, bool changeBGM, Action loadedCallback)
		{
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x0000216D File Offset: 0x0000036D
		public void Setup(int cardID, int rareID, bool isMyself, string cardName, Content.Attribute attribute, MonsterCutinEffect.LevelType levelType, int level, int attack, int defense, bool changeBGM, Action loadedCallback)
		{
		}

		// Token: 0x06006EAB RID: 28331 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLoaded(bool result)
		{
		}

		// Token: 0x06006EAC RID: 28332 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(Action finishCallback, Action onStartCard, double playOffset = 0.0, bool mute = false)
		{
		}

		// Token: 0x06006EAD RID: 28333 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPlayFinished(Action onFinished)
		{
		}

		// Token: 0x06006EAE RID: 28334 RVA: 0x0000216D File Offset: 0x0000036D
		public void Stop()
		{
		}

		// Token: 0x06006EAF RID: 28335 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x06006EB0 RID: 28336 RVA: 0x0000216D File Offset: 0x0000036D
		private void StopSE(PlayableDirector timeline)
		{
		}

		// Token: 0x06006EB1 RID: 28337 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetScreenSize(float screenWidth, float screenHeight, Camera targetCamera = null)
		{
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetScreenSize(PlayableDirector target, float screenWidth, float screenHeight, Camera targetCamera = null)
		{
		}

		// Token: 0x0400A97A RID: 43386
		private bool isMyself;

		// Token: 0x0400A97B RID: 43387
		private int loadCounter;

		// Token: 0x0400A97C RID: 43388
		private int playCount;

		// Token: 0x0400A97D RID: 43389
		private bool changeBGM;

		// Token: 0x0400A97E RID: 43390
		private Sprite textureLinkNum;

		// Token: 0x0400A97F RID: 43391
		private PlayableDirector monsterTimeline;

		// Token: 0x0400A980 RID: 43392
		private PlayableDirector bgTimeline;

		// Token: 0x0400A981 RID: 43393
		private PlayableDirector nameTimeline;

		// Token: 0x0400A982 RID: 43394
		private PlayableDirector effectTimeline;

		// Token: 0x0400A983 RID: 43395
		private int cardID;

		// Token: 0x0400A984 RID: 43396
		private string cardName;

		// Token: 0x0400A985 RID: 43397
		private Content.Attribute attribute;

		// Token: 0x0400A986 RID: 43398
		private int level;

		// Token: 0x0400A987 RID: 43399
		private MonsterCutinEffect.LevelType levelType;

		// Token: 0x0400A988 RID: 43400
		private int attack;

		// Token: 0x0400A989 RID: 43401
		private int defense;

		// Token: 0x0400A98A RID: 43402
		private bool startCardInvoked;

		// Token: 0x0400A98B RID: 43403
		private bool ready;

		// Token: 0x0400A98C RID: 43404
		private Action loadedCallback;

		// Token: 0x0400A98D RID: 43405
		private const string SUMMON_STRONG_BG_DARK = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/04BackEff/SummonMonster_Bgdak_S2";

		// Token: 0x0400A98E RID: 43406
		private const string SUMMON_STRONG_BG_LIGHT = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/04BackEff/SummonMonster_Bglit_S2";

		// Token: 0x0400A98F RID: 43407
		private const string SUMMON_STRONG_BG_EARTH = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/04BackEff/SummonMonster_Bgeah_S2";

		// Token: 0x0400A990 RID: 43408
		private const string SUMMON_STRONG_BG_FIRE = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/04BackEff/SummonMonster_Bgfie_S2";

		// Token: 0x0400A991 RID: 43409
		private const string SUMMON_STRONG_BG_WIND = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/04BackEff/SummonMonster_Bgwid_S2";

		// Token: 0x0400A992 RID: 43410
		private const string SUMMON_STRONG_BG_WATER = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/04BackEff/SummonMonster_Bgwtr_S2";

		// Token: 0x0400A993 RID: 43411
		private const string SUMMON_STRONG_BG_GOD = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/04BackEff/SummonMonster_Bgdve_S2";

		// Token: 0x0400A994 RID: 43412
		private const string SUMMON_STRONG_NAME_NEAR = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/01Text/SummonMonster_Name_near";

		// Token: 0x0400A995 RID: 43413
		private const string SUMMON_STRONG_NAME_FAR = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/01Text/SummonMonster_Name_far";

		// Token: 0x0400A996 RID: 43414
		private const string SUMMON_STRONG_EFFECT_HIGH = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/02FrontEff/SummonMonster_Thunder_power";

		// Token: 0x0400A997 RID: 43415
		private const string SUMMON_STRONG_EFFECT_MIDDLE = "Duel/Timeline/Duel/Universal/Summon/SummonMonster/02FrontEff/SummonMonster_Thunder_normal";

		// Token: 0x02000ED7 RID: 3799
		public enum LevelType
		{
			// Token: 0x0400A999 RID: 43417
			Level,
			// Token: 0x0400A99A RID: 43418
			Rank,
			// Token: 0x0400A99B RID: 43419
			Link
		}
	}
}
