using System;
using UnityEngine;
using YgomSystem.Effect;

namespace YgomSystem.UI
{
	// Token: 0x02000621 RID: 1569
	public class TweenGraphMatFloat : Tween
	{
		// Token: 0x060031C5 RID: 12741 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E41 RID: 11841
		[SerializeField]
		public string field;

		// Token: 0x04002E42 RID: 11842
		[SerializeField]
		public float from;

		// Token: 0x04002E43 RID: 11843
		[SerializeField]
		public float to;

		// Token: 0x04002E44 RID: 11844
		private MaterialSetterGraphWriter m_TargetWriter;
	}
}
