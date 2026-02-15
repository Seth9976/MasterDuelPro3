using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000E9 RID: 233
	internal class XRLayoutBuilder
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x0003E768 File Offset: 0x0003C968
		private static uint GetSizeOfFeature(XRFeatureDescriptor featureDescriptor)
		{
			switch (featureDescriptor.featureType)
			{
			case FeatureType.Custom:
				return featureDescriptor.customSize;
			case FeatureType.Binary:
				return 1U;
			case FeatureType.DiscreteStates:
				return 4U;
			case FeatureType.Axis1D:
				return 4U;
			case FeatureType.Axis2D:
				return 8U;
			case FeatureType.Axis3D:
				return 12U;
			case FeatureType.Rotation:
				return 16U;
			case FeatureType.Hand:
				return 104U;
			case FeatureType.Bone:
				return 32U;
			case FeatureType.Eyes:
				return 76U;
			default:
				return 0U;
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		private static string SanitizeString(string original, bool allowPaths = false)
		{
			int stringLength = original.Length;
			StringBuilder sanitizedName = new StringBuilder(stringLength);
			for (int i = 0; i < stringLength; i++)
			{
				char letter = original[i];
				if (char.IsUpper(letter) || char.IsLower(letter) || char.IsDigit(letter) || letter == '_' || (allowPaths && letter == '/'))
				{
					sanitizedName.Append(letter);
				}
			}
			return sanitizedName.ToString();
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0003E830 File Offset: 0x0003CA30
		internal static string OnFindLayoutForDevice(ref InputDeviceDescription description, string matchedLayout, InputDeviceExecuteCommandDelegate executeCommandDelegate)
		{
			if (description.interfaceName != "XRInputV1" && description.interfaceName != "XRInput")
			{
				return null;
			}
			if (string.IsNullOrEmpty(description.capabilities))
			{
				return null;
			}
			XRDeviceDescriptor deviceDescriptor;
			try
			{
				deviceDescriptor = XRDeviceDescriptor.FromJson(description.capabilities);
			}
			catch (Exception)
			{
				return null;
			}
			if (deviceDescriptor == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(matchedLayout))
			{
				if ((deviceDescriptor.characteristics & InputDeviceCharacteristics.HeadMounted) != InputDeviceCharacteristics.None)
				{
					matchedLayout = "XRHMD";
				}
				else if ((deviceDescriptor.characteristics & (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller)) == (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller))
				{
					matchedLayout = "XRController";
				}
			}
			string layoutName;
			if (string.IsNullOrEmpty(description.manufacturer))
			{
				layoutName = XRLayoutBuilder.SanitizeString(description.interfaceName, false) + "::" + XRLayoutBuilder.SanitizeString(description.product, false);
			}
			else
			{
				layoutName = string.Concat(new string[]
				{
					XRLayoutBuilder.SanitizeString(description.interfaceName, false),
					"::",
					XRLayoutBuilder.SanitizeString(description.manufacturer, false),
					"::",
					XRLayoutBuilder.SanitizeString(description.product, false)
				});
			}
			XRLayoutBuilder layout = new XRLayoutBuilder
			{
				descriptor = deviceDescriptor,
				parentLayout = matchedLayout,
				interfaceName = description.interfaceName
			};
			InputSystem.RegisterLayoutBuilder(() => layout.Build(), layoutName, matchedLayout, null);
			return layoutName;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0003E994 File Offset: 0x0003CB94
		private static string ConvertPotentialAliasToName(InputControlLayout layout, string nameOrAlias)
		{
			InternedString internedNameOrAlias = new InternedString(nameOrAlias);
			ReadOnlyArray<InputControlLayout.ControlItem> controls = layout.controls;
			for (int i = 0; i < controls.Count; i++)
			{
				InputControlLayout.ControlItem controlItem = controls[i];
				if (controlItem.name == internedNameOrAlias)
				{
					return nameOrAlias;
				}
				ReadOnlyArray<InternedString> aliases = controlItem.aliases;
				for (int j = 0; j < aliases.Count; j++)
				{
					if (aliases[j] == nameOrAlias)
					{
						return controlItem.name.ToString();
					}
				}
			}
			return nameOrAlias;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0003EA24 File Offset: 0x0003CC24
		private bool IsSubControl(string name)
		{
			return name.Contains('/');
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0003EA30 File Offset: 0x0003CC30
		private string GetParentControlName(string name)
		{
			int idx = name.IndexOf('/');
			return name.Substring(0, idx);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0003EA50 File Offset: 0x0003CC50
		private bool IsPoseControl(List<XRFeatureDescriptor> features, int startIndex)
		{
			for (int i = 0; i < 6; i++)
			{
				if (!features[startIndex + i].name.EndsWith(XRLayoutBuilder.poseSubControlNames[i]) || features[startIndex + i].featureType != XRLayoutBuilder.poseSubControlTypes[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0003EAA0 File Offset: 0x0003CCA0
		private InputControlLayout Build()
		{
			InputControlLayout.Builder builder = new InputControlLayout.Builder
			{
				stateFormat = new FourCC('X', 'R', 'S', '0'),
				extendsLayout = this.parentLayout,
				updateBeforeRender = new bool?(true)
			};
			InputControlLayout inheritedLayout = ((!string.IsNullOrEmpty(this.parentLayout)) ? InputSystem.LoadLayout(this.parentLayout) : null);
			List<string> parentControls = new List<string>();
			List<string> currentUsages = new List<string>();
			uint currentOffset = 0U;
			for (int i = 0; i < this.descriptor.inputFeatures.Count; i++)
			{
				XRFeatureDescriptor feature = this.descriptor.inputFeatures[i];
				currentUsages.Clear();
				if (feature.usageHints != null)
				{
					foreach (UsageHint usageHint in feature.usageHints)
					{
						if (!string.IsNullOrEmpty(usageHint.content))
						{
							currentUsages.Add(usageHint.content);
						}
					}
				}
				string featureName = feature.name;
				featureName = XRLayoutBuilder.SanitizeString(featureName, true);
				if (inheritedLayout != null)
				{
					featureName = XRLayoutBuilder.ConvertPotentialAliasToName(inheritedLayout, featureName);
				}
				featureName = featureName.ToLower();
				if (this.IsSubControl(featureName))
				{
					string parentControl = this.GetParentControlName(featureName);
					if (!parentControls.Contains(parentControl) && this.IsPoseControl(this.descriptor.inputFeatures, i))
					{
						builder.AddControl(parentControl).WithLayout("Pose").WithByteOffset(0U);
						parentControls.Add(parentControl);
					}
				}
				uint nextOffset = XRLayoutBuilder.GetSizeOfFeature(feature);
				if (!(this.interfaceName == "XRInput") && nextOffset >= 4U && currentOffset % 4U != 0U)
				{
					currentOffset += 4U - currentOffset % 4U;
				}
				switch (feature.featureType)
				{
				case FeatureType.Binary:
					builder.AddControl(featureName).WithLayout("Button").WithByteOffset(currentOffset)
						.WithFormat(InputStateBlock.FormatBit)
						.WithUsages(currentUsages);
					break;
				case FeatureType.DiscreteStates:
					builder.AddControl(featureName).WithLayout("Integer").WithByteOffset(currentOffset)
						.WithFormat(InputStateBlock.FormatInt)
						.WithUsages(currentUsages);
					break;
				case FeatureType.Axis1D:
					builder.AddControl(featureName).WithLayout("Analog").WithRange(-1f, 1f)
						.WithByteOffset(currentOffset)
						.WithFormat(InputStateBlock.FormatFloat)
						.WithUsages(currentUsages);
					break;
				case FeatureType.Axis2D:
					builder.AddControl(featureName).WithLayout("Stick").WithByteOffset(currentOffset)
						.WithFormat(InputStateBlock.FormatVector2)
						.WithUsages(currentUsages);
					builder.AddControl(featureName + "/x").WithLayout("Analog").WithRange(-1f, 1f);
					builder.AddControl(featureName + "/y").WithLayout("Analog").WithRange(-1f, 1f);
					break;
				case FeatureType.Axis3D:
					builder.AddControl(featureName).WithLayout("Vector3").WithByteOffset(currentOffset)
						.WithFormat(InputStateBlock.FormatVector3)
						.WithUsages(currentUsages);
					break;
				case FeatureType.Rotation:
					builder.AddControl(featureName).WithLayout("Quaternion").WithByteOffset(currentOffset)
						.WithFormat(InputStateBlock.FormatQuaternion)
						.WithUsages(currentUsages);
					break;
				case FeatureType.Bone:
					builder.AddControl(featureName).WithLayout("Bone").WithByteOffset(currentOffset)
						.WithUsages(currentUsages);
					break;
				case FeatureType.Eyes:
					builder.AddControl(featureName).WithLayout("Eyes").WithByteOffset(currentOffset)
						.WithUsages(currentUsages);
					break;
				}
				currentOffset += nextOffset;
			}
			return builder.Build();
		}

		// Token: 0x0400056B RID: 1387
		private string parentLayout;

		// Token: 0x0400056C RID: 1388
		private string interfaceName;

		// Token: 0x0400056D RID: 1389
		private XRDeviceDescriptor descriptor;

		// Token: 0x0400056E RID: 1390
		private static readonly string[] poseSubControlNames = new string[] { "/isTracked", "/trackingState", "/position", "/rotation", "/velocity", "/angularVelocity" };

		// Token: 0x0400056F RID: 1391
		private static readonly FeatureType[] poseSubControlTypes = new FeatureType[]
		{
			FeatureType.Binary,
			FeatureType.DiscreteStates,
			FeatureType.Axis3D,
			FeatureType.Rotation,
			FeatureType.Axis3D,
			FeatureType.Axis3D
		};
	}
}
