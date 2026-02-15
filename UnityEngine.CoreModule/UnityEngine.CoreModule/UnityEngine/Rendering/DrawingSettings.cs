using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020003AD RID: 941
	public struct DrawingSettings : IEquatable<DrawingSettings>
	{
		// Token: 0x06001981 RID: 6529 RVA: 0x000377FC File Offset: 0x000359FC
		public unsafe DrawingSettings(ShaderTagId shaderPassName, SortingSettings sortingSettings)
		{
			this.m_SortingSettings = sortingSettings;
			this.m_PerObjectData = PerObjectData.None;
			this.m_Flags = DrawRendererFlags.EnableInstancing;
			this.m_OverrideShaderID = 0;
			this.m_OverrideShaderPassIndex = 0;
			this.m_OverrideMaterialInstanceId = 0;
			this.m_OverrideMaterialPassIndex = 0;
			this.m_fallbackMaterialInstanceId = 0;
			this.m_MainLightIndex = -1;
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* p = ptr;
				*p = shaderPassName.id;
				for (int i = 1; i < DrawingSettings.maxShaderPasses; i++)
				{
					p[i] = -1;
				}
			}
			this.m_UseSrpBatcher = 0;
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0003788C File Offset: 0x00035A8C
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x000378A4 File Offset: 0x00035AA4
		public SortingSettings sortingSettings
		{
			get
			{
				return this.m_SortingSettings;
			}
			set
			{
				this.m_SortingSettings = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x000378AE File Offset: 0x00035AAE
		public PerObjectData perObjectData
		{
			set
			{
				this.m_PerObjectData = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x000378B8 File Offset: 0x00035AB8
		public bool enableDynamicBatching
		{
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableDynamicBatching;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableDynamicBatching;
				}
			}
		}

		// Token: 0x170003B5 RID: 949
		// (set) Token: 0x06001986 RID: 6534 RVA: 0x000378EC File Offset: 0x00035AEC
		public bool enableInstancing
		{
			set
			{
				if (value)
				{
					this.m_Flags |= DrawRendererFlags.EnableInstancing;
				}
				else
				{
					this.m_Flags &= ~DrawRendererFlags.EnableInstancing;
				}
			}
		}

		// Token: 0x170003B6 RID: 950
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x0003791E File Offset: 0x00035B1E
		public Material overrideMaterial
		{
			set
			{
				this.m_OverrideMaterialInstanceId = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (set) Token: 0x06001988 RID: 6536 RVA: 0x00037933 File Offset: 0x00035B33
		public Shader overrideShader
		{
			set
			{
				this.m_OverrideShaderID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x00037948 File Offset: 0x00035B48
		public int overrideMaterialPassIndex
		{
			set
			{
				this.m_OverrideMaterialPassIndex = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (set) Token: 0x0600198A RID: 6538 RVA: 0x00037952 File Offset: 0x00035B52
		public int overrideShaderPassIndex
		{
			set
			{
				this.m_OverrideShaderPassIndex = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x0003795C File Offset: 0x00035B5C
		public int mainLightIndex
		{
			set
			{
				this.m_MainLightIndex = value;
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00037968 File Offset: 0x00035B68
		public unsafe ShaderTagId GetShaderPassName(int index)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* p = ptr;
				return new ShaderTagId
				{
					id = p[index]
				};
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000379DC File Offset: 0x00035BDC
		public unsafe void SetShaderPassName(int index, ShaderTagId shaderPassName)
		{
			bool flag = index >= DrawingSettings.maxShaderPasses || index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Index should range from 0 to DrawSettings.maxShaderPasses ({0}), was {1}", DrawingSettings.maxShaderPasses, index));
			}
			fixed (int* ptr = &this.shaderPassNames.FixedElementField)
			{
				int* p = ptr;
				p[index] = shaderPassName.id;
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00037A44 File Offset: 0x00035C44
		public bool Equals(DrawingSettings other)
		{
			for (int i = 0; i < DrawingSettings.maxShaderPasses; i++)
			{
				bool flag = !this.GetShaderPassName(i).Equals(other.GetShaderPassName(i));
				if (flag)
				{
					return false;
				}
			}
			return this.m_SortingSettings.Equals(other.m_SortingSettings) && this.m_PerObjectData == other.m_PerObjectData && this.m_Flags == other.m_Flags && this.m_OverrideMaterialInstanceId == other.m_OverrideMaterialInstanceId && this.m_OverrideMaterialPassIndex == other.m_OverrideMaterialPassIndex && this.m_fallbackMaterialInstanceId == other.m_fallbackMaterialInstanceId && this.m_UseSrpBatcher == other.m_UseSrpBatcher;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00037B00 File Offset: 0x00035D00
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DrawingSettings && this.Equals((DrawingSettings)obj);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00037B38 File Offset: 0x00035D38
		public override int GetHashCode()
		{
			int hashCode = this.m_SortingSettings.GetHashCode();
			hashCode = (hashCode * 397) ^ (int)this.m_PerObjectData;
			hashCode = (hashCode * 397) ^ (int)this.m_Flags;
			hashCode = (hashCode * 397) ^ this.m_OverrideMaterialInstanceId;
			hashCode = (hashCode * 397) ^ this.m_OverrideMaterialPassIndex;
			hashCode = (hashCode * 397) ^ this.m_fallbackMaterialInstanceId;
			return (hashCode * 397) ^ this.m_UseSrpBatcher;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00037BB8 File Offset: 0x00035DB8
		public static bool operator ==(DrawingSettings left, DrawingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x04000BED RID: 3053
		public static readonly int maxShaderPasses = 16;

		// Token: 0x04000BEE RID: 3054
		private SortingSettings m_SortingSettings;

		// Token: 0x04000BEF RID: 3055
		[FixedBuffer(typeof(int), 16)]
		internal DrawingSettings.<shaderPassNames>e__FixedBuffer shaderPassNames;

		// Token: 0x04000BF0 RID: 3056
		private PerObjectData m_PerObjectData;

		// Token: 0x04000BF1 RID: 3057
		private DrawRendererFlags m_Flags;

		// Token: 0x04000BF2 RID: 3058
		private int m_OverrideShaderID;

		// Token: 0x04000BF3 RID: 3059
		private int m_OverrideShaderPassIndex;

		// Token: 0x04000BF4 RID: 3060
		private int m_OverrideMaterialInstanceId;

		// Token: 0x04000BF5 RID: 3061
		private int m_OverrideMaterialPassIndex;

		// Token: 0x04000BF6 RID: 3062
		private int m_fallbackMaterialInstanceId;

		// Token: 0x04000BF7 RID: 3063
		private int m_MainLightIndex;

		// Token: 0x04000BF8 RID: 3064
		private int m_UseSrpBatcher;

		// Token: 0x020003AE RID: 942
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <shaderPassNames>e__FixedBuffer
		{
			// Token: 0x04000BF9 RID: 3065
			public int FixedElementField;
		}
	}
}
