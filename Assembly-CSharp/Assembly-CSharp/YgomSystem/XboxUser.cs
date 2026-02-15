using System;

namespace YgomSystem
{
	// Token: 0x020004E8 RID: 1256
	public class XboxUser
	{
		// Token: 0x060027E7 RID: 10215 RVA: 0x000F1669 File Offset: 0x000EF869
		public static ulong GetMyUserId()
		{
			return 0UL;
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowGamerCard(ulong xuid, Action<bool> onEnd)
		{
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowMyGamerCard(Action<bool> onEnd)
		{
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResolvePriviledgeProblem(XboxUser.Privilege target, Action<bool> onEnd)
		{
		}

		// Token: 0x020004E9 RID: 1257
		public enum Privilege
		{
			// Token: 0x040028B5 RID: 10421
			MULTIPLAY,
			// Token: 0x040028B6 RID: 10422
			CROSSPLAY,
			// Token: 0x040028B7 RID: 10423
			ADD_FRIEND,
			// Token: 0x040028B8 RID: 10424
			COMMUNICATION
		}
	}
}
