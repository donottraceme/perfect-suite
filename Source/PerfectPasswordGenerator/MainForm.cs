using System.Net;
using Nabu.Forms.Html;
using System.IO;
using System.Text;
using System;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PerfectPasswordGenerator
{
	public partial class MainForm : HtmlForm
	{
		#region Fields

		private PasswordGenerator _passwordGenerator;
		private StringBuilder _passwords;
		private HashSet<string> _dictionary;

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
			catch (Exception e)
			{
				return null;
			}
		}

		private bool ValidateWithSearchEngine(
			string urlTemplate,
			string zeroResultPattern,
			string nonZeroResultPattern,
			string passwordText)
		{
			string content =
				GetUriContent(
					string.Format(
						urlTemplate,
						Uri.EscapeDataString(passwordText)));

			Match zeroMatch = Regex.Match(content, zeroResultPattern);

			if (zeroMatch.Success)
			{
				return true;
			}

			Match nonZeroMatch = Regex.Match(content, nonZeroResultPattern);

			if (nonZeroMatch.Success)
			{
				int result = int.Parse(nonZeroMatch.Groups[1].Value);

				return result < 100;
			}

			return false;
		}

		private bool ValidateWithGoogle(string passwordText)
		{
			return
				ValidateWithSearchEngine(
					"http://www.google.com/search?hl=en&q=%22{0}%22",
					"did not match any documents",
					@"\<p\>&nbsp;Results \<b\>\d+</b> - <b>\d+</b> of <b>(\d+)</b> for",
					passwordText);
		}

		private bool ValidateWithYahoo(string passwordText)
		{
			return
				ValidateWithSearchEngine(
					"http://search.yahoo.com/search?p=%22{0}%22",
					"We did not find results for",
					@"\<span class=""count""\>\<strong id=""resultCount"">(\d+)\</strong\> results for\</span\>",
					passwordText);
		}

		private bool ValidateWithBing(string passwordText)
		{
			return
				ValidateWithSearchEngine(
					"http://www.bing.com/search?q=%22{0}%22",
					"We did not find any results for",
					@"\<span class=""sb_count"" id=""count""\>\d+-\d+ of (\d+) results\</span\>",
					passwordText);
		}

		private void PrepareDictionaries()
		{
			this._dictionary = new HashSet<string>();

			foreach (string dictionaryPath in
				Directory.GetFiles(
					Application.StartupPath + "\\Dictionaries\\",
					"*.txt",
					SearchOption.AllDirectories))
			{
				foreach (string dictionaryWord in File.ReadAllLines(dictionaryPath))
				{
					this._dictionary.Add(dictionaryWord.ToLowerInvariant());
				}
			}
		}

		private bool ValidateWithDictionary(string passwordText)
		{
			return !this._dictionary.Contains(passwordText.ToLowerInvariant());
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
			this._groupGeneratorOptions.IsEnabled = false;
			this._groupValidatorOptions.IsEnabled = false;
			this._buttonGenerate.IsEnabled = false;
			this._backgroundWorker.RunWorkerAsync();
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
			bool validateWithDictionary = (bool)this._checkValidatorDictionaries.Value;

			this._passwords = new StringBuilder();

			if (validateWithDictionary)
			{
				PrepareDictionaries();
			}

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

				if (isPasswordValid && validateWithDictionary)
				{
					this._backgroundWorker.ReportProgress(successfulTries | 0x30000, passwordText);
					isPasswordValid = ValidateWithDictionary(passwordText);
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
							"Password #{0}: Validating '{1}' with Google.",
							(e.ProgressPercentage & 0xFFFF) + 1,
							e.UserState);
					break;
				case 1:
					this._labelStatus.Text =
						string.Format(
							"Password #{0}: Validating '{1}' with Yahoo!.",
							(e.ProgressPercentage & 0xFFFF) + 1,
							e.UserState);
					break;
				case 2:
					this._labelStatus.Text =
						string.Format(
							"Password #{0}: Validating '{1}' with Bing.",
							(e.ProgressPercentage & 0xFFFF) + 1,
							e.UserState);
					break;
				case 3:
					this._labelStatus.Text =
						string.Format(
							"Password #{0}: Validating '{1}' with dictionary ({2} words).",
							(e.ProgressPercentage & 0xFFFF) + 1,
							e.UserState,
							this._dictionary.Count);
					break;
			}

			base.Refresh();
			Application.DoEvents();
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this._textPasswords.Value = this._passwords.ToString();
			this._groupGeneratorOptions.IsEnabled = true;
			this._groupValidatorOptions.IsEnabled = true;
			this._buttonGenerate.IsEnabled = true;
			this._labelStatus.Text = string.Empty;
		}

		#endregion

		#endregion
	}
}
