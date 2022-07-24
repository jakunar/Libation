using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Dinah.Core;
using FileManager;
using System;

namespace LibationAvalonia.AvaloniaUI.Views.Dialogs
{
	public partial class MessageBoxAlertAdminDialog : DialogWindow
	{
		public string ErrorDescription { get; set; } = "[Error message]\n[Error message]\n[Error message]";
		public string ExceptionMessage { get; set; } = "EXCEPTION MESSAGE!";

		public MessageBoxAlertAdminDialog()
		{
			InitializeComponent();
			ControlToFocusOnShow = this.FindControl<Button>(nameof(OkButton));

			if (Design.IsDesignMode)
				DataContext = this;
		}

		public MessageBoxAlertAdminDialog(string text, string caption, Exception exception) : this()
		{
			ErrorDescription = text;
			this.Title = caption;
			ExceptionMessage = $"{exception.Message}\r\n\r\n{exception.StackTrace}";
			DataContext = this;
		}

		private void GoToGithub_Tapped(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			var url = "https://github.com/rmcrackan/Libation/issues";
			try
			{
				Go.To.Url(url);
			}
			catch
			{
				MessageBox.Show($"Error opening url\r\n{url}", "Error opening url", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void GoToLogs_Tapped(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			LongPath dir = "";
			try
			{
				dir = LibationFileManager.Configuration.Instance.LibationFiles;
			}
			catch { }

			try
			{
				Go.To.Folder(dir.ShortPathName);
			}
			catch
			{
				MessageBox.Show($"Error opening folder\r\n{dir}", "Error opening folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public void OkButton_Clicked(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			SaveAndClose();
		}

	}
}
