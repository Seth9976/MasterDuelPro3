using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D7 RID: 983
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	public readonly struct GlobalKeyword
	{
		// Token: 0x06001AEB RID: 6891
		[FreeFunction("ShaderScripting::GetGlobalKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetGlobalKeywordCount();

		// Token: 0x06001AEC RID: 6892 RVA: 0x0003AFA4 File Offset: 0x000391A4
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		private unsafe static uint GetGlobalKeywordIndex(string keyword)
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
				globalKeywordIndex_Injected = GlobalKeyword.GetGlobalKeywordIndex_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return globalKeywordIndex_Injected;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0003AFFC File Offset: 0x000391FC
		[FreeFunction("ShaderScripting::CreateGlobalKeyword")]
		private unsafe static void CreateGlobalKeyword(string keyword)
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
				GlobalKeyword.CreateGlobalKeyword_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0003B050 File Offset: 0x00039250
		public static GlobalKeyword Create(string name)
		{
			GlobalKeyword.CreateGlobalKeyword(name);
			return new GlobalKeyword(name);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0003B070 File Offset: 0x00039270
		public GlobalKeyword(string name)
		{
			this.m_Name = name;
			this.m_Index = GlobalKeyword.GetGlobalKeywordIndex(name);
			bool flag = this.m_Index >= GlobalKeyword.GetGlobalKeywordCount();
			if (flag)
			{
				Debug.LogErrorFormat("Global keyword {0} doesn't exist.", new object[] { name });
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0003B0BC File Offset: 0x000392BC
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x06001AF1 RID: 6897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetGlobalKeywordIndex_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x06001AF2 RID: 6898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateGlobalKeyword_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x04000CE9 RID: 3305
		internal readonly string m_Name;

		// Token: 0x04000CEA RID: 3306
		internal readonly uint m_Index;
	}
}
