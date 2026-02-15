using System;
using YgomGame.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000CA9 RID: 3241
	public class BezierMotionSettingManager : ScriptableObjectManager<BezierMotionSetting>
	{
		// Token: 0x06005C69 RID: 23657 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Setup(Action on_finished)
		{
		}

		// Token: 0x040097D7 RID: 38871
		public const string motionPathCardShowMoveHandNear = "Duel/ScriptableObject/BezierMotion/CardShowMoveHandNear";

		// Token: 0x040097D8 RID: 38872
		public const string motionPathCardShowMoveHandFar = "Duel/ScriptableObject/BezierMotion/CardShowMoveHandFar";

		// Token: 0x040097D9 RID: 38873
		public const string motionPathCardShowMoveFieldNear = "Duel/ScriptableObject/BezierMotion/CardShowMoveFieldNear";

		// Token: 0x040097DA RID: 38874
		public const string motionPathCardShowMoveFieldFar = "Duel/ScriptableObject/BezierMotion/CardShowMoveFieldFar";

		// Token: 0x040097DB RID: 38875
		public const string motionPathCardShowBackHandNear = "Duel/ScriptableObject/BezierMotion/CardShowBackHandNear";

		// Token: 0x040097DC RID: 38876
		public const string motionPathCardShowBackHandFar = "Duel/ScriptableObject/BezierMotion/CardShowBackHandFar";

		// Token: 0x040097DD RID: 38877
		public const string motionPathCardShowBackFieldNear = "Duel/ScriptableObject/BezierMotion/CardShowBackFieldNear";

		// Token: 0x040097DE RID: 38878
		public const string motionPathCardShowBackFieldFar = "Duel/ScriptableObject/BezierMotion/CardShowBackFieldFar";

		// Token: 0x040097DF RID: 38879
		public const string motionPathCardShowApplyMoveHandNear = "Duel/ScriptableObject/BezierMotion/CardShowApplyMoveHandNear";

		// Token: 0x040097E0 RID: 38880
		public const string motionPathCardShowApplyMoveHandFar = "Duel/ScriptableObject/BezierMotion/CardShowApplyMoveHandFar";

		// Token: 0x040097E1 RID: 38881
		public const string motionPathCardShowApplyMoveFieldNear = "Duel/ScriptableObject/BezierMotion/CardShowApplyMoveFieldNear";

		// Token: 0x040097E2 RID: 38882
		public const string motionPathCardShowApplyMoveFieldFar = "Duel/ScriptableObject/BezierMotion/CardShowApplyMoveFieldFar";

		// Token: 0x040097E3 RID: 38883
		public const string motionPathCardShowApplyBack = "Duel/ScriptableObject/BezierMotion/CardShowApplyBack";

		// Token: 0x040097E4 RID: 38884
		public const string motionPathCardShowHappen = "Duel/ScriptableObject/BezierMotion/CardShowHappen";

		// Token: 0x040097E5 RID: 38885
		public const string motionPathCardShowDisabled = "Duel/ScriptableObject/BezierMotion/CardShowDisabled";

		// Token: 0x040097E6 RID: 38886
		public const string motionPathCardShowApply = "Duel/ScriptableObject/BezierMotion/CardShowApply";

		// Token: 0x040097E7 RID: 38887
		public const string motionPathCardFlipCard = "Duel/ScriptableObject/BezierMotion/CardFlipCard";

		// Token: 0x040097E8 RID: 38888
		public const string motionPathCardFlipPlateMonster = "Duel/ScriptableObject/BezierMotion/CardFlipPlateMonster";

		// Token: 0x040097E9 RID: 38889
		public const string motionPathCardFlipPlateMagic = "Duel/ScriptableObject/BezierMotion/CardFlipPlateMagic";

		// Token: 0x040097EA RID: 38890
		public const string motionPathCardFlipDeckCard = "Duel/ScriptableObject/BezierMotion/CardFlipDeckCard";
	}
}
