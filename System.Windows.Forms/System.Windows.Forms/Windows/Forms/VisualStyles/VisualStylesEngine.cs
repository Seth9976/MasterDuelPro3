using System;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x0200036D RID: 877
	internal class VisualStylesEngine
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x00087358 File Offset: 0x00085558
		public static IVisualStyles Instance
		{
			get
			{
				return VisualStylesEngine.instance;
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00087360 File Offset: 0x00085560
		private static IVisualStyles Initialize()
		{
			string text = Environment.GetEnvironmentVariable("MONO_VISUAL_STYLES");
			if (text != null)
			{
				text = text.ToLower();
			}
			if (text == "gtkplus" && VisualStylesGtkPlus.Initialize())
			{
				return new VisualStylesGtkPlus();
			}
			return new VisualStylesNative();
		}

		// Token: 0x04001827 RID: 6183
		private static IVisualStyles instance = VisualStylesEngine.Initialize();
	}
}
