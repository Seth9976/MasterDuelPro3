using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.XR
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/VR/ScriptBindings/XR.bindings.h")]
	[NativeHeader("Runtime/Interfaces/IVRDevice.h")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/VR/VRModule.h")]
	public static class XRSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		public static extern bool enabled
		{
			[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		[NativeName("Active")]
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool isDeviceActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern int eyeTextureWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern int eyeTextureHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		[NativeConditional("ENABLE_VR", "RenderTextureDesc()")]
		[NativeName("IntermediateEyeTextureDesc")]
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				RenderTextureDescriptor renderTextureDescriptor;
				XRSettings.get_eyeTextureDesc_Injected(out renderTextureDescriptor);
				return renderTextureDescriptor;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002068 File Offset: 0x00000268
		[NativeName("DeviceName")]
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static string loadedDeviceName
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					XRSettings.get_loadedDeviceName_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000007 RID: 7
		public static extern string[] supportedDevices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000008 RID: 8
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_eyeTextureDesc_Injected(out RenderTextureDescriptor ret);

		// Token: 0x06000009 RID: 9
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_loadedDeviceName_Injected(out ManagedSpanWrapper ret);
	}
}
