using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200117A RID: 4474
	public class TextVertexLimitter : BaseMeshEffect
	{
		// Token: 0x060084BD RID: 33981 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ModifyMesh(VertexHelper vh)
		{
		}

		// Token: 0x060084BE RID: 33982 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnModifyMesh(VertexHelper vh, List<UIVertex> verts)
		{
		}

		// Token: 0x060084BF RID: 33983 RVA: 0x000029CC File Offset: 0x00000BCC
		private int RemoveRange(List<UIVertex> verts, int index, int count, List<TextVertexLimitter.TagInfo> tags)
		{
			return 0;
		}

		// Token: 0x060084C0 RID: 33984 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckTag(int index, List<TextVertexLimitter.TagInfo> tags, ref int tagIdx)
		{
			return false;
		}

		// Token: 0x060084C1 RID: 33985 RVA: 0x000F752A File Offset: 0x000F572A
		private IEnumerator GetRegexMatchedTagCollection(string line, out int lineLengthWithoutTags)
		{
			lineLengthWithoutTags = 0;
			return null;
		}

		// Token: 0x0400C04A RID: 49226
		private const string SupportedTagRegexPattern = "<b>|</b>|<i>|</i>|<size=.*?>|</size>|<color=.*?>|</color>|<material=.*?>|</material>";

		// Token: 0x0400C04B RID: 49227
		[SerializeField]
		private bool useRichText;

		// Token: 0x0400C04C RID: 49228
		[SerializeField]
		public int startPos;

		// Token: 0x0400C04D RID: 49229
		[SerializeField]
		public int length;

		// Token: 0x0400C04E RID: 49230
		protected const int VERTLEN = 6;

		// Token: 0x0200117B RID: 4475
		private struct TagInfo
		{
			// Token: 0x0400C04F RID: 49231
			public int Index;

			// Token: 0x0400C050 RID: 49232
			public int Length;
		}
	}
}
