using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using System.IO;

namespace Lift
{
    public interface IJsonLoader
    {
        string LoadJson();
    }
}
