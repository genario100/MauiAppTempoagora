using MauiAppTempoagora.Models;

namespace MauiAppTempoagora
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }// Fim do construtor

        private async void OnCounterClicked(object? sender, EventArgs e)
        {

            try
            {

                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    
                Tempo? t= await Services.DataService.GetPrevisao(txt_cidade.Text.Trim());
                    if(t != null)
                    {
                        string dados_previsao = $"Cidade: {txt_cidade.Text}\n" +
                                                $"Descrição: {t.description}\n" +
                                                $"Temperatura Mínima: {t.temp_min} °C\n" +
                                                $"Temperatura Máxima: {t.temp_max} °C\n" +
                                                $"Visibilidade: {t.visibility} metros\n" +
                                                $"Nascer do Sol: {t.sunrise}\n" +
                                                $"Pôr do Sol: {t.sunset}\n" +
                                                $"Velocidade do Vento: {t.speed} m/s\n" +
                                                $"Coordenadas: Lon {t.lon}, Lat {t.lat}";

                        lbl_resp.Text = dados_previsao;
                    }
                    else
                    {
                      lbl_resp.Text =  "Não foi possível obter a previsão para a cidade informada.";
                    }

                }
                else
                {
                    await DisplayAlert("Aviso", "Por favor, insira o nome de uma cidade.", "OK");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }// Fim do try catch



        }// Fim do OnCounterClicked
    }// Fim da classe MainPage
}// Fim do namespace MauiAppTempoagora
