using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[NativeHeader(Header = "Modules/Physics2D/Public/PhysicsScripting2D.h")]
	[UsedByNativeCode]
	public struct PhysicsShape2D
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002654 File Offset: 0x00000854
		public PhysicsShapeType2D shapeType
		{
			get
			{
				return this.m_ShapeType;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000266C File Offset: 0x0000086C
		public float radius
		{
			get
			{
				return this.m_Radius;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002684 File Offset: 0x00000884
		public int vertexStartIndex
		{
			get
			{
				return this.m_VertexStartIndex;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000269C File Offset: 0x0000089C
		public int vertexCount
		{
			get
			{
				return this.m_VertexCount;
			}
		}

		// Token: 0x0400000C RID: 12
		private PhysicsShapeType2D m_ShapeType;

		// Token: 0x0400000D RID: 13
		private float m_Radius;

		// Token: 0x0400000E RID: 14
		private int m_VertexStartIndex;

		// Token: 0x0400000F RID: 15
		private int m_VertexCount;

		// Token: 0x04000010 RID: 16
		private int m_UseAdjacentStart;

		// Token: 0x04000011 RID: 17
		private int m_UseAdjacentEnd;

		// Token: 0x04000012 RID: 18
		private Vector2 m_AdjacentStart;

		// Token: 0x04000013 RID: 19
		private Vector2 m_AdjacentEnd;
	}
}
