using System;

namespace System.Windows.Forms
{
	/// <summary>Determines how a control validates its data when it loses user input focus.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000017 RID: 23
	public enum AutoValidate
	{
		/// <summary>The control inherits its <see cref="T:System.Windows.Forms.AutoValidate" /> behavior from its container (such as a form or another control). If there is no container control, it defaults to <see cref="F:System.Windows.Forms.AutoValidate.EnablePreventFocusChange" />.</summary>
		// Token: 0x04000094 RID: 148
		Inherit = -1,
		/// <summary>Implicit validation will not occur. Setting this value will not interfere with explicit calls to <see cref="M:System.Windows.Forms.ContainerControl.Validate" /> or <see cref="M:System.Windows.Forms.ContainerControl.ValidateChildren" />.</summary>
		// Token: 0x04000095 RID: 149
		Disable,
		/// <summary>Implicit validation occurs when the control loses focus.</summary>
		// Token: 0x04000096 RID: 150
		EnablePreventFocusChange,
		/// <summary>Implicit validation occurs, but if validation fails, focus will still change to the new control. If validation fails, the <see cref="E:System.Windows.Forms.Control.Validated" /> event will not fire.</summary>
		// Token: 0x04000097 RID: 151
		EnableAllowFocusChange
	}
}
