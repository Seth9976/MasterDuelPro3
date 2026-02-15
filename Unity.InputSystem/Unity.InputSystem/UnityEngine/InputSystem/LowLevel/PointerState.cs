using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x0200019C RID: 412
	internal struct PointerState : IInputStateTypeInfo
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0004DF07 File Offset: 0x0004C107
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('P', 'T', 'R', ' ');
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0004DF16 File Offset: 0x0004C116
		public FourCC format
		{
			get
			{
				return PointerState.kFormat;
			}
		}

		// Token: 0x040009A8 RID: 2472
		private uint pointerId;

		// Token: 0x040009A9 RID: 2473
		[InputControl(layout = "Vector2", displayName = "Position", usage = "Point", dontReset = true)]
		public Vector2 position;

		// Token: 0x040009AA RID: 2474
		[InputControl(layout = "Delta", displayName = "Delta", usage = "Secondary2DMotion")]
		public Vector2 delta;

		// Token: 0x040009AB RID: 2475
		[InputControl(layout = "Analog", displayName = "Pressure", usage = "Pressure", defaultState = 1f)]
		public float pressure;

		// Token: 0x040009AC RID: 2476
		[InputControl(layout = "Vector2", displayName = "Radius", usage = "Radius")]
		public Vector2 radius;

		// Token: 0x040009AD RID: 2477
		[InputControl(name = "press", displayName = "Press", layout = "Button", format = "BIT", bit = 0U)]
		public ushort buttons;

		// Token: 0x040009AE RID: 2478
		[InputControl(name = "displayIndex", layout = "Integer", displayName = "Display Index")]
		public ushort displayIndex;
	}
}
