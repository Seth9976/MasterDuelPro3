using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E86 RID: 3718
	public class EngineBusyIdTable
	{
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06006BB4 RID: 27572 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<Engine.ViewType, Engine.ViewType> table
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006BB5 RID: 27573 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Engine.ViewType GetBusyEffectId(Engine.ViewType runEffectId)
		{
			return Engine.ViewType.Null;
		}

		// Token: 0x0400A773 RID: 42867
		private static readonly Dictionary<Engine.ViewType, Engine.ViewType> tbl;
	}
}
