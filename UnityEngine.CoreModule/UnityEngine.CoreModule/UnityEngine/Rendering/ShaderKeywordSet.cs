using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003DC RID: 988
	[UsedByNativeCode]
	[NativeHeader("Editor/Src/Graphics/ShaderCompilerData.h")]
	public struct ShaderKeywordSet
	{
		// Token: 0x06001B0D RID: 6925 RVA: 0x0003B510 File Offset: 0x00039710
		[FreeFunction("keywords::IsKeywordEnabled")]
		private unsafe static bool IsKeywordNameEnabled(ShaderKeywordSet state, string name)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = ShaderKeywordSet.IsKeywordNameEnabled_Injected(ref state, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0003B568 File Offset: 0x00039768
		private void CheckKeywordCompatible(ShaderKeyword keyword)
		{
			bool isLocal = keyword.m_IsLocal;
			if (isLocal)
			{
				bool flag = this.m_Shader != IntPtr.Zero;
				if (flag)
				{
					Assert.IsTrue(!keyword.m_IsCompute, "Trying to use a keyword that comes from a different shader.");
				}
				else
				{
					Assert.IsTrue(keyword.m_IsCompute, "Trying to use a keyword that comes from a different shader.");
				}
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0003B5C0 File Offset: 0x000397C0
		public bool IsEnabled(ShaderKeyword keyword)
		{
			this.CheckKeywordCompatible(keyword);
			return ShaderKeywordSet.IsKeywordNameEnabled(this, keyword.m_Name);
		}

		// Token: 0x06001B10 RID: 6928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordNameEnabled_Injected([In] ref ShaderKeywordSet state, ref ManagedSpanWrapper name);

		// Token: 0x04000CF4 RID: 3316
		private IntPtr m_KeywordState;

		// Token: 0x04000CF5 RID: 3317
		private IntPtr m_Shader;

		// Token: 0x04000CF6 RID: 3318
		private IntPtr m_ComputeShader;

		// Token: 0x04000CF7 RID: 3319
		private ulong m_StateIndex;
	}
}
