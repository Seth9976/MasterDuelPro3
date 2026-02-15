using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VisualEffectBindings.h")]
	[NativeHeader("Modules/VFX/Public/VisualEffect.h")]
	[RequireComponent(typeof(Transform))]
	public class VisualEffect : Behaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002480 File Offset: 0x00000680
		public VisualEffectAsset visualEffectAsset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<VisualEffectAsset>(VisualEffect.get_visualEffectAsset_Injected(intPtr));
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024A8 File Offset: 0x000006A8
		public VFXEventAttribute CreateVFXEventAttribute()
		{
			bool flag = this.visualEffectAsset == null;
			VFXEventAttribute vfxeventAttribute;
			if (flag)
			{
				vfxeventAttribute = null;
			}
			else
			{
				VFXEventAttribute vfxEventAttribute = VFXEventAttribute.Internal_InstanciateVFXEventAttribute(this.visualEffectAsset);
				vfxeventAttribute = vfxEventAttribute;
			}
			return vfxeventAttribute;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024DC File Offset: 0x000006DC
		[RequiredByNativeCode]
		private static VFXEventAttribute InvokeGetCachedEventAttributeForOutputEvent_Internal(VisualEffect source)
		{
			bool flag = source.outputEventReceived == null;
			VFXEventAttribute vfxeventAttribute;
			if (flag)
			{
				vfxeventAttribute = null;
			}
			else
			{
				bool flag2 = source.m_cachedEventAttribute == null;
				if (flag2)
				{
					source.m_cachedEventAttribute = source.CreateVFXEventAttribute();
				}
				vfxeventAttribute = source.m_cachedEventAttribute;
			}
			return vfxeventAttribute;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002520 File Offset: 0x00000720
		[RequiredByNativeCode]
		private static void InvokeOutputEventReceived_Internal(VisualEffect source, int eventNameId)
		{
			VFXOutputEventArgs evt = new VFXOutputEventArgs(eventNameId, source.m_cachedEventAttribute);
			source.outputEventReceived(evt);
		}

		// Token: 0x06000029 RID: 41
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_visualEffectAsset_Injected(IntPtr _unity_self);

		// Token: 0x04000020 RID: 32
		private VFXEventAttribute m_cachedEventAttribute;

		// Token: 0x04000021 RID: 33
		public Action<VFXOutputEventArgs> outputEventReceived;
	}
}
