using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("RaycastHit2D", "struct RaycastHit2D;")]
	[NativeHeader("Runtime/Interfaces/IPhysics2D.h")]
	public struct RaycastHit2D
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000028A4 File Offset: 0x00000AA4
		public Vector2 point
		{
			get
			{
				return this.m_Point;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000028BC File Offset: 0x00000ABC
		public Vector2 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000028D4 File Offset: 0x00000AD4
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028EC File Offset: 0x00000AEC
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x04000039 RID: 57
		[NativeName("centroid")]
		private Vector2 m_Centroid;

		// Token: 0x0400003A RID: 58
		[NativeName("point")]
		private Vector2 m_Point;

		// Token: 0x0400003B RID: 59
		[NativeName("normal")]
		private Vector2 m_Normal;

		// Token: 0x0400003C RID: 60
		[NativeName("distance")]
		private float m_Distance;

		// Token: 0x0400003D RID: 61
		[NativeName("fraction")]
		private float m_Fraction;

		// Token: 0x0400003E RID: 62
		[NativeName("collider")]
		private int m_Collider;
	}
}
