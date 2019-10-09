using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace E85_Calculator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        List<Parametre> parametre = new List<Parametre>();
        string chemin = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Configuration.bin");
        double PrixE85 = 0.0, PrixSP95 = 0.0, chiffrePrix = 0, Reservoir = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        public void Parametres()
        {
            if (File.Exists(chemin) == true)
            {
                parametre.Clear();
                parametre = (List<Parametre>)Serialisation.Deserialiser(chemin);
                PrixE85 = parametre[0].PrixE85;
                PrixSP95 = parametre[0].PrixSP95;
                Reservoir = parametre[0].Reservoir;
            }
        }

        private void SliderPourcentage_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double Value = e.NewValue;

            double prix1 = (chiffrePrix * Value) / 100;
            double prix2 = chiffrePrix - prix1;

            double litreE85 = prix1 / PrixE85;
            double litreSP95 = prix2 / PrixSP95;
            double totalLitres = litreE85 + litreSP95;

            double pourcentage1 = Value;
            double pourcentage2 = 0;

            double pourcentageReservoir = (totalLitres * 100) / Reservoir;

            if (pourcentage1 == 100)
            {
                lbPE85.Text = "% (E85) : " + 100 + " %";
                lbPSP95.Text = "% (SP95) : " + 0 + " %";

                lbPrixE85.Text = "Prix (E85) : " + prix1.ToString() + " €";
                lbPrixSP95.Text = "Prix (SP95) : " + 0 + " €";

                lbLTRE85.Text = "Litres (E85) : " + litreE85.ToString(".##") + " L";
                lbLTRSP95.Text = "Litres (SP95) : " + 0 + " L";
            }
            else if (pourcentage1 == 0)
            {
                lbPE85.Text = "% (E85) : " + 0 + " %";
                lbPSP95.Text = "% (SP95) : " + 100 + " %";

                lbPrixE85.Text = "Prix (E85) : " + 0 + " €";
                lbPrixSP95.Text = "Prix (SP95) : " + prix2.ToString() + " €";

                lbLTRE85.Text = "Litres (E85) : " + 0 + " L";
                lbLTRSP95.Text = "Litres (SP95) : " + litreSP95.ToString(".##") + " L";
            }
            else
            {
                pourcentage2 = 100 - pourcentage1;

                lbPE85.Text = "% (E85) : " + pourcentage1.ToString(".##") + " %";
                lbPSP95.Text = "% (SP95) : " + pourcentage2.ToString(".##") + " %";

                lbPrixE85.Text = "Prix (E85) : " + prix1.ToString(".##") + " €";
                lbPrixSP95.Text = "Prix (SP95) : " + prix2.ToString(".##") + " €";

                lbLTRE85.Text = "Litres (E85) : " + litreE85.ToString(".##") + " L";
                lbLTRSP95.Text = "Litres (SP95) : " + litreSP95.ToString(".##") + " L";
            }

            progressLitres.Progress = Math.Round(pourcentageReservoir) / 100;
            lbTotalL.Text = "Total Litres : " + totalLitres.ToString(".##") + " L" + " - " + pourcentageReservoir.ToString("##.") + "%";
    }

        private async void Configuration(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Configuration());
        }

        private void Somme(object sender, EventArgs e)
        {
            if (File.Exists(chemin))
            {
                chiffrePrix = Convert.ToDouble(entrySomme.Text);
                sliderPourcentage.IsEnabled = true;
                Parametres();
            }
            else
            {
                DisplayAlert("Information", "Veuillez d'abord aller dans la page configuration!", "OK");
            } 
        }
    }
}
