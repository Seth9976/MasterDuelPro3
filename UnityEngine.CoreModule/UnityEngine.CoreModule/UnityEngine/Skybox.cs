using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000FB RID: 251
	[NativeHeader("Runtime/Camera/Skybox.h")]
	public sealed class Skybox : Behaviour
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00012C30 File Offset: 0x00010E30
		public Material material
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Skybox>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(Skybox.get_material_Injected(intPtr));
			}
		}

		// Token: 0x06000A08 RID: 2568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_material_Injected(IntPtr _unity_self);
	}
}
