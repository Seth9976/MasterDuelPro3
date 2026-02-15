using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	[NativeHeader("Modules/Animation/AnimatorControllerParameter.h")]
	[NativeAsStruct]
	[UsedByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoAnimatorControllerParameter")]
	[StructLayout(LayoutKind.Sequential)]
	public class AnimatorControllerParameter
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00005300 File Offset: 0x00003500
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00005318 File Offset: 0x00003518
		public override bool Equals(object o)
		{
			AnimatorControllerParameter other = o as AnimatorControllerParameter;
			return other != null && this.m_Name == other.m_Name && this.m_Type == other.m_Type && this.m_DefaultFloat == other.m_DefaultFloat && this.m_DefaultInt == other.m_DefaultInt && this.m_DefaultBool == other.m_DefaultBool;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00005384 File Offset: 0x00003584
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0400005F RID: 95
		internal string m_Name = "";

		// Token: 0x04000060 RID: 96
		internal AnimatorControllerParameterType m_Type;

		// Token: 0x04000061 RID: 97
		internal float m_DefaultFloat;

		// Token: 0x04000062 RID: 98
		internal int m_DefaultInt;

		// Token: 0x04000063 RID: 99
		internal bool m_DefaultBool;
	}
}
