using System.Net;
using Nabu.Forms.Html;
using System.IO;
using System.Text;
using System;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace PerfectPasswordGenerator
{
	public partial class MainForm : HtmlForm
	{
		#region Fields

		private PasswordGenerator _passwordGenerator;
		private StringBuilder _passwords;

		#endregion

		#region Methods

		#region Utility

		private void PrepareGenerator()
		{
			this._passwordGenerator =
				new PasswordGenerator(
					(bool)this._checkGeneratorLowerCaseLetters.Value,
					(bool)this._checkGeneratorUpperCaseLetters.Value,
					(bool)this._checkGeneratorDigits.Value,
					(bool)this._checkGeneratorPunctuation.Value,
					(bool)this._checkGeneratorPronounceable.Value,
					(bool)this._checkGeneratorUnambiguous.Value,
					(bool)this._checkGeneratorMobileFriendly.Value);

			int passwordLength = (int)this._numberPasswordLength.Value;
			double passwordRating = this._passwordGenerator.GetStrengthRating(passwordLength);

			this._labelPasswordRating.Value = passwordRating.ToString("0.00");

			if (passwordRating < 1)
			{
				this._buttonGenerate.IsEnabled = false;
			}
			else
			{
				this._buttonGenerate.IsEnabled = true;
			}
		}

		private string GetUriContent(string uri)
		{
			try
			{
				HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
				HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

				using (Stream httpResponseStream = httpResponse.GetResponseStream())
				{
					using (TextReader httpResponseReader = new StreamReader(httpResponseStream))
					{
						return httpResponseReader.ReadToEnd();
					}
				}
			}
			catch (Exception exception)
			{
				return string.Empty;
			}
		}

		private bool ValidateWithGoogle(string passwordText)
		{
			string content =
				GetUriContent(
					string.Format(
						"http://www.google.com/search?hl=en&q={0}",
						Uri.EscapeDataString(passwordText)));

			return content.IndexOf("did not match any documents") != -1;
		}

		private bool ValidateWithYahoo(string passwordText)
		{
			string content =
				GetUriContent(
					string.Format(
						"http://search.yahoo.com/search?p={0}",
						Uri.EscapeDataString(passwordText)));

			return content.IndexOf("We did not find results for") != -1;
		}

		private bool ValidateWithBing(string passwordText)
		{
			string content =
				GetUriContent(
					string.Format(
						"http://search.yahoo.com/search?p={0}",
						Uri.EscapeDataString(passwordText)));

			return content.IndexOf("We did not find any results for") != -1;
		}

		private bool ValidateWithWikipedia(string passwordText)
		{
			string content =
				GetUriContent(
					string.Format(
						"http://en.wikipedia.org/wiki/Special:Search?search={0}",
						Uri.EscapeDataString(passwordText)));

			return content.IndexOf("There were no results matching the query") != -1;
		}

		#endregion

		#region Form

		public MainForm()
		{
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			PrepareGenerator();

			base.OnShown(e);
		}

		#endregion

		#region Controls

		private void GeneratorOptions_Changed(object sender, HtmlBehaviorEventArgs e)
		{
			PrepareGenerator();
		}

		private void ButtonGenerate_Clicked(object sender, HtmlBehaviorEventArgs e)
		{
			this._backgroundWorker.RunWorkerAsync();
			this._buttonGenerate.IsEnabled = false;
		}

		#endregion

		#region Worker

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			int passwordLength = (int)this._numberPasswordLength.Value;
			int passwordNumber = (int)this._numberPasswordNumber.Value;
			int totalTries = 0;
			int successfulTries = 0;
			bool validateWithGoogle = (bool)this._checkValidatorGoogle.Value;
			bool validateWithYahoo = (bool)this._checkValidatorYahoo.Value;
			bool validateWithBing = (bool)this._checkValidatorBing.Value;
			bool validateWithWikipedia = (bool)this._checkValidatorWikipedia.Value;

			this._passwords = new StringBuilder();

			while (
				(successfulTries < passwordNumber) &&
				(totalTries < 2 * passwordNumber))
			{
				totalTries++;

				string passwordText = this._passwordGenerator.Generate(passwordLength);
				bool isPasswordValid = true;

				if (isPasswordValid && validateWithGoogle)
				{
					this._backgroundWorker.ReportProgress(successfulTries | 0x00000, passwordText);
					isPasswordValid = ValidateWithGoogle(passwordText);
				}

				if (isPasswordValid && validateWithYahoo)
				{
					this._backgroundWorker.ReportProgress(successfulTries | 0x10000, passwordText);
					isPasswordValid = ValidateWithYahoo(passwordText);
				}

				if (isPasswordValid && validateWithBing)
				{
					this._backgroundWorker.ReportProgress(successfulTries | 0x20000, passwordText);
					isPasswordValid = ValidateWithBing(passwordText);
				}

				if (isPasswordValid && validateWithWikipedia)
				{
					this._backgroundWorker.ReportProgress(successfulTries | 0x30000, passwordText);
					isPasswordValid = ValidateWithWikipedia(passwordText);
				}

				if (isPasswordValid)
				{
					this._passwords.AppendLine(passwordText);
					successfulTries++;
				}
			}
		}

		private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			switch (e.ProgressPercentage >> 16)
			{
				case 0:
					this._labelStatus.Text =
						string.Format(
							"{0}: Validating '{1}' with Google.",
							e.ProgressPercentage & 0xFFFF,
							e.UserState);
					break;
				case 1:
					this._labelStatus.Text =
						string.Format(
							"{0}: Validating '{1}' with Yahoo!",
							e.ProgressPercentage & 0xFFFF,
							e.UserState);
					break;
				case 2:
					this._labelStatus.Text =
						string.Format(
							"{0}: Validating '{1}' with Bing.",
							e.ProgressPercentage & 0xFFFF,
							e.UserState);
					break;
				case 3:
					this._labelStatus.Text =
						string.Format(
							"{0}: Validating '{1}' with Wikipedia.",
							e.ProgressPercentage & 0xFFFF,
							e.UserState);
					break;
			}
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._textPasswords.Value = this._passwords.ToString();
			this._buttonGenerate.IsEnabled = true;
			this._labelStatus.Text = string.Empty;
		}

		#endregion

		#endregion
	}
}
