using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScreenSaverPierre.UI;
using ScreenSaverPierre.Rss;

namespace ScreenSaverPierre
{
    partial class ScreenSaverForm : Form
    {
        // RssFeed à partir duquel afficher les articles
        private RssFeed rssFeed;

        // Objets utilisés pour afficher le contenu RSS
        private ItemListView<RssItem> rssView;
        private ItemDescriptionView<RssItem> rssDescriptionView;

        // Images affichées en arrière-plan
        private List<Image> backgroundImages;
        private int currentImageIndex;

        // Effectuer le suivi de l'activation de l'écran de veille.
        private bool isActive = false;

        // Effectuer le suivi de la position de la souris
        private Point mouseLocation;

        private List<string> imageExtensions = new List<string>(new string[] { "*.bmp", "*.gif", "*.png", "*.jpg", "*.jpeg" });

        public ScreenSaverForm()
        {
            InitializeComponent();

            SetupScreenSaver();
            LoadBackgroundImage();
            LoadRssFeed();

            // Initialisez ItemListView pour afficher la liste des éléments figurant dans 
            // RssItem. Elle est placée sur le côté gauche de l'écran.            
            rssView = new ItemListView<RssItem>(rssFeed.MainChannel.Title, rssFeed.MainChannel.Items);
            InitializeRssView();

            // Initialisez ItemDescriptionView pour afficher la description de 
            // RssItem. Elle est placée sur le côté droit de l'écran.
            rssDescriptionView = new ItemDescriptionView<RssItem>();
            InitializeRssDescriptionView();
        }


        /// <summary>
        /// Définissez le formulaire en tant qu'écran de veille plein écran.
        /// </summary>
        private void SetupScreenSaver()
        {
            // Utilise le mécanisme de double tampon pour améliorer les performances de dessin
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            // Capture la souris
            this.Capture = true;

            // Met l'application en mode plein écran et masque la souris
            Cursor.Hide();
            Bounds = Screen.PrimaryScreen.Bounds;
            WindowState = FormWindowState.Maximized;
            ShowInTaskbar = false;
            DoubleBuffered = true;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void LoadBackgroundImage()
        {
            // Initialise les images d'arrière-plan.
            backgroundImages = new List<Image>();
            currentImageIndex = 0;

            if (Directory.Exists(Properties.Settings.Default.BackgroundImagePath))
            {
                try
                {
                    // Charge les images fournies par les utilisateurs.
                    LoadImagesFromFolder();
                }
                catch
                {
                    // En cas d'échec, charge les images par défaut.
                    LoadDefaultBackgroundImages();
                }
            }

            // Si aucune image n'est chargée, charge les images par défaut
            if (backgroundImages.Count == 0)
            {
                LoadDefaultBackgroundImages();
            }
        }

        private void LoadImagesFromFolder()
        {
            DirectoryInfo backgroundImageDir = new DirectoryInfo(Properties.Settings.Default.BackgroundImagePath);
            // Pour chaque extension d'image (.jpg, .bmp, etc.)
            foreach (string imageExtension in imageExtensions)
            {
                // Pour chaque fichier du répertoire fourni par l'utilisateur
                foreach (FileInfo file in backgroundImageDir.GetFiles(imageExtension))
                {
                    // Chargez l'image
                    try
                    {
                        Image image = Image.FromFile(file.FullName);
                        backgroundImages.Add(image);
                    }
                    catch (OutOfMemoryException)
                    {
                        // Si l'image ne peut pas être chargée, continuez.
                        continue;
                    }
                }
            }
        }


        private void LoadDefaultBackgroundImages()
        {
            // Si, pour une raison quelconque, il est impossible de charger les images d'arrière-plan
            // utilisez l'image enregistrée dans les ressources 
            backgroundImages.Add(Properties.Resources.SSaverBackground);
            backgroundImages.Add(Properties.Resources.SSaverBackground2);
        }

        private void LoadRssFeed()
        {
            try
            {
                // Obtenez-la à partir des paramètres utilisateur
                rssFeed = RssFeed.FromUri(Properties.Settings.Default.RssFeedUri);
            }
            catch
            {
                // En cas de problème lors du chargement du RSS, chargez un flux RSS de message d'erreur
                rssFeed = RssFeed.FromText(Properties.Resources.DefaultRSSText);
            }
        }

        /// <summary>
        /// Initialisez les propriétés d'affichage de rssView.
        /// </summary>
        private void InitializeRssView()
        {
            rssView.BackColor = Color.FromArgb(120, 240, 234, 232);
            rssView.BorderColor = Color.White;
            rssView.ForeColor = Color.FromArgb(255, 40, 40, 40);
            rssView.SelectedBackColor = Color.FromArgb(200, 105, 61, 76);
            rssView.SelectedForeColor = Color.FromArgb(255, 204, 184, 163);
            rssView.TitleBackColor = Color.Empty;
            rssView.TitleForeColor = Color.FromArgb(255, 240, 234, 232);
            rssView.MaxItemsToShow = 20;
            rssView.MinItemsToShow = 15;
            rssView.Location = new Point(Width / 10, Height / 10);
            rssView.Size = new Size(Width / 2, Height / 2);
        }

        /// <summary>
        /// Initialisez les propriétés d'affichage de rssDescriptionView.
        /// </summary>
        private void InitializeRssDescriptionView()
        {
            rssDescriptionView.DisplayItem = rssView.SelectedItem;
            rssDescriptionView.ForeColor = Color.FromArgb(255, 240, 234, 232);
            rssDescriptionView.TitleFont = rssView.TitleFont;
            rssDescriptionView.LineColor = Color.FromArgb(120, 240, 234, 232);
            rssDescriptionView.LineWidth = 2f;
            rssDescriptionView.FadeTimer.Tick += new EventHandler(FadeTimer_Tick);
            rssDescriptionView.FadeTimer.Interval = 40;
            rssDescriptionView.Location = new Point(3 * Width / 4, Height / 3);
            rssDescriptionView.Size = new Size(Width / 4, Height / 2);
            rssDescriptionView.FadingComplete += new EventHandler(rssItemView_FadingComplete);
        }

        private void ScreenSaverForm_MouseMove(object sender, MouseEventArgs e)
        {
            // Définissez IsActive et MouseLocation uniquement lors du premier appel de cet événement.
            if (!isActive)
            {
                mouseLocation = MousePosition;
                isActive = true;
            }
            else
            {
                // Si la souris a été considérablement déplacée depuis le premier appel, la fermeture est nécessaire.
                if ((Math.Abs(MousePosition.X - mouseLocation.X) > 10) ||
                    (Math.Abs(MousePosition.Y - mouseLocation.Y) > 10))
                {
                    Close();
                }
            }
        }

        private void ScreenSaverForm_KeyDown(object sender, KeyEventArgs e)
        {
            Close();

        }

        private void ScreenSaverForm_MouseDown(object sender, MouseEventArgs e)
        {
            Close();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Étirez l'image d'arrière-plan actuelle de manière à remplir la totalité de l'écran
            e.Graphics.DrawImage(backgroundImages[currentImageIndex], 0, 0, Size.Width, Size.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            rssView.Paint(e);
            rssDescriptionView.Paint(e);
        }

        private void backgroundChangeTimerTick(object sender, EventArgs e)
        {
            // Remplacez l'image d'arrière-plan par l'image suivante.
            currentImageIndex = (currentImageIndex + 1) % backgroundImages.Count;
        }

        void FadeTimer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        void rssItemView_FadingComplete(object sender, EventArgs e)
        {
            rssView.NextArticle();
            rssDescriptionView.DisplayItem = rssView.SelectedItem;
        }
    }
}
