namespace PerfectPasswordGenerator
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
			this._backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this._groupGeneratorOptions = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkGeneratorLowerCaseLetters = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkGeneratorUpperCaseLetters = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkGeneratorDigits = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkGeneratorPunctuation = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkGeneratorPronounceable = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkGeneratorUnambiguous = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkGeneratorMobileFriendly = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._numberPasswordLength = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._numberPasswordNumber = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._groupValidatorOptions = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkValidatorGoogle = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkValidatorYahoo = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkValidatorBing = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._checkValidatorDictionaries = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._groupPasswords = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._textPasswords = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._labelPasswordRating = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._buttonGenerate = new Nabu.Forms.Html.HtmlComponent(this.components);
			this._labelStatus = new Nabu.Forms.Html.HtmlComponent(this.components);
			this.SuspendLayout();
			// 
			// _backgroundWorker
			// 
			this._backgroundWorker.WorkerReportsProgress = true;
			this._backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
			this._backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
			this._backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
			// 
			// _groupGeneratorOptions
			// 
			this._groupGeneratorOptions.BindingID = "_groupGeneratorOptions";
			this._groupGeneratorOptions.HtmlForm = this;
			// 
			// _checkGeneratorLowerCaseLetters
			// 
			this._checkGeneratorLowerCaseLetters.BindingID = "_checkGeneratorLowerCaseLetters";
			this._checkGeneratorLowerCaseLetters.HtmlForm = this;
			this._checkGeneratorLowerCaseLetters.Changed += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.GeneratorOptions_Changed);
			// 
			// _checkGeneratorUpperCaseLetters
			// 
			this._checkGeneratorUpperCaseLetters.BindingID = "_checkGeneratorUpperCaseLetters";
			this._checkGeneratorUpperCaseLetters.HtmlForm = this;
			this._checkGeneratorUpperCaseLetters.Changed += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.GeneratorOptions_Changed);
			// 
			// _checkGeneratorDigits
			// 
			this._checkGeneratorDigits.BindingID = "_checkGeneratorDigits";
			this._checkGeneratorDigits.HtmlForm = this;
			this._checkGeneratorDigits.Changed += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.GeneratorOptions_Changed);
			// 
			// _checkGeneratorPunctuation
			// 
			this._checkGeneratorPunctuation.BindingID = "_checkGeneratorPunctuation";
			this._checkGeneratorPunctuation.HtmlForm = this;
			this._checkGeneratorPunctuation.Changed += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.GeneratorOptions_Changed);
			// 
			// _checkGeneratorPronounceable
			// 
			this._checkGeneratorPronounceable.BindingID = "_checkGeneratorPronounceable";
			this._checkGeneratorPronounceable.HtmlForm = this;
			this._checkGeneratorPronounceable.Changed += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.GeneratorOptions_Changed);
			// 
			// _checkGeneratorUnambiguous
			// 
			this._checkGeneratorUnambiguous.BindingID = "_checkGeneratorUnambiguous";
			this._checkGeneratorUnambiguous.HtmlForm = this;
			this._checkGeneratorUnambiguous.Changed += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.GeneratorOptions_Changed);
			// 
			// _checkGeneratorMobileFriendly
			// 
			this._checkGeneratorMobileFriendly.BindingID = "_checkGeneratorMobileFriendly";
			this._checkGeneratorMobileFriendly.HtmlForm = this;
			this._checkGeneratorMobileFriendly.Changed += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.GeneratorOptions_Changed);
			// 
			// _numberPasswordLength
			// 
			this._numberPasswordLength.BindingID = "_numberPasswordLength";
			this._numberPasswordLength.HtmlForm = this;
			this._numberPasswordLength.Changed += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.GeneratorOptions_Changed);
			// 
			// _numberPasswordNumber
			// 
			this._numberPasswordNumber.BindingID = "_numberPasswordNumber";
			this._numberPasswordNumber.HtmlForm = this;
			// 
			// _groupValidatorOptions
			// 
			this._groupValidatorOptions.BindingID = "_groupValidatorOptions";
			this._groupValidatorOptions.HtmlForm = this;
			// 
			// _checkValidatorGoogle
			// 
			this._checkValidatorGoogle.BindingID = "_checkValidatorGoogle";
			this._checkValidatorGoogle.HtmlForm = this;
			// 
			// _checkValidatorYahoo
			// 
			this._checkValidatorYahoo.BindingID = "_checkValidatorYahoo";
			this._checkValidatorYahoo.HtmlForm = this;
			// 
			// _checkValidatorBing
			// 
			this._checkValidatorBing.BindingID = "_checkValidatorBing";
			this._checkValidatorBing.HtmlForm = this;
			// 
			// _checkValidatorDictionaries
			// 
			this._checkValidatorDictionaries.BindingID = "_checkValidatorDictionaries";
			this._checkValidatorDictionaries.HtmlForm = this;
			// 
			// _groupPasswords
			// 
			this._groupPasswords.BindingID = "_groupPasswords";
			this._groupPasswords.HtmlForm = this;
			// 
			// _textPasswords
			// 
			this._textPasswords.BindingID = "_textPasswords";
			this._textPasswords.HtmlForm = this;
			// 
			// _labelPasswordRating
			// 
			this._labelPasswordRating.BindingID = "_labelPasswordRating";
			this._labelPasswordRating.HtmlForm = this;
			// 
			// _buttonGenerate
			// 
			this._buttonGenerate.BindingID = "_buttonGenerate";
			this._buttonGenerate.HtmlForm = this;
			this._buttonGenerate.Clicked += new System.EventHandler<Nabu.Forms.Html.HtmlBehaviorEventArgs>(this.ButtonGenerate_Clicked);
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
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(0, 0);
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Perfect Password Generator";
			this.ResumeLayout(false);

		}

		#endregion

		private System.ComponentModel.BackgroundWorker _backgroundWorker;
		private Nabu.Forms.Html.HtmlComponent _groupGeneratorOptions;
		private Nabu.Forms.Html.HtmlComponent _checkGeneratorLowerCaseLetters;
		private Nabu.Forms.Html.HtmlComponent _checkGeneratorUpperCaseLetters;
		private Nabu.Forms.Html.HtmlComponent _checkGeneratorDigits;
		private Nabu.Forms.Html.HtmlComponent _checkGeneratorPunctuation;
		private Nabu.Forms.Html.HtmlComponent _checkGeneratorPronounceable;
		private Nabu.Forms.Html.HtmlComponent _checkGeneratorUnambiguous;
		private Nabu.Forms.Html.HtmlComponent _checkGeneratorMobileFriendly;
		private Nabu.Forms.Html.HtmlComponent _numberPasswordLength;
		private Nabu.Forms.Html.HtmlComponent _numberPasswordNumber;
		private Nabu.Forms.Html.HtmlComponent _groupValidatorOptions;
		private Nabu.Forms.Html.HtmlComponent _checkValidatorGoogle;
		private Nabu.Forms.Html.HtmlComponent _checkValidatorYahoo;
		private Nabu.Forms.Html.HtmlComponent _checkValidatorBing;
		private Nabu.Forms.Html.HtmlComponent _checkValidatorDictionaries;
		private Nabu.Forms.Html.HtmlComponent _groupPasswords;
		private Nabu.Forms.Html.HtmlComponent _textPasswords;
		private Nabu.Forms.Html.HtmlComponent _labelPasswordRating;
		private Nabu.Forms.Html.HtmlComponent _buttonGenerate;
		private Nabu.Forms.Html.HtmlComponent _labelStatus;





















































	}
}

