using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D8 RID: 984
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public readonly struct LocalKeyword : IEquatable<LocalKeyword>
	{
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0003B0D4 File Offset: 0x000392D4
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		private static uint GetShaderKeywordCount(Shader shader)
		{
			return LocalKeyword.GetShaderKeywordCount_Injected(Object.MarshalledUnityObject.Marshal<Shader>(shader));
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0003B0EC File Offset: 0x000392EC
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		private unsafe static uint GetShaderKeywordIndex(Shader shader, string keyword)
		{
			uint shaderKeywordIndex_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Shader>(shader);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				shaderKeywordIndex_Injected = LocalKeyword.GetShaderKeywordIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return shaderKeywordIndex_Injected;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0003B148 File Offset: 0x00039348
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		private static uint GetComputeShaderKeywordCount(ComputeShader shader)
		{
			return LocalKeyword.GetComputeShaderKeywordCount_Injected(Object.MarshalledUnityObject.Marshal<ComputeShader>(shader));
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0003B160 File Offset: 0x00039360
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		private unsafe static uint GetComputeShaderKeywordIndex(ComputeShader shader, string keyword)
		{
			uint computeShaderKeywordIndex_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.Marshal<ComputeShader>(shader);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				computeShaderKeywordIndex_Injected = LocalKeyword.GetComputeShaderKeywordIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return computeShaderKeywordIndex_Injected;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0003B1BC File Offset: 0x000393BC
		public LocalKeyword(Shader shader, string name)
		{
			bool flag = shader == null;
			if (flag)
			{
				Debug.LogError("Cannot initialize a LocalKeyword with a null Shader.");
			}
			this.m_SpaceInfo = shader.keywordSpace;
			this.m_Name = name;
			this.m_Index = LocalKeyword.GetShaderKeywordIndex(shader, name);
			bool flag2 = this.m_Index >= LocalKeyword.GetShaderKeywordCount(shader);
			if (flag2)
			{
				Debug.LogErrorFormat("Local keyword {0} doesn't exist in the shader.", new object[] { name });
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0003B22C File Offset: 0x0003942C
		public LocalKeyword(ComputeShader shader, string name)
		{
			bool flag = shader == null;
			if (flag)
			{
				Debug.LogError("Cannot initialize a LocalKeyword with a null ComputeShader.");
			}
			this.m_SpaceInfo = shader.keywordSpace;
			this.m_Name = name;
			this.m_Index = LocalKeyword.GetComputeShaderKeywordIndex(shader, name);
			bool flag2 = this.m_Index >= LocalKeyword.GetComputeShaderKeywordCount(shader);
			if (flag2)
			{
				Debug.LogErrorFormat("Local keyword {0} doesn't exist in the compute shader.", new object[] { name });
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0003B29C File Offset: 0x0003949C
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0003B2B4 File Offset: 0x000394B4
		public override bool Equals(object o)
		{
			bool flag;
			if (o is LocalKeyword)
			{
				LocalKeyword other = (LocalKeyword)o;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0003B2E0 File Offset: 0x000394E0
		public bool Equals(LocalKeyword rhs)
		{
			return this.m_SpaceInfo == rhs.m_SpaceInfo && this.m_Index == rhs.m_Index;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0003B318 File Offset: 0x00039518
		public override int GetHashCode()
		{
			return this.m_Index.GetHashCode() ^ this.m_SpaceInfo.GetHashCode();
		}

		// Token: 0x06001AFD RID: 6909
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetShaderKeywordCount_Injected(IntPtr shader);

		// Token: 0x06001AFE RID: 6910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetShaderKeywordIndex_Injected(IntPtr shader, ref ManagedSpanWrapper keyword);

		// Token: 0x06001AFF RID: 6911
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetComputeShaderKeywordCount_Injected(IntPtr shader);

		// Token: 0x06001B00 RID: 6912
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetComputeShaderKeywordIndex_Injected(IntPtr shader, ref ManagedSpanWrapper keyword);

		// Token: 0x04000CEB RID: 3307
		internal readonly LocalKeywordSpace m_SpaceInfo;

		// Token: 0x04000CEC RID: 3308
		internal readonly string m_Name;

		// Token: 0x04000CED RID: 3309
		internal readonly uint m_Index;
	}
}
