using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[NativeClass("ScriptingContactPoint2D", "struct ScriptingContactPoint2D;")]
	[RequiredByNativeCode(Optional = false, GenerateProxy = true)]
	[NativeHeader("Modules/Physics2D/Public/PhysicsScripting2D.h")]
	public struct ContactPoint2D
	{
		// Token: 0x0400002C RID: 44
		[NativeName("point")]
		private Vector2 m_Point;

		// Token: 0x0400002D RID: 45
		[NativeName("normal")]
		private Vector2 m_Normal;

		// Token: 0x0400002E RID: 46
		[NativeName("relativeVelocity")]
		private Vector2 m_RelativeVelocity;

		// Token: 0x0400002F RID: 47
		[NativeName("friction")]
		private float m_Friction;

		// Token: 0x04000030 RID: 48
		[NativeName("bounciness")]
		private float m_Bounciness;

		// Token: 0x04000031 RID: 49
		[NativeName("separation")]
		private float m_Separation;

		// Token: 0x04000032 RID: 50
		[NativeName("normalImpulse")]
		private float m_NormalImpulse;

		// Token: 0x04000033 RID: 51
		[NativeName("tangentImpulse")]
		private float m_TangentImpulse;

		// Token: 0x04000034 RID: 52
		[NativeName("collider")]
		private int m_Collider;

		// Token: 0x04000035 RID: 53
		[NativeName("otherCollider")]
		private int m_OtherCollider;

		// Token: 0x04000036 RID: 54
		[NativeName("rigidbody")]
		private int m_Rigidbody;

		// Token: 0x04000037 RID: 55
		[NativeName("otherRigidbody")]
		private int m_OtherRigidbody;

		// Token: 0x04000038 RID: 56
		[NativeName("enabled")]
		private int m_Enabled;
	}
}
