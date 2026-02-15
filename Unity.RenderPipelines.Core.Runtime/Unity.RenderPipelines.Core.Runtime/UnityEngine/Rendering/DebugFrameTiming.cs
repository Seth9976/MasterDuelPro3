using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008E RID: 142
	public class DebugFrameTiming
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000BAEE File Offset: 0x00009CEE
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0000BAF6 File Offset: 0x00009CF6
		public int bottleneckHistorySize { get; set; } = 60;

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000BAFF File Offset: 0x00009CFF
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0000BB07 File Offset: 0x00009D07
		public int sampleHistorySize { get; set; } = 30;

		// Token: 0x06000586 RID: 1414 RVA: 0x0000BB10 File Offset: 0x00009D10
		public DebugFrameTiming()
		{
			this.m_FrameHistory = new FrameTimeSampleHistory(this.sampleHistorySize);
			this.m_BottleneckHistory = new BottleneckHistory(this.bottleneckHistorySize);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000BB64 File Offset: 0x00009D64
		public void UpdateFrameTiming()
		{
			this.m_Timing[0] = default(FrameTiming);
			this.m_Sample = default(FrameTimeSample);
			FrameTimingManager.CaptureFrameTimings();
			FrameTimingManager.GetLatestTimings(1U, this.m_Timing);
			if (this.m_Timing.Length != 0)
			{
				this.m_Sample.FullFrameTime = (float)this.m_Timing.First<FrameTiming>().cpuFrameTime;
				this.m_Sample.FramesPerSecond = ((this.m_Sample.FullFrameTime > 0f) ? (1000f / this.m_Sample.FullFrameTime) : 0f);
				this.m_Sample.MainThreadCPUFrameTime = (float)this.m_Timing.First<FrameTiming>().cpuMainThreadFrameTime;
				this.m_Sample.MainThreadCPUPresentWaitTime = (float)this.m_Timing.First<FrameTiming>().cpuMainThreadPresentWaitTime;
				this.m_Sample.RenderThreadCPUFrameTime = (float)this.m_Timing.First<FrameTiming>().cpuRenderThreadFrameTime;
				this.m_Sample.GPUFrameTime = (float)this.m_Timing.First<FrameTiming>().gpuFrameTime;
			}
			this.m_FrameHistory.DiscardOldSamples(this.sampleHistorySize);
			this.m_FrameHistory.Add(this.m_Sample);
			this.m_FrameHistory.ComputeAggregateValues();
			this.m_BottleneckHistory.DiscardOldSamples(this.bottleneckHistorySize);
			this.m_BottleneckHistory.AddBottleneckFromAveragedSample(this.m_FrameHistory.SampleAverage);
			this.m_BottleneckHistory.ComputeHistogram();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public void RegisterDebugUI(List<DebugUI.Widget> list)
		{
			list.Add(new DebugUI.Foldout
			{
				displayName = "Frame Stats",
				isHeader = true,
				opened = true,
				columnLabels = new string[] { "Avg", "Min", "Max" },
				children = 
				{
					new DebugUI.ValueTuple
					{
						displayName = "Frame Rate (FPS)",
						values = new DebugUI.Value[]
						{
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F1}",
								getter = () => this.m_FrameHistory.SampleAverage.FramesPerSecond
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F1}",
								getter = () => this.m_FrameHistory.SampleMin.FramesPerSecond
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F1}",
								getter = () => this.m_FrameHistory.SampleMax.FramesPerSecond
							}
						}
					},
					new DebugUI.ValueTuple
					{
						displayName = "Frame Time",
						values = new DebugUI.Value[]
						{
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleAverage.FullFrameTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMin.FullFrameTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMax.FullFrameTime
							}
						}
					},
					new DebugUI.ValueTuple
					{
						displayName = "CPU Main Thread Frame",
						values = new DebugUI.Value[]
						{
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleAverage.MainThreadCPUFrameTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMin.MainThreadCPUFrameTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMax.MainThreadCPUFrameTime
							}
						}
					},
					new DebugUI.ValueTuple
					{
						displayName = "CPU Render Thread Frame",
						values = new DebugUI.Value[]
						{
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleAverage.RenderThreadCPUFrameTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMin.RenderThreadCPUFrameTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMax.RenderThreadCPUFrameTime
							}
						}
					},
					new DebugUI.ValueTuple
					{
						displayName = "CPU Present Wait",
						values = new DebugUI.Value[]
						{
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleAverage.MainThreadCPUPresentWaitTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMin.MainThreadCPUPresentWaitTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMax.MainThreadCPUPresentWaitTime
							}
						}
					},
					new DebugUI.ValueTuple
					{
						displayName = "GPU Frame",
						values = new DebugUI.Value[]
						{
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleAverage.GPUFrameTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMin.GPUFrameTime
							},
							new DebugUI.Value
							{
								refreshRate = 0.2f,
								formatString = "{0:F2}ms",
								getter = () => this.m_FrameHistory.SampleMax.GPUFrameTime
							}
						}
					}
				}
			});
			list.Add(new DebugUI.Foldout
			{
				displayName = "Bottlenecks",
				isHeader = true,
				children = 
				{
					new DebugUI.ProgressBarValue
					{
						displayName = "CPU",
						getter = () => this.m_BottleneckHistory.Histogram.CPU
					},
					new DebugUI.ProgressBarValue
					{
						displayName = "GPU",
						getter = () => this.m_BottleneckHistory.Histogram.GPU
					},
					new DebugUI.ProgressBarValue
					{
						displayName = "Present limited",
						getter = () => this.m_BottleneckHistory.Histogram.PresentLimited
					},
					new DebugUI.ProgressBarValue
					{
						displayName = "Balanced",
						getter = () => this.m_BottleneckHistory.Histogram.Balanced
					}
				}
			});
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000C24E File Offset: 0x0000A44E
		internal void Reset()
		{
			this.m_BottleneckHistory.Clear();
			this.m_FrameHistory.Clear();
		}

		// Token: 0x040001B3 RID: 435
		private const string k_FpsFormatString = "{0:F1}";

		// Token: 0x040001B4 RID: 436
		private const string k_MsFormatString = "{0:F2}ms";

		// Token: 0x040001B5 RID: 437
		private const float k_RefreshRate = 0.2f;

		// Token: 0x040001B6 RID: 438
		internal FrameTimeSampleHistory m_FrameHistory;

		// Token: 0x040001B7 RID: 439
		internal BottleneckHistory m_BottleneckHistory;

		// Token: 0x040001BA RID: 442
		private FrameTiming[] m_Timing = new FrameTiming[1];

		// Token: 0x040001BB RID: 443
		private FrameTimeSample m_Sample;
	}
}
