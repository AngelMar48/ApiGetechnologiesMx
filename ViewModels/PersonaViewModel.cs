using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfCliente.Models;
using WpfCliente.MVVM;
using static System.Net.WebRequestMethods;

namespace WpfCliente.ViewModels
{
    public class PersonaViewModel : ViewModelBase
    {
        //public Persona User { get; set; }


        public ICommand SaveCommand { get; set; }
        public RelayCommand AddCommand => new RelayCommand(execute => AddItem());

        public PersonaViewModel()
        {


        }

        private bool CanSave()
        {
            IsVisibleNombre = string.IsNullOrWhiteSpace(NewName) ? "Visible" : "Hidden";
            IsVisible = string.IsNullOrWhiteSpace(NewApellidoPat) ? "Visible" : "Hidden";
            IsVisibleApellidoMat = string.IsNullOrWhiteSpace(NewApellidoMat) ? "Visible" : "Hidden";
            IsVisibleIdentificacion = string.IsNullOrWhiteSpace(NewIdentificacion) ? "Visible" : "Hidden";
            if (IsVisibleNombre == "Visible" || IsVisible == "Visible" || IsVisibleApellidoMat == "Visible" || IsVisibleIdentificacion == "Visible")
                return false;
            else
                return true;
        }


        #region Binding Texbox
        private string _newName;
        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                OnPropertyChanged();

            }
        }

        private string _newApellidoPat;
        public string NewApellidoPat
        {
            get => _newApellidoPat;
            set
            {
                _newApellidoPat = value;
                OnPropertyChanged();
            }
        }

        private string _newApellidoMat;
        public string NewApellidoMat
        {
            get => _newApellidoMat;
            set
            {
                _newApellidoMat = value;
                OnPropertyChanged();
            }
        }

        private string _newIdentificacion;
        public string NewIdentificacion
        {
            get => _newIdentificacion;
            set
            {
                _newIdentificacion = value;
                OnPropertyChanged();

            }
        }
        #endregion



        #region Validación Campos
        private string _isVisibleNombre = "Hidden";
        public string IsVisibleNombre
        {
            get { return _isVisibleNombre; }
            set { SetProperty(ref _isVisibleNombre, value, nameof(IsVisibleNombre)); }
        }


        private string _isVisible = "Hidden";
        public string IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value, nameof(IsVisible)); }
        }


        private string _isVisibleApellidoMat = "Hidden";
        public string IsVisibleApellidoMat
        {
            get { return _isVisibleApellidoMat; }
            set { SetProperty(ref _isVisibleApellidoMat, value, nameof(IsVisibleApellidoMat)); }
        }


        private string _isVisibleIdentificacion = "Hidden";
        public string IsVisibleIdentificacion
        {
            get { return _isVisibleIdentificacion; }
            set { SetProperty(ref _isVisibleIdentificacion, value, nameof(IsVisibleIdentificacion)); }
        }
        #endregion


        private async void AddItem()
        {
            if (CanSave())
            {
                Persona person = new Persona();
                person.Id = Guid.NewGuid();
                person.Nombre = NewName;
                person.ApellidoPaterno = NewApellidoPat;
                person.ApellidoMaterno = NewApellidoMat;
                person.Identificacion = NewIdentificacion;

                string jsonBody = JsonConvert.SerializeObject(person);

               

                // Crear una instancia de HttpClient
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        // Configurar el encabezado Content-Type
                        client.DefaultRequestHeaders.Add("ContentType", "application/json");

                        // Crear el contenido de la solicitud POST
                        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                        // Enviar la solicitud POST y obtener la respuesta
                        HttpResponseMessage response = await client.PostAsync("https://localhost:7075/api/Directorio", content);

                        // Verificar si la solicitud fue exitosa (código de estado 200)
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Solicitud POST exitosa!");
                        }
                        else
                        {
                            MessageBox.Show($"Error en la solicitud POST: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al enviar la solicitud POST: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show($"Usuario No creado. {NewName}");
            }

        }


    

    }
}
