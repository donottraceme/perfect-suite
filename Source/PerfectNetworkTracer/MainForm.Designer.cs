namespace PerfectNetworkTracer
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._divInterfacesInformation = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._labelStatus = new Nabu.Forms.Html.HtmlComponent(this.components);
			this.SuspendLayout();
			// 
			// _divInterfacesInformation
			// 
			this._divInterfacesInformation.BindingID = "_divInterfacesInformation";
			this._divInterfacesInformation.HtmlForm = this;
			// 
			// _labelStatus
			// 
			this._labelStatus.BindingID = "_labelStatus";
			this._labelStatus.HtmlForm = this;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(622, 435);
			this.Html = resources.GetString("$this.Html");
			this.Location = new System.Drawing.Point(0, 0);
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Perfect Network Tracer";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Nabu.Forms.Html.HtmlComponent _divInterfacesInformation;
		private Nabu.Forms.Html.HtmlComponent _labelStatus;








	}
}

