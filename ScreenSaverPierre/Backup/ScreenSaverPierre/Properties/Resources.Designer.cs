﻿//------------------------------------------------------------------------------
// <autogenerated>
//     Ce code a été généré par un outil.
//     Version du runtime :2.0.50727.832
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </autogenerated>
//------------------------------------------------------------------------------

[assembly: global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "ScreenSaverPierre.Properties.Resources.get_ResourceManager():System.Resources.ResourceManager")]
[assembly: global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "ScreenSaverPierre.Properties.Resources.get_Culture():System.Globalization.CultureInfo")]
[assembly: global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "ScreenSaverPierre.Properties.Resources.set_Culture(System.Globalization.CultureInfo):Void")]

namespace ScreenSaverPierre.Properties
{


    /// <summary>
    ///   Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
    /// </summary>
    // Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
    // à l'aide d'un outil, tel que ResGen ou Visual Studio.
    // Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
    // avec l'option /str ou régénérez votre projet VS.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources()
        {
        }

        /// <summary>
        ///   Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if ((resourceMan == null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ScreenSaverPierre.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Remplace la propriété CurrentUICulture du thread actuel pour toutes
        ///   les recherches de ressources à l'aide de cette classe de ressource fortement typée.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Recherche une chaîne localisée semblable à &lt;rss version=&quot;2.0&quot;&gt;&lt;channel&gt;&lt;title&gt;Flux RSS non disponible&lt;/title&gt;&lt;link&gt;http://go.microsoft.com/fwlink/?LinkId=49535&lt;/link&gt;&lt;description&gt;Le flux RSS n'a pas pu être chargé.&lt;/description&gt;&lt;language&gt;en-us&lt;/language&gt;&lt;ttl&gt;1440&lt;/ttl&gt;&lt;item&gt;&lt;title&gt;Vous n'êtes peut-être pas connecté à Internet.&lt;/title&gt;&lt;description&gt;Si vous utilisez un flux RSS sur Internet, vérifiez votre connexion Internet.&lt;/description&gt;&lt;link&gt;http://go.microsoft.com/fwlink/?LinkId=49535&lt;/link&gt;&lt;/item&gt;&lt;item&gt;&lt;title&gt;Sélectionnez un autre flux RSS.&lt;/title&gt;&lt;des[rest of String was truncated]&quot;;.
        /// </summary>
        internal static string DefaultRSSText
        {
            get
            {
                return ResourceManager.GetString("DefaultRSSText", resourceCulture);
            }
        }

        internal static System.Drawing.Bitmap SSaverBackground
        {
            get
            {
                return ((System.Drawing.Bitmap)(ResourceManager.GetObject("SSaverBackground", resourceCulture)));
            }
        }

        internal static System.Drawing.Bitmap SSaverBackground2
        {
            get
            {
                return ((System.Drawing.Bitmap)(ResourceManager.GetObject("SSaverBackground2", resourceCulture)));
            }
        }
    }
}
