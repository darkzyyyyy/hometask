using App4.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private readonly PageViewmodel viewmodel = new PageViewmodel();
        public Page1()
        {
            InitializeComponent();
            BindingContext = viewmodel;
        }
    }
}