﻿using System.Windows.Media;
using System.Windows;
using System.Threading;
using KEPAVerwaltungWPF.Views.Windows;
using NetSparkleUpdater;
using NetSparkleUpdater.Interfaces;
using NetSparkleUpdater.Properties;
using KEPAVerwaltungWPF.ViewModels.NetSparkleUpdaterCustomUI;
using KEPAVerwaltungWPF.Views.Windows.NetSparkleUpdaterCustomUI;

namespace KEPAVerwaltungWPF.Helper;

public class CustomUIFactory : IUIFactory
{
    /// <summary>
    /// Icon used on various windows shown by NetSparkleUpdater
    /// </summary>
    protected ImageSource? _applicationIcon = null;

    /// <summary>
    /// Create a new UIFactory for WPF applications
    /// </summary>
    public CustomUIFactory()
    {
        HideReleaseNotes = false;
        HideRemindMeLaterButton = false;
        HideSkipButton = false;
        ReleaseNotesHTMLTemplate = "";
        AdditionalReleaseNotesHeaderHTML = "";
        UseStaticUpdateWindowBackgroundColor = true;
        UpdateWindowGridBackgroundBrush = (System.Windows.Media.Brush)(new BrushConverter().ConvertFrom("#EEEEEE")
                                                                       ?? new SolidColorBrush(
                                                                           System.Windows.Media.Color.FromArgb(255, 0,
                                                                               0, 0)));
        UpdateWindowGridBackgroundBrush.Freeze();
    }

    /// <summary>
    /// Create a new UIFactory for WPF applications with the given
    /// application icon to show in all update windows
    /// </summary>
    /// <param name="applicationIcon">the <see cref="ImageSource"/> to show in all windows</param>
    public CustomUIFactory(ImageSource applicationIcon) : this()
    {
        _applicationIcon = applicationIcon;
        _applicationIcon?.Freeze();
    }

    /// <inheritdoc/>
    public bool HideReleaseNotes { get; set; }

    /// <inheritdoc/>
    public bool HideSkipButton { get; set; }

    /// <inheritdoc/>
    public bool HideRemindMeLaterButton { get; set; }

    /// <inheritdoc/>
    public string? ReleaseNotesHTMLTemplate { get; set; }

    /// <inheritdoc/>
    public string? AdditionalReleaseNotesHeaderHTML { get; set; }

    /// <summary>
    /// The DateTime.ToString() format used when formatting dates to show in the release notes
    /// header. NetSparkle is not responsible for what happens if you send a bad format! :)
    /// </summary>
    public string ReleaseNotesDateTimeFormat { get; set; } = "D";

    /// <summary>
    /// <para>
    /// Easily set / override the ReleaseNotesGrabber used by the <see cref="UpdateAvailableWindowViewModel"/>.
    /// Note that this will NOT automatically use the <see cref="UIFactory"/> ReleaseNotesHTMLTemplate,
    /// AdditionalReleaseNotesHeaderHTML, and ReleaseNotesDateTimeFormat that you may have set on 
    /// the UIFactory - you must set these on this manual override yourself!
    /// </para>
    /// <para>
    /// Use this if you want to easily override the ReleaseNotesGrabber with your own subclass.
    /// </para>
    /// </summary>
    public ReleaseNotesGrabber? ReleaseNotesGrabberOverride { get; set; } = null;

    /// <summary>
    /// Whether or not a hardcoded window background color is set on the updates window.
    /// Defaults to true.
    /// </summary>
    public bool UseStaticUpdateWindowBackgroundColor { get; set; }

    /// <summary>
    /// Brush for the background of the main grid on the update (change log) window
    /// </summary>
    public System.Windows.Media.Brush UpdateWindowGridBackgroundBrush { get; set; }

    /// <summary>
    /// A delegate for handling windows that are created by a <see cref="UIFactory"/>
    /// </summary>
    /// <param name="window"><see cref="Window"/> that has been created and initialized (with view model, if applicable)</param>
    /// <param name="factory"><see cref="UIFactory"/> that created the given <see cref="Window"/></param>
    public delegate void WindowHandler(Window window, CustomUIFactory factory);

    /// <summary>
    /// Set this property to manually do any other setup on a <see cref="Window"/> after it has been created by the <see cref="UIFactory"/>.
    /// Can be used to tweak view models, change styles on the <see cref="Window"/>, etc.
    /// </summary>
    public WindowHandler? ProcessWindowAfterInit { get; set; }

    /// <inheritdoc/>
    public virtual IUpdateAvailable CreateUpdateAvailableWindow(List<AppCastItem> updates,
        ISignatureVerifier? signatureVerifier,
        string currentVersion = "", string appName = "the application", bool isUpdateAlreadyDownloaded = false)
    {
        var viewModel = new UpdateAvailableWindowViewModel();
        var window = new UpdateAvailableWindow(viewModel)
        {
            Icon = _applicationIcon
        };
        if (UseStaticUpdateWindowBackgroundColor)
        {
            window.ChangeMainGridBackgroundColor(UpdateWindowGridBackgroundBrush);
        }

        if (HideReleaseNotes)
        {
            (window as IUpdateAvailable).HideReleaseNotes();
        }

        if (HideSkipButton)
        {
            (window as IUpdateAvailable).HideSkipButton();
        }

        if (HideRemindMeLaterButton)
        {
            (window as IUpdateAvailable).HideRemindMeLaterButton();
        }

        if (ReleaseNotesGrabberOverride != null)
        {
            viewModel.ReleaseNotesGrabber = ReleaseNotesGrabberOverride;
        }

        viewModel.Initialize(updates, signatureVerifier, isUpdateAlreadyDownloaded, ReleaseNotesHTMLTemplate ?? "",
            AdditionalReleaseNotesHeaderHTML ?? "", ReleaseNotesDateTimeFormat, appName, currentVersion);
        ProcessWindowAfterInit?.Invoke(window, this);
        return window;
    }

    /// <inheritdoc/>
    public virtual IDownloadProgress CreateProgressWindow(string downloadTitle, string actionButtonTitleAfterDownload)
    {
        var viewModel = new DownloadProgressWindowViewModel()
        {
            DownloadingTitle = downloadTitle,
            //ActionButtonTitle = actionButtonTitleAfterDownload
            ActionButtonTitle = "Installieren und Neustart"
        };
        var window = new DownloadProgressWindow(viewModel)
        {
            Icon = _applicationIcon
        };
        ProcessWindowAfterInit?.Invoke(window, this);
        return window;
    }

    /// <inheritdoc/>
    public virtual ICheckingForUpdates ShowCheckingForUpdates()
    {
        var window = new CheckingForUpdatesWindow() { Icon = _applicationIcon };
        ProcessWindowAfterInit?.Invoke(window, this);
        return window;
    }

    /// <inheritdoc/>
    public virtual void ShowUnknownInstallerFormatMessage(string downloadFileName)
    {
        ShowMessage(Resources.DefaultUIFactory_MessageTitle,
            string.Format(Resources.DefaultUIFactory_ShowUnknownInstallerFormatMessageText, downloadFileName));
    }

    /// <inheritdoc/>
    public virtual void ShowVersionIsUpToDate()
    {
        //ShowMessage(Resources.DefaultUIFactory_MessageTitle, Resources.DefaultUIFactory_ShowVersionIsUpToDateMessage);
    }

    /// <inheritdoc/>
    public virtual void ShowVersionIsSkippedByUserRequest()
    {
        //ShowMessage(Resources.DefaultUIFactory_MessageTitle,
        //    Resources.DefaultUIFactory_ShowVersionIsSkippedByUserRequestMessage);
    }

    /// <inheritdoc/>
    public virtual void ShowCannotDownloadAppcast(string? appcastUrl)
    {
        ShowMessage(Resources.DefaultUIFactory_ErrorTitle, Resources.DefaultUIFactory_ShowCannotDownloadAppcastMessage);
    }

    /// <inheritdoc/>
    public virtual bool CanShowToastMessages()
    {
        return true;
    }

    /// <inheritdoc/>
    public virtual void ShowToast(Action clickHandler)
    {
        Thread thread = new Thread(() =>
        {
            var toast = new ToastNotification()
            {
                ClickAction = clickHandler,
                Icon = _applicationIcon
            };
            try
            {
                ProcessWindowAfterInit?.Invoke(toast, this);
                toast.Show(Resources.DefaultUIFactory_ToastMessage, Resources.DefaultUIFactory_ToastCallToAction, 5);
                System.Windows.Threading.Dispatcher.Run();
            }
            catch (ThreadAbortException)
            {
                toast.Dispatcher.InvokeShutdown();
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }

    /// <inheritdoc/>
    public virtual void ShowDownloadErrorMessage(string message, string? appcastUrl)
    {
        ShowMessage(Resources.DefaultUIFactory_ErrorTitle,
            string.Format(Resources.DefaultUIFactory_ShowDownloadErrorMessage, message));
    }

    private void ShowMessage(string title, string message)
    {
        var messageWindow = new MessageNotificationWindow(new MessageNotificationWindowViewModel(message))
        {
            Title = title,
            Icon = _applicationIcon
        };
        messageWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        ProcessWindowAfterInit?.Invoke(messageWindow, this);
        messageWindow.ShowDialog();
    }

    /// <inheritdoc/>
    public void Shutdown()
    {
        System.Windows.Application.Current.Shutdown();
    }
}