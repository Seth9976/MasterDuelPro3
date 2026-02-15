using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Pool;

namespace UnityEngine.InputSystem.HID
{
	// Token: 0x02000137 RID: 311
	public class HID : InputDevice
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x000480F0 File Offset: 0x000462F0
		public static FourCC QueryHIDReportDescriptorDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'D');
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x000480FF File Offset: 0x000462FF
		public static FourCC QueryHIDReportDescriptorSizeDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'S');
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0004810E File Offset: 0x0004630E
		public static FourCC QueryHIDParsedReportDescriptorDeviceCommandType
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'P');
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00048120 File Offset: 0x00046320
		public HID.HIDDeviceDescriptor hidDescriptor
		{
			get
			{
				if (!this.m_HaveParsedHIDDescriptor)
				{
					if (!string.IsNullOrEmpty(base.description.capabilities))
					{
						this.m_HIDDescriptor = JsonUtility.FromJson<HID.HIDDeviceDescriptor>(base.description.capabilities);
					}
					this.m_HaveParsedHIDDescriptor = true;
				}
				return this.m_HIDDescriptor;
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00048170 File Offset: 0x00046370
		internal static string OnFindLayoutForDevice(ref InputDeviceDescription description, string matchedLayout, InputDeviceExecuteCommandDelegate executeDeviceCommand)
		{
			if (!string.IsNullOrEmpty(matchedLayout))
			{
				return null;
			}
			if (description.interfaceName != "HID")
			{
				return null;
			}
			HID.HIDDeviceDescriptor hidDeviceDescriptor = HID.ReadHIDDeviceDescriptor(ref description, executeDeviceCommand);
			if (!HIDSupport.supportedHIDUsages.Contains(new HIDSupport.HIDPageUsage(hidDeviceDescriptor.usagePage, hidDeviceDescriptor.usage)))
			{
				return null;
			}
			bool hasUsableElements = false;
			if (hidDeviceDescriptor.elements != null)
			{
				foreach (HID.HIDElementDescriptor element in hidDeviceDescriptor.elements)
				{
					if (element.IsUsableElement())
					{
						hasUsableElements = true;
						break;
					}
				}
			}
			if (!hasUsableElements)
			{
				return null;
			}
			Type baseType = typeof(HID);
			string baseLayout = "HID";
			if (hidDeviceDescriptor.usagePage == HID.UsagePage.GenericDesktop && (hidDeviceDescriptor.usage == 4 || hidDeviceDescriptor.usage == 5))
			{
				baseLayout = "Joystick";
				baseType = typeof(Joystick);
			}
			string usageName = "";
			if (baseLayout != "Joystick")
			{
				usageName = ((hidDeviceDescriptor.usagePage == HID.UsagePage.GenericDesktop) ? string.Format(" {0}", (HID.GenericDesktop)hidDeviceDescriptor.usage) : string.Format(" {0}-{1}", hidDeviceDescriptor.usagePage, hidDeviceDescriptor.usage));
			}
			InputDeviceMatcher deviceMatcher = InputDeviceMatcher.FromDeviceDescription(description);
			string layoutName;
			if (!string.IsNullOrEmpty(description.product) && !string.IsNullOrEmpty(description.manufacturer))
			{
				layoutName = string.Concat(new string[] { "HID::", description.manufacturer, " ", description.product, usageName });
			}
			else if (!string.IsNullOrEmpty(description.product))
			{
				layoutName = "HID::" + description.product + usageName;
			}
			else
			{
				if (hidDeviceDescriptor.vendorId == 0)
				{
					return null;
				}
				layoutName = string.Format("{0}::{1:X}-{2:X}{3}", new object[] { "HID", hidDeviceDescriptor.vendorId, hidDeviceDescriptor.productId, usageName });
				deviceMatcher = deviceMatcher.WithCapability<int>("productId", hidDeviceDescriptor.productId).WithCapability<int>("vendorId", hidDeviceDescriptor.vendorId);
			}
			deviceMatcher = deviceMatcher.WithCapability<int>("usage", hidDeviceDescriptor.usage).WithCapability<HID.UsagePage>("usagePage", hidDeviceDescriptor.usagePage);
			HID.HIDLayoutBuilder layout = new HID.HIDLayoutBuilder
			{
				displayName = description.product,
				hidDescriptor = hidDeviceDescriptor,
				parentLayout = baseLayout,
				deviceType = (baseType ?? typeof(HID))
			};
			InputSystem.RegisterLayoutBuilder(() => layout.Build(), layoutName, baseLayout, new InputDeviceMatcher?(deviceMatcher));
			return layoutName;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00048418 File Offset: 0x00046618
		internal unsafe static HID.HIDDeviceDescriptor ReadHIDDeviceDescriptor(ref InputDeviceDescription deviceDescription, InputDeviceExecuteCommandDelegate executeCommandDelegate)
		{
			if (deviceDescription.interfaceName != "HID")
			{
				throw new ArgumentException(string.Format("Device '{0}' is not a HID", deviceDescription));
			}
			bool needToRequestDescriptor = true;
			HID.HIDDeviceDescriptor hidDeviceDescriptor = default(HID.HIDDeviceDescriptor);
			if (!string.IsNullOrEmpty(deviceDescription.capabilities))
			{
				try
				{
					hidDeviceDescriptor = HID.HIDDeviceDescriptor.FromJson(deviceDescription.capabilities);
					if (hidDeviceDescriptor.elements != null && hidDeviceDescriptor.elements.Length != 0)
					{
						needToRequestDescriptor = false;
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("Could not parse HID descriptor of device '{0}'", deviceDescription));
					Debug.LogException(ex);
				}
			}
			if (needToRequestDescriptor)
			{
				InputDeviceCommand sizeOfDescriptorCommand = new InputDeviceCommand(HID.QueryHIDReportDescriptorSizeDeviceCommandType, 8);
				long sizeOfDescriptorInBytes = executeCommandDelegate(ref sizeOfDescriptorCommand);
				if (sizeOfDescriptorInBytes > 0L)
				{
					using (NativeArray<byte> buffer = InputDeviceCommand.AllocateNative(HID.QueryHIDReportDescriptorDeviceCommandType, (int)sizeOfDescriptorInBytes))
					{
						InputDeviceCommand* commandPtr = (InputDeviceCommand*)buffer.GetUnsafePtr<byte>();
						if (executeCommandDelegate(ref *commandPtr) != sizeOfDescriptorInBytes)
						{
							HID.HIDDeviceDescriptor hiddeviceDescriptor = default(HID.HIDDeviceDescriptor);
							return hiddeviceDescriptor;
						}
						if (!HIDParser.ParseReportDescriptor((byte*)commandPtr->payloadPtr, (int)sizeOfDescriptorInBytes, ref hidDeviceDescriptor))
						{
							return default(HID.HIDDeviceDescriptor);
						}
					}
					deviceDescription.capabilities = hidDeviceDescriptor.ToJson();
					return hidDeviceDescriptor;
				}
				using (NativeArray<byte> buffer2 = InputDeviceCommand.AllocateNative(HID.QueryHIDParsedReportDescriptorDeviceCommandType, 2097152))
				{
					InputDeviceCommand* commandPtr2 = (InputDeviceCommand*)buffer2.GetUnsafePtr<byte>();
					long utf8Length = executeCommandDelegate(ref *commandPtr2);
					if (utf8Length < 0L)
					{
						return default(HID.HIDDeviceDescriptor);
					}
					byte[] utf8 = new byte[utf8Length];
					try
					{
						byte[] array;
						byte* utf8Ptr;
						if ((array = utf8) == null || array.Length == 0)
						{
							utf8Ptr = null;
						}
						else
						{
							utf8Ptr = &array[0];
						}
						UnsafeUtility.MemCpy((void*)utf8Ptr, commandPtr2->payloadPtr, utf8Length);
					}
					finally
					{
						byte[] array = null;
					}
					string descriptorJson = Encoding.UTF8.GetString(utf8, 0, (int)utf8Length);
					try
					{
						hidDeviceDescriptor = HID.HIDDeviceDescriptor.FromJson(descriptorJson);
					}
					catch (Exception ex2)
					{
						Debug.LogError(string.Format("Could not parse HID descriptor of device '{0}'", deviceDescription));
						Debug.LogException(ex2);
						return default(HID.HIDDeviceDescriptor);
					}
					deviceDescription.capabilities = descriptorJson;
				}
				return hidDeviceDescriptor;
			}
			return hidDeviceDescriptor;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00048664 File Offset: 0x00046864
		public static string UsagePageToString(HID.UsagePage usagePage)
		{
			if (usagePage < HID.UsagePage.VendorDefined)
			{
				return usagePage.ToString();
			}
			return "Vendor-Defined";
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00048684 File Offset: 0x00046884
		public static string UsageToString(HID.UsagePage usagePage, int usage)
		{
			if (usagePage == HID.UsagePage.GenericDesktop)
			{
				HID.GenericDesktop genericDesktop = (HID.GenericDesktop)usage;
				return genericDesktop.ToString();
			}
			if (usagePage != HID.UsagePage.Simulation)
			{
				return null;
			}
			HID.Simulation simulation = (HID.Simulation)usage;
			return simulation.ToString();
		}

		// Token: 0x04000737 RID: 1847
		internal const string kHIDInterface = "HID";

		// Token: 0x04000738 RID: 1848
		internal const string kHIDNamespace = "HID";

		// Token: 0x04000739 RID: 1849
		private bool m_HaveParsedHIDDescriptor;

		// Token: 0x0400073A RID: 1850
		private HID.HIDDeviceDescriptor m_HIDDescriptor;

		// Token: 0x0400073B RID: 1851
		private static readonly ProfilerMarker k_HIDParseDescriptorFallback = new ProfilerMarker("HIDParseDescriptorFallback");

		// Token: 0x02000138 RID: 312
		[Serializable]
		private class HIDLayoutBuilder
		{
			// Token: 0x06000E53 RID: 3667 RVA: 0x000486D0 File Offset: 0x000468D0
			public InputControlLayout Build()
			{
				InputControlLayout.Builder builder = new InputControlLayout.Builder
				{
					displayName = this.displayName,
					type = this.deviceType,
					extendsLayout = this.parentLayout,
					stateFormat = new FourCC('H', 'I', 'D', ' ')
				};
				HID.HIDElementDescriptor xElement = Array.Find<HID.HIDElementDescriptor>(this.hidDescriptor.elements, (HID.HIDElementDescriptor element) => element.usagePage == HID.UsagePage.GenericDesktop && element.usage == 48);
				HID.HIDElementDescriptor yElement = Array.Find<HID.HIDElementDescriptor>(this.hidDescriptor.elements, (HID.HIDElementDescriptor element) => element.usagePage == HID.UsagePage.GenericDesktop && element.usage == 49);
				bool haveStick = xElement.usage == 48 && yElement.usage == 49;
				if (haveStick)
				{
					int bitOffset;
					int byteOffset;
					int sizeInBits;
					if (xElement.reportOffsetInBits <= yElement.reportOffsetInBits)
					{
						bitOffset = xElement.reportOffsetInBits % 8;
						byteOffset = xElement.reportOffsetInBits / 8;
						sizeInBits = yElement.reportOffsetInBits + yElement.reportSizeInBits - xElement.reportOffsetInBits;
					}
					else
					{
						bitOffset = yElement.reportOffsetInBits % 8;
						byteOffset = yElement.reportOffsetInBits / 8;
						sizeInBits = xElement.reportOffsetInBits + xElement.reportSizeInBits - yElement.reportSizeInBits;
					}
					InputControlLayout.Builder.ControlBuilder controlBuilder = builder.AddControl("stick");
					controlBuilder = controlBuilder.WithDisplayName("Stick");
					controlBuilder = controlBuilder.WithLayout("Stick");
					controlBuilder = controlBuilder.WithBitOffset((uint)bitOffset);
					controlBuilder = controlBuilder.WithByteOffset((uint)byteOffset);
					controlBuilder = controlBuilder.WithSizeInBits((uint)sizeInBits);
					controlBuilder.WithUsages(new InternedString[] { CommonUsages.Primary2DMotion });
					string xElementParameters = xElement.DetermineParameters();
					string yElementParameters = yElement.DetermineParameters();
					controlBuilder = builder.AddControl("stick/x");
					controlBuilder = controlBuilder.WithFormat(xElement.isSigned ? InputStateBlock.FormatSBit : InputStateBlock.FormatBit);
					controlBuilder = controlBuilder.WithByteOffset((uint)(xElement.reportOffsetInBits / 8 - byteOffset));
					controlBuilder = controlBuilder.WithBitOffset((uint)(xElement.reportOffsetInBits % 8));
					controlBuilder = controlBuilder.WithSizeInBits((uint)xElement.reportSizeInBits);
					controlBuilder = controlBuilder.WithParameters(xElementParameters);
					controlBuilder = controlBuilder.WithDefaultState(xElement.DetermineDefaultState());
					controlBuilder.WithProcessors(xElement.DetermineProcessors());
					controlBuilder = builder.AddControl("stick/y");
					controlBuilder = controlBuilder.WithFormat(yElement.isSigned ? InputStateBlock.FormatSBit : InputStateBlock.FormatBit);
					controlBuilder = controlBuilder.WithByteOffset((uint)(yElement.reportOffsetInBits / 8 - byteOffset));
					controlBuilder = controlBuilder.WithBitOffset((uint)(yElement.reportOffsetInBits % 8));
					controlBuilder = controlBuilder.WithSizeInBits((uint)yElement.reportSizeInBits);
					controlBuilder = controlBuilder.WithParameters(yElementParameters);
					controlBuilder = controlBuilder.WithDefaultState(yElement.DetermineDefaultState());
					controlBuilder.WithProcessors(yElement.DetermineProcessors());
					controlBuilder = builder.AddControl("stick/up");
					controlBuilder.WithParameters(StringHelpers.Join<string>(",", new string[] { yElementParameters, "clamp=2,clampMin=-1,clampMax=0,invert=true" }));
					controlBuilder = builder.AddControl("stick/down");
					controlBuilder.WithParameters(StringHelpers.Join<string>(",", new string[] { yElementParameters, "clamp=2,clampMin=0,clampMax=1,invert=false" }));
					controlBuilder = builder.AddControl("stick/left");
					controlBuilder.WithParameters(StringHelpers.Join<string>(",", new string[] { xElementParameters, "clamp=2,clampMin=-1,clampMax=0,invert" }));
					controlBuilder = builder.AddControl("stick/right");
					controlBuilder.WithParameters(StringHelpers.Join<string>(",", new string[] { xElementParameters, "clamp=2,clampMin=0,clampMax=1" }));
				}
				HID.HIDElementDescriptor[] elements = this.hidDescriptor.elements;
				int elementCount = elements.Length;
				for (int i = 0; i < elementCount; i++)
				{
					ref HID.HIDElementDescriptor element2 = ref elements[i];
					if (element2.reportType == HID.HIDReportType.Input && (!haveStick || (!element2.Is(HID.UsagePage.GenericDesktop, 48) && !element2.Is(HID.UsagePage.GenericDesktop, 49))))
					{
						string layout = element2.DetermineLayout();
						if (layout != null)
						{
							string name = element2.DetermineName();
							name = StringHelpers.MakeUniqueName<InputControlLayout.ControlItem>(name, builder.controls, (InputControlLayout.ControlItem x) => x.name);
							InputControlLayout.Builder.ControlBuilder controlBuilder = builder.AddControl(name);
							controlBuilder = controlBuilder.WithDisplayName(element2.DetermineDisplayName());
							controlBuilder = controlBuilder.WithLayout(layout);
							controlBuilder = controlBuilder.WithByteOffset((uint)(element2.reportOffsetInBits / 8));
							controlBuilder = controlBuilder.WithBitOffset((uint)(element2.reportOffsetInBits % 8));
							controlBuilder = controlBuilder.WithSizeInBits((uint)element2.reportSizeInBits);
							controlBuilder = controlBuilder.WithFormat(element2.DetermineFormat());
							controlBuilder = controlBuilder.WithDefaultState(element2.DetermineDefaultState());
							InputControlLayout.Builder.ControlBuilder control = controlBuilder.WithProcessors(element2.DetermineProcessors());
							string parameters = element2.DetermineParameters();
							if (!string.IsNullOrEmpty(parameters))
							{
								control.WithParameters(parameters);
							}
							InternedString[] usages = element2.DetermineUsages();
							if (usages != null)
							{
								control.WithUsages(usages);
							}
							element2.AddChildControls(ref element2, name, ref builder);
						}
					}
				}
				return builder.Build();
			}

			// Token: 0x0400073C RID: 1852
			public string displayName;

			// Token: 0x0400073D RID: 1853
			public HID.HIDDeviceDescriptor hidDescriptor;

			// Token: 0x0400073E RID: 1854
			public string parentLayout;

			// Token: 0x0400073F RID: 1855
			public Type deviceType;
		}

		// Token: 0x0200013A RID: 314
		public enum HIDReportType
		{
			// Token: 0x04000745 RID: 1861
			Unknown,
			// Token: 0x04000746 RID: 1862
			Input,
			// Token: 0x04000747 RID: 1863
			Output,
			// Token: 0x04000748 RID: 1864
			Feature
		}

		// Token: 0x0200013B RID: 315
		public enum HIDCollectionType
		{
			// Token: 0x0400074A RID: 1866
			Physical,
			// Token: 0x0400074B RID: 1867
			Application,
			// Token: 0x0400074C RID: 1868
			Logical,
			// Token: 0x0400074D RID: 1869
			Report,
			// Token: 0x0400074E RID: 1870
			NamedArray,
			// Token: 0x0400074F RID: 1871
			UsageSwitch,
			// Token: 0x04000750 RID: 1872
			UsageModifier
		}

		// Token: 0x0200013C RID: 316
		[Flags]
		public enum HIDElementFlags
		{
			// Token: 0x04000752 RID: 1874
			Constant = 1,
			// Token: 0x04000753 RID: 1875
			Variable = 2,
			// Token: 0x04000754 RID: 1876
			Relative = 4,
			// Token: 0x04000755 RID: 1877
			Wrap = 8,
			// Token: 0x04000756 RID: 1878
			NonLinear = 16,
			// Token: 0x04000757 RID: 1879
			NoPreferred = 32,
			// Token: 0x04000758 RID: 1880
			NullState = 64,
			// Token: 0x04000759 RID: 1881
			Volatile = 128,
			// Token: 0x0400075A RID: 1882
			BufferedBytes = 256
		}

		// Token: 0x0200013D RID: 317
		[Serializable]
		public struct HIDElementDescriptor
		{
			// Token: 0x170003CD RID: 973
			// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00048C12 File Offset: 0x00046E12
			public bool hasNullState
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NullState) == HID.HIDElementFlags.NullState;
				}
			}

			// Token: 0x170003CE RID: 974
			// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00048C21 File Offset: 0x00046E21
			public bool hasPreferredState
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NoPreferred) != HID.HIDElementFlags.NoPreferred;
				}
			}

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00048C33 File Offset: 0x00046E33
			public bool isArray
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Variable) != HID.HIDElementFlags.Variable;
				}
			}

			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00048C43 File Offset: 0x00046E43
			public bool isNonLinear
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.NonLinear) == HID.HIDElementFlags.NonLinear;
				}
			}

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00048C52 File Offset: 0x00046E52
			public bool isRelative
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Relative) == HID.HIDElementFlags.Relative;
				}
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00048C5F File Offset: 0x00046E5F
			public bool isConstant
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Constant) == HID.HIDElementFlags.Constant;
				}
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00048C6C File Offset: 0x00046E6C
			public bool isWrapping
			{
				get
				{
					return (this.flags & HID.HIDElementFlags.Wrap) == HID.HIDElementFlags.Wrap;
				}
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00048C79 File Offset: 0x00046E79
			internal bool isSigned
			{
				get
				{
					return this.logicalMin < 0;
				}
			}

			// Token: 0x170003D5 RID: 981
			// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00048C84 File Offset: 0x00046E84
			internal float minFloatValue
			{
				get
				{
					if (this.isSigned)
					{
						int minValue = (int)(-(int)((int)1L << this.reportSizeInBits - 1));
						int maxValue = (int)((1L << this.reportSizeInBits - 1) - 1L);
						return NumberHelpers.IntToNormalizedFloat(this.logicalMin, minValue, maxValue) * 2f - 1f;
					}
					uint maxValue2 = (uint)((1L << this.reportSizeInBits) - 1L);
					return NumberHelpers.UIntToNormalizedFloat((uint)this.logicalMin, 0U, maxValue2);
				}
			}

			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00048CF8 File Offset: 0x00046EF8
			internal float maxFloatValue
			{
				get
				{
					if (this.isSigned)
					{
						int minValue = (int)(-(int)((int)1L << this.reportSizeInBits - 1));
						int maxValue = (int)((1L << this.reportSizeInBits - 1) - 1L);
						return NumberHelpers.IntToNormalizedFloat(this.logicalMax, minValue, maxValue) * 2f - 1f;
					}
					uint maxValue2 = (uint)((1L << this.reportSizeInBits) - 1L);
					return NumberHelpers.UIntToNormalizedFloat((uint)this.logicalMax, 0U, maxValue2);
				}
			}

			// Token: 0x06000E64 RID: 3684 RVA: 0x00048D69 File Offset: 0x00046F69
			public bool Is(HID.UsagePage usagePage, int usage)
			{
				return usagePage == this.usagePage && usage == this.usage;
			}

			// Token: 0x06000E65 RID: 3685 RVA: 0x00048D80 File Offset: 0x00046F80
			internal string DetermineName()
			{
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage != HID.UsagePage.GenericDesktop)
				{
					if (usagePage != HID.UsagePage.Button)
					{
						return string.Format("UsagePage({0:X}) Usage({1:X})", this.usagePage, this.usage);
					}
					if (this.usage == 1)
					{
						return "trigger";
					}
					return string.Format("button{0}", this.usage);
				}
				else
				{
					if (this.usage == 57)
					{
						return "hat";
					}
					HID.GenericDesktop genericDesktop = (HID.GenericDesktop)this.usage;
					string text = genericDesktop.ToString();
					return char.ToLowerInvariant(text[0]).ToString() + text.Substring(1);
				}
			}

			// Token: 0x06000E66 RID: 3686 RVA: 0x00048E2C File Offset: 0x0004702C
			internal string DetermineDisplayName()
			{
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage == HID.UsagePage.GenericDesktop)
				{
					HID.GenericDesktop genericDesktop = (HID.GenericDesktop)this.usage;
					return genericDesktop.ToString();
				}
				if (usagePage != HID.UsagePage.Button)
				{
					return null;
				}
				if (this.usage == 1)
				{
					return "Trigger";
				}
				return string.Format("Button {0}", this.usage);
			}

			// Token: 0x06000E67 RID: 3687 RVA: 0x00048E84 File Offset: 0x00047084
			internal bool IsUsableElement()
			{
				int num = this.usage;
				if (num - 48 <= 1)
				{
					return this.usagePage == HID.UsagePage.GenericDesktop;
				}
				return this.DetermineLayout() != null;
			}

			// Token: 0x06000E68 RID: 3688 RVA: 0x00048EB4 File Offset: 0x000470B4
			internal string DetermineLayout()
			{
				if (this.reportType != HID.HIDReportType.Input)
				{
					return null;
				}
				HID.UsagePage usagePage = this.usagePage;
				if (usagePage == HID.UsagePage.GenericDesktop)
				{
					int num = this.usage;
					switch (num)
					{
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
						return "Axis";
					case 57:
						if (this.logicalMax - this.logicalMin + 1 == 8)
						{
							return "Dpad";
						}
						goto IL_00BC;
					case 58:
					case 59:
					case 60:
					case 63:
						goto IL_00BC;
					case 61:
					case 62:
						break;
					default:
						if (num - 144 > 3)
						{
							goto IL_00BC;
						}
						break;
					}
					return "Button";
				}
				if (usagePage == HID.UsagePage.Button)
				{
					return "Button";
				}
				IL_00BC:
				return null;
			}

			// Token: 0x06000E69 RID: 3689 RVA: 0x00048F80 File Offset: 0x00047180
			internal FourCC DetermineFormat()
			{
				int num = this.reportSizeInBits;
				if (num != 8)
				{
					if (num != 16)
					{
						if (num != 32)
						{
							return InputStateBlock.FormatBit;
						}
						if (!this.isSigned)
						{
							return InputStateBlock.FormatUInt;
						}
						return InputStateBlock.FormatInt;
					}
					else
					{
						if (!this.isSigned)
						{
							return InputStateBlock.FormatUShort;
						}
						return InputStateBlock.FormatShort;
					}
				}
				else
				{
					if (!this.isSigned)
					{
						return InputStateBlock.FormatByte;
					}
					return InputStateBlock.FormatSByte;
				}
			}

			// Token: 0x06000E6A RID: 3690 RVA: 0x00048FE8 File Offset: 0x000471E8
			internal InternedString[] DetermineUsages()
			{
				if (this.usagePage == HID.UsagePage.Button && this.usage == 1)
				{
					return new InternedString[]
					{
						CommonUsages.PrimaryTrigger,
						CommonUsages.PrimaryAction
					};
				}
				if (this.usagePage == HID.UsagePage.Button && this.usage == 2)
				{
					return new InternedString[]
					{
						CommonUsages.SecondaryTrigger,
						CommonUsages.SecondaryAction
					};
				}
				if (this.usagePage == HID.UsagePage.GenericDesktop && this.usage == 53)
				{
					return new InternedString[] { CommonUsages.Twist };
				}
				return null;
			}

			// Token: 0x06000E6B RID: 3691 RVA: 0x00049080 File Offset: 0x00047280
			internal string DetermineParameters()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					switch (this.usage)
					{
					case 48:
					case 50:
					case 51:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 66:
					case 67:
					case 69:
						return this.DetermineAxisNormalizationParameters();
					case 49:
					case 52:
					case 65:
					case 68:
						return StringHelpers.Join<string>(",", new string[]
						{
							"invert",
							this.DetermineAxisNormalizationParameters()
						});
					}
				}
				return null;
			}

			// Token: 0x06000E6C RID: 3692 RVA: 0x00049130 File Offset: 0x00047330
			private string DetermineAxisNormalizationParameters()
			{
				if (this.logicalMin == 0 && this.logicalMax == 0)
				{
					return "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5";
				}
				float min = this.minFloatValue;
				float max = this.maxFloatValue;
				if (Mathf.Approximately(0f, min) && Mathf.Approximately(0f, max))
				{
					return null;
				}
				float zero = min + (max - min) / 2f;
				return string.Format(CultureInfo.InvariantCulture, "normalize,normalizeMin={0},normalizeMax={1},normalizeZero={2}", min, max, zero);
			}

			// Token: 0x06000E6D RID: 3693 RVA: 0x000491AC File Offset: 0x000473AC
			internal string DetermineProcessors()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					int num = this.usage;
					if (num - 48 <= 8 || num - 64 <= 5)
					{
						return "axisDeadzone";
					}
				}
				return null;
			}

			// Token: 0x06000E6E RID: 3694 RVA: 0x000491E0 File Offset: 0x000473E0
			internal PrimitiveValue DetermineDefaultState()
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop)
				{
					switch (this.usage)
					{
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
						if (!this.isSigned)
						{
							int defaultValue = this.logicalMin + (this.logicalMax - this.logicalMin) / 2;
							if (defaultValue != 0)
							{
								return new PrimitiveValue(defaultValue);
							}
						}
						break;
					case 57:
						if (this.hasNullState)
						{
							if (this.logicalMin >= 1)
							{
								return new PrimitiveValue(this.logicalMin - 1);
							}
							ulong maxValue = (1UL << this.reportSizeInBits) - 1UL;
							if ((long)this.logicalMax < (long)maxValue)
							{
								return new PrimitiveValue(this.logicalMax + 1);
							}
						}
						break;
					}
				}
				return default(PrimitiveValue);
			}

			// Token: 0x06000E6F RID: 3695 RVA: 0x000492DC File Offset: 0x000474DC
			internal void AddChildControls(ref HID.HIDElementDescriptor element, string controlName, ref InputControlLayout.Builder builder)
			{
				if (this.usagePage == HID.UsagePage.GenericDesktop && this.usage == 57)
				{
					PrimitiveValue nullValue = this.DetermineDefaultState();
					if (nullValue.isEmpty)
					{
						return;
					}
					builder.AddControl(controlName + "/up").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1},nullValue={2},wrapAtValue={3}", new object[]
						{
							this.logicalMax,
							this.logicalMin + 1,
							nullValue.ToString(),
							this.logicalMax
						}))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/right").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 1, this.logicalMin + 3))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/down").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 3, this.logicalMin + 5))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
					builder.AddControl(controlName + "/left").WithFormat(InputStateBlock.FormatBit).WithLayout("DiscreteButton")
						.WithParameters(string.Format(CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", this.logicalMin + 5, this.logicalMin + 7))
						.WithBitOffset((uint)(element.reportOffsetInBits % 8))
						.WithSizeInBits((uint)this.reportSizeInBits);
				}
			}

			// Token: 0x0400075B RID: 1883
			public int usage;

			// Token: 0x0400075C RID: 1884
			public HID.UsagePage usagePage;

			// Token: 0x0400075D RID: 1885
			public int unit;

			// Token: 0x0400075E RID: 1886
			public int unitExponent;

			// Token: 0x0400075F RID: 1887
			public int logicalMin;

			// Token: 0x04000760 RID: 1888
			public int logicalMax;

			// Token: 0x04000761 RID: 1889
			public int physicalMin;

			// Token: 0x04000762 RID: 1890
			public int physicalMax;

			// Token: 0x04000763 RID: 1891
			public HID.HIDReportType reportType;

			// Token: 0x04000764 RID: 1892
			public int collectionIndex;

			// Token: 0x04000765 RID: 1893
			public int reportId;

			// Token: 0x04000766 RID: 1894
			public int reportSizeInBits;

			// Token: 0x04000767 RID: 1895
			public int reportOffsetInBits;

			// Token: 0x04000768 RID: 1896
			public HID.HIDElementFlags flags;

			// Token: 0x04000769 RID: 1897
			public int? usageMin;

			// Token: 0x0400076A RID: 1898
			public int? usageMax;
		}

		// Token: 0x0200013E RID: 318
		[Serializable]
		public struct HIDCollectionDescriptor
		{
			// Token: 0x0400076B RID: 1899
			public HID.HIDCollectionType type;

			// Token: 0x0400076C RID: 1900
			public int usage;

			// Token: 0x0400076D RID: 1901
			public HID.UsagePage usagePage;

			// Token: 0x0400076E RID: 1902
			public int parent;

			// Token: 0x0400076F RID: 1903
			public int childCount;

			// Token: 0x04000770 RID: 1904
			public int firstChild;
		}

		// Token: 0x0200013F RID: 319
		[Serializable]
		public struct HIDDeviceDescriptor
		{
			// Token: 0x06000E70 RID: 3696 RVA: 0x0004952B File Offset: 0x0004772B
			public string ToJson()
			{
				return JsonUtility.ToJson(this, true);
			}

			// Token: 0x06000E71 RID: 3697 RVA: 0x00049540 File Offset: 0x00047740
			public static HID.HIDDeviceDescriptor FromJson(string json)
			{
				HID.HIDDeviceDescriptor hiddeviceDescriptor;
				try
				{
					HID.HIDDeviceDescriptor descriptor = default(HID.HIDDeviceDescriptor);
					ReadOnlySpan<char> jsonSpan = json.AsSpan();
					PredictiveParser parser = default(PredictiveParser);
					parser.ExpectSingleChar(jsonSpan, '{');
					ReadOnlySpan<char> readOnlySpan;
					parser.AcceptString(jsonSpan, out readOnlySpan);
					parser.ExpectSingleChar(jsonSpan, ':');
					descriptor.vendorId = parser.ExpectInt(jsonSpan);
					parser.AcceptSingleChar(jsonSpan, ',');
					parser.AcceptString(jsonSpan, out readOnlySpan);
					parser.ExpectSingleChar(jsonSpan, ':');
					descriptor.productId = parser.ExpectInt(jsonSpan);
					parser.AcceptSingleChar(jsonSpan, ',');
					parser.AcceptString(jsonSpan, out readOnlySpan);
					parser.ExpectSingleChar(jsonSpan, ':');
					descriptor.usage = parser.ExpectInt(jsonSpan);
					parser.AcceptSingleChar(jsonSpan, ',');
					parser.AcceptString(jsonSpan, out readOnlySpan);
					parser.ExpectSingleChar(jsonSpan, ':');
					descriptor.usagePage = (HID.UsagePage)parser.ExpectInt(jsonSpan);
					parser.AcceptSingleChar(jsonSpan, ',');
					parser.AcceptString(jsonSpan, out readOnlySpan);
					parser.ExpectSingleChar(jsonSpan, ':');
					descriptor.inputReportSize = parser.ExpectInt(jsonSpan);
					parser.AcceptSingleChar(jsonSpan, ',');
					parser.AcceptString(jsonSpan, out readOnlySpan);
					parser.ExpectSingleChar(jsonSpan, ':');
					descriptor.outputReportSize = parser.ExpectInt(jsonSpan);
					parser.AcceptSingleChar(jsonSpan, ',');
					parser.AcceptString(jsonSpan, out readOnlySpan);
					parser.ExpectSingleChar(jsonSpan, ':');
					descriptor.featureReportSize = parser.ExpectInt(jsonSpan);
					parser.AcceptSingleChar(jsonSpan, ',');
					ReadOnlySpan<char> key;
					parser.AcceptString(jsonSpan, out key);
					if (key.ToString() != "elements")
					{
						hiddeviceDescriptor = descriptor;
					}
					else
					{
						parser.ExpectSingleChar(jsonSpan, ':');
						parser.ExpectSingleChar(jsonSpan, '[');
						List<HID.HIDElementDescriptor> elements;
						using (CollectionPool<List<HID.HIDElementDescriptor>, HID.HIDElementDescriptor>.Get(out elements))
						{
							while (!parser.AcceptSingleChar(jsonSpan, ']'))
							{
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectSingleChar(jsonSpan, '{');
								HID.HIDElementDescriptor elementDesc = default(HID.HIDElementDescriptor);
								parser.AcceptSingleChar(jsonSpan, '}');
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.usage = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.usagePage = (HID.UsagePage)parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.unit = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.unitExponent = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.logicalMin = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.logicalMax = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.physicalMin = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.physicalMax = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.collectionIndex = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.reportType = (HID.HIDReportType)parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.reportId = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								parser.AcceptInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.reportSizeInBits = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.reportOffsetInBits = parser.ExpectInt(jsonSpan);
								parser.AcceptSingleChar(jsonSpan, ',');
								parser.ExpectString(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, ':');
								elementDesc.flags = (HID.HIDElementFlags)parser.ExpectInt(jsonSpan);
								parser.ExpectSingleChar(jsonSpan, '}');
								elements.Add(elementDesc);
							}
							descriptor.elements = elements.ToArray();
							hiddeviceDescriptor = descriptor;
						}
					}
				}
				catch (Exception)
				{
					hiddeviceDescriptor = JsonUtility.FromJson<HID.HIDDeviceDescriptor>(json);
				}
				return hiddeviceDescriptor;
			}

			// Token: 0x04000771 RID: 1905
			public int vendorId;

			// Token: 0x04000772 RID: 1906
			public int productId;

			// Token: 0x04000773 RID: 1907
			public int usage;

			// Token: 0x04000774 RID: 1908
			public HID.UsagePage usagePage;

			// Token: 0x04000775 RID: 1909
			public int inputReportSize;

			// Token: 0x04000776 RID: 1910
			public int outputReportSize;

			// Token: 0x04000777 RID: 1911
			public int featureReportSize;

			// Token: 0x04000778 RID: 1912
			public HID.HIDElementDescriptor[] elements;

			// Token: 0x04000779 RID: 1913
			public HID.HIDCollectionDescriptor[] collections;
		}

		// Token: 0x02000140 RID: 320
		public struct HIDDeviceDescriptorBuilder
		{
			// Token: 0x06000E72 RID: 3698 RVA: 0x00049A50 File Offset: 0x00047C50
			public HIDDeviceDescriptorBuilder(HID.UsagePage usagePage, int usage)
			{
				this = default(HID.HIDDeviceDescriptorBuilder);
				this.usagePage = usagePage;
				this.usage = usage;
			}

			// Token: 0x06000E73 RID: 3699 RVA: 0x00049A67 File Offset: 0x00047C67
			public HIDDeviceDescriptorBuilder(HID.GenericDesktop usage)
			{
				this = new HID.HIDDeviceDescriptorBuilder(HID.UsagePage.GenericDesktop, (int)usage);
			}

			// Token: 0x06000E74 RID: 3700 RVA: 0x00049A71 File Offset: 0x00047C71
			public HID.HIDDeviceDescriptorBuilder StartReport(HID.HIDReportType reportType, int reportId = 1)
			{
				this.m_CurrentReportId = reportId;
				this.m_CurrentReportType = reportType;
				this.m_CurrentReportOffsetInBits = 8;
				return this;
			}

			// Token: 0x06000E75 RID: 3701 RVA: 0x00049A90 File Offset: 0x00047C90
			public HID.HIDDeviceDescriptorBuilder AddElement(HID.UsagePage usagePage, int usage, int sizeInBits)
			{
				if (this.m_Elements == null)
				{
					this.m_Elements = new List<HID.HIDElementDescriptor>();
				}
				else
				{
					foreach (HID.HIDElementDescriptor element in this.m_Elements)
					{
						if (element.reportId == this.m_CurrentReportId && element.reportType == this.m_CurrentReportType && element.usagePage == usagePage && element.usage == usage)
						{
							throw new InvalidOperationException(string.Format("Cannot add two elements with the same usage page '{0}' and usage '0x{1:X} the to same device", usagePage, usage));
						}
					}
				}
				this.m_Elements.Add(new HID.HIDElementDescriptor
				{
					usage = usage,
					usagePage = usagePage,
					reportOffsetInBits = this.m_CurrentReportOffsetInBits,
					reportSizeInBits = sizeInBits,
					reportType = this.m_CurrentReportType,
					reportId = this.m_CurrentReportId
				});
				this.m_CurrentReportOffsetInBits += sizeInBits;
				return this;
			}

			// Token: 0x06000E76 RID: 3702 RVA: 0x00049BA4 File Offset: 0x00047DA4
			public HID.HIDDeviceDescriptorBuilder AddElement(HID.GenericDesktop usage, int sizeInBits)
			{
				return this.AddElement(HID.UsagePage.GenericDesktop, (int)usage, sizeInBits);
			}

			// Token: 0x06000E77 RID: 3703 RVA: 0x00049BB0 File Offset: 0x00047DB0
			public HID.HIDDeviceDescriptorBuilder WithPhysicalMinMax(int min, int max)
			{
				int index = this.m_Elements.Count - 1;
				if (index < 0)
				{
					throw new InvalidOperationException("No element has been added to the descriptor yet");
				}
				HID.HIDElementDescriptor element = this.m_Elements[index];
				element.physicalMin = min;
				element.physicalMax = max;
				this.m_Elements[index] = element;
				return this;
			}

			// Token: 0x06000E78 RID: 3704 RVA: 0x00049C0C File Offset: 0x00047E0C
			public HID.HIDDeviceDescriptorBuilder WithLogicalMinMax(int min, int max)
			{
				int index = this.m_Elements.Count - 1;
				if (index < 0)
				{
					throw new InvalidOperationException("No element has been added to the descriptor yet");
				}
				HID.HIDElementDescriptor element = this.m_Elements[index];
				element.logicalMin = min;
				element.logicalMax = max;
				this.m_Elements[index] = element;
				return this;
			}

			// Token: 0x06000E79 RID: 3705 RVA: 0x00049C68 File Offset: 0x00047E68
			public HID.HIDDeviceDescriptor Finish()
			{
				HID.HIDDeviceDescriptor hiddeviceDescriptor = default(HID.HIDDeviceDescriptor);
				hiddeviceDescriptor.usage = this.usage;
				hiddeviceDescriptor.usagePage = this.usagePage;
				List<HID.HIDElementDescriptor> elements = this.m_Elements;
				hiddeviceDescriptor.elements = ((elements != null) ? elements.ToArray() : null);
				List<HID.HIDCollectionDescriptor> collections = this.m_Collections;
				hiddeviceDescriptor.collections = ((collections != null) ? collections.ToArray() : null);
				return hiddeviceDescriptor;
			}

			// Token: 0x0400077A RID: 1914
			public HID.UsagePage usagePage;

			// Token: 0x0400077B RID: 1915
			public int usage;

			// Token: 0x0400077C RID: 1916
			private int m_CurrentReportId;

			// Token: 0x0400077D RID: 1917
			private HID.HIDReportType m_CurrentReportType;

			// Token: 0x0400077E RID: 1918
			private int m_CurrentReportOffsetInBits;

			// Token: 0x0400077F RID: 1919
			private List<HID.HIDElementDescriptor> m_Elements;

			// Token: 0x04000780 RID: 1920
			private List<HID.HIDCollectionDescriptor> m_Collections;

			// Token: 0x04000781 RID: 1921
			private int m_InputReportSize;

			// Token: 0x04000782 RID: 1922
			private int m_OutputReportSize;

			// Token: 0x04000783 RID: 1923
			private int m_FeatureReportSize;
		}

		// Token: 0x02000141 RID: 321
		public enum UsagePage
		{
			// Token: 0x04000785 RID: 1925
			Undefined,
			// Token: 0x04000786 RID: 1926
			GenericDesktop,
			// Token: 0x04000787 RID: 1927
			Simulation,
			// Token: 0x04000788 RID: 1928
			VRControls,
			// Token: 0x04000789 RID: 1929
			SportControls,
			// Token: 0x0400078A RID: 1930
			GameControls,
			// Token: 0x0400078B RID: 1931
			GenericDeviceControls,
			// Token: 0x0400078C RID: 1932
			Keyboard,
			// Token: 0x0400078D RID: 1933
			LEDs,
			// Token: 0x0400078E RID: 1934
			Button,
			// Token: 0x0400078F RID: 1935
			Ordinal,
			// Token: 0x04000790 RID: 1936
			Telephony,
			// Token: 0x04000791 RID: 1937
			Consumer,
			// Token: 0x04000792 RID: 1938
			Digitizer,
			// Token: 0x04000793 RID: 1939
			PID = 15,
			// Token: 0x04000794 RID: 1940
			Unicode,
			// Token: 0x04000795 RID: 1941
			AlphanumericDisplay = 20,
			// Token: 0x04000796 RID: 1942
			MedicalInstruments = 64,
			// Token: 0x04000797 RID: 1943
			Monitor = 128,
			// Token: 0x04000798 RID: 1944
			Power = 132,
			// Token: 0x04000799 RID: 1945
			BarCodeScanner = 140,
			// Token: 0x0400079A RID: 1946
			MagneticStripeReader = 142,
			// Token: 0x0400079B RID: 1947
			Camera = 144,
			// Token: 0x0400079C RID: 1948
			Arcade,
			// Token: 0x0400079D RID: 1949
			VendorDefined = 65280
		}

		// Token: 0x02000142 RID: 322
		public enum GenericDesktop
		{
			// Token: 0x0400079F RID: 1951
			Undefined,
			// Token: 0x040007A0 RID: 1952
			Pointer,
			// Token: 0x040007A1 RID: 1953
			Mouse,
			// Token: 0x040007A2 RID: 1954
			Joystick = 4,
			// Token: 0x040007A3 RID: 1955
			Gamepad,
			// Token: 0x040007A4 RID: 1956
			Keyboard,
			// Token: 0x040007A5 RID: 1957
			Keypad,
			// Token: 0x040007A6 RID: 1958
			MultiAxisController,
			// Token: 0x040007A7 RID: 1959
			TabletPCControls,
			// Token: 0x040007A8 RID: 1960
			AssistiveControl,
			// Token: 0x040007A9 RID: 1961
			X = 48,
			// Token: 0x040007AA RID: 1962
			Y,
			// Token: 0x040007AB RID: 1963
			Z,
			// Token: 0x040007AC RID: 1964
			Rx,
			// Token: 0x040007AD RID: 1965
			Ry,
			// Token: 0x040007AE RID: 1966
			Rz,
			// Token: 0x040007AF RID: 1967
			Slider,
			// Token: 0x040007B0 RID: 1968
			Dial,
			// Token: 0x040007B1 RID: 1969
			Wheel,
			// Token: 0x040007B2 RID: 1970
			HatSwitch,
			// Token: 0x040007B3 RID: 1971
			CountedBuffer,
			// Token: 0x040007B4 RID: 1972
			ByteCount,
			// Token: 0x040007B5 RID: 1973
			MotionWakeup,
			// Token: 0x040007B6 RID: 1974
			Start,
			// Token: 0x040007B7 RID: 1975
			Select,
			// Token: 0x040007B8 RID: 1976
			Vx = 64,
			// Token: 0x040007B9 RID: 1977
			Vy,
			// Token: 0x040007BA RID: 1978
			Vz,
			// Token: 0x040007BB RID: 1979
			Vbrx,
			// Token: 0x040007BC RID: 1980
			Vbry,
			// Token: 0x040007BD RID: 1981
			Vbrz,
			// Token: 0x040007BE RID: 1982
			Vno,
			// Token: 0x040007BF RID: 1983
			FeatureNotification,
			// Token: 0x040007C0 RID: 1984
			ResolutionMultiplier,
			// Token: 0x040007C1 RID: 1985
			SystemControl = 128,
			// Token: 0x040007C2 RID: 1986
			SystemPowerDown,
			// Token: 0x040007C3 RID: 1987
			SystemSleep,
			// Token: 0x040007C4 RID: 1988
			SystemWakeUp,
			// Token: 0x040007C5 RID: 1989
			SystemContextMenu,
			// Token: 0x040007C6 RID: 1990
			SystemMainMenu,
			// Token: 0x040007C7 RID: 1991
			SystemAppMenu,
			// Token: 0x040007C8 RID: 1992
			SystemMenuHelp,
			// Token: 0x040007C9 RID: 1993
			SystemMenuExit,
			// Token: 0x040007CA RID: 1994
			SystemMenuSelect,
			// Token: 0x040007CB RID: 1995
			SystemMenuRight,
			// Token: 0x040007CC RID: 1996
			SystemMenuLeft,
			// Token: 0x040007CD RID: 1997
			SystemMenuUp,
			// Token: 0x040007CE RID: 1998
			SystemMenuDown,
			// Token: 0x040007CF RID: 1999
			SystemColdRestart,
			// Token: 0x040007D0 RID: 2000
			SystemWarmRestart,
			// Token: 0x040007D1 RID: 2001
			DpadUp,
			// Token: 0x040007D2 RID: 2002
			DpadDown,
			// Token: 0x040007D3 RID: 2003
			DpadRight,
			// Token: 0x040007D4 RID: 2004
			DpadLeft,
			// Token: 0x040007D5 RID: 2005
			SystemDock = 160,
			// Token: 0x040007D6 RID: 2006
			SystemUndock,
			// Token: 0x040007D7 RID: 2007
			SystemSetup,
			// Token: 0x040007D8 RID: 2008
			SystemBreak,
			// Token: 0x040007D9 RID: 2009
			SystemDebuggerBreak,
			// Token: 0x040007DA RID: 2010
			ApplicationBreak,
			// Token: 0x040007DB RID: 2011
			ApplicationDebuggerBreak,
			// Token: 0x040007DC RID: 2012
			SystemSpeakerMute,
			// Token: 0x040007DD RID: 2013
			SystemHibernate,
			// Token: 0x040007DE RID: 2014
			SystemDisplayInvert = 176,
			// Token: 0x040007DF RID: 2015
			SystemDisplayInternal,
			// Token: 0x040007E0 RID: 2016
			SystemDisplayExternal,
			// Token: 0x040007E1 RID: 2017
			SystemDisplayBoth,
			// Token: 0x040007E2 RID: 2018
			SystemDisplayDual,
			// Token: 0x040007E3 RID: 2019
			SystemDisplayToggleIntExt,
			// Token: 0x040007E4 RID: 2020
			SystemDisplaySwapPrimarySecondary,
			// Token: 0x040007E5 RID: 2021
			SystemDisplayLCDAutoScale
		}

		// Token: 0x02000143 RID: 323
		public enum Simulation
		{
			// Token: 0x040007E7 RID: 2023
			Undefined,
			// Token: 0x040007E8 RID: 2024
			FlightSimulationDevice,
			// Token: 0x040007E9 RID: 2025
			AutomobileSimulationDevice,
			// Token: 0x040007EA RID: 2026
			TankSimulationDevice,
			// Token: 0x040007EB RID: 2027
			SpaceshipSimulationDevice,
			// Token: 0x040007EC RID: 2028
			SubmarineSimulationDevice,
			// Token: 0x040007ED RID: 2029
			SailingSimulationDevice,
			// Token: 0x040007EE RID: 2030
			MotorcycleSimulationDevice,
			// Token: 0x040007EF RID: 2031
			SportsSimulationDevice,
			// Token: 0x040007F0 RID: 2032
			AirplaneSimulationDevice,
			// Token: 0x040007F1 RID: 2033
			HelicopterSimulationDevice,
			// Token: 0x040007F2 RID: 2034
			MagicCarpetSimulationDevice,
			// Token: 0x040007F3 RID: 2035
			BicylcleSimulationDevice,
			// Token: 0x040007F4 RID: 2036
			FlightControlStick = 32,
			// Token: 0x040007F5 RID: 2037
			FlightStick,
			// Token: 0x040007F6 RID: 2038
			CyclicControl,
			// Token: 0x040007F7 RID: 2039
			CyclicTrim,
			// Token: 0x040007F8 RID: 2040
			FlightYoke,
			// Token: 0x040007F9 RID: 2041
			TrackControl,
			// Token: 0x040007FA RID: 2042
			Aileron = 176,
			// Token: 0x040007FB RID: 2043
			AileronTrim,
			// Token: 0x040007FC RID: 2044
			AntiTorqueControl,
			// Token: 0x040007FD RID: 2045
			AutopilotEnable,
			// Token: 0x040007FE RID: 2046
			ChaffRelease,
			// Token: 0x040007FF RID: 2047
			CollectiveControl,
			// Token: 0x04000800 RID: 2048
			DiveBreak,
			// Token: 0x04000801 RID: 2049
			ElectronicCountermeasures,
			// Token: 0x04000802 RID: 2050
			Elevator,
			// Token: 0x04000803 RID: 2051
			ElevatorTrim,
			// Token: 0x04000804 RID: 2052
			Rudder,
			// Token: 0x04000805 RID: 2053
			Throttle,
			// Token: 0x04000806 RID: 2054
			FlightCommunications,
			// Token: 0x04000807 RID: 2055
			FlareRelease,
			// Token: 0x04000808 RID: 2056
			LandingGear,
			// Token: 0x04000809 RID: 2057
			ToeBreak,
			// Token: 0x0400080A RID: 2058
			Trigger,
			// Token: 0x0400080B RID: 2059
			WeaponsArm,
			// Token: 0x0400080C RID: 2060
			WeaponsSelect,
			// Token: 0x0400080D RID: 2061
			WingFlaps,
			// Token: 0x0400080E RID: 2062
			Accelerator,
			// Token: 0x0400080F RID: 2063
			Brake,
			// Token: 0x04000810 RID: 2064
			Clutch,
			// Token: 0x04000811 RID: 2065
			Shifter,
			// Token: 0x04000812 RID: 2066
			Steering,
			// Token: 0x04000813 RID: 2067
			TurretDirection,
			// Token: 0x04000814 RID: 2068
			BarrelElevation,
			// Token: 0x04000815 RID: 2069
			DivePlane,
			// Token: 0x04000816 RID: 2070
			Ballast,
			// Token: 0x04000817 RID: 2071
			BicycleCrank,
			// Token: 0x04000818 RID: 2072
			HandleBars,
			// Token: 0x04000819 RID: 2073
			FrontBrake,
			// Token: 0x0400081A RID: 2074
			RearBrake
		}

		// Token: 0x02000144 RID: 324
		public enum Button
		{
			// Token: 0x0400081C RID: 2076
			Undefined,
			// Token: 0x0400081D RID: 2077
			Primary,
			// Token: 0x0400081E RID: 2078
			Secondary,
			// Token: 0x0400081F RID: 2079
			Tertiary
		}
	}
}
