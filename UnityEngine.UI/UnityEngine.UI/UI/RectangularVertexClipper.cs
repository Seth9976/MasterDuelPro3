using System;

namespace UnityEngine.UI
{
	// Token: 0x02000010 RID: 16
	internal class RectangularVertexClipper
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002BEC File Offset: 0x00000DEC
		public Rect GetCanvasRect(RectTransform t, Canvas c)
		{
			if (c == null)
			{
				return default(Rect);
			}
			t.GetWorldCorners(this.m_WorldCorners);
			Transform canvasTransform = c.GetComponent<Transform>();
			for (int i = 0; i < 4; i++)
			{
				this.m_CanvasCorners[i] = canvasTransform.InverseTransformPoint(this.m_WorldCorners[i]);
			}
			return new Rect(this.m_CanvasCorners[0].x, this.m_CanvasCorners[0].y, this.m_CanvasCorners[2].x - this.m_CanvasCorners[0].x, this.m_CanvasCorners[2].y - this.m_CanvasCorners[0].y);
		}

		// Token: 0x0400002F RID: 47
		private readonly Vector3[] m_WorldCorners = new Vector3[4];

		// Token: 0x04000030 RID: 48
		private readonly Vector3[] m_CanvasCorners = new Vector3[4];
	}
}
