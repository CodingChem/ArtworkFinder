using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkFinder.Library.Events;

internal class SearchCompletedEventArgs : EventArgs
{
    public bool ArtworkFound { get; private set; }
    public string Fpack { get; private set; }
    public SearchCompletedEventArgs(bool artworkFound, string fpack)
    {
        ArtworkFound = artworkFound;
        Fpack = fpack;
    }
}
