using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x02000075 RID: 117
	public class DebugDisplaySettingsVolume : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000A0C6 File Offset: 0x000082C6
		public IVolumeDebugSettings volumeDebugSettings { get; }

		// Token: 0x0600052C RID: 1324 RVA: 0x0000A0CE File Offset: 0x000082CE
		public DebugDisplaySettingsVolume(IVolumeDebugSettings volumeDebugSettings)
		{
			this.volumeDebugSettings = volumeDebugSettings;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000090C6 File Offset: 0x000072C6
		public bool AreAnySettingsActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new DebugDisplaySettingsVolume.SettingsPanel(this);
		}

		// Token: 0x04000161 RID: 353
		internal int volumeComponentEnumIndex;

		// Token: 0x04000162 RID: 354
		internal Dictionary<string, VolumeComponent> debugState = new Dictionary<string, VolumeComponent>();

		// Token: 0x04000163 RID: 355
		private const string k_PanelTitle = "Volume";

		// Token: 0x02000076 RID: 118
		private static class Styles
		{
			// Token: 0x04000164 RID: 356
			public static readonly GUIContent none = new GUIContent("None");

			// Token: 0x04000165 RID: 357
			public static readonly GUIContent editorCamera = new GUIContent("Editor Camera");
		}

		// Token: 0x02000077 RID: 119
		private static class Strings
		{
			// Token: 0x04000166 RID: 358
			public static readonly string none = "None";

			// Token: 0x04000167 RID: 359
			public static readonly string camera = "Camera";

			// Token: 0x04000168 RID: 360
			public static readonly string parameter = "Parameter";

			// Token: 0x04000169 RID: 361
			public static readonly string component = "Component";

			// Token: 0x0400016A RID: 362
			public static readonly string debugViewNotSupported = "Debug view not supported";

			// Token: 0x0400016B RID: 363
			public static readonly string volumeInfo = "Volume Info";

			// Token: 0x0400016C RID: 364
			public static readonly string resultValue = "Result";

			// Token: 0x0400016D RID: 365
			public static readonly string resultValueTooltip = "The interpolated result value of the parameter. This value is used to render the camera.";

			// Token: 0x0400016E RID: 366
			public static readonly string globalDefaultValue = "Default";

			// Token: 0x0400016F RID: 367
			public static readonly string globalDefaultValueTooltip = "Default value for this parameter, defined by the Default Volume Profile in Global Settings.";

			// Token: 0x04000170 RID: 368
			public static readonly string qualityLevelValue = "SRP Asset";

			// Token: 0x04000171 RID: 369
			public static readonly string qualityLevelValueTooltip = "Override value for this parameter, defined by the Volume Profile in the current SRP Asset.";

			// Token: 0x04000172 RID: 370
			public static readonly string global = "Global";

			// Token: 0x04000173 RID: 371
			public static readonly string local = "Local";
		}

		// Token: 0x02000078 RID: 120
		internal static class WidgetFactory
		{
			// Token: 0x06000531 RID: 1329 RVA: 0x0000A1AC File Offset: 0x000083AC
			public static DebugUI.EnumField CreateComponentSelector(DebugDisplaySettingsVolume.SettingsPanel panel, Action<DebugUI.Field<int>, int> refresh)
			{
				int componentIndex = 0;
				List<GUIContent> componentNames = new List<GUIContent> { DebugDisplaySettingsVolume.Styles.none };
				List<int> componentValues = new List<int> { componentIndex++ };
				foreach (ValueTuple<string, Type> type in VolumeManager.instance.GetVolumeComponentsForDisplay(GraphicsSettings.currentRenderPipelineAssetType))
				{
					componentNames.Add(new GUIContent
					{
						text = type.Item1
					});
					componentValues.Add(componentIndex++);
				}
				return new DebugUI.EnumField
				{
					displayName = DebugDisplaySettingsVolume.Strings.component,
					getter = () => panel.data.volumeDebugSettings.selectedComponent,
					setter = delegate(int value)
					{
						panel.data.volumeDebugSettings.selectedComponent = value;
					},
					enumNames = componentNames.ToArray(),
					enumValues = componentValues.ToArray(),
					getIndex = () => panel.data.volumeComponentEnumIndex,
					setIndex = delegate(int value)
					{
						panel.data.volumeComponentEnumIndex = value;
					},
					onValueChanged = refresh
				};
			}

			// Token: 0x06000532 RID: 1330 RVA: 0x0000A2D0 File Offset: 0x000084D0
			public static DebugUI.ObjectPopupField CreateCameraSelector(DebugDisplaySettingsVolume.SettingsPanel panel, Action<DebugUI.Field<Object>, Object> refresh)
			{
				return new DebugUI.ObjectPopupField
				{
					displayName = DebugDisplaySettingsVolume.Strings.camera,
					getter = () => panel.data.volumeDebugSettings.selectedCamera,
					setter = delegate(Object value)
					{
						Camera[] c = panel.data.volumeDebugSettings.cameras.ToArray<Camera>();
						panel.data.volumeDebugSettings.selectedCameraIndex = Array.IndexOf<Camera>(c, value as Camera);
					},
					getObjects = () => panel.data.volumeDebugSettings.cameras,
					onValueChanged = refresh
				};
			}

			// Token: 0x06000533 RID: 1331 RVA: 0x0000A338 File Offset: 0x00008538
			private static DebugUI.Widget CreateVolumeParameterWidget(string name, VolumeParameter param, Func<bool> isHiddenCallback = null)
			{
				DebugDisplaySettingsVolume.WidgetFactory.<>c__DisplayClass2_0 CS$<>8__locals1 = new DebugDisplaySettingsVolume.WidgetFactory.<>c__DisplayClass2_0();
				CS$<>8__locals1.param = param;
				if (CS$<>8__locals1.param == null)
				{
					DebugUI.Value value3 = new DebugUI.Value();
					value3.displayName = name;
					value3.getter = () => "-";
					return value3;
				}
				CS$<>8__locals1.parameterType = CS$<>8__locals1.param.GetType();
				if (CS$<>8__locals1.parameterType == typeof(ColorParameter))
				{
					ColorParameter p2 = (ColorParameter)CS$<>8__locals1.param;
					return new DebugUI.ColorField
					{
						displayName = name,
						hdr = p2.hdr,
						showAlpha = p2.showAlpha,
						getter = () => p2.value,
						setter = delegate(Color value)
						{
							p2.value = value;
						},
						isHiddenCallback = isHiddenCallback
					};
				}
				if (CS$<>8__locals1.parameterType == typeof(BoolParameter))
				{
					BoolParameter p = (BoolParameter)CS$<>8__locals1.param;
					return new DebugUI.BoolField
					{
						displayName = name,
						getter = () => p.value,
						setter = delegate(bool value)
						{
							p.value = value;
						},
						isHiddenCallback = isHiddenCallback
					};
				}
				Type[] genericArguments = CS$<>8__locals1.parameterType.GetTypeInfo().BaseType.GenericTypeArguments;
				if (genericArguments.Length != 0 && genericArguments[0].IsArray)
				{
					return new DebugUI.ObjectListField
					{
						displayName = name,
						getter = () => (Object[])CS$<>8__locals1.parameterType.GetProperty("value").GetValue(CS$<>8__locals1.param, null),
						type = CS$<>8__locals1.parameterType
					};
				}
				CS$<>8__locals1.property = CS$<>8__locals1.param.GetType().GetProperty("value");
				MethodInfo toString = CS$<>8__locals1.property.PropertyType.GetMethod("ToString", Type.EmptyTypes);
				if (!(toString == null) && !(toString.DeclaringType == typeof(object)) && !(toString.DeclaringType == typeof(Object)))
				{
					return new DebugUI.Value
					{
						displayName = name,
						getter = delegate
						{
							object value4 = CS$<>8__locals1.property.GetValue(CS$<>8__locals1.param);
							if (value4 != null)
							{
								return value4.ToString();
							}
							return DebugDisplaySettingsVolume.Strings.none;
						},
						isHiddenCallback = isHiddenCallback
					};
				}
				PropertyInfo nameProp = CS$<>8__locals1.property.PropertyType.GetProperty("name");
				if (nameProp == null)
				{
					DebugUI.Value value2 = new DebugUI.Value();
					value2.displayName = name;
					value2.getter = () => DebugDisplaySettingsVolume.Strings.debugViewNotSupported;
					return value2;
				}
				return new DebugUI.Value
				{
					displayName = name,
					getter = delegate
					{
						object value5 = CS$<>8__locals1.property.GetValue(CS$<>8__locals1.param);
						if (value5 == null || value5.Equals(null))
						{
							return DebugDisplaySettingsVolume.Strings.none;
						}
						return nameProp.GetValue(value5) ?? DebugDisplaySettingsVolume.Strings.none;
					},
					isHiddenCallback = isHiddenCallback
				};
			}

			// Token: 0x06000534 RID: 1332 RVA: 0x0000A60C File Offset: 0x0000880C
			public static DebugUI.Table CreateVolumeTable(DebugDisplaySettingsVolume data)
			{
				DebugDisplaySettingsVolume.WidgetFactory.<>c__DisplayClass4_0 CS$<>8__locals1 = new DebugDisplaySettingsVolume.WidgetFactory.<>c__DisplayClass4_0();
				CS$<>8__locals1.data = data;
				CS$<>8__locals1.table = new DebugUI.Table
				{
					displayName = DebugDisplaySettingsVolume.Strings.parameter,
					isReadOnly = true,
					isHiddenCallback = () => CS$<>8__locals1.data.volumeDebugSettings.selectedComponent == 0
				};
				CS$<>8__locals1.selectedType = CS$<>8__locals1.data.volumeDebugSettings.selectedComponentType;
				if (CS$<>8__locals1.selectedType == null)
				{
					return CS$<>8__locals1.table;
				}
				CS$<>8__locals1.volumeManager = VolumeManager.instance;
				VolumeStack stack = CS$<>8__locals1.data.volumeDebugSettings.selectedCameraVolumeStack ?? CS$<>8__locals1.volumeManager.stack;
				CS$<>8__locals1.stackComponent = stack.GetComponent(CS$<>8__locals1.selectedType);
				if (CS$<>8__locals1.stackComponent == null)
				{
					return CS$<>8__locals1.table;
				}
				CS$<>8__locals1.volumes = CS$<>8__locals1.data.volumeDebugSettings.GetVolumes();
				DebugUI.Table.Row row5 = new DebugUI.Table.Row();
				row5.displayName = DebugDisplaySettingsVolume.Strings.volumeInfo;
				row5.opened = true;
				ObservableList<DebugUI.Widget> children = row5.children;
				DebugUI.Value value = new DebugUI.Value();
				value.displayName = DebugDisplaySettingsVolume.Strings.resultValue;
				value.tooltip = DebugDisplaySettingsVolume.Strings.resultValueTooltip;
				value.getter = () => string.Empty;
				children.Add(value);
				DebugUI.Table.Row row = row5;
				DebugUI.Table.Row row2 = new DebugUI.Table.Row
				{
					displayName = "GameObject",
					children = { DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue }
				};
				DebugUI.Table.Row row3 = new DebugUI.Table.Row
				{
					displayName = "Volume Profile",
					children = { DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue }
				};
				DebugUI.Table.Row row4 = new DebugUI.Table.Row
				{
					displayName = string.Empty,
					children = { DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue }
				};
				Volume[] volumes = CS$<>8__locals1.volumes;
				for (int j = 0; j < volumes.Length; j++)
				{
					Volume volume = volumes[j];
					VolumeProfile profile = (volume.HasInstantiatedProfile() ? volume.profile : volume.sharedProfile);
					row.children.Add(new DebugUI.Value
					{
						displayName = profile.name,
						tooltip = "Override value for this parameter, defined by " + profile.name,
						getter = delegate
						{
							string text = (volume.isGlobal ? DebugDisplaySettingsVolume.Strings.global : DebugDisplaySettingsVolume.Strings.local);
							float weight = CS$<>8__locals1.data.volumeDebugSettings.GetVolumeWeight(volume);
							return text + " (" + (weight * 100f).ToString() + "%)";
						}
					});
					row2.children.Add(new DebugUI.ObjectField
					{
						displayName = string.Empty,
						getter = () => volume
					});
					row3.children.Add(new DebugUI.ObjectField
					{
						displayName = string.Empty,
						getter = () => profile
					});
					row4.children.Add(DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue);
				}
				CS$<>8__locals1.globalDefaultComponent = CS$<>8__locals1.<CreateVolumeTable>g__GetSelectedVolumeComponent|0(CS$<>8__locals1.volumeManager.globalDefaultProfile);
				CS$<>8__locals1.qualityDefaultComponent = CS$<>8__locals1.<CreateVolumeTable>g__GetSelectedVolumeComponent|0(CS$<>8__locals1.volumeManager.qualityDefaultProfile);
				CS$<>8__locals1.customDefaultComponents = new List<ValueTuple<VolumeProfile, VolumeComponent>>();
				if (CS$<>8__locals1.volumeManager.customDefaultProfiles != null)
				{
					foreach (VolumeProfile customProfile2 in CS$<>8__locals1.volumeManager.customDefaultProfiles)
					{
						VolumeComponent customDefaultComponent = CS$<>8__locals1.<CreateVolumeTable>g__GetSelectedVolumeComponent|0(customProfile2);
						if (customDefaultComponent != null)
						{
							CS$<>8__locals1.customDefaultComponents.Add(new ValueTuple<VolumeProfile, VolumeComponent>(customProfile2, customDefaultComponent));
						}
					}
				}
				foreach (ValueTuple<VolumeProfile, VolumeComponent> valueTuple in CS$<>8__locals1.customDefaultComponents)
				{
					VolumeProfile customProfile = valueTuple.Item1;
					ObservableList<DebugUI.Widget> children2 = row.children;
					DebugUI.Value value2 = new DebugUI.Value();
					value2.displayName = customProfile.name;
					value2.getter = () => string.Empty;
					children2.Add(value2);
					row2.children.Add(DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue);
					row3.children.Add(new DebugUI.ObjectField
					{
						displayName = string.Empty,
						getter = () => customProfile
					});
					row4.children.Add(DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue);
				}
				ObservableList<DebugUI.Widget> children3 = row.children;
				DebugUI.Value value3 = new DebugUI.Value();
				value3.displayName = DebugDisplaySettingsVolume.Strings.qualityLevelValue;
				value3.tooltip = DebugDisplaySettingsVolume.Strings.qualityLevelValueTooltip;
				value3.getter = () => string.Empty;
				children3.Add(value3);
				row2.children.Add(DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue);
				row3.children.Add(new DebugUI.ObjectField
				{
					displayName = string.Empty,
					getter = () => CS$<>8__locals1.volumeManager.qualityDefaultProfile
				});
				row4.children.Add(DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue);
				ObservableList<DebugUI.Widget> children4 = row.children;
				DebugUI.Value value4 = new DebugUI.Value();
				value4.displayName = DebugDisplaySettingsVolume.Strings.globalDefaultValue;
				value4.tooltip = DebugDisplaySettingsVolume.Strings.globalDefaultValueTooltip;
				value4.getter = () => string.Empty;
				children4.Add(value4);
				row2.children.Add(DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue);
				row3.children.Add(new DebugUI.ObjectField
				{
					displayName = string.Empty,
					getter = () => CS$<>8__locals1.volumeManager.globalDefaultProfile
				});
				row4.children.Add(DebugDisplaySettingsVolume.WidgetFactory.s_EmptyDebugUIValue);
				CS$<>8__locals1.table.children.Add(row);
				CS$<>8__locals1.table.children.Add(row2);
				CS$<>8__locals1.table.children.Add(row3);
				CS$<>8__locals1.table.children.Add(row4);
				CS$<>8__locals1.rows = new List<DebugUI.Table.Row>();
				CS$<>8__locals1.<CreateVolumeTable>g__AddParameterRows|1(CS$<>8__locals1.selectedType, null, 0);
				foreach (DebugUI.Table.Row r in CS$<>8__locals1.rows.OrderBy((DebugUI.Table.Row t) => t.displayName))
				{
					CS$<>8__locals1.table.children.Add(r);
				}
				CS$<>8__locals1.data.volumeDebugSettings.RefreshVolumes(CS$<>8__locals1.volumes);
				for (int i = 0; i < CS$<>8__locals1.volumes.Length; i++)
				{
					CS$<>8__locals1.table.SetColumnVisibility(i + 1, CS$<>8__locals1.data.volumeDebugSettings.VolumeHasInfluence(CS$<>8__locals1.volumes[i]));
				}
				CS$<>8__locals1.timer = 0f;
				CS$<>8__locals1.refreshRate = 0.2f;
				CS$<>8__locals1.table.isHiddenCallback = delegate
				{
					CS$<>8__locals1.timer += Time.deltaTime;
					if (CS$<>8__locals1.timer >= CS$<>8__locals1.refreshRate)
					{
						if (CS$<>8__locals1.data.volumeDebugSettings.selectedCamera != null)
						{
							Volume[] newVolumes = CS$<>8__locals1.data.volumeDebugSettings.GetVolumes();
							if (!CS$<>8__locals1.data.volumeDebugSettings.RefreshVolumes(newVolumes))
							{
								for (int k = 0; k < newVolumes.Length; k++)
								{
									bool visible = CS$<>8__locals1.data.volumeDebugSettings.VolumeHasInfluence(newVolumes[k]);
									CS$<>8__locals1.table.SetColumnVisibility(k + 1, visible);
								}
							}
							if (!CS$<>8__locals1.volumes.SequenceEqual(newVolumes))
							{
								CS$<>8__locals1.volumes = newVolumes;
								DebugManager.instance.ReDrawOnScreenDebug();
							}
						}
						CS$<>8__locals1.timer = 0f;
					}
					return false;
				};
				return CS$<>8__locals1.table;
			}

			// Token: 0x04000174 RID: 372
			private static DebugUI.Value s_EmptyDebugUIValue = new DebugUI.Value
			{
				getter = () => string.Empty
			};
		}

		// Token: 0x02000086 RID: 134
		[DisplayInfo(name = "Volume", order = 2147483647)]
		internal class SettingsPanel : DebugDisplaySettingsPanel<DebugDisplaySettingsVolume>
		{
			// Token: 0x06000567 RID: 1383 RVA: 0x0000B464 File Offset: 0x00009664
			public SettingsPanel(DebugDisplaySettingsVolume data)
				: base(data)
			{
				base.AddWidget(DebugDisplaySettingsVolume.WidgetFactory.CreateCameraSelector(this, delegate(DebugUI.Field<Object> _, Object __)
				{
					this.Refresh();
				}));
				base.AddWidget(DebugDisplaySettingsVolume.WidgetFactory.CreateComponentSelector(this, delegate(DebugUI.Field<int> _, int __)
				{
					this.Refresh();
				}));
				this.m_VolumeTable = DebugDisplaySettingsVolume.WidgetFactory.CreateVolumeTable(this.m_Data);
				base.AddWidget(this.m_VolumeTable);
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x0000B4C8 File Offset: 0x000096C8
			private void Refresh()
			{
				DebugUI.Panel panel = DebugManager.instance.GetPanel(this.PanelName, false, 0, false);
				if (panel == null)
				{
					return;
				}
				bool needsRefresh = false;
				if (this.m_VolumeTable != null)
				{
					needsRefresh = true;
					panel.children.Remove(this.m_VolumeTable);
				}
				if (this.m_Data.volumeDebugSettings.selectedComponent > 0 && this.m_Data.volumeDebugSettings.selectedCamera != null)
				{
					needsRefresh = true;
					this.m_VolumeTable = DebugDisplaySettingsVolume.WidgetFactory.CreateVolumeTable(this.m_Data);
					base.AddWidget(this.m_VolumeTable);
					panel.children.Add(this.m_VolumeTable);
				}
				if (needsRefresh)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				}
			}

			// Token: 0x0400019A RID: 410
			private DebugUI.Table m_VolumeTable;
		}
	}
}
