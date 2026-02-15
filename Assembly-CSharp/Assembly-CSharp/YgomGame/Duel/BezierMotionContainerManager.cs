using System;
using YgomGame.Utility;

namespace YgomGame.Duel
{
	// Token: 0x02000C99 RID: 3225
	public class BezierMotionContainerManager : ScriptableObjectManager<BezierMotionContainer>
	{
		// Token: 0x06005C52 RID: 23634 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Setup(Action on_finished)
		{
		}

		// Token: 0x04009786 RID: 38790
		public const string motionPathDrawPhaseDrawFirst = "Duel/ScriptableObject/BezierMotion/MotionContainerDrawFirst";

		// Token: 0x04009787 RID: 38791
		public const string motionPathDrawPhaseDrawLatter = "Duel/ScriptableObject/BezierMotion/MotionContainerDrawLatter";

		// Token: 0x04009788 RID: 38792
		public const string motionPathDrawPhaseToHand = "Duel/ScriptableObject/BezierMotion/MotionContainerDrawToHand";

		// Token: 0x04009789 RID: 38793
		public const string motionPathDrawPhaseDeckCenter = "Duel/ScriptableObject/BezierMotion/MotionContainerDeckCenter";

		// Token: 0x0400978A RID: 38794
		public const string motionPathDrawPhaseDeckBack = "Duel/ScriptableObject/BezierMotion/MotionContainerDeckBack";

		// Token: 0x0400978B RID: 38795
		public const string motionPathCardMoveStrongSummon = "Duel/ScriptableObject/BezierMotion/CardMoveMotion/MotionContainerStrongSummon";

		// Token: 0x0400978C RID: 38796
		public const string motionPathDeckReverse = "Duel/ScriptableObject/BezierMotion/MosionContainerDeckReverse";
	}
}
