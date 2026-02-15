using System;

namespace System.Xml.Schema
{
	// Token: 0x0200030B RID: 779
	internal enum ValidatorState
	{
		// Token: 0x04001010 RID: 4112
		None,
		// Token: 0x04001011 RID: 4113
		Start,
		// Token: 0x04001012 RID: 4114
		TopLevelAttribute,
		// Token: 0x04001013 RID: 4115
		TopLevelTextOrWS,
		// Token: 0x04001014 RID: 4116
		Element,
		// Token: 0x04001015 RID: 4117
		Attribute,
		// Token: 0x04001016 RID: 4118
		EndOfAttributes,
		// Token: 0x04001017 RID: 4119
		Text,
		// Token: 0x04001018 RID: 4120
		Whitespace,
		// Token: 0x04001019 RID: 4121
		EndElement,
		// Token: 0x0400101A RID: 4122
		SkipToEndElement,
		// Token: 0x0400101B RID: 4123
		Finish
	}
}
