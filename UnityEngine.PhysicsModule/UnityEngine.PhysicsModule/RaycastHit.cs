using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000016 RID: 22
	[UsedByNativeCode]
	[NativeHeader("Modules/Physics/RaycastHit.h")]
	[NativeHeader("PhysicsScriptingClasses.h")]
	[NativeHeader("Runtime/Interfaces/IRaycast.h")]
	public struct RaycastHit
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003404 File Offset: 0x00001604
		public Collider collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003428 File Offset: 0x00001628
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003440 File Offset: 0x00001640
		public Vector3 point
		{
			get
			{
				return this.m_Point;
			}
			set
			{
				this.m_Point = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000344C File Offset: 0x0000164C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003464 File Offset: 0x00001664
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
			set
			{
				this.m_Normal = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003470 File Offset: 0x00001670
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003488 File Offset: 0x00001688
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = value;
			}
		}

		// Token: 0x0400005D RID: 93
		[NativeName("point")]
		internal Vector3 m_Point;

		// Token: 0x0400005E RID: 94
		[NativeName("normal")]
		internal Vector3 m_Normal;

		// Token: 0x0400005F RID: 95
		[NativeName("faceID")]
		internal uint m_FaceID;

		// Token: 0x04000060 RID: 96
		[NativeName("distance")]
		internal float m_Distance;

		// Token: 0x04000061 RID: 97
		[NativeName("uv")]
		internal Vector2 m_UV;

		// Token: 0x04000062 RID: 98
		[NativeName("collider")]
		internal int m_Collider;
	}
}
