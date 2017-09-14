using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ScreenSaverPierre.UI
{
    /// <summary>
    /// Encapsule le rendu d'une liste d'éléments. La description de chaque élément est affichée dans une liste et un élément est sélectionné.
    /// </summary>
    /// <typeparam name="T">Type d'élément dessiné par ce ItemListView.</typeparam>
    public class ItemListView<T> : IDisposable where T : IItem
    {
        private const float percentOfArticleDisplayBoxToFillWithText = 0.5f;
        private const float percentOfFontHeightForSelectionBox = 1.5f;
        private const int padding = 20;

        // Où dessiner
        private Point location;
        private Size size;

        private string title;
        private Font itemFont;
        private Font titleFont;
        private Color backColor;
        private Color borderColor;
        private Color foreColor;
        private Color titleBackColor;
        private Color titleForeColor;
        private Color selectedForeColor;
        private Color selectedBackColor;
        private float itemFontHeight;
        // Index de l'élément actuellement sélectionné
        private int selectedIndex = 0;
        // Liste des éléments à dessiner
        private IList<T> items;
        // Nombre maximum d'articles affichés
        private int maxItemsToShow;
        // Nombre minimum d'articles affichés
        // Si le nombre d'éléments dans le canal RSS est inférieur,
        // l'affichage comportera un espace
        private int minItemsToShow;

        private int NumArticles { get { return Math.Min(items.Count, maxItemsToShow); } }
        private int NumArticleRows { get { return Math.Max(NumArticles, minItemsToShow); } }

        public Point Location { get { return location; } set { location = value; } }
        public Size Size { get { return size; } set { size = value; } }

        public Color ForeColor { get { return foreColor; } set { foreColor = value; } }
        public Color BackColor { get { return backColor; } set { backColor = value; } }
        public Color BorderColor { get { return borderColor; } set { borderColor = value; } }
        public Color TitleForeColor { get { return titleForeColor; } set { titleForeColor = value; } }
        public Color TitleBackColor { get { return titleBackColor; } set { titleBackColor = value; } }
        public Color SelectedForeColor { get { return selectedForeColor; } set { selectedForeColor = value; } }
        public Color SelectedBackColor { get { return selectedBackColor; } set { selectedBackColor = value; } }
        public int MaxItemsToShow { get { return maxItemsToShow; } set { maxItemsToShow = value; } }
        public int MinItemsToShow { get { return minItemsToShow; } set { minItemsToShow = value; } }
        public int SelectedIndex { get { return selectedIndex; } }
        public T SelectedItem { get { return items[selectedIndex]; } }

        public int RowHeight
        {
            get
            {
                // Il existe une ligne pour chaque élément plus 2 lignes pour le titre.
                return size.Height / (NumArticleRows + 2);
            }
        }

        public Font ItemFont
        {
            get
            {
                // Choisissez une police pour chaque titre d'élément figurant dans tous les numItems 
                // (plus de l'espace supplémentaire pour le titre) dans le contrôle 
                itemFontHeight = (float)(percentOfArticleDisplayBoxToFillWithText * RowHeight);
                if (itemFont == null || itemFont.Size != itemFontHeight)
                {
                    itemFont = new Font("Microsoft Sans Serif", itemFontHeight, GraphicsUnit.Pixel);
                }
                return itemFont;
            }
        }

        public Font TitleFont
        {
            get
            {
                // Choisissez une police pour le texte du titre.
                // Cette police sera deux fois plus grande que ItemFont
                float titleFontHeight = (float)(percentOfArticleDisplayBoxToFillWithText * 2 * RowHeight);
                if (titleFont == null || titleFont.Size != titleFontHeight)
                {
                    titleFont = new Font("Microsoft Sans Serif", titleFontHeight, GraphicsUnit.Pixel);
                }
                return titleFont;
            }
        }

        public void NextArticle()
        {
            if (selectedIndex < NumArticles - 1)
                selectedIndex++;
            else
                selectedIndex = 0;
        }

        public void PreviousArticle()
        {
            if (selectedIndex > 0)
                selectedIndex--;
            else
                selectedIndex = NumArticles - 1;
        }

        public ItemListView(string title, IList<T> items)
        {
            if (items == null)
                throw new ArgumentException("Les éléments ne peuvent pas être null", "items");

            this.items = items;
            this.title = title;
        }

        public void Paint(PaintEventArgs args)
        {
            Graphics g = args.Graphics;

            // Paramètres permettant d'améliorer le dessin du texte
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            DrawBackground(g);

            // Trace la description de chaque article
            for (int index = 0; index < items.Count && index < maxItemsToShow; index++)
            {
                DrawItemTitle(g, index);
            }

            // Trace le texte du titre
            DrawTitle(g);
        }

        /// <summary>
        /// Trace une zone et une bordure sur lesquelles dessiner le texte des éléments.
        /// </summary>
        /// <param name="g">Objet Graphics sur lequel dessiner</param>
        private void DrawBackground(Graphics g)
        {
            using (Brush backBrush = new SolidBrush(BackColor))
            using (Pen borderPen = new Pen(BorderColor, 4))
            {
                g.FillRectangle(backBrush, new Rectangle(Location.X + 4, Location.Y + 4, Size.Width - 8, Size.Height - 8));
                g.DrawRectangle(borderPen, new Rectangle(Location, Size));
            }
        }

        /// <summary>
        /// Trace le titre de l'élément avec l'index donné.
        /// </summary>
        /// <param name="g">Objet Graphics sur lequel dessiner</param>
        /// <param name="index">Index de l'élément dans la liste</param>
        private void DrawItemTitle(Graphics g, int index)
        {
            // Définissez la mise en forme et la présentation
            StringFormat stringFormat = new StringFormat(StringFormatFlags.LineLimit);
            stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            Rectangle articleRect = new Rectangle(Location.X + padding, Location.Y + (int)(index * RowHeight) + padding, Size.Width - (2 * padding), (int)(percentOfFontHeightForSelectionBox * itemFontHeight));

            // Sélectionne la couleur et trace la bordure si l'index actuel est sélectionné
            Color textBrushColor = ForeColor;
            if (index == SelectedIndex)
            {
                textBrushColor = SelectedForeColor;
                using (Brush backBrush = new SolidBrush(SelectedBackColor))
                {
                    g.FillRectangle(backBrush, articleRect);
                }
            }

            // Trace le titre de l'élément
            string textToDraw = items[index].Title;
            using (Brush textBrush = new SolidBrush(textBrushColor))
            {
                g.DrawString(textToDraw, ItemFont, textBrush, articleRect, stringFormat);
            }
        }

        /// <summary>
        /// Trace une barre de titre.
        /// </summary>
        /// <param name="g">Objet Graphics sur lequel dessiner</param>
        private void DrawTitle(Graphics g)
        {
            Point titleLocation = new Point(Location.X + padding, Location.Y + Size.Height - (RowHeight) - padding);
            Size titleSize = new Size(Size.Width - (2 * padding), 2 * RowHeight);
            Rectangle titleRectangle = new Rectangle(titleLocation, titleSize);

            // Trace la zone de titre et la zone d'élément sélectionnée
            using (Brush titleBackBrush = new SolidBrush(TitleBackColor))
            {
                g.FillRectangle(titleBackBrush, titleRectangle);
            }

            // Trace le texte du titre
            StringFormat titleFormat = new StringFormat(StringFormatFlags.LineLimit);
            titleFormat.Alignment = StringAlignment.Far;
            titleFormat.Trimming = StringTrimming.EllipsisCharacter;
            using (Brush titleBrush = new SolidBrush(TitleForeColor))
            {
                g.DrawString(title, titleFont, titleBrush, titleRectangle, titleFormat);
            }
        }

        /// <summary>
        /// Supprime tous les champs pouvant être supprimés
        /// </summary>
        public void Dispose()
        {
            if (itemFont != null)
                itemFont.Dispose();
            if (titleFont != null)
                titleFont.Dispose();
        }
    }
}