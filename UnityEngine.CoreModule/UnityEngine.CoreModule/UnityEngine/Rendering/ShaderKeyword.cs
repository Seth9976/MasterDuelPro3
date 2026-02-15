using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003DB RID: 987
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public struct ShaderKeyword
	{
		// Token: 0x06001B05 RID: 6917
		[FreeFunction("ShaderScripting::GetGlobalKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetGlobalKeywordCount();

		// Token: 0x06001B06 RID: 6918 RVA: 0x0003B3D4 File Offset: 0x000395D4
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		internal unsafe static uint GetGlobalKeywordIndex(string keyword)
		{
			uint globalKeywordIndex_Injected;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				globalKeywordIndex_Injected = ShaderKeyword.GetGlobalKeywordIndex_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return globalKeywordIndex_Injected;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0003B42C File Offset: 0x0003962C
		[FreeFunction("ShaderScripting::CreateGlobalKeyword")]
		internal unsafe static void CreateGlobalKeyword(string keyword)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ShaderKeyword.CreateGlobalKeyword_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x0003B480 File Offset: 0x00039680
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0003B498 File Offset: 0x00039698
		public ShaderKeyword(string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
			bool flag = this.m_Index >= ShaderKeyword.GetGlobalKeywordCount();
			if (flag)
			{
				ShaderKeyword.CreateGlobalKeyword(keywordName);
				this.m_Index = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
			}
			this.m_IsValid = true;
			this.m_IsLocal = false;
			this.m_IsCompute = false;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0003B4F8 File Offset: 0x000396F8
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x06001B0B RID: 6923
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetGlobalKeywordIndex_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x06001B0C RID: 6924
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateGlobalKeyword_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x04000CEF RID: 3311
		internal string m_Name;

		// Token: 0x04000CF0 RID: 3312
		internal uint m_Index;

		// Token: 0x04000CF1 RID: 3313
		internal bool m_IsLocal;

		// Token: 0x04000CF2 RID: 3314
		internal bool m_IsCompute;

		// Token: 0x04000CF3 RID: 3315
		internal bool m_IsValid;
	}
}
