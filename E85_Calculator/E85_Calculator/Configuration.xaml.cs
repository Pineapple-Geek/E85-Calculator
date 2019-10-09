using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E85_Calculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuration : ContentPage
    {
        string chemin = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Configuration.bin");
        double PrixE85 = 0.0, PrixSP95 = 0.0, Reservoir = 0;
        public Configuration()
        {
            InitializeComponent();
        }

        private async void Valider_Clicked(object sender, EventArgs e)
        {
            if (File.Exists(chemin))
            {
                File.Delete(chemin);
            }

            List<Parametre> listEnregistrement = new List<Parametre>();
            Parametre enregistrement = new Parametre();
            PrixE85 = Double.Parse(tbE85.Text);
            PrixSP95 = Double.Parse(tbSP95.Text);
            Reservoir = Double.Parse(tbReservoir.Text);
            enregistrement.PrixE85 = PrixE85;
            enregistrement.PrixSP95 = PrixSP95;
            enregistrement.Reservoir = Reservoir;
            listEnregistrement.Add(enregistrement);
            Serialisation.Serialiser(listEnregistrement, chemin);
            await DisplayAlert("Information", "Enregistrement terminer", "OK");
            await Navigation.PopToRootAsync();
        }

        private async void Annuler_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
