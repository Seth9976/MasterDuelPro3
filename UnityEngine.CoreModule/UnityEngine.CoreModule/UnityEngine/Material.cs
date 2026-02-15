using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000F0 RID: 240
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/Material.h")]
	public class Material : Object
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x0000F54C File Offset: 0x0000D74C
		[Obsolete("Creating materials from shader source string will be removed in the future. Use Shader assets instead.", false)]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0000F564 File Offset: 0x0000D764
		[FreeFunction("MaterialScripting::CreateWithShader")]
		private static void CreateWithShader([Writable] Material self, [NotNull] Shader shader)
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
			Material.CreateWithShader_Injected(self, intPtr);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0000F59C File Offset: 0x0000D79C
		[FreeFunction("MaterialScripting::CreateWithMaterial")]
		private static void CreateWithMaterial([Writable] Material self, [NotNull] Material source)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			Material.CreateWithMaterial_Injected(self, intPtr);
		}

		// Token: 0x060007EE RID: 2030
		[FreeFunction("MaterialScripting::CreateWithString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithString([Writable] Material self);

		// Token: 0x060007EF RID: 2031 RVA: 0x0000F5D3 File Offset: 0x0000D7D3
		public Material(Shader shader)
		{
			Material.CreateWithShader(this, shader);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0000F5E5 File Offset: 0x0000D7E5
		[RequiredByNativeCode]
		public Material(Material source)
		{
			Material.CreateWithMaterial(this, source);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0000F5F7 File Offset: 0x0000D7F7
		[Obsolete("Creating materials from shader source string is no longer supported. Use Shader assets instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Material(string contents)
		{
			Material.CreateWithString(this);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0000F608 File Offset: 0x0000D808
		internal static Material GetDefaultMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Material.GetDefaultMaterial_Injected());
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0000F620 File Offset: 0x0000D820
		internal static Material GetDefaultParticleMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Material.GetDefaultParticleMaterial_Injected());
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0000F638 File Offset: 0x0000D838
		internal static Material GetDefaultLineMaterial()
		{
			return Unmarshal.UnmarshalUnityObject<Material>(Material.GetDefaultLineMaterial_Injected());
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0000F650 File Offset: 0x0000D850
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0000F678 File Offset: 0x0000D878
		public Shader shader
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Shader>(Material.get_shader_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_shader_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(value));
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		public Color color
		{
			get
			{
				int nameId = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = nameId >= 0;
				Color color;
				if (flag)
				{
					color = this.GetColor(nameId);
				}
				else
				{
					color = this.GetColor(Material.k_ColorId);
				}
				return color;
			}
			set
			{
				int nameId = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainColor);
				bool flag = nameId >= 0;
				if (flag)
				{
					this.SetColor(nameId, value);
				}
				else
				{
					this.SetColor(Material.k_ColorId, value);
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0000F720 File Offset: 0x0000D920
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0000F760 File Offset: 0x0000D960
		public Texture mainTexture
		{
			get
			{
				int nameId = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = nameId >= 0;
				Texture texture;
				if (flag)
				{
					texture = this.GetTexture(nameId);
				}
				else
				{
					texture = this.GetTexture(Material.k_MainTexId);
				}
				return texture;
			}
			set
			{
				int nameId = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = nameId >= 0;
				if (flag)
				{
					this.SetTexture(nameId, value);
				}
				else
				{
					this.SetTexture(Material.k_MainTexId, value);
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public Vector2 mainTextureOffset
		{
			get
			{
				int nameId = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = nameId >= 0;
				Vector2 vector;
				if (flag)
				{
					vector = this.GetTextureOffset(nameId);
				}
				else
				{
					vector = this.GetTextureOffset(Material.k_MainTexId);
				}
				return vector;
			}
			set
			{
				int nameId = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = nameId >= 0;
				if (flag)
				{
					this.SetTextureOffset(nameId, value);
				}
				else
				{
					this.SetTextureOffset(Material.k_MainTexId, value);
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0000F820 File Offset: 0x0000DA20
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0000F860 File Offset: 0x0000DA60
		public Vector2 mainTextureScale
		{
			get
			{
				int nameId = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = nameId >= 0;
				Vector2 vector;
				if (flag)
				{
					vector = this.GetTextureScale(nameId);
				}
				else
				{
					vector = this.GetTextureScale(Material.k_MainTexId);
				}
				return vector;
			}
			set
			{
				int nameId = this.GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags.MainTexture);
				bool flag = nameId >= 0;
				if (flag)
				{
					this.SetTextureScale(nameId, value);
				}
				else
				{
					this.SetTextureScale(Material.k_MainTexId, value);
				}
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0000F8A0 File Offset: 0x0000DAA0
		[NativeName("GetFirstPropertyNameIdByAttributeFromScript")]
		private int GetFirstPropertyNameIdByAttribute(ShaderPropertyFlags attributeFlag)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetFirstPropertyNameIdByAttribute_Injected(intPtr, attributeFlag);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		[NativeName("HasPropertyFromScript")]
		public bool HasProperty(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasProperty_Injected(intPtr, nameID);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		public bool HasProperty(string name)
		{
			return this.HasProperty(Shader.PropertyToID(name));
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0000F908 File Offset: 0x0000DB08
		[NativeName("HasFloatFromScript")]
		private bool HasFloatImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasFloatImpl_Injected(intPtr, name);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0000F92C File Offset: 0x0000DB2C
		public bool HasFloat(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0000F94C File Offset: 0x0000DB4C
		public bool HasFloat(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0000F968 File Offset: 0x0000DB68
		public bool HasInt(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0000F988 File Offset: 0x0000DB88
		public bool HasInt(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		[NativeName("HasIntegerFromScript")]
		private bool HasIntImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasIntImpl_Injected(intPtr, name);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public bool HasInteger(string name)
		{
			return this.HasIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0000F9E8 File Offset: 0x0000DBE8
		public bool HasInteger(int nameID)
		{
			return this.HasIntImpl(nameID);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0000FA04 File Offset: 0x0000DC04
		[NativeName("HasTextureFromScript")]
		private bool HasTextureImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasTextureImpl_Injected(intPtr, name);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0000FA28 File Offset: 0x0000DC28
		public bool HasTexture(string name)
		{
			return this.HasTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public bool HasTexture(int nameID)
		{
			return this.HasTextureImpl(nameID);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0000FA64 File Offset: 0x0000DC64
		[NativeName("HasMatrixFromScript")]
		private bool HasMatrixImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasMatrixImpl_Injected(intPtr, name);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public bool HasMatrix(string name)
		{
			return this.HasMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0000FAA8 File Offset: 0x0000DCA8
		public bool HasMatrix(int nameID)
		{
			return this.HasMatrixImpl(nameID);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
		[NativeName("HasVectorFromScript")]
		private bool HasVectorImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasVectorImpl_Injected(intPtr, name);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public bool HasVector(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000FB08 File Offset: 0x0000DD08
		public bool HasVector(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000FB24 File Offset: 0x0000DD24
		public bool HasColor(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public bool HasColor(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000FB60 File Offset: 0x0000DD60
		[NativeName("HasBufferFromScript")]
		private bool HasBufferImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasBufferImpl_Injected(intPtr, name);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000FB84 File Offset: 0x0000DD84
		public bool HasBuffer(string name)
		{
			return this.HasBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public bool HasBuffer(int nameID)
		{
			return this.HasBufferImpl(nameID);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		[NativeName("HasConstantBufferFromScript")]
		private bool HasConstantBufferImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.HasConstantBufferImpl_Injected(intPtr, name);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		public bool HasConstantBuffer(string name)
		{
			return this.HasConstantBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public bool HasConstantBuffer(int nameID)
		{
			return this.HasConstantBufferImpl(nameID);
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0000FC20 File Offset: 0x0000DE20
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x0000FC44 File Offset: 0x0000DE44
		public int renderQueue
		{
			[NativeName("GetActualRenderQueue")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_renderQueue_Injected(intPtr);
			}
			[NativeName("SetCustomRenderQueue")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_renderQueue_Injected(intPtr, value);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0000FC68 File Offset: 0x0000DE68
		public int rawRenderQueue
		{
			[NativeName("GetCustomRenderQueue")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_rawRenderQueue_Injected(intPtr);
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		public unsafe void EnableKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Material.EnableKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public unsafe void DisableKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Material.DisableKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0000FD54 File Offset: 0x0000DF54
		public unsafe bool IsKeywordEnabled(string keyword)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Material.IsKeywordEnabled_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		[FreeFunction("MaterialScripting::EnableKeyword", HasExplicitThis = true)]
		private void EnableLocalKeyword(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.EnableLocalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		[FreeFunction("MaterialScripting::DisableKeyword", HasExplicitThis = true)]
		private void DisableLocalKeyword(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.DisableLocalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0000FE04 File Offset: 0x0000E004
		[FreeFunction("MaterialScripting::SetKeyword", HasExplicitThis = true)]
		private void SetLocalKeyword(LocalKeyword keyword, bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetLocalKeyword_Injected(intPtr, ref keyword, value);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0000FE2C File Offset: 0x0000E02C
		[FreeFunction("MaterialScripting::IsKeywordEnabled", HasExplicitThis = true)]
		private bool IsLocalKeywordEnabled(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.IsLocalKeywordEnabled_Injected(intPtr, ref keyword);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0000FE50 File Offset: 0x0000E050
		public void EnableKeyword(in LocalKeyword keyword)
		{
			this.EnableLocalKeyword(keyword);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0000FE60 File Offset: 0x0000E060
		public void DisableKeyword(in LocalKeyword keyword)
		{
			this.DisableLocalKeyword(keyword);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0000FE70 File Offset: 0x0000E070
		public void SetKeyword(in LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword(keyword, value);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0000FE84 File Offset: 0x0000E084
		public bool IsKeywordEnabled(in LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled(keyword);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		[FreeFunction("MaterialScripting::GetEnabledKeywords", HasExplicitThis = true)]
		private LocalKeyword[] GetEnabledKeywords()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetEnabledKeywords_Injected(intPtr);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		[FreeFunction("MaterialScripting::SetEnabledKeywords", HasExplicitThis = true)]
		private void SetEnabledKeywords(LocalKeyword[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetEnabledKeywords_Injected(intPtr, keywords);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x0000FF04 File Offset: 0x0000E104
		public LocalKeyword[] enabledKeywords
		{
			get
			{
				return this.GetEnabledKeywords();
			}
			set
			{
				this.SetEnabledKeywords(value);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0000FF10 File Offset: 0x0000E110
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x0000FF34 File Offset: 0x0000E134
		public MaterialGlobalIlluminationFlags globalIlluminationFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_globalIlluminationFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_globalIlluminationFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0000FF58 File Offset: 0x0000E158
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x0000FF7C File Offset: 0x0000E17C
		public bool doubleSidedGI
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_doubleSidedGI_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_doubleSidedGI_Injected(intPtr, value);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		[NativeProperty("EnableInstancingVariants")]
		public bool enableInstancing
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_enableInstancing_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Material.set_enableInstancing_Injected(intPtr, value);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public int passCount
		{
			[NativeName("GetShader()->GetPassCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Material.get_passCount_Injected(intPtr);
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001000C File Offset: 0x0000E20C
		[FreeFunction("MaterialScripting::SetShaderPassEnabled", HasExplicitThis = true)]
		public unsafe void SetShaderPassEnabled(string passName, bool enabled)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(passName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = passName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Material.SetShaderPassEnabled_Injected(intPtr, ref managedSpanWrapper, enabled);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00010074 File Offset: 0x0000E274
		[FreeFunction("MaterialScripting::GetShaderPassEnabled", HasExplicitThis = true)]
		public unsafe bool GetShaderPassEnabled(string passName)
		{
			bool shaderPassEnabled_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(passName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = passName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				shaderPassEnabled_Injected = Material.GetShaderPassEnabled_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return shaderPassEnabled_Injected;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000100DC File Offset: 0x0000E2DC
		public string GetPassName(int pass)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Material.GetPassName_Injected(intPtr, pass, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001011C File Offset: 0x0000E31C
		public unsafe int FindPass(string passName)
		{
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(passName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = passName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Material.FindPass_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00010184 File Offset: 0x0000E384
		public unsafe void SetOverrideTag(string tag, string val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(val, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = val.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				Material.SetOverrideTag_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001021C File Offset: 0x0000E41C
		[NativeName("GetTag")]
		private unsafe string GetTagImpl(string tag, bool currentSubShaderOnly, string defaultValue)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(tag, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = tag.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(defaultValue, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = defaultValue.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper3;
				Material.GetTagImpl_Injected(intPtr, ref managedSpanWrapper, currentSubShaderOnly, ref managedSpanWrapper2, out managedSpanWrapper3);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				ManagedSpanWrapper managedSpanWrapper3;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper3);
			}
			return stringAndDispose;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000102C0 File Offset: 0x0000E4C0
		public string GetTag(string tag, bool searchFallbacks, string defaultValue)
		{
			return this.GetTagImpl(tag, !searchFallbacks, defaultValue);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000102E0 File Offset: 0x0000E4E0
		public string GetTag(string tag, bool searchFallbacks)
		{
			return this.GetTagImpl(tag, !searchFallbacks, "");
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00010304 File Offset: 0x0000E504
		[FreeFunction("MaterialScripting::Lerp", HasExplicitThis = true)]
		[NativeThrows]
		public void Lerp(Material start, Material end, float t)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.Lerp_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(start), Object.MarshalledUnityObject.Marshal<Material>(end), t);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00010334 File Offset: 0x0000E534
		[FreeFunction("MaterialScripting::SetPass", HasExplicitThis = true)]
		public bool SetPass(int pass)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.SetPass_Injected(intPtr, pass);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00010358 File Offset: 0x0000E558
		[FreeFunction("MaterialScripting::CopyPropertiesFrom", HasExplicitThis = true)]
		public void CopyPropertiesFromMaterial(Material mat)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.CopyPropertiesFromMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(mat));
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00010380 File Offset: 0x0000E580
		[FreeFunction("MaterialScripting::CopyMatchingPropertiesFrom", HasExplicitThis = true)]
		public void CopyMatchingPropertiesFromMaterial(Material mat)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.CopyMatchingPropertiesFromMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(mat));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000103A8 File Offset: 0x0000E5A8
		[FreeFunction("MaterialScripting::GetShaderKeywords", HasExplicitThis = true)]
		private string[] GetShaderKeywords()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetShaderKeywords_Injected(intPtr);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000103CC File Offset: 0x0000E5CC
		[FreeFunction("MaterialScripting::SetShaderKeywords", HasExplicitThis = true)]
		private void SetShaderKeywords(string[] names)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetShaderKeywords_Injected(intPtr, names);
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x000103F0 File Offset: 0x0000E5F0
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x00010408 File Offset: 0x0000E608
		public string[] shaderKeywords
		{
			get
			{
				return this.GetShaderKeywords();
			}
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00010414 File Offset: 0x0000E614
		[FreeFunction("MaterialScripting::GetPropertyNames", HasExplicitThis = true)]
		private string[] GetPropertyNamesImpl(int propertyType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetPropertyNamesImpl_Injected(intPtr, propertyType);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00010438 File Offset: 0x0000E638
		public int ComputeCRC()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.ComputeCRC_Injected(intPtr);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001045C File Offset: 0x0000E65C
		[FreeFunction("MaterialScripting::GetTexturePropertyNames", HasExplicitThis = true)]
		public string[] GetTexturePropertyNames()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetTexturePropertyNames_Injected(intPtr);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00010480 File Offset: 0x0000E680
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDs", HasExplicitThis = true)]
		public int[] GetTexturePropertyNameIDs()
		{
			int[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetTexturePropertyNameIDs_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000104C4 File Offset: 0x0000E6C4
		[FreeFunction("MaterialScripting::GetTexturePropertyNamesInternal", HasExplicitThis = true)]
		private void GetTexturePropertyNamesInternal(object outNames)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.GetTexturePropertyNamesInternal_Injected(intPtr, outNames);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000104E8 File Offset: 0x0000E6E8
		[FreeFunction("MaterialScripting::GetTexturePropertyNameIDsInternal", HasExplicitThis = true)]
		private void GetTexturePropertyNameIDsInternal(object outNames)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.GetTexturePropertyNameIDsInternal_Injected(intPtr, outNames);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001050C File Offset: 0x0000E70C
		public void GetTexturePropertyNames(List<string> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNamesInternal(outNames);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00010538 File Offset: 0x0000E738
		public void GetTexturePropertyNameIDs(List<int> outNames)
		{
			bool flag = outNames == null;
			if (flag)
			{
				throw new ArgumentNullException("outNames");
			}
			this.GetTexturePropertyNameIDsInternal(outNames);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00010564 File Offset: 0x0000E764
		[NativeName("SetIntFromScript")]
		private void SetIntImpl(int name, int value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetIntImpl_Injected(intPtr, name, value);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00010588 File Offset: 0x0000E788
		[NativeName("SetFloatFromScript")]
		private void SetFloatImpl(int name, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetFloatImpl_Injected(intPtr, name, value);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000105AC File Offset: 0x0000E7AC
		[NativeName("SetColorFromScript")]
		private void SetColorImpl(int name, Color value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetColorImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000105D4 File Offset: 0x0000E7D4
		[NativeName("SetMatrixFromScript")]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetMatrixImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000105FC File Offset: 0x0000E7FC
		[NativeName("SetTextureFromScript")]
		private void SetTextureImpl(int name, Texture value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetTextureImpl_Injected(intPtr, name, Object.MarshalledUnityObject.Marshal<Texture>(value));
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00010628 File Offset: 0x0000E828
		[NativeName("SetRenderTextureFromScript")]
		private void SetRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetRenderTextureImpl_Injected(intPtr, name, Object.MarshalledUnityObject.Marshal<RenderTexture>(value), element);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00010654 File Offset: 0x0000E854
		[NativeName("SetBufferFromScript")]
		private void SetBufferImpl(int name, ComputeBuffer value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00010688 File Offset: 0x0000E888
		[NativeName("SetBufferFromScript")]
		private void SetGraphicsBufferImpl(int name, GraphicsBuffer value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetGraphicsBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000106BC File Offset: 0x0000E8BC
		[NativeName("SetConstantBufferFromScript")]
		private void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetConstantBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000106F4 File Offset: 0x0000E8F4
		[NativeName("SetConstantBufferFromScript")]
		private void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetConstantGraphicsBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001072C File Offset: 0x0000E92C
		[NativeName("GetIntFromScript")]
		private int GetIntImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetIntImpl_Injected(intPtr, name);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00010750 File Offset: 0x0000E950
		[NativeName("GetFloatFromScript")]
		private float GetFloatImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetFloatImpl_Injected(intPtr, name);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00010774 File Offset: 0x0000E974
		[NativeName("GetColorFromScript")]
		private Color GetColorImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Material.GetColorImpl_Injected(intPtr, name, out color);
			return color;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001079C File Offset: 0x0000E99C
		[NativeName("GetMatrixFromScript")]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Material.GetMatrixImpl_Injected(intPtr, name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000107C4 File Offset: 0x0000E9C4
		[NativeName("GetTextureFromScript")]
		private Texture GetTextureImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture>(Material.GetTextureImpl_Injected(intPtr, name));
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000107EC File Offset: 0x0000E9EC
		[NativeName("GetBufferFromScript")]
		private GraphicsBufferHandle GetBufferImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBufferHandle graphicsBufferHandle;
			Material.GetBufferImpl_Injected(intPtr, name, out graphicsBufferHandle);
			return graphicsBufferHandle;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00010814 File Offset: 0x0000EA14
		[NativeName("GetConstantBufferFromScript")]
		private GraphicsBufferHandle GetConstantBufferImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsBufferHandle graphicsBufferHandle;
			Material.GetConstantBufferImpl_Injected(intPtr, name, out graphicsBufferHandle);
			return graphicsBufferHandle;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001083C File Offset: 0x0000EA3C
		[FreeFunction(Name = "MaterialScripting::SetFloatArray", HasExplicitThis = true)]
		private unsafe void SetFloatArrayImpl(int name, float[] values, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Material.SetFloatArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00010888 File Offset: 0x0000EA88
		[FreeFunction(Name = "MaterialScripting::SetVectorArray", HasExplicitThis = true)]
		private unsafe void SetVectorArrayImpl(int name, Vector4[] values, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Material.SetVectorArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000108D4 File Offset: 0x0000EAD4
		[FreeFunction(Name = "MaterialScripting::SetColorArray", HasExplicitThis = true)]
		private unsafe void SetColorArrayImpl(int name, Color[] values, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Color> span = new Span<Color>(values);
			fixed (Color* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Material.SetColorArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00010920 File Offset: 0x0000EB20
		[FreeFunction(Name = "MaterialScripting::SetMatrixArray", HasExplicitThis = true)]
		private unsafe void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Material.SetMatrixArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001096C File Offset: 0x0000EB6C
		[FreeFunction(Name = "MaterialScripting::GetFloatArray", HasExplicitThis = true)]
		private float[] GetFloatArrayImpl(int name)
		{
			float[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetFloatArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
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

		// Token: 0x06000862 RID: 2146 RVA: 0x000109B0 File Offset: 0x0000EBB0
		[FreeFunction(Name = "MaterialScripting::GetVectorArray", HasExplicitThis = true)]
		private Vector4[] GetVectorArrayImpl(int name)
		{
			Vector4[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetVectorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
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

		// Token: 0x06000863 RID: 2147 RVA: 0x000109F4 File Offset: 0x0000EBF4
		[FreeFunction(Name = "MaterialScripting::GetColorArray", HasExplicitThis = true)]
		private Color[] GetColorArrayImpl(int name)
		{
			Color[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetColorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Color[] array;
				blittableArrayWrapper.Unmarshal<Color>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00010A38 File Offset: 0x0000EC38
		[FreeFunction(Name = "MaterialScripting::GetMatrixArray", HasExplicitThis = true)]
		private Matrix4x4[] GetMatrixArrayImpl(int name)
		{
			Matrix4x4[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Material.GetMatrixArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
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

		// Token: 0x06000865 RID: 2149 RVA: 0x00010A7C File Offset: 0x0000EC7C
		[FreeFunction(Name = "MaterialScripting::GetFloatArrayCount", HasExplicitThis = true)]
		private int GetFloatArrayCountImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetFloatArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		[FreeFunction(Name = "MaterialScripting::GetVectorArrayCount", HasExplicitThis = true)]
		private int GetVectorArrayCountImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetVectorArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00010AC4 File Offset: 0x0000ECC4
		[FreeFunction(Name = "MaterialScripting::GetColorArrayCount", HasExplicitThis = true)]
		private int GetColorArrayCountImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetColorArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00010AE8 File Offset: 0x0000ECE8
		[FreeFunction(Name = "MaterialScripting::GetMatrixArrayCount", HasExplicitThis = true)]
		private int GetMatrixArrayCountImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Material.GetMatrixArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00010B0C File Offset: 0x0000ED0C
		[FreeFunction(Name = "MaterialScripting::ExtractFloatArray", HasExplicitThis = true)]
		private unsafe void ExtractFloatArrayImpl(int name, [Out] float[] val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
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
				Material.ExtractFloatArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00010B6C File Offset: 0x0000ED6C
		[FreeFunction(Name = "MaterialScripting::ExtractVectorArray", HasExplicitThis = true)]
		private unsafe void ExtractVectorArrayImpl(int name, [Out] Vector4[] val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
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
				Material.ExtractVectorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				Vector4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00010BCC File Offset: 0x0000EDCC
		[FreeFunction(Name = "MaterialScripting::ExtractColorArray", HasExplicitThis = true)]
		private unsafe void ExtractColorArrayImpl(int name, [Out] Color[] val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (Color[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Material.ExtractColorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				Color[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Color>(ref array);
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00010C2C File Offset: 0x0000EE2C
		[FreeFunction(Name = "MaterialScripting::ExtractMatrixArray", HasExplicitThis = true)]
		private unsafe void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
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
				Material.ExtractMatrixArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				Matrix4x4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00010C8C File Offset: 0x0000EE8C
		[NativeName("GetTextureScaleAndOffsetFromScript")]
		private Vector4 GetTextureScaleAndOffsetImpl(int name)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			Material.GetTextureScaleAndOffsetImpl_Injected(intPtr, name, out vector);
			return vector;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00010CB4 File Offset: 0x0000EEB4
		[NativeName("SetTextureOffsetFromScript")]
		private void SetTextureOffsetImpl(int name, Vector2 offset)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetTextureOffsetImpl_Injected(intPtr, name, ref offset);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00010CDC File Offset: 0x0000EEDC
		[NativeName("SetTextureScaleFromScript")]
		private void SetTextureScaleImpl(int name, Vector2 scale)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Material.SetTextureScaleImpl_Injected(intPtr, name, ref scale);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00010D04 File Offset: 0x0000EF04
		private void SetFloatArray(int name, float[] values, int count)
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
			this.SetFloatArrayImpl(name, values, count);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00010D58 File Offset: 0x0000EF58
		private void SetVectorArray(int name, Vector4[] values, int count)
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
			this.SetVectorArrayImpl(name, values, count);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00010DAC File Offset: 0x0000EFAC
		private void SetColorArray(int name, Color[] values, int count)
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
			this.SetColorArrayImpl(name, values, count);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00010E00 File Offset: 0x0000F000
		private void SetMatrixArray(int name, Matrix4x4[] values, int count)
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
			this.SetMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00010E54 File Offset: 0x0000F054
		private void ExtractFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int count = this.GetFloatArrayCountImpl(name);
			bool flag2 = count > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, count);
				this.ExtractFloatArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<float>(values));
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		private void ExtractVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int count = this.GetVectorArrayCountImpl(name);
			bool flag2 = count > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, count);
				this.ExtractVectorArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Vector4>(values));
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		private void ExtractColorArray(int name, List<Color> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int count = this.GetColorArrayCountImpl(name);
			bool flag2 = count > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Color>(values, count);
				this.ExtractColorArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Color>(values));
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00010F44 File Offset: 0x0000F144
		private void ExtractMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int count = this.GetMatrixArrayCountImpl(name);
			bool flag2 = count > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, count);
				this.ExtractMatrixArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values));
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00010F94 File Offset: 0x0000F194
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00010FA6 File Offset: 0x0000F1A6
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00010FB3 File Offset: 0x0000F1B3
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00010FD0 File Offset: 0x0000F1D0
		public void SetInteger(string name, int value)
		{
			this.SetIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00010FE1 File Offset: 0x0000F1E1
		public void SetInteger(int nameID, int value)
		{
			this.SetIntImpl(nameID, value);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00010FED File Offset: 0x0000F1ED
		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00010FFE File Offset: 0x0000F1FE
		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001100A File Offset: 0x0000F20A
		public void SetVector(string name, Vector4 value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00011020 File Offset: 0x0000F220
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00011031 File Offset: 0x0000F231
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00011042 File Offset: 0x0000F242
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001104E File Offset: 0x0000F24E
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001105F File Offset: 0x0000F25F
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001106B File Offset: 0x0000F26B
		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001107D File Offset: 0x0000F27D
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001108A File Offset: 0x0000F28A
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001109B File Offset: 0x0000F29B
		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000110A7 File Offset: 0x0000F2A7
		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000110B8 File Offset: 0x0000F2B8
		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000110D8 File Offset: 0x0000F2D8
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000110E7 File Offset: 0x0000F2E7
		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000110FB File Offset: 0x0000F2FB
		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001110A File Offset: 0x0000F30A
		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00011126 File Offset: 0x0000F326
		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001113D File Offset: 0x0000F33D
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00011151 File Offset: 0x0000F351
		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00011160 File Offset: 0x0000F360
		public void SetColorArray(string name, List<Color> values)
		{
			this.SetColorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Color>(values), values.Count);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001117C File Offset: 0x0000F37C
		public void SetColorArray(int nameID, List<Color> values)
		{
			this.SetColorArray(nameID, NoAllocHelpers.ExtractArrayFromList<Color>(values), values.Count);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00011193 File Offset: 0x0000F393
		public void SetColorArray(string name, Color[] values)
		{
			this.SetColorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000111A7 File Offset: 0x0000F3A7
		public void SetColorArray(int nameID, Color[] values)
		{
			this.SetColorArray(nameID, values, values.Length);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000111B6 File Offset: 0x0000F3B6
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000111D2 File Offset: 0x0000F3D2
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000111E9 File Offset: 0x0000F3E9
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000111FD File Offset: 0x0000F3FD
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001120C File Offset: 0x0000F40C
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00011228 File Offset: 0x0000F428
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001123F File Offset: 0x0000F43F
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00011253 File Offset: 0x0000F453
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00011264 File Offset: 0x0000F464
		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00011284 File Offset: 0x0000F484
		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000112A0 File Offset: 0x0000F4A0
		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000112DC File Offset: 0x0000F4DC
		public int GetInteger(string name)
		{
			return this.GetIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000112FC File Offset: 0x0000F4FC
		public int GetInteger(int nameID)
		{
			return this.GetIntImpl(nameID);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00011318 File Offset: 0x0000F518
		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00011338 File Offset: 0x0000F538
		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00011354 File Offset: 0x0000F554
		public Vector4 GetVector(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00011378 File Offset: 0x0000F578
		public Vector4 GetVector(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00011398 File Offset: 0x0000F598
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x000113F4 File Offset: 0x0000F5F4
		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00011410 File Offset: 0x0000F610
		public GraphicsBufferHandle GetBuffer(string name)
		{
			return this.GetBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00011430 File Offset: 0x0000F630
		public GraphicsBufferHandle GetConstantBuffer(string name)
		{
			return this.GetConstantBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00011450 File Offset: 0x0000F650
		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00011470 File Offset: 0x0000F670
		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00011498 File Offset: 0x0000F698
		public Color[] GetColorArray(string name)
		{
			return this.GetColorArray(Shader.PropertyToID(name));
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000114B8 File Offset: 0x0000F6B8
		public Color[] GetColorArray(int nameID)
		{
			return (this.GetColorArrayCountImpl(nameID) != 0) ? this.GetColorArrayImpl(nameID) : null;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000114E0 File Offset: 0x0000F6E0
		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00011500 File Offset: 0x0000F700
		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00011528 File Offset: 0x0000F728
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00011548 File Offset: 0x0000F748
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001156D File Offset: 0x0000F76D
		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001157E File Offset: 0x0000F77E
		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001158A File Offset: 0x0000F78A
		public void GetColorArray(string name, List<Color> values)
		{
			this.ExtractColorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001159B File Offset: 0x0000F79B
		public void GetColorArray(int nameID, List<Color> values)
		{
			this.ExtractColorArray(nameID, values);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000115A7 File Offset: 0x0000F7A7
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000115B8 File Offset: 0x0000F7B8
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000115C4 File Offset: 0x0000F7C4
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000115D5 File Offset: 0x0000F7D5
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000115E1 File Offset: 0x0000F7E1
		public void SetTextureOffset(string name, Vector2 value)
		{
			this.SetTextureOffsetImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000115F2 File Offset: 0x0000F7F2
		public void SetTextureOffset(int nameID, Vector2 value)
		{
			this.SetTextureOffsetImpl(nameID, value);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000115FE File Offset: 0x0000F7FE
		public void SetTextureScale(string name, Vector2 value)
		{
			this.SetTextureScaleImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001160F File Offset: 0x0000F80F
		public void SetTextureScale(int nameID, Vector2 value)
		{
			this.SetTextureScaleImpl(nameID, value);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001161C File Offset: 0x0000F81C
		public Vector2 GetTextureOffset(string name)
		{
			return this.GetTextureOffset(Shader.PropertyToID(name));
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001163C File Offset: 0x0000F83C
		public Vector2 GetTextureOffset(int nameID)
		{
			Vector4 st = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(st.z, st.w);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00011668 File Offset: 0x0000F868
		public Vector2 GetTextureScale(string name)
		{
			return this.GetTextureScale(Shader.PropertyToID(name));
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00011688 File Offset: 0x0000F888
		public Vector2 GetTextureScale(int nameID)
		{
			Vector4 st = this.GetTextureScaleAndOffsetImpl(nameID);
			return new Vector2(st.x, st.y);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000116B4 File Offset: 0x0000F8B4
		public string[] GetPropertyNames(MaterialPropertyType type)
		{
			return this.GetPropertyNamesImpl((int)type);
		}

		// Token: 0x060008CA RID: 2250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithShader_Injected([Writable] Material self, IntPtr shader);

		// Token: 0x060008CB RID: 2251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateWithMaterial_Injected([Writable] Material self, IntPtr source);

		// Token: 0x060008CC RID: 2252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultMaterial_Injected();

		// Token: 0x060008CD RID: 2253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultParticleMaterial_Injected();

		// Token: 0x060008CE RID: 2254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDefaultLineMaterial_Injected();

		// Token: 0x060008CF RID: 2255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_shader_Injected(IntPtr _unity_self);

		// Token: 0x060008D0 RID: 2256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shader_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060008D1 RID: 2257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFirstPropertyNameIdByAttribute_Injected(IntPtr _unity_self, ShaderPropertyFlags attributeFlag);

		// Token: 0x060008D2 RID: 2258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasProperty_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060008D3 RID: 2259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasFloatImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060008D4 RID: 2260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasIntImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060008D5 RID: 2261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasTextureImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060008D6 RID: 2262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasMatrixImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060008D7 RID: 2263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVectorImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060008D8 RID: 2264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasBufferImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060008D9 RID: 2265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasConstantBufferImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x060008DA RID: 2266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_renderQueue_Injected(IntPtr _unity_self);

		// Token: 0x060008DB RID: 2267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderQueue_Injected(IntPtr _unity_self, int value);

		// Token: 0x060008DC RID: 2268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_rawRenderQueue_Injected(IntPtr _unity_self);

		// Token: 0x060008DD RID: 2269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x060008DE RID: 2270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x060008DF RID: 2271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabled_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x060008E0 RID: 2272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x060008E1 RID: 2273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x060008E2 RID: 2274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword, bool value);

		// Token: 0x060008E3 RID: 2275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsLocalKeywordEnabled_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x060008E4 RID: 2276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LocalKeyword[] GetEnabledKeywords_Injected(IntPtr _unity_self);

		// Token: 0x060008E5 RID: 2277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnabledKeywords_Injected(IntPtr _unity_self, LocalKeyword[] keywords);

		// Token: 0x060008E6 RID: 2278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MaterialGlobalIlluminationFlags get_globalIlluminationFlags_Injected(IntPtr _unity_self);

		// Token: 0x060008E7 RID: 2279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_globalIlluminationFlags_Injected(IntPtr _unity_self, MaterialGlobalIlluminationFlags value);

		// Token: 0x060008E8 RID: 2280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_doubleSidedGI_Injected(IntPtr _unity_self);

		// Token: 0x060008E9 RID: 2281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_doubleSidedGI_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060008EA RID: 2282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableInstancing_Injected(IntPtr _unity_self);

		// Token: 0x060008EB RID: 2283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableInstancing_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060008EC RID: 2284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_passCount_Injected(IntPtr _unity_self);

		// Token: 0x060008ED RID: 2285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShaderPassEnabled_Injected(IntPtr _unity_self, ref ManagedSpanWrapper passName, bool enabled);

		// Token: 0x060008EE RID: 2286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetShaderPassEnabled_Injected(IntPtr _unity_self, ref ManagedSpanWrapper passName);

		// Token: 0x060008EF RID: 2287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPassName_Injected(IntPtr _unity_self, int pass, out ManagedSpanWrapper ret);

		// Token: 0x060008F0 RID: 2288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindPass_Injected(IntPtr _unity_self, ref ManagedSpanWrapper passName);

		// Token: 0x060008F1 RID: 2289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetOverrideTag_Injected(IntPtr _unity_self, ref ManagedSpanWrapper tag, ref ManagedSpanWrapper val);

		// Token: 0x060008F2 RID: 2290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTagImpl_Injected(IntPtr _unity_self, ref ManagedSpanWrapper tag, bool currentSubShaderOnly, ref ManagedSpanWrapper defaultValue, out ManagedSpanWrapper ret);

		// Token: 0x060008F3 RID: 2291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lerp_Injected(IntPtr _unity_self, IntPtr start, IntPtr end, float t);

		// Token: 0x060008F4 RID: 2292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPass_Injected(IntPtr _unity_self, int pass);

		// Token: 0x060008F5 RID: 2293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPropertiesFromMaterial_Injected(IntPtr _unity_self, IntPtr mat);

		// Token: 0x060008F6 RID: 2294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyMatchingPropertiesFromMaterial_Injected(IntPtr _unity_self, IntPtr mat);

		// Token: 0x060008F7 RID: 2295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetShaderKeywords_Injected(IntPtr _unity_self);

		// Token: 0x060008F8 RID: 2296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShaderKeywords_Injected(IntPtr _unity_self, string[] names);

		// Token: 0x060008F9 RID: 2297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetPropertyNamesImpl_Injected(IntPtr _unity_self, int propertyType);

		// Token: 0x060008FA RID: 2298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ComputeCRC_Injected(IntPtr _unity_self);

		// Token: 0x060008FB RID: 2299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetTexturePropertyNames_Injected(IntPtr _unity_self);

		// Token: 0x060008FC RID: 2300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTexturePropertyNameIDs_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060008FD RID: 2301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTexturePropertyNamesInternal_Injected(IntPtr _unity_self, object outNames);

		// Token: 0x060008FE RID: 2302
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTexturePropertyNameIDsInternal_Injected(IntPtr _unity_self, object outNames);

		// Token: 0x060008FF RID: 2303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntImpl_Injected(IntPtr _unity_self, int name, int value);

		// Token: 0x06000900 RID: 2304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatImpl_Injected(IntPtr _unity_self, int name, float value);

		// Token: 0x06000901 RID: 2305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorImpl_Injected(IntPtr _unity_self, int name, [In] ref Color value);

		// Token: 0x06000902 RID: 2306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixImpl_Injected(IntPtr _unity_self, int name, [In] ref Matrix4x4 value);

		// Token: 0x06000903 RID: 2307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06000904 RID: 2308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTextureImpl_Injected(IntPtr _unity_self, int name, IntPtr value, RenderTextureSubElement element);

		// Token: 0x06000905 RID: 2309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06000906 RID: 2310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGraphicsBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06000907 RID: 2311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value, int offset, int size);

		// Token: 0x06000908 RID: 2312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantGraphicsBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value, int offset, int size);

		// Token: 0x06000909 RID: 2313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600090A RID: 2314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600090B RID: 2315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColorImpl_Injected(IntPtr _unity_self, int name, out Color ret);

		// Token: 0x0600090C RID: 2316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrixImpl_Injected(IntPtr _unity_self, int name, out Matrix4x4 ret);

		// Token: 0x0600090D RID: 2317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTextureImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600090E RID: 2318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBufferImpl_Injected(IntPtr _unity_self, int name, out GraphicsBufferHandle ret);

		// Token: 0x0600090F RID: 2319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetConstantBufferImpl_Injected(IntPtr _unity_self, int name, out GraphicsBufferHandle ret);

		// Token: 0x06000910 RID: 2320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06000911 RID: 2321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06000912 RID: 2322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06000913 RID: 2323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06000914 RID: 2324
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFloatArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06000915 RID: 2325
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVectorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06000916 RID: 2326
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06000917 RID: 2327
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrixArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06000918 RID: 2328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFloatArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000919 RID: 2329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVectorArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600091A RID: 2330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColorArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600091B RID: 2331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMatrixArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x0600091C RID: 2332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractFloatArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x0600091D RID: 2333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractVectorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x0600091E RID: 2334
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractColorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x0600091F RID: 2335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractMatrixArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x06000920 RID: 2336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTextureScaleAndOffsetImpl_Injected(IntPtr _unity_self, int name, out Vector4 ret);

		// Token: 0x06000921 RID: 2337
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureOffsetImpl_Injected(IntPtr _unity_self, int name, [In] ref Vector2 offset);

		// Token: 0x06000922 RID: 2338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureScaleImpl_Injected(IntPtr _unity_self, int name, [In] ref Vector2 scale);

		// Token: 0x040002B7 RID: 695
		private static readonly int k_ColorId = Shader.PropertyToID("_Color");

		// Token: 0x040002B8 RID: 696
		private static readonly int k_MainTexId = Shader.PropertyToID("_MainTex");
	}
}
