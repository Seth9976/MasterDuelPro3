using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x02000410 RID: 1040
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeType(CodegenOptions.Custom, "ScriptingSpriteBone")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataMarshalling.h")]
	[RequiredByNativeCode]
	[Serializable]
	public struct SpriteBone
	{
		// Token: 0x04000E84 RID: 3716
		[NativeName("name")]
		[SerializeField]
		private string m_Name;

		// Token: 0x04000E85 RID: 3717
		[NativeName("guid")]
		[SerializeField]
		private string m_Guid;

		// Token: 0x04000E86 RID: 3718
		[NativeName("position")]
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x04000E87 RID: 3719
		[SerializeField]
		[NativeName("rotation")]
		private Quaternion m_Rotation;

		// Token: 0x04000E88 RID: 3720
		[NativeName("length")]
		[SerializeField]
		private float m_Length;

		// Token: 0x04000E89 RID: 3721
		[SerializeField]
		[NativeName("parentId")]
		private int m_ParentId;

		// Token: 0x04000E8A RID: 3722
		[NativeName("color")]
		[SerializeField]
		private Color32 m_Color;
	}
}
