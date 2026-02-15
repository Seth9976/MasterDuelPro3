using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001B RID: 27
	public class BindingCompleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state, and binding context.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values. </param>
		// Token: 0x06000084 RID: 132 RVA: 0x00003A94 File Offset: 0x00001C94
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context)
			: this(binding, state, context, string.Empty, null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state and text, binding context, exception, and whether the binding should be cancelled.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values. </param>
		/// <param name="errorText">The error text or exception message for errors that occurred during the binding.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> that occurred during the binding.</param>
		/// <param name="cancel">true to cancel the binding and keep focus on the current control; false to allow focus to shift to another control.</param>
		// Token: 0x06000085 RID: 133 RVA: 0x00003AA6 File Offset: 0x00001CA6
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText, Exception exception, bool cancel)
			: base(cancel)
		{
			this.binding = binding;
			this.state = state;
			this.context = context;
			this.error_text = errorText;
			this.exception = exception;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003AD5 File Offset: 0x00001CD5
		internal void SetErrorText(string error_text)
		{
			this.error_text = error_text;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003ADE File Offset: 0x00001CDE
		internal void SetException(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x040000B2 RID: 178
		private Binding binding;

		// Token: 0x040000B3 RID: 179
		private BindingCompleteState state;

		// Token: 0x040000B4 RID: 180
		private BindingCompleteContext context;

		// Token: 0x040000B5 RID: 181
		private string error_text;

		// Token: 0x040000B6 RID: 182
		private Exception exception;
	}
}
