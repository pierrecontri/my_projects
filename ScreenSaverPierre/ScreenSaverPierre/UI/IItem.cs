using System;
using System.Collections.Generic;
using System.Text;

namespace ScreenSaverPierre.UI
{
    /// <summary>
    /// Généralisation d'un élément avec une <c>Description</c> et un <c>Titre</c>.
    /// Toute implémentation de IItem peut être rendue à l'aide des types ItemListView et ItemDescriptionView.
    /// </summary>
    public interface IItem
    {
        string Description { get; }
        string Title { get; }
    }
}
