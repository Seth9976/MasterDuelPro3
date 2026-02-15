using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002E RID: 46
	internal class ConstantBufferSingleton<CBType> : ConstantBuffer<CBType> where CBType : struct
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00006AA0 File Offset: 0x00004CA0
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00006AC2 File Offset: 0x00004CC2
		internal static ConstantBufferSingleton<CBType> instance
		{
			get
			{
				if (ConstantBufferSingleton<CBType>.s_Instance == null)
				{
					ConstantBufferSingleton<CBType>.s_Instance = new ConstantBufferSingleton<CBType>();
					ConstantBuffer.Register(ConstantBufferSingleton<CBType>.s_Instance);
				}
				return ConstantBufferSingleton<CBType>.s_Instance;
			}
			set
			{
				ConstantBufferSingleton<CBType>.s_Instance = value;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00006ACA File Offset: 0x00004CCA
		public override void Release()
		{
			base.Release();
			ConstantBufferSingleton<CBType>.s_Instance = null;
		}

		// Token: 0x040000AD RID: 173
		private static ConstantBufferSingleton<CBType> s_Instance;
	}
}
