using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C98 RID: 3224
	public class BezierMotionContainer : ScriptableObject
	{
		// Token: 0x06005C4F RID: 23631 RVA: 0x0000216A File Offset: 0x0000036A
		public ChainedBezierMotion GetChainedBezierMotion()
		{
			return null;
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x0000216A File Offset: 0x0000036A
		public BezierMotionContainer Clone()
		{
			return null;
		}

		// Token: 0x04009785 RID: 38789
		public List<BezierMotionSetting> motionList;
	}
}
