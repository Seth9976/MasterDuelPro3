using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000642 RID: 1602
	public class TweenStretchTo : Tween
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_RectTransform
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002EBD RID: 11965
		protected TweenStretchTo.StretchOffset m_From;

		// Token: 0x04002EBE RID: 11966
		[SerializeField]
		public TweenStretchTo.StretchOffset to;

		// Token: 0x04002EBF RID: 11967
		[SerializeField]
		public bool ResetFromOnEnable;

		// Token: 0x04002EC0 RID: 11968
		private RectTransform m_RectTransform_Field;

		// Token: 0x02000643 RID: 1603
		[Serializable]
		public struct StretchOffset
		{
			// Token: 0x06003221 RID: 12833 RVA: 0x000F2910 File Offset: 0x000F0B10
			public static TweenStretchTo.StretchOffset operator +(TweenStretchTo.StretchOffset a, TweenStretchTo.StretchOffset b)
			{
				return default(TweenStretchTo.StretchOffset);
			}

			// Token: 0x06003222 RID: 12834 RVA: 0x000F2928 File Offset: 0x000F0B28
			public static TweenStretchTo.StretchOffset operator *(TweenStretchTo.StretchOffset a, TweenStretchTo.StretchOffset b)
			{
				return default(TweenStretchTo.StretchOffset);
			}

			// Token: 0x06003223 RID: 12835 RVA: 0x000F2940 File Offset: 0x000F0B40
			public static TweenStretchTo.StretchOffset operator *(TweenStretchTo.StretchOffset a, float b)
			{
				return default(TweenStretchTo.StretchOffset);
			}

			// Token: 0x06003224 RID: 12836 RVA: 0x000F2958 File Offset: 0x000F0B58
			public static TweenStretchTo.StretchOffset operator -(TweenStretchTo.StretchOffset a, TweenStretchTo.StretchOffset b)
			{
				return default(TweenStretchTo.StretchOffset);
			}

			// Token: 0x04002EC1 RID: 11969
			public float left;

			// Token: 0x04002EC2 RID: 11970
			public float right;

			// Token: 0x04002EC3 RID: 11971
			public float top;

			// Token: 0x04002EC4 RID: 11972
			public float bottom;
		}
	}
}
