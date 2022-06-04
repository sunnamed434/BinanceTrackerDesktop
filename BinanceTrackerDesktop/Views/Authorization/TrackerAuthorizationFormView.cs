using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Forms.Authentication;
using BinanceTrackerDesktop.Localizations.Language.Names;
using BinanceTrackerDesktop.Models.User.Authorization;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Tracker.Forms;
using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.User.Status.Detector;
using BinanceTrackerDesktop.Views.Authorization;
using BinanceTrackerDesktop.Views.Authorization.Exceptions;
using BinanceTrackerDesktop.Views.Authorization.Exceptions.ErrorCode;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Forms.Authorization;

public sealed partial class TrackerAuthorizationFormView : Form, IAuthorizationView
{
    private AuthorizationController controller;



    public TrackerAuthorizationFormView()
    {
        InitializeComponent();

        base.FormBorderStyle = FormBorderStyle.FixedSingle;
        base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        base.StartPosition = FormStartPosition.CenterScreen;
        base.Icon = ApplicationDirectories.Resources.ImagesFolder.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();
        base.MaximizeBox = false;

        this.UserCurrenyTextBox.Text = "EUR";
        this.UserKeyTextBox.TextAlign = HorizontalAlignment.Center;
        this.UserSecretTextBox.TextAlign = HorizontalAlignment.Center;
        this.UserCurrenyTextBox.TextAlign = HorizontalAlignment.Center;
        this.LanguageComboBox.DrawMode = DrawMode.OwnerDrawVariable;
        this.LanguageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        foreach (FieldInfo fieldInfo in typeof(LanguageNames).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
        {
            this.LanguageComboBox.Items.Add(fieldInfo.GetValue(null));
        }
        this.LanguageComboBox.SelectedItem = LanguageNames.English;

        this.AuthorizeButton.Click += onAuthorizeButtonClicked;
        this.AddAuthenticatorButton.Click += onAddAuthenticatorButtonClicked;
        this.LanguageComboBox.DrawItem += onLanguageComboBoxDrawItem;
    }



    public void SetController(AuthorizationController controller)
    {
        this.controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }



    private void onAuthorizeButtonClicked(object sender, EventArgs e)
    {
        try
        {
            controller.Authorize(new UserAuthorizationModel(this.UserKeyTextBox.Text, this.UserSecretTextBox.Text, this.UserCurrenyTextBox.Text));
        }
        catch (AuthorizationException ex) when (ex.ErrorCode == AuthorizationErrorCode.Key)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Cannot to authorizate, please check your Key!")
                .BuildToMessageBox();
            return;
        }
        catch (AuthorizationException ex) when (ex.ErrorCode == AuthorizationErrorCode.Secret)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Cannot to authorizate, please check your Secret!")
                .BuildToMessageBox();
            return;
        }
        catch (AuthorizationException ex) when (ex.ErrorCode == AuthorizationErrorCode.Currency)
        {
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Cannot to authorizate, please check your Currency!")
                .BuildToMessageBox();
            return;
        }

        base.Hide();

        TrackerFormView trackerFormView = new TrackerFormView();

        new TrackerController(trackerFormView, new UserStatusDetector(UserClient.SaveDataSystem, UserClient.Wallet).GetStatus());
        trackerFormView.ShowDialog();

        base.Close();

        this.AuthorizeButton.Click -= onAuthorizeButtonClicked;
        this.AddAuthenticatorButton.Click -= onAddAuthenticatorButtonClicked;
    }

    private void onAddAuthenticatorButtonClicked(object sender, EventArgs e)
    {
        AuthenticationFormView authenticationFormView = new AuthenticationFormView();
        new AuthenticationController(authenticationFormView);
        authenticationFormView.ShowDialog();
    }

    private void onLanguageComboBoxDrawItem(object sender, DrawItemEventArgs e)
    {
        const int UnsupportedIndex = -1;
        if (e.Index > UnsupportedIndex)
        {
            Color foregroundColor = e.ForeColor;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (e.State.HasFlag(DrawItemState.Focus) && e.State.HasFlag(DrawItemState.ComboBoxEdit) == false)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
            }
            else
            {
                using (Brush backgbrush = new SolidBrush(Color.WhiteSmoke))
                {
                    e.Graphics.FillRectangle(backgbrush, e.Bounds);
                    foregroundColor = Color.Black;
                }
            }

            using (Brush textbrush = new SolidBrush(foregroundColor))
            {
                e.Graphics.DrawString(this.LanguageComboBox.Items[e.Index].ToString(),
                                      e.Font, textbrush, e.Bounds.Height + 10, e.Bounds.Y,
                                      StringFormat.GenericTypographic);
            }

            e.Graphics.DrawImage(ApplicationDirectories.Resources.ImagesFolder.Flags.GetDirectoryFile(this.LanguageComboBox.Items[e.Index].ToString()).GetImage(),
                                 new Rectangle(e.Bounds.Location,
                                 new Size(e.Bounds.Height - 2, e.Bounds.Height - 2)));
        }
    }
}

public class BinanceAPIKeysCharactersLength
{
    public const int MaxLengthAPIKey = 64;

    public const int MaxLengthSecretKey = 64;
}
