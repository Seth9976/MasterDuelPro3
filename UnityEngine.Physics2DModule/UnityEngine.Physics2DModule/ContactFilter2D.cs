using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
	[NativeClass("ContactFilter", "struct ContactFilter;")]
	[Serializable]
	public struct ContactFilter2D
	{
		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CheckConsistency();

		// Token: 0x06000036 RID: 54 RVA: 0x0000279C File Offset: 0x0000099C
		public void SetLayerMask(LayerMask layerMask)
		{
			this.layerMask = layerMask;
			this.useLayerMask = true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027AD File Offset: 0x000009AD
		public void SetDepth(float minDepth, float maxDepth)
		{
			this.minDepth = minDepth;
			this.maxDepth = maxDepth;
			this.useDepth = true;
			this.CheckConsistency();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027CC File Offset: 0x000009CC
		internal static ContactFilter2D CreateLegacyFilter(int layerMask, float minDepth, float maxDepth)
		{
			ContactFilter2D contactFilter = default(ContactFilter2D);
			contactFilter.useTriggers = Physics2D.queriesHitTriggers;
			contactFilter.SetLayerMask(layerMask);
			contactFilter.SetDepth(minDepth, maxDepth);
			return contactFilter;
		}

		// Token: 0x04000018 RID: 24
		[NativeName("m_UseTriggers")]
		public bool useTriggers;

		// Token: 0x04000019 RID: 25
		[NativeName("m_UseLayerMask")]
		public bool useLayerMask;

		// Token: 0x0400001A RID: 26
		[NativeName("m_UseDepth")]
		public bool useDepth;

		// Token: 0x0400001B RID: 27
		[NativeName("m_UseOutsideDepth")]
		public bool useOutsideDepth;

		// Token: 0x0400001C RID: 28
		[NativeName("m_UseNormalAngle")]
		public bool useNormalAngle;

		// Token: 0x0400001D RID: 29
		[NativeName("m_UseOutsideNormalAngle")]
		public bool useOutsideNormalAngle;

		// Token: 0x0400001E RID: 30
		[NativeName("m_LayerMask")]
		public LayerMask layerMask;

		// Token: 0x0400001F RID: 31
		[NativeName("m_MinDepth")]
		public float minDepth;

		// Token: 0x04000020 RID: 32
		[NativeName("m_MaxDepth")]
		public float maxDepth;

		// Token: 0x04000021 RID: 33
		[NativeName("m_MinNormalAngle")]
		public float minNormalAngle;

		// Token: 0x04000022 RID: 34
		[NativeName("m_MaxNormalAngle")]
		public float maxNormalAngle;
	}
}
