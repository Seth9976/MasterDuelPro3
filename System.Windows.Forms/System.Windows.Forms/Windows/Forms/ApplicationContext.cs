using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the contextual information about an application thread.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200000E RID: 14
	public class ApplicationContext : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with no context.</summary>
		// Token: 0x06000033 RID: 51 RVA: 0x00002BEC File Offset: 0x00000DEC
		public ApplicationContext()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with the specified <see cref="T:System.Windows.Forms.Form" />.</summary>
		/// <param name="mainForm">The main <see cref="T:System.Windows.Forms.Form" /> of the application to use for context. </param>
		// Token: 0x06000034 RID: 52 RVA: 0x00002BF5 File Offset: 0x00000DF5
		public ApplicationContext(Form mainForm)
		{
			this.MainForm = mainForm;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C04 File Offset: 0x00000E04
		~ApplicationContext()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.Form" /> to use as context.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> to use as context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002C34 File Offset: 0x00000E34
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002C3C File Offset: 0x00000E3C
		public Form MainForm
		{
			get
			{
				return this.main_form;
			}
			set
			{
				if (this.main_form != value)
				{
					if (this.main_form != null)
					{
						this.main_form.HandleDestroyed -= this.OnMainFormClosed;
					}
					this.main_form = value;
					if (this.main_form != null)
					{
						this.main_form.HandleDestroyed += this.OnMainFormClosed;
					}
				}
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000038 RID: 56 RVA: 0x00002C99 File Offset: 0x00000E99
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Terminates the message loop of the thread.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000039 RID: 57 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public void ExitThread()
		{
			this.ExitThreadCore();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600003A RID: 58 RVA: 0x00002CB0 File Offset: 0x00000EB0
		protected virtual void Dispose(bool disposing)
		{
			this.MainForm = null;
			this.tag = null;
		}

		/// <summary>Terminates the message loop of the thread.</summary>
		// Token: 0x0600003B RID: 59 RVA: 0x00002CC0 File Offset: 0x00000EC0
		protected virtual void ExitThreadCore()
		{
			if (Application.MWFThread.Current.Context == this)
			{
				XplatUI.PostQuitMessage(0);
			}
			if (!this.thread_exit_raised && this.ThreadExit != null)
			{
				this.thread_exit_raised = true;
				this.ThreadExit(this, EventArgs.Empty);
			}
		}

		/// <summary>Calls <see cref="M:System.Windows.Forms.ApplicationContext.ExitThreadCore" />, which raises the <see cref="E:System.Windows.Forms.ApplicationContext.ThreadExit" /> event.</summary>
		/// <param name="sender">The object that raised the event. </param>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600003C RID: 60 RVA: 0x00002CFD File Offset: 0x00000EFD
		protected virtual void OnMainFormClosed(object sender, EventArgs e)
		{
			if (!this.MainForm.RecreatingHandle)
			{
				this.ExitThreadCore();
			}
		}

		// Token: 0x04000066 RID: 102
		private Form main_form;

		// Token: 0x04000067 RID: 103
		private object tag;

		// Token: 0x04000068 RID: 104
		private bool thread_exit_raised;

		// Token: 0x04000069 RID: 105
		[CompilerGenerated]
		private EventHandler ThreadExit;
	}
}
