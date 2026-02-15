using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200000B RID: 11
	public class DebugDisplayGPUResidentDrawer : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002442 File Offset: 0x00000642
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002454 File Offset: 0x00000654
		private bool displayBatcherStats
		{
			get
			{
				DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
				return debugStats != null && debugStats.enabled;
			}
			set
			{
				DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
				if (debugStats != null)
				{
					debugStats.enabled = value;
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002474 File Offset: 0x00000674
		internal bool GetOccluderViewInstanceID(out int viewInstanceID)
		{
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			if (debugStats != null && this.occluderDebugViewIndex >= 0 && this.occluderDebugViewIndex < debugStats.occluderStats.Length)
			{
				viewInstanceID = debugStats.occluderStats[this.occluderDebugViewIndex].viewInstanceID;
				return true;
			}
			viewInstanceID = 0;
			return false;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024C4 File Offset: 0x000006C4
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024D8 File Offset: 0x000006D8
		internal bool occlusionTestOverlayEnable
		{
			get
			{
				DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
				return debugStats != null && debugStats.occlusionOverlayEnabled;
			}
			set
			{
				DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
				if (debugStats != null)
				{
					debugStats.occlusionOverlayEnabled = value;
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000024F5 File Offset: 0x000006F5
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002508 File Offset: 0x00000708
		private bool occlusionTestOverlayCountVisible
		{
			get
			{
				DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
				return debugStats != null && debugStats.occlusionOverlayCountVisible;
			}
			set
			{
				DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
				if (debugStats != null)
				{
					debugStats.occlusionOverlayCountVisible = value;
				}
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002525 File Offset: 0x00000725
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002538 File Offset: 0x00000738
		private bool overrideOcclusionTestToAlwaysPass
		{
			get
			{
				DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
				return debugStats != null && debugStats.overrideOcclusionTestToAlwaysPass;
			}
			set
			{
				DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
				if (debugStats != null)
				{
					debugStats.overrideOcclusionTestToAlwaysPass = value;
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002558 File Offset: 0x00000758
		private static InstanceCullerViewStats GetInstanceCullerViewStats(int viewIndex)
		{
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			if (debugStats != null && viewIndex < debugStats.instanceCullerStats.Length)
			{
				return debugStats.instanceCullerStats[viewIndex];
			}
			return default(InstanceCullerViewStats);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002594 File Offset: 0x00000794
		private static InstanceOcclusionEventStats GetInstanceOcclusionEventStats(int passIndex)
		{
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			if (debugStats != null && passIndex < debugStats.instanceOcclusionEventStats.Length)
			{
				return debugStats.instanceOcclusionEventStats[passIndex];
			}
			return default(InstanceOcclusionEventStats);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025D0 File Offset: 0x000007D0
		private static DebugOccluderStats GetOccluderStats(int occluderIndex)
		{
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			if (debugStats != null && occluderIndex < debugStats.occluderStats.Length)
			{
				return debugStats.occluderStats[occluderIndex];
			}
			return default(DebugOccluderStats);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000260A File Offset: 0x0000080A
		private static int GetOcclusionContextsCounts()
		{
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			if (debugStats == null)
			{
				return 0;
			}
			return debugStats.occluderStats.Length;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002621 File Offset: 0x00000821
		private static int GetInstanceCullerViewCount()
		{
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			if (debugStats == null)
			{
				return 0;
			}
			return debugStats.instanceCullerStats.Length;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002638 File Offset: 0x00000838
		private static int GetInstanceOcclusionEventCount()
		{
			DebugRendererBatcherStats debugStats = GPUResidentDrawer.GetDebugStats();
			if (debugStats == null)
			{
				return 0;
			}
			return debugStats.instanceOcclusionEventStats.Length;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002650 File Offset: 0x00000850
		private static DebugUI.Table.Row AddInstanceCullerViewDataRow(int viewIndex)
		{
			return new DebugUI.Table.Row
			{
				displayName = "",
				opened = true,
				isHiddenCallback = () => viewIndex >= DebugDisplayGPUResidentDrawer.GetInstanceCullerViewCount(),
				children = 
				{
					new DebugUI.Value
					{
						displayName = "View Type",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => DebugDisplayGPUResidentDrawer.GetInstanceCullerViewStats(viewIndex).viewType
					},
					new DebugUI.Value
					{
						displayName = "View Instance ID",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => DebugDisplayGPUResidentDrawer.GetInstanceCullerViewStats(viewIndex).viewInstanceID
					},
					new DebugUI.Value
					{
						displayName = "Split Index",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => DebugDisplayGPUResidentDrawer.GetInstanceCullerViewStats(viewIndex).splitIndex
					},
					new DebugUI.Value
					{
						displayName = "Visible Instances",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => DebugDisplayGPUResidentDrawer.GetInstanceCullerViewStats(viewIndex).visibleInstances
					},
					new DebugUI.Value
					{
						displayName = "Draw Commands",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => DebugDisplayGPUResidentDrawer.GetInstanceCullerViewStats(viewIndex).drawCommands
					}
				}
			};
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027E2 File Offset: 0x000009E2
		private static object OccluderVersionString(in InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != InstanceOcclusionEventType.OccluderUpdate && stats.occlusionTest == OcclusionTest.None)
			{
				return "-";
			}
			return stats.occluderVersion;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002806 File Offset: 0x00000A06
		private static object OcclusionTestString(in InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != InstanceOcclusionEventType.OcclusionTest)
			{
				return "-";
			}
			return stats.occlusionTest;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002821 File Offset: 0x00000A21
		private static object VisibleInstancesString(in InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != InstanceOcclusionEventType.OcclusionTest)
			{
				return "-";
			}
			return stats.visibleInstances;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000283C File Offset: 0x00000A3C
		private static object CulledInstancesString(in InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != InstanceOcclusionEventType.OcclusionTest)
			{
				return "-";
			}
			return stats.culledInstances;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002858 File Offset: 0x00000A58
		private static DebugUI.Table.Row AddInstanceOcclusionPassDataRow(int eventIndex)
		{
			return new DebugUI.Table.Row
			{
				displayName = "",
				opened = true,
				isHiddenCallback = () => eventIndex >= DebugDisplayGPUResidentDrawer.GetInstanceOcclusionEventCount(),
				children = 
				{
					new DebugUI.Value
					{
						displayName = "View Instance ID",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => DebugDisplayGPUResidentDrawer.GetInstanceOcclusionEventStats(eventIndex).viewInstanceID
					},
					new DebugUI.Value
					{
						displayName = "Event Type",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => string.Format("{0}", DebugDisplayGPUResidentDrawer.GetInstanceOcclusionEventStats(eventIndex).eventType)
					},
					new DebugUI.Value
					{
						displayName = "Occluder Version",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = delegate
						{
							InstanceOcclusionEventStats instanceOcclusionEventStats = DebugDisplayGPUResidentDrawer.GetInstanceOcclusionEventStats(eventIndex);
							return DebugDisplayGPUResidentDrawer.OccluderVersionString(in instanceOcclusionEventStats);
						}
					},
					new DebugUI.Value
					{
						displayName = "Subview Mask",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => string.Format("0x{0:X}", DebugDisplayGPUResidentDrawer.GetInstanceOcclusionEventStats(eventIndex).subviewMask)
					},
					new DebugUI.Value
					{
						displayName = "Occlusion Test",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = delegate
						{
							string text = "{0}";
							InstanceOcclusionEventStats instanceOcclusionEventStats2 = DebugDisplayGPUResidentDrawer.GetInstanceOcclusionEventStats(eventIndex);
							return string.Format(text, DebugDisplayGPUResidentDrawer.OcclusionTestString(in instanceOcclusionEventStats2));
						}
					},
					new DebugUI.Value
					{
						displayName = "Visible Instances",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = delegate
						{
							InstanceOcclusionEventStats instanceOcclusionEventStats3 = DebugDisplayGPUResidentDrawer.GetInstanceOcclusionEventStats(eventIndex);
							return DebugDisplayGPUResidentDrawer.VisibleInstancesString(in instanceOcclusionEventStats3);
						}
					},
					new DebugUI.Value
					{
						displayName = "Culled Instances",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = delegate
						{
							InstanceOcclusionEventStats instanceOcclusionEventStats4 = DebugDisplayGPUResidentDrawer.GetInstanceOcclusionEventStats(eventIndex);
							return DebugDisplayGPUResidentDrawer.CulledInstancesString(in instanceOcclusionEventStats4);
						}
					}
				}
			};
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A70 File Offset: 0x00000C70
		private static DebugUI.Table.Row AddOcclusionContextDataRow(int index)
		{
			return new DebugUI.Table.Row
			{
				displayName = "",
				opened = true,
				isHiddenCallback = () => index >= DebugDisplayGPUResidentDrawer.GetOcclusionContextsCounts(),
				children = 
				{
					new DebugUI.Value
					{
						displayName = "View Instance ID",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => DebugDisplayGPUResidentDrawer.GetOccluderStats(index).viewInstanceID
					},
					new DebugUI.Value
					{
						displayName = "Subview Count",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => DebugDisplayGPUResidentDrawer.GetOccluderStats(index).subviewCount
					},
					new DebugUI.Value
					{
						displayName = "Size Per Subview",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = delegate
						{
							Vector2Int size = DebugDisplayGPUResidentDrawer.GetOccluderStats(index).occluderMipLayoutSize;
							return string.Format("{0}x{1}", size.x, size.y);
						}
					}
				}
			};
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002B7C File Offset: 0x00000D7C
		public bool AreAnySettingsActive
		{
			get
			{
				return this.displayBatcherStats;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002B84 File Offset: 0x00000D84
		public bool IsPostProcessingAllowed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002B84 File Offset: 0x00000D84
		public bool IsLightingActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B87 File Offset: 0x00000D87
		public bool TryGetScreenClearColor(ref Color color)
		{
			return false;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B8A File Offset: 0x00000D8A
		IDebugDisplaySettingsPanelDisposable IDebugDisplaySettingsData.CreatePanel()
		{
			return new DebugDisplayGPUResidentDrawer.SettingsPanel(this);
		}

		// Token: 0x04000011 RID: 17
		private const string k_FormatString = "{0}";

		// Token: 0x04000012 RID: 18
		private const float k_RefreshRate = 0.2f;

		// Token: 0x04000013 RID: 19
		private const int k_MaxViewCount = 32;

		// Token: 0x04000014 RID: 20
		private const int k_MaxOcclusionPassCount = 32;

		// Token: 0x04000015 RID: 21
		private const int k_MaxContextCount = 16;

		// Token: 0x04000016 RID: 22
		public bool occluderDebugViewEnable;

		// Token: 0x04000017 RID: 23
		internal bool occluderContextStats;

		// Token: 0x04000018 RID: 24
		internal Vector2 occluderDebugViewRange = new Vector2(0f, 1f);

		// Token: 0x04000019 RID: 25
		internal int occluderDebugViewIndex;

		// Token: 0x0200000C RID: 12
		private static class Strings
		{
			// Token: 0x0400001A RID: 26
			public const string drawerSettingsContainerName = "GPU Resident Drawer Settings";

			// Token: 0x0400001B RID: 27
			public static readonly DebugUI.Widget.NameAndTooltip displayBatcherStats = new DebugUI.Widget.NameAndTooltip
			{
				name = "Display Culling Stats",
				tooltip = "Enable the checkbox to display stats for instance culling."
			};

			// Token: 0x0400001C RID: 28
			public const string occlusionCullingTitle = "Occlusion Culling";

			// Token: 0x0400001D RID: 29
			public static readonly DebugUI.Widget.NameAndTooltip occlusionTestOverlayEnable = new DebugUI.Widget.NameAndTooltip
			{
				name = "Occlusion Test Overlay",
				tooltip = "Occlusion test visualisation."
			};

			// Token: 0x0400001E RID: 30
			public static readonly DebugUI.Widget.NameAndTooltip occlusionTestOverlayCountVisible = new DebugUI.Widget.NameAndTooltip
			{
				name = "Occlusion Test Overlay Count Visible",
				tooltip = "Occlusion test visualisation should count visible instances instead of occluded instances."
			};

			// Token: 0x0400001F RID: 31
			public static readonly DebugUI.Widget.NameAndTooltip overrideOcclusionTestToAlwaysPass = new DebugUI.Widget.NameAndTooltip
			{
				name = "Override Occlusion Test To Always Pass",
				tooltip = "Occlusion test always passes."
			};

			// Token: 0x04000020 RID: 32
			public static readonly DebugUI.Widget.NameAndTooltip occluderContextStats = new DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Context Stats",
				tooltip = "Show all the active occluder context textures."
			};

			// Token: 0x04000021 RID: 33
			public static readonly DebugUI.Widget.NameAndTooltip occluderDebugViewEnable = new DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Debug View",
				tooltip = "Debug view of occluder texture."
			};

			// Token: 0x04000022 RID: 34
			public static readonly DebugUI.Widget.NameAndTooltip occluderDebugViewIndex = new DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Debug View Index",
				tooltip = "Index of the view for which the occluder texture is displayed. Use the Occlusion Test Context Stats for a list of the views."
			};

			// Token: 0x04000023 RID: 35
			public static readonly DebugUI.Widget.NameAndTooltip occluderDebugViewRangeMin = new DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Debug View Range Min",
				tooltip = "Range in which the occluder debug texture are displayed."
			};

			// Token: 0x04000024 RID: 36
			public static readonly DebugUI.Widget.NameAndTooltip occluderDebugViewRangeMax = new DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Debug View Range Max",
				tooltip = "Range in which the occluder debug texture are displayed."
			};
		}

		// Token: 0x0200000D RID: 13
		[DisplayInfo(name = "GPU Resident Drawer", order = 5)]
		private class SettingsPanel : DebugDisplaySettingsPanel
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600003A RID: 58 RVA: 0x00002D13 File Offset: 0x00000F13
			public override string PanelName
			{
				get
				{
					return "GPU Resident Drawer";
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600003B RID: 59 RVA: 0x00002D1A File Offset: 0x00000F1A
			public override DebugUI.Flags Flags
			{
				get
				{
					return DebugUI.Flags.EditorForceUpdate;
				}
			}

			// Token: 0x0600003C RID: 60 RVA: 0x00002D20 File Offset: 0x00000F20
			public SettingsPanel(DebugDisplayGPUResidentDrawer data)
			{
				DebugUI.MessageBox messageBox = new DebugUI.MessageBox();
				messageBox.displayName = "Not Supported";
				messageBox.style = DebugUI.MessageBox.Style.Warning;
				messageBox.messageCallback = delegate
				{
					string msg;
					LogType logType;
					if (!GPUResidentDrawer.IsGPUResidentDrawerSupportedBySRP(GPUResidentDrawer.GetGlobalSettingsFromRPAsset(), out msg, out logType))
					{
						return msg;
					}
					return string.Empty;
				};
				messageBox.isHiddenCallback = () => GPUResidentDrawer.IsEnabled();
				DebugUI.MessageBox helpBox = messageBox;
				base.AddWidget(helpBox);
				DebugUI.Container container = new DebugUI.Container();
				container.displayName = "Occlusion Culling";
				container.isHiddenCallback = () => !GPUResidentDrawer.IsEnabled();
				container.children.Add(new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.occlusionTestOverlayEnable,
					getter = () => data.occlusionTestOverlayEnable,
					setter = delegate(bool value)
					{
						data.occlusionTestOverlayEnable = value;
					}
				});
				container.children.Add(new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.occlusionTestOverlayCountVisible,
					getter = () => data.occlusionTestOverlayCountVisible,
					setter = delegate(bool value)
					{
						data.occlusionTestOverlayCountVisible = value;
					}
				});
				container.children.Add(new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.overrideOcclusionTestToAlwaysPass,
					getter = () => data.overrideOcclusionTestToAlwaysPass,
					setter = delegate(bool value)
					{
						data.overrideOcclusionTestToAlwaysPass = value;
					}
				});
				container.children.Add(new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.occluderContextStats,
					getter = () => data.occluderContextStats,
					setter = delegate(bool value)
					{
						data.occluderContextStats = value;
					}
				});
				container.children.Add(new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.occluderDebugViewEnable,
					getter = () => data.occluderDebugViewEnable,
					setter = delegate(bool value)
					{
						data.occluderDebugViewEnable = value;
					}
				});
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.IntField intField = new DebugUI.IntField();
				intField.nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.occluderDebugViewIndex;
				intField.getter = () => data.occluderDebugViewIndex;
				intField.setter = delegate(int value)
				{
					data.occluderDebugViewIndex = value;
				};
				intField.isHiddenCallback = () => !data.occluderDebugViewEnable;
				intField.min = () => 0;
				intField.max = () => Math.Max(DebugDisplayGPUResidentDrawer.GetOcclusionContextsCounts() - 1, 0);
				children.Add(intField);
				container.children.Add(new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.occluderDebugViewRangeMin,
					getter = () => data.occluderDebugViewRange.x,
					setter = delegate(float value)
					{
						data.occluderDebugViewRange.x = value;
					},
					isHiddenCallback = () => !data.occluderDebugViewEnable
				});
				container.children.Add(new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.occluderDebugViewRangeMax,
					getter = () => data.occluderDebugViewRange.y,
					setter = delegate(float value)
					{
						data.occluderDebugViewRange.y = value;
					},
					isHiddenCallback = () => !data.occluderDebugViewEnable
				});
				base.AddWidget(container);
				this.AddOcclusionContextStatsWidget(data);
				DebugUI.Container container2 = new DebugUI.Container();
				container2.displayName = "GPU Resident Drawer Settings";
				container2.isHiddenCallback = () => !GPUResidentDrawer.IsEnabled();
				container2.children.Add(new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplayGPUResidentDrawer.Strings.displayBatcherStats,
					getter = () => data.displayBatcherStats,
					setter = delegate(bool value)
					{
						data.displayBatcherStats = value;
					}
				});
				base.AddWidget(container2);
				this.AddInstanceCullingStatsWidget(data);
			}

			// Token: 0x0600003D RID: 61 RVA: 0x000030F0 File Offset: 0x000012F0
			private void AddInstanceCullingStatsWidget(DebugDisplayGPUResidentDrawer data)
			{
				DebugUI.Foldout instanceCullerStats = new DebugUI.Foldout
				{
					displayName = "Instance Culler Stats",
					isHeader = true,
					opened = true,
					isHiddenCallback = () => !data.displayBatcherStats
				};
				ObservableList<DebugUI.Widget> children = instanceCullerStats.children;
				DebugUI.ValueTuple valueTuple = new DebugUI.ValueTuple();
				valueTuple.displayName = "View Count";
				DebugUI.ValueTuple valueTuple2 = valueTuple;
				DebugUI.Value[] array = new DebugUI.Value[1];
				int num = 0;
				DebugUI.Value value = new DebugUI.Value();
				value.refreshRate = 0.2f;
				value.formatString = "{0}";
				value.getter = () => DebugDisplayGPUResidentDrawer.GetInstanceCullerViewCount();
				array[num] = value;
				valueTuple2.values = array;
				children.Add(valueTuple);
				DebugUI.Table viewTable = new DebugUI.Table
				{
					displayName = "",
					isReadOnly = true
				};
				for (int i = 0; i < 32; i++)
				{
					viewTable.children.Add(DebugDisplayGPUResidentDrawer.AddInstanceCullerViewDataRow(i));
				}
				DebugUI.Foldout perViewStats = new DebugUI.Foldout
				{
					displayName = "Per View Stats",
					isHeader = true,
					opened = false,
					isHiddenCallback = () => !data.displayBatcherStats
				};
				perViewStats.children.Add(viewTable);
				instanceCullerStats.children.Add(perViewStats);
				DebugUI.Table eventTable = new DebugUI.Table
				{
					displayName = "",
					isReadOnly = true
				};
				for (int j = 0; j < 32; j++)
				{
					eventTable.children.Add(DebugDisplayGPUResidentDrawer.AddInstanceOcclusionPassDataRow(j));
				}
				DebugUI.Foldout perEventStats = new DebugUI.Foldout
				{
					displayName = "Occlusion Culling Events",
					isHeader = true,
					opened = false,
					isHiddenCallback = () => !data.displayBatcherStats
				};
				perEventStats.children.Add(eventTable);
				instanceCullerStats.children.Add(perEventStats);
				base.AddWidget(instanceCullerStats);
			}

			// Token: 0x0600003E RID: 62 RVA: 0x000032C0 File Offset: 0x000014C0
			private void AddOcclusionContextStatsWidget(DebugDisplayGPUResidentDrawer data)
			{
				DebugUI.Foldout visibilityStats = new DebugUI.Foldout
				{
					displayName = "Occlusion Context Stats",
					isHeader = true,
					opened = true,
					isHiddenCallback = () => !data.occluderContextStats
				};
				ObservableList<DebugUI.Widget> children = visibilityStats.children;
				DebugUI.ValueTuple valueTuple = new DebugUI.ValueTuple();
				valueTuple.displayName = "Active Occlusion Contexts";
				DebugUI.ValueTuple valueTuple2 = valueTuple;
				DebugUI.Value[] array = new DebugUI.Value[1];
				int num = 0;
				DebugUI.Value value = new DebugUI.Value();
				value.refreshRate = 0.2f;
				value.formatString = "{0}";
				value.getter = () => DebugDisplayGPUResidentDrawer.GetOcclusionContextsCounts();
				array[num] = value;
				valueTuple2.values = array;
				children.Add(valueTuple);
				DebugUI.Table viewTable = new DebugUI.Table
				{
					displayName = "",
					isReadOnly = true
				};
				for (int i = 0; i < 16; i++)
				{
					viewTable.children.Add(DebugDisplayGPUResidentDrawer.AddOcclusionContextDataRow(i));
				}
				visibilityStats.children.Add(viewTable);
				base.AddWidget(visibilityStats);
			}
		}
	}
}
