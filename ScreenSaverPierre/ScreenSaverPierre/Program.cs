using System;
using System.Windows.Forms;
using System.Globalization;

namespace ScreenSaverPierre
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // Obtenir l'argument de ligne de commande à 2 caractères
                string arg = args[0].ToLower(CultureInfo.InvariantCulture).Trim().Substring(0, 2);
                switch (arg)
                {
                    case "/c":
                        // Afficher la boîte de dialogue Options
                        ShowOptions();
                        break;
                    case "/p":
                        // Ne rien faire pour l'aperçu
                        break;
                    case "/s":
                        // Afficher le formulaire d'écran de veille
                        ShowScreenSaver();
                        break;
                    default:
                        MessageBox.Show("Argument de ligne de commande non valide :" + arg, "Argument de ligne de commande non valide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                // Si aucun argument n'a été passé, afficher l'écran de veille
                ShowScreenSaver();
            }
        }


        static void ShowOptions()
        {
            OptionsForm optionsForm = new OptionsForm();
            Application.Run(optionsForm);
        }

        static void ShowScreenSaver()
        {
            ScreenSaverForm screenSaver = new ScreenSaverForm();
            Application.Run(screenSaver);
        }
    }
}