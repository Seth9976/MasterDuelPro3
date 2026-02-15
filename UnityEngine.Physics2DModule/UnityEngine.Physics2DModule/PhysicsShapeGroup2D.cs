using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	public class PhysicsShapeGroup2D
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026B4 File Offset: 0x000008B4
		internal List<Vector2> groupVertices
		{
			get
			{
				return this.m_GroupState.m_Vertices;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026D4 File Offset: 0x000008D4
		internal List<PhysicsShape2D> groupShapes
		{
			get
			{
				return this.m_GroupState.m_Shapes;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026F4 File Offset: 0x000008F4
		public int shapeCount
		{
			get
			{
				return this.m_GroupState.m_Shapes.Count;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002718 File Offset: 0x00000918
		public PhysicsShapeGroup2D([DefaultValue("1")] int shapeCapacity = 1, [DefaultValue("8")] int vertexCapacity = 8)
		{
			this.m_GroupState = new PhysicsShapeGroup2D.GroupState
			{
				m_Shapes = new List<PhysicsShape2D>(shapeCapacity),
				m_Vertices = new List<Vector2>(vertexCapacity),
				m_LocalToWorld = Matrix4x4.identity
			};
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002762 File Offset: 0x00000962
		public void Clear()
		{
			this.m_GroupState.ClearGeometry();
			this.m_GroupState.m_LocalToWorld = Matrix4x4.identity;
		}

		// Token: 0x04000014 RID: 20
		internal PhysicsShapeGroup2D.GroupState m_GroupState;

		// Token: 0x02000008 RID: 8
		[NativeHeader(Header = "Modules/Physics2D/Public/PhysicsScripting2D.h")]
		internal struct GroupState
		{
			// Token: 0x06000034 RID: 52 RVA: 0x00002781 File Offset: 0x00000981
			public void ClearGeometry()
			{
				this.m_Shapes.Clear();
				this.m_Vertices.Clear();
			}

			// Token: 0x04000015 RID: 21
			[NativeName("shapesList")]
			public List<PhysicsShape2D> m_Shapes;

			// Token: 0x04000016 RID: 22
			[NativeName("verticesList")]
			public List<Vector2> m_Vertices;

			// Token: 0x04000017 RID: 23
			[NativeName("localToWorld")]
			public Matrix4x4 m_LocalToWorld;
		}
	}
}
