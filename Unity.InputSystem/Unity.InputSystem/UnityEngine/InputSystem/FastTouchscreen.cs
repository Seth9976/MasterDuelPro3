using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000095 RID: 149
	internal class FastTouchscreen : Touchscreen
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x000236F0 File Offset: 0x000218F0
		public FastTouchscreen()
		{
			InputControlExtensions.DeviceBuilder builder = this.Setup(302, 5, 0).WithName("Touchscreen").WithDisplayName("Touchscreen")
				.WithChildren(0, 17)
				.WithLayout(new InternedString("Touchscreen"))
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414742866),
					sizeInBits = 4928U
				});
			InternedString kTouchLayout = new InternedString("Touch");
			InternedString kVector2Layout = new InternedString("Vector2");
			InternedString kDeltaLayout = new InternedString("Delta");
			InternedString kAnalogLayout = new InternedString("Analog");
			InternedString kTouchPressLayout = new InternedString("TouchPress");
			InternedString kIntegerLayout = new InternedString("Integer");
			InternedString kAxisLayout = new InternedString("Axis");
			InternedString kTouchPhaseLayout = new InternedString("TouchPhase");
			InternedString kButtonLayout = new InternedString("Button");
			InternedString kDoubleLayout = new InternedString("Double");
			TouchControl ctrlTouchscreenprimaryTouch = this.Initialize_ctrlTouchscreenprimaryTouch(kTouchLayout, this);
			Vector2Control ctrlTouchscreenposition = this.Initialize_ctrlTouchscreenposition(kVector2Layout, this);
			DeltaControl ctrlTouchscreendelta = this.Initialize_ctrlTouchscreendelta(kDeltaLayout, this);
			AxisControl ctrlTouchscreenpressure = this.Initialize_ctrlTouchscreenpressure(kAnalogLayout, this);
			Vector2Control ctrlTouchscreenradius = this.Initialize_ctrlTouchscreenradius(kVector2Layout, this);
			TouchPressControl ctrlTouchscreenpress = this.Initialize_ctrlTouchscreenpress(kTouchPressLayout, this);
			IntegerControl ctrlTouchscreendisplayIndex = this.Initialize_ctrlTouchscreendisplayIndex(kIntegerLayout, this);
			TouchControl ctrlTouchscreentouch0 = this.Initialize_ctrlTouchscreentouch0(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch = this.Initialize_ctrlTouchscreentouch1(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch2 = this.Initialize_ctrlTouchscreentouch2(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch3 = this.Initialize_ctrlTouchscreentouch3(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch4 = this.Initialize_ctrlTouchscreentouch4(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch5 = this.Initialize_ctrlTouchscreentouch5(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch6 = this.Initialize_ctrlTouchscreentouch6(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch7 = this.Initialize_ctrlTouchscreentouch7(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch8 = this.Initialize_ctrlTouchscreentouch8(kTouchLayout, this);
			TouchControl ctrlTouchscreentouch9 = this.Initialize_ctrlTouchscreentouch9(kTouchLayout, this);
			IntegerControl ctrlTouchscreenprimaryTouchtouchId = this.Initialize_ctrlTouchscreenprimaryTouchtouchId(kIntegerLayout, ctrlTouchscreenprimaryTouch);
			Vector2Control ctrlTouchscreenprimaryTouchposition = this.Initialize_ctrlTouchscreenprimaryTouchposition(kVector2Layout, ctrlTouchscreenprimaryTouch);
			DeltaControl ctrlTouchscreenprimaryTouchdelta = this.Initialize_ctrlTouchscreenprimaryTouchdelta(kDeltaLayout, ctrlTouchscreenprimaryTouch);
			AxisControl ctrlTouchscreenprimaryTouchpressure = this.Initialize_ctrlTouchscreenprimaryTouchpressure(kAxisLayout, ctrlTouchscreenprimaryTouch);
			Vector2Control ctrlTouchscreenprimaryTouchradius = this.Initialize_ctrlTouchscreenprimaryTouchradius(kVector2Layout, ctrlTouchscreenprimaryTouch);
			TouchPhaseControl ctrlTouchscreenprimaryTouchphase = this.Initialize_ctrlTouchscreenprimaryTouchphase(kTouchPhaseLayout, ctrlTouchscreenprimaryTouch);
			TouchPressControl ctrlTouchscreenprimaryTouchpress = this.Initialize_ctrlTouchscreenprimaryTouchpress(kTouchPressLayout, ctrlTouchscreenprimaryTouch);
			IntegerControl ctrlTouchscreenprimaryTouchtapCount = this.Initialize_ctrlTouchscreenprimaryTouchtapCount(kIntegerLayout, ctrlTouchscreenprimaryTouch);
			IntegerControl ctrlTouchscreenprimaryTouchdisplayIndex = this.Initialize_ctrlTouchscreenprimaryTouchdisplayIndex(kIntegerLayout, ctrlTouchscreenprimaryTouch);
			ButtonControl ctrlTouchscreenprimaryTouchindirectTouch = this.Initialize_ctrlTouchscreenprimaryTouchindirectTouch(kButtonLayout, ctrlTouchscreenprimaryTouch);
			ButtonControl ctrlTouchscreenprimaryTouchtap = this.Initialize_ctrlTouchscreenprimaryTouchtap(kButtonLayout, ctrlTouchscreenprimaryTouch);
			DoubleControl ctrlTouchscreenprimaryTouchstartTime = this.Initialize_ctrlTouchscreenprimaryTouchstartTime(kDoubleLayout, ctrlTouchscreenprimaryTouch);
			Vector2Control ctrlTouchscreenprimaryTouchstartPosition = this.Initialize_ctrlTouchscreenprimaryTouchstartPosition(kVector2Layout, ctrlTouchscreenprimaryTouch);
			AxisControl ctrlTouchscreenprimaryTouchpositionx = this.Initialize_ctrlTouchscreenprimaryTouchpositionx(kAxisLayout, ctrlTouchscreenprimaryTouchposition);
			AxisControl ctrlTouchscreenprimaryTouchpositiony = this.Initialize_ctrlTouchscreenprimaryTouchpositiony(kAxisLayout, ctrlTouchscreenprimaryTouchposition);
			AxisControl ctrlTouchscreenprimaryTouchdeltaup = this.Initialize_ctrlTouchscreenprimaryTouchdeltaup(kAxisLayout, ctrlTouchscreenprimaryTouchdelta);
			AxisControl ctrlTouchscreenprimaryTouchdeltadown = this.Initialize_ctrlTouchscreenprimaryTouchdeltadown(kAxisLayout, ctrlTouchscreenprimaryTouchdelta);
			AxisControl ctrlTouchscreenprimaryTouchdeltaleft = this.Initialize_ctrlTouchscreenprimaryTouchdeltaleft(kAxisLayout, ctrlTouchscreenprimaryTouchdelta);
			AxisControl ctrlTouchscreenprimaryTouchdeltaright = this.Initialize_ctrlTouchscreenprimaryTouchdeltaright(kAxisLayout, ctrlTouchscreenprimaryTouchdelta);
			AxisControl ctrlTouchscreenprimaryTouchdeltax = this.Initialize_ctrlTouchscreenprimaryTouchdeltax(kAxisLayout, ctrlTouchscreenprimaryTouchdelta);
			AxisControl ctrlTouchscreenprimaryTouchdeltay = this.Initialize_ctrlTouchscreenprimaryTouchdeltay(kAxisLayout, ctrlTouchscreenprimaryTouchdelta);
			AxisControl ctrlTouchscreenprimaryTouchradiusx = this.Initialize_ctrlTouchscreenprimaryTouchradiusx(kAxisLayout, ctrlTouchscreenprimaryTouchradius);
			AxisControl ctrlTouchscreenprimaryTouchradiusy = this.Initialize_ctrlTouchscreenprimaryTouchradiusy(kAxisLayout, ctrlTouchscreenprimaryTouchradius);
			AxisControl ctrlTouchscreenprimaryTouchstartPositionx = this.Initialize_ctrlTouchscreenprimaryTouchstartPositionx(kAxisLayout, ctrlTouchscreenprimaryTouchstartPosition);
			AxisControl ctrlTouchscreenprimaryTouchstartPositiony = this.Initialize_ctrlTouchscreenprimaryTouchstartPositiony(kAxisLayout, ctrlTouchscreenprimaryTouchstartPosition);
			AxisControl ctrlTouchscreenpositionx = this.Initialize_ctrlTouchscreenpositionx(kAxisLayout, ctrlTouchscreenposition);
			AxisControl ctrlTouchscreenpositiony = this.Initialize_ctrlTouchscreenpositiony(kAxisLayout, ctrlTouchscreenposition);
			AxisControl ctrlTouchscreendeltaup = this.Initialize_ctrlTouchscreendeltaup(kAxisLayout, ctrlTouchscreendelta);
			AxisControl ctrlTouchscreendeltadown = this.Initialize_ctrlTouchscreendeltadown(kAxisLayout, ctrlTouchscreendelta);
			AxisControl ctrlTouchscreendeltaleft = this.Initialize_ctrlTouchscreendeltaleft(kAxisLayout, ctrlTouchscreendelta);
			AxisControl ctrlTouchscreendeltaright = this.Initialize_ctrlTouchscreendeltaright(kAxisLayout, ctrlTouchscreendelta);
			AxisControl ctrlTouchscreendeltax = this.Initialize_ctrlTouchscreendeltax(kAxisLayout, ctrlTouchscreendelta);
			AxisControl ctrlTouchscreendeltay = this.Initialize_ctrlTouchscreendeltay(kAxisLayout, ctrlTouchscreendelta);
			AxisControl ctrlTouchscreenradiusx = this.Initialize_ctrlTouchscreenradiusx(kAxisLayout, ctrlTouchscreenradius);
			AxisControl ctrlTouchscreenradiusy = this.Initialize_ctrlTouchscreenradiusy(kAxisLayout, ctrlTouchscreenradius);
			IntegerControl ctrlTouchscreentouch0touchId = this.Initialize_ctrlTouchscreentouch0touchId(kIntegerLayout, ctrlTouchscreentouch0);
			Vector2Control ctrlTouchscreentouch0position = this.Initialize_ctrlTouchscreentouch0position(kVector2Layout, ctrlTouchscreentouch0);
			DeltaControl ctrlTouchscreentouch0delta = this.Initialize_ctrlTouchscreentouch0delta(kDeltaLayout, ctrlTouchscreentouch0);
			AxisControl ctrlTouchscreentouch0pressure = this.Initialize_ctrlTouchscreentouch0pressure(kAxisLayout, ctrlTouchscreentouch0);
			Vector2Control ctrlTouchscreentouch0radius = this.Initialize_ctrlTouchscreentouch0radius(kVector2Layout, ctrlTouchscreentouch0);
			TouchPhaseControl ctrlTouchscreentouch0phase = this.Initialize_ctrlTouchscreentouch0phase(kTouchPhaseLayout, ctrlTouchscreentouch0);
			TouchPressControl ctrlTouchscreentouch0press = this.Initialize_ctrlTouchscreentouch0press(kTouchPressLayout, ctrlTouchscreentouch0);
			IntegerControl ctrlTouchscreentouch0tapCount = this.Initialize_ctrlTouchscreentouch0tapCount(kIntegerLayout, ctrlTouchscreentouch0);
			IntegerControl ctrlTouchscreentouch0displayIndex = this.Initialize_ctrlTouchscreentouch0displayIndex(kIntegerLayout, ctrlTouchscreentouch0);
			ButtonControl ctrlTouchscreentouch0indirectTouch = this.Initialize_ctrlTouchscreentouch0indirectTouch(kButtonLayout, ctrlTouchscreentouch0);
			ButtonControl ctrlTouchscreentouch0tap = this.Initialize_ctrlTouchscreentouch0tap(kButtonLayout, ctrlTouchscreentouch0);
			DoubleControl ctrlTouchscreentouch0startTime = this.Initialize_ctrlTouchscreentouch0startTime(kDoubleLayout, ctrlTouchscreentouch0);
			Vector2Control ctrlTouchscreentouch0startPosition = this.Initialize_ctrlTouchscreentouch0startPosition(kVector2Layout, ctrlTouchscreentouch0);
			AxisControl ctrlTouchscreentouch0positionx = this.Initialize_ctrlTouchscreentouch0positionx(kAxisLayout, ctrlTouchscreentouch0position);
			AxisControl ctrlTouchscreentouch0positiony = this.Initialize_ctrlTouchscreentouch0positiony(kAxisLayout, ctrlTouchscreentouch0position);
			AxisControl ctrlTouchscreentouch0deltaup = this.Initialize_ctrlTouchscreentouch0deltaup(kAxisLayout, ctrlTouchscreentouch0delta);
			AxisControl ctrlTouchscreentouch0deltadown = this.Initialize_ctrlTouchscreentouch0deltadown(kAxisLayout, ctrlTouchscreentouch0delta);
			AxisControl ctrlTouchscreentouch0deltaleft = this.Initialize_ctrlTouchscreentouch0deltaleft(kAxisLayout, ctrlTouchscreentouch0delta);
			AxisControl ctrlTouchscreentouch0deltaright = this.Initialize_ctrlTouchscreentouch0deltaright(kAxisLayout, ctrlTouchscreentouch0delta);
			AxisControl ctrlTouchscreentouch0deltax = this.Initialize_ctrlTouchscreentouch0deltax(kAxisLayout, ctrlTouchscreentouch0delta);
			AxisControl ctrlTouchscreentouch0deltay = this.Initialize_ctrlTouchscreentouch0deltay(kAxisLayout, ctrlTouchscreentouch0delta);
			AxisControl ctrlTouchscreentouch0radiusx = this.Initialize_ctrlTouchscreentouch0radiusx(kAxisLayout, ctrlTouchscreentouch0radius);
			AxisControl ctrlTouchscreentouch0radiusy = this.Initialize_ctrlTouchscreentouch0radiusy(kAxisLayout, ctrlTouchscreentouch0radius);
			AxisControl ctrlTouchscreentouch0startPositionx = this.Initialize_ctrlTouchscreentouch0startPositionx(kAxisLayout, ctrlTouchscreentouch0startPosition);
			AxisControl ctrlTouchscreentouch0startPositiony = this.Initialize_ctrlTouchscreentouch0startPositiony(kAxisLayout, ctrlTouchscreentouch0startPosition);
			IntegerControl ctrlTouchscreentouch1touchId = this.Initialize_ctrlTouchscreentouch1touchId(kIntegerLayout, ctrlTouchscreentouch);
			Vector2Control ctrlTouchscreentouch1position = this.Initialize_ctrlTouchscreentouch1position(kVector2Layout, ctrlTouchscreentouch);
			DeltaControl ctrlTouchscreentouch1delta = this.Initialize_ctrlTouchscreentouch1delta(kDeltaLayout, ctrlTouchscreentouch);
			AxisControl ctrlTouchscreentouch1pressure = this.Initialize_ctrlTouchscreentouch1pressure(kAxisLayout, ctrlTouchscreentouch);
			Vector2Control ctrlTouchscreentouch1radius = this.Initialize_ctrlTouchscreentouch1radius(kVector2Layout, ctrlTouchscreentouch);
			TouchPhaseControl ctrlTouchscreentouch1phase = this.Initialize_ctrlTouchscreentouch1phase(kTouchPhaseLayout, ctrlTouchscreentouch);
			TouchPressControl ctrlTouchscreentouch1press = this.Initialize_ctrlTouchscreentouch1press(kTouchPressLayout, ctrlTouchscreentouch);
			IntegerControl ctrlTouchscreentouch1tapCount = this.Initialize_ctrlTouchscreentouch1tapCount(kIntegerLayout, ctrlTouchscreentouch);
			IntegerControl ctrlTouchscreentouch1displayIndex = this.Initialize_ctrlTouchscreentouch1displayIndex(kIntegerLayout, ctrlTouchscreentouch);
			ButtonControl ctrlTouchscreentouch1indirectTouch = this.Initialize_ctrlTouchscreentouch1indirectTouch(kButtonLayout, ctrlTouchscreentouch);
			ButtonControl ctrlTouchscreentouch1tap = this.Initialize_ctrlTouchscreentouch1tap(kButtonLayout, ctrlTouchscreentouch);
			DoubleControl ctrlTouchscreentouch1startTime = this.Initialize_ctrlTouchscreentouch1startTime(kDoubleLayout, ctrlTouchscreentouch);
			Vector2Control ctrlTouchscreentouch1startPosition = this.Initialize_ctrlTouchscreentouch1startPosition(kVector2Layout, ctrlTouchscreentouch);
			AxisControl ctrlTouchscreentouch1positionx = this.Initialize_ctrlTouchscreentouch1positionx(kAxisLayout, ctrlTouchscreentouch1position);
			AxisControl ctrlTouchscreentouch1positiony = this.Initialize_ctrlTouchscreentouch1positiony(kAxisLayout, ctrlTouchscreentouch1position);
			AxisControl ctrlTouchscreentouch1deltaup = this.Initialize_ctrlTouchscreentouch1deltaup(kAxisLayout, ctrlTouchscreentouch1delta);
			AxisControl ctrlTouchscreentouch1deltadown = this.Initialize_ctrlTouchscreentouch1deltadown(kAxisLayout, ctrlTouchscreentouch1delta);
			AxisControl ctrlTouchscreentouch1deltaleft = this.Initialize_ctrlTouchscreentouch1deltaleft(kAxisLayout, ctrlTouchscreentouch1delta);
			AxisControl ctrlTouchscreentouch1deltaright = this.Initialize_ctrlTouchscreentouch1deltaright(kAxisLayout, ctrlTouchscreentouch1delta);
			AxisControl ctrlTouchscreentouch1deltax = this.Initialize_ctrlTouchscreentouch1deltax(kAxisLayout, ctrlTouchscreentouch1delta);
			AxisControl ctrlTouchscreentouch1deltay = this.Initialize_ctrlTouchscreentouch1deltay(kAxisLayout, ctrlTouchscreentouch1delta);
			AxisControl ctrlTouchscreentouch1radiusx = this.Initialize_ctrlTouchscreentouch1radiusx(kAxisLayout, ctrlTouchscreentouch1radius);
			AxisControl ctrlTouchscreentouch1radiusy = this.Initialize_ctrlTouchscreentouch1radiusy(kAxisLayout, ctrlTouchscreentouch1radius);
			AxisControl ctrlTouchscreentouch1startPositionx = this.Initialize_ctrlTouchscreentouch1startPositionx(kAxisLayout, ctrlTouchscreentouch1startPosition);
			AxisControl ctrlTouchscreentouch1startPositiony = this.Initialize_ctrlTouchscreentouch1startPositiony(kAxisLayout, ctrlTouchscreentouch1startPosition);
			IntegerControl ctrlTouchscreentouch2touchId = this.Initialize_ctrlTouchscreentouch2touchId(kIntegerLayout, ctrlTouchscreentouch2);
			Vector2Control ctrlTouchscreentouch2position = this.Initialize_ctrlTouchscreentouch2position(kVector2Layout, ctrlTouchscreentouch2);
			DeltaControl ctrlTouchscreentouch2delta = this.Initialize_ctrlTouchscreentouch2delta(kDeltaLayout, ctrlTouchscreentouch2);
			AxisControl ctrlTouchscreentouch2pressure = this.Initialize_ctrlTouchscreentouch2pressure(kAxisLayout, ctrlTouchscreentouch2);
			Vector2Control ctrlTouchscreentouch2radius = this.Initialize_ctrlTouchscreentouch2radius(kVector2Layout, ctrlTouchscreentouch2);
			TouchPhaseControl ctrlTouchscreentouch2phase = this.Initialize_ctrlTouchscreentouch2phase(kTouchPhaseLayout, ctrlTouchscreentouch2);
			TouchPressControl ctrlTouchscreentouch2press = this.Initialize_ctrlTouchscreentouch2press(kTouchPressLayout, ctrlTouchscreentouch2);
			IntegerControl ctrlTouchscreentouch2tapCount = this.Initialize_ctrlTouchscreentouch2tapCount(kIntegerLayout, ctrlTouchscreentouch2);
			IntegerControl ctrlTouchscreentouch2displayIndex = this.Initialize_ctrlTouchscreentouch2displayIndex(kIntegerLayout, ctrlTouchscreentouch2);
			ButtonControl ctrlTouchscreentouch2indirectTouch = this.Initialize_ctrlTouchscreentouch2indirectTouch(kButtonLayout, ctrlTouchscreentouch2);
			ButtonControl ctrlTouchscreentouch2tap = this.Initialize_ctrlTouchscreentouch2tap(kButtonLayout, ctrlTouchscreentouch2);
			DoubleControl ctrlTouchscreentouch2startTime = this.Initialize_ctrlTouchscreentouch2startTime(kDoubleLayout, ctrlTouchscreentouch2);
			Vector2Control ctrlTouchscreentouch2startPosition = this.Initialize_ctrlTouchscreentouch2startPosition(kVector2Layout, ctrlTouchscreentouch2);
			AxisControl ctrlTouchscreentouch2positionx = this.Initialize_ctrlTouchscreentouch2positionx(kAxisLayout, ctrlTouchscreentouch2position);
			AxisControl ctrlTouchscreentouch2positiony = this.Initialize_ctrlTouchscreentouch2positiony(kAxisLayout, ctrlTouchscreentouch2position);
			AxisControl ctrlTouchscreentouch2deltaup = this.Initialize_ctrlTouchscreentouch2deltaup(kAxisLayout, ctrlTouchscreentouch2delta);
			AxisControl ctrlTouchscreentouch2deltadown = this.Initialize_ctrlTouchscreentouch2deltadown(kAxisLayout, ctrlTouchscreentouch2delta);
			AxisControl ctrlTouchscreentouch2deltaleft = this.Initialize_ctrlTouchscreentouch2deltaleft(kAxisLayout, ctrlTouchscreentouch2delta);
			AxisControl ctrlTouchscreentouch2deltaright = this.Initialize_ctrlTouchscreentouch2deltaright(kAxisLayout, ctrlTouchscreentouch2delta);
			AxisControl ctrlTouchscreentouch2deltax = this.Initialize_ctrlTouchscreentouch2deltax(kAxisLayout, ctrlTouchscreentouch2delta);
			AxisControl ctrlTouchscreentouch2deltay = this.Initialize_ctrlTouchscreentouch2deltay(kAxisLayout, ctrlTouchscreentouch2delta);
			AxisControl ctrlTouchscreentouch2radiusx = this.Initialize_ctrlTouchscreentouch2radiusx(kAxisLayout, ctrlTouchscreentouch2radius);
			AxisControl ctrlTouchscreentouch2radiusy = this.Initialize_ctrlTouchscreentouch2radiusy(kAxisLayout, ctrlTouchscreentouch2radius);
			AxisControl ctrlTouchscreentouch2startPositionx = this.Initialize_ctrlTouchscreentouch2startPositionx(kAxisLayout, ctrlTouchscreentouch2startPosition);
			AxisControl ctrlTouchscreentouch2startPositiony = this.Initialize_ctrlTouchscreentouch2startPositiony(kAxisLayout, ctrlTouchscreentouch2startPosition);
			IntegerControl ctrlTouchscreentouch3touchId = this.Initialize_ctrlTouchscreentouch3touchId(kIntegerLayout, ctrlTouchscreentouch3);
			Vector2Control ctrlTouchscreentouch3position = this.Initialize_ctrlTouchscreentouch3position(kVector2Layout, ctrlTouchscreentouch3);
			DeltaControl ctrlTouchscreentouch3delta = this.Initialize_ctrlTouchscreentouch3delta(kDeltaLayout, ctrlTouchscreentouch3);
			AxisControl ctrlTouchscreentouch3pressure = this.Initialize_ctrlTouchscreentouch3pressure(kAxisLayout, ctrlTouchscreentouch3);
			Vector2Control ctrlTouchscreentouch3radius = this.Initialize_ctrlTouchscreentouch3radius(kVector2Layout, ctrlTouchscreentouch3);
			TouchPhaseControl ctrlTouchscreentouch3phase = this.Initialize_ctrlTouchscreentouch3phase(kTouchPhaseLayout, ctrlTouchscreentouch3);
			TouchPressControl ctrlTouchscreentouch3press = this.Initialize_ctrlTouchscreentouch3press(kTouchPressLayout, ctrlTouchscreentouch3);
			IntegerControl ctrlTouchscreentouch3tapCount = this.Initialize_ctrlTouchscreentouch3tapCount(kIntegerLayout, ctrlTouchscreentouch3);
			IntegerControl ctrlTouchscreentouch3displayIndex = this.Initialize_ctrlTouchscreentouch3displayIndex(kIntegerLayout, ctrlTouchscreentouch3);
			ButtonControl ctrlTouchscreentouch3indirectTouch = this.Initialize_ctrlTouchscreentouch3indirectTouch(kButtonLayout, ctrlTouchscreentouch3);
			ButtonControl ctrlTouchscreentouch3tap = this.Initialize_ctrlTouchscreentouch3tap(kButtonLayout, ctrlTouchscreentouch3);
			DoubleControl ctrlTouchscreentouch3startTime = this.Initialize_ctrlTouchscreentouch3startTime(kDoubleLayout, ctrlTouchscreentouch3);
			Vector2Control ctrlTouchscreentouch3startPosition = this.Initialize_ctrlTouchscreentouch3startPosition(kVector2Layout, ctrlTouchscreentouch3);
			AxisControl ctrlTouchscreentouch3positionx = this.Initialize_ctrlTouchscreentouch3positionx(kAxisLayout, ctrlTouchscreentouch3position);
			AxisControl ctrlTouchscreentouch3positiony = this.Initialize_ctrlTouchscreentouch3positiony(kAxisLayout, ctrlTouchscreentouch3position);
			AxisControl ctrlTouchscreentouch3deltaup = this.Initialize_ctrlTouchscreentouch3deltaup(kAxisLayout, ctrlTouchscreentouch3delta);
			AxisControl ctrlTouchscreentouch3deltadown = this.Initialize_ctrlTouchscreentouch3deltadown(kAxisLayout, ctrlTouchscreentouch3delta);
			AxisControl ctrlTouchscreentouch3deltaleft = this.Initialize_ctrlTouchscreentouch3deltaleft(kAxisLayout, ctrlTouchscreentouch3delta);
			AxisControl ctrlTouchscreentouch3deltaright = this.Initialize_ctrlTouchscreentouch3deltaright(kAxisLayout, ctrlTouchscreentouch3delta);
			AxisControl ctrlTouchscreentouch3deltax = this.Initialize_ctrlTouchscreentouch3deltax(kAxisLayout, ctrlTouchscreentouch3delta);
			AxisControl ctrlTouchscreentouch3deltay = this.Initialize_ctrlTouchscreentouch3deltay(kAxisLayout, ctrlTouchscreentouch3delta);
			AxisControl ctrlTouchscreentouch3radiusx = this.Initialize_ctrlTouchscreentouch3radiusx(kAxisLayout, ctrlTouchscreentouch3radius);
			AxisControl ctrlTouchscreentouch3radiusy = this.Initialize_ctrlTouchscreentouch3radiusy(kAxisLayout, ctrlTouchscreentouch3radius);
			AxisControl ctrlTouchscreentouch3startPositionx = this.Initialize_ctrlTouchscreentouch3startPositionx(kAxisLayout, ctrlTouchscreentouch3startPosition);
			AxisControl ctrlTouchscreentouch3startPositiony = this.Initialize_ctrlTouchscreentouch3startPositiony(kAxisLayout, ctrlTouchscreentouch3startPosition);
			IntegerControl ctrlTouchscreentouch4touchId = this.Initialize_ctrlTouchscreentouch4touchId(kIntegerLayout, ctrlTouchscreentouch4);
			Vector2Control ctrlTouchscreentouch4position = this.Initialize_ctrlTouchscreentouch4position(kVector2Layout, ctrlTouchscreentouch4);
			DeltaControl ctrlTouchscreentouch4delta = this.Initialize_ctrlTouchscreentouch4delta(kDeltaLayout, ctrlTouchscreentouch4);
			AxisControl ctrlTouchscreentouch4pressure = this.Initialize_ctrlTouchscreentouch4pressure(kAxisLayout, ctrlTouchscreentouch4);
			Vector2Control ctrlTouchscreentouch4radius = this.Initialize_ctrlTouchscreentouch4radius(kVector2Layout, ctrlTouchscreentouch4);
			TouchPhaseControl ctrlTouchscreentouch4phase = this.Initialize_ctrlTouchscreentouch4phase(kTouchPhaseLayout, ctrlTouchscreentouch4);
			TouchPressControl ctrlTouchscreentouch4press = this.Initialize_ctrlTouchscreentouch4press(kTouchPressLayout, ctrlTouchscreentouch4);
			IntegerControl ctrlTouchscreentouch4tapCount = this.Initialize_ctrlTouchscreentouch4tapCount(kIntegerLayout, ctrlTouchscreentouch4);
			IntegerControl ctrlTouchscreentouch4displayIndex = this.Initialize_ctrlTouchscreentouch4displayIndex(kIntegerLayout, ctrlTouchscreentouch4);
			ButtonControl ctrlTouchscreentouch4indirectTouch = this.Initialize_ctrlTouchscreentouch4indirectTouch(kButtonLayout, ctrlTouchscreentouch4);
			ButtonControl ctrlTouchscreentouch4tap = this.Initialize_ctrlTouchscreentouch4tap(kButtonLayout, ctrlTouchscreentouch4);
			DoubleControl ctrlTouchscreentouch4startTime = this.Initialize_ctrlTouchscreentouch4startTime(kDoubleLayout, ctrlTouchscreentouch4);
			Vector2Control ctrlTouchscreentouch4startPosition = this.Initialize_ctrlTouchscreentouch4startPosition(kVector2Layout, ctrlTouchscreentouch4);
			AxisControl ctrlTouchscreentouch4positionx = this.Initialize_ctrlTouchscreentouch4positionx(kAxisLayout, ctrlTouchscreentouch4position);
			AxisControl ctrlTouchscreentouch4positiony = this.Initialize_ctrlTouchscreentouch4positiony(kAxisLayout, ctrlTouchscreentouch4position);
			AxisControl ctrlTouchscreentouch4deltaup = this.Initialize_ctrlTouchscreentouch4deltaup(kAxisLayout, ctrlTouchscreentouch4delta);
			AxisControl ctrlTouchscreentouch4deltadown = this.Initialize_ctrlTouchscreentouch4deltadown(kAxisLayout, ctrlTouchscreentouch4delta);
			AxisControl ctrlTouchscreentouch4deltaleft = this.Initialize_ctrlTouchscreentouch4deltaleft(kAxisLayout, ctrlTouchscreentouch4delta);
			AxisControl ctrlTouchscreentouch4deltaright = this.Initialize_ctrlTouchscreentouch4deltaright(kAxisLayout, ctrlTouchscreentouch4delta);
			AxisControl ctrlTouchscreentouch4deltax = this.Initialize_ctrlTouchscreentouch4deltax(kAxisLayout, ctrlTouchscreentouch4delta);
			AxisControl ctrlTouchscreentouch4deltay = this.Initialize_ctrlTouchscreentouch4deltay(kAxisLayout, ctrlTouchscreentouch4delta);
			AxisControl ctrlTouchscreentouch4radiusx = this.Initialize_ctrlTouchscreentouch4radiusx(kAxisLayout, ctrlTouchscreentouch4radius);
			AxisControl ctrlTouchscreentouch4radiusy = this.Initialize_ctrlTouchscreentouch4radiusy(kAxisLayout, ctrlTouchscreentouch4radius);
			AxisControl ctrlTouchscreentouch4startPositionx = this.Initialize_ctrlTouchscreentouch4startPositionx(kAxisLayout, ctrlTouchscreentouch4startPosition);
			AxisControl ctrlTouchscreentouch4startPositiony = this.Initialize_ctrlTouchscreentouch4startPositiony(kAxisLayout, ctrlTouchscreentouch4startPosition);
			IntegerControl ctrlTouchscreentouch5touchId = this.Initialize_ctrlTouchscreentouch5touchId(kIntegerLayout, ctrlTouchscreentouch5);
			Vector2Control ctrlTouchscreentouch5position = this.Initialize_ctrlTouchscreentouch5position(kVector2Layout, ctrlTouchscreentouch5);
			DeltaControl ctrlTouchscreentouch5delta = this.Initialize_ctrlTouchscreentouch5delta(kDeltaLayout, ctrlTouchscreentouch5);
			AxisControl ctrlTouchscreentouch5pressure = this.Initialize_ctrlTouchscreentouch5pressure(kAxisLayout, ctrlTouchscreentouch5);
			Vector2Control ctrlTouchscreentouch5radius = this.Initialize_ctrlTouchscreentouch5radius(kVector2Layout, ctrlTouchscreentouch5);
			TouchPhaseControl ctrlTouchscreentouch5phase = this.Initialize_ctrlTouchscreentouch5phase(kTouchPhaseLayout, ctrlTouchscreentouch5);
			TouchPressControl ctrlTouchscreentouch5press = this.Initialize_ctrlTouchscreentouch5press(kTouchPressLayout, ctrlTouchscreentouch5);
			IntegerControl ctrlTouchscreentouch5tapCount = this.Initialize_ctrlTouchscreentouch5tapCount(kIntegerLayout, ctrlTouchscreentouch5);
			IntegerControl ctrlTouchscreentouch5displayIndex = this.Initialize_ctrlTouchscreentouch5displayIndex(kIntegerLayout, ctrlTouchscreentouch5);
			ButtonControl ctrlTouchscreentouch5indirectTouch = this.Initialize_ctrlTouchscreentouch5indirectTouch(kButtonLayout, ctrlTouchscreentouch5);
			ButtonControl ctrlTouchscreentouch5tap = this.Initialize_ctrlTouchscreentouch5tap(kButtonLayout, ctrlTouchscreentouch5);
			DoubleControl ctrlTouchscreentouch5startTime = this.Initialize_ctrlTouchscreentouch5startTime(kDoubleLayout, ctrlTouchscreentouch5);
			Vector2Control ctrlTouchscreentouch5startPosition = this.Initialize_ctrlTouchscreentouch5startPosition(kVector2Layout, ctrlTouchscreentouch5);
			AxisControl ctrlTouchscreentouch5positionx = this.Initialize_ctrlTouchscreentouch5positionx(kAxisLayout, ctrlTouchscreentouch5position);
			AxisControl ctrlTouchscreentouch5positiony = this.Initialize_ctrlTouchscreentouch5positiony(kAxisLayout, ctrlTouchscreentouch5position);
			AxisControl ctrlTouchscreentouch5deltaup = this.Initialize_ctrlTouchscreentouch5deltaup(kAxisLayout, ctrlTouchscreentouch5delta);
			AxisControl ctrlTouchscreentouch5deltadown = this.Initialize_ctrlTouchscreentouch5deltadown(kAxisLayout, ctrlTouchscreentouch5delta);
			AxisControl ctrlTouchscreentouch5deltaleft = this.Initialize_ctrlTouchscreentouch5deltaleft(kAxisLayout, ctrlTouchscreentouch5delta);
			AxisControl ctrlTouchscreentouch5deltaright = this.Initialize_ctrlTouchscreentouch5deltaright(kAxisLayout, ctrlTouchscreentouch5delta);
			AxisControl ctrlTouchscreentouch5deltax = this.Initialize_ctrlTouchscreentouch5deltax(kAxisLayout, ctrlTouchscreentouch5delta);
			AxisControl ctrlTouchscreentouch5deltay = this.Initialize_ctrlTouchscreentouch5deltay(kAxisLayout, ctrlTouchscreentouch5delta);
			AxisControl ctrlTouchscreentouch5radiusx = this.Initialize_ctrlTouchscreentouch5radiusx(kAxisLayout, ctrlTouchscreentouch5radius);
			AxisControl ctrlTouchscreentouch5radiusy = this.Initialize_ctrlTouchscreentouch5radiusy(kAxisLayout, ctrlTouchscreentouch5radius);
			AxisControl ctrlTouchscreentouch5startPositionx = this.Initialize_ctrlTouchscreentouch5startPositionx(kAxisLayout, ctrlTouchscreentouch5startPosition);
			AxisControl ctrlTouchscreentouch5startPositiony = this.Initialize_ctrlTouchscreentouch5startPositiony(kAxisLayout, ctrlTouchscreentouch5startPosition);
			IntegerControl ctrlTouchscreentouch6touchId = this.Initialize_ctrlTouchscreentouch6touchId(kIntegerLayout, ctrlTouchscreentouch6);
			Vector2Control ctrlTouchscreentouch6position = this.Initialize_ctrlTouchscreentouch6position(kVector2Layout, ctrlTouchscreentouch6);
			DeltaControl ctrlTouchscreentouch6delta = this.Initialize_ctrlTouchscreentouch6delta(kDeltaLayout, ctrlTouchscreentouch6);
			AxisControl ctrlTouchscreentouch6pressure = this.Initialize_ctrlTouchscreentouch6pressure(kAxisLayout, ctrlTouchscreentouch6);
			Vector2Control ctrlTouchscreentouch6radius = this.Initialize_ctrlTouchscreentouch6radius(kVector2Layout, ctrlTouchscreentouch6);
			TouchPhaseControl ctrlTouchscreentouch6phase = this.Initialize_ctrlTouchscreentouch6phase(kTouchPhaseLayout, ctrlTouchscreentouch6);
			TouchPressControl ctrlTouchscreentouch6press = this.Initialize_ctrlTouchscreentouch6press(kTouchPressLayout, ctrlTouchscreentouch6);
			IntegerControl ctrlTouchscreentouch6tapCount = this.Initialize_ctrlTouchscreentouch6tapCount(kIntegerLayout, ctrlTouchscreentouch6);
			IntegerControl ctrlTouchscreentouch6displayIndex = this.Initialize_ctrlTouchscreentouch6displayIndex(kIntegerLayout, ctrlTouchscreentouch6);
			ButtonControl ctrlTouchscreentouch6indirectTouch = this.Initialize_ctrlTouchscreentouch6indirectTouch(kButtonLayout, ctrlTouchscreentouch6);
			ButtonControl ctrlTouchscreentouch6tap = this.Initialize_ctrlTouchscreentouch6tap(kButtonLayout, ctrlTouchscreentouch6);
			DoubleControl ctrlTouchscreentouch6startTime = this.Initialize_ctrlTouchscreentouch6startTime(kDoubleLayout, ctrlTouchscreentouch6);
			Vector2Control ctrlTouchscreentouch6startPosition = this.Initialize_ctrlTouchscreentouch6startPosition(kVector2Layout, ctrlTouchscreentouch6);
			AxisControl ctrlTouchscreentouch6positionx = this.Initialize_ctrlTouchscreentouch6positionx(kAxisLayout, ctrlTouchscreentouch6position);
			AxisControl ctrlTouchscreentouch6positiony = this.Initialize_ctrlTouchscreentouch6positiony(kAxisLayout, ctrlTouchscreentouch6position);
			AxisControl ctrlTouchscreentouch6deltaup = this.Initialize_ctrlTouchscreentouch6deltaup(kAxisLayout, ctrlTouchscreentouch6delta);
			AxisControl ctrlTouchscreentouch6deltadown = this.Initialize_ctrlTouchscreentouch6deltadown(kAxisLayout, ctrlTouchscreentouch6delta);
			AxisControl ctrlTouchscreentouch6deltaleft = this.Initialize_ctrlTouchscreentouch6deltaleft(kAxisLayout, ctrlTouchscreentouch6delta);
			AxisControl ctrlTouchscreentouch6deltaright = this.Initialize_ctrlTouchscreentouch6deltaright(kAxisLayout, ctrlTouchscreentouch6delta);
			AxisControl ctrlTouchscreentouch6deltax = this.Initialize_ctrlTouchscreentouch6deltax(kAxisLayout, ctrlTouchscreentouch6delta);
			AxisControl ctrlTouchscreentouch6deltay = this.Initialize_ctrlTouchscreentouch6deltay(kAxisLayout, ctrlTouchscreentouch6delta);
			AxisControl ctrlTouchscreentouch6radiusx = this.Initialize_ctrlTouchscreentouch6radiusx(kAxisLayout, ctrlTouchscreentouch6radius);
			AxisControl ctrlTouchscreentouch6radiusy = this.Initialize_ctrlTouchscreentouch6radiusy(kAxisLayout, ctrlTouchscreentouch6radius);
			AxisControl ctrlTouchscreentouch6startPositionx = this.Initialize_ctrlTouchscreentouch6startPositionx(kAxisLayout, ctrlTouchscreentouch6startPosition);
			AxisControl ctrlTouchscreentouch6startPositiony = this.Initialize_ctrlTouchscreentouch6startPositiony(kAxisLayout, ctrlTouchscreentouch6startPosition);
			IntegerControl ctrlTouchscreentouch7touchId = this.Initialize_ctrlTouchscreentouch7touchId(kIntegerLayout, ctrlTouchscreentouch7);
			Vector2Control ctrlTouchscreentouch7position = this.Initialize_ctrlTouchscreentouch7position(kVector2Layout, ctrlTouchscreentouch7);
			DeltaControl ctrlTouchscreentouch7delta = this.Initialize_ctrlTouchscreentouch7delta(kDeltaLayout, ctrlTouchscreentouch7);
			AxisControl ctrlTouchscreentouch7pressure = this.Initialize_ctrlTouchscreentouch7pressure(kAxisLayout, ctrlTouchscreentouch7);
			Vector2Control ctrlTouchscreentouch7radius = this.Initialize_ctrlTouchscreentouch7radius(kVector2Layout, ctrlTouchscreentouch7);
			TouchPhaseControl ctrlTouchscreentouch7phase = this.Initialize_ctrlTouchscreentouch7phase(kTouchPhaseLayout, ctrlTouchscreentouch7);
			TouchPressControl ctrlTouchscreentouch7press = this.Initialize_ctrlTouchscreentouch7press(kTouchPressLayout, ctrlTouchscreentouch7);
			IntegerControl ctrlTouchscreentouch7tapCount = this.Initialize_ctrlTouchscreentouch7tapCount(kIntegerLayout, ctrlTouchscreentouch7);
			IntegerControl ctrlTouchscreentouch7displayIndex = this.Initialize_ctrlTouchscreentouch7displayIndex(kIntegerLayout, ctrlTouchscreentouch7);
			ButtonControl ctrlTouchscreentouch7indirectTouch = this.Initialize_ctrlTouchscreentouch7indirectTouch(kButtonLayout, ctrlTouchscreentouch7);
			ButtonControl ctrlTouchscreentouch7tap = this.Initialize_ctrlTouchscreentouch7tap(kButtonLayout, ctrlTouchscreentouch7);
			DoubleControl ctrlTouchscreentouch7startTime = this.Initialize_ctrlTouchscreentouch7startTime(kDoubleLayout, ctrlTouchscreentouch7);
			Vector2Control ctrlTouchscreentouch7startPosition = this.Initialize_ctrlTouchscreentouch7startPosition(kVector2Layout, ctrlTouchscreentouch7);
			AxisControl ctrlTouchscreentouch7positionx = this.Initialize_ctrlTouchscreentouch7positionx(kAxisLayout, ctrlTouchscreentouch7position);
			AxisControl ctrlTouchscreentouch7positiony = this.Initialize_ctrlTouchscreentouch7positiony(kAxisLayout, ctrlTouchscreentouch7position);
			AxisControl ctrlTouchscreentouch7deltaup = this.Initialize_ctrlTouchscreentouch7deltaup(kAxisLayout, ctrlTouchscreentouch7delta);
			AxisControl ctrlTouchscreentouch7deltadown = this.Initialize_ctrlTouchscreentouch7deltadown(kAxisLayout, ctrlTouchscreentouch7delta);
			AxisControl ctrlTouchscreentouch7deltaleft = this.Initialize_ctrlTouchscreentouch7deltaleft(kAxisLayout, ctrlTouchscreentouch7delta);
			AxisControl ctrlTouchscreentouch7deltaright = this.Initialize_ctrlTouchscreentouch7deltaright(kAxisLayout, ctrlTouchscreentouch7delta);
			AxisControl ctrlTouchscreentouch7deltax = this.Initialize_ctrlTouchscreentouch7deltax(kAxisLayout, ctrlTouchscreentouch7delta);
			AxisControl ctrlTouchscreentouch7deltay = this.Initialize_ctrlTouchscreentouch7deltay(kAxisLayout, ctrlTouchscreentouch7delta);
			AxisControl ctrlTouchscreentouch7radiusx = this.Initialize_ctrlTouchscreentouch7radiusx(kAxisLayout, ctrlTouchscreentouch7radius);
			AxisControl ctrlTouchscreentouch7radiusy = this.Initialize_ctrlTouchscreentouch7radiusy(kAxisLayout, ctrlTouchscreentouch7radius);
			AxisControl ctrlTouchscreentouch7startPositionx = this.Initialize_ctrlTouchscreentouch7startPositionx(kAxisLayout, ctrlTouchscreentouch7startPosition);
			AxisControl ctrlTouchscreentouch7startPositiony = this.Initialize_ctrlTouchscreentouch7startPositiony(kAxisLayout, ctrlTouchscreentouch7startPosition);
			IntegerControl ctrlTouchscreentouch8touchId = this.Initialize_ctrlTouchscreentouch8touchId(kIntegerLayout, ctrlTouchscreentouch8);
			Vector2Control ctrlTouchscreentouch8position = this.Initialize_ctrlTouchscreentouch8position(kVector2Layout, ctrlTouchscreentouch8);
			DeltaControl ctrlTouchscreentouch8delta = this.Initialize_ctrlTouchscreentouch8delta(kDeltaLayout, ctrlTouchscreentouch8);
			AxisControl ctrlTouchscreentouch8pressure = this.Initialize_ctrlTouchscreentouch8pressure(kAxisLayout, ctrlTouchscreentouch8);
			Vector2Control ctrlTouchscreentouch8radius = this.Initialize_ctrlTouchscreentouch8radius(kVector2Layout, ctrlTouchscreentouch8);
			TouchPhaseControl ctrlTouchscreentouch8phase = this.Initialize_ctrlTouchscreentouch8phase(kTouchPhaseLayout, ctrlTouchscreentouch8);
			TouchPressControl ctrlTouchscreentouch8press = this.Initialize_ctrlTouchscreentouch8press(kTouchPressLayout, ctrlTouchscreentouch8);
			IntegerControl ctrlTouchscreentouch8tapCount = this.Initialize_ctrlTouchscreentouch8tapCount(kIntegerLayout, ctrlTouchscreentouch8);
			IntegerControl ctrlTouchscreentouch8displayIndex = this.Initialize_ctrlTouchscreentouch8displayIndex(kIntegerLayout, ctrlTouchscreentouch8);
			ButtonControl ctrlTouchscreentouch8indirectTouch = this.Initialize_ctrlTouchscreentouch8indirectTouch(kButtonLayout, ctrlTouchscreentouch8);
			ButtonControl ctrlTouchscreentouch8tap = this.Initialize_ctrlTouchscreentouch8tap(kButtonLayout, ctrlTouchscreentouch8);
			DoubleControl ctrlTouchscreentouch8startTime = this.Initialize_ctrlTouchscreentouch8startTime(kDoubleLayout, ctrlTouchscreentouch8);
			Vector2Control ctrlTouchscreentouch8startPosition = this.Initialize_ctrlTouchscreentouch8startPosition(kVector2Layout, ctrlTouchscreentouch8);
			AxisControl ctrlTouchscreentouch8positionx = this.Initialize_ctrlTouchscreentouch8positionx(kAxisLayout, ctrlTouchscreentouch8position);
			AxisControl ctrlTouchscreentouch8positiony = this.Initialize_ctrlTouchscreentouch8positiony(kAxisLayout, ctrlTouchscreentouch8position);
			AxisControl ctrlTouchscreentouch8deltaup = this.Initialize_ctrlTouchscreentouch8deltaup(kAxisLayout, ctrlTouchscreentouch8delta);
			AxisControl ctrlTouchscreentouch8deltadown = this.Initialize_ctrlTouchscreentouch8deltadown(kAxisLayout, ctrlTouchscreentouch8delta);
			AxisControl ctrlTouchscreentouch8deltaleft = this.Initialize_ctrlTouchscreentouch8deltaleft(kAxisLayout, ctrlTouchscreentouch8delta);
			AxisControl ctrlTouchscreentouch8deltaright = this.Initialize_ctrlTouchscreentouch8deltaright(kAxisLayout, ctrlTouchscreentouch8delta);
			AxisControl ctrlTouchscreentouch8deltax = this.Initialize_ctrlTouchscreentouch8deltax(kAxisLayout, ctrlTouchscreentouch8delta);
			AxisControl ctrlTouchscreentouch8deltay = this.Initialize_ctrlTouchscreentouch8deltay(kAxisLayout, ctrlTouchscreentouch8delta);
			AxisControl ctrlTouchscreentouch8radiusx = this.Initialize_ctrlTouchscreentouch8radiusx(kAxisLayout, ctrlTouchscreentouch8radius);
			AxisControl ctrlTouchscreentouch8radiusy = this.Initialize_ctrlTouchscreentouch8radiusy(kAxisLayout, ctrlTouchscreentouch8radius);
			AxisControl ctrlTouchscreentouch8startPositionx = this.Initialize_ctrlTouchscreentouch8startPositionx(kAxisLayout, ctrlTouchscreentouch8startPosition);
			AxisControl ctrlTouchscreentouch8startPositiony = this.Initialize_ctrlTouchscreentouch8startPositiony(kAxisLayout, ctrlTouchscreentouch8startPosition);
			IntegerControl ctrlTouchscreentouch9touchId = this.Initialize_ctrlTouchscreentouch9touchId(kIntegerLayout, ctrlTouchscreentouch9);
			Vector2Control ctrlTouchscreentouch9position = this.Initialize_ctrlTouchscreentouch9position(kVector2Layout, ctrlTouchscreentouch9);
			DeltaControl ctrlTouchscreentouch9delta = this.Initialize_ctrlTouchscreentouch9delta(kDeltaLayout, ctrlTouchscreentouch9);
			AxisControl ctrlTouchscreentouch9pressure = this.Initialize_ctrlTouchscreentouch9pressure(kAxisLayout, ctrlTouchscreentouch9);
			Vector2Control ctrlTouchscreentouch9radius = this.Initialize_ctrlTouchscreentouch9radius(kVector2Layout, ctrlTouchscreentouch9);
			TouchPhaseControl ctrlTouchscreentouch9phase = this.Initialize_ctrlTouchscreentouch9phase(kTouchPhaseLayout, ctrlTouchscreentouch9);
			TouchPressControl ctrlTouchscreentouch9press = this.Initialize_ctrlTouchscreentouch9press(kTouchPressLayout, ctrlTouchscreentouch9);
			IntegerControl ctrlTouchscreentouch9tapCount = this.Initialize_ctrlTouchscreentouch9tapCount(kIntegerLayout, ctrlTouchscreentouch9);
			IntegerControl ctrlTouchscreentouch9displayIndex = this.Initialize_ctrlTouchscreentouch9displayIndex(kIntegerLayout, ctrlTouchscreentouch9);
			ButtonControl ctrlTouchscreentouch9indirectTouch = this.Initialize_ctrlTouchscreentouch9indirectTouch(kButtonLayout, ctrlTouchscreentouch9);
			ButtonControl ctrlTouchscreentouch9tap = this.Initialize_ctrlTouchscreentouch9tap(kButtonLayout, ctrlTouchscreentouch9);
			DoubleControl ctrlTouchscreentouch9startTime = this.Initialize_ctrlTouchscreentouch9startTime(kDoubleLayout, ctrlTouchscreentouch9);
			Vector2Control ctrlTouchscreentouch9startPosition = this.Initialize_ctrlTouchscreentouch9startPosition(kVector2Layout, ctrlTouchscreentouch9);
			AxisControl ctrlTouchscreentouch9positionx = this.Initialize_ctrlTouchscreentouch9positionx(kAxisLayout, ctrlTouchscreentouch9position);
			AxisControl ctrlTouchscreentouch9positiony = this.Initialize_ctrlTouchscreentouch9positiony(kAxisLayout, ctrlTouchscreentouch9position);
			AxisControl ctrlTouchscreentouch9deltaup = this.Initialize_ctrlTouchscreentouch9deltaup(kAxisLayout, ctrlTouchscreentouch9delta);
			AxisControl ctrlTouchscreentouch9deltadown = this.Initialize_ctrlTouchscreentouch9deltadown(kAxisLayout, ctrlTouchscreentouch9delta);
			AxisControl ctrlTouchscreentouch9deltaleft = this.Initialize_ctrlTouchscreentouch9deltaleft(kAxisLayout, ctrlTouchscreentouch9delta);
			AxisControl ctrlTouchscreentouch9deltaright = this.Initialize_ctrlTouchscreentouch9deltaright(kAxisLayout, ctrlTouchscreentouch9delta);
			AxisControl ctrlTouchscreentouch9deltax = this.Initialize_ctrlTouchscreentouch9deltax(kAxisLayout, ctrlTouchscreentouch9delta);
			AxisControl ctrlTouchscreentouch9deltay = this.Initialize_ctrlTouchscreentouch9deltay(kAxisLayout, ctrlTouchscreentouch9delta);
			AxisControl ctrlTouchscreentouch9radiusx = this.Initialize_ctrlTouchscreentouch9radiusx(kAxisLayout, ctrlTouchscreentouch9radius);
			AxisControl ctrlTouchscreentouch9radiusy = this.Initialize_ctrlTouchscreentouch9radiusy(kAxisLayout, ctrlTouchscreentouch9radius);
			AxisControl ctrlTouchscreentouch9startPositionx = this.Initialize_ctrlTouchscreentouch9startPositionx(kAxisLayout, ctrlTouchscreentouch9startPosition);
			AxisControl ctrlTouchscreentouch9startPositiony = this.Initialize_ctrlTouchscreentouch9startPositiony(kAxisLayout, ctrlTouchscreentouch9startPosition);
			builder.WithControlUsage(0, new InternedString("PrimaryAction"), ctrlTouchscreenprimaryTouchtap);
			builder.WithControlUsage(1, new InternedString("Point"), ctrlTouchscreenposition);
			builder.WithControlUsage(2, new InternedString("Secondary2DMotion"), ctrlTouchscreendelta);
			builder.WithControlUsage(3, new InternedString("Pressure"), ctrlTouchscreenpressure);
			builder.WithControlUsage(4, new InternedString("Radius"), ctrlTouchscreenradius);
			base.touchControlArray = new TouchControl[10];
			base.touchControlArray[0] = ctrlTouchscreentouch0;
			base.touchControlArray[1] = ctrlTouchscreentouch;
			base.touchControlArray[2] = ctrlTouchscreentouch2;
			base.touchControlArray[3] = ctrlTouchscreentouch3;
			base.touchControlArray[4] = ctrlTouchscreentouch4;
			base.touchControlArray[5] = ctrlTouchscreentouch5;
			base.touchControlArray[6] = ctrlTouchscreentouch6;
			base.touchControlArray[7] = ctrlTouchscreentouch7;
			base.touchControlArray[8] = ctrlTouchscreentouch8;
			base.touchControlArray[9] = ctrlTouchscreentouch9;
			base.primaryTouch = ctrlTouchscreenprimaryTouch;
			base.position = ctrlTouchscreenposition;
			base.delta = ctrlTouchscreendelta;
			base.radius = ctrlTouchscreenradius;
			base.pressure = ctrlTouchscreenpressure;
			base.press = ctrlTouchscreenpress;
			base.displayIndex = ctrlTouchscreendisplayIndex;
			ctrlTouchscreenprimaryTouch.press = ctrlTouchscreenprimaryTouchpress;
			ctrlTouchscreenprimaryTouch.displayIndex = ctrlTouchscreenprimaryTouchdisplayIndex;
			ctrlTouchscreenprimaryTouch.touchId = ctrlTouchscreenprimaryTouchtouchId;
			ctrlTouchscreenprimaryTouch.position = ctrlTouchscreenprimaryTouchposition;
			ctrlTouchscreenprimaryTouch.delta = ctrlTouchscreenprimaryTouchdelta;
			ctrlTouchscreenprimaryTouch.pressure = ctrlTouchscreenprimaryTouchpressure;
			ctrlTouchscreenprimaryTouch.radius = ctrlTouchscreenprimaryTouchradius;
			ctrlTouchscreenprimaryTouch.phase = ctrlTouchscreenprimaryTouchphase;
			ctrlTouchscreenprimaryTouch.indirectTouch = ctrlTouchscreenprimaryTouchindirectTouch;
			ctrlTouchscreenprimaryTouch.tap = ctrlTouchscreenprimaryTouchtap;
			ctrlTouchscreenprimaryTouch.tapCount = ctrlTouchscreenprimaryTouchtapCount;
			ctrlTouchscreenprimaryTouch.startTime = ctrlTouchscreenprimaryTouchstartTime;
			ctrlTouchscreenprimaryTouch.startPosition = ctrlTouchscreenprimaryTouchstartPosition;
			ctrlTouchscreenposition.x = ctrlTouchscreenpositionx;
			ctrlTouchscreenposition.y = ctrlTouchscreenpositiony;
			ctrlTouchscreendelta.up = ctrlTouchscreendeltaup;
			ctrlTouchscreendelta.down = ctrlTouchscreendeltadown;
			ctrlTouchscreendelta.left = ctrlTouchscreendeltaleft;
			ctrlTouchscreendelta.right = ctrlTouchscreendeltaright;
			ctrlTouchscreendelta.x = ctrlTouchscreendeltax;
			ctrlTouchscreendelta.y = ctrlTouchscreendeltay;
			ctrlTouchscreenradius.x = ctrlTouchscreenradiusx;
			ctrlTouchscreenradius.y = ctrlTouchscreenradiusy;
			ctrlTouchscreentouch0.press = ctrlTouchscreentouch0press;
			ctrlTouchscreentouch0.displayIndex = ctrlTouchscreentouch0displayIndex;
			ctrlTouchscreentouch0.touchId = ctrlTouchscreentouch0touchId;
			ctrlTouchscreentouch0.position = ctrlTouchscreentouch0position;
			ctrlTouchscreentouch0.delta = ctrlTouchscreentouch0delta;
			ctrlTouchscreentouch0.pressure = ctrlTouchscreentouch0pressure;
			ctrlTouchscreentouch0.radius = ctrlTouchscreentouch0radius;
			ctrlTouchscreentouch0.phase = ctrlTouchscreentouch0phase;
			ctrlTouchscreentouch0.indirectTouch = ctrlTouchscreentouch0indirectTouch;
			ctrlTouchscreentouch0.tap = ctrlTouchscreentouch0tap;
			ctrlTouchscreentouch0.tapCount = ctrlTouchscreentouch0tapCount;
			ctrlTouchscreentouch0.startTime = ctrlTouchscreentouch0startTime;
			ctrlTouchscreentouch0.startPosition = ctrlTouchscreentouch0startPosition;
			ctrlTouchscreentouch.press = ctrlTouchscreentouch1press;
			ctrlTouchscreentouch.displayIndex = ctrlTouchscreentouch1displayIndex;
			ctrlTouchscreentouch.touchId = ctrlTouchscreentouch1touchId;
			ctrlTouchscreentouch.position = ctrlTouchscreentouch1position;
			ctrlTouchscreentouch.delta = ctrlTouchscreentouch1delta;
			ctrlTouchscreentouch.pressure = ctrlTouchscreentouch1pressure;
			ctrlTouchscreentouch.radius = ctrlTouchscreentouch1radius;
			ctrlTouchscreentouch.phase = ctrlTouchscreentouch1phase;
			ctrlTouchscreentouch.indirectTouch = ctrlTouchscreentouch1indirectTouch;
			ctrlTouchscreentouch.tap = ctrlTouchscreentouch1tap;
			ctrlTouchscreentouch.tapCount = ctrlTouchscreentouch1tapCount;
			ctrlTouchscreentouch.startTime = ctrlTouchscreentouch1startTime;
			ctrlTouchscreentouch.startPosition = ctrlTouchscreentouch1startPosition;
			ctrlTouchscreentouch2.press = ctrlTouchscreentouch2press;
			ctrlTouchscreentouch2.displayIndex = ctrlTouchscreentouch2displayIndex;
			ctrlTouchscreentouch2.touchId = ctrlTouchscreentouch2touchId;
			ctrlTouchscreentouch2.position = ctrlTouchscreentouch2position;
			ctrlTouchscreentouch2.delta = ctrlTouchscreentouch2delta;
			ctrlTouchscreentouch2.pressure = ctrlTouchscreentouch2pressure;
			ctrlTouchscreentouch2.radius = ctrlTouchscreentouch2radius;
			ctrlTouchscreentouch2.phase = ctrlTouchscreentouch2phase;
			ctrlTouchscreentouch2.indirectTouch = ctrlTouchscreentouch2indirectTouch;
			ctrlTouchscreentouch2.tap = ctrlTouchscreentouch2tap;
			ctrlTouchscreentouch2.tapCount = ctrlTouchscreentouch2tapCount;
			ctrlTouchscreentouch2.startTime = ctrlTouchscreentouch2startTime;
			ctrlTouchscreentouch2.startPosition = ctrlTouchscreentouch2startPosition;
			ctrlTouchscreentouch3.press = ctrlTouchscreentouch3press;
			ctrlTouchscreentouch3.displayIndex = ctrlTouchscreentouch3displayIndex;
			ctrlTouchscreentouch3.touchId = ctrlTouchscreentouch3touchId;
			ctrlTouchscreentouch3.position = ctrlTouchscreentouch3position;
			ctrlTouchscreentouch3.delta = ctrlTouchscreentouch3delta;
			ctrlTouchscreentouch3.pressure = ctrlTouchscreentouch3pressure;
			ctrlTouchscreentouch3.radius = ctrlTouchscreentouch3radius;
			ctrlTouchscreentouch3.phase = ctrlTouchscreentouch3phase;
			ctrlTouchscreentouch3.indirectTouch = ctrlTouchscreentouch3indirectTouch;
			ctrlTouchscreentouch3.tap = ctrlTouchscreentouch3tap;
			ctrlTouchscreentouch3.tapCount = ctrlTouchscreentouch3tapCount;
			ctrlTouchscreentouch3.startTime = ctrlTouchscreentouch3startTime;
			ctrlTouchscreentouch3.startPosition = ctrlTouchscreentouch3startPosition;
			ctrlTouchscreentouch4.press = ctrlTouchscreentouch4press;
			ctrlTouchscreentouch4.displayIndex = ctrlTouchscreentouch4displayIndex;
			ctrlTouchscreentouch4.touchId = ctrlTouchscreentouch4touchId;
			ctrlTouchscreentouch4.position = ctrlTouchscreentouch4position;
			ctrlTouchscreentouch4.delta = ctrlTouchscreentouch4delta;
			ctrlTouchscreentouch4.pressure = ctrlTouchscreentouch4pressure;
			ctrlTouchscreentouch4.radius = ctrlTouchscreentouch4radius;
			ctrlTouchscreentouch4.phase = ctrlTouchscreentouch4phase;
			ctrlTouchscreentouch4.indirectTouch = ctrlTouchscreentouch4indirectTouch;
			ctrlTouchscreentouch4.tap = ctrlTouchscreentouch4tap;
			ctrlTouchscreentouch4.tapCount = ctrlTouchscreentouch4tapCount;
			ctrlTouchscreentouch4.startTime = ctrlTouchscreentouch4startTime;
			ctrlTouchscreentouch4.startPosition = ctrlTouchscreentouch4startPosition;
			ctrlTouchscreentouch5.press = ctrlTouchscreentouch5press;
			ctrlTouchscreentouch5.displayIndex = ctrlTouchscreentouch5displayIndex;
			ctrlTouchscreentouch5.touchId = ctrlTouchscreentouch5touchId;
			ctrlTouchscreentouch5.position = ctrlTouchscreentouch5position;
			ctrlTouchscreentouch5.delta = ctrlTouchscreentouch5delta;
			ctrlTouchscreentouch5.pressure = ctrlTouchscreentouch5pressure;
			ctrlTouchscreentouch5.radius = ctrlTouchscreentouch5radius;
			ctrlTouchscreentouch5.phase = ctrlTouchscreentouch5phase;
			ctrlTouchscreentouch5.indirectTouch = ctrlTouchscreentouch5indirectTouch;
			ctrlTouchscreentouch5.tap = ctrlTouchscreentouch5tap;
			ctrlTouchscreentouch5.tapCount = ctrlTouchscreentouch5tapCount;
			ctrlTouchscreentouch5.startTime = ctrlTouchscreentouch5startTime;
			ctrlTouchscreentouch5.startPosition = ctrlTouchscreentouch5startPosition;
			ctrlTouchscreentouch6.press = ctrlTouchscreentouch6press;
			ctrlTouchscreentouch6.displayIndex = ctrlTouchscreentouch6displayIndex;
			ctrlTouchscreentouch6.touchId = ctrlTouchscreentouch6touchId;
			ctrlTouchscreentouch6.position = ctrlTouchscreentouch6position;
			ctrlTouchscreentouch6.delta = ctrlTouchscreentouch6delta;
			ctrlTouchscreentouch6.pressure = ctrlTouchscreentouch6pressure;
			ctrlTouchscreentouch6.radius = ctrlTouchscreentouch6radius;
			ctrlTouchscreentouch6.phase = ctrlTouchscreentouch6phase;
			ctrlTouchscreentouch6.indirectTouch = ctrlTouchscreentouch6indirectTouch;
			ctrlTouchscreentouch6.tap = ctrlTouchscreentouch6tap;
			ctrlTouchscreentouch6.tapCount = ctrlTouchscreentouch6tapCount;
			ctrlTouchscreentouch6.startTime = ctrlTouchscreentouch6startTime;
			ctrlTouchscreentouch6.startPosition = ctrlTouchscreentouch6startPosition;
			ctrlTouchscreentouch7.press = ctrlTouchscreentouch7press;
			ctrlTouchscreentouch7.displayIndex = ctrlTouchscreentouch7displayIndex;
			ctrlTouchscreentouch7.touchId = ctrlTouchscreentouch7touchId;
			ctrlTouchscreentouch7.position = ctrlTouchscreentouch7position;
			ctrlTouchscreentouch7.delta = ctrlTouchscreentouch7delta;
			ctrlTouchscreentouch7.pressure = ctrlTouchscreentouch7pressure;
			ctrlTouchscreentouch7.radius = ctrlTouchscreentouch7radius;
			ctrlTouchscreentouch7.phase = ctrlTouchscreentouch7phase;
			ctrlTouchscreentouch7.indirectTouch = ctrlTouchscreentouch7indirectTouch;
			ctrlTouchscreentouch7.tap = ctrlTouchscreentouch7tap;
			ctrlTouchscreentouch7.tapCount = ctrlTouchscreentouch7tapCount;
			ctrlTouchscreentouch7.startTime = ctrlTouchscreentouch7startTime;
			ctrlTouchscreentouch7.startPosition = ctrlTouchscreentouch7startPosition;
			ctrlTouchscreentouch8.press = ctrlTouchscreentouch8press;
			ctrlTouchscreentouch8.displayIndex = ctrlTouchscreentouch8displayIndex;
			ctrlTouchscreentouch8.touchId = ctrlTouchscreentouch8touchId;
			ctrlTouchscreentouch8.position = ctrlTouchscreentouch8position;
			ctrlTouchscreentouch8.delta = ctrlTouchscreentouch8delta;
			ctrlTouchscreentouch8.pressure = ctrlTouchscreentouch8pressure;
			ctrlTouchscreentouch8.radius = ctrlTouchscreentouch8radius;
			ctrlTouchscreentouch8.phase = ctrlTouchscreentouch8phase;
			ctrlTouchscreentouch8.indirectTouch = ctrlTouchscreentouch8indirectTouch;
			ctrlTouchscreentouch8.tap = ctrlTouchscreentouch8tap;
			ctrlTouchscreentouch8.tapCount = ctrlTouchscreentouch8tapCount;
			ctrlTouchscreentouch8.startTime = ctrlTouchscreentouch8startTime;
			ctrlTouchscreentouch8.startPosition = ctrlTouchscreentouch8startPosition;
			ctrlTouchscreentouch9.press = ctrlTouchscreentouch9press;
			ctrlTouchscreentouch9.displayIndex = ctrlTouchscreentouch9displayIndex;
			ctrlTouchscreentouch9.touchId = ctrlTouchscreentouch9touchId;
			ctrlTouchscreentouch9.position = ctrlTouchscreentouch9position;
			ctrlTouchscreentouch9.delta = ctrlTouchscreentouch9delta;
			ctrlTouchscreentouch9.pressure = ctrlTouchscreentouch9pressure;
			ctrlTouchscreentouch9.radius = ctrlTouchscreentouch9radius;
			ctrlTouchscreentouch9.phase = ctrlTouchscreentouch9phase;
			ctrlTouchscreentouch9.indirectTouch = ctrlTouchscreentouch9indirectTouch;
			ctrlTouchscreentouch9.tap = ctrlTouchscreentouch9tap;
			ctrlTouchscreentouch9.tapCount = ctrlTouchscreentouch9tapCount;
			ctrlTouchscreentouch9.startTime = ctrlTouchscreentouch9startTime;
			ctrlTouchscreentouch9.startPosition = ctrlTouchscreentouch9startPosition;
			ctrlTouchscreenprimaryTouchposition.x = ctrlTouchscreenprimaryTouchpositionx;
			ctrlTouchscreenprimaryTouchposition.y = ctrlTouchscreenprimaryTouchpositiony;
			ctrlTouchscreenprimaryTouchdelta.up = ctrlTouchscreenprimaryTouchdeltaup;
			ctrlTouchscreenprimaryTouchdelta.down = ctrlTouchscreenprimaryTouchdeltadown;
			ctrlTouchscreenprimaryTouchdelta.left = ctrlTouchscreenprimaryTouchdeltaleft;
			ctrlTouchscreenprimaryTouchdelta.right = ctrlTouchscreenprimaryTouchdeltaright;
			ctrlTouchscreenprimaryTouchdelta.x = ctrlTouchscreenprimaryTouchdeltax;
			ctrlTouchscreenprimaryTouchdelta.y = ctrlTouchscreenprimaryTouchdeltay;
			ctrlTouchscreenprimaryTouchradius.x = ctrlTouchscreenprimaryTouchradiusx;
			ctrlTouchscreenprimaryTouchradius.y = ctrlTouchscreenprimaryTouchradiusy;
			ctrlTouchscreenprimaryTouchstartPosition.x = ctrlTouchscreenprimaryTouchstartPositionx;
			ctrlTouchscreenprimaryTouchstartPosition.y = ctrlTouchscreenprimaryTouchstartPositiony;
			ctrlTouchscreentouch0position.x = ctrlTouchscreentouch0positionx;
			ctrlTouchscreentouch0position.y = ctrlTouchscreentouch0positiony;
			ctrlTouchscreentouch0delta.up = ctrlTouchscreentouch0deltaup;
			ctrlTouchscreentouch0delta.down = ctrlTouchscreentouch0deltadown;
			ctrlTouchscreentouch0delta.left = ctrlTouchscreentouch0deltaleft;
			ctrlTouchscreentouch0delta.right = ctrlTouchscreentouch0deltaright;
			ctrlTouchscreentouch0delta.x = ctrlTouchscreentouch0deltax;
			ctrlTouchscreentouch0delta.y = ctrlTouchscreentouch0deltay;
			ctrlTouchscreentouch0radius.x = ctrlTouchscreentouch0radiusx;
			ctrlTouchscreentouch0radius.y = ctrlTouchscreentouch0radiusy;
			ctrlTouchscreentouch0startPosition.x = ctrlTouchscreentouch0startPositionx;
			ctrlTouchscreentouch0startPosition.y = ctrlTouchscreentouch0startPositiony;
			ctrlTouchscreentouch1position.x = ctrlTouchscreentouch1positionx;
			ctrlTouchscreentouch1position.y = ctrlTouchscreentouch1positiony;
			ctrlTouchscreentouch1delta.up = ctrlTouchscreentouch1deltaup;
			ctrlTouchscreentouch1delta.down = ctrlTouchscreentouch1deltadown;
			ctrlTouchscreentouch1delta.left = ctrlTouchscreentouch1deltaleft;
			ctrlTouchscreentouch1delta.right = ctrlTouchscreentouch1deltaright;
			ctrlTouchscreentouch1delta.x = ctrlTouchscreentouch1deltax;
			ctrlTouchscreentouch1delta.y = ctrlTouchscreentouch1deltay;
			ctrlTouchscreentouch1radius.x = ctrlTouchscreentouch1radiusx;
			ctrlTouchscreentouch1radius.y = ctrlTouchscreentouch1radiusy;
			ctrlTouchscreentouch1startPosition.x = ctrlTouchscreentouch1startPositionx;
			ctrlTouchscreentouch1startPosition.y = ctrlTouchscreentouch1startPositiony;
			ctrlTouchscreentouch2position.x = ctrlTouchscreentouch2positionx;
			ctrlTouchscreentouch2position.y = ctrlTouchscreentouch2positiony;
			ctrlTouchscreentouch2delta.up = ctrlTouchscreentouch2deltaup;
			ctrlTouchscreentouch2delta.down = ctrlTouchscreentouch2deltadown;
			ctrlTouchscreentouch2delta.left = ctrlTouchscreentouch2deltaleft;
			ctrlTouchscreentouch2delta.right = ctrlTouchscreentouch2deltaright;
			ctrlTouchscreentouch2delta.x = ctrlTouchscreentouch2deltax;
			ctrlTouchscreentouch2delta.y = ctrlTouchscreentouch2deltay;
			ctrlTouchscreentouch2radius.x = ctrlTouchscreentouch2radiusx;
			ctrlTouchscreentouch2radius.y = ctrlTouchscreentouch2radiusy;
			ctrlTouchscreentouch2startPosition.x = ctrlTouchscreentouch2startPositionx;
			ctrlTouchscreentouch2startPosition.y = ctrlTouchscreentouch2startPositiony;
			ctrlTouchscreentouch3position.x = ctrlTouchscreentouch3positionx;
			ctrlTouchscreentouch3position.y = ctrlTouchscreentouch3positiony;
			ctrlTouchscreentouch3delta.up = ctrlTouchscreentouch3deltaup;
			ctrlTouchscreentouch3delta.down = ctrlTouchscreentouch3deltadown;
			ctrlTouchscreentouch3delta.left = ctrlTouchscreentouch3deltaleft;
			ctrlTouchscreentouch3delta.right = ctrlTouchscreentouch3deltaright;
			ctrlTouchscreentouch3delta.x = ctrlTouchscreentouch3deltax;
			ctrlTouchscreentouch3delta.y = ctrlTouchscreentouch3deltay;
			ctrlTouchscreentouch3radius.x = ctrlTouchscreentouch3radiusx;
			ctrlTouchscreentouch3radius.y = ctrlTouchscreentouch3radiusy;
			ctrlTouchscreentouch3startPosition.x = ctrlTouchscreentouch3startPositionx;
			ctrlTouchscreentouch3startPosition.y = ctrlTouchscreentouch3startPositiony;
			ctrlTouchscreentouch4position.x = ctrlTouchscreentouch4positionx;
			ctrlTouchscreentouch4position.y = ctrlTouchscreentouch4positiony;
			ctrlTouchscreentouch4delta.up = ctrlTouchscreentouch4deltaup;
			ctrlTouchscreentouch4delta.down = ctrlTouchscreentouch4deltadown;
			ctrlTouchscreentouch4delta.left = ctrlTouchscreentouch4deltaleft;
			ctrlTouchscreentouch4delta.right = ctrlTouchscreentouch4deltaright;
			ctrlTouchscreentouch4delta.x = ctrlTouchscreentouch4deltax;
			ctrlTouchscreentouch4delta.y = ctrlTouchscreentouch4deltay;
			ctrlTouchscreentouch4radius.x = ctrlTouchscreentouch4radiusx;
			ctrlTouchscreentouch4radius.y = ctrlTouchscreentouch4radiusy;
			ctrlTouchscreentouch4startPosition.x = ctrlTouchscreentouch4startPositionx;
			ctrlTouchscreentouch4startPosition.y = ctrlTouchscreentouch4startPositiony;
			ctrlTouchscreentouch5position.x = ctrlTouchscreentouch5positionx;
			ctrlTouchscreentouch5position.y = ctrlTouchscreentouch5positiony;
			ctrlTouchscreentouch5delta.up = ctrlTouchscreentouch5deltaup;
			ctrlTouchscreentouch5delta.down = ctrlTouchscreentouch5deltadown;
			ctrlTouchscreentouch5delta.left = ctrlTouchscreentouch5deltaleft;
			ctrlTouchscreentouch5delta.right = ctrlTouchscreentouch5deltaright;
			ctrlTouchscreentouch5delta.x = ctrlTouchscreentouch5deltax;
			ctrlTouchscreentouch5delta.y = ctrlTouchscreentouch5deltay;
			ctrlTouchscreentouch5radius.x = ctrlTouchscreentouch5radiusx;
			ctrlTouchscreentouch5radius.y = ctrlTouchscreentouch5radiusy;
			ctrlTouchscreentouch5startPosition.x = ctrlTouchscreentouch5startPositionx;
			ctrlTouchscreentouch5startPosition.y = ctrlTouchscreentouch5startPositiony;
			ctrlTouchscreentouch6position.x = ctrlTouchscreentouch6positionx;
			ctrlTouchscreentouch6position.y = ctrlTouchscreentouch6positiony;
			ctrlTouchscreentouch6delta.up = ctrlTouchscreentouch6deltaup;
			ctrlTouchscreentouch6delta.down = ctrlTouchscreentouch6deltadown;
			ctrlTouchscreentouch6delta.left = ctrlTouchscreentouch6deltaleft;
			ctrlTouchscreentouch6delta.right = ctrlTouchscreentouch6deltaright;
			ctrlTouchscreentouch6delta.x = ctrlTouchscreentouch6deltax;
			ctrlTouchscreentouch6delta.y = ctrlTouchscreentouch6deltay;
			ctrlTouchscreentouch6radius.x = ctrlTouchscreentouch6radiusx;
			ctrlTouchscreentouch6radius.y = ctrlTouchscreentouch6radiusy;
			ctrlTouchscreentouch6startPosition.x = ctrlTouchscreentouch6startPositionx;
			ctrlTouchscreentouch6startPosition.y = ctrlTouchscreentouch6startPositiony;
			ctrlTouchscreentouch7position.x = ctrlTouchscreentouch7positionx;
			ctrlTouchscreentouch7position.y = ctrlTouchscreentouch7positiony;
			ctrlTouchscreentouch7delta.up = ctrlTouchscreentouch7deltaup;
			ctrlTouchscreentouch7delta.down = ctrlTouchscreentouch7deltadown;
			ctrlTouchscreentouch7delta.left = ctrlTouchscreentouch7deltaleft;
			ctrlTouchscreentouch7delta.right = ctrlTouchscreentouch7deltaright;
			ctrlTouchscreentouch7delta.x = ctrlTouchscreentouch7deltax;
			ctrlTouchscreentouch7delta.y = ctrlTouchscreentouch7deltay;
			ctrlTouchscreentouch7radius.x = ctrlTouchscreentouch7radiusx;
			ctrlTouchscreentouch7radius.y = ctrlTouchscreentouch7radiusy;
			ctrlTouchscreentouch7startPosition.x = ctrlTouchscreentouch7startPositionx;
			ctrlTouchscreentouch7startPosition.y = ctrlTouchscreentouch7startPositiony;
			ctrlTouchscreentouch8position.x = ctrlTouchscreentouch8positionx;
			ctrlTouchscreentouch8position.y = ctrlTouchscreentouch8positiony;
			ctrlTouchscreentouch8delta.up = ctrlTouchscreentouch8deltaup;
			ctrlTouchscreentouch8delta.down = ctrlTouchscreentouch8deltadown;
			ctrlTouchscreentouch8delta.left = ctrlTouchscreentouch8deltaleft;
			ctrlTouchscreentouch8delta.right = ctrlTouchscreentouch8deltaright;
			ctrlTouchscreentouch8delta.x = ctrlTouchscreentouch8deltax;
			ctrlTouchscreentouch8delta.y = ctrlTouchscreentouch8deltay;
			ctrlTouchscreentouch8radius.x = ctrlTouchscreentouch8radiusx;
			ctrlTouchscreentouch8radius.y = ctrlTouchscreentouch8radiusy;
			ctrlTouchscreentouch8startPosition.x = ctrlTouchscreentouch8startPositionx;
			ctrlTouchscreentouch8startPosition.y = ctrlTouchscreentouch8startPositiony;
			ctrlTouchscreentouch9position.x = ctrlTouchscreentouch9positionx;
			ctrlTouchscreentouch9position.y = ctrlTouchscreentouch9positiony;
			ctrlTouchscreentouch9delta.up = ctrlTouchscreentouch9deltaup;
			ctrlTouchscreentouch9delta.down = ctrlTouchscreentouch9deltadown;
			ctrlTouchscreentouch9delta.left = ctrlTouchscreentouch9deltaleft;
			ctrlTouchscreentouch9delta.right = ctrlTouchscreentouch9deltaright;
			ctrlTouchscreentouch9delta.x = ctrlTouchscreentouch9deltax;
			ctrlTouchscreentouch9delta.y = ctrlTouchscreentouch9deltay;
			ctrlTouchscreentouch9radius.x = ctrlTouchscreentouch9radiusx;
			ctrlTouchscreentouch9radius.y = ctrlTouchscreentouch9radiusy;
			ctrlTouchscreentouch9startPosition.x = ctrlTouchscreentouch9startPositionx;
			ctrlTouchscreentouch9startPosition.y = ctrlTouchscreentouch9startPositiony;
			builder.WithStateOffsetToControlIndexMap(new uint[]
			{
				32785U, 16810014U, 16810026U, 33587231U, 33587243U, 50364450U, 50364451U, 50364452U, 50364462U, 50364463U,
				50364464U, 67141664U, 67141665U, 67141669U, 67141676U, 67141677U, 67141681U, 83918851U, 83918868U, 100696102U,
				100696114U, 117473319U, 117473331U, 134225925U, 134225942U, 134225943U, 138420248U, 142614534U, 142614553U, 146801690U,
				148898843U, 167837724U, 201359400U, 218136617U, 234913844U, 251691073U, 268468290U, 285245509U, 285245510U, 285245511U,
				302022723U, 302022724U, 302022728U, 318799927U, 335577161U, 352354378U, 369107001U, 369107002U, 373301307U, 377495612U,
				381682749U, 383779902U, 402718783U, 436240459U, 453017676U, 469794893U, 486572122U, 503349339U, 520126558U, 520126559U,
				520126560U, 536903772U, 536903773U, 536903777U, 553680976U, 570458210U, 587235427U, 603988050U, 603988051U, 608182356U,
				612376661U, 616563798U, 618660951U, 637599832U, 671121508U, 687898725U, 704675942U, 721453171U, 738230388U, 755007607U,
				755007608U, 755007609U, 771784821U, 771784822U, 771784826U, 788562025U, 805339259U, 822116476U, 838869099U, 838869100U,
				843063405U, 847257710U, 851444847U, 853542000U, 872480881U, 906002557U, 922779774U, 939556991U, 956334220U, 973111437U,
				989888656U, 989888657U, 989888658U, 1006665870U, 1006665871U, 1006665875U, 1023443074U, 1040220308U, 1056997525U, 1073750148U,
				1073750149U, 1077944454U, 1082138759U, 1086325896U, 1088423049U, 1107361930U, 1140883606U, 1157660823U, 1174438040U, 1191215269U,
				1207992486U, 1224769705U, 1224769706U, 1224769707U, 1241546919U, 1241546920U, 1241546924U, 1258324123U, 1275101357U, 1291878574U,
				1308631197U, 1308631198U, 1312825503U, 1317019808U, 1321206945U, 1323304098U, 1342242979U, 1375764655U, 1392541872U, 1409319089U,
				1426096318U, 1442873535U, 1459650754U, 1459650755U, 1459650756U, 1476427968U, 1476427969U, 1476427973U, 1493205172U, 1509982406U,
				1526759623U, 1543512246U, 1543512247U, 1547706552U, 1551900857U, 1556087994U, 1558185147U, 1577124028U, 1610645704U, 1627422921U,
				1644200138U, 1660977367U, 1677754584U, 1694531803U, 1694531804U, 1694531805U, 1711309017U, 1711309018U, 1711309022U, 1728086221U,
				1744863455U, 1761640672U, 1778393295U, 1778393296U, 1782587601U, 1786781906U, 1790969043U, 1793066196U, 1812005077U, 1845526753U,
				1862303970U, 1879081187U, 1895858416U, 1912635633U, 1929412852U, 1929412853U, 1929412854U, 1946190066U, 1946190067U, 1946190071U,
				1962967270U, 1979744504U, 1996521721U, 2013274344U, 2013274345U, 2017468650U, 2021662955U, 2025850092U, 2027947245U, 2046886126U,
				2080407802U, 2097185019U, 2113962236U, 2130739465U, 2147516682U, 2164293901U, 2164293902U, 2164293903U, 2181071115U, 2181071116U,
				2181071120U, 2197848319U, 2214625553U, 2231402770U, 2248155393U, 2248155394U, 2252349699U, 2256544004U, 2260731141U, 2262828294U,
				2281767175U, 2315288851U, 2332066068U, 2348843285U, 2365620514U, 2382397731U, 2399174950U, 2399174951U, 2399174952U, 2415952164U,
				2415952165U, 2415952169U, 2432729368U, 2449506602U, 2466283819U, 2483036442U, 2483036443U, 2487230748U, 2491425053U, 2495612190U,
				2497709343U, 2516648224U, 2550169900U, 2566947117U
			});
			builder.WithControlTree(new byte[]
			{
				63, 19, 1, 0, 0, 0, 0, 192, 8, 3,
				0, 0, 0, 0, 63, 19, 1, 1, 0, 0,
				0, 128, 3, 5, 0, 0, 0, 0, 192, 8,
				103, 0, 0, 0, 0, 192, 1, 7, 0, 0,
				0, 1, 128, 3, 53, 0, 68, 0, 1, 192,
				0, 9, 0, 0, 0, 0, 192, 1, 21, 0,
				0, 0, 0, 96, 0, 11, 0, 0, 0, 0,
				192, 0, 15, 0, 0, 0, 0, 32, 0, byte.MaxValue,
				byte.MaxValue, 1, 0, 1, 96, 0, 13, 0, 2, 0,
				2, 64, 0, byte.MaxValue, byte.MaxValue, 4, 0, 2, 96, 0,
				byte.MaxValue, byte.MaxValue, 6, 0, 2, 144, 0, 17, 0, 8,
				0, 8, 192, 0, 19, 0, 16, 0, 8, 120,
				0, byte.MaxValue, byte.MaxValue, 24, 0, 6, 144, 0, byte.MaxValue, byte.MaxValue,
				30, 0, 6, 168, 0, byte.MaxValue, byte.MaxValue, 36, 0, 2,
				192, 0, byte.MaxValue, byte.MaxValue, 38, 0, 2, 29, 1, 23,
				0, 0, 0, 0, 192, 1, 47, 0, 0, 0,
				0, 8, 1, 25, 0, 0, 0, 0, 29, 1,
				35, 0, 0, 0, 0, 228, 0, 27, 0, 40,
				0, 4, 8, 1, 29, 0, 44, 0, 4, 210,
				0, byte.MaxValue, byte.MaxValue, 48, 0, 2, 228, 0, byte.MaxValue, byte.MaxValue,
				50, 0, 2, 246, 0, byte.MaxValue, byte.MaxValue, 0, 0, 0,
				8, 1, 31, 0, 0, 0, 0, byte.MaxValue, 0, byte.MaxValue,
				byte.MaxValue, 0, 0, 0, 8, 1, 33, 0, 0, 0,
				0, 4, 1, byte.MaxValue, byte.MaxValue, 52, 0, 3, 8, 1,
				byte.MaxValue, byte.MaxValue, 55, 0, 3, 24, 1, 37, 0, 0,
				0, 0, 29, 1, 39, 0, 0, 0, 0, 16,
				1, byte.MaxValue, byte.MaxValue, 58, 0, 1, 24, 1, byte.MaxValue, byte.MaxValue,
				59, 0, 2, 27, 1, 41, 0, 0, 0, 0,
				29, 1, 45, 0, 0, 0, 0, 26, 1, 43,
				0, 0, 0, 0, 27, 1, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 25, 1, byte.MaxValue, byte.MaxValue, 61, 0, 1, 26, 1,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 28, 1, byte.MaxValue, byte.MaxValue, 0,
				0, 0, 29, 1, byte.MaxValue, byte.MaxValue, 62, 0, 1, 128,
				1, 49, 0, 0, 0, 0, 192, 1, 51, 0,
				65, 0, 1, 79, 1, byte.MaxValue, byte.MaxValue, 63, 0, 1,
				128, 1, byte.MaxValue, byte.MaxValue, 64, 0, 1, 160, 1, byte.MaxValue,
				byte.MaxValue, 66, 0, 1, 192, 1, byte.MaxValue, byte.MaxValue, 67, 0,
				1, 160, 2, 55, 0, 92, 0, 1, 128, 3,
				71, 0, 93, 0, 1, 48, 2, 57, 0, 77,
				0, 4, 160, 2, 65, 0, 81, 0, 4, 248,
				1, 59, 0, 71, 0, 2, 48, 2, 61, 0,
				73, 0, 2, 220, 1, byte.MaxValue, byte.MaxValue, 69, 0, 1,
				248, 1, byte.MaxValue, byte.MaxValue, 70, 0, 1, 32, 2, 63,
				0, 0, 0, 0, 48, 2, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 12, 2, byte.MaxValue, byte.MaxValue, 75, 0, 1, 32, 2,
				byte.MaxValue, byte.MaxValue, 76, 0, 1, 96, 2, 67, 0, 0,
				0, 0, 160, 2, 69, 0, 0, 0, 0, 72,
				2, byte.MaxValue, byte.MaxValue, 85, 0, 3, 96, 2, byte.MaxValue, byte.MaxValue,
				88, 0, 3, 128, 2, byte.MaxValue, byte.MaxValue, 91, 0, 1,
				160, 2, byte.MaxValue, byte.MaxValue, 94, 0, 1, 16, 3, 73,
				0, 105, 0, 1, 128, 3, 99, 0, 106, 0,
				1, 216, 2, 75, 0, 0, 0, 0, 16, 3,
				83, 0, 0, 0, 0, 188, 2, byte.MaxValue, byte.MaxValue, 95,
				0, 1, 216, 2, 77, 0, 96, 0, 1, 200,
				2, 79, 0, 0, 0, 0, 216, 2, 81, 0,
				0, 0, 0, 194, 2, byte.MaxValue, byte.MaxValue, 97, 0, 2,
				200, 2, byte.MaxValue, byte.MaxValue, 99, 0, 2, 208, 2, byte.MaxValue,
				byte.MaxValue, 101, 0, 1, 216, 2, byte.MaxValue, byte.MaxValue, 102, 0,
				1, 244, 2, 85, 0, 0, 0, 0, 16, 3,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 230, 2, 87, 0, 0,
				0, 0, 244, 2, byte.MaxValue, byte.MaxValue, 0, 0, 0, 223,
				2, 89, 0, 0, 0, 0, 230, 2, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 220, 2, 91, 0, 0, 0, 0,
				223, 2, 95, 0, 0, 0, 0, 218, 2, 93,
				0, 0, 0, 0, 220, 2, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 217, 2, byte.MaxValue, byte.MaxValue, 103, 0, 1, 218, 2,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 222, 2, 97, 0, 0,
				0, 0, 223, 2, byte.MaxValue, byte.MaxValue, 0, 0, 0, 221,
				2, byte.MaxValue, byte.MaxValue, 104, 0, 1, 222, 2, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 72, 3, byte.MaxValue, byte.MaxValue, 107, 0, 2,
				128, 3, 101, 0, 109, 0, 2, 100, 3, byte.MaxValue,
				byte.MaxValue, 111, 0, 1, 128, 3, byte.MaxValue, byte.MaxValue, 112, 0,
				1, 0, 7, 105, 0, 0, 0, 0, 192, 8,
				207, 0, 203, 0, 1, 64, 5, 107, 0, 113,
				0, 1, 0, 7, 157, 0, 158, 0, 1, 96,
				4, 109, 0, 137, 0, 1, 64, 5, 125, 0,
				138, 0, 1, 240, 3, 111, 0, 122, 0, 4,
				96, 4, 119, 0, 126, 0, 4, 184, 3, 113,
				0, 116, 0, 2, 240, 3, 115, 0, 118, 0,
				2, 156, 3, byte.MaxValue, byte.MaxValue, 114, 0, 1, 184, 3,
				byte.MaxValue, byte.MaxValue, 115, 0, 1, 224, 3, 117, 0, 0,
				0, 0, 240, 3, byte.MaxValue, byte.MaxValue, 0, 0, 0, 204,
				3, byte.MaxValue, byte.MaxValue, 120, 0, 1, 224, 3, byte.MaxValue, byte.MaxValue,
				121, 0, 1, 32, 4, 121, 0, 0, 0, 0,
				96, 4, 123, 0, 0, 0, 0, 8, 4, byte.MaxValue,
				byte.MaxValue, 130, 0, 3, 32, 4, byte.MaxValue, byte.MaxValue, 133, 0,
				3, 64, 4, byte.MaxValue, byte.MaxValue, 136, 0, 1, 96, 4,
				byte.MaxValue, byte.MaxValue, 139, 0, 1, 208, 4, 127, 0, 150,
				0, 1, 64, 5, 153, 0, 151, 0, 1, 152,
				4, 129, 0, 0, 0, 0, 208, 4, 137, 0,
				0, 0, 0, 124, 4, byte.MaxValue, byte.MaxValue, 140, 0, 1,
				152, 4, 131, 0, 141, 0, 1, 136, 4, 133,
				0, 0, 0, 0, 152, 4, 135, 0, 0, 0,
				0, 130, 4, byte.MaxValue, byte.MaxValue, 142, 0, 2, 136, 4,
				byte.MaxValue, byte.MaxValue, 144, 0, 2, 144, 4, byte.MaxValue, byte.MaxValue, 146,
				0, 1, 152, 4, byte.MaxValue, byte.MaxValue, 147, 0, 1, 180,
				4, 139, 0, 0, 0, 0, 208, 4, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 166, 4, 141, 0, 0, 0, 0,
				180, 4, byte.MaxValue, byte.MaxValue, 0, 0, 0, 159, 4, 143,
				0, 0, 0, 0, 166, 4, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 156, 4, 145, 0, 0, 0, 0, 159, 4,
				149, 0, 0, 0, 0, 154, 4, 147, 0, 0,
				0, 0, 156, 4, byte.MaxValue, byte.MaxValue, 0, 0, 0, 153,
				4, byte.MaxValue, byte.MaxValue, 148, 0, 1, 154, 4, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 158, 4, 151, 0, 0, 0, 0,
				159, 4, byte.MaxValue, byte.MaxValue, 0, 0, 0, 157, 4, byte.MaxValue,
				byte.MaxValue, 149, 0, 1, 158, 4, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 8, 5, byte.MaxValue, byte.MaxValue, 152, 0, 2, 64, 5,
				155, 0, 154, 0, 2, 36, 5, byte.MaxValue, byte.MaxValue, 156,
				0, 1, 64, 5, byte.MaxValue, byte.MaxValue, 157, 0, 1, 32,
				6, 159, 0, 182, 0, 1, 0, 7, 175, 0,
				183, 0, 1, 176, 5, 161, 0, 167, 0, 4,
				32, 6, 169, 0, 171, 0, 4, 120, 5, 163,
				0, 161, 0, 2, 176, 5, 165, 0, 163, 0,
				2, 92, 5, byte.MaxValue, byte.MaxValue, 159, 0, 1, 120, 5,
				byte.MaxValue, byte.MaxValue, 160, 0, 1, 160, 5, 167, 0, 0,
				0, 0, 176, 5, byte.MaxValue, byte.MaxValue, 0, 0, 0, 140,
				5, byte.MaxValue, byte.MaxValue, 165, 0, 1, 160, 5, byte.MaxValue, byte.MaxValue,
				166, 0, 1, 224, 5, 171, 0, 0, 0, 0,
				32, 6, 173, 0, 0, 0, 0, 200, 5, byte.MaxValue,
				byte.MaxValue, 175, 0, 3, 224, 5, byte.MaxValue, byte.MaxValue, 178, 0,
				3, 0, 6, byte.MaxValue, byte.MaxValue, 181, 0, 1, 32, 6,
				byte.MaxValue, byte.MaxValue, 184, 0, 1, 144, 6, 177, 0, 195,
				0, 1, 0, 7, 203, 0, 196, 0, 1, 88,
				6, 179, 0, 0, 0, 0, 144, 6, 187, 0,
				0, 0, 0, 60, 6, byte.MaxValue, byte.MaxValue, 185, 0, 1,
				88, 6, 181, 0, 186, 0, 1, 72, 6, 183,
				0, 0, 0, 0, 88, 6, 185, 0, 0, 0,
				0, 66, 6, byte.MaxValue, byte.MaxValue, 187, 0, 2, 72, 6,
				byte.MaxValue, byte.MaxValue, 189, 0, 2, 80, 6, byte.MaxValue, byte.MaxValue, 191,
				0, 1, 88, 6, byte.MaxValue, byte.MaxValue, 192, 0, 1, 116,
				6, 189, 0, 0, 0, 0, 144, 6, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 102, 6, 191, 0, 0, 0, 0,
				116, 6, byte.MaxValue, byte.MaxValue, 0, 0, 0, 95, 6, 193,
				0, 0, 0, 0, 102, 6, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 92, 6, 195, 0, 0, 0, 0, 95, 6,
				199, 0, 0, 0, 0, 90, 6, 197, 0, 0,
				0, 0, 92, 6, byte.MaxValue, byte.MaxValue, 0, 0, 0, 89,
				6, byte.MaxValue, byte.MaxValue, 193, 0, 1, 90, 6, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 94, 6, 201, 0, 0, 0, 0,
				95, 6, byte.MaxValue, byte.MaxValue, 0, 0, 0, 93, 6, byte.MaxValue,
				byte.MaxValue, 194, 0, 1, 94, 6, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 200, 6, byte.MaxValue, byte.MaxValue, 197, 0, 2, 0, 7,
				205, 0, 199, 0, 2, 228, 6, byte.MaxValue, byte.MaxValue, 201,
				0, 1, 0, 7, byte.MaxValue, byte.MaxValue, 202, 0, 1, 224,
				7, 209, 0, 227, 0, 1, 192, 8, 225, 0,
				228, 0, 1, 112, 7, 211, 0, 212, 0, 4,
				224, 7, 219, 0, 216, 0, 4, 56, 7, 213,
				0, 206, 0, 2, 112, 7, 215, 0, 208, 0,
				2, 28, 7, byte.MaxValue, byte.MaxValue, 204, 0, 1, 56, 7,
				byte.MaxValue, byte.MaxValue, 205, 0, 1, 96, 7, 217, 0, 0,
				0, 0, 112, 7, byte.MaxValue, byte.MaxValue, 0, 0, 0, 76,
				7, byte.MaxValue, byte.MaxValue, 210, 0, 1, 96, 7, byte.MaxValue, byte.MaxValue,
				211, 0, 1, 160, 7, 221, 0, 0, 0, 0,
				224, 7, 223, 0, 0, 0, 0, 136, 7, byte.MaxValue,
				byte.MaxValue, 220, 0, 3, 160, 7, byte.MaxValue, byte.MaxValue, 223, 0,
				3, 192, 7, byte.MaxValue, byte.MaxValue, 226, 0, 1, 224, 7,
				byte.MaxValue, byte.MaxValue, 229, 0, 1, 80, 8, 227, 0, 240,
				0, 1, 192, 8, 253, 0, 241, 0, 1, 24,
				8, 229, 0, 0, 0, 0, 80, 8, 237, 0,
				0, 0, 0, 252, 7, byte.MaxValue, byte.MaxValue, 230, 0, 1,
				24, 8, 231, 0, 231, 0, 1, 8, 8, 233,
				0, 0, 0, 0, 24, 8, 235, 0, 0, 0,
				0, 2, 8, byte.MaxValue, byte.MaxValue, 232, 0, 2, 8, 8,
				byte.MaxValue, byte.MaxValue, 234, 0, 2, 16, 8, byte.MaxValue, byte.MaxValue, 236,
				0, 1, 24, 8, byte.MaxValue, byte.MaxValue, 237, 0, 1, 52,
				8, 239, 0, 0, 0, 0, 80, 8, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 38, 8, 241, 0, 0, 0, 0,
				52, 8, byte.MaxValue, byte.MaxValue, 0, 0, 0, 31, 8, 243,
				0, 0, 0, 0, 38, 8, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 28, 8, 245, 0, 0, 0, 0, 31, 8,
				249, 0, 0, 0, 0, 26, 8, 247, 0, 0,
				0, 0, 28, 8, byte.MaxValue, byte.MaxValue, 0, 0, 0, 25,
				8, byte.MaxValue, byte.MaxValue, 238, 0, 1, 26, 8, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 30, 8, 251, 0, 0, 0, 0,
				31, 8, byte.MaxValue, byte.MaxValue, 0, 0, 0, 29, 8, byte.MaxValue,
				byte.MaxValue, 239, 0, 1, 30, 8, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 136, 8, byte.MaxValue, byte.MaxValue, 242, 0, 2, 192, 8,
				byte.MaxValue, 0, 244, 0, 2, 164, 8, byte.MaxValue, byte.MaxValue, 246,
				0, 1, 192, 8, byte.MaxValue, byte.MaxValue, 247, 0, 1, 0,
				14, 3, 1, 0, 0, 0, 63, 19, 157, 1,
				0, 0, 0, 64, 12, 5, 1, 0, 0, 0,
				0, 14, 107, 1, 82, 1, 1, 128, 10, 7,
				1, 248, 0, 1, 64, 12, 57, 1, 37, 1,
				1, 160, 9, 9, 1, 16, 1, 1, 128, 10,
				25, 1, 17, 1, 1, 48, 9, 11, 1, 1,
				1, 4, 160, 9, 19, 1, 5, 1, 4, 248,
				8, 13, 1, 251, 0, 2, 48, 9, 15, 1,
				253, 0, 2, 220, 8, byte.MaxValue, byte.MaxValue, 249, 0, 1,
				248, 8, byte.MaxValue, byte.MaxValue, 250, 0, 1, 32, 9, 17,
				1, 0, 0, 0, 48, 9, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 12, 9, byte.MaxValue, byte.MaxValue, byte.MaxValue, 0, 1, 32, 9,
				byte.MaxValue, byte.MaxValue, 0, 1, 1, 96, 9, 21, 1, 0,
				0, 0, 160, 9, 23, 1, 0, 0, 0, 72,
				9, byte.MaxValue, byte.MaxValue, 9, 1, 3, 96, 9, byte.MaxValue, byte.MaxValue,
				12, 1, 3, 128, 9, byte.MaxValue, byte.MaxValue, 15, 1, 1,
				160, 9, byte.MaxValue, byte.MaxValue, 18, 1, 1, 16, 10, 27,
				1, 29, 1, 1, 128, 10, 53, 1, 30, 1,
				1, 216, 9, 29, 1, 0, 0, 0, 16, 10,
				37, 1, 0, 0, 0, 188, 9, byte.MaxValue, byte.MaxValue, 19,
				1, 1, 216, 9, 31, 1, 20, 1, 1, 200,
				9, 33, 1, 0, 0, 0, 216, 9, 35, 1,
				0, 0, 0, 194, 9, byte.MaxValue, byte.MaxValue, 21, 1, 2,
				200, 9, byte.MaxValue, byte.MaxValue, 23, 1, 2, 208, 9, byte.MaxValue,
				byte.MaxValue, 25, 1, 1, 216, 9, byte.MaxValue, byte.MaxValue, 26, 1,
				1, 244, 9, 39, 1, 0, 0, 0, 16, 10,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 230, 9, 41, 1, 0,
				0, 0, 244, 9, byte.MaxValue, byte.MaxValue, 0, 0, 0, 223,
				9, 43, 1, 0, 0, 0, 230, 9, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 220, 9, 45, 1, 0, 0, 0,
				223, 9, 49, 1, 0, 0, 0, 218, 9, 47,
				1, 0, 0, 0, 220, 9, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 217, 9, byte.MaxValue, byte.MaxValue, 27, 1, 1, 218, 9,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 222, 9, 51, 1, 0,
				0, 0, 223, 9, byte.MaxValue, byte.MaxValue, 0, 0, 0, 221,
				9, byte.MaxValue, byte.MaxValue, 28, 1, 1, 222, 9, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 72, 10, byte.MaxValue, byte.MaxValue, 31, 1, 2,
				128, 10, 55, 1, 33, 1, 2, 100, 10, byte.MaxValue,
				byte.MaxValue, 35, 1, 1, 128, 10, byte.MaxValue, byte.MaxValue, 36, 1,
				1, 96, 11, 59, 1, 61, 1, 1, 64, 12,
				75, 1, 62, 1, 1, 240, 10, 61, 1, 46,
				1, 4, 96, 11, 69, 1, 50, 1, 4, 184,
				10, 63, 1, 40, 1, 2, 240, 10, 65, 1,
				42, 1, 2, 156, 10, byte.MaxValue, byte.MaxValue, 38, 1, 1,
				184, 10, byte.MaxValue, byte.MaxValue, 39, 1, 1, 224, 10, 67,
				1, 0, 0, 0, 240, 10, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 204, 10, byte.MaxValue, byte.MaxValue, 44, 1, 1, 224, 10,
				byte.MaxValue, byte.MaxValue, 45, 1, 1, 32, 11, 71, 1, 0,
				0, 0, 96, 11, 73, 1, 0, 0, 0, 8,
				11, byte.MaxValue, byte.MaxValue, 54, 1, 3, 32, 11, byte.MaxValue, byte.MaxValue,
				57, 1, 3, 64, 11, byte.MaxValue, byte.MaxValue, 60, 1, 1,
				96, 11, byte.MaxValue, byte.MaxValue, 63, 1, 1, 208, 11, 77,
				1, 74, 1, 1, 64, 12, 103, 1, 75, 1,
				1, 152, 11, 79, 1, 0, 0, 0, 208, 11,
				87, 1, 0, 0, 0, 124, 11, byte.MaxValue, byte.MaxValue, 64,
				1, 1, 152, 11, 81, 1, 65, 1, 1, 136,
				11, 83, 1, 0, 0, 0, 152, 11, 85, 1,
				0, 0, 0, 130, 11, byte.MaxValue, byte.MaxValue, 66, 1, 2,
				136, 11, byte.MaxValue, byte.MaxValue, 68, 1, 2, 144, 11, byte.MaxValue,
				byte.MaxValue, 70, 1, 1, 152, 11, byte.MaxValue, byte.MaxValue, 71, 1,
				1, 180, 11, 89, 1, 0, 0, 0, 208, 11,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 166, 11, 91, 1, 0,
				0, 0, 180, 11, byte.MaxValue, byte.MaxValue, 0, 0, 0, 159,
				11, 93, 1, 0, 0, 0, 166, 11, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 156, 11, 95, 1, 0, 0, 0,
				159, 11, 99, 1, 0, 0, 0, 154, 11, 97,
				1, 0, 0, 0, 156, 11, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 153, 11, byte.MaxValue, byte.MaxValue, 72, 1, 1, 154, 11,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 158, 11, 101, 1, 0,
				0, 0, 159, 11, byte.MaxValue, byte.MaxValue, 0, 0, 0, 157,
				11, byte.MaxValue, byte.MaxValue, 73, 1, 1, 158, 11, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 8, 12, byte.MaxValue, byte.MaxValue, 76, 1, 2,
				64, 12, 105, 1, 78, 1, 2, 36, 12, byte.MaxValue,
				byte.MaxValue, 80, 1, 1, 64, 12, byte.MaxValue, byte.MaxValue, 81, 1,
				1, 32, 13, 109, 1, 106, 1, 1, 0, 14,
				125, 1, 107, 1, 1, 176, 12, 111, 1, 91,
				1, 4, 32, 13, 119, 1, 95, 1, 4, 120,
				12, 113, 1, 85, 1, 2, 176, 12, 115, 1,
				87, 1, 2, 92, 12, byte.MaxValue, byte.MaxValue, 83, 1, 1,
				120, 12, byte.MaxValue, byte.MaxValue, 84, 1, 1, 160, 12, 117,
				1, 0, 0, 0, 176, 12, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 140, 12, byte.MaxValue, byte.MaxValue, 89, 1, 1, 160, 12,
				byte.MaxValue, byte.MaxValue, 90, 1, 1, 224, 12, 121, 1, 0,
				0, 0, 32, 13, 123, 1, 0, 0, 0, 200,
				12, byte.MaxValue, byte.MaxValue, 99, 1, 3, 224, 12, byte.MaxValue, byte.MaxValue,
				102, 1, 3, 0, 13, byte.MaxValue, byte.MaxValue, 105, 1, 1,
				32, 13, byte.MaxValue, byte.MaxValue, 108, 1, 1, 144, 13, 127,
				1, 119, 1, 1, 0, 14, 153, 1, 120, 1,
				1, 88, 13, 129, 1, 0, 0, 0, 144, 13,
				137, 1, 0, 0, 0, 60, 13, byte.MaxValue, byte.MaxValue, 109,
				1, 1, 88, 13, 131, 1, 110, 1, 1, 72,
				13, 133, 1, 0, 0, 0, 88, 13, 135, 1,
				0, 0, 0, 66, 13, byte.MaxValue, byte.MaxValue, 111, 1, 2,
				72, 13, byte.MaxValue, byte.MaxValue, 113, 1, 2, 80, 13, byte.MaxValue,
				byte.MaxValue, 115, 1, 1, 88, 13, byte.MaxValue, byte.MaxValue, 116, 1,
				1, 116, 13, 139, 1, 0, 0, 0, 144, 13,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 102, 13, 141, 1, 0,
				0, 0, 116, 13, byte.MaxValue, byte.MaxValue, 0, 0, 0, 95,
				13, 143, 1, 0, 0, 0, 102, 13, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 92, 13, 145, 1, 0, 0, 0,
				95, 13, 149, 1, 0, 0, 0, 90, 13, 147,
				1, 0, 0, 0, 92, 13, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 89, 13, byte.MaxValue, byte.MaxValue, 117, 1, 1, 90, 13,
				byte.MaxValue, byte.MaxValue, 0, 0, 0, 94, 13, 151, 1, 0,
				0, 0, 95, 13, byte.MaxValue, byte.MaxValue, 0, 0, 0, 93,
				13, byte.MaxValue, byte.MaxValue, 118, 1, 1, 94, 13, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 200, 13, byte.MaxValue, byte.MaxValue, 121, 1, 2,
				0, 14, 155, 1, 123, 1, 2, 228, 13, byte.MaxValue,
				byte.MaxValue, 125, 1, 1, 0, 14, byte.MaxValue, byte.MaxValue, 126, 1,
				1, 128, 17, 159, 1, 0, 0, 0, 63, 19,
				5, 2, 0, 0, 0, 192, 15, 161, 1, 127,
				1, 1, 128, 17, 211, 1, 172, 1, 1, 224,
				14, 163, 1, 151, 1, 1, 192, 15, 179, 1,
				152, 1, 1, 112, 14, 165, 1, 136, 1, 4,
				224, 14, 173, 1, 140, 1, 4, 56, 14, 167,
				1, 130, 1, 2, 112, 14, 169, 1, 132, 1,
				2, 28, 14, byte.MaxValue, byte.MaxValue, 128, 1, 1, 56, 14,
				byte.MaxValue, byte.MaxValue, 129, 1, 1, 96, 14, 171, 1, 0,
				0, 0, 112, 14, byte.MaxValue, byte.MaxValue, 0, 0, 0, 76,
				14, byte.MaxValue, byte.MaxValue, 134, 1, 1, 96, 14, byte.MaxValue, byte.MaxValue,
				135, 1, 1, 160, 14, 175, 1, 0, 0, 0,
				224, 14, 177, 1, 0, 0, 0, 136, 14, byte.MaxValue,
				byte.MaxValue, 144, 1, 3, 160, 14, byte.MaxValue, byte.MaxValue, 147, 1,
				3, 192, 14, byte.MaxValue, byte.MaxValue, 150, 1, 1, 224, 14,
				byte.MaxValue, byte.MaxValue, 153, 1, 1, 80, 15, 181, 1, 164,
				1, 1, 192, 15, 207, 1, 165, 1, 1, 24,
				15, 183, 1, 0, 0, 0, 80, 15, 191, 1,
				0, 0, 0, 252, 14, byte.MaxValue, byte.MaxValue, 154, 1, 1,
				24, 15, 185, 1, 155, 1, 1, 8, 15, 187,
				1, 0, 0, 0, 24, 15, 189, 1, 0, 0,
				0, 2, 15, byte.MaxValue, byte.MaxValue, 156, 1, 2, 8, 15,
				byte.MaxValue, byte.MaxValue, 158, 1, 2, 16, 15, byte.MaxValue, byte.MaxValue, 160,
				1, 1, 24, 15, byte.MaxValue, byte.MaxValue, 161, 1, 1, 52,
				15, 193, 1, 0, 0, 0, 80, 15, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 38, 15, 195, 1, 0, 0, 0,
				52, 15, byte.MaxValue, byte.MaxValue, 0, 0, 0, 31, 15, 197,
				1, 0, 0, 0, 38, 15, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 28, 15, 199, 1, 0, 0, 0, 31, 15,
				203, 1, 0, 0, 0, 26, 15, 201, 1, 0,
				0, 0, 28, 15, byte.MaxValue, byte.MaxValue, 0, 0, 0, 25,
				15, byte.MaxValue, byte.MaxValue, 162, 1, 1, 26, 15, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 30, 15, 205, 1, 0, 0, 0,
				31, 15, byte.MaxValue, byte.MaxValue, 0, 0, 0, 29, 15, byte.MaxValue,
				byte.MaxValue, 163, 1, 1, 30, 15, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 136, 15, byte.MaxValue, byte.MaxValue, 166, 1, 2, 192, 15,
				209, 1, 168, 1, 2, 164, 15, byte.MaxValue, byte.MaxValue, 170,
				1, 1, 192, 15, byte.MaxValue, byte.MaxValue, 171, 1, 1, 160,
				16, 213, 1, 196, 1, 1, 128, 17, 229, 1,
				197, 1, 1, 48, 16, 215, 1, 181, 1, 4,
				160, 16, 223, 1, 185, 1, 4, 248, 15, 217,
				1, 175, 1, 2, 48, 16, 219, 1, 177, 1,
				2, 220, 15, byte.MaxValue, byte.MaxValue, 173, 1, 1, 248, 15,
				byte.MaxValue, byte.MaxValue, 174, 1, 1, 32, 16, 221, 1, 0,
				0, 0, 48, 16, byte.MaxValue, byte.MaxValue, 0, 0, 0, 12,
				16, byte.MaxValue, byte.MaxValue, 179, 1, 1, 32, 16, byte.MaxValue, byte.MaxValue,
				180, 1, 1, 96, 16, 225, 1, 0, 0, 0,
				160, 16, 227, 1, 0, 0, 0, 72, 16, byte.MaxValue,
				byte.MaxValue, 189, 1, 3, 96, 16, byte.MaxValue, byte.MaxValue, 192, 1,
				3, 128, 16, byte.MaxValue, byte.MaxValue, 195, 1, 1, 160, 16,
				byte.MaxValue, byte.MaxValue, 198, 1, 1, 16, 17, 231, 1, 209,
				1, 1, 128, 17, 1, 2, 210, 1, 1, 216,
				16, 233, 1, 0, 0, 0, 16, 17, 241, 1,
				0, 0, 0, 188, 16, byte.MaxValue, byte.MaxValue, 199, 1, 1,
				216, 16, 235, 1, 200, 1, 1, 200, 16, 237,
				1, 0, 0, 0, 216, 16, 239, 1, 0, 0,
				0, 194, 16, byte.MaxValue, byte.MaxValue, 201, 1, 2, 200, 16,
				byte.MaxValue, byte.MaxValue, 203, 1, 2, 208, 16, byte.MaxValue, byte.MaxValue, 205,
				1, 1, 216, 16, byte.MaxValue, byte.MaxValue, 206, 1, 1, 244,
				16, 243, 1, 0, 0, 0, 16, 17, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 230, 16, 245, 1, 0, 0, 0,
				244, 16, byte.MaxValue, byte.MaxValue, 0, 0, 0, 223, 16, 247,
				1, 0, 0, 0, 230, 16, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 220, 16, 249, 1, 0, 0, 0, 223, 16,
				253, 1, 0, 0, 0, 218, 16, 251, 1, 0,
				0, 0, 220, 16, byte.MaxValue, byte.MaxValue, 0, 0, 0, 217,
				16, byte.MaxValue, byte.MaxValue, 207, 1, 1, 218, 16, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 222, 16, byte.MaxValue, 1, 0, 0, 0,
				223, 16, byte.MaxValue, byte.MaxValue, 0, 0, 0, 221, 16, byte.MaxValue,
				byte.MaxValue, 208, 1, 1, 222, 16, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 72, 17, byte.MaxValue, byte.MaxValue, 211, 1, 2, 128, 17,
				3, 2, 213, 1, 2, 100, 17, byte.MaxValue, byte.MaxValue, 215,
				1, 1, 128, 17, byte.MaxValue, byte.MaxValue, 216, 1, 1, 96,
				18, 7, 2, 217, 1, 2, 63, 19, 23, 2,
				219, 1, 2, 240, 17, 9, 2, 229, 1, 4,
				96, 18, 17, 2, 233, 1, 4, 184, 17, 11,
				2, 223, 1, 2, 240, 17, 13, 2, 225, 1,
				2, 156, 17, byte.MaxValue, byte.MaxValue, 221, 1, 1, 184, 17,
				byte.MaxValue, byte.MaxValue, 222, 1, 1, 224, 17, 15, 2, 0,
				0, 0, 240, 17, byte.MaxValue, byte.MaxValue, 0, 0, 0, 204,
				17, byte.MaxValue, byte.MaxValue, 227, 1, 1, 224, 17, byte.MaxValue, byte.MaxValue,
				228, 1, 1, 32, 18, 19, 2, 0, 0, 0,
				96, 18, 21, 2, 0, 0, 0, 8, 18, byte.MaxValue,
				byte.MaxValue, 237, 1, 3, 32, 18, byte.MaxValue, byte.MaxValue, 240, 1,
				3, 64, 18, byte.MaxValue, byte.MaxValue, 243, 1, 1, 96, 18,
				byte.MaxValue, byte.MaxValue, 244, 1, 1, 208, 18, 25, 2, byte.MaxValue,
				1, 1, 63, 19, 51, 2, 0, 2, 1, 152,
				18, 27, 2, 0, 0, 0, 208, 18, 35, 2,
				0, 0, 0, 124, 18, byte.MaxValue, byte.MaxValue, 245, 1, 1,
				152, 18, 29, 2, 246, 1, 1, 136, 18, 31,
				2, 0, 0, 0, 152, 18, 33, 2, 0, 0,
				0, 130, 18, byte.MaxValue, byte.MaxValue, 247, 1, 2, 136, 18,
				byte.MaxValue, byte.MaxValue, 249, 1, 2, 144, 18, byte.MaxValue, byte.MaxValue, 251,
				1, 1, 152, 18, byte.MaxValue, byte.MaxValue, 252, 1, 1, 180,
				18, 37, 2, 0, 0, 0, 208, 18, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 166, 18, 39, 2, 0, 0, 0,
				180, 18, byte.MaxValue, byte.MaxValue, 0, 0, 0, 159, 18, 41,
				2, 0, 0, 0, 166, 18, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 156, 18, 43, 2, 0, 0, 0, 159, 18,
				47, 2, 0, 0, 0, 154, 18, 45, 2, 0,
				0, 0, 156, 18, byte.MaxValue, byte.MaxValue, 0, 0, 0, 153,
				18, byte.MaxValue, byte.MaxValue, 253, 1, 1, 154, 18, byte.MaxValue, byte.MaxValue,
				0, 0, 0, 158, 18, 49, 2, 0, 0, 0,
				159, 18, byte.MaxValue, byte.MaxValue, 0, 0, 0, 157, 18, byte.MaxValue,
				byte.MaxValue, 254, 1, 1, 158, 18, byte.MaxValue, byte.MaxValue, 0, 0,
				0, 0, 19, byte.MaxValue, byte.MaxValue, 0, 0, 0, 63, 19,
				53, 2, 0, 0, 0, 32, 19, byte.MaxValue, byte.MaxValue, 1,
				2, 2, 63, 19, 55, 2, 3, 2, 1, 48,
				19, byte.MaxValue, byte.MaxValue, 4, 2, 1, 63, 19, byte.MaxValue, byte.MaxValue,
				5, 2, 1
			}, new ushort[]
			{
				0, 17, 18, 1, 30, 42, 31, 43, 19, 32,
				33, 37, 2, 44, 45, 49, 19, 32, 33, 37,
				2, 44, 45, 49, 34, 35, 36, 46, 47, 48,
				34, 35, 36, 46, 47, 48, 20, 3, 20, 3,
				21, 39, 4, 51, 21, 39, 4, 51, 38, 50,
				38, 50, 22, 23, 5, 22, 23, 5, 24, 25,
				6, 26, 27, 28, 28, 29, 40, 41, 7, 52,
				52, 53, 65, 53, 65, 66, 66, 54, 69, 70,
				71, 54, 69, 70, 71, 67, 68, 72, 67, 68,
				72, 55, 56, 56, 73, 74, 74, 57, 58, 57,
				58, 59, 60, 61, 62, 63, 63, 64, 75, 64,
				75, 76, 76, 8, 77, 77, 78, 90, 78, 90,
				91, 91, 79, 94, 95, 96, 79, 94, 95, 96,
				92, 93, 97, 92, 93, 97, 80, 81, 81, 98,
				99, 99, 82, 83, 82, 83, 84, 85, 86, 87,
				88, 88, 89, 100, 89, 100, 101, 101, 9, 102,
				102, 103, 115, 103, 115, 116, 116, 104, 119, 120,
				121, 104, 119, 120, 121, 117, 118, 122, 117, 118,
				122, 105, 106, 106, 123, 124, 124, 107, 108, 107,
				108, 109, 110, 111, 112, 113, 113, 114, 125, 114,
				125, 126, 126, 10, 127, 127, 128, 140, 128, 140,
				141, 141, 129, 144, 145, 146, 129, 144, 145, 146,
				142, 143, 147, 142, 143, 147, 130, 131, 131, 148,
				149, 149, 132, 133, 132, 133, 134, 135, 136, 137,
				138, 138, 139, 150, 139, 150, 151, 151, 11, 152,
				152, 153, 165, 153, 165, 166, 166, 154, 169, 170,
				171, 154, 169, 170, 171, 167, 168, 172, 167, 168,
				172, 155, 156, 156, 173, 174, 174, 157, 158, 157,
				158, 159, 160, 161, 162, 163, 163, 164, 175, 164,
				175, 176, 176, 12, 177, 177, 178, 190, 178, 190,
				191, 191, 179, 194, 195, 196, 179, 194, 195, 196,
				192, 193, 197, 192, 193, 197, 180, 181, 181, 198,
				199, 199, 182, 183, 182, 183, 184, 185, 186, 187,
				188, 188, 189, 200, 189, 200, 201, 201, 13, 202,
				202, 203, 215, 203, 215, 216, 216, 204, 219, 220,
				221, 204, 219, 220, 221, 217, 218, 222, 217, 218,
				222, 205, 206, 206, 223, 224, 224, 207, 208, 207,
				208, 209, 210, 211, 212, 213, 213, 214, 225, 214,
				225, 226, 226, 14, 227, 227, 228, 240, 228, 240,
				241, 241, 229, 244, 245, 246, 229, 244, 245, 246,
				242, 243, 247, 242, 243, 247, 230, 231, 231, 248,
				249, 249, 232, 233, 232, 233, 234, 235, 236, 237,
				238, 238, 239, 250, 239, 250, 251, 251, 15, 252,
				252, 253, 265, 253, 265, 266, 266, 254, 269, 270,
				271, 254, 269, 270, 271, 267, 268, 272, 267, 268,
				272, 255, 256, 256, 273, 274, 274, 257, 258, 257,
				258, 259, 260, 261, 262, 263, 263, 264, 275, 264,
				275, 276, 276, 16, 281, 16, 281, 277, 277, 278,
				290, 278, 290, 291, 291, 279, 294, 295, 296, 279,
				294, 295, 296, 292, 293, 297, 292, 293, 297, 280,
				298, 299, 299, 282, 283, 282, 283, 284, 285, 286,
				287, 288, 288, 289, 300, 289, 301, 301
			});
			builder.Finish();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00025408 File Offset: 0x00023608
		private TouchControl Initialize_ctrlTouchscreenprimaryTouch(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 0).WithParent(parent)
				.WithChildren(17, 13)
				.WithName("primaryTouch")
				.WithDisplayName("Primary Touch")
				.WithLayout(kTouchLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 0U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000254B4 File Offset: 0x000236B4
		private Vector2Control Initialize_ctrlTouchscreenposition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 1).WithParent(parent)
				.WithChildren(42, 2)
				.WithName("position")
				.WithDisplayName("Position")
				.WithLayout(kVector2Layout)
				.WithUsages(1, 1)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 4U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00025564 File Offset: 0x00023764
		private DeltaControl Initialize_ctrlTouchscreendelta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 2).WithParent(parent)
				.WithChildren(44, 6)
				.WithName("delta")
				.WithDisplayName("Delta")
				.WithLayout(kDeltaLayout)
				.WithUsages(2, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 12U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002560C File Offset: 0x0002380C
		private AxisControl Initialize_ctrlTouchscreenpressure(InternedString kAnalogLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 3).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Pressure")
				.WithLayout(kAnalogLayout)
				.WithUsages(3, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 20U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.WithDefaultState(1)
				.Finish();
			return axisControl;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000256B8 File Offset: 0x000238B8
		private Vector2Control Initialize_ctrlTouchscreenradius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 4).WithParent(parent)
				.WithChildren(50, 2)
				.WithName("radius")
				.WithDisplayName("Radius")
				.WithLayout(kVector2Layout)
				.WithUsages(4, 1)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 24U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00025760 File Offset: 0x00023960
		private TouchPressControl Initialize_ctrlTouchscreenpress(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 5).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Press")
				.WithLayout(kTouchPressLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 32U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00025818 File Offset: 0x00023A18
		private IntegerControl Initialize_ctrlTouchscreendisplayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 6).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 34U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000258AC File Offset: 0x00023AAC
		private TouchControl Initialize_ctrlTouchscreentouch0(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 7).WithParent(parent)
				.WithChildren(52, 13)
				.WithName("touch0")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 56U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00025950 File Offset: 0x00023B50
		private TouchControl Initialize_ctrlTouchscreentouch1(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 8).WithParent(parent)
				.WithChildren(77, 13)
				.WithName("touch1")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 112U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000259F4 File Offset: 0x00023BF4
		private TouchControl Initialize_ctrlTouchscreentouch2(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 9).WithParent(parent)
				.WithChildren(102, 13)
				.WithName("touch2")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 168U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00025A9C File Offset: 0x00023C9C
		private TouchControl Initialize_ctrlTouchscreentouch3(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 10).WithParent(parent)
				.WithChildren(127, 13)
				.WithName("touch3")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 224U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00025B44 File Offset: 0x00023D44
		private TouchControl Initialize_ctrlTouchscreentouch4(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 11).WithParent(parent)
				.WithChildren(152, 13)
				.WithName("touch4")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 280U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00025BEC File Offset: 0x00023DEC
		private TouchControl Initialize_ctrlTouchscreentouch5(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 12).WithParent(parent)
				.WithChildren(177, 13)
				.WithName("touch5")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 336U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00025C94 File Offset: 0x00023E94
		private TouchControl Initialize_ctrlTouchscreentouch6(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 13).WithParent(parent)
				.WithChildren(202, 13)
				.WithName("touch6")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 392U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00025D3C File Offset: 0x00023F3C
		private TouchControl Initialize_ctrlTouchscreentouch7(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 14).WithParent(parent)
				.WithChildren(227, 13)
				.WithName("touch7")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 448U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00025DE4 File Offset: 0x00023FE4
		private TouchControl Initialize_ctrlTouchscreentouch8(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 15).WithParent(parent)
				.WithChildren(252, 13)
				.WithName("touch8")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 504U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00025E8C File Offset: 0x0002408C
		private TouchControl Initialize_ctrlTouchscreentouch9(InternedString kTouchLayout, InputControl parent)
		{
			TouchControl touchControl = new TouchControl();
			touchControl.Setup().At(this, 16).WithParent(parent)
				.WithChildren(277, 13)
				.WithName("touch9")
				.WithDisplayName("Touch")
				.WithLayout(kTouchLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1414485315),
					byteOffset = 560U,
					bitOffset = 0U,
					sizeInBits = 448U
				})
				.Finish();
			return touchControl;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00025F34 File Offset: 0x00024134
		private IntegerControl Initialize_ctrlTouchscreenprimaryTouchtouchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 17).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Primary Touch Touch ID")
				.WithShortDisplayName("Primary Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 0U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00025FE8 File Offset: 0x000241E8
		private Vector2Control Initialize_ctrlTouchscreenprimaryTouchposition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 18).WithParent(parent)
				.WithChildren(30, 2)
				.WithName("position")
				.WithDisplayName("Primary Touch Position")
				.WithShortDisplayName("Primary Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 4U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002609C File Offset: 0x0002429C
		private DeltaControl Initialize_ctrlTouchscreenprimaryTouchdelta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 19).WithParent(parent)
				.WithChildren(32, 6)
				.WithName("delta")
				.WithDisplayName("Primary Touch Delta")
				.WithShortDisplayName("Primary Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 12U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00026148 File Offset: 0x00024348
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchpressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 20).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Primary Touch Pressure")
				.WithShortDisplayName("Primary Touch Pressure")
				.WithLayout(kAxisLayout)
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

		// Token: 0x0600083E RID: 2110 RVA: 0x000261E8 File Offset: 0x000243E8
		private Vector2Control Initialize_ctrlTouchscreenprimaryTouchradius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 21).WithParent(parent)
				.WithChildren(38, 2)
				.WithName("radius")
				.WithDisplayName("Primary Touch Radius")
				.WithShortDisplayName("Primary Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 24U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00026294 File Offset: 0x00024494
		private TouchPhaseControl Initialize_ctrlTouchscreenprimaryTouchphase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 22).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Primary Touch Touch Phase")
				.WithShortDisplayName("Primary Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 32U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002633C File Offset: 0x0002453C
		private TouchPressControl Initialize_ctrlTouchscreenprimaryTouchpress(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 23).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Primary Touch Touch Contact?")
				.WithShortDisplayName("Primary Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 32U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000263F8 File Offset: 0x000245F8
		private IntegerControl Initialize_ctrlTouchscreenprimaryTouchtapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 24).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Primary Touch Tap Count")
				.WithShortDisplayName("Primary Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 33U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00026498 File Offset: 0x00024698
		private IntegerControl Initialize_ctrlTouchscreenprimaryTouchdisplayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 25).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Primary Touch Display Index")
				.WithShortDisplayName("Primary Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 34U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00026538 File Offset: 0x00024738
		private ButtonControl Initialize_ctrlTouchscreenprimaryTouchindirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 26).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Primary Touch Indirect Touch?")
				.WithShortDisplayName("Primary Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 35U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00026600 File Offset: 0x00024800
		private ButtonControl Initialize_ctrlTouchscreenprimaryTouchtap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 27).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Primary Touch Tap")
				.WithShortDisplayName("Primary Touch Tap")
				.WithLayout(kButtonLayout)
				.WithUsages(0, 1)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 35U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000266C8 File Offset: 0x000248C8
		private DoubleControl Initialize_ctrlTouchscreenprimaryTouchstartTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 28).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Primary Touch Start Time")
				.WithShortDisplayName("Primary Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 40U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00026774 File Offset: 0x00024974
		private Vector2Control Initialize_ctrlTouchscreenprimaryTouchstartPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 29).WithParent(parent)
				.WithChildren(40, 2)
				.WithName("startPosition")
				.WithDisplayName("Primary Touch Start Position")
				.WithShortDisplayName("Primary Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 48U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00026828 File Offset: 0x00024A28
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchpositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 30).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Primary Touch Primary Touch Position X")
				.WithShortDisplayName("Primary Touch Primary Touch Position X")
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

		// Token: 0x06000848 RID: 2120 RVA: 0x000268D0 File Offset: 0x00024AD0
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchpositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 31).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Primary Touch Primary Touch Position Y")
				.WithShortDisplayName("Primary Touch Primary Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
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

		// Token: 0x06000849 RID: 2121 RVA: 0x00026978 File Offset: 0x00024B78
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 32).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Primary Touch Primary Touch Delta Up")
				.WithShortDisplayName("Primary Touch Primary Touch Delta Up")
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

		// Token: 0x0600084A RID: 2122 RVA: 0x00026A34 File Offset: 0x00024C34
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 33).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Primary Touch Primary Touch Delta Down")
				.WithShortDisplayName("Primary Touch Primary Touch Delta Down")
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

		// Token: 0x0600084B RID: 2123 RVA: 0x00026AF8 File Offset: 0x00024CF8
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 34).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Primary Touch Primary Touch Delta Left")
				.WithShortDisplayName("Primary Touch Primary Touch Delta Left")
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

		// Token: 0x0600084C RID: 2124 RVA: 0x00026BBC File Offset: 0x00024DBC
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 35).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Primary Touch Primary Touch Delta Right")
				.WithShortDisplayName("Primary Touch Primary Touch Delta Right")
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

		// Token: 0x0600084D RID: 2125 RVA: 0x00026C78 File Offset: 0x00024E78
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 36).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Primary Touch Primary Touch Delta X")
				.WithShortDisplayName("Primary Touch Primary Touch Delta X")
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

		// Token: 0x0600084E RID: 2126 RVA: 0x00026D18 File Offset: 0x00024F18
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchdeltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 37).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Primary Touch Primary Touch Delta Y")
				.WithShortDisplayName("Primary Touch Primary Touch Delta Y")
				.WithLayout(kAxisLayout)
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

		// Token: 0x0600084F RID: 2127 RVA: 0x00026DB8 File Offset: 0x00024FB8
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchradiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 38).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Primary Touch Primary Touch Radius X")
				.WithShortDisplayName("Primary Touch Primary Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 24U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00026E58 File Offset: 0x00025058
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchradiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 39).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Primary Touch Primary Touch Radius Y")
				.WithShortDisplayName("Primary Touch Primary Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 28U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00026EF8 File Offset: 0x000250F8
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchstartPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 40).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Primary Touch Primary Touch Start Position X")
				.WithShortDisplayName("Primary Touch Primary Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 48U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00026F98 File Offset: 0x00025198
		private AxisControl Initialize_ctrlTouchscreenprimaryTouchstartPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 41).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Primary Touch Primary Touch Start Position Y")
				.WithShortDisplayName("Primary Touch Primary Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 52U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00027038 File Offset: 0x00025238
		private AxisControl Initialize_ctrlTouchscreenpositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 42).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Position X")
				.WithShortDisplayName("Position X")
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

		// Token: 0x06000854 RID: 2132 RVA: 0x000270E0 File Offset: 0x000252E0
		private AxisControl Initialize_ctrlTouchscreenpositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 43).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Position Y")
				.WithShortDisplayName("Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
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

		// Token: 0x06000855 RID: 2133 RVA: 0x00027188 File Offset: 0x00025388
		private AxisControl Initialize_ctrlTouchscreendeltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 44).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Delta Up")
				.WithShortDisplayName("Delta Up")
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

		// Token: 0x06000856 RID: 2134 RVA: 0x00027244 File Offset: 0x00025444
		private AxisControl Initialize_ctrlTouchscreendeltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 45).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Delta Down")
				.WithShortDisplayName("Delta Down")
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

		// Token: 0x06000857 RID: 2135 RVA: 0x00027308 File Offset: 0x00025508
		private AxisControl Initialize_ctrlTouchscreendeltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 46).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Delta Left")
				.WithShortDisplayName("Delta Left")
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

		// Token: 0x06000858 RID: 2136 RVA: 0x000273CC File Offset: 0x000255CC
		private AxisControl Initialize_ctrlTouchscreendeltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 47).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Delta Right")
				.WithShortDisplayName("Delta Right")
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

		// Token: 0x06000859 RID: 2137 RVA: 0x00027488 File Offset: 0x00025688
		private AxisControl Initialize_ctrlTouchscreendeltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 48).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Delta X")
				.WithShortDisplayName("Delta X")
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

		// Token: 0x0600085A RID: 2138 RVA: 0x00027528 File Offset: 0x00025728
		private AxisControl Initialize_ctrlTouchscreendeltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 49).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Delta Y")
				.WithShortDisplayName("Delta Y")
				.WithLayout(kAxisLayout)
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

		// Token: 0x0600085B RID: 2139 RVA: 0x000275C8 File Offset: 0x000257C8
		private AxisControl Initialize_ctrlTouchscreenradiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 50).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Radius X")
				.WithShortDisplayName("Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 24U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00027668 File Offset: 0x00025868
		private AxisControl Initialize_ctrlTouchscreenradiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 51).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Radius Y")
				.WithShortDisplayName("Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 28U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00027708 File Offset: 0x00025908
		private IntegerControl Initialize_ctrlTouchscreentouch0touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 52).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 56U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000277BC File Offset: 0x000259BC
		private Vector2Control Initialize_ctrlTouchscreentouch0position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 53).WithParent(parent)
				.WithChildren(65, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 60U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00027870 File Offset: 0x00025A70
		private DeltaControl Initialize_ctrlTouchscreentouch0delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 54).WithParent(parent)
				.WithChildren(67, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 68U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002791C File Offset: 0x00025B1C
		private AxisControl Initialize_ctrlTouchscreentouch0pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 55).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 76U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000279BC File Offset: 0x00025BBC
		private Vector2Control Initialize_ctrlTouchscreentouch0radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 56).WithParent(parent)
				.WithChildren(73, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 80U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00027A68 File Offset: 0x00025C68
		private TouchPhaseControl Initialize_ctrlTouchscreentouch0phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 57).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 88U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00027B10 File Offset: 0x00025D10
		private TouchPressControl Initialize_ctrlTouchscreentouch0press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 58).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 88U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00027BCC File Offset: 0x00025DCC
		private IntegerControl Initialize_ctrlTouchscreentouch0tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 59).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 89U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00027C6C File Offset: 0x00025E6C
		private IntegerControl Initialize_ctrlTouchscreentouch0displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 60).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 90U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00027D0C File Offset: 0x00025F0C
		private ButtonControl Initialize_ctrlTouchscreentouch0indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 61).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 91U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00027DD4 File Offset: 0x00025FD4
		private ButtonControl Initialize_ctrlTouchscreentouch0tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 62).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 91U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00027E90 File Offset: 0x00026090
		private DoubleControl Initialize_ctrlTouchscreentouch0startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 63).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 96U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00027F3C File Offset: 0x0002613C
		private Vector2Control Initialize_ctrlTouchscreentouch0startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 64).WithParent(parent)
				.WithChildren(75, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 104U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00027FF0 File Offset: 0x000261F0
		private AxisControl Initialize_ctrlTouchscreentouch0positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 65).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 60U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002809C File Offset: 0x0002629C
		private AxisControl Initialize_ctrlTouchscreentouch0positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 66).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 64U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00028148 File Offset: 0x00026348
		private AxisControl Initialize_ctrlTouchscreentouch0deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 67).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 72U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00028204 File Offset: 0x00026404
		private AxisControl Initialize_ctrlTouchscreentouch0deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 68).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 72U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000282C8 File Offset: 0x000264C8
		private AxisControl Initialize_ctrlTouchscreentouch0deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 69).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 68U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002838C File Offset: 0x0002658C
		private AxisControl Initialize_ctrlTouchscreentouch0deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 70).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 68U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00028448 File Offset: 0x00026648
		private AxisControl Initialize_ctrlTouchscreentouch0deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 71).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 68U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000284E8 File Offset: 0x000266E8
		private AxisControl Initialize_ctrlTouchscreentouch0deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 72).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 72U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00028588 File Offset: 0x00026788
		private AxisControl Initialize_ctrlTouchscreentouch0radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 73).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 80U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00028628 File Offset: 0x00026828
		private AxisControl Initialize_ctrlTouchscreentouch0radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 74).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 84U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000286C8 File Offset: 0x000268C8
		private AxisControl Initialize_ctrlTouchscreentouch0startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 75).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 104U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00028768 File Offset: 0x00026968
		private AxisControl Initialize_ctrlTouchscreentouch0startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 76).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 108U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00028808 File Offset: 0x00026A08
		private IntegerControl Initialize_ctrlTouchscreentouch1touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 77).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 112U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000288BC File Offset: 0x00026ABC
		private Vector2Control Initialize_ctrlTouchscreentouch1position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 78).WithParent(parent)
				.WithChildren(90, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 116U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00028970 File Offset: 0x00026B70
		private DeltaControl Initialize_ctrlTouchscreentouch1delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 79).WithParent(parent)
				.WithChildren(92, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 124U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00028A1C File Offset: 0x00026C1C
		private AxisControl Initialize_ctrlTouchscreentouch1pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 80).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 132U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00028AC0 File Offset: 0x00026CC0
		private Vector2Control Initialize_ctrlTouchscreentouch1radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 81).WithParent(parent)
				.WithChildren(98, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 136U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00028B70 File Offset: 0x00026D70
		private TouchPhaseControl Initialize_ctrlTouchscreentouch1phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 82).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 144U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00028C1C File Offset: 0x00026E1C
		private TouchPressControl Initialize_ctrlTouchscreentouch1press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 83).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 144U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00028CDC File Offset: 0x00026EDC
		private IntegerControl Initialize_ctrlTouchscreentouch1tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 84).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 145U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00028D80 File Offset: 0x00026F80
		private IntegerControl Initialize_ctrlTouchscreentouch1displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 85).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 146U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00028E24 File Offset: 0x00027024
		private ButtonControl Initialize_ctrlTouchscreentouch1indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 86).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 147U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00028EEC File Offset: 0x000270EC
		private ButtonControl Initialize_ctrlTouchscreentouch1tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 87).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 147U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00028FAC File Offset: 0x000271AC
		private DoubleControl Initialize_ctrlTouchscreentouch1startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 88).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 152U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00029058 File Offset: 0x00027258
		private Vector2Control Initialize_ctrlTouchscreentouch1startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 89).WithParent(parent)
				.WithChildren(100, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 160U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00029110 File Offset: 0x00027310
		private AxisControl Initialize_ctrlTouchscreentouch1positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 90).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 116U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000291BC File Offset: 0x000273BC
		private AxisControl Initialize_ctrlTouchscreentouch1positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 91).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 120U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00029268 File Offset: 0x00027468
		private AxisControl Initialize_ctrlTouchscreentouch1deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 92).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 128U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00029328 File Offset: 0x00027528
		private AxisControl Initialize_ctrlTouchscreentouch1deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 93).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 128U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000293F0 File Offset: 0x000275F0
		private AxisControl Initialize_ctrlTouchscreentouch1deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 94).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 124U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000294B4 File Offset: 0x000276B4
		private AxisControl Initialize_ctrlTouchscreentouch1deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 95).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 124U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00029570 File Offset: 0x00027770
		private AxisControl Initialize_ctrlTouchscreentouch1deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 96).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 124U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00029610 File Offset: 0x00027810
		private AxisControl Initialize_ctrlTouchscreentouch1deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 97).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 128U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000296B4 File Offset: 0x000278B4
		private AxisControl Initialize_ctrlTouchscreentouch1radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 98).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 136U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00029758 File Offset: 0x00027958
		private AxisControl Initialize_ctrlTouchscreentouch1radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 99).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 140U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000297FC File Offset: 0x000279FC
		private AxisControl Initialize_ctrlTouchscreentouch1startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 100).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 160U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000298A0 File Offset: 0x00027AA0
		private AxisControl Initialize_ctrlTouchscreentouch1startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 101).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 164U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00029944 File Offset: 0x00027B44
		private IntegerControl Initialize_ctrlTouchscreentouch2touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 102).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 168U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000299FC File Offset: 0x00027BFC
		private Vector2Control Initialize_ctrlTouchscreentouch2position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 103).WithParent(parent)
				.WithChildren(115, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 172U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00029AB4 File Offset: 0x00027CB4
		private DeltaControl Initialize_ctrlTouchscreentouch2delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 104).WithParent(parent)
				.WithChildren(117, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 180U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00029B64 File Offset: 0x00027D64
		private AxisControl Initialize_ctrlTouchscreentouch2pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 105).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 188U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00029C08 File Offset: 0x00027E08
		private Vector2Control Initialize_ctrlTouchscreentouch2radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 106).WithParent(parent)
				.WithChildren(123, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 192U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00029CB8 File Offset: 0x00027EB8
		private TouchPhaseControl Initialize_ctrlTouchscreentouch2phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 107).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 200U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00029D64 File Offset: 0x00027F64
		private TouchPressControl Initialize_ctrlTouchscreentouch2press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 108).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 200U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00029E24 File Offset: 0x00028024
		private IntegerControl Initialize_ctrlTouchscreentouch2tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 109).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 201U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00029EC8 File Offset: 0x000280C8
		private IntegerControl Initialize_ctrlTouchscreentouch2displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 110).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 202U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00029F6C File Offset: 0x0002816C
		private ButtonControl Initialize_ctrlTouchscreentouch2indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 111).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 203U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002A034 File Offset: 0x00028234
		private ButtonControl Initialize_ctrlTouchscreentouch2tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 112).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 203U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002A0F4 File Offset: 0x000282F4
		private DoubleControl Initialize_ctrlTouchscreentouch2startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 113).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 208U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002A1A0 File Offset: 0x000283A0
		private Vector2Control Initialize_ctrlTouchscreentouch2startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 114).WithParent(parent)
				.WithChildren(125, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 216U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002A258 File Offset: 0x00028458
		private AxisControl Initialize_ctrlTouchscreentouch2positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 115).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 172U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002A304 File Offset: 0x00028504
		private AxisControl Initialize_ctrlTouchscreentouch2positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 116).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 176U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0002A3B0 File Offset: 0x000285B0
		private AxisControl Initialize_ctrlTouchscreentouch2deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 117).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 184U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0002A470 File Offset: 0x00028670
		private AxisControl Initialize_ctrlTouchscreentouch2deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 118).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 184U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002A538 File Offset: 0x00028738
		private AxisControl Initialize_ctrlTouchscreentouch2deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 119).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 180U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002A600 File Offset: 0x00028800
		private AxisControl Initialize_ctrlTouchscreentouch2deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 120).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 180U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002A6C0 File Offset: 0x000288C0
		private AxisControl Initialize_ctrlTouchscreentouch2deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 121).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 180U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002A764 File Offset: 0x00028964
		private AxisControl Initialize_ctrlTouchscreentouch2deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 122).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 184U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002A808 File Offset: 0x00028A08
		private AxisControl Initialize_ctrlTouchscreentouch2radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 123).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 192U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002A8AC File Offset: 0x00028AAC
		private AxisControl Initialize_ctrlTouchscreentouch2radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 124).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 196U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002A950 File Offset: 0x00028B50
		private AxisControl Initialize_ctrlTouchscreentouch2startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 125).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 216U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002A9F4 File Offset: 0x00028BF4
		private AxisControl Initialize_ctrlTouchscreentouch2startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 126).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 220U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002AA98 File Offset: 0x00028C98
		private IntegerControl Initialize_ctrlTouchscreentouch3touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 127).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 224U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002AB50 File Offset: 0x00028D50
		private Vector2Control Initialize_ctrlTouchscreentouch3position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 128).WithParent(parent)
				.WithChildren(140, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 228U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002AC10 File Offset: 0x00028E10
		private DeltaControl Initialize_ctrlTouchscreentouch3delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 129).WithParent(parent)
				.WithChildren(142, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 236U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002ACC4 File Offset: 0x00028EC4
		private AxisControl Initialize_ctrlTouchscreentouch3pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 130).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 244U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002AD6C File Offset: 0x00028F6C
		private Vector2Control Initialize_ctrlTouchscreentouch3radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 131).WithParent(parent)
				.WithChildren(148, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 248U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0002AE20 File Offset: 0x00029020
		private TouchPhaseControl Initialize_ctrlTouchscreentouch3phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 132).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 256U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002AED0 File Offset: 0x000290D0
		private TouchPressControl Initialize_ctrlTouchscreentouch3press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 133).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 256U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0002AF94 File Offset: 0x00029194
		private IntegerControl Initialize_ctrlTouchscreentouch3tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 134).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 257U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0002B03C File Offset: 0x0002923C
		private IntegerControl Initialize_ctrlTouchscreentouch3displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 135).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 258U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002B0E4 File Offset: 0x000292E4
		private ButtonControl Initialize_ctrlTouchscreentouch3indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 136).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 259U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002B1B0 File Offset: 0x000293B0
		private ButtonControl Initialize_ctrlTouchscreentouch3tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 137).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 259U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002B274 File Offset: 0x00029474
		private DoubleControl Initialize_ctrlTouchscreentouch3startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 138).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 264U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002B324 File Offset: 0x00029524
		private Vector2Control Initialize_ctrlTouchscreentouch3startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 139).WithParent(parent)
				.WithChildren(150, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 272U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002B3E4 File Offset: 0x000295E4
		private AxisControl Initialize_ctrlTouchscreentouch3positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 140).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 228U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002B494 File Offset: 0x00029694
		private AxisControl Initialize_ctrlTouchscreentouch3positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 141).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 232U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002B544 File Offset: 0x00029744
		private AxisControl Initialize_ctrlTouchscreentouch3deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 142).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 240U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002B608 File Offset: 0x00029808
		private AxisControl Initialize_ctrlTouchscreentouch3deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 143).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 240U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002B6D0 File Offset: 0x000298D0
		private AxisControl Initialize_ctrlTouchscreentouch3deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 144).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 236U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002B798 File Offset: 0x00029998
		private AxisControl Initialize_ctrlTouchscreentouch3deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 145).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 236U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002B85C File Offset: 0x00029A5C
		private AxisControl Initialize_ctrlTouchscreentouch3deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 146).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 236U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002B904 File Offset: 0x00029B04
		private AxisControl Initialize_ctrlTouchscreentouch3deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 147).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 240U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002B9AC File Offset: 0x00029BAC
		private AxisControl Initialize_ctrlTouchscreentouch3radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 148).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 248U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002BA54 File Offset: 0x00029C54
		private AxisControl Initialize_ctrlTouchscreentouch3radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 149).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 252U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002BAFC File Offset: 0x00029CFC
		private AxisControl Initialize_ctrlTouchscreentouch3startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 150).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 272U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002BBA4 File Offset: 0x00029DA4
		private AxisControl Initialize_ctrlTouchscreentouch3startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 151).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 276U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002BC4C File Offset: 0x00029E4C
		private IntegerControl Initialize_ctrlTouchscreentouch4touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 152).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 280U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002BD04 File Offset: 0x00029F04
		private Vector2Control Initialize_ctrlTouchscreentouch4position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 153).WithParent(parent)
				.WithChildren(165, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 284U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002BDC4 File Offset: 0x00029FC4
		private DeltaControl Initialize_ctrlTouchscreentouch4delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 154).WithParent(parent)
				.WithChildren(167, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 292U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002BE78 File Offset: 0x0002A078
		private AxisControl Initialize_ctrlTouchscreentouch4pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 155).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 300U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002BF20 File Offset: 0x0002A120
		private Vector2Control Initialize_ctrlTouchscreentouch4radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 156).WithParent(parent)
				.WithChildren(173, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 304U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002BFD4 File Offset: 0x0002A1D4
		private TouchPhaseControl Initialize_ctrlTouchscreentouch4phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 157).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 312U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002C084 File Offset: 0x0002A284
		private TouchPressControl Initialize_ctrlTouchscreentouch4press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 158).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 312U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002C148 File Offset: 0x0002A348
		private IntegerControl Initialize_ctrlTouchscreentouch4tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 159).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 313U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002C1F0 File Offset: 0x0002A3F0
		private IntegerControl Initialize_ctrlTouchscreentouch4displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 160).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 314U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002C298 File Offset: 0x0002A498
		private ButtonControl Initialize_ctrlTouchscreentouch4indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 161).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 315U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002C364 File Offset: 0x0002A564
		private ButtonControl Initialize_ctrlTouchscreentouch4tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 162).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 315U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002C428 File Offset: 0x0002A628
		private DoubleControl Initialize_ctrlTouchscreentouch4startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 163).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 320U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
		private Vector2Control Initialize_ctrlTouchscreentouch4startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 164).WithParent(parent)
				.WithChildren(175, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 328U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002C598 File Offset: 0x0002A798
		private AxisControl Initialize_ctrlTouchscreentouch4positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 165).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 284U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002C648 File Offset: 0x0002A848
		private AxisControl Initialize_ctrlTouchscreentouch4positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 166).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 288U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002C6F8 File Offset: 0x0002A8F8
		private AxisControl Initialize_ctrlTouchscreentouch4deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 167).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 296U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002C7BC File Offset: 0x0002A9BC
		private AxisControl Initialize_ctrlTouchscreentouch4deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 168).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 296U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002C884 File Offset: 0x0002AA84
		private AxisControl Initialize_ctrlTouchscreentouch4deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 169).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 292U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002C94C File Offset: 0x0002AB4C
		private AxisControl Initialize_ctrlTouchscreentouch4deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 170).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 292U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002CA10 File Offset: 0x0002AC10
		private AxisControl Initialize_ctrlTouchscreentouch4deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 171).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 292U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002CAB8 File Offset: 0x0002ACB8
		private AxisControl Initialize_ctrlTouchscreentouch4deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 172).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 296U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002CB60 File Offset: 0x0002AD60
		private AxisControl Initialize_ctrlTouchscreentouch4radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 173).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 304U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002CC08 File Offset: 0x0002AE08
		private AxisControl Initialize_ctrlTouchscreentouch4radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 174).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 308U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002CCB0 File Offset: 0x0002AEB0
		private AxisControl Initialize_ctrlTouchscreentouch4startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 175).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 328U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002CD58 File Offset: 0x0002AF58
		private AxisControl Initialize_ctrlTouchscreentouch4startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 176).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 332U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002CE00 File Offset: 0x0002B000
		private IntegerControl Initialize_ctrlTouchscreentouch5touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 177).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 336U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002CEB8 File Offset: 0x0002B0B8
		private Vector2Control Initialize_ctrlTouchscreentouch5position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 178).WithParent(parent)
				.WithChildren(190, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 340U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002CF78 File Offset: 0x0002B178
		private DeltaControl Initialize_ctrlTouchscreentouch5delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 179).WithParent(parent)
				.WithChildren(192, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 348U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002D02C File Offset: 0x0002B22C
		private AxisControl Initialize_ctrlTouchscreentouch5pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 180).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 356U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002D0D4 File Offset: 0x0002B2D4
		private Vector2Control Initialize_ctrlTouchscreentouch5radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 181).WithParent(parent)
				.WithChildren(198, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 360U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002D188 File Offset: 0x0002B388
		private TouchPhaseControl Initialize_ctrlTouchscreentouch5phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 182).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 368U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002D238 File Offset: 0x0002B438
		private TouchPressControl Initialize_ctrlTouchscreentouch5press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 183).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 368U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002D2FC File Offset: 0x0002B4FC
		private IntegerControl Initialize_ctrlTouchscreentouch5tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 184).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 369U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002D3A4 File Offset: 0x0002B5A4
		private IntegerControl Initialize_ctrlTouchscreentouch5displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 185).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 370U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002D44C File Offset: 0x0002B64C
		private ButtonControl Initialize_ctrlTouchscreentouch5indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 186).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 371U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002D518 File Offset: 0x0002B718
		private ButtonControl Initialize_ctrlTouchscreentouch5tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 187).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 371U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002D5DC File Offset: 0x0002B7DC
		private DoubleControl Initialize_ctrlTouchscreentouch5startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 188).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 376U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002D68C File Offset: 0x0002B88C
		private Vector2Control Initialize_ctrlTouchscreentouch5startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 189).WithParent(parent)
				.WithChildren(200, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 384U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002D74C File Offset: 0x0002B94C
		private AxisControl Initialize_ctrlTouchscreentouch5positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 190).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 340U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002D7FC File Offset: 0x0002B9FC
		private AxisControl Initialize_ctrlTouchscreentouch5positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 191).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 344U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002D8AC File Offset: 0x0002BAAC
		private AxisControl Initialize_ctrlTouchscreentouch5deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 192).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 352U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002D970 File Offset: 0x0002BB70
		private AxisControl Initialize_ctrlTouchscreentouch5deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 193).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 352U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0002DA38 File Offset: 0x0002BC38
		private AxisControl Initialize_ctrlTouchscreentouch5deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 194).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 348U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002DB00 File Offset: 0x0002BD00
		private AxisControl Initialize_ctrlTouchscreentouch5deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 195).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 348U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002DBC4 File Offset: 0x0002BDC4
		private AxisControl Initialize_ctrlTouchscreentouch5deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 196).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 348U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002DC6C File Offset: 0x0002BE6C
		private AxisControl Initialize_ctrlTouchscreentouch5deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 197).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 352U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002DD14 File Offset: 0x0002BF14
		private AxisControl Initialize_ctrlTouchscreentouch5radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 198).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 360U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002DDBC File Offset: 0x0002BFBC
		private AxisControl Initialize_ctrlTouchscreentouch5radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 199).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 364U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002DE64 File Offset: 0x0002C064
		private AxisControl Initialize_ctrlTouchscreentouch5startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 200).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 384U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002DF0C File Offset: 0x0002C10C
		private AxisControl Initialize_ctrlTouchscreentouch5startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 201).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 388U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002DFB4 File Offset: 0x0002C1B4
		private IntegerControl Initialize_ctrlTouchscreentouch6touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 202).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 392U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002E06C File Offset: 0x0002C26C
		private Vector2Control Initialize_ctrlTouchscreentouch6position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 203).WithParent(parent)
				.WithChildren(215, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 396U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002E12C File Offset: 0x0002C32C
		private DeltaControl Initialize_ctrlTouchscreentouch6delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 204).WithParent(parent)
				.WithChildren(217, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 404U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
		private AxisControl Initialize_ctrlTouchscreentouch6pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 205).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 412U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002E288 File Offset: 0x0002C488
		private Vector2Control Initialize_ctrlTouchscreentouch6radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 206).WithParent(parent)
				.WithChildren(223, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 416U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0002E33C File Offset: 0x0002C53C
		private TouchPhaseControl Initialize_ctrlTouchscreentouch6phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 207).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 424U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002E3EC File Offset: 0x0002C5EC
		private TouchPressControl Initialize_ctrlTouchscreentouch6press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 208).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 424U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002E4B0 File Offset: 0x0002C6B0
		private IntegerControl Initialize_ctrlTouchscreentouch6tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 209).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 425U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002E558 File Offset: 0x0002C758
		private IntegerControl Initialize_ctrlTouchscreentouch6displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 210).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 426U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002E600 File Offset: 0x0002C800
		private ButtonControl Initialize_ctrlTouchscreentouch6indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 211).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 427U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002E6CC File Offset: 0x0002C8CC
		private ButtonControl Initialize_ctrlTouchscreentouch6tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 212).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 427U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002E790 File Offset: 0x0002C990
		private DoubleControl Initialize_ctrlTouchscreentouch6startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 213).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 432U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002E840 File Offset: 0x0002CA40
		private Vector2Control Initialize_ctrlTouchscreentouch6startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 214).WithParent(parent)
				.WithChildren(225, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 440U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002E900 File Offset: 0x0002CB00
		private AxisControl Initialize_ctrlTouchscreentouch6positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 215).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 396U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002E9B0 File Offset: 0x0002CBB0
		private AxisControl Initialize_ctrlTouchscreentouch6positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 216).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 400U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002EA60 File Offset: 0x0002CC60
		private AxisControl Initialize_ctrlTouchscreentouch6deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 217).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 408U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002EB24 File Offset: 0x0002CD24
		private AxisControl Initialize_ctrlTouchscreentouch6deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 218).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 408U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002EBEC File Offset: 0x0002CDEC
		private AxisControl Initialize_ctrlTouchscreentouch6deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 219).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 404U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
		private AxisControl Initialize_ctrlTouchscreentouch6deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 220).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 404U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002ED78 File Offset: 0x0002CF78
		private AxisControl Initialize_ctrlTouchscreentouch6deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 221).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 404U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002EE20 File Offset: 0x0002D020
		private AxisControl Initialize_ctrlTouchscreentouch6deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 222).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 408U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		private AxisControl Initialize_ctrlTouchscreentouch6radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 223).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 416U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002EF70 File Offset: 0x0002D170
		private AxisControl Initialize_ctrlTouchscreentouch6radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 224).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 420U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002F018 File Offset: 0x0002D218
		private AxisControl Initialize_ctrlTouchscreentouch6startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 225).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 440U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002F0C0 File Offset: 0x0002D2C0
		private AxisControl Initialize_ctrlTouchscreentouch6startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 226).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 444U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002F168 File Offset: 0x0002D368
		private IntegerControl Initialize_ctrlTouchscreentouch7touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 227).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 448U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002F220 File Offset: 0x0002D420
		private Vector2Control Initialize_ctrlTouchscreentouch7position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 228).WithParent(parent)
				.WithChildren(240, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 452U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002F2E0 File Offset: 0x0002D4E0
		private DeltaControl Initialize_ctrlTouchscreentouch7delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 229).WithParent(parent)
				.WithChildren(242, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 460U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002F394 File Offset: 0x0002D594
		private AxisControl Initialize_ctrlTouchscreentouch7pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 230).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 468U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002F43C File Offset: 0x0002D63C
		private Vector2Control Initialize_ctrlTouchscreentouch7radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 231).WithParent(parent)
				.WithChildren(248, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 472U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002F4F0 File Offset: 0x0002D6F0
		private TouchPhaseControl Initialize_ctrlTouchscreentouch7phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 232).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 480U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002F5A0 File Offset: 0x0002D7A0
		private TouchPressControl Initialize_ctrlTouchscreentouch7press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 233).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 480U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002F664 File Offset: 0x0002D864
		private IntegerControl Initialize_ctrlTouchscreentouch7tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 234).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 481U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002F70C File Offset: 0x0002D90C
		private IntegerControl Initialize_ctrlTouchscreentouch7displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 235).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 482U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002F7B4 File Offset: 0x0002D9B4
		private ButtonControl Initialize_ctrlTouchscreentouch7indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 236).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 483U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002F880 File Offset: 0x0002DA80
		private ButtonControl Initialize_ctrlTouchscreentouch7tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 237).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 483U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002F944 File Offset: 0x0002DB44
		private DoubleControl Initialize_ctrlTouchscreentouch7startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 238).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 488U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002F9F4 File Offset: 0x0002DBF4
		private Vector2Control Initialize_ctrlTouchscreentouch7startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 239).WithParent(parent)
				.WithChildren(250, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 496U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002FAB4 File Offset: 0x0002DCB4
		private AxisControl Initialize_ctrlTouchscreentouch7positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 240).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 452U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002FB64 File Offset: 0x0002DD64
		private AxisControl Initialize_ctrlTouchscreentouch7positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 241).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 456U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0002FC14 File Offset: 0x0002DE14
		private AxisControl Initialize_ctrlTouchscreentouch7deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 242).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 464U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002FCD8 File Offset: 0x0002DED8
		private AxisControl Initialize_ctrlTouchscreentouch7deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 243).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 464U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002FDA0 File Offset: 0x0002DFA0
		private AxisControl Initialize_ctrlTouchscreentouch7deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 244).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 460U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002FE68 File Offset: 0x0002E068
		private AxisControl Initialize_ctrlTouchscreentouch7deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 245).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 460U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002FF2C File Offset: 0x0002E12C
		private AxisControl Initialize_ctrlTouchscreentouch7deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 246).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 460U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
		private AxisControl Initialize_ctrlTouchscreentouch7deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 247).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 464U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0003007C File Offset: 0x0002E27C
		private AxisControl Initialize_ctrlTouchscreentouch7radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 248).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 472U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00030124 File Offset: 0x0002E324
		private AxisControl Initialize_ctrlTouchscreentouch7radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 249).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 476U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000301CC File Offset: 0x0002E3CC
		private AxisControl Initialize_ctrlTouchscreentouch7startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 250).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 496U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00030274 File Offset: 0x0002E474
		private AxisControl Initialize_ctrlTouchscreentouch7startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 251).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 500U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0003031C File Offset: 0x0002E51C
		private IntegerControl Initialize_ctrlTouchscreentouch8touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 252).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 504U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000303D4 File Offset: 0x0002E5D4
		private Vector2Control Initialize_ctrlTouchscreentouch8position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 253).WithParent(parent)
				.WithChildren(265, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 508U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00030494 File Offset: 0x0002E694
		private DeltaControl Initialize_ctrlTouchscreentouch8delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 254).WithParent(parent)
				.WithChildren(267, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 516U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00030548 File Offset: 0x0002E748
		private AxisControl Initialize_ctrlTouchscreentouch8pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 255).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 524U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000305F0 File Offset: 0x0002E7F0
		private Vector2Control Initialize_ctrlTouchscreentouch8radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 256).WithParent(parent)
				.WithChildren(273, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 528U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000306A4 File Offset: 0x0002E8A4
		private TouchPhaseControl Initialize_ctrlTouchscreentouch8phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 257).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 536U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00030754 File Offset: 0x0002E954
		private TouchPressControl Initialize_ctrlTouchscreentouch8press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 258).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 536U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00030818 File Offset: 0x0002EA18
		private IntegerControl Initialize_ctrlTouchscreentouch8tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 259).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 537U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000308C0 File Offset: 0x0002EAC0
		private IntegerControl Initialize_ctrlTouchscreentouch8displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 260).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 538U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00030968 File Offset: 0x0002EB68
		private ButtonControl Initialize_ctrlTouchscreentouch8indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 261).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 539U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00030A34 File Offset: 0x0002EC34
		private ButtonControl Initialize_ctrlTouchscreentouch8tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 262).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 539U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00030AF8 File Offset: 0x0002ECF8
		private DoubleControl Initialize_ctrlTouchscreentouch8startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 263).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 544U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00030BA8 File Offset: 0x0002EDA8
		private Vector2Control Initialize_ctrlTouchscreentouch8startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 264).WithParent(parent)
				.WithChildren(275, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 552U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00030C68 File Offset: 0x0002EE68
		private AxisControl Initialize_ctrlTouchscreentouch8positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 265).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 508U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00030D18 File Offset: 0x0002EF18
		private AxisControl Initialize_ctrlTouchscreentouch8positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 266).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 512U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00030DC8 File Offset: 0x0002EFC8
		private AxisControl Initialize_ctrlTouchscreentouch8deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 267).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 520U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00030E8C File Offset: 0x0002F08C
		private AxisControl Initialize_ctrlTouchscreentouch8deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 268).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 520U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00030F54 File Offset: 0x0002F154
		private AxisControl Initialize_ctrlTouchscreentouch8deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 269).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 516U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0003101C File Offset: 0x0002F21C
		private AxisControl Initialize_ctrlTouchscreentouch8deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 270).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 516U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000310E0 File Offset: 0x0002F2E0
		private AxisControl Initialize_ctrlTouchscreentouch8deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 271).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 516U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00031188 File Offset: 0x0002F388
		private AxisControl Initialize_ctrlTouchscreentouch8deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 272).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 520U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00031230 File Offset: 0x0002F430
		private AxisControl Initialize_ctrlTouchscreentouch8radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 273).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 528U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000312D8 File Offset: 0x0002F4D8
		private AxisControl Initialize_ctrlTouchscreentouch8radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 274).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 532U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00031380 File Offset: 0x0002F580
		private AxisControl Initialize_ctrlTouchscreentouch8startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 275).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 552U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00031428 File Offset: 0x0002F628
		private AxisControl Initialize_ctrlTouchscreentouch8startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 276).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 556U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000314D0 File Offset: 0x0002F6D0
		private IntegerControl Initialize_ctrlTouchscreentouch9touchId(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 277).WithParent(parent)
				.WithName("touchId")
				.WithDisplayName("Touch Touch ID")
				.WithShortDisplayName("Touch Touch ID")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(true)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1229870112),
					byteOffset = 560U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00031588 File Offset: 0x0002F788
		private Vector2Control Initialize_ctrlTouchscreentouch9position(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 278).WithParent(parent)
				.WithChildren(290, 2)
				.WithName("position")
				.WithDisplayName("Touch Position")
				.WithShortDisplayName("Touch Position")
				.WithLayout(kVector2Layout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 564U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00031648 File Offset: 0x0002F848
		private DeltaControl Initialize_ctrlTouchscreentouch9delta(InternedString kDeltaLayout, InputControl parent)
		{
			DeltaControl deltaControl = new DeltaControl();
			deltaControl.Setup().At(this, 279).WithParent(parent)
				.WithChildren(292, 6)
				.WithName("delta")
				.WithDisplayName("Touch Delta")
				.WithShortDisplayName("Touch Delta")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 572U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return deltaControl;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000316FC File Offset: 0x0002F8FC
		private AxisControl Initialize_ctrlTouchscreentouch9pressure(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 280).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Touch Pressure")
				.WithShortDisplayName("Touch Pressure")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 580U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000317A4 File Offset: 0x0002F9A4
		private Vector2Control Initialize_ctrlTouchscreentouch9radius(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 281).WithParent(parent)
				.WithChildren(298, 2)
				.WithName("radius")
				.WithDisplayName("Touch Radius")
				.WithShortDisplayName("Touch Radius")
				.WithLayout(kVector2Layout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 584U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00031858 File Offset: 0x0002FA58
		private TouchPhaseControl Initialize_ctrlTouchscreentouch9phase(InternedString kTouchPhaseLayout, InputControl parent)
		{
			TouchPhaseControl touchPhaseControl = new TouchPhaseControl();
			touchPhaseControl.Setup().At(this, 282).WithParent(parent)
				.WithName("phase")
				.WithDisplayName("Touch Touch Phase")
				.WithShortDisplayName("Touch Touch Phase")
				.WithLayout(kTouchPhaseLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 592U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return touchPhaseControl;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00031908 File Offset: 0x0002FB08
		private TouchPressControl Initialize_ctrlTouchscreentouch9press(InternedString kTouchPressLayout, InputControl parent)
		{
			TouchPressControl touchPressControl = new TouchPressControl();
			touchPressControl.Setup().At(this, 283).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Touch Touch Contact?")
				.WithShortDisplayName("Touch Touch Contact?")
				.WithLayout(kTouchPressLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 592U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return touchPressControl;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000319CC File Offset: 0x0002FBCC
		private IntegerControl Initialize_ctrlTouchscreentouch9tapCount(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 284).WithParent(parent)
				.WithName("tapCount")
				.WithDisplayName("Touch Tap Count")
				.WithShortDisplayName("Touch Tap Count")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 593U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00031A74 File Offset: 0x0002FC74
		private IntegerControl Initialize_ctrlTouchscreentouch9displayIndex(InternedString kIntegerLayout, InputControl parent)
		{
			IntegerControl integerControl = new IntegerControl();
			integerControl.Setup().At(this, 285).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Touch Display Index")
				.WithShortDisplayName("Touch Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1113150533),
					byteOffset = 594U,
					bitOffset = 0U,
					sizeInBits = 8U
				})
				.Finish();
			return integerControl;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00031B1C File Offset: 0x0002FD1C
		private ButtonControl Initialize_ctrlTouchscreentouch9indirectTouch(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 286).WithParent(parent)
				.WithName("indirectTouch")
				.WithDisplayName("Touch Indirect Touch?")
				.WithShortDisplayName("Touch Indirect Touch?")
				.WithLayout(kButtonLayout)
				.IsSynthetic(true)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 595U,
					bitOffset = 0U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00031BE8 File Offset: 0x0002FDE8
		private ButtonControl Initialize_ctrlTouchscreentouch9tap(InternedString kButtonLayout, InputControl parent)
		{
			ButtonControl buttonControl = new ButtonControl();
			buttonControl.Setup().At(this, 287).WithParent(parent)
				.WithName("tap")
				.WithDisplayName("Touch Tap")
				.WithShortDisplayName("Touch Tap")
				.WithLayout(kButtonLayout)
				.IsButton(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1112101920),
					byteOffset = 595U,
					bitOffset = 4U,
					sizeInBits = 1U
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00031CAC File Offset: 0x0002FEAC
		private DoubleControl Initialize_ctrlTouchscreentouch9startTime(InternedString kDoubleLayout, InputControl parent)
		{
			DoubleControl doubleControl = new DoubleControl();
			doubleControl.Setup().At(this, 288).WithParent(parent)
				.WithName("startTime")
				.WithDisplayName("Touch Start Time")
				.WithShortDisplayName("Touch Start Time")
				.WithLayout(kDoubleLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1145195552),
					byteOffset = 600U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return doubleControl;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00031D5C File Offset: 0x0002FF5C
		private Vector2Control Initialize_ctrlTouchscreentouch9startPosition(InternedString kVector2Layout, InputControl parent)
		{
			Vector2Control vector2Control = new Vector2Control();
			vector2Control.Setup().At(this, 289).WithParent(parent)
				.WithChildren(300, 2)
				.WithName("startPosition")
				.WithDisplayName("Touch Start Position")
				.WithShortDisplayName("Touch Start Position")
				.WithLayout(kVector2Layout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1447379762),
					byteOffset = 608U,
					bitOffset = 0U,
					sizeInBits = 64U
				})
				.Finish();
			return vector2Control;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00031E1C File Offset: 0x0003001C
		private AxisControl Initialize_ctrlTouchscreentouch9positionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 290).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Position X")
				.WithShortDisplayName("Touch Touch Position X")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 564U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00031ECC File Offset: 0x000300CC
		private AxisControl Initialize_ctrlTouchscreentouch9positiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 291).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Position Y")
				.WithShortDisplayName("Touch Touch Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 568U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00031F7C File Offset: 0x0003017C
		private AxisControl Initialize_ctrlTouchscreentouch9deltaup(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 292).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Touch Touch Delta Up")
				.WithShortDisplayName("Touch Touch Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 576U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00032040 File Offset: 0x00030240
		private AxisControl Initialize_ctrlTouchscreentouch9deltadown(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 293).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Touch Touch Delta Down")
				.WithShortDisplayName("Touch Touch Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 576U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00032108 File Offset: 0x00030308
		private AxisControl Initialize_ctrlTouchscreentouch9deltaleft(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMin = -3.402823E+38f;
			axisControl.invert = true;
			axisControl.Setup().At(this, 294).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Touch Touch Delta Left")
				.WithShortDisplayName("Touch Touch Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 572U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000321D0 File Offset: 0x000303D0
		private AxisControl Initialize_ctrlTouchscreentouch9deltaright(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.clamp = AxisControl.Clamp.BeforeNormalize;
			axisControl.clampMax = 3.402823E+38f;
			axisControl.Setup().At(this, 295).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Touch Touch Delta Right")
				.WithShortDisplayName("Touch Touch Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(true)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 572U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00032294 File Offset: 0x00030494
		private AxisControl Initialize_ctrlTouchscreentouch9deltax(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 296).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Delta X")
				.WithShortDisplayName("Touch Touch Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 572U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0003233C File Offset: 0x0003053C
		private AxisControl Initialize_ctrlTouchscreentouch9deltay(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 297).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Delta Y")
				.WithShortDisplayName("Touch Touch Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 576U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000323E4 File Offset: 0x000305E4
		private AxisControl Initialize_ctrlTouchscreentouch9radiusx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 298).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Radius X")
				.WithShortDisplayName("Touch Touch Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 584U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0003248C File Offset: 0x0003068C
		private AxisControl Initialize_ctrlTouchscreentouch9radiusy(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 299).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Radius Y")
				.WithShortDisplayName("Touch Touch Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 588U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00032534 File Offset: 0x00030734
		private AxisControl Initialize_ctrlTouchscreentouch9startPositionx(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 300).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Touch Touch Start Position X")
				.WithShortDisplayName("Touch Touch Start Position X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 608U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000325DC File Offset: 0x000307DC
		private AxisControl Initialize_ctrlTouchscreentouch9startPositiony(InternedString kAxisLayout, InputControl parent)
		{
			AxisControl axisControl = new AxisControl();
			axisControl.Setup().At(this, 301).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Touch Touch Start Position Y")
				.WithShortDisplayName("Touch Touch Start Position Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new InputStateBlock
				{
					format = new FourCC(1179407392),
					byteOffset = 612U,
					bitOffset = 0U,
					sizeInBits = 32U
				})
				.Finish();
			return axisControl;
		}

		// Token: 0x040003E6 RID: 998
		public const string metadata = "AutoWindowSpace;Touch;Vector2;Delta;Analog;TouchPress;Button;Axis;Integer;TouchPhase;Double;Touchscreen;Pointer";
	}
}
