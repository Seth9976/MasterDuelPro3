using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[UsedByNativeCode("SubsystemDescriptorBase")]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class IntegratedSubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020D4 File Offset: 0x000002D4
		public string id
		{
			get
			{
				return SubsystemDescriptorBindings.GetId(this.m_Ptr);
			}
		}

		// Token: 0x04000003 RID: 3
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal IntPtr m_Ptr;
	}
}
