using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001C4 RID: 452
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public struct TextEvent : IInputEventTypeInfo
	{
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x000512E6 File Offset: 0x0004F4E6
		public FourCC typeStatic
		{
			get
			{
				return 1413830740;
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000512F4 File Offset: 0x0004F4F4
		public unsafe static TextEvent* From(InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			if (!eventPtr.IsA<TextEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into TextEvent", eventPtr.type));
			}
			return (TextEvent*)eventPtr.data;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00051344 File Offset: 0x0004F544
		public static TextEvent Create(int deviceId, char character, double time = -1.0)
		{
			return new TextEvent
			{
				baseEvent = new InputEvent(1413830740, 24, deviceId, time),
				character = (int)character
			};
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0005137C File Offset: 0x0004F57C
		public static TextEvent Create(int deviceId, int character, double time = -1.0)
		{
			return new TextEvent
			{
				baseEvent = new InputEvent(1413830740, 24, deviceId, time),
				character = character
			};
		}

		// Token: 0x04000A41 RID: 2625
		public const int Type = 1413830740;

		// Token: 0x04000A42 RID: 2626
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x04000A43 RID: 2627
		[FieldOffset(20)]
		public int character;
	}
}
