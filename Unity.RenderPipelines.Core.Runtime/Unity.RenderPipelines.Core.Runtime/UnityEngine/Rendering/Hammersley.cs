using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000185 RID: 389
	public static class Hammersley
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x0002752C File Offset: 0x0002572C
		public unsafe static void Initialize()
		{
			Hammersley.Hammersley2dSeq16 hammersley2DSeq16 = default(Hammersley.Hammersley2dSeq16);
			Hammersley.Hammersley2dSeq32 hammersley2DSeq17 = default(Hammersley.Hammersley2dSeq32);
			Hammersley.Hammersley2dSeq64 hammersley2DSeq18 = default(Hammersley.Hammersley2dSeq64);
			Hammersley.Hammersley2dSeq256 hammersley2DSeq19 = default(Hammersley.Hammersley2dSeq256);
			for (int i = 0; i < Hammersley.k_Hammersley2dSeq16.Length; i++)
			{
				*((ref hammersley2DSeq16.hammersley2dSeq16.FixedElementField) + (IntPtr)i * 4) = Hammersley.k_Hammersley2dSeq16[i];
			}
			for (int j = 0; j < Hammersley.k_Hammersley2dSeq32.Length; j++)
			{
				*((ref hammersley2DSeq17.hammersley2dSeq32.FixedElementField) + (IntPtr)j * 4) = Hammersley.k_Hammersley2dSeq32[j];
			}
			for (int k = 0; k < Hammersley.k_Hammersley2dSeq64.Length; k++)
			{
				*((ref hammersley2DSeq18.hammersley2dSeq64.FixedElementField) + (IntPtr)k * 4) = Hammersley.k_Hammersley2dSeq64[k];
			}
			for (int l = 0; l < Hammersley.k_Hammersley2dSeq256.Length; l++)
			{
				*((ref hammersley2DSeq19.hammersley2dSeq256.FixedElementField) + (IntPtr)l * 4) = Hammersley.k_Hammersley2dSeq256[l];
			}
			ConstantBuffer.UpdateData<Hammersley.Hammersley2dSeq16>(in hammersley2DSeq16);
			ConstantBuffer.UpdateData<Hammersley.Hammersley2dSeq32>(in hammersley2DSeq17);
			ConstantBuffer.UpdateData<Hammersley.Hammersley2dSeq64>(in hammersley2DSeq18);
			ConstantBuffer.UpdateData<Hammersley.Hammersley2dSeq256>(in hammersley2DSeq19);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00027639 File Offset: 0x00025839
		public static void BindConstants(CommandBuffer cmd, ComputeShader cs)
		{
			ConstantBuffer.Set<Hammersley.Hammersley2dSeq16>(cmd, cs, Hammersley.s_hammersley2DSeq16Id);
			ConstantBuffer.Set<Hammersley.Hammersley2dSeq32>(cmd, cs, Hammersley.s_hammersley2DSeq32Id);
			ConstantBuffer.Set<Hammersley.Hammersley2dSeq64>(cmd, cs, Hammersley.s_hammersley2DSeq64Id);
			ConstantBuffer.Set<Hammersley.Hammersley2dSeq256>(cmd, cs, Hammersley.s_hammersley2DSeq256Id);
		}

		// Token: 0x0400078C RID: 1932
		private static float[] k_Hammersley2dSeq16 = new float[]
		{
			0f, 0f, 0f, 0f, 0.0625f, 0.5f, 0f, 0f, 0.125f, 0.25f,
			0f, 0f, 0.1875f, 0.75f, 0f, 0f, 0.25f, 0.125f, 0f, 0f,
			0.3125f, 0.625f, 0f, 0f, 0.375f, 0.375f, 0f, 0f, 0.4375f, 0.875f,
			0f, 0f, 0.5f, 0.0625f, 0f, 0f, 0.5625f, 0.5625f, 0f, 0f,
			0.625f, 0.3125f, 0f, 0f, 0.6875f, 0.8125f, 0f, 0f, 0.75f, 0.1875f,
			0f, 0f, 0.8125f, 0.6875f, 0f, 0f, 0.875f, 0.4375f, 0f, 0f,
			0.9375f, 0.9375f, 0f, 0f
		};

		// Token: 0x0400078D RID: 1933
		private static float[] k_Hammersley2dSeq32 = new float[]
		{
			0f, 0f, 0f, 0f, 0.03125f, 0.5f, 0f, 0f, 0.0625f, 0.25f,
			0f, 0f, 0.09375f, 0.75f, 0f, 0f, 0.125f, 0.125f, 0f, 0f,
			0.15625f, 0.625f, 0f, 0f, 0.1875f, 0.375f, 0f, 0f, 0.21875f, 0.875f,
			0f, 0f, 0.25f, 0.0625f, 0f, 0f, 0.28125f, 0.5625f, 0f, 0f,
			0.3125f, 0.3125f, 0f, 0f, 0.34375f, 0.8125f, 0f, 0f, 0.375f, 0.1875f,
			0f, 0f, 0.40625f, 0.6875f, 0f, 0f, 0.4375f, 0.4375f, 0f, 0f,
			0.46875f, 0.9375f, 0f, 0f, 0.5f, 0.03125f, 0f, 0f, 0.53125f, 0.53125f,
			0f, 0f, 0.5625f, 0.28125f, 0f, 0f, 0.59375f, 0.78125f, 0f, 0f,
			0.625f, 0.15625f, 0f, 0f, 0.65625f, 0.65625f, 0f, 0f, 0.6875f, 0.40625f,
			0f, 0f, 0.71875f, 0.90625f, 0f, 0f, 0.75f, 0.09375f, 0f, 0f,
			0.78125f, 0.59375f, 0f, 0f, 0.8125f, 0.34375f, 0f, 0f, 0.84375f, 0.84375f,
			0f, 0f, 0.875f, 0.21875f, 0f, 0f, 0.90625f, 0.71875f, 0f, 0f,
			0.9375f, 0.46875f, 0f, 0f, 0.96875f, 0.96875f, 0f, 0f
		};

		// Token: 0x0400078E RID: 1934
		private static float[] k_Hammersley2dSeq64 = new float[]
		{
			0f, 0f, 0f, 0f, 0.015625f, 0.5f, 0f, 0f, 0.03125f, 0.25f,
			0f, 0f, 0.046875f, 0.75f, 0f, 0f, 0.0625f, 0.125f, 0f, 0f,
			0.078125f, 0.625f, 0f, 0f, 0.09375f, 0.375f, 0f, 0f, 0.109375f, 0.875f,
			0f, 0f, 0.125f, 0.0625f, 0f, 0f, 0.140625f, 0.5625f, 0f, 0f,
			0.15625f, 0.3125f, 0f, 0f, 0.171875f, 0.8125f, 0f, 0f, 0.1875f, 0.1875f,
			0f, 0f, 0.203125f, 0.6875f, 0f, 0f, 0.21875f, 0.4375f, 0f, 0f,
			0.234375f, 0.9375f, 0f, 0f, 0.25f, 0.03125f, 0f, 0f, 0.265625f, 0.53125f,
			0f, 0f, 0.28125f, 0.28125f, 0f, 0f, 0.296875f, 0.78125f, 0f, 0f,
			0.3125f, 0.15625f, 0f, 0f, 0.328125f, 0.65625f, 0f, 0f, 0.34375f, 0.40625f,
			0f, 0f, 0.359375f, 0.90625f, 0f, 0f, 0.375f, 0.09375f, 0f, 0f,
			0.390625f, 0.59375f, 0f, 0f, 0.40625f, 0.34375f, 0f, 0f, 0.421875f, 0.84375f,
			0f, 0f, 0.4375f, 0.21875f, 0f, 0f, 0.453125f, 0.71875f, 0f, 0f,
			0.46875f, 0.46875f, 0f, 0f, 0.484375f, 0.96875f, 0f, 0f, 0.5f, 0.015625f,
			0f, 0f, 0.515625f, 0.515625f, 0f, 0f, 0.53125f, 0.265625f, 0f, 0f,
			0.546875f, 0.765625f, 0f, 0f, 0.5625f, 0.140625f, 0f, 0f, 0.578125f, 0.640625f,
			0f, 0f, 0.59375f, 0.390625f, 0f, 0f, 0.609375f, 0.890625f, 0f, 0f,
			0.625f, 0.078125f, 0f, 0f, 0.640625f, 0.578125f, 0f, 0f, 0.65625f, 0.328125f,
			0f, 0f, 0.671875f, 0.828125f, 0f, 0f, 0.6875f, 0.203125f, 0f, 0f,
			0.703125f, 0.703125f, 0f, 0f, 0.71875f, 0.453125f, 0f, 0f, 0.734375f, 0.953125f,
			0f, 0f, 0.75f, 0.046875f, 0f, 0f, 0.765625f, 0.546875f, 0f, 0f,
			0.78125f, 0.296875f, 0f, 0f, 0.796875f, 0.796875f, 0f, 0f, 0.8125f, 0.171875f,
			0f, 0f, 0.828125f, 0.671875f, 0f, 0f, 0.84375f, 0.421875f, 0f, 0f,
			0.859375f, 0.921875f, 0f, 0f, 0.875f, 0.109375f, 0f, 0f, 0.890625f, 0.609375f,
			0f, 0f, 0.90625f, 0.359375f, 0f, 0f, 0.921875f, 0.859375f, 0f, 0f,
			0.9375f, 0.234375f, 0f, 0f, 0.953125f, 0.734375f, 0f, 0f, 0.96875f, 0.484375f,
			0f, 0f, 0.984375f, 0.984375f, 0f, 0f
		};

		// Token: 0x0400078F RID: 1935
		private static float[] k_Hammersley2dSeq256 = new float[]
		{
			0f, 0f, 0f, 0f, 0.00390625f, 0.5f, 0f, 0f, 0.0078125f, 0.25f,
			0f, 0f, 0.01171875f, 0.75f, 0f, 0f, 0.015625f, 0.125f, 0f, 0f,
			0.01953125f, 0.625f, 0f, 0f, 0.0234375f, 0.375f, 0f, 0f, 0.02734375f, 0.875f,
			0f, 0f, 0.03125f, 0.0625f, 0f, 0f, 0.03515625f, 0.5625f, 0f, 0f,
			0.0390625f, 0.3125f, 0f, 0f, 0.04296875f, 0.8125f, 0f, 0f, 0.046875f, 0.1875f,
			0f, 0f, 0.05078125f, 0.6875f, 0f, 0f, 0.0546875f, 0.4375f, 0f, 0f,
			0.05859375f, 0.9375f, 0f, 0f, 0.0625f, 0.03125f, 0f, 0f, 0.06640625f, 0.53125f,
			0f, 0f, 0.0703125f, 0.28125f, 0f, 0f, 0.07421875f, 0.78125f, 0f, 0f,
			0.078125f, 0.15625f, 0f, 0f, 0.08203125f, 0.65625f, 0f, 0f, 0.0859375f, 0.40625f,
			0f, 0f, 0.08984375f, 0.90625f, 0f, 0f, 0.09375f, 0.09375f, 0f, 0f,
			0.09765625f, 0.59375f, 0f, 0f, 0.1015625f, 0.34375f, 0f, 0f, 0.10546875f, 0.84375f,
			0f, 0f, 0.109375f, 0.21875f, 0f, 0f, 0.11328125f, 0.71875f, 0f, 0f,
			0.1171875f, 0.46875f, 0f, 0f, 0.12109375f, 0.96875f, 0f, 0f, 0.125f, 0.015625f,
			0f, 0f, 0.12890625f, 0.515625f, 0f, 0f, 0.1328125f, 0.265625f, 0f, 0f,
			0.13671875f, 0.765625f, 0f, 0f, 0.140625f, 0.140625f, 0f, 0f, 0.14453125f, 0.640625f,
			0f, 0f, 0.1484375f, 0.390625f, 0f, 0f, 0.15234375f, 0.890625f, 0f, 0f,
			0.15625f, 0.078125f, 0f, 0f, 0.16015625f, 0.578125f, 0f, 0f, 0.1640625f, 0.328125f,
			0f, 0f, 0.16796875f, 0.828125f, 0f, 0f, 0.171875f, 0.203125f, 0f, 0f,
			0.17578125f, 0.703125f, 0f, 0f, 0.1796875f, 0.453125f, 0f, 0f, 0.18359375f, 0.953125f,
			0f, 0f, 0.1875f, 0.046875f, 0f, 0f, 0.19140625f, 0.546875f, 0f, 0f,
			0.1953125f, 0.296875f, 0f, 0f, 0.19921875f, 0.796875f, 0f, 0f, 0.203125f, 0.171875f,
			0f, 0f, 0.20703125f, 0.671875f, 0f, 0f, 0.2109375f, 0.421875f, 0f, 0f,
			0.21484375f, 0.921875f, 0f, 0f, 0.21875f, 0.109375f, 0f, 0f, 0.22265625f, 0.609375f,
			0f, 0f, 0.2265625f, 0.359375f, 0f, 0f, 0.23046875f, 0.859375f, 0f, 0f,
			0.234375f, 0.234375f, 0f, 0f, 0.23828125f, 0.734375f, 0f, 0f, 0.2421875f, 0.484375f,
			0f, 0f, 0.24609375f, 0.984375f, 0f, 0f, 0.25f, 0.0078125f, 0f, 0f,
			0.25390625f, 0.5078125f, 0f, 0f, 0.2578125f, 0.2578125f, 0f, 0f, 0.26171875f, 0.7578125f,
			0f, 0f, 0.265625f, 0.1328125f, 0f, 0f, 0.26953125f, 0.6328125f, 0f, 0f,
			0.2734375f, 0.3828125f, 0f, 0f, 0.27734375f, 0.8828125f, 0f, 0f, 0.28125f, 0.0703125f,
			0f, 0f, 0.28515625f, 0.5703125f, 0f, 0f, 0.2890625f, 0.3203125f, 0f, 0f,
			0.29296875f, 0.8203125f, 0f, 0f, 0.296875f, 0.1953125f, 0f, 0f, 0.30078125f, 0.6953125f,
			0f, 0f, 0.3046875f, 0.4453125f, 0f, 0f, 0.30859375f, 0.9453125f, 0f, 0f,
			0.3125f, 0.0390625f, 0f, 0f, 0.31640625f, 0.5390625f, 0f, 0f, 0.3203125f, 0.2890625f,
			0f, 0f, 0.32421875f, 0.7890625f, 0f, 0f, 0.328125f, 0.1640625f, 0f, 0f,
			0.33203125f, 0.6640625f, 0f, 0f, 0.3359375f, 0.4140625f, 0f, 0f, 0.33984375f, 0.9140625f,
			0f, 0f, 0.34375f, 0.1015625f, 0f, 0f, 0.34765625f, 0.6015625f, 0f, 0f,
			0.3515625f, 0.3515625f, 0f, 0f, 0.35546875f, 0.8515625f, 0f, 0f, 0.359375f, 0.2265625f,
			0f, 0f, 0.36328125f, 0.7265625f, 0f, 0f, 0.3671875f, 0.4765625f, 0f, 0f,
			0.37109375f, 0.9765625f, 0f, 0f, 0.375f, 0.0234375f, 0f, 0f, 0.37890625f, 0.5234375f,
			0f, 0f, 0.3828125f, 0.2734375f, 0f, 0f, 0.38671875f, 0.7734375f, 0f, 0f,
			0.390625f, 0.1484375f, 0f, 0f, 0.39453125f, 0.6484375f, 0f, 0f, 0.3984375f, 0.3984375f,
			0f, 0f, 0.40234375f, 0.8984375f, 0f, 0f, 0.40625f, 0.0859375f, 0f, 0f,
			0.41015625f, 0.5859375f, 0f, 0f, 0.4140625f, 0.3359375f, 0f, 0f, 0.41796875f, 0.8359375f,
			0f, 0f, 0.421875f, 0.2109375f, 0f, 0f, 0.42578125f, 0.7109375f, 0f, 0f,
			0.4296875f, 0.4609375f, 0f, 0f, 0.43359375f, 0.9609375f, 0f, 0f, 0.4375f, 0.0546875f,
			0f, 0f, 0.44140625f, 0.5546875f, 0f, 0f, 0.4453125f, 0.3046875f, 0f, 0f,
			0.44921875f, 0.8046875f, 0f, 0f, 0.453125f, 0.1796875f, 0f, 0f, 0.45703125f, 0.6796875f,
			0f, 0f, 0.4609375f, 0.4296875f, 0f, 0f, 0.46484375f, 0.9296875f, 0f, 0f,
			0.46875f, 0.1171875f, 0f, 0f, 0.47265625f, 0.6171875f, 0f, 0f, 0.4765625f, 0.3671875f,
			0f, 0f, 0.48046875f, 0.8671875f, 0f, 0f, 0.484375f, 0.2421875f, 0f, 0f,
			0.48828125f, 0.7421875f, 0f, 0f, 0.4921875f, 0.4921875f, 0f, 0f, 0.49609375f, 0.9921875f,
			0f, 0f, 0.5f, 0.00390625f, 0f, 0f, 0.50390625f, 0.50390625f, 0f, 0f,
			0.5078125f, 0.25390625f, 0f, 0f, 0.51171875f, 0.75390625f, 0f, 0f, 0.515625f, 0.12890625f,
			0f, 0f, 0.51953125f, 0.62890625f, 0f, 0f, 0.5234375f, 0.37890625f, 0f, 0f,
			0.52734375f, 0.87890625f, 0f, 0f, 0.53125f, 0.06640625f, 0f, 0f, 0.53515625f, 0.56640625f,
			0f, 0f, 0.5390625f, 0.31640625f, 0f, 0f, 0.54296875f, 0.81640625f, 0f, 0f,
			0.546875f, 0.19140625f, 0f, 0f, 0.55078125f, 0.69140625f, 0f, 0f, 0.5546875f, 0.44140625f,
			0f, 0f, 0.55859375f, 0.94140625f, 0f, 0f, 0.5625f, 0.03515625f, 0f, 0f,
			0.56640625f, 0.53515625f, 0f, 0f, 0.5703125f, 0.28515625f, 0f, 0f, 0.57421875f, 0.78515625f,
			0f, 0f, 0.578125f, 0.16015625f, 0f, 0f, 0.58203125f, 0.66015625f, 0f, 0f,
			0.5859375f, 0.41015625f, 0f, 0f, 0.58984375f, 0.91015625f, 0f, 0f, 0.59375f, 0.09765625f,
			0f, 0f, 0.59765625f, 0.59765625f, 0f, 0f, 0.6015625f, 0.34765625f, 0f, 0f,
			0.60546875f, 0.84765625f, 0f, 0f, 0.609375f, 0.22265625f, 0f, 0f, 0.61328125f, 0.72265625f,
			0f, 0f, 0.6171875f, 0.47265625f, 0f, 0f, 0.62109375f, 0.97265625f, 0f, 0f,
			0.625f, 0.01953125f, 0f, 0f, 0.62890625f, 0.51953125f, 0f, 0f, 0.6328125f, 0.26953125f,
			0f, 0f, 0.63671875f, 0.76953125f, 0f, 0f, 0.640625f, 0.14453125f, 0f, 0f,
			0.64453125f, 0.64453125f, 0f, 0f, 0.6484375f, 0.39453125f, 0f, 0f, 0.65234375f, 0.89453125f,
			0f, 0f, 0.65625f, 0.08203125f, 0f, 0f, 0.66015625f, 0.58203125f, 0f, 0f,
			0.6640625f, 0.33203125f, 0f, 0f, 0.66796875f, 0.83203125f, 0f, 0f, 0.671875f, 0.20703125f,
			0f, 0f, 0.67578125f, 0.70703125f, 0f, 0f, 0.6796875f, 0.45703125f, 0f, 0f,
			0.68359375f, 0.95703125f, 0f, 0f, 0.6875f, 0.05078125f, 0f, 0f, 0.69140625f, 0.55078125f,
			0f, 0f, 0.6953125f, 0.30078125f, 0f, 0f, 0.69921875f, 0.80078125f, 0f, 0f,
			0.703125f, 0.17578125f, 0f, 0f, 0.70703125f, 0.67578125f, 0f, 0f, 0.7109375f, 0.42578125f,
			0f, 0f, 0.71484375f, 0.92578125f, 0f, 0f, 0.71875f, 0.11328125f, 0f, 0f,
			0.72265625f, 0.61328125f, 0f, 0f, 0.7265625f, 0.36328125f, 0f, 0f, 0.73046875f, 0.86328125f,
			0f, 0f, 0.734375f, 0.23828125f, 0f, 0f, 0.73828125f, 0.73828125f, 0f, 0f,
			0.7421875f, 0.48828125f, 0f, 0f, 0.74609375f, 0.98828125f, 0f, 0f, 0.75f, 0.01171875f,
			0f, 0f, 0.75390625f, 0.51171875f, 0f, 0f, 0.7578125f, 0.26171875f, 0f, 0f,
			0.76171875f, 0.76171875f, 0f, 0f, 0.765625f, 0.13671875f, 0f, 0f, 0.76953125f, 0.63671875f,
			0f, 0f, 0.7734375f, 0.38671875f, 0f, 0f, 0.77734375f, 0.88671875f, 0f, 0f,
			0.78125f, 0.07421875f, 0f, 0f, 0.78515625f, 0.57421875f, 0f, 0f, 0.7890625f, 0.32421875f,
			0f, 0f, 0.79296875f, 0.82421875f, 0f, 0f, 0.796875f, 0.19921875f, 0f, 0f,
			0.80078125f, 0.69921875f, 0f, 0f, 0.8046875f, 0.44921875f, 0f, 0f, 0.80859375f, 0.94921875f,
			0f, 0f, 0.8125f, 0.04296875f, 0f, 0f, 0.81640625f, 0.54296875f, 0f, 0f,
			0.8203125f, 0.29296875f, 0f, 0f, 0.82421875f, 0.79296875f, 0f, 0f, 0.828125f, 0.16796875f,
			0f, 0f, 0.83203125f, 0.66796875f, 0f, 0f, 0.8359375f, 0.41796875f, 0f, 0f,
			0.83984375f, 0.91796875f, 0f, 0f, 0.84375f, 0.10546875f, 0f, 0f, 0.84765625f, 0.60546875f,
			0f, 0f, 0.8515625f, 0.35546875f, 0f, 0f, 0.85546875f, 0.85546875f, 0f, 0f,
			0.859375f, 0.23046875f, 0f, 0f, 0.86328125f, 0.73046875f, 0f, 0f, 0.8671875f, 0.48046875f,
			0f, 0f, 0.87109375f, 0.98046875f, 0f, 0f, 0.875f, 0.02734375f, 0f, 0f,
			0.87890625f, 0.52734375f, 0f, 0f, 0.8828125f, 0.27734375f, 0f, 0f, 0.88671875f, 0.77734375f,
			0f, 0f, 0.890625f, 0.15234375f, 0f, 0f, 0.89453125f, 0.65234375f, 0f, 0f,
			0.8984375f, 0.40234375f, 0f, 0f, 0.90234375f, 0.90234375f, 0f, 0f, 0.90625f, 0.08984375f,
			0f, 0f, 0.91015625f, 0.58984375f, 0f, 0f, 0.9140625f, 0.33984375f, 0f, 0f,
			0.91796875f, 0.83984375f, 0f, 0f, 0.921875f, 0.21484375f, 0f, 0f, 0.92578125f, 0.71484375f,
			0f, 0f, 0.9296875f, 0.46484375f, 0f, 0f, 0.93359375f, 0.96484375f, 0f, 0f,
			0.9375f, 0.05859375f, 0f, 0f, 0.94140625f, 0.55859375f, 0f, 0f, 0.9453125f, 0.30859375f,
			0f, 0f, 0.94921875f, 0.80859375f, 0f, 0f, 0.953125f, 0.18359375f, 0f, 0f,
			0.95703125f, 0.68359375f, 0f, 0f, 0.9609375f, 0.43359375f, 0f, 0f, 0.96484375f, 0.93359375f,
			0f, 0f, 0.96875f, 0.12109375f, 0f, 0f, 0.97265625f, 0.62109375f, 0f, 0f,
			0.9765625f, 0.37109375f, 0f, 0f, 0.98046875f, 0.87109375f, 0f, 0f, 0.984375f, 0.24609375f,
			0f, 0f, 0.98828125f, 0.74609375f, 0f, 0f, 0.9921875f, 0.49609375f, 0f, 0f,
			0.99609375f, 0.99609375f, 0f, 0f
		};

		// Token: 0x04000790 RID: 1936
		private static readonly int s_hammersley2DSeq16Id = Shader.PropertyToID("Hammersley2dSeq16");

		// Token: 0x04000791 RID: 1937
		private static readonly int s_hammersley2DSeq32Id = Shader.PropertyToID("Hammersley2dSeq32");

		// Token: 0x04000792 RID: 1938
		private static readonly int s_hammersley2DSeq64Id = Shader.PropertyToID("Hammersley2dSeq64");

		// Token: 0x04000793 RID: 1939
		private static readonly int s_hammersley2DSeq256Id = Shader.PropertyToID("Hammersley2dSeq256");

		// Token: 0x02000186 RID: 390
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/ShaderLibrary/Sampling/Hammersley.cs", needAccessors = false, generateCBuffer = true)]
		private struct Hammersley2dSeq16
		{
			// Token: 0x04000794 RID: 1940
			[FixedBuffer(typeof(float), 64)]
			[HLSLArray(16, typeof(Vector4))]
			public Hammersley.Hammersley2dSeq16.<hammersley2dSeq16>e__FixedBuffer hammersley2dSeq16;

			// Token: 0x02000187 RID: 391
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 256)]
			public struct <hammersley2dSeq16>e__FixedBuffer
			{
				// Token: 0x04000795 RID: 1941
				public float FixedElementField;
			}
		}

		// Token: 0x02000188 RID: 392
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/ShaderLibrary/Sampling/Hammersley.cs", needAccessors = false, generateCBuffer = true)]
		private struct Hammersley2dSeq32
		{
			// Token: 0x04000796 RID: 1942
			[FixedBuffer(typeof(float), 128)]
			[HLSLArray(32, typeof(Vector4))]
			public Hammersley.Hammersley2dSeq32.<hammersley2dSeq32>e__FixedBuffer hammersley2dSeq32;

			// Token: 0x02000189 RID: 393
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 512)]
			public struct <hammersley2dSeq32>e__FixedBuffer
			{
				// Token: 0x04000797 RID: 1943
				public float FixedElementField;
			}
		}

		// Token: 0x0200018A RID: 394
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/ShaderLibrary/Sampling/Hammersley.cs", needAccessors = false, generateCBuffer = true)]
		private struct Hammersley2dSeq64
		{
			// Token: 0x04000798 RID: 1944
			[FixedBuffer(typeof(float), 256)]
			[HLSLArray(64, typeof(Vector4))]
			public Hammersley.Hammersley2dSeq64.<hammersley2dSeq64>e__FixedBuffer hammersley2dSeq64;

			// Token: 0x0200018B RID: 395
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 1024)]
			public struct <hammersley2dSeq64>e__FixedBuffer
			{
				// Token: 0x04000799 RID: 1945
				public float FixedElementField;
			}
		}

		// Token: 0x0200018C RID: 396
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/ShaderLibrary/Sampling/Hammersley.cs", needAccessors = false, generateCBuffer = true)]
		private struct Hammersley2dSeq256
		{
			// Token: 0x0400079A RID: 1946
			[FixedBuffer(typeof(float), 1024)]
			[HLSLArray(256, typeof(Vector4))]
			public Hammersley.Hammersley2dSeq256.<hammersley2dSeq256>e__FixedBuffer hammersley2dSeq256;

			// Token: 0x0200018D RID: 397
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 4096)]
			public struct <hammersley2dSeq256>e__FixedBuffer
			{
				// Token: 0x0400079B RID: 1947
				public float FixedElementField;
			}
		}
	}
}
