using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x02000194 RID: 404
	public interface ITextInputReceiver
	{
		// Token: 0x06000FBD RID: 4029
		void OnTextInput(char character);

		// Token: 0x06000FBE RID: 4030
		void OnIMECompositionChanged(IMECompositionString compositionString);
	}
}
