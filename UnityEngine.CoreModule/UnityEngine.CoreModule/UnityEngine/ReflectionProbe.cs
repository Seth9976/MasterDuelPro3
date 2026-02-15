using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000B6 RID: 182
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	public sealed class ReflectionProbe : Behaviour
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00009014 File Offset: 0x00007214
		public ReflectionProbeRefreshMode refreshMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_refreshMode_Injected(intPtr);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00009038 File Offset: 0x00007238
		[StaticAccessor("GetReflectionProbes()")]
		public static Vector4 defaultTextureHDRDecodeValues
		{
			get
			{
				Vector4 vector;
				ReflectionProbe.get_defaultTextureHDRDecodeValues_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00009050 File Offset: 0x00007250
		[StaticAccessor("GetReflectionProbes()")]
		public static Texture defaultTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture>(ReflectionProbe.get_defaultTexture_Injected());
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00009068 File Offset: 0x00007268
		[RequiredByNativeCode]
		private static void CallReflectionProbeEvent(ReflectionProbe probe, ReflectionProbe.ReflectionProbeEvent probeEvent)
		{
			Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> callback = ReflectionProbe.reflectionProbeChanged;
			bool flag = callback != null;
			if (flag)
			{
				callback(probe, probeEvent);
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00009090 File Offset: 0x00007290
		[RequiredByNativeCode]
		private static void CallSetDefaultReflection(Texture defaultReflectionCubemap)
		{
			foreach (Action<Texture> callback in ReflectionProbe.registeredDefaultReflectionTextureActions)
			{
				callback(defaultReflectionCubemap);
			}
		}

		// Token: 0x06000474 RID: 1140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReflectionProbeRefreshMode get_refreshMode_Injected(IntPtr _unity_self);

		// Token: 0x06000475 RID: 1141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultTextureHDRDecodeValues_Injected(out Vector4 ret);

		// Token: 0x06000476 RID: 1142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_defaultTexture_Injected();

		// Token: 0x04000239 RID: 569
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> reflectionProbeChanged;

		// Token: 0x0400023A RID: 570
		private static Dictionary<int, Action<Texture>> registeredDefaultReflectionSetActions = new Dictionary<int, Action<Texture>>();

		// Token: 0x0400023B RID: 571
		private static List<Action<Texture>> registeredDefaultReflectionTextureActions = new List<Action<Texture>>();

		// Token: 0x020000B7 RID: 183
		public enum ReflectionProbeEvent
		{
			// Token: 0x0400023D RID: 573
			ReflectionProbeAdded,
			// Token: 0x0400023E RID: 574
			ReflectionProbeRemoved
		}
	}
}
