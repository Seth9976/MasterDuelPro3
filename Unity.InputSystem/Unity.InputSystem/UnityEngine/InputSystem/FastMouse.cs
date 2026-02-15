using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000094 RID: 148
	internal class FastMouse : Mouse, IInputStateCallbackReceiver, IEventMerger
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x00021BD4 File Offset: 0x0001FDD4
		public FastMouse()
		{
			InputControlExtensions.DeviceBuilder builder = this.Setup(30, 10, 2).WithName("Mouse").WithDisplayName("Mouse")
				.WithChildren(0, 14)
				.WithLayout(new InternedString("Mouse"))
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1297044819),
					sizeInBits = 392U
				});
			InternedString kVector2Layout = new InternedString("Vector2");
			InternedString kDeltaLayout = new InternedString("Delta");
			InternedString kButtonLayout = new InternedString("Button");
			InternedString kAxisLayout = new InternedString("Axis");
			InternedString kDigitalLayout = new InternedString("Digital");
			InternedString kIntegerLayout = new InternedString("Integer");
			Vector2Control ctrlMouseposition = this.Initialize_ctrlMouseposition(kVector2Layout, this);
			DeltaControl ctrlMousedelta = this.Initialize_ctrlMousedelta(kDeltaLayout, this);
			DeltaControl ctrlMousescroll = this.Initialize_ctrlMousescroll(kDeltaLayout, this);
			ButtonControl ctrlMousepress = this.Initialize_ctrlMousepress(kButtonLayout, this);
			ButtonControl ctrlMouseleftButton = this.Initialize_ctrlMouseleftButton(kButtonLayout, this);
			ButtonControl ctrlMouserightButton = this.Initialize_ctrlMouserightButton(kButtonLayout, this);
			ButtonControl ctrlMousemiddleButton = this.Initialize_ctrlMousemiddleButton(kButtonLayout, this);
			ButtonControl ctrlMouseforwardButton = this.Initialize_ctrlMouseforwardButton(kButtonLayout, this);
			ButtonControl ctrlMousebackButton = this.Initialize_ctrlMousebackButton(kButtonLayout, this);
			AxisControl ctrlMousepressure = this.Initialize_ctrlMousepressure(kAxisLayout, this);
			Vector2Control ctrlMouseradius = this.Initialize_ctrlMouseradius(kVector2Layout, this);
			this.Initialize_ctrlMousepointerId(kDigitalLayout, this);
			IntegerControl ctrlMousedisplayIndex = this.Initialize_ctrlMousedisplayIndex(kIntegerLayout, this);
			IntegerControl ctrlMouseclickCount = this.Initialize_ctrlMouseclickCount(kIntegerLayout, this);
			AxisControl ctrlMousepositionx = this.Initialize_ctrlMousepositionx(kAxisLayout, ctrlMouseposition);
			AxisControl ctrlMousepositiony = this.Initialize_ctrlMousepositiony(kAxisLayout, ctrlMouseposition);
			AxisControl ctrlMousedeltaup = this.Initialize_ctrlMousedeltaup(kAxisLayout, ctrlMousedelta);
			AxisControl ctrlMousedeltadown = this.Initialize_ctrlMousedeltadown(kAxisLayout, ctrlMousedelta);
			AxisControl ctrlMousedeltaleft = this.Initialize_ctrlMousedeltaleft(kAxisLayout, ctrlMousedelta);
			AxisControl ctrlMousedeltaright = this.Initialize_ctrlMousedeltaright(kAxisLayout, ctrlMousedelta);
			AxisControl ctrlMousedeltax = this.Initialize_ctrlMousedeltax(kAxisLayout, ctrlMousedelta);
			AxisControl ctrlMousedeltay = this.Initialize_ctrlMousedeltay(kAxisLayout, ctrlMousedelta);
			AxisControl ctrlMousescrollup = this.Initialize_ctrlMousescrollup(kAxisLayout, ctrlMousescroll);
			AxisControl ctrlMousescrolldown = this.Initialize_ctrlMousescrolldown(kAxisLayout, ctrlMousescroll);
			AxisControl ctrlMousescrollleft = this.Initialize_ctrlMousescrollleft(kAxisLayout, ctrlMousescroll);
			AxisControl ctrlMousescrollright = this.Initialize_ctrlMousescrollright(kAxisLayout, ctrlMousescroll);
			AxisControl ctrlMousescrollx = this.Initialize_ctrlMousescrollx(kAxisLayout, ctrlMousescroll);
			AxisControl ctrlMousescrolly = this.Initialize_ctrlMousescrolly(kAxisLayout, ctrlMousescroll);
			AxisControl ctrlMouseradiusx = this.Initialize_ctrlMouseradiusx(kAxisLayout, ctrlMouseradius);
			AxisControl ctrlMouseradiusy = this.Initialize_ctrlMouseradiusy(kAxisLayout, ctrlMouseradius);
			builder.WithControlUsage(0, new InternedString("Point"), ctrlMouseposition);
			builder.WithControlUsage(1, new InternedString("Secondary2DMotion"), ctrlMousedelta);
			builder.WithControlUsage(2, new InternedString("ScrollHorizontal"), ctrlMousescrollx);
			builder.WithControlUsage(3, new InternedString("ScrollVertical"), ctrlMousescrolly);
			builder.WithControlUsage(4, new InternedString("PrimaryAction"), ctrlMouseleftButton);
			builder.WithControlUsage(5, new InternedString("SecondaryAction"), ctrlMouserightButton);
			builder.WithControlUsage(6, new InternedString("Forward"), ctrlMouseforwardButton);
			builder.WithControlUsage(7, new InternedString("Back"), ctrlMousebackButton);
			builder.WithControlUsage(8, new InternedString("Pressure"), ctrlMousepressure);
			builder.WithControlUsage(9, new InternedString("Radius"), ctrlMouseradius);
			builder.WithControlAlias(0, new InternedString("horizontal"));
			builder.WithControlAlias(1, new InternedString("vertical"));
			base.scroll = ctrlMousescroll;
			base.leftButton = ctrlMouseleftButton;
			base.middleButton = ctrlMousemiddleButton;
			base.rightButton = ctrlMouserightButton;
			base.backButton = ctrlMousebackButton;
			base.forwardButton = ctrlMouseforwardButton;
			base.clickCount = ctrlMouseclickCount;
			base.position = ctrlMouseposition;
			base.delta = ctrlMousedelta;
			base.radius = ctrlMouseradius;
			base.pressure = ctrlMousepressure;
			base.press = ctrlMousepress;
			base.displayIndex = ctrlMousedisplayIndex;
			ctrlMouseposition.x = ctrlMousepositionx;
			ctrlMouseposition.y = ctrlMousepositiony;
			ctrlMousedelta.up = ctrlMousedeltaup;
			ctrlMousedelta.down = ctrlMousedeltadown;
			ctrlMousedelta.left = ctrlMousedeltaleft;
			ctrlMousedelta.right = ctrlMousedeltaright;
			ctrlMousedelta.x = ctrlMousedeltax;
			ctrlMousedelta.y = ctrlMousedeltay;
			ctrlMousescroll.up = ctrlMousescrollup;
			ctrlMousescroll.down = ctrlMousescrolldown;
			ctrlMousescroll.left = ctrlMousescrollleft;
			ctrlMousescroll.right = ctrlMousescrollright;
			ctrlMousescroll.x = ctrlMousescrollx;
			ctrlMousescroll.y = ctrlMousescrolly;
			ctrlMouseradius.x = ctrlMouseradiusx;
			ctrlMouseradius.y = ctrlMouseradiusy;
			builder.WithStateOffsetToControlIndexMap(new uint[]
			{
				32782U, 16809999U, 33587218U, 33587219U, 33587220U, 50364432U, 50364433U, 50364437U, 67141656U, 67141657U,
				67141658U, 83918870U, 83918871U, 83918875U, 100664323U, 100664324U, 101188613U, 101712902U, 102237191U, 102761480U,
				109068300U, 117456909U, 134250505U, 167804956U, 184582173U, 201327627U
			});
			builder.WithControlTree(new byte[]
			{
				135, 1, 1, 0, 0, 0, 0, 196, 0, 3,
				0, 0, 0, 0, 135, 1, 23, 0, 0, 0,
				0, 128, 0, 5, 0, 0, 0, 0, 196, 0,
				11, 0, 0, 0, 0, 64, 0, 7, 0, 0,
				0, 1, 128, 0, 9, 0, 3, 0, 1, 32,
				0, byte.MaxValue, byte.MaxValue, 1, 0, 1, 64, 0, byte.MaxValue, byte.MaxValue,
				2, 0, 1, 96, 0, byte.MaxValue, byte.MaxValue, 7, 0, 3,
				128, 0, byte.MaxValue, byte.MaxValue, 4, 0, 3, 193, 0, 13,
				0, 0, 0, 0, 196, 0, 19, 0, 0, 0,
				0, 161, 0, 15, 0, 10, 0, 4, 193, 0,
				17, 0, 14, 0, 4, 145, 0, byte.MaxValue, byte.MaxValue, 18,
				0, 3, 161, 0, byte.MaxValue, byte.MaxValue, 21, 0, 3, 192,
				0, byte.MaxValue, byte.MaxValue, 0, 0, 0, 193, 0, byte.MaxValue, byte.MaxValue,
				24, 0, 2, 195, 0, 21, 0, 0, 0, 0,
				196, 0, byte.MaxValue, byte.MaxValue, 28, 0, 1, 194, 0, byte.MaxValue,
				byte.MaxValue, 26, 0, 1, 195, 0, byte.MaxValue, byte.MaxValue, 27, 0,
				1, 32, 1, 25, 0, 0, 0, 0, 135, 1,
				41, 0, 0, 0, 0, 240, 0, 27, 0, 0,
				0, 0, 32, 1, 39, 0, 0, 0, 0, 224,
				0, 29, 0, 0, 0, 0, 240, 0, byte.MaxValue, byte.MaxValue,
				41, 0, 1, 210, 0, 31, 0, 39, 0, 1,
				224, 0, byte.MaxValue, byte.MaxValue, 40, 0, 1, 203, 0, 33,
				0, 0, 0, 0, 210, 0, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 200, 0, 35, 0, 0, 0, 0, 203, 0,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 198, 0, 37, 0, 0,
				0, 0, 200, 0, byte.MaxValue, byte.MaxValue, 0, 0, 0, 197,
				0, byte.MaxValue, byte.MaxValue, 29, 0, 1, 198, 0, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 8, 1, byte.MaxValue, byte.MaxValue, 30, 0, 1,
				32, 1, byte.MaxValue, byte.MaxValue, 31, 0, 1, 128, 1, 43,
				0, 0, 0, 0, 135, 1, 47, 0, 0, 0,
				0, 80, 1, byte.MaxValue, byte.MaxValue, 32, 0, 2, 128, 1,
				45, 0, 34, 0, 2, 104, 1, byte.MaxValue, byte.MaxValue, 36,
				0, 1, 128, 1, byte.MaxValue, byte.MaxValue, 37, 0, 1, 132,
				1, 49, 0, 0, 0, 0, 135, 1, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 130, 1, 51, 0, 0, 0, 0,
				132, 1, byte.MaxValue, byte.MaxValue, 0, 0, 0, 129, 1, byte.MaxValue,
				byte.MaxValue, 38, 0, 1, 130, 1, byte.MaxValue, byte.MaxValue, 0, 0,
				0
			}, new ushort[]
			{
				0, 14, 15, 1, 16, 17, 21, 18, 19, 20,
				2, 22, 23, 27, 2, 22, 23, 27, 24, 25,
				26, 24, 25, 26, 3, 4, 5, 6, 7, 8,
				9, 9, 10, 28, 10, 28, 29, 29, 11, 12,
				12, 13
			});
			builder.Finish();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00022038 File Offset: 0x00020238
		private Vector2Control Initialize_ctrlMouseposition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 0).WithParent(parent)
				.WithChildren(14, 2)
				.WithName("position")
				.WithDisplayName("Position")
				.WithLayout(kVector2Layout)
				.WithUsages(0, 1)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 0U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000220E8 File Offset: 0x000202E8
		private DeltaControl Initialize_ctrlMousedelta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 1).WithParent(parent)
				.WithChildren(16, 6)
				.WithName("delta")
				.WithDisplayName("Delta")
				.WithLayout(kDeltaLayout)
				.WithUsages(1, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 8U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00022190 File Offset: 0x00020390
		private DeltaControl Initialize_ctrlMousescroll(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 2).WithParent(parent)
				.WithChildren(22, 6)
				.WithName("scroll")
				.WithDisplayName("Scroll")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 16U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00022230 File Offset: 0x00020430
		private ButtonControl Initialize_ctrlMousepress(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 3).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Press")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000222E8 File Offset: 0x000204E8
		private ButtonControl Initialize_ctrlMouseleftButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 4).WithParent(parent)
				.WithName("leftButton")
				.WithDisplayName("Left Button")
				.WithShortDisplayName("LMB")
				.WithLayout(kButtonLayout)
				.WithUsages(4, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000223B0 File Offset: 0x000205B0
		private ButtonControl Initialize_ctrlMouserightButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 5).WithParent(parent)
				.WithName("rightButton")
				.WithDisplayName("Right Button")
				.WithShortDisplayName("RMB")
				.WithLayout(kButtonLayout)
				.WithUsages(5, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 1U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00022478 File Offset: 0x00020678
		private ButtonControl Initialize_ctrlMousemiddleButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 6).WithParent(parent)
				.WithName("middleButton")
				.WithDisplayName("Middle Button")
				.WithShortDisplayName("MMB")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 2U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00022534 File Offset: 0x00020734
		private ButtonControl Initialize_ctrlMouseforwardButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 7).WithParent(parent)
				.WithName("forwardButton")
				.WithDisplayName("Forward")
				.WithLayout(kButtonLayout)
				.WithUsages(6, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 3U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000225EC File Offset: 0x000207EC
		private ButtonControl Initialize_ctrlMousebackButton(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 8).WithParent(parent)
				.WithName("backButton")
				.WithDisplayName("Back")
				.WithLayout(kButtonLayout)
				.WithUsages(7, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 24U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000226A4 File Offset: 0x000208A4
		private AxisControl Initialize_ctrlMousepressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 9).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Pressure")
				.WithLayout(kAxisLayout)
				.WithUsages(8, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 32U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.WithDefaultState(1)
				.Finish();
			return axisControl;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00022750 File Offset: 0x00020950
		private Vector2Control Initialize_ctrlMouseradius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 10).WithParent(parent)
				.WithChildren(28, 2)
				.WithName("radius")
				.WithDisplayName("Radius")
				.WithLayout(kVector2Layout)
				.WithUsages(9, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 40U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000227FC File Offset: 0x000209FC
		private IntegerControl Initialize_ctrlMousepointerId(InternedString kDigitalLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 11).WithParent(parent)
				.WithName("pointerId")
				.WithDisplayName("pointerId")
				.WithLayout(kDigitalLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 48U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00022890 File Offset: 0x00020A90
		private IntegerControl Initialize_ctrlMousedisplayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 12).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1431521364),
					byteOffset = 26U,
					bitOffset = 0U,
					sizeInBits = 16U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00022924 File Offset: 0x00020B24
		private IntegerControl Initialize_ctrlMouseclickCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 13).WithParent(parent)
				.WithName("clickCount")
				.WithDisplayName("Click Count")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1431521364),
					byteOffset = 28U,
					bitOffset = 0U,
					sizeInBits = 16U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000229C0 File Offset: 0x00020BC0
		private AxisControl Initialize_ctrlMousepositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 14).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Position X")
				.WithShortDisplayName("Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 0U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00022A68 File Offset: 0x00020C68
		private AxisControl Initialize_ctrlMousepositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 15).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Position Y")
				.WithShortDisplayName("Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 4U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00022B10 File Offset: 0x00020D10
		private AxisControl Initialize_ctrlMousedeltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 16).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Delta Up")
				.WithShortDisplayName("Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 12U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00022BCC File Offset: 0x00020DCC
		private AxisControl Initialize_ctrlMousedeltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 17).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Delta Down")
				.WithShortDisplayName("Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 12U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00022C90 File Offset: 0x00020E90
		private AxisControl Initialize_ctrlMousedeltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 18).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Delta Left")
				.WithShortDisplayName("Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 8U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00022D54 File Offset: 0x00020F54
		private AxisControl Initialize_ctrlMousedeltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 19).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Delta Right")
				.WithShortDisplayName("Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 8U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00022E10 File Offset: 0x00021010
		private AxisControl Initialize_ctrlMousedeltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 20).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Delta X")
				.WithShortDisplayName("Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 8U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00022EB0 File Offset: 0x000210B0
		private AxisControl Initialize_ctrlMousedeltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 21).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Delta Y")
				.WithShortDisplayName("Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 12U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00022F50 File Offset: 0x00021150
		private AxisControl Initialize_ctrlMousescrollup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 22).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Scroll Up")
				.WithShortDisplayName("Scroll Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 20U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002300C File Offset: 0x0002120C
		private AxisControl Initialize_ctrlMousescrolldown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 23).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Scroll Down")
				.WithShortDisplayName("Scroll Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 20U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000230D0 File Offset: 0x000212D0
		private AxisControl Initialize_ctrlMousescrollleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 24).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Scroll Left")
				.WithShortDisplayName("Scroll Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 16U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00023194 File Offset: 0x00021394
		private AxisControl Initialize_ctrlMousescrollright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 25).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Scroll Right")
				.WithShortDisplayName("Scroll Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 16U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00023250 File Offset: 0x00021450
		private AxisControl Initialize_ctrlMousescrollx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 26).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Scroll Left/Right")
				.WithShortDisplayName("Scroll Left/Right")
				.WithLayout(kAxisLayout)
				.WithUsages(2, 1)
				.WithAliases(0, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 16U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00023304 File Offset: 0x00021504
		private AxisControl Initialize_ctrlMousescrolly(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 27).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Scroll Up/Down")
				.WithShortDisplayName("Scroll Wheel")
				.WithLayout(kAxisLayout)
				.WithUsages(3, 1)
				.WithAliases(1, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 20U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000233B8 File Offset: 0x000215B8
		private AxisControl Initialize_ctrlMouseradiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 28).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Radius X")
				.WithShortDisplayName("Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 40U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00023458 File Offset: 0x00021658
		private AxisControl Initialize_ctrlMouseradiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 29).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Radius Y")
				.WithShortDisplayName("Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 44U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000234F8 File Offset: 0x000216F8
		protected new void OnNextUpdate()
		{
			InputState.Change<Vector2>(base.delta, Vector2.zero, InputState.currentUpdateType, default(InputEventPtr));
			InputState.Change<Vector2>(base.scroll, Vector2.zero, InputState.currentUpdateType, default(InputEventPtr));
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00023544 File Offset: 0x00021744
		protected new unsafe void OnStateEvent(InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				base.OnStateEvent(eventPtr);
				return;
			}
			StateEvent* stateEvent = StateEvent.FromUnchecked(eventPtr);
			if (stateEvent->stateFormat != MouseState.Format)
			{
				base.OnStateEvent(eventPtr);
				return;
			}
			MouseState newState = *(MouseState*)stateEvent->state;
			MouseState* stateFromDevice = (MouseState*)((byte*)base.currentStatePtr + this.m_StateBlock.byteOffset);
			newState.delta += stateFromDevice->delta;
			newState.scroll += stateFromDevice->scroll;
			InputState.Change<MouseState>(this, ref newState, InputState.currentUpdateType, eventPtr);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000235FA File Offset: 0x000217FA
		void IInputStateCallbackReceiver.OnNextUpdate()
		{
			this.OnNextUpdate();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00023602 File Offset: 0x00021802
		void IInputStateCallbackReceiver.OnStateEvent(InputEventPtr eventPtr)
		{
			this.OnStateEvent(eventPtr);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002360C File Offset: 0x0002180C
		internal unsafe static bool MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			StateEvent* currentEvent = StateEvent.FromUnchecked(currentEventPtr);
			StateEvent* nextEvent = StateEvent.FromUnchecked(nextEventPtr);
			if (currentEvent->stateFormat != MouseState.Format || nextEvent->stateFormat != MouseState.Format)
			{
				return false;
			}
			MouseState* currentState = (MouseState*)currentEvent->state;
			MouseState* nextState = (MouseState*)nextEvent->state;
			if (currentState->buttons != nextState->buttons || currentState->clickCount != nextState->clickCount)
			{
				return false;
			}
			nextState->delta += currentState->delta;
			nextState->scroll += currentState->scroll;
			return true;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000236E4 File Offset: 0x000218E4
		bool IEventMerger.MergeForward(InputEventPtr currentEventPtr, InputEventPtr nextEventPtr)
		{
			return FastMouse.MergeForward(currentEventPtr, nextEventPtr);
		}

		// Token: 0x040003E5 RID: 997
		public const string metadata = "AutoWindowSpace;Vector2;Delta;Button;Axis;Digital;Integer;Mouse;Pointer";
	}
}
