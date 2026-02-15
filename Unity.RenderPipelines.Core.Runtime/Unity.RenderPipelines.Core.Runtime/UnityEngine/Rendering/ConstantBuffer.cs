using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002B RID: 43
	public class ConstantBuffer
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x00006780 File Offset: 0x00004980
		public static void PushGlobal<CBType>(CommandBuffer cmd, in CBType data, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.SetGlobal(cmd, shaderId);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00006796 File Offset: 0x00004996
		public static void PushGlobal<CBType>(in CBType data, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.SetGlobal(shaderId);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000067AA File Offset: 0x000049AA
		public static void Push<CBType>(CommandBuffer cmd, in CBType data, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.Set(cmd, cs, shaderId);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000067C1 File Offset: 0x000049C1
		public static void Push<CBType>(in CBType data, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.Set(cs, shaderId);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000067D6 File Offset: 0x000049D6
		public static void Push<CBType>(CommandBuffer cmd, in CBType data, Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.Set(mat, shaderId);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000067EC File Offset: 0x000049EC
		public static void Push<CBType>(in CBType data, Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.Set(mat, shaderId);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00006801 File Offset: 0x00004A01
		public static void UpdateData<CBType>(CommandBuffer cmd, in CBType data) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.UpdateData(cmd, in data);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000680F File Offset: 0x00004A0F
		public static void UpdateData<CBType>(in CBType data) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.UpdateData(in data);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000681C File Offset: 0x00004A1C
		public static void SetGlobal<CBType>(CommandBuffer cmd, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.SetGlobal(cmd, shaderId);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000682A File Offset: 0x00004A2A
		public static void SetGlobal<CBType>(int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.SetGlobal(shaderId);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00006837 File Offset: 0x00004A37
		public static void Set<CBType>(CommandBuffer cmd, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(cmd, cs, shaderId);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00006846 File Offset: 0x00004A46
		public static void Set<CBType>(ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(cs, shaderId);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00006854 File Offset: 0x00004A54
		public static void Set<CBType>(Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(mat, shaderId);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00006864 File Offset: 0x00004A64
		public static void ReleaseAll()
		{
			foreach (ConstantBufferBase constantBufferBase in ConstantBuffer.m_RegisteredConstantBuffers)
			{
				constantBufferBase.Release();
			}
			ConstantBuffer.m_RegisteredConstantBuffers.Clear();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000068C0 File Offset: 0x00004AC0
		internal static void Register(ConstantBufferBase cb)
		{
			ConstantBuffer.m_RegisteredConstantBuffers.Add(cb);
		}

		// Token: 0x040000A9 RID: 169
		private static List<ConstantBufferBase> m_RegisteredConstantBuffers = new List<ConstantBufferBase>();
	}
}
