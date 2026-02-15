using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YgomSystem.UI
{
	// Token: 0x02000638 RID: 1592
	public class TweenSet : Tween
	{
		// Token: 0x060031FB RID: 12795 RVA: 0x0000216D File Offset: 0x0000036D
		private void ScanChildGraphic(GameObject obj)
		{
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CaptureFrom()
		{
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnSetValue(float par)
		{
		}

		// Token: 0x04002E85 RID: 11909
		[SerializeField]
		public TweenSet.Param from;

		// Token: 0x04002E86 RID: 11910
		[SerializeField]
		public TweenSet.Param to;

		// Token: 0x04002E87 RID: 11911
		[SerializeField]
		[EnumFlags]
		public TweenSet.Target target;

		// Token: 0x04002E88 RID: 11912
		private Color fromColor;

		// Token: 0x04002E89 RID: 11913
		private RectTransform rtrans;

		// Token: 0x04002E8A RID: 11914
		private Graphic graphic;

		// Token: 0x04002E8B RID: 11915
		private int crntParam;

		// Token: 0x04002E8C RID: 11916
		private List<KeyValuePair<Graphic, Color>> childGraps;

		// Token: 0x02000639 RID: 1593
		[Flags]
		public enum Target
		{
			// Token: 0x04002E8E RID: 11918
			position = 1,
			// Token: 0x04002E8F RID: 11919
			scale = 2,
			// Token: 0x04002E90 RID: 11920
			rotation = 4,
			// Token: 0x04002E91 RID: 11921
			color = 8,
			// Token: 0x04002E92 RID: 11922
			gameObject = 16
		}

		// Token: 0x0200063A RID: 1594
		[Serializable]
		public class Param
		{
			// Token: 0x04002E93 RID: 11923
			public Vector3 position;

			// Token: 0x04002E94 RID: 11924
			public Vector3 scale;

			// Token: 0x04002E95 RID: 11925
			public Quaternion rotation;

			// Token: 0x04002E96 RID: 11926
			public Color color;

			// Token: 0x04002E97 RID: 11927
			public GameObject gameObject;
		}
	}
}
