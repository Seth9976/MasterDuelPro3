using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;
using YgomGame.Effect;

namespace YgomGame.Deck
{
	// Token: 0x02000FC2 RID: 4034
	public class CraftEffect
	{
		// Token: 0x060078BC RID: 30908 RVA: 0x0000216A File Offset: 0x0000036A
		public static CraftEffect Create(CraftEffect.Mode mode, Transform parent, RectTransform targetCard, RectTransform targetPoint, List<BezierMotionSetting> motionList, bool actionMenu)
		{
			return null;
		}

		// Token: 0x060078BD RID: 30909 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize(CraftEffect.Mode mode, Transform parent, RectTransform targetCard, RectTransform targetPoint, List<BezierMotionSetting> motionList, bool actionMenu)
		{
		}

		// Token: 0x060078BE RID: 30910 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartEffect(Action onPlayPointEffect, Action onFinished)
		{
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator UpdateCreateEffect(Action onPlayPointEffect)
		{
			return null;
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator UpdateDismantleEffect(Action onPlayPointEffect)
		{
			return null;
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x0000216D File Offset: 0x0000036D
		public void Finish()
		{
		}

		// Token: 0x0400B077 RID: 45175
		private CraftEffect.Mode mode;

		// Token: 0x0400B078 RID: 45176
		private const string prefabPathCraftEffectCreateCard = "Prefabs/UI/DeckEdit/fxp_DeckEditUI/fxp_DeckEditUI_generate_001";

		// Token: 0x0400B079 RID: 45177
		private const string prefabPathCraftEffectDismantleCard = "Prefabs/UI/DeckEdit/fxp_DeckEditUI/fxp_DeckEditUI_dismantle_001";

		// Token: 0x0400B07A RID: 45178
		private const string prefabPathCraftEffectTrail = "Prefabs/UI/DeckEdit/fxp_DeckEditUI/fxp_DeckEditUI_trail_001";

		// Token: 0x0400B07B RID: 45179
		private const string prefabPathCraftEffectPoint = "Prefabs/UI/DeckEdit/fxp_DeckEditUI/fxp_DeckEditUI_point_001";

		// Token: 0x0400B07C RID: 45180
		private EffectHandler effectCard;

		// Token: 0x0400B07D RID: 45181
		private EffectHandler effectTrail;

		// Token: 0x0400B07E RID: 45182
		private EffectHandler effectPoint;

		// Token: 0x0400B07F RID: 45183
		private ChainedBezierMotion motion;

		// Token: 0x0400B080 RID: 45184
		private float time;

		// Token: 0x0400B081 RID: 45185
		private CraftEffect.Step step;

		// Token: 0x0400B082 RID: 45186
		private Action onFinished;

		// Token: 0x02000FC3 RID: 4035
		public enum Mode
		{
			// Token: 0x0400B084 RID: 45188
			Create,
			// Token: 0x0400B085 RID: 45189
			Dismantle
		}

		// Token: 0x02000FC4 RID: 4036
		private enum Step
		{
			// Token: 0x0400B087 RID: 45191
			Loading,
			// Token: 0x0400B088 RID: 45192
			CardEffect,
			// Token: 0x0400B089 RID: 45193
			TrailEffect,
			// Token: 0x0400B08A RID: 45194
			PointEffect,
			// Token: 0x0400B08B RID: 45195
			Finished
		}
	}
}
