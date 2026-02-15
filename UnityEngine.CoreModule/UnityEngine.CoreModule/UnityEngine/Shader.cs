using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x020000EF RID: 239
	[NativeHeader("Runtime/Shaders/Shader.h")]
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManager.h")]
	[NativeHeader("Runtime/Shaders/ShaderNameRegistry.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Shaders/GpuPrograms/ShaderVariantCollection.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public sealed class Shader : Object
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0000DD08 File Offset: 0x0000BF08
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0000DD1F File Offset: 0x0000BF1F
		[Obsolete("Use Graphics.activeTier instead (UnityUpgradable) -> UnityEngine.Graphics.activeTier", false)]
		public static ShaderHardwareTier globalShaderHardwareTier
		{
			get
			{
				return (ShaderHardwareTier)Graphics.activeTier;
			}
			set
			{
				Graphics.activeTier = (GraphicsTier)value;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000DD29 File Offset: 0x0000BF29
		public static Shader Find(string name)
		{
			return ResourcesAPI.ActiveAPI.FindShaderByName(name);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000DD38 File Offset: 0x0000BF38
		[FreeFunction("GetBuiltinResource<Shader>")]
		internal unsafe static Shader FindBuiltin(string name)
		{
			Shader shader;
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
				IntPtr intPtr = Shader.FindBuiltin_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				shader = Unmarshal.UnmarshalUnityObject<Shader>(intPtr);
				char* ptr = null;
			}
			return shader;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000708 RID: 1800
		// (set) Token: 0x06000709 RID: 1801
		[NativeProperty("MaxChunksRuntimeOverride")]
		public static extern int maximumChunksOverride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000DD98 File Offset: 0x0000BF98
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		[NativeProperty("MaximumShaderLOD")]
		public int maximumLOD
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_maximumLOD_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Shader.set_maximumLOD_Injected(intPtr, value);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600070C RID: 1804
		// (set) Token: 0x0600070D RID: 1805
		[NativeProperty("GlobalMaximumShaderLOD")]
		public static extern int globalMaximumLOD
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		public bool isSupported
		{
			[NativeMethod("IsSupported")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_isSupported_Injected(intPtr);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0000DE04 File Offset: 0x0000C004
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0000DE34 File Offset: 0x0000C034
		public unsafe static string globalRenderPipeline
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Shader.get_globalRenderPipeline_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Shader.set_globalRenderPipeline_Injected(ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0000DE88 File Offset: 0x0000C088
		public static GlobalKeyword[] enabledGlobalKeywords
		{
			get
			{
				return Shader.GetEnabledGlobalKeywords();
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
		public static GlobalKeyword[] globalKeywords
		{
			get
			{
				return Shader.GetAllGlobalKeywords();
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public LocalKeywordSpace keywordSpace
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LocalKeywordSpace localKeywordSpace;
				Shader.get_keywordSpace_Injected(intPtr, out localKeywordSpace);
				return localKeywordSpace;
			}
		}

		// Token: 0x06000714 RID: 1812
		[FreeFunction("keywords::GetEnabledGlobalKeywords")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GlobalKeyword[] GetEnabledGlobalKeywords();

		// Token: 0x06000715 RID: 1813
		[FreeFunction("keywords::GetAllGlobalKeywords")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GlobalKeyword[] GetAllGlobalKeywords();

		// Token: 0x06000716 RID: 1814 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		[FreeFunction("ShaderScripting::EnableKeyword")]
		public unsafe static void EnableKeyword(string keyword)
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
				Shader.EnableKeyword_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000DF34 File Offset: 0x0000C134
		[FreeFunction("ShaderScripting::DisableKeyword")]
		public unsafe static void DisableKeyword(string keyword)
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
				Shader.DisableKeyword_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000DF88 File Offset: 0x0000C188
		[FreeFunction("ShaderScripting::IsKeywordEnabled")]
		public unsafe static bool IsKeywordEnabled(string keyword)
		{
			bool flag;
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
				flag = Shader.IsKeywordEnabled_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		[FreeFunction("ShaderScripting::EnableKeyword")]
		internal static void EnableKeywordFast(GlobalKeyword keyword)
		{
			Shader.EnableKeywordFast_Injected(ref keyword);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		[FreeFunction("ShaderScripting::DisableKeyword")]
		internal static void DisableKeywordFast(GlobalKeyword keyword)
		{
			Shader.DisableKeywordFast_Injected(ref keyword);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000E008 File Offset: 0x0000C208
		[FreeFunction("ShaderScripting::SetKeyword")]
		internal static void SetKeywordFast(GlobalKeyword keyword, bool value)
		{
			Shader.SetKeywordFast_Injected(ref keyword, value);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000E020 File Offset: 0x0000C220
		[FreeFunction("ShaderScripting::IsKeywordEnabled")]
		internal static bool IsKeywordEnabledFast(GlobalKeyword keyword)
		{
			return Shader.IsKeywordEnabledFast_Injected(ref keyword);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000E034 File Offset: 0x0000C234
		public static void EnableKeyword(in GlobalKeyword keyword)
		{
			Shader.EnableKeywordFast(keyword);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000E043 File Offset: 0x0000C243
		public static void DisableKeyword(in GlobalKeyword keyword)
		{
			Shader.DisableKeywordFast(keyword);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000E052 File Offset: 0x0000C252
		public static void SetKeyword(in GlobalKeyword keyword, bool value)
		{
			Shader.SetKeywordFast(keyword, value);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000E064 File Offset: 0x0000C264
		public static bool IsKeywordEnabled(in GlobalKeyword keyword)
		{
			return Shader.IsKeywordEnabledFast(keyword);
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0000E084 File Offset: 0x0000C284
		public int renderQueue
		{
			[FreeFunction("ShaderScripting::GetRenderQueue", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_renderQueue_Injected(intPtr);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		internal DisableBatchingType disableBatching
		{
			[FreeFunction("ShaderScripting::GetDisableBatchingType", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_disableBatching_Injected(intPtr);
			}
		}

		// Token: 0x06000723 RID: 1827
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WarmupAllShaders();

		// Token: 0x06000724 RID: 1828 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		[FreeFunction("ShaderScripting::TagToID")]
		internal unsafe static int TagToID(string name)
		{
			int num;
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
				num = Shader.TagToID_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000E124 File Offset: 0x0000C324
		[FreeFunction("ShaderScripting::IDToTag")]
		internal static string IDToTag(int name)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Shader.IDToTag_Injected(name, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000E154 File Offset: 0x0000C354
		[FreeFunction(Name = "ShaderScripting::PropertyToID", IsThreadSafe = true)]
		public unsafe static int PropertyToID(string name)
		{
			int num;
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
				num = Shader.PropertyToID_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		public unsafe Shader GetDependency(string name)
		{
			Shader shader;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr dependency_Injected = Shader.GetDependency_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr dependency_Injected;
				shader = Unmarshal.UnmarshalUnityObject<Shader>(dependency_Injected);
				char* ptr = null;
			}
			return shader;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0000E21C File Offset: 0x0000C41C
		public int passCount
		{
			[FreeFunction(Name = "ShaderScripting::GetPassCount", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_passCount_Injected(intPtr);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0000E240 File Offset: 0x0000C440
		public int subshaderCount
		{
			[FreeFunction(Name = "ShaderScripting::GetSubshaderCount", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_subshaderCount_Injected(intPtr);
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000E264 File Offset: 0x0000C464
		[FreeFunction(Name = "ShaderScripting::GetPassCountInSubshader", HasExplicitThis = true)]
		public int GetPassCountInSubshader(int subshaderIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.GetPassCountInSubshader_Injected(intPtr, subshaderIndex);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000E288 File Offset: 0x0000C488
		public ShaderTagId FindPassTagValue(int passIndex, ShaderTagId tagName)
		{
			bool flag = passIndex < 0 || passIndex >= this.passCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("passIndex");
			}
			int id = this.Internal_FindPassTagValue(passIndex, tagName.id);
			return new ShaderTagId
			{
				id = id
			};
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		public ShaderTagId FindPassTagValue(int subshaderIndex, int passIndex, ShaderTagId tagName)
		{
			bool flag = subshaderIndex < 0 || subshaderIndex >= this.subshaderCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("subshaderIndex");
			}
			bool flag2 = passIndex < 0 || passIndex >= this.GetPassCountInSubshader(subshaderIndex);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("passIndex");
			}
			int id = this.Internal_FindPassTagValueInSubShader(subshaderIndex, passIndex, tagName.id);
			return new ShaderTagId
			{
				id = id
			};
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000E35C File Offset: 0x0000C55C
		public ShaderTagId FindSubshaderTagValue(int subshaderIndex, ShaderTagId tagName)
		{
			bool flag = subshaderIndex < 0 || subshaderIndex >= this.subshaderCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Invalid subshaderIndex {0}. Value must be in the range [0, {1})", subshaderIndex, this.subshaderCount));
			}
			int id = this.Internal_FindSubshaderTagValue(subshaderIndex, tagName.id);
			return new ShaderTagId
			{
				id = id
			};
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		[FreeFunction(Name = "ShaderScripting::FindPassTagValue", HasExplicitThis = true)]
		private int Internal_FindPassTagValue(int passIndex, int tagName)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.Internal_FindPassTagValue_Injected(intPtr, passIndex, tagName);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000E3EC File Offset: 0x0000C5EC
		[FreeFunction(Name = "ShaderScripting::FindPassTagValue", HasExplicitThis = true)]
		private int Internal_FindPassTagValueInSubShader(int subShaderIndex, int passIndex, int tagName)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.Internal_FindPassTagValueInSubShader_Injected(intPtr, subShaderIndex, passIndex, tagName);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000E414 File Offset: 0x0000C614
		[FreeFunction(Name = "ShaderScripting::FindSubshaderTagValue", HasExplicitThis = true)]
		private int Internal_FindSubshaderTagValue(int subShaderIndex, int tagName)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.Internal_FindSubshaderTagValue_Injected(intPtr, subShaderIndex, tagName);
		}

		// Token: 0x06000731 RID: 1841
		[FreeFunction("ShaderScripting::SetGlobalInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalIntImpl(int name, int value);

		// Token: 0x06000732 RID: 1842
		[FreeFunction("ShaderScripting::SetGlobalFloat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatImpl(int name, float value);

		// Token: 0x06000733 RID: 1843 RVA: 0x0000E438 File Offset: 0x0000C638
		[FreeFunction("ShaderScripting::SetGlobalVector")]
		private static void SetGlobalVectorImpl(int name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl_Injected(name, ref value);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000E450 File Offset: 0x0000C650
		[FreeFunction("ShaderScripting::SetGlobalMatrix")]
		private static void SetGlobalMatrixImpl(int name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000E468 File Offset: 0x0000C668
		[FreeFunction("ShaderScripting::SetGlobalTexture")]
		private static void SetGlobalTextureImpl(int name, Texture value)
		{
			Shader.SetGlobalTextureImpl_Injected(name, Object.MarshalledUnityObject.Marshal<Texture>(value));
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000E484 File Offset: 0x0000C684
		[FreeFunction("ShaderScripting::SetGlobalRenderTexture")]
		private static void SetGlobalRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl_Injected(name, Object.MarshalledUnityObject.Marshal<RenderTexture>(value), element);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		private static void SetGlobalBufferImpl(int name, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl_Injected(name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		private static void SetGlobalGraphicsBufferImpl(int name, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl_Injected(name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		private static void SetGlobalConstantBufferImpl(int name, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl_Injected(name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000E50C File Offset: 0x0000C70C
		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		private static void SetGlobalConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl_Injected(name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000E530 File Offset: 0x0000C730
		[FreeFunction("ShaderScripting::SetGlobalRayTracingAccelerationStructure")]
		private static void SetGlobalRayTracingAccelerationStructureImpl(int name, RayTracingAccelerationStructure accelerationStructure)
		{
			Shader.SetGlobalRayTracingAccelerationStructureImpl_Injected(name, (accelerationStructure == null) ? ((IntPtr)0) : RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure));
		}

		// Token: 0x0600073C RID: 1852
		[FreeFunction("ShaderScripting::GetGlobalInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalIntImpl(int name);

		// Token: 0x0600073D RID: 1853
		[FreeFunction("ShaderScripting::GetGlobalFloat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGlobalFloatImpl(int name);

		// Token: 0x0600073E RID: 1854 RVA: 0x0000E554 File Offset: 0x0000C754
		[FreeFunction("ShaderScripting::GetGlobalVector")]
		private static Vector4 GetGlobalVectorImpl(int name)
		{
			Vector4 vector;
			Shader.GetGlobalVectorImpl_Injected(name, out vector);
			return vector;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000E56C File Offset: 0x0000C76C
		[FreeFunction("ShaderScripting::GetGlobalMatrix")]
		private static Matrix4x4 GetGlobalMatrixImpl(int name)
		{
			Matrix4x4 matrix4x;
			Shader.GetGlobalMatrixImpl_Injected(name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000E584 File Offset: 0x0000C784
		[FreeFunction("ShaderScripting::GetGlobalTexture")]
		private static Texture GetGlobalTextureImpl(int name)
		{
			return Unmarshal.UnmarshalUnityObject<Texture>(Shader.GetGlobalTextureImpl_Injected(name));
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000E59C File Offset: 0x0000C79C
		[FreeFunction("ShaderScripting::SetGlobalFloatArray")]
		private unsafe static void SetGlobalFloatArrayImpl(int name, float[] values, int count)
		{
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Shader.SetGlobalFloatArrayImpl_Injected(name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		[FreeFunction("ShaderScripting::SetGlobalVectorArray")]
		private unsafe static void SetGlobalVectorArrayImpl(int name, Vector4[] values, int count)
		{
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Shader.SetGlobalVectorArrayImpl_Injected(name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000E614 File Offset: 0x0000C814
		[FreeFunction("ShaderScripting::SetGlobalMatrixArray")]
		private unsafe static void SetGlobalMatrixArrayImpl(int name, Matrix4x4[] values, int count)
		{
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Shader.SetGlobalMatrixArrayImpl_Injected(name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000E650 File Offset: 0x0000C850
		[FreeFunction("ShaderScripting::GetGlobalFloatArray")]
		private static float[] GetGlobalFloatArrayImpl(int name)
		{
			float[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Shader.GetGlobalFloatArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				float[] array;
				blittableArrayWrapper.Unmarshal<float>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000E684 File Offset: 0x0000C884
		[FreeFunction("ShaderScripting::GetGlobalVectorArray")]
		private static Vector4[] GetGlobalVectorArrayImpl(int name)
		{
			Vector4[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Shader.GetGlobalVectorArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Vector4[] array;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000E6B8 File Offset: 0x0000C8B8
		[FreeFunction("ShaderScripting::GetGlobalMatrixArray")]
		private static Matrix4x4[] GetGlobalMatrixArrayImpl(int name)
		{
			Matrix4x4[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Shader.GetGlobalMatrixArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Matrix4x4[] array;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000747 RID: 1863
		[FreeFunction("ShaderScripting::GetGlobalFloatArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalFloatArrayCountImpl(int name);

		// Token: 0x06000748 RID: 1864
		[FreeFunction("ShaderScripting::GetGlobalVectorArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalVectorArrayCountImpl(int name);

		// Token: 0x06000749 RID: 1865
		[FreeFunction("ShaderScripting::GetGlobalMatrixArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalMatrixArrayCountImpl(int name);

		// Token: 0x0600074A RID: 1866 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		[FreeFunction("ShaderScripting::ExtractGlobalFloatArray")]
		private unsafe static void ExtractGlobalFloatArrayImpl(int name, [Out] float[] val)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (float[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Shader.ExtractGlobalFloatArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000E73C File Offset: 0x0000C93C
		[FreeFunction("ShaderScripting::ExtractGlobalVectorArray")]
		private unsafe static void ExtractGlobalVectorArrayImpl(int name, [Out] Vector4[] val)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (Vector4[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Shader.ExtractGlobalVectorArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				Vector4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000E78C File Offset: 0x0000C98C
		[FreeFunction("ShaderScripting::ExtractGlobalMatrixArray")]
		private unsafe static void ExtractGlobalMatrixArrayImpl(int name, [Out] Matrix4x4[] val)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (Matrix4x4[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Shader.ExtractGlobalMatrixArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				Matrix4x4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		private static void SetGlobalFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			Shader.SetGlobalFloatArrayImpl(name, values, count);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000E830 File Offset: 0x0000CA30
		private static void SetGlobalVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			Shader.SetGlobalVectorArrayImpl(name, values, count);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000E884 File Offset: 0x0000CA84
		private static void SetGlobalMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			Shader.SetGlobalMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		private static void ExtractGlobalFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int count = Shader.GetGlobalFloatArrayCountImpl(name);
			bool flag2 = count > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, count);
				Shader.ExtractGlobalFloatArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<float>(values));
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000E928 File Offset: 0x0000CB28
		private static void ExtractGlobalVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int count = Shader.GetGlobalVectorArrayCountImpl(name);
			bool flag2 = count > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, count);
				Shader.ExtractGlobalVectorArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Vector4>(values));
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000E978 File Offset: 0x0000CB78
		private static void ExtractGlobalMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int count = Shader.GetGlobalMatrixArrayCountImpl(name);
			bool flag2 = count > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, count);
				Shader.ExtractGlobalMatrixArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values));
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000E9C6 File Offset: 0x0000CBC6
		public static void SetGlobalInt(string name, int value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000E9D7 File Offset: 0x0000CBD7
		public static void SetGlobalInt(int nameID, int value)
		{
			Shader.SetGlobalFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000E9E3 File Offset: 0x0000CBE3
		public static void SetGlobalFloat(string name, float value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000E9F3 File Offset: 0x0000CBF3
		public static void SetGlobalFloat(int nameID, float value)
		{
			Shader.SetGlobalFloatImpl(nameID, value);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000E9FE File Offset: 0x0000CBFE
		public static void SetGlobalInteger(string name, int value)
		{
			Shader.SetGlobalIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000EA0E File Offset: 0x0000CC0E
		public static void SetGlobalInteger(int nameID, int value)
		{
			Shader.SetGlobalIntImpl(nameID, value);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000EA19 File Offset: 0x0000CC19
		public static void SetGlobalVector(string name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000EA29 File Offset: 0x0000CC29
		public static void SetGlobalVector(int nameID, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000EA34 File Offset: 0x0000CC34
		public static void SetGlobalColor(string name, Color value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0000EA49 File Offset: 0x0000CC49
		public static void SetGlobalColor(int nameID, Color value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000EA59 File Offset: 0x0000CC59
		public static void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000EA69 File Offset: 0x0000CC69
		public static void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(nameID, value);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000EA74 File Offset: 0x0000CC74
		public static void SetGlobalTexture(string name, Texture value)
		{
			Shader.SetGlobalTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public static void SetGlobalTexture(int nameID, Texture value)
		{
			Shader.SetGlobalTextureImpl(nameID, value);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000EA8F File Offset: 0x0000CC8F
		public static void SetGlobalTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		public static void SetGlobalTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		public static void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000EABC File Offset: 0x0000CCBC
		public static void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(nameID, value);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000EAC7 File Offset: 0x0000CCC7
		public static void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000EAD7 File Offset: 0x0000CCD7
		public static void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000EAE2 File Offset: 0x0000CCE2
		public static void SetGlobalConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		public static void SetGlobalConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000EB01 File Offset: 0x0000CD01
		public static void SetGlobalConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000EB13 File Offset: 0x0000CD13
		public static void SetGlobalConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000EB20 File Offset: 0x0000CD20
		public static void SetGlobalRayTracingAccelerationStructure(string name, RayTracingAccelerationStructure value)
		{
			Shader.SetGlobalRayTracingAccelerationStructureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000EB30 File Offset: 0x0000CD30
		public static void SetGlobalRayTracingAccelerationStructure(int nameID, RayTracingAccelerationStructure value)
		{
			Shader.SetGlobalRayTracingAccelerationStructureImpl(nameID, value);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000EB3B File Offset: 0x0000CD3B
		public static void SetGlobalFloatArray(string name, List<float> values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000EB56 File Offset: 0x0000CD56
		public static void SetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.SetGlobalFloatArray(nameID, NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		public static void SetGlobalFloatArray(string name, float[] values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000EB7F File Offset: 0x0000CD7F
		public static void SetGlobalFloatArray(int nameID, float[] values)
		{
			Shader.SetGlobalFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000EB8D File Offset: 0x0000CD8D
		public static void SetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000EBA8 File Offset: 0x0000CDA8
		public static void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(nameID, NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000EBBE File Offset: 0x0000CDBE
		public static void SetGlobalVectorArray(string name, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000EBD1 File Offset: 0x0000CDD1
		public static void SetGlobalVectorArray(int nameID, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000EBDF File Offset: 0x0000CDDF
		public static void SetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000EBFA File Offset: 0x0000CDFA
		public static void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public static void SetGlobalMatrixArray(string name, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000EC23 File Offset: 0x0000CE23
		public static void SetGlobalMatrixArray(int nameID, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public static int GetGlobalInt(string name)
		{
			return (int)Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000EC54 File Offset: 0x0000CE54
		public static int GetGlobalInt(int nameID)
		{
			return (int)Shader.GetGlobalFloatImpl(nameID);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000EC70 File Offset: 0x0000CE70
		public static float GetGlobalFloat(string name)
		{
			return Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public static float GetGlobalFloat(int nameID)
		{
			return Shader.GetGlobalFloatImpl(nameID);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		public static int GetGlobalInteger(string name)
		{
			return Shader.GetGlobalIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public static int GetGlobalInteger(int nameID)
		{
			return Shader.GetGlobalIntImpl(nameID);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		public static Vector4 GetGlobalVector(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000ED00 File Offset: 0x0000CF00
		public static Vector4 GetGlobalVector(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000ED18 File Offset: 0x0000CF18
		public static Color GetGlobalColor(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		public static Color GetGlobalColor(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		public static Matrix4x4 GetGlobalMatrix(string name)
		{
			return Shader.GetGlobalMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public static Matrix4x4 GetGlobalMatrix(int nameID)
		{
			return Shader.GetGlobalMatrixImpl(nameID);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000ED94 File Offset: 0x0000CF94
		public static Texture GetGlobalTexture(string name)
		{
			return Shader.GetGlobalTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public static Texture GetGlobalTexture(int nameID)
		{
			return Shader.GetGlobalTextureImpl(nameID);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		public static float[] GetGlobalFloatArray(string name)
		{
			return Shader.GetGlobalFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		public static float[] GetGlobalFloatArray(int nameID)
		{
			return (Shader.GetGlobalFloatArrayCountImpl(nameID) != 0) ? Shader.GetGlobalFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000EE10 File Offset: 0x0000D010
		public static Vector4[] GetGlobalVectorArray(string name)
		{
			return Shader.GetGlobalVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000EE30 File Offset: 0x0000D030
		public static Vector4[] GetGlobalVectorArray(int nameID)
		{
			return (Shader.GetGlobalVectorArrayCountImpl(nameID) != 0) ? Shader.GetGlobalVectorArrayImpl(nameID) : null;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000EE54 File Offset: 0x0000D054
		public static Matrix4x4[] GetGlobalMatrixArray(string name)
		{
			return Shader.GetGlobalMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000EE74 File Offset: 0x0000D074
		public static Matrix4x4[] GetGlobalMatrixArray(int nameID)
		{
			return (Shader.GetGlobalMatrixArrayCountImpl(nameID) != 0) ? Shader.GetGlobalMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000EE97 File Offset: 0x0000D097
		public static void GetGlobalFloatArray(string name, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000EEA7 File Offset: 0x0000D0A7
		public static void GetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(nameID, values);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000EEB2 File Offset: 0x0000D0B2
		public static void GetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000EEC2 File Offset: 0x0000D0C2
		public static void GetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(nameID, values);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000EECD File Offset: 0x0000D0CD
		public static void GetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000EEDD File Offset: 0x0000D0DD
		public static void GetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(nameID, values);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00004DC7 File Offset: 0x00002FC7
		private Shader()
		{
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		[FreeFunction("ShaderScripting::GetPropertyName")]
		private static string GetPropertyName([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(shader, "shader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				Shader.GetPropertyName_Injected(intPtr, propertyIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000EF3C File Offset: 0x0000D13C
		[FreeFunction("ShaderScripting::GetPropertyNameId")]
		private static int GetPropertyNameId([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyNameId_Injected(intPtr, propertyIndex);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000EF74 File Offset: 0x0000D174
		[FreeFunction("ShaderScripting::GetPropertyType")]
		private static ShaderPropertyType GetPropertyType([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyType_Injected(intPtr, propertyIndex);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		[FreeFunction("ShaderScripting::GetPropertyDescription")]
		private static string GetPropertyDescription([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(shader, "shader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				Shader.GetPropertyDescription_Injected(intPtr, propertyIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000F000 File Offset: 0x0000D200
		[FreeFunction("ShaderScripting::GetPropertyFlags")]
		private static ShaderPropertyFlags GetPropertyFlags([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyFlags_Injected(intPtr, propertyIndex);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000F038 File Offset: 0x0000D238
		[FreeFunction("ShaderScripting::GetPropertyAttributes")]
		private static string[] GetPropertyAttributes([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyAttributes_Injected(intPtr, propertyIndex);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000F070 File Offset: 0x0000D270
		[FreeFunction("ShaderScripting::GetPropertyDefaultIntValue")]
		private static int GetPropertyDefaultIntValue([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyDefaultIntValue_Injected(intPtr, propertyIndex);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		[FreeFunction("ShaderScripting::GetPropertyDefaultValue")]
		private static Vector4 GetPropertyDefaultValue([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			Vector4 vector;
			Shader.GetPropertyDefaultValue_Injected(intPtr, propertyIndex, out vector);
			return vector;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		[FreeFunction("ShaderScripting::GetPropertyTextureDimension")]
		private static TextureDimension GetPropertyTextureDimension([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyTextureDimension_Injected(intPtr, propertyIndex);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000F11C File Offset: 0x0000D31C
		[FreeFunction("ShaderScripting::GetPropertyTextureDefaultName")]
		private static string GetPropertyTextureDefaultName([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(shader, "shader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				Shader.GetPropertyTextureDefaultName_Injected(intPtr, propertyIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000F170 File Offset: 0x0000D370
		[FreeFunction("ShaderScripting::FindTextureStack")]
		private static bool FindTextureStackImpl([NotNull] Shader s, int propertyIdx, out string stackName, out int layerIndex)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(s, "s");
			}
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(s);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(s, "s");
				}
				ManagedSpanWrapper managedSpanWrapper;
				flag = Shader.FindTextureStackImpl_Injected(intPtr, propertyIdx, out managedSpanWrapper, out layerIndex);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stackName = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		private static void CheckPropertyIndex(Shader s, int propertyIndex)
		{
			bool flag = propertyIndex < 0 || propertyIndex >= s.GetPropertyCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("propertyIndex");
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public int GetPropertyCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.GetPropertyCount_Injected(intPtr);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000F21C File Offset: 0x0000D41C
		public unsafe int FindPropertyIndex(string propertyName)
		{
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = propertyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Shader.FindPropertyIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000F284 File Offset: 0x0000D484
		public string GetPropertyName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyName(this, propertyIndex);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000F2A8 File Offset: 0x0000D4A8
		public int GetPropertyNameId(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyNameId(this, propertyIndex);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000F2CC File Offset: 0x0000D4CC
		public ShaderPropertyType GetPropertyType(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyType(this, propertyIndex);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		public string GetPropertyDescription(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyDescription(this, propertyIndex);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000F314 File Offset: 0x0000D514
		public ShaderPropertyFlags GetPropertyFlags(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyFlags(this, propertyIndex);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000F338 File Offset: 0x0000D538
		public string[] GetPropertyAttributes(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyAttributes(this, propertyIndex);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000F35C File Offset: 0x0000D55C
		public float GetPropertyDefaultFloatValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propType = this.GetPropertyType(propertyIndex);
			bool flag = propType != ShaderPropertyType.Float && propType != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Float or Range.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex)[0];
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000F3AC File Offset: 0x0000D5AC
		public Vector4 GetPropertyDefaultVectorValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propType = this.GetPropertyType(propertyIndex);
			bool flag = propType != ShaderPropertyType.Color && propType != ShaderPropertyType.Vector;
			if (flag)
			{
				throw new ArgumentException("Property type is not Color or Vector.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		public Vector2 GetPropertyRangeLimits(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Range.");
			}
			Vector4 defValues = Shader.GetPropertyDefaultValue(this, propertyIndex);
			return new Vector2(defValues[1], defValues[2]);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000F448 File Offset: 0x0000D648
		public int GetPropertyDefaultIntValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Int;
			if (flag)
			{
				throw new ArgumentException("Property type is not Int.");
			}
			return Shader.GetPropertyDefaultIntValue(this, propertyIndex);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000F488 File Offset: 0x0000D688
		public TextureDimension GetPropertyTextureDimension(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not TexEnv.");
			}
			return Shader.GetPropertyTextureDimension(this, propertyIndex);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		public string GetPropertyTextureDefaultName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propType = this.GetPropertyType(propertyIndex);
			bool flag = propType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.GetPropertyTextureDefaultName(this, propertyIndex);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000F508 File Offset: 0x0000D708
		public bool FindTextureStack(int propertyIndex, out string stackName, out int layerIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propType = this.GetPropertyType(propertyIndex);
			bool flag = propType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.FindTextureStackImpl(this, propertyIndex, out stackName, out layerIndex);
		}

		// Token: 0x060007AF RID: 1967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindBuiltin_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060007B0 RID: 1968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_maximumLOD_Injected(IntPtr _unity_self);

		// Token: 0x060007B1 RID: 1969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maximumLOD_Injected(IntPtr _unity_self, int value);

		// Token: 0x060007B2 RID: 1970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isSupported_Injected(IntPtr _unity_self);

		// Token: 0x060007B3 RID: 1971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_globalRenderPipeline_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060007B4 RID: 1972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_globalRenderPipeline_Injected(ref ManagedSpanWrapper value);

		// Token: 0x060007B5 RID: 1973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_keywordSpace_Injected(IntPtr _unity_self, out LocalKeywordSpace ret);

		// Token: 0x060007B6 RID: 1974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeyword_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x060007B7 RID: 1975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeyword_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x060007B8 RID: 1976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabled_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x060007B9 RID: 1977
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeywordFast_Injected([In] ref GlobalKeyword keyword);

		// Token: 0x060007BA RID: 1978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeywordFast_Injected([In] ref GlobalKeyword keyword);

		// Token: 0x060007BB RID: 1979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetKeywordFast_Injected([In] ref GlobalKeyword keyword, bool value);

		// Token: 0x060007BC RID: 1980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabledFast_Injected([In] ref GlobalKeyword keyword);

		// Token: 0x060007BD RID: 1981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_renderQueue_Injected(IntPtr _unity_self);

		// Token: 0x060007BE RID: 1982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DisableBatchingType get_disableBatching_Injected(IntPtr _unity_self);

		// Token: 0x060007BF RID: 1983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int TagToID_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060007C0 RID: 1984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IDToTag_Injected(int name, out ManagedSpanWrapper ret);

		// Token: 0x060007C1 RID: 1985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PropertyToID_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060007C2 RID: 1986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDependency_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060007C3 RID: 1987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_passCount_Injected(IntPtr _unity_self);

		// Token: 0x060007C4 RID: 1988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_subshaderCount_Injected(IntPtr _unity_self);

		// Token: 0x060007C5 RID: 1989
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPassCountInSubshader_Injected(IntPtr _unity_self, int subshaderIndex);

		// Token: 0x060007C6 RID: 1990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_FindPassTagValue_Injected(IntPtr _unity_self, int passIndex, int tagName);

		// Token: 0x060007C7 RID: 1991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_FindPassTagValueInSubShader_Injected(IntPtr _unity_self, int subShaderIndex, int passIndex, int tagName);

		// Token: 0x060007C8 RID: 1992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_FindSubshaderTagValue_Injected(IntPtr _unity_self, int subShaderIndex, int tagName);

		// Token: 0x060007C9 RID: 1993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorImpl_Injected(int name, [In] ref Vector4 value);

		// Token: 0x060007CA RID: 1994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixImpl_Injected(int name, [In] ref Matrix4x4 value);

		// Token: 0x060007CB RID: 1995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTextureImpl_Injected(int name, IntPtr value);

		// Token: 0x060007CC RID: 1996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalRenderTextureImpl_Injected(int name, IntPtr value, RenderTextureSubElement element);

		// Token: 0x060007CD RID: 1997
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalBufferImpl_Injected(int name, IntPtr value);

		// Token: 0x060007CE RID: 1998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalGraphicsBufferImpl_Injected(int name, IntPtr value);

		// Token: 0x060007CF RID: 1999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantBufferImpl_Injected(int name, IntPtr value, int offset, int size);

		// Token: 0x060007D0 RID: 2000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantGraphicsBufferImpl_Injected(int name, IntPtr value, int offset, int size);

		// Token: 0x060007D1 RID: 2001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalRayTracingAccelerationStructureImpl_Injected(int name, IntPtr accelerationStructure);

		// Token: 0x060007D2 RID: 2002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalVectorImpl_Injected(int name, out Vector4 ret);

		// Token: 0x060007D3 RID: 2003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x060007D4 RID: 2004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetGlobalTextureImpl_Injected(int name);

		// Token: 0x060007D5 RID: 2005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatArrayImpl_Injected(int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x060007D6 RID: 2006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorArrayImpl_Injected(int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x060007D7 RID: 2007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixArrayImpl_Injected(int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x060007D8 RID: 2008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalFloatArrayImpl_Injected(int name, out BlittableArrayWrapper ret);

		// Token: 0x060007D9 RID: 2009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalVectorArrayImpl_Injected(int name, out BlittableArrayWrapper ret);

		// Token: 0x060007DA RID: 2010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalMatrixArrayImpl_Injected(int name, out BlittableArrayWrapper ret);

		// Token: 0x060007DB RID: 2011
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalFloatArrayImpl_Injected(int name, out BlittableArrayWrapper val);

		// Token: 0x060007DC RID: 2012
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalVectorArrayImpl_Injected(int name, out BlittableArrayWrapper val);

		// Token: 0x060007DD RID: 2013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalMatrixArrayImpl_Injected(int name, out BlittableArrayWrapper val);

		// Token: 0x060007DE RID: 2014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyName_Injected(IntPtr shader, int propertyIndex, out ManagedSpanWrapper ret);

		// Token: 0x060007DF RID: 2015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPropertyNameId_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x060007E0 RID: 2016
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderPropertyType GetPropertyType_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x060007E1 RID: 2017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyDescription_Injected(IntPtr shader, int propertyIndex, out ManagedSpanWrapper ret);

		// Token: 0x060007E2 RID: 2018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderPropertyFlags GetPropertyFlags_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x060007E3 RID: 2019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetPropertyAttributes_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x060007E4 RID: 2020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPropertyDefaultIntValue_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x060007E5 RID: 2021
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyDefaultValue_Injected(IntPtr shader, int propertyIndex, out Vector4 ret);

		// Token: 0x060007E6 RID: 2022
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension GetPropertyTextureDimension_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x060007E7 RID: 2023
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyTextureDefaultName_Injected(IntPtr shader, int propertyIndex, out ManagedSpanWrapper ret);

		// Token: 0x060007E8 RID: 2024
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindTextureStackImpl_Injected(IntPtr s, int propertyIdx, out ManagedSpanWrapper stackName, out int layerIndex);

		// Token: 0x060007E9 RID: 2025
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPropertyCount_Injected(IntPtr _unity_self);

		// Token: 0x060007EA RID: 2026
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindPropertyIndex_Injected(IntPtr _unity_self, ref ManagedSpanWrapper propertyName);
	}
}
