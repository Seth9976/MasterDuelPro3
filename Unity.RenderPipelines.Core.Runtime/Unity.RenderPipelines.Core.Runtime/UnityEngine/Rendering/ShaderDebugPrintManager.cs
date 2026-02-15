using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DC RID: 220
	public sealed class ShaderDebugPrintManager
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x0001085C File Offset: 0x0000EA5C
		private int DebugValueTypeToElemSize(ShaderDebugPrintManager.DebugValueType type)
		{
			switch (type)
			{
			case ShaderDebugPrintManager.DebugValueType.TypeUint:
			case ShaderDebugPrintManager.DebugValueType.TypeInt:
			case ShaderDebugPrintManager.DebugValueType.TypeFloat:
			case ShaderDebugPrintManager.DebugValueType.TypeBool:
				return 1;
			case ShaderDebugPrintManager.DebugValueType.TypeUint2:
			case ShaderDebugPrintManager.DebugValueType.TypeInt2:
			case ShaderDebugPrintManager.DebugValueType.TypeFloat2:
				return 2;
			case ShaderDebugPrintManager.DebugValueType.TypeUint3:
			case ShaderDebugPrintManager.DebugValueType.TypeInt3:
			case ShaderDebugPrintManager.DebugValueType.TypeFloat3:
				return 3;
			case ShaderDebugPrintManager.DebugValueType.TypeUint4:
			case ShaderDebugPrintManager.DebugValueType.TypeInt4:
			case ShaderDebugPrintManager.DebugValueType.TypeFloat4:
				return 4;
			default:
				return 0;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000108B0 File Offset: 0x0000EAB0
		private ShaderDebugPrintManager()
		{
			for (int i = 0; i < 4; i++)
			{
				this.m_OutputBuffers.Add(new GraphicsBuffer(GraphicsBuffer.Target.Structured, 16384, 4));
				this.m_ReadbackRequests.Add(default(AsyncGPUReadbackRequest));
			}
			this.m_BufferReadCompleteAction = new Action<AsyncGPUReadbackRequest>(this.BufferReadComplete);
			this.m_OutputAction = new Action<string>(this.DefaultOutput);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00010940 File Offset: 0x0000EB40
		public static ShaderDebugPrintManager instance
		{
			get
			{
				return ShaderDebugPrintManager.s_Instance;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00010948 File Offset: 0x0000EB48
		public void SetShaderDebugPrintInputConstants(CommandBuffer cmd, ShaderDebugPrintInput input)
		{
			Vector4 mouse = new Vector4(input.pos.x, input.pos.y, (float)(input.leftDown ? 1 : 0), (float)(input.rightDown ? 1 : 0));
			cmd.SetGlobalVector(ShaderDebugPrintManager.m_ShaderPropertyIDInputMouse, mouse);
			cmd.SetGlobalInt(ShaderDebugPrintManager.m_ShaderPropertyIDInputFrame, this.m_FrameCounter);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000109B0 File Offset: 0x0000EBB0
		public void SetShaderDebugPrintBindings(CommandBuffer cmd)
		{
			int index = this.m_FrameCounter % 4;
			if (!this.m_ReadbackRequests[index].done)
			{
				this.m_ReadbackRequests[index].WaitForCompletion();
			}
			cmd.SetGlobalBuffer(ShaderDebugPrintManager.m_shaderDebugOutputData, this.m_OutputBuffers[index]);
			this.ClearShaderDebugPrintBuffer();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00010A10 File Offset: 0x0000EC10
		private void ClearShaderDebugPrintBuffer()
		{
			if (!this.m_FrameCleared)
			{
				int index = this.m_FrameCounter % 4;
				NativeArray<uint> data = new NativeArray<uint>(1, Allocator.Temp, NativeArrayOptions.ClearMemory);
				data[0] = 0U;
				this.m_OutputBuffers[index].SetData<uint>(data, 0, 0, 1);
				this.m_FrameCleared = true;
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00010A60 File Offset: 0x0000EC60
		private unsafe void BufferReadComplete(AsyncGPUReadbackRequest request)
		{
			using (new ProfilingScope(ShaderDebugPrintManager.Profiling.BufferReadComplete))
			{
				if (!request.hasError)
				{
					NativeArray<uint> data = request.GetData<uint>(0);
					uint count = data[0];
					if (count >= 16384U)
					{
						count = 16384U;
						Debug.LogWarning("Debug Shader Print Buffer Full!");
					}
					string newOutputLine = "";
					if (count > 0U)
					{
						newOutputLine = newOutputLine + "Frame #" + this.m_FrameCounter.ToString() + ": ";
					}
					uint* ptr = (uint*)data.GetUnsafePtr<uint>();
					int i = 1;
					while ((long)i < (long)((ulong)count))
					{
						ShaderDebugPrintManager.DebugValueType type = (ShaderDebugPrintManager.DebugValueType)(data[i] & 15U);
						if ((data[i] & 128U) == 128U && (long)(i + 1) < (long)((ulong)count))
						{
							uint tagEncoded = data[i + 1];
							i++;
							for (int j = 0; j < 4; j++)
							{
								char c = (char)(tagEncoded & 255U);
								if (c != '\0')
								{
									newOutputLine += c.ToString();
									tagEncoded >>= 8;
								}
							}
							newOutputLine += " ";
						}
						int elemSize = this.DebugValueTypeToElemSize(type);
						if ((long)(i + elemSize) > (long)((ulong)count))
						{
							break;
						}
						i++;
						switch (type)
						{
						case ShaderDebugPrintManager.DebugValueType.TypeUint:
							newOutputLine += string.Format("{0}u", data[i]);
							break;
						case ShaderDebugPrintManager.DebugValueType.TypeInt:
						{
							int valueInt = (int)ptr[i];
							newOutputLine += valueInt.ToString();
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeFloat:
						{
							float valueFloat = *(float*)(ptr + i);
							newOutputLine += string.Format("{0}f", valueFloat);
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeUint2:
						{
							uint* valueUint2 = ptr + i;
							newOutputLine += string.Format("uint2({0}, {1})", *valueUint2, valueUint2[1]);
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeInt2:
						{
							int* valueInt2 = (int*)(ptr + i);
							newOutputLine += string.Format("int2({0}, {1})", *valueInt2, valueInt2[1]);
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeFloat2:
						{
							float* valueFloat2 = (float*)(ptr + i);
							newOutputLine += string.Format("float2({0}, {1})", *valueFloat2, valueFloat2[1]);
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeUint3:
						{
							uint* valueUint3 = ptr + i;
							newOutputLine += string.Format("uint3({0}, {1}, {2})", *valueUint3, valueUint3[1], valueUint3[2]);
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeInt3:
						{
							int* valueInt3 = (int*)(ptr + i);
							newOutputLine += string.Format("int3({0}, {1}, {2})", *valueInt3, valueInt3[1], valueInt3[2]);
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeFloat3:
						{
							float* valueFloat3 = (float*)(ptr + i);
							newOutputLine += string.Format("float3({0}, {1}, {2})", *valueFloat3, valueFloat3[1], valueFloat3[2]);
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeUint4:
						{
							uint* valueUint4 = ptr + i;
							newOutputLine += string.Format("uint4({0}, {1}, {2}, {3})", new object[]
							{
								*valueUint4,
								valueUint4[1],
								valueUint4[2],
								valueUint4[3]
							});
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeInt4:
						{
							int* valueInt4 = (int*)(ptr + i);
							newOutputLine += string.Format("int4({0}, {1}, {2}, {3})", new object[]
							{
								*valueInt4,
								valueInt4[1],
								valueInt4[2],
								valueInt4[3]
							});
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeFloat4:
						{
							float* valueFloat4 = (float*)(ptr + i);
							newOutputLine += string.Format("float4({0}, {1}, {2}, {3})", new object[]
							{
								*valueFloat4,
								valueFloat4[1],
								valueFloat4[2],
								valueFloat4[3]
							});
							break;
						}
						case ShaderDebugPrintManager.DebugValueType.TypeBool:
							newOutputLine += ((data[i] == 0U) ? "False" : "True");
							break;
						default:
							i = (int)count;
							break;
						}
						i += elemSize;
						newOutputLine += " ";
					}
					if (count > 0U)
					{
						this.m_OutputLine = newOutputLine;
						this.m_OutputAction(newOutputLine);
					}
				}
				else
				{
					this.m_OutputLine = "Error at read back!";
					this.m_OutputAction("Error at read back!");
				}
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00010F44 File Offset: 0x0000F144
		public void EndFrame()
		{
			int index = this.m_FrameCounter % 4;
			this.m_ReadbackRequests[index] = AsyncGPUReadback.Request(this.m_OutputBuffers[index], this.m_BufferReadCompleteAction);
			this.m_FrameCounter++;
			this.m_FrameCleared = false;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00010F94 File Offset: 0x0000F194
		public void PrintImmediate()
		{
			int index = this.m_FrameCounter % 4;
			AsyncGPUReadbackRequest request = AsyncGPUReadback.Request(this.m_OutputBuffers[index], null);
			request.WaitForCompletion();
			this.m_BufferReadCompleteAction(request);
			this.m_FrameCounter++;
			this.m_FrameCleared = false;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00010FE5 File Offset: 0x0000F1E5
		public string outputLine
		{
			get
			{
				return this.m_OutputLine;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00010FED File Offset: 0x0000F1ED
		public Action<string> outputAction
		{
			set
			{
				this.m_OutputAction = value;
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00010FF6 File Offset: 0x0000F1F6
		public void DefaultOutput(string line)
		{
			Debug.Log(line);
		}

		// Token: 0x0400029D RID: 669
		private static readonly ShaderDebugPrintManager s_Instance = new ShaderDebugPrintManager();

		// Token: 0x0400029E RID: 670
		private const int k_FramesInFlight = 4;

		// Token: 0x0400029F RID: 671
		private const int k_MaxBufferElements = 16384;

		// Token: 0x040002A0 RID: 672
		private List<GraphicsBuffer> m_OutputBuffers = new List<GraphicsBuffer>();

		// Token: 0x040002A1 RID: 673
		private List<AsyncGPUReadbackRequest> m_ReadbackRequests = new List<AsyncGPUReadbackRequest>();

		// Token: 0x040002A2 RID: 674
		private Action<AsyncGPUReadbackRequest> m_BufferReadCompleteAction;

		// Token: 0x040002A3 RID: 675
		private int m_FrameCounter;

		// Token: 0x040002A4 RID: 676
		private bool m_FrameCleared;

		// Token: 0x040002A5 RID: 677
		private string m_OutputLine = "";

		// Token: 0x040002A6 RID: 678
		private Action<string> m_OutputAction;

		// Token: 0x040002A7 RID: 679
		private static readonly int m_ShaderPropertyIDInputMouse = Shader.PropertyToID("_ShaderDebugPrintInputMouse");

		// Token: 0x040002A8 RID: 680
		private static readonly int m_ShaderPropertyIDInputFrame = Shader.PropertyToID("_ShaderDebugPrintInputFrame");

		// Token: 0x040002A9 RID: 681
		private static readonly int m_shaderDebugOutputData = Shader.PropertyToID("shaderDebugOutputData");

		// Token: 0x040002AA RID: 682
		private const uint k_TypeHasTag = 128U;

		// Token: 0x020000DD RID: 221
		private static class Profiling
		{
			// Token: 0x040002AB RID: 683
			public static readonly ProfilingSampler BufferReadComplete = new ProfilingSampler("ShaderDebugPrintManager.BufferReadComplete");
		}

		// Token: 0x020000DE RID: 222
		private enum DebugValueType
		{
			// Token: 0x040002AD RID: 685
			TypeUint = 1,
			// Token: 0x040002AE RID: 686
			TypeInt,
			// Token: 0x040002AF RID: 687
			TypeFloat,
			// Token: 0x040002B0 RID: 688
			TypeUint2,
			// Token: 0x040002B1 RID: 689
			TypeInt2,
			// Token: 0x040002B2 RID: 690
			TypeFloat2,
			// Token: 0x040002B3 RID: 691
			TypeUint3,
			// Token: 0x040002B4 RID: 692
			TypeInt3,
			// Token: 0x040002B5 RID: 693
			TypeFloat3,
			// Token: 0x040002B6 RID: 694
			TypeUint4,
			// Token: 0x040002B7 RID: 695
			TypeInt4,
			// Token: 0x040002B8 RID: 696
			TypeFloat4,
			// Token: 0x040002B9 RID: 697
			TypeBool
		}
	}
}
