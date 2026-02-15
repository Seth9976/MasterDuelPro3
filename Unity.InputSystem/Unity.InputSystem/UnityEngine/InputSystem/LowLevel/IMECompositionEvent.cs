using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001AF RID: 431
	[StructLayout(LayoutKind.Explicit, Size = 152)]
	public struct IMECompositionEvent : IInputEventTypeInfo
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0004E65A File Offset: 0x0004C85A
		public FourCC typeStatic
		{
			get
			{
				return 1229800787;
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0004E668 File Offset: 0x0004C868
		public static IMECompositionEvent Create(int deviceId, string compositionString, double time)
		{
			return new IMECompositionEvent
			{
				baseEvent = new InputEvent(1229800787, 152, deviceId, time),
				compositionString = new IMECompositionString(compositionString)
			};
		}

		// Token: 0x040009E4 RID: 2532
		internal const int kIMECharBufferSize = 64;

		// Token: 0x040009E5 RID: 2533
		public const int Type = 1229800787;

		// Token: 0x040009E6 RID: 2534
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x040009E7 RID: 2535
		[FieldOffset(20)]
		public IMECompositionString compositionString;
	}
}
