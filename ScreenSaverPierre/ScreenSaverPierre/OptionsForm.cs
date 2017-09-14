using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using ScreenSaverPierre.Rss;


namespace ScreenSaverPierre
{
    partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();

            // Charge les zones de texte à partir des paramètres actuels
            try
            {
                backgroundImageFolderTextBox.Text = Properties.Settings.Default.BackgroundImagePath;
                rssFeedTextBox.Text = Properties.Settings.Default.RssFeedUri;
            }
            catch
            {
                MessageBox.Show("Un problème s'est produit lors de la lecture des paramètres de l'écran de veille.");
            }
        }

        // Met à jour le bouton Appliquer de manière à l'activer uniquement si des modifications 
        // ont été effectuées depuis la dernière utilisation du bouton
        private void UpdateApply()
        {
            if (Properties.Settings.Default.BackgroundImagePath != backgroundImageFolderTextBox.Text
                  || Properties.Settings.Default.RssFeedUri != rssFeedTextBox.Text)
                applyButton.Enabled = true;
            else
                applyButton.Enabled = false;
        }

        // Applique toutes les modifications effectuées depuis la dernière utilisation du bouton Appliquer
        private void ApplyChanges()
        {
            Properties.Settings.Default.BackgroundImagePath = backgroundImageFolderTextBox.Text;
            Properties.Settings.Default.RssFeedUri = rssFeedTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ApplyChanges();
            }
            catch (ConfigurationException)
            {
                MessageBox.Show("Impossible d'enregistrer vos paramètres. Assurez-vous que le fichier .config se trouve dans le même répertoire que l'écran de veille.", "Impossible d'enregistrer les paramètres", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyChanges();
            applyButton.Enabled = false;
        }

        // Vérifiez si l'utilisateur a fourni des points URI à un flux RSS valide
        private void validateButton_Click(object sender, EventArgs e)
        {
            try
            {
                RssFeed.FromUri(rssFeedTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Flux RSS non valide.", "Flux RSS non valide.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Flux RSS valide.", "Flux RSS valide.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            // Affiche une boîte de dialogue Ouvrir permettant de choisir une image

            DialogResult result = backgroundImageFolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                backgroundImageFolderTextBox.Text = backgroundImageFolderBrowser.SelectedPath;
                UpdateApply();
            }
        }

        private void rssFeedTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateApply();
        }

        private void backgroundImageFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateApply();
        }
    }
}