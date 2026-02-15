using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	public struct AnimatorClipInfo
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002D68 File Offset: 0x00000F68
		public AnimationClip clip
		{
			get
			{
				return (this.m_ClipInstanceID != 0) ? AnimatorClipInfo.InstanceIDToAnimationClipPPtr(this.m_ClipInstanceID) : null;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D90 File Offset: 0x00000F90
		public float weight
		{
			get
			{
				return this.m_Weight;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DA8 File Offset: 0x00000FA8
		[FreeFunction("AnimationBindings::InstanceIDToAnimationClipPPtr")]
		private static AnimationClip InstanceIDToAnimationClipPPtr(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimatorClipInfo.InstanceIDToAnimationClipPPtr_Injected(instanceID));
		}

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InstanceIDToAnimationClipPPtr_Injected(int instanceID);

		// Token: 0x0400004A RID: 74
		private int m_ClipInstanceID;

		// Token: 0x0400004B RID: 75
		private float m_Weight;
	}
}
