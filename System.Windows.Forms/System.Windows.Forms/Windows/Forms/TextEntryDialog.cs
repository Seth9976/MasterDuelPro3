using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000091 RID: 145
	internal partial class TextEntryDialog : Form
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x00018A1C File Offset: 0x00016C1C
		public TextEntryDialog()
		{
			this.groupBox1 = new GroupBox();
			this.cancelButton = new Button();
			this.iconPictureBox = new PictureBox();
			this.newNameTextBox = new TextBox();
			this.okButton = new Button();
			this.label1 = new Label();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.newNameTextBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.iconPictureBox);
			this.groupBox1.Location = new Point(8, 8);
			this.groupBox1.Size = new Size(232, 160);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = Locale.GetText("New Name");
			this.cancelButton.DialogResult = DialogResult.Cancel;
			this.cancelButton.Location = new Point(168, 176);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = Locale.GetText("Cancel");
			this.iconPictureBox.BorderStyle = BorderStyle.Fixed3D;
			this.iconPictureBox.Location = new Point(86, 24);
			this.iconPictureBox.Size = new Size(60, 60);
			this.iconPictureBox.TabIndex = 3;
			this.iconPictureBox.TabStop = false;
			this.iconPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			this.newNameTextBox.Location = new Point(16, 128);
			this.newNameTextBox.Size = new Size(200, 20);
			this.newNameTextBox.TabIndex = 5;
			this.newNameTextBox.Text = string.Empty;
			this.okButton.DialogResult = DialogResult.OK;
			this.okButton.Location = new Point(80, 176);
			this.okButton.TabIndex = 3;
			this.okButton.Text = Locale.GetText("OK");
			this.label1.Location = new Point(16, 96);
			this.label1.Size = new Size(200, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = Locale.GetText("Enter Name:");
			this.label1.TextAlign = ContentAlignment.MiddleCenter;
			base.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new Size(5, 13);
			base.CancelButton = this.cancelButton;
			base.ClientSize = new Size(248, 205);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.okButton);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.Text = Locale.GetText("New Folder or File");
			this.groupBox1.ResumeLayout(false);
			base.ResumeLayout(false);
			this.newNameTextBox.Select();
		}

		// Token: 0x17000171 RID: 369
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00018D46 File Offset: 0x00016F46
		public Image IconPictureBoxImage
		{
			set
			{
				this.iconPictureBox.Image = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00018D54 File Offset: 0x00016F54
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00018D61 File Offset: 0x00016F61
		public string FileName
		{
			get
			{
				return this.newNameTextBox.Text;
			}
			set
			{
				this.newNameTextBox.Text = value;
			}
		}

		// Token: 0x040003BF RID: 959
		private Label label1;

		// Token: 0x040003C0 RID: 960
		private Button okButton;

		// Token: 0x040003C1 RID: 961
		private TextBox newNameTextBox;

		// Token: 0x040003C2 RID: 962
		private PictureBox iconPictureBox;

		// Token: 0x040003C3 RID: 963
		private Button cancelButton;

		// Token: 0x040003C4 RID: 964
		private GroupBox groupBox1;
	}
}
